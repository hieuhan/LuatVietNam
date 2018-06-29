
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
    public class NewsletterEmailGroups
    {
        private int _NewsletterEmailGroupId;
        private int _NewsletterEmailId;
        private short _NewsletterGroupId;
        private DateTime _ExpireTime;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public NewsletterEmailGroups()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public NewsletterEmailGroups(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~NewsletterEmailGroups()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int NewsletterEmailGroupId
        {
            get { return _NewsletterEmailGroupId; }
            set { _NewsletterEmailGroupId = value; }
        }
        //-----------------------------------------------------------------
        public int NewsletterEmailId
        {
            get { return _NewsletterEmailId; }
            set { _NewsletterEmailId = value; }
        }
        //-----------------------------------------------------------------
        public short NewsletterGroupId
        {
            get { return _NewsletterGroupId; }
            set { _NewsletterGroupId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ExpireTime
        {
            get { return _ExpireTime; }
            set { _ExpireTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<NewsletterEmailGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<NewsletterEmailGroups> l_NewsletterEmailGroups = new List<NewsletterEmailGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    NewsletterEmailGroups m_NewsletterEmailGroups = new NewsletterEmailGroups(db.ConnectionString);
                    m_NewsletterEmailGroups.NewsletterEmailGroupId = smartReader.GetInt32("NewsletterEmailGroupId");
                    m_NewsletterEmailGroups.NewsletterEmailId = smartReader.GetInt32("NewsletterEmailId");
                    m_NewsletterEmailGroups.NewsletterGroupId = smartReader.GetInt16("NewsletterGroupId");
                    m_NewsletterEmailGroups.ExpireTime = smartReader.GetDateTime("ExpireTime");
                    m_NewsletterEmailGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_NewsletterEmailGroups.Add(m_NewsletterEmailGroups);
                }
                reader.Close();
                return l_NewsletterEmailGroups;
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
                SqlCommand cmd = new SqlCommand("NewsletterEmailGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterEmailId", this.NewsletterEmailId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupId", this.NewsletterGroupId));
                if (this.ExpireTime == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@ExpireTime", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ExpireTime", this.ExpireTime));
                cmd.Parameters.Add(new SqlParameter("@NewsletterEmailGroupId", this.NewsletterEmailGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.NewsletterEmailGroupId = Convert.ToInt32((cmd.Parameters["@NewsletterEmailGroupId"].Value == null) ? 0 : (cmd.Parameters["@NewsletterEmailGroupId"].Value));
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
                SqlCommand cmd = new SqlCommand("NewsletterEmailGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterEmailGroupId", this.NewsletterEmailGroupId));
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
        public byte DeleteByNewsletterEmailId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterEmailGroups_DeleteByNewsletterEmailId");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterEmailId", this.NewsletterEmailId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<NewsletterEmailGroups> GetList(int NewsletterEmailId)
        {
            List<NewsletterEmailGroups> RetVal = new List<NewsletterEmailGroups>();
            try
            {
                string sql = "SELECT * FROM NewsletterEmailGroups WHERE NewsletterEmailId=" + NewsletterEmailId.ToString();
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
        public List<NewsletterEmailGroups> GetList_ByCustomerId(int CustomerId)
        {
            List<NewsletterEmailGroups> RetVal = new List<NewsletterEmailGroups>();
            try
            {
                string sql = "select * from NewsletterEmailGroups where NewsletterEmailId in (select NewsletterEmailId from NewsletterEmails where CustomerId =" + CustomerId.ToString() + " )";
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
        public List<NewsletterEmailGroups> GetListByNewsletterEmailGroupId(int NewsletterEmailGroupId)
        {
            List<NewsletterEmailGroups> RetVal = new List<NewsletterEmailGroups>();
            try
            {
                if (NewsletterEmailGroupId > 0)
                {
                    string sql = "SELECT * FROM NewsletterEmailGroups WHERE (NewsletterEmailGroupId=" + NewsletterEmailGroupId.ToString() + ")";
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
        public NewsletterEmailGroups Get(int NewsletterEmailGroupId)
        {
            NewsletterEmailGroups RetVal = new NewsletterEmailGroups(db.ConnectionString);
            try
            {
                List<NewsletterEmailGroups> list = GetListByNewsletterEmailGroupId(NewsletterEmailGroupId);
                if (list.Count > 0)
                {
                    RetVal = (NewsletterEmailGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public NewsletterEmailGroups Get()
        {
            return Get(this.NewsletterEmailGroupId);
        }
        //-------------------------------------------------------------- 
        public static NewsletterEmailGroups Static_Get(int NewsletterEmailGroupId)
        {
            return new NewsletterEmailGroups().Get(NewsletterEmailGroupId);
        }
        //--------------------------------------------------------------     
        public List<NewsletterEmailGroups> GetByAllGroup(int ActUserId, int NewsletterEmailId)
        {
            List<NewsletterEmailGroups> RetVal = new List<NewsletterEmailGroups>();
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterEmailGroups_GetByAllGroup");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterEmailId", NewsletterEmailId));
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