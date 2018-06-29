
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
    public class ArticleCategoriesView
    {
        private int _ArticleCategoryId;
        private int _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _ArticleId;
        private short _CategoryId;
        private string _CategoryName;
        private string _CategoryDesc;
        //-----------------------------------------------------------------
        public ArticleCategoriesView()
        {
        }
        //-----------------------------------------------------------------        
        ~ArticleCategoriesView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleCategoryId
        {
            get { return _ArticleCategoryId; }
            set { _ArticleCategoryId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        //-----------------------------------------------------------------
        public string CategoryDesc
        {
            get { return _CategoryDesc; }
            set { _CategoryDesc = value; }
        }
        //-----------------------------------------------------------------
        public static List<ArticleCategoriesView> Init(SmartDataReader smartReader, bool hasCategoryName)
        {
            List<ArticleCategoriesView> l_ArticleCategories = new List<ArticleCategoriesView>();
            try
            {
                while (smartReader.Read())
                {
                    ArticleCategoriesView m_ArticleCategories = new ArticleCategoriesView();
                    m_ArticleCategories.ArticleCategoryId = smartReader.GetInt32("ArticleCategoryId");
                    m_ArticleCategories.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleCategories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_ArticleCategories.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_ArticleCategories.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ArticleCategories.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    if (hasCategoryName)
                    {
                        m_ArticleCategories.CategoryName = smartReader.GetString("CategoryName");
                        m_ArticleCategories.CategoryDesc = smartReader.GetString("CategoryDesc");
                    }

                    l_ArticleCategories.Add(m_ArticleCategories);
                }
                return l_ArticleCategories;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-------------------------------------------------------------- 
        public static List<ArticleCategoriesView> Static_GetList(int ArticleId, List<ArticleCategoriesView> list)
        {
            List<ArticleCategoriesView> RetVal = new List<ArticleCategoriesView>();
            try
            {
                RetVal = list.FindAll(i => i.ArticleId == ArticleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}