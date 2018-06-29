
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Data;
using ICSoft.CMSLib;
using System.Text.RegularExpressions;

namespace ICSoft.CMSViewLib
{
    public class SongsView
    {
        private int _SongSingerId;
        private string _SongSingerName;
        private int _SongId;
        private string _SongName;
        private string _SongDesc;
        private string _Lyric;
        private short _CountryId;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _ViewCount;
        private int _DownloadCount;
        private int _ShareCount;
        private int _LikeCount;

        private int _SongSingerRBTId;
        private byte _TelcoId;
        private string _RBTName;
        private string _RBTCode;
        private int _Price;
        private int _PromotionPrice;
        private DateTime _ExpireDateTime;
        private string _FilePath = "";
        private byte _MusicContentTypeId;
        private short _DataSourceId;
        private byte _ViewType;
        public byte SongSingersStatusId { get; set; }
        public byte IsHot { get; set; }
        public byte IsVideoHot { get; set; }
        public byte VideoStatusId { get; set; }
        public DateTime PromotionEndDate { get; set; }
        private List<SongMusicTypesView> _lMusicTypes = new List<SongMusicTypesView>();
        private List<SongAuthorsView> _lAuthors = new List<SongAuthorsView>();
        private List<SongSingerSingersView> _lSingers = new List<SongSingerSingersView>();
        private List<SongSingerAlbumsView> _lAlbums = new List<SongSingerAlbumsView>();
        private List<SongSingerFilesView> _lFiles = new List<SongSingerFilesView>();
        private List<SongSingerRBTsView> _lRBTs = new List<SongSingerRBTsView>();
        private List<SongSingerCategoriesView> _lCategory = new List<SongSingerCategoriesView>();
        private List<SongSingerRBTCategoriesView> _lCategoryRBT = new List<SongSingerRBTCategoriesView>();
        //-----------------------------------------------------------------
        public SongsView()
        {
        }
        //-----------------------------------------------------------------        
        ~SongsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public List<SongMusicTypesView> lMusicTypes
        {
            get { return _lMusicTypes; }
            set { _lMusicTypes = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongSingerCategoriesView> lCategory
        {
            get { return _lCategory; }
            set { _lCategory = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongSingerRBTCategoriesView> lCategoryRBT
        {
            get { return _lCategoryRBT; }
            set { _lCategoryRBT = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongAuthorsView> lAuthors
        {
            get { return _lAuthors; }
            set { _lAuthors = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongSingerSingersView> lSingers
        {
            get { return _lSingers; }
            set { _lSingers = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongSingerAlbumsView> lAlbums
        {
            get { return _lAlbums; }
            set { _lAlbums = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongSingerFilesView> lFiles
        {
            get { return _lFiles; }
            set { _lFiles = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongSingerRBTsView> lRBTs
        {
            get { return _lRBTs; }
            set { _lRBTs = value; }
        }
        //-----------------------------------------------------------------    
        public int SongId
        {
            get { return _SongId; }
            set { _SongId = value; }
        }
        //-----------------------------------------------------------------    
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public string SongSingerName
        {
            get { return _SongSingerName; }
            set { _SongSingerName = value; }
        }
        //-----------------------------------------------------------------
        public string SongName
        {
            get { return _SongName; }
            set { _SongName = value; }
        }
        //-----------------------------------------------------------------
        public string SongDesc
        {
            get { return _SongDesc; }
            set { _SongDesc = value; }
        }
        //-----------------------------------------------------------------
        public string Lyric
        {
            get { return _Lyric; }
            set { _Lyric = value; }
        }
        //-----------------------------------------------------------------
        public short CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
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
        public int ViewCount
        {
            get { return _ViewCount; }
            set { _ViewCount = value; }
        }
        //-----------------------------------------------------------------
        public int DownloadCount
        {
            get { return _DownloadCount; }
            set { _DownloadCount = value; }
        }
        //-----------------------------------------------------------------
        public int ShareCount
        {
            get { return _ShareCount; }
            set { _ShareCount = value; }
        }
        //-----------------------------------------------------------------    
        public int SongSingerRBTId
        {
            get { return _SongSingerRBTId; }
            set { _SongSingerRBTId = value; }
        }
        //-----------------------------------------------------------------
        public byte TelcoId
        {
            get { return _TelcoId; }
            set { _TelcoId = value; }
        }
        //-----------------------------------------------------------------
        public string RBTName
        {
            get { return _RBTName; }
            set { _RBTName = value; }
        }
        //-----------------------------------------------------------------
        public string RBTCode
        {
            get { return _RBTCode; }
            set { _RBTCode = value; }
        }
        //-----------------------------------------------------------------
        public int Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        //-----------------------------------------------------------------
        public int PromotionPrice
        {
            get { return _PromotionPrice; }
            set { _PromotionPrice = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ExpireDateTime
        {
            get { return _ExpireDateTime; }
            set { _ExpireDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        //-----------------------------------------------------------------
        public byte MusicContentTypeId
        {
            get { return _MusicContentTypeId; }
            set { _MusicContentTypeId = value; }
        }
        //-----------------------------------------------------------------
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------
        public int LikeCount
        {
            get { return _LikeCount; }
            set { _LikeCount = value; }
        }
        //-----------------------------------------------------------------
        public byte ViewType
        {
            get { return _ViewType; }
            set { _ViewType = value; }
        }
        //-----------------------------------------------------------------
        public static List<SongsView> Init(SmartDataReader smartReader, byte GetSongSinger, byte GetSongSingerRBT)
        {
            List<SongsView> l_Songs = new List<SongsView>();
            try
            {
                while (smartReader.Read())
                {
                    SongsView m_Songs = new SongsView();
                    m_Songs.SongId = smartReader.GetInt32("SongId");
                    m_Songs.SongName = smartReader.GetString("SongName");
                    m_Songs.SongDesc = smartReader.GetString("SongDesc");
                    m_Songs.Lyric = smartReader.GetString("Lyric");
                    m_Songs.CountryId = smartReader.GetInt16("CountryId");
                    m_Songs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Songs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Songs.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Songs.ViewType = smartReader.GetByte("ViewType");
                    if (GetSongSinger == 1)
                    {
                        m_Songs.SongSingerId = smartReader.GetInt32("SongSingerId");
                        m_Songs.SongSingerName = smartReader.GetString("SongSingerName");
                        m_Songs.ViewCount = smartReader.GetInt32("ViewCount");
                        m_Songs.DownloadCount = smartReader.GetInt32("DownloadCount");
                        m_Songs.ShareCount = smartReader.GetInt32("ShareCount");
                        m_Songs.LikeCount = smartReader.GetInt32("LikeCount");
                        m_Songs.IsHot = smartReader.GetByte("IsHot");
                        m_Songs.IsVideoHot = smartReader.GetByte("IsVideoHot");
                        m_Songs.VideoStatusId = smartReader.GetByte("VideoStatusId");
                    }
                    if (GetSongSingerRBT == 2)
                    {
                        m_Songs.SongSingerRBTId = smartReader.GetInt32("SongSingerRBTId");
                        m_Songs.RBTName = smartReader.GetString("RBTName");
                        if (string.IsNullOrEmpty(m_Songs.RBTName)) m_Songs.RBTName = m_Songs.SongDesc;
                        m_Songs.RBTCode = smartReader.GetString("RBTCode");
                        m_Songs.Price = smartReader.GetInt32("Price");
                        m_Songs.PromotionPrice = smartReader.GetInt32("PromotionPrice");
                        m_Songs.DataSourceId = smartReader.GetInt16("DataSourceId");
                        m_Songs.TelcoId = smartReader.GetByte("TelcoId");
                        m_Songs.FilePath = smartReader.GetString("FilePath");
                        m_Songs.PromotionEndDate = smartReader.GetDateTime("PromotionEndDate");
                        m_Songs.ExpireDateTime = smartReader.GetDateTime("ExpireDateTime");
                        m_Songs.SongSingersStatusId = smartReader.GetByte("SongSingersStatusId");
                    }

                    l_Songs.Add(m_Songs);
                }
                return l_Songs;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------
        public static List<SongsView> Static_GetListByViewType(byte ViewType, List<SongsView> list)
        {
            List<SongsView> RetVal = new List<SongsView>();
            try
            {
                RetVal = list.FindAll(i => i.ViewType == ViewType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static SongsView Static_GetByViewType(byte ViewType, List<SongsView> list)
        {
            SongsView RetVal = new SongsView();
            try
            {
                if (list != null) RetVal = list.Find(i => i.ViewType == ViewType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static SongsView Static_GetBySongSingerId(int SongSingerId, List<SongsView> list)
        {
            SongsView RetVal = new SongsView();
            try
            {
                RetVal = list.Find(i => i.SongSingerId == SongSingerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public string GetLyric()
        {
            return _Lyric.Replace("\r\n", "<br/>");
        }
        //-----------------------------------------------------------
        public string GetMusicTypeName()
        {
            string RetVal = "";
            if (_lMusicTypes != null)
            {
                for (int i = 0; i < _lMusicTypes.Count; i++)
                {
                    if (RetVal != "") RetVal = RetVal + ", ";
                    RetVal = RetVal + _lMusicTypes[i].MusicTypeName;
                }
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public string GetAuthorName()
        {
            string RetVal = "";
            if (_lAuthors != null)
            {
                for (int i = 0; i < _lAuthors.Count; i++)
                {
                    if (RetVal != "") RetVal = RetVal + ", ";
                    RetVal = RetVal + _lAuthors[i].AuthorName;
                }
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public string GetSingerName()
        {
            string RetVal = "";
            if (_lSingers != null)
            {
                for (int i = 0; i < _lSingers.Count; i++)
                {
                    if (RetVal != "") RetVal = RetVal + ", ";
                    RetVal = RetVal + _lSingers[i].SingerName;
                }
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public string GetAlbumName()
        {
            string RetVal = "";
            if (_lAlbums != null)
            {
                for (int i = 0; i < _lAlbums.Count; i++)
                {
                    if (RetVal != "") RetVal = RetVal + ", ";
                    RetVal = RetVal + _lAlbums[i].AlbumName;
                }
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public string GetCategoryName()
        {
            string RetVal = "";
            if (_lCategory != null)
            {
                for (int i = 0; i < _lCategory.Count; i++)
                {
                    if (RetVal != "") RetVal = RetVal + ", ";
                    RetVal = RetVal + _lCategory[i].CategoryName;
                }
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public bool CheckExistCategoryId(short CategoryId)
        {
            bool RetVal = false;
            if (_lCategory != null)
            {
                for (int i = 0; i < _lCategory.Count; i++)
                {
                    if (_lCategory[i].CategoryId == CategoryId)
                    {
                        RetVal = true;
                        break;
                    }
                }
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public string GetCategoryName_RBT()
        {
            string RetVal = "";
            if (_lCategoryRBT != null)
            {
                for (int i = 0; i < _lCategoryRBT.Count; i++)
                {
                    if (RetVal != "") RetVal = RetVal + ", ";
                    RetVal = RetVal + _lCategoryRBT[i].CategoryName;
                }
            }
            return RetVal;
        }
        private string RemoveCharInUrl(string Url)
        {
            return Regex.Replace(Url, "[^0-9a-zA-Z-_.]+", "");
        }
        //-------------------------------------------------------------- 
        public string GetUrl()
        {
            string RetVal = _SongName.ToLower().Replace(" ", "-") + "-" + _SongSingerId.ToString() + "-songs.html";
            RetVal = RemoveCharInUrl(RetVal);
            RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetVideoUrl()
        {
            string RetVal = _SongName.ToLower().Replace(" ", "-") + "-" + _SongSingerId.ToString() + "-videos.html";
            RetVal = RemoveCharInUrl(RetVal);
            RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetRBTUrl()
        {
            string RetVal = _SongName.ToLower().Replace(" ", "-") + "-" + _SongSingerRBTId.ToString() + "-rbt.html";
            RetVal = RemoveCharInUrl(RetVal);
            RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetMp3FullUrl()
        {
            string RetVal = "";
            SongSingerFilesView m_SongSingerFilesView = SongSingerFilesView.Static_GetBySongFileTypeId(1, _lFiles);
            if (m_SongSingerFilesView == null)
            {
                m_SongSingerFilesView = SongSingerFilesView.Static_GetBySongFileTypeId(99, _lFiles); //Xem co file nhac cho ko
            }
            if (m_SongSingerFilesView != null)
            {
                RetVal = m_SongSingerFilesView.FilePath;
            }
            if (string.IsNullOrEmpty(RetVal) && _lRBTs != null)
            {
                if (_lRBTs.Count > 0) RetVal = _lRBTs[0].FilePath;
            }
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_MEDIADOMAIN + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetRBTFileUrl()
        {
            string RetVal = "";
            if (!string.IsNullOrEmpty(FilePath))
            {
                RetVal = FilePath;
            }
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_MEDIADOMAIN + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetMp4Url()
        {
            string RetVal = "";
            if (_lFiles != null)
            {
                SongSingerFilesView m_SongSingerFilesView = SongSingerFilesView.Static_GetBySongFileTypeId(2, _lFiles);
                if (m_SongSingerFilesView != null)
                {
                    RetVal = m_SongSingerFilesView.FilePath;
                    if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_MEDIADOMAIN + RetVal;
                }
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetMp4EmbedScript()
        {
            string RetVal = "";
            if (_lFiles != null)
            {
                SongSingerFilesView m_SongSingerFilesView = SongSingerFilesView.Static_GetBySongFileTypeId(2, _lFiles);
                if (m_SongSingerFilesView != null)
                {
                    RetVal = m_SongSingerFilesView.EmbedScript;
                }
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl()
        {
            string RetVal = "";
            SongSingerFilesView m_SongSingerFilesView = SongSingerFilesView.Static_GetBySongFileTypeId(3, _lFiles);
            if (m_SongSingerFilesView != null)
            {
                RetVal = m_SongSingerFilesView.FilePath;
                if (string.IsNullOrEmpty(RetVal))
                {
                    //Lay anh ca sy
                    if (lSingers != null)
                    {
                        if (lSingers.Count>0) RetVal = lSingers[0].GetImageUrl();
                    }
                    if (string.IsNullOrEmpty(RetVal)) RetVal = CmsConstants.NO_IMAGE_URL;
                }
                if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_MEDIADOMAIN + RetVal;
            }
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

