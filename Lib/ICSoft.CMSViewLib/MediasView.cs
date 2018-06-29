
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
    public class MediasView
    {
        private int _MediaId;
        private byte _MediaTypeId;
        private short _MediaGroupId;
        private string _MediaName;
        private string _MediaDesc;
        private string _FilePath;
        private int _FileSize;
        private int _CrUserId;
        private DateTime _CrDateTime;
        //-----------------------------------------------------------------
        public MediasView()
        {
        }
        //-----------------------------------------------------------------        
        ~MediasView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MediaId
        {
            get { return _MediaId; }
            set { _MediaId = value; }
        }
        //-----------------------------------------------------------------
        public byte MediaTypeId
        {
            get { return _MediaTypeId; }
            set { _MediaTypeId = value; }
        }
        //-----------------------------------------------------------------
        public short MediaGroupId
        {
            get { return _MediaGroupId; }
            set { _MediaGroupId = value; }
        }
        //-----------------------------------------------------------------
        public string MediaName
        {
            get { return _MediaName; }
            set { _MediaName = value; }
        }
        //-----------------------------------------------------------------
        public string MediaDesc
        {
            get { return _MediaDesc; }
            set { _MediaDesc = value; }
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

    }
}