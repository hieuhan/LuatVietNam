using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ExtractWordLib
{
    public class Converter
    {
        private static char[] tcvnchars = {
        'µ', '¸', '¶', '·', '¹', 
        '¨', '»', '¾', '¼', '½', 'Æ', 
        '©', 'Ç', 'Ê', 'È', 'É', 'Ë', 
        '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ', 
        'ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö', 
        '×', 'Ý', 'Ø', 'Ü', 'Þ', 
        'ß', 'ã', 'á', 'â', 'ä', 
        '«', 'å', 'è', 'æ', 'ç', 'é', 
        '¬', 'ê', 'í', 'ë', 'ì', 'î', 
        'ï', 'ó', 'ñ', 'ò', 'ô', 
        '­', 'õ', 'ø', 'ö', '÷', 'ù', 
        'ú', 'ý', 'û', 'ü', 'þ', 
        '¡', '¢', '§', '£', '¤', '¥', '¦'
    };

        private static char[] unichars = {
        'à', 'á', 'ả', 'ã', 'ạ', 
        'ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ', 
        'â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ', 
        'đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ', 
        'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ', 
        'ì', 'í', 'ỉ', 'ĩ', 'ị', 
        'ò', 'ó', 'ỏ', 'õ', 'ọ', 
        'ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ', 
        'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ', 
        'ù', 'ú', 'ủ', 'ũ', 'ụ', 
        'ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự', 
        'ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ', 
        'Ă', 'Â', 'Đ', 'Ê', 'Ô', 'Ơ', 'Ư'
    };
        private static string[] TCVN3Font = { "vntime", "vntimeh" };
        private static string[] TCVN3FontUpper = { "vntimeh" };
        private static char[] convertTable;

        static Converter()
        {
            convertTable = new char[256];
            for (int i = 0; i < 256; i++)
                convertTable[i] = (char)i;
            for (int i = 0; i < tcvnchars.Length; i++)
                convertTable[tcvnchars[i]] = unichars[i];
        }
        public static bool IsTCVN3(string font)
        {
            bool RetVal = false;
            if (String.IsNullOrEmpty(font))
                return false;
            foreach (string m_font in TCVN3Font)
            {
                if (font.ToLower().Contains(m_font))
                {
                    RetVal = true;
                    break;
                }
            }
            return RetVal;
        }
        public static bool IsTCVN3Upper(string font)
        {
            bool RetVal = false;
            foreach (string m_font in TCVN3FontUpper)
            {
                if (font.ToLower().Contains(m_font))
                {
                    RetVal = true;
                    break;
                }
            }
            return RetVal;
        }
        public static string getConvertTable()
        {
            string RetVal = "";
            RetVal = convertTable.ToString();
            return RetVal;
        }
        public static string TCVN3ToUnicode(string value)
        {
            string RetVal = value;            
            char[] chars = value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] < (char)256)
                {
                    chars[i] = convertTable[chars[i]];
                }
            }
            RetVal = new string(chars);
            RetVal = ValidateUpper(RetVal);
            return RetVal;
        }
        public static string TCVN3ToUnicode(string value, string font)
        {
            string RetVal = value;
            if (IsTCVN3(font) == false)
            {
                if (String.IsNullOrEmpty(font.Trim()) == false)
                    return RetVal;
                else if (IsTCVN3ByInput(value) == false)
                    return RetVal;
            }
            
            char[] chars = value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] < (char)256)
                {
                    chars[i] = convertTable[chars[i]];
                }
            }
            RetVal = new string(chars);
            if (IsTCVN3Upper(font))
                RetVal = RetVal.ToUpper();
            
            return RetVal;
        }
        // process for abc
        public static string ReplaceOther(string value)
        {
            string[] arrReplaceSource, arrReplaceDistin;
            arrReplaceSource = "«$1".Split('$');
            arrReplaceDistin = "ô$1".Split('$');
            for (int index = 0; index < arrReplaceSource.Length; index++)
            {
                value = value.Replace(arrReplaceSource[index], arrReplaceDistin[index]);
            }
            return value;
        }
        //=================
        public static string ValidateUpper(string value)
        {
            // upper case
            string[] arrword;
            arrword = value.Split(' ');
            value = "";
            int count;
            for ( int index = 0; index < arrword.Length; index++)
            {
                count = 0;
                int curentLength = arrword[index].Length;
                for (int i = 0; i < curentLength; i++)
                {
                    if (char.IsUpper(arrword[index][i])) count++;
                }
                if (count >= 2 || (count >= 1 && arrword[index].Length > 1 && (char.IsUpper(arrword[index][1]) || char.IsUpper(arrword[index][arrword[index].Length - 1]))))
                    arrword[index] = arrword[index].ToUpper();
                value += arrword[index] + " ";
            }
            return value;
        }
        //=================
        public static bool IsTCVN3ByInput(string s)
        {
            s = s.Replace(" ", "");
            if (s.Length < 1)
                return false;
            if (s.Contains("§iÒu") || s.Contains("®iÒu"))
                return true;
            string _TCVN3 = "§®¸µ¶·¹¨¾»¼½Æ©ÇËÏÑªÖ×ØÜÞßä«åæç¬ëîïñøö÷ûüþ¸µ¶·¹¡¾»¢";// "ïø÷«åç¬©ÇË¨¾»½×ÞÜÖªßö®¸ñÞ";//"Ëµ®Þë÷Ö";//
            string tempchar = "";
            int counter = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (i >= 200)
                    break;
                tempchar = s.Substring(i, 1);
                if (tempchar == " " || tempchar == "t" || tempchar == "s" || tempchar == "T" || tempchar == "S")
                    continue;
                if (tempchar == "a" || tempchar == "A")
                    continue;
                if (_TCVN3.IndexOf(tempchar) >= 0)
                {
                    counter++;
                    //LogControl.WriteMessage(txtStatus, "Counter: " + counter.ToString() + " - " + s.Substring(i, 1));
                }
            }
            //LogControl.WriteMessage(txtStatus, "Counter: " + counter.ToString() + " - " + s.Length.ToString());
            float percent = counter / s.Length;
            if (percent > 0.1 || counter > 1)
                return true;
            else
                return false;
        }
    }

}
