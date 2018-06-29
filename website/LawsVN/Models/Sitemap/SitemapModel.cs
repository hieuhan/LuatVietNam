using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using LawsVN.Library;

namespace LawsVN.Models.Sitemap
{
    public class SitemapModel
    {
        public int RowCountDocs { get; set; }
        public int RowCountArticles { get; set; }
        public int RowCountTags { get; set; }
    }

    public class SitemapHomeModel
    {
        private List<MenuItemsView> _listMenuAll;

        public List<MenuItemsView> ListMenuAll
        {
            get { return !_listMenuAll.HasValue() ? DropDownListHelpers.GetAllMenuItems() : _listMenuAll;  }
            set { _listMenuAll = value; }
        }

        public List<MenuItemsView> ListMenu { get; set; }

        public List<Categories> ListCategories { get; set; }
    }
}