
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
    public class TransactionStatus
    {
        private byte _TransactionStatusId;
        private string _TransactionStatusName;
        private string _TransactionStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public TransactionStatus()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public TransactionStatus(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~TransactionStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte TransactionStatusId
        {
            get { return _TransactionStatusId; }
            set { _TransactionStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string TransactionStatusName
        {
            get { return _TransactionStatusName; }
            set { _TransactionStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string TransactionStatusDesc
        {
            get { return _TransactionStatusDesc; }
            set { _TransactionStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<TransactionStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<TransactionStatus> l_TransactionStatus = new List<TransactionStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    TransactionStatus m_TransactionStatus = new TransactionStatus(db.ConnectionString);
                    m_TransactionStatus.TransactionStatusId = smartReader.GetByte("TransactionStatusId");
                    m_TransactionStatus.TransactionStatusName = smartReader.GetString("TransactionStatusName");
                    m_TransactionStatus.TransactionStatusDesc = smartReader.GetString("TransactionStatusDesc");

                    l_TransactionStatus.Add(m_TransactionStatus);
                }
                reader.Close();
                return l_TransactionStatus;
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
                SqlCommand cmd = new SqlCommand("TransactionStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusName", this.TransactionStatusName));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusDesc", this.TransactionStatusDesc));
                cmd.Parameters.Add("@TransactionStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.TransactionStatusId = Convert.ToByte((cmd.Parameters["@TransactionStatusId"].Value == null) ? 0 : (cmd.Parameters["@TransactionStatusId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("TransactionStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusName", this.TransactionStatusName));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusDesc", this.TransactionStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", this.TransactionStatusId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("TransactionStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", this.TransactionStatusId));
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
        public List<TransactionStatus> GetList()
        {
            List<TransactionStatus> RetVal = new List<TransactionStatus>();
            try
            {
                string sql = "SELECT * FROM TransactionStatus";
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
        public static List<TransactionStatus> Static_GetList(string ConStr)
        {
            List<TransactionStatus> RetVal = new List<TransactionStatus>();
            try
            {
                TransactionStatus m_TransactionStatus = new TransactionStatus(ConStr);
                RetVal = m_TransactionStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<TransactionStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<TransactionStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            TransactionStatus m_TransactionStatus = new TransactionStatus(ConStr);
            List<TransactionStatus> RetVal = m_TransactionStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_TransactionStatus.TransactionStatusName = TextOptionAll;
                m_TransactionStatus.TransactionStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_TransactionStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<TransactionStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<TransactionStatus> GetListOrderBy(string OrderBy)
        {
            List<TransactionStatus> RetVal = new List<TransactionStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM TransactionStatus ";
                if (OrderBy.Length > 0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
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
        public static List<TransactionStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            TransactionStatus m_TransactionStatus = new TransactionStatus(ConStr);
            return m_TransactionStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<TransactionStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<TransactionStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<TransactionStatus> RetVal = new List<TransactionStatus>();
            TransactionStatus m_TransactionStatus = new TransactionStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_TransactionStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_TransactionStatus.TransactionStatusName = TextOptionAll;
                    m_TransactionStatus.TransactionStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_TransactionStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<TransactionStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<TransactionStatus> GetListByTransactionStatusId(byte TransactionStatusId)
        {
            List<TransactionStatus> RetVal = new List<TransactionStatus>();
            try
            {
                if (TransactionStatusId > 0)
                {
                    string sql = "SELECT * FROM TransactionStatus WHERE (TransactionStatusId=" + TransactionStatusId.ToString() + ")";
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
        public TransactionStatus Get(byte TransactionStatusId)
        {
            TransactionStatus RetVal = new TransactionStatus(db.ConnectionString);
            try
            {
                List<TransactionStatus> list = GetListByTransactionStatusId(TransactionStatusId);
                if (list.Count > 0)
                {
                    RetVal = (TransactionStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public TransactionStatus Get()
        {
            return Get(this.TransactionStatusId);
        }
        //-------------------------------------------------------------- 
        public static TransactionStatus Static_Get(byte TransactionStatusId)
        {
            return new TransactionStatus().Get(TransactionStatusId);
        }
        //-----------------------------------------------------------------------------
        public static TransactionStatus Static_Get(byte TransactionStatusId, List<TransactionStatus> lList)
        {
            TransactionStatus RetVal = new TransactionStatus();
            if (TransactionStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.TransactionStatusId == TransactionStatusId);
                if (RetVal == null) RetVal = new TransactionStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
