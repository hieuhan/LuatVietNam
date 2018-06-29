
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class ServiceTypes
    {
        private byte _ServiceTypeId;
        private string _ServiceTypeName;
        private string _ServiceTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ServiceTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ServiceTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ServiceTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte ServiceTypeId
        {
            get { return _ServiceTypeId; }
            set { _ServiceTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string ServiceTypeName
        {
            get { return _ServiceTypeName; }
            set { _ServiceTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string ServiceTypeDesc
        {
            get { return _ServiceTypeDesc; }
            set { _ServiceTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<ServiceTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ServiceTypes> l_ServiceTypes = new List<ServiceTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ServiceTypes m_ServiceTypes = new ServiceTypes(db.ConnectionString);
                    m_ServiceTypes.ServiceTypeId = smartReader.GetByte("ServiceTypeId");
                    m_ServiceTypes.ServiceTypeName = smartReader.GetString("ServiceTypeName");
                    m_ServiceTypes.ServiceTypeDesc = smartReader.GetString("ServiceTypeDesc");

                    l_ServiceTypes.Add(m_ServiceTypes);
                }
                reader.Close();
                return l_ServiceTypes;
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
                SqlCommand cmd = new SqlCommand("ServiceTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeName", this.ServiceTypeName));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeDesc", this.ServiceTypeDesc));
                cmd.Parameters.Add("@ServiceTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ServiceTypeId = Convert.ToByte((cmd.Parameters["@ServiceTypeId"].Value == null) ? 0 : (cmd.Parameters["@ServiceTypeId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServiceTypes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeName", this.ServiceTypeName));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeDesc", this.ServiceTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeId", this.ServiceTypeId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ServiceTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ServiceTypeId", this.ServiceTypeId));
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
        public List<ServiceTypes> GetList()
        {
            List<ServiceTypes> RetVal = new List<ServiceTypes>();
            try
            {
                string sql = "SELECT * FROM V$ServiceTypes";
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
        public static List<ServiceTypes> Static_GetList(string ConStr)
        {
            List<ServiceTypes> RetVal = new List<ServiceTypes>();
            try
            {
                ServiceTypes m_ServiceTypes = new ServiceTypes(ConStr);
                RetVal = m_ServiceTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ServiceTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<ServiceTypes> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            ServiceTypes m_ServiceTypes = new ServiceTypes(ConStr);
            List<ServiceTypes> RetVal = m_ServiceTypes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_ServiceTypes.ServiceTypeName = TextOptionAll;
                m_ServiceTypes.ServiceTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_ServiceTypes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ServiceTypes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<ServiceTypes> GetListOrderBy(string OrderBy)
        {
            List<ServiceTypes> RetVal = new List<ServiceTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$ServiceTypes ";
                if (OrderBy.Length > 0)
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
        public static List<ServiceTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ServiceTypes m_ServiceTypes = new ServiceTypes(ConStr);
            return m_ServiceTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServiceTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ServiceTypes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<ServiceTypes> RetVal = new List<ServiceTypes>();
            ServiceTypes m_ServiceTypes = new ServiceTypes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_ServiceTypes.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_ServiceTypes.ServiceTypeName = TextOptionAll;
                    m_ServiceTypes.ServiceTypeDesc = TextOptionAll;
                    RetVal.Insert(0, m_ServiceTypes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ServiceTypes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<ServiceTypes> GetListByServiceTypeId(byte ServiceTypeId)
        {
            List<ServiceTypes> RetVal = new List<ServiceTypes>();
            try
            {
                if (ServiceTypeId > 0)
                {
                    string sql = "SELECT * FROM V$ServiceTypes WHERE (ServiceTypeId=" + ServiceTypeId.ToString() + ")";
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
        public ServiceTypes Get(byte ServiceTypeId)
        {
            ServiceTypes RetVal = new ServiceTypes(db.ConnectionString);
            try
            {
                List<ServiceTypes> list = GetListByServiceTypeId(ServiceTypeId);
                if (list.Count > 0)
                {
                    RetVal = (ServiceTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ServiceTypes Get()
        {
            return Get(this.ServiceTypeId);
        }
        //-------------------------------------------------------------- 
        public static ServiceTypes Static_Get(byte ServiceTypeId)
        {
            return Static_Get(ServiceTypeId);
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte ServiceTypesId)
        {
            string RetVal = "";
            ServiceTypes m_ServiceTypes = new ServiceTypes();
            m_ServiceTypes = m_ServiceTypes.Get(ServiceTypesId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == CmsConstants.CULTURE_VN)
            {
                RetVal = m_ServiceTypes.ServiceTypeDesc;
            }
            else
            {
                RetVal = m_ServiceTypes.ServiceTypeName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
