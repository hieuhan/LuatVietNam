
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
    public class CustomerDocs
    {
        private int _CustomerDocId;
        private int _CustomerId;
        private int _DocId;
        private byte _LanguageId;
        private DateTime _CrDateTime;
        private DBAccess db;

        private byte _registTypeId;

        //-----------------------------------------------------------------
        public CustomerDocs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerDocs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CustomerDocs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CustomerDocId
        {
            get { return _CustomerDocId; }
            set { _CustomerDocId = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }
        //-----------------------------------------------------------------
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }

        public byte RegistTypeId
        {
            get { return _registTypeId; }
            set { _registTypeId = value; }
        }
        //-----------------------------------------------------------------

        private List<CustomerDocs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerDocs> l_CustomerDocs = new List<CustomerDocs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerDocs m_CustomerDocs = new CustomerDocs(db.ConnectionString);
                    m_CustomerDocs.CustomerDocId = smartReader.GetInt32("CustomerDocId");
                    m_CustomerDocs.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerDocs.DocId = smartReader.GetInt32("DocId");
                    m_CustomerDocs.LanguageId = smartReader.GetByte("LanguageId");
                    m_CustomerDocs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_CustomerDocs.Add(m_CustomerDocs);
                }
                reader.Close();
                return l_CustomerDocs;
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
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerDocs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@RegistTypeId", this.RegistTypeId));
                cmd.Parameters.Add("@CustomerDocId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerDocId = Convert.ToInt32((cmd.Parameters["@CustomerDocId"].Value == null) ? 0 : (cmd.Parameters["@CustomerDocId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerDocs_DeleteBy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte DeleteList(int ActUserId,int CustomerId, string ListDocId, int LanguageId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerDocs_DeleteByListDocId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@ListDocId", ListDocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<CustomerDocs> CustomerDocs_GetList(int CustomerId)
        {
            List<CustomerDocs> RetVal = new List<CustomerDocs>();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerDocs_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));     
                RetVal = Init(cmd);                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataSet CustomerDocs_GetCount(int CustomerId, byte LanguageId, int DocGroupId, byte RegistTypeId, byte GetCountByRegistType, byte GetCountByDocGroup, byte GetCountByField, byte GetCountByEffectStatus)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerDocs_GetCount");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                cmd.Parameters.Add(new SqlParameter("@GetCountByRegistType", GetCountByRegistType));
                cmd.Parameters.Add(new SqlParameter("@GetCountByDocGroup", GetCountByDocGroup));
                cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", GetCountByEffectStatus));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static DataTable Static_GetCount_GetListDocGroup(DataSet ds)
        {
            DataTable RetVal = new DataTable();
            try
            {
                RetVal = ds.Tables[1];
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static DataTable Static_GetCount_GetListField(DataSet ds)
        {
            DataTable RetVal = new DataTable();
            try
            {
                RetVal = ds.Tables[3];
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static DataTable Static_GetCount_GetListEffectStatus(DataSet ds)
        {
            DataTable RetVal = new DataTable();
            try
            {
                RetVal = ds.Tables[4];
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static int Static_GetCount_CountByDocGroupId(DataSet ds, int DocGroupId)
        {
            int RetVal = 0;
            try
            {
                DataRow[] m_Rows = ds.Tables[1].Select("DocGroupId=" + DocGroupId.ToString());
                if (m_Rows.Length > 0) RetVal = m_Rows[0].Field<int>("Soluong");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static int Static_GetCount_CountByRegistTypeId(DataSet ds, int RegistTypeId)
        {
            int RetVal = 0;
            try
            {
                DataRow[] m_Rows = ds.Tables[2].Select("RegistTypeId=" + RegistTypeId.ToString());
                if (m_Rows.Length > 0) RetVal = m_Rows[0].Field<int>("Soluong");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

