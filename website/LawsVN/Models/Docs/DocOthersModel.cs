using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVN.Models.Docs
{
    public class DocOthersModel:ViewModelBase
    {
        public List<DocsView> ListDocsView { get; set; }
        public List<Fields> ListFields { get; set; }
        public List<EffectStatus> ListEffectStatus
        {
            get { return DropDownListHelpers.GetAllEffectStatus(); }
        }
    }
}