using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace VanBanLuat.Librarys
{
    public class Constants
    {
        #region SEO
        public static string WebsiteTitle = ConfigurationManager.AppSettings["WebsiteTitle"] ?? "";
        public static string WebsiteDescription = ConfigurationManager.AppSettings["WebsiteDescription"] ?? "";
        public static string WebsiteKeywords = ConfigurationManager.AppSettings["WebsiteKeywords"] ?? "";
        public static string WebsiteCanonical = ConfigurationManager.AppSettings["WebsiteCanonical"] ?? "";
        public static string WEBSITE_IMAGEDOMAIN = ConfigurationManager.AppSettings["WEBSITE_IMAGEDOMAIN"] ?? "";
        public static string SeoHeader = ConfigurationManager.AppSettings["SeoHeader"] ?? "";
        public static string WebsiteImage = ConfigurationManager.AppSettings["WebsiteImage"] ?? "";
        public static string NoImageUrl = ConfigurationManager.AppSettings["NoImageUrl"] ?? "";
        public static string NoImageUrl_Article = ConfigurationManager.AppSettings["NoImageUrl_Article"] ?? "";
        public static string NoImageUrl_File = ConfigurationManager.AppSettings["NoImageUrl_File"] ?? "";
        public static string NoAvatar = ConfigurationManager.AppSettings["NoAvatar"] ?? "/assets/images/avata-ad1.png";
        public static string MaleAvatar = ConfigurationManager.AppSettings["MaleAvatar"] ?? "/assets/images/avata-ad1.png";
        public static string FeMaleAvatar = ConfigurationManager.AppSettings["FeMaleAvatar"] ?? "/assets/images/avata-ad2.png";
        #endregion

        #region string
        public static readonly string FacebookAppId = ConfigurationManager.AppSettings["FacebookAppId"] ?? "193341217939183";
        public static readonly string GoogleApiKey = ConfigurationManager.AppSettings["GoogleApiKey"] ?? "AIzaSyBoRfI5ccorKe3EcpQYf0Y6DY3YA3onxhQ";
        public static readonly string WEBSITE_DOMAIN = ConfigurationManager.AppSettings["WEBSITE_DOMAIN"] ?? "";
        public static readonly string MEDIA_VIEWPATH = ConfigurationManager.AppSettings["MEDIA_VIEWPATH"] ?? "";
        public static readonly string MEDIA_DOWNLOADPATH = ConfigurationManager.AppSettings["MEDIA_DOWNLOADPATH"] ?? "";
        public static readonly string ROOT_PATH = ConfigurationManager.AppSettings["ROOT_PATH"] ?? "/";
        public static readonly string CateId_DanhMucPhapLy_ListCate = ConfigurationManager.AppSettings["CateId_DanhMucPhapLy_ListCate"] ?? "585;567;571;566";//;568;569;570 chính sách mới, DN cần biết, DN vốn nước ngoài, Biểu mẫu cần biết
        #endregion

        #region byte
        public static readonly byte SiteId = byte.Parse(ConfigurationManager.AppSettings["SiteId"] ?? "4");
        public static readonly byte DocGroupIdVbpq = byte.Parse(ConfigurationManager.AppSettings["DocGroupIdVbpq"] ?? "1");
        public static readonly byte DocGroupIdUbnd = byte.Parse(ConfigurationManager.AppSettings["DocGroupIdUbnd"] ?? "2");
        public static readonly byte DocGroupIdTcvn = byte.Parse(ConfigurationManager.AppSettings["DocGroupIdTcvn"] ?? "3");
        public static readonly byte DocGroupIdTthc = byte.Parse(ConfigurationManager.AppSettings["DocGroupIdTthc"] ?? "4");
        public static readonly byte DocGroupIdVbhn = byte.Parse(ConfigurationManager.AppSettings["DocGroupIdVbhn"] ?? "5");
        public static readonly byte DocGroupIdCongVan = byte.Parse(ConfigurationManager.AppSettings["DocGroupIdCongVan"] ?? "6");
        public static readonly byte DocGroupIdKhongCoNoiDungDownload = byte.Parse(ConfigurationManager.AppSettings["DocGroupIdKhongCoNoiDungDownload"] ?? "8");
        public static readonly byte LanguageId = byte.Parse(ConfigurationManager.AppSettings["LanguageId"] ?? "1");
        public static readonly byte OpenAuthTypeIdFacebook = byte.Parse(ConfigurationManager.AppSettings["OpenAuthTypeIdFacebook"] ?? "1");
        public static readonly byte OpenAuthTypeIdGoogle = byte.Parse(ConfigurationManager.AppSettings["OpenAuthTypeIdGoogle"] ?? "2");
        public static readonly byte SysMessageTypeIdSuccess = byte.Parse(ConfigurationManager.AppSettings["SysMessageTypeIdSuccess"] ?? "2");
        public static readonly byte ReviewStatusIdApproved = byte.Parse(ConfigurationManager.AppSettings["ReviewStatusIdApproved"] ?? "2");
        public static readonly byte RegistTypeIdVbqt = byte.Parse(ConfigurationManager.AppSettings["RegistTypeId_VBQT"] ?? "1");
        public static readonly byte ArticleTypeIdTinThuong = byte.Parse(ConfigurationManager.AppSettings["ArticleTypeIdTinThuong"] ?? "1");
        public static readonly byte ArticleTypeIdBieuMau = byte.Parse(ConfigurationManager.AppSettings["ArticleTypeIdBieuMau"] ?? "10");
        #endregion

        #region short
        public static readonly short MenuIdHeader = short.Parse(ConfigurationManager.AppSettings["MenuIdHeader"] ?? "209");
        public static readonly short MenuIdFooter = short.Parse(ConfigurationManager.AppSettings["MenuIdFooter"] ?? "210");
        public static readonly short MenuIdHeaderMobile = short.Parse(ConfigurationManager.AppSettings["MenuIdHeaderMobile"] ?? "212");
        public static readonly short MenuItemIdHomePage = short.Parse(ConfigurationManager.AppSettings["MenuItemIdHomePage"] ?? "1849");
        public static readonly short MenuItemIdVbMoi = short.Parse(ConfigurationManager.AppSettings["MenuItemIdVbMoi"] ?? "1850");
        public static readonly short MenuItemIdCongVanMoi = short.Parse(ConfigurationManager.AppSettings["MenuItemIdCongVanMoi"] ?? "1851");
        public static readonly short MenuItemIdTinTucPhapLuat = short.Parse(ConfigurationManager.AppSettings["MenuItemIdTinTucPhapLuat"] ?? "1852");
        public static readonly short MenuItemIdPhapLyDoanhNghiep = short.Parse(ConfigurationManager.AppSettings["MenuItemIdPhapLyDoanhNghiep"] ?? "1872");
        public static readonly short CateId_TinTuc = short.Parse(ConfigurationManager.AppSettings["CateId_TinTuc"] ?? "564");
        public static readonly short CateId_TinTucPhapLuat = short.Parse(ConfigurationManager.AppSettings["CateId_TinTucPhapLuat"] ?? "559");
        public static readonly short CateId_ThongBaoVBMoi = short.Parse(ConfigurationManager.AppSettings["CateId_ThongBaoVBMoi"] ?? "560");
        public static readonly short CateId_PhapLyDoanhNghiep = short.Parse(ConfigurationManager.AppSettings["CateId_PhapLyDoanhNghiep"] ?? "561");
        public static readonly short CateId_ChinhSachMoi = short.Parse(ConfigurationManager.AppSettings["CateId_ChinhSachMoi"] ?? "562");
        public static readonly short CateId_TieuDiem = short.Parse(ConfigurationManager.AppSettings["CateId_TieuDiem"] ?? "565");
        public static readonly short CateId_LoaiHinhDoanhNghiep = short.Parse(ConfigurationManager.AppSettings["CateId_LoaiHinhDoanhNghiep"] ?? "573");
        public static readonly short CateId_BieuMauCanBiet = short.Parse(ConfigurationManager.AppSettings["CateId_BieuMauCanBiet"] ?? "566");
        public static readonly short CateId_BieuMau = short.Parse(ConfigurationManager.AppSettings["CateId_BieuMau"] ?? "572");
        public static readonly short FeedBackGroupIdGopY = short.Parse(ConfigurationManager.AppSettings["FeedBackGroupIdGopY"] ?? "75");
        public static readonly short FeedBackGroupId = short.Parse(ConfigurationManager.AppSettings["FeedBackGroupId"] ?? "71");
        public static readonly short FeedBackGroupIdGuiYeuCauVB = short.Parse(ConfigurationManager.AppSettings["FeedBackGroupIdGuiYeuCauVB"] ?? "74");
        public static readonly short FieldDisplayTypeIdVbpq = short.Parse(ConfigurationManager.AppSettings["FieldDisplayTypeIdVbpq"] ?? "62");
        public static readonly short TcvnDisplaytypeId = short.Parse(ConfigurationManager.AppSettings["TcvnDisplaytypeId"] ?? "52");
        public static readonly short ViNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["ViNewsletterGroupId"] ?? "5");
        public static readonly short AllNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["AllNewsletterGroupId"] ?? "3");
        public static readonly short EnNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["EnNewsletterGroupId"] ?? "4");
        public static readonly short ViEnNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["ViEnNewsletterGroupId"] ?? "6");
        public static readonly short TemplateDocSendMail = short.Parse(ConfigurationManager.AppSettings["TemplateDocSendMail"] ?? "23");
        public static readonly short TemplateArticleSendLink = short.Parse(ConfigurationManager.AppSettings["TemplateArticleSendLink"] ?? "24");
        /// <summary>
        /// Menu dưới box Tra cứu văn bản
        /// </summary>
        public static readonly short MenuIdLeft = short.Parse(ConfigurationManager.AppSettings["MenuIdLeft"] ?? "211");

        /// <summary>
        /// DisplayTypeId lĩnh vực hiển thị của site
        /// </summary>
        public static readonly short FieldDisplaysDisplayTypeIdVbpq = short.Parse(ConfigurationManager.AppSettings["FieldDisplaysDisplayTypeIdVbpq"] ?? "62");
        public static readonly short DisplayTypeId_VBQuanTam = short.Parse(ConfigurationManager.AppSettings["DisplayTypeId_VBQuanTam"] ?? "81");
        public static readonly short DisplayTypeId_LuatDNXemNhieu = short.Parse(ConfigurationManager.AppSettings["DisplayTypeId_LuatDNXemNhieu"] ?? "82");
        #endregion

        #region int
        public static readonly int RowAmount3 = int.Parse(ConfigurationManager.AppSettings["RowAmount3"] ?? "3");
        public static readonly int RowAmount4 = int.Parse(ConfigurationManager.AppSettings["RowAmount3"] ?? "4");
        public static readonly int RowAmount5 = int.Parse(ConfigurationManager.AppSettings["RowAmount5"] ?? "5");
        public static readonly int RowAmount6 = int.Parse(ConfigurationManager.AppSettings["RowAmount6"] ?? "6");
        public static readonly int RowAmount7 = int.Parse(ConfigurationManager.AppSettings["RowAmount6"] ?? "7");
        public static readonly int RowAmount8 = int.Parse(ConfigurationManager.AppSettings["RowAmount8"] ?? "8");
        public static readonly int RowAmount10 = int.Parse(ConfigurationManager.AppSettings["RowAmount10"] ?? "10");
        public static readonly int RowAmount20 = int.Parse(ConfigurationManager.AppSettings["RowAmount20"] ?? "20");


        public static readonly int SeoByField = int.Parse(ConfigurationManager.AppSettings["SeoByField"] ?? "13");
        public static readonly int SeoByField1 = int.Parse(ConfigurationManager.AppSettings["SeoByField1"] ?? "46");
        public static readonly int SeoByField2 = int.Parse(ConfigurationManager.AppSettings["SeoByField2"] ?? "47");
        public static readonly int SeoByField3 = int.Parse(ConfigurationManager.AppSettings["SeoByField3"] ?? "48");
        public static readonly int SeoByField4 = int.Parse(ConfigurationManager.AppSettings["SeoByField4"] ?? "49");
        public static readonly int SeoByField5 = int.Parse(ConfigurationManager.AppSettings["SeoByField5"] ?? "50");
        public static readonly int SeoByField6 = int.Parse(ConfigurationManager.AppSettings["SeoByField6"] ?? "51");
        public static readonly int SeoByField8 = int.Parse(ConfigurationManager.AppSettings["SeoByField8"] ?? "52");


        public static readonly int DocIdMinIndex = int.Parse(ConfigurationManager.AppSettings["DocIdMinIndex"] ?? "162740");

        #endregion
    }
}