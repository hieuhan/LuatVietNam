using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SyncMediaFile
{
    public class MediasDB
    {
        public void UpdateUpdateSyncStatusOK(int MediaId)
        {
            SqlConnection con=new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            SqlCommand cmd = new SqlCommand("Medias_UpdateSyncStatusOK", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MediaId", MediaId));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }

        public void UpdateSyncStatus(int MediaId, byte SyncStatusId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            string sql = "UPDATE Medias SET SyncStatusId=" + SyncStatusId.ToString() + " WHERE MediaId=" + MediaId.ToString();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }
        public void UpdateSyncServerCount(int MediaId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            string sql = "UPDATE Medias SET SyncServerCount=SyncServerCount+1 WHERE MediaId=" + MediaId.ToString();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }
        public void ResetSyncServerCount(int MediaId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            string sql = "UPDATE Medias SET SyncServerCount=0 WHERE MediaId=" + MediaId.ToString();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }
        public DataSet GetFilesNotSync()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            SqlCommand cmd = new SqlCommand("SELECT TOP(100) MediaId,FileName,MediaTypeId FROM Medias WHERE SyncStatusId=0 ORDER BY MediaId DESC", con);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataSet result = new DataSet();
            con.Open();
            adt.Fill(result);
            con.Close();
            con.Dispose();
            return result;
        }

        public DataSet GetFilesById(int MediaId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStr"].ToString());
            SqlCommand cmd = new SqlCommand("SELECT TOP(100) MediaId,FileName,MediaTypeId,SyncServerCount FROM Medias WHERE MediaId=" + MediaId.ToString(), con);
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
