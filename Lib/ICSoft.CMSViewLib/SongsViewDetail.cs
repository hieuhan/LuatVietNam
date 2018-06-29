
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
    public class SongsViewDetail
    {
        private SongsView _mSongsView;
        private List<ArticlesView> _lSinger;
        private List<SongsView> _lSongOther;
        private List<SongsView> _lVideoOther;
        private List<ArticlesView> _lAlbum;

        //-----------------------------------------------------------------
        public SongsViewDetail()
        {
        }
        //-----------------------------------------------------------------
        ~SongsViewDetail()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public SongsView mSongsView 
        {
            get { return _mSongsView; }
            set { _mSongsView = value; }
        }
        //-----------------------------------------------------------------    
        public List<ArticlesView> lSinger
        {
            get { return _lSinger; }
            set { _lSinger = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongsView> lSongOther
        {
            get { return _lSongOther; }
            set { _lSongOther = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongsView> lVideoOther
        {
            get { return _lVideoOther; }
            set { _lVideoOther = value; }
        }
        //-----------------------------------------------------------------    
        public List<ArticlesView> lAlbum
        {
            get { return _lAlbum; }
            set { _lAlbum = value; }
        }
        //-----------------------------------------------------------------    
        
    }
}