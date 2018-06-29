using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocLinkData
    {
        public List<DocTypes> lDocTypes { get; set; }
        public List<EffectStatus> lEffectStatus { get; set; }
        public List<DocOrgans> lDocOrgans { get; set; }
        public List<Organs> lOrgans { get; set; }
        public List<DocSigners> lDocSigners { get; set; }
        public List<Signers> lSigners { get; set; }
        public List<DocFields> lDocFields { get; set; }
        public List<Fields> lFields { get; set; }
    }
}
