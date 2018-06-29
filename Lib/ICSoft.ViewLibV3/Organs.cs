using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Organs
    {
        public short OrganId { get; set; }
        public string OrganName { get; set; }
        public string OrganDesc { get; set; }
        public int DocCount { get; set; }
        //-----------------------------------------------------------------
        public static List<Organs> Static_Init(SmartDataReader smartReader, byte GetDocCount = 0)
        {
            List<Organs> l_Organs = new List<Organs>();
            Organs m_Organs;
            while (smartReader.Read())
            {
                m_Organs = new Organs();
                m_Organs.OrganId = smartReader.GetInt16("OrganId");
                m_Organs.OrganName = smartReader.GetString("OrganName");
                m_Organs.OrganDesc = smartReader.GetString("OrganDesc");
                if (GetDocCount == 1) m_Organs.DocCount = smartReader.GetInt32("Soluong");

                l_Organs.Add(m_Organs);
            }
            return l_Organs;
        }
        //-----------------------------------------------------------------
        public static Organs Static_InitOne(SmartDataReader smartReader)
        {
            List<Organs> l_Organs = Static_Init(smartReader);
            if (l_Organs.Count > 0) return l_Organs[0];
            return null;
        }
        //-----------------------------------------------------------------
        public static Organs Static_GetById(short OrganId, List<Organs> list)
        {
            Organs RetVal = list.Find(i => i.OrganId == OrganId);
            return RetVal == null ? new Organs() : RetVal;
        }
        //-----------------------------------------------------------
        public static List<Organs> Static_GetList(byte ReviewStatusId = 2, byte LanguageId = 1, string FieldSelect = "OrganId,OrganDesc", string OrderBy = "DisplayOrder")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<Organs> RetVal = new List<Organs>();
            try
            {
                string sql = "SELECT " + FieldSelect + " FROM " + (LanguageId == 1 ? "Organs" : "OrganLanguages") + " WHERE (1=1)";
                if (ReviewStatusId > 0) sql = sql + " AND (ReviewStatusId=" + ReviewStatusId.ToString() + ")";
                if (LanguageId > 1) sql = sql + " AND (LanguageId=" + LanguageId.ToString() + ")";
                sql = sql + " ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Organs.Static_Init(smartReader, 0);

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
        public static List<Organs> Static_GetListByDisplayType(short DisplayTypeId, byte ReviewStatusId = 0, byte LanguageId = 1, string FieldSelect = "OrganId,OrganDesc", string OrderBy = "DisplayOrder")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<Organs> RetVal = new List<Organs>();
            try
            {
                FieldSelect = "A." + FieldSelect;
                FieldSelect = FieldSelect.Replace(" ", "").Replace(",", ",A.");
                OrderBy = OrderBy.Replace("DisplayOrder", "B.DisplayOrder");
                OrderBy = OrderBy.Replace("OrganId", "A.OrganId");
                string sql = "SELECT " + FieldSelect + " FROM " + (LanguageId == 1 ? "Organs" : "OrganLanguages") + " A";
                sql = sql + " INNER JOIN OrganDisplays B ON A.OrganId=B.OrganId";
                sql = sql + " WHERE DisplayTypeId=" + DisplayTypeId.ToString();
                if (ReviewStatusId > 0) sql = sql + " AND (ReviewStatusId=" + ReviewStatusId.ToString() + ")";
                if (LanguageId > 1) sql = sql + " AND (B.LanguageId=" + LanguageId.ToString() + ")";
                sql = sql + " ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Organs.Static_Init(smartReader, 0);

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
        public static Organs Static_GetById(short OrganId, byte LanguageId = 1)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            Organs RetVal = new Organs();
            try
            {
                string sql = "SELECT OrganId,OrganName,OrganDesc FROM " + (LanguageId == 1 ? "Organs" : "OrganLanguages") + " WHERE (OrganId=" + OrganId.ToString() + ")";
                if (LanguageId > 1) sql = sql + " AND (LanguageId=" + LanguageId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Organs.Static_InitOne(smartReader);

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
