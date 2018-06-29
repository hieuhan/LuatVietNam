
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLibEstate
{
    public class MessageTypes
    {
        private byte _MessageTypeId;
        private string _MessageTypeName;
        private string _MessageTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public MessageTypes()
        {
            db = new DBAccess(EstateConstants.CONN_ESTATE);
        }
        //-----------------------------------------------------------------        
        public MessageTypes(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~MessageTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte MessageTypeId
        {
            get { return _MessageTypeId; }
            set { _MessageTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string MessageTypeName
        {
            get { return _MessageTypeName; }
            set { _MessageTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string MessageTypeDesc
        {
            get { return _MessageTypeDesc; }
            set { _MessageTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<MessageTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MessageTypes> l_MessageTypes = new List<MessageTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MessageTypes m_MessageTypes = new MessageTypes(db.ConnectionString);
                    m_MessageTypes.MessageTypeId = smartReader.GetByte("MessageTypeId");
                    m_MessageTypes.MessageTypeName = smartReader.GetString("MessageTypeName");
                    m_MessageTypes.MessageTypeDesc = smartReader.GetString("MessageTypeDesc");

                    l_MessageTypes.Add(m_MessageTypes);
                }
                reader.Close();
                return l_MessageTypes;
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
        public List<MessageTypes> GetList()
        {
            List<MessageTypes> RetVal = new List<MessageTypes>();
            try
            {
                string sql = "SELECT * FROM MessageTypes";
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
        public List<MessageTypes> GetListOrderBy(string OrderBy)
        {
            List<MessageTypes> RetVal = new List<MessageTypes>();
            try
            {
                string sql = "SELECT * FROM MessageTypes ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<MessageTypes> GetListByMessageTypeId(byte MessageTypeId)
        {
            List<MessageTypes> RetVal = new List<MessageTypes>();
            try
            {
                if (MessageTypeId > 0)
                {
                    string sql = "SELECT * FROM MessageTypes WHERE (MessageTypeId=" + MessageTypeId.ToString() + ")";
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
        public MessageTypes Get(byte MessageTypeId)
        {
            MessageTypes RetVal = new MessageTypes();
            try
            {
                List<MessageTypes> list = GetListByMessageTypeId(MessageTypeId);
                if (list.Count > 0)
                {
                    RetVal = (MessageTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static MessageTypes Static_Get(byte MessageTypeId, List<MessageTypes> list)
        {
            MessageTypes RetVal = new MessageTypes();
            try
            {
                RetVal = list.Find(i => i.MessageTypeId == MessageTypeId);
                if (RetVal == null) RetVal = new MessageTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MessageTypes Get()
        {
            return Get(this.MessageTypeId);
        }
        //-------------------------------------------------------------- 
        public static MessageTypes Static_Get(byte MessageTypeId)
        {
            return new MessageTypes().Get(MessageTypeId);
        }
        //--------------------------------------------------------------     
        public static List<MessageTypes> Static_GetList()
        {
            List<MessageTypes> RetVal = new List<MessageTypes>();
            try
            {
                MessageTypes m_MessageTypes = new MessageTypes();
                RetVal = m_MessageTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

