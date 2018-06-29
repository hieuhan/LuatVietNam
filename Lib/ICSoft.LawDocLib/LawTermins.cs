
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
    /// class LawTermins
    /// </summary>
    public class LawTermins
    {
        private byte _LanguageId;
        private int _LawTerminId;
        private string _TermName;
        private string _TermDesc;
        private string _TermSource;
        private byte _LawTerminGroupId;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public LawTermins()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public LawTermins(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~LawTermins()
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
        public int LawTerminId
        {
            get { return _LawTerminId; }
            set { _LawTerminId = value; }
        }
        //-----------------------------------------------------------------
        public string TermName
        {
            get { return _TermName; }
            set { _TermName = value; }
        }
        //-----------------------------------------------------------------
        public string TermDesc
        {
            get { return _TermDesc; }
            set { _TermDesc = value; }
        }
        //-----------------------------------------------------------------
        public string TermSource
        {
            get { return _TermSource; }
            set { _TermSource = value; }
        }
        //-----------------------------------------------------------------
        public byte LawTerminGroupId
        {
            get { return _LawTerminGroupId; }
            set { _LawTerminGroupId = value; }
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

        private List<LawTermins> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<LawTermins> l_LawTermins = new List<LawTermins>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    LawTermins m_LawTermins = new LawTermins(db.ConnectionString);
                    m_LawTermins.LanguageId = smartReader.GetByte("LanguageId");
                    m_LawTermins.LawTerminId = smartReader.GetInt32("LawTerminId");
                    m_LawTermins.TermName = smartReader.GetString("TermName");
                    m_LawTermins.TermDesc = smartReader.GetString("TermDesc");
                    m_LawTermins.TermSource = smartReader.GetString("TermSource");
                    m_LawTermins.LawTerminGroupId = smartReader.GetByte("LawTerminGroupId");
                    m_LawTermins.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_LawTermins.CrUserId = smartReader.GetInt32("CrUserId");
                    m_LawTermins.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_LawTermins.Add(m_LawTermins);
                }
                reader.Close();
                return l_LawTermins;
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
                SqlCommand cmd = new SqlCommand("LawTermins_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@TermName", this.TermName));
                cmd.Parameters.Add(new SqlParameter("@TermDesc", this.TermDesc));
                cmd.Parameters.Add(new SqlParameter("@TermSource", this.TermSource));
                cmd.Parameters.Add(new SqlParameter("@LawTerminGroupId", this.LawTerminGroupId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@LawTerminId", this.LawTerminId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.LawTerminId = Convert.ToInt32((cmd.Parameters["@LawTerminId"].Value == null) ? 0 : (cmd.Parameters["@LawTerminId"].Value));
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
                SqlCommand cmd = new SqlCommand("LawTermins_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LawTerminId", this.LawTerminId));
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
        public List<LawTermins> GetList()
        {
            List<LawTermins> RetVal = new List<LawTermins>();
            try
            {
                string sql = "SELECT * FROM V$LawTermins";
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
        public static List<LawTermins> Static_GetList(string ConStr)
        {
            List<LawTermins> RetVal = new List<LawTermins>();
            try
            {
                LawTermins m_LawTermins = new LawTermins(ConStr);
                RetVal = m_LawTermins.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<LawTermins> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<LawTermins> GetListByLawTerminId(int LawTerminId, byte LanguageId)
        {
            List<LawTermins> RetVal = new List<LawTermins>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                string TermName = "";
                byte ReviewStatusId = 0;
                byte LawTerminGroupId = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, LawTerminId, TermName, ReviewStatusId,LawTerminGroupId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public LawTermins Get(int LawTerminId, byte LanguageId)
        {
            LawTermins RetVal = new LawTermins(db.ConnectionString);
            try
            {
                List<LawTermins> list = GetListByLawTerminId(LawTerminId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (LawTermins)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public LawTermins Get()
        {
            return Get(this.LawTerminId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static LawTermins Static_Get(string Constr, int LawTerminId, byte LanguageId)
        {
            LawTermins m_LawTermins = new LawTermins(Constr);
            return m_LawTermins.Get(LawTerminId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static LawTermins Static_Get(int LawTerminId, byte LanguageId)
        {
            return Static_Get("", LawTerminId, LanguageId);
        }
        //--------------------------------------------------------------     
        public List<LawTermins> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, int LawTerminId, string TermName, byte ReviewStatusId, byte LawTerminGroupId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<LawTermins> RetVal = new List<LawTermins>();
            try
            {
                SqlCommand cmd = new SqlCommand("LawTermins_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@LawTerminId", LawTerminId));
                cmd.Parameters.Add(new SqlParameter("@LawTerminGroupId", LawTerminGroupId));
                cmd.Parameters.Add(new SqlParameter("@TermName", StringUtil.InjectionString(TermName)));
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
        public List<LawTermins> Search(int ActUserId, string OrderBy, byte LanguageId, int LawTerminId, string TermName, byte ReviewStatusId, byte LawTerminGroupId,string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, LawTerminId, TermName, ReviewStatusId,LawTerminGroupId, DateFrom, DateTo, ref RowCount);
        }
        public DataSet LawTermins_GetIdAndNameByJson(string Name, int RowAmount, ref string Result)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("LawTermins_GetIdAndNameByJson");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                Result = Convert.ToString(cmd.Parameters["@Result"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}