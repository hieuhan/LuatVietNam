using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using ICSoft.LawDocsLib;
using ICSoft.CMSViewLib;
using LawsVN.Library;
using ICSoft.CMSLib;
using LawsVN.App_GlobalResources;

namespace LawsVN.Models
{
    public class LawerViewModel : ViewModelBase
    {
        private List<Fields> _listFieldsLawer;
        private List<Provinces> _listAllProvinces;
        public LawersView mLawersView { get; set; }

        public List<Provinces> ListProvinces { get; set; }

        public IEnumerable<ProvinceCountModel> ListProvinceCount { get; set; }

        public PartialPaginationAjax PartialPaginationAjax { get; set; }

        public string LawerKeywords { get; set; }

        // danh sách lọc
        public List<Fields> ListFieldsLawer
        {
            get { return !_listFieldsLawer.HasValue() ? DropDownListHelpers.GetAllFields() : _listFieldsLawer; }
            set { _listFieldsLawer = value; }
        }

        //tinh thanh pho
        public List<Provinces> ListAllProvinces
        {
            get { return !_listAllProvinces.HasValue() ? DropDownListHelpers.GetAllProvinces() : _listAllProvinces; }
            set { _listAllProvinces = value; }
        }
    }

    public class LawerInsertModel : ViewModelBase
    {
        public static HttpSessionState LawsVnSession
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    throw new ApplicationException(Resource.NoHttpContext);
                }
                return HttpContext.Current.Session;
            }
        }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "{0} (*)")]
        public string FullName { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "{0} (*)")]
        public string Address { get; set; }

        [Display(Name = "Địa chỉ công tác")]
        [Required(ErrorMessage = "{0} (*)")]
        public string LawOfficeName { get; set; }

        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Di động")]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage =
            "Số điện thoại không đúng !")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} (*)")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-zA-Z0-9_\.]+@[-a-zA-Z0-9_]+\.[-a-zA-Z0-9_]+(\.[-a-zA-Z0-9_]+)*", ErrorMessage =
            "Địa chỉ Email không đúng !")]
        public string Email { get; set; }

        public short ProvinceId { get; set; }

        [Display(Name = "Kinh nghiệm")]
        [Required(ErrorMessage = "{0} (*)")]
        public string Experience { get; set; }

        [Display(Name = "Học vấn")]
        [Required(ErrorMessage = "{0} (*)")]
        public string Education { get; set; }

        [Display(Name = "Giới thiệu")]
        [Required(ErrorMessage = "{0} (*)")]
        public string Content { get; set; }

        public string Avatar { get; set; }

        [Display(Name = "Quy ước sử dụng của Luật Việt Nam")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Vui lòng chọn đồng ý với {0}")]
        public bool TermsAndConditions { get; set; }

        [Display(Name = "Mã an toàn")]
        public string LawerInsertCaptcha
        {
            get
            {
                return LawsVnSession["Captcha_LawerInsert"] == null ? default(string) : LawsVnSession["Captcha_LawerInsert"].ToString();
            }
            set
            {
                LawsVnSession["Captcha_LawerInsert"] = value;
            }
        }

        [Display(Name = "Mã an toàn")]
        [Required(ErrorMessage = "{0} (*)")]
        [Compare("LawerInsertCaptcha", ErrorMessage = "{0} không đúng.")]
        public string LawerInsertCaptchaCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PleaseAelectTheFieldOfActivityOfTheLawyer")]
        [MultiCheckBoxRequired(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PleaseAelectTheFieldOfActivityOfTheLawyer")]
        public short[] FieldId { get; set; }

        [Display(Name = "Ngày sinh")]
        [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
        public string DateOfBirth { get; set; }

        public byte GenderId { get; set; }

        #region ds dữ liệu từ điển

        public List<Fields> ListFields
        {
            get { return DropDownListHelpers.GetAllFields(); }
        }

        public List<Provinces> ListProvinces
        {
            get { return DropDownListHelpers.GetAllProvinces(); }
        }
        #endregion

        /// <summary>
        /// Nội dung quy ước bảo mật từ bài viết chi tiết
        /// </summary>
        public ArticlesViewDetail QuyUocBaoMat
        {
            get { return ArticlesViewHelpers.View_GetArticleDetail(Constants.QuyUocBaoMatArticleId, 0, 0, 0); }
        }

    }

    public class ProvinceCountModel
    {
        public int ProvinceId { get; set; }
        public int Count { get; set; }
        public string ProvinceName { get; set; }
    }
}