using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for StringUtil
/// </summary>
namespace ICSoft.ViewLibV3
{
    public class StringUtil
    {
        public StringUtil()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //----------------------------------------------------------------------------------------------
        public static string ConnectionStringIpReplace(string connStr, string ipReplace)
        {
            string retVal = "";
            try
            {
                string ip = StringUtil.ExactValue(connStr, "server", ';');
                if (ip == ipReplace)
                {
                    retVal = connStr;
                }
                else
                {
                    retVal = connStr.Replace(ip, ipReplace);                    
                }
            }
            catch
            {
                retVal = "";
            }
            return retVal;
        }
        //----------------------------------------------------------------------------------------------
        public static string GetQuote(string odString)
        {
            string[] codes = odString.Split(',');
            string result = "";
            for (int i = 0; i < codes.Length; i++)
            { 
                result=result+"'"+codes[i]+"',";
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }
        //----------------------------------------------------------------------------------------------
        public static string ExactValue(string str, string objName, char delimiter)
        {
            string[] codes = str.Split(delimiter);
            string result = "";
            int i = 0;
            while (i < codes.Length)
            {
                if (codes[i].Contains(objName))
                {
                    result = codes[i].Replace(objName,"");
                    result = result.Replace("=", "").Trim();
                    i = codes.Length;
                }
                i++;
            }
            return result;
        }
        //----------------------------------------------------------------------------------------------
        public static bool isNumeric(string s)
		{
			try
			{
				Convert.ToInt32(s);
				return true;
			}
			catch 
            {
				return false;
			}
		}
        //----------------------------------------------------------------------------------------------
        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        //----------------------------------------------------------------------------------------------
        public static string RemoveSign4VietnameseString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        
        }
        //----------------------------------------------------------------------------------------------
        public static string RemoveSignatureForURL(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            input = RemoveSignature(input);
            input = input.Replace("?", "").Replace("&", "").Replace("\"", "").Replace("'", "");
            input = input.Replace(".", "").Replace(",", "").Replace(";", "");
            input = input.Replace("@", "").Replace("$", "").Replace("%", "").Replace("*", "");
            input = input.Replace("~", "").Replace("\\", "").Replace("/", "").Replace("!", "");
            input = input.Replace(":", "").Replace(")", "").Replace("(", "").Replace("+", "");
            input = input.Replace("`", "").Replace("|", "");
            while (input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }
            input = input.Replace(" ", "-");
            return input;
        }
        public static string RemoveSignature(string input)
        {
            if (input == null)
            {
                return null;
            }
            input = input.Replace("-", "");
            Regex rga = new Regex("[àÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬ]");
            Regex rgd = new Regex("[đĐ]");
            Regex rge = new Regex("[èÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆ]");
            Regex rgi = new Regex("[ìÌỉỈĩĨíÍịỊ]");
            Regex rgo = new Regex("[òÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢ]");
            Regex rgu = new Regex("[ùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰ]");
            Regex rgy = new Regex("[ỳỲỷỶỹỸýÝỵỴ]");
            input = rga.Replace(input, "a");
            input = rgd.Replace(input, "d");
            input = rge.Replace(input, "e");
            input = rgi.Replace(input, "i");
            input = rgo.Replace(input, "o");
            input = rgu.Replace(input, "u");
            input = rgy.Replace(input, "y");
            return input;
        }
        public static string GetLead(string inputString, int length)
        {
            string retVal = "";
            try
            {
                if (string.IsNullOrEmpty(inputString))
                {
                    return retVal;
                }
                if (inputString.Length <= length)
                {
                    retVal = inputString;
                }
                else
                {
                    retVal = inputString.Substring(0, length);
                    if (retVal.Contains(" "))
                    {
                        retVal = retVal.Substring(0, retVal.LastIndexOf(" "));
                    }
                    retVal += "...";
                }
            }
            catch
            {
                retVal = inputString;
            }
            return retVal;
        }
        public static string injectionString(string str)
        {
            try
            {
                string tmp;
                tmp = killChar(str).Replace("'", "''");
                return str;
            }
            catch
            {
                return "";
            }
        }
        //-----------------------------------------------------------------------
        private static string killChar(string strInput)
        {
            try
            {
                string newChars;
                string[] badChars = new string[] { "select", "drop", ";", "--", "insert", "delete", "xp_" };
                newChars = strInput.Trim();
                for (int i = 0; i < badChars.Length; i++)
                {
                    newChars = newChars.Replace(badChars[i], "");
                }
                return newChars;
            }
            catch
            {
                return "";
            }
        }
        //-----------------------------------------------------------------------
        public static bool CheckPasswordStrength(string password)
        {
            int score = 1;
            if (password.Length >= 8)
                score++;
            if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
                Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.IsMatch(password, @"[0-9]+(\.[0-9][0-9]?)?", RegexOptions.ECMAScript) == true)
                score++;
            if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                score++;

            return (score >= 3 ? true : false);
        }
    }
}