
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
    public class Sponsors
    {   
        private byte _LanguageId;
        private byte _ApplicationTypeId;
	    private short _SponsorId;
	    private string _SponsorName;
	    private string _ContactName;
	    private string _ContactPhone;
	    private string _ContactTel;
	    private string _ContactEmail;
	    private string _ContactFax;
	    private string _SponsorLogoPath;
	    private string _SponsorWebsiteUrl;
	    private string _ProfileDetail;
	    private short _DisplayOrder;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _SponsorStatusId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Sponsors()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Sponsors(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Sponsors()
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
	    public short SponsorId
        {
		    get { return _SponsorId; }
		    set { _SponsorId = value; }
	    }
        //-----------------------------------------------------------------
        public string SponsorName
		{
            get { return _SponsorName; }
		    set { _SponsorName = value; }
		}    
        //-----------------------------------------------------------------
        public string ContactName
		{
            get { return _ContactName; }
		    set { _ContactName = value; }
		}    
        //-----------------------------------------------------------------
        public string ContactPhone
		{
            get { return _ContactPhone; }
		    set { _ContactPhone = value; }
		}    
        //-----------------------------------------------------------------
        public string ContactTel
		{
            get { return _ContactTel; }
		    set { _ContactTel = value; }
		}    
        //-----------------------------------------------------------------
        public string ContactEmail
		{
            get { return _ContactEmail; }
		    set { _ContactEmail = value; }
		}    
        //-----------------------------------------------------------------
        public string ContactFax
		{
            get { return _ContactFax; }
		    set { _ContactFax = value; }
		}    
        //-----------------------------------------------------------------
        public string SponsorLogoPath
		{
            get { return _SponsorLogoPath; }
		    set { _SponsorLogoPath = value; }
		}    
        //-----------------------------------------------------------------
        public string SponsorWebsiteUrl
		{
            get { return _SponsorWebsiteUrl; }
		    set { _SponsorWebsiteUrl = value; }
		}    
        //-----------------------------------------------------------------
        public string ProfileDetail
		{
            get { return _ProfileDetail; }
		    set { _ProfileDetail = value; }
		}    
        //-----------------------------------------------------------------
        public short DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
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
    
	    public byte SponsorStatusId
        {
		    get { return _SponsorStatusId; }
		    set { _SponsorStatusId = value; }
	    }
        private List<Sponsors> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Sponsors> l_Sponsors = new List<Sponsors>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Sponsors m_Sponsors = new Sponsors(db.ConnectionString);
                    m_Sponsors.LanguageId = smartReader.GetByte("LanguageId");
                    m_Sponsors.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Sponsors.SponsorId = smartReader.GetInt16("SponsorId");
                    m_Sponsors.SponsorName = smartReader.GetString("SponsorName");
                    m_Sponsors.ContactName = smartReader.GetString("ContactName");
                    m_Sponsors.ContactPhone = smartReader.GetString("ContactPhone");
                    m_Sponsors.ContactTel = smartReader.GetString("ContactTel");
                    m_Sponsors.ContactEmail = smartReader.GetString("ContactEmail");
                    m_Sponsors.ContactFax = smartReader.GetString("ContactFax");
                    m_Sponsors.SponsorLogoPath = smartReader.GetString("SponsorLogoPath");
                    m_Sponsors.SponsorWebsiteUrl = smartReader.GetString("SponsorWebsiteUrl");
                    m_Sponsors.ProfileDetail = smartReader.GetString("ProfileDetail");
                    m_Sponsors.SponsorStatusId = smartReader.GetByte("SponsorStatusId");
                    m_Sponsors.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_Sponsors.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Sponsors.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Sponsors.Add(m_Sponsors);
                }
                reader.Close();
                return l_Sponsors;
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
                SqlCommand cmd = new SqlCommand("Sponsors_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SponsorName", this.SponsorName));
                cmd.Parameters.Add(new SqlParameter("@ContactName", this.ContactName));
                cmd.Parameters.Add(new SqlParameter("@ContactPhone", this.ContactPhone));
                cmd.Parameters.Add(new SqlParameter("@ContactTel", this.ContactTel));
                cmd.Parameters.Add(new SqlParameter("@ContactEmail", this.ContactEmail));
                cmd.Parameters.Add(new SqlParameter("@ContactFax", this.ContactFax));
                cmd.Parameters.Add(new SqlParameter("@SponsorLogoPath", this.SponsorLogoPath));
                cmd.Parameters.Add(new SqlParameter("@SponsorWebsiteUrl", this.SponsorWebsiteUrl));
                cmd.Parameters.Add(new SqlParameter("@ProfileDetail", this.ProfileDetail));
                cmd.Parameters.Add(new SqlParameter("@SponsorStatusId", this.SponsorStatusId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SponsorId", this.SponsorId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SponsorId =Convert.ToInt16((cmd.Parameters["@SponsorId"].Value == null) ? 0 : (cmd.Parameters["@SponsorId"].Value));
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
        public byte UpdateSponsorStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Sponsors_UpdateSponsorStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SponsorId", this.SponsorId));
                cmd.Parameters.Add(new SqlParameter("@SponsorStatusId", this.SponsorStatusId));
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
                SqlCommand cmd = new SqlCommand("Sponsors_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SponsorId",this.SponsorId));
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
        public List<Sponsors> GetList()
        {
            List<Sponsors> RetVal = new List<Sponsors>();
            try
            {
                string sql = "SELECT * FROM V$Sponsors";
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
        public List<Sponsors> GetList( byte LanguageId, byte ApplicationTypeId)
        {
            List<Sponsors> RetVal = new List<Sponsors>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string SponsorName = "";
                byte SponsorStatusId = 2;
                int RowCount = 0;
                short SponsorId = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SponsorId, LanguageId, ApplicationTypeId, SponsorName, SponsorStatusId, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static List<Sponsors> Static_GetList(string ConStr)
        {
            List<Sponsors> RetVal = new List<Sponsors>();
            try
            {
                Sponsors m_Sponsors = new Sponsors(ConStr);
                RetVal = m_Sponsors.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Sponsors> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<Sponsors> GetListOrderBy(string OrderBy)
        {
            List<Sponsors> RetVal = new List<Sponsors>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Sponsors " ;
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
        public static List<Sponsors> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Sponsors m_Sponsors = new Sponsors(ConStr);
            return m_Sponsors.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Sponsors> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<Sponsors> GetListBySponsorId(short SponsorId, byte LanguageId, byte ApplicationTypeId)
        {
            List<Sponsors> RetVal = new List<Sponsors>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string SponsorName = "";
                byte SponsorStatusId = 0;
                int RowCount=0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SponsorId, LanguageId, ApplicationTypeId, SponsorName, SponsorStatusId, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Sponsors> GetListByMeetingId(int MeetingId, byte LanguageId, byte ApplicationTypeId)
        {
            List<Sponsors> RetVal = new List<Sponsors>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string SponsorName = "";
                byte SponsorStatusId = 0;
                int RowCount = 0;
                short SponsorId = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MeetingId, SponsorId, LanguageId, ApplicationTypeId, SponsorName, SponsorStatusId, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Sponsors Get(short SponsorId, byte LanguageId, byte ApplicationTypeId)
        {
            Sponsors RetVal = new Sponsors(db.ConnectionString);
            try
            {
                List<Sponsors> list = GetListBySponsorId(SponsorId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (Sponsors)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Sponsors Get()
        {
            return Get(this.SponsorId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Sponsors Static_Get(string Constr, short SponsorId, byte LanguageId, byte ApplicationTypeId)
        {
            Sponsors m_Sponsors = new Sponsors(Constr);
            return m_Sponsors.Get(SponsorId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Sponsors Static_Get(short SponsorId, byte LanguageId, byte ApplicationTypeId)
        {
            return Static_Get("",SponsorId, LanguageId, ApplicationTypeId);
        }
        //--------------------------------------------------------------     
        public List<Sponsors> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short SponsorId, byte LanguageId, byte ApplicationTypeId, string SponsorName, byte SponsorStatusId, ref int RowCount)
        {
            int MeetingId = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MeetingId, SponsorId, LanguageId, ApplicationTypeId, SponsorName, SponsorStatusId, ref RowCount);
        }
        //--------------------------------------------------------------     
        public List<Sponsors> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int MeetingId, short SponsorId, byte LanguageId, byte ApplicationTypeId, string SponsorName, byte SponsorStatusId, ref int RowCount)
        {
            List<Sponsors> RetVal = new List<Sponsors>();
            try
            {
                SqlCommand cmd = new SqlCommand("Sponsors_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MeetingId", MeetingId));
                cmd.Parameters.Add(new SqlParameter("@SponsorId", SponsorId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SponsorName", StringUtil.InjectionString(SponsorName)));
                cmd.Parameters.Add(new SqlParameter("@SponsorStatusId", SponsorStatusId));
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
        public List<Sponsors> Search(int ActUserId, string OrderBy, short SponsorId, byte LanguageId, byte ApplicationTypeId, string SponsorName, byte SponsorStatusId, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SponsorId, LanguageId, ApplicationTypeId, SponsorName, SponsorStatusId, ref RowCount);
        }
    } 
}

