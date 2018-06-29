using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;

namespace LawsVNEN.Models
{
    public class HeaderModel:ViewModelBase
    {
        public List<MenuItemsView> ListMenuItemsView { get; set; }
        public string ReturnUrl { get; set; }
    }
}