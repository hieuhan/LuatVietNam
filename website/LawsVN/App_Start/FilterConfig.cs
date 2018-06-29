using System.Web;
using System.Web.Mvc;
using LawsVN.Library;

namespace LawsVN
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LawsVnHandleErrorAttribute());
        }
    }
}