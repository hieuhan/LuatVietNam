
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
    public class ApplicationTypes
    {
        private byte _ApplicationTypeId;
        private string _ApplicationTypeName;
        private string _ApplicationTypeDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ApplicationTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ApplicationTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ApplicationTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string ApplicationTypeName
        {
            get { return _ApplicationTypeName; }
            set { _ApplicationTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string ApplicationTypeDesc
        {
            get { return _ApplicationTypeDesc; }
            set { _ApplicationTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<ApplicationTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ApplicationTypes> l_ApplicationTypes = new List<ApplicationTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ApplicationTypes m_ApplicationTypes = new ApplicationTypes(db.ConnectionString);
                    m_ApplicationTypes.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_ApplicationTypes.ApplicationTypeName = smartReader.GetString("ApplicationTypeName");
                    m_ApplicationTypes.ApplicationTypeDesc = smartReader.GetString("ApplicationTypeDesc");
                    m_ApplicationTypes.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_ApplicationTypes.Add(m_ApplicationTypes);
                }
                reader.Close();
                return l_ApplicationTypes;
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
                SqlCommand cmd = new SqlCommand("ApplicationTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeName", this.ApplicationTypeName));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeDesc", this.ApplicationTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@ApplicationTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ApplicationTypeId = Convert.ToByte((cmd.Parameters["@ApplicationTypeId"].Value == null) ? 0 : (cmd.Parameters["@ApplicationTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("ApplicationTypes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeName", this.ApplicationTypeName));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeDesc", this.ApplicationTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
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
                SqlCommand cmd = new SqlCommand("ApplicationTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
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
        public List<ApplicationTypes> GetList()
        {
            List<ApplicationTypes> RetVal = new List<ApplicationTypes>();
            try
            {
                string sql = "SELECT * FROM V$ApplicationTypes";
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
        public static List<ApplicationTypes> Static_GetList(string ConStr)
        {
            List<ApplicationTypes> RetVal = new List<ApplicationTypes>();
            try
            {
                ApplicationTypes m_ApplicationTypes = new ApplicationTypes(ConStr);
                RetVal = m_ApplicationTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ApplicationTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<ApplicationTypes> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            ApplicationTypes m_ApplicationTypes = new ApplicationTypes(ConStr);
            List<ApplicationTypes> RetVal = m_ApplicationTypes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_ApplicationTypes.ApplicationTypeName = TextOptionAll;
                m_ApplicationTypes.ApplicationTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_ApplicationTypes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ApplicationTypes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<ApplicationTypes> GetListOrderBy(string OrderBy)
        {
            List<ApplicationTypes> RetVal = new List<ApplicationTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$ApplicationTypes ";
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
        public static List<ApplicationTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ApplicationTypes m_ApplicationTypes = new ApplicationTypes(ConStr);
            return m_ApplicationTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ApplicationTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ApplicationTypes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<ApplicationTypes> RetVal = new List<ApplicationTypes>();
            ApplicationTypes m_ApplicationTypes = new ApplicationTypes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_ApplicationTypes.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_ApplicationTypes.ApplicationTypeName = TextOptionAll;
                    m_ApplicationTypes.ApplicationTypeDesc = TextOptionAll;
                    RetVal.Insert(0, m_ApplicationTypes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ApplicationTypes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
       //--------------------------------------------------------------  
        public List<ApplicationTypes> GetListByApplicationTypeId(byte ApplicationTypeId)
        {
            List<ApplicationTypes> RetVal = new List<ApplicationTypes>();
            try
            {
                if (ApplicationTypeId > 0)
                {
                    string sql = "SELECT * FROM V$ApplicationTypes WHERE (ApplicationTypeId=" + ApplicationTypeId.ToString() + ")";
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
        public ApplicationTypes Get(byte ApplicationTypeId)
        {
            ApplicationTypes RetVal = new ApplicationTypes(db.ConnectionString);
            try
            {
                List<ApplicationTypes> list = GetListByApplicationTypeId(ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (ApplicationTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ApplicationTypes Get()
        {
            return Get(this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static ApplicationTypes Static_Get(byte ApplicationTypeId)
        {
            return Static_Get(ApplicationTypeId);
        }
        //--------------------------------------------------------------     
        public static ApplicationTypes Static_Get(byte ApplicationTypeId, List<ApplicationTypes> list)
        {
            ApplicationTypes RetVal = new ApplicationTypes();
            RetVal = list.Find(i => i.ApplicationTypeId == ApplicationTypeId);
            if (RetVal == null) RetVal = new ApplicationTypes();
            return RetVal;
        }
        //-------------------------------------------------------------- 
    }
}