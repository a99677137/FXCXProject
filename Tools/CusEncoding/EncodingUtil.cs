using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CusEncoding
{
    /// <summary>
    /// 内部有两个缓冲buffer
    /// </summary>
    public class EncodingUtil
    {

//         private static String ConvertLocalForString(byte[] byteArray, int index, int length)
//         {
//             return System.Text.Encoding.UTF8.GetString(ConvertLocalForByte(byteArray, index, length));
//         }
//         private static byte[] ConvertLocalForByte(byte[] byteArray)
//         {
//             return ConvertLocalForByte(byteArray, 0, byteArray.Length);
//         }
//         private static byte[] ConvertLocalForByte(byte[] byteArray, int index, int length)
//         {
//             System.Text.Encoding encoding = EncodingType.GetType(byteArray, index, length);
//             if( encoding == System.Text.Encoding.UTF8)
//             {
//                 return byteArray;
//             }
//             else if( encoding == System.Text.Encoding.Unicode)
//             {
//                 return System.Text.Encoding.Convert(System.Text.Encoding.Unicode,System.Text.Encoding.UTF8,byteArray,index,length);
//             }
//             else if( encoding == System.Text.Encoding.BigEndianUnicode)
//             {
//                 return System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.UTF8, System.Text.Encoding.Convert(System.Text.Encoding.BigEndianUnicode, System.Text.Encoding.Unicode, byteArray, index, length));
//             }
//             else //GB2312 null
//             {
//                 return System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.UTF8, CusEncoding.GB2312ToUnicode.GBToUnicodeForByte(byteArray, index, length));
//             }
//         }

        public static byte[] FileByteToLocal(byte[] byteData)
        {
            switch(FileEncodingType.GetType(byteData))
            {
                case EncodingType.Encoding_ASCII:
                case EncodingType.Encoding_UTF8:
                    return byteData;
                case EncodingType.Encoding_UTF8_BOM:
                    return byteData.Skip(3).ToArray();
                case EncodingType.Encoding_Unicode:
                    return Encoding.Convert(Encoding.Unicode, Encoding.UTF8, byteData);
                case EncodingType.Encoding_BigEndianUnicode:
                    return Encoding.Convert(Encoding.BigEndianUnicode, Encoding.UTF8, byteData);
                case EncodingType.Encoding_GBK:
                    return (Encoding.Convert(Encoding.Unicode, Encoding.UTF8, GBKTools.GBKToUn(byteData, 0, byteData.Length)));
                default:
                    return null;
            }
        }

        public static byte[] ByteToLocal(byte[] byteData,int index,int len)
        {
            switch (FileEncodingType.GetType(byteData, index, len))
            {
                case EncodingType.Encoding_ASCII:
                case EncodingType.Encoding_UTF8:
                    {
                        byte[] newData = new byte[len];
                        Array.Copy(byteData, index, newData, 0, len);
                        return newData;
                    }
                case EncodingType.Encoding_UTF8_BOM:
                    {
                        if (len <= 3)
                            return null;
                        byte[] newData = new byte[len - 3];
                        Array.Copy(byteData, index + 3, newData, 0, len - 3);
                        return newData;
                    }
                case EncodingType.Encoding_Unicode:
                    return Encoding.Convert(Encoding.Unicode, Encoding.UTF8, byteData, index, len);
                case EncodingType.Encoding_BigEndianUnicode:
                    return Encoding.Convert(Encoding.BigEndianUnicode, Encoding.UTF8, byteData, index, len);
                case EncodingType.Encoding_GBK:
                    return (Encoding.Convert(Encoding.Unicode, Encoding.UTF8, GBKTools.GBKToUn(byteData, index, len)));
                default:
                    return null;
            }
        }

        /// <summary>
        /// GBK->UTF8
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns>返回值为new Char[]</returns>
        public static char[] ByteConvertCharArrayByNet(byte[] byteArray, int index, int length)
        {
            char[] chars;
            int charCount;
            GBKTools.GbkConvertToUtf16(out chars ,out charCount, byteArray, index, length, false);
            return chars;
        }

        /// <summary>
        /// GBK->UTF8
        /// 返回值为strig，中间不产生GC，但最后返回string会有一次内存分配，如果外部调用可以用char[]而不用string的话，可以使用GbkConvertToChar
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GbkConvertToString(byte[] byteArray, int index, int length)
        {
            return GBKTools.GbkConvertToUtf16_String(byteArray, index, length);
        }

        public static void GbkConvertToChar(out char[] chars, out int charCount, byte[] byteArray, int index, int length)
        {
            GBKTools.GbkConvertToUtf16(out chars, out charCount, byteArray, index, length, true);
        }

        /// <summary>
        /// UFT8->GBK
        /// </summary>
        /// <param name="charArray"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] CharArrayConvertByteByNet(char[] charArray, int index, int length)
        {
            return GBKTools.Utf16ConvertToGbk(charArray, index, length);
        }

        public static int GetGBKLength(char[] charArray, int index, int length)
        {
            return GBKTools.UnicodeToGbkSize(charArray, index, length);
        }
    }
}
