using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVN.Models.Account
{
    public class HistoryTransactionsModel:ViewModelBase
    {
        private List<ServicePackages> _listServicePackages;
        private List<PaymentTypes> _listPaymentTypes;
        private List<Services> _listServices;
        private string _PayMessage = "";
        private List<TransactionTypes> _listTransactionTypes;
        public int RowCount { get; set; }
        public int TotalMoney { get; set; }

        /// <summary>
        /// Danh sách giao dịch full
        /// </summary>
        public IEnumerable<PaymentTransactionsModel> ListPaymentTransactions { get; set; }

        public PartialPaginationAjax PartialPaginationAjax { get; set; }

        /// <summary>
        /// Thông tin dịch vụ đang sử dụng
        /// </summary>
        public DtCustomerServices DtCustomerServices { get; set; }

        /// <summary>
        /// Số tháng sử dụng dịch vụ còn lại
        /// </summary>
        public double MonthRemain { get; set; }

        /// <summary>
        /// Số ngày sử dụng dịch vụ còn lại
        /// </summary>
        public double DayRemain { get; set; }

        public List<ServicePackages> ListServicePackages
        {
            get { return !_listServicePackages.HasValue() ? DropDownListHelpers.GetAllServicePackages() : _listServicePackages; }
            set { _listServicePackages = value; }
        }

        public List<PaymentTypes> ListPaymentTypes
        {
            get { return !_listPaymentTypes.HasValue() ? DropDownListHelpers.GetAllPaymentTypes() : _listPaymentTypes; }
            set { _listPaymentTypes = value; }
        }

        public List<Services> ListServices
        {
            get { return !_listServices.HasValue() ? DropDownListHelpers.GetAllServices() : _listServices; }
            set { _listServices = value; }
        }

        public List<TransactionTypes> ListTransactionTypes
        {
            get { return !_listTransactionTypes.HasValue() ? DropDownListHelpers.GetAllPTransactionTypes() : _listTransactionTypes; } 
            set { _listTransactionTypes = value; }
        }

        public string PayMessage
        {
            get
            {
                return _PayMessage;
            }

            set
            {
                _PayMessage = value;
            }
        }
    }

    public class PaymentTransactionsModel
    {
        public int PaymentTransactionId { get; set; }
        public string TransactionDesc { get; set; }
        public byte TransactionStatusId { get; set; }
        public string TransactionStatusDesc { get; set; }
        public string Amount { get; set; }
        public byte PaymentTypeId { get; set; }
        public string PaymentDate { get; set; }
        public short ServiceId { get; set; }
        public string ServiceDesc { get; set; }
        public short ServicePackageId { get; set; }
        public string ServicePackageDesc { get; set; }
        public string PaymentTypeDesc { get; set; }
        public byte TransactionTypeId { get; set; }
        public string TransactionTypesDesc { get; set; }
    }
}