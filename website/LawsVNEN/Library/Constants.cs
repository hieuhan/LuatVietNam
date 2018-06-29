using System.Configuration;

namespace LawsVNEN.AppCode
{
    public static class Constants
    {
        #region SEO
        public static string WebsiteTitle = ConfigurationManager.AppSettings["WebsiteTitle"] ?? "";
        public static string WebsiteDescription = ConfigurationManager.AppSettings["WebsiteDescription"] ?? "";
        public static string WebsiteKeywords = ConfigurationManager.AppSettings["WebsiteKeywords"] ?? "";
        public static string WebsiteCanonical = ConfigurationManager.AppSettings["WebsiteCanonical"] ?? "";
        public static string WebsiteImage = ConfigurationManager.AppSettings["WebsiteImage"] ?? "";
        public static string NoImageUrl = ConfigurationManager.AppSettings["NoImageUrl"] ?? "";
        public static string NoImageUrl_Article = ConfigurationManager.AppSettings["NoImageUrl_Article"] ?? "";
        public static string NoImageUrl_File = ConfigurationManager.AppSettings["NoImageUrl_File"] ?? "";
        public static string NoAvatar = ConfigurationManager.AppSettings["NoAvatar"] ?? "/assets/images/150x180.png";
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
        public static string PAY_RESULT_URL = (ConfigurationManager.AppSettings["PAY_RESULT_URL"] == null) ? "http://luatvietnam.com/Pay/Result123Pay" : ConfigurationManager.AppSettings["PAY_RESULT_URL"];
        public static string Link_DownloadVN = (ConfigurationManager.AppSettings["Link_DownloadVN"] == null) ? "http://cms.luatvietnam.vn/uploaded/Others/2017/10/27/vn_2710174305.pdf" : ConfigurationManager.AppSettings["Link_DownloadVN"];
        public static string Link_DownloadEN = (ConfigurationManager.AppSettings["Link_DownloadEN"] == null) ? "http://cms.luatvietnam.vn/uploaded/Others/2017/10/27/en_2710174256.pdf" : ConfigurationManager.AppSettings["Link_DownloadEN"];
        public static string RolesFull = ConfigurationManager.AppSettings["RolesFull"] ?? "NVTC,NVTA,NVNC,TC,TA,NC";
        public static string RolesFullEng = ConfigurationManager.AppSettings["RolesFullEng"] ?? "NVTA,NVNC,TA,NC";
        public static string WebsiteDomainVi = ConfigurationManager.AppSettings["WebsiteDomainVi"] ?? "http://luatvietnam.vn/";
        #endregion

        #region byte
        public static readonly byte SiteId_TiengViet = byte.Parse(ConfigurationManager.AppSettings["SiteId_TiengViet"] ?? "1");
        public static readonly byte SiteId = byte.Parse(ConfigurationManager.AppSettings["SiteId"] ?? "2");
        public static readonly byte SiteId_AnhViet = byte.Parse(ConfigurationManager.AppSettings["SiteId_AnhViet"] ?? "3");
        public static readonly byte ManualPaymentTypeId = byte.Parse(ConfigurationManager.AppSettings["ManualPaymentTypeId"] ?? "5");
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
        public static readonly byte DocTypesIdCongVan = byte.Parse(ConfigurationManager.AppSettings["DocTypesIdCongVan"] ?? "3");
        public static readonly byte DocTypesIdVbhn = byte.Parse(ConfigurationManager.AppSettings["DocTypesIdVbhn"] ?? "59");
        public static readonly byte DocTypesQuyChuanVn = byte.Parse(ConfigurationManager.AppSettings["DocTypes_QUY_CHUAN_VN"] ?? "62");
        public static readonly byte DocTypesTieuChuanXdvn = byte.Parse(ConfigurationManager.AppSettings["DocTypes_TIEU_CHUAN_XDVN"] ?? "64");
        public static readonly byte DocTypesTieuChuanNganh = byte.Parse(ConfigurationManager.AppSettings["DocTypes_TIEU_CHUAN_NGANH"] ?? "63");
        public static readonly byte CongVanHaiQuanMoi = byte.Parse(ConfigurationManager.AppSettings["CongVanHaiQuanMoi"] ?? "70");
        public static readonly byte CongVanThueMoi = byte.Parse(ConfigurationManager.AppSettings["CongVanThueMoi"] ?? "13");

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
        public static readonly byte ActionTypeIdXemNoiDungTiengViet = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemNoiDungTiengViet"] ?? "30");
        public static readonly byte ActionTypeIdXemTaiVe = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemTaiVe"] ?? "27");
        public static readonly byte ActionTypeIdXemTomTat = byte.Parse(ConfigurationManager.AppSettings["ActionTypeId_XemTomTat"] ?? "28");
        public static readonly byte TransactionTypeIdDangKy = byte.Parse(ConfigurationManager.AppSettings["TransactionTypeIdDangKy"] ?? "1");
        public static readonly byte TransactionTypeIdGiaHan = byte.Parse(ConfigurationManager.AppSettings["TransactionTypeIdGiaHan"] ?? "2");
        public static readonly byte TransactionTypeIdChuyenDoi = byte.Parse(ConfigurationManager.AppSettings["TransactionTypeIdChuyenDoi"] ?? "11");
        public static readonly byte TransactionStatusIdSuccess = byte.Parse(ConfigurationManager.AppSettings["TransactionStatusIdSuccess"] ?? "1");
        public static readonly byte TransactionStatusIdUnSuccess = byte.Parse(ConfigurationManager.AppSettings["TransactionStatusIdUnSuccess"] ?? "2");
        public static readonly byte TransactionStatusIdPending = byte.Parse(ConfigurationManager.AppSettings["TransactionStatusIdPending"] ?? "3");
        public static readonly byte FileTypeIdVbChinhThucPDF = byte.Parse(ConfigurationManager.AppSettings["FileTypeIdVbChinhThucPDF"] ?? "2");
        public static readonly byte FileTypeIdVbThamKhao = byte.Parse(ConfigurationManager.AppSettings["FileTypeIdVbThamKhao"] ?? "3");
        #endregion

        #region short
        public static readonly short ViNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["VI_NewsletterGroupId"] ?? "5");
        public static readonly short EnNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["EN_NewsletterGroupId"] ?? "4");
        public static readonly short ViEnNewsletterGroupId = short.Parse(ConfigurationManager.AppSettings["VI_EN_NewsletterGroupId"] ?? "6");
        public static readonly short CateIdTinVan = short.Parse(ConfigurationManager.AppSettings["CateID_TinVan"] ?? "553");// văn bản chính sách mới
        public static readonly short CateIdHotNewsLaw = short.Parse(ConfigurationManager.AppSettings["CateIdHotNewsLaw"] ?? "502");
        public static readonly short CateIdNewsLaw = short.Parse(ConfigurationManager.AppSettings["cateIdNewsLaw"] ?? "220");// điểm tin văn bản mới
        public static readonly short CateIdTieuDiem = short.Parse(ConfigurationManager.AppSettings["CateIdTieuDiem"] ?? "216");
        public static readonly short CateIdBanTinLuatVN = short.Parse(ConfigurationManager.AppSettings["CateIdBanTinLuatVN"] ?? "553");
        public static readonly short CateIdBanTinLuatVN_AnhViet = short.Parse(ConfigurationManager.AppSettings["CateIdBanTinLuatVN_AnhViet"] ?? "554");
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
        public static readonly short ServicePackageIdAdvanced = short.Parse(ConfigurationManager.AppSettings["ServicePackageIdAdvanced"] ?? "16");
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
        public static readonly short MenuIdTop = short.Parse(ConfigurationManager.AppSettings["MenuIdTop"] ?? "83");
        public static readonly short MenuIdTop_AnhViet = short.Parse(ConfigurationManager.AppSettings["MenuIdTop_AnhViet"] ?? "86");
        public static readonly short MenuIdBottom = short.Parse(ConfigurationManager.AppSettings["MenuIdBottom"] ?? "85");
        public static readonly short MenuIdBottom_AnhViet = short.Parse(ConfigurationManager.AppSettings["MenuIdBottom_AnhViet"] ?? "88");
        public static readonly short MenuIdLeftTop = short.Parse(ConfigurationManager.AppSettings["MenuIdTop"] ?? "90");
        public static readonly short MenuIdLeftTop_AnhViet = short.Parse(ConfigurationManager.AppSettings["MenuIdLeftTop_AnhViet"] ?? "87");
        public static readonly short MenuIdLeftBottom = short.Parse(ConfigurationManager.AppSettings["MenuIdBottom"] ?? "84");
        public static readonly short MenuIdLeftBottom_AnhViet = short.Parse(ConfigurationManager.AppSettings["MenuIdLeftBottom_AnhViet"] ?? "89");
        public static readonly short MenuIdTaisaochonLuatvietnam = short.Parse(ConfigurationManager.AppSettings["MenuIdTaisaochonLuatvietnam"] ?? "79");
        public static readonly short MenuIdLeft = short.Parse(ConfigurationManager.AppSettings["MenuIdLeft"] ?? "80");
        public static readonly short MenuIdMyLuatVn = short.Parse(ConfigurationManager.AppSettings["MenuIdMyLuatVN"] ?? "81");
        public static readonly short TcvnDisplaytypeId = short.Parse(ConfigurationManager.AppSettings["TCVN_DISPLAYTYPE_ID"] ?? "52");
        public static readonly short FieldThue = short.Parse(ConfigurationManager.AppSettings["FieldThue"] ?? "4");
        public static readonly short FieldHaiQuan = short.Parse(ConfigurationManager.AppSettings["FieldHaiQuan"] ?? "21");
        public static readonly short MessageTemplateIdForgotPassword = short.Parse(ConfigurationManager.AppSettings["MessageTemplateIdForgotPassword"] ?? "14");
        public static readonly short MessageTemplateIdDocSendMail = short.Parse(ConfigurationManager.AppSettings["MessageTemplateIdDocSendMail"] ?? "18");
        public static readonly short ServicePackageIdTheCaoLuat = short.Parse(ConfigurationManager.AppSettings["ServicePackageIdTheCaoLuat"] ?? "149");
        public static short[] ServiceId_NghiepVu = new short[] { 24, 25, 26 };
        #endregion

        #region int
        public static readonly int RowAmount3 = int.Parse(ConfigurationManager.AppSettings["RowAmount3"] ?? "3");
        public static readonly int RowAmount4 = int.Parse(ConfigurationManager.AppSettings["RowAmount4"] ?? "4");
        public static readonly int RowAmount5 = int.Parse(ConfigurationManager.AppSettings["RowAmount5"] ?? "5");
        public static readonly int RowAmount10 = int.Parse(ConfigurationManager.AppSettings["RowAmount10"] ?? "10");
        public static readonly int RowAmount15 = int.Parse(ConfigurationManager.AppSettings["RowAmount15"] ?? "15");
        public static readonly int RowAmount20 = int.Parse(ConfigurationManager.AppSettings["RowAmount20"] ?? "20");
        public static readonly int RowAmount100 = int.Parse(ConfigurationManager.AppSettings["RowAmount100"] ?? "100");
        public static readonly int RowAmountVblqDetail = int.Parse(ConfigurationManager.AppSettings["RowAmount_VBLQ_Detail"] ?? "7");
        public static readonly int QuyUocBaoMatArticleId = int.Parse(ConfigurationManager.AppSettings["QuyUocBaoMatArticleId"] ?? "12078");
        public static readonly int QuyUocBaoMatArticleIdEN = int.Parse(ConfigurationManager.AppSettings["QuyUocBaoMatArticleIdEN"] ?? "12130");
        public static readonly int QuyUocBaoMatArticleIdVNI = int.Parse(ConfigurationManager.AppSettings["QuyUocBaoMatArticleIdVNI"] ?? "12126");
        public static readonly int IntrorductionForPaymentVNI = int.Parse(ConfigurationManager.AppSettings["IntrorductionForPaymentVNI"] ?? "12184");
        public static readonly int IntrorductionForPaymentEN = int.Parse(ConfigurationManager.AppSettings["IntrorductionForPaymentEN"] ?? "12183");

        public static readonly int TinVanCategoryId = int.Parse(ConfigurationManager.AppSettings["TinVanCategoryId"] ?? "502");
        public static readonly int LawNewsCategoryId = int.Parse(ConfigurationManager.AppSettings["LawNewsCategoryId"] ?? "230");
        public static readonly int DiemTinVbMoiCategoryId = int.Parse(ConfigurationManager.AppSettings["DiemTinVBMoiCategoryId"] ?? "220");
        public static readonly int ArticleIdSoSanhQl = int.Parse(ConfigurationManager.AppSettings["ArticleID_SoSanhQL"] ?? "12030");
        public static readonly int ArticleIdGioiThieu = int.Parse(ConfigurationManager.AppSettings["ArticleID_GioiThieu"] ?? "12075");

        public static readonly int RowAmountSlide = int.Parse(ConfigurationManager.AppSettings["RowAmountSlide"] ?? "3");
        public static readonly int RowAmountMostView = int.Parse(ConfigurationManager.AppSettings["RowAmountMostView"] ?? "5");
        public static readonly int MenuIdBottomRight = int.Parse(ConfigurationManager.AppSettings["MenuIdBottomRight"] ?? "82");

        public static readonly int DisplayTypeIdVBMoi = int.Parse(ConfigurationManager.AppSettings["DisplayTypeIdVBMoi"] ?? "72");
        public static readonly int DisplayTypeIdCongBaoPDF = int.Parse(ConfigurationManager.AppSettings["DisplayTypeIdCongBaoPDF"] ?? "73");
        public static readonly int DisplayTypeIdCongBaoDOC = int.Parse(ConfigurationManager.AppSettings["DisplayTypeIdCongBaoDOC"] ?? "74");
        
        #endregion

        #endregion
    }
}