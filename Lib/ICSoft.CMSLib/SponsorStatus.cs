
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
    public class SponsorStatus
    {
        private byte _SponsorStatusId;
        private string _SponsorStatusName;
        private string _SponsorStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SponsorStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SponsorStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SponsorStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte SponsorStatusId
        {
            get { return _SponsorStatusId; }
            set { _SponsorStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string SponsorStatusName
        {
            get { return _SponsorStatusName; }
            set { _SponsorStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string SponsorStatusDesc
        {
            get { return _SponsorStatusDesc; }
            set { _SponsorStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<SponsorStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SponsorStatus> l_SponsorStatus = new List<SponsorStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SponsorStatus m_SponsorStatus = new SponsorStatus(db.ConnectionString);
                    m_SponsorStatus.SponsorStatusId = smartReader.GetByte("SponsorStatusId");
                    m_SponsorStatus.SponsorStatusName = smartReader.GetString("SponsorStatusName");
                    m_SponsorStatus.SponsorStatusDesc = smartReader.GetString("SponsorStatusDesc");

                    l_SponsorStatus.Add(m_SponsorStatus);
                }
                reader.Close();
                return l_SponsorStatus;
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
                SqlCommand cmd = new SqlCommand("SponsorStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SponsorStatusName", this.SponsorStatusName));
                cmd.Parameters.Add(new SqlParameter("@SponsorStatusDesc", this.SponsorStatusDesc));
                cmd.Parameters.Add("@SponsorStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SponsorStatusId = Convert.ToByte((cmd.Parameters["@SponsorStatusId"].Value == null) ? 0 : (cmd.Parameters["@SponsorStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("SponsorStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SponsorStatusName", this.SponsorStatusName));
                cmd.Parameters.Add(new SqlParameter("@SponsorStatusDesc", this.SponsorStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@SponsorStatusId", this.SponsorStatusId));
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
                SqlCommand cmd = new SqlCommand("SponsorStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SponsorStatusId", this.SponsorStatusId));
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
        public List<SponsorStatus> GetList()
        {
            List<SponsorStatus> RetVal = new List<SponsorStatus>();
            try
            {
                string sql = "SELECT * FROM V$SponsorStatus";
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
        public static List<SponsorStatus> Static_GetList(string ConStr)
        {
            List<SponsorStatus> RetVal = new List<SponsorStatus>();
            try
            {
                SponsorStatus m_SponsorStatus = new SponsorStatus(ConStr);
                RetVal = m_SponsorStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<SponsorStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<SponsorStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            SponsorStatus m_SponsorStatus = new SponsorStatus(ConStr);
            List<SponsorStatus> RetVal = m_SponsorStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_SponsorStatus.SponsorStatusName = TextOptionAll;
                m_SponsorStatus.SponsorStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_SponsorStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SponsorStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<SponsorStatus> GetListOrderBy(string OrderBy)
        {
            List<SponsorStatus> RetVal = new List<SponsorStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$SponsorStatus ";
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
        public static List<SponsorStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            SponsorStatus m_SponsorStatus = new SponsorStatus(ConStr);
            return m_SponsorStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SponsorStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<SponsorStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<SponsorStatus> RetVal = new List<SponsorStatus>();
            SponsorStatus m_SponsorStatus = new SponsorStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_SponsorStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_SponsorStatus.SponsorStatusName = TextOptionAll;
                    m_SponsorStatus.SponsorStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_SponsorStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<SponsorStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<SponsorStatus> GetListBySponsorStatusId(byte SponsorStatusId)
        {
            List<SponsorStatus> RetVal = new List<SponsorStatus>();
            try
            {
                if (SponsorStatusId > 0)
                {
                    string sql = "SELECT * FROM V$SponsorStatus WHERE (SponsorStatusId=" + SponsorStatusId.ToString() + ")";
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
        public SponsorStatus Get(byte SponsorStatusId)
        {
            SponsorStatus RetVal = new SponsorStatus(db.ConnectionString);
            try
            {
                List<SponsorStatus> list = GetListBySponsorStatusId(SponsorStatusId);
                if (list.Count > 0)
                {
                    RetVal = (SponsorStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SponsorStatus Get()
        {
            return Get(this.SponsorStatusId);
        }
        //-------------------------------------------------------------- 
        public static SponsorStatus Static_Get(byte SponsorStatusId)
        {
            return Static_Get(SponsorStatusId);
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte SponsorStatusId)
        {
            string RetVal = "";
            SponsorStatus m_SponsorStatus = new SponsorStatus();
            m_SponsorStatus = m_SponsorStatus.Get(SponsorStatusId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == CmsConstants.CULTURE_VN)
            {
                RetVal = m_SponsorStatus.SponsorStatusDesc;
            }
            else
            {
                RetVal = m_SponsorStatus.SponsorStatusName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
