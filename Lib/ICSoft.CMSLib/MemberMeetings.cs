
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
    public class MemberMeetings
    {
        private int _MemberMeetingId;
        private int _MeetingId;
        private byte _MemberMeetingStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _MemberId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public MemberMeetings()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MemberMeetings(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MemberMeetings()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MemberMeetingId
        {
            get { return _MemberMeetingId; }
            set { _MemberMeetingId = value; }
        }
        //-----------------------------------------------------------------
        public int MeetingId
        {
            get { return _MeetingId; }
            set { _MeetingId = value; }
        }
        //-----------------------------------------------------------------
        public byte MemberMeetingStatusId
        {
            get { return _MemberMeetingStatusId; }
            set { _MemberMeetingStatusId = value; }
        }
        //-----------------------------------------------------------------
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        public int MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private List<MemberMeetings> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MemberMeetings> l_MemberMeetings = new List<MemberMeetings>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MemberMeetings m_MemberMeetings = new MemberMeetings(db.ConnectionString);
                    m_MemberMeetings.MemberMeetingId = smartReader.GetInt32("MemberMeetingId");
                    m_MemberMeetings.MemberId = smartReader.GetInt32("MemberId");
                    m_MemberMeetings.MeetingId = smartReader.GetInt32("MeetingId");
                    m_MemberMeetings.MemberMeetingStatusId = smartReader.GetByte("MemberMeetingStatusId");
                    m_MemberMeetings.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MemberMeetings.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_MemberMeetings.Add(m_MemberMeetings);
                }
                reader.Close();
                return l_MemberMeetings;
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
                SqlCommand cmd = new SqlCommand("MemberMeetings_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@MeetingId", this.MeetingId));
                cmd.Parameters.Add(new SqlParameter("@MemberMeetingStatusId", this.MemberMeetingStatusId));
                cmd.Parameters.Add("@MemberMeetingId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MemberMeetingId = Convert.ToInt32((cmd.Parameters["@MemberMeetingId"].Value == null) ? 0 : (cmd.Parameters["@MemberMeetingId"].Value));
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
                SqlCommand cmd = new SqlCommand("MemberMeetings_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@MeetingId", this.MeetingId));
                cmd.Parameters.Add(new SqlParameter("@MemberMeetingStatusId", this.MemberMeetingStatusId));
                cmd.Parameters.Add(new SqlParameter("@MemberMeetingId", this.MemberMeetingId));
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
                SqlCommand cmd = new SqlCommand("MemberMeetings_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberMeetingId", this.MemberMeetingId));
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
        public List<MemberMeetings> GetList()
        {
            List<MemberMeetings> RetVal = new List<MemberMeetings>();
            try
            {
                string sql = "SELECT * FROM V$MemberMeetings";
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
        public static List<MemberMeetings> Static_GetList(string ConStr)
        {
            List<MemberMeetings> RetVal = new List<MemberMeetings>();
            try
            {
                MemberMeetings m_MemberMeetings = new MemberMeetings(ConStr);
                RetVal = m_MemberMeetings.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MemberMeetings> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<MemberMeetings> GetListByMemberMeetingId(int MemberMeetingId)
        {
            List<MemberMeetings> RetVal = new List<MemberMeetings>();
            try
            {
                if (MemberMeetingId > 0)
                {
                    string sql = "SELECT * FROM V$MemberMeetings WHERE (MemberMeetingId=" + MemberMeetingId.ToString() + ")";
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
        public MemberMeetings Get(int MemberMeetingId)
        {
            MemberMeetings RetVal = new MemberMeetings(db.ConnectionString);
            try
            {
                List<MemberMeetings> list = GetListByMemberMeetingId(MemberMeetingId);
                if (list.Count > 0)
                {
                    RetVal = (MemberMeetings)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MemberMeetings Get()
        {
            return Get(this.MemberMeetingId);
        }
        //-------------------------------------------------------------- 
        public static MemberMeetings Static_Get(int MemberMeetingId)
        {
            return Static_Get(MemberMeetingId);
        }
        //--------------------------------------------------------------     
        public List<MemberMeetings> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int MeetingId, byte MemberMeetingStatusId, int MemberId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<MemberMeetings> RetVal = new List<MemberMeetings>();
            try
            {
                SqlCommand cmd = new SqlCommand("MemberMeetings_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MeetingId", MeetingId));
                cmd.Parameters.Add(new SqlParameter("@MemberMeetingStatusId", MemberMeetingStatusId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", MemberId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<MemberMeetings> Search(int ActUserId, string OrderBy, int MeetingId, byte MemberMeetingStatusId, int MemberId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MeetingId, MemberMeetingStatusId, MemberId, DateFrom, DateTo, ref RowCount);
        }
    }
}