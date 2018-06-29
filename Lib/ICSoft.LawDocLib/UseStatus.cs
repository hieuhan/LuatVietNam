
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
    public class UseStatus
    {
        private byte _UseStatusId;
        private string _UseStatusName;
        private string _UseStatusDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public UseStatus()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public UseStatus(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~UseStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte UseStatusId
        {
            get { return _UseStatusId; }
            set { _UseStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string UseStatusName
        {
            get { return _UseStatusName; }
            set { _UseStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string UseStatusDesc
        {
            get { return _UseStatusDesc; }
            set { _UseStatusDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<UseStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<UseStatus> l_UseStatus = new List<UseStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    UseStatus m_UseStatus = new UseStatus(db.ConnectionString);
                    m_UseStatus.UseStatusId = smartReader.GetByte("UseStatusId");
                    m_UseStatus.UseStatusName = smartReader.GetString("UseStatusName");
                    m_UseStatus.UseStatusDesc = smartReader.GetString("UseStatusDesc");
                    m_UseStatus.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_UseStatus.Add(m_UseStatus);
                }
                reader.Close();
                return l_UseStatus;
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("UseStatus_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusName", this.UseStatusName));
                cmd.Parameters.Add(new SqlParameter("@UseStatusDesc", this.UseStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", this.UseStatusId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.UseStatusId = Convert.ToByte((cmd.Parameters["@UseStatusId"].Value == null) ? 0 : (cmd.Parameters["@UseStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("UseStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@UseStatusId", this.UseStatusId));
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
        public List<UseStatus> GetList()
        {
            List<UseStatus> RetVal = new List<UseStatus>();
            try
            {
                string sql = "SELECT * FROM V$UseStatus";
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
        public static List<UseStatus> Static_GetList(string ConStr)
        {
            List<UseStatus> RetVal = new List<UseStatus>();
            try
            {
                UseStatus m_UseStatus = new UseStatus(ConStr);
                RetVal = m_UseStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<UseStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<UseStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            UseStatus m_UseStatus = new UseStatus(ConStr);
            List<UseStatus> RetVal = m_UseStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_UseStatus.UseStatusName = TextOptionAll;
                m_UseStatus.UseStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_UseStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<UseStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<UseStatus> GetListOrderBy(string OrderBy)
        {
            List<UseStatus> RetVal = new List<UseStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$UseStatus ";
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
        public static List<UseStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            UseStatus m_UseStatus = new UseStatus(ConStr);
            return m_UseStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<UseStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<UseStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<UseStatus> RetVal = new List<UseStatus>();
            UseStatus m_UseStatus = new UseStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_UseStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_UseStatus.UseStatusName = TextOptionAll;
                    m_UseStatus.UseStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_UseStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<UseStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
       //--------------------------------------------------------------  
        public List<UseStatus> GetListByUseStatusId(byte UseStatusId)
        {
            List<UseStatus> RetVal = new List<UseStatus>();
            try
            {
                if (UseStatusId > 0)
                {
                    string sql = "SELECT * FROM V$UseStatus WHERE (UseStatusId=" + UseStatusId.ToString() + ")";
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
        public UseStatus Get(byte UseStatusId)
        {
            UseStatus RetVal = new UseStatus(db.ConnectionString);
            try
            {
                List<UseStatus> list = GetListByUseStatusId(UseStatusId);
                if (list.Count > 0)
                {
                    RetVal = (UseStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public UseStatus Get()
        {
            return Get(this.UseStatusId);
        }
        //-------------------------------------------------------------- 
        public static UseStatus Static_Get(byte UseStatusId)
        {
            return Static_Get(UseStatusId);
        }
        //-----------------------------------------------------------------------------
        public static UseStatus Static_Get(byte UseStatusId, List<UseStatus> lList)
        {
            UseStatus RetVal = new UseStatus();
            if (UseStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.UseStatusId == UseStatusId);
                if (RetVal == null) RetVal = new UseStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
       
    }
}
