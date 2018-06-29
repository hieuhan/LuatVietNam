using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawsVNEN.Models
{
    public class PartialLinkDocumentAttributeModel
    {
        public DocsView mDocsView { get; set; }
        public DocRelates mDocRelates { get; set; }
        private string _effectStatusName;

        public string EffectStatusName
        {
            get
            {
                return !string.IsNullOrEmpty(_effectStatusName) ? _effectStatusName : string.Empty;
            }
            set { _effectStatusName = value; }
        }

        public string ClassName { get; set; }

    }
}