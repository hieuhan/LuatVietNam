using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocGroups
    {
        public byte DocGroupId { get; set; }
        public string DocGroupName { get; set; }
        public string DocGroupDesc { get; set; }
        public int DocCount { get; set; }
        //-----------------------------------------------------------------
        public static List<DocGroups> Static_Init(SmartDataReader smartReader, byte GetDocCount = 0)
        {
            List<DocGroups> l_DocGroups = new List<DocGroups>();
            DocGroups m_DocGroups;
            while (smartReader.Read())
            {
                m_DocGroups = new DocGroups();
                m_DocGroups.DocGroupId = smartReader.GetByte("DocGroupId");
                m_DocGroups.DocGroupName = smartReader.GetString("DocGroupName");
                m_DocGroups.DocGroupDesc = smartReader.GetString("DocGroupDesc");
                if (GetDocCount == 1) m_DocGroups.DocCount = smartReader.GetInt32("Soluong");

                l_DocGroups.Add(m_DocGroups);
            }
            return l_DocGroups;
        }
        //-----------------------------------------------------------
        public static List<DocGroups> Static_GetList(string OrderBy = "DocGroupId")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<DocGroups> RetVal = new List<DocGroups>();
            try
            {
                string sql = "SELECT DocGroupId,DocGroupName,DocGroupDesc FROM DocGroups";
                sql = sql + " ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = DocGroups.Static_Init(smartReader);

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
        //-----------------------------------------------------------
        public static DocGroups Static_Get(byte DocGroupId = 0)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<DocGroups> list = new List<DocGroups>();
            DocGroups RetVal = new DocGroups();
            try
            {
                string sql = "SELECT DocGroupId,DocGroupName,DocGroupDesc FROM DocGroups Where DocGroupId=" +DocGroupId;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                list = DocGroups.Static_Init(smartReader);
                if (list.Count > 0)
                    RetVal = list[0];
                else
                    RetVal = new DocGroups();
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
