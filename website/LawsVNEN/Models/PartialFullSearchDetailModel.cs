using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using LawsVNEN.Library;
using LawsVNEN.AppCode;
using LawsVNEN.App_GlobalResources;

namespace LawsVNEN.Models
{
    public class PartialFullSearchDetailModel
    {
        public short FieldId { get; set; }
        public byte DocGroupId { get; set; }
        public byte SearchOptions { get; set; }
        
        private List<FieldDisplays> _listFields;
        //private string _keywords;
        //[Display(Name = "SearchKeywords", ResourceType = typeof(Resource))]
        //[Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "SearchKeywordsRequired")]
        //public string Keywords
        //{
        //    get { return !string.IsNullOrEmpty(_keywords)? _keywords.StripTags().Sanitize() : string.Empty; }
        //    set { _keywords = value; }
        //}
        public string Keywords { get; set; }
        public List<FieldDisplays> ListFields
        {
            get { return !_listFields.HasValue() ? DropDownListHelpers.GetAllFieldDisplays(Constants.FieldDisplayTypeIdVbpq) : _listFields; }
            set { _listFields = value; }
        }
    }
}