using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using ICSoft.ViewLibV3;
using VanBanLuat.Librarys;
using VanBanLuat.Models;
using Constants = VanBanLuat.Librarys.Constants;

namespace VanBanLuatVN.Models
{
    public class PaginationModel:ViewModelBase
    {
        private int _page;
        private int _pageSize;
        public string Keywords { get; set; }

        public int TotalPage { get; set; }

        public int PageSize
        {
            get { return _pageSize <= 0 ? Constants.RowAmount20 : _pageSize; }
            set { _pageSize = value; }
        }

        public int Page
        {
            get { return _page == 0 ? 2 : _page; }
            set { _page = value; }
        }

        //public bool IsSearchPage { get; set; }

        public string QueryString { get; set; }

        /// <summary>
        /// url gọi Ajax
        /// </summary>
        public string AjaxUrl { get; set; }

        public byte? SearchOptions { get; set; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int PageCount
        {
            get { return (int)Math.Ceiling(TotalPage / (double)PageSize); }
        }

        public NameValueCollection NameValueCollection { get; set; }

        #region Document
        public byte DocTypeId { get; set; }
        public byte LanguageId { get; set; }
        public short CategoryId { get; set; }

        public byte EffectStatusId { get; set; }
        public string EffectStatusName { get; set; }
        public short FieldId { get; set; }
        public int DocId { get; set; }

        public short RelateTypeId { get; set; }
        public string DisplayPosition { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public short OrganId { get; set; }
        public string SignerName { get; set; }
        public string SearchByDate { get; set; }
        public short SignerId { get; set; }
        public string EffectStatusType { get; set; }
        public byte IsSearchExact { get; set; }

        public DocList DocList { get; set; }
        public DocRelateList DocRelateList { get; set; }
        #endregion

        #region Acticle
        public CategoryArticles CategoryArticles { get; set; }

        #endregion

    }
}