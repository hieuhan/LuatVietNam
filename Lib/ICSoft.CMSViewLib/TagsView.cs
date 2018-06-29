
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
    public class TagsView
    {
        private byte _LanguageId;
        private int _TagId;
        private string _TagName;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        //-----------------------------------------------------------------
        public TagsView()
        {
        }
        //-----------------------------------------------------------------        
        ~TagsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------    
        public int TagId
        {
            get { return _TagId; }
            set { _TagId = value; }
        }
        //-----------------------------------------------------------------
        public string TagName
        {
            get { return _TagName; }
            set { _TagName = value; }
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

    }
}