using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocRelateList
    {
        public List<DocRelates> lDocRelates { get; set; }
        public List<RelateTypes> lCountByRelateType { get; set; }
        public int RowCount { get; set; }
    }
}
