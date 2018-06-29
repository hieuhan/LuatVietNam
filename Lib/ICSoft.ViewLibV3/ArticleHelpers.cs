using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class ArticleHelpers
    {
        public static List<Articles> Init(SmartDataReader smartReader, string FieldSelect = "*")
        {
            List<Articles> l_Articles = new List<Articles>();
            Articles m_Articles;
            while (smartReader.Read())
            {
                m_Articles = new Articles();
                if (FieldSelect=="*" || FieldSelect.Contains("ArticleId")) m_Articles.ArticleId = smartReader.GetInt32("ArticleId");
                if (FieldSelect == "*" || FieldSelect.Contains("Title")) m_Articles.Title = smartReader.GetString("Title");
                if (FieldSelect == "*" || FieldSelect.Contains("Summary")) m_Articles.Summary = smartReader.GetString("Summary");
                if (FieldSelect == "*" || FieldSelect.Contains("ImagePath")) m_Articles.ImagePath = smartReader.GetString("ImagePath");
                if (FieldSelect == "*" || FieldSelect.Contains("PublishTime")) m_Articles.PublishTime = smartReader.GetDateTime("PublishTime");
                if (FieldSelect == "*" || FieldSelect.Contains("CrDateTime")) m_Articles.CrDateTime = smartReader.GetDateTime("CrDateTime");
                if (FieldSelect == "*" || FieldSelect.Contains("ArticleUrl")) m_Articles.ArticleUrl = smartReader.GetString("ArticleUrl");
                if (FieldSelect == "*" || FieldSelect.Contains("ArticleCode")) m_Articles.ArticleCode = smartReader.GetString("ArticleCode");
                if (FieldSelect == "*" || FieldSelect.Contains("ArticleContent")) m_Articles.ArticleContent = smartReader.GetString("ArticleContent");
                if (FieldSelect == "*" || FieldSelect.Contains("IconStatusId")) m_Articles.IconStatusId = smartReader.GetByte("IconStatusId");
                if (FieldSelect == "*" || FieldSelect.Contains("CategoryId")) m_Articles.CategoryId = smartReader.GetInt16("CategoryId");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaTitle")) m_Articles.MetaTitle = smartReader.GetString("MetaTitle");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaDesc")) m_Articles.MetaDesc = smartReader.GetString("MetaDesc");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaKeyword")) m_Articles.MetaKeyword = smartReader.GetString("MetaKeyword");
                if (FieldSelect == "*" || FieldSelect.Contains("ViewCount")) m_Articles.ViewCount = smartReader.GetInt32("ViewCount");

                l_Articles.Add(m_Articles);
            }
            return l_Articles;
        }
        //-----------------------------------------------------------------
        public static Articles InitOne(SmartDataReader smartReader, string FieldSelect = "*")
        {
            List<Articles> l_Articles = Init(smartReader, FieldSelect);
            if (l_Articles.Count > 0) return l_Articles[0];
            return null;
        }
        //-----------------------------------------------------------
        //Lay danh sach top bai viet theo chuyen muc
        public static CategoryArticles GetTopByCategoryId(short CategoryId, int RowAmount = 5, string OrderBy = "DisplayOrder DESC", string ArticleField = "ArticleId,Title,ImagePath,ArticleUrl", string CategoryField = "CategoryId,CategoryDesc,CategoryUrl")
        {
            return GetTopByCategoryId(CategoryId, RowAmount, 0, 0, OrderBy, ArticleField, CategoryField);
        }
        //-----------------------------------------------------------
        //Lay danh sach top bai viet theo chuyen muc
        public static CategoryArticles GetTopByCategoryId(short CategoryId, int RowAmount = 5, byte GetListSubCate = 0, byte GetListParentCate = 0, string OrderBy = "DisplayOrder DESC", string ArticleField = "ArticleId,Title,ImagePath,ArticleUrl", string CategoryField = "CategoryId,CategoryDesc,CategoryUrl")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            CategoryArticles RetVal = new CategoryArticles();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ArticleFieldSelect", ArticleField));
                cmd.Parameters.Add(new SqlParameter("@CategoryFieldSelect", CategoryField));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@GetListSubCate", GetListSubCate));
                cmd.Parameters.Add(new SqlParameter("@GetListParentCate", GetListParentCate));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //Lay thong tin chuyen muc
                RetVal.mCategory = CategoryHelpers.InitOne(smartReader, CategoryField);
                //Lay danh sach chuyen muc con
                if (GetListSubCate == 1)
                {
                    reader.NextResult();
                    RetVal.lSubCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                }
                //Lay danh sach chuyen muc cha
                if (GetListParentCate == 1)
                {
                    reader.NextResult();
                    RetVal.lParentCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                }
                //Lay danh sach bai viet
                reader.NextResult();
                RetVal.lArticle = Init(smartReader, ArticleField);

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
            return RetVal;
        }
        //-----------------------------------------------------------
        //Lay danh sach bai viet theo chuyen muc co paging
        public static CategoryArticles GetByCategoryId(short CategoryId, int RowAmount = 5, int PageIndex=0, byte GetRowCount=0, byte GetListSubCate = 0, byte GetListParentCate = 0, string OrderBy = "DisplayOrder DESC", string ArticleField = "ArticleId,Title,ImagePath,ArticleUrl", string CategoryField = "CategoryId,CategoryDesc,CategoryUrl")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            CategoryArticles RetVal = new CategoryArticles();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ArticleFieldSelect", ArticleField));
                cmd.Parameters.Add(new SqlParameter("@CategoryFieldSelect", CategoryField));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add(new SqlParameter("@GetListSubCate", GetListSubCate));
                cmd.Parameters.Add(new SqlParameter("@GetListParentCate", GetListParentCate));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //Lay thong tin chuyen muc
                RetVal.mCategory = CategoryHelpers.InitOne(smartReader, CategoryField);
                //Lay danh sach chuyen muc con
                if (GetListSubCate == 1)
                {
                    reader.NextResult();
                    RetVal.lSubCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                }
                //Lay danh sach chuyen muc cha
                if (GetListParentCate == 1)
                {
                    reader.NextResult();
                    RetVal.lParentCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                }
                //Lay danh sach bai viet
                reader.NextResult();
                RetVal.lArticle = Init(smartReader, ArticleField);

                reader.Close();
                RetVal.RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        //Lay danh sach bai viet theo tag co paging
        public static TagArticles GetByTagId(short SiteId, int TagId, int RowAmount = 5, int PageIndex = 0, byte GetRowCount = 0, string OrderBy = "PublishTime DESC", string ArticleField = "ArticleId,Title,ImagePath,ArticleUrl")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            TagArticles RetVal = new TagArticles();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@ArticleFieldSelect", ArticleField));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //Lay thong tin tag
                RetVal.mTag = Tags.InitOne(smartReader);
                //Lay danh sach bai viet
                reader.NextResult();
                RetVal.lArticle = Init(smartReader, ArticleField);

                reader.Close();
                RetVal.RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        //Lay danh sach top bai viet theo chuyen muc
        public static CategoryArticles GetMostView(short SiteId, short CategoryId, int RowAmount = 5, string ArticleField = "ArticleId,Title,ImagePath,ArticleUrl")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            CategoryArticles RetVal = new CategoryArticles();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ArticleFieldSelect", ArticleField));
                cmd.Parameters.Add(new SqlParameter("@GetMostView", 1));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //Lay danh sach bai viet
                RetVal.lArticle = Init(smartReader, ArticleField);

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
            return RetVal;
        }
        //-----------------------------------------------------------
        //Lay chi tiet bai viet
        public static ArticleDetail GetById(short CategoryId, int ArticleId, int RowAmountOther = 5, byte GetTagList = 0, byte GetListSubCate = 0, byte GetListParentCate = 0, string ArticleField = "ArticleId,Title,ImagePath,ArticleUrl,Summary,ArticleContent,PublishTime", string ArticleOtherField = "ArticleId,Title,ImagePath,ArticleUrl")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticleDetail RetVal = new ArticleDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ArticleFieldSelect", ArticleField));
                cmd.Parameters.Add(new SqlParameter("@ArticleFieldSelectOther", ArticleOtherField));
                cmd.Parameters.Add(new SqlParameter("@GetTagList", GetTagList));
                cmd.Parameters.Add(new SqlParameter("@GetListSubCate", GetListSubCate));
                cmd.Parameters.Add(new SqlParameter("@GetListParentCate", GetListParentCate));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmountOther));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //Lay thong tin chuyen muc
                RetVal.mCategory = CategoryHelpers.InitOne(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                //Lay danh sach chuyen muc con
                if (GetListSubCate == 1)
                {
                    reader.NextResult();
                    RetVal.lSubCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                }
                //Lay danh sach chuyen muc cha
                if (GetListParentCate == 1)
                {
                    reader.NextResult();
                    RetVal.lParentCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                }
                //Chi tiet bai viet
                reader.NextResult();
                RetVal.mArticle = InitOne(smartReader, ArticleField);
                //Lay danh tag
                if (GetTagList == 1)
                {
                    reader.NextResult();
                    RetVal.lTag = Tags.Init(smartReader);
                }
                //Lay danh sach bai viet khac
                if (RowAmountOther > 0)
                {
                    reader.NextResult();
                    RetVal.lOtherArticle = Init(smartReader, ArticleField);
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
            return RetVal;
        }
        //-----------------------------------------------------------
        //Lay chi tiet bai viet
        public static ArticleDetail GetById(short CategoryId, int ArticleId, int RowAmountOther = 5, byte GetTagList = 0, byte GetListSubCate = 0, byte GetListParentCate = 0, byte GetArticleRelate = 0, byte GetArticleMedia = 0, string ArticleField = "ArticleId,Title,ImagePath,ArticleUrl,Summary,ArticleContent,PublishTime", string ArticleOtherField = "ArticleId,Title,ImagePath,ArticleUrl")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticleDetail RetVal = new ArticleDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ArticleFieldSelect", ArticleField));
                cmd.Parameters.Add(new SqlParameter("@ArticleFieldSelectOther", ArticleOtherField));
                cmd.Parameters.Add(new SqlParameter("@GetTagList", GetTagList));
                cmd.Parameters.Add(new SqlParameter("@GetListSubCate", GetListSubCate));
                cmd.Parameters.Add(new SqlParameter("@GetListParentCate", GetListParentCate));
                cmd.Parameters.Add(new SqlParameter("@GetArticleRelate", GetArticleRelate));
                cmd.Parameters.Add(new SqlParameter("@GetArticleMedia", GetArticleMedia));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmountOther));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //Lay thong tin chuyen muc
                RetVal.mCategory = CategoryHelpers.InitOne(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                //Lay danh sach chuyen muc con
                if (GetListSubCate == 1)
                {
                    reader.NextResult();
                    RetVal.lSubCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                }
                //Lay danh sach chuyen muc cha
                if (GetListParentCate == 1)
                {
                    reader.NextResult();
                    RetVal.lParentCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                }
                //Chi tiet bai viet
                reader.NextResult();
                RetVal.mArticle = InitOne(smartReader, ArticleField);
                //Lay danh tag
                if (GetTagList == 1)
                {
                    reader.NextResult();
                    RetVal.lTag = Tags.Init(smartReader);
                }
                //Lay danh sach lien quan
                if (GetArticleRelate == 1)
                {
                    reader.NextResult();
                    RetVal.lArticleRelate = ArticleHelpers.Init(smartReader, "ArticleId,Title,ImagePath,ArticleUrl,PublishTime");
                }
                //Lay danh sach media
                if (GetArticleMedia == 1)
                {
                    reader.NextResult();
                    RetVal.lMedia = Medias.Static_Init(smartReader);
                }
                //Lay danh sach bai viet khac
                if (RowAmountOther > 0)
                {
                    reader.NextResult();
                    RetVal.lOtherArticle = Init(smartReader, ArticleOtherField);
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
            return RetVal;
        }
        //-----------------------------------------------------------
        //Lay danh sach bai viet theo chuyen muc co paging
        public static CategoryArticles GetPage(ArticleFilterParams mArticleFilterParams)
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            CategoryArticles RetVal = new CategoryArticles();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleFieldSelect", mArticleFilterParams.ArticleFieldSelect));
                cmd.Parameters.Add(new SqlParameter("@CategoryFieldSelect", mArticleFilterParams.CategoryFieldSelect));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", mArticleFilterParams.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", mArticleFilterParams.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", mArticleFilterParams.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ArticleTypeId", mArticleFilterParams.ArticleTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", mArticleFilterParams.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@TagId", mArticleFilterParams.TagId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", mArticleFilterParams.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@Keyword", mArticleFilterParams.Keyword));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", mArticleFilterParams.OrderBy));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", mArticleFilterParams.RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", mArticleFilterParams.PageIndex));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", mArticleFilterParams.GetRowCount));
                cmd.Parameters.Add(new SqlParameter("@GetListSubCate", mArticleFilterParams.GetListSubCate));
                cmd.Parameters.Add(new SqlParameter("@GetListParentCate", mArticleFilterParams.GetListParentCate));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                bool ReadNext = false;
                //Lay thong tin chuyen muc
                if (mArticleFilterParams.CategoryId > 0)
                {
                    RetVal.mCategory = CategoryHelpers.InitOne(smartReader, mArticleFilterParams.CategoryFieldSelect);
                    ReadNext = true;
                    //Lay danh sach chuyen muc con
                    if (mArticleFilterParams.GetListSubCate == 1)
                    {
                        reader.NextResult();
                        RetVal.lSubCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                    }
                    //Lay danh sach chuyen muc cha
                    if (mArticleFilterParams.GetListParentCate == 1)
                    {
                        reader.NextResult();
                        RetVal.lParentCategory = CategoryHelpers.Init(smartReader, "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath");
                    }
                }
                //Lay danh sach bai viet
                if (ReadNext) reader.NextResult();
                RetVal.lArticle = Init(smartReader, mArticleFilterParams.ArticleFieldSelect);

                reader.Close();
                RetVal.RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        //Luu log xem bai viet
        public static void InsertViewLog(int ArticleId, short SiteId, byte DataTypeId, short CategoryId, byte LanguageId, byte ActionTypeId, int CustomerId, string RefererFrom, string UserAgent, string FromIP)
        {
            byte Replicated = 1;
            byte ActUserId = 0;
            byte ApplicationTypeId = 0;
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleViewLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@RefererFrom", RefererFrom));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", UserAgent));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add("@ArticleViewLogByDayId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ArticleViewLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
    }
}
