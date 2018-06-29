
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
    public class SongSingerAlbumsView
    {
        private int _SongSingerAlbumId;
        private int _SongSingerId;
        private int _AlbumId;
        private int _DisplayOrder;
        private string _AlbumName;
        private string _ImagePath;
        private string _Url;
        //-----------------------------------------------------------------
        public SongSingerAlbumsView()
        {
        }
        //-----------------------------------------------------------------        
        ~SongSingerAlbumsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerAlbumId
        {
            get { return _SongSingerAlbumId; }
            set { _SongSingerAlbumId = value; }
        }
        //-----------------------------------------------------------------
        public string AlbumName
        {
            get { return _AlbumName; }
            set { _AlbumName = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public int AlbumId
        {
            get { return _AlbumId; }
            set { _AlbumId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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

        public static List<SongSingerAlbumsView> Init(SmartDataReader smartReader, bool hasAlbumName)
        {
            List<SongSingerAlbumsView> l_SongSingerAlbums = new List<SongSingerAlbumsView>();
            try
            {
                while (smartReader.Read())
                {
                    SongSingerAlbumsView m_SongSingerAlbums = new SongSingerAlbumsView();
                    m_SongSingerAlbums.SongSingerAlbumId = smartReader.GetInt32("SongSingerAlbumId");
                    m_SongSingerAlbums.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerAlbums.AlbumId = smartReader.GetInt32("AlbumId");
                    m_SongSingerAlbums.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    if (hasAlbumName)
                    {
                        m_SongSingerAlbums.AlbumName = smartReader.GetString("AlbumName");
                        m_SongSingerAlbums.ImagePath = smartReader.GetString("ImagePath");
                        m_SongSingerAlbums.Url = smartReader.GetString("Url");
                    }

                    l_SongSingerAlbums.Add(m_SongSingerAlbums);
                }
                return l_SongSingerAlbums;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-------------------------------------------------------------- 
        public static List<SongSingerAlbumsView> Static_GetList(int SongSingerId, List<SongSingerAlbumsView> list)
        {
            List<SongSingerAlbumsView> RetVal = new List<SongSingerAlbumsView>();
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

