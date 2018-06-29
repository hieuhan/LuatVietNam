using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVN.Models.Templates
{
    public class DocsNewest:ViewModelBase
    {
        private List<EffectStatus> _listEffectStatus;
        public List<DocsView> ListDocsView { get; set; }
        public DocsViewDetail mDocsViewDetail { get; set; }
        public List<LawTermins> l_LawTermins { get; set;}
        public RelateTypes mRelateTypes { get; set; }
        public int CountByRelateTypeId { get; set; }
        public List<EffectStatus> ListEffectStatus
        {
            get { return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus; }
            set { _listEffectStatus = value; }
        }

        public PartialPaginationAjax PartialPaginationAjax { get; set; }
    }
}