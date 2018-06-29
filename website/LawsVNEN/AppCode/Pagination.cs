using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;

namespace LawsVNEN.Library
{
    public class Pagination
    {
        public static string PaginationLink(int page = 1, string urlPaging = "")
        {
            string rawUrl = HttpContext.Current.Request.RawUrl;
            if (string.IsNullOrEmpty(rawUrl))
            {
                return rawUrl;
            }
            if (!string.IsNullOrEmpty(urlPaging))
            {
                urlPaging = urlPaging.StartsWith("/") ? urlPaging : CmsConstants.ROOT_PATH + urlPaging;
                return string.Concat(urlPaging , "?page=" , page);
            }
            rawUrl = Regex.Replace(rawUrl,@"[?|&]page=[0-9]+",string.Empty);

            return string.Format("{0}{1}{2}", rawUrl, rawUrl.Contains("?") ? "&page=" : "?page=", page);
        }
    }
}