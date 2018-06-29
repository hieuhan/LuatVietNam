
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
    public class SmsProcessStatus
    {
        private byte _SmsProcessStatusId;
        private string _SmsProcessStatusName;
        private string _SmsProcessStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SmsProcessStatus()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SmsProcessStatus(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SmsProcessStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte SmsProcessStatusId
        {
            get { return _SmsProcessStatusId; }
            set { _SmsProcessStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string SmsProcessStatusName
        {
            get { return _SmsProcessStatusName; }
            set { _SmsProcessStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string SmsProcessStatusDesc
        {
            get { return _SmsProcessStatusDesc; }
            set { _SmsProcessStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<SmsProcessStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SmsProcessStatus> l_SmsProcessStatus = new List<SmsProcessStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SmsProcessStatus m_SmsProcessStatus = new SmsProcessStatus(db.ConnectionString);
                    m_SmsProcessStatus.SmsProcessStatusId = smartReader.GetByte("SmsProcessStatusId");
                    m_SmsProcessStatus.SmsProcessStatusName = smartReader.GetString("SmsProcessStatusName");
                    m_SmsProcessStatus.SmsProcessStatusDesc = smartReader.GetString("SmsProcessStatusDesc");

                    l_SmsProcessStatus.Add(m_SmsProcessStatus);
                }
                reader.Close();
                return l_SmsProcessStatus;
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
                SqlCommand cmd = new SqlCommand("SmsProcessStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusName", this.SmsProcessStatusName));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusDesc", this.SmsProcessStatusDesc));
                cmd.Parameters.Add("@SmsProcessStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SmsProcessStatusId = Convert.ToByte((cmd.Parameters["@SmsProcessStatusId"].Value == null) ? 0 : (cmd.Parameters["@SmsProcessStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("SmsProcessStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusName", this.SmsProcessStatusName));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusDesc", this.SmsProcessStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusId", this.SmsProcessStatusId));
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
                SqlCommand cmd = new SqlCommand("SmsProcessStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusId", this.SmsProcessStatusId));
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
        public List<SmsProcessStatus> GetList()
        {
            List<SmsProcessStatus> RetVal = new List<SmsProcessStatus>();
            try
            {
                string sql = "SELECT * FROM SmsProcessStatus";
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
        public static List<SmsProcessStatus> Static_GetList(string ConStr)
        {
            List<SmsProcessStatus> RetVal = new List<SmsProcessStatus>();
            try
            {
                SmsProcessStatus m_SmsProcessStatus = new SmsProcessStatus(ConStr);
                RetVal = m_SmsProcessStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<SmsProcessStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<SmsProcessStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            SmsProcessStatus m_SmsProcessStatus = new SmsProcessStatus(ConStr);
            List<SmsProcessStatus> RetVal = m_SmsProcessStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_SmsProcessStatus.SmsProcessStatusName = TextOptionAll;
                m_SmsProcessStatus.SmsProcessStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_SmsProcessStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SmsProcessStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<SmsProcessStatus> GetListOrderBy(string OrderBy)
        {
            List<SmsProcessStatus> RetVal = new List<SmsProcessStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM SmsProcessStatus ";
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
        public static List<SmsProcessStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            SmsProcessStatus m_SmsProcessStatus = new SmsProcessStatus(ConStr);
            return m_SmsProcessStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SmsProcessStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SmsProcessStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<SmsProcessStatus> RetVal = new List<SmsProcessStatus>();
            SmsProcessStatus m_SmsProcessStatus = new SmsProcessStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_SmsProcessStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_SmsProcessStatus.SmsProcessStatusName = TextOptionAll;
                    m_SmsProcessStatus.SmsProcessStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_SmsProcessStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SmsProcessStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<SmsProcessStatus> GetListBySmsProcessStatusId(byte SmsProcessStatusId)
        {
            List<SmsProcessStatus> RetVal = new List<SmsProcessStatus>();
            try
            {
                if (SmsProcessStatusId > 0)
                {
                    string sql = "SELECT * FROM SmsProcessStatus WHERE (SmsProcessStatusId=" + SmsProcessStatusId.ToString() + ")";
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
        public SmsProcessStatus Get(byte SmsProcessStatusId)
        {
            SmsProcessStatus RetVal = new SmsProcessStatus(db.ConnectionString);
            try
            {
                List<SmsProcessStatus> list = GetListBySmsProcessStatusId(SmsProcessStatusId);
                if (list.Count > 0)
                {
                    RetVal = (SmsProcessStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SmsProcessStatus Get()
        {
            return Get(this.SmsProcessStatusId);
        }
        //-------------------------------------------------------------- 
        public static SmsProcessStatus Static_Get(byte SmsProcessStatusId)
        {
            return Static_Get(SmsProcessStatusId);
        }
        //-----------------------------------------------------------------------------
        public static SmsProcessStatus Static_Get(byte SmsProcessStatusId, List<SmsProcessStatus> lList)
        {
            SmsProcessStatus RetVal = new SmsProcessStatus();
            if (SmsProcessStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.SmsProcessStatusId == SmsProcessStatusId);
                if (RetVal == null) RetVal = new SmsProcessStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
