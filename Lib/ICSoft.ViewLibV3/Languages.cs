using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Languages
    {
        public byte LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string LanguageDesc { get; set; }
        //-----------------------------------------------------------------
        public static List<Languages> Static_Init(SmartDataReader smartReader)
        {
            List<Languages> l_Languages = new List<Languages>();
            Languages m_Languages;
            while (smartReader.Read())
            {
                m_Languages = new Languages();
                m_Languages.LanguageId = smartReader.GetByte("LanguageId");
                m_Languages.LanguageName = smartReader.GetString("LanguageName");
                m_Languages.LanguageDesc = smartReader.GetString("LanguageDesc");

                l_Languages.Add(m_Languages);
            }
            return l_Languages;
        }
        //-----------------------------------------------------------
        public static List<Languages> Static_GetList(string OrderBy = "LanguageDesc")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<Languages> RetVal = new List<Languages>();
            try
            {
                string sql = "SELECT LanguageId,LanguageName,LanguageDesc FROM Languages";
                sql = sql + " ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Static_Init(smartReader);

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
