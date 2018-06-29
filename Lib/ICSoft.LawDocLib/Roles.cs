
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
    public class Roles
    {
        private byte _RoleId;
        private string _RoleName;
        private string _RoleDesc;
        private string _RoleCode;
        private byte _CheckTimeService;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Roles()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Roles(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Roles()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        //-----------------------------------------------------------------
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        //-----------------------------------------------------------------
        public string RoleDesc
        {
            get { return _RoleDesc; }
            set { _RoleDesc = value; }
        }
        //-----------------------------------------------------------------
        public string RoleCode
        {
            get { return _RoleCode; }
            set { _RoleCode = value; }
        }
        //-----------------------------------------------------------------
        public byte CheckTimeService
        {
            get { return _CheckTimeService; }
            set { _CheckTimeService = value; }
        }
        //-----------------------------------------------------------------

        private List<Roles> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Roles> l_Roles = new List<Roles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Roles m_Roles = new Roles(db.ConnectionString);
                    m_Roles.RoleId = smartReader.GetByte("RoleId");
                    m_Roles.RoleName = smartReader.GetString("RoleName");
                    m_Roles.RoleDesc = smartReader.GetString("RoleDesc");
                    m_Roles.RoleCode = smartReader.GetString("RoleCode");
                    m_Roles.CheckTimeService = smartReader.GetByte("CheckTimeService");

                    l_Roles.Add(m_Roles);
                }
                reader.Close();
                return l_Roles;
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
                SqlCommand cmd = new SqlCommand("Roles_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RoleName", this.RoleName));
                cmd.Parameters.Add(new SqlParameter("@RoleDesc", this.RoleDesc));
                cmd.Parameters.Add(new SqlParameter("@RoleCode", this.RoleCode));
                cmd.Parameters.Add(new SqlParameter("@CheckTimeService", this.CheckTimeService));
                cmd.Parameters.Add("@RoleId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.RoleId = Convert.ToByte((cmd.Parameters["@RoleId"].Value == null) ? 0 : (cmd.Parameters["@RoleId"].Value));
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
                SqlCommand cmd = new SqlCommand("Roles_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RoleName", this.RoleName));
                cmd.Parameters.Add(new SqlParameter("@RoleDesc", this.RoleDesc));
                cmd.Parameters.Add(new SqlParameter("@RoleCode", this.RoleCode));
                cmd.Parameters.Add(new SqlParameter("@CheckTimeService", this.CheckTimeService));
                cmd.Parameters.Add(new SqlParameter("@RoleId", this.RoleId));
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
                SqlCommand cmd = new SqlCommand("Roles_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RoleId", this.RoleId));
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
        public List<Roles> GetList()
        {
            List<Roles> RetVal = new List<Roles>();
            try
            {
                string sql = "SELECT * FROM V$Roles";
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
        public static List<Roles> Static_GetList(string ConStr)
        {
            List<Roles> RetVal = new List<Roles>();
            try
            {
                Roles m_Roles = new Roles(ConStr);
                RetVal = m_Roles.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Roles> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<Roles> GetListOrderBy(string OrderBy)
        {
            List<Roles> RetVal = new List<Roles>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Roles ";
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
        public static List<Roles> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Roles m_Roles = new Roles(ConStr);
            return m_Roles.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Roles> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Roles> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<Roles> RetVal = new List<Roles>();
            Roles m_Roles = new Roles(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_Roles.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_Roles.RoleName = TextOptionAll;
                    m_Roles.RoleDesc = TextOptionAll;
                    RetVal.Insert(0, m_Roles);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Roles> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<Roles> GetListByRoleId(byte RoleId)
        {
            List<Roles> RetVal = new List<Roles>();
            try
            {
                if (RoleId > 0)
                {
                    string sql = "SELECT * FROM V$Roles WHERE (RoleId=" + RoleId.ToString() + ")";
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
        public Roles Get(byte RoleId)
        {
            Roles RetVal = new Roles(db.ConnectionString);
            try
            {
                List<Roles> list = GetListByRoleId(RoleId);
                if (list.Count > 0)
                {
                    RetVal = (Roles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Roles Get()
        {
            return Get(this.RoleId);
        }
        //-------------------------------------------------------------- 
        public static Roles Static_Get(byte RoleId)
        {
            return Static_Get(RoleId);
        }
        //-----------------------------------------------------------------------------
        public static Roles Static_Get(byte RoleId, List<Roles> lList)
        {
            Roles RetVal = new Roles();
            if (RoleId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.RoleId == RoleId);
                if (RetVal == null) RetVal = new Roles();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}