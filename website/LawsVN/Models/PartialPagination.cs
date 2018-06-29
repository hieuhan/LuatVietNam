using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;

namespace LawsVN.Models
{
    public class PartialPagination: ViewModelBase
    {
        public string Keywords { get; set; }

        public int TotalPage { get; set; }

        public int PageSize { get; set; }

        public int ShowNumberOfResults { get; set; }

        public int LinkLimit { get; set; }

        public int PageIndex { get; set; }

        public string UrlPaging { get; set; }

        public byte DocGroupId { get; set; }

        public byte LanguageId { get; set; }

        public byte UsingDisplayTable { get; set; }

        public new string ActionName { get; set; }

        public new string ControllerName { get; set; }

        public short CategoryId { get; set; }

        public string EffectStatusName { get; set; }

        public short FieldId { get; set; }

        public byte EffectStatusId { get; set; }

        public short OrganId { get; set; }

        public byte DocTypeId { get; set; }
        public short ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public int DocId { get; set; }
        public short RelateTypeId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string TerName { get; set; }
        public byte LawTerminGroupId { get; set; }

        public string SignerName { get; set; }
        public byte MessageTypeId { get; set; }
        public bool IsMyMessage { get; set; }
        public byte RegistTypeId { get; set; }
        public int TagId { get; set; }
        public byte SearchOptions { get; set; }
        public short SignerId { get; set; }
        public short DisplayTypeId { get; set; }
        /// <summary>
        /// Class post-time-right
        /// </summary>
        public string PostTimeRight { get; set; }
        public string ClassTagItem { get; set; }
        public short ServiceId { get; set; }
        public short ServicePackageId { get; set; }
        public byte PaginationType { get; set; }
        public byte TransactionStatusId { get; set; }
        public string DisplayPosition { get; set; }
        public byte IsSearchExact { get; set; }
        public bool IsMobile { get; set; }
        public bool PageLoad { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int pageCount
        {
            get { return TotalPage > 0 && PageSize > 0 ? (int)Math.Ceiling(TotalPage / (double)PageSize) : 0; }
        }
    }

    public class PartialPaginationAjax : PartialPagination
    {
        public AjaxOptions LawsAjaxOptions { get; set; }
    }
}