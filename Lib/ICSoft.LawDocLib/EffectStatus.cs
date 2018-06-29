
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
    /// <summary>
    /// class EffectStatus
    /// </summary>
    public class EffectStatus
    {
        private byte _EffectStatusId;
        private string _EffectStatusName;
        private string _EffectStatusDesc;
        private byte _DisplayOrder;
        private int _SoLuong;
        private DBAccess db;
        //-----------------------------------------------------------------
        public EffectStatus()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public EffectStatus(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~EffectStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte EffectStatusId
        {
            get { return _EffectStatusId; }
            set { _EffectStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string EffectStatusName
        {
            get { return _EffectStatusName; }
            set { _EffectStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string EffectStatusDesc
        {
            get { return _EffectStatusDesc; }
            set { _EffectStatusDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------    
        public int SoLuong
        {
            get { return _SoLuong; }
            set { _SoLuong = value; }
        }
        //-----------------------------------------------------------------

        private List<EffectStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<EffectStatus> l_EffectStatus = new List<EffectStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    EffectStatus m_EffectStatus = new EffectStatus(db.ConnectionString);
                    m_EffectStatus.EffectStatusId = smartReader.GetByte("EffectStatusId");
                    m_EffectStatus.EffectStatusName = smartReader.GetString("EffectStatusName");
                    m_EffectStatus.EffectStatusDesc = smartReader.GetString("EffectStatusDesc");
                    m_EffectStatus.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_EffectStatus.Add(m_EffectStatus);
                }
                reader.Close();
                return l_EffectStatus;
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
                SqlCommand cmd = new SqlCommand("EffectStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusName", this.EffectStatusName));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusDesc", this.EffectStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@EffectStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.EffectStatusId = Convert.ToByte((cmd.Parameters["@EffectStatusId"].Value == null) ? 0 : (cmd.Parameters["@EffectStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("EffectStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusName", this.EffectStatusName));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusDesc", this.EffectStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", this.EffectStatusId));
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
                SqlCommand cmd = new SqlCommand("EffectStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", this.EffectStatusId));
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
        public List<EffectStatus> GetList()
        {
            List<EffectStatus> RetVal = new List<EffectStatus>();
            try
            {
                string sql = "SELECT * FROM V$EffectStatus ORDER BY DisplayOrder asc ";
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
        public static List<EffectStatus> Static_GetList(string ConStr)
        {
            List<EffectStatus> RetVal = new List<EffectStatus>();
            try
            {
                EffectStatus m_EffectStatus = new EffectStatus(ConStr);
                RetVal = m_EffectStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<EffectStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<EffectStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            EffectStatus m_EffectStatus = new EffectStatus(ConStr);
            List<EffectStatus> RetVal = m_EffectStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_EffectStatus.EffectStatusName = TextOptionAll;
                m_EffectStatus.EffectStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_EffectStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<EffectStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<EffectStatus> GetListOrderBy(string OrderBy)
        {
            List<EffectStatus> RetVal = new List<EffectStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$EffectStatus ";
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
        public static List<EffectStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            EffectStatus m_EffectStatus = new EffectStatus(ConStr);
            return m_EffectStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<EffectStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<EffectStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<EffectStatus> RetVal = new List<EffectStatus>();
            EffectStatus m_EffectStatus = new EffectStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_EffectStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_EffectStatus.EffectStatusName = TextOptionAll;
                    m_EffectStatus.EffectStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_EffectStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<EffectStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<EffectStatus> GetListByEffectStatusId(byte EffectStatusId)
        {
            List<EffectStatus> RetVal = new List<EffectStatus>();
            try
            {
                if (EffectStatusId > 0)
                {
                    string sql = "SELECT * FROM V$EffectStatus WHERE (EffectStatusId=" + EffectStatusId.ToString() + ")";
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
        public EffectStatus Get(byte EffectStatusId)
        {
            EffectStatus RetVal = new EffectStatus(db.ConnectionString);
            try
            {
                List<EffectStatus> list = GetListByEffectStatusId(EffectStatusId);
                if (list.Count > 0)
                {
                    RetVal = (EffectStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public EffectStatus Get()
        {
            return Get(this.EffectStatusId);
        }
        //-------------------------------------------------------------- 
        public static EffectStatus Static_Get(byte EffectStatusId)
        {
            EffectStatus m_EffectStatus = new EffectStatus();

            return m_EffectStatus.Get(EffectStatusId);
        }
        //-----------------------------------------------------------------------------
        public static EffectStatus Static_Get(byte EffectStatusId, List<EffectStatus> lList)
        {
            EffectStatus RetVal = new EffectStatus();
            if (EffectStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.EffectStatusId == EffectStatusId);
                if (RetVal == null) RetVal = new EffectStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}