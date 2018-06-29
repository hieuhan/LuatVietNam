
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
    public class VoteLogs
    {
        private int _VoteLogId;
        private byte _LanguageId;
        private int _VoteContentId;
        private string _Comment;
        private string _FromIP;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public VoteLogs()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public VoteLogs(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~VoteLogs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int VoteLogId
        {
            get { return _VoteLogId; }
            set { _VoteLogId = value; }
        }
        //-----------------------------------------------------------------
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------
        public int VoteContentId
        {
            get { return _VoteContentId; }
            set { _VoteContentId = value; }
        }
        //-----------------------------------------------------------------
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<VoteLogs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<VoteLogs> l_VoteLogs = new List<VoteLogs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    VoteLogs m_VoteLogs = new VoteLogs(db.ConnectionString);
                    m_VoteLogs.VoteLogId = smartReader.GetInt32("VoteLogId");
                    m_VoteLogs.LanguageId = smartReader.GetByte("LanguageId");
                    m_VoteLogs.VoteContentId = smartReader.GetInt32("VoteContentId");
                    m_VoteLogs.Comment = smartReader.GetString("Comment");
                    m_VoteLogs.FromIP = smartReader.GetString("FromIP");
                    m_VoteLogs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_VoteLogs.Add(m_VoteLogs);
                }
                reader.Close();
                return l_VoteLogs;
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
                SqlCommand cmd = new SqlCommand("VoteLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@VoteContentId", this.VoteContentId));
                cmd.Parameters.Add(new SqlParameter("@Comment", this.Comment));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add("@VoteLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.VoteLogId = Convert.ToInt32((cmd.Parameters["@VoteLogId"].Value == null) ? 0 : (cmd.Parameters["@VoteLogId"].Value));
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
                SqlCommand cmd = new SqlCommand("VoteLogs_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@VoteContentId", this.VoteContentId));
                cmd.Parameters.Add(new SqlParameter("@Comment", this.Comment));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@VoteLogId", this.VoteLogId));
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
                SqlCommand cmd = new SqlCommand("VoteLogs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@VoteLogId", this.VoteLogId));
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
        public List<VoteLogs> GetList()
        {
            List<VoteLogs> RetVal = new List<VoteLogs>();
            try
            {
                string sql = "SELECT * FROM V$VoteLogs";
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
        public static List<VoteLogs> Static_GetList(string ConStr)
        {
            List<VoteLogs> RetVal = new List<VoteLogs>();
            try
            {
                VoteLogs m_VoteLogs = new VoteLogs(ConStr);
                RetVal = m_VoteLogs.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<VoteLogs> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<VoteLogs> GetListOrderBy(string OrderBy)
        {
            List<VoteLogs> RetVal = new List<VoteLogs>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$VoteLogs ";
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
        public static List<VoteLogs> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            VoteLogs m_VoteLogs = new VoteLogs(ConStr);
            return m_VoteLogs.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<VoteLogs> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<VoteLogs> GetListByVoteLogId(int VoteLogId)
        {
            List<VoteLogs> RetVal = new List<VoteLogs>();
            try
            {
                if (VoteLogId > 0)
                {
                    string sql = "SELECT * FROM V$VoteLogs WHERE (VoteLogId=" + VoteLogId.ToString() + ")";
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
        public VoteLogs Get(int VoteLogId)
        {
            VoteLogs RetVal = new VoteLogs(db.ConnectionString);
            try
            {
                List<VoteLogs> list = GetListByVoteLogId(VoteLogId);
                if (list.Count > 0)
                {
                    RetVal = (VoteLogs)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public VoteLogs Get()
        {
            return Get(this.VoteLogId);
        }
        //-------------------------------------------------------------- 
        public static VoteLogs Static_Get(int VoteLogId)
        {
            return Static_Get(VoteLogId);
        }
        //--------------------------------------------------------------     
        
    }
}