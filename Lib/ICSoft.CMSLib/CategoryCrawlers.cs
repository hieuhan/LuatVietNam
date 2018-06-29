
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
    public class CategoryCrawlers
    {
        private int _CategoryCrawlerId;
        private short _CategoryId;
        private short _DataSourceId;
        private byte _DataTypeId;
        private string _LinkGetList;
        private string _LinkType;
        private int _MaxItem;
        private byte _StatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CategoryCrawlers()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CategoryCrawlers(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CategoryCrawlers()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CategoryCrawlerId
        {
            get { return _CategoryCrawlerId; }
            set { _CategoryCrawlerId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------
        public byte DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string LinkGetList
        {
            get { return _LinkGetList; }
            set { _LinkGetList = value; }
        }
        //-----------------------------------------------------------------
        public string LinkType
        {
            get { return _LinkType; }
            set { _LinkType = value; }
        }
        //-----------------------------------------------------------------
        public int MaxItem
        {
            get { return _MaxItem; }
            set { _MaxItem = value; }
        }
        //-----------------------------------------------------------------
        public byte StatusId
        {
            get { return _StatusId; }
            set { _StatusId = value; }
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

        private List<CategoryCrawlers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CategoryCrawlers> l_CategoryCrawlers = new List<CategoryCrawlers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CategoryCrawlers m_CategoryCrawlers = new CategoryCrawlers(db.ConnectionString);
                    m_CategoryCrawlers.CategoryCrawlerId = smartReader.GetInt32("CategoryCrawlerId");
                    m_CategoryCrawlers.CategoryId = smartReader.GetInt16("CategoryId");
                    m_CategoryCrawlers.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_CategoryCrawlers.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_CategoryCrawlers.LinkGetList = smartReader.GetString("LinkGetList");
                    m_CategoryCrawlers.LinkType = smartReader.GetString("LinkType");
                    m_CategoryCrawlers.MaxItem = smartReader.GetInt32("MaxItem");
                    m_CategoryCrawlers.StatusId = smartReader.GetByte("StatusId");
                    m_CategoryCrawlers.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CategoryCrawlers.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_CategoryCrawlers.Add(m_CategoryCrawlers);
                }
                reader.Close();
                return l_CategoryCrawlers;
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
                SqlCommand cmd = new SqlCommand("CategoryCrawlers_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@LinkGetList", this.LinkGetList));
                cmd.Parameters.Add(new SqlParameter("@LinkType", this.LinkType));
                cmd.Parameters.Add(new SqlParameter("@MaxItem", this.MaxItem));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add("@CategoryCrawlerId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CategoryCrawlerId = (cmd.Parameters["@CategoryCrawlerId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@CategoryCrawlerId"].Value);
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
                SqlCommand cmd = new SqlCommand("CategoryCrawlers_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@LinkGetList", this.LinkGetList));
                cmd.Parameters.Add(new SqlParameter("@LinkType", this.LinkType));
                cmd.Parameters.Add(new SqlParameter("@MaxItem", this.MaxItem));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CategoryCrawlerId", this.CategoryCrawlerId));
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CategoryCrawlers_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@LinkGetList", this.LinkGetList));
                cmd.Parameters.Add(new SqlParameter("@LinkType", this.LinkType));
                cmd.Parameters.Add(new SqlParameter("@MaxItem", this.MaxItem));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CategoryCrawlerId", this.CategoryCrawlerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CategoryCrawlerId = (cmd.Parameters["@CategoryCrawlerId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@CategoryCrawlerId"].Value);
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
                SqlCommand cmd = new SqlCommand("CategoryCrawlers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryCrawlerId", this.CategoryCrawlerId));
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
        public List<CategoryCrawlers> GetListByCategoryCrawlerId(int CategoryCrawlerId)
        {
            List<CategoryCrawlers> RetVal = new List<CategoryCrawlers>();
            try
            {
                if (CategoryCrawlerId > 0)
                {
                    string sql = "SELECT * FROM CategoryCrawlers WHERE (CategoryCrawlerId=" + CategoryCrawlerId.ToString() + ")";
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
        public CategoryCrawlers Get(int CategoryCrawlerId)
        {
            CategoryCrawlers RetVal = new CategoryCrawlers();
            try
            {
                List<CategoryCrawlers> list = GetListByCategoryCrawlerId(CategoryCrawlerId);
                if (list.Count > 0)
                {
                    RetVal = (CategoryCrawlers)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static CategoryCrawlers Static_Get(int CategoryCrawlerId, List<CategoryCrawlers> list)
        {
            CategoryCrawlers RetVal = new CategoryCrawlers();
            try
            {
                RetVal = list.Find(i => i.CategoryCrawlerId == CategoryCrawlerId);
                if (RetVal == null) RetVal = new CategoryCrawlers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public CategoryCrawlers Get()
        {
            return Get(this.CategoryCrawlerId);
        }
        //-------------------------------------------------------------- 
        public List<CategoryCrawlers> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short SiteId, ref int RowCount)
        {
            List<CategoryCrawlers> RetVal = new List<CategoryCrawlers>();
            try
            {
                SqlCommand cmd = new SqlCommand("CategoryCrawlers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@LinkType", this.LinkType));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
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
    }
}

