using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;

namespace LawsVN.Models.Shared
{
    public class PartialFieldOfSearchModel
    {
        public byte DocGroupId { get; set; }

        public List<FieldDisplays> ListFieldDisplays { get; set; }

        public List<FieldDisplays> ListFieldVbhn { get; set; }

        public List<Fields> ListFieldsOther { get; set; }

        public List<MenuItemsView> ListMenuItemsView { get; set; }

        public bool IsHome { get; set; }
    }
}