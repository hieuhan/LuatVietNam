
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
    public class ArticlesViewDetail
    {
        private CategoriesView _mCategoriesView;
        private ArticlesView _mArticlesView;
        private List<CategoriesView> _lCategoriesViewToRoot;
        private List<ArticlesView> _lArticlesRelate;
        private List<ArticlesView> _lArticlesOther;
        private ArticlesViewMaster _mArticlesViewMaster = new ArticlesViewMaster();
        private List<ArticlesView> _lArticlesOtherNewest;

        //-----------------------------------------------------------------
        public ArticlesViewDetail()
        {
        }
        //-----------------------------------------------------------------
        ~ArticlesViewDetail()
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
        public List<CategoriesView> lCategoriesViewToRoot
        {
            get { return _lCategoriesViewToRoot; }
            set { _lCategoriesViewToRoot = value; }
        }
        //-----------------------------------------------------------------    
        public ArticlesView mArticlesView
        {
            get { return _mArticlesView; }
            set { _mArticlesView = value; }
        }
        
        //-----------------------------------------------------------------
        public List<ArticlesView> lArticlesRelate
        {
            get { return _lArticlesRelate; }
            set { _lArticlesRelate = value; }
        }
        //-----------------------------------------------------------------
        public List<ArticlesView> lArticlesOther
        {
            get { return _lArticlesOther; }
            set { _lArticlesOther = value; }
        }
        //-----------------------------------------------------------------    
        public ArticlesViewMaster mArticlesViewMaster
        {
            get { return _mArticlesViewMaster; }
            set { _mArticlesViewMaster = value; }
        }

        public List<ArticlesView> lArticlesOtherNewest
        {
            get { return _lArticlesOtherNewest; }
            set { _lArticlesOtherNewest = value; }
        }
    }
}