using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
namespace WYL
{
    
    class ByteData
    {
        public byte[] m_data;
        byte ParseValue(string text)
        {
            if (text[1] == 'x' || text[1] == 'X')
                return Convert.ToByte(text, 16);// byte.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                return byte.Parse(text);
        }
        public ByteData(string text)
        {
            char[] stringSeparators = new char[] { ' ', '\n', '\t', '\r', ',', '{', '}' };
            string[] items = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            m_data = new byte[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                m_data[i] = ParseValue(items[i]);
            }
        }
    }
    class UInt16Data
    {
        public UInt16[] m_data;
        UInt16 ParseValue(string text)
        {
            if (text[1] == 'x' || text[1] == 'X')
                return Convert.ToUInt16(text, 16);//UInt16.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                return UInt16.Parse(text);
        }
        public UInt16Data(string text)
        {
            char[] stringSeparators = new char[] { ' ', '\n', '\t', '\r', ',', '{', '}' };
            string[] items = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            m_data = new UInt16[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                m_data[i] = ParseValue(items[i]);
            }
        }
    }
    class UInt32Data
    {
        public UInt32[] m_data;
        UInt32 ParseValue(string text)
        {
            if (text[1] == 'x' || text[1] == 'X')
                return Convert.ToUInt32(text, 16);//UInt16.Parse(text, System.Globalization.NumberStyles.HexNumber);
            else
                return UInt32.Parse(text);
        }
        public UInt32Data(string text)
        {
            char[] stringSeparators = new char[] { ' ', '\n', '\t', '\r', ',', '{', '}' };
            string[] items = text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            m_data = new UInt32[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                m_data[i] = ParseValue(items[i]);
            }
        }
    }
    public class FontBitmap
    {
        public UInt16 m_ch;
        public string m_text;
        public byte m_width;
        public byte m_height;
        public byte m_dWidth;
        public FontBitmap(UInt16 ch, byte width,byte dWidth, byte height, int index, byte[] data, UInt32 font_offset)
        { 
            m_ch = ch;
            m_text = "STARTCHAR " + ch + "\r\n" +
                             "ENCODING " + ch + "\r\n" +
                             "SWIDTH 520 0" + "\r\n" +
                             "DWIDTH " + dWidth + " 0" + "\r\n" +
                             "BBX " + width + " " + height + " " + "0" + " " + "0" + "\r\n"+
                             "BITMAP\r\n";

            UInt64 colData = 0;
            UInt32 offset = 0;
            m_width = width;
            m_height = height;
            for (int i = 0; i < height; i++)
            {
                colData = 0;
                for (int j = 0; j < width; j++)
                {
                    colData = (colData << 1) | (uint)(((data[font_offset + offset / 8] & ((uint)1 << (int)(offset % 8))) != 0) ? 1 : 0);//((colData << ((UInt32)(offset % 8));
                    offset++;
                }
                if ((width % 8) != 0)
                {
                    for (int j = (int)(width % 8); j < 8; j++)
                    {
                        colData = (colData << 1);
                    }
                }
                m_text += colData.ToString("x16").Substring(16 - ((width + 7) / 8) * 2, ((width + 7) / 8) * 2) + "\r\n";
            }
            m_text += "ENDCHAR\r\n";
        }
		 public FontBitmap(UInt16 ch, byte width,byte dWidth, byte height, int index, byte[] data, UInt16 font_offset)
        { 
            m_ch = ch;
            m_text = "STARTCHAR " + ch + "\r\n" +
                             "ENCODING " + ch + "\r\n" +
                             "SWIDTH 520 0" + "\r\n" +
                             "DWIDTH " + dWidth + " 0" + "\r\n" +
                             "BBX " + width + " " + height + " " + "0" + " " + "0" + "\r\n"+
                             "BITMAP\r\n";

            UInt64 colData = 0;
            UInt32 offset = 0;
            m_width = width;
            m_height = height;
            for (int i = 0; i < height; i++)
            {
                colData = 0;
                for (int j = 0; j < width; j++)
                {
                    colData = (colData << 1) | (uint)(((data[font_offset + offset / 8] & ((uint)1 << (int)(offset % 8))) != 0) ? 1 : 0);//((colData << ((UInt32)(offset % 8));
                    offset++;
                }
                if ((width % 8) != 0)
                {
                    for (int j = (int)(width % 8); j < 8; j++)
                    {
                        colData = (colData << 1);
                    }
                }
                m_text += colData.ToString("x16").Substring(16 - ((width + 7) / 8) * 2, ((width + 7) / 8) * 2) + "\r\n";
            }
            m_text += "ENDCHAR\r\n";
        }
        public FontBitmap(string text)
        {
            int en =0;
            string patten = @"((ENCODING)\s*(?<en>(-1|\d+))\s*)";
            Regex buf = new Regex(patten, RegexOptions.IgnoreCase);
            MatchCollection matches = buf.Matches(text);

            if (matches.Count == 1)
            {
                en = int.Parse(matches[0].Groups["en"].Value);
            }
            if(en == -1)
            {
                patten = @"((STARTCHAR)\s*(?<ch>(\S+))\s*)";
                buf = new Regex(patten, RegexOptions.IgnoreCase);
                matches = buf.Matches(text);

                if (matches.Count == 1)
                {
                    int dot_offset =matches[0].Groups["ch"].Value.IndexOf('.');
                    if (dot_offset != -1)
                    {
                        m_ch = Convert.ToUInt16(matches[0].Groups["ch"].Value.Substring(dot_offset+1,matches[0].Groups["ch"].Value.IndexOf('.',dot_offset+1) - dot_offset-1),16);
                    }
                    else
                    {
                        m_ch = Convert.ToUInt16(matches[0].Groups["ch"].Value, 16);
                    }
                }
            }
            else
            {
                m_ch = (UInt16)en;
            }
            patten = @"((BBX)\s*(?<width>\d+)\s*(?<height>\d+)\s*)";
            buf = new Regex(patten, RegexOptions.IgnoreCase);
            matches = buf.Matches(text);

            if (matches.Count == 1)
            {
                m_width = byte.Parse(matches[0].Groups["width"].Value);
                m_height = byte.Parse(matches[0].Groups["height"].Value);
            }

            m_text = text;
        }
        public FontBitmap(UInt16 ch, Bitmap bmp)
        { 
            m_ch = ch;
            m_text = "STARTCHAR " + ch + "\r\n" +
                             "ENCODING " + ch + "\r\n" +
                             "SWIDTH 520 0" + "\r\n" +
                             "DWIDTH " + bmp.Width + " 0" + "\r\n" +
                             "BBX " + bmp.Width + " " + bmp.Height + " " + "0" + " " + "0" + "\r\n" +
                             "BITMAP\r\n";

            m_width = (byte)bmp.Width;
            m_height = (byte)bmp.Height;

            UInt64 colData = 0;
            Color pixel;
            for (int i = 0; i < bmp.Height; i++)
            {
                colData = 0;
                for (int j = 0; j < bmp.Width; j++)
                {
                    pixel = bmp.GetPixel(j, i);
                    if (pixel.R == 0)
                    {
                        colData |= (UInt64)1 << ((((bmp.Width + 7) >> 3) << 3) - j - 1);
                    }
                }
                
                m_text += colData.ToString("x16").Substring(16 - ((bmp.Width + 7) / 8) * 2, ((bmp.Width + 7) / 8) * 2) + "\r\n";
            }
            m_text += "ENDCHAR\r\n";
        }
        public Bitmap ToBitmap()
        {
            if (m_width == 0 || m_height == 0)
                return null;
            UInt64 colData = 0;
            Bitmap bmp = new Bitmap((int)m_width, (int)m_height);
            string[] stringSeparators = new string[] { "BITMAP","ENDCHAR" };
            string[] items = m_text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            char[] charSeparators = new char[] { '\r', '\n' };
            string[] bitmaps = items[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            //m_data = new UInt16[items.Length];
            for (int i = 0; i < bitmaps.Length; i++)
            {
                colData = Convert.ToUInt64(bitmaps[i], 16);
                for (int j = 0; j < bmp.Width; j++)
                {
                    bmp.SetPixel(j, i, ((colData & (UInt64)1 << ((((bmp.Width + 7) >> 3) << 3) - j - 1))!=0 )? Color.Black:Color.White);
                }
            }

            return bmp;
        }
    };
}
