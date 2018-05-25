using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CusEncoding
{
    public enum EncodingType : byte
    {
        Encoding_INVALID                = 0,
        Encoding_ASCII                  = 1,
        Encoding_UTF8_BOM               = 2,
        Encoding_UTF8                   = 3,
        Encoding_Unicode                = 4,
        Encoding_BigEndianUnicode       = 5,
        //Encoding_GB2312                 = 6,
        Encoding_GBK                    = 6,
        Encoding_Unrecognized           = Byte.MaxValue
    }
    public class FileEncodingType
    {

        private static byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
        private static byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
        private static byte[] UTF8BOM = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM 
        public static EncodingType GetType(byte[] byteData)
        {
            if (byteData == null || byteData.Length < 3)
                return EncodingType.Encoding_Unrecognized;
            else if (byteData[0] == UTF8BOM[0]/*0xEF*/ && byteData[1] == UTF8BOM[1]/*0xBB*/ && byteData[2] == UTF8BOM[2]/*0xBF*/)
            {
                return EncodingType.Encoding_UTF8_BOM;
            }
            else if (byteData[0] == UnicodeBIG[0]/*0xFE*/ && byteData[1] == UnicodeBIG[1]/*0xFF*/ && byteData[2] == UnicodeBIG[2]/*0x00*/)
            {
                 return EncodingType.Encoding_BigEndianUnicode;
            }
            else if (byteData[0] == Unicode[0]/*0xFF*/ && byteData[1] == Unicode[1]/*0xFE*/ && byteData[2] == Unicode[2]/*0x41*/)
            {
                return EncodingType.Encoding_Unicode;
            }
            else if (utf8_probability(byteData)!=0)
            {
                return EncodingType.Encoding_UTF8;
            }
            else if (IsASCII(byteData))
            {
                return EncodingType.Encoding_ASCII;
            }
            else
            {
                return EncodingType.Encoding_GBK;
            }
            //return EncodingType.Encoding_INVALID;
        }
        public static EncodingType GetType(byte[] byteData,int index,int len)
        {
            if (byteData == null || len < 3)
                return EncodingType.Encoding_Unrecognized;
            else if (byteData[index] == UTF8BOM[0]/*0xEF*/ && byteData[index+1] == UTF8BOM[1]/*0xBB*/ && byteData[index+2] == UTF8BOM[2]/*0xBF*/)
            {
                return EncodingType.Encoding_UTF8_BOM;
            }
            else if (byteData[index] == UnicodeBIG[0]/*0xFE*/ && byteData[index+1] == UnicodeBIG[1]/*0xFF*/ && byteData[index+2] == UnicodeBIG[2]/*0x00*/)
            {
                return EncodingType.Encoding_BigEndianUnicode;
            }
            else if (byteData[index] == Unicode[0]/*0xFF*/ && byteData[index+1] == Unicode[1]/*0xFE*/ && byteData[index+2] == Unicode[2]/*0x41*/)
            {
                return EncodingType.Encoding_Unicode;
            }
            else if (utf8_probability(byteData,index,len) != 0)
            {
                return EncodingType.Encoding_UTF8;
            }
            else if (IsASCII(byteData, index, len))
            {
                return EncodingType.Encoding_ASCII;
            }
            else
            {
                return EncodingType.Encoding_GBK;
            }
            //return EncodingType.Encoding_INVALID;
        }

        private static bool IsASCII(byte[] rawtext)
        {
            int i, rawtextlen = 0;
            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen; i++)
            {
                if ((rawtext[i] & (byte)0x7F) == rawtext[i])
                    continue;
                else
                    return false;
            }
            return true;
        }
        private static bool IsASCII(byte[] rawtext,int index,int len)
        {
            int i, rawtextlen = 0;
            rawtextlen = len;
            int w;
            for (i = 0; i < rawtextlen; i++)
            {
                w = index + i;
                if ((rawtext[w] & (byte)0x7F) == rawtext[w])
                    continue;
                else
                    return false;
            }
            return true;
        }
        private static int utf8_probability(byte[] rawtext,int index,int len) // No Bom
        {
            int score = 0;
            int i, rawtextlen = 0;
            int goodbytes = 0, asciibytes = 0;

            // Maybe also use UTF8 Byte Order Mark:  EF BB BF

            // Check to see if characters fit into acceptable ranges
            rawtextlen = len;
            int w;
            for (i = 0; i < rawtextlen; i++)
            {
                w = index + i;
                if ((rawtext[w] & (byte)0x7F) == rawtext[w])
                {  // One byte
                    asciibytes++;
                    // Ignore ASCII, can throw off count
                }
                else
                {
                    try
                    {
                        int m_rawInt0 = Convert.ToInt16(rawtext[w]);
                        int m_rawInt1 = Convert.ToInt16(rawtext[w + 1]);

                        if (256 - 64 <= m_rawInt0 && m_rawInt0 <= 256 - 33 && // Two bytes
                         i + 1 < rawtextlen &&
                         256 - 128 <= m_rawInt1 && m_rawInt1 <= 256 - 65)
                        {
                            goodbytes += 2;
                            i++;
                        }
                        else
                        {
                            int m_rawInt2 = Convert.ToInt16(rawtext[w + 2]);
                            if (256 - 32 <= m_rawInt0 && m_rawInt0 <= 256 - 17 && // Three bytes
                                 i + 2 < rawtextlen &&
                                 256 - 128 <= m_rawInt1 && m_rawInt1 <= 256 - 65 &&
                                 256 - 128 <= m_rawInt2 && m_rawInt2 <= 256 - 65)
                            {
                                goodbytes += 3;
                                i += 2;
                            }
                        }
                    }
                    catch (System.Exception)
                    {
                        return 0;
                    }
                }
            }

            if (asciibytes == rawtextlen) { return 0; }

            score = (int)(100 * ((float)goodbytes / (float)(rawtextlen - asciibytes)));

            // If not above 98, reduce to zero to prevent coincidental matches
            // Allows for some (few) bad formed sequences
            if (score > 98)
            {
                return score;
            }
            else if (score > 95 && goodbytes > 30)
            {
                return score;
            }
            else
            {
                return 0;
            }
        }

        private static int utf8_probability(byte[] rawtext) // No Bom
        {
            int score = 0;
            int i, rawtextlen = 0;
            int goodbytes = 0, asciibytes = 0;

            // Maybe also use UTF8 Byte Order Mark:  EF BB BF

            // Check to see if characters fit into acceptable ranges
            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen; i++)
            {
                if ((rawtext[i] & (byte)0x7F) == rawtext[i])
                {  // One byte
                    asciibytes++;
                    // Ignore ASCII, can throw off count
                }
                else
                {
                    try
                    {
                        int m_rawInt0 = Convert.ToInt16(rawtext[i]);
                        int m_rawInt1 = Convert.ToInt16(rawtext[i + 1]);

                        if (256 - 64 <= m_rawInt0 && m_rawInt0 <= 256 - 33 && // Two bytes
                         i + 1 < rawtextlen &&
                         256 - 128 <= m_rawInt1 && m_rawInt1 <= 256 - 65)
                        {
                            goodbytes += 2;
                            i++;
                        }
                        else
                        {
                            int m_rawInt2 = Convert.ToInt16(rawtext[i + 2]);
                            if (256 - 32 <= m_rawInt0 && m_rawInt0 <= 256 - 17 && // Three bytes
                                 i + 2 < rawtextlen &&
                                 256 - 128 <= m_rawInt1 && m_rawInt1 <= 256 - 65 &&
                                 256 - 128 <= m_rawInt2 && m_rawInt2 <= 256 - 65)
                            {
                                goodbytes += 3;
                                i += 2;
                            }
                        }
                    }
                    catch(System.Exception)
                    {
                        return 0;
                    }
                }
            }

            if (asciibytes == rawtextlen) { return 0; }

            score = (int)(100 * ((float)goodbytes / (float)(rawtextlen - asciibytes)));

            // If not above 98, reduce to zero to prevent coincidental matches
            // Allows for some (few) bad formed sequences
            if (score > 98)
            {
                return score;
            }
            else if (score > 95 && goodbytes > 30)
            {
                return score;
            }
            else
            {
                return 0;
            }
        }
        //private static System.Text.Encoding GetType(byte[] bt, int index, int length)
        //{
        //    System.Text.Encoding reVal = null;
        //    if (IsUTF8Bytes(bt, index, length) || (bt[index] == 0xEF && bt[index + 1] == 0xBB && bt[index + 2] == 0xBF))
        //    {
        //        reVal = System.Text.Encoding.UTF8;
        //    }
        //    else if (bt[index] == 0xFE && bt[index + 1] == 0xFF && bt[index + 2] == 0x00)
        //    {
        //        reVal = System.Text.Encoding.BigEndianUnicode;
        //    }
        //    else if (bt[index] == 0xFF && bt[index + 1] == 0xFE && bt[index + 2] == 0x41)
        //    {
        //        reVal = System.Text.Encoding.Unicode;
        //    }
        //    return reVal;
        //}
        //private static System.Text.Encoding GetType(byte[] bt)
        //{
        //    return GetType(bt, 0, bt.Length);
        //}
        //private static bool IsUTF8Bytes(byte[] data, int index, int len)
        //{
        //    int charByteCounter = 1;//计算当前正分析的字符应还有的字节数
        //    byte curByte; //当前分析的字节.
        //    for (int i = index; i < len + index; i++)
        //    {
        //        curByte = data[i];
        //        if (charByteCounter == 1)
        //        {
        //            if (curByte >= 0x80)
        //            {
        //                //判断当前
        //                while (((curByte <<= 1) & 0x80) != 0)
        //                {
        //                    charByteCounter++;
        //                }
        //                //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　
        //                if (charByteCounter == 1 || charByteCounter > 6)
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //若是UTF-8 此时第一位必须为1
        //            if ((curByte & 0xC0) != 0x80)
        //            {
        //                return false;
        //            }
        //            charByteCounter--;
        //        }
        //    }
        //    if (charByteCounter > 1)
        //    {
        //        throw new Exception("非预期的byte格式");
        //    }
        //    return true;
        //}

    }
}
