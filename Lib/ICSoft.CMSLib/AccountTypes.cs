
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
    public class AccountTypes
    {
        private byte _AccountTypeId;
        private string _AccountTypeName;
        private string _AccountTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public AccountTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public AccountTypes(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~AccountTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte AccountTypeId
        {
            get { return _AccountTypeId; }
            set { _AccountTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string AccountTypeName
        {
            get { return _AccountTypeName; }
            set { _AccountTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string AccountTypeDesc
        {
            get { return _AccountTypeDesc; }
            set { _AccountTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<AccountTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<AccountTypes> l_AccountTypes = new List<AccountTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    AccountTypes m_AccountTypes = new AccountTypes(db.ConnectionString);
                    m_AccountTypes.AccountTypeId = smartReader.GetByte("AccountTypeId");
                    m_AccountTypes.AccountTypeName = smartReader.GetString("AccountTypeName");
                    m_AccountTypes.AccountTypeDesc = smartReader.GetString("AccountTypeDesc");

                    l_AccountTypes.Add(m_AccountTypes);
                }
                reader.Close();
                return l_AccountTypes;
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
        public List<AccountTypes> GetList()
        {
            List<AccountTypes> RetVal = new List<AccountTypes>();
            try
            {
                string sql = "SELECT * FROM AccountTypes";
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
        public List<AccountTypes> GetListOrderBy(string OrderBy)
        {
            List<AccountTypes> RetVal = new List<AccountTypes>();
            try
            {
                string sql = "SELECT * FROM AccountTypes ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<AccountTypes> GetListByAccountTypeId(byte AccountTypeId)
        {
            List<AccountTypes> RetVal = new List<AccountTypes>();
            try
            {
                if (AccountTypeId > 0)
                {
                    string sql = "SELECT * FROM AccountTypes WHERE (AccountTypeId=" + AccountTypeId.ToString() + ")";
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
        public AccountTypes Get(byte AccountTypeId)
        {
            AccountTypes RetVal = new AccountTypes();
            try
            {
                List<AccountTypes> list = GetListByAccountTypeId(AccountTypeId);
                if (list.Count > 0)
                {
                    RetVal = (AccountTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static AccountTypes Static_Get(byte AccountTypeId, List<AccountTypes> list)
        {
            AccountTypes RetVal = new AccountTypes();
            try
            {
                RetVal = list.Find(i => i.AccountTypeId == AccountTypeId);
                if (RetVal == null) RetVal = new AccountTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public AccountTypes Get()
        {
            return Get(this.AccountTypeId);
        }
        //-------------------------------------------------------------- 
        public static AccountTypes Static_Get(byte AccountTypeId)
        {
            return Static_Get(AccountTypeId);
        }
        //--------------------------------------------------------------     
        public static List<AccountTypes> Static_GetList()
        {
            List<AccountTypes> RetVal = new List<AccountTypes>();
            try
            {
                AccountTypes m_AccountTypes = new AccountTypes();
                RetVal = m_AccountTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

