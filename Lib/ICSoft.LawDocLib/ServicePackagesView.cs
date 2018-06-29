
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
    public class ServicePackagesView
    {   
	    private short _ServicePackageId;
	    private string _ServicePackageName;
	    private string _ServicePackageDesc;
	    private short _NumMonthUse;
        private short _NumDayUse;
        private short _NumDownload;
	    private byte _ConcurrentLogin;
	    private int _Price;
	    private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private short _ServiceId;
        private short _SiteId;
        //-----------------------------------------------------------------
		public ServicePackagesView()
        {
		}
        //-----------------------------------------------------------------        
        ~ServicePackagesView()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short ServicePackageId
        {
		    get { return _ServicePackageId; }
		    set { _ServicePackageId = value; }
	    }
        //-----------------------------------------------------------------
        public string ServicePackageName
		{
            get { return _ServicePackageName; }
		    set { _ServicePackageName = value; }
		}    
        //-----------------------------------------------------------------
        public string ServicePackageDesc
		{
            get { return _ServicePackageDesc; }
		    set { _ServicePackageDesc = value; }
		}    
        //-----------------------------------------------------------------
        public short NumMonthUse
		{
            get { return _NumMonthUse; }
		    set { _NumMonthUse = value; }
		}    
        //-----------------------------------------------------------------
        public short NumDayUse
		{
            get { return _NumDayUse; }
		    set { _NumDayUse = value; }
		}
        //-----------------------------------------------------------------
        public short NumDownload
        {
            get { return _NumDownload; }
            set { _NumDownload = value; }
        }
        //-----------------------------------------------------------------
        public byte ConcurrentLogin
		{
            get { return _ConcurrentLogin; }
		    set { _ConcurrentLogin = value; }
		}    
        //-----------------------------------------------------------------
        public int Price
		{
            get { return _Price; }
		    set { _Price = value; }
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
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }    
        //-----------------------------------------------------------------
	    public short ServiceId
        {
		    get { return _ServiceId; }
		    set { _ServiceId = value; }
	    }
        public static List<ServicePackagesView> Init(SmartDataReader smartReader)
        {
            List<ServicePackagesView> l_ServicePackages = new List<ServicePackagesView>();
            try
            {
                while (smartReader.Read())
                {
                    ServicePackagesView m_ServicePackages = new ServicePackagesView();
                    m_ServicePackages.ServicePackageId = smartReader.GetInt16("ServicePackageId");
                    m_ServicePackages.ServiceId = smartReader.GetInt16("ServiceId");
                    m_ServicePackages.SiteId = smartReader.GetInt16("SiteId");
                    m_ServicePackages.ServicePackageName = smartReader.GetString("ServicePackageName");
                    m_ServicePackages.ServicePackageDesc = smartReader.GetString("ServicePackageDesc");
                    m_ServicePackages.NumMonthUse = smartReader.GetInt16("NumMonthUse");
                    m_ServicePackages.NumDayUse = smartReader.GetInt16("NumDayUse");
                    m_ServicePackages.NumDownload = smartReader.GetInt16("NumDownload");
                    m_ServicePackages.ConcurrentLogin = smartReader.GetByte("ConcurrentLogin");
                    m_ServicePackages.Price = smartReader.GetInt32("Price");
                    m_ServicePackages.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_ServicePackages.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ServicePackages.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_ServicePackages.Add(m_ServicePackages);
                }
                return l_ServicePackages;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //--------------------------------------------------------------     
        public static ServicePackagesView Static_Get(short ServicePagekageId, List<ServicePackagesView> list)
        {
            ServicePackagesView RetVal = list.Find(i => i.ServicePackageId == ServicePagekageId);
            return RetVal == null ? new ServicePackagesView() : RetVal;
        }
    } 
}
