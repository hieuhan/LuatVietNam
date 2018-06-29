using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;

namespace LawsVN.Models.Docs
{
    public class CongVanModel:ViewModelBase
    {
        public short DisplayTypeId { get; set; }

        /// <summary>
        /// Điểm tin văn bản mới
        /// </summary>
        public List<ArticlesView> ListArticlesView { get; set; }

        public List<ArticlesView> ListArticlesViewCapNhatHangTuan { get; set; }

        public List<DocsView> ListCongVanThueMoi { get; set; }

        public List<DocsView> ListCongVanHaiQuanMoi { get; set; }

        public List<DocsView> ListCongVanMoi { get; set; }

        public List<DocsView> ListDocsViewMostView { get; set; }

        public List<DocsView> ListDocsView { get; set; }

        public PartialPaginationAjax mPartialPaginationAjax { get; set; }

        #region Danh sách dữ liệu từ điển

        public List<EffectStatus> ListEffectStatus
        {
            get
            {
               return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus;
            } 
            set { _listEffectStatus = value; }
        }

        private List<Fields> _listFields;
        private List<EffectStatus> _listEffectStatus;

        public List<Fields> ListFields
        {
            get
            {
                return !_listFields.HasValue()
                    ? DropDownListHelpers.GetAllFields()
                    : _listFields;
            }
            set
            {
                _listFields = value;
            }
        }

        #endregion

    }
}