using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.LawDocsLib;

namespace ICSoft.CMSLib
{
    public class ArticlesReport
    {
        private int _ArticleId;
        private string _Title;
        private int _DataSourceId;
        private int _CategoryId;
        private int _ReviewStatusId;
        private DateTime _CrDateTime;
        private int _ViewCount;
        private int _LanguageId;
        private int _ApplicationTypeId;
        private int _CrUserId;
        private int _RevUserId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticlesReport()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticlesReport(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticlesReport()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        //-----------------------------------------------------------------    
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //-----------------------------------------------------------------    
        public int DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------    
        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------    
        public int ReviewStatusId
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
        public int ViewCount
        {
            get { return _ViewCount; }
            set { _ViewCount = value; }
        }
        //-----------------------------------------------------------------  
        public int LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------    
        public int ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------  
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------    
        public int RevUserId
        {
            get { return _RevUserId; }
            set { _RevUserId = value; }
        }
        //-----------------------------------------------------------------
        private List<ArticlesReport> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticlesReport> l_ArticlesReport = new List<ArticlesReport>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticlesReport m_ArticlesReport = new ArticlesReport(db.ConnectionString);
                    m_ArticlesReport.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticlesReport.Title = smartReader.GetString("Title");
                    m_ArticlesReport.DataSourceId = smartReader.GetInt32("DataSourceId");
                    m_ArticlesReport.CategoryId = smartReader.GetInt32("CategoryId");
                    m_ArticlesReport.ReviewStatusId = smartReader.GetInt32("ReviewStatusId");
                    m_ArticlesReport.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_ArticlesReport.ViewCount = smartReader.GetInt32("ViewCount");
                    m_ArticlesReport.LanguageId = smartReader.GetInt32("LanguageId");
                    m_ArticlesReport.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ArticlesReport.RevUserId = smartReader.GetInt32("RevUserId");
                    m_ArticlesReport.ApplicationTypeId = smartReader.GetInt32("ApplicationTypeId");
                    l_ArticlesReport.Add(m_ArticlesReport);
                }
                reader.Close();
                return l_ArticlesReport;
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
        public List<ArticlesReport> GetListReport(int ActUserId, int RowAmount, int PageIndex,byte SiteId, string OrderBy,
            int LanguageId, int ApplicationTypeId, int ArticleId, int CategoryId, string DateFrom, string DateTo, byte ReviewStatusId,
            ref int RowCount, ref int SumPending, ref int SumReviewed)
        {
            List<ArticlesReport> RetVal = new List<ArticlesReport>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_ReportDetail_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));

                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SumPending", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SumReviewed", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
                SumPending = Convert.ToInt32(cmd.Parameters["@SumPending"].Value);
                SumReviewed = Convert.ToInt32(cmd.Parameters["@SumReviewed"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}