
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
    public class Tags
    {
        private byte _LanguageId;
        private int _TagId;
        private string _TagName;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Tags()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Tags(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Tags()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------    
        public int TagId
        {
            get { return _TagId; }
            set { _TagId = value; }
        }
        //-----------------------------------------------------------------
        public string TagName
        {
            get { return _TagName; }
            set { _TagName = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
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

        private List<Tags> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Tags> l_Tags = new List<Tags>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Tags m_Tags = new Tags(db.ConnectionString);
                    m_Tags.LanguageId = smartReader.GetByte("LanguageId");
                    m_Tags.TagId = smartReader.GetInt32("TagId");
                    m_Tags.TagName = smartReader.GetString("TagName");
                    m_Tags.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Tags.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Tags.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Tags.Add(m_Tags);
                }
                reader.Close();
                return l_Tags;
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
                SqlCommand cmd = new SqlCommand("Tags_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@TagName", this.TagName));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@TagId", this.TagId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.TagId = Convert.ToInt32((cmd.Parameters["@TagId"].Value == null) ? 0 : (cmd.Parameters["@TagId"].Value));
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
                SqlCommand cmd = new SqlCommand("Tags_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@TagId", this.TagId));
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
        public List<Tags> GetList()
        {
            List<Tags> RetVal = new List<Tags>();
            try
            {
                string sql = "SELECT * FROM V$Tags";
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
        public static List<Tags> Static_GetList(string ConStr)
        {
            List<Tags> RetVal = new List<Tags>();
            try
            {
                Tags m_Tags = new Tags(ConStr);
                RetVal = m_Tags.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Tags> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<Tags> GetListOrderBy(string OrderBy)
        {
            List<Tags> RetVal = new List<Tags>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Tags ";
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
        public static List<Tags> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Tags m_Tags = new Tags(ConStr);
            return m_Tags.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Tags> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<Tags> GetListByTagId(int TagId, byte LanguageId)
        {
            List<Tags> RetVal = new List<Tags>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string TagName="";
                byte ReviewStatusId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, TagId, TagName, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Tags Get(int TagId, byte LanguageId)
        {
            Tags RetVal = new Tags(db.ConnectionString);
            try
            {
                List<Tags> list = GetListByTagId(TagId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (Tags)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Tags Get(int TagId,string TagUrl, byte LanguageId)
        {
            Tags RetVal = new Tags(db.ConnectionString);
            List<Tags> l_Tags = new List<Tags>();
            try
            {
                
                string sql = "SELECT * FROM Tags WHERE TagUrl = N'" + TagUrl + "'";
                if(TagId >0)
                    sql = "SELECT * FROM Tags WHERE TagId = " + TagId.ToString();
                SqlCommand cmd = new SqlCommand(sql);
                l_Tags = Init(cmd);
                if (l_Tags.Count > 0)
                    RetVal = l_Tags[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Tags Get()
        {
            return Get(this.TagId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Tags Static_Get(string Constr, int TagId, byte LanguageId)
        {
            Tags m_Tags = new Tags(Constr);
            return m_Tags.Get(TagId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Tags Static_Get(int TagId, byte LanguageId)
        {
            return Static_Get("", TagId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Tags Static_Get(int TagId, string TagUrl, byte LanguageId)
        {
            Tags m_Tags = new Tags();
            return m_Tags.Get(TagId, TagUrl, LanguageId);
        }
        //--------------------------------------------------------------  
        public List<Tags> GetByLanguageId(int TagId, byte LanguageId)
        {
            List<Tags> RetVal = new List<Tags>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string TagName = "";
                byte ReviewStatusId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, TagId, TagName, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Tags> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, int TagId, string TagName, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Tags> RetVal = new List<Tags>();
            try
            {
                SqlCommand cmd = new SqlCommand("Tags_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@TagName", StringUtil.InjectionString(TagName)));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Tags> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, int TagId, int ArticleId, string TagName, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Tags> RetVal = new List<Tags>();
            try
            {
                SqlCommand cmd = new SqlCommand("Tags_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@TagName", StringUtil.InjectionString(TagName)));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Tags> Search(int ActUserId, string OrderBy, byte LanguageId, int TagId, string TagName, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, TagId,  TagName, ReviewStatusId, DateFrom, DateTo, ref RowCount);
        }

        //---------------------------------------------------------------
        public int GetSiteMapPage(int rowAmount = 5000)
        {
            int retVal = 0;
            try
            {
                string sql = "SELECT COUNT(1) FROM Tags Where ReviewStatusId = 2 ";
                SqlCommand cmd = new SqlCommand(sql) { CommandType = CommandType.Text };
                retVal = db.ExecuteScalar(cmd);
                if (retVal % rowAmount > 0)
                    retVal = retVal / rowAmount + 1;
                else
                    retVal = retVal / rowAmount;
            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return retVal;
        }
    }
}