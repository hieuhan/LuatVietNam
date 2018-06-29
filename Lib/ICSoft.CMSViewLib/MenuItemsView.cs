using System;
using System.Collections.Generic;
using sms.database;
using System.Data.SqlClient;
using ICSoft.CMSLib;

namespace ICSoft.CMSViewLib
{
    public class MenuItemsView
    {
        private int _MenuItemId;
        private int _MenuId;
        private string _ItemName;
        private string _ItemDesc;
        private string _IconPath;
        private string _Url;
        private int _ParentItemId;
        private byte _ItemLevel;
        private int _DisplayOrder;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private short _CategoryId;
        private string _MetaTitle;
        private string _MetaDesc;
        private string _MetaKeyword;
        private string _CanonicalTag;
        private string _H1Tag;
        private string _SeoFooter;
        //-----------------------------------------------------------------
        public MenuItemsView()
        {
        }
        //-----------------------------------------------------------------        
        ~MenuItemsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------    
        public int MenuItemId
        {
            get { return _MenuItemId; }
            set { _MenuItemId = value; }
        }
        //-----------------------------------------------------------------
        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }
        //-----------------------------------------------------------------
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
        //-----------------------------------------------------------------
        public string ItemDesc
        {
            get { return _ItemDesc; }
            set { _ItemDesc = value; }
        }
        //-----------------------------------------------------------------
        public string IconPath
        {
            get { return _IconPath; }
            set { _IconPath = value; }
        }
        //-----------------------------------------------------------------
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        //-----------------------------------------------------------------
        public int ParentItemId
        {
            get { return _ParentItemId; }
            set { _ParentItemId = value; }
        }
        //-----------------------------------------------------------------
        public byte ItemLevel
        {
            get { return _ItemLevel; }
            set { _ItemLevel = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string MetaTitle
        {
            get { return _MetaTitle; }
            set { _MetaTitle = value; }
        }
        //-----------------------------------------------------------------
        public string MetaDesc
        {
            get { return _MetaDesc; }
            set { _MetaDesc = value; }
        }
        //-----------------------------------------------------------------
        public string MetaKeyword
        {
            get { return _MetaKeyword; }
            set { _MetaKeyword = value; }
        }
        //-----------------------------------------------------------------
        public string CanonicalTag
        {
            get { return _CanonicalTag; }
            set { _CanonicalTag = value; }
        }
        //-----------------------------------------------------------------
        public string H1Tag
        {
            get { return _H1Tag; }
            set { _H1Tag = value; }
        }
        //-----------------------------------------------------------------
        public string SeoFooter
        {
            get { return _SeoFooter; }
            set { _SeoFooter = value; }
        }
        //-----------------------------------------------------------------
        public static List<MenuItemsView> Init(SmartDataReader smartReader, bool JoinWithMenu)
        {
            List<MenuItemsView> l_MenuItems = new List<MenuItemsView>();
            try
            {
                while (smartReader.Read())
                {
                    MenuItemsView m_MenuItems = new MenuItemsView();
                    m_MenuItems.MenuItemId = smartReader.GetInt32("MenuItemId");
                    m_MenuItems.MenuId = smartReader.GetInt32("MenuId");
                    m_MenuItems.ItemName = smartReader.GetString("ItemName");
                    m_MenuItems.ItemDesc = smartReader.GetString("ItemDesc");
                    m_MenuItems.IconPath = smartReader.GetString("IconPath");
                    m_MenuItems.Url = smartReader.GetString("Url");
                    m_MenuItems.ParentItemId = smartReader.GetInt32("ParentItemId");
                    m_MenuItems.ItemLevel = smartReader.GetByte("ItemLevel");
                    m_MenuItems.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_MenuItems.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_MenuItems.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MenuItems.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_MenuItems.MetaTitle = smartReader.GetString("MetaTitle");
                    m_MenuItems.MetaDesc = smartReader.GetString("MetaDesc");
                    m_MenuItems.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_MenuItems.CanonicalTag = smartReader.GetString("CanonicalTag");
                    m_MenuItems.H1Tag = smartReader.GetString("H1Tag");
                    m_MenuItems.SeoFooter = smartReader.GetString("SeoFooter");
                    if (JoinWithMenu) m_MenuItems.CategoryId = smartReader.GetInt16("CategoryId");

                    l_MenuItems.Add(m_MenuItems);
                }
                return l_MenuItems;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------------
        public static MenuItemsView GetByMenuItemId(int MenuItemId, List<MenuItemsView> list)
        {
            MenuItemsView RetVal = list.Find(i => i.MenuItemId == MenuItemId);
            return RetVal == null ? new MenuItemsView() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<MenuItemsView> GetListByMenuId(int MenuId, List<MenuItemsView> list)
        {
            List<MenuItemsView> RetVal = list.FindAll(i => i.MenuId == MenuId);
            return RetVal == null ? new List<MenuItemsView>() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<MenuItemsView> GetListByMenuId(short CategoryId, List<MenuItemsView> list)
        {
            List<MenuItemsView> RetVal = list.FindAll(i => i.CategoryId == CategoryId);
            return RetVal == null ? new List<MenuItemsView>() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<MenuItemsView> GetListByItemName(string ItemName, List<MenuItemsView> list)
        {
            List<MenuItemsView> RetVal = list.FindAll(i => i.ItemName == ItemName);
            return RetVal == null ? new List<MenuItemsView>() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<MenuItemsView> GetListByParentItemId(int ParentItemId, List<MenuItemsView> list)
        {
            List<MenuItemsView> RetVal = list.FindAll(i => i.ParentItemId == ParentItemId);
            return RetVal == null ? new List<MenuItemsView>() : RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetUrl()
        {
            string RetVal = _Url;
            if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl()
        {
            string RetVal = _IconPath;
            if (string.IsNullOrEmpty(_IconPath))
            {
                RetVal = CmsConstants.NO_IMAGE_URL;
            }
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
            return RetVal;
        }
    }
}