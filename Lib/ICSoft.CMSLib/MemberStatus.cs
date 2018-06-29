
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
    public class MemberStatus
    {
        private byte _MemberStatusId;
        private string _MemberStatusName;
        private string _MemberStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public MemberStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MemberStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MemberStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte MemberStatusId
        {
            get { return _MemberStatusId; }
            set { _MemberStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string MemberStatusName
        {
            get { return _MemberStatusName; }
            set { _MemberStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string MemberStatusDesc
        {
            get { return _MemberStatusDesc; }
            set { _MemberStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<MemberStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MemberStatus> l_MemberStatus = new List<MemberStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MemberStatus m_MemberStatus = new MemberStatus(db.ConnectionString);
                    m_MemberStatus.MemberStatusId = smartReader.GetByte("MemberStatusId");
                    m_MemberStatus.MemberStatusName = smartReader.GetString("MemberStatusName");
                    m_MemberStatus.MemberStatusDesc = smartReader.GetString("MemberStatusDesc");

                    l_MemberStatus.Add(m_MemberStatus);
                }
                reader.Close();
                return l_MemberStatus;
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
                SqlCommand cmd = new SqlCommand("MemberStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberStatusName", this.MemberStatusName));
                cmd.Parameters.Add(new SqlParameter("@MemberStatusDesc", this.MemberStatusDesc));
                cmd.Parameters.Add("@MemberStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MemberStatusId = Convert.ToByte((cmd.Parameters["@MemberStatusId"].Value == null) ? 0 : (cmd.Parameters["@MemberStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("MemberStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberStatusName", this.MemberStatusName));
                cmd.Parameters.Add(new SqlParameter("@MemberStatusDesc", this.MemberStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@MemberStatusId", this.MemberStatusId));
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
                SqlCommand cmd = new SqlCommand("MemberStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberStatusId", this.MemberStatusId));
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
        public List<MemberStatus> GetList()
        {
            List<MemberStatus> RetVal = new List<MemberStatus>();
            try
            {
                string sql = "SELECT * FROM V$MemberStatus";
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
        public static List<MemberStatus> Static_GetList(string ConStr)
        {
            List<MemberStatus> RetVal = new List<MemberStatus>();
            try
            {
                MemberStatus m_MemberStatus = new MemberStatus(ConStr);
                RetVal = m_MemberStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MemberStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<MemberStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            MemberStatus m_MemberStatus = new MemberStatus(ConStr);
            List<MemberStatus> RetVal = m_MemberStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_MemberStatus.MemberStatusName = TextOptionAll;
                m_MemberStatus.MemberStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_MemberStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<MemberStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<MemberStatus> GetListOrderBy(string OrderBy)
        {
            List<MemberStatus> RetVal = new List<MemberStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$MemberStatus ";
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
        public static List<MemberStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            MemberStatus m_MemberStatus = new MemberStatus(ConStr);
            return m_MemberStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MemberStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MemberStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<MemberStatus> RetVal = new List<MemberStatus>();
            MemberStatus m_MemberStatus = new MemberStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_MemberStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_MemberStatus.MemberStatusName = TextOptionAll;
                    m_MemberStatus.MemberStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_MemberStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<MemberStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<MemberStatus> GetListByMemberStatusId(byte MemberStatusId)
        {
            List<MemberStatus> RetVal = new List<MemberStatus>();
            try
            {
                if (MemberStatusId > 0)
                {
                    string sql = "SELECT * FROM V$MemberStatus WHERE (MemberStatusId=" + MemberStatusId.ToString() + ")";
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
        public MemberStatus Get(byte MemberStatusId)
        {
            MemberStatus RetVal = new MemberStatus(db.ConnectionString);
            try
            {
                List<MemberStatus> list = GetListByMemberStatusId(MemberStatusId);
                if (list.Count > 0)
                {
                    RetVal = (MemberStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MemberStatus Get()
        {
            return Get(this.MemberStatusId);
        }
        //-------------------------------------------------------------- 
        public static MemberStatus Static_Get(byte MemberStatusId)
        {
            return Static_Get(MemberStatusId);
        }
        //-----------------------------------------------------------------------------
        public static MemberStatus Static_Get(byte MemberStatusId, List<MemberStatus> lList)
        {
            MemberStatus RetVal = new MemberStatus();
            if (MemberStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.MemberStatusId == MemberStatusId);
                if (RetVal == null) RetVal = new MemberStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte MemberStatusId)
        {
            string RetVal = "";
            MemberStatus m_MemberStatus = new MemberStatus();
            m_MemberStatus = m_MemberStatus.Get(MemberStatusId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == CmsConstants.CULTURE_VN)
            {
                RetVal = m_MemberStatus.MemberStatusDesc;
            }
            else
            {
                RetVal = m_MemberStatus.MemberStatusName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
