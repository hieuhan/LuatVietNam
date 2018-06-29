
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSLib
{
    public class ServicePakages
    {   
        private byte _LanguageId;
        private byte _ApplicationTypeId;
	    private short _ServicePakageId;
	    private string _PakageName;
	    private string _PakageContent;
	    private string _PakageImagePath;
	    private short _DisplayOrder;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private short _ServicePakageGroupId;
	    private byte _ServicePakageStatusId;
        private int _NumMonthUse;
        private int _NumDayUse;
        private int _NumDownload;
        private int _ReviewStatusId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public ServicePakages()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public ServicePakages(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ServicePakages()
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
        public byte ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------    
	    public short ServicePakageId
        {
		    get { return _ServicePakageId; }
		    set { _ServicePakageId = value; }
	    }
        //-----------------------------------------------------------------
        public string PakageName
		{
            get { return _PakageName; }
		    set { _PakageName = value; }
		}    
        //-----------------------------------------------------------------
        public string PakageContent
		{
            get { return _PakageContent; }
		    set { _PakageContent = value; }
		}    
        //-----------------------------------------------------------------
        public string PakageImagePath
		{
            get { return _PakageImagePath; }
		    set { _PakageImagePath = value; }
		}    
        //-----------------------------------------------------------------
        public short DisplayOrder
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
        public int NumMonthUse
        {
            get { return _NumMonthUse; }
            set { _NumMonthUse = value; }
        }
        //-----------------------------------------------------------------
        public int NumDayUse
        {
            get { return _NumDayUse; }
            set { _NumDayUse = value; }
        }
        //-----------------------------------------------------------------
        public int NumDownload
        {
            get { return _NumDownload; }
            set { _NumDownload = value; }
        }
        //-----------------------------------------------------------------
        public int ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }   
    
	    public short ServicePakageGroupId
        {
		    get { return _ServicePakageGroupId; }
		    set { _ServicePakageGroupId = value; }
	    }
	    public byte ServicePakageStatusId
        {
		    get { return _ServicePakageStatusId; }
		    set { _ServicePakageStatusId = value; }
	    }
        private List<ServicePakages> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ServicePakages> l_ServicePakages = new List<ServicePakages>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ServicePakages m_ServicePakages = new ServicePakages(db.ConnectionString);
                    m_ServicePakages.LanguageId = smartReader.GetByte("LanguageId");
                    m_ServicePakages.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_ServicePakages.ServicePakageId = smartReader.GetInt16("ServicePackageId");
                    m_ServicePakages.ServicePakageGroupId = smartReader.GetInt16("ServicePakageGroupId");
                    m_ServicePakages.PakageName = smartReader.GetString("ServicePackageName");
                    m_ServicePakages.PakageContent = smartReader.GetString("ServicePackageDesc");
                    m_ServicePakages.PakageImagePath = smartReader.GetString("PakageImagePath");
                    m_ServicePakages.ServicePakageStatusId = smartReader.GetByte("ServicePakageStatusId");
                    m_ServicePakages.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_ServicePakages.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ServicePakages.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_ServicePakages.NumMonthUse = smartReader.GetInt32("NumMonthUse");
                    m_ServicePakages.NumDayUse = smartReader.GetInt32("NumDayUse");
                    m_ServicePakages.NumDownload = smartReader.GetInt32("NumDownload");
                    m_ServicePakages.ReviewStatusId = smartReader.GetInt32("ReviewStatusId");
                    l_ServicePakages.Add(m_ServicePakages);
                }
                reader.Close();
                return l_ServicePakages;
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServicePakages_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageGroupId", this.ServicePakageGroupId));
                cmd.Parameters.Add(new SqlParameter("@PakageName", this.PakageName));
                cmd.Parameters.Add(new SqlParameter("@PakageContent", this.PakageContent));
                cmd.Parameters.Add(new SqlParameter("@PakageImagePath", this.PakageImagePath));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageStatusId", this.ServicePakageStatusId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageId", this.ServicePakageId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServicePakageId =Convert.ToInt16((cmd.Parameters["@ServicePakageId"].Value == null) ? 0 : (cmd.Parameters["@ServicePakageId"].Value));
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
                SqlCommand cmd = new SqlCommand("ServicePakages_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageId", this.ServicePakageId));
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
        public List<ServicePakages> GetList()
        {
            List<ServicePakages> RetVal = new List<ServicePakages>();
            try
            {
                string sql = "SELECT * FROM V$ServicePakages";
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
        public static List<ServicePakages> Static_GetList(string ConStr)
        {
            List<ServicePakages> RetVal = new List<ServicePakages>();
            try
            {
                ServicePakages m_ServicePakages = new ServicePakages(ConStr);
                RetVal = m_ServicePakages.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServicePakages> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<ServicePakages> GetListOrderBy(string OrderBy)
        {
            List<ServicePakages> RetVal = new List<ServicePakages>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$ServicePakages " ;
                if (OrderBy.Length >0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
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
        public static List<ServicePakages> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ServicePakages m_ServicePakages = new ServicePakages(ConStr);
            return m_ServicePakages.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServicePakages> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public List<ServicePakages> GetListByServicePakageId(short ServicePakageId, byte LanguageId, byte ApplicationTypeId)
        {
            List<ServicePakages> RetVal = new List<ServicePakages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PakageName="";
                short ServicePakageGroupId = 0;
                byte ServicePakageStatusId=0;
                int RowCount=0;          
                RetVal= GetPage(ActUserId, RowAmount, PageIndex, OrderBy, ServicePakageId, ServicePakageGroupId, LanguageId, ApplicationTypeId, PakageName, ServicePakageStatusId, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        
        //-------------------------------------------------------------- 
        public ServicePakages Get(short ServicePakageId, byte LanguageId, byte ApplicationTypeId)
        {
            ServicePakages RetVal = new ServicePakages(db.ConnectionString);
            try
            {
                List<ServicePakages> list = GetListByServicePakageId(ServicePakageId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (ServicePakages)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ServicePakages Get()
        {
            return Get(this.ServicePakageId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static ServicePakages Static_Get(string Constr, short ServicePakageId, byte LanguageId, byte ApplicationTypeId)
        {
            ServicePakages m_ServicePakages = new ServicePakages(Constr);
            return m_ServicePakages.Get(ServicePakageId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static ServicePakages Static_Get(short ServicePakageId, byte LanguageId, byte ApplicationTypeId)
        {
            return Static_Get("",ServicePakageId, LanguageId, ApplicationTypeId);
        }
        //--------------------------------------------------------------     
        public List<ServicePakages> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short ServicePakageId, short ServicePakageGroupId, byte LanguageId, byte ApplicationTypeId, string PakageName, byte ServicePakageStatusId, ref int RowCount)
        {
            List<ServicePakages> RetVal = new List<ServicePakages>();
            try
            {
                SqlCommand cmd = new SqlCommand("ServicePakages_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageId", ServicePakageId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageGroupId", ServicePakageGroupId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@PakageName", PakageName));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageStatusId", ServicePakageStatusId));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);                
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<ServicePakages> Search(int ActUserId, string OrderBy, short ServicePakageId, short ServicePakageGroupId, byte LanguageId, byte ApplicationTypeId, string PakageName, byte ServicePakageStatusId, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, ServicePakageId, ServicePakageGroupId, LanguageId, ApplicationTypeId, PakageName, ServicePakageStatusId, ref RowCount);
        }
    } 
}