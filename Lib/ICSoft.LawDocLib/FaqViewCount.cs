
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class FaqViewCount
    {
        private int _FaqId;
        private string _Question;
        private byte _LanguageId;
        private string _FaqCode;
        private byte _FaqTypeId;
        private byte _FaqGroupId;
        private short _FieldId;
        private short _DataSourceId;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private int _ViewCount;
        private DateTime _CrDateTime;
        private DateTime _LastViewTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public FaqViewCount()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public FaqViewCount(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FaqViewCount()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int FaqId
        {
            get { return _FaqId; }
            set { _FaqId = value; }
        }
        //-----------------------------------------------------------------
        public string Question
        {
            get { return _Question; }
            set { _Question = value; }
        }
        //-----------------------------------------------------------------
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------
        public string FaqCode
        {
            get { return _FaqCode; }
            set { _FaqCode = value; }
        }
        //-----------------------------------------------------------------
        public byte FaqTypeId
        {
            get { return _FaqTypeId; }
            set { _FaqTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte FaqGroupId
        {
            get { return _FaqGroupId; }
            set { _FaqGroupId = value; }
        }
        //-----------------------------------------------------------------
        public short FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }
        //-----------------------------------------------------------------
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
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
        public int ViewCount
        {
            get { return _ViewCount; }
            set { _ViewCount = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime LastViewTime
        {
            get { return _LastViewTime; }
            set { _LastViewTime = value; }
        }
        //-----------------------------------------------------------------

        private List<FaqViewCount> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FaqViewCount> l_FaqViewCount = new List<FaqViewCount>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FaqViewCount m_FaqViewCount = new FaqViewCount(db.ConnectionString);
                    m_FaqViewCount.FaqId = smartReader.GetInt32("FaqId");
                    m_FaqViewCount.Question = smartReader.GetString("Question");
                    m_FaqViewCount.LanguageId = smartReader.GetByte("LanguageId");
                    m_FaqViewCount.FaqCode = smartReader.GetString("FaqCode");
                    m_FaqViewCount.FaqTypeId = smartReader.GetByte("FaqTypeId");
                    m_FaqViewCount.FaqGroupId = smartReader.GetByte("FaqGroupId");
                    m_FaqViewCount.FieldId = smartReader.GetInt16("FieldId");
                    m_FaqViewCount.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_FaqViewCount.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_FaqViewCount.CrUserId = smartReader.GetInt32("CrUserId");
                    m_FaqViewCount.ViewCount = smartReader.GetInt32("ViewCount");
                    m_FaqViewCount.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_FaqViewCount.LastViewTime = smartReader.GetDateTime("LastViewTime");

                    l_FaqViewCount.Add(m_FaqViewCount);
                }
                reader.Close();
                return l_FaqViewCount;
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
                SqlCommand cmd = new SqlCommand("FaqViewCount_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ViewCount", this.ViewCount));
                cmd.Parameters.Add(new SqlParameter("@LastViewTime", this.LastViewTime));
                cmd.Parameters.Add("@FaqId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FaqId = Convert.ToInt32((cmd.Parameters["@FaqId"].Value == null) ? 0 : (cmd.Parameters["@FaqId"].Value));
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
        public byte FaqViewCount_Update()
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("FaqViewCount_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ViewCount", this.ViewCount));
                cmd.Parameters.Add(new SqlParameter("@LastViewTime", this.LastViewTime));
                cmd.Parameters.Add(new SqlParameter("@FaqId", this.FaqId));
                db.ExecuteSQL(cmd);
                RetVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<FaqViewCount> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string Question, byte LanguageId, string FaqCode, byte FaqTypeId, 
                                            byte FaqGroupId, short FieldId, short DataSourceId, byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<FaqViewCount> RetVal = new List<FaqViewCount>();
            try
            {
                SqlCommand cmd = new SqlCommand("FaqViewCount_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@Question", StringUtil.InjectionString(Question)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FaqCode", StringUtil.InjectionString(FaqCode)));
                cmd.Parameters.Add(new SqlParameter("@FaqTypeId", FaqTypeId));
                cmd.Parameters.Add(new SqlParameter("@FaqGroupId", FaqGroupId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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
       
    }
}