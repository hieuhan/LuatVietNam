
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
    public class AlbumSingersView
    {
        private int _AlbumId;
        private int _SingerId;
        private string _SingerName;
        private string _ImagePath;
        private string _Url;
        //-----------------------------------------------------------------
        public AlbumSingersView()
        {
        }
        //-----------------------------------------------------------------        
        ~AlbumSingersView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public string SingerName
        {
            get { return _SingerName; }
            set { _SingerName = value; }
        }
        //-----------------------------------------------------------------
        public int AlbumId
        {
            get { return _AlbumId; }
            set { _AlbumId = value; }
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

        public static List<AlbumSingersView> Init(SmartDataReader smartReader, bool hasSingerName)
        {
            List<AlbumSingersView> list = new List<AlbumSingersView>();
            try
            {
                while (smartReader.Read())
                {
                    AlbumSingersView obj = new AlbumSingersView();
                    obj.AlbumId = smartReader.GetInt32("ArticleId");
                    obj.SingerId = smartReader.GetInt32("ArticleReferenceId");
                    if (hasSingerName)
                    {
                        obj.SingerName = smartReader.GetString("SingerName");
                        obj.ImagePath = smartReader.GetString("ImagePath");
                        obj.Url = smartReader.GetString("Url");
                    }

                    list.Add(obj);
                }
                return list;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-------------------------------------------------------------- 
        public static List<AlbumSingersView> Static_GetList(int AlbumId, List<AlbumSingersView> list)
        {
            List<AlbumSingersView> RetVal = new List<AlbumSingersView>();
            try
            {
                RetVal = list.FindAll(i => i.AlbumId == AlbumId);
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
    }
}

