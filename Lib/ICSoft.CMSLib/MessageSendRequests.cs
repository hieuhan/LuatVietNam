using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class MessageSendRequests
    {
        #region Private Properties
        private int _MessageSendRequestId;
        private byte _RequestTypeId;
        private int _DocId;
        private DateTime _BeginDateTime;
        private DateTime _EndDateTime;
        private byte _RequestStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DateTime _GenStartTime;
        private DateTime _GenEndTime;
        private string _EventName;
        private int _TotalEmail;
        private int _TotalSendSuccess;
        private int _TotalOpenMail;
        private int _TotalClickLink;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public MessageSendRequests()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MessageSendRequests(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~MessageSendRequests()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public int MessageSendRequestId
        {
            get
            {
                return _MessageSendRequestId;
            }
            set
            {
                _MessageSendRequestId = value;
            }
        }

        public byte RequestTypeId
        {
            get
            {
                return _RequestTypeId;
            }
            set
            {
                _RequestTypeId = value;
            }
        }
        public int DocId
        {
            get
            {
                return _DocId;
            }
            set
            {
                _DocId = value;
            }
        }
        public DateTime BeginDateTime
        {
            get
            {
                return _BeginDateTime;
            }
            set
            {
                _BeginDateTime = value;
            }
        }
        public DateTime EndDateTime
        {
            get
            {
                return _EndDateTime;
            }
            set
            {
                _EndDateTime = value;
            }
        }
        public byte RequestStatusId
        {
            get
            {
                return _RequestStatusId;
            }
            set
            {
                _RequestStatusId = value;
            }
        }
        public int CrUserId
        {
            get
            {
                return _CrUserId;
            }
            set
            {
                _CrUserId = value;
            }
        }
        public DateTime CrDateTime
        {
            get
            {
                return _CrDateTime;
            }
            set
            {
                _CrDateTime = value;
            }
        }
        public DateTime GenStartTime
        {
            get
            {
                return _GenStartTime;
            }
            set
            {
                _GenStartTime = value;
            }
        }
        public DateTime GenEndTime
        {
            get
            {
                return _GenEndTime;
            }
            set
            {
                _GenEndTime = value;
            }
        }
        public string EventName
        {
            get
            {
                return _EventName;
            }
            set
            {
                _EventName = value;
            }
        }

        public int TotalEmail
        {
            get
            {
                return _TotalEmail;
            }
            set
            {
                _TotalEmail = value;
            }
        }

        public int TotalSendSuccess
        {
            get
            {
                return _TotalSendSuccess;
            }
            set
            {
                _TotalSendSuccess = value;
            }
        }

        public int TotalOpenMail
        {
            get
            {
                return _TotalOpenMail;
            }
            set
            {
                _TotalOpenMail = value;
            }
        }

        public int TotalClickLink
        {
            get
            {
                return _TotalClickLink;
            }
            set
            {
                _TotalClickLink = value;
            }
        }

        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<MessageSendRequests> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MessageSendRequests> l_MessageSendRequests = new List<MessageSendRequests>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MessageSendRequests m_MessageSendRequests = new MessageSendRequests();
                    m_MessageSendRequests.MessageSendRequestId = smartReader.GetInt32("MessageSendRequestId");
                    m_MessageSendRequests.RequestTypeId = smartReader.GetByte("RequestTypeId");
                    m_MessageSendRequests.DocId = smartReader.GetInt32("DocId");
                    m_MessageSendRequests.BeginDateTime = smartReader.GetDateTime("BeginDateTime");
                    m_MessageSendRequests.EndDateTime = smartReader.GetDateTime("EndDateTime");
                    m_MessageSendRequests.RequestStatusId = smartReader.GetByte("RequestStatusId");
                    m_MessageSendRequests.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MessageSendRequests.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_MessageSendRequests.GenStartTime = smartReader.GetDateTime("GenStartTime");
                    m_MessageSendRequests.GenEndTime = smartReader.GetDateTime("GenEndTime");
                    m_MessageSendRequests.EventName = smartReader.GetString("EventName");
                    m_MessageSendRequests.TotalEmail = smartReader.GetInt32("TotalEmail");
                    m_MessageSendRequests.TotalSendSuccess = smartReader.GetInt32("TotalSendSuccess");
                    m_MessageSendRequests.TotalOpenMail = smartReader.GetInt32("TotalOpenMail");
                    m_MessageSendRequests.TotalClickLink = smartReader.GetInt32("TotalClickLink");
                    l_MessageSendRequests.Add(m_MessageSendRequests);
                }
                reader.Close();
                return l_MessageSendRequests;
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
                SqlCommand cmd = new SqlCommand("MessageSendRequests_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RequestTypeId", this.RequestTypeId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(BeginDateTime == DateTime.MinValue
                    ? new SqlParameter("@BeginDateTime", DBNull.Value)
                    : new SqlParameter("@BeginDateTime", this.BeginDateTime));
                cmd.Parameters.Add(EndDateTime == DateTime.MinValue
                    ? new SqlParameter("@EndDateTime", DBNull.Value)
                    : new SqlParameter("@EndDateTime", this.EndDateTime));
                cmd.Parameters.Add(new SqlParameter("@RequestStatusId", this.RequestStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(GenStartTime == DateTime.MinValue
                    ? new SqlParameter("@GenStartTime", DBNull.Value)
                    : new SqlParameter("@GenStartTime", this.GenStartTime));
                cmd.Parameters.Add(GenEndTime == DateTime.MinValue
                    ? new SqlParameter("@GenEndTime", DBNull.Value)
                    : new SqlParameter("@GenEndTime", this.GenEndTime));
                cmd.Parameters.Add(new SqlParameter("@EventName", this.EventName));
                cmd.Parameters.Add(new SqlParameter("@MessageSendRequestId", this.MessageSendRequestId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MessageSendRequestId = int.Parse(cmd.Parameters["@MessageSendRequestId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("MessageSendRequests_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageSendRequestId", this.MessageSendRequestId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = short.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                RetVal = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public MessageSendRequests Get()
        {
            MessageSendRequests retVal = new MessageSendRequests();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<MessageSendRequests> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<MessageSendRequests> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount, string SearchByDate = "CrDateTime")
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MessageSendRequests_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MessageSendRequestId", this.MessageSendRequestId));
                cmd.Parameters.Add(new SqlParameter("@RequestTypeId", this.RequestTypeId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@RequestStatusId", this.RequestStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<MessageSendRequests> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        #endregion
    }
}
