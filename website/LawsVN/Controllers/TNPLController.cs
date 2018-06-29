using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using LawsVN.Models;
using sms.utils;
using LawsVN.AppCode.Attribute;

namespace LawsVN.Controllers
{
    public class TNPLController : Controller
    {
        //
        // GET: /TNPL/
        [SEOAction]
        public ActionResult Index(byte lawTerminGroupId = 0, string terName = "", int page = 0, int pSize = 20)
        {
            int rowCount = 0;
            byte languageId = LawsVnLanguages.GetCurrentLanguageId();
            LawTermins mLawTermins = new LawTermins();
            TNPLViewModel model = new TNPLViewModel
            {
                HaveAmp = true,
                l_ArticlesNewView =
                    ArticlesViewHelpers.View_GetArticleByCategoryId(Constants.CateIdNewsLaw, 1, Constants.RowAmount5,""),
                l_ArticlesMostView = ArticlesViewHelpers.GetViewMostView_Paging(Constants.SiteId, 1,
                    Constants.RowAmount10, 1, 0, 0, 0, ref rowCount),
                l_LawTermins = mLawTermins.GetPage(0, pSize, page > 0 ? page - 1 : page, "TermName",
                    languageId, 0, terName, 0, lawTerminGroupId, "", "", ref rowCount),
                Page = page,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    PageIndex = page,
                    TotalPage = rowCount,
                    PageSize = pSize,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = pSize,
                    PageLoad = true,
                    UrlPaging = string.Empty,
                    LanguageId = LawsVnLanguages.GetCurrentLanguageId(),
                    UsingDisplayTable = 0,
                    TerName = terName,
                    LawTerminGroupId = lawTerminGroupId,
                    ControllerName = "Ajax",
                    ActionName = "TNPL_GetViewSearch",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "ListLawTerminsTab",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            return Extensions.GetViewMode("Index", model, Request);
        }

    }
}
