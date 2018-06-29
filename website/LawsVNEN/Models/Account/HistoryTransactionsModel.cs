using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.LawDocsLib;
using LawsVNEN.Library;
using LawsVNEN.AppCode;

namespace LawsVNEN.Models.Account
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
        public string TotalMoneyText { get; set; }
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

        /// <summary>
        /// Số ngày cảnh báo dịch vụ sắp hết hạn
        /// </summary>
        public int DaysAlert { get; set; }

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
        private string _transactionData ;
        private string _transactionTypesDesc;
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

        public string TransactionTypesDesc
        {
            get { return !string.IsNullOrEmpty(_transactionTypesDesc) ? _transactionTypesDesc : string.Empty; } 
            set { _transactionTypesDesc = value; }
        }

        public string TransactionData
        {
            get { return !string.IsNullOrEmpty(_transactionData) ? _transactionData : string.Empty ; } 
            set { _transactionData  = value; }
        }
    }
}