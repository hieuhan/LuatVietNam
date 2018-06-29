using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class EffectStatus
    {
        public byte EffectStatusId { get; set; }
        public string EffectStatusName { get; set; }
        public string EffectStatusDesc { get; set; }
        public int DocCount { get; set; }
        //-----------------------------------------------------------------
        public static List<EffectStatus> Static_Init(SmartDataReader smartReader, byte GetDocCount = 0)
        {
            List<EffectStatus> l_EffectStatus = new List<EffectStatus>();
            EffectStatus m_EffectStatus;
            while (smartReader.Read())
            {
                m_EffectStatus = new EffectStatus();
                m_EffectStatus.EffectStatusId = smartReader.GetByte("EffectStatusId");
                m_EffectStatus.EffectStatusName = smartReader.GetString("EffectStatusName");
                m_EffectStatus.EffectStatusDesc = smartReader.GetString("EffectStatusDesc");
                if (GetDocCount == 1) m_EffectStatus.DocCount = smartReader.GetInt32("Soluong");

                l_EffectStatus.Add(m_EffectStatus);
            }
            return l_EffectStatus;
        }
        //-----------------------------------------------------------------
        public static EffectStatus Static_InitOne(SmartDataReader smartReader)
        {
            List<EffectStatus> l_EffectStatus = Static_Init(smartReader);
            if (l_EffectStatus.Count > 0) return l_EffectStatus[0];
            return null;
        }
        //-----------------------------------------------------------------
        public static EffectStatus Static_GetById(byte EffectStatusId, List<EffectStatus> list)
        {
            EffectStatus RetVal = list.Find(i => i.EffectStatusId == EffectStatusId);
            return RetVal == null ? new EffectStatus() : RetVal;
        }
        //-----------------------------------------------------------
        public static List<EffectStatus> Static_GetList(string OrderBy = "DisplayOrder")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<EffectStatus> RetVal = new List<EffectStatus>();
            try
            {
                string sql = "SELECT EffectStatusId,EffectStatusName,EffectStatusDesc FROM EffectStatus";
                sql = sql + " ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = EffectStatus.Static_Init(smartReader);

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
        public static EffectStatus Static_GetById(byte EffectStatusId)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            EffectStatus RetVal = new EffectStatus();
            try
            {
                string sql = "SELECT EffectStatusId,EffectStatusName,EffectStatusDesc FROM EffectStatus WHERE (EffectStatusId=" + EffectStatusId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = EffectStatus.Static_InitOne(smartReader);

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
