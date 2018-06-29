
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
    public class MemberProfiles
    {
        private int _MemberProfileId;
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private string _Title;
        private string _Organizartion;
        private string _Regency;
        private string _MemberProfileContent;
        private string _OrganizartionProfile;
        private byte _ReciveMail;
        private byte _ShowLocation;
        private byte _ReciveAlert;
        private byte _ShowProfile;
        private byte _SynForum;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _MemberId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public MemberProfiles()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MemberProfiles(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MemberProfiles()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MemberProfileId
        {
            get { return _MemberProfileId; }
            set { _MemberProfileId = value; }
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
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //-----------------------------------------------------------------
        public string Organizartion
        {
            get { return _Organizartion; }
            set { _Organizartion = value; }
        }
        //-----------------------------------------------------------------
        public string Regency
        {
            get { return _Regency; }
            set { _Regency = value; }
        }
        //-----------------------------------------------------------------
        public string MemberProfileContent
        {
            get { return _MemberProfileContent; }
            set { _MemberProfileContent = value; }
        }
        //-----------------------------------------------------------------
        public string OrganizartionProfile
        {
            get { return _OrganizartionProfile; }
            set { _OrganizartionProfile = value; }
        }
        //-----------------------------------------------------------------
        public byte ReciveMail
        {
            get { return _ReciveMail; }
            set { _ReciveMail = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowLocation
        {
            get { return _ShowLocation; }
            set { _ShowLocation = value; }
        }
        //-----------------------------------------------------------------
        public byte ReciveAlert
        {
            get { return _ReciveAlert; }
            set { _ReciveAlert = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowProfile
        {
            get { return _ShowProfile; }
            set { _ShowProfile = value; }
        }
        //-----------------------------------------------------------------
        public byte SynForum
        {
            get { return _SynForum; }
            set { _SynForum = value; }
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

        public int MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private List<MemberProfiles> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MemberProfiles> l_MemberProfiles = new List<MemberProfiles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MemberProfiles m_MemberProfiles = new MemberProfiles(db.ConnectionString);
                    m_MemberProfiles.MemberProfileId = smartReader.GetInt32("MemberProfileId");
                    m_MemberProfiles.MemberId = smartReader.GetInt32("MemberId");
                    m_MemberProfiles.LanguageId = smartReader.GetByte("LanguageId");
                    m_MemberProfiles.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_MemberProfiles.Title = smartReader.GetString("Title");
                    m_MemberProfiles.Organizartion = smartReader.GetString("Organizartion");
                    m_MemberProfiles.Regency = smartReader.GetString("Regency");
                    m_MemberProfiles.MemberProfileContent = smartReader.GetString("MemberProfileContent");
                    m_MemberProfiles.OrganizartionProfile = smartReader.GetString("OrganizartionProfile");
                    m_MemberProfiles.ReciveMail = smartReader.GetByte("ReciveMail");
                    m_MemberProfiles.ShowLocation = smartReader.GetByte("ShowLocation");
                    m_MemberProfiles.ReciveAlert = smartReader.GetByte("ReciveAlert");
                    m_MemberProfiles.ShowProfile = smartReader.GetByte("ShowProfile");
                    m_MemberProfiles.SynForum = smartReader.GetByte("SynForum");
                    m_MemberProfiles.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MemberProfiles.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_MemberProfiles.Add(m_MemberProfiles);
                }
                reader.Close();
                return l_MemberProfiles;
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
                SqlCommand cmd = new SqlCommand("MemberProfiles_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
                cmd.Parameters.Add(new SqlParameter("@Organizartion", this.Organizartion));
                cmd.Parameters.Add(new SqlParameter("@Regency", this.Regency));
                cmd.Parameters.Add(new SqlParameter("@MemberProfileContent", this.MemberProfileContent));
                cmd.Parameters.Add(new SqlParameter("@OrganizartionProfile", this.OrganizartionProfile));
                cmd.Parameters.Add(new SqlParameter("@ReciveMail", this.ReciveMail));
                cmd.Parameters.Add(new SqlParameter("@ShowLocation", this.ShowLocation));
                cmd.Parameters.Add(new SqlParameter("@ReciveAlert", this.ReciveAlert));
                cmd.Parameters.Add(new SqlParameter("@ShowProfile", this.ShowProfile));
                cmd.Parameters.Add(new SqlParameter("@SynForum", this.SynForum));
                cmd.Parameters.Add(new SqlParameter("@MemberProfileId", this.MemberProfileId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MemberProfileId = Convert.ToInt32((cmd.Parameters["@MemberProfileId"].Value == null) ? 0 : (cmd.Parameters["@MemberProfileId"].Value));
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
                SqlCommand cmd = new SqlCommand("MemberProfiles_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberProfileId", this.MemberProfileId));
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
        public List<MemberProfiles> GetListByMemberProfileId(int MemberProfileId)
        {
            List<MemberProfiles> RetVal = new List<MemberProfiles>();
            try
            {
                if (MemberProfileId > 0)
                {
                    string sql = "SELECT * FROM V$MemberProfiles WHERE (MemberProfileId=" + MemberProfileId.ToString() + ")";
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
        public MemberProfiles Get(int MemberProfileId)
        {
            MemberProfiles RetVal = new MemberProfiles(db.ConnectionString);
            try
            {
                List<MemberProfiles> list = GetListByMemberProfileId(MemberProfileId);
                if (list.Count > 0)
                {
                    RetVal = (MemberProfiles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MemberProfiles Get()
        {
            return Get(this.MemberProfileId);
        }
        //-------------------------------------------------------------- 
        public static MemberProfiles Static_Get(int MemberProfileId)
        {
            return Static_Get(MemberProfileId);
        }
        //--------------------------------------------------------------  
        public List<MemberProfiles> GetListByMemberId(int MemberId, byte LanguageId, byte ApplicationTypeId)
        {
            List<MemberProfiles> RetVal = new List<MemberProfiles>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";                
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberId, LanguageId, ApplicationTypeId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MemberProfiles GetByMemberId(int MemberId, byte LanguageId, byte ApplicationTypeId)
        {
            MemberProfiles RetVal = new MemberProfiles(db.ConnectionString);
            try
            {
                List<MemberProfiles> list = GetListByMemberId(MemberId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (MemberProfiles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        ////-------------------------------------------------------------- 
        public MemberProfiles GetByMemberId()
        {
            return GetByMemberId(this.MemberId, this.LanguageId, this.ApplicationTypeId);
        }
      
        //--------------------------------------------------------------     
        public List<MemberProfiles> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int MemberId, byte LanguageId, byte ApplicationTypeId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<MemberProfiles> RetVal = new List<MemberProfiles>();
            try
            {
                SqlCommand cmd = new SqlCommand("MemberProfiles_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MemberId", MemberId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
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
        public List<MemberProfiles> Search(int ActUserId, string OrderBy, int MemberId, byte LanguageId, byte ApplicationTypeId, string Title, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberId, LanguageId, ApplicationTypeId, DateFrom, DateTo, ref RowCount);
        }
    }
}