using System.Linq;
using System.Web.Mvc;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using LawsVN.Models.Sitemap;

namespace LawsVN.Controllers
{
    public class SitemapController : Controller
    {
        private readonly byte _languageId = LawsVnLanguages.AvailableLanguages[0].LanguageId;
        private readonly int _rowCountSitemap = 5000;
        private readonly byte _reviewStatusId = 2;
        //
        // GET: /Sitemaps/
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult Index()
        {
            var model = new SitemapModel
            {
                RowCountArticles = new Articles().GetSiteMapPage(Constants.SiteId),
                RowCountDocs = new Docs().GetSiteMapPage(),
                RowCountTags = new Tags().GetSiteMapPage()
            };
            return View(model);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult SitemapHome()
        {
            var model = new SitemapHomeModel();
            model.ListMenu = model.ListMenuAll.FindAll(m=>m.MenuId==Constants.MenuIdSitemap).ToList();
            model.ListCategories = new Categories{SiteId = Constants.SiteId}.GetAllHierachy(0, "", _languageId, 0, 0, _reviewStatusId, "");
            return View(model);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult SitemapTags(int page = 1)
        {
            int rowCount = 0;
            var list = new Tags().GetPage(0, _rowCountSitemap, page > 0 ? page - 1 : page, "", _languageId, 0, 0, "", _reviewStatusId,
                "", "", ref rowCount);
            return View(list);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult SitemapNews(int page = 1)
        {
            int rowCount = 0;
            var list = new Articles().GetPage(0, _rowCountSitemap, page > 0 ? page - 1 : page, "", _languageId, 0, 0, "", 0, _reviewStatusId, 0, 0,
                0, 0, "", "", 0, ref rowCount);
            return View(list);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult SitemapDocument(int page = 1)
        {
            int rowCount = 0;
            var list = new Docs().GetPage(0, _rowCountSitemap, page > 0 ? page - 1 : page, "", _languageId, "", 0, 0, 0, "", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", "", ref rowCount);
            return View(list);
        }

    }
}
