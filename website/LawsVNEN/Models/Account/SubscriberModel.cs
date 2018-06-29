using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using LawsVNEN.AppCode;
using LawsVNEN.App_GlobalResources;
using LawsVNEN.Library;
using ICSoft.CMSViewLib;

namespace LawsVNEN.Models.Account
{
    public class SubscriberModel:ViewModelBase
    {
        private ArticlesViewDetail _instructionforpayment;
        public short ServiceId { get; set; }
        public int InstructionforpaymentArticleId
        {
            get
            {
                return LawsVnLanguages.GetCurrentLanguageId() == 1
                    ? Constants.IntrorductionForPaymentVNI
                    : Constants.IntrorductionForPaymentEN;
            }
        }

        /// <summary>
        /// Dich vu dang su dung
        /// </summary>
        public short ServiceIdUse { get; set; }

        public byte IsRegistService { get; set; }

        public string ServiceName { get; set; }

        public string ServiceStatus { get; set; }

        public short ServicePackageId { get; set; }

        public string ServicePackageName { get; set; }

        public string ServicePackageTime { get; set; }

        public short CurrentLogin { get; set; }

        public int Price { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime RenewTime { get; set; }

        public string FeeType { get; set; }

        public string ActionButton { get; set; }

        public string Messages { get; set; }

        public string ServiceTimeRemain { get; set; }

        public short ServicePackageParentId { get; set; }

        public int PriceVAT { get; set; }

        public int Total { get; set; }

        /// <summary>
        /// Thông báo chuyển đổi số ngày khi gia hạn, nâng cấp gói dịch vụ
        /// </summary>
        public string MsgChangeService { get; set; }

        public Services Service { get; set; }

        public ServicePackages ServicePackageSingle { get; set; }

        public ServicePackages ServicePackagesParent { get; set; }

        public ServicePackages ServicePackage { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ hỗ trợ nâng cấp
        /// </summary>
        public List<short> ListServicesIdUpgradeAllowed { get; set; }

        /// <summary>
        /// Gói dịch vụ đang sử dụng
        /// </summary>
        public DataTable DtCustomerService { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ theo ServiceId
        /// </summary>
        public List<ServicePackages> ListServicePackagesParent { get; set; }

        /// <summary>
        /// Danh sách gói dịch vụ theo ServicePackageId
        /// </summary>
        public List<ServicePackages> ListServicePackages { get; set; }

        public ArticlesViewDetail Instructionforpayment
        {
            get { return _instructionforpayment ?? DropDownListHelpers.GetArticlesViewDetail(InstructionforpaymentArticleId); } 
            set { _instructionforpayment = value; }
        }
    }
}