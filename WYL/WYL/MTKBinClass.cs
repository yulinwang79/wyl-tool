﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WYL
{
    public class mmi_font_range_offset_struct
    {
        public UInt16 num_of_block;
        public UInt16 block_index_array;
        public UInt16 rangeOffset;
    } 

    public class RangeDetails
    {
        public UInt16 nNoOfRanges;
        public RangeData pRangeData;
        public RangeDetails()
        {

        }
        public RangeDetails(MTKRegionInfo regionInfo, ref int offset)
        {
             
             nNoOfRanges=BitConverter.ToUInt16(regionInfo.m_bin, offset);
             offset +=2;
             offset += 2;
            int offsetTmp = (int)(BitConverter.ToUInt32(regionInfo.m_bin, offset) - regionInfo.Image__ROM__Base);
            pRangeData = new RangeData(regionInfo.m_bin, ref offsetTmp, (int)nNoOfRanges);
             offset +=4;
        }

    }

   public class sCustFontData
    {
        public byte nHeight;
        public byte nWidth;
        public byte nAscent;
        public byte nDescent;
        public byte nEquiDistant;
        public byte nCharBytes;
        public UInt16 nMaxChars;
        public byte[] pDWidthArray;
        public byte[] pWidthArray;
        public UInt16[] pOffsetArrayC;
        public UInt32[] pOffsetArray;
        public byte[] pDataArray;
        public mmi_font_range_offset_struct[] pRangeC;
        public UInt32[] pRange;
        public UInt16[] pFontType;
        public RangeDetails pRangeDetails;
        
        public int bin_offset =0;
		
		MTKRegionInfo m_regionInfo;
        public sCustFontData()
        {

        }
        public sCustFontData(string text)
        {
            char[] stringSeparators = new char[] { ' ', '\n', '\t', '\r', ',', '{', '}' };
            string[] item = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            nHeight = byte.Parse(item[0]);
            nWidth = byte.Parse(item[1]);
            nAscent = byte.Parse(item[2]);
            nDescent = byte.Parse(item[3]);
            nEquiDistant = byte.Parse(item[4]);
            nMaxChars = UInt16.Parse(item[5]);
        }
        public sCustFontData(MTKRegionInfo regionInfo, ref int offset)
        {
            
            bin_offset = offset;
            nHeight= regionInfo.m_bin[offset++];
            nWidth= regionInfo.m_bin[offset++];
            nAscent= regionInfo.m_bin[offset++];
            nDescent= regionInfo.m_bin[offset++];
            nEquiDistant= regionInfo.m_bin[offset++];
            nCharBytes= regionInfo.m_bin[offset++];
            nMaxChars=BitConverter.ToUInt16(regionInfo.m_bin, offset);
            offset+=2;
            UInt32 sDWidthArrayAddr = BitConverter.ToUInt32(regionInfo.m_bin, offset);
            offset+=4;
            UInt32 sWidthArrayAddr = BitConverter.ToUInt32(regionInfo.m_bin, offset);
            offset+=4;
            UInt32 sOffsetArrayAddr = BitConverter.ToUInt32(regionInfo.m_bin, offset);
            offset+=4;
            UInt32 sDataArrayAddr = BitConverter.ToUInt32(regionInfo.m_bin, offset);
            offset+=4;
            UInt32 sRangeAddr = BitConverter.ToUInt32(regionInfo.m_bin, offset);
            offset+=4;
//            UInt32 sRangeInfo = regionInfo.Address_Converter_And_Check(offset);
//            offset+=4;
/*
            pFontType = new UInt16[6*2];
            for(int i=0; i<6*2; i++)
            {
                pFontType[i]=BitConverter.ToUInt16(regionInfo.m_bin, offset);
                offset +=2;
            }
*/            int offsetTmp = (int)(BitConverter.ToUInt32(regionInfo.m_bin, offset)- regionInfo.Image__ROM__Base);

            pRangeDetails = new RangeDetails(regionInfo, ref offsetTmp);

            if(sDWidthArrayAddr!=0)
            {
                pDWidthArray = new byte[pRangeDetails.pRangeData.m_chars];
                Array.Copy(regionInfo.m_bin,(int)(sDWidthArrayAddr - regionInfo.Image__ROM__Base),pDWidthArray,0,pRangeDetails.pRangeData.m_chars);
            }
            
            if(sWidthArrayAddr!=0)
            {
                pWidthArray = new byte[pRangeDetails.pRangeData.m_chars];
                Array.Copy(regionInfo.m_bin,(int)(sWidthArrayAddr - regionInfo.Image__ROM__Base),pWidthArray,0,pRangeDetails.pRangeData.m_chars);
            }
            /*
            if(sOffsetArrayAddr!=0)
            {
                pOffsetArray = new UInt32[pRangeDetails.pRangeData.m_chars+1];
                
                for(int i=0; i<pOffsetArray.Length; i++)
                {
                    pOffsetArray[i] = BitConverter.ToUInt32(regionInfo.m_bin, (int)(sOffsetArrayAddr - regionInfo.Image__ROM__Base + (i <<2)));
                }
            }
             if(sDataArrayAddr!=0)
            {
                if(nEquiDistant == 0)
                {
                    pDataArray = new byte[pOffsetArray[pOffsetArray.Length -2] + nMaxChars ];
                    Array.Copy(regionInfo.m_bin,(int)(sDataArrayAddr - regionInfo.Image__ROM__Base),pDataArray,0,pDataArray.Length);
                }
                else
                {
                    pDataArray = new byte[nCharBytes * pRangeDetails.pRangeData.m_chars+ nMaxChars ];
                    Array.Copy(regionInfo.m_bin,(int)(sDataArrayAddr - regionInfo.Image__ROM__Base),pDataArray,0,pDataArray.Length);
                }
            }
            
            */
            if (sOffsetArrayAddr != 0)
            {
                pOffsetArrayC = new UInt16[pRangeDetails.pRangeData.m_chars + 1];

                for (int i = 0; i < pOffsetArrayC.Length; i++)
                {
                    pOffsetArrayC[i] = BitConverter.ToUInt16(regionInfo.m_bin, (int)(sOffsetArrayAddr - regionInfo.Image__ROM__Base + (i << 1)));
                }
            }
            if (sDataArrayAddr != 0)
            {
                if (nEquiDistant == 0)
                {
                    pDataArray = new byte[pOffsetArrayC[pOffsetArrayC.Length - 2] + nMaxChars];
                    Array.Copy(regionInfo.m_bin, (int)(sDataArrayAddr - regionInfo.Image__ROM__Base), pDataArray, 0, pDataArray.Length);
                }
                else
                {
                    pDataArray = new byte[nCharBytes * pRangeDetails.pRangeData.m_chars + nMaxChars];
                    Array.Copy(regionInfo.m_bin, (int)(sDataArrayAddr - regionInfo.Image__ROM__Base), pDataArray, 0, pDataArray.Length);
                }
            }
            
            if(sRangeAddr!=0)
            {
                pRange = new UInt32[pRangeDetails.pRangeData.m_chars+1];
                
                for(int i=0; i<pRangeDetails.nNoOfRanges; i++)
                {
                    pRange[i] = BitConverter.ToUInt32(regionInfo.m_bin, (int)(sRangeAddr - regionInfo.Image__ROM__Base + (i <<2)));
                }
            }

        }
        public void savefile(string path, string filename,byte[] bin,int offset, int len,int type)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if(!dir.Exists)
                dir.Create();
            pDataArray = new byte[len];
            Array.Copy(bin, (int)(offset), pDataArray, 0, pDataArray.Length);
            StreamWriter w = new StreamWriter(path + "\\"+filename+ ".txt");
            File.WriteAllBytes(path + "\\"+filename, pDataArray);
            if (type == 2)
            {
                for (int i = 0; i < len; i += 2)
                {
                    w.Write(((int)(pDataArray[i + 1] << 8 | pDataArray[i])).ToString() + ",");
                }

            }
            else if (type == 4)
            {
                for (int i = 0; i < len; i += 4)
                {
                    w.Write("0x" + ((int)(pDataArray[i+3] << 24 | pDataArray[i + 2] << 16 | pDataArray[i + 1] << 8 | pDataArray[i])).ToString("x8") + ",");
                }
            }
            else
            {
                for (int i = 0; i < len; i ++)
                {
                    w.Write("0x" + ((int)(pDataArray[i])).ToString("x2") + ",");
                }
            }
            w.Close();
        }
		public bool CheckData(MTKRegionInfo regionInfo, int offset)
		{
			int tmp;
			if(offset<0 ||offset +50 >= regionInfo.m_bin.Length)
				return false;
		    bin_offset = offset;
            nHeight= regionInfo.m_bin[offset++];
            nWidth= regionInfo.m_bin[offset++];
            nAscent= regionInfo.m_bin[offset++];
            nDescent= regionInfo.m_bin[offset++];
            nEquiDistant= regionInfo.m_bin[offset++];
            nCharBytes= regionInfo.m_bin[offset++];
            nMaxChars=BitConverter.ToUInt16(regionInfo.m_bin, offset);
            offset+=2;
            UInt32 sDWidthArrayAddr = regionInfo.Address_Converter_And_Check(offset);
            offset+=4;
            UInt32 sWidthArrayAddr = regionInfo.Address_Converter_And_Check(offset);
            offset+=4;
            UInt32 sOffsetArrayAddr = regionInfo.Address_Converter_And_Check(offset);
            offset+=4;
            UInt32 sDataArrayAddr = regionInfo.Address_Converter_And_Check(offset);
            offset+=4;
            UInt32 sRangeAddr = regionInfo.Address_Converter_And_Check(offset);
            offset+=4;
            UInt32 sRangeInfo = regionInfo.Address_Converter_And_Check(offset);
            offset+=4;

			if(sWidthArrayAddr == 0 || sOffsetArrayAddr ==0 ||sDataArrayAddr ==0|| sRangeAddr==0 || sRangeInfo == 0|| sRangeInfo - regionInfo.Image__ROM__Base+ 8 !=bin_offset)
				return false;
			UInt32 sRangeData  = regionInfo.Address_Converter_And_Check((int)(sRangeInfo - regionInfo.Image__ROM__Base+4));
			UInt16 sRandeCount = BitConverter.ToUInt16(regionInfo.m_bin, (int)(sRangeInfo - regionInfo.Image__ROM__Base));
			if(sRangeData == 0  || sRangeInfo != ((sRangeData + sRandeCount * 4 + 3) >> 2 << 2))
				return false;
            int offset1 = (int)(sRangeData - regionInfo.Image__ROM__Base);


			RangeData pRd = new RangeData(regionInfo.m_bin,ref offset1,(int)sRandeCount);
            if (pRd.m_chars<sRandeCount || pRd.m_chars != sOffsetArrayAddr - sDWidthArrayAddr)
                return false;

			UInt16 sRangeBlockCount  = BitConverter.ToUInt16(regionInfo.m_bin,(int)(sRangeAddr - regionInfo.Image__ROM__Base) );
			UInt32 sRangeBlockIndex  = regionInfo.Address_Converter_And_Check((int)(sRangeAddr - regionInfo.Image__ROM__Base + 4) );
			UInt32 sRangeOffset  = regionInfo.Address_Converter_And_Check((int)(sRangeAddr - regionInfo.Image__ROM__Base + 8) );

            //if (sDWidthArrayAddr!=0)
            //    savefile(bin_offset.ToString(), "DWidthArrayAddr", regionInfo.m_bin, (int)(sDWidthArrayAddr - regionInfo.Image__ROM__Base), (int)pRd.m_chars);


            savefile(bin_offset.ToString(), "sWidthArrayAddr", regionInfo.m_bin, (int)(sWidthArrayAddr - regionInfo.Image__ROM__Base), (int)pRd.m_chars,1);


            savefile(bin_offset.ToString(), "sOffsetArrayAddr", regionInfo.m_bin, (int)(sOffsetArrayAddr - regionInfo.Image__ROM__Base), (int)(pRd.m_chars+1),1);


            savefile(bin_offset.ToString(), "sRangeOffset", regionInfo.m_bin, (int)(sRangeOffset - regionInfo.Image__ROM__Base), (int)sRandeCount *2,2);


            savefile(bin_offset.ToString(), "sRangeData", regionInfo.m_bin, (int)(sRangeData - regionInfo.Image__ROM__Base), (int)sRandeCount * 4,2);


            savefile(bin_offset.ToString(), "sDataArrayAddr", regionInfo.m_bin, (int)(sDataArrayAddr - regionInfo.Image__ROM__Base), (int)(sRangeBlockIndex - sDataArrayAddr),1);

            savefile(bin_offset.ToString(), "sRangeBlockIndex", regionInfo.m_bin, (int)(sDataArrayAddr - regionInfo.Image__ROM__Base), (int)(sRangeBlockCount),1);

            //savefile(bin_offset.ToString(), "sRangeOffset", regionInfo.m_bin, (int)(sDataArrayAddr - regionInfo.Image__ROM__Base), (int)(sRangeBlockCount),1);

            /*
            if( sOffsetArrayAddr != ((sWidthArrayAddr + pRd.m_chars + 3) >> 2 << 2))
							return false;

            if (sRangeAddr != ((sOffsetArrayAddr + (pRd.m_chars + 1) * 4 + 3) >> 2 << 2))
							return false;

            if (sDataArrayAddr != (sRangeAddr + sRandeCount *2 ))
                return false;
            */
            
			return true;
		}
    }

    public class Range
    {
        public UInt16 nMin;
        public UInt16 nMax;
        public Range()
        {

        }
        public Range(byte[] bin, ref int offset)
        {
            nMin = BitConverter.ToUInt16(bin, offset);
            offset +=2;
            nMax = BitConverter.ToUInt16(bin, offset);
            offset +=2;
        }

    };

    public class RangeData
    {
        public Range[] m_rangeData;
        public UInt16 m_chars;
        UInt16 ParseValue(string text)
        {
            if(text[1] == 'x' || text[1] == 'X')
                return Convert.ToUInt16(text, 16);//UInt32.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                return UInt16.Parse(text);
        }
        public RangeData( )
        {
        }

        public RangeData(string text)
        {
            
            //string patten = @"(\s*{\s*(\d|0[xX]\xnn)\s*,\s*(\d|0[xX]\xnn)\s*}\s*,*\s*)+";
            //text = "{ {35,35},{42,44},{48,59},{65,65},{77,77},{80,80},{87,87},{112,112},{119,119},}";
            string patten = @"(\s*\{(?<min>\s*\w+\s*),(?<max>\s*\w+\s*)\}\s*,*\s*)";
            Regex buf = new Regex(patten, RegexOptions.IgnoreCase); 
            //string ip_pattern = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";//ip 地?址·的?正y则ò表括?达?式?
            //Regex rgx_ip_adr = new Regex(ip_pattern, RegexOptions.IgnoreCase);

            // Find matches.
            MatchCollection matches = buf.Matches(text);

            m_rangeData = new Range[matches.Count];
            // Report on each match.
            m_chars = 0;
            for(int i=0; i< matches.Count; i++)
            {
                m_rangeData[i] = new Range();
                m_rangeData[i].nMin = ParseValue(matches[i].Groups["min"].Value);
                m_rangeData[i].nMax = ParseValue(matches[i].Groups["max"].Value);
                m_chars +=(UInt16) (m_rangeData[i].nMax + 1 - m_rangeData[i].nMin);
            }
            

        }
        
        public RangeData(byte[] bin, ref int offset,int count)
        {
            m_rangeData = new Range[count];
            for(int i=0; i< count; i++)
            {
                m_rangeData[i] = new Range(bin,ref offset);
            }
            m_chars = 0;
            for(int i=0; i< count; i++)
            {
                m_chars +=(UInt16) (m_rangeData[i].nMax + 1 - m_rangeData[i].nMin);
            }
            
        }

    }
    
    public class MTKCustpack2ndJumpTbl
    {
        public UInt32 mtk_theme_header;    //(void *) &mtk_theme_header,
        public UInt32 mtk_image_header;   //(void *)&mtk_image_header,    //CUSTPACK_IMAGE
        public UInt32 custpack_audio;   // (void *)&custpack_audio,        /* CUSTPACK_AUDIO */
        public UInt32 mtk_audio_header;   //(void *)&mtk_audio_header,  //CUSTPACK_AUTOGEN_AUDIO
        public UInt32 custpack_nvram_ptr;  //  (void *)&custpack_nvram_ptr,    /* CUSTPACK_NVRAM */
    }
    
    public class CUSTOM_DATA
    {
        public UInt32 pData; /* Customizable Image filenames. */
    }

    public class CUSTPACK_DATA_HEADER
    {
        public UInt16 MaxImageNumEXT;
        public UInt32 CustImageNamesEXT;
    } 

    public class CUSTPACK_DATA_HEADER1
    {
        public UInt16  MaxImageNumEXT;
        public UInt32 CustImageNamesEXT;
    } 
    public class CUSTPACK_DATA_HEADER2
    {
        public UInt32  MaxImageNumEXT;
        public UInt32  CustImageNamesEXT;
    } 
    public class CUSTPACK_NVRAM_HEADER
    {
        public UInt32  version;
        public UInt32  COMMON_NVRAM_EF_ALS_LINE_ID_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_MSCAP_DEFAULT;
        public UInt32  NVRAM_EF_MMI_CACHE_BYTE_DEFAULT;
        public UInt32  NVRAM_EF_MMI_CACHE_SHORT_DEFAULT;
        public UInt32  NVRAM_EF_MMI_CACHE_DOUBLE_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_PHB_SOS_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_SMSAL_MAILBOX_ADDR_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_SMSAL_COMMON_PARAM_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_CB_DEFAULT_CH_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_SETTING_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_MS_SECURITY_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_RAC_PREFERENCE_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_INET_CUSTPACK_DEFAULT;
        public UInt32  COMMON_NVRAM_EF_WAP_BOOKMARK_LIST_DEFAULT;
    }
    public class sFontFamily
    {
        //public UInt32 MAX_FONTS_DATA = 6;
        public UInt16 nTotalFonts;
        public sCustFontData[] DatafontData;//= new sCustFontData [MAX_FONTS_DATA];//MAX_FONTS_DATA
        public int bin_offset =0;
        public sFontFamily()
        {

        }
        public sFontFamily(MTKRegionInfo regionInfo, ref int offset)
        {
            bin_offset = offset;
            nTotalFonts=BitConverter.ToUInt16(regionInfo.m_bin, offset);
            offset +=2;

            offset +=2;
            DatafontData = new sCustFontData[nTotalFonts];
            if(regionInfo.m_bin_type>= MTKRegionInfo.BIN_Type.MTK6276_11A)
            {
                 int offsetTmp =  regionInfo.GetBinOffset(BitConverter.ToUInt32(regionInfo.m_bin, offset));
                 for(int i=0; i< nTotalFonts; i++)
                 {
                     int offsetTmp1 =  regionInfo.GetBinOffset(BitConverter.ToUInt32(regionInfo.m_bin, offsetTmp));
                     DatafontData[i] = new sCustFontData(regionInfo, ref offsetTmp1);
                     offsetTmp+=4;
                 }
                 offset +=4;
            }
            else
            {
                for(int i=0; i< nTotalFonts; i++)
                {
                    int offsetTmp =  regionInfo.GetBinOffset(BitConverter.ToUInt32(regionInfo.m_bin, offset));
                    DatafontData[i] = new sCustFontData(regionInfo, ref offsetTmp);
                    offset +=4;
                }
            }
        }
    }

    public class sLanguageDetails
    {

        public const UInt32 MAX_FONT_FAMILIES= 6;
        public const UInt32 LANGUAGE_NAME= 40;
        public const UInt32 SSC_SIZE= 10;
        public const UInt32 LCC_SIZE= 8;

        public byte[] aName = new byte[LANGUAGE_NAME];//LANGUAGE_NAME
        public byte[]  aLangSSC = new byte[SSC_SIZE]; //SSC_SIZE
        public byte  nCurrentFamily;
        public sFontFamily[] fontfamilyList = new sFontFamily[MAX_FONT_FAMILIES];
        public byte[] aLangCountryCode =new byte[LCC_SIZE];
        public sLanguageDetails()
        {

        }
        public sLanguageDetails(MTKRegionInfo regionInfo, ref int offset)
        {
            Array.Copy(regionInfo.m_bin,offset,aName,0,aName.Length);
            offset +=aName.Length;
            Array.Copy(regionInfo.m_bin,offset,aLangSSC,0,aLangSSC.Length);
            offset +=aLangSSC.Length;
            nCurrentFamily = regionInfo.m_bin[offset];
            offset ++;
            if(regionInfo.m_bin_type< MTKRegionInfo.BIN_Type.MTK6276_11A)
            {
                offset =((offset + 3) >> 2) << 2;
                for(int i=0; i< MAX_FONT_FAMILIES; i++)
                {
                    UInt32 fontFamilyAddr = BitConverter.ToUInt32(regionInfo.m_bin, offset);
                    if(fontFamilyAddr!=0)
                    {
                        int offsetTmp =  regionInfo.GetBinOffset(fontFamilyAddr);
                        fontfamilyList[i] = new sFontFamily(regionInfo, ref offsetTmp);
                    }
                    offset+=4;
                }
            }
            Array.Copy(regionInfo.m_bin,offset,aLangCountryCode,0,aLangCountryCode.Length);
            offset +=aLangCountryCode.Length;
            
            offset =((offset + 1) >> 1) << 1;
        }
    } 
    public class MTKLangpack2ndJumpTbl
    { //{
    
            public UInt32   m_bin_offset;//                    (void*) &mtk_gMaxDeployedLangs,
            public sLanguageDetails[]  mtk_gLanguageArray;//                       (void*) mtk_gLanguageArray, 
            public sFontFamily[] fontfamilyList;
            public UInt32   mtk_gMaxDeployedLangs;//                    (void*) &mtk_gMaxDeployedLangs,
            public UInt32   mtk_gStringList;//                  (void*) mtk_gStringList,
            public UInt32   mtk_gIMEModeArray;//                    (void*) mtk_gIMEModeArray,
            public UInt32   mtk_gIMEQSearchModeArray; // (void*) mtk_gIMEQSearchModeArray
            public UInt32   mtk_gIMELDBArray;//                                       ,(void*) mtk_gIMELDBArray,
            public UInt32   mtk_gIMEModule;   //(void*) &mtk_gIMEModule
            public UInt32   tmp1; //                                      ,0
            public UInt32   tmp2; //                         ,0
            public UInt32   mtk_nCustMenus;//                                      ,(void*) mtk_nCustMenus//071706 menu resource
    }//                                           };
    
    public class MTKImage2ndJumpTbl
    { //{
    
            public UInt32   m_bin_offset;//                    (void*) &mtk_gMaxDeployedLangs,
            public UInt32[]   m_imageOffset;//                                      ,(void*) mtk_nCustMenus//071706 menu resource
    }//                                           };
    public class NVRAM_MMI_CACHE_DEFAULT
    {
        public UInt32 m_type;
        public bool m_isCompressed;
        public UInt32 m_base;
        public UInt32 m_len;
        public byte[] m_data = new byte[512];
    }
    public class MTKRegionInfo
    {
        public enum BIN_Type
        {
            TYPE_NONE,
            MTK6223_09A,
            MTK6223_09A_F,
            MTK6225_08B,
            MTK6252_10A,
            MTK6252_10A_1116,
			MTK6253_09A,
            MTK6276_11A,
			MTK6252_11B,
        };
        public enum REGION_Type
        {
            Load__EMIINIT_CODE__Base,
            Image__EMIINIT_CODE__Base,
            Image__EMIINIT_CODE__Length,
            Image__EMIINIT_CODE__ZI__Length,
            Image__EMIINIT_CODE__ZI__Base,
            Load__INTSRAM_DATA_PREINIT__Base,
            Image__INTSRAM_DATA_PREINIT__Base,
            Image__INTSRAM_DATA_PREINIT__Length,
            Image__INTSRAM_DATA_PREINIT__ZI__Length,
            Image__INTSRAM_DATA_PREINIT__ZI__Base,
            Load__SINGLE_BANK_CODE__Base,
            Image__SINGLE_BANK_CODE__Base,
            Image__SINGLE_BANK_CODE__Length,
            Image__SINGLE_BANK_CODE__ZI__Length,
            Image__SINGLE_BANK_CODE__ZI__Base,
            Load__INTSRAM_DATA__Base,
            Image__INTSRAM_DATA__Base,
            Image__INTSRAM_DATA__Length,
            Image__INTSRAM_DATA__ZI__Length,
            Image__INTSRAM_DATA__ZI__Base,
            Load__INTSRAM_MULTIMEDIA__Base,
            Image__INTSRAM_MULTIMEDIA__Base,
            Image__INTSRAM_MULTIMEDIA__Length,
            Image__INTSRAM_MULTIMEDIA__ZI__Length,
            Image__INTSRAM_MULTIMEDIA__ZI__Base,
            Load__PAGE_TABLE__Base,
            Image__PAGE_TABLE__Base,
            Image__PAGE_TABLE__Length,
            Image__PAGE_TABLE__ZI__Length,
            Image__PAGE_TABLE__ZI__Base,
            Load__CACHED_EXTSRAM__Base,
            Image__CACHED_EXTSRAM__Base,
            Image__CACHED_EXTSRAM__Length,
            Image__CACHED_EXTSRAM__ZI__Length,
            Image__CACHED_EXTSRAM__ZI__Base,
            Load__CACHED_EXTSRAM_PROTECTED_RES__Base,
            Image__CACHED_EXTSRAM_PROTECTED_RES__Base,
            Image__CACHED_EXTSRAM_PROTECTED_RES__Length,
            Image__CACHED_EXTSRAM_PROTECTED_RES__ZI__Length,
            Image__CACHED_EXTSRAM_PROTECTED_RES__ZI__Base,
            Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Length,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__ZI__Length,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__ZI__Base,
            Load__CACHED_EXTSRAM_CODE__Base,
            Image__CACHED_EXTSRAM_CODE__Base,
            Image__CACHED_EXTSRAM_CODE__Length,
            Image__CACHED_EXTSRAM_CODE__ZI__Length,
            Image__CACHED_EXTSRAM_CODE__ZI__Base,
            Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Length,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Length,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Base,
            Load__EXTSRAM__Base,
            Image__EXTSRAM__Base,
            Image__EXTSRAM__Length,
            Image__EXTSRAM__ZI__Length,
            Image__EXTSRAM__ZI__Base,
            Load__EXTSRAM_PROTECTED_RES__Base,
            Image__EXTSRAM_PROTECTED_RES__Base,
            Image__EXTSRAM_PROTECTED_RES__Length,
            Image__EXTSRAM_PROTECTED_RES__ZI__Length,
            Image__EXTSRAM_PROTECTED_RES__ZI__Base,
            Load__INTSRAM_CODE__Base,
            Image__INTSRAM_CODE__Base,
            Image__INTSRAM_CODE__Length,
            Image__INTSRAM_CODE__ZI__Length,
            Image__INTSRAM_CODE__ZI__Base,
            Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Length,
            Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Base,
            Load__CODE_PATCH_CODE__Base,
            Image__CODE_PATCH_CODE__Base,
            Image__CODE_PATCH_CODE__Length,
            Image__CODE_PATCH_CODE__ZI__Length,
            Image__CODE_PATCH_CODE__ZI__Base,
            Load__CACHED_CODE_PATCH_CODE__Base,
            Image__CACHED_CODE_PATCH_CODE__Base,
            Image__CACHED_CODE_PATCH_CODE__Length,
            Image__CACHED_CODE_PATCH_CODE__ZI__Length,
            Image__CACHED_CODE_PATCH_CODE__ZI__Base,
            Load__EXTSRAM_DSP_TX__Base,
            Image__EXTSRAM_DSP_TX__Base,
            Image__EXTSRAM_DSP_TX__Length,
            Image__EXTSRAM_DSP_TX__ZI__Length,
            Image__EXTSRAM_DSP_TX__ZI__Base,
            Load__EXTSRAM_DSP_RX__Base,
            Image__EXTSRAM_DSP_RX__Base,
            Image__EXTSRAM_DSP_RX__Length,
            Image__EXTSRAM_DSP_RX__ZI__Length,
            Image__EXTSRAM_DSP_RX__ZI__Base,
            Load__INTSRAM__Base,                         
            Image__INTSRAM__Base,                        
            Image__INTSRAM__Length,                      
            Image__INTSRAM__ZI__Length,                  
            Image__INTSRAM__ZI__Base,     
            Load__L2TCM_CODE__Base,
            Image__L2TCM_CODE__Base,
            Image__L2TCM_CODE__Length,
            Image__L2TCM_CODE__ZI__Length,
            Image__L2TCM_CODE__ZI__Base,
            Load__L2TCM_DATA__Base,
            Image__L2TCM_DATA__Base,
            Image__L2TCM_DATA__Length,
            Image__L2TCM_DATA__ZI__Length,
            Image__L2TCM_DATA__ZI__Base,
            Load__FLEXL2_DATA__Base,
            Image__FLEXL2_DATA__Base,
            Image__FLEXL2_DATA__Length,
            Image__FLEXL2_DATA__ZI__Length,
            Image__FLEXL2_DATA__ZI__Base,
            Load__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Base,
            Image__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Base,
            Image__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Length,
            Load__PRIMARY_CACHED_EXTSRAM__Base,
            Image__PRIMARY_CACHED_EXTSRAM__Base,
            Image__PRIMARY_CACHED_EXTSRAM__Length,
            Load__PRIMARY_EXTSRAM__Base,
            Image__PRIMARY_EXTSRAM__Base,
            Image__PRIMARY_EXTSRAM__Length,
            Load__ROM_1__Base,
            Image__ROM_1__Base,
            Image__ROM_1__Length,
            Image__ROM_1__ZI__Length,
            Image__ROM_1__ZI__Base,
            Image__PRIMARY_EXTSRAM__ZI__Length,
            Image__PRIMARY_EXTSRAM__ZI__Base,
            Image__PRIMARY_CACHED_EXTSRAM__ZI__Length,
            Image__PRIMARY_CACHED_EXTSRAM__ZI__Base,
            Image__SECONDARY_EXTSRAM__ZI__Length,
            Image__SECONDARY_EXTSRAM__ZI__Base,
            Image__SECONDARY_EXTSRAM_ZI__ZI__Length,
            Image__SECONDARY_EXTSRAM_ZI__ZI__Base,
            Image__EXTSRAM_GADGET__ZI__Length,
            Image__EXTSRAM_GADGET__ZI__Base,
            Load__SECONDARY_EXTSRAM_DSP_TX__Base,
            Image__SECONDARY_EXTSRAM_DSP_TX__Base,
            Image__SECONDARY_EXTSRAM_DSP_TX__Length,
            Image__SECONDARY_EXTSRAM_DSP_TX__ZI__Length,
            Image__SECONDARY_EXTSRAM_DSP_TX__ZI__Base,
            Load__SECONDARY_EXTSRAM_DSP_RX__Base,
            Image__SECONDARY_EXTSRAM_DSP_RX__Base,
            Image__SECONDARY_EXTSRAM_DSP_RX__Length,
            Image__SECONDARY_EXTSRAM_DSP_RX__ZI__Length,
            Image__SECONDARY_EXTSRAM_DSP_RX__ZI__Base,
            Load__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Base,
            Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Base,
            Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Length,
            Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__ZI__Length,
            Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__ZI__Base,
            Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Length,
            Load__EXTSRAM_GADGET__Base,
            Image__EXTSRAM_GADGET__Base,
            Image__EXTSRAM_GADGET__Length,
            Load__SECONDARY_EXTSRAM__Base,
            Image__SECONDARY_EXTSRAM__Base,
            Image__SECONDARY_EXTSRAM__Length,
            Load__SECONDARY_EXTSRAM_RW__Base,
            Image__SECONDARY_EXTSRAM_RW__Base,
            Image__SECONDARY_EXTSRAM_RW__Length,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_ZI__ZI__Length,
            Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_ZI__ZI__Base,
            Image__SECONDARY_EXTSRAM_LARGEPOOL_NORMAL__ZI__Length,
            Image__SECONDARY_EXTSRAM_LARGEPOOL_NORMAL__ZI__Base,
            REGION_TYPE_MAX
        };
        UInt32[] m_mark_6523_09a = new UInt32[]
                        {
                                0xe1a0700e,
                                0xe59f0204,
                                0xe59f1204,
                                0xe1a02001,
                                0xe59f4200,
                                0xe0822004,
                                0xebfffffe,
                                0xe59f21f8,
                                0xe59f01f8,
                                0xe1a01000,
                                0xe0811002,
                                0xebfffffe,
                                0xe12fff17,
                                0xe1a0700e,
                                0xe12fff17,
                                0xe1a0700e,
                                0xe59f01dc,
                                0xe59f11dc,
                                0xe1a02001,
                                0xe59f41d8,
                                0xe0822004,
                                0xebfffffe,
                                0xe59f21d0,
                                0xe59f01d0,
                                0xe1a01000,
                                0xe0811002,
                                0xebfffffe,
                                0xe59f01c4,
                                0xe59f11c4,
                                0xe1a02001,
                                0xe59f41c0,
                                0xe0822004,
                                0xebfffffe,
                                0xe59f21b8,
                                0xe59f01b8,
                                0xe1a01000,
                                0xe0811002,
                                0xebfffffe,
                                0xe59f01ac,
                                0xe59f11ac,
                                0xe1a02001,
                                0xe59f41a8,
                                0xe0822004,
                                0xebfffffe,
                                0xe59f21a0,
                                0xe59f01a0,
                                0xe1a01000,
                                0xe0811002,
                                0xebfffffe,
                                0xe59f2194,
                                0xe59f0194,
                                0xe1a01000,
                                0xe0811002,
                                0xebfffffe,
                                0xe59f0188,
                                0xe59f1188,
                                0xe1a02001,
                                0xe59f4184,
                                0xe0822004,
                                0xebfffffe,
                                0xe59f217c,
                                0xe59f017c,
                                0xe1a01000,
                                0xe0811002,
                                0xebfffffe,
                                0xe59f0170,
                                0xe59f1170,
                                0xe1a02001,
                                0xe59f416c,
                                0xe0822004,
                                0xebfffffe,
                                0xe59f2164,
                                0xe59f0164,
                                0xe1a01000,
                                0xe0811002,
                                0xebfffffe,
                                0xe59f0158,
                                0xe59f1158,
                                0xe1a02001,
                                0xe59f4154,
                                0xe0822004,
                                0xebfffffe,
                                0xe59f214c,
                                0xe59f014c,
                                0xe1a01000,
                                0xe0811002,
                                0xebfffffe,
                                0xe59f0140,
                                0xe59f1140,
                                0xe1a02001,
                                0xe59f413c,
                                0xe0822004,
                                0xebfffffe,
                                0xe59f2134,
                                0xe59f0134,
                                0xe1a01000,
                                0xe0811002,
                                0xebfffffe,
                                0xe12fff17,
                                0xe92d00ff,
                                0xe1a0700e,
                                0xe1a0e007,
                                0xe8bd00ff,
                                0xe12fff1e,
                                0xe1510002,
                                0x34903004,
                                0x34813004,
                                0x3afffffe,
                                0xe1a0f00e,
                                0xe1510002,
                                0xa4103004,
                                0xa4013004,
                                0xaafffffe,
                                0xe1a0f00e,
                                0xe3a02000,
                                0xe1500001,
                                0x34802004,
                                0x3afffffe,
                                0xe1a0f00e,
                                0xe92d0700,
                                0xe3a03000,
                                0xe3a08000,
                                0xe3a09000,
                                0xe3a0a000,
                                0xe2522020,
                                0x28a00708,
                                0x28a00708,
                                0x22522020,
                                0x2afffffe,
                                0xe8bd0700,
                                0xeafffffe,
                                0xe1a0f00e,
                       };
        public UInt32 Image__ROM__Base;
        public UInt32[] g_region = new UInt32[(int)REGION_Type.REGION_TYPE_MAX];
                                         

        public byte[] m_bin;
        public bool m_load_successed = false;
        public MTKRegionInfo.BIN_Type m_bin_type = MTKRegionInfo.BIN_Type.TYPE_NONE;
        public  MTKRegionInfo( )
        {
        }
        public UInt32 Address_Converter_And_Check(int offset)
        {
            if(offset > m_bin.Length-4)
				return 0;
            UInt32 addr = BitConverter.ToUInt32(m_bin, (int)offset);
            
            if((addr& 0xFF000000) != Image__ROM__Base)    
                return 0;

            if(addr - Image__ROM__Base >= m_bin.Length)    
                return 0;
                
            return addr;
        }
        public UInt32 GetValidAddress(UInt32 addr)
        {
            if(addr >= g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Base ] && addr<  g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Base ]+ g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Base ] + g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__EXTSRAM__Base ] && addr<  g_region[ (int)REGION_Type.Image__EXTSRAM__Base ]+ g_region[ (int)REGION_Type.Image__EXTSRAM__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__EXTSRAM__Base ] + g_region[ (int)REGION_Type.Load__EXTSRAM__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Base ] && addr<  g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Base ]+ g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Base ] + g_region[ (int)REGION_Type.Load__EMIINIT_CODE__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Base ] && addr<  g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Base ]+ g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Base ] + g_region[ (int)REGION_Type.Load__INTSRAM_DATA_PREINIT__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Base ] && addr<  g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Base ]+ g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Base ] + g_region[ (int)REGION_Type.Load__SINGLE_BANK_CODE__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Base ] && addr<  g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Base ]+ g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Base ] + g_region[ (int)REGION_Type.Load__INTSRAM_DATA__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Base ] && addr<  g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Base ]+ g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Base ] + g_region[ (int)REGION_Type.Load__INTSRAM_MULTIMEDIA__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__PAGE_TABLE__Base ] && addr<  g_region[ (int)REGION_Type.Image__PAGE_TABLE__Base ]+ g_region[ (int)REGION_Type.Image__PAGE_TABLE__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__PAGE_TABLE__Base ] + g_region[ (int)REGION_Type.Load__PAGE_TABLE__Base ];
            }

            else if(addr >= g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Base ] && addr<  g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Base ]+ g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Base ] + g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Base ] && addr<  g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Base ]+ g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Base ] + g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM_PROTECTED_RES__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base ] && addr<  g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base ]+ g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base ] + g_region[ (int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Base ] && addr<  g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Base ]+ g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Base ] + g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM_CODE__Base ];
            }

            else if(addr >= g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ] && addr<  g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ]+ g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ] + g_region[ (int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__EXTSRAM__Base ] && addr<  g_region[ (int)REGION_Type.Image__EXTSRAM__Base ]+ g_region[ (int)REGION_Type.Image__EXTSRAM__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__EXTSRAM__Base ] + g_region[ (int)REGION_Type.Load__EXTSRAM__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Base ] && addr<  g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Base ]+ g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Base ] + g_region[ (int)REGION_Type.Load__EXTSRAM_PROTECTED_RES__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Base ] && addr<  g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Base ]+ g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Base ] + g_region[ (int)REGION_Type.Load__INTSRAM_CODE__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ] && addr<  g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ]+ g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ] + g_region[ (int)REGION_Type.Load__CODE_PATCH_CODE__Base ];
            }
            
            else if(addr >= g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base ] && addr<  g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base ]+ g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base ] + g_region[ (int)REGION_Type.Load__CACHED_CODE_PATCH_CODE__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Base ] && addr<  g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Base ]+ g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Base ] + g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_TX__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Base ] && addr<  g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Base ]+ g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Base ] + g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_RX__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__INTSRAM__Base ] && addr<  g_region[ (int)REGION_Type.Image__INTSRAM__Base ]+ g_region[ (int)REGION_Type.Image__INTSRAM__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__INTSRAM__Base ] + g_region[ (int)REGION_Type.Load__INTSRAM__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__L2TCM_CODE__Base ] && addr<  g_region[ (int)REGION_Type.Image__L2TCM_CODE__Base ]+ g_region[ (int)REGION_Type.Image__L2TCM_CODE__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__L2TCM_CODE__Base ] + g_region[ (int)REGION_Type.Load__L2TCM_CODE__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__L2TCM_DATA__Base ] && addr<  g_region[ (int)REGION_Type.Image__L2TCM_DATA__Base ]+ g_region[ (int)REGION_Type.Image__L2TCM_DATA__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__L2TCM_DATA__Base ] + g_region[ (int)REGION_Type.Load__L2TCM_DATA__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__FLEXL2_DATA__Base ] && addr<  g_region[ (int)REGION_Type.Image__FLEXL2_DATA__Base ]+ g_region[ (int)REGION_Type.Image__FLEXL2_DATA__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__FLEXL2_DATA__Base ] + g_region[ (int)REGION_Type.Load__FLEXL2_DATA__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Base ] && addr<  g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Base ]+ g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Base ] + g_region[ (int)REGION_Type.Load__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__PRIMARY_EXTSRAM__Base ] && addr<  g_region[ (int)REGION_Type.Image__PRIMARY_EXTSRAM__Base ]+ g_region[ (int)REGION_Type.Image__PRIMARY_EXTSRAM__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__PRIMARY_EXTSRAM__Base ] + g_region[ (int)REGION_Type.Load__PRIMARY_EXTSRAM__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__ROM_1__Base ] && addr<  g_region[ (int)REGION_Type.Image__ROM_1__Base ]+ g_region[ (int)REGION_Type.Image__ROM_1__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__ROM_1__Base ] + g_region[ (int)REGION_Type.Load__ROM_1__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_TX__Base ] && addr<  g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_TX__Base ]+ g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_TX__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_TX__Base ] + g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM_DSP_TX__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_RX__Base ] && addr<  g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_RX__Base ]+ g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_RX__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_RX__Base ] + g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM_DSP_RX__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Base ] && addr<  g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Base ]+ g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Base ] + g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base ] && addr<  g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base ]+ g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base ] + g_region[ (int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__EXTSRAM_GADGET__Base ] && addr<  g_region[ (int)REGION_Type.Image__EXTSRAM_GADGET__Base ]+ g_region[ (int)REGION_Type.Image__EXTSRAM_GADGET__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__EXTSRAM_GADGET__Base ] + g_region[ (int)REGION_Type.Load__EXTSRAM_GADGET__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM__Base ] && addr<  g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM__Base ]+ g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM__Base ] + g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM__Base ];
            }
            else if(addr >= g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_RW__Base ] && addr<  g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_RW__Base ]+ g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_RW__Length ])
            {
                return addr - g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_RW__Base ] + g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM_RW__Base ];
            }



            return addr;

        }
        public int GetBinOffset(UInt32 addr)
        {
            return (int)(GetValidAddress(addr) - Image__ROM__Base);
        }
        public UInt32 GetBinOffsetU(UInt32 addr)
        {
            return (GetValidAddress(addr) - Image__ROM__Base);
        }

        public bool LoadBin(byte[] bin)
        {
            m_bin = bin;
            if(IsMTK6223_09A()
                ||IsMTK6223_09A_F()
                ||IsMTK6225_08B()
                ||IsMTK6252_10A()
                ||IsMTK6252_10A_1116()
                ||IsMTK6252_11B()
                ||IsMTK6253_09A())
                m_load_successed = true;
            else
                m_load_successed = false;
            return m_load_successed;
        }
        bool IsMTK6223_09A()
        {
             
                    int index,j;
                    int file_size = m_bin.Length;
                    
                    long bin_size = file_size/4;
                    for(index = 0; index < bin_size; index++)
                    {
                            if (BitConverter.ToUInt32(m_bin, index << 2) == m_mark_6523_09a[0])
                            {
                                for(j=0; j < m_mark_6523_09a.Length; j++)
                                {
                                    if (BitConverter.ToUInt32(m_bin, (index + j) << 2) != m_mark_6523_09a[j] && (m_mark_6523_09a[j] &0xFFFFFF) != 0x00fffffe)
                                    {
                                        break;
                                    }
                                }
                                if (j == m_mark_6523_09a.Length)
                                {
                                //g_region_info = (t_region*)&bin[index+j];
                                    int i=0;
                                    g_region[ (int)REGION_Type.Load__EMIINIT_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                    
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                   
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                 
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);             
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);               
                                    g_region[ (int)REGION_Type.Load__EXTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                         
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                        
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                      
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                  
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                    
                                    g_region[ (int)REGION_Type.Load__INTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                         
                                    g_region[ (int)REGION_Type.Image__INTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                        
                                    g_region[ (int)REGION_Type.Image__INTSRAM__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                      
                                    g_region[ (int)REGION_Type.Image__INTSRAM__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                  
                                    g_region[ (int)REGION_Type.Image__INTSRAM__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                    
                                    g_region[ (int)REGION_Type.Load__SINGLE_BANK_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);               
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);             
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);         
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);           
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);   
                                    g_region[ (int)REGION_Type.Load__CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                 
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);              
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);          
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);            
                                    g_region[ (int)REGION_Type.Load__CACHED_CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);          
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);         
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);       
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);   
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);     
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_TX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                  
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);               
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);           
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);             
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_RX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                  
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);               
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);           
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);  
                                    Image__ROM__Base = g_region[ (int)REGION_Type.Load__EXTSRAM__Base ] & 0xFF000000;
                                    m_bin_type = BIN_Type.MTK6223_09A;
                                    return true;
                                }
                            }
                    }
                    return false;
                    
            }
            bool IsMTK6223_09A_F()
            {
             UInt32[] m_mark = new UInt32[]
                        {
                            0xe1a0700e,
                            0xe59f01d8,
                            0xe59f11d8,
                            0xe1a02001,
                            0xe59f41d4,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f21cc,
                            0xe59f01cc,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe12fff17,
                            0xe1a0700e,
                            0xe12fff17,
                            0xe1a0700e,
                            0xe59f01b0,
                            0xe59f11b0,
                            0xe1a02001,
                            0xe59f41ac,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f21a4,
                            0xe59f01a4,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f0198,
                            0xe59f1198,
                            0xe1a02001,
                            0xe59f4194,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f218c,
                            0xe59f018c,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f2180,
                            0xe59f0180,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f0174,
                            0xe59f1174,
                            0xe1a02001,
                            0xe59f4170,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2168,
                            0xe59f0168,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f015c,
                            0xe59f115c,
                            0xe1a02001,
                            0xe59f4158,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2150,
                            0xe59f0150,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f0144,
                            0xe59f1144,
                            0xe1a02001,
                            0xe59f4140,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2138,
                            0xe59f0138,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f012c,
                            0xe59f112c,
                            0xe1a02001,
                            0xe59f4128,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2120,
                            0xe59f0120,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe12fff17,
                            0xe92d00ff,
                            0xe1a0700e,
                            0xe1a0e007,
                            0xe8bd00ff,
                            0xe12fff1e,
                            0xe1510002,
                            0x34903004,
                            0x34813004,
                            0x3afffffe,
                            0xe1a0f00e,
                            0xe1510002,
                            0xa4103004,
                            0xa4013004,
                            0xaafffffe,
                            0xe1a0f00e,
                            0xe3a02000,
                            0xe1500001,
                            0x34802004,
                            0x3afffffe,
                            0xe1a0f00e,
                            0xe92d0700,
                            0xe3a03000,
                            0xe3a08000,
                            0xe3a09000,
                            0xe3a0a000,
                            0xe2522020,
                            0x28a00708,
                            0x28a00708,
                            0x22522020,
                            0x2afffffe,
                            0xe8bd0700,
                            0xeafffffe,
                            0xe1a0f00e,
                    };
                    int index,j;
                    int file_size = m_bin.Length;
                    
                    long bin_size = file_size/4;
                    for(index = 0; index < bin_size; index++)
                    {
                            if (BitConverter.ToUInt32(m_bin, index << 2) == m_mark[0])
                            {
                                for(j=0; j < m_mark.Length; j++)
                                {
                                    if (BitConverter.ToUInt32(m_bin, (index + j) << 2) != m_mark[j] && (m_mark[j] &0xFFFFFF) != 0x00fffffe)
                                    {
                                        break;
                                    }
                                }
                                if (j == m_mark.Length)
                                {
                                //g_region_info = (t_region*)&bin[index+j];
                                    int i=0;
                                    g_region[ (int)REGION_Type.Load__EMIINIT_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                    
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                   
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                 
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);             
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);               
                                    g_region[ (int)REGION_Type.Load__EXTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                         
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                        
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                      
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                  
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                    
                                    g_region[ (int)REGION_Type.Load__INTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                         
                                    g_region[ (int)REGION_Type.Image__INTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                        
                                    g_region[ (int)REGION_Type.Image__INTSRAM__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                      
                                    g_region[ (int)REGION_Type.Image__INTSRAM__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                  
                                    g_region[ (int)REGION_Type.Image__INTSRAM__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                    
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);   
                                    g_region[ (int)REGION_Type.Load__CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                 
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);              
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);          
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);            
                                    g_region[ (int)REGION_Type.Load__CACHED_CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);          
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);         
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);       
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);   
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);     
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_TX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                  
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);               
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);           
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);             
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_RX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                  
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);                 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);               
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);           
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);  
                                    Image__ROM__Base = g_region[ (int)REGION_Type.Load__EXTSRAM__Base ] & 0xFF000000;
                                    
                                    m_bin_type = BIN_Type.MTK6223_09A_F;
                                    return true;
                                }
                            }
                    }
                    return false;
                    
            }
        bool IsMTK6225_08B()
        {
             UInt32[] m_mark = new UInt32[]
                        {
                0xe1a0700e,
                0xe59f0160,
                0xe59f1160,
                0xe1a02001,
                0xe59f415c,
                0xe0822004,
                0xebfffffe,
                0xe59f2154,
                0xe59f0154,
                0xe1a01000,
                0xe0811002,
                0xebfffffe,
                0xe59f0148,
                0xe59f1148,
                0xe1a02001,
                0xe59f4144,
                0xe0822004,
                0xebfffffe,
                0xe59f213c,
                0xe59f013c,
                0xe1a01000,
                0xe0811002,
                0xebfffffe,
                0xe59f0130,
                0xe59f1130,
                0xe1a02001,
                0xe59f412c,
                0xe0822004,
                0xebfffffe,
                0xe59f2124,
                0xe59f0124,
                0xe1a01000,
                0xe0811002,
                0xebfffffe,
                0xe12fff17,
                0xe1a0700e,
                0xe59f0110,
                0xe59f1110,
                0xe1a02001,
                0xe59f410c,
                0xe0822004,
                0xebfffffe,
                0xe59f2104,
                0xe59f0104,
                0xe1a01000,
                0xe0811002,
                0xebfffffe,
                0xe59f20f8,
                0xe59f00f8,
                0xe1a01000,
                0xe0811002,
                0xebfffffe,
                0xe59f00ec,
                0xe59f10ec,
                0xe1a02001,
                0xe59f40e8,
                0xe0822004,
                0xebfffffe,
                0xe59f20e0,
                0xe59f00e0,
                0xe1a01000,
                0xe0811002,
                0xebfffffe,
                0xe12fff17,
                0xe1a0700e,
                0xe92d001f,
                0xe8bd001f,
                0xe12fff17,
                0xe1510002,
                0x34903004,
                0x34813004,
                0x3afffffe,
                0xe1a0f00e,
                0xe3a02000,
                0xe1500001,
                0x34802004,
                0x3afffffe,
                0xe1a0f00e,
                0xe92d0700,
                0xe3a03000,
                0xe3a08000,
                0xe3a09000,
                0xe3a0a000,
                0xe2522020,
                0x28a00708,
                0x28a00708,
                0x22522020,
                0x2afffffe,
                0xe8bd0700,
                0xeafffffe,
                0xe1a0f00e,

                       };
                    int index,j;
                    int file_size = m_bin.Length;
                    
                    long bin_size = file_size/4;
                    for(index = 0; index < bin_size; index++)
                    {
                            if (BitConverter.ToUInt32(m_bin, index << 2) == m_mark[0])
                            {
                                for(j=0; j < m_mark.Length; j++)
                                {
                                    if (BitConverter.ToUInt32(m_bin, (index + j) << 2) != m_mark[j] && (m_mark[j] &0xFFFFFF) != 0x00fffffe)
                                    {
                                        break;
                                    }
                                }
                                if (j == m_mark.Length)
                                {
                                //g_region_info = (t_region*)&bin[index+j];
                                    int i=0;
                                g_region[ (int)REGION_Type.Load__INTSRAM_CODE__Base ]                        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Base ]                       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Length ]                     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Length ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Base ]                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Load__INTSRAM_DATA__Base ]                        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Base ]                       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Length ]                     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Length ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Base ]                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Load__INTSRAM_MULTIMEDIA__Base ]                  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Base ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Length ]               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Length ]           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Base ]             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Load__EXTSRAM__Base ]                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__EXTSRAM__Base ]                            =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__EXTSRAM__Length ]                          =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Length ]                      =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Base ]                        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Length ]     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Base ]       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Load__CODE_PATCH_CODE__Base ]                     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ]                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Length ]                  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Length ]              =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Base ]                =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                Image__ROM__Base = g_region[ (int)REGION_Type.Load__EXTSRAM__Base ] & 0xFF000000;
                                
                                m_bin_type = BIN_Type.MTK6225_08B;
                                    return true;
                                }
                            }
                    }
                    return false;
                    
            }
            bool IsMTK6252_10A()
            {
            
             UInt32[] m_mark = new UInt32[]
                                                    {
                                                            0xe1a0700e,
                                                            0xe59f03f0,
                                                            0xe59f13f0,
                                                            0xe1a02001,
                                                            0xe59f43ec,
                                                            0xe0822004,
                                                            0xeb0000db,
                                                            0xe59f23e4,
                                                            0xe59f03e4,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb0000e5,
                                                            0xe59f03d8,
                                                            0xe59f13d8,
                                                            0xe1a02001,
                                                            0xe59f43d4,
                                                            0xe0822004,
                                                            0xeb0000d0,
                                                            0xe59f23cc,
                                                            0xe59f03cc,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb0000da,
                                                            0xe59f03c0,
                                                            0xe59f13c0,
                                                            0xe1a02001,
                                                            0xe59f43bc,
                                                            0xe0822004,
                                                            0xeb0000c5,
                                                            0xe59f23b4,
                                                            0xe59f03b4,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb0000cf,
                                                            0xe12fff17,
                                                            0xe1a0700e,
                                                            0xe59f03a0,
                                                            0xe59f13a0,
                                                            0xe1a02001,
                                                            0xe59f439c,
                                                            0xe0822004,
                                                            0xeb0000b8,
                                                            0xe59f2394,
                                                            0xe59f0394,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb0000c2,
                                                            0xe59f0388,
                                                            0xe59f1388,
                                                            0xe1a02001,
                                                            0xe59f4384,
                                                            0xe0822004,
                                                            0xeb0000ad,
                                                            0xe59f237c,
                                                            0xe59f037c,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb0000b7,
                                                            0xe12fff17,
                                                            0xe1a0700e,
                                                            0xe59f0368,
                                                            0xe59f1368,
                                                            0xe1a02001,
                                                            0xe59f4364,
                                                            0xe0822004,
                                                            0xeb0000a0,
                                                            0xe59f235c,
                                                            0xe59f035c,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb0000aa,
                                                            0xe59f0350,
                                                            0xe59f1350,
                                                            0xe1a02001,
                                                            0xe59f434c,
                                                            0xe0822004,
                                                            0xeb000095,
                                                            0xe59f2344,
                                                            0xe59f0344,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb00009f,
                                                            0xe59f0338,
                                                            0xe59f1338,
                                                            0xe1a02001,
                                                            0xe59f4334,
                                                            0xe0822004,
                                                            0xeb00008a,
                                                            0xe59f232c,
                                                            0xe59f032c,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb000094,
                                                            0xe59f0320,
                                                            0xe59f1320,
                                                            0xe1a02001,
                                                            0xe59f431c,
                                                            0xe0822004,
                                                            0xeb00007f,
                                                            0xe59f2314,
                                                            0xe59f0314,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb000089,
                                                            0xe59f0308,
                                                            0xe59f1308,
                                                            0xe1a02001,
                                                            0xe59f4304,
                                                            0xe0822004,
                                                            0xeb000074,
                                                            0xe59f22fc,
                                                            0xe59f02fc,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb00007e,
                                                            0xe59f02f0,
                                                            0xe59f12f0,
                                                            0xe1a02001,
                                                            0xe59f42ec,
                                                            0xe2444004,
                                                            0xe0800004,
                                                            0xe0811004,
                                                            0xeb00006c,
                                                            0xe59f22dc,
                                                            0xe59f02dc,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb000071,
                                                            0xe59f02d0,
                                                            0xe59f12d0,
                                                            0xe1a02001,
                                                            0xe59f42cc,
                                                            0xe0822004,
                                                            0xeb00005c,
                                                            0xe59f22c4,
                                                            0xe59f02c4,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb000066,
                                                            0xe59f02b8,
                                                            0xe59f12b8,
                                                            0xe1a02001,
                                                            0xe59f42b4,
                                                            0xe0822004,
                                                            0xeb000051,
                                                            0xe59f22ac,
                                                            0xe59f02ac,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb00005b,
                                                            0xe59f02a0,
                                                            0xe59f12a0,
                                                            0xe1a02001,
                                                            0xe59f429c,
                                                            0xe0822004,
                                                            0xeb000046,
                                                            0xe59f2294,
                                                            0xe59f0294,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb000050,
                                                            0xe59f0198,
                                                            0xe59f1198,
                                                            0xe1a02001,
                                                            0xe59f4194,
                                                            0xe0822004,
                                                            0xeb00003b,
                                                            0xe59f218c,
                                                            0xe59f018c,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb000045,
                                                            0xe59f225c,
                                                            0xe59f025c,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb000040,
                                                            0xe59f0250,
                                                            0xe59f1250,
                                                            0xe1a02001,
                                                            0xe59f424c,
                                                            0xe0822004,
                                                            0xeb00002b,
                                                            0xe59f2244,
                                                            0xe59f0244,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb000035,
                                                            0xe59f0238,
                                                            0xe59f1238,
                                                            0xe1a02001,
                                                            0xe59f4234,
                                                            0xe0822004,
                                                            0xeb000020,
                                                            0xe59f222c,
                                                            0xe59f022c,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb00002a,
                                                            0xe59f0220,
                                                            0xe59f1220,
                                                            0xe1a02001,
                                                            0xe59f421c,
                                                            0xe0822004,
                                                            0xeb000015,
                                                            0xe59f2214,
                                                            0xe59f0214,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb00001f,
                                                            0xe59f0208,
                                                            0xe59f1208,
                                                            0xe1a02001,
                                                            0xe59f4204,
                                                            0xe0822004,
                                                            0xeb00000a,
                                                            0xe59f21fc,
                                                            0xe59f01fc,
                                                            0xe1a01000,
                                                            0xe0811002,
                                                            0xeb000014,
                                                            0xe12fff17,
                                                            0xe92d00ff,
                                                            0xe1a0700e,
                                                            0xe1a0e007,
                                                            0xe8bd00ff,
                                                            0xe12fff1e,
                                                            0xe1510002,
                                                            0x34903004,
                                                            0x34813004,
                                                            0x3afffffb,
                                                            0xe1a0f00e,
                                                            0xe1510002,
                                                            0xa4103004,
                                                            0xa4013004,
                                                            0xaafffffb,
                                                            0xe1a0f00e,
                                                            0xe3a02000,
                                                            0xe1500001,
                                                            0x34802004,
                                                            0x3afffffb,
                                                            0xe1a0f00e,
                                                            0xe92d0700,
                                                            0xe3a03000,
                                                            0xe3a08000,
                                                            0xe3a09000,
                                                            0xe3a0a000,
                                                            0xe2522020,
                                                            0x28a00708,
                                                            0x28a00708,
                                                            0x22522020,
                                                            0x2afffffb,
                                                            0xe8bd0700,
                                                            0xeaffffee,
                                                            0xe1a0f00e,
                                                    };
                    int index,j;
                    int file_size = m_bin.Length;
                    
                    long bin_size = file_size/4;
                    for(index = 0; index < bin_size; index++)
                    {
                            if (BitConverter.ToUInt32(m_bin, index << 2) == m_mark[0])
                            {
                                for(j=0; j < m_mark.Length; j++)
                                {
                                    if (BitConverter.ToUInt32(m_bin, (index + j) << 2) != m_mark[j] )
                                    {
                                        break;
                                    }
                                }
                                if (j == m_mark.Length)
                                {
                                //g_region_info = (t_region*)&bin[index+j];
                                    int i=0;
                                    
                                    g_region[ (int)REGION_Type.Load__EMIINIT_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Load__INTSRAM_DATA_PREINIT__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__SINGLE_BANK_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Load__INTSRAM_DATA__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Load__INTSRAM_MULTIMEDIA__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__PAGE_TABLE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__PAGE_TABLE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__PAGE_TABLE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__PAGE_TABLE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__PAGE_TABLE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM_PROTECTED_RES__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_PROTECTED_RES__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Load__CACHED_CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_TX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_RX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Length ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Base ]=BitConverter.ToUInt32(m_bin, (index + j + i++) << 2); 
                                    Image__ROM__Base = g_region[ (int)REGION_Type.Load__EXTSRAM__Base ] & 0xFF000000;
                                    
                                    m_bin_type = BIN_Type.MTK6252_10A;
                                     return true;
                                }
                            }
                    }
                    return false;
            }
            
            bool IsMTK6253_09A()
            {
             UInt32[] m_mark = new UInt32[]
                        {
                            0xe1a0700e,
                            0xe59f0230,
                            0xe59f1230,
                            0xe1a02001,
                            0xe59f422c,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2224,
                            0xe59f0224,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe12fff17,
                            0xe1a0700e,
                            0xe59f0210,
                            0xe59f1210,
                            0xe1a02001,
                            0xe59f420c,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2204,
                            0xe59f0204,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe12fff17,
                            0xe1a0700e,
                            0xe59f01f0,
                            0xe59f11f0,
                            0xe1a02001,
                            0xe59f41ec,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f21e4,
                            0xe59f01e4,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f01d8,
                            0xe59f11d8,
                            0xe1a02001,
                            0xe59f41d4,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f21cc,
                            0xe59f01cc,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f01c0,
                            0xe59f11c0,
                            0xe1a02001,
                            0xe59f41bc,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f21b4,
                            0xe59f01b4,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f21a8,
                            0xe59f01a8,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f019c,
                            0xe59f119c,
                            0xe1a02001,
                            0xe59f4198,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2190,
                            0xe59f0190,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f0184,
                            0xe59f1184,
                            0xe1a02001,
                            0xe59f4180,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2178,
                            0xe59f0178,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f016c,
                            0xe59f116c,
                            0xe1a02001,
                            0xe59f4168,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2160,
                            0xe59f0160,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe59f0154,
                            0xe59f1154,
                            0xe1a02001,
                            0xe59f4150,
                            0xe0822004,
                            0xebfffffe,
                            0xe59f2148,
                            0xe59f0148,
                            0xe1a01000,
                            0xe0811002,
                            0xebfffffe,
                            0xe12fff17,
                            0xe92d00ff,
                            0xe1a0700e,
                            0xe1a0e007,
                            0xe8bd00ff,
                            0xe12fff1e,
                            0xe1510002,
                            0x34903004,
                            0x34813004,
                            0x3afffffe,
                            0xe1a0f00e,
                            0xe1510002,
                            0xa4103004,
                            0xa4013004,
                            0xaafffffe,
                            0xe1a0f00e,
                            0xe3a02000,
                            0xe1500001,
                            0x34802004,
                            0x3afffffe,
                            0xe1a0f00e,
                            0xe92d0700,
                            0xe3a03000,
                            0xe3a08000,
                            0xe3a09000,
                            0xe3a0a000,
                            0xe2522020,
                            0x28a00708,
                            0x28a00708,
                            0x22522020,
                            0x2afffffe,
                            0xe8bd0700,
                            0xeafffffe,
                            0xe1a0f00e,
                    };
                    int index,j;
                    int file_size = m_bin.Length;
                    
                    long bin_size = file_size/4;
                    for(index = 0; index < bin_size; index++)
                    {
                            if (BitConverter.ToUInt32(m_bin, index << 2) == m_mark[0])
                            {
                                for(j=0; j < m_mark.Length; j++)
                                {
                                    if (BitConverter.ToUInt32(m_bin, (index + j) << 2) != m_mark[j] && (m_mark[j] &0xFFFFFF) != 0x00fffffe)
                                    {
                                        break;
                                    }
                                }
                                if (j == m_mark.Length)
                                {
                                    int i=0;
                                    g_region[ (int)REGION_Type.Load__EMIINIT_CODE__Base ]                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Base ]                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Length ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Length ]             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Base ]               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_MULTIMEDIA__Base ]              =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Base ]             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Length ]           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Length ]       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Base ]         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM__Base ]                         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Base ]                        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Length ]                      =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Length ]                  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Base ]                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_CODE__Base ]                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Base ]                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Length ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Length ]             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Base ]               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_DATA__Base ]                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Base ]                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Length ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Length ]             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Base ]               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CODE_PATCH_CODE__Base ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ]                =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Length ]              =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Length ]          =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Base ]            =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CACHED_CODE_PATCH_CODE__Base ]          =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base ]         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Length ]       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Base ]     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_TX__Base ]                  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Base ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Length ]               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Length ]           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Base ]             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_RX__Base ]                  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Base ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Length ]               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Length ]           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Base ]             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    Image__ROM__Base = g_region[ (int)REGION_Type.Load__EXTSRAM__Base ] & 0xFF000000;
                                    
                                    m_bin_type = BIN_Type.MTK6253_09A;
                                    return true;
                                }
                            }
                    }
                    return false;
                    
            }
            bool IsMTK6252_10A_1116()
            {
            
             UInt32[] m_mark = new UInt32[]
                                            {
                                            0xe1a0700e  ,
                                            0xe59f03f0  ,
                                            0xe59f13f0  ,
                                            0xe1a02001  ,
                                            0xe59f43ec  ,
                                            0xe0822004  ,
                                            0xeb0000db  ,
                                            0xe59f23e4  ,
                                            0xe59f03e4  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb0000e5  ,
                                            0xe59f03d8  ,
                                            0xe59f13d8  ,
                                            0xe1a02001  ,
                                            0xe59f43d4  ,
                                            0xe0822004  ,
                                            0xeb0000d0  ,
                                            0xe59f23cc  ,
                                            0xe59f03cc  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb0000da  ,
                                            0xe59f03c0  ,
                                            0xe59f13c0  ,
                                            0xe1a02001  ,
                                            0xe59f43bc  ,
                                            0xe0822004  ,
                                            0xeb0000c5  ,
                                            0xe59f23b4  ,
                                            0xe59f03b4  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb0000cf  ,
                                            0xe12fff17  ,
                                            0xe1a0700e  ,
                                            0xe59f03a0  ,
                                            0xe59f13a0  ,
                                            0xe1a02001  ,
                                            0xe59f439c  ,
                                            0xe0822004  ,
                                            0xeb0000b8  ,
                                            0xe59f2394  ,
                                            0xe59f0394  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb0000c2  ,
                                            0xe59f0388  ,
                                            0xe59f1388  ,
                                            0xe1a02001  ,
                                            0xe59f4384  ,
                                            0xe0822004  ,
                                            0xeb0000ad  ,
                                            0xe59f237c  ,
                                            0xe59f037c  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb0000b7  ,
                                            0xe12fff17  ,
                                            0xe1a0700e  ,
                                            0xe59f0368  ,
                                            0xe59f1368  ,
                                            0xe1a02001  ,
                                            0xe59f4364  ,
                                            0xe0822004  ,
                                            0xeb0000a0  ,
                                            0xe59f235c  ,
                                            0xe59f035c  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb0000aa  ,
                                            0xe59f0350  ,
                                            0xe59f1350  ,
                                            0xe1a02001  ,
                                            0xe59f434c  ,
                                            0xe0822004  ,
                                            0xeb000095  ,
                                            0xe59f2344  ,
                                            0xe59f0344  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb00009f  ,
                                            0xe59f0338  ,
                                            0xe59f1338  ,
                                            0xe1a02001  ,
                                            0xe59f4334  ,
                                            0xe0822004  ,
                                            0xeb00008a  ,
                                            0xe59f232c  ,
                                            0xe59f032c  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb000094  ,
                                            0xe59f0320  ,
                                            0xe59f1320  ,
                                            0xe1a02001  ,
                                            0xe59f431c  ,
                                            0xe0822004  ,
                                            0xeb00007f  ,
                                            0xe59f2314  ,
                                            0xe59f0314  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb000089  ,
                                            0xe59f0308  ,
                                            0xe59f1308  ,
                                            0xe1a02001  ,
                                            0xe59f4304  ,
                                            0xe0822004  ,
                                            0xeb000074  ,
                                            0xe59f22fc  ,
                                            0xe59f02fc  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb00007e  ,
                                            0xe59f02f0  ,
                                            0xe59f12f0  ,
                                            0xe1a02001  ,
                                            0xe59f42ec  ,
                                            0xe2444004  ,
                                            0xe0800004  ,
                                            0xe0811004  ,
                                            0xeb00006c  ,
                                            0xe59f22dc  ,
                                            0xe59f02dc  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb000071  ,
                                            0xe59f02d0  ,
                                            0xe59f12d0  ,
                                            0xe1a02001  ,
                                            0xe59f42cc  ,
                                            0xe0822004  ,
                                            0xeb00005c  ,
                                            0xe59f22c4  ,
                                            0xe59f02c4  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb000066  ,
                                            0xe59f02b8  ,
                                            0xe59f12b8  ,
                                            0xe1a02001  ,
                                            0xe59f42b4  ,
                                            0xe0822004  ,
                                            0xeb000051  ,
                                            0xe59f22ac  ,
                                            0xe59f02ac  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb00005b  ,
                                            0xe59f02a0  ,
                                            0xe59f12a0  ,
                                            0xe1a02001  ,
                                            0xe59f429c  ,
                                            0xe0822004  ,
                                            0xeb000046  ,
                                            0xe59f2294  ,
                                            0xe59f0294  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb000050  ,
                                            0xe59f0198  ,
                                            0xe59f1198  ,
                                            0xe1a02001  ,
                                            0xe59f4194  ,
                                            0xe0822004  ,
                                            0xeb00003b  ,
                                            0xe59f218c  ,
                                            0xe59f018c  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb000045  ,
                                            0xe59f225c  ,
                                            0xe59f025c  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb000040  ,
                                            0xe59f0250  ,
                                            0xe59f1250  ,
                                            0xe1a02001  ,
                                            0xe59f424c  ,
                                            0xe0822004  ,
                                            0xeb00002b  ,
                                            0xe59f2244  ,
                                            0xe59f0244  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb000035  ,
                                            0xe59f0238  ,
                                            0xe59f1238  ,
                                            0xe1a02001  ,
                                            0xe59f4234  ,
                                            0xe0822004  ,
                                            0xeb000020  ,
                                            0xe59f222c  ,
                                            0xe59f022c  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb00002a  ,
                                            0xe59f0220  ,
                                            0xe59f1220  ,
                                            0xe1a02001  ,
                                            0xe59f421c  ,
                                            0xe0822004  ,
                                            0xeb000015  ,
                                            0xe59f2214  ,
                                            0xe59f0214  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb00001f  ,
                                            0xe59f0208  ,
                                            0xe59f1208  ,
                                            0xe1a02001  ,
                                            0xe59f4204  ,
                                            0xe0822004  ,
                                            0xeb00000a  ,
                                            0xe59f21fc  ,
                                            0xe59f01fc  ,
                                            0xe1a01000  ,
                                            0xe0811002  ,
                                            0xeb000014  ,
                                            0xe12fff17  ,
                                            0xe92d00ff  ,
                                            0xe1a0700e  ,
                                            0xe1a0e007  ,
                                            0xe8bd00ff  ,
                                            0xe12fff1e  ,
                                            0xe1510002  ,
                                            0x34903004  ,
                                            0x34813004  ,
                                            0x3afffffb  ,
                                            0xe1a0f00e  ,
                                            0xe1510002  ,
                                            0xa4103004  ,
                                            0xa4013004  ,
                                            0xaafffffb  ,
                                            0xe1a0f00e  ,
                                            0xe3a02000  ,
                                            0xe1500001  ,
                                            0x34802004  ,
                                            0x3afffffb  ,
                                            0xe1a0f00e  ,
                                            0xe92d0700  ,
                                            0xe3a03000  ,
                                            0xe3a08000  ,
                                            0xe3a09000  ,
                                            0xe3a0a000  ,
                                            0xe2522020  ,
                                            0x28a00708  ,
                                            0x28a00708  ,
                                            0x22522020  ,
                                            0x2afffffb  ,
                                            0xe8bd0700  ,
                                            0xeaffffee  ,
                                            0xe1a0f00e  ,
                                            };
                    int index,j;
                    int file_size = m_bin.Length;
                    
                    long bin_size = file_size/4;
                    for(index = 0; index < bin_size; index++)
                    {
                            if (BitConverter.ToUInt32(m_bin, index << 2) == m_mark[0])
                            {
                                for(j=0; j < m_mark.Length; j++)
                                {
                                    if (BitConverter.ToUInt32(m_bin, (index + j) << 2) != m_mark[j] )
                                    {
                                        break;
                                    }
                                }
                                if (j == m_mark.Length)
                                {
                                //g_region_info = (t_region*)&bin[index+j];
                                    int i=0;
                                    
                                    g_region[ (int)REGION_Type.Load__EMIINIT_CODE__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_DATA_PREINIT__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__ZI__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA_PREINIT__ZI__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__SINGLE_BANK_CODE__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__ZI__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SINGLE_BANK_CODE__ZI__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_DATA__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_MULTIMEDIA__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__PAGE_TABLE__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PAGE_TABLE__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PAGE_TABLE__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PAGE_TABLE__ZI__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PAGE_TABLE__ZI__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__ZI__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__ZI__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM_PROTECTED_RES__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__ZI__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__ZI__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__Length ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__ZI__Length ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE__ZI__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM_CODE__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Length ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__ZI__Length ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM_CODE__ZI__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Length ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Length ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__Length ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Length ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM__ZI__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_PROTECTED_RES__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Length ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__ZI__Length ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__ZI__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_CODE__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Length ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Base ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CODE_PATCH_CODE__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Length ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Length ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Base ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CACHED_CODE_PATCH_CODE__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_TX__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_DSP_RX__Base ]  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Length ]   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Base ] =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    Image__ROM__Base = g_region[ (int)REGION_Type.Load__EXTSRAM__Base ] & 0xFF000000;
                                    
                                    m_bin_type = BIN_Type.MTK6252_10A_1116;
                                     return true;
                                }
                            }
                    }
                    return false;
            }
			bool IsMTK6252_11B()
            {
            
             UInt32[] m_mark = new UInt32[]
                                            {
                                            
											0xe1a0700e,
											0xe59f0400,
											0xe59f1400,
											0xe1a02001,
											0xe59f43fc,
											0xe0822004,
											0xeb0000df,
											0xe59f23f4,
											0xe59f03f4,
											0xe1a01000,
											0xe0811002,
											0xeb0000e9,
											0xe59f03e8,
											0xe59f13e8,
											0xe1a02001,
											0xe59f43e4,
											0xe0822004,
											0xeb0000d4,
											0xe59f23dc,
											0xe59f03dc,
											0xe1a01000,
											0xe0811002,
											0xeb0000de,
											0xe59f03d0,
											0xe59f13d0,
											0xe1a02001,
											0xe59f43cc,
											0xe0822004,
											0xeb0000c9,
											0xe59f23c4,
											0xe59f03c4,
											0xe1a01000,
											0xe0811002,
											0xeb0000d3,
											0xe12fff17,
											0xe1a0700e,
											0xe59f03b0,
											0xe59f13b0,
											0xe1a02001,
											0xe59f43ac,
											0xe0822004,
											0xeb0000bc,
											0xe59f23a4,
											0xe59f03a4,
											0xe1a01000,
											0xe0811002,
											0xeb0000c6,
											0xe12fff17,
											0xe1a0700e,
											0xe59f0390,
											0xe59f1390,
											0xe1a02001,
											0xe59f438c,
											0xe0822004,
											0xeb0000af,
											0xe59f2384,
											0xe59f0384,
											0xe1a01000,
											0xe0811002,
											0xeb0000b9,
											0xe12fff17,
											0xe1a0700e,
											0xe59f0370,
											0xe59f1370,
											0xe1a02001,
											0xe59f436c,
											0xe0822004,
											0xeb0000a2,
											0xe59f2364,
											0xe59f0364,
											0xe1a01000,
											0xe0811002,
											0xeb0000ac,
											0xe59f0358,
											0xe59f1358,
											0xe1a02001,
											0xe59f4354,
											0xe0822004,
											0xeb000097,
											0xe59f234c,
											0xe59f034c,
											0xe1a01000,
											0xe0811002,
											0xeb0000a1,
											0xe59f0340,
											0xe59f1340,
											0xe1a02001,
											0xe59f433c,
											0xe0822004,
											0xeb00008c,
											0xe59f2334,
											0xe59f0334,
											0xe1a01000,
											0xe0811002,
											0xeb000096,
											0xe59f0328,
											0xe59f1328,
											0xe1a02001,
											0xe59f4324,
											0xe2444004,
											0xe0800004,
											0xe0811004,
											0xeb000084,
											0xe59f0314,
											0xe59f1314,
											0xe1a02001,
											0xe59f4310,
											0xe0822004,
											0xeb000079,
											0xe59f2308,
											0xe59f0308,
											0xe1a01000,
											0xe0811002,
											0xeb000083,
											0xe59f02fc,
											0xe59f12fc,
											0xe1a02001,
											0xe59f42f8,
											0xe2444004,
											0xe0800004,
											0xe0811004,
											0xeb000071,
											0xe59f22e8,
											0xe59f02e8,
											0xe1a01000,
											0xe0811002,
											0xeb000076,
											0xe59f22dc,
											0xe59f02dc,
											0xe1a01000,
											0xe0811002,
											0xeb000071,
											0xe59f02d0,
											0xe59f12d0,
											0xe1a02001,
											0xe59f42cc,
											0xe0822004,
											0xeb00005c,
											0xe59f22c4,
											0xe59f02c4,
											0xe1a01000,
											0xe0811002,
											0xeb000066,
											0xe59f02b8,
											0xe59f12b8,
											0xe1a02001,
											0xe59f42b4,
											0xe0822004,
											0xeb000051,
											0xe59f22ac,
											0xe59f02ac,
											0xe1a01000,
											0xe0811002,
											0xeb00005b,
											0xe59f02a0,
											0xe59f12a0,
											0xe1a02001,
											0xe59f429c,
											0xe0822004,
											0xeb000046,
											0xe59f2294,
											0xe59f0294,
											0xe1a01000,
											0xe0811002,
											0xeb000050,
											0xe59f0198,
											0xe59f1198,
											0xe1a02001,
											0xe59f4194,
											0xe0822004,
											0xeb00003b,
											0xe59f218c,
											0xe59f018c,
											0xe1a01000,
											0xe0811002,
											0xeb000045,
											0xe59f225c,
											0xe59f025c,
											0xe1a01000,
											0xe0811002,
											0xeb000040,
											0xe59f0250,
											0xe59f1250,
											0xe1a02001,
											0xe59f424c,
											0xe0822004,
											0xeb00002b,
											0xe59f2244,
											0xe59f0244,
											0xe1a01000,
											0xe0811002,
											0xeb000035,
											0xe59f0238,
											0xe59f1238,
											0xe1a02001,
											0xe59f4234,
											0xe0822004,
											0xeb000020,
											0xe59f222c,
											0xe59f022c,
											0xe1a01000,
											0xe0811002,
											0xeb00002a,
											0xe59f0220,
											0xe59f1220,
											0xe1a02001,
											0xe59f421c,
											0xe0822004,
											0xeb000015,
											0xe59f2214,
											0xe59f0214,
											0xe1a01000,
											0xe0811002,
											0xeb00001f,
											0xe59f0208,
											0xe59f1208,
											0xe1a02001,
											0xe59f4204,
											0xe0822004,
											0xeb00000a,
											0xe59f21fc,
											0xe59f01fc,
											0xe1a01000,
											0xe0811002,
											0xeb000014,
											0xe12fff17,
											0xe92d00ff,
											0xe1a0700e,
											0xe1a0e007,
											0xe8bd00ff,
											0xe12fff1e,
											0xe1510002,
											0x34903004,
											0x34813004,
											0x3afffffb,
											0xe1a0f00e,
											0xe1510002,
											0xa4103004,
											0xa4013004,
											0xaafffffb,
											0xe1a0f00e,
											0xe3a02000,
											0xe1500001,
											0x34802004,
											0x3afffffb,
											0xe1a0f00e,
											0xe92d0700,
											0xe3a03000,
											0xe3a08000,
											0xe3a09000,
											0xe3a0a000,
											0xe2522020,
											0x28a00708,
											0x28a00708,
											0x22522020,
											0x2afffffb,
											0xe8bd0700,
											0xeaffffee,
											0xe1a0f00e,
                                            
                                            };
                    int index,j;
                    int file_size = m_bin.Length;
                    
                    long bin_size = file_size/4;
                    for(index = 0; index < bin_size; index++)
                    {
                            if (BitConverter.ToUInt32(m_bin, index << 2) == m_mark[0])
                            {
                                for(j=0; j < m_mark.Length; j++)
                                {
                                    if (BitConverter.ToUInt32(m_bin, (index + j) << 2) != m_mark[j] )
                                    {
                                        break;
                                    }
                                }
                                if (j == m_mark.Length)
                                {
                                //g_region_info = (t_region*)&bin[index+j];
                                    int i=0;
                                    
									g_region[(int)REGION_Type.Load__EMIINIT_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EMIINIT_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EMIINIT_CODE__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EMIINIT_CODE__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EMIINIT_CODE__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__INTSRAM_DATA_PREINIT__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_DATA_PREINIT__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_DATA_PREINIT__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_DATA_PREINIT__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__SINGLE_BANK_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__SINGLE_BANK_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__SINGLE_BANK_CODE__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__SINGLE_BANK_CODE__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__SINGLE_BANK_CODE__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__INTSRAM_MULTIMEDIA__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__INTSRAM_DATA__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_DATA__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_DATA__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_DATA__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_DATA__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__PAGE_TABLE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__PAGE_TABLE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__PAGE_TABLE__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__PAGE_TABLE__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__PAGE_TABLE__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__CACHED_EXTSRAM__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__CACHED_EXTSRAM_PROTECTED_RES__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM_PROTECTED_RES__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__CACHED_EXTSRAM_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM_CODE__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM_CODE__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_EXTSRAM_CODE__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_ZI__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_ZI__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__EXTSRAM__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__EXTSRAM_PROTECTED_RES__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_PROTECTED_RES__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__INTSRAM_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_CODE__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_CODE__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__INTSRAM_CODE__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_LARGEPOOL_NORMAL__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__CODE_PATCH_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CODE_PATCH_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CODE_PATCH_CODE__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__CACHED_CODE_PATCH_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__EXTSRAM_DSP_TX__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_DSP_TX__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_DSP_TX__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_DSP_TX__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Load__EXTSRAM_DSP_RX__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_DSP_RX__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_DSP_RX__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Length]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
									g_region[(int)REGION_Type.Image__EXTSRAM_DSP_RX__ZI__Base]=BitConverter.ToUInt32(m_bin,(index+j+i++)<<2);
                                    Image__ROM__Base = g_region[ (int)REGION_Type.Load__EXTSRAM__Base ] & 0xFF000000;
                                    
                                    m_bin_type = BIN_Type.MTK6252_11B;
                                     return true;
                                }
                            }
                    }
                    return false;
            }
            bool IsMTK6276_11A()
            {
             UInt32[] m_mark = new UInt32[]
                        {
                            
                            0xe1a0700e,
                            0xe59f04a8,
                            0xe59f14a8,
                            0xe1a02001,
                            0xe59f44a4,
                            0xe0822004,
                            0xeb000109,
                            0xe59f249c,
                            0xe59f049c,
                            0xe1a01000,
                            0xe0811002,
                            0xeb000113,
                            0xe12fff17,
                            0xe1a0700e,
                            0xe12fff17,
                            0xe1a0700e,
                            0xe12fff17,
                            0xe1a0700e,
                            0xe59f0478,
                            0xe59f1478,
                            0xe1a02001,
                            0xe59f4474,
                            0xe0822004,
                            0xeb0000f8,
                            0xe59f246c,
                            0xe59f046c,
                            0xe1a01000,
                            0xe0811002,
                            0xeb000102,
                            0xe59f0460,
                            0xe59f1460,
                            0xe1a02001,
                            0xe59f445c,
                            0xe0822004,
                            0xeb0000ed,
                            0xe59f2454,
                            0xe59f0454,
                            0xe1a01000,
                            0xe0811002,
                            0xeb0000f7,
                            0xe59f0448,
                            0xe59f1448,
                            0xe1a02001,
                            0xe59f4444,
                            0xe0822004,
                            0xeb0000e2,
                            0xe59f243c,
                            0xe59f043c,
                            0xe1a01000,
                            0xe0811002,
                            0xeb0000ec,
                            0xe59f0430,
                            0xe59f1430,
                            0xe1a02001,
                            0xe59f442c,
                            0xe0822004,
                            0xeb0000d7,
                            0xe59f2424,
                            0xe59f0424,
                            0xe1a01000,
                            0xe0811002,
                            0xeb0000e1,
                            0xe59f0418,
                            0xe59f1418,
                            0xe1a02001,
                            0xe59f4414,
                            0xe0822004,
                            0xeb0000cc,
                            0xe59f240c,
                            0xe59f040c,
                            0xe1a01000,
                            0xe0811002,
                            0xeb0000d6,
                            0xe59f0400,
                            0xe59f1400,
                            0xe1a02001,
                            0xe59f43fc,
                            0xe2444004,
                            0xe0800004,
                            0xe0811004,
                            0xeb0000c4,
                            0xe59f03ec,
                            0xe59f13ec,
                            0xe1a02001,
                            0xe59f43e8,
                            0xe2444004,
                            0xe0800004,
                            0xe0811004,
                            0xeb0000bc,
                            0xe59f03d8,
                            0xe59f13d8,
                            0xe1a02001,
                            0xe59f43d4,
                            0xe2444004,
                            0xe0800004,
                            0xe0811004,
                            0xeb0000b4,
                            0xe59f03c4,
                            0xe59f13c4,
                            0xe1a02001,
                            0xe59f43c0,
                            0xe0822004,
                            0xeb0000a9,
                            0xe59f23b8,
                            0xe59f03b8,
                            0xe1a01000,
                            0xe0811002,
                            0xeb0000b3,
                            0xe59f23ac,
                            0xe59f03ac,
                            0xe1a01000,
                            0xe0811002,
                            0xeb0000ae,
                            0xe59f23a0,
                            0xe59f03a0,
                            0xe1a01000,
                            0xe0811002,
                            0xeb0000a9,
                            0xe59f2394,
                            0xe59f0394,
                            0xe1a01000,
                            0xe0811002,
                            0xeb0000a4,
                            0xe59f2388,
                            0xe59f0388,
                            0xe1a01000,
                            0xe0811002,
                            0xeb00009f,
                            0xe59f237c,
                            0xe59f037c,
                            0xe1a01000,
                            0xe0811002,
                            0xeb00009a,
                            0xe59f2370,
                            0xe59f0370,
                            0xe1a01000,
                            0xe0811002,
                            0xeb000095,
                            0xe12fff17,
                            0xe92d00ff,
                            0xe1a0700e,
                            0xe59f0358,
                            0xe59f1358,
                            0xe1a02001,
                            0xe59f4354,
                            0xe0822004,
                            0xeb00007d,
                            0xe59f234c,
                            0xe59f034c,
                            0xe1a01000,
                            0xe0811002,
                            0xeb000087,
                            0xe59f0340,
                            0xe59f1340,
                            0xe1a02001,
                            0xe59f433c,
                            0xe0822004,
                            0xeb000072,
                            0xe59f2334,
                            0xe59f0334,
                            0xe1a01000,
                            0xe0811002,
                            0xeb00007c,
                            0xe59f0328,
                            0xe59f1328,
                            0xe1a02001,
                            0xe59f4324,
                            0xe0822004,
                            0xeb000067,
                            0xe59f231c,
                            0xe59f031c,
                            0xe1a01000,
                            0xe0811002,
                            0xeb000071,
                            0xe59f0310,
                            0xe59f1310,
                            0xe1a02001,
                            0xe59f430c,
                            0xe0822004,
                            0xeb00005c,
                            0xe59f2304,
                            0xe59f0304,
                            0xe1a01000,
                            0xe0811002,
                            0xeb000066,
                            0xe59f02f8,
                            0xe59f12f8,
                            0xe1a02001,
                            0xe59f42f4,
                            0xe0822004,
                            0xeb000051,
                            0xe59f22ec,
                            0xe59f02ec,
                            0xe1a01000,
                            0xe0811002,
                            0xeb00005b,
                            0xe59f02e0,
                            0xe59f12e0,
                            0xe1a02001,
                            0xe59f42dc,
                            0xe0822004,
                            0xeb000046,
                            0xe59f22d4,
                            0xe59f02d4,
                            0xe1a01000,
                            0xe0811002,
                            0xeb000050,
                            0xe59f02c8,
                            0xe59f12c8,
                            0xe1a02001,
                            0xe59f42c4,
                            0xe2444004,
                            0xe0800004,
                            0xe0811004,
                            0xeb00003e,
                            0xe59f02b4,
                            0xe59f12b4,
                            0xe1a02001,
                            0xe59f42b0,
                            0xe2444004,
                            0xe0800004,
                            0xe0811004,
                            0xeb000036,
                            0xe59f02a0,
                            0xe59f12a0,
                            0xe1a02001,
                            0xe59f429c,
                            0xe2444004,
                            0xe0800004,
                            0xe0811004,
                            0xeb00002e,
                            0xe59f028c,
                            0xe59f128c,
                            0xe1a02001,
                            0xe59f4288,
                            0xe2444004,
                            0xe0800004,
                            0xe0811004,
                            0xeb000026,
                            0xe59f0278,
                            0xe59f1278,
                            0xe1a02001,
                            0xe59f4274,
                            0xe2444004,
                            0xe0800004,
                            0xe0811004,
                            0xeb00001e,
                            0xe59f0264,
                            0xe59f1264,
                            0xe1a02001,
                            0xe59f4260,
                            0xe2444004,
                            0xe0800004,
                            0xe0811004,
                            0xeb000016,
                            0xe59f2250,
                            0xe59f0250,
                            0xe1a01000,
                            0xe0811002,
                            0xeb00001b,
                            0xe59f2244,
                            0xe59f0244,
                            0xe1a01000,
                            0xe0811002,
                            0xeb000016,
                            0xe59f2238,
                            0xe59f0238,
                            0xe1a01000,
                            0xe0811002,
                            0xeb000011,
                            0xe1a0e007,
                            0xe8bd00ff,
                            0xe12fff1e,
                            0xe1510002,
                            0x34903004,
                            0x34813004,
                            0x3afffffb,
                            0xe1a0f00e,
                            0xe1510002,
                            0xa4103004,
                            0xa4013004,
                            0xaafffffb,
                            0xe1a0f00e,
                            0xe3a02000,
                            0xe1500001,
                            0x34802004,
                            0x3afffffb,
                            0xe1a0f00e,
                            0xe92d0700,
                            0xe3a03000,
                            0xe3a08000,
                            0xe3a09000,
                            0xe3a0a000,
                            0xe2522020,
                            0x28a00708,
                            0x28a00708,
                            0x22522020,
                            0x2afffffb,
                            0xe8bd0700,
                            0xeaffffee,
                            0xe1a0f00e,
                            
                    };
                    int index,j;
                    int file_size = m_bin.Length;
                    
                    long bin_size = file_size/4;
                    for(index = 0; index < bin_size; index++)
                    {
                            if (BitConverter.ToUInt32(m_bin, index << 2) == m_mark[0])
                            {
                                for(j=0; j < m_mark.Length; j++)
                                {
                                    if (BitConverter.ToUInt32(m_bin, (index + j) << 2) != m_mark[j] && (m_mark[j] &0xFFFFFF) != 0x00fffffe)
                                    {
                                        break;
                                    }
                                }
                                if (j == m_mark.Length)
                                {
                                    int i=0;
                                    
                                    g_region[ (int)REGION_Type.Load__EMIINIT_CODE__Base ]                                                =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Base ]                                               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__Length ]                                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Length ]                                         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EMIINIT_CODE__ZI__Base ]                                           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_CODE__Base ]                                                =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Base ]                                               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__Length ]                                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Length ]                                         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_CODE__ZI__Base ]                                           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_DATA__Base ]                                                =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Base ]                                               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__Length ]                                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Length ]                                         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_DATA__ZI__Base ]                                           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__L2TCM_CODE__Base ]                                                  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__L2TCM_CODE__Base ]                                                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__L2TCM_CODE__Length ]                                               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__L2TCM_CODE__ZI__Length ]                                           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__L2TCM_CODE__ZI__Base ]                                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__L2TCM_DATA__Base ]                                                  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__L2TCM_DATA__Base ]                                                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__L2TCM_DATA__Length ]                                               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__L2TCM_DATA__ZI__Length ]                                           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__L2TCM_DATA__ZI__Base ]                                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__FLEXL2_DATA__Base ]                                                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__FLEXL2_DATA__Base ]                                                =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__FLEXL2_DATA__Length ]                                              =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__FLEXL2_DATA__ZI__Length ]                                          =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__FLEXL2_DATA__ZI__Base ]                                            =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Base ]                        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Base ]                       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM_PROTECTED_RES__Length ]                     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__PRIMARY_CACHED_EXTSRAM__Base ]                                      =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM__Base ]                                     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM__Length ]                                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__PRIMARY_EXTSRAM__Base ]                                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_EXTSRAM__Base ]                                            =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_EXTSRAM__Length ]                                          =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__ROM_1__Base ]                                                       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__ROM_1__Base ]                                                      =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__ROM_1__Length ]                                                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__ROM_1__ZI__Length ]                                                =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__ROM_1__ZI__Base ]                                                  =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_EXTSRAM__ZI__Length ]                                      =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_EXTSRAM__ZI__Base ]                                        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM__ZI__Length ]                               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__PRIMARY_CACHED_EXTSRAM__ZI__Base ]                                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM__ZI__Length ]                                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM__ZI__Base ]                                      =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_ZI__ZI__Length ]                                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_ZI__ZI__Base ]                                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__ZI__Length ]                                       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__ZI__Base ]                                         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_GADGET__ZI__Length ]                                       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_GADGET__ZI__Base ]                                         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM_DSP_TX__Base ]                                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_TX__Base ]                                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_TX__Length ]                                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_TX__ZI__Length ]                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_TX__ZI__Base ]                               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM_DSP_RX__Base ]                                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_RX__Base ]                                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_RX__Length ]                                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_RX__ZI__Length ]                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_DSP_RX__ZI__Base ]                               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Base ]                    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Base ]                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__Length ]                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__ZI__Length ]             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_MCU_NC_DSP_NC_SHAREMEM__ZI__Base ]               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__INTSRAM_MULTIMEDIA__Base ]                                          =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Base ]                                         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__Length ]                                       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Length ]                                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__INTSRAM_MULTIMEDIA__ZI__Base ]                                     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CODE_PATCH_CODE__Base ]                                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Base ]                                            =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__Length ]                                          =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Length ]                                      =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CODE_PATCH_CODE__ZI__Base ]                                        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CACHED_CODE_PATCH_CODE__Base ]                                      =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Base ]                                     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__Length ]                                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Length ]                               =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_CODE_PATCH_CODE__ZI__Base ]                                 =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base ]              =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Base ]             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_RW__Length ]           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__EXTSRAM_GADGET__Base ]                                              =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_GADGET__Base ]                                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__EXTSRAM_GADGET__Length ]                                           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__CACHED_EXTSRAM__Base ]                                              =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Base ]                                             =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__CACHED_EXTSRAM__Length ]                                           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ]           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Base ]          =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_RW__Length ]        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM__Base ]                                           =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM__Base ]                                          =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM__Length ]                                        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Load__SECONDARY_EXTSRAM_RW__Base ]                                        =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_RW__Base ]                                       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_RW__Length ]                                     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Length ]    =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_NONCACHEABLE_ZI__ZI__Base ]      =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_ZI__ZI__Length ]       =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__DYNAMIC_CACHEABLE_EXTSRAM_DEFAULT_CACHEABLE_ZI__ZI__Base ]         =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_LARGEPOOL_NORMAL__ZI__Length ]                   =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    g_region[ (int)REGION_Type.Image__SECONDARY_EXTSRAM_LARGEPOOL_NORMAL__ZI__Base ]                     =BitConverter.ToUInt32(m_bin, (index + j + i++) << 2);
                                    
                                    Image__ROM__Base = g_region[ (int)REGION_Type.Load__EXTSRAM__Base ] & 0xFF000000;
                                    
                                    m_bin_type = BIN_Type.MTK6276_11A;
                                    return true;
                                }
                            }
                    }
                    return false;
                    
            }
     }
}




