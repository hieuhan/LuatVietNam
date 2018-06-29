
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class Telcos
    {
        private byte _TelcoId;
        private string _TelcoName;
        private string _TelcoDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Telcos()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Telcos(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Telcos()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte TelcoId
        {
            get { return _TelcoId; }
            set { _TelcoId = value; }
        }
        //-----------------------------------------------------------------
        public string TelcoName
        {
            get { return _TelcoName; }
            set { _TelcoName = value; }
        }
        //-----------------------------------------------------------------
        public string TelcoDesc
        {
            get { return _TelcoDesc; }
            set { _TelcoDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<Telcos> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Telcos> l_Telcos = new List<Telcos>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Telcos m_Telcos = new Telcos(db.ConnectionString);
                    m_Telcos.TelcoId = smartReader.GetByte("TelcoId");
                    m_Telcos.TelcoName = smartReader.GetString("TelcoName");
                    m_Telcos.TelcoDesc = smartReader.GetString("TelcoDesc");

                    l_Telcos.Add(m_Telcos);
                }
                reader.Close();
                return l_Telcos;
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
        //-----------------------------------------------------------
        public List<Telcos> GetList()
        {
            List<Telcos> RetVal = new List<Telcos>();
            try
            {
                string sql = "SELECT * FROM Telcos";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Telcos> GetListByTelcoId(byte TelcoId)
        {
            List<Telcos> RetVal = new List<Telcos>();
            try
            {
                if (TelcoId > 0)
                {
                    string sql = "SELECT * FROM Telcos WHERE (TelcoId=" + TelcoId.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = Init(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public Telcos Get(byte TelcoId)
        {
            Telcos RetVal = new Telcos();
            try
            {
                List<Telcos> list = GetListByTelcoId(TelcoId);
                if (list.Count > 0)
                {
                    RetVal = (Telcos)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Telcos Static_Get(byte TelcoId, List<Telcos> list)
        {
            Telcos RetVal = new Telcos();
            try
            {
                RetVal = list.Find(i => i.TelcoId == TelcoId);
                if (RetVal == null) RetVal = new Telcos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Telcos Get()
        {
            return Get(this.TelcoId);
        }
        //-------------------------------------------------------------- 
        public static Telcos Static_Get(byte TelcoId)
        {
            return new Telcos().Get(TelcoId);
        }
        //-------------------------------------------------------------- 
        public static List<Telcos> Static_GetList()
        {
            return new Telcos().GetList();
        }
        //--------------------------------------------------------------     
    }
}

