using System.Web;
using System.Web.Mvc;
using LawsVNEN.Library;

namespace LawsVNEN
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LawsVnHandleErrorAttribute());
        }
    }
}