
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
    public class SendStatus
    {
        private byte _SendStatusId;
        private string _SendStatusName;
        private string _SendStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SendStatus()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SendStatus(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SendStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte SendStatusId
        {
            get { return _SendStatusId; }
            set { _SendStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string SendStatusName
        {
            get { return _SendStatusName; }
            set { _SendStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string SendStatusDesc
        {
            get { return _SendStatusDesc; }
            set { _SendStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<SendStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SendStatus> l_SendStatus = new List<SendStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SendStatus m_SendStatus = new SendStatus(db.ConnectionString);
                    m_SendStatus.SendStatusId = smartReader.GetByte("SendStatusId");
                    m_SendStatus.SendStatusName = smartReader.GetString("SendStatusName");
                    m_SendStatus.SendStatusDesc = smartReader.GetString("SendStatusDesc");

                    l_SendStatus.Add(m_SendStatus);
                }
                reader.Close();
                return l_SendStatus;
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
                SqlCommand cmd = new SqlCommand("SendStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SendStatusName", this.SendStatusName));
                cmd.Parameters.Add(new SqlParameter("@SendStatusDesc", this.SendStatusDesc));
                cmd.Parameters.Add("@SendStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SendStatusId = Convert.ToByte((cmd.Parameters["@SendStatusId"].Value == null) ? 0 : (cmd.Parameters["@SendStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("SendStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SendStatusName", this.SendStatusName));
                cmd.Parameters.Add(new SqlParameter("@SendStatusDesc", this.SendStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@SendStatusId", this.SendStatusId));
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
                SqlCommand cmd = new SqlCommand("SendStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SendStatusId", this.SendStatusId));
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
        public List<SendStatus> GetList()
        {
            List<SendStatus> RetVal = new List<SendStatus>();
            try
            {
                string sql = "SELECT * FROM SendStatus";
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
        public static List<SendStatus> Static_GetList(string ConStr)
        {
            List<SendStatus> RetVal = new List<SendStatus>();
            try
            {
                SendStatus m_SendStatus = new SendStatus(ConStr);
                RetVal = m_SendStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<SendStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<SendStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            SendStatus m_SendStatus = new SendStatus(ConStr);
            List<SendStatus> RetVal = m_SendStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_SendStatus.SendStatusName = TextOptionAll;
                m_SendStatus.SendStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_SendStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SendStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<SendStatus> GetListOrderBy(string OrderBy)
        {
            List<SendStatus> RetVal = new List<SendStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM SendStatus ";
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
        public static List<SendStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            SendStatus m_SendStatus = new SendStatus(ConStr);
            return m_SendStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SendStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SendStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<SendStatus> RetVal = new List<SendStatus>();
            SendStatus m_SendStatus = new SendStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_SendStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_SendStatus.SendStatusName = TextOptionAll;
                    m_SendStatus.SendStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_SendStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SendStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<SendStatus> GetListBySendStatusId(byte SendStatusId)
        {
            List<SendStatus> RetVal = new List<SendStatus>();
            try
            {
                if (SendStatusId > 0)
                {
                    string sql = "SELECT * FROM SendStatus WHERE (SendStatusId=" + SendStatusId.ToString() + ")";
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
        public SendStatus Get(byte SendStatusId)
        {
            SendStatus RetVal = new SendStatus(db.ConnectionString);
            try
            {
                List<SendStatus> list = GetListBySendStatusId(SendStatusId);
                if (list.Count > 0)
                {
                    RetVal = (SendStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SendStatus Get()
        {
            return Get(this.SendStatusId);
        }
        //-------------------------------------------------------------- 
        public static SendStatus Static_Get(byte SendStatusId)
        {
            return new SendStatus().Get(SendStatusId);
        }
        //-----------------------------------------------------------------------------
        public static SendStatus Static_Get(byte SendStatusId, List<SendStatus> lList)
        {
            SendStatus RetVal = new SendStatus();
            if (SendStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.SendStatusId == SendStatusId);
                if (RetVal == null) RetVal = new SendStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
