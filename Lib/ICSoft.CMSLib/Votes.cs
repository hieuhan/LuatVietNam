
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
    public class Votes
    {
        private byte _LanguageId;
        private byte _ApplicationTypeId;
        private int _VoteId;
        private string _VoteName;
        private string _VoteDesc;
        private byte _VoteTypeId;
        private DateTime _StartTime;
        private DateTime _EndTime;
        private short _SiteId;
        private short _CategoryId;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Votes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Votes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Votes()
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
        public int VoteId
        {
            get { return _VoteId; }
            set { _VoteId = value; }
        }
        //-----------------------------------------------------------------
        public string VoteName
        {
            get { return _VoteName; }
            set { _VoteName = value; }
        }
        //-----------------------------------------------------------------
        public string VoteDesc
        {
            get { return _VoteDesc; }
            set { _VoteDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte VoteTypeId
        {
            get { return _VoteTypeId; }
            set { _VoteTypeId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }    
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
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

        private List<Votes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Votes> l_Votes = new List<Votes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Votes m_Votes = new Votes(db.ConnectionString);
                    m_Votes.LanguageId = smartReader.GetByte("LanguageId");
                    m_Votes.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Votes.VoteId = smartReader.GetInt32("VoteId");
                    m_Votes.VoteName = smartReader.GetString("VoteName");
                    m_Votes.VoteDesc = smartReader.GetString("VoteDesc");
                    m_Votes.VoteTypeId = smartReader.GetByte("VoteTypeId");
                    m_Votes.StartTime = smartReader.GetDateTime("StartTime");
                    m_Votes.EndTime = smartReader.GetDateTime("EndTime");
                    m_Votes.SiteId = smartReader.GetInt16("SiteId");
                    m_Votes.CategoryId = smartReader.GetInt16("CategoryId");
                    m_Votes.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Votes.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Votes.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Votes.Add(m_Votes);
                }
                reader.Close();
                return l_Votes;
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
                SqlCommand cmd = new SqlCommand("Votes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@VoteName", this.VoteName));
                cmd.Parameters.Add(new SqlParameter("@VoteDesc", this.VoteDesc));
                cmd.Parameters.Add(new SqlParameter("@VoteTypeId", this.VoteTypeId));
                cmd.Parameters.Add(new SqlParameter("@StartTime", this.StartTime));
                cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@VoteId", this.VoteId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.VoteId = Convert.ToInt32((cmd.Parameters["@VoteId"].Value == null) ? 0 : (cmd.Parameters["@VoteId"].Value));
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
                SqlCommand cmd = new SqlCommand("Votes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@VoteId", this.VoteId));
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
        public List<Votes> GetList()
        {
            List<Votes> RetVal = new List<Votes>();
            try
            {
                string sql = "SELECT * FROM V$Votes";
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
        public static List<Votes> Static_GetList(string ConStr)
        {
            List<Votes> RetVal = new List<Votes>();
            try
            {
                Votes m_Votes = new Votes(ConStr);
                RetVal = m_Votes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Votes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<Votes> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            Votes m_Votes = new Votes(ConStr);
            List<Votes> RetVal = m_Votes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_Votes.VoteName = TextOptionAll;
                m_Votes.VoteDesc = TextOptionAll;
                RetVal.Insert(0, m_Votes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Votes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<Votes> GetListOrderBy(string OrderBy)
        {
            List<Votes> RetVal = new List<Votes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Votes ";
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
        public static List<Votes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Votes m_Votes = new Votes(ConStr);
            return m_Votes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Votes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Votes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<Votes> RetVal = new List<Votes>();
            Votes m_Votes = new Votes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_Votes.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_Votes.VoteName = TextOptionAll;
                    m_Votes.VoteDesc = TextOptionAll;
                    RetVal.Insert(0, m_Votes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Votes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<Votes> GetListByVoteId(int VoteId, byte LanguageId, byte ApplicationTypeId)
        {
            List<Votes> RetVal = new List<Votes>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string VoteName = "";
                byte VoteTypeId=0;
                short CategoryId=0;
                byte ReviewStatusId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, VoteId, VoteName, VoteTypeId, CategoryId, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public Votes Get(int VoteId, byte LanguageId, byte ApplicationTypeId)
        {
            Votes RetVal = new Votes(db.ConnectionString);
            try
            {
                List<Votes> list = GetListByVoteId(VoteId, LanguageId, ApplicationTypeId);
                if (list.Count > 0)
                {
                    RetVal = (Votes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Votes Get()
        {
            return Get(this.VoteId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Votes Static_Get(string Constr, int VoteId, byte LanguageId, byte ApplicationTypeId)
        {
            Votes m_Votes = new Votes(Constr);
            return m_Votes.Get(VoteId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Votes Static_Get(int VoteId, byte LanguageId, byte ApplicationTypeId)
        {
            return Static_Get("", VoteId, LanguageId, ApplicationTypeId);
        }
        //--------------------------------------------------------------     
        public List<Votes> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, int VoteId, string VoteName, byte VoteTypeId, short CategoryId, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Votes> RetVal = new List<Votes>();
            try
            {
                SqlCommand cmd = new SqlCommand("Votes_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@VoteId", VoteId));
                cmd.Parameters.Add(new SqlParameter("@VoteName", VoteName));
                cmd.Parameters.Add(new SqlParameter("@VoteTypeId", VoteTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
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
        public List<Votes> Search(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, int VoteId, string VoteName, byte VoteTypeId, short CategoryId, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, VoteId, VoteName, VoteTypeId, CategoryId, ReviewStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}