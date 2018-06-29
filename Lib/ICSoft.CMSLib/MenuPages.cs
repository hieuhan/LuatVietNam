
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
    public class MenuPages
    {   
        private byte _LanguageId;
        private byte _ApplicationTypeId;
	    private short _MenuPageId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _MenuId;
	    private short _PageId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public MenuPages()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public MenuPages(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MenuPages()
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
	    public short MenuPageId
        {
		    get { return _MenuPageId; }
		    set { _MenuPageId = value; }
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
    
	    public byte MenuId
        {
		    get { return _MenuId; }
		    set { _MenuId = value; }
	    }
	    public short PageId
        {
		    get { return _PageId; }
		    set { _PageId = value; }
	    }
        private List<MenuPages> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MenuPages> l_MenuPages = new List<MenuPages>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MenuPages m_MenuPages = new MenuPages(db.ConnectionString);
                    m_MenuPages.LanguageId = smartReader.GetByte("LanguageId");
                    m_MenuPages.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_MenuPages.MenuPageId = smartReader.GetInt16("MenuPageId");
                    m_MenuPages.MenuId = smartReader.GetByte("MenuId");
                    m_MenuPages.PageId = smartReader.GetInt16("PageId");
                    m_MenuPages.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MenuPages.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_MenuPages.Add(m_MenuPages);
                }
                reader.Close();
                return l_MenuPages;
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
                SqlCommand cmd = new SqlCommand("MenuPages_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MenuId", this.MenuId));
                cmd.Parameters.Add(new SqlParameter("@PageId", this.PageId));
                cmd.Parameters.Add(new SqlParameter("@MenuPageId", this.MenuPageId)).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MenuPageId = Convert.ToInt16((cmd.Parameters["@MenuPageId"].Value == DBNull.Value) ? 0 : (cmd.Parameters["@MenuPageId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("MenuPages_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MenuId", this.MenuId));
                cmd.Parameters.Add(new SqlParameter("@PageId", this.PageId));
                cmd.Parameters.Add(new SqlParameter("@MenuPageId", this.MenuPageId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MenuPageId = Convert.ToInt16((cmd.Parameters["@MenuPageId"].Value == DBNull.Value) ? 0 : (cmd.Parameters["@MenuPageId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("MenuPages_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MenuId", this.MenuId));
                cmd.Parameters.Add(new SqlParameter("@PageId", this.PageId));
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
        public List<MenuPages> GetList()
        {
            List<MenuPages> RetVal = new List<MenuPages>();
            try
            {
                string sql = "SELECT * FROM V$MenuPages";
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
        public static List<MenuPages> Static_GetList(string ConStr)
        {
            List<MenuPages> RetVal = new List<MenuPages>();
            try
            {
                MenuPages m_MenuPages = new MenuPages(ConStr);
                RetVal = m_MenuPages.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MenuPages> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<MenuPages> GetListByMenuPageId(short MenuPageId, byte LanguageId, byte ApplicationTypeId)
        {
            List<MenuPages> RetVal = new List<MenuPages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                short PageId = 0;               
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId,MenuPageId, PageId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<MenuPages> GetListByPageId(short PageId, byte LanguageId, byte ApplicationTypeId)
        {
            List<MenuPages> RetVal = new List<MenuPages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                byte MenuId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<MenuPages> GetListByMenuId(byte MenuId, byte LanguageId, byte ApplicationTypeId)
        {
            List<MenuPages> RetVal = new List<MenuPages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<MenuPages> GetListByMenuAndPageId(byte MenuId, short PageId, byte LanguageId, byte ApplicationTypeId)
        {
            List<MenuPages> RetVal = new List<MenuPages>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MenuPages Get(short MenuPageId, byte LanguageId, byte ApplicationTypeId)
        {
            MenuPages RetVal = new MenuPages(db.ConnectionString);
            try
            {
                List<MenuPages> list = GetListByMenuPageId(MenuPageId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (MenuPages)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MenuPages Get()
        {
            return Get(this.MenuPageId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static MenuPages Static_Get(string Constr, short MenuPageId, byte LanguageId, byte ApplicationTypeId)
        {
            MenuPages m_MenuPages = new MenuPages(Constr);
            return m_MenuPages.Get(MenuPageId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static MenuPages Static_Get(short MenuPageId, byte LanguageId, byte ApplicationTypeId)
        {
            return Static_Get("",MenuPageId, LanguageId, ApplicationTypeId);
        }
        //--------------------------------------------------------------     
        public List<MenuPages> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, short MenuPageId, short PageId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<MenuPages> RetVal = new List<MenuPages>();
            try
            {
                SqlCommand cmd = new SqlCommand("MenuPages_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MenuId", MenuPageId));
                cmd.Parameters.Add(new SqlParameter("@PageId", PageId));
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
        public List<MenuPages> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, byte MenuId, short PageId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<MenuPages> RetVal = new List<MenuPages>();
            try
            {
                SqlCommand cmd = new SqlCommand("MenuPages_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MenuId", MenuId));
                cmd.Parameters.Add(new SqlParameter("@PageId", PageId));
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
        public List<MenuPages> Search(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, short MenuPageId, short PageId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuPageId, PageId, DateFrom, DateTo, ref RowCount);
        }
    } 
}