using System;
using System.Collections.Generic;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVN.Models.Docs
{
    public class DocsModel:ViewModelBase
    {
        private List<DocsView> _listDocsView;
        private DocsView _mDocsView;
        private List<DocRelates> _lDocRelates;
        private List<DocFiles> _lDocFiles;
        private string _docContent;
        private string _keywords;
        private string[] _searchOptions;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private byte _docType;
        private byte _organId;
        private byte _effectStatusId;
        private short _fieldId;
        private short _signerId;
        private byte _languageId;
        private List<DocsView> _listDocsViewMostView;

        public PartialPaginationAjax PartialPaginationAjax { get; set; }

        public string EffectStatusName { get; set; }

        public string EffectStatus { get; set; }

        public DocsView mDocsView
        {
            get { return _mDocsView; }
            set { _mDocsView = value; }
        }

        public List<DocRelates> lDocRelates
        {
            get { return _lDocRelates; }
            set { _lDocRelates = value; }
        }

        public List<DocFiles> lDocFiles
        {
            get { return _lDocFiles; }
            set { _lDocFiles = value; }
        }

        public string DocContent
        {
            get { return _docContent; }
            set { _docContent = value; }
        }

        public List<DocsView> ListDocsView
        {
            get { return _listDocsView; }
            set { _listDocsView = value; }
        }

        /// Danh sách văn bản xem nhiều
        public List<DocsView> ListDocsViewMostView
        {
            get { return _listDocsViewMostView; }
            set { _listDocsViewMostView = value; }
        }

        public byte LanguageId
        {
            get { return _languageId; }
            set { _languageId = value; }
        }

        public short SignerId
        {
            get { return _signerId; }
            set { _signerId = value; }
        }

        public short FieldId
        {
            get { return _fieldId; }
            set { _fieldId = value; }
        }

        public byte EffectStatusId
        {
            get { return _effectStatusId; }
            set { _effectStatusId = value; }
        }

        public byte OrganId
        {
            get { return _organId; }
            set { _organId = value; }
        }

        public byte DocType
        {
            get { return _docType; }
            set { _docType = value; }
        }

        public DateTime DateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; }
        }

        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }

        public string[] SearchOptions
        {
            get { return _searchOptions; }
            set { _searchOptions = value; }
        }

        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        public List<EffectStatus> ListEffectStatus
        {
            get { return DropDownListHelpers.GetAllEffectStatus(); }
        }
    }
}