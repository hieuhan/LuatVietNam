
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
    public class SongsViewSinger
    {
        private ArticlesView _mSinger;
        private List<SongsView> _lSong;
        private List<SongsView> _lVideo;
        private List<ArticlesView> _lAlbum;

        //-----------------------------------------------------------------
        public SongsViewSinger()
        {
        }
        //-----------------------------------------------------------------
        ~SongsViewSinger()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public ArticlesView mSinger 
        {
            get { return _mSinger; }
            set { _mSinger = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongsView> lSong
        {
            get { return _lSong; }
            set { _lSong = value; }
        }
        //-----------------------------------------------------------------    
        public List<SongsView> lVideo
        {
            get { return _lVideo; }
            set { _lVideo = value; }
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