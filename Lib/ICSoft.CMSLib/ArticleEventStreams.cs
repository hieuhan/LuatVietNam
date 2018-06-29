
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSLib
{
    public class ArticleEventStreams
    {
        private int _ArticleEventStreamId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _ArticleId;
        private int _EventStreamId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticleEventStreams()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticleEventStreams(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticleEventStreams()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleEventStreamId
        {
            get { return _ArticleEventStreamId; }
            set { _ArticleEventStreamId = value; }
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

        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        public int EventStreamId
        {
            get { return _EventStreamId; }
            set { _EventStreamId = value; }
        }
        private List<ArticleEventStreams> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleEventStreams> l_ArticleEventStreams = new List<ArticleEventStreams>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleEventStreams m_ArticleEventStreams = new ArticleEventStreams(db.ConnectionString);
                    m_ArticleEventStreams.ArticleEventStreamId = smartReader.GetInt32("ArticleEventStreamId");
                    m_ArticleEventStreams.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleEventStreams.EventStreamId = smartReader.GetInt32("EventStreamId");
                    m_ArticleEventStreams.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ArticleEventStreams.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_ArticleEventStreams.Add(m_ArticleEventStreams);
                }
                reader.Close();
                return l_ArticleEventStreams;
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
                SqlCommand cmd = new SqlCommand("ArticleEventStreams_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", this.EventStreamId));
                cmd.Parameters.Add("@ArticleEventStreamId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleEventStreamId = Convert.ToInt32((cmd.Parameters["@ArticleEventStreamId"].Value == null) ? 0 : (cmd.Parameters["@ArticleEventStreamId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleEventStreams_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", this.EventStreamId));
                cmd.Parameters.Add(new SqlParameter("@ArticleEventStreamId", this.ArticleEventStreamId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleEventStreams_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleEventStreamId", this.ArticleEventStreamId));
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
        public byte DeleteByArticleId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleEventStreams_DeleteByArticleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
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
        public List<ArticleEventStreams> GetListByArticleId(int ArticleId)
        {
            List<ArticleEventStreams> RetVal = new List<ArticleEventStreams>();
            try
            {
                string sql = "SELECT * FROM ArticleEventStreams WHERE ArticleId=" + ArticleId.ToString();
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }      
    }
}