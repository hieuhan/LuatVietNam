using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ICSoft.LawDocsLib;

namespace ICSoft.CMSViewLib
{
    public class CustomersViewDetail
    {
        private Customers _mCustomers;
        private List<CustomerFieldsView> _listCustomerFieldsView;
        private CustomerServicesView _mCustomerServicesView;

        public Customers mCustomers
        {
            get { return _mCustomers; }
            set { _mCustomers = value; }
        }

        public List<CustomerFieldsView> ListCustomerFieldsView
        {
            get { return _listCustomerFieldsView; }
            set { _listCustomerFieldsView = value; }
        }

        public CustomerServicesView mCustomerServicesView
        {
            get { return _mCustomerServicesView; }
            set { _mCustomerServicesView = value; }
        }

        public CustomerAccessLogs mCustomerAccessLogs { get; set; }

        //-----------------------------------------------------------------
        public CustomersViewDetail()
        {
        }
        //-----------------------------------------------------------------
        ~CustomersViewDetail()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //----------------------------------------------------------------
    }
}
