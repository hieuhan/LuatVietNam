using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.CMSViewLib
{
    public class CustomerLoginView
    {
        public string LoginStatus { set; get; }
        public string LoginMessage { set; get; }

        public CustomersView mCustomers { set; get; }
        public List<RolesView> lRoles { set; get; }

        public CustomerLoginView()
        {
            mCustomers = new CustomersView();
            lRoles = new List<RolesView>();
        }

    }
}
