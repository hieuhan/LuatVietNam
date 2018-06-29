
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
    public class Meetings
    {
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private int _MeetingId;
        private DateTime _MeetingTime;
        private string _MeetingTitle;
        private string _MeetingDesc;
        private string _MeetingLocation;
        private string _MeetingRoport;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private short _MeetingGroupId;
        private byte _MeetingStatusId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Meetings()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Meetings(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Meetings()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------    
        public byte ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------    
        public int MeetingId
        {
            get { return _MeetingId; }
            set { _MeetingId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime MeetingTime
        {
            get { return _MeetingTime; }
            set { _MeetingTime = value; }
        }
        //-----------------------------------------------------------------
        public string MeetingTitle
        {
            get { return _MeetingTitle; }
            set { _MeetingTitle = value; }
        }
        //-----------------------------------------------------------------
        public string MeetingDesc
        {
            get { return _MeetingDesc; }
            set { _MeetingDesc = value; }
        }
        //-----------------------------------------------------------------
        public string MeetingLocation
        {
            get { return _MeetingLocation; }
            set { _MeetingLocation = value; }
        }
        //-----------------------------------------------------------------
        public string MeetingRoport
        {
            get { return _MeetingRoport; }
            set { _MeetingRoport = value; }
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

        public short MeetingGroupId
        {
            get { return _MeetingGroupId; }
            set { _MeetingGroupId = value; }
        }
        public byte MeetingStatusId
        {
            get { return _MeetingStatusId; }
            set { _MeetingStatusId = value; }
        }
        private List<Meetings> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Meetings> l_Meetings = new List<Meetings>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Meetings m_Meetings = new Meetings(db.ConnectionString);
                    m_Meetings.LanguageId = smartReader.GetByte("LanguageId");
                    m_Meetings.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Meetings.MeetingId = smartReader.GetInt32("MeetingId");
                    m_Meetings.MeetingGroupId = smartReader.GetInt16("MeetingGroupId");
                    m_Meetings.MeetingTime = smartReader.GetDateTime("MeetingTime");
                    m_Meetings.MeetingTitle = smartReader.GetString("MeetingTitle");
                    m_Meetings.MeetingDesc = smartReader.GetString("MeetingDesc");
                    m_Meetings.MeetingLocation = smartReader.GetString("MeetingLocation");
                    m_Meetings.MeetingRoport = smartReader.GetString("MeetingRoport");
                    m_Meetings.MeetingStatusId = smartReader.GetByte("MeetingStatusId");
                    m_Meetings.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Meetings.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Meetings.Add(m_Meetings);
                }
                reader.Close();
                return l_Meetings;
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
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
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
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Meetings_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupId", this.MeetingGroupId));
                cmd.Parameters.Add(new SqlParameter("@MeetingTime", this.MeetingTime));
                cmd.Parameters.Add(new SqlParameter("@MeetingTitle", this.MeetingTitle));
                cmd.Parameters.Add(new SqlParameter("@MeetingDesc", this.MeetingDesc));
                cmd.Parameters.Add(new SqlParameter("@MeetingLocation", this.MeetingLocation));
                cmd.Parameters.Add(new SqlParameter("@MeetingRoport", this.MeetingRoport));
                cmd.Parameters.Add(new SqlParameter("@MeetingStatusId", this.MeetingStatusId));
                cmd.Parameters.Add(new SqlParameter("@MeetingId", this.MeetingId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MeetingId = Convert.ToInt32((cmd.Parameters["@MeetingId"].Value == null) ? 0 : (cmd.Parameters["@MeetingId"].Value));
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
                SqlCommand cmd = new SqlCommand("Meetings_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@MeetingId", this.MeetingId));
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
        public List<Meetings> GetList()
        {
            List<Meetings> RetVal = new List<Meetings>();
            try
            {
                string sql = "SELECT * FROM V$Meetings";
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
        public static List<Meetings> Static_GetList(string ConStr)
        {
            List<Meetings> RetVal = new List<Meetings>();
            try
            {
                Meetings m_Meetings = new Meetings(ConStr);
                RetVal = m_Meetings.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Meetings> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<Meetings> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            Meetings m_Meetings = new Meetings(ConStr);
            List<Meetings> RetVal = m_Meetings.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_Meetings.MeetingTitle = TextOptionAll;
                m_Meetings.MeetingDesc = TextOptionAll;
                RetVal.Insert(0, m_Meetings);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Meetings> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<Meetings> GetListOrderBy(string OrderBy)
        {
            List<Meetings> RetVal = new List<Meetings>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Meetings ";
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
        public static List<Meetings> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Meetings m_Meetings = new Meetings(ConStr);
            return m_Meetings.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Meetings> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Meetings> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<Meetings> RetVal = new List<Meetings>();
            Meetings m_Meetings = new Meetings(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_Meetings.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_Meetings.MeetingTitle = TextOptionAll;
                    m_Meetings.MeetingDesc = TextOptionAll;
                    RetVal.Insert(0, m_Meetings);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Meetings> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<Meetings> GetListByMeetingGroupId(short MeetingGroupId, byte MeetingStatusId, byte LanguageId, byte ApplicationTypeId)
        {
            List<Meetings> RetVal = new List<Meetings>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                int MeetingId = 0;
                string MeetingTitle = "";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MeetingId, MeetingGroupId, MeetingTitle, MeetingStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Meetings> GetListByMeetingId(int MeetingId, byte LanguageId, byte ApplicationTypeId)
        {
            List<Meetings> RetVal = new List<Meetings>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                short MeetingGroupId=0;
                string MeetingTitle="";
                byte MeetingStatusId=0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MeetingId, MeetingGroupId, MeetingTitle, MeetingStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Meetings> GetListByMeetingGroupId(byte LanguageId, byte ApplicationTypeId, byte MeetingStatusId, short MeetingGroupId)
        {
            List<Meetings> RetVal = new List<Meetings>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                int MeetingId=0;
                string MeetingTitle = "";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MeetingId, MeetingGroupId, MeetingTitle, MeetingStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public Meetings Get(int MeetingId, byte LanguageId, byte ApplicationTypeId)
        {
            Meetings RetVal = new Meetings(db.ConnectionString);
            try
            {
                List<Meetings> list = GetListByMeetingId(MeetingId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (Meetings)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Meetings Get()
        {
            return Get(this.MeetingId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Meetings Static_Get(string Constr, int MeetingId, byte LanguageId, byte ApplicationTypeId)
        {
            Meetings m_Meetings = new Meetings(Constr);
            return m_Meetings.Get(MeetingId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Meetings Static_Get(int MeetingId, byte LanguageId, byte ApplicationTypeId)
        {
            return Static_Get("", MeetingId, LanguageId, ApplicationTypeId);
        }
        //--------------------------------------------------------------     
        public List<Meetings> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, int MeetingId, short MeetingGroupId, string MeetingTitle, byte MeetingStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Meetings> RetVal = new List<Meetings>();
            try
            {
                SqlCommand cmd = new SqlCommand("Meetings_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MeetingId", MeetingId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupId", MeetingGroupId));
                cmd.Parameters.Add(new SqlParameter("@MeetingTitle", MeetingTitle));
                cmd.Parameters.Add(new SqlParameter("@MeetingStatusId", MeetingStatusId));
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
        public List<Meetings> Search(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, short MeetingGroupId, string MeetingTitle, byte MeetingStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MeetingId, MeetingGroupId, MeetingTitle, MeetingStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}

