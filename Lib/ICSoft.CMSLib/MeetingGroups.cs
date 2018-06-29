
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class MeetingGroups
    {
        private byte _LanguageId;
        private byte _ApplicationTypeId;
	    private short _MeetingGroupId;
	    private string _MeetingGroupName;
	    private short _DisplayOrder;
	    private byte _MeetingGroupStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public MeetingGroups()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public MeetingGroups(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MeetingGroups()
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
	    public short MeetingGroupId
        {
		    get { return _MeetingGroupId; }
		    set { _MeetingGroupId = value; }
	    }
        //-----------------------------------------------------------------
        public string MeetingGroupName
		{
            get { return _MeetingGroupName; }
		    set { _MeetingGroupName = value; }
		}    
        //-----------------------------------------------------------------
        public short DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
		}    
        //-----------------------------------------------------------------
        public byte MeetingGroupStatusId
		{
            get { return _MeetingGroupStatusId; }
		    set { _MeetingGroupStatusId = value; }
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
    
        private List<MeetingGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MeetingGroups> l_MeetingGroups = new List<MeetingGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MeetingGroups m_MeetingGroups = new MeetingGroups(db.ConnectionString);
                    m_MeetingGroups.LanguageId = smartReader.GetByte("LanguageId");
                    m_MeetingGroups.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_MeetingGroups.MeetingGroupId = smartReader.GetInt16("MeetingGroupId");
                    m_MeetingGroups.MeetingGroupName = smartReader.GetString("MeetingGroupName");
                    m_MeetingGroups.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_MeetingGroups.MeetingGroupStatusId = smartReader.GetByte("MeetingGroupStatusId");
                    m_MeetingGroups.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MeetingGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_MeetingGroups.Add(m_MeetingGroups);
                }
                reader.Close();
                return l_MeetingGroups;
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
                SqlCommand cmd = new SqlCommand("MeetingGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupName", this.MeetingGroupName));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupStatusId", this.MeetingGroupStatusId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupId", this.MeetingGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MeetingGroupId = Convert.ToInt16((cmd.Parameters["@MeetingGroupId"].Value == null) ? 0 : (cmd.Parameters["@MeetingGroupId"].Value));
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
                SqlCommand cmd = new SqlCommand("MeetingGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupId",this.MeetingGroupId));
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
        public List<MeetingGroups> GetList()
        {
            List<MeetingGroups> RetVal = new List<MeetingGroups>();
            try
            {
                string sql = "SELECT * FROM V$MeetingGroups";
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
        public static List<MeetingGroups> Static_GetList(string ConStr)
        {
            List<MeetingGroups> RetVal = new List<MeetingGroups>();
            try
            {
                MeetingGroups m_MeetingGroups = new MeetingGroups(ConStr);
                RetVal = m_MeetingGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MeetingGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<MeetingGroups> Static_GetListAll(string ConStr ,string TextOptionAll = "...")
        {
            MeetingGroups m_MeetingGroups = new MeetingGroups(ConStr);
            List<MeetingGroups> RetVal = m_MeetingGroups.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_MeetingGroups.MeetingGroupName = TextOptionAll;
                m_MeetingGroups.MeetingGroupName = TextOptionAll;
                RetVal.Insert(0, m_MeetingGroups);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<MeetingGroups> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<MeetingGroups> GetListOrderBy(string OrderBy)
        {
            List<MeetingGroups> RetVal = new List<MeetingGroups>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$MeetingGroups " ;
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
        public static List<MeetingGroups> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            MeetingGroups m_MeetingGroups = new MeetingGroups(ConStr);
            return m_MeetingGroups.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MeetingGroups> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MeetingGroups> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<MeetingGroups> RetVal = new List<MeetingGroups>();
            MeetingGroups m_MeetingGroups = new MeetingGroups(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_MeetingGroups.GetListOrderBy(OrderBy);
                }               
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_MeetingGroups.MeetingGroupName = TextOptionAll;
                    m_MeetingGroups.MeetingGroupName = TextOptionAll;
                    RetVal.Insert(0, m_MeetingGroups);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<MeetingGroups> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<MeetingGroups> GetListByMeetingGroupId(short MeetingGroupId, byte LanguageId, byte ApplicationTypeId)
        {
            List<MeetingGroups> RetVal = new List<MeetingGroups>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string MeetingGroupName = "";
                byte MeetingGroupStatusId = 0;
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MeetingGroupId, LanguageId, ApplicationTypeId, MeetingGroupName, MeetingGroupStatusId, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<MeetingGroups> GetList( byte LanguageId, byte ApplicationTypeId)
        {
            List<MeetingGroups> RetVal = new List<MeetingGroups>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string MeetingGroupName = "";
                byte MeetingGroupStatusId = 0;
                short MeetingGroupId = 0;
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MeetingGroupId, LanguageId, ApplicationTypeId, MeetingGroupName, MeetingGroupStatusId, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<MeetingGroups> Static_GetList(byte LanguageId, byte ApplicationTypeId)
        {
            List<MeetingGroups> RetVal = new List<MeetingGroups>();
            MeetingGroups m_MeetingGroups = new MeetingGroups();
            RetVal = m_MeetingGroups.GetList(LanguageId, ApplicationTypeId);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MeetingGroups Get(short MeetingGroupId, byte LanguageId, byte ApplicationTypeId)
        {
            MeetingGroups RetVal = new MeetingGroups(db.ConnectionString);
            try
            {
                List<MeetingGroups> list = GetListByMeetingGroupId(MeetingGroupId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (MeetingGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MeetingGroups Get()
        {
            return Get(this.MeetingGroupId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static MeetingGroups Static_Get(short MeetingGroupId)
        {
            return Static_Get(MeetingGroupId);
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(short MeetingGroupId, byte LanguageId, byte ApplicationTypeId)
        {
            string RetVal = "";
            MeetingGroups m_MeetingGroups = new MeetingGroups();
            try
            {
                RetVal = m_MeetingGroups.Get(MeetingGroupId, LanguageId, ApplicationTypeId).MeetingGroupName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<MeetingGroups> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short MeetingGroupId, byte LanguageId, byte ApplicationTypeId, string MeetingGroupName, byte MeetingGroupStatusId, ref int RowCount)
        {
            List<MeetingGroups> RetVal = new List<MeetingGroups>();
            try
            {
                SqlCommand cmd = new SqlCommand("MeetingGroups_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupId", MeetingGroupId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupName", StringUtil.InjectionString(MeetingGroupName)));
                cmd.Parameters.Add(new SqlParameter("@MeetingGroupStatusId", MeetingGroupStatusId));
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
        public List<MeetingGroups> Search(int ActUserId, string OrderBy, short MeetingGroupId, byte LanguageId, byte ApplicationTypeId, string MeetingGroupName, byte MeetingGroupStatusId, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MeetingGroupId, LanguageId, ApplicationTypeId, MeetingGroupName, MeetingGroupStatusId, ref RowCount);
        }
    } 
}
