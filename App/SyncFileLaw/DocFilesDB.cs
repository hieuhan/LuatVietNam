using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SyncMediaFile
{
    public class DocFilesDB
    {
        public void UpdateUpdateSyncStatusOK(int DocFileId)
        {
            SqlConnection con=new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            SqlCommand cmd = new SqlCommand("DocFiles_UpdateSyncStatusOK", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DocFileId", DocFileId));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }

        public void UpdateSyncStatus(int DocFileId, byte SyncStatusId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            string sql = "UPDATE DocFiles SET SyncStatusId=" + SyncStatusId.ToString() + " WHERE DocFileId=" + DocFileId.ToString();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }
        public void UpdateSyncServerCount(int DocFileId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            string sql = "UPDATE DocFiles SET SyncServerCount=SyncServerCount+1 WHERE DocFileId=" + DocFileId.ToString();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }
        public void ResetSyncServerCount(int DocFileId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            string sql = "UPDATE DocFiles SET SyncServerCount=0 WHERE DocFileId=" + DocFileId.ToString();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }
        public DataSet GetFilesNotSync()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            SqlCommand cmd = new SqlCommand("SELECT TOP(100) DocFileId,FilePath,FileTypeId FROM DocFiles WHERE SyncStatusId=0 ORDER BY DocFileId DESC", con);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataSet result = new DataSet();
            con.Open();
            adt.Fill(result);
            con.Close();
            con.Dispose();
            return result;
        }

        public DataSet GetFilesById(int DocFileId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            SqlCommand cmd = new SqlCommand("SELECT TOP(100) DocFileId,FilePath,FileTypeId,SyncServerCount FROM DocFiles WHERE DocFileId=" + DocFileId.ToString(), con);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataSet result = new DataSet();
            con.Open();
            adt.Fill(result);
            con.Close();
            con.Dispose();
            return result;
        }
    }
}
