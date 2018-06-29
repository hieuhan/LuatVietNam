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
    public class AdvertsViewHelpers
    {
        public static List<AdvertsView> GetListBySiteId(short SiteId)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<AdvertsView> RetVal = new List<AdvertsView>();
            try
            {
                string sql = "SELECT * FROM Adverts WHERE SiteId=" + SiteId.ToString() + " AND AdvertStatusId=1 ORDER BY AdvertId DESC";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = AdvertsView.Init(smartReader);

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
        //---------------------------------------- 
        public static List<AdvertsView> GetListByAdvertPositionId(int AdvertPositionId, short CategoryId)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<AdvertsView> RetVal = new List<AdvertsView>();
            try
            {
                string sql = "SELECT A.* FROM Adverts A INNER JOIN AdvertPositionAdverts P ON A.AdvertId=P.AdvertId WHERE (AdvertPositionId=" + AdvertPositionId.ToString() + " AND AdvertStatusId=1) ORDER BY DisplayOrder";
                if (CategoryId > 0)
                {
                    sql = "SELECT A.* FROM Adverts A INNER JOIN AdvertPositionAdverts P ON A.AdvertId=P.AdvertId WHERE (AdvertPositionId=" + AdvertPositionId.ToString() + " AND AdvertStatusId=1 AND CategoryId=" + CategoryId.ToString() + ") ORDER BY DisplayOrder";
                }
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = AdvertsView.Init(smartReader);

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
