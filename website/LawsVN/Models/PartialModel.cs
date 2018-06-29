using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;

namespace LawsVN.Models
{
    public class PartialModel
    {
    }

    public class MenuDocDetail : ViewModelBase
    {
        public Menus Menu { get; set; }
        public List<MenuItemsView> ListMenuItemsView { get; set; }
    }
}