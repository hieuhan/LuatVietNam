using System;
using System.Linq;
using System.Security.Principal;
using ICSoft.CMSLib;

namespace LawsVN.Library.Sercurity
{
    public class LawsVnPrincipal:IPrincipal
    {
        public bool IsInRole(string role)
        {
            return Roles.Any(role.Contains);
        }

        public IIdentity Identity { get; private set; }

        public LawsVnPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerMail { get; set; }

        public string CustomerMobile { get; set; }

        public string Avatar { get; set; }

        public byte GenderId { get; set; }

        public string GetAvatar()
        {
            var retVal = this.Avatar;
            return !string.IsNullOrEmpty(retVal)
                ? (!this.Avatar.StartsWith("http") && !this.Avatar.StartsWith("/") 
                    ? string.Concat(CmsConstants.WEBSITE_IMAGEDOMAIN, retVal)
                    : retVal)
                    : (this.GenderId == 1 ? Constants.MaleAvatar : (this.GenderId == 2 ? Constants.FeMaleAvatar : Constants.NoAvatar));
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

        public byte GenderId { get; set; }

        public string[] Roles { get; set; }
    }

}