using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class FooterModel//:ViewModelBase
    {
        public List<MenuItemsView> ListMenuItemsViewParent { get; set; }

        public List<MenuItemsView> ListMenuItemsView { get; set; }

        public List<MenuItemsView> ListMenuItemsBottomRightView =
            DropDownListHelpers.GetMenuItemsByMenuId(Constants.MenuIdBottomRight);
    }
}