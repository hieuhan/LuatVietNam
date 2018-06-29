using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sms.database;
using System.Data.SqlClient;

namespace ICSoft.CMSViewLib
{
    public class ArticleViewCountView
    {
        private int _ArticleViewCountId;
        private int _ArticleId;
	    private byte _LanguageId;
	    private byte _ApplicationTypeId;
	    private int _ViewCount;
	    private DateTime _CrDateTime;
	    private DateTime _LastViewTime;
        //-----------------------------------------------------------------
        public ArticleViewCountView()
        {
		}
        //-----------------------------------------------------------------
        ~ArticleViewCountView()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public int ArticleViewCountId
        {
		    get { return _ArticleViewCountId; }
		    set { _ArticleViewCountId = value; }
	    }
        //-----------------------------------------------------------------
        public int ArticleId
		{
            get { return _ArticleId; }
		    set { _ArticleId = value; }
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
        public int ViewCount
		{
            get { return _ViewCount; }
		    set { _ViewCount = value; }
		}    
        //-----------------------------------------------------------------
        public DateTime CrDateTime
		{
            get { return _CrDateTime; }
		    set { _CrDateTime = value; }
		}    
        //-----------------------------------------------------------------
        public DateTime LastViewTime
		{
            get { return _LastViewTime; }
		    set { _LastViewTime = value; }
		}    
        //-----------------------------------------------------------------
        public static List<ArticleViewCountView> Init(SmartDataReader smartReader)
        {
            List<ArticleViewCountView> l_ArticleViewCount = new List<ArticleViewCountView>();
            try
            {
                while (smartReader.Read())
                {
                    ArticleViewCountView m_ArticleViewCount = new ArticleViewCountView();
                    m_ArticleViewCount.ArticleViewCountId = smartReader.GetInt32("ArticleViewCountId");
                    m_ArticleViewCount.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleViewCount.LanguageId = smartReader.GetByte("LanguageId");
                    m_ArticleViewCount.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_ArticleViewCount.ViewCount = smartReader.GetInt32("ViewCount");
                    m_ArticleViewCount.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_ArticleViewCount.LastViewTime = smartReader.GetDateTime("LastViewTime");
         
                    l_ArticleViewCount.Add(m_ArticleViewCount);
                }
                return l_ArticleViewCount;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------------
        public static ArticleViewCountView Static_Get(int ArticleId, List<ArticleViewCountView> list)
        {
            ArticleViewCountView RetVal = list.Find(i => i.ArticleId == ArticleId);
            return RetVal == null ? new ArticleViewCountView() : RetVal;
        }
    }
}
