using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Provinces
    {
        public short ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceDesc { get; set; }
        public int DocCount { get; set; }
        //-----------------------------------------------------------------
        public static List<Provinces> Static_Init(SmartDataReader smartReader, byte GetDocCount = 0)
        {
            List<Provinces> l_Provinces = new List<Provinces>();
            Provinces m_Provinces;
            while (smartReader.Read())
            {
                m_Provinces = new Provinces();
                m_Provinces.ProvinceId = smartReader.GetInt16("ProvinceId");
                m_Provinces.ProvinceName = smartReader.GetString("ProvinceName");
                m_Provinces.ProvinceDesc = smartReader.GetString("ProvinceDesc");
                if (GetDocCount == 1) m_Provinces.DocCount = smartReader.GetInt32("Soluong");

                l_Provinces.Add(m_Provinces);
            }
            return l_Provinces;
        }
        //-----------------------------------------------------------------
        public static Provinces Static_InitOne(SmartDataReader smartReader)
        {
            List<Provinces> l_Provinces = Static_Init(smartReader);
            if (l_Provinces.Count > 0) return l_Provinces[0];
            return null;
        }
        //-----------------------------------------------------------------
        public static Provinces Static_GetById(short ProvinceId, List<Provinces> list)
        {
            Provinces RetVal = list.Find(i => i.ProvinceId == ProvinceId);
            return RetVal == null ? new Provinces() : RetVal;
        }
        //-----------------------------------------------------------
        public static List<Provinces> Static_GetList(short CountryId = 1, string OrderBy = "DisplayOrder")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<Provinces> RetVal = new List<Provinces>();
            try
            {
                string sql = "SELECT ProvinceId,ProvinceName,ProvinceDesc FROM Provinces WHERE (1=1)";
                if (CountryId > 0) sql = sql + " AND (CountryId=" + CountryId.ToString() + ")";
                sql = sql + " ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Provinces.Static_Init(smartReader, 0);

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
