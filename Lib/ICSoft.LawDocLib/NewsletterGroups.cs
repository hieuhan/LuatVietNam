
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
    public class NewsletterGroups
    {   
	    private short _NewsletterGroupId;
	    private string _NewsletterGroupName;
	    private string _NewsletterGroupDesc;
        private short _SiteId;
        private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
         //-----------------------------------------------------------------
        public NewsletterGroups()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public NewsletterGroups(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~NewsletterGroups()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short NewsletterGroupId
        {
		    get { return _NewsletterGroupId; }
		    set { _NewsletterGroupId = value; }
	    }
        //-----------------------------------------------------------------
        public string NewsletterGroupName
		{
            get { return _NewsletterGroupName; }
		    set { _NewsletterGroupName = value; }
		}    
        //-----------------------------------------------------------------
        public string NewsletterGroupDesc
		{
            get { return _NewsletterGroupDesc; }
		    set { _NewsletterGroupDesc = value; }
		}
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public int CrUserId
		{
            get { return _CrUserId; }
		    set { _CrUserId = value; }
		}    
        //-----------------------------------------------------------------
        public DateTime CrDateTime
		{
            get { return _CrDateTime; }
		    set { _CrDateTime = value; }
		}    
        //-----------------------------------------------------------------
    
        private List<NewsletterGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<NewsletterGroups> l_NewsletterGroups = new List<NewsletterGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    NewsletterGroups m_NewsletterGroups = new NewsletterGroups(db.ConnectionString);
                    m_NewsletterGroups.NewsletterGroupId = smartReader.GetInt16("NewsletterGroupId");
                    m_NewsletterGroups.NewsletterGroupName = smartReader.GetString("NewsletterGroupName");
                    m_NewsletterGroups.NewsletterGroupDesc = smartReader.GetString("NewsletterGroupDesc");
                    m_NewsletterGroups.SiteId = smartReader.GetInt16("SiteId");
                    m_NewsletterGroups.CrUserId = smartReader.GetInt32("CrUserId");
                    m_NewsletterGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_NewsletterGroups.Add(m_NewsletterGroups);
                }
                reader.Close();
                return l_NewsletterGroups;
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
                SqlCommand cmd = new SqlCommand("NewsletterGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupName", this.NewsletterGroupName));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupDesc", this.NewsletterGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupId", this.NewsletterGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.NewsletterGroupId =Convert.ToInt16((cmd.Parameters["@NewsletterGroupId"].Value == null) ? 0 : (cmd.Parameters["@NewsletterGroupId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("NewsletterGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupId",this.NewsletterGroupId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<NewsletterGroups> GetList()
        {
            List<NewsletterGroups> RetVal = new List<NewsletterGroups>();
            try
            {
                string sql = "SELECT * FROM NewsletterGroups";
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
        public static List<NewsletterGroups> Static_GetList(string ConStr)
        {
            List<NewsletterGroups> RetVal = new List<NewsletterGroups>();
            try
            {
                NewsletterGroups m_NewsletterGroups = new NewsletterGroups(ConStr);
                RetVal = m_NewsletterGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<NewsletterGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<NewsletterGroups> Static_GetListAll(string ConStr ,string TextOptionAll = "...")
        {
            NewsletterGroups m_NewsletterGroups = new NewsletterGroups(ConStr);
            List<NewsletterGroups> RetVal = m_NewsletterGroups.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_NewsletterGroups.NewsletterGroupName = TextOptionAll;
                m_NewsletterGroups.NewsletterGroupDesc = TextOptionAll;
                RetVal.Insert(0, m_NewsletterGroups);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<NewsletterGroups> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<NewsletterGroups> GetListOrderBy(string OrderBy)
        {
            List<NewsletterGroups> RetVal = new List<NewsletterGroups>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM NewsletterGroups " ;
                if (OrderBy.Length >0)
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
        public static List<NewsletterGroups> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            NewsletterGroups m_NewsletterGroups = new NewsletterGroups(ConStr);
            return m_NewsletterGroups.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<NewsletterGroups> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<NewsletterGroups> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<NewsletterGroups> RetVal = new List<NewsletterGroups>();
            NewsletterGroups m_NewsletterGroups = new NewsletterGroups(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_NewsletterGroups.GetListOrderBy(OrderBy);
                }               
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_NewsletterGroups.NewsletterGroupName = TextOptionAll;
                    m_NewsletterGroups.NewsletterGroupDesc = TextOptionAll;
                    RetVal.Insert(0, m_NewsletterGroups);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<NewsletterGroups> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<NewsletterGroups> GetListByNewsletterGroupId(short NewsletterGroupId)
        {
            List<NewsletterGroups> RetVal = new List<NewsletterGroups>();
            try
            {
                if (NewsletterGroupId > 0)
                {
                    string sql = "SELECT * FROM NewsletterGroups WHERE (NewsletterGroupId=" + NewsletterGroupId.ToString() + ")";
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
        public NewsletterGroups Get(short NewsletterGroupId)
        {
            NewsletterGroups RetVal = new NewsletterGroups(db.ConnectionString);
            try
            {
                List<NewsletterGroups> list = GetListByNewsletterGroupId(NewsletterGroupId);
                if (list.Count > 0)
                {
                    RetVal = (NewsletterGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public NewsletterGroups Get()
        {
            return Get(this.NewsletterGroupId);
        }
        //-------------------------------------------------------------- 
        public static NewsletterGroups Static_Get(short NewsletterGroupId)
        {
            return new NewsletterGroups().Get(NewsletterGroupId);
        }
        //-----------------------------------------------------------------------------
        public static NewsletterGroups Static_Get(short NewsletterGroupId, List<NewsletterGroups> lList)
        {
            NewsletterGroups RetVal = new NewsletterGroups();
            if (NewsletterGroupId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.NewsletterGroupId == NewsletterGroupId);
                if (RetVal == null) RetVal = new NewsletterGroups();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<NewsletterGroups> NewsletterGroups_GetList(int ActUserId, string OrderBy, string NewsletterGroupName, short SiteId)
        {
            List<NewsletterGroups> RetVal = new List<NewsletterGroups>();
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterGroups_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@NewsletterGroupName", StringUtil.InjectionString(NewsletterGroupName)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return RetVal;
        }
        //--------------------------------------------------------------     
   } 
}

