
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
    /// <summary>
    /// class Faqs
    /// </summary>
    public class Faqs
    {
        private int _FaqId;
        private string _Question;
        private string _Answer;
        private byte _LanguageId;
        private string _FaqCode;
        private byte _FaqTypeId;
        private byte _FaqGroupId;
        private short _FieldId;
        private int _LawerId;
        private short _DataSourceId;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Faqs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Faqs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Faqs()
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
        public string Answer
        {
            get { return _Answer; }
            set { _Answer = value; }
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
        public int LawerId
        {
            get { return _LawerId; }
            set { _LawerId = value; }
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
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<Faqs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Faqs> l_Faqs = new List<Faqs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Faqs m_Faqs = new Faqs(db.ConnectionString);
                    m_Faqs.FaqId = smartReader.GetInt32("FaqId");
                    m_Faqs.Question = smartReader.GetString("Question");
                    m_Faqs.Answer = smartReader.GetString("Answer");
                    m_Faqs.LanguageId = smartReader.GetByte("LanguageId");
                    m_Faqs.FaqCode = smartReader.GetString("FaqCode");
                    m_Faqs.FaqTypeId = smartReader.GetByte("FaqTypeId");
                    m_Faqs.FaqGroupId = smartReader.GetByte("FaqGroupId");
                    m_Faqs.FieldId = smartReader.GetInt16("FieldId");
                    m_Faqs.LawerId = smartReader.GetInt32("LawerId");
                    m_Faqs.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_Faqs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Faqs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Faqs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Faqs.Add(m_Faqs);
                }
                reader.Close();
                return l_Faqs;
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
                SqlCommand cmd = new SqlCommand("Faqs_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@Question", this.Question));
                cmd.Parameters.Add(new SqlParameter("@Answer", this.Answer));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FaqCode", this.FaqCode));
                cmd.Parameters.Add(new SqlParameter("@FaqTypeId", this.FaqTypeId));
                cmd.Parameters.Add(new SqlParameter("@FaqGroupId", this.FaqGroupId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
                cmd.Parameters.Add(new SqlParameter("@LawerId", this.LawerId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@FaqId", this.FaqId)).Direction = ParameterDirection.InputOutput;
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Faqs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqId", this.FaqId));
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
        public List<Faqs> GetList()
        {
            List<Faqs> RetVal = new List<Faqs>();
            try
            {
                string sql = "SELECT * FROM V$Faqs";
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
        public static List<Faqs> Static_GetList(string ConStr)
        {
            List<Faqs> RetVal = new List<Faqs>();
            try
            {
                Faqs m_Faqs = new Faqs(ConStr);
                RetVal = m_Faqs.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Faqs> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<Faqs> GetListByFaqId(int FaqId)
        {
            List<Faqs> RetVal = new List<Faqs>();
            try
            {
                if (FaqId > 0)
                {
                    string sql = "SELECT * FROM V$Faqs WHERE (FaqId=" + FaqId.ToString() + ")";
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
        public Faqs Get(int FaqId)
        {
            Faqs RetVal = new Faqs(db.ConnectionString);
            try
            {
                List<Faqs> list = GetListByFaqId(FaqId);
                if (list.Count > 0)
                {
                    RetVal = (Faqs)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Faqs Get()
        {
            return Get(this.FaqId);
        }
        //-------------------------------------------------------------- 
        public static Faqs Static_Get(int FaqId)
        {
            return Static_Get(FaqId);
        }
        //--------------------------------------------------------------     
        public List<Faqs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string Question, byte LanguageId, string FaqCode, byte FaqTypeId, byte FaqGroupId,
                                   short FieldId, int LawerId, short DataSourceId, byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Faqs> RetVal = new List<Faqs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Faqs_GetPage");
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
                cmd.Parameters.Add(new SqlParameter("@LawerId", LawerId));
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
        public List<Faqs> Search(int ActUserId, string OrderBy, string Question, byte LanguageId, string FaqCode, byte FaqTypeId, byte FaqGroupId, short FieldId, int LawerId,
                                 short DataSourceId, byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, Question, LanguageId, FaqCode, FaqTypeId, FaqGroupId, FieldId, LawerId, DataSourceId, ReviewStatusId, CrUserId, 
                            DateFrom, DateTo, ref RowCount);
        }
    }
}