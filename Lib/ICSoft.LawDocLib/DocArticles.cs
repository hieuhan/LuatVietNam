
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
    public class DocArticles
    {
        private int _DocArticleId;
        private int _ArticleId;
        private int _DocId;
        private int _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocArticles()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocArticles(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~DocArticles()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocArticleId
        {
            get { return _DocArticleId; }
            set { _DocArticleId = value; }
        }
        //-----------------------------------------------------------------
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        //-----------------------------------------------------------------
        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<DocArticles> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocArticles> l_DocArticles = new List<DocArticles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocArticles m_DocArticles = new DocArticles(db.ConnectionString);
                    m_DocArticles.DocArticleId = smartReader.GetInt32("DocArticleId");
                    m_DocArticles.ArticleId = smartReader.GetInt32("ArticleId");
                    m_DocArticles.DocId = smartReader.GetInt32("DocId");
                    m_DocArticles.DisplayOrder = smartReader.GetInt32("DisplayOrder");

                    l_DocArticles.Add(m_DocArticles);
                }
                reader.Close();
                return l_DocArticles;
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
        private List<Docs> InitDocs(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Docs> l_Docs = new List<Docs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Docs m_Docs = new Docs(db.ConnectionString);
                    m_Docs.LanguageId = smartReader.GetByte("LanguageId");
                    m_Docs.DocId = smartReader.GetInt32("DocId");
                    //m_Docs.DocGUId = smartReader.GetString("DocGUId");
                    m_Docs.DocName = smartReader.GetString("DocName");
                    m_Docs.DocIdentity = smartReader.GetString("DocIdentity");
                    m_Docs.DocIdentityClear = smartReader.GetString("DocIdentityClear");
                    m_Docs.DocSummary = smartReader.GetString("DocSummary");
                    //m_Docs.DocContent = smartReader.GetString("DocContent");
                    m_Docs.DocTypeId = smartReader.GetByte("DocTypeId");
                    m_Docs.IssueDate = smartReader.GetDateTime("IssueDate");
                    m_Docs.EffectDate = smartReader.GetDateTime("EffectDate");
                    m_Docs.ExpireDate = smartReader.GetDateTime("ExpireDate");
                    m_Docs.GazetteNumber = smartReader.GetString("GazetteNumber");
                    m_Docs.GazetteDate = smartReader.GetDateTime("GazetteDate");
                    m_Docs.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_Docs.UseStatusId = smartReader.GetByte("UseStatusId");
                    m_Docs.EffectStatusId = smartReader.GetByte("EffectStatusId");
                    m_Docs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Docs.DocGroupId = smartReader.GetByte("DocGroupId");
                    m_Docs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Docs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Docs.UpdUserId = smartReader.GetInt32("UpdUserId");
                    m_Docs.UpdDateTime = smartReader.GetDateTime("UpdDateTime");
                    m_Docs.RevUserId = smartReader.GetInt32("RevUserId");
                    m_Docs.RevDateTime = smartReader.GetDateTime("RevDateTime");
                    l_Docs.Add(m_Docs);
                }
                reader.Close();
                return l_Docs;
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
        public short Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            short RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocArticles_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@DocArticleId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocArticleId = (cmd.Parameters["@DocArticleId"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["@DocArticleId"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToInt16((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public short Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            short RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocArticles_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DocArticleId", this.DocArticleId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToInt16((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public short InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            short RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocArticles_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DocArticleId", this.DocArticleId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocArticleId = (cmd.Parameters["@DocArticleId"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["@DocArticleId"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToInt16((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public short Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            short RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocArticles_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocArticleId", this.DocArticleId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToInt16((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<Docs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
                                  int ArticleId, string DocGUId, string DocName, string DocIdentity, byte DocTypeId, short DataSourceId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, byte DocGroupId,
                                  int DocRelateId, short DisplayTypeId, byte UseStatusId, byte EffectStatusId, byte ReviewStatusId, byte HasDocFile, int CrUserId, int UpdUserId, int RevUserId, int IssueDate, string SearchByDate,
                                  string DateFrom, string DateTo, ref int RowCount)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocArticles_SearchDoc");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", StringUtil.InjectionString(SearchKeyword)));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                //if (DocGUId != "") cmd.Parameters.Add("@DocGUId", SqlDbType.UniqueIdentifier).Value = new Guid(DocGUId);
                if (DocGUId != "") cmd.Parameters.Add(new SqlParameter("@DocGUId", DocGUId));
                cmd.Parameters.Add(new SqlParameter("@DocName", StringUtil.InjectionString(DocName)));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", StringUtil.InjectionString(DocIdentity)));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", UseStatusId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@UpdUserId", UpdUserId));
                cmd.Parameters.Add(new SqlParameter("@RevUserId", RevUserId));
                cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));

                cmd.Parameters.Add(new SqlParameter("@SearchByDate", StringUtil.InjectionString(SearchByDate)));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                //add 
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = InitDocs(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Docs> GetListByArticleId(int ArticleId, byte LanguageId)
        {
            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocArticles_GetByArticleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                RetVal = InitDocs(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataSet GetDataGenByArticleId(int ArticleId, byte LanguageId)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DocArticles_GetDataGenByArticleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataSet GetDataGenHotByArticleId(int ArticleId, byte LanguageId)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DocArticles_GetDataGenHotByArticleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataSet GetDataGenNewestByArticleId(int ArticleId, byte LanguageId)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("DocArticles_GetDataGenNewestByArticleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

