
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
    public class ActionTypes
    {
        private byte _ActionTypeId;
        private string _ActionTypeName;
        private string _ActionTypeDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ActionTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ActionTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ActionTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte ActionTypeId
        {
            get { return _ActionTypeId; }
            set { _ActionTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string ActionTypeName
        {
            get { return _ActionTypeName; }
            set { _ActionTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string ActionTypeDesc
        {
            get { return _ActionTypeDesc; }
            set { _ActionTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<ActionTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ActionTypes> l_ActionTypes = new List<ActionTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ActionTypes m_ActionTypes = new ActionTypes(db.ConnectionString);
                    m_ActionTypes.ActionTypeId = smartReader.GetByte("ActionTypeId");
                    m_ActionTypes.ActionTypeName = smartReader.GetString("ActionTypeName");
                    m_ActionTypes.ActionTypeDesc = smartReader.GetString("ActionTypeDesc");
                    m_ActionTypes.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_ActionTypes.Add(m_ActionTypes);
                }
                reader.Close();
                return l_ActionTypes;
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
                SqlCommand cmd = new SqlCommand("ActionTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeName", this.ActionTypeName));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeDesc", this.ActionTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@ActionTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ActionTypeId = Convert.ToByte((cmd.Parameters["@ActionTypeId"].Value == null) ? 0 : (cmd.Parameters["@ActionTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("ActionTypes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeName", this.ActionTypeName));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeDesc", this.ActionTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", this.ActionTypeId));
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
                SqlCommand cmd = new SqlCommand("ActionTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", this.ActionTypeId));
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
        public List<ActionTypes> GetList()
        {
            List<ActionTypes> RetVal = new List<ActionTypes>();
            try
            {
                string sql = "SELECT * FROM V$ActionTypes";
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
        public static List<ActionTypes> Static_GetList(string ConStr)
        {
            List<ActionTypes> RetVal = new List<ActionTypes>();
            try
            {
                ActionTypes m_ActionTypes = new ActionTypes(ConStr);
                RetVal = m_ActionTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ActionTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<ActionTypes> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            ActionTypes m_ActionTypes = new ActionTypes(ConStr);
            List<ActionTypes> RetVal = m_ActionTypes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_ActionTypes.ActionTypeName = TextOptionAll;
                m_ActionTypes.ActionTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_ActionTypes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ActionTypes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<ActionTypes> GetListOrderBy(string OrderBy)
        {
            List<ActionTypes> RetVal = new List<ActionTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$ActionTypes ";
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
        public static List<ActionTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            ActionTypes m_ActionTypes = new ActionTypes(ConStr);
            return m_ActionTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ActionTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<ActionTypes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<ActionTypes> RetVal = new List<ActionTypes>();
            ActionTypes m_ActionTypes = new ActionTypes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_ActionTypes.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_ActionTypes.ActionTypeName = TextOptionAll;
                    m_ActionTypes.ActionTypeDesc = TextOptionAll;
                    RetVal.Insert(0, m_ActionTypes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<ActionTypes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<ActionTypes> GetListByActionTypeId(byte ActionTypeId)
        {
            List<ActionTypes> RetVal = new List<ActionTypes>();
            try
            {
                if (ActionTypeId > 0)
                {
                    string sql = "SELECT * FROM V$ActionTypes WHERE (ActionTypeId=" + ActionTypeId.ToString() + ")";
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
        public ActionTypes Get(byte ActionTypeId)
        {
            ActionTypes RetVal = new ActionTypes(db.ConnectionString);
            try
            {
                List<ActionTypes> list = GetListByActionTypeId(ActionTypeId);
                if (list.Count > 0)
                {
                    RetVal = (ActionTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ActionTypes Get()
        {
            return Get(this.ActionTypeId);
        }
        //-------------------------------------------------------------- 
        public static ActionTypes Static_Get(byte ActionTypeId)
        {
            return Static_Get(ActionTypeId);
        }
        //-----------------------------------------------------------------------------
        public static ActionTypes Static_Get(byte ActionTypeId, List<ActionTypes> lList)
        {
            ActionTypes RetVal = new ActionTypes();
            if (ActionTypeId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.ActionTypeId == ActionTypeId);
                if (RetVal == null) RetVal = new ActionTypes();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        
    }
}