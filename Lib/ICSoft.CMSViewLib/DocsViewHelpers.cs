using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using sms.database;
using sms.utils;

namespace ICSoft.CMSViewLib
{
    public class DocsViewHelpers
    {
        public static DocsViewDetail View_GetDocDetail(int DocId, byte LanguageId)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            DocsViewDetail retVal = new DocsViewDetail();
            using (SqlConnection con = db.getConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Docs_GetViewDetail");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    //Lay chi tiet van ban
                    retVal.mDocsView = InitViewHelpers.InitDocsOne(smartReader);
                    reader.NextResult();
                    retVal.lDocOthersNewest = InitViewHelpers.InitDocs(smartReader);
                    reader.NextResult();
                    retVal.lDocOthers = InitViewHelpers.InitDocs(smartReader);
                    reader.NextResult();
                    retVal.lDocIndexes = InitViewHelpers.InitDocIndexes(smartReader);
                    reader.NextResult();
                    retVal.lFieldDisplays = InitViewHelpers.InitFieldDisplays(smartReader);
                }
                catch (SqlException err)
                {
                    throw new ApplicationException("Data error: " + err.Message);
                }
            }
            return retVal;
        }

        public static DocsViewDetail View_GetDocDetail_EN(int DocId, byte LanguageId)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            DocsViewDetail retVal = new DocsViewDetail();
            using (SqlConnection con = db.getConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Docs_GetViewDetail_EN");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    //Lay chi tiet van ban
                    retVal.mDocsView = InitViewHelpers.InitDocsOne(smartReader);
                    reader.NextResult();
                    retVal.lDocOthers = InitViewHelpers.InitDocs(smartReader);
                    reader.NextResult();
                    retVal.lDocIndexes = InitViewHelpers.InitDocIndexes(smartReader);
                    reader.NextResult();
                    retVal.lFieldDisplays = InitViewHelpers.InitFieldDisplays(smartReader);
                }
                catch (SqlException err)
                {
                    throw new ApplicationException("Data error: " + err.Message);
                }
            }
            return retVal;
        }

        public static List<DocsView> View_GetByGroupId(byte DocGroupId, int RowAmount, int PageIndex, byte LanguageId, byte GetRowCount, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_View_GetByGroupId") { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                retVal = InitViewHelpers.InitDocs(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        /// <summary>
        /// View nội dung văn bản
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="languageId"></param>
        /// <param name="showDocIndexes"></param>
        /// <returns></returns>
        public static DocsViewDetail View_GetDocContentDetail(int docId, byte languageId, byte showDocIndexes = 0)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            DocsViewDetail retVal = new DocsViewDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("DocsContentDetail_View") { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@DocId", docId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", languageId));
                cmd.Parameters.Add(new SqlParameter("@ShowDocIndexes", showDocIndexes));
 
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                retVal.mDocsView = InitViewHelpers.InitDocsOne(smartReader);

                reader.NextResult();
                retVal.lDocIndexes = InitViewHelpers.InitDocIndexes(smartReader);

                reader.NextResult();
                retVal.lFieldDisplays = InitViewHelpers.InitFieldDisplays(smartReader);
                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        /// <summary>
        /// View thuộc tính vb
        /// </summary>
        /// <param name="DocId"></param>
        /// <param name="LanguageId"></param>
        /// <param name="OrganTypeId"></param>
        /// <returns></returns> 
        public static DocsViewDetail View_GetDocProperties(int DocId, byte LanguageId, byte OrganTypeId, byte ShowDocIndexes = 0)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            DocsViewDetail retVal = new DocsViewDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetViewProperties") { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@ShowDocIndexes", ShowDocIndexes));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                retVal.mDocPropertiesView = InitViewHelpers.InitDocPropertiesOne(smartReader);

                if (ShowDocIndexes > 0)
                {
                    reader.NextResult();
                    retVal.lDocIndexes = InitViewHelpers.InitDocIndexes(smartReader);
                }
                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        /// <summary>
        /// View Hiệu lực vb
        /// </summary>
        /// <param name="DocId"></param>
        /// <param name="LanguageId"></param>
        /// <param name="OrderBy"></param>
        /// <param name="RowAmountDocsRelate"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        public static DocsViewDetail View_GetDocEffect(int DocId, byte LanguageId, string OrderBy, int RowAmountDocsRelate, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            DocsViewDetail retVal = new DocsViewDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetViewEffect") { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@RowAmountDocsRelate", RowAmountDocsRelate));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                retVal.mDocsView = InitViewHelpers.InitDocsOne(smartReader);

                reader.NextResult();
                retVal.lDocRelates = InitViewHelpers.InitDocRelateEffect(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static DocsViewDetail View_GetTopDocRelateByDocId(int DocId, byte LanguageId, int RowAmount, ref int DocGroupId)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            DocsViewDetail retVal = new DocsViewDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("DocRelates_GetTopViewByDocId") { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@DocGroupId", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                retVal.lDocRelates = InitViewHelpers.InitDocRelate(smartReader);

                reader.Close();
                DocGroupId = Convert.ToInt32(cmd.Parameters["@DocGroupId"].Value == DBNull.Value ? "0" : cmd.Parameters["@DocGroupId"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static DataSet View_GetRelateTypesDocsCountByDocGroupId(int DocId, byte LanguageId, int DocGroupId)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            DataSet retVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("RelateTypesDocsCountByDocGroupId_View") { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                retVal = db.getDataSet(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static List<DocsView> View_GetDocsViewNewest(byte LanguageId, int DocGroupId, int RowAmount, int PageIndex, byte UsingDisplayTable, byte GetRowCount, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewNewest") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@UsingDisplayTable", UsingDisplayTable));
                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitDocsViewNewest(smartReader);
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static List<DocsView> View_GetDocsViewNewestEN(byte LanguageId, int DocGroupId, int DisplayTypeId, int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewNewestEN") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitDocsViewNewest(smartReader);
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static List<DocsView> Docs_GetViewMostView(byte LanguageId, int DocGroupId, short FieldId, int RowAmount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewMostView") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitDocsMostView(smartReader);
                    }
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        /// <summary>
        /// Danh sách văn bản theo hiệu lực
        /// </summary>
        public static List<DocsView> Docs_GetViewEffect(byte LanguageId, string EffectStatus, int RowAmount, int PageIndex, byte UsingDisplayTable, byte GetRowCount, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewEffect") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatus", EffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@UsingDisplayTable", UsingDisplayTable));
                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitDocsViewNewest(smartReader);
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static DocsViewSearch MyDocuments_ViewGetPage(int CustomerId, byte DocGroupId, byte RegistTypeId, byte LanguageId, short FieldId, short OrganId, byte EffectStatusId, byte DocTypeId, string DateFrom, string DateTo, string OrderBy, int RowAmount, int PageIndex, byte GetCountByField, byte GetCountByEffectStatus, ref int RowCount)
        {
            int ActUserId = 0, DocRelateId = 0, IssueDate = 0, DocId = 0;
            byte HighlightKeyword = 0, OrganTypeId = 0, HasDocFile = 0, GetCountByDocType = 0, GetCountByOrgan = 0, GetCountByProvince = 0, GetCountByYear = 0, GetRowCount = 1, IsSearchExact = 0;
            short DisplayTypeId = 0, ProvinceId = 0, SignerId =0;
            string SearchKeyword = string.Empty, DocName = string.Empty, DocIdentity = string.Empty, SearchByDate = String.Empty, EffectStatusType = string.Empty;
            DocsViewSearch retVal = new DocsViewSearch();
            try
            {
                retVal = Docs_GetViewSearchWithKeyword(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, DocGroupId, SearchKeyword, IsSearchExact, HighlightKeyword,
                DocName, DocIdentity, DocTypeId, FieldId, OrganTypeId, OrganId, SignerId, DocRelateId,
                DisplayTypeId, CustomerId, RegistTypeId, EffectStatusId, EffectStatusType, ProvinceId, HasDocFile, IssueDate, SearchByDate,
                DateFrom, DateTo, GetRowCount, GetCountByField, GetCountByDocType, GetCountByOrgan, GetCountByProvince, GetCountByEffectStatus, GetCountByYear, ref RowCount);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return retVal;
        }
        public static DocsViewSearch MyDocumentsEN_ViewGetPage(int CustomerId, byte DocGroupId, byte RegistTypeId, byte LanguageId, short FieldId, short OrganId, byte EffectStatusId, byte DocTypeId, string DateFrom, string DateTo, string OrderBy, int RowAmount, int PageIndex, byte GetCountByField, byte GetCountByEffectStatus, ref int RowCount)
        {
            int ActUserId = 0, DocRelateId = 0, IssueDate = 0, DocId = 0;
            byte HighlightKeyword = 0, OrganTypeId = 0, HasDocFile = 0, GetCountByDocType = 0, GetCountByOrgan = 0, GetCountByProvince = 0, GetCountByYear = 0, GetRowCount = 1, IsSearchExact = 0;
            short DisplayTypeId = 0, ProvinceId = 0, SignerId = 0;
            string SearchKeyword = string.Empty, DocName = string.Empty, DocIdentity = string.Empty, SearchByDate = String.Empty, EffectStatusType = string.Empty;
            DocsViewSearch retVal = new DocsViewSearch();
            try
            {
                retVal = Docs_GetViewSearchWithKeywordEN(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, DocGroupId, SearchKeyword, IsSearchExact, HighlightKeyword,
                DocName, DocIdentity, DocTypeId, FieldId, OrganTypeId, OrganId, SignerId, DocRelateId,
                DisplayTypeId, CustomerId, RegistTypeId, EffectStatusId, EffectStatusType, ProvinceId, HasDocFile, IssueDate, SearchByDate,
                DateFrom, DateTo, GetRowCount, GetCountByField, GetCountByDocType, GetCountByOrgan, GetCountByProvince, GetCountByEffectStatus, GetCountByYear, ref RowCount);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return retVal;
        }

        public static DocsViewDetail Docs_GetViewRelates(byte LanguageId, int DocId, short RelateTypeId, string DisplayPosition, int RowAmount, int PageIndex, byte GetCountByRelateType, ref int RowCount, byte GetRelateTypeCancuTu = 0)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            DocsViewDetail retVal = new DocsViewDetail();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewRelates") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                    cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayPosition", DisplayPosition));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByRelateType", GetCountByRelateType));
                    cmd.Parameters.Add(new SqlParameter("@GetRelateTypeCancuTu", GetRelateTypeCancuTu));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal.lDocRelates = InitViewHelpers.InitDocsViewRelates(smartReader);
                        reader.NextResult();
                        retVal.lRelateTypes = InitViewHelpers.InitRelateTypes(smartReader);
                        reader.Close();
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static DocsViewDetail Docs_GetViewRelatesEffect(byte LanguageId, int DocId)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            DocsViewDetail retVal = new DocsViewDetail();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewRelatesEffect") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    retVal.lDocRelates = InitViewHelpers.InitDocsViewRelates(smartReader);
                    reader.NextResult();
                    retVal.lRelateTypes = InitViewHelpers.InitRelateTypes(smartReader);
                    reader.Close();
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static List<DocsView> Docs_GetViewFullSearch(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId,string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewSearch") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
                    cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                    cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                    cmd.Parameters.Add(new SqlParameter("@DocName", DocName));
                    cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                    cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                    cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                    cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                    cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                    cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                    cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusType", EffectStatusType));
                    cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                    cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));
                    cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));

                    if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                    if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));

                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByDocType", GetCountByDocType));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByOrgan", GetCountByOrgan));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", GetCountByProvince));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", GetCountByEffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByYear", GetCountByYear));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitDocsSearchView(smartReader);
                        //reader.NextResult();
                        //RetVal.lEstatePosts = EstatePosts.InitSomeField(smartReader);
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static List<DocsView> Docs_GetViewSearch(int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, short OrganId, short SignerId, int CustomerId, byte EffectStatusId, string EffectStatusType, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, ref int RowCount)
        {
            int ActUserId = 0, DocRelateId = 0, IssueDate = 0;
            byte HighlightKeyword = 0, OrganTypeId = 0, HasDocFile = 0,GetCountByField  =0, GetCountByDocType =0, GetCountByOrgan =0 , GetCountByProvince =0, GetCountByEffectStatus =0, GetCountByYear =0;
            short DisplayTypeId = 0, ProvinceId = 0;
            
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                retVal = Docs_GetViewFullSearch(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, DocGroupId,
                    SearchKeyword, IsSearchExact, HighlightKeyword, DocName, DocIdentity, DocTypeId, FieldId,
                    OrganTypeId, OrganId, SignerId, DocRelateId, DisplayTypeId, CustomerId, EffectStatusId,
                    EffectStatusType, ProvinceId, HasDocFile, IssueDate, SearchByDate, DateFrom, DateTo, GetRowCount,
                    GetCountByField, GetCountByDocType, GetCountByOrgan, GetCountByProvince, GetCountByEffectStatus,
                    GetCountByYear, ref RowAmount);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return retVal;
        }

        public static List<DocsView> Docs_GetViewSearch(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, short RegistTypeId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewSearch") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
                    cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                    cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                    cmd.Parameters.Add(new SqlParameter("@DocName", DocName));
                    cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                    cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                    cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                    cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                    cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                    cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                    cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusType", EffectStatusType));
                    cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                    cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));
                    cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));

                    if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                    if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByDocType", GetCountByDocType));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByOrgan", GetCountByOrgan));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", GetCountByProvince));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", GetCountByEffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByYear", GetCountByYear));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitDocsSearchView(smartReader);
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }
        public static List<DocsView> Docs_GetViewSearchEN(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, short RegistTypeId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount)
        {
            byte FileTypeId=0;
            return Docs_GetViewSearchEN(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, DocGroupId, SearchKeyword, IsSearchExact, HighlightKeyword,
            DocName, DocIdentity, DocTypeId, FieldId, OrganTypeId, OrganId, SignerId, DocRelateId,
            DisplayTypeId, CustomerId, RegistTypeId, EffectStatusId, EffectStatusType, ProvinceId, HasDocFile, FileTypeId, IssueDate, SearchByDate,
            DateFrom, DateTo, GetRowCount, GetCountByField, GetCountByDocType, GetCountByOrgan, GetCountByProvince, GetCountByEffectStatus, GetCountByYear, ref RowCount);
        }
        public static List<DocsView> Docs_GetViewSearchEN(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, short RegistTypeId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, byte FileTypeId, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewSearchEN") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
                    cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                    cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                    cmd.Parameters.Add(new SqlParameter("@DocName", DocName));
                    cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                    cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                    cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                    cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                    cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                    cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                    cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusType", EffectStatusType));
                    cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                    cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                    cmd.Parameters.Add(new SqlParameter("@FileTypeId", FileTypeId));
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));
                    cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));

                    if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                    if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByDocType", GetCountByDocType));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByOrgan", GetCountByOrgan));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", GetCountByProvince));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", GetCountByEffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByYear", GetCountByYear));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitDocsSearchView(smartReader);
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static List<DocsView> Docs_GetViewSearchSuggest(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, short RegistTypeId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewSearchSuggest") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
                    cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                    cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                    cmd.Parameters.Add(new SqlParameter("@DocName", DocName));
                    cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                    cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                    cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                    cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                    cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                    cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                    cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusType", EffectStatusType));
                    cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                    cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));
                    cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));

                    if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                    if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByDocType", GetCountByDocType));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByOrgan", GetCountByOrgan));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", GetCountByProvince));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", GetCountByEffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByYear", GetCountByYear));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitDocsSearchView(smartReader);
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }
        public static DocsViewSearch Docs_GetViewSearchWithCount(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, short RegistTypeId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount)
        {
            DocsViewSearch mDocsViewSearch = new DocsViewSearch();
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewSearch") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
                    cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                    cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                    cmd.Parameters.Add(new SqlParameter("@DocName", DocName));
                    cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                    cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                    cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                    cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                    cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                    cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                    cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusType", EffectStatusType));
                    cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                    cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));
                    cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));

                    if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                    if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByDocType", GetCountByDocType));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByOrgan", GetCountByOrgan));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", GetCountByProvince));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", GetCountByEffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByYear", GetCountByYear));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        do
                        {
                            var fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
                            SmartDataReader smartReader = new SmartDataReader(reader);
                            if (fieldNames.Contains("DocId"))
                            {
                                mDocsViewSearch.lDocsView = InitViewHelpers.InitDocsSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("FieldId"))
                            {
                                mDocsViewSearch.lFields = InitViewHelpers.InitFieldsSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("DocTypeDesc"))
                            {
                                mDocsViewSearch.lDocTypes = InitViewHelpers.InitDocTypesSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("OrganDesc"))
                            {
                                mDocsViewSearch.lOrgans = InitViewHelpers.InitOrgansSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("ProvinceId"))
                            {
                                mDocsViewSearch.lProvinces = InitViewHelpers.InitProvincesSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("EffectStatusDesc"))
                            {
                                mDocsViewSearch.lEffectStatus = InitViewHelpers.InitEffectStatusSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("Year"))
                            {
                                mDocsViewSearch.YearView = InitViewHelpers.InitYearViewSearchView(smartReader);
                            }
                        }
                        while (reader.NextResult());
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return mDocsViewSearch;
        }


        public static DocsViewSearch Docs_GetViewSearchWithKeyword(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, short RegistTypeId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount,byte GetCountByGroup = 0)
        {
            DocsViewSearch mDocsViewSearch = new DocsViewSearch();
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewSearchWithKeyword") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
                    cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                    cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                    cmd.Parameters.Add(new SqlParameter("@DocName", DocName));
                    cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                    cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                    cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                    cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                    cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                    cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                    cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusType", EffectStatusType));
                    cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                    cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));
                    cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));

                    if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                    if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));

                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByDocType", GetCountByDocType));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByOrgan", GetCountByOrgan));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", GetCountByProvince));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", GetCountByEffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByYear", GetCountByYear));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByGroup", GetCountByGroup));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        do
                        {
                            var fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
                            SmartDataReader smartReader = new SmartDataReader(reader);
                            if (fieldNames.Contains("DocId"))
                            {
                                mDocsViewSearch.lDocsView = InitViewHelpers.InitDocsSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("FieldId"))
                            {
                                mDocsViewSearch.lFields = InitViewHelpers.InitFieldsSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("DocTypeDesc"))
                            {
                                mDocsViewSearch.lDocTypes = InitViewHelpers.InitDocTypesSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("OrganDesc"))
                            {
                                mDocsViewSearch.lOrgans = InitViewHelpers.InitOrgansSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("ProvinceId"))
                            {
                                mDocsViewSearch.lProvinces = InitViewHelpers.InitProvincesSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("EffectStatusDesc"))
                            {
                                mDocsViewSearch.lEffectStatus = InitViewHelpers.InitEffectStatusSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("Year"))
                            {
                                mDocsViewSearch.YearView = InitViewHelpers.InitYearViewSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("DocGroupId"))
                            {
                                mDocsViewSearch.lDocGroupsView = InitViewHelpers.InitDocGroupsViews(smartReader);
                            }
                        }
                        while (reader.NextResult());
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return mDocsViewSearch;
        }
        public static DocsViewSearch Docs_GetViewSearchWithKeywordEN(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, short RegistTypeId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount)
        {
            byte FileTypeId = 0;
            return Docs_GetViewSearchWithKeywordEN(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, DocGroupId, SearchKeyword, IsSearchExact, HighlightKeyword,
            DocName, DocIdentity, DocTypeId, FieldId, OrganTypeId, OrganId, SignerId, DocRelateId,
            DisplayTypeId, CustomerId, RegistTypeId, EffectStatusId, EffectStatusType, ProvinceId, HasDocFile, FileTypeId, IssueDate, SearchByDate,
            DateFrom, DateTo, GetRowCount, GetCountByField, GetCountByDocType, GetCountByOrgan, GetCountByProvince, GetCountByEffectStatus, GetCountByYear, ref RowCount);
        }
        public static DocsViewSearch Docs_GetViewSearchWithKeywordEN(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, short RegistTypeId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, byte FileTypeId, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount)
        {
            DocsViewSearch mDocsViewSearch = new DocsViewSearch();
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewSearchWithKeywordEN") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
                    cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                    cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                    cmd.Parameters.Add(new SqlParameter("@DocName", DocName));
                    cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                    cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                    cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                    cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                    cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                    cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                    cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusType", EffectStatusType));
                    cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                    cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                    cmd.Parameters.Add(new SqlParameter("@FileTypeId", FileTypeId));
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));
                    cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));

                    if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                    if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));

                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByDocType", GetCountByDocType));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByOrgan", GetCountByOrgan));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", GetCountByProvince));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", GetCountByEffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByYear", GetCountByYear));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        do
                        {
                            var fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
                            SmartDataReader smartReader = new SmartDataReader(reader);
                            if (fieldNames.Contains("DocId"))
                            {
                                mDocsViewSearch.lDocsView = InitViewHelpers.InitDocsSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("FieldId"))
                            {
                                mDocsViewSearch.lFields = InitViewHelpers.InitFieldsSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("DocTypeDesc"))
                            {
                                mDocsViewSearch.lDocTypes = InitViewHelpers.InitDocTypesSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("OrganDesc"))
                            {
                                mDocsViewSearch.lOrgans = InitViewHelpers.InitOrgansSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("ProvinceId"))
                            {
                                mDocsViewSearch.lProvinces = InitViewHelpers.InitProvincesSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("EffectStatusDesc"))
                            {
                                mDocsViewSearch.lEffectStatus = InitViewHelpers.InitEffectStatusSearchView(smartReader);
                            }
                            else if (fieldNames.Contains("Year"))
                            {
                                mDocsViewSearch.YearView = InitViewHelpers.InitYearViewSearchView(smartReader);
                            }
                        }
                        while (reader.NextResult());
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return mDocsViewSearch;
        }

        public static List<DocsView> CustomerDocs_GetViewNewest(byte LanguageId, int CustomerId, byte RegistTypeId, int RowAmount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CustomerDocs_GetViewNewest") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitCustomerDocsNewest(smartReader);
                    }
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        public static List<DocsView> CustomerDocs_GetViewEffect(byte LanguageId, int CustomerId, string EffectStatus, int RowAmount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CustomerDocs_GetViewEffect") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatus", EffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitCustomerDocsNewest(smartReader);
                    }
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }


        public static List<DocsView>CustomerDocs_CustomerFields(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte DocGroupId, string SearchKeyword, byte IsSearchExact, byte HighlightKeyword,
            string DocName, string DocIdentity, byte DocTypeId, short FieldId, byte OrganTypeId, short OrganId, short SignerId, int DocRelateId,
            short DisplayTypeId, int CustomerId, short RegistTypeId, byte EffectStatusId, string EffectStatusType, short ProvinceId, byte HasDocFile, int IssueDate, string SearchByDate,
            string DateFrom, string DateTo, byte GetRowCount, byte GetCountByField, byte GetCountByDocType, byte GetCountByOrgan, byte GetCountByProvince, byte GetCountByEffectStatus, byte GetCountByYear, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CustomerFields_SearchDoc") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                    cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                    cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                    cmd.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
                    cmd.Parameters.Add(new SqlParameter("@IsSearchExact", IsSearchExact));
                    cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", HighlightKeyword));
                    cmd.Parameters.Add(new SqlParameter("@DocName", DocName));
                    cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                    cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                    cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                    cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                    cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                    cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                    cmd.Parameters.Add(new SqlParameter("@DocRelateId", DocRelateId));
                    cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusId", EffectStatusId));
                    cmd.Parameters.Add(new SqlParameter("@EffectStatusType", EffectStatusType));
                    cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                    cmd.Parameters.Add(new SqlParameter("@HasDocFile", HasDocFile));
                    cmd.Parameters.Add(new SqlParameter("@IssueDate", IssueDate));
                    cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));

                    if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                    if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByDocType", GetCountByDocType));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByOrgan", GetCountByOrgan));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", GetCountByProvince));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", GetCountByEffectStatus));
                    cmd.Parameters.Add(new SqlParameter("@GetCountByYear", GetCountByYear));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitDocsSearchView(smartReader);
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

        //-----------------------------------------------------------
        public static List<DocsView> GetViewByKeyWordId_Paging(int KeywordId, int RowAmount, byte GetListCateOfShowArticle, byte GetRowCount, int PageIndex, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<DocsView> retVal = new List<DocsView>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("Docs_GetViewByKeywordId_Paging") { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@KeywordId", KeywordId));
                    cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                    cmd.Parameters.Add(new SqlParameter("@GetListCateOfShowArticle", GetListCateOfShowArticle));
                    cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                    cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                    cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SmartDataReader smartReader = new SmartDataReader(reader);
                        retVal = InitViewHelpers.InitCustomerDocsNewest(smartReader);
                    }
                    RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
                }

            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return retVal;
        }

    }
}

