using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LawsVN.Models;
using ICSoft.LawDocsLib;
using ICSoft.CMSViewLib;
using LawsVN.Library;

namespace LawsVN.Controllers
{
    public class ServicesController : Controller
    {

        [OutputCache(Duration = 180, VaryByParam = "Services,Page")]
        public ActionResult Services()
        {
            ServicesViewModel mServicesViewModel = new ServicesViewModel();
            ServicePackages m_ServicePackages = new ServicePackages();

            mServicesViewModel.l_ServicePackages = m_ServicePackages.ServicePackages_GetList(0, "ServicePackageName asc", 0, "", 2);
            mServicesViewModel.m_ArticlesViewDetail = ArticlesViewHelpers.View_GetArticleDetail(Constants.ArticleIdSoSanhQl, 0, 0, 0);
            return View(mServicesViewModel);
        }
    }
}
