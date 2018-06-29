using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocFilterParams
    {
        public DocFilterParams()
        {
            LanguageId = 1;
            DocId = 0;
            DocGUId = "";
            DocName = "";
            DocIdentity = "";
            EffectStatusId = 0;
            ReviewStatusId = 0;
            DocGroupId = 0;
            DocTypeId = 0;
            FieldSelect = "DocId,DocName";
            RowAmount = 10;
            PageIndex = 0;
            OrderBy = "IssueDate DESC";
            SearchKeyword = "";
            IsSearchExact = 0;
            HighlightKeyword = 1;
            FieldId = 0;
            OrganId = 0;
            SignerId = 0;
            DisplayTypeId = 0;
            ProvinceId = 0;
            CustomerId = 0;
            RegistTypeId = 0;
            EffectStatusType = "";
            SearchByDate = "IssueDate";
            DateFrom = DateTime.MinValue;
            DateTo = DateTime.MinValue;
            GetDocFile = 0;
            GetDocIndex = 0;
            GetRowCount = 0;
            GetCountByField = 0;
            GetCountByDocType = 0;
            GetCountByOrgan = 0 ;
            GetCountByProvince = 0;
            GetCountByEffectStatus = 0;
            GetCountByYear = 0;
            GetCountByGroup = 0;
            GetOrganName = 0;
            GetSignerName = 0;
            GetFieldName = 0;
            GetDocTypeName = 0;
            GetEffectStatusName = 0;
        }
        public byte LanguageId { get; set; }
        public int DocId { get; set; }
        public string DocGUId { get; set; }
        public string DocName { get; set; }
        public string DocIdentity { get; set; }
        public byte EffectStatusId { get; set; }
        public byte ReviewStatusId { get; set; }
        public byte DocGroupId { get; set; }
        public byte DocTypeId { get; set; }
        public string FieldSelect { get; set; }
        public int RowAmount { get; set; }
        public int PageIndex { get; set; }
        public string OrderBy { get; set; }
        public string SearchKeyword { get; set; }
        public byte IsSearchExact { get; set; }
        public byte HighlightKeyword { get; set; }
        public short FieldId { get; set; }
        public short OrganId { get; set; }
        public short SignerId { get; set; }
        public short DisplayTypeId { get; set; }
        public short ProvinceId { get; set; }
        public int CustomerId { get; set; }
        public byte RegistTypeId { get; set; }
        public string EffectStatusType { get; set; }
        public string SearchByDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public byte GetDocFile { get; set; }
        public byte GetDocIndex { get; set; }
        public byte GetRowCount { get; set; }
        public byte GetCountByField { get; set; }
        public byte GetCountByDocType { get; set; }
        public byte GetCountByOrgan { get; set; }
        public byte GetCountByProvince { get; set; }
        public byte GetCountByEffectStatus { get; set; }
        public byte GetCountByYear { get; set; }
        public byte GetCountByGroup { get; set; }
        public byte GetOrganName { get; set; }
        public byte GetSignerName { get; set; }
        public byte GetFieldName { get; set; }
        public byte GetDocTypeName { get; set; }
        public byte GetEffectStatusName { get; set; }

    }
}
