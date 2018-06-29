
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
    public class SongSingerCategoriesView
    {
        private int _SongSingerCategoryId;
        private int _SongSingerId;
        private short _CategoryId;
        private int _DisplayOrder;
        private string _CategoryName;
        private string _CategoryUrl;
        //-----------------------------------------------------------------
        public SongSingerCategoriesView()
        {

        }
        //-----------------------------------------------------------------
        ~SongSingerCategoriesView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerCategoryId
        {
            get { return _SongSingerCategoryId; }
            set { _SongSingerCategoryId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
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

        public static List<SongSingerCategoriesView> Init(SmartDataReader smartReader, bool hasCategoryName)
        {
            List<SongSingerCategoriesView> l_SongSingerCategories = new List<SongSingerCategoriesView>();
            try
            {
                while (smartReader.Read())
                {
                    SongSingerCategoriesView m_SongSingerCategories = new SongSingerCategoriesView();
                    m_SongSingerCategories.SongSingerCategoryId = smartReader.GetInt32("SongSingerCategoryId");
                    m_SongSingerCategories.SongSingerId = smartReader.GetInt32("SongSingerId");
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
        public static List<SongSingerCategoriesView> Static_GetListBySongSingerId(int SongSingerId, List<SongSingerCategoriesView> list)
        {
            List<SongSingerCategoriesView> RetVal = new List<SongSingerCategoriesView>();
            try
            {
                RetVal = list.FindAll(i => i.SongSingerId == SongSingerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

