
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
            db = new DBAccess(DocConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public MessageTemplates(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
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
                SqlCommand cmd = new SqlCommand("MessageTemplates_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MessageName", this.MessageName));
                cmd.Parameters.Add(new SqlParameter("@SendFrom", this.SendFrom));
                cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
                cmd.Parameters.Add(new SqlParameter("@MsgContent", this.MsgContent));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@SendMethodId", this.SendMethodId));
                cmd.Parameters.Add(new SqlParameter("@MessageTemplateId", this.MessageTemplateId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MessageTemplateId =Convert.ToInt16((cmd.Parameters["@MessageTemplateId"].Value == null) ? 0 : (cmd.Parameters["@MessageTemplateId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("MessageTemplates_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MessageTemplateId",this.MessageTemplateId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<MessageTemplates> GetList()
        {
            List<MessageTemplates> RetVal = new List<MessageTemplates>();
            try
            {
                string sql = "SELECT * FROM MessageTemplates";
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
        public static List<MessageTemplates> Static_GetList(string ConStr)
        {
            List<MessageTemplates> RetVal = new List<MessageTemplates>();
            try
            {
                MessageTemplates m_MessageTemplates = new MessageTemplates(ConStr);
                RetVal = m_MessageTemplates.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MessageTemplates> Static_GetList()
        {
            return Static_GetList("");
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
        //-----------------------------------------------------------------------------
        public static MessageTemplates Static_Get(short MessageTemplateId, List<MessageTemplates> lList)
        {
            MessageTemplates RetVal = new MessageTemplates();
            if (MessageTemplateId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.MessageTemplateId == MessageTemplateId);
                if (RetVal == null) RetVal = new MessageTemplates();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<MessageTemplates> MessageTemplates_GetList(int ActUserId, string OrderBy, string MessageName, string SendFrom, byte SendMethodId)
        {
            List<MessageTemplates> RetVal = new List<MessageTemplates>();
            try
            {
                SqlCommand cmd = new SqlCommand("MessageTemplates_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MessageName", StringUtil.InjectionString(MessageName)));
                cmd.Parameters.Add(new SqlParameter("@SendFrom", StringUtil.InjectionString(SendFrom)));
                cmd.Parameters.Add(new SqlParameter("@SendMethodId", SendMethodId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return RetVal;
        }
        //--------------------------------------------------------------     
       
    } 
}

