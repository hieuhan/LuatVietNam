
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
    public class ArticleRateDetails
    {
        private int _ArticleRateDetailId;
        private int _ArticleId;
        private byte _RateTypeId;
        private string _FromIP;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticleRateDetails()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticleRateDetails(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticleRateDetails()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleRateDetailId
        {
            get { return _ArticleRateDetailId; }
            set { _ArticleRateDetailId = value; }
        }
        //-----------------------------------------------------------------
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        //-----------------------------------------------------------------
        public byte RateTypeId
        {
            get { return _RateTypeId; }
            set { _RateTypeId = value; }
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

        private List<ArticleRateDetails> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleRateDetails> l_ArticleRateDetails = new List<ArticleRateDetails>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleRateDetails m_ArticleRateDetails = new ArticleRateDetails(db.ConnectionString);
                    m_ArticleRateDetails.ArticleRateDetailId = smartReader.GetInt32("ArticleRateDetailId");
                    m_ArticleRateDetails.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleRateDetails.RateTypeId = smartReader.GetByte("RateTypeId");
                    m_ArticleRateDetails.FromIP = smartReader.GetString("FromIP");
                    m_ArticleRateDetails.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_ArticleRateDetails.Add(m_ArticleRateDetails);
                }
                reader.Close();
                return l_ArticleRateDetails;
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
                SqlCommand cmd = new SqlCommand("ArticleRateDetails_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@RateTypeId", this.RateTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add("@ArticleRateDetailId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleRateDetailId = Convert.ToInt32((cmd.Parameters["@ArticleRateDetailId"].Value == null) ? 0 : (cmd.Parameters["@ArticleRateDetailId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleRateDetails_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@RateTypeId", this.RateTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
                cmd.Parameters.Add(new SqlParameter("@ArticleRateDetailId", this.ArticleRateDetailId));
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
                SqlCommand cmd = new SqlCommand("ArticleRateDetails_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleRateDetailId", this.ArticleRateDetailId));
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
        public List<ArticleRateDetails> GetList()
        {
            List<ArticleRateDetails> RetVal = new List<ArticleRateDetails>();
            try
            {
                string sql = "SELECT * FROM V$ArticleRateDetails";
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
        public List<ArticleRateDetails> GetListOrderBy(string OrderBy)
        {
            List<ArticleRateDetails> RetVal = new List<ArticleRateDetails>();
            try
            {
                string sql = "SELECT * FROM V$ArticleRateDetails ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<ArticleRateDetails> GetListByArticleRateDetailId(int ArticleRateDetailId)
        {
            List<ArticleRateDetails> RetVal = new List<ArticleRateDetails>();
            try
            {
                if (ArticleRateDetailId > 0)
                {
                    string sql = "SELECT * FROM V$ArticleRateDetails WHERE (ArticleRateDetailId=" + ArticleRateDetailId.ToString() + ")";
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
        public ArticleRateDetails Get(int ArticleRateDetailId)
        {
            ArticleRateDetails RetVal = new ArticleRateDetails(db.ConnectionString);
            try
            {
                List<ArticleRateDetails> list = GetListByArticleRateDetailId(ArticleRateDetailId);
                if (list.Count > 0)
                {
                    RetVal = (ArticleRateDetails)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ArticleRateDetails Get()
        {
            return Get(this.ArticleRateDetailId);
        }
        //-------------------------------------------------------------- 
        public static ArticleRateDetails Static_Get(string Constr, int ArticleRateDetailId)
        {
            ArticleRateDetails m_ArticleRateDetails = new ArticleRateDetails(Constr);
            return m_ArticleRateDetails.Get(ArticleRateDetailId);
        }
        //-------------------------------------------------------------- 
        public static ArticleRateDetails Static_Get(int ArticleRateDetailId)
        {
            return Static_Get("", ArticleRateDetailId);
        }
        //-------------------------------------------------------------- 
        public static List<ArticleRateDetails> Static_GetList(string ConStr)
        {
            List<ArticleRateDetails> RetVal = new List<ArticleRateDetails>();
            try
            {
                ArticleRateDetails m_ArticleRateDetails = new ArticleRateDetails(ConStr);
                RetVal = m_ArticleRateDetails.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ArticleRateDetails> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
        public List<ArticleRateDetails> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            List<ArticleRateDetails> RetVal = new List<ArticleRateDetails>();
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleRateDetails_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@RateTypeId", this.RateTypeId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", this.FromIP));
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
        public List<ArticleRateDetails> Search(int ActUserId, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DateFrom, DateTo, ref RowCount);
        }
    }
}

