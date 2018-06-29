
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class ServicesView
    {   
	    private short _ServiceId;
	    private string _ServiceName;
	    private string _ServiceDesc;
	    private string _ServiceCode;
        private short _SiteId;
        private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        //-----------------------------------------------------------------
		public ServicesView()
        {
		}
        //-----------------------------------------------------------------        
        ~ServicesView()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short ServiceId
        {
		    get { return _ServiceId; }
		    set { _ServiceId = value; }
	    }
        //-----------------------------------------------------------------
        public string ServiceName
		{
            get { return _ServiceName; }
		    set { _ServiceName = value; }
		}    
        //-----------------------------------------------------------------
        public string ServiceDesc
		{
            get { return _ServiceDesc; }
		    set { _ServiceDesc = value; }
		}    
        //-----------------------------------------------------------------
        public string ServiceCode
		{
            get { return _ServiceCode; }
		    set { _ServiceCode = value; }
		}
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
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

        public static List<ServicesView> Init(SmartDataReader smartReader)
        {
            List<ServicesView> l_Services = new List<ServicesView>();
            try
            {
                while (smartReader.Read())
                {
                    ServicesView m_Services = new ServicesView();
                    m_Services.ServiceId = smartReader.GetInt16("ServiceId");
                    m_Services.ServiceName = smartReader.GetString("ServiceName");
                    m_Services.ServiceDesc = smartReader.GetString("ServiceDesc");
                    m_Services.ServiceCode = smartReader.GetString("ServiceCode");
                    m_Services.SiteId = smartReader.GetInt16("SiteId");
                    m_Services.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Services.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Services.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Services.Add(m_Services);
                }
                return l_Services;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------
        public static ServicesView Static_Get(short ServiceId, List<ServicesView> list)
        {
            ServicesView RetVal = list.Find(i => i.ServiceId == ServiceId);
            return RetVal == null ? new ServicesView() : RetVal;
        }
        //--------------------------------------------------------------     
    } 
}

