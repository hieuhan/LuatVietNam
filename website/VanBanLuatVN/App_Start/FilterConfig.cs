using System.Web;
using System.Web.Mvc;
using VanBanLuat.Librarys;

namespace LawsVN
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LawsHandleErrorAttribute());
        }
    }
}