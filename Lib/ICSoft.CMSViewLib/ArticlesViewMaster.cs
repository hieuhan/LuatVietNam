
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
    public class ArticlesViewMaster
    {
        private List<CategoriesView> _lMenuTop;
        private List<CategoriesView> _lMenuBottom;
        private List<CategoriesView> _lCategoriesTop1;
        private List<CategoriesView> _lCategoriesTop2;
        private List<CategoriesView> _lCategoriesTop3;
        private List<CategoriesView> _lCategoriesTop4;
        private List<CategoriesView> _lCategoriesTop5;
        private List<CategoriesView> _lCategoriesBottom1;
        private List<CategoriesView> _lCategoriesBottom2;
        private List<CategoriesView> _lCategoriesBottom3;
        private List<CategoriesView> _lCategoriesBottom4;
        private List<CategoriesView> _lCategoriesBottom5;
        private List<CategoriesView> _lCategoriesMain1;
        private List<CategoriesView> _lCategoriesMain2;
        private List<CategoriesView> _lCategoriesMain3;
        private List<CategoriesView> _lCategoriesMain4;
        private List<CategoriesView> _lCategoriesMain5;
        private List<CategoriesView> _lCategoriesLeft1;
        private List<CategoriesView> _lCategoriesLeft2;
        private List<CategoriesView> _lCategoriesLeft3;
        private List<CategoriesView> _lCategoriesLeft4;
        private List<CategoriesView> _lCategoriesLeft5;
        private List<CategoriesView> _lCategoriesRight1;
        private List<CategoriesView> _lCategoriesRight2;
        private List<CategoriesView> _lCategoriesRight3;
        private List<CategoriesView> _lCategoriesRight4;
        private List<CategoriesView> _lCategoriesRight5;
        
        //-----------------------------------------------------------------
        public ArticlesViewMaster()
        {
        }
        //-----------------------------------------------------------------
        ~ArticlesViewMaster()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lMenuTop
        {
            get { return _lMenuTop; }
            set { _lMenuTop = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lMenuBottom
        {
            get { return _lMenuBottom; }
            set { _lMenuBottom = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesTop1
        {
            get { return _lCategoriesTop1; }
            set { _lCategoriesTop1 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesTop2
        {
            get { return _lCategoriesTop2; }
            set { _lCategoriesTop2 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesTop3
        {
            get { return _lCategoriesTop3; }
            set { _lCategoriesTop3 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesTop4
        {
            get { return _lCategoriesTop4; }
            set { _lCategoriesTop4 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesTop5
        {
            get { return _lCategoriesTop5; }
            set { _lCategoriesTop5 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesBottom1
        {
            get { return _lCategoriesBottom1; }
            set { _lCategoriesBottom1 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesBottom2
        {
            get { return _lCategoriesBottom2; }
            set { _lCategoriesBottom2 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesBottom3
        {
            get { return _lCategoriesBottom3; }
            set { _lCategoriesBottom3 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesBottom4
        {
            get { return _lCategoriesBottom4; }
            set { _lCategoriesBottom4 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesBottom5
        {
            get { return _lCategoriesBottom5; }
            set { _lCategoriesBottom5 = value; }
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
        public List<CategoriesView> lCategoriesLeft1
        {
            get { return _lCategoriesLeft1; }
            set { _lCategoriesLeft1 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesLeft2
        {
            get { return _lCategoriesLeft2; }
            set { _lCategoriesLeft2 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesLeft3
        {
            get { return _lCategoriesLeft3; }
            set { _lCategoriesLeft3 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesLeft4
        {
            get { return _lCategoriesLeft4; }
            set { _lCategoriesLeft4 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesLeft5
        {
            get { return _lCategoriesLeft5; }
            set { _lCategoriesLeft5 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesRight1
        {
            get { return _lCategoriesRight1; }
            set { _lCategoriesRight1 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesRight2
        {
            get { return _lCategoriesRight2; }
            set { _lCategoriesRight2 = value; }
        }

        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesRight3
        {
            get { return _lCategoriesRight3; }
            set { _lCategoriesRight3 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesRight4
        {
            get { return _lCategoriesRight4; }
            set { _lCategoriesRight4 = value; }
        }
        //-----------------------------------------------------------------    
        public List<CategoriesView> lCategoriesRight5
        {
            get { return _lCategoriesRight5; }
            set { _lCategoriesRight5 = value; }
        }
        //-----------------------------------------------------------------    
        //-----------------------------------------------------------------
        public static ArticlesViewMaster Static_CreatePosition(ArticlesViewMaster initArticlesViewMaster, List<CategoriesView> l_CategoriesDisplay)
        {
            ArticlesViewMaster RetVal = new ArticlesViewMaster();
            if (initArticlesViewMaster != null) RetVal = initArticlesViewMaster;

            RetVal.lCategoriesMain1 = CategoriesView.Static_GetByDisplayTypeName("Main1", l_CategoriesDisplay);
            RetVal.lCategoriesMain2 = CategoriesView.Static_GetByDisplayTypeName("Main2", l_CategoriesDisplay);
            RetVal.lCategoriesMain3 = CategoriesView.Static_GetByDisplayTypeName("Main3", l_CategoriesDisplay);
            RetVal.lCategoriesMain4 = CategoriesView.Static_GetByDisplayTypeName("Main4", l_CategoriesDisplay);
            RetVal.lCategoriesMain5 = CategoriesView.Static_GetByDisplayTypeName("Main5", l_CategoriesDisplay);

            RetVal.lCategoriesTop1 = CategoriesView.Static_GetByDisplayTypeName("Top1", l_CategoriesDisplay);
            RetVal.lCategoriesTop2 = CategoriesView.Static_GetByDisplayTypeName("Top2", l_CategoriesDisplay);
            RetVal.lCategoriesTop3 = CategoriesView.Static_GetByDisplayTypeName("Top3", l_CategoriesDisplay);
            RetVal.lCategoriesTop4 = CategoriesView.Static_GetByDisplayTypeName("Top4", l_CategoriesDisplay);
            RetVal.lCategoriesTop5 = CategoriesView.Static_GetByDisplayTypeName("Top5", l_CategoriesDisplay);

            RetVal.lCategoriesBottom1 = CategoriesView.Static_GetByDisplayTypeName("Bottom1", l_CategoriesDisplay);
            RetVal.lCategoriesBottom2 = CategoriesView.Static_GetByDisplayTypeName("Bottom2", l_CategoriesDisplay);
            RetVal.lCategoriesBottom3 = CategoriesView.Static_GetByDisplayTypeName("Bottom3", l_CategoriesDisplay);
            RetVal.lCategoriesBottom4 = CategoriesView.Static_GetByDisplayTypeName("Bottom4", l_CategoriesDisplay);
            RetVal.lCategoriesBottom5 = CategoriesView.Static_GetByDisplayTypeName("Bottom5", l_CategoriesDisplay);

            RetVal.lCategoriesLeft1 = CategoriesView.Static_GetByDisplayTypeName("Left1", l_CategoriesDisplay);
            RetVal.lCategoriesLeft2 = CategoriesView.Static_GetByDisplayTypeName("Left2", l_CategoriesDisplay);
            RetVal.lCategoriesLeft3 = CategoriesView.Static_GetByDisplayTypeName("Left3", l_CategoriesDisplay);
            RetVal.lCategoriesLeft4 = CategoriesView.Static_GetByDisplayTypeName("Left4", l_CategoriesDisplay);
            RetVal.lCategoriesLeft5 = CategoriesView.Static_GetByDisplayTypeName("Left5", l_CategoriesDisplay);

            RetVal.lCategoriesRight1 = CategoriesView.Static_GetByDisplayTypeName("Right1", l_CategoriesDisplay);
            RetVal.lCategoriesRight2 = CategoriesView.Static_GetByDisplayTypeName("Right2", l_CategoriesDisplay);
            RetVal.lCategoriesRight3 = CategoriesView.Static_GetByDisplayTypeName("Right3", l_CategoriesDisplay);
            RetVal.lCategoriesRight4 = CategoriesView.Static_GetByDisplayTypeName("Right4", l_CategoriesDisplay);
            RetVal.lCategoriesRight5 = CategoriesView.Static_GetByDisplayTypeName("Right5", l_CategoriesDisplay);

            return RetVal;
        }
    }
}