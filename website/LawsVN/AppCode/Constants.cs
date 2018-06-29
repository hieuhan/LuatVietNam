using System.Configuration;

namespace LawsVN.Library
{
    public static class Constants
    {
        #region SEO
        public static string WebsiteTitle = ConfigurationManager.AppSettings["WebsiteTitle"] ?? "";
        public static string WebsiteDescription = ConfigurationManager.AppSettings["WebsiteDescription"] ?? "";
        public static string WebsiteKeywords = ConfigurationManager.AppSettings["WebsiteKeywords"] ?? "";
        public static string WebsiteCanonical = ConfigurationManager.AppSettings["WebsiteCanonical"] ?? "";
        public static string SeoHeader = ConfigurationManager.AppSettings["SeoHeader"] ?? "";
        public static string WebsiteImage = ConfigurationManager.AppSettings["WebsiteImage"] ?? "";
        public static string NoImageUrl = ConfigurationManager.AppSettings["NoImageUrl"] ?? "";
        public static string NoImageUrl_Article = ConfigurationManager.AppSettings["NoImageUrl_Article"] ?? "";
        public static string NoImageUrl_File = ConfigurationManager.AppSettings["NoImageUrl_File"] ?? "";
        public static string NoAvatar = ConfigurationManager.AppSettings["NoAvatar"] ?? "/assets/images/avata-ad1.png";
        public static string MaleAvatar = ConfigurationManager.AppSettings["MaleAvatar"] ?? "/assets/images/avata-ad1.png";
        public static string FeMaleAvatar = ConfigurationManager.AppSettings["FeMaleAvatar"] ?? "/assets/images/avata-ad2.png";
        #endregion

        #region Configs

        #region bool

        public static bool IsRedirectOldLink = bool.Parse(ConfigurationManager.AppSettings["IsRedirectOldLink"] ?? "false"); 
        
        #endregion

        #region string

        public static string AppKey = ConfigurationManager.AppSettings["AppKey"] ?? "luatvietnam@$%#^&2017";
        public static string MediaUploadPath = ConfigurationManager.AppSettings["MEDIA_UPLOAD_PATH"] == null ? "Uploaded/Users/" : ConfigurationManager.AppSettings["MEDIA_UPLOAD_PATH"];
        public static readonly string FacebookAppId = ConfigurationManager.AppSettings["FacebookAppId"] ?? "1523947861021721"; //1618678111774339
        public static string EffectStatusMoiBanHanh = ConfigurationManager.AppSettings["EffectStatus_MoiBanHanh"] ?? "MoiBanHanh";
        public static string EffectStatusHetHieuLuc = ConfigurationManager.AppSettings["EffectStatus_HetHieuLuc"] ?? "HetHieuLuc";
        public static string EffectStatusSapCoHieuLuc = ConfigurationManager.AppSettings["EffectStatus_SapCoHieuLuc"] ?? "SapCoHieuLuc";
        public static string EffectStatusSapSuaDoi = ConfigurationManager.AppSettings["EffectStatus_SapSuaDoi"] ?? "SapSuaDoi";
        public static string EffectStatusSapHetHieuLuc = ConfigurationManager.AppSettings["EffectStatus_SapHetHieuLuc"] ?? "SapHetHieuLuc";
        public static string DangCapNhat = ConfigurationManager.AppSettings["DangCapNhat"] ?? "Đang cập nhật";
        public static string EXT_CONSTR = ConfigurationManager.AppSettings["EXT_CONSTR"] ?? "";
        public static string MEDIA_VIEWPATH = (ConfigurationManager.AppSettings["MEDIA_VIEWPATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_VIEWPATH"];
        public static string MEDIA_DOWNLOADPATH = (ConfigurationManager.AppSettings["MEDIA_DOWNLOADPATH"] == null) ? "" : ConfigurationManager.AppSettings["MEDIA_DOWNLOADPATH"];
        
        public static string MediaLawyersPath = (ConfigurationManager.AppSettings["MEDIA_LAWYERS_PATH"] == null) ? "Lawyers" : ConfigurationManager.AppSettings["MEDIA_LAWYERS_PATH"];
        public static string RolesFull = ConfigurationManager.AppSettings["RolesFull"] ?? "NVTC,NVTA,NVNC,TC,TA,NC";
        public static string RolesFullEng = ConfigurationManager.AppSettings["RolesFullEng"] ?? "NVTA,NVNC,TA,NC";
        public static string RolesFullVi = ConfigurationManager.AppSettings["RolesFullVi"] ?? "NVTC,NVNC,TC,NC";
        public static string RolesTC = ConfigurationManager.AppSettings["RolesTC"] ?? "NVTC,TC";
        public static string PAY_RESULT_URL = (ConfigurationManager.AppSettings["PAY_RESULT_URL"] == null) ? "http://luatvietnam.com/Pay/Result123Pay" : ConfigurationManager.AppSettings["PAY_RESULT_URL"];
        public static string WebsiteDomainEnglish = ConfigurationManager.AppSettings["WebsiteDomainEnglish"] ?? "http://english.luatvietnam.vn/";
        #endregion

        #region byte
        public static readonly byte SiteId = byte.Parse(ConfigurationManager.AppSettings["SiteId"] ?? "47");
        public static readonly byte ManualPaymentTypeId = byte.Parse(ConfigurationManager.AppSettings["ManualPaymentTypeId"] ?? "5");
        public static readonly byte PaymentTypeIdSystem = byte.Parse(ConfigurationManager.AppSettings["PaymentTypeIdSystem"] ?? "10");
        public static readonly byte DocGroupIdVbpq = byte.Parse(ConfigurationManager.AppSettings["DocGroupId_VBPQ"] ?? "1");
        public static readonly byte DocGroupIdUbnd = byte.Parse(ConfigurationManager.AppSettings["DocGroupId_UBND"] ?? "2");
        public static readonly byte DocGroupIdCongVan = byte.Parse(ConfigurationManager.AppSettings["DocGroupId_CongVan"] ?? "6");
        public static readonly byte DocGroupIdTcvn = byte.Parse(ConfigurationManager.AppSettings["DocGroupId_TCVN"] ?? "3");
        public static readonly byte DocGroupIdTthc = byte.Parse(ConfigurationManager.AppSettings["DocGroupIdTthc"] ?? "4");
        public static readonly byte DocGroupIdVbhn = byte.Parse(ConfigurationManager.AppSettings["DocGroupId_VBHN"] ?? "5");
        public static readonly byte DocGroupIdVbkhongconoidung = byte.Parse(ConfigurationManager.AppSettings["DocGroupIdVbkhongconoidung"] ?? "8");
        public static readonly byte Truncate20 = byte.Parse(ConfigurationManager.AppSettings["Truncate20"] ?? "20");
        public static readonly byte Truncate90 = byte.Parse(ConfigurationManager.AppSettings["Truncate90"] ?? "90");
        public static readonly byte SysMesssageTypeIdDeleteSuccessful = byte.Parse(ConfigurationManager.AppSettings["SysMesssageTypeId_Delete_Successful"] ?? "1");
        public static readonly byte SysMesssageTypeIdInsertOrUpdateSuccessful = byte.Parse(ConfigurationManager.AppSettings["SysMesssageTypeId_InsertOrUpdate_Successful"] ?? "2");
        public static readonly byte DocTypesTieuChuanVn = byte.Parse(ConfigurationManager.AppSettings["DocTypes_TIEUCHUANVN"] ?? "61");
        public static readonly byte OrganTieuChuanVn = byte.Parse(ConfigurationManager.AppSettings["OrganTieuChuanVn"] ?? "52");
        public static readonly byte DocTypesIdCongVan = byte.Parse(ConfigurationManager.AppSettings["DocTypesIdCongVan"] ?? "3");
        public static readonly byte DocTypesIdVbhn = byte.Parse(ConfigurationManager.AppSettings["DocTypesIdVbhn"] ?? "59");
        public static readonly byte DocTypesQuyChuanVn = byte.Parse(ConfigurationManager.AppSettings["DocTypes_QUY_CHUAN_VN"] ?? "62");
        public static readonly byte DocTypesTieuChuanXdvn = byte.Parse(ConfigurationManager.AppSettings["DocTypes_TIEU_CHUAN_XDVN"] ?? "64");
        public static readonly byte DocTypesTieuChuanNganh = byte.Parse(ConfigurationManager.AppSettings["DocTypes_TIEU_CHUAN_NGANH"] ?? "63");
        public static readonly byte CongVanHaiQuanMoi = byte.Parse(ConfigurationManager.AppSettings["CongVanHaiQuanMoi"] ?? "70");
        public static readonly byte CongVanThueMoi = byte.Parse(ConfigurationManager.AppSettings["CongVanThueMoi"] ?? "13");
        public static readonly byte CongVanMoi = byte.Parse(ConfigurationManager.AppSettings["CongVanMoi"] ?? "12");

        public static readonly byte RelateTypeGroupHieuLuc = byte.Parse(ConfigurationManager.AppSettings["RelateTypeGroup_HieuLuc"] ?? "1");
        public static readonly byte RelateTypeGroupNoiDung = byte.Parse(ConfigurationManager.AppSettings["RelateTypeGroup_NoiDung"] ?? "2");
        public static readonly byte RegistTypeIdVbqt = byte.Parse(ConfigurationManager.AppSettings["RegistTypeId_VBQT"] ?? "1");
        public static readonly byte RegistTypeIdTheoDoiHieuLuc = byte.Parse(ConfigurationManager.AppSettings["RegistTypeId_TheoDoiHieuLuc"] ?? "2");
        public static readonly byte MessageTypeIdInbox = byte.Parse(ConfigurationManager.AppSettings["MessageTypeId_Inbox"] ?? "1");
        public static readonly byte MessageTypeIdSave = byte.Parse(ConfigurationManager.AppSettings["MessageTypeId_Save"] ?? "4");

        public static readonly byte ActionTypeIdXemThuocTinh = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemThuocTinh"] ?? "22");
        public static readonly byte ActionTypeIdXemLuocDo = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemLuocDo"] ?? "23");
        public static readonly byte ActionTypeIdXemHieuLuc = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemHieuLuc"] ?? "24");
        public static readonly byte ActionTypeIdXemLienQuan = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemLienQuan"] ?? "25");
        public static readonly byte ActionTypeIdXemNoiDung = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemNoiDung"] ?? "29");
        public static readonly byte ActionTypeIdXemNoiDungTiengAnh = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemNoiDungTiengAnh"] ?? "26");
        public static readonly byte ActionTypeIdXemTaiVe = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemTaiVe"] ?? "27");
        public static readonly byte ActionTypeIdXemTomTat = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemTomTat"] ?? "28");
        public static readonly byte TransactionTypeIdDangKy = byte.Parse(ConfigurationManager.AppSettings["TransactionTypeIdDangKy"] ?? "1");
        public static readonly byte TransactionTypeIdGiaHan = byte.Parse(ConfigurationManager.AppSettings["TransactionTypeIdGiaHan"] ?? "2");
        public static readonly byte TransactionTypeIdChuyenDoi = byte.Parse(ConfigurationManager.AppSettings["TransactionTypeIdChuyenDoi"] ?? "11");
        public static readonly byte TransactionStatusIdSuccess = byte.Parse(ConfigurationManager.AppSettings["TransactionStatusIdSuccess"] ?? "1");
        public static readonly byte TransactionStatusIdUnSuccess = byte.Parse(ConfigurationManager.AppSettings["TransactionStatusIdUnSuccess"] ?? "2");
        public static readonly byte TransactionStatusIdPending = byte.Parse(ConfigurationManager.AppSettings["TransactionStatusIdPending"] ?? "3");
        public static readonly byte FileTypeIdVbThamKhao = byte.Parse(ConfigurationManager.AppSettings["FileTypeIdVbThamKhao"] ?? "3");
        public static readonly byte HasReadGetAllMessages = byte.Parse(ConfigurationManager.AppSettings["HasReadGetAllMessages"] ?? "10");
        public static readonly byte ReviewStatusIdDaDuyet = byte.Parse(ConfigurationManager.AppSettings["ReviewStatusIdDaDuyet"] ?? "2");
        #endregion

        #region short
        public static readonly short ViNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["VI_NewsletterGroupId"] ?? "5");
        public static readonly short EnNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["EN_NewsletterGroupId"] ?? "4");
        public static readonly short ViEnNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["VI_EN_NewsletterGroupId"] ?? "6");
        public static readonly short CateIdBanTinLuat = short.Parse(ConfigurationManager.AppSettings["CateIdBanTinLuat"] ?? "531");
        public static readonly short CateIdTinVan = short.Parse(ConfigurationManager.AppSettings["CateID_TinVan"] ?? "502");// văn bản chính sách mới
        public static readonly short CateIdHotNewsLaw = short.Parse(ConfigurationManager.AppSettings["CateIdHotNewsLaw"] ?? "502");
        public static readonly short CateIdNewsLaw = short.Parse(ConfigurationManager.AppSettings["cateIdNewsLaw"] ?? "220");// điểm tin văn bản mới
        public static readonly short CateIdTieuDiem = short.Parse(ConfigurationManager.AppSettings["CateIdTieuDiem"] ?? "216");
        public static readonly short CateIdTinVbMoi = short.Parse(ConfigurationManager.AppSettings["CateIdTinVbMoi"] ?? "186");
        public static readonly short CateIdBanTinHieuLuc = short.Parse(ConfigurationManager.AppSettings["CateIdBanTinHieuLuc"] ?? "221");
        public static readonly short CateIdDiemTinChinhSachMoi = short.Parse(ConfigurationManager.AppSettings["CateIdDiemTinChinhSachMoi"] ?? "685");
        public static readonly short CateIdSlide = short.Parse(ConfigurationManager.AppSettings["CateID_Slide"] ?? "515");
        public static readonly short CateIdTinPl = short.Parse(ConfigurationManager.AppSettings["CateID_TinPL"] ?? "230");
        public static readonly short CateIdDiemTinVb = short.Parse(ConfigurationManager.AppSettings["CateIdDiemTinVb"] ?? "220");
        public static readonly short CateIdLuatCanBiet = short.Parse(ConfigurationManager.AppSettings["CateIdLuatCanBiet"] ?? "221");
        public static readonly short CateIdHuongDan = short.Parse(ConfigurationManager.AppSettings["CateIdHuongDan"] ?? "218");
        public static readonly short CateIdCapNhatHangTuan = short.Parse(ConfigurationManager.AppSettings["CateIdCapNhatHangTuan"] ?? "219");
        public static readonly short ServiceFree = short.Parse(ConfigurationManager.AppSettings["ServiceFree"] ?? "18");
        public static readonly short ServiceIdDungThu15Ngay = short.Parse(ConfigurationManager.AppSettings["ServiceIdDungThu15Ngay"] ?? "14");
        public static readonly short ServiceIdTraCuuNangCao = short.Parse(ConfigurationManager.AppSettings["ServiceIdTraCuuNangCao"] ?? "23");
        public static readonly short ServiceIdTraCuuTiengAnh = short.Parse(ConfigurationManager.AppSettings["ServiceIdTraCuuTiengAnh"] ?? "17");
        public static readonly short ServiceIdTraCuuMienPhi = short.Parse(ConfigurationManager.AppSettings["ServiceIdTraCuuMienPhi"] ?? "16");
        public static readonly short ServiceIdTraCuuTieuChuan = short.Parse(ConfigurationManager.AppSettings["ServiceIdTraCuuTieuChuan"] ?? "15");
        public static readonly short ServicePackageIdTrial15Days = short.Parse(ConfigurationManager.AppSettings["ServicePackageIdTrial15Days"] ?? "18");
        public static readonly short ServicePackageIdTheCaoLuat = short.Parse(ConfigurationManager.AppSettings["ServicePackageIdTheCaoLuat"] ?? "149");
        public static readonly short FieldDisplayTypeIdVbpq = short.Parse(ConfigurationManager.AppSettings["FieldDisplayTypeIdVbpq"] ?? "62");
        public static readonly short FieldDisplayTypeIdTcvn = short.Parse(ConfigurationManager.AppSettings["FieldDisplayTypeIdTcvn"] ?? "52");
        public static readonly short FieldDisplayTypeIdTthc = short.Parse(ConfigurationManager.AppSettings["FieldDisplayTypeIdTthc"] ?? "53");
        public static readonly short FieldsDisplayTypeIdVbpq = short.Parse(ConfigurationManager.AppSettings["FieldsDisplayTypeIdVbpq"] ?? "68");
        public static readonly short FieldDisplayTypeIdVbhn = short.Parse(ConfigurationManager.AppSettings["FieldDisplayTypeIdVbhn"] ?? "54");
        public static readonly short FieldDisplayTypeIdCongVan = short.Parse(ConfigurationManager.AppSettings["FieldDisplayTypeIdCongVan"] ?? "67");
        public static readonly short DocTypeIdDisplayTypeIdVbpq = short.Parse(ConfigurationManager.AppSettings["DocTypeIdDisplayTypeIdVbpq"] ?? "62");
        public static readonly short FieldDisplayTypeIdUbnd = short.Parse(ConfigurationManager.AppSettings["FieldDisplayTypeIdUbnd"] ?? "5");
        public static readonly short DisplayTypeIdUbndXemnhieu = byte.Parse(ConfigurationManager.AppSettings["DisplayTypeIdUbndXemnhieu"] ?? "69");
        public static readonly short DisplayTypeIdVbTwLienquan = byte.Parse(ConfigurationManager.AppSettings["DisplayTypeIdVbTwLienquan"] ?? "71");
        public static readonly short DisplayTypeIdByField = byte.Parse(ConfigurationManager.AppSettings["DisplayTypeIdByField"] ?? "79");
        public static readonly short DisplayTypeIdByDocsHot = byte.Parse(ConfigurationManager.AppSettings["DisplayTypeIdByDocsHot"] ?? "81");
        public static readonly short MenuIdTop = short.Parse(ConfigurationManager.AppSettings["MenuIdTop"] ?? "18");
        public static readonly short MenuIdHeader = short.Parse(ConfigurationManager.AppSettings["MenuIdHeader"] ?? "109");
        //public static readonly short MenuIdBottomHomePage = short.Parse(ConfigurationManager.AppSettings["MenuIdBottomHomePage"] ?? "20");
        public static readonly short MenuIdBottom = short.Parse(ConfigurationManager.AppSettings["MenuIdBottom"] ?? "20");
        public static readonly short MenuIdMobile = short.Parse(ConfigurationManager.AppSettings["MenuIdMobile"] ?? "92");
        //public static readonly short MenuIdBottomMobileHomePage = short.Parse(ConfigurationManager.AppSettings["MenuIdBottomMobileHomePage"] ?? "91");
        public static readonly short MenuIdBottomMobile = short.Parse(ConfigurationManager.AppSettings["MenuIdBottomMobile"] ?? "91");
        public static readonly short MenuIdTaisaochonLuatvietnam = short.Parse(ConfigurationManager.AppSettings["MenuIdTaisaochonLuatvietnam"] ?? "79");
        public static readonly short MenuIdLeft = short.Parse(ConfigurationManager.AppSettings["MenuIdLeft"] ?? "80");
        public static readonly short MenuIdMyLuatVn = short.Parse(ConfigurationManager.AppSettings["MenuIdMyLuatVN"] ?? "81");
        public static readonly short MenuItemIdTatCaLinhVuc = short.Parse(ConfigurationManager.AppSettings["MenuItemIdTatCaLinhVuc"] ?? "809");
        public static readonly short MenuIdSitemap = short.Parse(ConfigurationManager.AppSettings["MenuIdSitemap"] ?? "93");
        public static readonly short MenuIdDocDetail = short.Parse(ConfigurationManager.AppSettings["MenuIdDocDetail"] ?? "95");
        public static readonly short TcvnDisplaytypeId = short.Parse(ConfigurationManager.AppSettings["TCVN_DISPLAYTYPE_ID"] ?? "52");
        public static readonly short FieldThue = short.Parse(ConfigurationManager.AppSettings["FieldThue"] ?? "4");
        public static readonly short FieldHaiQuan = short.Parse(ConfigurationManager.AppSettings["FieldHaiQuan"] ?? "21");
        public static readonly short MessageTemplateIdForgotPassword = short.Parse(ConfigurationManager.AppSettings["MessageTemplateIdForgotPassword"] ?? "14");
        public static readonly short MessageTemplateIdOnlinePaymentOrder = short.Parse(ConfigurationManager.AppSettings["MessageTemplateIdOnlinePaymentOrder"] ?? "15");
        public static readonly short MessageTemplateIdDocSendMail = short.Parse(ConfigurationManager.AppSettings["MessageTemplateIdDocSendMail"] ?? "16");
        public static readonly short FeedBackGroupId = short.Parse(ConfigurationManager.AppSettings["FeedBackGroupId"] ?? "71");
        public static readonly short FeedBackGroupIdSendQuestion = short.Parse(ConfigurationManager.AppSettings["FeedBackGroupIdSendQuestion"] ?? "73");
        public static readonly short FeedBackGroupIdGopY = short.Parse(ConfigurationManager.AppSettings["FeedBackGroupIdGopY"] ?? "72");
        public static short[] ServiceId_NghiepVu = new short[] { 24,25,26 };
        #endregion

        #region int
        public static readonly int RowAmount2 = int.Parse(ConfigurationManager.AppSettings["RowAmount2"] ?? "2");
        public static readonly int RowAmount3 = int.Parse(ConfigurationManager.AppSettings["RowAmount3"] ?? "3");
        public static readonly int RowAmount4 = int.Parse(ConfigurationManager.AppSettings["RowAmount4"] ?? "4");
        public static readonly int RowAmount5 = int.Parse(ConfigurationManager.AppSettings["RowAmount5"] ?? "5");
        public static readonly int RowAmount6 = int.Parse(ConfigurationManager.AppSettings["RowAmount6"] ?? "6");
        public static readonly int RowAmount8 = int.Parse(ConfigurationManager.AppSettings["RowAmount8"] ?? "8");
        public static readonly int RowAmount10 = int.Parse(ConfigurationManager.AppSettings["RowAmount10"] ?? "10");
        public static readonly int RowAmount15 = int.Parse(ConfigurationManager.AppSettings["RowAmount15"] ?? "15");
        public static readonly int RowAmount20 = int.Parse(ConfigurationManager.AppSettings["RowAmount20"] ?? "20");
        public static readonly int RowAmount100 = int.Parse(ConfigurationManager.AppSettings["RowAmount100"] ?? "100");
        public static readonly int RowAmount300 = int.Parse(ConfigurationManager.AppSettings["RowAmount300"] ?? "300");
        public static readonly int RowAmount500 = int.Parse(ConfigurationManager.AppSettings["RowAmount500"] ?? "500");
        public static readonly int RowAmount1000 = int.Parse(ConfigurationManager.AppSettings["RowAmount1000"] ?? "1000");
        public static readonly int RowAmount5000 = int.Parse(ConfigurationManager.AppSettings["RowAmount5000"] ?? "5000");
        public static readonly int RowAmountVblqDetail = int.Parse(ConfigurationManager.AppSettings["RowAmount_VBLQ_Detail"] ?? "7");
        public static readonly int QuyUocBaoMatArticleId = int.Parse(ConfigurationManager.AppSettings["QuyUocBaoMatArticleId"] ?? "12078");

        public static readonly int TinVanCategoryId = int.Parse(ConfigurationManager.AppSettings["TinVanCategoryId"] ?? "502");
        public static readonly int LawNewsCategoryId = int.Parse(ConfigurationManager.AppSettings["LawNewsCategoryId"] ?? "230");
        public static readonly int DiemTinVbMoiCategoryId = int.Parse(ConfigurationManager.AppSettings["DiemTinVBMoiCategoryId"] ?? "220");
        public static readonly int ArticleIdSoSanhQl = int.Parse(ConfigurationManager.AppSettings["ArticleID_SoSanhQL"] ?? "12030");
        public static readonly int ArticleIdGioiThieu = int.Parse(ConfigurationManager.AppSettings["ArticleID_GioiThieu"] ?? "12075");

        public static readonly int RowAmountSlide = int.Parse(ConfigurationManager.AppSettings["RowAmountSlide"] ?? "3");
        public static readonly int RowAmountMostView = int.Parse(ConfigurationManager.AppSettings["RowAmountMostView"] ?? "5");
        public static readonly int MenuIdBottomRight = int.Parse(ConfigurationManager.AppSettings["MenuIdBottomRight"] ?? "82");

        public static readonly int SeoByField = int.Parse(ConfigurationManager.AppSettings["SeoByField"] ?? "13");
        public static readonly int SeoByField1 = int.Parse(ConfigurationManager.AppSettings["SeoByField1"] ?? "46");
        public static readonly int SeoByField2 = int.Parse(ConfigurationManager.AppSettings["SeoByField2"] ?? "47");
        public static readonly int SeoByField3 = int.Parse(ConfigurationManager.AppSettings["SeoByField3"] ?? "48");
        public static readonly int SeoByField4 = int.Parse(ConfigurationManager.AppSettings["SeoByField4"] ?? "49");
        public static readonly int SeoByField5 = int.Parse(ConfigurationManager.AppSettings["SeoByField5"] ?? "50");
        public static readonly int SeoByField6 = int.Parse(ConfigurationManager.AppSettings["SeoByField6"] ?? "51");
        public static readonly int SeoByField8 = int.Parse(ConfigurationManager.AppSettings["SeoByField8"] ?? "52");
        public static readonly int SeoByProvince = int.Parse(ConfigurationManager.AppSettings["SeoByProvince"] ?? "56");
        #endregion

        #endregion
    }
}