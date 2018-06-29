using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.CMSLibEstate;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.Library;
using Messages = ICSoft.CMSLibEstate.Messages;

namespace LawsVN.Models
{
    public class HeaderMobileModel : ViewModelBase
    {
        //public new int MenuItemId { get; set; }
        //public new byte GetCountPaymentTransactionSuccess { get; set; }

        //public new byte GetCountThongBaoHieuLuc { get; set; }
        public int RowCountNotifyMessages { get; set; }

        public List<NotifyMessages> ListNotifyMessages { get; set; }
        public List<Messages> ListMessages { get; set; }
        public List<MenuItemsView> ListMenuItemsView { get; set; }
        public List<MenuItemsView> ListMenuItemsViewParent { get; set; }
        public byte DocGroupId { get; set; }
        public List<Fields> ListField { get; set; }
        public bool Havechild(int menuItemId)
        {
            if (ListMenuItemsView.HasValue())
            {
                return ListMenuItemsView.Any(x => x.ParentItemId == menuItemId);
            }
            return false;
        }
    }
}