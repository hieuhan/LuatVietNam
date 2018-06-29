
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
    public class CrawlerRules
    {
        private int _CrawlerRuleId;
        private short _DataSourceId;
        private short _CategoryId;
        private string _TagName;
        private string _TagClass;
        private string _TagId;
        private int _DataPosition;
        private byte _CrawlerContentTypeId;
        private byte _StatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CrawlerRules()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CrawlerRules(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CrawlerRules()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CrawlerRuleId
        {
            get { return _CrawlerRuleId; }
            set { _CrawlerRuleId = value; }
        }
        //-----------------------------------------------------------------
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------
        public string TagName
        {
            get { return _TagName; }
            set { _TagName = value; }
        }
        //-----------------------------------------------------------------
        public string TagClass
        {
            get { return _TagClass; }
            set { _TagClass = value; }
        }
        //-----------------------------------------------------------------
        public string TagId
        {
            get { return _TagId; }
            set { _TagId = value; }
        }
        //-----------------------------------------------------------------
        public int DataPosition
        {
            get { return _DataPosition; }
            set { _DataPosition = value; }
        }
        //-----------------------------------------------------------------
        public byte CrawlerContentTypeId
        {
            get { return _CrawlerContentTypeId; }
            set { _CrawlerContentTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte StatusId
        {
            get { return _StatusId; }
            set { _StatusId = value; }
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

        private List<CrawlerRules> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CrawlerRules> l_CrawlerRules = new List<CrawlerRules>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CrawlerRules m_CrawlerRules = new CrawlerRules(db.ConnectionString);
                    m_CrawlerRules.CrawlerRuleId = smartReader.GetInt32("CrawlerRuleId");
                    m_CrawlerRules.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_CrawlerRules.CategoryId = smartReader.GetInt16("CategoryId");
                    m_CrawlerRules.TagName = smartReader.GetString("TagName");
                    m_CrawlerRules.TagClass = smartReader.GetString("TagClass");
                    m_CrawlerRules.TagId = smartReader.GetString("TagId");
                    m_CrawlerRules.DataPosition = smartReader.GetInt32("DataPosition");
                    m_CrawlerRules.CrawlerContentTypeId = smartReader.GetByte("CrawlerContentTypeId");
                    m_CrawlerRules.StatusId = smartReader.GetByte("StatusId");
                    m_CrawlerRules.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CrawlerRules.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_CrawlerRules.Add(m_CrawlerRules);
                }
                reader.Close();
                return l_CrawlerRules;
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
                SqlCommand cmd = new SqlCommand("CrawlerRules_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@TagName", this.TagName));
                cmd.Parameters.Add(new SqlParameter("@TagClass", this.TagClass));
                cmd.Parameters.Add(new SqlParameter("@TagId", this.TagId));
                cmd.Parameters.Add(new SqlParameter("@DataPosition", this.DataPosition));
                cmd.Parameters.Add(new SqlParameter("@CrawlerContentTypeId", this.CrawlerContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add("@CrawlerRuleId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CrawlerRuleId = (cmd.Parameters["@CrawlerRuleId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@CrawlerRuleId"].Value);
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
                SqlCommand cmd = new SqlCommand("CrawlerRules_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@TagName", this.TagName));
                cmd.Parameters.Add(new SqlParameter("@TagClass", this.TagClass));
                cmd.Parameters.Add(new SqlParameter("@TagId", this.TagId));
                cmd.Parameters.Add(new SqlParameter("@DataPosition", this.DataPosition));
                cmd.Parameters.Add(new SqlParameter("@CrawlerContentTypeId", this.CrawlerContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CrawlerRuleId", this.CrawlerRuleId));
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CrawlerRules_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@TagName", this.TagName));
                cmd.Parameters.Add(new SqlParameter("@TagClass", this.TagClass));
                cmd.Parameters.Add(new SqlParameter("@TagId", this.TagId));
                cmd.Parameters.Add(new SqlParameter("@DataPosition", this.DataPosition));
                cmd.Parameters.Add(new SqlParameter("@CrawlerContentTypeId", this.CrawlerContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CrawlerRuleId", this.CrawlerRuleId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CrawlerRuleId = (cmd.Parameters["@CrawlerRuleId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@CrawlerRuleId"].Value);
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
                SqlCommand cmd = new SqlCommand("CrawlerRules_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CrawlerRuleId", this.CrawlerRuleId));
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
        public List<CrawlerRules> GetListByCrawlerRuleId(int CrawlerRuleId)
        {
            List<CrawlerRules> RetVal = new List<CrawlerRules>();
            try
            {
                if (CrawlerRuleId > 0)
                {
                    string sql = "SELECT * FROM CrawlerRules WHERE (CrawlerRuleId=" + CrawlerRuleId.ToString() + ")";
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
        public CrawlerRules Get(int CrawlerRuleId)
        {
            CrawlerRules RetVal = new CrawlerRules();
            try
            {
                List<CrawlerRules> list = GetListByCrawlerRuleId(CrawlerRuleId);
                if (list.Count > 0)
                {
                    RetVal = (CrawlerRules)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static CrawlerRules Static_Get(int CrawlerRuleId, List<CrawlerRules> list)
        {
            CrawlerRules RetVal = new CrawlerRules();
            try
            {
                RetVal = list.Find(i => i.CrawlerRuleId == CrawlerRuleId);
                if (RetVal == null) RetVal = new CrawlerRules();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<CrawlerRules> Static_GetList(short DataSourceId, short CategoryId, byte CrawlerContentTypeId, List<CrawlerRules> list)
        {
            List<CrawlerRules> RetVal = new List<CrawlerRules>();
            try
            {
                RetVal = list.FindAll(i => i.DataSourceId == DataSourceId && i.CategoryId == CategoryId && i._CrawlerContentTypeId == CrawlerContentTypeId);
                if (RetVal.Count == 0 && CategoryId > 0) RetVal = list.FindAll(i => i.DataSourceId == DataSourceId && i.CategoryId == 0 && i._CrawlerContentTypeId == CrawlerContentTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public CrawlerRules Get()
        {
            return Get(this.CrawlerRuleId);
        }
        //-------------------------------------------------------------- 
        public List<CrawlerRules> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short SiteId, ref int RowCount)
        {
            List<CrawlerRules> RetVal = new List<CrawlerRules>();
            try
            {
                SqlCommand cmd = new SqlCommand("CrawlerRules_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
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
    }
}

