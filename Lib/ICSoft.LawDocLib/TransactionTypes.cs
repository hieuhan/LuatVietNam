
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class TransactionTypes
    {
        private byte _TransactionTypeId;
        private string _TransactionTypeName;
        private string _TransactionTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public TransactionTypes()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public TransactionTypes(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~TransactionTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte TransactionTypeId
        {
            get { return _TransactionTypeId; }
            set { _TransactionTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string TransactionTypeName
        {
            get { return _TransactionTypeName; }
            set { _TransactionTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string TransactionTypeDesc
        {
            get { return _TransactionTypeDesc; }
            set { _TransactionTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<TransactionTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<TransactionTypes> l_TransactionTypes = new List<TransactionTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    TransactionTypes m_TransactionTypes = new TransactionTypes(db.ConnectionString);
                    m_TransactionTypes.TransactionTypeId = smartReader.GetByte("TransactionTypeId");
                    m_TransactionTypes.TransactionTypeName = smartReader.GetString("TransactionTypeName");
                    m_TransactionTypes.TransactionTypeDesc = smartReader.GetString("TransactionTypeDesc");

                    l_TransactionTypes.Add(m_TransactionTypes);
                }
                reader.Close();
                return l_TransactionTypes;
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
        public List<TransactionTypes> GetList()
        {
            List<TransactionTypes> RetVal = new List<TransactionTypes>();
            try
            {
                string sql = "SELECT * FROM TransactionTypes ORDER BY TransactionTypeName";
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
        public List<TransactionTypes> GetListByTransactionTypeId(byte TransactionTypeId)
        {
            List<TransactionTypes> RetVal = new List<TransactionTypes>();
            try
            {
                if (TransactionTypeId > 0)
                {
                    string sql = "SELECT * FROM TransactionTypes WHERE (TransactionTypeId=" + TransactionTypeId.ToString() + ")";
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
        public TransactionTypes Get(byte TransactionTypeId)
        {
            TransactionTypes RetVal = new TransactionTypes();
            try
            {
                List<TransactionTypes> list = GetListByTransactionTypeId(TransactionTypeId);
                if (list.Count > 0)
                {
                    RetVal = (TransactionTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static TransactionTypes Static_Get(byte TransactionTypeId, List<TransactionTypes> list)
        {
            TransactionTypes RetVal = new TransactionTypes();
            try
            {
                RetVal = list.Find(i => i.TransactionTypeId == TransactionTypeId);
                if (RetVal == null) RetVal = new TransactionTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public TransactionTypes Get()
        {
            return Get(this.TransactionTypeId);
        }
        //-------------------------------------------------------------- 
        public static TransactionTypes Static_Get(byte TransactionTypeId)
        {
            return new TransactionTypes().Get(TransactionTypeId);
        }
        //--------------------------------------------------------------     
        public static List<TransactionTypes> Static_GetList()
        {
            return new TransactionTypes().GetList();
        }
    }
}

