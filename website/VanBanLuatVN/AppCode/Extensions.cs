using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VanBanLuatVN.Library.Sercurity;
using ICSoft.ViewLibV3;
using System.Web.SessionState;
namespace VanBanLuat.Librarys
{
    public static class Extensions
    {
        #region Account   

        private static VbLuatPrincipal CurrentUser
        {
            get { return (VbLuatPrincipal) HttpContext.Current.User; }
        }

        public static bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User != null &&
                       HttpContext.Current.User.Identity != null &&
                       HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public static int GetCurrentCustomerId()
        {
            return IsAuthenticated ? CurrentUser.CustomerId : 0;
        }

        public static string GetCurrentCustomerName()
        {
            return IsAuthenticated ? CurrentUser.CustomerName : string.Empty;
        }

        public static string GetCurrentCustomerFullName()
        {
            return IsAuthenticated
                ? CurrentUser.CustomerFullName.TrimmedOrDefault(CurrentUser.CustomerName)
                : string.Empty;
        }

        public static string GetCurrentCustomerMail()
        {
            return IsAuthenticated ? CurrentUser.CustomerMail : string.Empty;
        }

        public static string GetCurrentCustomerMobile()
        {
            return IsAuthenticated ? CurrentUser.CustomerMobile : string.Empty;
        }

        public static void Logout(HttpResponseBase response)
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty)
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            response.Cookies.Add(cookie);
            FormsAuthentication.RedirectToLoginPage();
        }

        #endregion

        #region HtmlHelpers

        public static string StripTags(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                char[] arr = new char[value.Length];
                int arrIndex = 0;
                bool check = false;
                for (int i = 0; i < value.Length; i++)
                {
                    char item = value[i];
                    if (item == '<')
                    {
                        check = true;
                        continue;
                    }

                    if (item == '>')
                    {
                        check = false;
                        continue;
                    }

                    if (!check)
                    {
                        arr[arrIndex] = item;
                        arrIndex++;
                    }
                }

                return new string(arr, 0, arrIndex);
            }
            else return value;
        }

        public static string Sanitize(this string stringValue)
        {
            if (null == stringValue)
                return null;
            return stringValue
                .RegexReplace("-{2,}", "-") // transforms multiple --- in - use to comment in sql scripts
                .RegexReplace(@"[*/]+", string.Empty) // removes / and * used also to comment in sql scripts
                .RegexReplace(
                    @"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s",
                    string.Empty, RegexOptions.IgnoreCase);
        }

        public static string SanitizeWithoutSplash(this string stringValue)
        {
            if (null == stringValue)
                return null;
            return stringValue
                .RegexReplace("-{2,}", "-") // transforms multiple --- in - use to comment in sql scripts
                .RegexReplace(@"[*]+", string.Empty) // removes / and * used also to comment in sql scripts
                .RegexReplace(
                    @"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s",
                    string.Empty, RegexOptions.IgnoreCase);
        }

        public static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith)
        {
            return Regex.Replace(stringValue, matchPattern, toReplaceWith);
        }

        public static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith,
            RegexOptions regexOptions)
        {
            return Regex.Replace(stringValue, matchPattern, toReplaceWith, regexOptions);
        }


        /// <summary>
        ///  Đường dẫn file đầy đủ
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="contentPath"></param>
        /// <param name="toAbsolute"></param>
        /// <returns></returns>
        public static string Content(this UrlHelper urlHelper, string contentPath, bool toAbsolute = false)
        {
            var path = urlHelper.Content(contentPath);
            var url = new Uri(HttpContext.Current.Request.Url, path);

            return toAbsolute ? url.AbsoluteUri : path;
        }

        #endregion

        #region Adv

        public static string AdvDataLoadFromFile(object AdvCateId)
        {
            string advContent = "";
            string fileName = string.Empty;
            string filePath = string.Empty;
            if (AdvCateId == null || string.IsNullOrEmpty(AdvCateId.ToString()))
            {
                sms.utils.Log.writeLog("AdvCateId is null:" + HttpContext.Current.Request.RawUrl,
                    ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                return advContent;
            }

            try
            {
                advContent = CacheHelpers.Get<string>("advs_site_1V2");
                fileName = "advs_site_1.js";
                filePath = AppDomain.CurrentDomain.BaseDirectory + "Data\\advs\\";
                filePath += fileName;
                if (String.IsNullOrEmpty(advContent))
                {
                    if (File.Exists(filePath))
                    {
                        StreamReader sr = new StreamReader(filePath);
                        advContent = "";
                        string m_LineData = m_LineData = sr.ReadLine();
                        while (!string.IsNullOrEmpty(m_LineData))
                        {
                            advContent =
                                advContent.Replace(
                                    "<script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script>",
                                    "");
                            advContent += m_LineData;
                            m_LineData = sr.ReadLine();
                        }

                        sr.Close();
                        sr.Dispose();
                        advContent = advContent.Replace("script", "scr' + 'ipt");
                        advContent = "<script type=\"text/javascript\">" + Environment.NewLine + advContent +
                                     Environment.NewLine + "</script>";
                        CacheHelpers.Insert("advs_site_1V2", advContent, null);
                    }
                    else
                    {
                        advContent = "";
                        sms.utils.Log.writeLog("file not exist:" + filePath,
                            ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                    }
                }

            }
            catch (Exception ex)
            {
                sms.utils.Log.writeLog(ex.ToString(),
                    ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }

            return advContent;
        }


        #endregion

        /// <summary>
        /// Trả về string với giá trị mặc định nếu không có giá trị
        /// </summary>
        /// <param name="str">value</param>
        /// <param name="strDefault">giá trị mặc định</param>
        /// <returns></returns>
        public static string TrimmedOrDefault(this string str, string strDefault)
        {
            return string.IsNullOrEmpty(str) ? strDefault : str.Trim();
        }

        /// <summary>
        /// Kiểm tra list có dữ liệu ko
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool HasValue<T>(this List<T> items)
        {
            if (items != null)
            {
                if (items.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Kiểm tra IEnumerable có dữ liệu không
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        public static string Truncate(this string value, int mLengthRemain)
        {
            string text = value;
            if (value.Length > mLengthRemain)
            {
                text = value.Substring(0, mLengthRemain);
                string temp = value.Substring(mLengthRemain, 1);
                if (temp != " ")
                {
                    text = text.Substring(0, text.LastIndexOf(" ", StringComparison.Ordinal));
                }

                text += " ...";
            }

            value = text;
            return value;
        }

        public static string TruncateValue(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return (value.Length <= maxLength ? value : value.Substring(0, maxLength) + " ...");
        }

        public static string BuildQueryStringUrl(this string url, NameValueCollection parameters)
        {
            string urlWithoutQuery = url.IndexOf('?') >= 0
                ? url.Substring(0, url.IndexOf('?'))
                : url;

            string queryString = url.IndexOf('?') >= 0
                ? url.Substring(url.IndexOf('?'))
                : null;

            var queryParamList = queryString != null
                ? HttpUtility.ParseQueryString(queryString)
                : HttpUtility.ParseQueryString(string.Empty);

            foreach (string key in parameters.AllKeys)
            {
                string value = parameters[key] ?? string.Empty;
                if (queryParamList[key] != null)
                {
                    if (value == "")
                        queryParamList.Remove(key);
                    else queryParamList[key] = value;
                }
                else
                {
                    if (value == "")
                        queryParamList.Remove(key);
                    else queryParamList.Add(key, value);
                }
            }
            return string.Format("{0}{1}{2}", urlWithoutQuery, queryParamList.Count > 0 ? "?" : string.Empty, queryParamList);
        }

        /// <summary>
        /// DateTime to string format
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="textEmpty"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string toString(this DateTime dt, string textEmpty = "", string format = "dd/MM/yyyy")
        {
            return dt == DateTime.MinValue ? textEmpty : dt.ToString(format);
        }
        // link tag
        public static string TagsNameGetUrl(this string value, int tagId = 0)
        {
            if (!string.IsNullOrEmpty(value) && tagId > 0)
            {
                value = StringUtil.RemoveSignature(value);
                value = value.Trim().Replace(" ", "-");
                value = value.ToLower();
                value = string.Concat(value, "-", tagId, "-tag", ".html");
                if (!value.StartsWith("http://")) value = string.Concat(ICSoft.ViewLibV3.Constants.ROOT_PATH, value);
                return value;
            }
            return string.Empty;
        }
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(m => m.Key,
                        m => m.Value.Errors
                            .Select(e => e.ErrorMessage).ToArray())
                    .Where(m => m.Value.Any());
            }
            return null;
        }

        #region Document
        /// <summary>
        /// Danh sách văn bản liên quan theo RelateTypeId và DisplayPosition
        /// </summary>
        /// <param name="list">ListDocRelates</param>
        /// <param name="relateTypeId">relateTypeId</param>
        /// <param name="displayPosition">displayPosition</param>
        /// <returns></returns>
        public static List<DocRelates> GetDocsRelatesByRelateTypeId_DisplayPosition(this List<DocRelates> list, byte relateTypeId, string displayPosition)
        {
            var retVal = new List<DocRelates>();
            if (list.HasValue())
            {
                retVal = !string.IsNullOrEmpty(displayPosition) ? list.FindAll(m => m.RelateTypeId == relateTypeId && m.DisplayPosition.Equals(displayPosition)) : list.FindAll(m => m.RelateTypeId == relateTypeId);
            }
            return retVal;
        }
        #endregion

    }
}