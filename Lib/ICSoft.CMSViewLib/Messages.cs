
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;
using ICSoft.CMSLibEstate;

namespace ICSoft.CMSViewLib
{
    public class Messages
    {
        private int _MessageId;
        private byte _MessageTypeId;
        private string _FromUser;
        private string _ToUser;
        private string _Subject;
        private string _MsgContent;
        private byte _HasRead;
        private byte _HasSave;
        private byte _HasStar;
        private int _UserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Messages()
        {
            db = new DBAccess(EstateConstants.CONN_ESTATE);
        }
        //-----------------------------------------------------------------        
        public Messages(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Messages()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MessageId
        {
            get { return _MessageId; }
            set { _MessageId = value; }
        }
        //-----------------------------------------------------------------
        public byte MessageTypeId
        {
            get { return _MessageTypeId; }
            set { _MessageTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string FromUser
        {
            get { return _FromUser; }
            set { _FromUser = value; }
        }
        //-----------------------------------------------------------------
        public string ToUser
        {
            get { return _ToUser; }
            set { _ToUser = value; }
        }
        //-----------------------------------------------------------------
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        //-----------------------------------------------------------------
        public string MsgContent
        {
            get { return _MsgContent; }
            set { _MsgContent = value; }
        }
        //-----------------------------------------------------------------
        public byte HasRead
        {
            get { return _HasRead; }
            set { _HasRead = value; }
        }
        //-----------------------------------------------------------------
        public byte HasSave
        {
            get { return _HasSave; }
            set { _HasSave = value; }
        }
        //-----------------------------------------------------------------
        public byte HasStar
        {
            get { return _HasStar; }
            set { _HasStar = value; }
        }
        //-----------------------------------------------------------------
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<Messages> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Messages> l_Messages = new List<Messages>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Messages m_Messages = new Messages(db.ConnectionString);
                    m_Messages.MessageId = smartReader.GetInt32("MessageId");
                    m_Messages.MessageTypeId = smartReader.GetByte("MessageTypeId");
                    m_Messages.FromUser = smartReader.GetString("FromUser");
                    m_Messages.ToUser = smartReader.GetString("ToUser");
                    m_Messages.Subject = smartReader.GetString("Subject");
                    m_Messages.MsgContent = smartReader.GetString("MsgContent");
                    m_Messages.HasRead = smartReader.GetByte("HasRead");
                    m_Messages.HasSave = smartReader.GetByte("HasSave");
                    m_Messages.HasStar = smartReader.GetByte("HasStar");
                    m_Messages.UserId = smartReader.GetInt32("UserId");
                    m_Messages.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Messages.Add(m_Messages);
                }
                reader.Close();
                return l_Messages;
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
        public void Insert_Quick()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Messages_Insert_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageTypeId", this.MessageTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromUser", this.FromUser));
                cmd.Parameters.Add(new SqlParameter("@ToUser", this.ToUser));
                cmd.Parameters.Add(new SqlParameter("@Subject", this.Subject));
                cmd.Parameters.Add(new SqlParameter("@MsgContent", this.MsgContent));
                cmd.Parameters.Add(new SqlParameter("@HasRead", this.HasRead));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                cmd.Parameters.Add("@MessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MessageId = (cmd.Parameters["@MessageId"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["@MessageId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-----------------------------------------------------------
        public void UpdateHasRead()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Messages_UpdateHasRead");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageId", this.MessageId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------------------------------------------
        public void UpdateHasRead_AllByUserId()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Messages_UpdateHasRead_AllByUserId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------------------------------------------
        public void UpdateHasSave()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Messages_UpdateHasSave");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageId", this.MessageId));
                cmd.Parameters.Add(new SqlParameter("@HasSave", this.HasSave));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------------------------------------------
        public void UpdateHasStar()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Messages_UpdateHasStar");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MessageId", this.MessageId));
                cmd.Parameters.Add(new SqlParameter("@HasStar", this.HasStar));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------            
        public void Delete(int ActUserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Messages_Delete_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MessageId", this.MessageId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        public List<Messages> GetListByMessageId(int MessageId)
        {
            List<Messages> RetVal = new List<Messages>();
            try
            {
                if (MessageId > 0)
                {
                    string sql = "SELECT * FROM Messages WHERE (MessageId=" + MessageId.ToString() + ")";
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
        public Messages Get(int MessageId)
        {
            Messages RetVal = new Messages();
            try
            {
                List<Messages> list = GetListByMessageId(MessageId);
                if (list.Count > 0)
                {
                    RetVal = (Messages)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Messages Static_Get(int MessageId, List<Messages> list)
        {
            Messages RetVal = new Messages();
            try
            {
                RetVal = list.Find(i => i.MessageId == MessageId);
                if (RetVal == null) RetVal = new Messages();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Messages Get()
        {
            return Get(this.MessageId);
        }
        //-------------------------------------------------------------- 
        public static Messages Static_Get(int MessageId)
        {
            return new Messages().Get(MessageId);
        }
        //--------------------------------------------------------------     
        public List<Messages> GetPage(int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Messages> RetVal = new List<Messages>();
            try
            {
                SqlCommand cmd = new SqlCommand("Messages_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@MessageTypeId", this.MessageTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromUser", this.FromUser));
                cmd.Parameters.Add(new SqlParameter("@ToUser", this.ToUser));
                cmd.Parameters.Add(new SqlParameter("@MsgContent", this.MsgContent));
                cmd.Parameters.Add(new SqlParameter("@HasRead", this.HasRead));
                cmd.Parameters.Add(new SqlParameter("@HasSave", this.HasSave));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
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
    }
}

