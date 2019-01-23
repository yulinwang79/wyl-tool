
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace WYL
{
    class MTKResourceClass
    {
        [DllImport("zlib4.dll ")]
        unsafe public static extern Int32 compress2(byte *dest,UInt32 *destLen,byte *source,UInt32 sourceLen,Int32 level);
        [DllImport("zlib4.dll ")]
        unsafe public static extern Int32 uncompress(byte* dest, UInt32* destLen, byte* source, UInt32 sourceLen);

        string m_filename;
        string m_directory;

        byte[] m_bin;
        bool m_isCompressed;
        public MTKRegionInfo g_regionInfo = new MTKRegionInfo( );
        public MTKCustpack2ndJumpTbl g_custpack2ndJumpTbl;    
        public MTKLangpack2ndJumpTbl g_langpack2ndJumpTbl;
        public MTKImage2ndJumpTbl g_image2ndJumpTbl;

        NVRAM_MMI_CACHE_DEFAULT m_nvram_byte_cache;
        NVRAM_MMI_CACHE_DEFAULT m_nvram_short_cache;
        NVRAM_MMI_CACHE_DEFAULT m_nvram_double_cache;
        UInt32 IMAGE_ROM_BASE_FLAG = 0xFF000000;
        
        UInt32 Image__ROM__Base=0;
        public MTKResourceClass(string filename)
        {
            FileStream fs;
            m_directory = DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "_").Replace("\\", "").Replace("/", "");
            DirectoryInfo dir = new DirectoryInfo(m_directory);
            dir.Create();
            m_filename = m_directory +"\\tmp";
            File.Copy(filename, m_filename);
            using (fs = File.OpenRead(m_filename))
            {
                byte[] b = new byte[fs.Length];
                if (fs.Read(b, 0, (int)fs.Length) == fs.Length)
                {
                    m_bin = b;
                    g_regionInfo.LoadBin(b);
                    MTKResource_get_resource();
                    
                    if(g_regionInfo.m_load_successed)
                    {
                        Image__ROM__Base = g_regionInfo.Image__ROM__Base;
                        resource_get_custpack2ndJumpTbl(m_bin);
                        resource_get_langpack2ndJumpTbl(m_bin);
                        resource_get_image2ndJumpTbl(m_bin);
                    }
                }
            }
        }
		public MTKResourceClass(string filename,bool font)
        {
            FileStream fs;
            m_directory = DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "_").Replace("\\", "").Replace("/", "");
            DirectoryInfo dir = new DirectoryInfo(m_directory);
            dir.Create();
            m_filename = m_directory +"\\tmp";
            File.Copy(filename, m_filename);
            using (fs = File.OpenRead(m_filename))
            {
                byte[] b = new byte[fs.Length];
                if (fs.Read(b, 0, (int)fs.Length) == fs.Length)
                {
                    m_bin = b;
                    g_regionInfo.LoadBin(b);
                    //MTKResource_get_resource();
                    
                    if(g_regionInfo.m_load_successed)
                    {
                        Image__ROM__Base = g_regionInfo.Image__ROM__Base;
                    }
					else
					{
						g_regionInfo.Image__ROM__Base=	Image__ROM__Base = 0x08000000;
					}
					resource_get_fontres(g_regionInfo.m_bin);
                }
            }
        }

        bool check_addr(UInt32 addr)
        {
            if((addr& IMAGE_ROM_BASE_FLAG) != Image__ROM__Base)    
                return false;

            if(addr - Image__ROM__Base >= m_bin.Length)    
                return false;
                
            return true;
        }

        UInt32 Address_Converter_And_Check(int offset)
        {
            UInt32 addr = BitConverter.ToUInt32(m_bin, offset);
            
            if((addr& IMAGE_ROM_BASE_FLAG) != Image__ROM__Base)    
                return 0;

            if(addr - Image__ROM__Base >= m_bin.Length)    
                return 0;
                
            return addr;
        }

        
        Int32 resource_uncompress(byte[] dest, UInt32 destLen, byte[] source, UInt32 offset,UInt32 sourceLen)
        {
            Int32 ret;
            unsafe
            {
                fixed (byte* ptr = &dest[0])
                {
                    fixed (byte* ptr1 = &source[offset + 4])
                    {
                        ret = uncompress(ptr, &destLen, ptr1, sourceLen);
                    }
                }
            }
            return ret;
        }
        Int32 resource_compress(byte[] dest, UInt32 offset, UInt32 destLen, byte[] source, UInt32 sourceLen)
        {
            Int32 ret;
            unsafe
            {
                fixed (byte* ptr = &dest[offset + 4])
                {
                    fixed (byte* ptr1 = &source[0])
                    {
                        /*Z_NO_COMPRESSION         0
                        Z_BEST_SPEED             1
                        Z_BEST_COMPRESSION       9
                        Z_DEFAULT_COMPRESSION  (-1)*/

                        ret = compress2(ptr,&destLen,ptr1,sourceLen,9);
                    }
                }
            }
            
            dest[0+offset] = (byte)(destLen >> 24);
            dest[1+offset] = (byte)(destLen >> 16); 
            dest[2+offset] = (byte)(destLen >> 8);
            dest[3+offset] = (byte)destLen;
            return ret;
            
        }
        bool resource_check_custpacktbl_item(byte[] bin,UInt32 rom_base,UInt32 rom_size,UInt32 addr)
        {    
            UInt32 offset;
	        CUSTPACK_DATA_HEADER1 image_header1;
	        CUSTPACK_DATA_HEADER2 image_header2;
            image_header1 = new CUSTPACK_DATA_HEADER1();
            image_header2 = new CUSTPACK_DATA_HEADER2();

            if(!(check_addr(addr)))    
                return false;
                
            offset = addr - Image__ROM__Base;
            
            if(offset < rom_base || offset >= rom_base + rom_size-8)
                return false;

            image_header1.MaxImageNumEXT = BitConverter.ToUInt16(bin, (int)offset);
            image_header1.CustImageNamesEXT = BitConverter.ToUInt32(bin, (int)offset + 2);

            image_header2.MaxImageNumEXT = BitConverter.ToUInt32(bin, (int)offset);
            image_header2.CustImageNamesEXT = BitConverter.ToUInt32(bin, (int)offset + 4);

            if(( image_header1.MaxImageNumEXT== 0) ||(UInt32)offset - image_header1.MaxImageNumEXT *4   != (UInt32)(image_header1.CustImageNamesEXT-Image__ROM__Base))
            {
                if( ( image_header2.MaxImageNumEXT== 0) ||(UInt32)offset - image_header2.MaxImageNumEXT *4  != (UInt32)(image_header2.CustImageNamesEXT-Image__ROM__Base))
                {
                    return false;
                }
            }
            return true;
        }
        
        bool resource_get_custpack2ndJumpTbl(byte[] bin)
        {
	        int index,i=0;

	        UInt32 rom_base = 0;
                UInt32 rom_size = (UInt32)m_bin.Length;

	        int offset;

	        g_custpack2ndJumpTbl = null;

                MTKCustpack2ndJumpTbl p_custpack2ndJumpTbl = new MTKCustpack2ndJumpTbl();
                CUSTPACK_NVRAM_HEADER nvram_header = new CUSTPACK_NVRAM_HEADER();
                CUSTPACK_DATA_HEADER image_header = new CUSTPACK_DATA_HEADER();
                CUSTPACK_DATA_HEADER1 image_header1 = new CUSTPACK_DATA_HEADER1();
                CUSTPACK_DATA_HEADER2 image_header2 = new CUSTPACK_DATA_HEADER2();
                NVRAM_MMI_CACHE_DEFAULT byte_cache = new NVRAM_MMI_CACHE_DEFAULT();
                NVRAM_MMI_CACHE_DEFAULT short_cache = new NVRAM_MMI_CACHE_DEFAULT();
                NVRAM_MMI_CACHE_DEFAULT double_cache = new NVRAM_MMI_CACHE_DEFAULT();

	        for(index = (int)rom_base; index < rom_base+rom_size - 50; index++)
	        {

                    p_custpack2ndJumpTbl.mtk_theme_header =BitConverter.ToUInt32(bin, index);
                    p_custpack2ndJumpTbl.mtk_image_header =BitConverter.ToUInt32(bin, index + 4);
                    p_custpack2ndJumpTbl.custpack_audio =BitConverter.ToUInt32(bin, index + 8);
                    p_custpack2ndJumpTbl.mtk_audio_header =BitConverter.ToUInt32(bin, index + 12);
                    p_custpack2ndJumpTbl.custpack_nvram_ptr =BitConverter.ToUInt32(bin, index + 16);

                    if(!g_regionInfo.m_load_successed)
                        Image__ROM__Base = p_custpack2ndJumpTbl.mtk_theme_header & IMAGE_ROM_BASE_FLAG;

                    if(!(Image__ROM__Base == 0 || Image__ROM__Base==0x08000000|| Image__ROM__Base==0xf0000000))
                    {
                        continue;
                    }
                    
                    if(!resource_check_custpacktbl_item(bin,rom_base,rom_size,p_custpack2ndJumpTbl.mtk_image_header)
                    //||!resource_check_custpacktbl_item(bin,rom_base,rom_size,p_custpack2ndJumpTbl.mtk_theme_header)
                        ||!resource_check_custpacktbl_item(bin,rom_base,rom_size,p_custpack2ndJumpTbl.mtk_audio_header)
                        )
                    {
                        continue;
                    }
                    i=0;
                    
                    offset = (int)(p_custpack2ndJumpTbl.custpack_nvram_ptr - Image__ROM__Base);
                    
                    if(!(check_addr(p_custpack2ndJumpTbl.custpack_nvram_ptr)))    
                    {
                        continue;
                    }
                    nvram_header.version=BitConverter.ToUInt32(bin, (int)offset + ((i++) <<2));
                    
                    if((nvram_header.COMMON_NVRAM_EF_ALS_LINE_ID_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_MSCAP_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.NVRAM_EF_MMI_CACHE_BYTE_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.NVRAM_EF_MMI_CACHE_SHORT_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.NVRAM_EF_MMI_CACHE_DOUBLE_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_PHB_SOS_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_SMSAL_MAILBOX_ADDR_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_SMSAL_COMMON_PARAM_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_CB_DEFAULT_CH_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_SETTING_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_MS_SECURITY_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_RAC_PREFERENCE_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_INET_CUSTPACK_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 
                    if((nvram_header.COMMON_NVRAM_EF_WAP_BOOKMARK_LIST_DEFAULT = Address_Converter_And_Check((int)offset+ ((i++) <<2)))==0) { continue; } 

                    
                    byte_cache.m_base = nvram_header.NVRAM_EF_MMI_CACHE_BYTE_DEFAULT - Image__ROM__Base;
                    byte_cache.m_len = (UInt32)((bin[byte_cache.m_base] << 24) + (bin[1 + byte_cache.m_base] << 16) + (bin[2 + byte_cache.m_base] << 8) + bin[3 + byte_cache.m_base]);

                    short_cache.m_base = nvram_header.NVRAM_EF_MMI_CACHE_SHORT_DEFAULT - Image__ROM__Base;
                    short_cache.m_len = (UInt32)((bin[short_cache.m_base] << 24) + (bin[1 + short_cache.m_base] << 16) + (bin[2 + short_cache.m_base] << 8) + bin[3 + short_cache.m_base]);

                    double_cache.m_base = nvram_header.NVRAM_EF_MMI_CACHE_DOUBLE_DEFAULT - Image__ROM__Base;
                    double_cache.m_len = (UInt32)((bin[double_cache.m_base] << 24) + (bin[1 + double_cache.m_base] << 16) + (bin[2 + double_cache.m_base] << 8) + bin[3 + double_cache.m_base]);

                    if(byte_cache.m_base + 512== short_cache.m_base 
                        && short_cache.m_base + 512== double_cache.m_base)
                    {
                        m_isCompressed = false;
                        byte_cache.m_len = short_cache.m_len = double_cache.m_len = 512;
                        Array.Copy(bin,byte_cache.m_base,byte_cache.m_data,0, 512);
                        Array.Copy(bin,short_cache.m_base,short_cache.m_data,0, 512);
                        Array.Copy(bin,double_cache.m_base,double_cache.m_data,0, 512);
                    }
                    else if(byte_cache.m_base + (byte_cache.m_len +4)== short_cache.m_base 
                        && short_cache.m_base + (short_cache.m_len +4)== double_cache.m_base)
                    {
                        m_isCompressed = true;
                        if(resource_uncompress(byte_cache.m_data,512,bin,byte_cache.m_base,byte_cache.m_len)!=0
                            ||resource_uncompress(short_cache.m_data,512,bin,short_cache.m_base,short_cache.m_len)!=0
                            ||resource_uncompress(double_cache.m_data,512,bin,double_cache.m_base,double_cache.m_len)!=0)
                            continue;
                            
                    }
                    else
                    {
                        continue;
                    }
                    
                   g_custpack2ndJumpTbl = p_custpack2ndJumpTbl;
		        break;
	        }
	        if(g_custpack2ndJumpTbl!=null)
		        return false;
	        else
		        return true;
        }
        
        int resource_get_langpack2ndJumpTbl(byte[] bin)
        {
            UInt32 index;
           int i;
           UInt32 rom_base = 0;
           UInt32 rom_size = (UInt32)m_bin.Length;
           int offset;

            UInt32   mtk_gLanguageArray_addr;//                       (void*) mtk_gLanguageArray, 
            UInt32   mtk_gMaxDeployedLangs_addr;//                    (void*) &mtk_gMaxDeployedLangs,
            UInt32   mtk_gStringList_addr;//                  (void*) mtk_gStringList,
            UInt32   mtk_gIMEModeArray_addr;//                    (void*) mtk_gIMEModeArray,
            UInt32   mtk_gIMEQSearchModeArray_addr; // (void*) mtk_gIMEQSearchModeArray
            UInt32   mtk_gIMELDBArray_addr;//                                       ,(void*) mtk_gIMELDBArray,
            UInt32   mtk_gIMEModule_addr;   //(void*) &mtk_gIMEModule
            UInt32   tmp1; //                                      ,0
            UInt32   tmp2; //                         ,0
            UInt32   mtk_nCustMenus_addr;//                                      ,(void*) mtk_nCustMenus//071706 menu resource

	        
            MTKLangpack2ndJumpTbl p_langpack2ndJumpTbl= new MTKLangpack2ndJumpTbl();
            sLanguageDetails gLanguageArray_header= new sLanguageDetails();
            g_langpack2ndJumpTbl = null;
            for(index = 0; index < rom_size - 50; index+=4)
            {
                i=0;
                if((mtk_gLanguageArray_addr = Address_Converter_And_Check((int)index+ ((i++) <<2)))==0) { continue; } 
                if((mtk_gMaxDeployedLangs_addr = Address_Converter_And_Check((int)index+ ((i++) <<2)))==0){ continue; }
                //if((mtk_gStringList_addr = Address_Converter_And_Check((int)index+ ((i++) <<2)))==0) { continue; }   
                //if((mtk_gIMEModeArray_addr = Address_Converter_And_Check((int)index+ ((i++) <<2)))==0)  { continue; }  
                //if((mtk_gIMEQSearchModeArray_addr = Address_Converter_And_Check((int)index+ ((i++) <<2)))==0)  { continue; }  
                //if((mtk_gIMELDBArray_addr = Address_Converter_And_Check((int)index+ ((i++) <<2)))==0)  { continue; }
                //mtk_gIMEModule_addr = BitConverter.ToUInt32(bin, (int)index + ((i++) << 2)); //
                //tmp1=BitConverter.ToUInt32(bin, (int)index+ ((i++) <<2)); //                                      ,0
                //tmp2=BitConverter.ToUInt32(bin, (int)index+ ((i++) <<2)); //                         ,0
                //if((mtk_nCustMenus_addr = Address_Converter_And_Check((int)index+ ((i++) <<2)))==0)  { continue; }  
       
        
                offset = (int)(mtk_gLanguageArray_addr - Image__ROM__Base);

                Array.Copy(m_bin,offset,gLanguageArray_header.aName,0,gLanguageArray_header.aName.Length);
                offset +=gLanguageArray_header.aName.Length;
                Array.Copy(m_bin,offset,gLanguageArray_header.aLangSSC,0,gLanguageArray_header.aLangSSC.Length);
                offset +=gLanguageArray_header.aLangSSC.Length;
                gLanguageArray_header.nCurrentFamily = m_bin[offset];
                offset ++;
                offset =((offset + 3) >> 2) << 2;
                for(i=0;i<gLanguageArray_header.fontfamilyList.Length;i++)
                {
                    //gLanguageArray_header.fontfamilyList[i] = BitConverter.ToUInt32(m_bin, offset);
                    offset +=4;
                }
                Array.Copy(m_bin,offset,gLanguageArray_header.aLangCountryCode,0,gLanguageArray_header.aLangCountryCode.Length);
   

                if( string.Compare("*#0044#",System.Text.Encoding.Default.GetString(gLanguageArray_header.aLangSSC))!=0 
                    //||string.Compare("en",0,System.Text.Encoding.Default.GetString(gLanguageArray_header.aLangCountryCode),0,2)!=0
                   )// || gLanguageArray_header.nCurrentFamily > gLanguageArray_header.fontfamilyList.Length)
                {
                    continue;
                }
                /*
                offset = (int)(mtk_gMaxDeployedLangs_addr - Image__ROM__Base);
                p_langpack2ndJumpTbl.mtk_gLanguageArray = new sLanguageDetails[(int)BitConverter.ToUInt16(bin, offset)];

                offset =  (int)(mtk_gLanguageArray_addr - Image__ROM__Base);
                for(i =0; i<p_langpack2ndJumpTbl.mtk_gLanguageArray.Length; i++ )
                {
                    p_langpack2ndJumpTbl.mtk_gLanguageArray[i] = new sLanguageDetails(g_regionInfo,ref offset);
                }

                if(g_regionInfo.m_bin_type>= MTKRegionInfo.BIN_Type.MTK6276_11A)
                {
                    p_langpack2ndJumpTbl.fontfamilyList = new sFontFamily[6];
                    offset = (g_regionInfo.GetBinOffset(mtk_gLanguageArray_addr) - (6 * 8)) >> 2 << 2;
                    offset =((offset + 3) >> 2) << 2;
                    int id=0;
                    for(i=0; i< 6; i++)
                    {
                        p_langpack2ndJumpTbl.fontfamilyList[i] = new sFontFamily(g_regionInfo, ref offset);
                    }
                    p_langpack2ndJumpTbl.m_bin_offset = index;
                    {
                        index = 0;
                        DirectoryInfo dir = new DirectoryInfo(m_directory+"\\resource_font");
                        dir.Create();
                        for(i=0; i< p_langpack2ndJumpTbl.fontfamilyList.Length; i++)
                        {
                            {
                                DirectoryInfo dir1 = new DirectoryInfo(m_directory + "\\resource_font\\cust" + i);
                                dir1.Create();
                            }
                            for(int j=0; j< p_langpack2ndJumpTbl.fontfamilyList[i].nTotalFonts; j++)
                            {
                                BdfClass bdf = new BdfClass();
                                bdf.LoadData(p_langpack2ndJumpTbl.fontfamilyList[i].DatafontData[j]);
                                bdf.SaveFile(m_directory+"\\resource_font\\cust" + i + "\\cust"+ index + ".bdf");
                                index ++;
                            }
                        }
                    }

                }   
                else
                {
                    p_langpack2ndJumpTbl.fontfamilyList = new sFontFamily[p_langpack2ndJumpTbl.mtk_gLanguageArray.Length];
                    
                    for(i =0; i<p_langpack2ndJumpTbl.mtk_gLanguageArray.Length; i++ )
                    {
                        p_langpack2ndJumpTbl.fontfamilyList[i] = p_langpack2ndJumpTbl.mtk_gLanguageArray[i].fontfamilyList[p_langpack2ndJumpTbl.mtk_gLanguageArray[i].nCurrentFamily];
                    }

                    p_langpack2ndJumpTbl.m_bin_offset = index;
                    {
                    index = 0;
                    DirectoryInfo dir = new DirectoryInfo(m_directory+"\\resource_font");
                    dir.Create();
                    for(i=0; i< p_langpack2ndJumpTbl.fontfamilyList.Length; i++)
                    {
                        UTF8Encoding temp = new UTF8Encoding(true);
                        string langSSC = temp.GetString(p_langpack2ndJumpTbl.mtk_gLanguageArray[i].aLangSSC).Substring(1,6);
                        {
                            DirectoryInfo dir1 = new DirectoryInfo(m_directory+"\\resource_font\\"+langSSC);
                            dir1.Create();
                        }
                        for(int j=0; j< p_langpack2ndJumpTbl.fontfamilyList[i].nTotalFonts; j++)
                        {
                            BdfClass bdf = new BdfClass();
                            bdf.LoadData(p_langpack2ndJumpTbl.fontfamilyList[i].DatafontData[j]);
                            bdf.SaveFile(m_directory+"\\resource_font\\"+langSSC+"\\cust"+ index + ".bdf");
                            index ++;
                        }
                    }
                    }                
                }
*/
                g_langpack2ndJumpTbl = p_langpack2ndJumpTbl;
                break;
            }
            if(g_langpack2ndJumpTbl!=null)
                return 1;
            else
                return 0;
        }
        int resource_get_image2ndJumpTbl(byte[] bin)
        {
            UInt32 index;
           int i;
           UInt32 addr = 0;
           UInt16 image_count = 0;
           int offset;
           MTKImage2ndJumpTbl p_image2ndJumpTbl = new MTKImage2ndJumpTbl();


                if(g_langpack2ndJumpTbl!=null)
                {
                    p_image2ndJumpTbl.m_bin_offset = g_langpack2ndJumpTbl.m_bin_offset - 8; 
                    addr = BitConverter.ToUInt32(m_bin, (int)p_image2ndJumpTbl.m_bin_offset + 4); //
                    image_count = BitConverter.ToUInt16(m_bin, g_regionInfo.GetBinOffset(addr));
                    p_image2ndJumpTbl.m_imageOffset = new UInt32[image_count];
                    
                    addr = BitConverter.ToUInt32(m_bin, (int)p_image2ndJumpTbl.m_bin_offset); //
                    offset = g_regionInfo.GetBinOffset(addr);
//                    byte[] b = new byte[image_count*4];
                    for(i = 0; i < image_count; i++)
                    {
                        p_image2ndJumpTbl.m_imageOffset[i] = g_regionInfo.GetBinOffsetU(BitConverter.ToUInt32(m_bin, (int)offset + ((i) <<2)));
                     
//                         b[i<<2] = (byte)(p_image2ndJumpTbl.m_imageOffset[i]);
//                         b[(i<<2) + 1] = (byte)(p_image2ndJumpTbl.m_imageOffset[i] >> 8);
//                         b[(i<<2) + 2] = (byte)(p_image2ndJumpTbl.m_imageOffset[i] >> 16);
//                         b[(i<<2) + 3] = (byte)(p_image2ndJumpTbl.m_imageOffset[i] >> 24);
                    }

                    g_image2ndJumpTbl=p_image2ndJumpTbl;
                    return 1;
                }
            else
                return 0;
        }
		int resource_get_fontres(byte[] bin)
        {
            UInt32 index;
           int i;
           UInt32 rom_base = 0;
           UInt32 rom_size = (UInt32)m_bin.Length;
           int offset;
			//resource_get_langpack2ndJumpTbl();

           for (index = 0; index < rom_size - 50; index += 4)
           {

               sCustFontData tmp = new sCustFontData();
               offset = (int)index;
               if (tmp.CheckData(g_regionInfo, offset))
               {
                   //new sCustFontData(g_regionInfo, ref offset);
                   offset = (int)index;
				   BdfClass bdf = new BdfClass();
				   try{
				   if(bdf.LoadData(new sCustFontData(g_regionInfo, ref offset)))
				       bdf.SaveFile(m_directory+"\\cust"+ index + ".bdf");
				   	}
				   catch
				   	{
					   continue;
				   }
               }
               else
               {
                   continue;
               }
           }
           return 0;
        }
        int MTKResource_get_resource()
        {
              if(g_regionInfo.m_load_successed)
              {
                  byte[] b = new byte[g_regionInfo.g_region.Length*4];
                  for(int i = 0; i < g_regionInfo.g_region.Length; i++)
                  {
                       b[i<<2] = (byte)(g_regionInfo.g_region[i]);
                       b[(i<<2) + 1] = (byte)(g_regionInfo.g_region[i] >> 8);
                       b[(i<<2) + 2] = (byte)(g_regionInfo.g_region[i] >> 16);
                       b[(i<<2) + 3] = (byte)(g_regionInfo.g_region[i] >> 24);
                  }
                  File.WriteAllBytes(System.Windows.Forms.Application.StartupPath + "\\" +m_directory +  "\\region_temp",b);
              }
              Process p = new Process();
              p.StartInfo.FileName = "cmd.exe";
              p.StartInfo.UseShellExecute = false;
              p.StartInfo.RedirectStandardInput = true;
              p.StartInfo.RedirectStandardOutput = true;
              p.StartInfo.RedirectStandardError = true;
              p.StartInfo.CreateNoWindow = true;
              p.Start();
              p.StandardInput.WriteLine("cd " + m_directory);
              p.StandardInput.WriteLine(".\\..\\resexp.exe " + System.Windows.Forms.Application.StartupPath + "\\" +m_filename,System.Windows.Forms.Application.StartupPath + "\\" +m_directory +  "\\region_temp");
//              p.StandardInput.WriteLine("D:\\resExp_code\\Trunk\\MS_Code\\MoDIS_VC9\\MoDIS\\Debug\\resexp.exe " + System.Windows.Forms.Application.StartupPath + "\\" + m_filename + " " + System.Windows.Forms.Application.StartupPath + "\\" + m_directory + "\\region_temp");
              p.StandardInput.WriteLine("exit");
              File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + m_directory + "\\region_temp");
              string strRst = p.StandardOutput.ReadToEnd();
              if (strRst.IndexOf("Done!") != -1)
              {
                    return 1;
              }
              return 0;
         }
    }
}

