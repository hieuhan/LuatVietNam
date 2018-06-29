
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
    public class ArticlesViewHome
    {
        private List<ArticlesView> _lArticlesTop;
        private List<CategoriesView> _lCategoriesMain1;
        private List<CategoriesView> _lCategoriesMain2;
        private List<CategoriesView> _lCategoriesMain3;
        private List<CategoriesView> _lCategoriesMain4;
        private List<CategoriesView> _lCategoriesMain5;
        private ArticlesViewMaster _mArticlesViewMaster = new ArticlesViewMaster();
        
        //-----------------------------------------------------------------
        public ArticlesViewHome()
        {
        }
        //-----------------------------------------------------------------
        ~ArticlesViewHome()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public List<ArticlesView> lArticlesTop
        {
            get { return _lArticlesTop; }
            set { _lArticlesTop = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesMain1
        {
            get { return _lCategoriesMain1; }
            set { _lCategoriesMain1 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesMain2
        {
            get { return _lCategoriesMain2; }
            set { _lCategoriesMain2 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesMain3
        {
            get { return _lCategoriesMain3; }
            set { _lCategoriesMain3 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesMain4
        {
            get { return _lCategoriesMain4; }
            set { _lCategoriesMain4 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesMain5
        {
            get { return _lCategoriesMain5; }
            set { _lCategoriesMain5 = value; }
        }
        //-----------------------------------------------------------------    
        public ArticlesViewMaster mArticlesViewMaster
        {
            get { return _mArticlesViewMaster; }
            set { _mArticlesViewMaster = value; }
        }
        //-----------------------------------------------------------------    
        //-----------------------------------------------------------------
        public static ArticlesViewHome Static_CreatePosition(ArticlesViewHome initArticlesViewHome, List<CategoriesView> l_CategoriesDisplay)
        {
            ArticlesViewHome RetVal = new ArticlesViewHome();
            if (initArticlesViewHome != null) RetVal = initArticlesViewHome;

            RetVal.lCategoriesMain1 = CategoriesView.Static_GetByDisplayTypeName("Main1", l_CategoriesDisplay);
            RetVal.lCategoriesMain2 = CategoriesView.Static_GetByDisplayTypeName("Main2", l_CategoriesDisplay);
            RetVal.lCategoriesMain3 = CategoriesView.Static_GetByDisplayTypeName("Main3", l_CategoriesDisplay);
            RetVal.lCategoriesMain4 = CategoriesView.Static_GetByDisplayTypeName("Main4", l_CategoriesDisplay);
            RetVal.lCategoriesMain5 = CategoriesView.Static_GetByDisplayTypeName("Main5", l_CategoriesDisplay);

            return RetVal;
        }
    }
}