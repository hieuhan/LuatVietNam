
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
    public class NewsletterMemberStatus
    {
        private byte _NewsletterMemberStatusId;
        private string _NewsletterMemberStatusName;
        private string _NewsletterMemberStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public NewsletterMemberStatus()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public NewsletterMemberStatus(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~NewsletterMemberStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte NewsletterMemberStatusId
        {
            get { return _NewsletterMemberStatusId; }
            set { _NewsletterMemberStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string NewsletterMemberStatusName
        {
            get { return _NewsletterMemberStatusName; }
            set { _NewsletterMemberStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string NewsletterMemberStatusDesc
        {
            get { return _NewsletterMemberStatusDesc; }
            set { _NewsletterMemberStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<NewsletterMemberStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<NewsletterMemberStatus> l_NewsletterMemberStatus = new List<NewsletterMemberStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    NewsletterMemberStatus m_NewsletterMemberStatus = new NewsletterMemberStatus(db.ConnectionString);
                    m_NewsletterMemberStatus.NewsletterMemberStatusId = smartReader.GetByte("NewsletterMemberStatusId");
                    m_NewsletterMemberStatus.NewsletterMemberStatusName = smartReader.GetString("NewsletterMemberStatusName");
                    m_NewsletterMemberStatus.NewsletterMemberStatusDesc = smartReader.GetString("NewsletterMemberStatusDesc");

                    l_NewsletterMemberStatus.Add(m_NewsletterMemberStatus);
                }
                reader.Close();
                return l_NewsletterMemberStatus;
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
                SqlCommand cmd = new SqlCommand("NewsletterMemberStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberStatusName", this.NewsletterMemberStatusName));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberStatusDesc", this.NewsletterMemberStatusDesc));
                cmd.Parameters.Add("@NewsletterMemberStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.NewsletterMemberStatusId = Convert.ToByte((cmd.Parameters["@NewsletterMemberStatusId"].Value == null) ? 0 : (cmd.Parameters["@NewsletterMemberStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("NewsletterMemberStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberStatusName", this.NewsletterMemberStatusName));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberStatusDesc", this.NewsletterMemberStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberStatusId", this.NewsletterMemberStatusId));
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
                SqlCommand cmd = new SqlCommand("NewsletterMemberStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterMemberStatusId", this.NewsletterMemberStatusId));
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
        public List<NewsletterMemberStatus> GetList()
        {
            List<NewsletterMemberStatus> RetVal = new List<NewsletterMemberStatus>();
            try
            {
                string sql = "SELECT * FROM NewsletterMemberStatus";
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
        public static List<NewsletterMemberStatus> Static_GetList(string ConStr)
        {
            List<NewsletterMemberStatus> RetVal = new List<NewsletterMemberStatus>();
            try
            {
                NewsletterMemberStatus m_NewsletterMemberStatus = new NewsletterMemberStatus(ConStr);
                RetVal = m_NewsletterMemberStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<NewsletterMemberStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<NewsletterMemberStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            NewsletterMemberStatus m_NewsletterMemberStatus = new NewsletterMemberStatus(ConStr);
            List<NewsletterMemberStatus> RetVal = m_NewsletterMemberStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_NewsletterMemberStatus.NewsletterMemberStatusName = TextOptionAll;
                m_NewsletterMemberStatus.NewsletterMemberStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_NewsletterMemberStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<NewsletterMemberStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<NewsletterMemberStatus> GetListOrderBy(string OrderBy)
        {
            List<NewsletterMemberStatus> RetVal = new List<NewsletterMemberStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM NewsletterMemberStatus ";
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
        public static List<NewsletterMemberStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            NewsletterMemberStatus m_NewsletterMemberStatus = new NewsletterMemberStatus(ConStr);
            return m_NewsletterMemberStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<NewsletterMemberStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<NewsletterMemberStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<NewsletterMemberStatus> RetVal = new List<NewsletterMemberStatus>();
            NewsletterMemberStatus m_NewsletterMemberStatus = new NewsletterMemberStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_NewsletterMemberStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_NewsletterMemberStatus.NewsletterMemberStatusName = TextOptionAll;
                    m_NewsletterMemberStatus.NewsletterMemberStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_NewsletterMemberStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<NewsletterMemberStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<NewsletterMemberStatus> GetListByNewsletterMemberStatusId(byte NewsletterMemberStatusId)
        {
            List<NewsletterMemberStatus> RetVal = new List<NewsletterMemberStatus>();
            try
            {
                if (NewsletterMemberStatusId > 0)
                {
                    string sql = "SELECT * FROM NewsletterMemberStatus WHERE (NewsletterMemberStatusId=" + NewsletterMemberStatusId.ToString() + ")";
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
        public NewsletterMemberStatus Get(byte NewsletterMemberStatusId)
        {
            NewsletterMemberStatus RetVal = new NewsletterMemberStatus(db.ConnectionString);
            try
            {
                List<NewsletterMemberStatus> list = GetListByNewsletterMemberStatusId(NewsletterMemberStatusId);
                if (list.Count > 0)
                {
                    RetVal = (NewsletterMemberStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public NewsletterMemberStatus Get()
        {
            return Get(this.NewsletterMemberStatusId);
        }
        //-------------------------------------------------------------- 
        public static NewsletterMemberStatus Static_Get(byte NewsletterMemberStatusId)
        {
            return new NewsletterMemberStatus().Get(NewsletterMemberStatusId);
        }
        //-----------------------------------------------------------------------------
        public static NewsletterMemberStatus Static_Get(byte NewsletterMemberStatusId, List<NewsletterMemberStatus> lList)
        {
            NewsletterMemberStatus RetVal = new NewsletterMemberStatus();
            if (NewsletterMemberStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.NewsletterMemberStatusId == NewsletterMemberStatusId);
                if (RetVal == null) RetVal = new NewsletterMemberStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
