
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
    public class DataDictionaries
    {
        private int _DataDictionaryId;
        private string _DataDictionaryName = "";
        private string _DataDictionaryDesc = "";
        private int _MinValue;
        private int _MaxValue;
        private int _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private short _DataDictionaryTypeId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DataDictionaries()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DataDictionaries(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DataDictionaries()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DataDictionaryId
        {
            get { return _DataDictionaryId; }
            set { _DataDictionaryId = value; }
        }
        //-----------------------------------------------------------------
        public string DataDictionaryName
        {
            get { return _DataDictionaryName; }
            set { _DataDictionaryName = value; }
        }
        //-----------------------------------------------------------------
        public string DataDictionaryDesc
        {
            get { return _DataDictionaryDesc; }
            set { _DataDictionaryDesc = value; }
        }
        //-----------------------------------------------------------------
        public int MinValue
        {
            get { return _MinValue; }
            set { _MinValue = value; }
        }
        //-----------------------------------------------------------------
        public int MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
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

        public short DataDictionaryTypeId
        {
            get { return _DataDictionaryTypeId; }
            set { _DataDictionaryTypeId = value; }
        }
        private List<DataDictionaries> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DataDictionaries> l_DataDictionaries = new List<DataDictionaries>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DataDictionaries m_DataDictionaries = new DataDictionaries(db.ConnectionString);
                    m_DataDictionaries.DataDictionaryId = smartReader.GetInt32("DataDictionaryId");
                    m_DataDictionaries.DataDictionaryTypeId = smartReader.GetInt16("DataDictionaryTypeId");
                    m_DataDictionaries.DataDictionaryName = smartReader.GetString("DataDictionaryName");
                    m_DataDictionaries.DataDictionaryDesc = smartReader.GetString("DataDictionaryDesc");
                    m_DataDictionaries.MinValue = smartReader.GetInt32("MinValue");
                    m_DataDictionaries.MaxValue = smartReader.GetInt32("MaxValue");
                    m_DataDictionaries.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_DataDictionaries.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DataDictionaries.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DataDictionaries.Add(m_DataDictionaries);
                }
                reader.Close();
                return l_DataDictionaries;
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
                SqlCommand cmd = new SqlCommand("DataDictionaries_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId", this.DataDictionaryTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryName", this.DataDictionaryName));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryDesc", this.DataDictionaryDesc));
                cmd.Parameters.Add(new SqlParameter("@MinValue", this.MinValue));
                cmd.Parameters.Add(new SqlParameter("@MaxValue", this.MaxValue));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@DataDictionaryId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DataDictionaryId = (cmd.Parameters["@DataDictionaryId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@DataDictionaryId"].Value);
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
                SqlCommand cmd = new SqlCommand("DataDictionaries_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId", this.DataDictionaryTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryName", this.DataDictionaryName));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryDesc", this.DataDictionaryDesc));
                cmd.Parameters.Add(new SqlParameter("@MinValue", this.MinValue));
                cmd.Parameters.Add(new SqlParameter("@MaxValue", this.MaxValue));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryId", this.DataDictionaryId));
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
                SqlCommand cmd = new SqlCommand("DataDictionaries_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId", this.DataDictionaryTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryName", this.DataDictionaryName));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryDesc", this.DataDictionaryDesc));
                cmd.Parameters.Add(new SqlParameter("@MinValue", this.MinValue));
                cmd.Parameters.Add(new SqlParameter("@MaxValue", this.MaxValue));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryId", this.DataDictionaryId).Direction = ParameterDirection.InputOutput);
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DataDictionaryId = (cmd.Parameters["@DataDictionaryId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@DataDictionaryId"].Value);
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
                SqlCommand cmd = new SqlCommand("DataDictionaries_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryId", this.DataDictionaryId));
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
        public List<DataDictionaries> GetList(short DataDictionaryTypeId)
        {
            List<DataDictionaries> RetVal = new List<DataDictionaries>();
            try
            {
                string sql = "SELECT * FROM DataDictionaries";
                if (DataDictionaryTypeId > 0) 
                {
                    sql = sql + " WHERE DataDictionaryTypeId=" + DataDictionaryTypeId.ToString();
                }
                sql = sql + " ORDER BY DisplayOrder, DataDictionaryName";
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
        public List<DataDictionaries> GetList(short DataDictionaryTypeId, string TextOptionAll = "...")
        {
            List<DataDictionaries> RetVal = GetList(DataDictionaryTypeId);
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                DataDictionaries m_DataDictionaries = new DataDictionaries();
                m_DataDictionaries.DataDictionaryName = TextOptionAll;
                m_DataDictionaries.DataDictionaryDesc = TextOptionAll;
                RetVal.Insert(0, m_DataDictionaries);
            }
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<DataDictionaries> GetListOrderBy(short DataDictionaryTypeId, string OrderBy)
        {
            List<DataDictionaries> RetVal = new List<DataDictionaries>();
            try
            {
                string sql = "SELECT * FROM DataDictionaries WHERE DataDictionaryTypeId=" + DataDictionaryTypeId.ToString() + " ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<DataDictionaries> GetListOrderBy(short DataDictionaryTypeId, string OrderBy, string TextOptionAll = "...")
        {
            List<DataDictionaries> RetVal = GetListOrderBy(DataDictionaryTypeId, OrderBy);
            if (TextOptionAll == null) TextOptionAll = "";
            if (TextOptionAll.Length > 0)
            {
                DataDictionaries m_DataDictionaries = new DataDictionaries();
                m_DataDictionaries.DataDictionaryName = TextOptionAll;
                m_DataDictionaries.DataDictionaryDesc = TextOptionAll;
                RetVal.Insert(0, m_DataDictionaries);
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<DataDictionaries> GetListByDataDictionaryId(int DataDictionaryId)
        {
            List<DataDictionaries> RetVal = new List<DataDictionaries>();
            try
            {
                if (DataDictionaryId > 0)
                {
                    string sql = "SELECT * FROM DataDictionaries WHERE (DataDictionaryId=" + DataDictionaryId.ToString() + ")";
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
        public DataDictionaries Get(int DataDictionaryId)
        {
            DataDictionaries RetVal = new DataDictionaries();
            try
            {
                List<DataDictionaries> list = GetListByDataDictionaryId(DataDictionaryId);
                if (list.Count > 0)
                {
                    RetVal = (DataDictionaries)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataDictionaries Get(int DataDictionaryId, List<DataDictionaries> list)
        {
            DataDictionaries RetVal = new DataDictionaries();
            try
            {
                RetVal = list.Find(i => i.DataDictionaryId == DataDictionaryId);
                if (RetVal == null) RetVal = new DataDictionaries();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataDictionaries Get()
        {
            return Get(this.DataDictionaryId);
        }
        //-------------------------------------------------------------- 
        public static DataDictionaries Static_Get(string Constr, int DataDictionaryId)
        {
            DataDictionaries m_DataDictionaries = new DataDictionaries(Constr);
            return m_DataDictionaries.Get(DataDictionaryId);
        }
        //-------------------------------------------------------------- 
        public static DataDictionaries Static_Get(int DataDictionaryId)
        {
            return Static_Get("", DataDictionaryId);
        }
        //-------------------------------------------------------------- 
        public static List<DataDictionaries> Static_GetList(short DataDictionaryTypeId, string ConStr)
        {
            List<DataDictionaries> RetVal = new List<DataDictionaries>();
            try
            {
                DataDictionaries m_DataDictionaries = new DataDictionaries(ConStr);
                RetVal = m_DataDictionaries.GetList(DataDictionaryTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DataDictionaries> Static_GetList(short DataDictionaryTypeId)
        {
            return Static_GetList(DataDictionaryTypeId, "");
        }
        //--------------------------------------------------------------     
        public List<DataDictionaries> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DataDictionaries> RetVal = new List<DataDictionaries>();
            try
            {
                SqlCommand cmd = new SqlCommand("DataDictionaries_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId", this.DataDictionaryTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryName", this.DataDictionaryName));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryDesc", this.DataDictionaryDesc));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
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

