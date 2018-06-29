
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
    public class FeedBackGroups
    {   
	    private short _FeedBackGroupId;
	    private string _FeedBackGroupName;
	    private string _FeedBackGroupDesc;
        private short _SiteId;
        private byte _IsPublic;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public FeedBackGroups()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public FeedBackGroups(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FeedBackGroups()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short FeedBackGroupId
        {
		    get { return _FeedBackGroupId; }
		    set { _FeedBackGroupId = value; }
	    }
        //-----------------------------------------------------------------
        public string FeedBackGroupName
		{
            get { return _FeedBackGroupName; }
		    set { _FeedBackGroupName = value; }
		}    
        //-----------------------------------------------------------------
        public string FeedBackGroupDesc
		{
            get { return _FeedBackGroupDesc; }
		    set { _FeedBackGroupDesc = value; }
		}
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public byte IsPublic
        {
            get { return _IsPublic; }
            set { _IsPublic = value; }
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
    
        private List<FeedBackGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FeedBackGroups> l_FeedBackGroups = new List<FeedBackGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FeedBackGroups m_FeedBackGroups = new FeedBackGroups(db.ConnectionString);
                    m_FeedBackGroups.FeedBackGroupId = smartReader.GetInt16("FeedBackGroupId");
                    m_FeedBackGroups.FeedBackGroupName = smartReader.GetString("FeedBackGroupName");
                    m_FeedBackGroups.FeedBackGroupDesc = smartReader.GetString("FeedBackGroupDesc");
                    m_FeedBackGroups.SiteId = smartReader.GetInt16("SiteId");
                    m_FeedBackGroups.IsPublic = smartReader.GetByte("IsPublic");
                    m_FeedBackGroups.CrUserId = smartReader.GetInt32("CrUserId");
                    m_FeedBackGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_FeedBackGroups.Add(m_FeedBackGroups);
                }
                reader.Close();
                return l_FeedBackGroups;
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
                SqlCommand cmd = new SqlCommand("FeedBackGroups_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeedBackGroupName", this.FeedBackGroupName));
                cmd.Parameters.Add(new SqlParameter("@FeedBackGroupDesc", this.FeedBackGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@IsPublic", this.IsPublic));
                cmd.Parameters.Add("@FeedBackGroupId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FeedBackGroupId =Convert.ToInt16((cmd.Parameters["@FeedBackGroupId"].Value == null) ? 0 : (cmd.Parameters["@FeedBackGroupId"].Value));
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
                SqlCommand cmd = new SqlCommand("FeedBackGroups_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeedBackGroupName", this.FeedBackGroupName));
                cmd.Parameters.Add(new SqlParameter("@FeedBackGroupDesc", this.FeedBackGroupDesc));
                 cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@IsPublic", this.IsPublic));
                cmd.Parameters.Add(new SqlParameter("@FeedBackGroupId",this.FeedBackGroupId));
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
                SqlCommand cmd = new SqlCommand("FeedBackGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeedBackGroupId",this.FeedBackGroupId));
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
        public List<FeedBackGroups> GetList(int ActUserId, short SiteId)
        {
            List<FeedBackGroups> RetVal = new List<FeedBackGroups>();
            try
            {
                SqlCommand cmd = new SqlCommand("FeedBackGroups_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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
        public List<FeedBackGroups> GetList()
        {
            List<FeedBackGroups> RetVal = new List<FeedBackGroups>();
            try
            {
                string sql = "SELECT * FROM FeedBackGroups";
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
        public static List<FeedBackGroups> Static_GetList(string ConStr)
        {
            List<FeedBackGroups> RetVal = new List<FeedBackGroups>();
            try
            {
                FeedBackGroups m_FeedBackGroups = new FeedBackGroups(ConStr);
                RetVal = m_FeedBackGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<FeedBackGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
        public static List<FeedBackGroups> Static_GetList(int ActUserId, short SiteId)
        {
            return new FeedBackGroups().GetList(ActUserId, SiteId);
        }
        //--------------------------------------------------------------   
        public static List<FeedBackGroups> Static_GetListAll(string ConStr ,string TextOptionAll = "...")
        {
            FeedBackGroups m_FeedBackGroups = new FeedBackGroups(ConStr);
            List<FeedBackGroups> RetVal = m_FeedBackGroups.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_FeedBackGroups.FeedBackGroupName = TextOptionAll;
                m_FeedBackGroups.FeedBackGroupDesc = TextOptionAll;
                RetVal.Insert(0, m_FeedBackGroups);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<FeedBackGroups> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<FeedBackGroups> GetListOrderBy(string OrderBy)
        {
            List<FeedBackGroups> RetVal = new List<FeedBackGroups>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM FeedBackGroups " ;
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
        public static List<FeedBackGroups> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            FeedBackGroups m_FeedBackGroups = new FeedBackGroups(ConStr);
            return m_FeedBackGroups.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<FeedBackGroups> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<FeedBackGroups> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<FeedBackGroups> RetVal = new List<FeedBackGroups>();
            FeedBackGroups m_FeedBackGroups = new FeedBackGroups(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_FeedBackGroups.GetListOrderBy(OrderBy);
                }               
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_FeedBackGroups.FeedBackGroupName = TextOptionAll;
                    m_FeedBackGroups.FeedBackGroupDesc = TextOptionAll;
                    RetVal.Insert(0, m_FeedBackGroups);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<FeedBackGroups> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<FeedBackGroups> GetListByFeedBackGroupId(short FeedBackGroupId)
        {
            List<FeedBackGroups> RetVal = new List<FeedBackGroups>();
            try
            {
                if (FeedBackGroupId > 0)
                {
                    string sql = "SELECT * FROM FeedBackGroups WHERE (FeedBackGroupId=" + FeedBackGroupId.ToString() + ")";
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
        public FeedBackGroups Get(short FeedBackGroupId)
        {
            FeedBackGroups RetVal = new FeedBackGroups(db.ConnectionString);
            try
            {
                List<FeedBackGroups> list = GetListByFeedBackGroupId(FeedBackGroupId);
                if (list.Count > 0)
                {
                    RetVal = (FeedBackGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public FeedBackGroups Get()
        {
            return Get(this.FeedBackGroupId);
        }
        //-------------------------------------------------------------- 
        public static FeedBackGroups Static_Get(short FeedBackGroupId)
        {
            return Static_Get(FeedBackGroupId);
        }
        //--------------------------------------------------------------
    } 
}
