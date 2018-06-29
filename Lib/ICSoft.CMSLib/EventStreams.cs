
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
    public class EventStreams
    {
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private int _EventStreamId;
        private string _EventStreamName;
        private string _EventStreamDesc;
        private short _SiteId;
        private short _CategoryId;
        private byte _ReviewStatusId;
        private string _ImagePath;
        private DateTime _DisplayStartTime;
        private DateTime _DisplayEndTime;
        private byte _ShowTop;
        private byte _ShowBottom;
        private byte _ShowWeb;
        private byte _ShowWap;
        private byte _ShowApp;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public EventStreams()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public EventStreams(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~EventStreams()
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
        public int EventStreamId
        {
            get { return _EventStreamId; }
            set { _EventStreamId = value; }
        }
        //-----------------------------------------------------------------
        public string EventStreamName
        {
            get { return _EventStreamName; }
            set { _EventStreamName = value; }
        }
        //-----------------------------------------------------------------
        public string EventStreamDesc
        {
            get { return _EventStreamDesc; }
            set { _EventStreamDesc = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }   
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        //-----------------------------------------------------------------
        public DateTime DisplayStartTime
        {
            get { return _DisplayStartTime; }
            set { _DisplayStartTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime DisplayEndTime
        {
            get { return _DisplayEndTime; }
            set { _DisplayEndTime = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowTop
        {
            get { return _ShowTop; }
            set { _ShowTop = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowBottom
        {
            get { return _ShowBottom; }
            set { _ShowBottom = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowWeb
        {
            get { return _ShowWeb; }
            set { _ShowWeb = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowWap
        {
            get { return _ShowWap; }
            set { _ShowWap = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowApp
        {
            get { return _ShowApp; }
            set { _ShowApp = value; }
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

        private List<EventStreams> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<EventStreams> l_EventStreams = new List<EventStreams>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    EventStreams m_EventStreams = new EventStreams(db.ConnectionString);
                    m_EventStreams.LanguageId = smartReader.GetByte("LanguageId");
                    m_EventStreams.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_EventStreams.EventStreamId = smartReader.GetInt32("EventStreamId");
                    m_EventStreams.EventStreamName = smartReader.GetString("EventStreamName");
                    m_EventStreams.EventStreamDesc = smartReader.GetString("EventStreamDesc");
                    m_EventStreams.SiteId = smartReader.GetInt16("SiteId");
                    m_EventStreams.CategoryId = smartReader.GetInt16("CategoryId");
                    m_EventStreams.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_EventStreams.ImagePath = smartReader.GetString("ImagePath");
                    m_EventStreams.DisplayStartTime = smartReader.GetDateTime("DisplayStartTime");
                    m_EventStreams.DisplayEndTime = smartReader.GetDateTime("DisplayEndTime");
                    m_EventStreams.ShowTop = smartReader.GetByte("ShowTop");
                    m_EventStreams.ShowBottom = smartReader.GetByte("ShowBottom");
                    m_EventStreams.ShowWeb = smartReader.GetByte("ShowWeb");
                    m_EventStreams.ShowWap = smartReader.GetByte("ShowWap");
                    m_EventStreams.ShowApp = smartReader.GetByte("ShowApp");
                    m_EventStreams.CrUserId = smartReader.GetInt32("CrUserId");
                    m_EventStreams.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_EventStreams.Add(m_EventStreams);
                }
                reader.Close();
                return l_EventStreams;
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
                SqlCommand cmd = new SqlCommand("EventStreams_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamName", this.EventStreamName));
                cmd.Parameters.Add(new SqlParameter("@EventStreamDesc", this.EventStreamDesc));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@DisplayStartTime", this.DisplayStartTime));
                cmd.Parameters.Add(new SqlParameter("@DisplayEndTime", this.DisplayEndTime));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", this.EventStreamId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.EventStreamId = Convert.ToInt32((cmd.Parameters["@EventStreamId"].Value == null) ? 0 : (cmd.Parameters["@EventStreamId"].Value));
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
                SqlCommand cmd = new SqlCommand("EventStreams_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", this.EventStreamId));
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
        public List<EventStreams> GetList()
        {
            List<EventStreams> RetVal = new List<EventStreams>();
            try
            {
                string sql = "SELECT * FROM EventStreams";
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
        public static List<EventStreams> Static_GetList(string ConStr)
        {
            List<EventStreams> RetVal = new List<EventStreams>();
            try
            {
                EventStreams m_EventStreams = new EventStreams(ConStr);
                RetVal = m_EventStreams.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<EventStreams> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<EventStreams> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            EventStreams m_EventStreams = new EventStreams(ConStr);
            List<EventStreams> RetVal = m_EventStreams.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_EventStreams.EventStreamName = TextOptionAll;
                m_EventStreams.EventStreamDesc = TextOptionAll;
                RetVal.Insert(0, m_EventStreams);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<EventStreams> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<EventStreams> GetListOrderBy(string OrderBy)
        {
            List<EventStreams> RetVal = new List<EventStreams>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM EventStreams ";
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
        public static List<EventStreams> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            EventStreams m_EventStreams = new EventStreams(ConStr);
            return m_EventStreams.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<EventStreams> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<EventStreams> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<EventStreams> RetVal = new List<EventStreams>();
            EventStreams m_EventStreams = new EventStreams(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_EventStreams.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_EventStreams.EventStreamName = TextOptionAll;
                    m_EventStreams.EventStreamDesc = TextOptionAll;
                    RetVal.Insert(0, m_EventStreams);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<EventStreams> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<EventStreams> GetListByEventStreamId(int EventStreamId, byte LanguageId, byte ApplicationTypeId)
        {
            List<EventStreams> RetVal = new List<EventStreams>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                short CategoryId = 0;
                string EventStreamName = "";
                byte ReviewStatusId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, EventStreamId, CategoryId, EventStreamName, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public EventStreams Get(int EventStreamId, byte LanguageId, byte ApplicationTypeId)
        {
            EventStreams RetVal = new EventStreams(db.ConnectionString);
            try
            {
                List<EventStreams> list = GetListByEventStreamId(EventStreamId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (EventStreams)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public EventStreams Get()
        {
            return Get(this.EventStreamId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static EventStreams Static_Get(string Constr, int EventStreamId, byte LanguageId, byte ApplicationTypeId)
        {
            EventStreams m_EventStreams = new EventStreams(Constr);
            return m_EventStreams.Get(EventStreamId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static EventStreams Static_Get(int EventStreamId, byte LanguageId, byte ApplicationTypeId)
        {
            return Static_Get("", EventStreamId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public List<EventStreams> GetListByLanguageIdAndEventStreamId(byte LanguageId, byte ApplicationTypeId, int EventStreamId)
        {
            List<EventStreams> RetVal = new List<EventStreams>();
            try
            {
                if (CategoryId > 0)
                {
                    SqlCommand cmd = new SqlCommand("EventStreams_GetByLanguageIdAndEventStreamId");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                    cmd.Parameters.Add(new SqlParameter("@EventStreamId", EventStreamId));
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
        // Added by: Vu.The
        // Date: June 12, 2015
        //-------------------------------------------------------------- 
        public List<EventStreams> GetByCategoryIdInRangeOfDate(byte LanguageId, byte ApplicationTypeId, short CategoryId, string DateFrom, string DateTo)
        {
            List<EventStreams> RetVal = new List<EventStreams>();
            try
            {
                if (CategoryId > 0)
                {
                    SqlCommand cmd = new SqlCommand("EventStreams_GetByCategoryIdInRangeOfDate");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                    if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                    if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
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
        public List<EventStreams> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, int EventStreamId, short CategoryId, string EventStreamName, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<EventStreams> RetVal = new List<EventStreams>();
            try
            {
                SqlCommand cmd = new SqlCommand("EventStreams_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", EventStreamId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamName", StringUtil.InjectionString(EventStreamName)));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
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
        public List<EventStreams> Search(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, int EventStreamId, short CategoryId, string EventStreamName, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, EventStreamId, CategoryId, EventStreamName, ReviewStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}
