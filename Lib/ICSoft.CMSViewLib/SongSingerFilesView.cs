
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
    public class SongSingerFilesView
    {
        private int _SongSingerFileId;
        private int _SongSingerId;
        private byte _SongFileTypeId;
        private string _FilePath;
        private int _FileSize;
        private string _EmbedScript;
        private short _DataSourceId;
        private byte _MusicContentTypeId;
        private int _Price;
        private int _PromotionPrice;
        private byte _IsOfficial;
        private byte _ReviewStatusId;
        private int _ViewCount;
        private int _DownloadCount;
        private int _ShareCount;
        private int _LikeCount;
        private int _PostUserId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        //-----------------------------------------------------------------
        public SongSingerFilesView()
        {
        }
        //-----------------------------------------------------------------        
        ~SongSingerFilesView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerFileId
        {
            get { return _SongSingerFileId; }
            set { _SongSingerFileId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public byte SongFileTypeId
        {
            get { return _SongFileTypeId; }
            set { _SongFileTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        //-----------------------------------------------------------------
        public int FileSize
        {
            get { return _FileSize; }
            set { _FileSize = value; }
        }
        //-----------------------------------------------------------------
        public string EmbedScript
        {
            get { return _EmbedScript; }
            set { _EmbedScript = value; }
        }
        //-----------------------------------------------------------------
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------
        public byte MusicContentTypeId
        {
            get { return _MusicContentTypeId; }
            set { _MusicContentTypeId = value; }
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
        public byte IsOfficial
        {
            get { return _IsOfficial; }
            set { _IsOfficial = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
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
        public int LikeCount
        {
            get { return _LikeCount; }
            set { _LikeCount = value; }
        }
        //-----------------------------------------------------------------
        public int PostUserId
        {
            get { return _PostUserId; }
            set { _PostUserId = value; }
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

        public static List<SongSingerFilesView> Init(SmartDataReader smartReader)
        {
            List<SongSingerFilesView> l_SongSingerFiles = new List<SongSingerFilesView>();
            try
            {
                while (smartReader.Read())
                {
                    SongSingerFilesView m_SongSingerFiles = new SongSingerFilesView();
                    m_SongSingerFiles.SongSingerFileId = smartReader.GetInt32("SongSingerFileId");
                    m_SongSingerFiles.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerFiles.SongFileTypeId = smartReader.GetByte("SongFileTypeId");
                    m_SongSingerFiles.FilePath = smartReader.GetString("FilePath");
                    m_SongSingerFiles.FileSize = smartReader.GetInt32("FileSize");
                    m_SongSingerFiles.EmbedScript = smartReader.GetString("EmbedScript");
                    m_SongSingerFiles.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_SongSingerFiles.MusicContentTypeId = smartReader.GetByte("MusicContentTypeId");
                    m_SongSingerFiles.Price = smartReader.GetInt32("Price");
                    m_SongSingerFiles.PromotionPrice = smartReader.GetInt32("PromotionPrice");
                    m_SongSingerFiles.IsOfficial = smartReader.GetByte("IsOfficial");
                    m_SongSingerFiles.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_SongSingerFiles.ViewCount = smartReader.GetInt32("ViewCount");
                    m_SongSingerFiles.DownloadCount = smartReader.GetInt32("DownloadCount");
                    m_SongSingerFiles.ShareCount = smartReader.GetInt32("ShareCount");
                    m_SongSingerFiles.LikeCount = smartReader.GetInt32("LikeCount");
                    m_SongSingerFiles.PostUserId = smartReader.GetInt32("PostUserId");
                    m_SongSingerFiles.CrUserId = smartReader.GetInt32("CrUserId");
                    m_SongSingerFiles.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_SongSingerFiles.Add(m_SongSingerFiles);
                }
                return l_SongSingerFiles;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-------------------------------------------------------------- 
        public static List<SongSingerFilesView> Static_GetList(int SongSingerId, List<SongSingerFilesView> list)
        {
            List<SongSingerFilesView> RetVal = new List<SongSingerFilesView>();
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
        public static SongSingerFilesView Static_GetBySongFileTypeId(byte SongFileTypeId, List<SongSingerFilesView> list)
        {
            SongSingerFilesView RetVal = new SongSingerFilesView();
            try
            {
                if (list != null) RetVal = list.Find(i => i.SongFileTypeId == SongFileTypeId);
                if (RetVal == null) RetVal = new SongSingerFilesView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static SongSingerFilesView Static_Get(byte SongSingerFileId, List<SongSingerFilesView> list)
        {
            SongSingerFilesView RetVal = new SongSingerFilesView();
            try
            {
                RetVal = list.Find(i => i.SongSingerFileId == SongSingerFileId);
                if (RetVal == null) RetVal = new SongSingerFilesView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetFileUrl()
        {
            string RetVal = _FilePath;
            if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_MEDIADOMAIN + RetVal;
            return RetVal;
        }
        
    }
}

