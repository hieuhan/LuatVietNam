
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
    public class RateTypes
    {
        private byte _RateTypeId;
        private string _RateTypeName;
        private string _RateTypeDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public RateTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public RateTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~RateTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte RateTypeId
        {
            get { return _RateTypeId; }
            set { _RateTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string RateTypeName
        {
            get { return _RateTypeName; }
            set { _RateTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string RateTypeDesc
        {
            get { return _RateTypeDesc; }
            set { _RateTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<RateTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<RateTypes> l_RateTypes = new List<RateTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RateTypes m_RateTypes = new RateTypes(db.ConnectionString);
                    m_RateTypes.RateTypeId = smartReader.GetByte("RateTypeId");
                    m_RateTypes.RateTypeName = smartReader.GetString("RateTypeName");
                    m_RateTypes.RateTypeDesc = smartReader.GetString("RateTypeDesc");
                    m_RateTypes.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_RateTypes.Add(m_RateTypes);
                }
                reader.Close();
                return l_RateTypes;
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
                SqlCommand cmd = new SqlCommand("RateTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RateTypeName", this.RateTypeName));
                cmd.Parameters.Add(new SqlParameter("@RateTypeDesc", this.RateTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@RateTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.RateTypeId = Convert.ToByte((cmd.Parameters["@RateTypeId"].Value == null) ? 0 : (cmd.Parameters["@RateTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("RateTypes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RateTypeName", this.RateTypeName));
                cmd.Parameters.Add(new SqlParameter("@RateTypeDesc", this.RateTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@RateTypeId", this.RateTypeId));
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
                SqlCommand cmd = new SqlCommand("RateTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RateTypeId", this.RateTypeId));
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
        public List<RateTypes> GetList()
        {
            List<RateTypes> RetVal = new List<RateTypes>();
            try
            {
                string sql = "SELECT * FROM V$RateTypes";
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
        public static List<RateTypes> Static_GetList(string ConStr)
        {
            List<RateTypes> RetVal = new List<RateTypes>();
            try
            {
                RateTypes m_RateTypes = new RateTypes(ConStr);
                RetVal = m_RateTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<RateTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<RateTypes> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            RateTypes m_RateTypes = new RateTypes(ConStr);
            List<RateTypes> RetVal = m_RateTypes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_RateTypes.RateTypeName = TextOptionAll;
                m_RateTypes.RateTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_RateTypes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<RateTypes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<RateTypes> GetListOrderBy(string OrderBy)
        {
            List<RateTypes> RetVal = new List<RateTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$RateTypes ";
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
        public static List<RateTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            RateTypes m_RateTypes = new RateTypes(ConStr);
            return m_RateTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<RateTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<RateTypes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<RateTypes> RetVal = new List<RateTypes>();
            RateTypes m_RateTypes = new RateTypes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_RateTypes.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_RateTypes.RateTypeName = TextOptionAll;
                    m_RateTypes.RateTypeDesc = TextOptionAll;
                    RetVal.Insert(0, m_RateTypes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<RateTypes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<RateTypes> GetListByRateTypeId(byte RateTypeId)
        {
            List<RateTypes> RetVal = new List<RateTypes>();
            try
            {
                if (RateTypeId > 0)
                {
                    string sql = "SELECT * FROM V$RateTypes WHERE (RateTypeId=" + RateTypeId.ToString() + ")";
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
        public RateTypes Get(byte RateTypeId)
        {
            RateTypes RetVal = new RateTypes(db.ConnectionString);
            try
            {
                List<RateTypes> list = GetListByRateTypeId(RateTypeId);
                if (list.Count > 0)
                {
                    RetVal = (RateTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public RateTypes Get()
        {
            return Get(this.RateTypeId);
        }
        //-------------------------------------------------------------- 
        public static RateTypes Static_Get(byte RateTypeId)
        {
            return Static_Get(RateTypeId);
        }
        //--------------------------------------------------------------
    }
}
