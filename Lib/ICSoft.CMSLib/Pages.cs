
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
    public class Pages
    {   
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private short _SiteId;
        private short _PageId;
	    private string _PageName;
	    private string _PageTitle;
	    private string _PageKeyword;
	    private string _PageDesciption;
	    private string _IconPath;
	    private string _Url;
	    private short _ParentId;
	    private byte _LevelId;
	    private short _DisplayOrder;
	    private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _PageTypeId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Pages()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Pages(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Pages()
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
        public byte ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }   
        //-----------------------------------------------------------------    
	    public short PageId
        {
		    get { return _PageId; }
		    set { _PageId = value; }
	    }
        //-----------------------------------------------------------------
        public string PageName
		{
            get { return _PageName; }
		    set { _PageName = value; }
		}    
        //-----------------------------------------------------------------
        public string PageTitle
		{
            get { return _PageTitle; }
		    set { _PageTitle = value; }
		}    
        //-----------------------------------------------------------------
        public string PageKeyword
		{
            get { return _PageKeyword; }
		    set { _PageKeyword = value; }
		}    
        //-----------------------------------------------------------------
        public string PageDesciption
		{
            get { return _PageDesciption; }
		    set { _PageDesciption = value; }
		}    
        //-----------------------------------------------------------------
        public string IconPath
		{
            get { return _IconPath; }
		    set { _IconPath = value; }
		}    
        //-----------------------------------------------------------------
        public string Url
		{
            get { return _Url; }
		    set { _Url = value; }
		}    
        //-----------------------------------------------------------------
        public short ParentId
		{
            get { return _ParentId; }
		    set { _ParentId = value; }
		}    
        //-----------------------------------------------------------------
        public byte LevelId
		{
            get { return _LevelId; }
		    set { _LevelId = value; }
		}    
        //-----------------------------------------------------------------
        public short DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
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
    
	    public byte PageTypeId
        {
		    get { return _PageTypeId; }
		    set { _PageTypeId = value; }
	    }
        private List<Pages> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Pages> l_Pages = new List<Pages>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Pages m_Pages = new Pages(db.ConnectionString);
                    m_Pages.LanguageId = smartReader.GetByte("LanguageId");
                    m_Pages.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Pages.SiteId = smartReader.GetInt16("SiteId");
                    m_Pages.PageId = smartReader.GetInt16("PageId");
                    m_Pages.PageTypeId = smartReader.GetByte("PageTypeId");
                    m_Pages.PageName = smartReader.GetString("PageName");
                    m_Pages.PageTitle = smartReader.GetString("PageTitle");
                    m_Pages.PageKeyword = smartReader.GetString("PageKeyword");
                    m_Pages.PageDesciption = smartReader.GetString("PageDesciption");
                    m_Pages.IconPath = smartReader.GetString("IconPath");
                    m_Pages.Url = smartReader.GetString("Url");
                    m_Pages.ParentId = smartReader.GetInt16("ParentId");
                    m_Pages.LevelId = smartReader.GetByte("LevelId");
                    m_Pages.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_Pages.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Pages.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Pages.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Pages.Add(m_Pages);
                }
                reader.Close();
                return l_Pages;
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
                SqlCommand cmd = new SqlCommand("Pages_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@PageTypeId", this.PageTypeId));
                cmd.Parameters.Add(new SqlParameter("@PageName", this.PageName));
                cmd.Parameters.Add(new SqlParameter("@PageTitle", this.PageTitle));
                cmd.Parameters.Add(new SqlParameter("@PageKeyword", this.PageKeyword));
                cmd.Parameters.Add(new SqlParameter("@PageDesciption", this.PageDesciption));
                cmd.Parameters.Add(new SqlParameter("@IconPath", this.IconPath));
                cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
                cmd.Parameters.Add(new SqlParameter("@ParentId", this.ParentId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@PageId", this.PageId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PageId = Convert.ToInt16((cmd.Parameters["@PageId"].Value == DBNull.Value) ? 0 : (cmd.Parameters["@PageId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==DBNull.Value)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
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
                SqlCommand cmd = new SqlCommand("Pages_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@PageId",this.PageId));
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
        public List<Pages> GetList()
        {
            List<Pages> RetVal = new List<Pages>();
            try
            {
                string sql = "SELECT * FROM Pages";
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
        public static List<Pages> Static_GetList(string ConStr)
        {
            List<Pages> RetVal = new List<Pages>();
            try
            {
                Pages m_Pages = new Pages(ConStr);
                RetVal = m_Pages.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Pages> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<Pages> GetListOrderBy(string OrderBy)
        {
            List<Pages> RetVal = new List<Pages>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM Pages " ;
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
        public static List<Pages> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Pages m_Pages = new Pages(ConStr);
            return m_Pages.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Pages> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<Pages> GetListByPageId( byte LanguageId, byte ApplicationTypeId, short PageId)
        {
            List<Pages> RetVal = new List<Pages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PageName="";
                short DisplayOrder=0;
                byte ReviewStatusId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount=0;          
                RetVal= GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Pages> GetListByPageId(byte LanguageId, byte ApplicationTypeId, short PageId, byte ReviewStatusId, string PaddingChar)
        {
            List<Pages> RetVal = GetListByPageId(LanguageId, ApplicationTypeId, PageId, ReviewStatusId);
            try
            {
                foreach (Pages m_Pages in RetVal)
                {
                    for (int index = 1; index < m_Pages.LevelId; index++)
                    {
                        m_Pages.PageName = PaddingChar + m_Pages.PageName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Pages> GetListByPageId(byte LanguageId, byte ApplicationTypeId, short PageId, byte ReviewStatusId)
        {
            List<Pages> RetVal = new List<Pages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PageName = "";
                short DisplayOrder = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Pages> GetListByMenuId(byte LanguageId, byte ApplicationTypeId, byte MenuId)
        {
            List<Pages> RetVal = new List<Pages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PageName = "";
                short DisplayOrder = 0;
                byte ReviewStatusId = 0;
                short PageId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public Pages GetByMenuId(byte LanguageId, byte ApplicationTypeId, byte MenuId)
        {
            Pages RetVal = new Pages();
            try
            {
                List<Pages> l_Pages = GetListByMenuId(LanguageId, ApplicationTypeId, MenuId);
                if (l_Pages.Count > 0)
                    RetVal = l_Pages[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Pages> GetListByMenuId(byte LanguageId, byte ApplicationTypeId, byte MenuId, short PageId)
        {
            List<Pages> RetVal = new List<Pages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PageName = "";
                short DisplayOrder = 0;
                byte ReviewStatusId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Pages> GetListByMenuId(byte LanguageId, byte ApplicationTypeId, byte MenuId, short PageId, byte ReviewStatusId = 0)
        {
            List<Pages> RetVal = new List<Pages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PageName = "";
                short DisplayOrder = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Pages> Static_GetListByMenuId(string ConStr, byte LanguageId, byte ApplicationTypeId, byte MenuId)
        {
            Pages m_Pages = new Pages(ConStr);
            return m_Pages.GetListByMenuId(LanguageId, ApplicationTypeId, MenuId);
        }
        //-------------------------------------------------------------- 
        public static List<Pages> Static_GetListByMenuId(byte LanguageId, byte ApplicationTypeId, byte MenuId)
        {
            string ConStr = "";
            return Static_GetListByMenuId(ConStr, LanguageId, ApplicationTypeId, MenuId);
        }
        //-------------------------------------------------------------- 
        public static List<Pages> Static_GetListByMenuId(string ConStr, byte LanguageId, byte ApplicationTypeId, byte MenuId, short PageId, byte ReviewStatusId = 0)
        {
            Pages m_Pages = new Pages(ConStr);
            return m_Pages.GetListByMenuId(LanguageId, ApplicationTypeId, MenuId, PageId, ReviewStatusId);
        }

        //-------------------------------------------------------------- 
        public static List<Pages> Static_GetListByMenuId(byte LanguageId, byte ApplicationTypeId, byte MenuId, short PageId, byte ReviewStatusId = 0)
        {
            string ConStr = "";
            return Static_GetListByMenuId(ConStr, LanguageId, ApplicationTypeId, MenuId, PageId, ReviewStatusId);
        }
        //-------------------------------------------------------------- 
        public static Pages Static_GetByMenuId(string ConStr, byte LanguageId, byte ApplicationTypeId, byte MenuId)
        {

            List<Pages> l_Pages = Static_GetListByMenuId(ConStr, LanguageId, ApplicationTypeId, MenuId);
            if (l_Pages.Count > 0)
                return l_Pages[0];
            else
                return new Pages(ConStr);
        }
        //-------------------------------------------------------------- 
        public static Pages Static_GetByMenuId( byte LanguageId, byte ApplicationTypeId,byte MenuId)
        {
            string ConStr = "";
            return Static_GetByMenuId(ConStr, LanguageId, ApplicationTypeId, MenuId);
        }
        
        //-------------------------------------------------------------- 
        public Pages Get(short PageId, byte LanguageId, byte ApplicationTypeId)
        {
            Pages RetVal = new Pages(db.ConnectionString);
            try
            {
                List<Pages> list = GetListByPageId( LanguageId, ApplicationTypeId, PageId);
                if (list.Count > 0)
                {
                    RetVal = (Pages)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Pages Get( byte LanguageId, byte ApplicationTypeId,short PageId)
        {
            Pages RetVal = new Pages(db.ConnectionString);
            try
            {
                List<Pages> list = GetListByPageId(LanguageId, ApplicationTypeId, PageId);
                if (list.Count > 0)
                {
                    RetVal = (Pages)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Pages Get()
        {
            return Get(this.PageId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Pages Static_Get(string Constr, byte LanguageId, byte ApplicationTypeId, short PageId)
        {
            Pages m_Pages = new Pages(Constr);
            return m_Pages.Get(PageId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Pages Static_Get(byte LanguageId, byte ApplicationTypeId, short PageId)
        {
            string Constr = "";
            return Static_Get(Constr, LanguageId, ApplicationTypeId, PageId);
        }
        //-------------------------------------------------------------- 
        public static Pages Static_Get(List<Pages> l_Pages, short PageId)
        {
            Pages RetVal;
            RetVal = l_Pages.Find(x => x.PageId == PageId);
            if (RetVal == null)
                RetVal = new Pages();
            return RetVal;
        }
       
        //--------------------------------------------------------------     
        public List<Pages> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, short PageId, string PageName, short DisplayOrder, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            byte MenuId = 0;            
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<Pages> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, byte MenuId, short PageId, string PageName, short DisplayOrder, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Pages> RetVal = new List<Pages>();
            try
            {
                SqlCommand cmd = new SqlCommand("Pages_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@MenuId", MenuId));
                cmd.Parameters.Add(new SqlParameter("@PageId", PageId));
                cmd.Parameters.Add(new SqlParameter("@PageName", StringUtil.InjectionString(PageName)));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", DisplayOrder));
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
        public List<Pages> Search(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, short PageId, string PageName, short DisplayOrder, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
        }
    } 
}
