
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
    public class MeetingSponsors
    {
        private int _MeetingSponsorId;
        private int _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _MeetingId;
        private short _SponsorId;
        private string _SponsorName;
        private DBAccess db;
        //-----------------------------------------------------------------
        public MeetingSponsors()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MeetingSponsors(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MeetingSponsors()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MeetingSponsorId
        {
            get { return _MeetingSponsorId; }
            set { _MeetingSponsorId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------
        public string SponsorName
        {
            get { return _SponsorName; }
            set { _SponsorName = value; }
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

        public int MeetingId
        {
            get { return _MeetingId; }
            set { _MeetingId = value; }
        }
        public short SponsorId
        {
            get { return _SponsorId; }
            set { _SponsorId = value; }
        }
        private List<MeetingSponsors> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MeetingSponsors> l_MeetingSponsors = new List<MeetingSponsors>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MeetingSponsors m_MeetingSponsors = new MeetingSponsors(db.ConnectionString);
                    m_MeetingSponsors.MeetingSponsorId = smartReader.GetInt32("MeetingSponsorId");
                    m_MeetingSponsors.MeetingId = smartReader.GetInt32("MeetingId");
                    m_MeetingSponsors.SponsorId = smartReader.GetInt16("SponsorId");
                    m_MeetingSponsors.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_MeetingSponsors.SponsorName = smartReader.GetString("SponsorName");
                    m_MeetingSponsors.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MeetingSponsors.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_MeetingSponsors.Add(m_MeetingSponsors);
                }
                reader.Close();
                return l_MeetingSponsors;
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
                SqlCommand cmd = new SqlCommand("MeetingSponsors_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingId", this.MeetingId));
                cmd.Parameters.Add(new SqlParameter("@SponsorId", this.SponsorId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@MeetingSponsorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MeetingSponsorId = Convert.ToInt32((cmd.Parameters["@MeetingSponsorId"].Value == null) ? 0 : (cmd.Parameters["@MeetingSponsorId"].Value));
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
                SqlCommand cmd = new SqlCommand("MeetingSponsors_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingId", this.MeetingId));
                cmd.Parameters.Add(new SqlParameter("@SponsorId", this.SponsorId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@MeetingSponsorId", this.MeetingSponsorId));
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
                SqlCommand cmd = new SqlCommand("MeetingSponsors_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingSponsorId", this.MeetingSponsorId));
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
        public List<MeetingSponsors> GetListByMeetingSponsorId(int MeetingSponsorId)
        {
            List<MeetingSponsors> RetVal = new List<MeetingSponsors>();
            try
            {
                if (MeetingSponsorId > 0)
                {
                    string sql = "SELECT * FROM V$MeetingSponsors WHERE (MeetingSponsorId=" + MeetingSponsorId.ToString() + ")";
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
        public List<MeetingSponsors> GetListByMeetingId(int MeetingId)
        {
            List<MeetingSponsors> RetVal = new List<MeetingSponsors>();
            try
            {
                if (MeetingId > 0)
                {
                    string sql = "SELECT * FROM V$MeetingSponsors WHERE (MeetingId=" + MeetingId.ToString() + ")";
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
        public MeetingSponsors Get(int MeetingSponsorId)
        {
            MeetingSponsors RetVal = new MeetingSponsors(db.ConnectionString);
            try
            {
                List<MeetingSponsors> list = GetListByMeetingSponsorId(MeetingSponsorId);
                if (list.Count > 0)
                {
                    RetVal = (MeetingSponsors)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MeetingSponsors Get()
        {
            return Get(this.MeetingSponsorId);
        }
        //-------------------------------------------------------------- 
        public static MeetingSponsors Static_Get(int MeetingSponsorId)
        {
            return Static_Get(MeetingSponsorId);
        }
        //--------------------------------------------------------------     
        
    }
}