
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;

namespace ICSoft.CMSViewLib
{
    public class AdvertsView
    {
        private int _AdvertId;
        private string _AdvertName;
        private string _AdvertDesc;
        private string _Url;
        private string _ImagePath;
        private string _ScriptContent;
        private short _SiteId;
        private string _Width;
        private string _Height;
        private DateTime _StartTime;
        private DateTime _EndTime;
        private short _PartnerId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private byte _AdvertContentTypeId;
        private byte _AdvertStatusId;
        //-----------------------------------------------------------------
        public AdvertsView()
        {
            
        }
        //-----------------------------------------------------------------
        ~AdvertsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int AdvertId
        {
            get { return _AdvertId; }
            set { _AdvertId = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertName
        {
            get { return _AdvertName; }
            set { _AdvertName = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertDesc
        {
            get { return _AdvertDesc; }
            set { _AdvertDesc = value; }
        }
        //-----------------------------------------------------------------
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        //-----------------------------------------------------------------
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        //-----------------------------------------------------------------
        public string ScriptContent
        {
            get { return _ScriptContent; }
            set { _ScriptContent = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public string Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        //-----------------------------------------------------------------
        public string Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        //-----------------------------------------------------------------
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        //-----------------------------------------------------------------
        public short PartnerId
        {
            get { return _PartnerId; }
            set { _PartnerId = value; }
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

        public byte AdvertContentTypeId
        {
            get { return _AdvertContentTypeId; }
            set { _AdvertContentTypeId = value; }
        }
        public byte AdvertStatusId
        {
            get { return _AdvertStatusId; }
            set { _AdvertStatusId = value; }
        }
        public static List<AdvertsView> Init(SmartDataReader smartReader)
        {
            List<AdvertsView> l_Adverts = new List<AdvertsView>();
            try
            {
                while (smartReader.Read())
                {
                    AdvertsView m_Adverts = new AdvertsView();
                    m_Adverts.AdvertId = smartReader.GetInt32("AdvertId");
                    m_Adverts.AdvertName = smartReader.GetString("AdvertName");
                    m_Adverts.AdvertDesc = smartReader.GetString("AdvertDesc");
                    m_Adverts.Url = smartReader.GetString("Url");
                    m_Adverts.ImagePath = smartReader.GetString("ImagePath");
                    m_Adverts.ScriptContent = smartReader.GetString("ScriptContent");
                    m_Adverts.SiteId = smartReader.GetInt16("SiteId");
                    m_Adverts.AdvertContentTypeId = smartReader.GetByte("AdvertContentTypeId");
                    m_Adverts.Width = smartReader.GetString("Width");
                    m_Adverts.Height = smartReader.GetString("Height");
                    m_Adverts.StartTime = smartReader.GetDateTime("StartTime");
                    m_Adverts.EndTime = smartReader.GetDateTime("EndTime");
                    m_Adverts.PartnerId = smartReader.GetInt16("PartnerId");
                    m_Adverts.AdvertStatusId = smartReader.GetByte("AdvertStatusId");
                    m_Adverts.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Adverts.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Adverts.Add(m_Adverts);
                }
                return l_Adverts;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
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
    }
}

