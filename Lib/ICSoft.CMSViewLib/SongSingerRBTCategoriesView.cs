
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Data;

namespace ICSoft.CMSViewLib
{
    public class SongSingerRBTCategoriesView
    {
        private int _SongSingerRBTCategoryId;
        private int _SongSingerRBTId;
        private short _CategoryId;
        private int _DisplayOrder;
        private string _CategoryName;
        private string _CategoryUrl;
        //-----------------------------------------------------------------
        public SongSingerRBTCategoriesView()
        {

        }
        //-----------------------------------------------------------------
        ~SongSingerRBTCategoriesView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerRBTCategoryId
        {
            get { return _SongSingerRBTCategoryId; }
            set { _SongSingerRBTCategoryId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerRBTId
        {
            get { return _SongSingerRBTId; }
            set { _SongSingerRBTId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        //-----------------------------------------------------------------
        public string CategoryUrl
        {
            get { return _CategoryUrl; }
            set { _CategoryUrl = value; }
        }
        //-----------------------------------------------------------------

        public static List<SongSingerRBTCategoriesView> Init(SmartDataReader smartReader, bool hasCategoryName)
        {
            List<SongSingerRBTCategoriesView> l_SongSingerCategories = new List<SongSingerRBTCategoriesView>();
            try
            {
                while (smartReader.Read())
                {
                    SongSingerRBTCategoriesView m_SongSingerCategories = new SongSingerRBTCategoriesView();
                    m_SongSingerCategories.SongSingerRBTCategoryId = smartReader.GetInt32("SongSingerRBTCategoryId");
                    m_SongSingerCategories.SongSingerRBTId = smartReader.GetInt32("SongSingerRBTId");
                    m_SongSingerCategories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_SongSingerCategories.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    if (hasCategoryName)
                    {
                        m_SongSingerCategories.CategoryName = smartReader.GetString("CategoryName");
                        m_SongSingerCategories.CategoryUrl = smartReader.GetString("CategoryUrl");
                    }

                    l_SongSingerCategories.Add(m_SongSingerCategories);
                }
                return l_SongSingerCategories;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------
        public static List<SongSingerRBTCategoriesView> Static_GetListBySongSingerRBTId(int SongSingerRBTId, List<SongSingerRBTCategoriesView> list)
        {
            List<SongSingerRBTCategoriesView> RetVal = new List<SongSingerRBTCategoriesView>();
            try
            {
                RetVal = list.FindAll(i => i.SongSingerRBTId == SongSingerRBTId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

