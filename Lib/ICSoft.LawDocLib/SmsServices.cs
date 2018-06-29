
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
    public class SmsServices
    {
        private byte _SmsServiceId;
        private string _SmsServiceName;
        private string _SmsServiceDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SmsServices()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SmsServices(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SmsServices()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte SmsServiceId
        {
            get { return _SmsServiceId; }
            set { _SmsServiceId = value; }
        }
        //-----------------------------------------------------------------
        public string SmsServiceName
        {
            get { return _SmsServiceName; }
            set { _SmsServiceName = value; }
        }
        //-----------------------------------------------------------------
        public string SmsServiceDesc
        {
            get { return _SmsServiceDesc; }
            set { _SmsServiceDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<SmsServices> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SmsServices> l_SmsServices = new List<SmsServices>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SmsServices m_SmsServices = new SmsServices(db.ConnectionString);
                    m_SmsServices.SmsServiceId = smartReader.GetByte("SmsServiceId");
                    m_SmsServices.SmsServiceName = smartReader.GetString("SmsServiceName");
                    m_SmsServices.SmsServiceDesc = smartReader.GetString("SmsServiceDesc");

                    l_SmsServices.Add(m_SmsServices);
                }
                reader.Close();
                return l_SmsServices;
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
                SqlCommand cmd = new SqlCommand("SmsServices_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceName", this.SmsServiceName));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceDesc", this.SmsServiceDesc));
                cmd.Parameters.Add("@SmsServiceId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SmsServiceId = Convert.ToByte((cmd.Parameters["@SmsServiceId"].Value == null) ? 0 : (cmd.Parameters["@SmsServiceId"].Value));
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
                SqlCommand cmd = new SqlCommand("SmsServices_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceName", this.SmsServiceName));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceDesc", this.SmsServiceDesc));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceId", this.SmsServiceId));
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
                SqlCommand cmd = new SqlCommand("SmsServices_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceId", this.SmsServiceId));
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
        public List<SmsServices> GetList()
        {
            List<SmsServices> RetVal = new List<SmsServices>();
            try
            {
                string sql = "SELECT * FROM SmsServices";
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
        public static List<SmsServices> Static_GetList(string ConStr)
        {
            List<SmsServices> RetVal = new List<SmsServices>();
            try
            {
                SmsServices m_SmsServices = new SmsServices(ConStr);
                RetVal = m_SmsServices.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<SmsServices> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<SmsServices> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            SmsServices m_SmsServices = new SmsServices(ConStr);
            List<SmsServices> RetVal = m_SmsServices.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_SmsServices.SmsServiceName = TextOptionAll;
                m_SmsServices.SmsServiceDesc = TextOptionAll;
                RetVal.Insert(0, m_SmsServices);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SmsServices> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<SmsServices> GetListOrderBy(string OrderBy)
        {
            List<SmsServices> RetVal = new List<SmsServices>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM SmsServices ";
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
        public static List<SmsServices> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            SmsServices m_SmsServices = new SmsServices(ConStr);
            return m_SmsServices.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SmsServices> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SmsServices> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<SmsServices> RetVal = new List<SmsServices>();
            SmsServices m_SmsServices = new SmsServices(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_SmsServices.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_SmsServices.SmsServiceName = TextOptionAll;
                    m_SmsServices.SmsServiceDesc = TextOptionAll;
                    RetVal.Insert(0, m_SmsServices);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SmsServices> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<SmsServices> GetListBySmsServiceId(byte SmsServiceId)
        {
            List<SmsServices> RetVal = new List<SmsServices>();
            try
            {
                if (SmsServiceId > 0)
                {
                    string sql = "SELECT * FROM SmsServices WHERE (SmsServiceId=" + SmsServiceId.ToString() + ")";
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
        public SmsServices Get(byte SmsServiceId)
        {
            SmsServices RetVal = new SmsServices(db.ConnectionString);
            try
            {
                List<SmsServices> list = GetListBySmsServiceId(SmsServiceId);
                if (list.Count > 0)
                {
                    RetVal = (SmsServices)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SmsServices Get()
        {
            return Get(this.SmsServiceId);
        }
        //-------------------------------------------------------------- 
        public static SmsServices Static_Get(byte SmsServiceId)
        {
            return Static_Get(SmsServiceId);
        }
        //-----------------------------------------------------------------------------
        public static SmsServices Static_Get(byte SmsServiceId, List<SmsServices> lList)
        {
            SmsServices RetVal = new SmsServices();
            if (SmsServiceId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.SmsServiceId == SmsServiceId);
                if (RetVal == null) RetVal = new SmsServices();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
