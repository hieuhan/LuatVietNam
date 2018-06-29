
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
    public class Newsletters
    {
        private int _NewsletterId;
        private short _NewsletterGroupId;
        private string _SendFrom;
        private string _Title;
        private string _MsgContent;
        private byte _NewsletterStatusId;
        private DateTime _ScheduleSendTime;
        private DateTime _SendTime;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Newsletters()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Newsletters(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Newsletters()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int NewsletterId
        {
            get { return _NewsletterId; }
            set { _NewsletterId = value; }
        }
        //-----------------------------------------------------------------
        public short NewsletterGroupId
        {
            get { return _NewsletterGroupId; }
            set { _NewsletterGroupId = value; }
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
        public byte NewsletterStatusId
        {
            get { return _NewsletterStatusId; }
            set { _NewsletterStatusId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ScheduleSendTime
        {
            get { return _ScheduleSendTime; }
            set { _ScheduleSendTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime SendTime
        {
            get { return _SendTime; }
            set { _SendTime = value; }
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

        private List<Newsletters> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Newsletters> l_Newsletters = new List<Newsletters>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Newsletters m_Newsletters = new Newsletters(db.ConnectionString);
                    m_Newsletters.NewsletterId = smartReader.GetInt32("NewsletterId");
                    m_Newsletters.NewsletterGroupId = smartReader.GetInt16("NewsletterGroupId");
                    m_Newsletters.SendFrom = smartReader.GetString("SendFrom");
                    m_Newsletters.Title = smartReader.GetString("Title");
                    m_Newsletters.MsgContent = smartReader.GetString("MsgContent");
                    m_Newsletters.NewsletterStatusId = smartReader.GetByte("NewsletterStatusId");
                    m_Newsletters.ScheduleSendTime = smartReader.GetDateTime("ScheduleSendTime");
                    m_Newsletters.SendTime = smartReader.GetDateTime("SendTime");
                    m_Newsletters.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Newsletters.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Newsletters.Add(m_Newsletters);
                }
                reader.Close();
                return l_Newsletters;
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
        //--------------------------------------------------------------
        public byte UpdateNewsletterStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Newsletters_UpdateNewsletterStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterStatusId", this.NewsletterStatusId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterId", this.NewsletterId));                
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Newsletters_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupId", this.NewsletterGroupId));
                cmd.Parameters.Add(new SqlParameter("@SendFrom", this.SendFrom));
                cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
                cmd.Parameters.Add(new SqlParameter("@MsgContent", this.MsgContent));
                cmd.Parameters.Add(new SqlParameter("@NewsletterStatusId", this.NewsletterStatusId));
                cmd.Parameters.Add(new SqlParameter("@ScheduleSendTime", this.ScheduleSendTime));
                cmd.Parameters.Add(new SqlParameter("@NewsletterId", this.NewsletterId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.NewsletterId = Convert.ToInt32((cmd.Parameters["@NewsletterId"].Value == null) ? 0 : (cmd.Parameters["@NewsletterId"].Value));
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
                SqlCommand cmd = new SqlCommand("Newsletters_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterId", this.NewsletterId));
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
        public List<Newsletters> GetList()
        {
            List<Newsletters> RetVal = new List<Newsletters>();
            try
            {
                string sql = "SELECT * FROM Newsletters";
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
        public static List<Newsletters> Static_GetList(string ConStr)
        {
            List<Newsletters> RetVal = new List<Newsletters>();
            try
            {
                Newsletters m_Newsletters = new Newsletters(ConStr);
                RetVal = m_Newsletters.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Newsletters> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<Newsletters> GetListByNewsletterId(int NewsletterId)
        {
            List<Newsletters> RetVal = new List<Newsletters>();
            try
            {
                if (NewsletterId > 0)
                {
                    string sql = "SELECT * FROM Newsletters WHERE (NewsletterId=" + NewsletterId.ToString() + ")";
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
        public Newsletters Get(int NewsletterId)
        {
            Newsletters RetVal = new Newsletters(db.ConnectionString);
            try
            {
                List<Newsletters> list = GetListByNewsletterId(NewsletterId);
                if (list.Count > 0)
                {
                    RetVal = (Newsletters)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Newsletters Get()
        {
            return Get(this.NewsletterId);
        }
        //-------------------------------------------------------------- 
        public static Newsletters Static_Get(int NewsletterId)
        {
            return new Newsletters().Get(NewsletterId);
        }
        //--------------------------------------------------------------     
        public List<Newsletters> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short SiteId, short NewsletterGroupId, string SendFrom, string Title, 
                                         byte NewsletterStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Newsletters> RetVal = new List<Newsletters>();
            try
            {
                SqlCommand cmd = new SqlCommand("Newsletters_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupId", NewsletterGroupId));
                cmd.Parameters.Add(new SqlParameter("@SendFrom", StringUtil.InjectionString(SendFrom)));
                cmd.Parameters.Add(new SqlParameter("@Title", StringUtil.InjectionString(Title)));
                cmd.Parameters.Add(new SqlParameter("@NewsletterStatusId", NewsletterStatusId));
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
        public List<Newsletters> Search(int ActUserId, string OrderBy, short SiteId, short NewsletterGroupId, string SendFrom, string Title, byte NewsletterStatusId, string DateFrom, 
                                        string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SiteId, NewsletterGroupId, SendFrom, Title, NewsletterStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}
