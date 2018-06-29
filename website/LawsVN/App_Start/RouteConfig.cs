using LawsVN.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LawsVN
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
              "OldDocProperties2",
              "{OldParam}docsproperties.aspx",
              new { controller = "Redirect", action = "OldUrlDinamic", OldParam = UrlParameter.Optional }
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
              "OldDinamicAll1",
              "{OldUrl}.aspx",
              new { controller = "Redirect", action = "OldUrl", OldUrl = UrlParameter.Optional }
          );
            routes.MapRoute(
             "OldDinamicAll2",
             "{NewPath}/{OldUrl}.aspx",
             new { controller = "Redirect", action = "OldUrl", OldUrl = UrlParameter.Optional }
         );
            #endregion
            #region Routers


            routes.MapRoute(
                "RegisterAccount",
                "user/dang-ky-tai-khoan.html",
                new { controller = "Account", action = "Register" }
            );
            routes.MapRoute(
                "AccountRegistrationSuccessful",
                "user/dang-ky-tai-khoan-thanh-cong.html",
                new { controller = "Account", action = "RegisterSuccessful" }
            );
            routes.MapRoute(
                "ChangePassword",
                "user/doi-mat-khau.html",
                new { controller = "Account", action = "ChangePassword" }
            );
            routes.MapRoute(
                "AccountProfile",
                "user/thong-tin-tai-khoan.html",
                new { controller = "Account", action = "AccountProfile" }
            );
            routes.MapRoute(
                "Service",
                "dich-vu.html",
                new { controller = "Home", action = "Service" }
            );

            routes.MapRoute(
                "LoginAccount",
                "user/dang-nhap-tai-khoan.html",
                new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                "ForgotPassword",
                "user/quen-mat-khau.html",
                new { controller = "Account", action = "ForgotPassword" }
            );

            routes.MapRoute(
                "ResetPassword",
                "ResetPassword.html",
                new { controller = "Account", action = "ResetPassword" }
            );

            routes.MapRoute(
                "NewUserActive",
                "NewUserActive.html",
                new { controller = "Account", action = "NewUserActive" }
            );
            
            routes.MapRoute(
                "LogoutAccount",
                "user/dang-xuat-tai-khoan.html",
                new { controller = "Account", action = "Logout" }
            );

            routes.MapRoute(
                "Unsubscribe",
                "huy-dang-ky-nhan-ban-tin.html",
                new { controller = "Account", action = "Unsubscribe" }
            );

            routes.MapRoute(
                "RegisterPersonalInterface",
                "user/dang-ky-giao-dien-ca-nhan.html",
                new { controller = "Account", action = "RegisterPersonalInterface" }
            );

            routes.MapRoute(
                "AccountProfileInfomation",
                "user/thong-tin-tai-khoan.html",
                new { controller = "Account", action = "AccountProfile" }
            );
            routes.MapRoute(
                "AccountEditProfileMobile",
                "user/chinh-sua-thong-tin-tai-khoan.html",
                new { controller = "Account", action = "AccountEditProfileMobile" }
            );
            routes.MapRoute(
                "AccountEditBusinessInfoMobile",
                "user/cap-nhat-thong-tin-doanh-nghiep.html",
                new { controller = "Account", action = "AccountEditBusinessInfoMobile" }
            );

            routes.MapRoute(
                "MyDocuments",
                "user/van-ban-cua-toi.html",
                new { controller = "Account", action = "MyDocuments" }
            );

            routes.MapRoute(
                "NoticeOfValidity",
                "user/thong-bao-hieu-luc.html",
                new { controller = "Account", action = "NoticeOfValidity" }
            );

            routes.MapRoute(
                "MyMessages",
                "user/tin-nhan.html",
                new { controller = "Account", action = "MyMessage" }
            );

            routes.MapRoute(
                "SavedMessages",
                "user/tin-nhan-da-luu.html",
                new { controller = "Account", action = "SavedMessages" }
            );

            routes.MapRoute(
                "DetailMessage",
                "user/chi-tiet-tin-nhan-{messageId}.html",
                new { controller = "Account", action = "DetailMessage", messageId = UrlParameter.Optional },
                new { messageId = @"\d+" }
            );

            routes.MapRoute(
                "HistoryTransactions",
                "user/lich-su-giao-dich.html",
                new { controller = "Account", action = "HistoryTransactions" }
            );

            routes.MapRoute(
                "ServiceNotice",
                "user/thong-bao-dang-ky-dich-vu.html",
                new { controller = "Account", action = "ServiceNotice" }
            );

            routes.MapRoute(
                "ServiceFreeNotice",
                "user/thong-bao-dang-ky-dich-vu-mien-phi.html",
                new { controller = "Account", action = "ServiceFreeNotice" }
            );

            routes.MapRoute(
                "RegisterFreeService",
                "user/dang-ky-goi-dich-vu-mien-phi.html",
                new { controller = "Account", action = "RegisterFreeService" }
            );

            routes.MapRoute(
                "RegisterServicePackages",
                "user/dang-ky-goi-dich-vu.html",
                new { controller = "Account", action = "RegisterService" }
            );

            routes.MapRoute(
                "ExtensionServicePackages",
                "user/nang-cap-goi-dich-vu.html",
                new { controller = "Account", action = "ExtensionServicePackages" }
            );

            routes.MapRoute(
                "RenewalOfService",
                "user/gia-han-dich-vu.html",
                new { controller = "Account", action = "RenewalOfService" }
            );

            routes.MapRoute(
                "ServiceConversion",
                "user/chuyen-doi-dich-vu.html",
                new { controller = "Account", action = "ServiceConversion" }
            );

            routes.MapRoute(
                "ChangeLanguage",
                "thay-doi-ngon-ngu.html",
                new { controller = "Home", action = "ChangeLanguage" }
            );
            routes.MapRoute(
                 "GioiThieu",
                 "gioi-thieu.html",
                 new { controller = "News", action = "GioiThieuDetail", ArticleId = Constants.ArticleIdGioiThieu }
            );

            routes.MapRoute(
                 "TinPhapLuat",
                 "tin-phap-luat.html",
                 new { controller = "News", action = "Index" }
            );

            routes.MapRoute(
                "ArticleSearch",
                "tim-kiem-tin-tuc.html",
                new { controller = "News", action = "Search" }
            );
            routes.MapRoute(
                "DocSearch",
                "tim-van-ban.html",
                new { controller = "Home", action = "DocSearch" }
            );
            routes.MapRoute(
                "TimKiem",
                "tim-van-ban-phap-luat.html",
                new { controller = "Home", action = "DocSearch" }
            );
            routes.MapRoute(
                "TimKiemCongVan",
                "Tim-Cong-Van.html",
                new { controller = "Home", action = "DocSearch" }
            );
            routes.MapRoute(
                "TimKiemVBPhapLuatTA",
                "Tim-Van-Ban-Phap-Luat-Tieng-Anh.html",
                new { controller = "Home", action = "DocSearch", LanguageId = 2 }
            );
            routes.MapRoute(
                "TimKiemUBND",
                "Tim-Van-Ban-Uy-Ban-Nhan-Dan.html",
                new { controller = "Home", action = "DocSearch" }
            );
            routes.MapRoute(
                "TimKiemTCVN",
                "Tim-Van-Ban-Tieu-Chuan-Viet-Nam.html",
                new { controller = "Home", action = "DocSearch" }
            );

            routes.MapRoute(
                "DocsViewNewest",
                "van-ban-moi.html",
                new { controller = "Docs", action = "ListDocs", docGroupId = 0 }
            );

            routes.MapRoute(
                "DocsViewEnglish",
                "van-ban-tieng-anh.html",
                new { controller = "Docs", action = "ListDocs", docGroupId = Constants.DocGroupIdVbpq, languageId = LawsVnLanguages.AvailableLanguages[1].LanguageId }
            );

            routes.MapRoute(
                "DocsViewEffectsSapCoHieuLuc",
                "van-ban-sap-co-hieu-luc.html",
                new { controller = "Docs", action = "ListDocsEffects", effectStatusName = "SapCoHieuLuc" }
            );

            routes.MapRoute(
                "DocsViewEffectsSapSuaDoi",
                "van-ban-sap-sua-doi.html",
                new { controller = "Docs", action = "ListDocsEffects", effectStatusName = "SapSuaDoi" }
            );

            routes.MapRoute(
                "DocsViewEffectsSapHetHieuLuc",
                "van-ban-sap-het-hieu-luc.html",
                new { controller = "Docs", action = "ListDocsEffects", effectStatusName = "SapHetHieuLuc" }
            );

            routes.MapRoute(
                "DocsViewEffectsHetHieuLuc",
                "van-ban-het-hieu-luc.html",
                new { controller = "Docs", action = "ListDocsEffects", effectStatusName = "HetHieuLuc" }
            );

            routes.MapRoute(
                "CongVan",
                "cong-van.html",
                new { controller = "DocsCongVan", action = "Index", fieldId = 0 }
            );
            routes.MapRoute(
                "CongVanThue",
                "cong-van-thue.html",
                new { controller = "DocsCongVan", action = "Index", fieldId = 4 }
            );
            routes.MapRoute(
                "CongVanHaiQuan",
                "cong-van-hai-quan.html",
                new { controller = "DocsCongVan", action = "Index", fieldId = 21 }
            );
            routes.MapRoute(
                "ListFields1",
                "{url}-{fieldId}-f{docGroupId}.html",
                new { controller = "Docs", action = "ListByField", fieldId = UrlParameter.Optional, docGroupId = UrlParameter.Optional },
                new { fieldId = @"\d+", docGroupId = @"\d+" }
            );
            //routes.MapRoute(
            //    "ListFields2",
            //    "{url}-{fieldId}-field2.html",
            //    new { controller = "DocsUBND", action = "ListByField", fieldId = UrlParameter.Optional },
            //    new { fieldId = @"\d+" }
            //);
            //routes.MapRoute(
            //    "ListFields5",
            //    "{url}-{fieldId}-field5.html",
            //    new { controller = "DocsConsolidation", action = "ListByField", fieldId = UrlParameter.Optional },
            //    new { fieldId = @"\d+" }
            //);
            //routes.MapRoute(
            //    "ListFields8",
            //    "{url}-{fieldId}-field8.html",
            //    new { controller = "Docs", action = "ListByField", fieldId = UrlParameter.Optional, docGroupId = Constants.DocGroupIdVbkhongconoidung },
            //    new { fieldId = @"\d+", docGroupId = @"\d+" }
            //);
            //routes.MapRoute(
            //    "ListFields6",
            //    "{url}-{fieldId}-field6.html",
            //    new { controller = "DocsCongVan", action = "ListByField", fieldId = UrlParameter.Optional },
            //    new { fieldId = @"\d+" }
            //);
            routes.MapRoute(
                "DocConsolidationDetail",
                "{DocName}-{docId}-Doc5.html",
                new { controller = "DocsConsolidation", action = "Properties", docId = UrlParameter.Optional },
                new { docId = @"\d+" }
            );
            routes.MapRoute(
                "CustomerListDocsByField",
                "giao-dien-ca-nhan/{url}-{fieldId}-f{docGroupId}.html",
                new { controller = "Customers", action = "CustomerListDocsByField", fieldId = UrlParameter.Optional, docGroupId = UrlParameter.Optional },
                new { fieldId = @"\d+", docGroupId = @"\d+" }
            );
            routes.MapRoute(
               "DocDetail1",
               "{DocName}-{docId}-Doc1.html",
               new { controller = "Docs", action = "Summary", docId = UrlParameter.Optional, docGroupId = 1 },
               new { docId = @"\d+" }
           );
            routes.MapRoute(
                "DocDetail2",
                "{DocName}-{docId}-Doc2.html",
                new { controller = "DocsUBND", action = "Properties", docId = UrlParameter.Optional, docGroupId = 2 },
                new { docId = @"\d+" }
            );
            routes.MapRoute(
               "DocDetail3",
               "{DocName}-{docId}-Doc3.html",
               new { controller = "TCVN", action = "Properties", docId = UrlParameter.Optional, docGroupId = 3 },
               new { docId = @"\d+" }
           );
            routes.MapRoute(
               "DocDetail6",
               "{DocName}-{docId}-Doc6.html",
               new { controller = "DocsCongVan", action = "Properties", docId = UrlParameter.Optional, docGroupId = 6 },
               new { docId = @"\d+" }
           );
            routes.MapRoute(
               "DocDetail8",
               "{DocName}-{docId}-Doc8.html",
               new { controller = "Docs", action = "Summary", docId = UrlParameter.Optional, docGroupId = 8 },
               new { docId = @"\d+" }
           );
            // route edit url
            routes.MapRoute(
                "DocConsolidationDetail2",
                "{FieldName}/{DocName}-{docId}-d5.html",
                new { controller = "DocsConsolidation", action = "DetailFull", docId = UrlParameter.Optional },
                new { docId = @"\d+" }
            );
            routes.MapRoute(
               "DocDetail12",
               "{FieldName}/{DocName}-{docId}-d1.html",
               new { controller = "Docs", action = "DetailFull", docId = UrlParameter.Optional, docGroupId = 1 },
               new { docId = @"\d+" }
           );
            routes.MapRoute(
                "DocDetail22",
                "{FieldName}/{DocName}-{docId}-d2.html",
                new { controller = "DocsUBND", action = "DetailFull", docId = UrlParameter.Optional, docGroupId = 2 },
                new { docId = @"\d+" }
            );
            routes.MapRoute(
               "DocDetail32",
               "{FieldName}/{DocName}-{docId}-d3.html",
               new { controller = "TCVN", action = "DetailFull", docId = UrlParameter.Optional, docGroupId = 3 },
               new { docId = @"\d+" }
           );
            routes.MapRoute(
               "DocDetail62",
               "{FieldName}/{DocName}-{docId}-d6.html",
               new { controller = "DocsCongVan", action = "DetailFull", docId = UrlParameter.Optional, docGroupId = 6 },
               new { docId = @"\d+" }
           );
            routes.MapRoute(
               "DocDetail82",
               "{FieldName}/{DocName}-{docId}-d8.html",
               new { controller = "Docs", action = "DetailFull", docId = UrlParameter.Optional, docGroupId = 8 },
               new { docId = @"\d+" }
           );

            routes.MapRoute(
                "CauHoiThuongGap",
                "nhung-cau-hoi-thuong-gap.html",
                new { controller = "News", action = "CauHoiThuongGap", CategoryId = 521 },
                new { CategoryId = @"\d+" }
            );
            routes.MapRoute(
                "DanhSachTag",
                "{TagName}-{TagId}-tag.html",
                new { controller = "News", action = "ArticleTags", TagId = UrlParameter.Optional },
                new { TagId = @"\d+" }
            );
            routes.MapRoute(
                "HuongDan",
                "huong-dan.html",
                new { controller = "News", action = "HuongDan" }
            );
            routes.MapRoute(
                "HuongDanChild",
                "huong-dan/{catename}.html",
                new { controller = "News", action = "HuongDan" }
            );
            routes.MapRoute(
                "LienHe",
                "lien-he.html",
                new { controller = "Feedbacks", action = "Feedbacks" }
            );
            routes.MapRoute(
                "ThuatNguPhapLy",
                "thuat-ngu-phap-ly.html",
                new { controller = "TNPL", action = "Index" }
            );
            routes.MapRoute(
                "LuatSuView",
                "danh-ba-luat-su.html",
                new { controller = "Lawers", action = "Index" }
            );
            routes.MapRoute(
                "LawerSearchView",
                "tim-kiem-luat-su.html",
                new { controller = "Lawers", action = "Search" }
            );
            routes.MapRoute(
                "LawerDetail",
                "{lawername}-{lawerId}-lawer.html",
                new { controller = "Lawers", action = "LawersDetail", lawerId = UrlParameter.Optional },
                new { lawerId = @"\d+" });
            routes.MapRoute(
                "CustomerInterFace",
                "giao-dien-ca-nhan.html",
                new { controller = "Customers", action = "CustomerInterFace" }
            );
            routes.MapRoute(
                "CustomerInterFaceLocation",
                "giao-dien-ca-nhan/van-ban-uy-ban-nhan-dan.html",
                new { controller = "Customers", action = "CustomerInterFaceLocation" }
            );
            routes.MapRoute(
                "CustomerInterFaceTCVN",
                "giao-dien-ca-nhan/van-ban-tieu-chuan-viet-nam.html",
                new { controller = "Customers", action = "CustomerInterFaceTCVN" }
            );
            routes.MapRoute(
                "BanTinLuatVN",
                "ban-tin-luatvietnam-c{CategoryId}-article.html",
                   new { controller = "News", action = "BanTinLuatVn", CategoryId = UrlParameter.Optional },
                   new { CategoryId = @"\d+"});

            routes.MapRoute(
                "BanTinLuatVN_Child",
                "ban-tin-luatvietnam/{CateName}-c{CategoryId}-article.html",
                   new { controller = "News", action = "BanTinLuatVn", CategoryId = UrlParameter.Optional },
                   new { CategoryId = @"\d+"});

            routes.MapRoute(
                "VB_CNHT",
                "van-ban-cap-nhat-hang-tuan/{ArticleDetail}-{CategoryId}-{ArticleId}-article.html",
                   new { controller = "News", action = "VB_CNHT", CategoryId = UrlParameter.Optional, ArticleId = UrlParameter.Optional },
                   new { CategoryId = @"\d+", ArticleId = @"\d+" });
            routes.MapRoute(
                "DSVanBanTieuBieu",
                "diem-tin-van-ban-moi/danh-sach-van-ban-tieu-bieu-{ArticleDetail}-{CategoryId}-{ArticleId}-article.html",
                   new { controller = "News", action = "BanTinHL", CategoryId = UrlParameter.Optional, ArticleId = UrlParameter.Optional },
                   new { CategoryId = @"\d+", ArticleId = @"\d+" });
            routes.MapRoute(
                "BanTinHieuLuc",
                "ban-tin-hieu-luc/{ArticleDetail}-{CategoryId}-{ArticleId}-article.html",
                   new { controller = "News", action = "BanTinHL", CategoryId = UrlParameter.Optional, ArticleId = UrlParameter.Optional },
                   new { CategoryId = @"\d+", ArticleId = @"\d+" });
            routes.MapRoute(
                "TinVBMoi",
                "diem-tin-van-ban-moi/{ArticleDetail}-{CategoryId}-{ArticleId}-article.html",
                   new { controller = "News", action = "TinVBMoi", CategoryId = UrlParameter.Optional, ArticleId = UrlParameter.Optional },
                   new { CategoryId = @"\d+", ArticleId = @"\d+" });

            routes.MapRoute(
                "ServiceDetail",
                "dich-vu/{ArticleDetail}-{CategoryId}-{ArticleId}-article.html",
                   new { controller = "News", action = "ServiceDetail", CategoryId = UrlParameter.Optional, ArticleId = UrlParameter.Optional },
                   new { CategoryId = @"\d+", ArticleId = @"\d+" });

            routes.MapRoute(
                "GoiDichVuMienPhi",
                "goi-dich-vu/goi-tra-cuu-mien-phi.html",
                new { controller = "News", action = "ArticleGoiDichVu", CategoryId = 529, ArticleId = 12094 });

            routes.MapRoute(
                "GoiDichVuTieuChuan",
                "goi-dich-vu/goi-tra-cuu-tieu-chuan.html",
                new { controller = "News", action = "ArticleGoiDichVu", CategoryId = 529, ArticleId = 12091 });

            routes.MapRoute(
                "GoiDichVuTiengAnh",
                "goi-dich-vu/goi-tra-cuu-tieng-anh.html",
                new { controller = "News", action = "ArticleGoiDichVu", CategoryId = 529, ArticleId = 12093 });

            routes.MapRoute(
                "GoiDichVuNangCao",
                "goi-dich-vu/goi-tra-cuu-nang-cao.html",
                new { controller = "News", action = "ArticleGoiDichVu", CategoryId = 529, ArticleId = 12092 });

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
                    new { controller = "News", action = "ArticlesCategory", CategoryId = UrlParameter.Optional },
                    new { CategoryId = @"\d+" });

                routes.MapRoute(routeName2, url2,
                    new { controller = "News", action = "ArticleDetail", ArticleId = UrlParameter.Optional, CategoryId = UrlParameter.Optional },
                    new { ArticleId = @"\d+", CategoryId = @"\d+" });

                routes.MapRoute(routeName3, url3,
                    new { controller = "News", action = "ArticleDetail", ArticleId = UrlParameter.Optional },
                    new { ArticleId = @"\d+" });
            }
            routes.MapRoute(
                 "PaymentTransactions",
                 "quan-ly-giao-dich.html",
                 new { controller = "Customers", action = "PaymentTransactionsManagement" }
            );
            //VBHN
            routes.MapRoute(
                 "VBHN",
                 "van-ban-hop-nhat.html",
                 new { controller = "DocsConsolidation", action = "Index" }
            );
            

            routes.MapRoute(
                 "TieuChuanVN",
                 "tieu-chuan-viet-nam.html",
                 new { controller = "TCVN", action = "Index", docTypeId = 61 }
            );
            routes.MapRoute(
                 "QuyChuanVietNam",
                 "quy-chuan-viet-nam.html",
                 new { controller = "TCVN", action = "Index", docTypeId = 62 }
            );
            routes.MapRoute(
                 "TieuChuanNganh",
                 "tieu-chuan-nganh.html",
                 new { controller = "TCVN", action = "Index", docTypeId = 63 }
            );
            routes.MapRoute(
                 "TieuChuanXDVN",
                 "tieu-chuan-xay-dung-viet-nam.html",
                 new { controller = "TCVN", action = "Index", docTypeId = 64 }
            );
            routes.MapRoute(
                 "VanBanUBND",
                 "van-ban-uy-ban-nhan-dan.html",
                 new { controller = "DocsUBND", action = "Index" }
            );
            routes.MapRoute(
                "VanBanUBNDTheoTinh",
                "van-ban-uy-ban-nhan-dan-{provinceName}-{provinceId}.html",
                new { controller = "DocsUBND", action = "ListDocsByProvince", provinceId = UrlParameter.Optional },
            new { provinceId = @"\d+" });

            routes.MapRoute(
                "LawersInsert",
                "them-luat-su.html",
                new { controller = "Lawers", action = "Insert" }
            );

            routes.MapRoute(
                "Sitemap",
                "sitemap.xml",
                new { controller = "Sitemap", action = "Index" }
            );
            routes.MapRoute(
                "SitemapHome",
                "sitemap_home.xml",
                new { controller = "Sitemap", action = "SitemapHome" }
            );
            routes.MapRoute(
                "SitemapTags",
                "sitemap_tags{page}.xml",
                new { controller = "Sitemap", action = "SitemapTags", page = UrlParameter.Optional },
                new { page = @"\d+" });

            routes.MapRoute(
                "SitemapNews",
                "sitemap_news{page}.xml",
                new { controller = "Sitemap", action = "SitemapNews", page = UrlParameter.Optional },
                new { page = @"\d+" });

            routes.MapRoute(
                "SitemapDocs",
                "sitemap_document{page}.xml",
                new { controller = "Sitemap", action = "SitemapDocument", page = UrlParameter.Optional },
                new { page = @"\d+" });

            #endregion

            #region partialRouters
            routes.MapRoute(
               "HeaderPartial",
               "header-partial.html",
               new { controller = "Shared", action = "HeaderPartial" },
               namespaces: new[] { "LawsVN.Controllers" }
           );
            #endregion

            #region ajaxRouters
            routes.MapRoute(
                "getDocsNewest",
                "docs-get-list.html",
                new { controller = "Ajax", action = "GetDocsNewest" }
            );

            routes.MapRoute(
                "DocsGetViewEffect",
                "docs-get-list-effect.html",
                new { controller = "Ajax", action = "Docs_GetViewEffect" }
            );

            routes.MapRoute(
                "NewsLetterEmail",
                "dang-ky-nhan-ban-tin.html",
                new { controller = "Ajax", action = "NewsLetterEmail" }
            );
            routes.MapRoute(
                "CaptchaImage",
                "lawvn-captcha.html",
                new { controller = "Home", action = "CaptchaImage" }
            );
            routes.MapRoute(
                "UserDeleteFields",
                "user/xoa-linh-vuc.html",
                new { controller = "Ajax", action = "DeleteOneFieldById" }
            );
            routes.MapRoute(
                "AccountProfileSwitchMode",
                "user/chinh-sua-thong-tin-ca-nhan.html",
                new { controller = "Ajax", action = "AccountProfileSwitchMode" }
            );
            routes.MapRoute(
                "EditAccountProfile",
                "user/sua-thong-tin-ca-nhan.html",
                new { controller = "Account", action = "EditAccountProfile" }
            );
            routes.MapRoute(
                "BusinessInformationSwitchMode",
                "user/chinh-sua-thong-tin-doanh-nghiep.html",
                new { controller = "Ajax", action = "BusinessInformationSwitchMode" }
            );
            routes.MapRoute(
                "EditBusinessInformation",
                "user/sua-thong-tin-doanh-nghiep.html",
                new { controller = "Account", action = "EditBusinessInformation" }
            );
            routes.MapRoute(
                "UploadAvatar",
                "user/upload-avatar.html",
                new { controller = "Ajax", action = "UploadAvatar" }
            );
            routes.MapRoute(
                "loadChangePasswordForm",
                "user/changePasswordForm.html",
                new { controller = "Ajax", action = "PartialChangePassword" }
            );
            routes.MapRoute(
                "EditCustomerFields",
                "user/sua-linh-vuc-quan-tam.html",
                new { controller = "Ajax", action = "EditCustomerFields" }
            );
            routes.MapRoute(
                "SubscriptionNoticeOfValidity",
                "user/dang-ky-thong-bao-hieu-luc.html",
                new { controller = "Ajax", action = "SubscriptionNoticeOfValidity" }
            );
            routes.MapRoute(
                "SaveDocument",
                "user/luu-van-ban-quan-tam.html",
                new { controller = "Ajax", action = "SaveDocument" }
            );
            routes.MapRoute(
                "DocsGetViewSearch",
                "doc-get-view-search.html",
                new { controller = "Ajax", action = "Docs_GetViewSearch" }
            );
            routes.MapRoute(
                "vbhn-getlist",
                "vbhn-getlist.html",
                new { controller = "Ajax", action = "DocsConsolidation_GetViewSearch" }
            );
            routes.MapRoute(
                "UploadFile",
                "uploadfile.html",
                new { controller = "Ajax", action = "UploadFile" }
            );
            routes.MapRoute(
                "Docs_ViewSearch",
                "docs-search.html",
                new { controller = "Ajax", action = "Docs_ViewSearch" }
            );
            routes.MapRoute(
                "Docs_GetViewSearchWithKeyword",
                "doc-search-by-keyword.html",
                new { controller = "Ajax", action = "Docs_GetViewSearchWithKeyword" }
            );
            routes.MapRoute(
                "CustomerDocs_GetViewEffect",
                "customer-docs-get-view-effect.html",
                new { controller = "Ajax", action = "CustomerDocs_GetViewEffect" }
            );
            routes.MapRoute(
                "Error404",
                "404.html",
                new { controller = "Error", action = "Error404" }
            );

            #endregion

            #region PAY
            routes.MapRoute(
               "123PayRequest",
               "Pay/123Pay",
               new { controller = "Pay", action = "_123PayRequest" }
           );
            routes.MapRoute(
               "ZaloResult",
               "Pay/ZaloPay",
               new { controller = "Pay", action = "ResultZaloPay" }
           );
            routes.MapRoute(
              "ZaloPayNotify",
              "Pay/NotifyZalo",
              new { controller = "Pay", action = "NotifyZalo" }
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
                namespaces: new string[] { "LawsVN.Controllers" }
            );

        }
    }
}