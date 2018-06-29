
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;

namespace ICSoft.ViewLibV3
{
    public class MessageTemplates
    {   
	    private short _MessageTemplateId;
	    private string _MessageName;
	    private string _SendFrom;
	    private string _Title;
	    private string _MsgContent;
        private short _SiteId;
        private byte _SendMethodId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public MessageTemplates()
        {
            db = new DBAccess(Constants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public MessageTemplates(string constr)
        {
            db = new DBAccess((constr == "") ? Constants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MessageTemplates()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short MessageTemplateId
        {
		    get { return _MessageTemplateId; }
		    set { _MessageTemplateId = value; }
	    }
        //-----------------------------------------------------------------
        public string MessageName
		{
            get { return _MessageName; }
		    set { _MessageName = value; }
		}    
        //-----------------------------------------------------------------
        public string SendFrom
		{
            get { return _SendFrom; }
		    set { _SendFrom = value; }
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
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public byte SendMethodId
		{
            get { return _SendMethodId; }
		    set { _SendMethodId = value; }
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
    
        private List<MessageTemplates> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MessageTemplates> l_MessageTemplates = new List<MessageTemplates>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MessageTemplates m_MessageTemplates = new MessageTemplates(db.ConnectionString);
                    m_MessageTemplates.MessageTemplateId = smartReader.GetInt16("MessageTemplateId");
                    m_MessageTemplates.MessageName = smartReader.GetString("MessageName");
                    m_MessageTemplates.SendFrom = smartReader.GetString("SendFrom");
                    m_MessageTemplates.Title = smartReader.GetString("Title");
                    m_MessageTemplates.MsgContent = smartReader.GetString("MsgContent");
                    m_MessageTemplates.SiteId = smartReader.GetInt16("SiteId");
                    m_MessageTemplates.SendMethodId = smartReader.GetByte("SendMethodId");
                    m_MessageTemplates.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MessageTemplates.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_MessageTemplates.Add(m_MessageTemplates);
                }
                reader.Close();
                return l_MessageTemplates;
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
        //--------------------------------------------------------------  
        public List<MessageTemplates> GetListByMessageTemplateId(short MessageTemplateId)
        {
            List<MessageTemplates> RetVal = new List<MessageTemplates>();
            try
            {
                if (MessageTemplateId > 0)
                {
                    string sql = "SELECT * FROM MessageTemplates WHERE (MessageTemplateId=" + MessageTemplateId.ToString() + ")";
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
        public MessageTemplates GetByMessageTemplateName(string MessageTemplateName)
        {
            MessageTemplates RetVal = new MessageTemplates();
            try
            {
                string sql = "SELECT * FROM MessageTemplates WHERE (MessageName='" + MessageTemplateName + "')";
                SqlCommand cmd = new SqlCommand(sql);
                List<MessageTemplates> list = Init(cmd);
                if (list.Count > 0) RetVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MessageTemplates Get(short MessageTemplateId)
        {
            MessageTemplates RetVal = new MessageTemplates(db.ConnectionString);
            try
            {
                List<MessageTemplates> list = GetListByMessageTemplateId(MessageTemplateId);
                if (list.Count > 0)
                {
                    RetVal = (MessageTemplates)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MessageTemplates Get()
        {
            return Get(this.MessageTemplateId);
        }
        //-------------------------------------------------------------- 
        public static MessageTemplates Static_Get(short MessageTemplateId)
        {
            return new MessageTemplates().Get(MessageTemplateId);
        }
    } 
}

