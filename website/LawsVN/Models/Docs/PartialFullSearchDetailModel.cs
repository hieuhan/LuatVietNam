using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;

namespace LawsVN.Models.Docs
{
    public class PartialFullSearchDetailModel
    {
        public short FieldId { get; set; }
        public byte DocGroupId { get; set; }
        public byte SearchOptions { get; set; }
        private string _keywords;
        private List<Fields> _listFields;

        [Display(Name = "SearchKeywords", ResourceType = typeof(Resource))]
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "SearchKeywordsRequired")]
        public string Keywords
        {
            get { return !string.IsNullOrEmpty(_keywords)? _keywords.StripTags().Sanitize() : string.Empty; }
            set { _keywords = value; }
        }

        public List<Fields> ListFields
        {
            get { return !_listFields.HasValue() ? DropDownListHelpers.GetAllFields() :_listFields ; }
            set { _listFields = value; }
        }
    }
}