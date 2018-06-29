
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
    public class SendMethods
    {
        private byte _SendMethodId;
        private string _SendMethodName;
        private string _SendMethodDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SendMethods()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SendMethods(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SendMethods()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte SendMethodId
        {
            get { return _SendMethodId; }
            set { _SendMethodId = value; }
        }
        //-----------------------------------------------------------------
        public string SendMethodName
        {
            get { return _SendMethodName; }
            set { _SendMethodName = value; }
        }
        //-----------------------------------------------------------------
        public string SendMethodDesc
        {
            get { return _SendMethodDesc; }
            set { _SendMethodDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<SendMethods> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SendMethods> l_SendMethods = new List<SendMethods>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SendMethods m_SendMethods = new SendMethods(db.ConnectionString);
                    m_SendMethods.SendMethodId = smartReader.GetByte("SendMethodId");
                    m_SendMethods.SendMethodName = smartReader.GetString("SendMethodName");
                    m_SendMethods.SendMethodDesc = smartReader.GetString("SendMethodDesc");

                    l_SendMethods.Add(m_SendMethods);
                }
                reader.Close();
                return l_SendMethods;
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
                SqlCommand cmd = new SqlCommand("SendMethods_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SendMethodName", this.SendMethodName));
                cmd.Parameters.Add(new SqlParameter("@SendMethodDesc", this.SendMethodDesc));
                cmd.Parameters.Add("@SendMethodId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SendMethodId = Convert.ToByte((cmd.Parameters["@SendMethodId"].Value == null) ? 0 : (cmd.Parameters["@SendMethodId"].Value));
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
                SqlCommand cmd = new SqlCommand("SendMethods_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SendMethodName", this.SendMethodName));
                cmd.Parameters.Add(new SqlParameter("@SendMethodDesc", this.SendMethodDesc));
                cmd.Parameters.Add(new SqlParameter("@SendMethodId", this.SendMethodId));
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
                SqlCommand cmd = new SqlCommand("SendMethods_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SendMethodId", this.SendMethodId));
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
        public List<SendMethods> GetList()
        {
            List<SendMethods> RetVal = new List<SendMethods>();
            try
            {
                string sql = "SELECT * FROM SendMethods ORDER BY SendMethodName";
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
        public static List<SendMethods> Static_GetList(string ConStr)
        {
            List<SendMethods> RetVal = new List<SendMethods>();
            try
            {
                SendMethods m_SendMethods = new SendMethods(ConStr);
                RetVal = m_SendMethods.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<SendMethods> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<SendMethods> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            SendMethods m_SendMethods = new SendMethods(ConStr);
            List<SendMethods> RetVal = m_SendMethods.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_SendMethods.SendMethodName = TextOptionAll;
                m_SendMethods.SendMethodDesc = TextOptionAll;
                RetVal.Insert(0, m_SendMethods);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SendMethods> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<SendMethods> GetListOrderBy(string OrderBy)
        {
            List<SendMethods> RetVal = new List<SendMethods>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM SendMethods ";
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
        public static List<SendMethods> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            SendMethods m_SendMethods = new SendMethods(ConStr);
            return m_SendMethods.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SendMethods> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SendMethods> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<SendMethods> RetVal = new List<SendMethods>();
            SendMethods m_SendMethods = new SendMethods(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_SendMethods.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_SendMethods.SendMethodName = TextOptionAll;
                    m_SendMethods.SendMethodDesc = TextOptionAll;
                    RetVal.Insert(0, m_SendMethods);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SendMethods> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<SendMethods> GetListBySendMethodId(byte SendMethodId)
        {
            List<SendMethods> RetVal = new List<SendMethods>();
            try
            {
                if (SendMethodId > 0)
                {
                    string sql = "SELECT * FROM SendMethods WHERE (SendMethodId=" + SendMethodId.ToString() + ")";
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
        public SendMethods Get(byte SendMethodId)
        {
            SendMethods RetVal = new SendMethods(db.ConnectionString);
            try
            {
                List<SendMethods> list = GetListBySendMethodId(SendMethodId);
                if (list.Count > 0)
                {
                    RetVal = (SendMethods)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SendMethods Get()
        {
            return Get(this.SendMethodId);
        }
        //-------------------------------------------------------------- 
        public static SendMethods Static_Get(byte SendMethodId)
        {
            return new SendMethods().Get(SendMethodId);
        }
        //-----------------------------------------------------------------------------
        public static SendMethods Static_Get(byte SendMethodId, List<SendMethods> lList)
        {
            SendMethods RetVal = new SendMethods();
            if (SendMethodId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.SendMethodId == SendMethodId);
                if (RetVal == null) RetVal = new SendMethods();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
