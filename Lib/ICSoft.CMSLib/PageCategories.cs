
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
    public class PageCategories
    {   
	    private short _PageCategoryId;
	    private short _CategoryId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private short _PageId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public PageCategories()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public PageCategories(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~PageCategories()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short PageCategoryId
        {
		    get { return _PageCategoryId; }
		    set { _PageCategoryId = value; }
	    }
        //-----------------------------------------------------------------
        public short CategoryId
		{
            get { return _CategoryId; }
		    set { _CategoryId = value; }
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
    
	    public short PageId
        {
		    get { return _PageId; }
		    set { _PageId = value; }
	    }
        private List<PageCategories> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<PageCategories> l_PageCategories = new List<PageCategories>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PageCategories m_PageCategories = new PageCategories(db.ConnectionString);
                    m_PageCategories.PageCategoryId = smartReader.GetInt16("PageCategoryId");
                    m_PageCategories.PageId = smartReader.GetInt16("PageId");
                    m_PageCategories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_PageCategories.CrUserId = smartReader.GetInt32("CrUserId");
                    m_PageCategories.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_PageCategories.Add(m_PageCategories);
                }
                reader.Close();
                return l_PageCategories;
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
                SqlCommand cmd = new SqlCommand("PageCategories_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PageId", this.PageId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@PageCategoryId", this.PageCategoryId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PageCategoryId =Convert.ToInt16((cmd.Parameters["@PageCategoryId"].Value == null) ? 0 : (cmd.Parameters["@PageCategoryId"].Value));
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
                SqlCommand cmd = new SqlCommand("PageCategories_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PageCategoryId",this.PageCategoryId));
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
        public List<PageCategories> GetList()
        {
            List<PageCategories> RetVal = new List<PageCategories>();
            try
            {
                string sql = "SELECT * FROM V$PageCategories";
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
        public static List<PageCategories> Static_GetList(string ConStr)
        {
            List<PageCategories> RetVal = new List<PageCategories>();
            try
            {
                PageCategories m_PageCategories = new PageCategories(ConStr);
                RetVal = m_PageCategories.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<PageCategories> Static_GetList()
        {
            return Static_GetList("");
        }
        
        //--------------------------------------------------------------    
        public List<PageCategories> GetListOrderBy(string OrderBy)
        {
            List<PageCategories> RetVal = new List<PageCategories>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$PageCategories " ;
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
        public static List<PageCategories> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            PageCategories m_PageCategories = new PageCategories(ConStr);
            return m_PageCategories.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<PageCategories> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }        
        //--------------------------------------------------------------  
        public List<PageCategories> GetListByPageCategoryId(short PageCategoryId)
        {
            List<PageCategories> RetVal = new List<PageCategories>();
            try
            {
                if (PageCategoryId > 0)
                {
                    string sql = "SELECT * FROM V$PageCategories WHERE (PageCategoryId=" + PageCategoryId.ToString() + ")";
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
        public PageCategories Get(short PageCategoryId)
        {
            PageCategories RetVal = new PageCategories(db.ConnectionString);
            try
            {
                List<PageCategories> list = GetListByPageCategoryId(PageCategoryId);
                if (list.Count > 0)
                {
                    RetVal = (PageCategories)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public PageCategories GetByPageId(short PageId)
        {
            PageCategories RetVal = new PageCategories(db.ConnectionString);
            int ActUserId = 0;
            int RowAmount = 0;
            int PageIndex = 0;
            string OrderBy = "";
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            try
            {
                List<PageCategories> list = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, PageId, CategoryId, DateFrom, DateTo, ref RowCount);
                if (list.Count > 0)
                {
                    RetVal = (PageCategories)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public PageCategories Get()
        {
            return Get(this.PageCategoryId);
        }
        //-------------------------------------------------------------- 
        public static PageCategories Static_Get(short PageCategoryId)
        {
            PageCategories RetVal = new PageCategories();
            RetVal = RetVal.Get(PageCategoryId);

            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static PageCategories Static_GetByPageId(short PageId)
        {
            PageCategories RetVal = new PageCategories();
            RetVal = RetVal.GetByPageId(PageId);
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<PageCategories> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short PageId, short CategoryId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<PageCategories> RetVal = new List<PageCategories>();
            try
            {
                SqlCommand cmd = new SqlCommand("PageCategories_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@PageId", PageId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                if (DateFrom!="") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo!="") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
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
        public List<PageCategories> Search(int ActUserId, string OrderBy, short PageId, short CategoryId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, PageId, CategoryId, DateFrom, DateTo, ref RowCount);
        }
    } 
}

