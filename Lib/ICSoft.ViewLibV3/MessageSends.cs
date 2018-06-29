
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;

namespace ICSoft.ViewLibV3
{
    public class MessageSends
    {
        private int _MessageSendId;
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
        private DBAccess db;
        //-----------------------------------------------------------------
        public MessageSends()
        {
            db = new DBAccess(Constants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MessageSends(string constr)
        {
            db = new DBAccess((constr == "") ? Constants.CMS_CONSTR : constr);
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
        public byte Insert()
        {
            byte RetVal = 0;
            byte Replicated = 1;
            int ActUserId = 0;
            short SysMessageId = 0;
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

    }
}

