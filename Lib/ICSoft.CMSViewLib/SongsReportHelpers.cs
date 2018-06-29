using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;
using sms.database;
using sms.utils;
namespace ICSoft.CMSViewLib
{
    public class SongsReportHelpers
    {
        public static DataSet RBT_TopView(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_RBT_TopView");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet RBT_TopDownload(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_RBT_TopDownload");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet RBT_TopPresent(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_RBT_TopPresent");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet RBT_TopCopy(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_RBT_TopCopy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet ChannelRBT_TopDownload(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_ChannelRBT_TopDownload");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet AlbumRBT_TopDownload(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_AlbumRBT_TopDownload");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet Song_TopView(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_TopView");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet Song_TopDownload(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_TopDownload");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet Song_TopPresent(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_TopPresent");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet Song_TopCopy(byte AppTypeId, DateTime DateFrom, DateTime DateTo, int RowAmount)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_TopCopy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet Songs_Tonghop(byte AppTypeId, DateTime DateFrom, DateTime DateTo)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_Tonghop");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static DataSet RBT_PromotionDownload(byte AppTypeId, DateTime DateFrom, DateTime DateTo)
        {
            DataSet RetVal = new DataSet();
            try
            {
                DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
                SqlCommand cmd = new SqlCommand("Songs_Rpt_RBT_PromotionDownload");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AppTypeId", AppTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                RetVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
    }

}//end namespace service
