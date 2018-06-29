using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;
using sms.database;
using sms.utils;
namespace ICSoft.CMSViewLib
{
    public class ArticlesViewHelpers
    {
        public static ArticlesViewDetail View_GetArticleDetail(int ArticleId, short CategoryId, int RowAmountOther, byte useMasterView)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewDetail RetVal = new ArticlesViewDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewDetail");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmountOther", RowAmountOther));
                cmd.Parameters.Add(new SqlParameter("@useMasterView", useMasterView));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //Lay chi tiet bai viet
                ArticlesView m_Articles = InitViewHelpers.InitArticleOne(smartReader);

                //Lay thuoc tinh mo rong
                reader.NextResult();
                List<ArticleFeatures> l_ArticleFeatures = InitViewHelpers.InitArticleFeatures(smartReader);
                
                string PaddingChar = "&nbsp;&nbsp;";
                Features m_Features = new Features();
                m_Features.FeatureGroupId = 0;
                //m_Features.IsDisplay = 1;
                List<Features> l_Features = m_Features.GetListByFeatureGroupId(m_Articles.DataTypeId, m_Articles.CategoryId, PaddingChar);
                List<FeaturesView> l_FeaturesView = new List<FeaturesView>();

                if (l_Features != null)
                {
                    for (int i = 0; i < l_Features.Count; i++)
                    {
                        FeaturesView m_FeaturesView = new FeaturesView();
                        m_FeaturesView.DataDictionaryTypeId = l_Features[i].DataDictionaryTypeId;
                        m_FeaturesView.FeatureId = l_Features[i].FeatureId;
                        m_FeaturesView.FeatureName = l_Features[i].FeatureName;
                        m_FeaturesView.FeatureDesc = l_Features[i].FeatureDesc;
                        m_FeaturesView.IconPath = l_Features[i].IconPath;
                        m_FeaturesView.InputTypeId = l_Features[i].InputTypeId;
                        m_FeaturesView.ParentFeatureId = l_Features[i].ParentFeatureId;
                        m_FeaturesView.CrDateTime = l_Features[i].CrDateTime;
                        m_FeaturesView.CrUserId = l_Features[i].CrUserId;
                        m_FeaturesView.DisplayOrder = l_Features[i].DisplayOrder;
                        m_FeaturesView.FeatureGroupId = l_Features[i].FeatureGroupId;
                        m_FeaturesView.IsData = l_Features[i].IsData;
                        m_FeaturesView.IsDisplay = l_Features[i].IsDisplay;
                        m_FeaturesView.IsSearch = l_Features[i].IsSearch;

                        ArticleFeatures m_ArticleFeatures = l_ArticleFeatures.Find(j => j.FeatureId == l_Features[i].FeatureId);
                        if (m_ArticleFeatures != null)
                        {
                            m_FeaturesView.FeatureValue = m_ArticleFeatures.FeatureValue;
                            if (m_FeaturesView.DataDictionaryTypeId > 0 && !string.IsNullOrEmpty(m_FeaturesView.FeatureValue))
                            {
                                if (m_FeaturesView.FeatureValue.IndexOf(";") > 0)
                                {
                                    string m_Value = "";
                                    string[] arrFeatureValue = m_FeaturesView.FeatureValue.Split(';');
                                    for (int j = 0; j < arrFeatureValue.Length; j++)
                                    {
                                        if (j > 0) m_Value = m_Value + "; ";
                                        m_Value = m_Value + DataDictionaries.Static_Get(int.Parse(arrFeatureValue[j])).DataDictionaryDesc;
                                    }
                                    m_FeaturesView.FeatureValue = m_Value;
                                }
                                else
                                {
                                    int DataId = 0;
                                    if(m_FeaturesView.InputTypeId != 0 && m_FeaturesView.InputTypeId != 5 && m_FeaturesView.InputTypeId != 1)
                                    {
                                        int.TryParse(m_FeaturesView.FeatureValue, out DataId);
                                        if (DataId > 0)
                                            m_FeaturesView.FeatureValue = DataDictionaries.Static_Get(int.Parse(m_FeaturesView.FeatureValue)).DataDictionaryDesc;
                                    }
                                   
                                }
                            }
                        }
                        l_FeaturesView.Add(m_FeaturesView);
                    }
                }
                m_Articles.lFeaturesView = l_FeaturesView;

                //Lay tag
                reader.NextResult();
                m_Articles.lTagsView = InitViewHelpers.InitTagsView(smartReader);

                //Lay Media
                reader.NextResult();
                m_Articles.lMediasView = InitViewHelpers.InitMediasView(smartReader);

                //Lay comment
                reader.NextResult();
                m_Articles.lArticleCommentsView = InitViewHelpers.InitArticleCommentsView(smartReader);
                RetVal.mArticlesView = m_Articles;

                //Lay category chi tiet
                reader.NextResult();
                RetVal.mCategoriesView = InitViewHelpers.InitCateOne(smartReader, null);

                //Lay category toi root
                reader.NextResult();
                RetVal.lCategoriesViewToRoot = InitViewHelpers.InitCate(smartReader, null);

                //Lay bai viet lien quan
                reader.NextResult();
                RetVal.lArticlesRelate = InitViewHelpers.InitArticle(smartReader);

                //Lay bai viet khac
                reader.NextResult();
                RetVal.lArticlesOther = InitViewHelpers.InitArticle(smartReader);

                //Lay bai viet khac moi nhat
                reader.NextResult();
                RetVal.lArticlesOtherNewest = InitViewHelpers.InitArticle(smartReader);

                if (useMasterView == 1)
                {
                    //Lay MenuTop
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuTop = InitViewHelpers.InitCate(smartReader, null);
                    //Lay MenuBottom
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuBottom = InitViewHelpers.InitCate(smartReader, null);

                    //Lay list bai viet theo display type
                    reader.NextResult();
                    List<ArticlesView> l_ArticlesDisplay = InitViewHelpers.InitArticle(smartReader, true);
                    //Lay list chuyen muc theo display type
                    reader.NextResult();
                    List<CategoriesView> l_CategoriesDisplay = InitViewHelpers.InitCate(smartReader, l_ArticlesDisplay, true);
                    RetVal.mArticlesViewMaster = ArticlesViewMaster.Static_CreatePosition(RetVal.mArticlesViewMaster, l_CategoriesDisplay);
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
        public static List<ArticlesView> View_GetNextPage(short CategoryId, int PageIndex, int RowAmount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewNextPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = InitViewHelpers.InitArticle(smartReader);

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
        public static List<ArticlesView> View_GetArticleByCategoryId(short CategoryId, byte ShowTop, int RowAmount, string OrderBy)
        {
            Articles m_Articles = new Articles();
            int ActUserId=0;
            int PageIndex=0;
            short TagId=0;
            short EventStreamId=0;
            short DisplayTypeId=0;
            string DateFrom="";
            string DateTo="";
            int RowCount = 0;
            m_Articles.ReviewStatusId = 2;//Reviewed
            m_Articles.ShowTop = ShowTop;
            m_Articles.CategoryId = CategoryId;
            return m_Articles.GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<ArticlesView> View_GetArticleMostView(short CategoryId, int RowAmount, int PageIndex=0)
        {
            Articles m_Articles = new Articles();
            int ActUserId = 0;
            short TagId = 0;
            short EventStreamId = 0;
            short DisplayTypeId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            m_Articles.ReviewStatusId = 2;//Reviewed
            m_Articles.ShowTop = 0;
            m_Articles.CategoryId = CategoryId;
            string OrderBy = "";
            return m_Articles.GetMostView(ActUserId, RowAmount, PageIndex, OrderBy, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<ArticlesView> View_GetArticleByArticleTypeId(byte ArticleTypeId, byte ShowTop, int RowAmount, string OrderBy)
        {
            Articles m_Articles = new Articles();
            int ActUserId = 0;
            int PageIndex = 0;
            short TagId = 0;
            short EventStreamId = 0;
            short DisplayTypeId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            m_Articles.ReviewStatusId = 2;//Reviewed
            m_Articles.ArticleTypeId = ArticleTypeId;
            m_Articles.ShowTop = ShowTop;
            return m_Articles.GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<ArticlesView> View_SearchWithContent(int RowAmount, int PageIndex, byte LanguageId, byte ApplicationTypeId, short CategoryId, short SiteId, byte DataTypeId, string Title, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewSearchWithContent");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@Title", Title));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = InitViewHelpers.InitArticle(smartReader);

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
            return RetVal;
        }
        public static List<ArticlesView> View_Search(int RowAmount, int PageIndex, byte LanguageId, byte ApplicationTypeId, short CategoryId, short SiteId, byte DataTypeId, string Title, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewSearch");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@Title", Title));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = InitViewHelpers.InitArticle(smartReader);

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
            return RetVal;
        }
        //-----------------------------------------------------------
        public static List<ArticlesView> View_Search(int RowAmount, int PageIndex, byte LanguageId, byte ApplicationTypeId, short CategoryId, short SiteId, byte DataTypeId, int TagId, string Title, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewSearch");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@Title", Title));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = InitViewHelpers.InitArticle(smartReader);

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
            return RetVal;
        }
        //-----------------------------------------------------------
        public static List<ArticlesView> View_Search_01(int RowAmount, int PageIndex, byte LanguageId, byte ApplicationTypeId, short CategoryId, short SiteId, byte DataTypeId, int TagId, string Title, int ProvinceId, int DistrictId, int PriceFrom, int PriceTo, int FeatureGroupId, string OrderBy, ref int RowCount, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewSearch");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@Title", Title));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DistrictId", DistrictId));
                cmd.Parameters.Add(new SqlParameter("@PriceFrom", PriceFrom));
                cmd.Parameters.Add(new SqlParameter("@PriceTo", PriceTo));
                if (DateFrom.HasValue && DateFrom != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                if (DateTo.HasValue && DateTo != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = InitViewHelpers.InitArticle(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);

                //lay feature data
                if (FeatureGroupId > 0)
                {
                    string m_ArticleId_List = GetLisArticleId(RetVal);
                    cmd = new SqlCommand("Articles_GetFeatureDataByListId");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FeatureGroupId", FeatureGroupId));
                    cmd.Parameters.Add(new SqlParameter("@ArticleId_List", m_ArticleId_List));
                    cmd.Connection = con;

                    reader = cmd.ExecuteReader();
                    smartReader = new SmartDataReader(reader);

                    //Lay ds feature
                    List<FeaturesView> l_Features = FeaturesView.Init(smartReader, true);

                    //Lay ds feature goc
                    reader.NextResult();
                    List<FeaturesView> l_FeaturesGoc = FeaturesView.Init(smartReader, false);

                    reader.Close();

                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].lFeaturesView = FeaturesView.Static_GetListByArticleId(RetVal[i].ArticleId, l_Features, l_FeaturesGoc);
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
            return RetVal;
        }
        //-----------------------------------------------------------
        public static ArticlesViewCate View_GetArticleCate(int CategoryId, int RowAmountTop, int RowAmountSubCate, int RowAmountNewest, byte useMasterView)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewCate RetVal = new ArticlesViewCate();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewCate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmountTop", RowAmountTop));
                cmd.Parameters.Add(new SqlParameter("@RowAmountSubCate", RowAmountSubCate));
                cmd.Parameters.Add(new SqlParameter("@RowAmountNewest", RowAmountNewest));
                cmd.Parameters.Add(new SqlParameter("@useMasterView", useMasterView));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay list bai viet top chuyen muc
                List<ArticlesView> l_ArticlesView = InitViewHelpers.InitArticle(smartReader);

                reader.NextResult();
                RetVal.mCategoriesView = InitViewHelpers.InitCateOne(smartReader, l_ArticlesView);

                //Lay list bai viet sub chuyen muc
                reader.NextResult();
                l_ArticlesView = InitViewHelpers.InitArticle(smartReader);

                //Lay sub category
                reader.NextResult();
                RetVal.lSubCategories = InitViewHelpers.InitCate(smartReader, l_ArticlesView);

                //Lay list bai viet moi nhat cua chuyen muc
                reader.NextResult();
                RetVal.lArticlesNewest = InitViewHelpers.InitArticle(smartReader);

                if (useMasterView == 1)
                {
                    //Lay MenuTop
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuTop = InitViewHelpers.InitCate(smartReader, null);
                    //Lay MenuBottom
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuBottom = InitViewHelpers.InitCate(smartReader, null);

                    //Lay list bai viet theo display type
                    reader.NextResult();
                    List<ArticlesView> l_ArticlesDisplay = InitViewHelpers.InitArticle(smartReader, true);
                    //Lay list chuyen muc theo display type
                    reader.NextResult();
                    List<CategoriesView> l_CategoriesDisplay = InitViewHelpers.InitCate(smartReader, l_ArticlesDisplay, true);
                    RetVal.mArticlesViewMaster = ArticlesViewMaster.Static_CreatePosition(RetVal.mArticlesViewMaster, l_CategoriesDisplay);
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
        public static List<ArticlesView> GetTopNews(short SiteId, byte DataTypeId, byte LanguageId, byte ApplicationTypeId, int RowAmountTop)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<ArticlesView> RetVal = new List<ArticlesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewTopNews");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowAmountTop", RowAmountTop));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay list bai viet top
                RetVal = InitViewHelpers.InitArticle(smartReader);
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
        public static ArticlesViewCate GetViewByCateId_Paging(int CategoryId, int RowAmount, byte GetListCateOfShowArticle, byte GetListSubCate, byte GetListParentCate, byte GetRowCount, byte useMasterView, int PageIndex, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewCate RetVal = new ArticlesViewCate();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewByCateId_Paging");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@GetListCateOfShowArticle", GetListCateOfShowArticle));
                cmd.Parameters.Add(new SqlParameter("@GetListSubCate", GetListSubCate));
                cmd.Parameters.Add(new SqlParameter("@GetListParentCate", GetListParentCate));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add(new SqlParameter("@useMasterView", useMasterView));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay list bai viet chuyen muc
                RetVal.lArticlesNewest = InitViewHelpers.InitArticle(smartReader);

                //Danh sach chuyen muc cua cac bai viet lay ra
                if (GetListCateOfShowArticle == 1)
                {
                    reader.NextResult();
                    RetVal.lCategoriesOfShowArticle = InitViewHelpers.InitCate(smartReader, null);
                }

                //Chi tiet chuyen muc
                reader.NextResult();
                RetVal.mCategoriesView = InitViewHelpers.InitCateOne(smartReader, null);

                //Lay list sub category
                if (GetListSubCate == 1)
                {
                    reader.NextResult();
                    RetVal.lSubCategories = InitViewHelpers.InitCate(smartReader, null);
                }

                //Lay list parent category
                if (GetListParentCate == 1)
                {
                    reader.NextResult();
                    RetVal.lParentCategories = InitViewHelpers.InitCate(smartReader, null);
                }

                if (useMasterView == 1)
                {
                    //Lay MenuTop
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuTop = InitViewHelpers.InitCate(smartReader, null);
                    //Lay MenuBottom
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuBottom = InitViewHelpers.InitCate(smartReader, null);

                    //Lay list bai viet theo display type
                    reader.NextResult();
                    List<ArticlesView> l_ArticlesDisplay = InitViewHelpers.InitArticle(smartReader, true);
                    //Lay list chuyen muc theo display type
                    reader.NextResult();
                    List<CategoriesView> l_CategoriesDisplay = InitViewHelpers.InitCate(smartReader, l_ArticlesDisplay, true);
                    RetVal.mArticlesViewMaster = ArticlesViewMaster.Static_CreatePosition(RetVal.mArticlesViewMaster, l_CategoriesDisplay);
                }

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
            return RetVal;
        }
        //-----------------------------------------------------------
        public static ArticlesViewCate GetViewByTagId_Paging(int TagId, int RowAmount, byte GetListCateOfShowArticle, byte GetRowCount, byte useMasterView, int PageIndex, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewCate RetVal = new ArticlesViewCate();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewByTagId_Paging");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@GetListCateOfShowArticle", GetListCateOfShowArticle));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add(new SqlParameter("@useMasterView", useMasterView));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay list bai viet
                RetVal.lArticlesNewest = InitViewHelpers.InitArticle(smartReader);

                //Danh sach chuyen muc cua cac bai viet lay ra
                if (GetListCateOfShowArticle == 1)
                {
                    reader.NextResult();
                    RetVal.lCategoriesOfShowArticle = InitViewHelpers.InitCate(smartReader, null);
                }

                if (useMasterView == 1)
                {
                    //Lay MenuTop
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuTop = InitViewHelpers.InitCate(smartReader, null);
                    //Lay MenuBottom
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuBottom = InitViewHelpers.InitCate(smartReader, null);

                    //Lay list bai viet theo display type
                    reader.NextResult();
                    List<ArticlesView> l_ArticlesDisplay = InitViewHelpers.InitArticle(smartReader, true);
                    //Lay list chuyen muc theo display type
                    reader.NextResult();
                    List<CategoriesView> l_CategoriesDisplay = InitViewHelpers.InitCate(smartReader, l_ArticlesDisplay, true);
                    RetVal.mArticlesViewMaster = ArticlesViewMaster.Static_CreatePosition(RetVal.mArticlesViewMaster, l_CategoriesDisplay);
                }

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
            return RetVal;
        }
        //-----------------------------------------------------------
        public static ArticlesViewCate GetViewNewest_Paging(short SiteId, byte DataTypeId, int RowAmount, byte GetListCateOfShowArticle, byte GetRowCount, byte useMasterView, int PageIndex, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewCate RetVal = new ArticlesViewCate();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewNewest_Paging");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@GetListCateOfShowArticle", GetListCateOfShowArticle));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add(new SqlParameter("@useMasterView", useMasterView));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay list bai viet
                RetVal.lArticlesNewest = InitViewHelpers.InitArticle(smartReader);

                //Danh sach chuyen muc cua cac bai viet lay ra
                if (GetListCateOfShowArticle == 1)
                {
                    reader.NextResult();
                    RetVal.lCategoriesOfShowArticle = InitViewHelpers.InitCate(smartReader, null);
                }

                if (useMasterView == 1)
                {
                    //Lay MenuTop
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuTop = InitViewHelpers.InitCate(smartReader, null);
                    //Lay MenuBottom
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuBottom = InitViewHelpers.InitCate(smartReader, null);

                    //Lay list bai viet theo display type
                    reader.NextResult();
                    List<ArticlesView> l_ArticlesDisplay = InitViewHelpers.InitArticle(smartReader, true);
                    //Lay list chuyen muc theo display type
                    reader.NextResult();
                    List<CategoriesView> l_CategoriesDisplay = InitViewHelpers.InitCate(smartReader, l_ArticlesDisplay, true);
                    RetVal.mArticlesViewMaster = ArticlesViewMaster.Static_CreatePosition(RetVal.mArticlesViewMaster, l_CategoriesDisplay);
                }

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
            return RetVal;
        }
        //-----------------------------------------------------------
        public static ArticlesViewCate GetViewMostView_Paging(short SiteId, byte DataTypeId, int RowAmount, byte GetListCateOfShowArticle, byte GetRowCount, byte useMasterView, int PageIndex, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewCate RetVal = new ArticlesViewCate();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewMostView_Paging");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@GetListCateOfShowArticle", GetListCateOfShowArticle));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add(new SqlParameter("@useMasterView", useMasterView));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay list bai viet
                RetVal.lArticlesNewest = InitViewHelpers.InitArticle(smartReader);

                //Danh sach chuyen muc cua cac bai viet lay ra
                if (GetListCateOfShowArticle == 1)
                {
                    reader.NextResult();
                    RetVal.lCategoriesOfShowArticle = InitViewHelpers.InitCate(smartReader, null);
                }

                if (useMasterView == 1)
                {
                    //Lay MenuTop
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuTop = InitViewHelpers.InitCate(smartReader, null);
                    //Lay MenuBottom
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuBottom = InitViewHelpers.InitCate(smartReader, null);

                    //Lay list bai viet theo display type
                    reader.NextResult();
                    List<ArticlesView> l_ArticlesDisplay = InitViewHelpers.InitArticle(smartReader, true);
                    //Lay list chuyen muc theo display type
                    reader.NextResult();
                    List<CategoriesView> l_CategoriesDisplay = InitViewHelpers.InitCate(smartReader, l_ArticlesDisplay, true);
                    RetVal.mArticlesViewMaster = ArticlesViewMaster.Static_CreatePosition(RetVal.mArticlesViewMaster, l_CategoriesDisplay);
                }

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
            return RetVal;
        }
        //-----------------------------------------------------------
        public static ArticlesViewCate GetViewMostViewByCate_Paging(short CategoryId, int RowAmount, byte GetListCateOfShowArticle, byte GetRowCount, int PageIndex, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewCate RetVal = new ArticlesViewCate();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewMostViewByCate_Paging");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@GetListCateOfShowArticle", GetListCateOfShowArticle));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay list bai viet
                RetVal.lArticlesNewest = InitViewHelpers.InitArticle(smartReader);

                //Danh sach chuyen muc cua cac bai viet lay ra
                if (GetListCateOfShowArticle == 1)
                {
                    reader.NextResult();
                    RetVal.lCategoriesOfShowArticle = InitViewHelpers.InitCate(smartReader, null);
                }

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
            return RetVal;
        }
        //-----------------------------------------------------------
        public static ArticlesViewCate View_GetArticleByDataTypeWithFeature(int DataTypeId, string Keyword, int RowAmount, int PageIndex, ref int RowCount)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewCate RetVal = new ArticlesViewCate();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewByDataTypeWithFeature");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@Keyword", Keyword));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal.lArticlesNewest = InitViewHelpers.InitArticle(smartReader);
                //Lay ds feature
                reader.NextResult();
                List<FeaturesView> l_Features = FeaturesView.Init(smartReader, true);

                //Lay ds feature goc
                reader.NextResult();
                List<FeaturesView> l_FeaturesGoc = FeaturesView.Init(smartReader, false);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);

                for (int i = 0; i < RetVal.lArticlesNewest.Count; i++)
                {
                    RetVal.lArticlesNewest[i].lFeaturesView = FeaturesView.Static_GetListByArticleId(RetVal.lArticlesNewest[i].ArticleId, l_Features, l_FeaturesGoc);
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
            return RetVal;
        }
        //-----------------------------------------------------------
        public static ArticlesViewHome View_GetArticleHome(short SiteId, byte DataTypeId, byte LanguageId, byte ApplicationTypeId, int RowAmountTop, byte useMasterView)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewHome RetVal = new ArticlesViewHome();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewHome");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowAmountTop", RowAmountTop));
                cmd.Parameters.Add(new SqlParameter("@useMasterView", useMasterView));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay list bai viet top
                RetVal.lArticlesTop = InitViewHelpers.InitArticle(smartReader);

                if (useMasterView == 1)
                {
                    //Lay MenuTop
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuTop = InitViewHelpers.InitCate(smartReader, null);
                    //Lay MenuBottom
                    reader.NextResult();
                    RetVal.mArticlesViewMaster.lMenuBottom = InitViewHelpers.InitCate(smartReader, null);

                    //MasterView: Lay list bai viet theo display type
                    reader.NextResult();
                    List<ArticlesView> l_ArticlesDisplay = InitViewHelpers.InitArticle(smartReader, true);

                    //MasterView: Lay list chuyen muc theo display type
                    reader.NextResult();
                    List<CategoriesView> l_CategoriesDisplay = InitViewHelpers.InitCate(smartReader, l_ArticlesDisplay, true);
                    RetVal.mArticlesViewMaster = ArticlesViewMaster.Static_CreatePosition(RetVal.mArticlesViewMaster, l_CategoriesDisplay);
                }
                else
                {
                    //Lay list bai viet theo display type
                    reader.NextResult();
                    List<ArticlesView> l_ArticlesDisplay = InitViewHelpers.InitArticle(smartReader, true);
                    //Lay list chuyen muc theo display type
                    reader.NextResult();
                    List<CategoriesView> l_CategoriesDisplay = InitViewHelpers.InitCate(smartReader, l_ArticlesDisplay, true);
                    RetVal = ArticlesViewHome.Static_CreatePosition(RetVal, l_CategoriesDisplay);
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
        public static ArticlesViewMaster View_GetArticleMaster(short SiteId, byte DataTypeId, byte LanguageId, byte ApplicationTypeId, byte IsHome)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            ArticlesViewMaster RetVal = new ArticlesViewMaster();
            try
            {
                SqlCommand cmd = new SqlCommand("Articles_GetViewMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@IsHome", IsHome));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay MenuTop
                RetVal.lMenuTop = InitViewHelpers.InitCate(smartReader, null);
                //Lay MenuBottom
                reader.NextResult();
                RetVal.lMenuBottom = InitViewHelpers.InitCate(smartReader, null);

                //MasterView: Lay list bai viet theo display type
                reader.NextResult();
                List<ArticlesView> l_ArticlesDisplay = InitViewHelpers.InitArticle(smartReader, true);
                //MasterView: Lay list chuyen muc theo display type
                reader.NextResult();
                List<CategoriesView> l_CategoriesDisplay = InitViewHelpers.InitCate(smartReader, l_ArticlesDisplay, true);
                RetVal = ArticlesViewMaster.Static_CreatePosition(RetVal, l_CategoriesDisplay);

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
        //-------------------------------------------------------------- 
        public static string GetLisArticleId(List<ArticlesView> list)
        {
            string RetVal = "0";
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    RetVal = RetVal + "," + list[i].ArticleId.ToString();
                }
            }
            return RetVal;
        }
    }

}//end namespace service
