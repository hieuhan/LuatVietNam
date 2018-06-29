
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
    public class VoteTypes
    {
        private byte _VoteTypeId;
        private string _VoteTypeName;
        private string _VoteTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public VoteTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public VoteTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~VoteTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte VoteTypeId
        {
            get { return _VoteTypeId; }
            set { _VoteTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string VoteTypeName
        {
            get { return _VoteTypeName; }
            set { _VoteTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string VoteTypeDesc
        {
            get { return _VoteTypeDesc; }
            set { _VoteTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<VoteTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<VoteTypes> l_VoteTypes = new List<VoteTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    VoteTypes m_VoteTypes = new VoteTypes(db.ConnectionString);
                    m_VoteTypes.VoteTypeId = smartReader.GetByte("VoteTypeId");
                    m_VoteTypes.VoteTypeName = smartReader.GetString("VoteTypeName");
                    m_VoteTypes.VoteTypeDesc = smartReader.GetString("VoteTypeDesc");

                    l_VoteTypes.Add(m_VoteTypes);
                }
                reader.Close();
                return l_VoteTypes;
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
                SqlCommand cmd = new SqlCommand("VoteTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@VoteTypeName", this.VoteTypeName));
                cmd.Parameters.Add(new SqlParameter("@VoteTypeDesc", this.VoteTypeDesc));
                cmd.Parameters.Add("@VoteTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.VoteTypeId = Convert.ToByte((cmd.Parameters["@VoteTypeId"].Value == null) ? 0 : (cmd.Parameters["@VoteTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("VoteTypes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@VoteTypeName", this.VoteTypeName));
                cmd.Parameters.Add(new SqlParameter("@VoteTypeDesc", this.VoteTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@VoteTypeId", this.VoteTypeId));
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
                SqlCommand cmd = new SqlCommand("VoteTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@VoteTypeId", this.VoteTypeId));
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
        public List<VoteTypes> GetList()
        {
            List<VoteTypes> RetVal = new List<VoteTypes>();
            try
            {
                string sql = "SELECT * FROM V$VoteTypes";
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
        public static List<VoteTypes> Static_GetList(string ConStr)
        {
            List<VoteTypes> RetVal = new List<VoteTypes>();
            try
            {
                VoteTypes m_VoteTypes = new VoteTypes(ConStr);
                RetVal = m_VoteTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<VoteTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<VoteTypes> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            VoteTypes m_VoteTypes = new VoteTypes(ConStr);
            List<VoteTypes> RetVal = m_VoteTypes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_VoteTypes.VoteTypeName = TextOptionAll;
                m_VoteTypes.VoteTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_VoteTypes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<VoteTypes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<VoteTypes> GetListOrderBy(string OrderBy)
        {
            List<VoteTypes> RetVal = new List<VoteTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$VoteTypes ";
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
        public static List<VoteTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            VoteTypes m_VoteTypes = new VoteTypes(ConStr);
            return m_VoteTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<VoteTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<VoteTypes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<VoteTypes> RetVal = new List<VoteTypes>();
            VoteTypes m_VoteTypes = new VoteTypes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_VoteTypes.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_VoteTypes.VoteTypeName = TextOptionAll;
                    m_VoteTypes.VoteTypeDesc = TextOptionAll;
                    RetVal.Insert(0, m_VoteTypes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<VoteTypes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<VoteTypes> GetListByVoteTypeId(byte VoteTypeId)
        {
            List<VoteTypes> RetVal = new List<VoteTypes>();
            try
            {
                if (VoteTypeId > 0)
                {
                    string sql = "SELECT * FROM V$VoteTypes WHERE (VoteTypeId=" + VoteTypeId.ToString() + ")";
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
        public VoteTypes Get(byte VoteTypeId)
        {
            VoteTypes RetVal = new VoteTypes(db.ConnectionString);
            try
            {
                List<VoteTypes> list = GetListByVoteTypeId(VoteTypeId);
                if (list.Count > 0)
                {
                    RetVal = (VoteTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public VoteTypes Get()
        {
            return Get(this.VoteTypeId);
        }
        //-------------------------------------------------------------- 
        public static VoteTypes Static_Get(byte VoteTypeId)
        {
            return Static_Get(VoteTypeId);
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte VoteTypesId)
        {
            string RetVal = "";
            VoteTypes m_VoteTypes = new VoteTypes();
            m_VoteTypes = m_VoteTypes.Get(VoteTypesId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == CmsConstants.CULTURE_VN)
            {
                RetVal = m_VoteTypes.VoteTypeDesc;
            }
            else
            {
                RetVal = m_VoteTypes.VoteTypeName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
