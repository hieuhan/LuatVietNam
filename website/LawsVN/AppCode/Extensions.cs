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
using System.Net;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.SessionState;
using HtmlAgilityPack;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library.Sercurity;
using sms.utils;
using LawsVN.Models;
using LawsVN.Models.Account;
using Newtonsoft.Json;
using System.Net;
using HtmlAgilityPack;

namespace LawsVN.Library
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
            var currentUser = (LawsVnPrincipal)HttpContext.Current.User;
            return IsAuthenticated ? currentUser.CustomerName : string.Empty;
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

        /// <summary>
        /// Cập nhật thông tin và quyền người dùng
        /// </summary>
        public static void UpdateUserData()
        {
            if (IsAuthenticated)
            {
                int customerId = GetCurrentCustomerId();
                var result = ICSoft.CMSViewLib.CustomerHelpers.GetRolesByCustomerId(customerId);
                var serializeModel = new AccountSerializerModel
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
                string currentSessionId = !string.IsNullOrWhiteSpace(session["_LuatVietNamSessionId"].ToString())
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

        public static string ExtSubstring(this string str, int index, int length)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length > length)
                {
                    StringBuilder sb = new StringBuilder(str);
                    return string.Concat(sb.ToString(index, length),"...");
                }
                return str;
            }
            return string.Empty;
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

        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
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
            try
            {
                string[] spldatefrom = yearview.Replace("-", "/").Split('/');
                value = int.Parse((spldatefrom.Length > 2 ? spldatefrom[2] : "0"));
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.WriteLog(ex.ToString());
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
            byte dataTypeId = 1, currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
            return new CacheHelpers().GetOrSet<ArticlesViewMaster>("ArticlesViewMaster", () => ArticlesViewHelpers.View_GetArticleMaster(Constants.SiteId, dataTypeId, currentLanguageId, 0,1));
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
                int a = rand.Next(1000, 9999);
                int b = rand.Next(0, 9);
                var captcha = a.ToString(); //string.Format("{0} + {1} = ?", a, b);
                LawsVnSession[prefix] = a;// + b;

                using (var bmp = new Bitmap(60, 30))
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
                case "luocdo":
                    actionTypeIdDefault = Constants.ActionTypeIdXemLuocDo;
                    break;
                case "hieuluc":
                    actionTypeIdDefault = Constants.ActionTypeIdXemHieuLuc;
                    break;
                case "lienquan":
                    actionTypeIdDefault = Constants.ActionTypeIdXemLienQuan;
                    break;
                case "tienganh":
                    actionTypeIdDefault = Constants.ActionTypeIdXemNoiDungTiengAnh;
                    break;
                case "taive":
                    actionTypeIdDefault = Constants.ActionTypeIdXemTaiVe;
                    break;
                case "noidung":
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
                ResourceSet resourceSet = LawsVN.App_GlobalResources.Resource.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
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

        public static string TagsUrl(this string value, int tagId = 0)
        {
            value = value.ToLowerInvariant();

            value = StringUtil.RemoveSignature(value);

            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            value = Regex.Replace(value, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);

            value = value.Trim('-', '_');

            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            value = string.Concat(value, "-", tagId, "-tag", ConstantHelpers.RewriteExt);

            return value;
        }

        public static string toString(this DateTime dt, string textEmpty = "", string format = "dd/MM/yyyy")
        {
            return dt == DateTime.MinValue ? textEmpty : dt.ToString(format);
        }

        public static IEnumerable<T> UpdateWhere<T>(this IEnumerable<T> enumerable,
            Action<T> update, Func<T, bool> where) where T : class
        {

            foreach (var item in enumerable)
            {
                if (where(item))
                {
                    update(item);
                }
            }
            return enumerable;
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
        public static bool IsMobile()
        {
            return HttpContext.Current.Request.Browser.IsMobileDevice;
        }
        public static ViewResult GetViewMode(string viewName, object model)
        {
            ViewResult retVal = new ViewResult
            {
                ViewName = viewName,
                ViewData = {Model = model}
            };
            if (IsMobile())
                retVal.ViewName = viewName + ".Mobile";
            return retVal;
        }

        public static ViewResult GetViewMode(string viewName, object model, HttpRequestBase request)
        {
            var query = GetUrlParameter(request, "layout");
            var retVal = new ViewResult
            {
                ViewName = viewName,
                ViewData = { Model = model }
            };
            if (!string.IsNullOrEmpty(query) && query.ToLower().Equals("amp"))
                retVal.ViewName = viewName + ".AMP";
            else if (IsMobile())
                retVal.ViewName = viewName + ".Mobile";
            return retVal;
        }

        public static bool IsImage(this string file)
        {
            string regexPattern = @"([^\s]+(\.(?i)(jpg|jpeg|png|gif|bmp))$)";
            return !string.IsNullOrEmpty(file) && Regex.IsMatch(file, regexPattern);
        }

        public static string GetLinkServiceById(this short serviceId)
        {
            if (serviceId == Constants.ServiceIdTraCuuNangCao)
            {
                return String.Concat(CmsConstants.ROOT_PATH, "goi-dich-vu/goi-tra-cuu-nang-cao.html");
            }
            if(serviceId == Constants.ServiceIdTraCuuTiengAnh)
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

            if (!IsAuthenticated)
            {
                dropdown.MergeAttribute("title", Resource.NoPermission);
                dropdown.AddCssClass("no-permission");
            }
            else
            {
                if (!((LawsVnPrincipal) HttpContext.Current.User).IsInRole(Constants.RolesFull))
                {
                    dropdown.MergeAttribute("title", Resource.NoPermissionNotInRole);
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
            }

            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }

        public static string TransactionStatusDescGetById(this byte transactionStatusId)
        {
            return transactionStatusId == Constants.TransactionStatusIdSuccess
                ? Resource.Success
                : (transactionStatusId == Constants.TransactionStatusIdUnSuccess
                    ? Resource.UnSuccess
                    : (transactionStatusId == Constants.TransactionStatusIdPending ? Resource.Pending : string.Empty));

        }

        public static List<string> AdvDataLoadFromFileToList(object AdvCateId)
        {
            List<string> l_advContent = new List<string>();
            string fileName = string.Empty;
            string filePath = string.Empty;

            try
            {
                l_advContent = CacheHelpers<List<string>>.Get("advs_site_1");
                fileName = "advs_site_1.js";
                filePath = AppDomain.CurrentDomain.BaseDirectory + "Data\\advs\\";
                filePath += fileName;
                if (l_advContent == null || l_advContent.Count == 0)
                {
                    if (File.Exists(filePath))
                    {
                        StreamReader sr = new StreamReader(filePath);
                        l_advContent = new List<string>();
                        string m_LineData = "";
                        while (!sr.EndOfStream)
                        {
                            m_LineData = sr.ReadLine();
                            m_LineData = m_LineData.Replace("<script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script>", "");
                            if (!m_LineData.Contains(" = '';"))
                            {
                                //cms.utils.Log.writeLog(m_LineData, ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                                l_advContent.Add(m_LineData);
                            }
                            else
                            {
                                //cms.utils.Log.writeLog("Not Add:" + m_LineData, ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                            }

                        }
                        sr.Close();
                        sr.Dispose();
                        CacheHelpers<List<string>>.Insert("advs_site_1", l_advContent);
                    }
                    else
                    {
                        l_advContent = new List<string>();
                        sms.utils.Log.writeLog("file not exist:" + filePath, ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                    }
                }

            }
            catch (Exception ex)
            {
                sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }

            return l_advContent;
        }
        public static string AdvDataLoadFromFile(object AdvCateId)
        {
            string advContent = "";
            string fileName = string.Empty;
            string filePath = string.Empty;
            if (AdvCateId == null || string.IsNullOrEmpty(AdvCateId.ToString()))
            {
                sms.utils.Log.writeLog("AdvCateId is null:" + HttpContext.Current.Request.RawUrl, ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                return advContent;
            }
            try
            {
                advContent = CacheHelpers<string>.Get("advs_site_1V2");
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
                            advContent = advContent.Replace("<script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script>", "");
                            advContent += m_LineData;
                            m_LineData = sr.ReadLine();
                        }
                        sr.Close();
                        sr.Dispose();
                        advContent = advContent.Replace("script", "scr' + 'ipt");
                        advContent = "<script type=\"text/javascript\">" + Environment.NewLine + advContent + Environment.NewLine + "</script>";
                        CacheHelpers<string>.Insert("advs_site_1V2", advContent);
                    }
                    else
                    {
                        advContent = "";
                        sms.utils.Log.writeLog("file not exist:" + filePath, ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                    }
                }

            }
            catch (Exception ex)
            {
                sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }

            return advContent;
        }

        public static string GetUrlParameter(HttpRequestBase request, string parName)
        {
            string result = string.Empty;
            if (request.Url != null)
            {
                var urlParameters = HttpUtility.ParseQueryString(request.Url.Query);
                if (urlParameters.AllKeys.Contains(parName))
                {
                    result = urlParameters.Get(parName);
                }
            }
            return result;
        }

        public static Uri AddOrUpdateParameter(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (query.AllKeys.Contains(paramName))
            {
                query[paramName] = paramValue;
            }
            else
            {
                query.Add(paramName, paramValue);
            }
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Lấy dữ liệu từ TempData
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this TempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(typeof(T).FullName + key, out o);
            return o == null ? null : (T)o;
        }

        /// <summary>
        /// Gán dữ liệu cho TempData 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void Set<T>(this TempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[typeof(T).FullName + key] = value;
        }

        public static void AddUnique<T>(this List<T> source, Func<T, T, bool> predicate, List<T> items)
        {
            foreach (T item in items)
            {
                bool existsInSource = source.Any(s => predicate(s, item));
                if (!existsInSource) source.Add(item);
            }
        }

        public static string GetImageUrlByCategoryId(this short categoryId)
        {
            if (categoryId == Constants.CateIdBanTinHieuLuc)
            {
                return "/assets/images/bantin3.png";
            } if (categoryId == Constants.CateIdCapNhatHangTuan)
            {
                return "/assets/images/bantin2.png";
            } if (categoryId == Constants.CateIdDiemTinVb)
            {
                return "/assets/images/bantin4.png";
            } return "/assets/images/bantin1.png";
        }

        public static bool IsDiemTin(this short categoryId)
        {
            return categoryId == Constants.CateIdBanTinLuat ||
                   categoryId == Constants.CateIdDiemTinVb ||
                   categoryId == Constants.CateIdDiemTinChinhSachMoi;
        }

        public static IEnumerable<Tuple<DateTime, DateTime>> SplitDateRange(this DateTime start, DateTime end, int dayChunkSize=7)
        {
            DateTime chunkEnd;
            while ((chunkEnd = start.AddDays(dayChunkSize)) < end)
            {
                yield return Tuple.Create(start, chunkEnd);
                start = chunkEnd.AddDays(1);
            }
            yield return Tuple.Create(start, end);
        }

        /// <summary>
        /// Trả về số tuần trong năm theo ngày
        /// </summary>
        /// <param name="date">Ngày cần biết số tuần</param>
        /// <param name="firstDayOfWeek">Ngày bắt đầu trong tuần - Mặc định: Chủ nhật</param>
        /// <returns></returns>
        public static int GetWeekNumber(this DateTime date, DayOfWeek firstDayOfWeek = DayOfWeek.Sunday)
        {
            DateTimeFormatInfo dinfo = DateTimeFormatInfo.CurrentInfo;
            return dinfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, firstDayOfWeek);
        }
    }

    public class GoogleAmp
    {
        private readonly string _source;
        private static string _result;
        private static HtmlDocument _htmlDoc = new HtmlDocument();
        private List<HtmlNode> _deleteNodes = new List<HtmlNode>();
        private readonly List<string> _notToRemove = new  List<string> {"br"};
        private readonly string paragraphClass = "paragraph";
        private readonly IDictionary<string, string[]> _whitelist = new Dictionary<string, string[]> 
        {
            {
                "img", new[] { "src","alt","title" ,"class","layout", "width","height"}
            },
            { "strong", null },
            { "span", new[] {"class" } },
            { "em" , null},
            { "p", new[] {"class" } },
            { "ul", new[] {"class" } },
            { "ol", new[] {"class" } },
            { "li", new[] {"class" } },
            { "div", new[] { "class", "align" } },
            { "u", new[] {"class" } },
            { "table", new[] {"class", "align","border", "cellpadding", "cellspacing" } },
            { "tbody", new[] {"class" } },
            { "tr", new[] {"class" } },
            { "td", new[] {"class" ,"colspan","rowspan","valign","width" } },
            { "th", new[] {"class"} },
            { "a", new[] {"href","class" } }
        };

        private readonly IDictionary<string, string[]> _blacklist = new Dictionary<string, string[]> 
        {
            {"font", null},
            {"meta", null},
            {"style", null},
            {"u1:p", null},
            {"st1:date", null},
            {"st1:country-region", null},
            {"st1:place", null},
            {"st1:placename", null},
            {"?xml:namespace", null},
            {"br", new[] {"type"}}
        };

        private GoogleAmp(string source)
        {
            this._source = source;
        }

        public static string Convert(string source)
        {
            var converter = new GoogleAmp(source);
            converter.Convert();
            return _result;
        }

        public static string DocConvert(string source)
        {
            var converter = new GoogleAmp(source);
            converter.DocConvert();
            return _result;
        }

        private void Convert()
        {
            _htmlDoc = GetHtmlDocument(this._source);
            CleanNode(_htmlDoc.DocumentNode);
            RemoveInlineStyles();
            UpdateAmpImages();
        }

        private void DocConvert()
        {
            _htmlDoc = GetHtmlDocument(this._source);
            DocCleanNode(_htmlDoc.DocumentNode);
            _result = _htmlDoc.DocumentNode.OuterHtml;
        }

        private void UpdateAmpImages()
        {
            var imageList = _htmlDoc.DocumentNode.Descendants("img");
            int wSize = 300, hSize = 240;
            string urlImage = string.Empty;
            if (imageList.IsAny())
            {
                if (!HtmlNode.ElementsFlags.ContainsKey("amp-img"))
                {
                    HtmlNode.ElementsFlags.Add("amp-img", HtmlElementFlag.Closed);
                }
                foreach (var imgTag in imageList)
                {
                    var original = imgTag.OuterHtml;
                    var replacement = imgTag.Clone();
                    replacement.Name = "amp-img";
                    HtmlAttribute srcAttribute = imgTag.Attributes.FirstOrDefault(i => i.Name.Equals("src"));
                    if (srcAttribute != null)
                    {
                        urlImage = srcAttribute.Value;
                    }
                    if (replacement.Attributes["width"] == null)
                    {
                        getSizeOfImage(urlImage, ref wSize, ref hSize);
                        hSize = (int)(hSize / ((Double)wSize / 320));
                        replacement.SetAttributeValue("width", "320");
                    }
                    if (replacement.Attributes["height"] == null)
                    {
                        replacement.SetAttributeValue("height", hSize.ToString());
                    }
                    replacement.Attributes.Add("class", "-amp-element -amp-layout-responsive -amp-layout-size-defined -amp-layout");
                    replacement.SetAttributeValue("layout", "responsive");
                    _result = _result.Replace(original, replacement.OuterHtml);
                }        
            }
        }

        private void RemoveInlineStyles()
        {
            var elements = _htmlDoc.DocumentNode.Descendants();
            foreach (var htmlNode in elements)
            {
                if (htmlNode.Attributes["style"] == null)
                {
                    continue;
                }
                htmlNode.Attributes.Remove(htmlNode.Attributes["style"]);
            }
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);
            _htmlDoc.Save(writer);
            _result = builder.ToString();
        }

        private static HtmlDocument GetHtmlDocument(string source)
        {
            HtmlDocument htmlDoc = new HtmlDocument
            {
                OptionFixNestedTags = true,
                OptionAutoCloseOnEnd = true,
                OptionDefaultStreamEncoding = Encoding.UTF8
            };
            htmlDoc.LoadHtml(source);
            return htmlDoc;
        }

        private void getSizeOfImage(string url, ref int width, ref int height)
        {
            width = 320;
            height = 240;
            try
            {
                 if(url.StartsWith("//"))
                 {
                     url = "http:" + url;
                 }
                byte[] imageData = new WebClient().DownloadData(url);
                MemoryStream imgStream = new MemoryStream(imageData);
                Image img = Image.FromStream(imgStream);

                width = img.Width;
                height = img.Height;
            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }

        }

        private void CleanNode(HtmlNode node)
        {
            CleanAttribuesUnSupport(node);
            for (int i = _deleteNodes.Count - 1; i >= 0; i--)
            {
                HtmlNode nodeToDelete = _deleteNodes[i];
                nodeToDelete.ParentNode.RemoveChild(nodeToDelete, true);
            }
        }

        private void CleanAttribuesUnSupport(HtmlNode node)
        {
            if (node.NodeType == HtmlNodeType.Element)
            {
                if (_blacklist.ContainsKey(node.Name))
                {
                    _deleteNodes.Add(node);
                }
                else if (node.HasAttributes)
                {
                    for (int i = node.Attributes.Count - 1; i >= 0; i--)
                    {
                        HtmlAttribute currentAttribute = node.Attributes[i];
                        if (_whitelist.ContainsKey(node.Name))
                        {
                            string[] allowedAttributes = _whitelist[node.Name];
                            if (allowedAttributes != null)
                            {
                                //xóa thuộc tính không hỗ trợ
                                if (!allowedAttributes.Contains(currentAttribute.Name))
                                    node.Attributes.Remove(currentAttribute);
                                //Xóa ảnh cũ
                                if (currentAttribute.Name.Equals("src") && currentAttribute.Value.IndexOf("blank.gif", StringComparison.Ordinal) >=0)
                                {
                                    _deleteNodes.Add(node);
                                }
                                if (currentAttribute.Name.Equals("href") && (currentAttribute.Value.Contains("javascript") || currentAttribute.Value.Contains("file://")))
                                {
                                    _deleteNodes.Add(node);
                                }
                            }
                            else
                            {
                                node.Attributes.Remove(currentAttribute);
                            }
                        }
                    }
                }
            }
            // xóa node comment và parrent của nó
            if (node.NodeType == HtmlNodeType.Comment)
            {
                var parentNodeRemove = node.ParentNode;
                node.Remove();
                if (parentNodeRemove != null && parentNodeRemove.Attributes.Count == 0 && !_notToRemove.Contains(parentNodeRemove.Name) && string.IsNullOrEmpty(parentNodeRemove.InnerText))
                {
                    parentNodeRemove.ParentNode.RemoveChild(parentNodeRemove);
                }
            }
            // đệ quy 
            if (node.HasChildNodes)
            {
                node.ChildNodes.ToList().ForEach(v => CleanAttribuesUnSupport(v));
            }
        }

        private void DocCleanNode(HtmlNode node)
        {
            DocCleanAttribuesUnSupport(node);
            for (int i = _deleteNodes.Count - 1; i >= 0; i--)
            {
                HtmlNode nodeToDelete = _deleteNodes[i];
                nodeToDelete.ParentNode.RemoveChild(nodeToDelete, true);
            }
        }

        private void DocCleanAttribuesUnSupport(HtmlNode node)
        {
            if (node.NodeType == HtmlNodeType.Element)
            {
                if (!_whitelist.ContainsKey(node.Name))
                {
                    _deleteNodes.Add(node);
                }
                else if (node.HasAttributes)
                {
                    for (int i = node.Attributes.Count - 1; i >= 0; i--)
                    {
                        HtmlAttribute currentAttribute = node.Attributes[i];
                        string[] allowedAttributes = _whitelist[node.Name];
                        if (allowedAttributes != null)
                        {
                            if (node.Name.Equals("p"))
                            {
                                if (currentAttribute.Name.Equals("style") &&
                                    currentAttribute.Value.IndexOf("text-indent", StringComparison.Ordinal) > 0)
                                {
                                    var paragraphClassValue = node.GetAttributeValue("class", null);
                                    var separator = paragraphClassValue != null ? ";" : null;
                                    node.SetAttributeValue("class", string.Concat(paragraphClassValue, separator, paragraphClass));
                                }
                            }

                            //xóa thuộc tính không hỗ trợ
                            if (!allowedAttributes.Contains(currentAttribute.Name))
                                node.Attributes.Remove(currentAttribute);
                        }
                        else
                        {
                            node.Attributes.Remove(currentAttribute);
                        }
                    }
                }
            }
            // xóa node comment và parrent của nó
            if (node.NodeType == HtmlNodeType.Comment)
            {
                var parentNodeRemove = node.ParentNode;
                node.Remove();
                if (parentNodeRemove != null && parentNodeRemove.Attributes.Count == 0 && !_notToRemove.Contains(parentNodeRemove.Name) && string.IsNullOrEmpty(parentNodeRemove.InnerText))
                {
                    parentNodeRemove.ParentNode.RemoveChild(parentNodeRemove);
                }
            }
            // đệ quy 
            if (node.HasChildNodes)
            {
                node.ChildNodes.ToList().ForEach(v => DocCleanAttribuesUnSupport(v));
            }
        }
    }
}