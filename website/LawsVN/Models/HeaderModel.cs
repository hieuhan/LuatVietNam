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
    public class HeaderModel:ViewModelBase
    {
        //public new int MenuItemId { get; set; }
        //public new byte GetCountPaymentTransactionSuccess { get; set; }
        //public new byte GetCountThongBaoHieuLuc { get; set; }
        public int RowCountNotifyMessages { get; set; }
        public List<NotifyMessages> ListNotifyMessages { get; set; }
        public List<Messages> ListMessages { get; set; }
        public List<MenuItemsView> ListMenuItemsHeaderView { get; set; }
        public List<MenuItemsView> ListMenuItemsView { get; set; }
        public string Title { get; set; }
    }
}