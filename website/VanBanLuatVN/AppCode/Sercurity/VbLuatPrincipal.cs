using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace VanBanLuatVN.Library.Sercurity
{
    public class VbLuatPrincipal : IPrincipal
    {
        public bool IsInRole(string role)
        {
            return true;
        }

        public IIdentity Identity { get; private set; }

        public VbLuatPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerMail { get; set; }
        public string CustomerMobile { get; set; }
        public bool OpenId { get; set; }
    }

    public class AccountSerializerModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerMail { get; set; }

        public string CustomerMobile { get; set; }

        public bool OpenId { get; set; }
    }
}