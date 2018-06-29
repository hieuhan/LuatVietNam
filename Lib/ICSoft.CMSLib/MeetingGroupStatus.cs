
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
    public class MeetingGroupStatus
    {
        private byte _MeetingGroupStatusId;
        private string _MeetingGroupStatusName;
        private string _MeetingGroupStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public MeetingGroupStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MeetingGroupStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MeetingGroupStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte MeetingGroupStatusId
        {
            get { return _MeetingGroupStatusId; }
            set { _MeetingGroupStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string MeetingGroupStatusName
        {
            get { return _MeetingGroupStatusName; }
            set { _MeetingGroupStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string MeetingGroupStatusDesc
        {
            get { return _MeetingGroupStatusDesc; }
            set { _MeetingGroupStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<MeetingGroupStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MeetingGroupStatus> l_MeetingGroupStatus = new List<MeetingGroupStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MeetingGroupStatus m_MeetingGroupStatus = new MeetingGroupStatus(db.ConnectionString);
                    m_MeetingGroupStatus.MeetingGroupStatusId = smartReader.GetByte("MeetingGroupStatusId");
                    m_MeetingGroupStatus.MeetingGroupStatusName = smartReader.GetString("MeetingGroupStatusName");
                    m_MeetingGroupStatus.MeetingGroupStatusDesc = smartReader.GetString("MeetingGroupStatusDesc");

                    l_MeetingGroupStatus.Add(m_MeetingGroupStatus);
                }
                reader.Close();
                return l_MeetingGroupStatus;
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
                SqlCommand cmd = new SqlCommand("MeetingGroupStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupStatusName", this.MeetingGroupStatusName));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupStatusDesc", this.MeetingGroupStatusDesc));
                cmd.Parameters.Add("@MeetingGroupStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MeetingGroupStatusId = Convert.ToByte((cmd.Parameters["@MeetingGroupStatusId"].Value == null) ? 0 : (cmd.Parameters["@MeetingGroupStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("MeetingGroupStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupStatusName", this.MeetingGroupStatusName));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupStatusDesc", this.MeetingGroupStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupStatusId", this.MeetingGroupStatusId));
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
                SqlCommand cmd = new SqlCommand("MeetingGroupStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupStatusId", this.MeetingGroupStatusId));
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
        public List<MeetingGroupStatus> GetList()
        {
            List<MeetingGroupStatus> RetVal = new List<MeetingGroupStatus>();
            try
            {
                string sql = "SELECT * FROM V$MeetingGroupStatus";
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
        public static List<MeetingGroupStatus> Static_GetList(string ConStr)
        {
            List<MeetingGroupStatus> RetVal = new List<MeetingGroupStatus>();
            try
            {
                MeetingGroupStatus m_MeetingGroupStatus = new MeetingGroupStatus(ConStr);
                RetVal = m_MeetingGroupStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MeetingGroupStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<MeetingGroupStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            MeetingGroupStatus m_MeetingGroupStatus = new MeetingGroupStatus(ConStr);
            List<MeetingGroupStatus> RetVal = m_MeetingGroupStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_MeetingGroupStatus.MeetingGroupStatusName = TextOptionAll;
                m_MeetingGroupStatus.MeetingGroupStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_MeetingGroupStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<MeetingGroupStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<MeetingGroupStatus> GetListOrderBy(string OrderBy)
        {
            List<MeetingGroupStatus> RetVal = new List<MeetingGroupStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$MeetingGroupStatus ";
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
        public static List<MeetingGroupStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            MeetingGroupStatus m_MeetingGroupStatus = new MeetingGroupStatus(ConStr);
            return m_MeetingGroupStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MeetingGroupStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MeetingGroupStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<MeetingGroupStatus> RetVal = new List<MeetingGroupStatus>();
            MeetingGroupStatus m_MeetingGroupStatus = new MeetingGroupStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_MeetingGroupStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_MeetingGroupStatus.MeetingGroupStatusName = TextOptionAll;
                    m_MeetingGroupStatus.MeetingGroupStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_MeetingGroupStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<MeetingGroupStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<MeetingGroupStatus> GetListByMeetingGroupStatusId(byte MeetingGroupStatusId)
        {
            List<MeetingGroupStatus> RetVal = new List<MeetingGroupStatus>();
            try
            {
                if (MeetingGroupStatusId > 0)
                {
                    string sql = "SELECT * FROM V$MeetingGroupStatus WHERE (MeetingGroupStatusId=" + MeetingGroupStatusId.ToString() + ")";
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
        public MeetingGroupStatus Get(byte MeetingGroupStatusId)
        {
            MeetingGroupStatus RetVal = new MeetingGroupStatus(db.ConnectionString);
            try
            {
                List<MeetingGroupStatus> list = GetListByMeetingGroupStatusId(MeetingGroupStatusId);
                if (list.Count > 0)
                {
                    RetVal = (MeetingGroupStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MeetingGroupStatus Get()
        {
            return Get(this.MeetingGroupStatusId);
        }
        //-------------------------------------------------------------- 
        public static MeetingGroupStatus Static_Get(string Constr, byte MeetingGroupStatusId)
        {
            MeetingGroupStatus m_MeetingGroupStatus = new MeetingGroupStatus(Constr);
            return m_MeetingGroupStatus.Get(MeetingGroupStatusId);
        }
        //-------------------------------------------------------------- 
        public static MeetingGroupStatus Static_Get(byte MeetingGroupStatusId)
        {
            return Static_Get("", MeetingGroupStatusId);
        }
        //-----------------------------------------------------------------------------
        public static MeetingGroupStatus Static_Get(byte MeetingGroupStatusId, List<MeetingGroupStatus> lList)
        {
            MeetingGroupStatus RetVal = new MeetingGroupStatus();
            if (MeetingGroupStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.MeetingGroupStatusId == MeetingGroupStatusId);
                if (RetVal == null) RetVal = new MeetingGroupStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte MeetingGroupStatusId)
        {
            string RetVal = "";
            MeetingGroupStatus m_MeetingGroupStatus = new MeetingGroupStatus();
            m_MeetingGroupStatus = m_MeetingGroupStatus.Get(MeetingGroupStatusId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == CmsConstants.CULTURE_VN)
            {
                RetVal = m_MeetingGroupStatus.MeetingGroupStatusDesc;
            }
            else
            {
                RetVal = m_MeetingGroupStatus.MeetingGroupStatusName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        
    }
}

