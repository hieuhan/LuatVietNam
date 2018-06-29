
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
    public class AdvertPositionAdvertsView
    {
        private int _AdvertPositionAdvertId;
        private int _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _AdvertPositionId;
        private short _CategoryId;
        private int _AdvertId;
        private string _AdvertName;
        private string _PositionName;
        private string _CategoryName;
        private string _ImagePath;
        private byte _AdvertStatusId;
        //-----------------------------------------------------------------
        public AdvertPositionAdvertsView()
        {
        }
        //-----------------------------------------------------------------        
        ~AdvertPositionAdvertsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int AdvertPositionAdvertId
        {
            get { return _AdvertPositionAdvertId; }
            set { _AdvertPositionAdvertId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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

        public int AdvertPositionId
        {
            get { return _AdvertPositionId; }
            set { _AdvertPositionId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }  
        public int AdvertId
        {
            get { return _AdvertId; }
            set { _AdvertId = value; }
        }
        public string AdvertName
        {
            get { return _AdvertName; }
            set { _AdvertName = value; }
        }
        public string PositionName
        {
            get { return _PositionName; }
            set { _PositionName = value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public byte AdvertStatusId
        {
            get { return _AdvertStatusId; }
            set { _AdvertStatusId = value; }
        }
    }
}

