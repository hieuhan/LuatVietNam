using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Seos
    {
        public int SeoId { get; set; }
        public string SeoName { get; set; }
        public string Url { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeyword { get; set; }
        public string CanonicalTag { get; set; }
        public string H1Tag { get; set; }
        public string SeoFooter { get; set; }
        private DBAccess db;
        //-----------------------------------------------------------------
        public Seos()
        {
            db = new DBAccess(Constants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Seos(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Seos()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------
        private List<Seos> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Seos> l_Seos = new List<Seos>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Seos m_Seos = new Seos(db.ConnectionString);
                    m_Seos.SeoId = smartReader.GetInt32("SeoId");
                    m_Seos.SeoName = smartReader.GetString("SeoName");
                    m_Seos.Url = smartReader.GetString("Url");
                    m_Seos.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Seos.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Seos.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Seos.CanonicalTag = smartReader.GetString("CanonicalTag");
                    m_Seos.H1Tag = smartReader.GetString("H1Tag");
                    m_Seos.SeoFooter = smartReader.GetString("SeoFooter");

                    l_Seos.Add(m_Seos);
                }
                reader.Close();
                return l_Seos;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
        }
        //--------------------------------------------------------------     
        public Seos GetDataAPI(int SeoId, string SeoName, string Url)
        {
            List<Seos> RetVal = new List<Seos>();
            try
            {
                SqlCommand cmd = new SqlCommand("Seos_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SeoId", SeoId));
                cmd.Parameters.Add(new SqlParameter("@SeoName", SeoName));
                cmd.Parameters.Add(new SqlParameter("@Url", Url));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal.Count > 0 ? RetVal[0] : new Seos();
        }
        //--------------------------------------------------------------     
        public static Seos Static_GetBySeoId(int SeoId)
        {
            return new Seos().GetDataAPI(SeoId, "", "");
        }
        //--------------------------------------------------------------     
        public static Seos Static_GetBySeoName(string SeoName)
        {
            return new Seos().GetDataAPI(0, SeoName, "");
        }
        //--------------------------------------------------------------     
        public static Seos Static_GetByUrl(string Url)
        {
            return new Seos().GetDataAPI(0, "", Url);
        }
    }
}
