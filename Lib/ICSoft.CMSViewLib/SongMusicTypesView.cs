
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
    public class SongMusicTypesView
    {
        private int _SongMusicTypeId;
        private int _SongId;
        private short _MusicTypeId;
        private string _MusicTypeName;
        private string _Url;
        //-----------------------------------------------------------------
        public SongMusicTypesView()
        {
        }
        //-----------------------------------------------------------------        
        ~SongMusicTypesView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongMusicTypeId
        {
            get { return _SongMusicTypeId; }
            set { _SongMusicTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string MusicTypeName
        {
            get { return _MusicTypeName; }
            set { _MusicTypeName = value; }
        }
        //-----------------------------------------------------------------
        public int SongId
        {
            get { return _SongId; }
            set { _SongId = value; }
        }
        //-----------------------------------------------------------------
        public short MusicTypeId
        {
            get { return _MusicTypeId; }
            set { _MusicTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        //-----------------------------------------------------------------
        public static List<SongMusicTypesView> Init(SmartDataReader smartReader, bool hasMusicTypeName)
        {
            List<SongMusicTypesView> l_SongMusicTypes = new List<SongMusicTypesView>();
            try
            {
                while (smartReader.Read())
                {
                    SongMusicTypesView m_SongMusicTypes = new SongMusicTypesView();
                    m_SongMusicTypes.SongMusicTypeId = smartReader.GetInt32("SongMusicTypeId");
                    m_SongMusicTypes.SongId = smartReader.GetInt32("SongId");
                    m_SongMusicTypes.MusicTypeId = smartReader.GetInt16("MusicTypeId");
                    if (hasMusicTypeName)
                    {
                        m_SongMusicTypes.MusicTypeName = smartReader.GetString("MusicTypeName");
                        m_SongMusicTypes.Url = smartReader.GetString("Url");
                    }
                    l_SongMusicTypes.Add(m_SongMusicTypes);
                }
                return l_SongMusicTypes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data error: " + ex.Message);
            }
        }
        //-------------------------------------------------------------- 
        public static List<SongMusicTypesView> Static_GetList(int SongId, List<SongMusicTypesView> list)
        {
            List<SongMusicTypesView> RetVal = new List<SongMusicTypesView>();
            try
            {
                RetVal = list.FindAll(i => i.SongId == SongId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal == null ? new List<SongMusicTypesView>() : RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetUrl()
        {
            string RetVal = _Url;
            if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }
        //--------------------------------------------------------------     
    }
}

