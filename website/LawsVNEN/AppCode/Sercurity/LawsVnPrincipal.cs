using System;
using System.Linq;
using System.Security.Principal;
using ICSoft.CMSLib;
using LawsVNEN.AppCode;

namespace LawsVN.Library.Sercurity
{
    public class LawsVnPrincipal:IPrincipal
    {
        private string _customerFullName;
        private string _customerMobile;

        public bool IsInRole(string role)
        {
            return Roles.Any(r => role.Contains(r));
        }

        public IIdentity Identity { get; private set; }

        public LawsVnPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerFullName
        {
            get { return !string.IsNullOrEmpty(_customerFullName) ?_customerFullName :  string.Empty; } 
            set { _customerFullName = value; }
        }

        public string CustomerMail { get; set; }

        public string CustomerMobile
        {
            get { return !string.IsNullOrEmpty(_customerMobile) ? _customerMobile : string.Empty; } 
            set { _customerMobile = value; }
        }

        public string Avatar { get; set; }

        public string GetAvatar()
        {
            var retVal = this.Avatar;
            return !string.IsNullOrEmpty(retVal)
                ? (!this.Avatar.StartsWith("http://")
                    ? string.Concat(CmsConstants.WEBSITE_IMAGEDOMAIN, retVal)
                    : retVal)
                : Constants.NoAvatar;
        }

        public string GetAvatarMobile()
        {
            return GetAvatar().Replace("/Original/", "/Mobile/");
        }

        public string[] Roles { get; set; }

    }

    public class AccountSerializerModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerMail { get; set; }

        public string CustomerMobile { get; set; }

        public string Avatar { get; set; }

        public string[] Roles { get; set; }
    }

}