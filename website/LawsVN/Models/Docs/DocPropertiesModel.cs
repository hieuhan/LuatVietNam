using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawsVN.Models.Docs
{
    public class DocPropertiesModel
    {
        public byte DocGroupId { get; set; }
        public string DocName { get; set; }
        public string OrganName { get; set; }
        public string FieldName { get; set; }
        public DateTime IssueDate { get; set; }
        public string Fee { get; set; }
        public DateTime EffectDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string DocTypeName { get; set; }
        public string DocIdentity { get; set; }
        public string SignerName { get; set; }
        public byte LanguageId { get; set; }
    }
}