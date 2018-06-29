
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSViewLib
{
    public class ArticleCommentsView
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
        //-----------------------------------------------------------------
        public ArticleCommentsView()
        {
        }
        //-----------------------------------------------------------------        
        ~ArticleCommentsView()
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
        //-------------------------------------------------------------- 
        public static List<ArticleCommentsView> Static_GetListByParentId(int ParentCommentId, List<ArticleCommentsView> list)
        {
            List<ArticleCommentsView> RetVal = new List<ArticleCommentsView>();
            try
            {
                RetVal = list.FindAll(i => i.ParentCommentId == ParentCommentId);
                if (RetVal == null) RetVal = new List<ArticleCommentsView>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}