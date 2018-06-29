using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;

namespace LawsVN.Models
{
    public class CustomerInterFaceFieldsModel:ViewModelBase
    {
        public List<Fields> ListFieldsVbpq { get; set; }
        public List<Fields> ListFieldsTcvb { get; set; }
    }
}