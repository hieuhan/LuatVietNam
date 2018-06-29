using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;

namespace ICSoft.CMSViewLib
{
    public class MenusViewHelpers
    {
        public static List<MenuItemsView> GetListMenuItemBySiteId(short SiteId)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<MenuItemsView> RetVal = new List<MenuItemsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("MenuItems_ViewGetBySiteId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = MenuItemsView.Init(smartReader, true);

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
        //------------------------------------------------
        public static List<MenuItemsView> GetListMenuItemByMenuId(int MenuId)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<MenuItemsView> RetVal = new List<MenuItemsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("MenuItems_ViewGetByMenuId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MenuId", MenuId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = MenuItemsView.Init(smartReader, true);

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
        //------------------------------------------------
        public static MenuItemsView GetByMenuItemId(int MenuItemId)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<MenuItemsView> RetVal = new List<MenuItemsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM MenuItems WHERE MenuItemId=" + MenuItemId.ToString());
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = MenuItemsView.Init(smartReader, true);

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
            return RetVal.Count > 0 ? RetVal[0] : new MenuItemsView();
        }
        //------------------------------------------------
        public static List<MenuItemsView> GetListMenuItemByParentId(int ParentId)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<MenuItemsView> RetVal = new List<MenuItemsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("MenuItems_ViewGetByParentId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ParentId", ParentId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = MenuItemsView.Init(smartReader, true);

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
    }
}
