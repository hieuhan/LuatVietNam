
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
    public class SongAuthorsView
    {
        private int _SongAuthorId;
        private int _SongId;
        private int _AuthorId;
        private string _AuthorName;
        private string _ImagePath;
        private string _Url;
        //-----------------------------------------------------------------
        public SongAuthorsView()
        {
        }
        //-----------------------------------------------------------------        
        ~SongAuthorsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongAuthorId
        {
            get { return _SongAuthorId; }
            set { _SongAuthorId = value; }
        }
        //-----------------------------------------------------------------
        public string AuthorName
        {
            get { return _AuthorName; }
            set { _AuthorName = value; }
        }
        //-----------------------------------------------------------------
        public int SongId
        {
            get { return _SongId; }
            set { _SongId = value; }
        }
        //-----------------------------------------------------------------
        public int AuthorId
        {
            get { return _AuthorId; }
            set { _AuthorId = value; }
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

        public static List<SongAuthorsView> Init(SmartDataReader smartReader, bool hasAuthorName)
        {
            List<SongAuthorsView> l_SongAuthors = new List<SongAuthorsView>();
            try
            {
                while (smartReader.Read())
                {
                    SongAuthorsView m_SongAuthors = new SongAuthorsView();
                    m_SongAuthors.SongAuthorId = smartReader.GetInt32("SongAuthorId");
                    m_SongAuthors.SongId = smartReader.GetInt32("SongId");
                    m_SongAuthors.AuthorId = smartReader.GetInt32("AuthorId");
                    if (hasAuthorName)
                    {
                        m_SongAuthors.AuthorName = smartReader.GetString("AuthorName");
                        m_SongAuthors.ImagePath = smartReader.GetString("ImagePath");
                        m_SongAuthors.Url = smartReader.GetString("Url");
                    }

                    l_SongAuthors.Add(m_SongAuthors);
                }
                return l_SongAuthors;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-------------------------------------------------------------- 
        public static List<SongAuthorsView> Static_GetList(int SongId, List<SongAuthorsView> list)
        {
            List<SongAuthorsView> RetVal = new List<SongAuthorsView>();
            try
            {
                RetVal = list.FindAll(i => i.SongId == SongId);
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
            if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
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
        //-------------------------------------------------------------- 
    }
}

