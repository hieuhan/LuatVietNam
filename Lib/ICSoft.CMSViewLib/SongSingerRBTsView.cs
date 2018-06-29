
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
    public class SongSingerRBTsView
    {
        private int _SongSingerRBTId;
        private int _SongSingerId;
        private byte _TelcoId;
        private string _RBTName;
        private string _RBTCode;
        private int _Price;
        private int _PromotionPrice;
        private DateTime _ExpireDateTime;
        private string _FilePath="";
        private byte _MusicContentTypeId;
        private short _DataSourceId;
        private byte _ReviewStatusId;
        private int _ViewCount;
        private int _DownloadCount;
        private int _ShareCount;
        private int _LikeCount;
        private int _CrUserId;
        private DateTime _CrDateTime;
        //-----------------------------------------------------------------
        public SongSingerRBTsView()
        {
        }
        //-----------------------------------------------------------------        
        ~SongSingerRBTsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerRBTId
        {
            get { return _SongSingerRBTId; }
            set { _SongSingerRBTId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
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
        public static List<SongSingerRBTsView> Init(SmartDataReader smartReader)
        {
            return Init(smartReader, null);
        }
        //-----------------------------------------------------------------
        public static List<SongSingerRBTsView> Init(SmartDataReader smartReader, List<SongsView> l_SongsView)
        {
            List<SongSingerRBTsView> l_SongSingerRBTs = new List<SongSingerRBTsView>();
            try
            {
                while (smartReader.Read())
                {
                    SongSingerRBTsView m_SongSingerRBTs = new SongSingerRBTsView();
                    m_SongSingerRBTs.SongSingerRBTId = smartReader.GetInt32("SongSingerRBTId");
                    m_SongSingerRBTs.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerRBTs.TelcoId = smartReader.GetByte("TelcoId");
                    m_SongSingerRBTs.RBTName = smartReader.GetString("RBTName");
                    if (string.IsNullOrEmpty(m_SongSingerRBTs.RBTName) && l_SongsView != null) m_SongSingerRBTs.RBTName = SongsView.Static_GetBySongSingerId(smartReader.GetInt32("SongSingerId"), l_SongsView).SongDesc;
                    m_SongSingerRBTs.RBTCode = smartReader.GetString("RBTCode");
                    m_SongSingerRBTs.Price = smartReader.GetInt32("Price");
                    m_SongSingerRBTs.PromotionPrice = smartReader.GetInt32("PromotionPrice");
                    m_SongSingerRBTs.ExpireDateTime = smartReader.GetDateTime("ExpireDateTime");
                    m_SongSingerRBTs.FilePath = smartReader.GetString("FilePath");
                    m_SongSingerRBTs.MusicContentTypeId = smartReader.GetByte("MusicContentTypeId");
                    m_SongSingerRBTs.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_SongSingerRBTs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_SongSingerRBTs.ViewCount = smartReader.GetInt32("ViewCount");
                    m_SongSingerRBTs.DownloadCount = smartReader.GetInt32("DownloadCount");
                    m_SongSingerRBTs.ShareCount = smartReader.GetInt32("ShareCount");
                    m_SongSingerRBTs.LikeCount = smartReader.GetInt32("LikeCount");
                    m_SongSingerRBTs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_SongSingerRBTs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_SongSingerRBTs.Add(m_SongSingerRBTs);
                }
                return l_SongSingerRBTs;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-------------------------------------------------------------- 
        public static List<SongSingerRBTsView> Static_GetList(int SongSingerId, List<SongSingerRBTsView> list)
        {
            List<SongSingerRBTsView> RetVal = new List<SongSingerRBTsView>();
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
        public static List<SongSingerRBTsView> Static_GetListByTelcoId(byte TelcoId, List<SongSingerRBTsView> list)
        {
            List<SongSingerRBTsView> RetVal = new List<SongSingerRBTsView>();
            try
            {
                RetVal = list.FindAll(i => i.TelcoId == TelcoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SongSingerRBTsView> Static_GetListByDataSourceId(short DataSourceId, List<SongSingerRBTsView> list)
        {
            List<SongSingerRBTsView> RetVal = new List<SongSingerRBTsView>();
            try
            {
                RetVal = list.FindAll(i => i.DataSourceId == DataSourceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SongSingerRBTsView> Static_GetListByTelcoIdAndDataSourceId(byte TelcoId, short DataSourceId, List<SongSingerRBTsView> list)
        {
            List<SongSingerRBTsView> RetVal = new List<SongSingerRBTsView>();
            try
            {
                RetVal = list.FindAll(i => i.TelcoId == TelcoId && i.DataSourceId == DataSourceId);
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
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_MEDIADOMAIN + RetVal;
            return RetVal;
        }
        
    }
}

