using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VanBanLuat.Librarys;

namespace LawsVN
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.RouteExistingFiles = false;

            #region Routers
            routes.MapRoute(
                "Register",
                "user/dang-ky.html",
                new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                "NewUserActive",
                "NewUserActive.html",
                new { controller = "Account", action = "NewUserActive" }
            );

            routes.MapRoute(
                "Login",
                "user/dang-nhap.html",
                new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                "Logout",
                "user/dang-xuat.html",
                new { controller = "Account", action = "Logout" }
            );

            routes.MapRoute(
                "ChangePassword",
                "user/doi-mat-khau.html",
                new { controller = "Account", action = "ChangePassword" }
            );

            routes.MapRoute(
                "Profile",
                "user/thong-tin-ca-nhan.html",
                new { controller = "Account", action = "AccountProfile" }
            );

            routes.MapRoute(
                "MyDocuments",
                "user/van-ban-cua-toi.html",
                new { controller = "Account", action = "MyDocuments" }
            );

            routes.MapRoute(
                "ForgotPassword",
                "quen-mat-khau.html",
                new { controller = "Account", action = "ForgotPassword" }
            );

            routes.MapRoute(
                "ResetPassword",
                "ResetPassword.html",
                new { controller = "Account", action = "ResetPassword" }
            );

            routes.MapRoute(
                "404",
                "404.html",
                new { controller = "Error", action = "Index" }
            );

            routes.MapRoute(
                "DocSearch",
                "tim-van-ban.html",
                new { controller = "Home", action = "DocSearch" }
            );

            routes.MapRoute(
                "VietNamStandardsDetailFull",
                "{FieldName}/{DocName}-{docId}-d3.html",
                new { controller = "Docs", action = "VietNamStandardsDetailFull", docId = UrlParameter.Optional, docGroupId = Constants.DocGroupIdTcvn },
                new { docId = @"\d+", docGroupId = @"\d+" }
            );

            routes.MapRoute(
                "DocsConsolidationDetailFull",
                "{FieldName}/{DocName}-{docId}-d5.html",
                new { controller = "Docs", action = "DocsConsolidationDetailFull", docId = UrlParameter.Optional, docGroupId = Constants.DocGroupIdVbhn },
                new { docId = @"\d+", docGroupId = @"\d+" }
            );

            routes.MapRoute(
                "DocDetailFull",
                "{FieldName}/{DocName}-{docId}-d{docGroupId}.html",
                new { controller = "Docs", action = "DetailFull", docId = UrlParameter.Optional, docGroupId = UrlParameter.Optional },
                new { docId = @"\d+", docGroupId = @"\d+" }
            );
            routes.MapRoute(
                "ListFieldsV2",
                "van-ban-luat-{FieldName}-{fieldHexId}.html",
                new { controller = "Docs", action = "ListDocByFieldV2", fieldHexId = UrlParameter.Optional },
                new { fieldHexId = @"[a-f0-9]+" }
            );
            routes.MapRoute(
                "DocDetailFullV2",
                "{FieldName}/{DocName}-{docHexId}.html",
                new { controller = "Docs", action = "DetailFullV2", docHexId = UrlParameter.Optional },
                new { docHexId = @"[a-f0-9]+" }
            );
            //Begin PhapLyDoanhNghiep
            routes.MapRoute(
                 "PhapLyDoanhNghiep",
                 "phap-ly-doanh-nghiep.html",
                 new { controller = "New", action = "PhapLyDoanhNghiep" }
            );
            routes.MapRoute(
                "LoaiHinhDoanhNghiep",
                "loai-hinh-doanh-nghiep/{CateName}-c{categoryId}-article.html",
                new { controller = "New", action = "LoaiHinhDoanhNghiep", categoryId = UrlParameter.Optional },
                new { categoryId = @"\d+" }
            );
            routes.MapRoute(
                "BieuMau",
                "bieu-mau.html",
                new { controller = "New", action = "BieuMau" }
            );
            routes.MapRoute(
                "BieuMauDetail1",
                "bieu-mau/{ArticleTitle}-{categoryId}-{articleId}-article.html",
                new { controller = "New", action = "BieuMauDetail", categoryId = UrlParameter.Optional, articleId = UrlParameter.Optional },
                new { categoryId = @"\d+", articleId = @"\d+" }
            );
            routes.MapRoute(
                "BieuMauDetail2",
                "bieu-mau{CateName}/{ArticleTitle}-{categoryId}-{articleId}-article.html",
                new { controller = "New", action = "BieuMauDetail", categoryId = UrlParameter.Optional, articleId = UrlParameter.Optional },
                new { categoryId = @"\d+", articleId = @"\d+" }
            );
            //End PhapLyDoanhNghiep
            routes.MapRoute(
                 "TinPhapLuat",
                 "tin-tuc-phap-luat.html",
                 new { controller = "New", action = "Index" }
            );
            routes.MapRoute(
                "LienHe",
                "lien-he.html",
                new { controller = "Feedbacks", action = "Feedbacks" }
            );
            routes.MapRoute(
                "DanhSachTag",
                "{TagName}-{TagId}-tag.html",
                new { controller = "New", action = "ArticleTags", TagId = UrlParameter.Optional },
                new { TagId = @"\d+"}
            );
            routes.MapRoute(
                "AccountProfileInfomation",
                "user/thong-tin-tai-khoan.html",
                new { controller = "Account", action = "AccountProfile" }
            );
            for (int i = 1; i <= 3; i++)
            {
                string routeName1 = "Article-Level-" + i;
                string routeName2 = "ArticleDetail-Level-" + i;
                string routeName3 = "ArticleDetail-" + i;
                IEnumerable<string> levels = Enumerable.Range(1, i).Select(r => string.Format("{{icsoft-level{0}}}", r));
                var enumerable = levels as string[] ?? levels.ToArray();
                string url1 = string.Join("/", enumerable);
                string url2 = string.Join("/", enumerable);
                string url3 = string.Join("/", enumerable);
                url1 += "-c{CategoryId}-article.html";
                url2 += "-{CategoryId}-{ArticleId}-article.html";
                url3 += "-{ArticleId}-article.html";

                routes.MapRoute(routeName1, url1,
                    new { controller = "New", action = "ArticleByCate", CategoryId = UrlParameter.Optional },
                    new { CategoryId = @"\d+" });

                routes.MapRoute(routeName2, url2,
                    new { controller = "New", action = "ArticleDetail", ArticleId = UrlParameter.Optional, CategoryId = UrlParameter.Optional },
                    new { ArticleId = @"\d+", CategoryId = @"\d+" });

                routes.MapRoute(routeName3, url3,
                    new { controller = "New", action = "ArticleDetail", ArticleId = UrlParameter.Optional },
                    new { ArticleId = @"\d+" });
            }
            #endregion

            #region DocsRouter

            routes.MapRoute(
                "VBHN",
                "van-ban-hop-nhat.html",
                new { controller = "Docs", action = "ListDocByDocGroupId", docGroupId = Constants.DocGroupIdVbhn }
            );
            routes.MapRoute(
                "TCVN",
                "tieu-chuan-viet-nam.html",
                new { controller = "Docs", action = "ListDocByDocGroupId", docGroupId = Constants.DocGroupIdTcvn }
            );
            routes.MapRoute(
                "UBND",
                "van-ban-uy-ban-nhan-dan.html",
                new { controller = "Docs", action = "ListDocByDocGroupId", docGroupId = Constants.DocGroupIdUbnd }
            );
            routes.MapRoute(
                "CV",
                "cong-van.html",
                new { controller = "Docs", action = "ListDocByDocGroupId", docGroupId = Constants.DocGroupIdCongVan, menuItemId = Constants.MenuItemIdCongVanMoi }
            );
            routes.MapRoute(
                "VBPQ",
                "van-ban-phap-quy.html",
                new { controller = "Docs", action = "ListDocByDocGroupId", docGroupId = Constants.DocGroupIdVbpq }
            );
            routes.MapRoute(
                "DocsViewNewest",
                "van-ban-moi.html",
                new { controller = "Docs", action = "ListDocs", docGroupId = 0, menuItemId = Constants.MenuItemIdVbMoi }
            );

            routes.MapRoute(
                "SapCoHieuLuc",
                "van-ban-sap-co-hieu-luc.html",
                new { controller = "Docs", action = "ListDocByEffectStatus", EffectStatusType = "SapCoHieuLuc" }
            );

            routes.MapRoute(
                "HetHieuLuc",
                "van-ban-het-hieu-luc.html",
                new { controller = "Docs", action = "ListDocByEffectStatus", EffectStatusType = "HetHieuLuc" }
            );

            routes.MapRoute(
                "SapHetHieuLuc",
                "van-ban-sap-het-hieu-luc.html",
                new { controller = "Docs", action = "ListDocByEffectStatus", EffectStatusType = "SapHetHieuLuc" }
            );

            routes.MapRoute(
                "SapSuaDoi",
                "van-ban-sap-sua-doi.html",
                new { controller = "Docs", action = "ListDocByEffectStatus", EffectStatusType = "SapSuaDoi" }
            );
            routes.MapRoute(
                "ListFields1",
                "{url}-{fieldId}-f{docGroupId}.html",
                new { controller = "Docs", action = "ListDocByField", fieldId = UrlParameter.Optional, docGroupId = UrlParameter.Optional },
                new { fieldId = @"\d+", docGroupId = @"\d+" }
            );
            #endregion

            #region AjaxRouter
            routes.MapRoute(
                "DocsGetPage",
                "docgetpage.html",
                new { controller = "Ajax", action = "DocsGetPage" }
            );

            routes.MapRoute(
                "DocSearchGetPage",
                "docsearchgetpage.html",
                new { controller = "Ajax", action = "DocSearchGetPage" }
            );

            routes.MapRoute(
                "DocsRelateGetPage",
                "docrelategetpage.html",
                new { controller = "Ajax", action = "DocsRelateGetPage" }
            );

            routes.MapRoute(
                "ArticlesGetPage",
                "articlegetpage.html",
                new { controller = "Ajax", action = "ArticlesGetPage" }
            );

            routes.MapRoute(
                "GoogleLogin",
                "google-login.html",
                new { controller = "Account", action = "GoogleLogin" }
            );

            routes.MapRoute(
                "FacebookLogin",
                "facebook-login.html",
                new { controller = "Account", action = "FacebookLogin" }
            );

            routes.MapRoute(
                "SaveMyDoc",
                "luu-van-ban-quan-tam.html",
                new { controller = "Ajax", action = "SaveMyDoc" }
            );

            routes.MapRoute(
                "CustomerDocsGetPage",
                "customer-docs-getpage.html",
                new { controller = "Ajax", action = "CustomerDocsGetPage" }
            );

            routes.MapRoute(
                "PartialDocSendMail",
                "mail-van-ban.html",
                new { controller = "Ajax", action = "DocSendMail" }
            );

            routes.MapRoute(
                "DocSendMail",
                "gui-mail-link-van-ban.html",
                new { controller = "Account", action = "DocSendMail" }
            );

            routes.MapRoute(
                "PartialDocFeedBack",
                "gop-y-van-ban.html",
                new { controller = "Ajax", action = "DocFeedback" }
            );

            routes.MapRoute(
                "DocFeedBack",
                "gui-gop-y-van-ban.html",
                new { controller = "Account", action = "DocFeedback" }
            );

            routes.MapRoute(
                "ArticleSendLinkForm",
                "form-gui-link-bai-viet.html",
                new { controller = "Ajax", action = "ArticleSendLinkForm" }
            );

            routes.MapRoute(
                "ArticleFeedBackForm",
                "form-gui-gop-y-bai-viet.html",
                new { controller = "Ajax", action = "ArticleFeedBackForm" }
            );

            routes.MapRoute(
                "ArticleSendLink",
                "gui-link-bai-viet.html",
                new { controller = "Ajax", action = "ArticleSendLink" }
            );

            routes.MapRoute(
                "PartialGuiYeuCauVB",
                "yeu-cau-van-ban.html",
                new { controller = "Ajax", action = "GuiYeuCauVB" }
            );

            routes.MapRoute(
                "GuiYeuCauVB",
                "gui-yeu-cau-van-ban.html",
                new { controller = "Account", action = "GuiYeuCauVB" }
            );

            routes.MapRoute(
                "ListDocsNewest",
                "ajax-van-ban-moi.html",
                new { controller = "Ajax", action = "ListDocsNewest" }
            );
            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "LawsVN.Controllers" }
            );

        }
    }
}