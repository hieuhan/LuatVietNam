
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
    public class ArticleComments
    {
        private int _ArticleCommentId;
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private int _ParentCommentId;
        private byte _CommentLevel;
        private string _FullName;
        private string _Email;
        private string _PhoneNumber;
        private string _Comment;
        private byte _RatingScore;
        private string _FromIP;
        private string _UserAgent;
        private byte _ReviewStatusId;
        private DateTime _CrDateTime;
        private int _ArticleId;
        private short _SiteId;
        private byte _DataTypeId;
        private string _Title;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticleComments()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticleComments(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticleComments()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleCommentId
        {
            get { return _ArticleCommentId; }
            set { _ArticleCommentId = value; }
        }
        //-----------------------------------------------------------------
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------
        public byte ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------
        public int ParentCommentId
        {
            get { return _ParentCommentId; }
            set { _ParentCommentId = value; }
        }
        //-----------------------------------------------------------------
        public byte CommentLevel
        {
            get { return _CommentLevel; }
            set { _CommentLevel = value; }
        }
        //-----------------------------------------------------------------
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        //-----------------------------------------------------------------
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        //-----------------------------------------------------------------
        public byte RatingScore
        {
            get { return _RatingScore; }
            set { _RatingScore = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
        }
        //-----------------------------------------------------------------
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //-----------------------------------------------------------------
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public byte DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }

        private List<ArticleComments> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleComments> l_ArticleComments = new List<ArticleComments>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleComments m_ArticleComments = new ArticleComments(db.ConnectionString);
                    m_ArticleComments.ArticleCommentId = smartReader.GetInt32("ArticleCommentId");
                    m_ArticleComments.LanguageId = smartReader.GetByte("LanguageId");
                    m_ArticleComments.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_ArticleComments.SiteId = smartReader.GetInt16("SiteId");
                    m_ArticleComments.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_ArticleComments.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleComments.ParentCommentId = smartReader.GetInt32("ParentCommentId");
                    m_ArticleComments.CommentLevel = smartReader.GetByte("CommentLevel");
                    m_ArticleComments.FullName = smartReader.GetString("FullName");
                    m_ArticleComments.Email = smartReader.GetString("Email");
                    m_ArticleComments.PhoneNumber = smartReader.GetString("PhoneNumber");
                    m_ArticleComments.Comment = smartReader.GetString("Comment");
                    m_ArticleComments.RatingScore = smartReader.GetByte("RatingScore");
                    m_ArticleComments.FromIP = smartReader.GetString("FromIP");
                    m_ArticleComments.UserAgent = smartReader.GetString("UserAgent");
                    m_ArticleComments.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_ArticleComments.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_ArticleComments.Title = smartReader.GetString("Title");

                    l_ArticleComments.Add(m_ArticleComments);
                }
                reader.Close();
                return l_ArticleComments;
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
                SqlCommand cmd = new SqlCommand("ArticleComments_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ParentCommentId", this.ParentCommentId));
                cmd.Parameters.Add(new SqlParameter("@CommentLevel", this.CommentLevel));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@Comment", this.Comment));
                cmd.Parameters.Add(new SqlParameter("@RatingScore", this.RatingScore));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", this.UserAgent));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ArticleCommentId", this.ArticleCommentId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleCommentId = Convert.ToInt32((cmd.Parameters["@ArticleCommentId"].Value == null) ? 0 : (cmd.Parameters["@ArticleCommentId"].Value));
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
        public byte UpdateReviewStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleComments_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleCommentId", this.ArticleCommentId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleComments_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleCommentId", this.ArticleCommentId));
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
        public static List<ArticleComments> Static_GetListByParentId(int ParentCommentId, List<ArticleComments> list)
        {
            List<ArticleComments> RetVal = new List<ArticleComments>();
            try
            {
                RetVal = list.FindAll(i => i.ParentCommentId == ParentCommentId);
                if (RetVal == null) RetVal = new List<ArticleComments>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static int Static_CountByRatingScore(byte RatingScore, List<ArticleComments> list)
        {
            List<ArticleComments> RetVal = new List<ArticleComments>();
            try
            {
                RetVal = list.FindAll(i => i.RatingScore == RatingScore);
                if (RetVal == null) RetVal = new List<ArticleComments>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal.Count;
        }
        //-------------------------------------------------------------- 
        public List<ArticleComments> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int EventStreamId,  string DateFrom, string DateTo, ref int RowCount)
        {
            List<ArticleComments> RetVal = new List<ArticleComments>();
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleComments_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@EventStreamId", EventStreamId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", this.UserAgent));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
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
        public List<ArticleComments> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, int ArticleId, string FromIP, string UserAgent, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int EventStreamId = 0;
            this.LanguageId = LanguageId;
            this.ApplicationTypeId = ApplicationTypeId;
            this.ArticleId = ArticleId;
            this.FromIP = FromIP;
            this.UserAgent = UserAgent;
            this.ReviewStatusId = ReviewStatusId;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, EventStreamId, DateFrom, DateTo, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<ArticleComments> Search(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, int ArticleId, string FromIP, string UserAgent, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, ArticleId, FromIP, UserAgent, ReviewStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}