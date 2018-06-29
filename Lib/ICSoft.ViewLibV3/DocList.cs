using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocList
    {
        public List<Docs> lDocs { get; set; }
        public List<Fields> lCountByField { get; set; }
        public List<DocTypes> lCountByDocType { get; set; }
        public List<Organs> lCountByOrgan { get; set; }
        public List<Provinces> lCountByProvince { get; set; }
        public List<EffectStatus> lCountByEffectStatus { get; set; }
        public List<Years> lCountByYear { get; set; }
        public List<DocGroups> lCountByDocGroup { get; set; }
        public int RowCount { get; set; }
    }
}
