
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using ICSoft.CMSLib;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class ServicePackages
    {   
	    private short _ServicePackageId;
        private short _ServicePackageParentId;
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
        private DBAccess db;
        //-----------------------------------------------------------------
		public ServicePackages()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
		}
        //-----------------------------------------------------------------        
        public ServicePackages(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ServicePackages()
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
        public short ServicePackageParentId
        {
            get { return _ServicePackageParentId; }
            set { _ServicePackageParentId = value; }
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
        private List<ServicePackages> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ServicePackages> l_ServicePackages = new List<ServicePackages>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ServicePackages m_ServicePackages = new ServicePackages(db.ConnectionString);
                    m_ServicePackages.ServicePackageId = smartReader.GetInt16("ServicePackageId");
                    m_ServicePackages.ServicePackageParentId = smartReader.GetInt16("ServicePackageParentId");
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
                reader.Close();
                return l_ServicePackages;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
        }
        //-----------------------------------------------------------
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte UpdateReviewStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServicePackages_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", this.ServicePackageId));  
                //cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                //RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
                RetVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServicePackages_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", this.ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageParentId", this.ServicePackageParentId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageName", this.ServicePackageName));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageDesc", this.ServicePackageDesc));
                cmd.Parameters.Add(new SqlParameter("@NumMonthUse", this.NumMonthUse));
                cmd.Parameters.Add(new SqlParameter("@NumDayUse", this.NumDayUse));
                cmd.Parameters.Add(new SqlParameter("@NumDownload", this.NumDownload));
                cmd.Parameters.Add(new SqlParameter("@ConcurrentLogin", this.ConcurrentLogin));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", this.ServicePackageId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServicePackageId =Convert.ToInt16((cmd.Parameters["@ServicePackageId"].Value == null) ? 0 : (cmd.Parameters["@ServicePackageId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServicePackages_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId",this.ServicePackageId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServicePackages> Static_GetList(int ActUserId, short SiteId, short ServiceId)
        {
            ServicePackages m_ServicePackages = new ServicePackages();
            m_ServicePackages.SiteId = SiteId;
            return m_ServicePackages.ServicePackages_GetList(ActUserId, "ServicePackageName", ServiceId, "", 0);
        }
        //--------------------------------------------------------------     
        public static List<ServicePackages> Static_GetList(int ActUserId, short SiteId, short ServiceId, string PaddingChar)
        {
            List<ServicePackages> RetVal = Static_GetList(ActUserId, SiteId, ServiceId);
            foreach (ServicePackages mServicePackages in RetVal)
            {
                if (mServicePackages.ServicePackageParentId > 0)
                {
                    mServicePackages.ServicePackageName = PaddingChar + mServicePackages.ServicePackageName;
                    mServicePackages.ServicePackageDesc = PaddingChar + mServicePackages.ServicePackageDesc;
                }
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<ServicePackages> GetListByServiceId(short ServiceId, byte ReviewStatusId = 0)
        {
            List<ServicePackages> RetVal = new List<ServicePackages>();
            try
            {
                if (ServiceId > 0)
                {
                    string sql = "SELECT * FROM ServicePackages WHERE (ServiceId=" + ServiceId.ToString() + (ReviewStatusId > 0 ? string.Format(" AND ReviewStatusId = {0}",ReviewStatusId) : string.Empty) + ") ORDER BY TreeOrder";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = Init(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<ServicePackages> GetListByServiceId(short ServiceId, string PaddingChar)
        {
            List<ServicePackages> RetVal = GetListByServiceId(ServiceId);
            foreach (ServicePackages mServicePackages in RetVal)
            {
                if (mServicePackages.ServicePackageParentId > 0)
                {
                    mServicePackages.ServicePackageName = PaddingChar + mServicePackages.ServicePackageName;
                    mServicePackages.ServicePackageDesc = PaddingChar + mServicePackages.ServicePackageDesc;
                }
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<ServicePackages> GetListByServiceIdAndParentId(short ServiceId, short ServicePackageParentId)
        {
            List<ServicePackages> RetVal = new List<ServicePackages>();
            try
            {
                string sql = "SELECT * FROM ServicePackages WHERE (ServiceId=" + ServiceId.ToString() + " AND ServicePackageParentId=" + ServicePackageParentId.ToString() + ") ORDER BY TreeOrder";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public static List<ServicePackages> Static_GetListByServiceIdAndParentId(short ServiceId, short ServicePackageParentId)
        {
            return new ServicePackages().GetListByServiceIdAndParentId(ServiceId, ServicePackageParentId);
        }
        //--------------------------------------------------------------
        public static List<ServicePackages> Static_GetListByServiceId(string ConStr, short ServiceId, byte ReviewStatusId=0)
        {
            List<ServicePackages> RetVal = new List<ServicePackages>();
            try
            {
                ServicePackages m_ServicePackages = new ServicePackages(ConStr);
                RetVal = m_ServicePackages.GetListByServiceId(ServiceId, ReviewStatusId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServicePackages> Static_GetListByServiceId(short ServiceId, byte ReviewStatusId = 0)
        {
            return Static_GetListByServiceId("", ServiceId, ReviewStatusId);
        }
        //--------------------------------------------------------------     
        public static List<ServicePackages> Static_GetListByServiceId(short ServiceId, string PaddingChar)
        {
            return new ServicePackages().GetListByServiceId(ServiceId, PaddingChar);
        }
        //--------------------------------------------------------------  
        public List<ServicePackages> GetListByServicePackageId(short ServicePackageId)
        {
            List<ServicePackages> RetVal = new List<ServicePackages>();
            try
            {
                if (ServicePackageId > 0)
                {
                    string sql = "SELECT * FROM ServicePackages WHERE (ServicePackageId=" + ServicePackageId.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = Init(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public ServicePackages Get(short ServicePackageId)
        {
            ServicePackages RetVal = new ServicePackages(db.ConnectionString);
            try
            {
                List<ServicePackages> list = GetListByServicePackageId(ServicePackageId);
                if (list.Count > 0)
                {
                    RetVal = (ServicePackages)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ServicePackages Get()
        {
            return Get(this.ServicePackageId);
        }
        //-------------------------------------------------------------- 
        public static ServicePackages Static_Get(short ServicePackageId)
        {
            return new ServicePackages().Get(ServicePackageId);
        }
        //-----------------------------------------------------------------------------
        public static ServicePackages Static_Get(short ServicePackageId, List<ServicePackages> lList)
        {
            ServicePackages RetVal = new ServicePackages();
            if (ServicePackageId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.ServicePackageId == ServicePackageId);
                if (RetVal == null) RetVal = new ServicePackages();
            }
            return RetVal;
        }

        //--------------------------------------------------------------  
        public DataSet GetDatasetByServicePackageId(short ServiceId)
        {
            DataSet RetVal = new DataSet();
            try
            {
                if (ServiceId > 0)
                {
                    string sql = "SELECT * FROM ServicePackages WHERE (ServiceId=" + ServiceId.ToString() + ") ORDER BY ServicePackageName";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = db.getDataSet(cmd); 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //--------------------------------------------------------------     
        public List<ServicePackages> ServicePackages_GetList(int ActUserId, string OrderBy, short ServiceId, string ServicePackageName, byte ReviewStatusId)
        {
            List<ServicePackages> RetVal = new List<ServicePackages>();
            try
            {
                SqlCommand cmd = new SqlCommand("ServicePackages_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageParentId", this.ServicePackageParentId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageName", StringUtil.InjectionString(ServicePackageName)));                
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return RetVal;
        }
        //--------------------------------------------------------------     
    } 
}
