using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using LawsVN.App_GlobalResources;

namespace LawsVN.Models.Account
{
    public class MyMessagesModel:ViewModelBase
    {
        public int RowCount { get; set; }
        public Messages mMessages { get; set; }
        public List<Messages> ListMessages { get; set; }
        public PartialPaginationAjax mPartialPaginationAjax { get; set; }
    }

    public class MessageSearch : ViewModelBase
    {
        public byte ActionType { get; set; }

        [Display(Name = "MsgContent", ResourceType = typeof(Resource))]
        //[Required(ErrorMessage = "{0} (*)")]
        public string MsgContent { get; set; }
    }
}