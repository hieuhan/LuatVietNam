
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
    public class NewsletterStatus
    {
        private byte _NewsletterStatusId;
        private string _NewsletterStatusName;
        private string _NewsletterStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public NewsletterStatus()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public NewsletterStatus(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~NewsletterStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte NewsletterStatusId
        {
            get { return _NewsletterStatusId; }
            set { _NewsletterStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string NewsletterStatusName
        {
            get { return _NewsletterStatusName; }
            set { _NewsletterStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string NewsletterStatusDesc
        {
            get { return _NewsletterStatusDesc; }
            set { _NewsletterStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<NewsletterStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<NewsletterStatus> l_NewsletterStatus = new List<NewsletterStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    NewsletterStatus m_NewsletterStatus = new NewsletterStatus(db.ConnectionString);
                    m_NewsletterStatus.NewsletterStatusId = smartReader.GetByte("NewsletterStatusId");
                    m_NewsletterStatus.NewsletterStatusName = smartReader.GetString("NewsletterStatusName");
                    m_NewsletterStatus.NewsletterStatusDesc = smartReader.GetString("NewsletterStatusDesc");

                    l_NewsletterStatus.Add(m_NewsletterStatus);
                }
                reader.Close();
                return l_NewsletterStatus;
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
                SqlCommand cmd = new SqlCommand("NewsletterStatus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterStatusName", this.NewsletterStatusName));
                cmd.Parameters.Add(new SqlParameter("@NewsletterStatusDesc", this.NewsletterStatusDesc));
                cmd.Parameters.Add("@NewsletterStatusId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.NewsletterStatusId = Convert.ToByte((cmd.Parameters["@NewsletterStatusId"].Value == null) ? 0 : (cmd.Parameters["@NewsletterStatusId"].Value));
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
                SqlCommand cmd = new SqlCommand("NewsletterStatus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterStatusName", this.NewsletterStatusName));
                cmd.Parameters.Add(new SqlParameter("@NewsletterStatusDesc", this.NewsletterStatusDesc));
                cmd.Parameters.Add(new SqlParameter("@NewsletterStatusId", this.NewsletterStatusId));
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
                SqlCommand cmd = new SqlCommand("NewsletterStatus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterStatusId", this.NewsletterStatusId));
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
        public List<NewsletterStatus> GetList()
        {
            List<NewsletterStatus> RetVal = new List<NewsletterStatus>();
            try
            {
                string sql = "SELECT * FROM NewsletterStatus";
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
        public static List<NewsletterStatus> Static_GetList(string ConStr)
        {
            List<NewsletterStatus> RetVal = new List<NewsletterStatus>();
            try
            {
                NewsletterStatus m_NewsletterStatus = new NewsletterStatus(ConStr);
                RetVal = m_NewsletterStatus.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<NewsletterStatus> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<NewsletterStatus> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            NewsletterStatus m_NewsletterStatus = new NewsletterStatus(ConStr);
            List<NewsletterStatus> RetVal = m_NewsletterStatus.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_NewsletterStatus.NewsletterStatusName = TextOptionAll;
                m_NewsletterStatus.NewsletterStatusDesc = TextOptionAll;
                RetVal.Insert(0, m_NewsletterStatus);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<NewsletterStatus> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<NewsletterStatus> GetListOrderBy(string OrderBy)
        {
            List<NewsletterStatus> RetVal = new List<NewsletterStatus>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM NewsletterStatus ";
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
        public static List<NewsletterStatus> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            NewsletterStatus m_NewsletterStatus = new NewsletterStatus(ConStr);
            return m_NewsletterStatus.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<NewsletterStatus> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<NewsletterStatus> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<NewsletterStatus> RetVal = new List<NewsletterStatus>();
            NewsletterStatus m_NewsletterStatus = new NewsletterStatus(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_NewsletterStatus.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_NewsletterStatus.NewsletterStatusName = TextOptionAll;
                    m_NewsletterStatus.NewsletterStatusDesc = TextOptionAll;
                    RetVal.Insert(0, m_NewsletterStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<NewsletterStatus> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<NewsletterStatus> GetListByNewsletterStatusId(byte NewsletterStatusId)
        {
            List<NewsletterStatus> RetVal = new List<NewsletterStatus>();
            try
            {
                if (NewsletterStatusId > 0)
                {
                    string sql = "SELECT * FROM NewsletterStatus WHERE (NewsletterStatusId=" + NewsletterStatusId.ToString() + ")";
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
        public NewsletterStatus Get(byte NewsletterStatusId)
        {
            NewsletterStatus RetVal = new NewsletterStatus(db.ConnectionString);
            try
            {
                List<NewsletterStatus> list = GetListByNewsletterStatusId(NewsletterStatusId);
                if (list.Count > 0)
                {
                    RetVal = (NewsletterStatus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public NewsletterStatus Get()
        {
            return Get(this.NewsletterStatusId);
        }
        //-------------------------------------------------------------- 
        public static NewsletterStatus Static_Get(byte NewsletterStatusId)
        {
            return new NewsletterStatus().Get(NewsletterStatusId);
        }
        //-----------------------------------------------------------------------------
        public static NewsletterStatus Static_Get(byte NewsletterStatusId, List<NewsletterStatus> lList)
        {
            NewsletterStatus RetVal = new NewsletterStatus();
            if (NewsletterStatusId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.NewsletterStatusId == NewsletterStatusId);
                if (RetVal == null) RetVal = new NewsletterStatus();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}