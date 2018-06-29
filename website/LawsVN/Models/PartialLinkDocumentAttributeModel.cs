using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;

namespace LawsVN.Models
{
    public class PartialLinkDocumentAttributeModel
    {
        public DocsView mDocsView { get; set; }
        public DocRelates mDocRelates { get; set; }

        public bool EnableSaveDoc
        {
            get { return _enableSaveDoc; }
            set { _enableSaveDoc = value; }
        }

        private string _effectStatusName;
        private string _className;
        private bool _enableSaveDoc = true;

        public string EffectStatusName
        {
            get
            {
                return !string.IsNullOrEmpty(_effectStatusName) ? _effectStatusName : string.Empty; }
            set { _effectStatusName = value; }
        }

        public string ClassName
        {
            get { return !string.IsNullOrEmpty(_className) ? _className : "tag-item"; } 
            set { _className = value; }
        }
    }
}