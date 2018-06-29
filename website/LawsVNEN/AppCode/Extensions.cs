using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.SessionState;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using LawsVNEN.Library;
using LawsVN.Library.Sercurity;
using LawsVNEN.App_GlobalResources;
using sms.utils;
using LawsVNEN.Models;
using LawsVNEN.Models.Account;
using Newtonsoft.Json;

namespace LawsVNEN.AppCode
{
    /// <summary>
    /// Class chứa các phương thức mở rộng
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Xác thực tài khoản người dùng
        /// </summary>
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
            return IsAuthenticated ? ((LawsVnPrincipal) HttpContext.Current.User).CustomerId : 0;
        }

        public static string GetCurrentCustomerName()
        {
            return IsAuthenticated ? ((LawsVnPrincipal)HttpContext.Current.User).CustomerName : string.Empty;
        }

        public static string GetCurrentCustomerFullName()
        {
            var currentUser = (LawsVnPrincipal) HttpContext.Current.User;
            return IsAuthenticated ? currentUser.CustomerFullName.TrimmedOrDefault(currentUser.CustomerName) : string.Empty;
        }

        public static string GetCurrentCustomerMail()
        {
            var currentUser = (LawsVnPrincipal)HttpContext.Current.User;
            return IsAuthenticated ? currentUser.CustomerMail : string.Empty;
        }

        public static string GetCurrentCustomerMobile()
        {
            var currentUser = (LawsVnPrincipal)HttpContext.Current.User;
            return IsAuthenticated ? currentUser.CustomerMobile : string.Empty;
        }

        public static byte GetSiteId()
        {
            byte siteId = Constants.SiteId;
            if(LawsVnLanguages.GetCurrentLanguageId() == 1)
            {
                siteId = Constants.SiteId_AnhViet;
            }
            return siteId;
        }
        public static short GetMenuIdTopId(byte languageId = 2)
        {
            short menuId = Constants.MenuIdTop;
            if (languageId == 1)
            {
                menuId = Constants.MenuIdTop_AnhViet;
            }
            return menuId;
        }
        public static short GetMenuIdBottomId(byte languageId = 2)
        {
            short menuId = Constants.MenuIdBottom;
            if (languageId == 1)
            {
                menuId = Constants.MenuIdBottom_AnhViet;
            }
            return menuId;
        }
        public static short GetMenuIdLeftTopId(byte languageId = 2)
        {
            short menuId = Constants.MenuIdLeftTop;
            if (languageId == 1)
            {
                menuId = Constants.MenuIdLeftTop_AnhViet;
            }
            return menuId;
        }
        public static short GetMenuIdLeftBottomId(byte languageId = 2)
        {
            short menuId = Constants.MenuIdLeftBottom;
            if (languageId == 1)
            {
                menuId = Constants.MenuIdLeftBottom_AnhViet;
            }
            return menuId;
        }
        public static short GetCateIdBanTinLuatVnId(byte languageId = 2)
        {
            short cateId = Constants.CateIdBanTinLuatVN;
            if (languageId == 1)
            {
                cateId = Constants.CateIdBanTinLuatVN_AnhViet;
            }
            return cateId;
        }

        /// <summary>
        /// Cập nhật thông tin và quyền người dùng
        /// </summary>
        public static void UpdateUserData()
        {
            if (IsAuthenticated)
            {
                int customerId = GetCurrentCustomerId();
                var result = ICSoft.CMSViewLib.CustomerHelpers.GetRolesByCustomerId(customerId);
                var serializeModel = new LawsVN.Library.Sercurity.AccountSerializerModel
                {
                    CustomerId = result.mCustomers.CustomerId,
                    CustomerName = result.mCustomers.CustomerName,
                    CustomerFullName = result.mCustomers.CustomerFullName,
                    CustomerMail = result.mCustomers.CustomerMail,
                    CustomerMobile = result.mCustomers.CustomerMobile,
                    Avatar = result.mCustomers.Avatar,
                    Roles = result.lRoles.HasValue() ? (from role in result.lRoles
                                                        select role.RoleName).ToArray() : new string[] { }
                };
                string userData = JsonConvert.SerializeObject(serializeModel);

                HttpCookie cookie = FormsAuthentication.GetAuthCookie(FormsAuthentication.FormsCookieName, true);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null)
                {
                    var newticket = new FormsAuthenticationTicket
                    (
                        ticket.Version,
                        ticket.Name,
                        ticket.IssueDate,
                        ticket.Expiration,
                        true,
                        userData,
                        ticket.CookiePath
                    );
                    cookie.Value = FormsAuthentication.Encrypt(newticket);
                    cookie.Expires = newticket.Expiration;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        // TODO: Logout tai khoan
        public static void Logout(HttpSessionStateBase session, HttpResponseBase response)
        {
            if (session != null)
            {
                string currentSessionId = session["_LuatVietNamSessionId"] != null
                    ? session["_LuatVietNamSessionId"].ToString()
                    : session.SessionID;
                //Xóa SessionId
                ICSoft.CMSViewLib.CustomerHelpers.LogoutSession(currentSessionId);
                session.Abandon();
            }
            FormsAuthentication.SignOut();
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty)
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            response.Cookies.Add(cookie);
            FormsAuthentication.RedirectToLoginPage();
        }

        public static void AbandonSession(HttpSessionStateBase session, HttpRequestBase request, HttpResponseBase response)
        {
            session.Clear();
            session.Abandon();
            session.RemoveAll();
            if (request.Cookies["ASP.NET_SessionId"] != null)
            {
                response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
        }

        public static string CreateSessionId(HttpContext httpContext)
        {
            var manager = new SessionIDManager();
            string newSessionId = manager.CreateSessionID(httpContext);
            return newSessionId;
        }

        public static void SetSessionId(HttpContext httpContext, string newSessionId)
        {
            var manager = new SessionIDManager();

            bool redirected;
            bool cookieAdded;

            manager.SaveSessionID(httpContext, newSessionId, out redirected, out cookieAdded);

        }

        public static string GetCurrentSessionId()
        {
            var manager = new SessionIDManager();
            return manager.GetSessionID(HttpContext.Current);
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

        public static string Truncatevalue(this string value, int maxLength)
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
                .RegexReplace("-{2,}", "-")                 // transforms multiple --- in - use to comment in sql scripts
                .RegexReplace(@"[*/]+", string.Empty)      // removes / and * used also to comment in sql scripts
                .RegexReplace(@"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase);
        }

        public static string SanitizeWithoutSplash(this string stringValue)
        {
            if (null == stringValue)
                return null;
            return stringValue
                .RegexReplace("-{2,}", "-")                 // transforms multiple --- in - use to comment in sql scripts
                .RegexReplace(@"[*]+", string.Empty)      // removes / and * used also to comment in sql scripts
                .RegexReplace(@"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase);
        }

        public static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith)
        {
            return Regex.Replace(stringValue, matchPattern, toReplaceWith);
        }

        public static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith, RegexOptions regexOptions)
        {
            return Regex.Replace(stringValue, matchPattern, toReplaceWith, regexOptions);
        }

        /// <summary>
        /// Kiểm tra List có giá trị hay không
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
        /// Kiểm tra Table có dữ liệu ko
        /// </summary>
        public static bool DataTableExists(this DataTable table)
        {
            if (table != null)
            {
                if (table.Rows.GetEnumerator().MoveNext())
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Lấy giá trị của ô cell theo tên cột
        /// </summary>
        public static T GetColumnValue<T>(DataRow row, string columnName)
        {
            T value = default(T);
            if (row.Table.Columns.Contains(columnName) && row[columnName] != null && !string.IsNullOrWhiteSpace(row[columnName].ToString()))
            {
                value = (T)Convert.ChangeType(row[columnName].ToString(), typeof(T));
            }
            return value;
        }

        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        public static string GenUrl(this string text, int id, string type = "")
        {
            if (!string.IsNullOrEmpty(text) && id > 0)
            {
                text = StringUtil.RemoveSignature(text);
                text = text.RegexReplace("(?:[^a-z0-9 _-]|(?<=['\"])s)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                text = text.Replace(" ", ConstantHelpers.RewriteSpace);
                text = text.ToLower();
                text = string.Concat(text, "-", id, "-", type, ConstantHelpers.RewriteExt);
                if (!text.StartsWith("http://")) text = string.Concat(CmsConstants.ROOT_PATH, text);
                return text;
            }
            return string.Empty;
        }
        public static int getYearfromString(string yearview)
        {
            int value = 0;
            try{
                string[] spldatefrom = yearview.Replace("-","/").Split('/');
                value = int.Parse((spldatefrom.Length > 2 ? spldatefrom[2] : "0"));
            }catch(Exception ex)
            {
                value = 0;
            }
            return value;
        }

        /// <summary>
        /// Danh sách tin bài theo vị trí
        /// </summary>
        /// <returns></returns>
        public static ArticlesViewHome GetArticlesViewHome()
        {
            byte dataTypeId = 1, currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
            return new CacheHelpers().GetOrSet<ArticlesViewHome>("ArticlesViewHome", () => ArticlesViewHelpers.View_GetArticleHome(Constants.SiteId, dataTypeId, currentLanguageId, 0,
                Constants.RowAmount3, 1));
        }

        public static ArticlesViewMaster GetArticlesViewMaster()
        {
            byte siteId = Extensions.GetSiteId();
            byte dataTypeId = 1, currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
            return new CacheHelpers().GetOrSet<ArticlesViewMaster>(currentLanguageId == LawsVnLanguages.AvailableLanguages[1].LanguageId ? "ArticlesViewMaster" : "ArticlesViewMasterEN", () => ArticlesViewHelpers.View_GetArticleMaster(siteId, dataTypeId, currentLanguageId, 0, 1));
        }

        public static ArticlesViewHome GetArticlesViewCustomerInterface()
        {
            byte dataTypeId = 1, currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
            return ArticlesViewHelpers.View_GetArticleHome(Constants.SiteId, dataTypeId, currentLanguageId, 0,
                Constants.RowAmount10, 0);
        }

        public static string GetEffectStatusNameById(this List<EffectStatus> list, byte effectStatusId)
        {
            int currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
            var effectStatus = EffectStatus.Static_Get(effectStatusId, list);
            if (effectStatus != null)
            {
                return currentLanguageId == 1 ? effectStatus.EffectStatusDesc : effectStatus.EffectStatusName;
            }
            return string.Empty;
        }

        /// <summary>
        /// ImageStreamCaptcha
        /// </summary>
        /// <param name="prefix">Lưu captcha theo prefix</param>
        /// <param name="noisy">Hiệu ứng vòng tròn bao phủ captcha</param>
        /// <returns></returns>
        public static MemoryStream ImageStreamCaptcha(string prefix = "", bool noisy = true)
        {
            using (var mMemoryStream = new MemoryStream())
            {
                var rand = new Random((int)DateTime.Now.Ticks);
                prefix = string.Concat("Captcha_", prefix);
                //tao cau hoi
                int a = rand.Next(10, 99);
                int b = rand.Next(0, 9);
                var captcha = string.Format("{0} + {1} = ?", a, b);
                LawsVnSession[prefix] = a + b;

                using (var bmp = new Bitmap(100, 30))
                using (var gfx = Graphics.FromImage((Image)bmp))
                {
                    gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    gfx.SmoothingMode = SmoothingMode.AntiAlias;
                    gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    //add noise 
                    if (noisy)
                    {
                        int i = 0, r = 0, x = 0, y = 0;
                        var pen = new Pen(Color.Yellow);
                        for (i = 1; i < 10; i++)
                        {
                            pen.Color = Color.FromArgb(
                                (rand.Next(0, 255)),
                                (rand.Next(0, 255)),
                                (rand.Next(0, 255)));

                            r = rand.Next(0, (100 / 3));
                            x = rand.Next(0, 100);
                            y = rand.Next(0, 30);

                            gfx.DrawEllipse(pen, x - r, y - r, r, r);
                        }
                    }

                    //add question 
                    gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                    //render as Jpeg 
                    bmp.Save(mMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //image = this.File(mem.GetBuffer(), "image/Jpeg");
                }

                return mMemoryStream;
            }
        }

        static HttpSessionState LawsVnSession
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new ApplicationException("No Http Context, No Session to Get!");

                return HttpContext.Current.Session;
            }
        }

        public static int GetCountByRelateType(this List<RelateTypes> list, byte relateTypeId)
        {
            int count = 0;
            if (list != null)
            {
                var relateType = list.FirstOrDefault(m => m.RelateTypeId == relateTypeId);
                count = relateType == null ? 0 : relateType.RowCount;
            }
            return count;
        }

        /// <summary>
        /// Danh sách văn bản liên quan theo RelateTypeId và DisplayPosition
        /// </summary>
        /// <param name="list">ListDocRelates</param>
        /// <param name="relateTypeId">relateTypeId</param>
        /// <param name="displayPosition">displayPosition</param>
        /// <returns></returns>
        public static List<DocRelates> GetDocsRelatesByRelateType(this List<DocRelates> list, byte relateTypeId, string displayPosition)
        {
            var retVal = new List<DocRelates>();
            if (list.HasValue())
            {
                retVal = !string.IsNullOrEmpty(displayPosition) ? list.FindAll(m => m.RelateTypeId == relateTypeId && m.DisplayPosition.Equals(displayPosition)) : list.FindAll(m => m.RelateTypeId == relateTypeId);
            }
            return retVal;
        }

        /// <summary>
        /// Trả về danh sách lỗi dạng key-value
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
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

        public static bool CustomerFieldsIsChecked(this List<CustomerFields> list, short fieldId)
        {
            return list.FirstOrDefault(m => m.FieldId == fieldId) != null;
        }


        public static bool CustomerFieldsIsChecked(this List<CustomerFields> list, short fieldId, byte docGroupId)
        {
            return list.FirstOrDefault(m => m.FieldId == fieldId && m.DocGroupId == docGroupId) != null;
        }
        public static bool CustomerProvincesIsChecked(this List<CustomerProvinces> list, short ProvinceId)
        {
            return list.FirstOrDefault(m => m.ProvinceId == ProvinceId) != null;
        }

        public static string TrimmedOrDefault(this string str, string strDefault)
        {
            return string.IsNullOrEmpty(str) ? strDefault : str.Trim();
        }

        /// <summary>
        /// Trả về ActionTypeId theo query tab
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte ActionTypeIdByTab(this string str)
        {
            byte actionTypeIdDefault = 0;
            switch (str)
            {
                case "effect":
                    actionTypeIdDefault = Constants.ActionTypeIdXemHieuLuc;
                    break;
                case "relate":
                    actionTypeIdDefault = Constants.ActionTypeIdXemLienQuan;
                    break;
                case "vietnamese":
                    actionTypeIdDefault = Constants.ActionTypeIdXemNoiDungTiengViet;
                    break;
                case "download":
                    actionTypeIdDefault = Constants.ActionTypeIdXemTaiVe;
                    break;
                case "content":
                    actionTypeIdDefault = Constants.ActionTypeIdXemNoiDung;
                    break;
                default:
                    actionTypeIdDefault = Constants.ActionTypeIdXemTomTat;
                    break;
            }
            return actionTypeIdDefault;
        }

        public static void GenResourcesToJavaScriptFile()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/assets/scripts/ui/resources.js");
            if (!File.Exists(filePath))
            {
                ResourceSet resourceSet = LawsVNEN.App_GlobalResources.Resource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                var resourceDictionary = resourceSet
                    .Cast<DictionaryEntry>().Where(x => x.Key.ToString().Contains("Javascript"))
                    .ToDictionary(entry => entry.Key.ToString(), entry => entry.Value.ToString());
                var json = new JavaScriptSerializer().Serialize(resourceDictionary);
                var javaScript = string.Format("window.lawsVn = window.lawsVn || {{}}; window.lawsVn.Resources = {0};", json);
                using (StreamWriter data = new StreamWriter(filePath))
                {
                    data.WriteLine(javaScript);
                }  
            }
        }

        public static string TagsNameGetUrl(this string value, int tagId = 0)
        {
            if (!string.IsNullOrEmpty(value) && tagId > 0)
            {
                value = StringUtil.RemoveSignature(value);
                value = LinkHelpers.RemoveSpecialCharactersNotSpace(value);
                value = value.Trim().Replace(" ", "-");
                value = value.ToLower();
                value = string.Concat(value, "-", tagId,"-tag", ConstantHelpers.RewriteExt);
                if (!value.StartsWith("http://")) value = string.Concat(CmsConstants.ROOT_PATH, value);
                return value;
            }
            return string.Empty;
        }

        public static string toString(this DateTime dt, string format = "dd/MM/yyyy")
        {
            return dt == DateTime.MinValue ? string.Empty : dt.ToString(format);
        }

        public static IEnumerable<Fields> GetListFieldsDocOthers(this string fieldText, List<Fields> listFields)
        {
            var arrayFieldText = fieldText.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in arrayFieldText)
            {
                var field = listFields.FirstOrDefault(m => m.FieldName.Equals(item.Trim()));
                if(field != null)
                    yield return field;
            }
        }

        public static string GetResourceNameByKey<T>(string key)
        {
            ResourceManager resourceManager = new ResourceManager(typeof(T));
            return resourceManager.GetString(key);
        }

        /// <summary>
        /// Dropdownlist hiển thị nội dung theo quyền truy cập
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">Select Id</param>
        /// <param name="name">Select Name</param>
        /// <param name="className">Select ClassName</param>
        /// <param name="selectList">SelectList</param>
        /// <returns></returns>
        public static MvcHtmlString DropdownListPermission(this HtmlHelper helper, string id, string name, string className, IEnumerable<SelectListItem> selectList)
        {
            return DropdownListPermission(helper, id, name, className, selectList, null);
        }

        private static MvcHtmlString DropdownListPermission(this HtmlHelper helper, string id, string name, string className, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            TagBuilder dropdown = new TagBuilder("select");
            dropdown.AddCssClass(className);
            dropdown.MergeAttribute("id", id);
            dropdown.MergeAttribute("name", name);

            TagBuilder option = new TagBuilder("option");
            option.MergeAttribute("value", "0");
            option.InnerHtml = Resource.EffectStatus;
            dropdown.InnerHtml += option.ToString();

            if (!IsAuthenticated || !((LawsVnPrincipal)HttpContext.Current.User).IsInRole("TC,TA,NC"))
            {
                dropdown.MergeAttribute("title", Resource.NoPermission);
                dropdown.AddCssClass("no-permission");
            }
            else
            {
                foreach (var selectListItem in selectList)
                {
                    option = new TagBuilder("option");
                    option.MergeAttribute("value", selectListItem.Value);
                    option.InnerHtml = selectListItem.Text;
                    if (selectListItem.Selected)
                    {
                        option.MergeAttribute("selected", "selected");
                    }
                    dropdown.InnerHtml += option.ToString();
                }
            }
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }

        public static bool IsRequest(this ControllerContext context, string method)
        {
            return context.HttpContext.Request.HttpMethod.Equals(method, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string TransactionStatusDescGetById(this byte transactionStatusId)
        {
            return transactionStatusId == Constants.TransactionStatusIdSuccess
                ? Resource.Success
                : (transactionStatusId == Constants.TransactionStatusIdUnSuccess
                    ? Resource.Unsuccessful
                    : (transactionStatusId == Constants.TransactionStatusIdPending ? Resource.WaitingForProgressing : string.Empty));

        }

        /// <summary>
        /// Link gói dịch vụ theo ServiceId
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public static string GetLinkServiceById(this short serviceId)
        {
            if (serviceId == Constants.ServiceIdTraCuuNangCao)
            {
                return String.Concat(CmsConstants.ROOT_PATH, "goi-dich-vu/goi-tra-cuu-nang-cao.html");
            }
            if (serviceId == Constants.ServiceIdTraCuuTiengAnh)
            {
                return String.Concat(CmsConstants.ROOT_PATH, "goi-dich-vu/goi-tra-cuu-tieng-anh.html");
            }
            if (serviceId == Constants.ServiceIdTraCuuTieuChuan)
            {
                return String.Concat(CmsConstants.ROOT_PATH, "goi-dich-vu/goi-tra-cuu-tieu-chuan.html");
            }
            if (serviceId == Constants.ServiceIdTraCuuMienPhi)
            {
                return String.Concat(CmsConstants.ROOT_PATH, "goi-dich-vu/goi-tra-cuu-mien-phi.html");
            }
            return "javascript:void(0)";
        }

    }
}