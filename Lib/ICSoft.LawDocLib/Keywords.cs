
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
    /// class Keywords
    /// </summary>
    public class Keywords
    {
        private byte _LanguageId;
        private int _KeywordId;
        private string _KeywordName;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Keywords()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Keywords(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Keywords()
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
        public int KeywordId
        {
            get { return _KeywordId; }
            set { _KeywordId = value; }
        }
        //-----------------------------------------------------------------
        public string KeywordName
        {
            get { return _KeywordName; }
            set { _KeywordName = value; }
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

        private List<Keywords> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Keywords> l_Keywords = new List<Keywords>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Keywords m_Keywords = new Keywords(db.ConnectionString);
                    m_Keywords.LanguageId = smartReader.GetByte("LanguageId");
                    m_Keywords.KeywordId = smartReader.GetInt32("KeywordId");
                    m_Keywords.KeywordName = smartReader.GetString("KeywordName");
                    m_Keywords.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Keywords.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Keywords.Add(m_Keywords);
                }
                reader.Close();
                return l_Keywords;
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
                SqlCommand cmd = new SqlCommand("Keywords_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@KeywordName", this.KeywordName));
                cmd.Parameters.Add(new SqlParameter("@KeywordId", this.KeywordId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.KeywordId = Convert.ToInt32((cmd.Parameters["@KeywordId"].Value == null) ? 0 : (cmd.Parameters["@KeywordId"].Value));
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
                SqlCommand cmd = new SqlCommand("Keywords_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@KeywordId", this.KeywordId));
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
        public List<Keywords> GetList()
        {
            List<Keywords> RetVal = new List<Keywords>();
            try
            {
                string sql = "SELECT * FROM V$Keywords";
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
        public static List<Keywords> Static_GetList(string ConStr)
        {
            List<Keywords> RetVal = new List<Keywords>();
            try
            {
                Keywords m_Keywords = new Keywords(ConStr);
                RetVal = m_Keywords.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Keywords> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<Keywords> GetListByKeywordId(int KeywordId, byte LanguageId)
        {
            List<Keywords> RetVal = new List<Keywords>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string KeywordName = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, KeywordId, KeywordName, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public Keywords Get(int KeywordId, byte LanguageId)
        {
            Keywords RetVal = new Keywords(db.ConnectionString);
            try
            {
                List<Keywords> list = GetListByKeywordId(KeywordId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (Keywords)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Keywords Get()
        {
            return Get(this.KeywordId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Keywords Static_Get(string Constr, int KeywordId, byte LanguageId)
        {
            Keywords m_Keywords = new Keywords(Constr);
            return m_Keywords.Get(KeywordId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Keywords Static_Get(int KeywordId, byte LanguageId)
        {
            return Static_Get("", KeywordId, LanguageId);
        }
        //-----------------------------------------------------------------------------
        public static Keywords Static_Get(int KeywordId, List<Keywords> lList)
        {
            Keywords RetVal = new Keywords();
            if (KeywordId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.KeywordId == KeywordId);
                if (RetVal == null) RetVal = new Keywords();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Keywords> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, int KeywordId, string KeywordName, ref int RowCount)
        {
            List<Keywords> RetVal = new List<Keywords>();
            try
            {
                SqlCommand cmd = new SqlCommand("Keywords_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@KeywordId", KeywordId));
                cmd.Parameters.Add(new SqlParameter("@KeywordName", StringUtil.InjectionString(KeywordName)));
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
        public List<Keywords> Search(int ActUserId, string OrderBy, byte LanguageId, int KeywordId, string KeywordName, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, KeywordId, KeywordName, ref RowCount);
        }
        //-------------------------------------------------------------- 
        public DataSet Keywords_GetNameByJson(string Name, int RowAmount, ref string Result)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("KeyWords_GetNameByJson");
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
        //--------------------------------------------------------------  
    }
}