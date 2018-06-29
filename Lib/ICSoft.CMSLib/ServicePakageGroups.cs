
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
    public class ServicePakageGroups
    {   
        private byte _LanguageId;
        private byte _ApplicationTypeId;
	    private short _ServicePakageGroupId;
	    private string _GroupName;
	    private short _GroupOrder;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _ServiceTypeId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public ServicePakageGroups()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public ServicePakageGroups(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ServicePakageGroups()
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
	    public short ServicePakageGroupId
        {
		    get { return _ServicePakageGroupId; }
		    set { _ServicePakageGroupId = value; }
	    }
        //-----------------------------------------------------------------
        public string GroupName
		{
            get { return _GroupName; }
		    set { _GroupName = value; }
		}    
        //-----------------------------------------------------------------
        public short GroupOrder
		{
            get { return _GroupOrder; }
		    set { _GroupOrder = value; }
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
    
	    public byte ServiceTypeId
        {
		    get { return _ServiceTypeId; }
		    set { _ServiceTypeId = value; }
	    }
        private List<ServicePakageGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ServicePakageGroups> l_ServicePakageGroups = new List<ServicePakageGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ServicePakageGroups m_ServicePakageGroups = new ServicePakageGroups(db.ConnectionString);
                    m_ServicePakageGroups.LanguageId = smartReader.GetByte("LanguageId");
                    m_ServicePakageGroups.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_ServicePakageGroups.ServicePakageGroupId = smartReader.GetInt16("ServicePakageGroupId");
                    m_ServicePakageGroups.ServiceTypeId = smartReader.GetByte("ServiceTypeId");
                    m_ServicePakageGroups.GroupName = smartReader.GetString("GroupName");
                    m_ServicePakageGroups.GroupOrder = smartReader.GetInt16("GroupOrder");
                    m_ServicePakageGroups.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ServicePakageGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_ServicePakageGroups.Add(m_ServicePakageGroups);
                }
                reader.Close();
                return l_ServicePakageGroups;
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
                SqlCommand cmd = new SqlCommand("ServicePakageGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeId", this.ServiceTypeId));
                cmd.Parameters.Add(new SqlParameter("@GroupName", this.GroupName));
                cmd.Parameters.Add(new SqlParameter("@GroupOrder", this.GroupOrder));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageGroupId", this.ServicePakageGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServicePakageGroupId =Convert.ToInt16((cmd.Parameters["@ServicePakageGroupId"].Value == null) ? 0 : (cmd.Parameters["@ServicePakageGroupId"].Value));
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
                SqlCommand cmd = new SqlCommand("ServicePakageGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageGroupId",this.ServicePakageGroupId));
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
        public List<ServicePakageGroups> GetList()
        {
            List<ServicePakageGroups> RetVal = new List<ServicePakageGroups>();
            try
            {
                string sql = "SELECT * FROM V$ServicePakageGroups";
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
        public List<ServicePakageGroups> GetListByServiceTypeId(byte ServiceTypeId)
        {
            List<ServicePakageGroups> RetVal = new List<ServicePakageGroups>();
            try
            {
                string sql = "";
                if (ServiceTypeId > 0)
                {
                     sql = "SELECT * FROM V$ServicePakageGroups where (ServiceTypeId=" + ServiceTypeId.ToString() + ")";
                }
                else
                {
                     sql = "SELECT * FROM V$ServicePakageGroups";
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
        public static List<ServicePakageGroups> Static_GetListByServiceTypeId(string ConStr, byte ServiceTypeId)
        {
            List<ServicePakageGroups> RetVal = new List<ServicePakageGroups>();
            try
            {
                ServicePakageGroups m_ServicePakageGroups = new ServicePakageGroups(ConStr);
                RetVal = m_ServicePakageGroups.GetListByServiceTypeId(ServiceTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServicePakageGroups> Static_GetListByServiceTypeId(byte ServiceTypeId)
        {
            return Static_GetListByServiceTypeId("", ServiceTypeId);
        }
        //--------------------------------------------------------------
        public static List<ServicePakageGroups> Static_GetList(string ConStr)
        {
            List<ServicePakageGroups> RetVal = new List<ServicePakageGroups>();
            try
            {
                ServicePakageGroups m_ServicePakageGroups = new ServicePakageGroups(ConStr);
                RetVal = m_ServicePakageGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServicePakageGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<ServicePakageGroups> GetListOrderBy(string OrderBy)
        {
            List<ServicePakageGroups> RetVal = new List<ServicePakageGroups>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$ServicePakageGroups " ;
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
        public static List<ServicePakageGroups> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ServicePakageGroups m_ServicePakageGroups = new ServicePakageGroups(ConStr);
            return m_ServicePakageGroups.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServicePakageGroups> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<ServicePakageGroups> GetListByServicePakageGroupId(short ServicePakageGroupId, byte LanguageId, byte ApplicationTypeId)
        {
            List<ServicePakageGroups> RetVal = new List<ServicePakageGroups>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                byte ServiceTypeId = 0;
                int RowCount=0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, ServicePakageGroupId, LanguageId, ApplicationTypeId, ServiceTypeId, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }        
        //-------------------------------------------------------------- 
        public ServicePakageGroups Get(short ServicePakageGroupId, byte LanguageId, byte ApplicationTypeId)
        {
            ServicePakageGroups RetVal = new ServicePakageGroups(db.ConnectionString);
            try
            {
                List<ServicePakageGroups> list = GetListByServicePakageGroupId(ServicePakageGroupId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (ServicePakageGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ServicePakageGroups Get()
        {
            return Get(this.ServicePakageGroupId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static ServicePakageGroups Static_Get(string Constr, short ServicePakageGroupId, byte LanguageId, byte ApplicationTypeId)
        {
            ServicePakageGroups m_ServicePakageGroups = new ServicePakageGroups(Constr);
            return m_ServicePakageGroups.Get(ServicePakageGroupId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static ServicePakageGroups Static_Get(short ServicePakageGroupId, byte LanguageId, byte ApplicationTypeId)
        {
            return Static_Get("", ServicePakageGroupId, LanguageId, ApplicationTypeId);
        }
        //--------------------------------------------------------------     
        public List<ServicePakageGroups> GetListByServiceTypeId(byte LanguageId, byte ApplicationTypeId, byte ServiceTypeId)
        {
            int ActUserId = 0;
            int RowAmount = 0;
            int PageIndex = 0;
            string OrderBy = "";
            int RowCount = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, ServicePakageGroupId, LanguageId, ApplicationTypeId, ServiceTypeId, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<ServicePakageGroups> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short ServicePakageGroupId, byte LanguageId, byte ApplicationTypeId, byte ServiceTypeId, ref int RowCount)
        {
            List<ServicePakageGroups> RetVal = new List<ServicePakageGroups>();
            try
            {
                SqlCommand cmd = new SqlCommand("ServicePakageGroups_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ServicePakageGroupId", ServicePakageGroupId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeId", ServiceTypeId));
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
        public List<ServicePakageGroups> Search(int ActUserId, string OrderBy, short ServicePakageGroupId, byte LanguageId, byte ApplicationTypeId, byte ServiceTypeId, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, ServicePakageGroupId, LanguageId, ApplicationTypeId, ServiceTypeId, ref RowCount);
        }  
       
    } 
}
