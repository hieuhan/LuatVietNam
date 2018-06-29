
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
    public class PageMeetingGroups
    {   
	    private short _PageMeetingGroupId;
	    private short _MeetingGroupId;
	    private short _PageId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public PageMeetingGroups()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public PageMeetingGroups(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~PageMeetingGroups()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short PageMeetingGroupId
        {
		    get { return _PageMeetingGroupId; }
		    set { _PageMeetingGroupId = value; }
	    }
        //-----------------------------------------------------------------
        public short MeetingGroupId
		{
            get { return _MeetingGroupId; }
		    set { _MeetingGroupId = value; }
		}    
        //-----------------------------------------------------------------
    
	    public short PageId
        {
		    get { return _PageId; }
		    set { _PageId = value; }
	    }
        private List<PageMeetingGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<PageMeetingGroups> l_PageMeetingGroups = new List<PageMeetingGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PageMeetingGroups m_PageMeetingGroups = new PageMeetingGroups(db.ConnectionString);
                    m_PageMeetingGroups.PageMeetingGroupId = smartReader.GetInt16("PageMeetingGroupId");
                    m_PageMeetingGroups.PageId = smartReader.GetInt16("PageId");
                    m_PageMeetingGroups.MeetingGroupId = smartReader.GetInt16("MeetingGroupId");
         
                    l_PageMeetingGroups.Add(m_PageMeetingGroups);
                }
                reader.Close();
                return l_PageMeetingGroups;
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
                SqlCommand cmd = new SqlCommand("PageMeetingGroups_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PageId", this.PageId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupId", this.MeetingGroupId));
                cmd.Parameters.Add("@PageMeetingGroupId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PageMeetingGroupId =Convert.ToInt16((cmd.Parameters["@PageMeetingGroupId"].Value == null) ? 0 : (cmd.Parameters["@PageMeetingGroupId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PageMeetingGroups_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PageId", this.PageId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupId", this.MeetingGroupId));
                cmd.Parameters.Add(new SqlParameter("@PageMeetingGroupId",this.PageMeetingGroupId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal =Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("PageMeetingGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PageMeetingGroupId",this.PageMeetingGroupId));
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
        public List<PageMeetingGroups> GetList()
        {
            List<PageMeetingGroups> RetVal = new List<PageMeetingGroups>();
            try
            {
                string sql = "SELECT * FROM V$PageMeetingGroups";
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
        public static List<PageMeetingGroups> Static_GetList(string ConStr)
        {
            List<PageMeetingGroups> RetVal = new List<PageMeetingGroups>();
            try
            {
                PageMeetingGroups m_PageMeetingGroups = new PageMeetingGroups(ConStr);
                RetVal = m_PageMeetingGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<PageMeetingGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<PageMeetingGroups> GetListOrderBy(string OrderBy)
        {
            List<PageMeetingGroups> RetVal = new List<PageMeetingGroups>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$PageMeetingGroups " ;
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
        public static List<PageMeetingGroups> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            PageMeetingGroups m_PageMeetingGroups = new PageMeetingGroups(ConStr);
            return m_PageMeetingGroups.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<PageMeetingGroups> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<PageMeetingGroups> GetListByPageMeetingGroupId(short PageMeetingGroupId)
        {
            List<PageMeetingGroups> RetVal = new List<PageMeetingGroups>();
            try
            {
                if (PageMeetingGroupId > 0)
                {
                    string sql = "SELECT * FROM V$PageMeetingGroups WHERE (PageMeetingGroupId=" + PageMeetingGroupId.ToString() + ")";
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
        public List<PageMeetingGroups> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short PageId, short MeetingGroupId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<PageMeetingGroups> RetVal = new List<PageMeetingGroups>();
            try
            {
                SqlCommand cmd = new SqlCommand("PageMeetingGroups_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@PageId", PageId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupId", MeetingGroupId));
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
        public PageMeetingGroups GetByPageId(short PageId)
        {
            PageMeetingGroups RetVal = new PageMeetingGroups(db.ConnectionString);
            int ActUserId = 0;
            int RowAmount = 0;
            int PageIndex = 0;
            string OrderBy = "";
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            short MeetingGroupId = 0;
            try
            {
                List<PageMeetingGroups> list = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, PageId, MeetingGroupId, DateFrom, DateTo, ref RowCount);
                if (list.Count > 0)
                {
                    RetVal = (PageMeetingGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public PageMeetingGroups Get(short PageMeetingGroupId)
        {
            PageMeetingGroups RetVal = new PageMeetingGroups(db.ConnectionString);
            try
            {
                List<PageMeetingGroups> list = GetListByPageMeetingGroupId(PageMeetingGroupId);
                if (list.Count > 0)
                {
                    RetVal = (PageMeetingGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public PageMeetingGroups Get()
        {
            return Get(this.PageMeetingGroupId);
        }
        //-------------------------------------------------------------- 
        public static PageMeetingGroups Static_Get(short PageMeetingGroupId)
        {
            PageMeetingGroups RetVal = new PageMeetingGroups();
            RetVal = RetVal.Get(PageMeetingGroupId);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static PageMeetingGroups Static_GetByPageId(short PageId)
        {
            PageMeetingGroups RetVal = new PageMeetingGroups();
            RetVal = RetVal.GetByPageId(PageId);
            return RetVal;
        }
        //--------------------------------------------------------------     
       
    } 
}