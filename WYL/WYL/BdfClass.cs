using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WYL
{
    public class BdfClass
    {
        public const int UICODE_TOTAL = 0x10000;
        string m_filename;
        string m_header;
        public byte m_width;
        public byte m_height;
        public FontBitmap[] m_FontArray = new FontBitmap[UICODE_TOTAL];
        public BdfClass()
        {
        }
        public int FontTotal()
        {
            int total =0;
            for(int i=0; i <UICODE_TOTAL; i++)
            {
                if(m_FontArray[i]!= null)
                    total ++;
            }
            return total;
        }
        public bool isExistFont()
        {
            for(int i=0; i <UICODE_TOTAL; i++)
            {
                if(m_FontArray[i]!= null)
                    return true;
            }
            return false;
        }

        public string GetTextString()
        {
            string temp = "";
            temp += m_header;
            temp +="CHARS " + FontTotal() + "\r\n";

            for (int i = 0; i < UICODE_TOTAL; i++)
            {
                if(m_FontArray[i]!= null)
                    temp += ((FontBitmap)m_FontArray[i]).m_text;             

            }
            temp += "ENDFONT" + "\r\n";
            return temp;
        }
        public byte[] GetTextBytes()
        {
            return System.Text.Encoding.ASCII.GetBytes(GetTextString());
        }    
        public void SaveFile()
        {

            try
            {
                StreamWriter sw;
                using (sw = new StreamWriter(m_filename))
                {
                    sw.Write(m_header + "CHARS " + FontTotal() + "\r\n");
                    

                    for (int i = 0; i < UICODE_TOTAL; i++)
                    {
                        if (m_FontArray[i] != null)
                            sw.Write(((FontBitmap)m_FontArray[i]).m_text);

                    }
                    sw.Write("ENDFONT" + "\r\n");
           
                }


                //File.WriteAllBytes(m_filename, GetTextBytes());
                //File.WriteAllText(m_filename, GetTextString());
            }
            catch
            {
                MessageBox.Show("Save Error!");
            }
        }

        public void SaveFile(string filename)
        {
            m_filename = filename;
            SaveFile();
        }
        
        public bool LoadData(string CustFontData, string RangeDataS, string Width,string DWidth,string Offset,string Data)
        {
            sCustFontData custFontData;
            RangeData rangeData;
            ByteData s_data;
            ByteData width;
            ByteData dWidth;
            UInt32Data s_offset;
            int index=0;
            custFontData = new sCustFontData(CustFontData);
            rangeData = new RangeData(RangeDataS);
            m_width = custFontData.nWidth;
            m_height = custFontData.nHeight;

            if (custFontData.nEquiDistant == 0)
            {
                if (Width == null || Width.Length <= 0)
                {
                    MessageBox.Show("Width can not be empty!");
                    return false;
                }
                width = new ByteData(Width);
                if (DWidth != null && DWidth.Length > 0)
                {
                    dWidth = new ByteData(DWidth);
                }
                else
                {
                    dWidth = width;
                }
                if (dWidth.m_data.Length != width.m_data.Length)
                {
                    MessageBox.Show("Number of different Width & Dwidth!");
                    return false;
                }
                s_offset = new UInt32Data(Offset);
                s_data = new ByteData(Data);

                m_header = "STARTFONT 2.1\r\n" +
                              "FONTBOUNDINGBOX " + custFontData.nWidth + " " + custFontData.nHeight + " " + "0" + " " + "0" + "\r\n" +
                              "STARTPROPERTIES " + "2" + "\r\n" +
                              "FONT_ASCENT " + custFontData.nAscent + "\r\n" +
                              "FONT_DESCENT " + custFontData.nDescent + "\r\n" +
                              "ENDPROPERTIES" + "\r\n";// +
                              //"CHARS " + m_rangeData.m_chars + "\r\n";

                for (int i = 0; i < rangeData.m_rangeData.Length; i++)
                {
                    for (UInt16 j = rangeData.m_rangeData[i].nMin; j < rangeData.m_rangeData[i].nMax + 1; j++)
                    {
                        FontBitmap fbmp = new FontBitmap(j, width.m_data[index],dWidth.m_data[index], custFontData.nHeight, index, s_data.m_data, s_offset.m_data[index]);
                        m_FontArray[fbmp.m_ch] = fbmp;
                        index++;
                    }
                }
                return true;
            }
            else
            {
                s_offset = new UInt32Data(Offset);
                s_data = new ByteData(Data);

                m_header = "STARTFONT 2.1\r\n" +
                              "FONTBOUNDINGBOX " + custFontData.nWidth + " " + custFontData.nHeight + " " + "0" + " " + "0" + "\r\n" +
                              "STARTPROPERTIES " + "2" + "\r\n" +
                              "FONT_ASCENT " + custFontData.nAscent + "\r\n" +
                              "FONT_DESCENT " + custFontData.nDescent + "\r\n" +
                              "ENDPROPERTIES" + "\r\n";// +
                //"CHARS " + m_rangeData.m_chars + "\r\n";

                for (int i = 0; i < rangeData.m_rangeData.Length; i++)
                {
                    for (UInt16 j = rangeData.m_rangeData[i].nMin; j < rangeData.m_rangeData[i].nMax + 1; j++)
                    {
                        FontBitmap fbmp = new FontBitmap(j, custFontData.nWidth, custFontData.nWidth, custFontData.nHeight, index, s_data.m_data, (uint)(s_offset.m_data[i] + (j - rangeData.m_rangeData[i].nMin) * ((custFontData.nWidth * custFontData.nHeight +7 )/8)));
                        m_FontArray[fbmp.m_ch] = fbmp;
                        index++;
                    }
                }
                return true;            
            }
            return false;
            //temp += "ENDFONT" + "\r\n";
        }
        public bool LoadData(string filename)
        {
            using (StreamReader fs = new StreamReader(filename, System.Text.Encoding.Default, false))
            {

                m_filename = filename;
                string content = fs.ReadToEnd();
                int offset = content.IndexOf("CHARS ");
                m_header = content.Substring(0, offset);
                string patten = @"((FONTBOUNDINGBOX)\s*(?<width>\d+)\s*(?<height>\d+)\s*)";
                Regex buf = new Regex(patten, RegexOptions.IgnoreCase);
                MatchCollection matches = buf.Matches(m_header);

                if (matches.Count == 1)
                {
                    m_width = byte.Parse(matches[0].Groups["width"].Value);
                    m_height = byte.Parse(matches[0].Groups["height"].Value);
                }
                offset = content.IndexOf("STARTCHAR");
                bool loop = (offset != -1)? true: false;
                while (loop)
                {
                    int offset_next = content.IndexOf("STARTCHAR", offset + 16);
                    if (offset_next == -1)
                    {
                        loop = false;
                        offset_next = content.IndexOf("ENDFONT", offset + 16);
                    }
                    FontBitmap fbmp = new FontBitmap(content.Substring(offset, offset_next - offset));
                    m_FontArray[fbmp.m_ch] = fbmp;
                    offset = offset_next;
                } 
                return true;
            }
            
        }
        public bool LoadData(sCustFontData custFontData)
        {
            RangeData rangeData = custFontData.pRangeDetails.pRangeData;
            byte[] WidthArray =custFontData.pWidthArray;
            byte[]  DWidthArray = custFontData.pDWidthArray;
            UInt32[] OffsetArray = custFontData.pOffsetArray;
            UInt16[] OffsetArrayC = custFontData.pOffsetArrayC;
            byte[] DataArray = custFontData.pDataArray;
            int index=0;
            m_width = custFontData.nWidth;
            m_height = custFontData.nHeight;
            
            if(DWidthArray ==  null)
                DWidthArray = WidthArray;
            if (custFontData.nEquiDistant == 0)
            {

                m_header = "STARTFONT 2.1\r\n" +
                              "FONTBOUNDINGBOX " + custFontData.nWidth + " " + custFontData.nHeight + " " + "0" + " " + "0" + "\r\n" +
                              "STARTPROPERTIES " + "2" + "\r\n" +
                              "FONT_ASCENT " + custFontData.nAscent + "\r\n" +
                              "FONT_DESCENT " + custFontData.nDescent + "\r\n" +
                              "ENDPROPERTIES" + "\r\n";// +
                              //"CHARS " + m_rangeData.m_chars + "\r\n";

                for (int i = 0; i < rangeData.m_rangeData.Length; i++)
                {
                    for (UInt16 j = rangeData.m_rangeData[i].nMin; j < rangeData.m_rangeData[i].nMax + 1; j++)
                    {
                        if(custFontData.nEquiDistant ==0)
                        {
                            FontBitmap fbmp = new FontBitmap(j, WidthArray[index], DWidthArray[index], custFontData.nHeight, index, DataArray, (UInt32)(OffsetArrayC[index]));
                            m_FontArray[fbmp.m_ch] = fbmp;
                        }
                        else
                        {
                            FontBitmap fbmp = new FontBitmap(j, custFontData.nWidth,custFontData.nWidth, custFontData.nHeight, index, DataArray,(UInt32) index *custFontData.nCharBytes);
                            m_FontArray[fbmp.m_ch] = fbmp;
                        }
                        index++;
                    }
                }
                return true;
            }
            return false;
            //temp += "ENDFONT" + "\r\n";
        }
        public bool Merge(BdfClass bdf)
        {
            if (m_height != bdf.m_height)
            {
                MessageBox.Show("Different heights!");
                return false;
            }
           
            for (int i = 0; i < UICODE_TOTAL; i++)
            {
                if(bdf.m_FontArray[i]!=null)
                    m_FontArray[i] = bdf.m_FontArray[i];
            }
            
            return true;
        }
        
        public bool Insert(FontBitmap bdf)
        {   
            m_FontArray[bdf.m_ch] = bdf;
            return true;
        }
    }
}
