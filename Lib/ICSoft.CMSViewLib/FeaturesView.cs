
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
    public class FeaturesView
    {   
	    private short _FeatureId;
        private short _FeatureGroupId;
	    private string _FeatureName;
	    private string _FeatureDesc;
	    private short _ParentFeatureId;
	    private string _IconPath;
	    private byte _DisplayOrder;
	    private byte _IsData;
	    private byte _IsSearch;
	    private byte _IsDisplay;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _InputTypeId;
	    private short _DataDictionaryTypeId;
        private string _FeatureValue;
        public int ArticleId { get; set; }
        public string FeatureValueDisplay { get; set; }
        //-----------------------------------------------------------------
        public FeaturesView()
        {
		}
        //-----------------------------------------------------------------
        ~FeaturesView()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short FeatureId
        {
		    get { return _FeatureId; }
		    set { _FeatureId = value; }
	    }
        
        //-----------------------------------------------------------------
        public short FeatureGroupId
		{
            get { return _FeatureGroupId; }
            set { _FeatureGroupId = value; }
		}    
        //-----------------------------------------------------------------
        public string FeatureName
		{
            get { return _FeatureName; }
		    set { _FeatureName = value; }
		}    
        //-----------------------------------------------------------------
        public string FeatureDesc
		{
            get { return _FeatureDesc; }
		    set { _FeatureDesc = value; }
		}    
        //-----------------------------------------------------------------
        public short ParentFeatureId
		{
            get { return _ParentFeatureId; }
		    set { _ParentFeatureId = value; }
		}    
        //-----------------------------------------------------------------
        public string IconPath
		{
            get { return _IconPath; }
		    set { _IconPath = value; }
		}    
        //-----------------------------------------------------------------
        public byte DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
		}    
        //-----------------------------------------------------------------
        public byte IsData
		{
            get { return _IsData; }
		    set { _IsData = value; }
		}    
        //-----------------------------------------------------------------
        public byte IsSearch
		{
            get { return _IsSearch; }
		    set { _IsSearch = value; }
		}    
        //-----------------------------------------------------------------
        public byte IsDisplay
		{
            get { return _IsDisplay; }
		    set { _IsDisplay = value; }
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
    
	    public byte InputTypeId
        {
		    get { return _InputTypeId; }
		    set { _InputTypeId = value; }
	    }
	    public short DataDictionaryTypeId
        {
		    get { return _DataDictionaryTypeId; }
		    set { _DataDictionaryTypeId = value; }
	    }
        //-----------------------------------------------------------------
        public string FeatureValue
        {
            get { return _FeatureValue; }
            set { _FeatureValue = value; }
        }
        //-----------------------------------------------------------------
        public static List<FeaturesView> Init(SmartDataReader smartReader, bool hasFeatureValue)
        {
            List<FeaturesView> l_Features = new List<FeaturesView>();
            try
            {
                while (smartReader.Read())
                {
                    FeaturesView m_Features = new FeaturesView();
                    m_Features.FeatureId = smartReader.GetInt16("FeatureId");
                    m_Features.FeatureGroupId = smartReader.GetInt16("FeatureGroupId");
                    m_Features.FeatureName = smartReader.GetString("FeatureName");
                    m_Features.FeatureDesc = smartReader.GetString("FeatureDesc");
                    m_Features.ParentFeatureId = smartReader.GetInt16("ParentFeatureId");
                    m_Features.InputTypeId = smartReader.GetByte("InputTypeId");
                    m_Features.DataDictionaryTypeId = smartReader.GetInt16("DataDictionaryTypeId");
                    m_Features.IconPath = smartReader.GetString("IconPath");
                    m_Features.DisplayOrder = smartReader.GetByte("DisplayOrder");
                    m_Features.IsData = smartReader.GetByte("IsData");
                    m_Features.IsSearch = smartReader.GetByte("IsSearch");
                    m_Features.IsDisplay = smartReader.GetByte("IsDisplay");
                    m_Features.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Features.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    if (hasFeatureValue)
                    {
                        m_Features.ArticleId = smartReader.GetInt32("ArticleId");
                        m_Features.FeatureValue = smartReader.GetString("FeatureValue");
                        m_Features.FeatureValueDisplay = smartReader.GetString("FeatureValue");
                        if (m_Features.DataDictionaryTypeId > 0) m_Features.FeatureValueDisplay = smartReader.GetString("DataDictionary");
                    }

                    l_Features.Add(m_Features);
                }
                return l_Features;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------------
        public static FeaturesView Static_Get(short FeatureId, List<FeaturesView> list)
        {
            FeaturesView RetVal = new FeaturesView();
            try
            {
                RetVal = list.Find(i => i.FeatureId == FeatureId);
                if (RetVal == null) RetVal = new FeaturesView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public static List<FeaturesView> Static_GetListByArticleId(int ArticleId, List<FeaturesView> list, List<FeaturesView> listDanhMuc)
        {
            List<FeaturesView> RetVal = new List<FeaturesView>();
            try
            {
                RetVal = list.FindAll(i => i.ArticleId == ArticleId);
                if (RetVal == null) RetVal = new List<FeaturesView>();
                for (int i = 0; i < listDanhMuc.Count; i++)
                {
                    if (RetVal.Find(j => j.FeatureId == listDanhMuc[i].FeatureId) == null)
                    {
                        RetVal.Add(listDanhMuc[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public static List<FeaturesView> Static_GetListByIsSearch(List<FeaturesView> list)
        {
            List<FeaturesView> RetVal = new List<FeaturesView>();
            try
            {
                RetVal = list.FindAll(i => i.IsSearch == 1);
                if (RetVal == null) RetVal = new List<FeaturesView>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public static List<FeaturesView> Static_GetListByIsDisplay(List<FeaturesView> list)
        {
            List<FeaturesView> RetVal = new List<FeaturesView>();
            try
            {
                RetVal = list.FindAll(i => i.IsDisplay == 1);
                if (RetVal == null) RetVal = new List<FeaturesView>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    } 
}

