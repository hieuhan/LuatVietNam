
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
    public class ArticlesViewCate
    {
        private CategoriesView _mCategoriesView;
        private List<CategoriesView> _lCategoriesOfShowArticle;
        private List<CategoriesView> _lSubCategories;
        private List<CategoriesView> _lParentCategories;
        private List<ArticlesView> _lArticlesNewest;
        private ArticlesViewMaster _mArticlesViewMaster = new ArticlesViewMaster();
        
        //-----------------------------------------------------------------
        public ArticlesViewCate()
        {
        }
        //-----------------------------------------------------------------
        ~ArticlesViewCate()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public CategoriesView mCategoriesView
        {
            get { return _mCategoriesView; }
            set { _mCategoriesView = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lSubCategories
        {
            get { return _lSubCategories; }
            set { _lSubCategories = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lParentCategories
        {
            get { return _lParentCategories; }
            set { _lParentCategories = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesOfShowArticle
        {
            get { return _lCategoriesOfShowArticle; }
            set { _lCategoriesOfShowArticle = value; }
        }
        //-----------------------------------------------------------------    
        public List<ArticlesView> lArticlesNewest
        {
            get { return _lArticlesNewest; }
            set { _lArticlesNewest = value; }
        }
        //-----------------------------------------------------------------    
        public ArticlesViewMaster mArticlesViewMaster
        {
            get { return _mArticlesViewMaster; }
            set { _mArticlesViewMaster = value; }
        }
        //-----------------------------------------------------------------    
    }
}