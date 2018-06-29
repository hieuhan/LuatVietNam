
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Data;
using ICSoft.CMSLib;

namespace ICSoft.CMSViewLib
{
    public class SongSingerSingersView
    {
        private int _SongSingerSingerId;
        private int _SongSingerId;
        private int _SingerId;
        private string _SingerName;
        private string _ImagePath;
        private string _Url;
        //-----------------------------------------------------------------
        public SongSingerSingersView()
        {
        }
        //-----------------------------------------------------------------        
        ~SongSingerSingersView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerSingerId
        {
            get { return _SongSingerSingerId; }
            set { _SongSingerSingerId = value; }
        }
        //-----------------------------------------------------------------
        public string SingerName
        {
            get { return _SingerName; }
            set { _SingerName = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public int SingerId
        {
            get { return _SingerId; }
            set { _SingerId = value; }
        }
        //-----------------------------------------------------------------
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        //-----------------------------------------------------------------
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        //-----------------------------------------------------------------

        public static List<SongSingerSingersView> Init(SmartDataReader smartReader, bool hasSingerName)
        {
            List<SongSingerSingersView> l_SongSingerSingers = new List<SongSingerSingersView>();
            try
            {
                while (smartReader.Read())
                {
                    SongSingerSingersView m_SongSingerSingers = new SongSingerSingersView();
                    m_SongSingerSingers.SongSingerSingerId = smartReader.GetInt32("SongSingerSingerId");
                    m_SongSingerSingers.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerSingers.SingerId = smartReader.GetInt32("SingerId");
                    if (hasSingerName)
                    {
                        m_SongSingerSingers.SingerName = smartReader.GetString("SingerName");
                        m_SongSingerSingers.ImagePath = smartReader.GetString("ImagePath");
                        m_SongSingerSingers.Url = smartReader.GetString("Url");
                    }

                    l_SongSingerSingers.Add(m_SongSingerSingers);
                }
                return l_SongSingerSingers;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-------------------------------------------------------------- 
        public static List<SongSingerSingersView> Static_GetList(int SongSingerId, List<SongSingerSingersView> list)
        {
            List<SongSingerSingersView> RetVal = new List<SongSingerSingersView>();
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
        //-------------------------------------------------------------- 
        public string GetUrl()
        {
            string RetVal = _Url;
            if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl()
        {
            string RetVal = _ImagePath;
            if (string.IsNullOrEmpty(_ImagePath))
            {
                RetVal = CmsConstants.NO_IMAGE_URL;
            }
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
            return RetVal;
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Icon()
        {
            return GetImageUrl().Replace("/Original/", "/Icon/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Thumb()
        {
            return GetImageUrl().Replace("/Original/", "/Thumb/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Standard()
        {
            return GetImageUrl().Replace("/Original/", "/Standard/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Mobile()
        {
            return GetImageUrl().Replace("/Original/", "/Mobile/");
        }
    }
}

