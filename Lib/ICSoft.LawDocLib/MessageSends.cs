
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
    public class MessageSends
    {
        private int _MessageSendId;
        private int _CampaignId;
        private short _SiteId;
        private short _MessageTemplateId;
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
        //-----------------------------------------------------------------
        public MessageSends()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MessageSends(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MessageSends()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MessageSendId
        {
            get { return _MessageSendId; }
            set { _MessageSendId = value; }
        }
        //-----------------------------------------------------------------    
        public int CampaignId
        {
            get { return _CampaignId; }
            set { _CampaignId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public short MessageTemplateId
        {
            get { return _MessageTemplateId; }
            set { _MessageTemplateId = value; }
        }
        //-----------------------------------------------------------------
        public string SendFrom
        {
            get { return _SendFrom; }
            set { _SendFrom = value; }
        }
        //-----------------------------------------------------------------
        public string SendTo
        {
            get { return _SendTo; }
            set { _SendTo = value; }
        }
        //-----------------------------------------------------------------
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //-----------------------------------------------------------------
        public string MsgContent
        {
            get { return _MsgContent; }
            set { _MsgContent = value; }
        }
        //-----------------------------------------------------------------
        public byte SendMethodId
        {
            get { return _SendMethodId; }
            set { _SendMethodId = value; }
        }
        //-----------------------------------------------------------------
        public byte SendStatusId
        {
            get { return _SendStatusId; }
            set { _SendStatusId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime SendTime
        {
            get { return _SendTime; }
            set { _SendTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime OpenMailTime
        {
            get { return _OpenMailTime; }
            set { _OpenMailTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ClickLinkTime
        {
            get { return _ClickLinkTime; }
            set { _ClickLinkTime = value; }
        }
        //-----------------------------------------------------------------

        private List<MessageSends> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MessageSends> l_MessageSends = new List<MessageSends>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MessageSends m_MessageSends = new MessageSends(db.ConnectionString);
                    m_MessageSends.MessageSendId = smartReader.GetInt32("MessageSendId");
                    m_MessageSends.CampaignId = smartReader.GetInt32("CampaignId");
                    m_MessageSends.SiteId = smartReader.GetInt16("SiteId");
                    m_MessageSends.MessageTemplateId = smartReader.GetInt16("MessageTemplateId");
                    m_MessageSends.SendFrom = smartReader.GetString("SendFrom");
                    m_MessageSends.SendTo = smartReader.GetString("SendTo");
                    m_MessageSends.Title = smartReader.GetString("Title");
                    m_MessageSends.MsgContent = smartReader.GetString("MsgContent");
                    m_MessageSends.SendMethodId = smartReader.GetByte("SendMethodId");
                    m_MessageSends.SendStatusId = smartReader.GetByte("SendStatusId");
                    m_MessageSends.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_MessageSends.SendTime = smartReader.GetDateTime("SendTime");
                    m_MessageSends.OpenMailTime = smartReader.GetDateTime("OpenMailTime");
                    m_MessageSends.ClickLinkTime = smartReader.GetDateTime("ClickLinkTime");
                    l_MessageSends.Add(m_MessageSends);
                }
                reader.Close();
                return l_MessageSends;
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
                SqlCommand cmd = new SqlCommand("MessageSends_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@MessageTemplateId", this.MessageTemplateId));
                cmd.Parameters.Add(new SqlParameter("@SendFrom", this.SendFrom));
                cmd.Parameters.Add(new SqlParameter("@SendTo", this.SendTo));
                cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
                cmd.Parameters.Add(new SqlParameter("@MsgContent", this.MsgContent));
                cmd.Parameters.Add(new SqlParameter("@SendMethodId", this.SendMethodId));
                cmd.Parameters.Add(new SqlParameter("@SendStatusId", this.SendStatusId));
                cmd.Parameters.Add(new SqlParameter("@SendTime", this.SendTime));
                cmd.Parameters.Add("@MessageSendId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MessageSendId = Convert.ToInt32((cmd.Parameters["@MessageSendId"].Value == null) ? 0 : (cmd.Parameters["@MessageSendId"].Value));
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
        public byte UpdateSendStatusId(ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("MessageSends_UpdateSendStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageSendId", this.MessageSendId));
                cmd.Parameters.Add(new SqlParameter("@SendStatusId", this.SendStatusId));
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
        public void MessageSends_Resend(int MessageSendId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MessageSends_Resend");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageSendId", MessageSendId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------------------- 
        public List<MessageSends> GetList()
        {
            List<MessageSends> RetVal = new List<MessageSends>();
            try
            {
                string sql = "SELECT * FROM MessageSends";
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
        public static List<MessageSends> Static_GetList(string ConStr)
        {
            List<MessageSends> RetVal = new List<MessageSends>();
            try
            {
                MessageSends m_MessageSends = new MessageSends(ConStr);
                RetVal = m_MessageSends.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MessageSends> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<MessageSends> GetListByMessageSendId(int MessageSendId)
        {
            List<MessageSends> RetVal = new List<MessageSends>();
            try
            {
                if (MessageSendId > 0)
                {
                    SqlCommand cmd = new SqlCommand("MessageSends_Get");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@MessageSendId", MessageSendId));
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
        public MessageSends Get(int MessageSendId)
        {
            MessageSends RetVal = new MessageSends(db.ConnectionString);
            try
            {
                List<MessageSends> list = GetListByMessageSendId(MessageSendId);
                if (list.Count > 0)
                {
                    RetVal = (MessageSends)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MessageSends Get()
        {
            return Get(this.MessageSendId);
        }
        //-------------------------------------------------------------- 
        public static MessageSends Static_Get(int MessageSendId)
        {
            return new MessageSends().Get(MessageSendId);
        }
        //--------------------------------------------------------------     
        public List<MessageSends> MessageSends_GetListToGateway(byte SendMethodId, int RowAmount)
        {
            List<MessageSends> RetVal = new List<MessageSends>();
            try
            {
                SqlCommand cmd = new SqlCommand("MessageSends_GetListToGateway");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SendMethodId", SendMethodId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<MessageSends> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short MessageTemplateId, string SendFrom, string SendTo, string Title,
                                            byte SendMethodId, byte SendStatusId, string DateFrom, string DateTo, ref int RowCount, int CampaignId = 0, byte hasOpenMail = 0, byte hasClickLink = 0)
        {
            List<MessageSends> RetVal = new List<MessageSends>();
            try
            {
                SqlCommand cmd = new SqlCommand("MessageSends_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@MessageTemplateId", MessageTemplateId));
                cmd.Parameters.Add(new SqlParameter("@SendFrom", StringUtil.InjectionString(SendFrom)));
                cmd.Parameters.Add(new SqlParameter("@SendTo", StringUtil.InjectionString(SendTo)));
                cmd.Parameters.Add(new SqlParameter("@Title", StringUtil.InjectionString(Title)));
                cmd.Parameters.Add(new SqlParameter("@SendMethodId", SendMethodId));
                cmd.Parameters.Add(new SqlParameter("@SendStatusId", SendStatusId));
                cmd.Parameters.Add(new SqlParameter("@CampaignId", CampaignId));
                cmd.Parameters.Add(new SqlParameter("@HasOpenMail", hasOpenMail));
                cmd.Parameters.Add(new SqlParameter("@HasClickLink", hasClickLink));
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

        public int MessageSends_AddOrder(int customerId, int orderId, short servicePackageId)
        {
            int retVal = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand("MessageSends_AddOrder") {CommandType = CommandType.StoredProcedure})
                {
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", customerId));
                    cmd.Parameters.Add(new SqlParameter("@OrderId", orderId));
                    cmd.Parameters.Add(new SqlParameter("@ServicePackageId", servicePackageId));
                    cmd.Parameters.Add("@MessageSendId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    db.ExecuteSQL(cmd);
                    retVal = Convert.ToInt32(cmd.Parameters["@MessageSendId"].Value == DBNull.Value ? "0" : cmd.Parameters["@MessageSendId"].Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        /// <summary>
        /// Cập nhật thông tin khi mở mail
        /// </summary>
        public void MessageSends_UpdateOpenMailTime_Quick()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("MessageSends_UpdateOpenMailTime_Quick") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@MessageSendId", this.MessageSendId));
                    cmd.Parameters.Add(new SqlParameter("@CampaignId", this.CampaignId));
                    cmd.Parameters.Add(new SqlParameter("@SendTime", this.SendTime));
                    db.ExecuteSQL(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cập nhật thông tin click link trong mail
        /// </summary>
        public void MessageSends_UpdateClickLinkTime_Quick()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("MessageSends_UpdateClickLinkTime_Quick") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@MessageSendId", this.MessageSendId));
                    cmd.Parameters.Add(new SqlParameter("@CampaignId", this.CampaignId));
                    cmd.Parameters.Add(new SqlParameter("@SendTime", this.SendTime));
                    db.ExecuteSQL(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

