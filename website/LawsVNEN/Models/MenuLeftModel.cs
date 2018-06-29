using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
namespace LawsVNEN.Models
{
    public class MenuLeftModel
    {
        public List<MenuItemsView> LMenuItemsLeftTop { get; set; }
        public byte DocGroupId { get; set; }
        public List<FieldDisplays> ListFieldDisplays { get; set; }
        public List<Fields> ListFieldsOther { get; set; }
        public bool IsHome { get; set; }
        public List<MenuItemsView> LMenuItemsLeftBottomParent { get; set; }
        public List<MenuItemsView> LMenuItemsLeftBottom { get; set; }
    }
}