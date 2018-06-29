
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
    public class SongsViewAlbumRBT
    {
        private AlbumsView _mAlbum;
        private List<SongsView> _lSong;
        private List<SongsView> _lVideo;
        private List<AlbumsView> _lAlbum;

        //-----------------------------------------------------------------
        public SongsViewAlbumRBT()
        {
        }
        //-----------------------------------------------------------------
        ~SongsViewAlbumRBT()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public AlbumsView mAlbum 
        {
            get { return _mAlbum; }
            set { _mAlbum = value; }
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
        public List<AlbumsView> lAlbum
        {
            get { return _lAlbum; }
            set { _lAlbum = value; }
        }
        //-----------------------------------------------------------------    
        
    }
}