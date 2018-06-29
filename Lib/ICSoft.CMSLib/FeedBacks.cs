
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
    public class FeedBacks
    {
        private int _FeedBackId;
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private short _SiteId;
        private short _FeedBackGroupId;
        private int _UserId;
        private string _OrganName;
        private string _FullName;
        private string _Email;
        private string _PhoneNumber;
        private string _Address;
        private string _Title;
        private string _Comment;
        private byte _RatingScore;
        private byte _ReviewStatusId;
        private string _FromIP;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public FeedBacks()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public FeedBacks(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FeedBacks()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int FeedBackId
        {
            get { return _FeedBackId; }
            set { _FeedBackId = value; }
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
        public short FeedBackGroupId
        {
            get { return _FeedBackGroupId; }
            set { _FeedBackGroupId = value; }
        }
        //-----------------------------------------------------------------
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //-----------------------------------------------------------------
        public string OrganName
        {
            get { return _OrganName; }
            set { _OrganName = value; }
        }
        //-----------------------------------------------------------------
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        //-----------------------------------------------------------------
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        //-----------------------------------------------------------------
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //-----------------------------------------------------------------
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        //-----------------------------------------------------------------
        public byte RatingScore
        {
            get { return _RatingScore; }
            set { _RatingScore = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string FromIP
        {
            get { return _FromIP; }
            set { _FromIP = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<FeedBacks> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FeedBacks> l_FeedBacks = new List<FeedBacks>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FeedBacks m_FeedBacks = new FeedBacks(db.ConnectionString);
                    m_FeedBacks.FeedBackId = smartReader.GetInt32("FeedBackId");
                    m_FeedBacks.LanguageId = smartReader.GetByte("LanguageId");
                    m_FeedBacks.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_FeedBacks.SiteId = smartReader.GetInt16("SiteId");
                    m_FeedBacks.FeedBackGroupId = smartReader.GetInt16("FeedBackGroupId");
                    m_FeedBacks.UserId = smartReader.GetInt32("UserId");
                    m_FeedBacks.OrganName = smartReader.GetString("OrganName");
                    m_FeedBacks.FullName = smartReader.GetString("FullName");
                    m_FeedBacks.Email = smartReader.GetString("Email");
                    m_FeedBacks.PhoneNumber = smartReader.GetString("PhoneNumber");
                    m_FeedBacks.Address = smartReader.GetString("Address");
                    m_FeedBacks.Title = smartReader.GetString("Title");
                    m_FeedBacks.Comment = smartReader.GetString("Comment");
                    m_FeedBacks.RatingScore = smartReader.GetByte("RatingScore");
                    m_FeedBacks.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_FeedBacks.FromIP = smartReader.GetString("FromIP");
                    m_FeedBacks.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_FeedBacks.Add(m_FeedBacks);
                }
                reader.Close();
                return l_FeedBacks;
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

        //-----------------------------------------------------------------

        private List<FeedBacks> InitRateScore(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FeedBacks> l_FeedBacks = new List<FeedBacks>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FeedBacks m_FeedBacks = new FeedBacks(db.ConnectionString);
                    m_FeedBacks.UserId = smartReader.GetInt32("CountTotal");
                    m_FeedBacks.RatingScore = smartReader.GetByte("RatingScore");
                    l_FeedBacks.Add(m_FeedBacks);
                }
                reader.Close();
                return l_FeedBacks;
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
                SqlCommand cmd = new SqlCommand("FeedBacks_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@FeedBackGroupId", this.FeedBackGroupId));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                cmd.Parameters.Add(new SqlParameter("@OrganName", this.OrganName));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
                cmd.Parameters.Add(new SqlParameter("@Comment", this.Comment));
                cmd.Parameters.Add(new SqlParameter("@RatingScore", this.RatingScore));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@FeedBackId", this.FeedBackId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FeedBackId = Convert.ToInt32((cmd.Parameters["@FeedBackId"].Value == null) ? 0 : (cmd.Parameters["@FeedBackId"].Value));
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
                SqlCommand cmd = new SqlCommand("FeedBacks_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeedBackId", this.FeedBackId));
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
        public List<FeedBacks> GetList()
        {
            List<FeedBacks> RetVal = new List<FeedBacks>();
            try
            {
                string sql = "SELECT TOP(500) * FROM FeedBacks";
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
        public static List<FeedBacks> Static_GetList(string ConStr)
        {
            List<FeedBacks> RetVal = new List<FeedBacks>();
            try
            {
                FeedBacks m_FeedBacks = new FeedBacks(ConStr);
                RetVal = m_FeedBacks.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<FeedBacks> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<FeedBacks> GetListOrderBy(string OrderBy)
        {
            List<FeedBacks> RetVal = new List<FeedBacks>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT TOP(500) * FROM FeedBacks ";
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
        public static List<FeedBacks> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            FeedBacks m_FeedBacks = new FeedBacks(ConStr);
            return m_FeedBacks.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<FeedBacks> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }        
        //--------------------------------------------------------------  
        public List<FeedBacks> GetListByFeedBackId(int FeedBackId)
        {
            List<FeedBacks> RetVal = new List<FeedBacks>();
            try
            {
                if (FeedBackId > 0)
                {
                    string sql = "SELECT * FROM FeedBacks WHERE (FeedBackId=" + FeedBackId.ToString() + ")";
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
        public FeedBacks Get(int FeedBackId)
        {
            FeedBacks RetVal = new FeedBacks(db.ConnectionString);
            try
            {
                List<FeedBacks> list = GetListByFeedBackId(FeedBackId);
                if (list.Count > 0)
                {
                    RetVal = (FeedBacks)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public FeedBacks Get()
        {
            return Get(this.FeedBackId);
        }
        //-------------------------------------------------------------- 
        public static FeedBacks Static_Get(int FeedBackId)
        {
            return Static_Get(FeedBackId);
        }
        //--------------------------------------------------------------     
        public List<FeedBacks> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, short FeedBackGroupId, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<FeedBacks> RetVal = new List<FeedBacks>();
            try
            {
                SqlCommand cmd = new SqlCommand("FeedBacks_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@FeedBackGroupId", FeedBackGroupId));
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
        public List<FeedBacks> GetCountByRatingScore( short FeedBackGroupId, ref int RowCount)
        {
            List<FeedBacks> RetVal = new List<FeedBacks>();
            try
            {
                SqlCommand cmd = new SqlCommand("FeedBacks_GetCountByRatingScore");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FeedBackGroupId", FeedBackGroupId));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = InitRateScore(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<FeedBacks> Search(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, short FeedBackGroupId, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, FeedBackGroupId, ReviewStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}