using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ICSoft.ViewLibV3;
using VanBanLuat.Librarys;
using VanBanLuat.Models;
using Constants = VanBanLuat.Librarys.Constants;

namespace VanBanLuatVN.Models
{
    public class DocViewModel
    {
        public class DocsGetPageModel
        {
            public DocList DocList { get; set; }
            public string Url { get; set; }

            /// <summary>
            /// Reset số thứ tự về bắt đầu từ 1 khi paste link phân trang
            /// </summary>
            public bool ResetIndex { get; set; }
        }

        /// <summary>
        /// Model tìm kiếm văn bản
        /// </summary>
        public class DocSearchModel:ViewModelBase
        {
            private string _keywords;
            private bool _showAdvancedSearchPannel;
            private string _searchByDate;

            [Display(Name = "Từ khóa")]
            public string Keywords
            {
                get { return !string.IsNullOrEmpty(_keywords) ? _keywords.StripTags().SanitizeWithoutSplash() : string.Empty; }
                set { _keywords = value; }
            }

            [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
            public string DateFrom { get; set; }

            [RegularExpression(@"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$", ErrorMessage = "Ngày chọn không hợp lệ.")]
            public string DateTo { get; set; }

            /// <summary>
            /// Trạng thái hiệu lực
            /// </summary>
            public byte EffectStatusId { get; set; }

            /// <summary>
            /// Lĩnh vực
            /// </summary>
            public short FieldId { get; set; }

            public byte LanguageId { get; set; }

            public short SignerId { get; set; }

            public string SignerName { get; set; }

            public short OrganId { get; set; }

            public byte DocGroupId { get; set; }

            public byte DocTypeId { get; set; }

            private short _displayTypeId;

            public short DisplayTypeId
            {
                get
                {
                    if (DocGroupId == Constants.DocGroupIdVbpq)
                    {
                        _displayTypeId = Constants.FieldDisplayTypeIdVbpq;
                    }
                    else
                    {
                        if (DocGroupId == Constants.DocGroupIdTcvn)
                        {
                            _displayTypeId = Constants.TcvnDisplaytypeId;
                        }
                    }

                    return _displayTypeId;
                }

                set
                {
                    _displayTypeId = value;
                }
            }

            /// <summary>
            /// Tìm chính xác cụm từ
            /// </summary>
            public byte SearchExact { get; set; }

            /// <summary>
            /// Extract tìm kiếm nâng cao khi chọn điều kiện lọc
            /// </summary>
            public bool ShowAdvancedSearchPannel
            {
                get { return !string.IsNullOrEmpty(DateFrom) || !string.IsNullOrEmpty(DateTo) || DocGroupId > 0 || DocTypeId > 0 || OrganId > 0 || EffectStatusId > 0 || FieldId > 0 || LanguageId > 0 || SignerId > 0; }
                set { _showAdvancedSearchPannel = value; }
            }

            public string SearchByDate
            {
                get { return !string.IsNullOrEmpty(_searchByDate) ? _searchByDate.StripTags().Sanitize() : string.Empty; }
                set { _searchByDate = value; }
            }

            /// <summary>
            /// Tùy chọn tìm kiếm: Tất cả - Tiêu đề - Số hiệu văn bản
            /// </summary>
            public byte? SearchOptions { get; set; }

            public string DocGroupName { get; set; }

            public string DocTypeName { get; set; }

            public string EffectStatusName { get; set; }

            public string FieldName { get; set; }

            public string OrganName { get; set; }

            public int Page { get { return _page == 0 ? 1 : _page ; }
                set { _page = value; }
            }
            public PaginationModel Pagination { get; set; }
            public DocList DocList { get; set; }

            #region ds du lieu tu dien

            private List<Fields> _listFields;
            public List<Fields> ListFields
            {
                set { _listFields = value; }
                get
                {
                    return !_listFields.HasValue() ? ListHelpers.FieldsGetList() : _listFields;
                }
            }

            private List<DocTypes> _listDocTypes;
            public List<DocTypes> ListDocTypes
            {
                set { _listDocTypes = value; }
                get
                { return !_listDocTypes.HasValue() ? ListHelpers.DocTypesGetListByDisplayType(_displayTypeId) : _listDocTypes; }

            }

            private List<Organs> _listOrgans;
            public List<Organs> ListOrgans
            {
                set { _listOrgans = value; }
                get { return !_listOrgans.HasValue() ? ListHelpers.OrgansGetList() : _listOrgans; }

            }

            private List<Languages> _listLanguages;
            public List<Languages> ListLanguages
            {
                get { return !_listLanguages.HasValue() ? ListHelpers.LanguagesGetList() : _listLanguages; }
                set { _listLanguages = value; }
            }

            private List<EffectStatus> _listEffectStatus;

            public List<EffectStatus> ListEffectStatus
            {
                get
                {
                    return !_listEffectStatus.HasValue() ? ListHelpers.EffectStatusGetList() : _listEffectStatus;
                }
                set { _listEffectStatus = value; }
            }

            private List<DocGroups> _listDocGroups;
            private int _page;

            public List<DocGroups> ListDocGroups
            {
                get { return !_listDocGroups.HasValue() ? ListHelpers.DocGroupsGetList() : _listDocGroups; }
                set { _listDocGroups = value; }
            }

            #endregion
        }

        /// <summary>
        /// Model chi tiết văn bản
        /// </summary>
        public class DocDetailModel:ViewModelBase
        {
            public Docs Docs { get; set; }
            public DocRelateList AllDocRelateLists { get; set; }
            public DocRelateList DocRelateLists { get; set; }
            public DocRelateList DocRelateListsEffect { get; set; }
            public PaginationModel Pagination { get; set; }
            /// <summary>
            /// Văn bản liên quan
            /// </summary>
            public List<DocRelates> ListDocRelates { get; set; }
            /// <summary>
            /// Danh sách file
            /// </summary>
            public List<DocFiles> ListDocFiles { get; set; }

            /// <summary>
            /// Tất cả ds loại liên quan theo nhóm VB
            /// </summary>
            public List<RelateTypes> ListRelateTypesByGroupId { get; set; }
            public string FilePath { get; set; }
            public string FileUrl { get; set; }
            public int CountByRelateTypeId { get; set; }
            public bool HaveDoc { get; set; }
            public short FieldId { get; set; }
        }

        /// <summary>
        /// Văn bản liên quan lĩnh vực
        /// </summary>
        public class RelateFieldDoc
        {
            public List<Docs> ListDocRelateNewest { get; set; }

            public List<Docs> ListDocRelate { get; set; }
        }

        /// <summary>
        /// Gửi email link văn bản
        /// </summary>
        public class DocSendMail
        {
            [Display(Name = "Email")]
            [Required(ErrorMessage = "Quý khách vui lòng nhập {0} (*)")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            [RegularExpression(@"[a-zA-Z0-9_\.]+@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)*", ErrorMessage = "Địa chỉ Email không hợp lệ !")]
            public string SendMail { get; set; }

            public string DocUrl { get; set; }
        }

        /// <summary>
        /// Gửi góp ý cho văn bản
        /// </summary>
        public class DocFeedback
        {
            private string _feedbackContent;

            [Display(Name = "Tên văn bản")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            public string DocName { get; set; }

            [Display(Name = "Nội dung góp ý")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            public string FeedbackContent
            {
                get { return !string.IsNullOrEmpty(_feedbackContent) ? _feedbackContent.StripTags().Sanitize() : string.Empty; }
                set { _feedbackContent = value; }
            }
        }
        /// <summary>
        /// Gửi yêu cầu văn bản
        /// </summary>
        public class GuiYeuCauVB
        {
            private string _feedbackContent;

            [Display(Name = "Nhập Email của bạn")]
            public string Email { get; set; }

            [Display(Name = "Nội dung yêu cầu")]
            [Required(ErrorMessage = "Vui lòng nhập {0} (*)")]
            public string FeedbackContent
            {
                get { return !string.IsNullOrEmpty(_feedbackContent) ? _feedbackContent.StripTags().Sanitize() : string.Empty; }
                set { _feedbackContent = value; }
            }
        }
    }

}