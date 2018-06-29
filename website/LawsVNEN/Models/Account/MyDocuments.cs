using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVNEN.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LawsVNEN.AppCode;

namespace LawsVNEN.Models.Account
{
    public class MyDocuments : ViewModelBase
    {
        private List<Fields> _listFields;
        private List<Organs> _listOrgans;
        private List<DocTypes> _listDocTypes;
        private List<EffectStatus> _listEffectStatus;
        public DocsViewSearch mDocsViewSearch { get; set; }
        public PartialPaginationAjax mPartialPaginationAjax { get; set; }
        public IEnumerable<MyDocumentsModel> ListMyDocuments { get; set; }

        public List<Fields> ListFields
        {
            get { return !_listFields.HasValue() ? DropDownListHelpers.GetAllFieldsByLanguage() : _listFields; }
            set { _listFields = value; }
        }

        public List<Organs> ListOrgans
        {
            get { return !_listOrgans.HasValue() ? DropDownListHelpers.GetAllOrgans() : _listOrgans; }
            set { _listOrgans = value; }
        }

        public List<DocTypes> ListDocTypes
        {
            get { return !_listDocTypes.HasValue() ? DropDownListHelpers.GetAllDocTypes() : _listDocTypes; }
            set { _listDocTypes = value; }
        }

        public List<EffectStatus> ListEffectStatus
        {
            get { return !_listEffectStatus.HasValue() ? DropDownListHelpers.GetAllEffectStatus() : _listEffectStatus; }
            set { _listEffectStatus = value; }
        } 
    }

    public class MyDocumentsModel
    {
        public DocsView DocsView { get; set; }
        public string EffectStatusName { get; set; }
    }
}