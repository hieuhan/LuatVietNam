
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
    public class ServiceRoles
    {   
	    private short _ServiceRoleId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private short _ServiceId;
	    private byte _RoleId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public ServiceRoles()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
		}
        //-----------------------------------------------------------------        
        public ServiceRoles(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ServiceRoles()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short ServiceRoleId
        {
		    get { return _ServiceRoleId; }
		    set { _ServiceRoleId = value; }
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
    
	    public short ServiceId
        {
		    get { return _ServiceId; }
		    set { _ServiceId = value; }
	    }
	    public byte RoleId
        {
		    get { return _RoleId; }
		    set { _RoleId = value; }
	    }
        private List<ServiceRoles> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ServiceRoles> l_ServiceRoles = new List<ServiceRoles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ServiceRoles m_ServiceRoles = new ServiceRoles(db.ConnectionString);
                    m_ServiceRoles.ServiceRoleId = smartReader.GetInt16("ServiceRoleId");
                    m_ServiceRoles.ServiceId = smartReader.GetInt16("ServiceId");
                    m_ServiceRoles.RoleId = smartReader.GetByte("RoleId");
                    m_ServiceRoles.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ServiceRoles.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_ServiceRoles.Add(m_ServiceRoles);
                }
                reader.Close();
                return l_ServiceRoles;
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
                SqlCommand cmd = new SqlCommand("ServiceRoles_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", this.ServiceId));
                cmd.Parameters.Add(new SqlParameter("@RoleId", this.RoleId));
                cmd.Parameters.Add("@ServiceRoleId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServiceRoleId =Convert.ToInt16((cmd.Parameters["@ServiceRoleId"].Value == null) ? 0 : (cmd.Parameters["@ServiceRoleId"].Value));
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
                SqlCommand cmd = new SqlCommand("ServiceRoles_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceRoleId",this.ServiceRoleId));
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
        public byte DeleteByServiceId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServiceRoles_DeleteByServiceId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", this.ServiceId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<ServiceRoles> GetList()
        {
            List<ServiceRoles> RetVal = new List<ServiceRoles>();
            try
            {
                string sql = "SELECT * FROM ServiceRoles";
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
        public static List<ServiceRoles> Static_GetList(string ConStr)
        {
            List<ServiceRoles> RetVal = new List<ServiceRoles>();
            try
            {
                ServiceRoles m_ServiceRoles = new ServiceRoles(ConStr);
                RetVal = m_ServiceRoles.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServiceRoles> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<ServiceRoles> GetListOrderBy(string OrderBy)
        {
            List<ServiceRoles> RetVal = new List<ServiceRoles>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM ServiceRoles " ;
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
        public static List<ServiceRoles> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ServiceRoles m_ServiceRoles = new ServiceRoles(ConStr);
            return m_ServiceRoles.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServiceRoles> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public List<ServiceRoles> GetListByServiceRoleId(short ServiceRoleId)
        {
            List<ServiceRoles> RetVal = new List<ServiceRoles>();
            try
            {
                if (ServiceRoleId > 0)
                {
                    string sql = "SELECT * FROM ServiceRoles WHERE (ServiceRoleId=" + ServiceRoleId.ToString() + ")";
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
        public ServiceRoles Get(short ServiceRoleId)
        {
            ServiceRoles RetVal = new ServiceRoles(db.ConnectionString);
            try
            {
                List<ServiceRoles> list = GetListByServiceRoleId(ServiceRoleId);
                if (list.Count > 0)
                {
                    RetVal = (ServiceRoles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ServiceRoles Get()
        {
            return Get(this.ServiceRoleId);
        }
        //-------------------------------------------------------------- 
        public static ServiceRoles Static_Get(short ServiceRoleId)
        {
            return Static_Get(ServiceRoleId);
        }
        //--------------------------------------------------------------     
        public List<ServiceRoles> ServiceRoles_GetList(int ActUserId, string OrderBy, short ServiceId, byte RoleId)
        {
            List<ServiceRoles> RetVal = new List<ServiceRoles>();
            try
            {
                SqlCommand cmd = new SqlCommand("ServiceRoles_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@RoleId", RoleId));
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

