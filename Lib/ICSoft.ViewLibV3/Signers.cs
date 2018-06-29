using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Signers
    {
        public short SignerId { get; set; }
        public string SignerName { get; set; }
        public string SignerNameClear { get; set; }
        //-----------------------------------------------------------------
        public static List<Signers> Static_Init(SmartDataReader smartReader)
        {
            List<Signers> l_Signers = new List<Signers>();
            Signers m_Signers;
            while (smartReader.Read())
            {
                m_Signers = new Signers();
                m_Signers.SignerId = smartReader.GetInt16("SignerId");
                m_Signers.SignerName = smartReader.GetString("SignerName");
                m_Signers.SignerNameClear = smartReader.GetString("SignerNameClear");

                l_Signers.Add(m_Signers);
            }
            return l_Signers;
        }
        //-----------------------------------------------------------------
        public static Signers Static_InitOne(SmartDataReader smartReader)
        {
            List<Signers> l_Signers = Static_Init(smartReader);
            if (l_Signers.Count > 0) return l_Signers[0];
            return null;
        }
        //-----------------------------------------------------------------
        public static Signers Static_GetById(short SignerId, List<Signers> list)
        {
            Signers RetVal = list.Find(i => i.SignerId == SignerId);
            return RetVal == null ? new Signers() : RetVal;
        }
        //-----------------------------------------------------------
        public static List<Signers> Static_GetList(string Keyword = "", int RowAmount = 10, byte LanguageId = 1)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<Signers> RetVal = new List<Signers>();
            try
            {
                string sql = "SELECT TOP(" + RowAmount.ToString() + ") SignerId," + (LanguageId == 1 ? "SignerName" : "SignerNameClear") + " FROM Signers WHERE (1=1)";
                if (Keyword != "") sql = sql + " AND (SignerName LIKE N'%" + StringUtil.injectionString(Keyword) + "%' OR SignerNameClear LIKE N'%" + StringUtil.injectionString(Keyword) + "%')";
                sql = sql + " ORDER BY SignerName";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Signers.Static_Init(smartReader);

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
