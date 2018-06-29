using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LawsVNEN
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.RouteExistingFiles = false;

            #region Redirect
            routes.MapRoute(
               "OldStaticLink",
               "{OldPath}/VL/{OldUrl}",
               new { controller = "Redirect", action = "OldUrl", OldUrl = UrlParameter.Optional }
           );
            routes.MapRoute(
               "OldStaticLink2",
               "VL/{OldUrl}",
               new { controller = "Redirect", action = "OldUrl", OldUrl = UrlParameter.Optional }
           );
            routes.MapRoute(
              "OldDocProperties",
              "Modules/VietLawsWS/docsproperties.aspx",
              new { controller = "Redirect", action = "OldUrlDinamic", OldParam = "docsproperties" }
            );
            routes.MapRoute(
               "OldDinamicLinkWithFolder",
               "{OldParam}/default.aspx",
               new { controller = "Redirect", action = "OldUrlDinamic", OldParam = UrlParameter.Optional }
           );
            routes.MapRoute(
               "OldDinamicLink",
               "default.aspx",
               new { controller = "Redirect", action = "OldUrlDinamic", OldParam = "AllOldPage" }
           );
            routes.MapRoute(
              "OldDinamicMenu",
              "{OldUrl}.aspx",
              new { controller = "Redirect", action = "OldUrl", OldUrl = UrlParameter.Optional }
          );
            #endregion

            routes.MapRoute(
                "UserGuide",
                "user-guide.html",
                   new { controller = "News", action = "UserGuide" });
            routes.MapRoute(
                "HuongDan",
                "guide.html",
                   new { controller = "News", action = "UserGuide" });
            routes.MapRoute(
                "LoginAccount",
                "user/login.html",
                new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                "Register",
                "user/register.html",
                new { controller = "Account", action = "Register" }
            );
            routes.MapRoute(
                "NewUserActive",
                "NewUserActive.html",
                new { controller = "Account", action = "NewUserActive" }
            );
            routes.MapRoute(
                "ResetPassword",
                "ResetPassword.html",
                new { controller = "Account", action = "ResetPassword" }
            );
            routes.MapRoute(
                "MyDocuments",
                "interested-documents.html",
                new { controller = "Account", action = "MyDocuments" }
            );
            routes.MapRoute(
                "ForgotPassword",
                "user/forgot-password.html",
                new { controller = "Account", action = "ForgotPassword" }
            );
            routes.MapRoute(
                "ChangePassword",
                "user/change-password.html",
                new { controller = "Account", action = "ChangePassword" }
            );
            routes.MapRoute(
                "LogoutAccount",
                "user/logout.html",
                new { controller = "Account", action = "Logout" }
            );
            routes.MapRoute(
                 "AccountProfileInfomation",
                 "user/account-information.html",
                 new { controller = "Account", action = "AccountProfile" }
             );
            routes.MapRoute(
                "BankPayment",
                "user/payment-using-bank-card.html",
                new { controller = "Ajax", action = "BankPayment" }
            );

            routes.MapRoute(
               "HistoryTransactions",
               "user/transactions-history.html",
               new { controller = "Account", action = "HistoryTransactions" }
           );
            routes.MapRoute(
                "HDSD",
                "huong-dan-su-dung/{CateName}-c{CategoryId}-article.html",
                   new { controller = "News", action = "UserGuide", CategoryId = UrlParameter.Optional },
                   new { CategoryId = @"\d+" });
            routes.MapRoute(
                "BanTinLuatVNEN",
                "legal-document-updates-c{CategoryId}-article.html",
                   new { controller = "News", action = "BanTinLuatVN", CategoryId = UrlParameter.Optional},
                   new { CategoryId = @"\d+"});
            routes.MapRoute(
                "BanTinLuatVN",
                "van-ban-cap-nhat-hang-tuan-c{CategoryId}-article.html",
                   new { controller = "News", action = "BanTinLuatVN", CategoryId = UrlParameter.Optional },
                   new { CategoryId = @"\d+"});
            routes.MapRoute(
                "BanTinEN",
                "legal-document-updates/{ArticleDetail}-{CategoryId}-{ArticleId}-article.html",
                   new { controller = "News", action = "BanTin", CategoryId = UrlParameter.Optional, ArticleId = UrlParameter.Optional },
                   new { CategoryId = @"\d+", ArticleId = @"\d+" });
            routes.MapRoute(
                "BanTin",
                "van-ban-cap-nhat-hang-tuan/{ArticleDetail}-{CategoryId}-{ArticleId}-article.html",
                   new { controller = "News", action = "BanTin", CategoryId = UrlParameter.Optional, ArticleId = UrlParameter.Optional },
                   new { CategoryId = @"\d+", ArticleId = @"\d+" });
            routes.MapRoute(
                "ListFields",
                "{url}-{fieldId}-f{docGroupId}.html",
                new { controller = "Docs", action = "Index", fieldId = UrlParameter.Optional, docGroupId = UrlParameter.Optional },
                new { fieldId = @"\d+", docGroupId = @"\d+" }
            );
            routes.MapRoute(
                "NewDocuments",
                "new-documents.html",
                new { controller = "Docs", action = "NewDocuments", DisplayTypeId = 72 }
            );
            routes.MapRoute(
                "OfficialGazette",
                "official-gazette.html",
                new { controller = "Docs", action = "NewDocuments", DisplayTypeId = 73, FileType = 2 }
            );
            routes.MapRoute(
                "OthersDocuments",
                "others-documents.html",
                new { controller = "Docs", action = "NewDocuments", DisplayTypeId = 74, FileType = 3 }
            );
            routes.MapRoute(
                "DocDetail",
                "{DocName}-{docId}-Doc{DocGroupId}.html",
                new { controller = "Docs", action = "DetailFull", docId = UrlParameter.Optional, DocGroupId = UrlParameter.Optional },
                new { docId = @"\d+", DocGroupId = @"\d+" },
                namespaces: new string[] { "LawsVNEN.Controllers" }
            );
            routes.MapRoute(
                "DocDetail12",
                "{FieldName}/{DocName}-{docId}-d{DocGroupId}.html",
                new { controller = "Docs", action = "DetailFull", docId = UrlParameter.Optional, DocGroupId = UrlParameter.Optional },
                new { docId = @"\d+", DocGroupId = @"\d+" },
                namespaces: new string[] { "LawsVNEN.Controllers" }
            );
            routes.MapRoute(
               "DocSearch",
               "seach-for-documents.html",
               new { controller = "Home", action = "DocSearch" }
           );
            routes.MapRoute(
                "vi",
                "change-language-vi.html",
                new { controller = "Home", action = "ChangeLanguage", lang = "vi" }
            );

            routes.MapRoute(
                "en",
                "change-language-en.html",
                new { controller = "Home", action = "ChangeLanguage", lang = "en" }
            );

            routes.MapRoute(
                "subscriber",
                "user/subscriber.html",
                new { controller = "Account", action = "Subscriber" }
            );

            routes.MapRoute(
                "serviceRenewal",
                "user/service-renewal.html",
                new { controller = "Account", action = "RenewalOfService" }
            );

            routes.MapRoute(
                "serviceConversion",
                "user/service-conversion.html",
                new { controller = "Account", action = "ServiceConversion" }
            );

            

            for (int i = 1; i <= 3; i++)
            {
                string routeName2 = "ArticleDetail-Level-" + i;
                string routeName3 = "ArticleDetail-" + i;
                IEnumerable<string> levels = Enumerable.Range(1, i).Select(r => string.Format("{{icsoft-level{0}}}", r));
                string url2 = string.Join("/", levels);
                string url3 = string.Join("/", levels);
                url2 += "-{CategoryId}-{ArticleId}-article.html";
                url3 += "-{ArticleId}-article.html";

                routes.MapRoute(routeName2, url2,
                    new { controller = "News", action = "ArticleDetail", ArticleId = UrlParameter.Optional, CategoryId = UrlParameter.Optional },
                    new { ArticleId = @"\d+", CategoryId = @"\d+" });

                routes.MapRoute(routeName3, url3,
                    new { controller = "News", action = "ArticleDetail", ArticleId = UrlParameter.Optional },
                    new { ArticleId = @"\d+" });
            }

            #region PAY
                routes.MapRoute(
                    "123PayRequest",
                    "Pay/123Pay",
                    new { controller = "Pay", action = "_123PayRequest" }
                );
                routes.MapRoute(
                    "123PayNotify",
                    "Pay/Notify",
                    new { controller = "Pay", action = "Notify" }
                );
            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "LawsVNEN.Controllers" }
            );

        }
    }
}