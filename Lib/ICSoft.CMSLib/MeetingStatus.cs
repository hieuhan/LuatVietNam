
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using sms.database;
using sms.utils;


namespace ICSoft.CMSLib
{
    public class MeetingStatus
    {
        private byte _MeetingStatusId;
        private string _MeetingStatusName;
        private string _MeetingStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public MeetingStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MeetingStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MeetingStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte MeetingStatusId
        {
            get { return _MeetingStatusId; }
            set { _MeetingStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string MeetingStatusName
        {
            get { return _MeetingStatusName; }
            set { _MeetingStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string MeetingStatusDesc
        {
            get { return _MeetingStatusDesc; }
            set { _MeetingStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<MeetingStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MeetingStatus> l_MeetingStatus = new List<MeetingStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MeetingStatus m_MeetingStatus = new MeetingStatus(db.ConnectionString);
                    m_MeetingStatus.MeetingStatusId = smartReader.GetByte("MeetingStatusId");
                    m_MeetingStatus.MeetingStatusName = smartReader.GetString("MeetingStatusName");
                    m_MeetingStatus.MeetingStatusDesc = smartReader.GetString("MeetingStatusDesc");

                    l_MeetingStatus.Add(m_MeetingStatus);
                }
                reader.Close();
                return l_MeetingStatus;
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
                SqlCommand cmd = new SqlCommand("MeetingStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingStatusName", this.MeetingStatusName));
                cmd.Parameters.Add(new SqlParameter("@MeetingStatusDesc", this.MeetingStatusDesc));
                cmd.Parameters.Add("@MeetingStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MeetingStatusId = Convert.ToByte((cmd.Parameters["@MeetingStatusId"].Value == null) ? 0 : (cmd.Parameters["@MeetingStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("MeetingStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingStatusName", this.MeetingStatusName));
                cmd.Parameters.Add(new SqlParameter("@MeetingStatusDesc", this.MeetingStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@MeetingStatusId", this.MeetingStatusId));
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
                SqlCommand cmd = new SqlCommand("MeetingStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingStatusId", this.MeetingStatusId));
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
        public List<MeetingStatus> GetList()
        {
            List<MeetingStatus> RetVal = new List<MeetingStatus>();
            try
            {
                string sql = "SELECT * FROM V$MeetingStatus";
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
        public static List<MeetingStatus> Static_GetList(string ConStr)
        {
            List<MeetingStatus> RetVal = new List<MeetingStatus>();
            try
            {
                MeetingStatus m_MeetingStatus = new MeetingStatus(ConStr);
                RetVal = m_MeetingStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MeetingStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<MeetingStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            MeetingStatus m_MeetingStatus = new MeetingStatus(ConStr);
            List<MeetingStatus> RetVal = m_MeetingStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_MeetingStatus.MeetingStatusName = TextOptionAll;
                m_MeetingStatus.MeetingStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_MeetingStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<MeetingStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<MeetingStatus> GetListOrderBy(string OrderBy)
        {
            List<MeetingStatus> RetVal = new List<MeetingStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$MeetingStatus ";
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
        public static List<MeetingStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            MeetingStatus m_MeetingStatus = new MeetingStatus(ConStr);
            return m_MeetingStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MeetingStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MeetingStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<MeetingStatus> RetVal = new List<MeetingStatus>();
            MeetingStatus m_MeetingStatus = new MeetingStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_MeetingStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_MeetingStatus.MeetingStatusName = TextOptionAll;
                    m_MeetingStatus.MeetingStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_MeetingStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<MeetingStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<MeetingStatus> GetListByMeetingStatusId(byte MeetingStatusId)
        {
            List<MeetingStatus> RetVal = new List<MeetingStatus>();
            try
            {
                if (MeetingStatusId > 0)
                {
                    string sql = "SELECT * FROM V$MeetingStatus WHERE (MeetingStatusId=" + MeetingStatusId.ToString() + ")";
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
        public MeetingStatus Get(byte MeetingStatusId)
        {
            MeetingStatus RetVal = new MeetingStatus(db.ConnectionString);
            try
            {
                List<MeetingStatus> list = GetListByMeetingStatusId(MeetingStatusId);
                if (list.Count > 0)
                {
                    RetVal = (MeetingStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MeetingStatus Get()
        {
            return Get(this.MeetingStatusId);
        }
        //-------------------------------------------------------------- 
        public static MeetingStatus Static_Get(byte MeetingStatusId)
        {
            return Static_Get(MeetingStatusId);
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte MeetingStatusId)
        {
            string RetVal = "";
            MeetingStatus m_MeetingStatus = new MeetingStatus();
            m_MeetingStatus = m_MeetingStatus.Get(MeetingStatusId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == CmsConstants.CULTURE_VN)
            {
                RetVal = m_MeetingStatus.MeetingStatusDesc;
            }
            else
            {
                RetVal = m_MeetingStatus.MeetingStatusName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}