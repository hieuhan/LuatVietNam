
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class Currencies
    {
        private byte _CurrencyId;
        private string _CurrencyName;
        private string _CurrencyDesc;
        private string _CurrencyCode;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Currencies()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Currencies(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Currencies()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte CurrencyId
        {
            get { return _CurrencyId; }
            set { _CurrencyId = value; }
        }
        //-----------------------------------------------------------------
        public string CurrencyName
        {
            get { return _CurrencyName; }
            set { _CurrencyName = value; }
        }
        //-----------------------------------------------------------------
        public string CurrencyDesc
        {
            get { return _CurrencyDesc; }
            set { _CurrencyDesc = value; }
        }
        //-----------------------------------------------------------------
        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<Currencies> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Currencies> l_Currencies = new List<Currencies>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Currencies m_Currencies = new Currencies(db.ConnectionString);
                    m_Currencies.CurrencyId = smartReader.GetByte("CurrencyId");
                    m_Currencies.CurrencyName = smartReader.GetString("CurrencyName");
                    m_Currencies.CurrencyDesc = smartReader.GetString("CurrencyDesc");
                    m_Currencies.CurrencyCode = smartReader.GetString("CurrencyCode");
                    m_Currencies.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_Currencies.Add(m_Currencies);
                }
                reader.Close();
                return l_Currencies;
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
        public List<Currencies> GetList()
        {
            List<Currencies> RetVal = new List<Currencies>();
            try
            {
                string sql = "SELECT * FROM Currencies ORDER BY DisplayOrder, CurrencyName";
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
        public List<Currencies> GetList(string TextOptionAll = "...")
        {
            List<Currencies> RetVal = GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                Currencies m_Currencies = new Currencies();
                m_Currencies.CurrencyName = TextOptionAll;
                m_Currencies.CurrencyDesc = TextOptionAll;
                RetVal.Insert(0, m_Currencies);
            }
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<Currencies> GetListOrderBy(string OrderBy)
        {
            List<Currencies> RetVal = new List<Currencies>();
            try
            {
                string sql = "SELECT * FROM Currencies ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<Currencies> GetListOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            List<Currencies> RetVal = GetListOrderBy(OrderBy);
            if (TextOptionAll == null) TextOptionAll = "";
            if (TextOptionAll.Length > 0)
            {
                Currencies m_Currencies = new Currencies();
                m_Currencies.CurrencyName = TextOptionAll;
                m_Currencies.CurrencyDesc = TextOptionAll;
                RetVal.Insert(0, m_Currencies);
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Currencies> GetListByCurrencyId(byte CurrencyId)
        {
            List<Currencies> RetVal = new List<Currencies>();
            try
            {
                if (CurrencyId > 0)
                {
                    string sql = "SELECT * FROM Currencies WHERE (CurrencyId=" + CurrencyId.ToString() + ")";
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
        public Currencies Get(byte CurrencyId)
        {
            Currencies RetVal = new Currencies();
            try
            {
                List<Currencies> list = GetListByCurrencyId(CurrencyId);
                if (list.Count > 0)
                {
                    RetVal = (Currencies)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Currencies Get()
        {
            return Get(this.CurrencyId);
        }
        //--------------------------------------------------------------     
        public static Currencies Static_Get(byte CurrencyId, List<Currencies> list)
        {
            Currencies RetVal = new Currencies();
            RetVal = list.Find(i => i.CurrencyId == CurrencyId);
            if (RetVal == null) RetVal = new Currencies();
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Currencies Static_Get(string Constr, byte CurrencyId)
        {
            Currencies m_Currencies = new Currencies(Constr);
            return m_Currencies.Get(CurrencyId);
        }
        //-------------------------------------------------------------- 
        public static Currencies Static_Get(byte CurrencyId)
        {
            return Static_Get("", CurrencyId);
        }
        //-------------------------------------------------------------- 
        public static List<Currencies> Static_GetList(string ConStr)
        {
            List<Currencies> RetVal = new List<Currencies>();
            try
            {
                Currencies m_Currencies = new Currencies(ConStr);
                RetVal = m_Currencies.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Currencies> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
    }
}

