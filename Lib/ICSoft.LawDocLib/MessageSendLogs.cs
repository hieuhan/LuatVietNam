using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class MessageSendLogs
    {
        #region Private Properties
        private int _MessageSendId;
        private short _SiteId;
        private short _MessageTemplateId;
        private int _CampaignId;
        private string _SendFrom;
        private string _SendTo;
        private string _Title;
        private string _MsgContent;
        private byte _SendMethodId;
        private byte _SendStatusId;
        private DateTime _CrDateTime;
        private DateTime _SendTime;
        private DateTime _OpenMailTime;
        private DateTime _ClickLinkTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public MessageSendLogs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MessageSendLogs(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~MessageSendLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public int MessageSendId
        {
            get
            {
                return _MessageSendId;
            }
            set
            {
                _MessageSendId = value;
            }
        }

        public short SiteId
        {
            get
            {
                return _SiteId;
            }
            set
            {
                _SiteId = value;
            }
        }
        public short MessageTemplateId
        {
            get
            {
                return _MessageTemplateId;
            }
            set
            {
                _MessageTemplateId = value;
            }
        }
        public int CampaignId
        {
            get
            {
                return _CampaignId;
            }
            set
            {
                _CampaignId = value;
            }
        }
        public string SendFrom
        {
            get
            {
                return _SendFrom;
            }
            set
            {
                _SendFrom = value;
            }
        }
        public string SendTo
        {
            get
            {
                return _SendTo;
            }
            set
            {
                _SendTo = value;
            }
        }
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        public string MsgContent
        {
            get
            {
                return _MsgContent;
            }
            set
            {
                _MsgContent = value;
            }
        }
        public byte SendMethodId
        {
            get
            {
                return _SendMethodId;
            }
            set
            {
                _SendMethodId = value;
            }
        }
        public byte SendStatusId
        {
            get
            {
                return _SendStatusId;
            }
            set
            {
                _SendStatusId = value;
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
        public DateTime SendTime
        {
            get
            {
                return _SendTime;
            }
            set
            {
                _SendTime = value;
            }
        }
        public DateTime OpenMailTime
        {
            get
            {
                return _OpenMailTime;
            }
            set
            {
                _OpenMailTime = value;
            }
        }
        public DateTime ClickLinkTime
        {
            get
            {
                return _ClickLinkTime;
            }
            set
            {
                _ClickLinkTime = value;
            }
        }



        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<MessageSendLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MessageSendLogs> l_MessageSendLogs = new List<MessageSendLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MessageSendLogs m_MessageSendLogs = new MessageSendLogs();
                    m_MessageSendLogs.MessageSendId = smartReader.GetInt32("MessageSendId");
                    m_MessageSendLogs.SiteId = smartReader.GetInt16("SiteId");
                    m_MessageSendLogs.MessageTemplateId = smartReader.GetInt16("MessageTemplateId");
                    m_MessageSendLogs.CampaignId = smartReader.GetInt32("CampaignId");
                    m_MessageSendLogs.SendFrom = smartReader.GetString("SendFrom");
                    m_MessageSendLogs.SendTo = smartReader.GetString("SendTo");
                    m_MessageSendLogs.Title = smartReader.GetString("Title");
                    m_MessageSendLogs.MsgContent = smartReader.GetString("MsgContent");
                    m_MessageSendLogs.SendMethodId = smartReader.GetByte("SendMethodId");
                    m_MessageSendLogs.SendStatusId = smartReader.GetByte("SendStatusId");
                    m_MessageSendLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_MessageSendLogs.SendTime = smartReader.GetDateTime("SendTime");
                    m_MessageSendLogs.OpenMailTime = smartReader.GetDateTime("OpenMailTime");
                    m_MessageSendLogs.ClickLinkTime = smartReader.GetDateTime("ClickLinkTime");
                    l_MessageSendLogs.Add(m_MessageSendLogs);
                }
                reader.Close();
                return l_MessageSendLogs;
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
        public byte Insert(byte Replicated, int ActUserId, ref int SysMessageId)
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
        public byte Update(byte Replicated, int ActUserId, ref int SysMessageId)
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
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("MessageSendLogs_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@MessageTemplateId", this.MessageTemplateId));
                cmd.Parameters.Add(new SqlParameter("@CampaignId", this.CampaignId));
                cmd.Parameters.Add(new SqlParameter("@SendFrom", this.SendFrom));
                cmd.Parameters.Add(new SqlParameter("@SendTo", this.SendTo));
                cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
                cmd.Parameters.Add(new SqlParameter("@MsgContent", this.MsgContent));
                cmd.Parameters.Add(new SqlParameter("@SendMethodId", this.SendMethodId));
                cmd.Parameters.Add(new SqlParameter("@SendStatusId", this.SendStatusId));
                cmd.Parameters.Add(new SqlParameter("@SendTime", this.SendTime));
                cmd.Parameters.Add(new SqlParameter("@OpenMailTime", this.OpenMailTime));
                cmd.Parameters.Add(new SqlParameter("@ClickLinkTime", this.ClickLinkTime));
                cmd.Parameters.Add(new SqlParameter("@MessageSendId", this.MessageSendId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MessageSendId = int.Parse(cmd.Parameters["@MessageSendId"].Value.ToString());
                SysMessageId = Convert.ToInt32((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("MessageSendLogs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MessageSendId", this.MessageSendId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = int.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                RetVal = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public MessageSendLogs Get()
        {
            MessageSendLogs retVal = new MessageSendLogs();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<MessageSendLogs> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 

        public List<MessageSendLogs> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MessageSendLogs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MessageSendId", this.MessageSendId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@MessageTemplateId", this.MessageTemplateId));
                cmd.Parameters.Add(new SqlParameter("@CampaignId", this.CampaignId));
                cmd.Parameters.Add(new SqlParameter("@SendFrom", this.SendFrom));
                cmd.Parameters.Add(new SqlParameter("@SendTo", this.SendTo));
                cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
                cmd.Parameters.Add(new SqlParameter("@MsgContent", this.MsgContent));
                cmd.Parameters.Add(new SqlParameter("@SendMethodId", this.SendMethodId));
                cmd.Parameters.Add(new SqlParameter("@SendStatusId", this.SendStatusId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<MessageSendLogs> list = Init(cmd);
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
