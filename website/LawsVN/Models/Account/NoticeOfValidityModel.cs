using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVN.Models.Account
{
    public class NoticeOfValidityModel:ViewModelBase
    {
        private List<Fields> _listFields;
        private List<EffectStatus> _listEffectStatus;
        public int RowCount { get; set; }
        public DocsViewSearch mDocsViewSearch { get; set; }
        public PartialPaginationAjax mPartialPaginationAjax { get; set; }
        public IEnumerable<MyDocumentsModel> ListMyDocuments { get; set; }

        public List<Fields> ListFields
        {
            get { return !_listFields.HasValue() ? DropDownListHelpers.GetAllFields() : _listFields; }
            set { _listFields = value; }
        }

        public List<EffectStatus> ListEffectStatus
        {
            get { return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus; }
            set { _listEffectStatus = value; }
        }
    }
}