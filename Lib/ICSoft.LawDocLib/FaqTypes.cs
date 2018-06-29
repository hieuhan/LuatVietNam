
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
    public class FaqTypes
    {
        private byte _FaqTypeId;
        private string _FaqTypeName;
        private string _FaqTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public FaqTypes()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public FaqTypes(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FaqTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte FaqTypeId
        {
            get { return _FaqTypeId; }
            set { _FaqTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string FaqTypeName
        {
            get { return _FaqTypeName; }
            set { _FaqTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string FaqTypeDesc
        {
            get { return _FaqTypeDesc; }
            set { _FaqTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<FaqTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FaqTypes> l_FaqTypes = new List<FaqTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FaqTypes m_FaqTypes = new FaqTypes(db.ConnectionString);
                    m_FaqTypes.FaqTypeId = smartReader.GetByte("FaqTypeId");
                    m_FaqTypes.FaqTypeName = smartReader.GetString("FaqTypeName");
                    m_FaqTypes.FaqTypeDesc = smartReader.GetString("FaqTypeDesc");

                    l_FaqTypes.Add(m_FaqTypes);
                }
                reader.Close();
                return l_FaqTypes;
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
                SqlCommand cmd = new SqlCommand("FaqTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqTypeName", this.FaqTypeName));
                cmd.Parameters.Add(new SqlParameter("@FaqTypeDesc", this.FaqTypeDesc));
                cmd.Parameters.Add("@FaqTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FaqTypeId = Convert.ToByte((cmd.Parameters["@FaqTypeId"].Value == null) ? 0 : (cmd.Parameters["@FaqTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("FaqTypes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqTypeName", this.FaqTypeName));
                cmd.Parameters.Add(new SqlParameter("@FaqTypeDesc", this.FaqTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@FaqTypeId", this.FaqTypeId));
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
                SqlCommand cmd = new SqlCommand("FaqTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqTypeId", this.FaqTypeId));
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
        public List<FaqTypes> GetList()
        {
            List<FaqTypes> RetVal = new List<FaqTypes>();
            try
            {
                string sql = "SELECT * FROM V$FaqTypes";
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
        public static List<FaqTypes> Static_GetList(string ConStr)
        {
            List<FaqTypes> RetVal = new List<FaqTypes>();
            try
            {
                FaqTypes m_FaqTypes = new FaqTypes(ConStr);
                RetVal = m_FaqTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<FaqTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<FaqTypes> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            FaqTypes m_FaqTypes = new FaqTypes(ConStr);
            List<FaqTypes> RetVal = m_FaqTypes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_FaqTypes.FaqTypeName = TextOptionAll;
                m_FaqTypes.FaqTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_FaqTypes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<FaqTypes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<FaqTypes> GetListOrderBy(string OrderBy)
        {
            List<FaqTypes> RetVal = new List<FaqTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$FaqTypes ";
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
        public static List<FaqTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            FaqTypes m_FaqTypes = new FaqTypes(ConStr);
            return m_FaqTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<FaqTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<FaqTypes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<FaqTypes> RetVal = new List<FaqTypes>();
            FaqTypes m_FaqTypes = new FaqTypes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_FaqTypes.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_FaqTypes.FaqTypeName = TextOptionAll;
                    m_FaqTypes.FaqTypeDesc = TextOptionAll;
                    RetVal.Insert(0, m_FaqTypes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<FaqTypes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<FaqTypes> GetListByFaqTypeId(byte FaqTypeId)
        {
            List<FaqTypes> RetVal = new List<FaqTypes>();
            try
            {
                if (FaqTypeId > 0)
                {
                    string sql = "SELECT * FROM V$FaqTypes WHERE (FaqTypeId=" + FaqTypeId.ToString() + ")";
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
        public FaqTypes Get(byte FaqTypeId)
        {
            FaqTypes RetVal = new FaqTypes(db.ConnectionString);
            try
            {
                List<FaqTypes> list = GetListByFaqTypeId(FaqTypeId);
                if (list.Count > 0)
                {
                    RetVal = (FaqTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public FaqTypes Get()
        {
            return Get(this.FaqTypeId);
        }
        //-------------------------------------------------------------- 
        public static FaqTypes Static_Get(byte FaqTypeId)
        {
            return Static_Get(FaqTypeId);
        }
        //-----------------------------------------------------------------------------
        public static FaqTypes Static_Get(byte FaqTypeId, List<FaqTypes> lList)
        {
            FaqTypes RetVal = new FaqTypes();
            if (FaqTypeId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.FaqTypeId == FaqTypeId);
                if (RetVal == null) RetVal = new FaqTypes();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
