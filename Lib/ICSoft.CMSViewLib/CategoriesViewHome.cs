
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
    public class CategoriesViewHome
    {
        private List<CategoriesView> _lCategoriesShowTop;
        private List<CategoriesView> _lCategoriesShowBottom;
        private List<CategoriesView> _lCategoriesShowWeb;
        private List<CategoriesView> _lCategoriesShowWap;
        private List<CategoriesView> _lCategoriesShowApp;
        
        //-----------------------------------------------------------------
        public CategoriesViewHome()
        {
        }
        //-----------------------------------------------------------------
        ~CategoriesViewHome()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesShowTop
        {
            get { return _lCategoriesShowTop; }
            set { _lCategoriesShowTop = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesShowBottom
        {
            get { return _lCategoriesShowBottom; }
            set { _lCategoriesShowBottom = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesShowWeb
        {
            get { return _lCategoriesShowWeb; }
            set { _lCategoriesShowWeb = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesShowWap
        {
            get { return _lCategoriesShowWap; }
            set { _lCategoriesShowWap = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesShowApp
        {
            get { return _lCategoriesShowApp; }
            set { _lCategoriesShowApp = value; }
        }
        //-----------------------------------------------------------------
    }
}