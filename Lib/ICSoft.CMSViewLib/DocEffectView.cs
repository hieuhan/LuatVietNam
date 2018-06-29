using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSoft.LawDocsLib;

namespace ICSoft.CMSViewLib
{
    public class DocEffectView
    {
        public DocsView mDocsView { get; set; }

        public List<DocRelates> lDocRelates { get; set; }

        //-----------------------------------------------------------------
        public DocEffectView()
        {
        }
        //-----------------------------------------------------------------
        ~DocEffectView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

    }
}
