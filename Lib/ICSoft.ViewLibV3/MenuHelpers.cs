using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class MenuHelpers
    {
        public static List<MenuItems> Init(SmartDataReader smartReader, string FieldSelect = "*")
        {
            List<MenuItems> l_MenuItems = new List<MenuItems>();
            MenuItems m_MenuItems;
            while (smartReader.Read())
            {
                m_MenuItems = new MenuItems();
                if (FieldSelect == "*" || FieldSelect.Contains("MenuItemId")) m_MenuItems.MenuItemId = smartReader.GetInt32("MenuItemId");
                if (FieldSelect == "*" || FieldSelect.Contains("MenuId")) m_MenuItems.MenuId = smartReader.GetInt32("MenuId");
                if (FieldSelect == "*" || FieldSelect.Contains("ItemName")) m_MenuItems.ItemName = smartReader.GetString("ItemName");
                if (FieldSelect == "*" || FieldSelect.Contains("ItemDesc")) m_MenuItems.ItemDesc = smartReader.GetString("ItemDesc");
                if (FieldSelect == "*" || FieldSelect.Contains("IconPath")) m_MenuItems.IconPath = smartReader.GetString("IconPath");
                if (FieldSelect == "*" || FieldSelect.Contains("Url")) m_MenuItems.Url = smartReader.GetString("Url");
                if (FieldSelect == "*" || FieldSelect.Contains("ParentItemId")) m_MenuItems.ParentItemId = smartReader.GetInt32("ParentItemId");
                if (FieldSelect == "*" || FieldSelect.Contains("ItemLevel")) m_MenuItems.ItemLevel = smartReader.GetByte("ItemLevel");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaTitle")) m_MenuItems.MetaTitle = smartReader.GetString("MetaTitle");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaDesc")) m_MenuItems.MetaDesc = smartReader.GetString("MetaDesc");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaKeyword")) m_MenuItems.MetaKeyword = smartReader.GetString("MetaKeyword");
                if (FieldSelect == "*" || FieldSelect.Contains("CanonicalTag")) m_MenuItems.CanonicalTag = smartReader.GetString("CanonicalTag");
                if (FieldSelect == "*" || FieldSelect.Contains("H1Tag")) m_MenuItems.H1Tag = smartReader.GetString("H1Tag");
                if (FieldSelect == "*" || FieldSelect.Contains("SeoFooter")) m_MenuItems.SeoFooter = smartReader.GetString("SeoFooter");

                l_MenuItems.Add(m_MenuItems);
            }
            return l_MenuItems;
        }
        //-----------------------------------------------------------
        public static MenuItems GetMenuItemById(int MenuItemId, string FieldSelect = "MenuItemId,ItemName,ItemDesc,Url,MetaTitle,MetaDesc,MetaKeyword,CanonicalTag,H1Tag,SeoFooter")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<MenuItems> RetVal = new List<MenuItems>();
            try
            {
                SqlCommand cmd = new SqlCommand("MenuItems_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FieldSelect", FieldSelect));
                cmd.Parameters.Add(new SqlParameter("@MenuItemId", MenuItemId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Init(smartReader, FieldSelect);

                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal.Count > 0 ? RetVal[0] : new MenuItems();
        }
        //-----------------------------------------------------------------
        public static MenuItems InitOne(SmartDataReader smartReader, string FieldSelect = "*")
        {
            List<MenuItems> l_MenuItems = Init(smartReader, FieldSelect);
            if (l_MenuItems.Count > 0) return l_MenuItems[0];
            return null;
        }
        //-----------------------------------------------------------
        public static List<MenuItems> GetListMenuItem(int MenuId, short SiteId = 0, short CategoryId = 0, string FieldSelect = "MenuItemId,MenuId,ParentItemId,ItemName,ItemDesc,IconPath,Url")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<MenuItems> RetVal = new List<MenuItems>();
            try
            {
                SqlCommand cmd = new SqlCommand("MenuItems_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FieldSelect", FieldSelect));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@MenuId", MenuId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Init(smartReader, FieldSelect);

                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public static MenuItems GetByMenuItemId(int MenuItemId, List<MenuItems> list)
        {
            MenuItems RetVal = list.Find(i => i.MenuItemId == MenuItemId);
            return RetVal == null ? new MenuItems() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<MenuItems> GetListByMenuId(int MenuId, List<MenuItems> list)
        {
            List<MenuItems> RetVal = list.FindAll(i => i.MenuId == MenuId);
            return RetVal == null ? new List<MenuItems>() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<MenuItems> GetListByItemName(string ItemName, List<MenuItems> list)
        {
            List<MenuItems> RetVal = list.FindAll(i => i.ItemName == ItemName);
            return RetVal == null ? new List<MenuItems>() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<MenuItems> GetListByParentItemId(int ParentItemId, List<MenuItems> list)
        {
            List<MenuItems> RetVal = list.FindAll(i => i.ParentItemId == ParentItemId);
            return RetVal == null ? new List<MenuItems>() : RetVal;
        }
    }
}
