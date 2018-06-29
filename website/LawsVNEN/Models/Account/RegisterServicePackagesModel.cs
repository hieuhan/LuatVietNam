using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;

namespace LawsVNEN.Models.Account
{
    public class RegisterServicePackagesModel : ViewModelBase
    {
        public short ServiceId { get; set; }

        /// <summary>
        /// Dich vu dang su dung
        /// </summary>
        public short ServiceIdUse { get; set; }

        public string ServiceName { get; set; }

        public short ServicePackageParentId { get; set; }

        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

        public string ServicePackageTime { get; set; }

        public string ServiceTimeRemain { get; set; }

        public short CurrentLogin { get; set; }

        public int Price { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public string FeeType { get; set; }

        public string ActionButton { get; set; }

        public int PriceVAT { get; set; }

        public int Total { get; set; }

        /// <summary>
        /// Thông báo chuyển đổi số ngày khi gia hạn, nâng cấp gói dịch vụ
        /// </summary>
        public string MsgChangeService { get; set; }

        public bool TaxInvoice { get; set; }

        [Display(Name = "Quy định thanh toán của Luật Việt Nam")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Vui lòng chọn đồng ý với {0}")]
        public bool TermsAndConditions { get; set; }

        /// <summary>
        /// Chi tiết dịch vụ
        /// </summary>
        public Services mServices { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ theo ServiceId
        /// </summary>
        public List<ServicePackages> ListServicePackagesParent { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ theo ServicePackageId
        /// </summary>
        public List<ServicePackages> ListServicePackages { get; set; }

        /// <summary>
        /// Chi tiết gói dịch vụ cha
        /// </summary>
        public ServicePackages mServicePackagesParent { get; set; }

        /// <summary>
        /// Chi tiết gói dịch vụ
        /// </summary>
        public ServicePackages mServicePackages { get; set; }

        /// <summary>
        /// Mã khuyến mại
        /// </summary>
        public string PromotionCode { get; set; }

        /// <summary>
        /// Gói dịch vụ đang sử dụng
        /// </summary>
        public DataTable DtCustomerService { get; set; }

        public PartialPaginationAjax PartialPaginationAjax { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ hỗ trợ nâng cấp
        /// </summary>
        public List<short> ListServicesIdUpgradeAllowed { get; set; }

        /// <summary>
        /// Hỗ trợ nâng cấp dịch vụ ko
        /// </summary>
        public bool IsUpgradeAllowed { get; set; }

    }
}