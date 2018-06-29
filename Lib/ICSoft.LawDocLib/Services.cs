
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
    public class Services
    {   
	    private short _ServiceId;
	    private string _ServiceName;
	    private string _ServiceDesc;
	    private string _ServiceCode;
        private short _SiteId;
        private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Services()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Services(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Services()
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
    
        private List<Services> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Services> l_Services = new List<Services>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Services m_Services = new Services(db.ConnectionString);
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
                reader.Close();
                return l_Services;
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
                SqlCommand cmd = new SqlCommand("Services_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", this.ServiceId));
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
                SqlCommand cmd = new SqlCommand("Services_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceName", this.ServiceName));
                cmd.Parameters.Add(new SqlParameter("@ServiceDesc", this.ServiceDesc));
                cmd.Parameters.Add(new SqlParameter("@ServiceCode", this.ServiceCode));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", this.ServiceId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServiceId =Convert.ToInt16((cmd.Parameters["@ServiceId"].Value == null) ? 0 : (cmd.Parameters["@ServiceId"].Value));
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
                SqlCommand cmd = new SqlCommand("Services_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId",this.ServiceId));
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
        public static List<Services> Static_GetList(int ActUserId, short SiteId)
        {
            Services m_Services = new Services();
            m_Services.SiteId = SiteId;
            return m_Services.Services_GetList(ActUserId, "DisplayOrder", "", "", 0);
        }
        //--------------------------------------------------------------  
        public List<Services> GetListByServiceId(short ServiceId)
        {
            List<Services> RetVal = new List<Services>();
            try
            {
                if (ServiceId > 0)
                {
                    string sql = "SELECT * FROM Services WHERE (ServiceId=" + ServiceId.ToString() + ")";
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
        public Services Get(short ServiceId)
        {
            Services RetVal = new Services(db.ConnectionString);
            try
            {
                List<Services> list = GetListByServiceId(ServiceId);
                if (list.Count > 0)
                {
                    RetVal = (Services)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Services Get()
        {
            return Get(this.ServiceId);
        }
        //-------------------------------------------------------------- 
        public static Services Static_Get(short ServiceId)
        {
            return new Services().Get(ServiceId);
        }
        //-------------------------------------------------------------- 
        public Services GetByServicePackageId(short ServicePackageId)
        {
            try
            {
                Services RetVal = new Services();
                SqlCommand cmd = new SqlCommand("Services_GetByServicePackageId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
                List<Services> list = Init(cmd);
                if (list.Count > 0) RetVal = list[0];
                return RetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------------------- 
        public static Services Static_GetByServicePackageId(short ServicePackageId)
        {
            return new Services().GetByServicePackageId(ServicePackageId);
        }
        //-----------------------------------------------------------------------------
        public static Services Static_Get(short ServiceId, List<Services> lList)
        {
            Services RetVal = new Services();
            if (ServiceId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.ServiceId == ServiceId);
                if (RetVal == null) RetVal = new Services();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Services> Services_GetList(int ActUserId, string OrderBy, string ServiceName, string ServiceCode, byte ReviewStatusId)
        {
            List<Services> RetVal = new List<Services>();
            try
            {
                SqlCommand cmd = new SqlCommand("Services_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ServiceName",  StringUtil.InjectionString(ServiceName)));
                cmd.Parameters.Add(new SqlParameter("@ServiceCode",  StringUtil.InjectionString(ServiceCode)));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
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

