
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
    /// Class DocKeywords
    /// </summary>
    public class DocKeywords
    {
        private int _DocKeywordId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _DocId;
        private int _KeywordId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocKeywords()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocKeywords(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocKeywords()
        {

        }

        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocKeywordId
        {
            get { return _DocKeywordId; }
            set { _DocKeywordId = value; }
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

        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }
        public int KeywordId
        {
            get { return _KeywordId; }
            set { _KeywordId = value; }
        }

        private List<DocKeywords> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocKeywords> l_DocKeywords = new List<DocKeywords>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocKeywords m_DocKeywords = new DocKeywords(db.ConnectionString);
                    m_DocKeywords.DocKeywordId = smartReader.GetInt32("DocKeywordId");
                    m_DocKeywords.DocId = smartReader.GetInt32("DocId");
                    m_DocKeywords.KeywordId = smartReader.GetInt32("KeywordId");
                    m_DocKeywords.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocKeywords.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DocKeywords.Add(m_DocKeywords);
                }
                reader.Close();
                return l_DocKeywords;
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

        /// <summary>
        /// Inserts the specified replicated.
        /// </summary>
        /// <param name="Replicated">The replicated.</param>
        /// <param name="ActUserId">The act user identifier.</param>
        /// <param name="SysMessageId">The system message identifier.</param>
        /// <returns></returns>
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocKeywords_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@KeywordId", this.KeywordId));
                //cmd.Parameters.Add("@DocKeywordId", SqlDbType.Int).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //this.DocKeywordId = Convert.ToInt32((cmd.Parameters["@DocKeywordId"].Value == null) ? 0 : (cmd.Parameters["@DocKeywordId"].Value));
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = 1;// Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertByString(byte Replicated, int ActUserId, string KeywordsString)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocKeywords_InsertByString");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@KeywordsString", KeywordsString));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocKeywords_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@KeywordId", this.KeywordId));
                cmd.Parameters.Add(new SqlParameter("@DocKeywordId", this.DocKeywordId));
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
                SqlCommand cmd = new SqlCommand("DocKeywords_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@KeywordId", this.KeywordId));
                //cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = 1;// Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte DeleteByDocId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocKeywords_DeleteByDocId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                //cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = 1;// Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<DocKeywords> GetList()
        {
            List<DocKeywords> RetVal = new List<DocKeywords>();
            try
            {
                string sql = "SELECT * FROM V$DocKeywords";
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
        public static List<DocKeywords> Static_GetList(string ConStr)
        {
            List<DocKeywords> RetVal = new List<DocKeywords>();
            try
            {
                DocKeywords m_DocKeywords = new DocKeywords(ConStr);
                RetVal = m_DocKeywords.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocKeywords> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<DocKeywords> GetListByDocId(int DocId)
        {
            List<DocKeywords> RetVal = new List<DocKeywords>();
            try
            {
                int ActUserId = 0;
                RetVal = DocKeywords_GetList(ActUserId, DocId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<DocKeywords> GetListByDocKeywordId(int DocKeywordId)
        {
            List<DocKeywords> RetVal = new List<DocKeywords>();
            try
            {
                if (DocKeywordId > 0)
                {
                    string sql = "SELECT * FROM V$DocKeywords WHERE (DocKeywordId=" + DocKeywordId.ToString() + ")";
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
        public DocKeywords Get(int DocKeywordId)
        {
            DocKeywords RetVal = new DocKeywords(db.ConnectionString);
            try
            {
                List<DocKeywords> list = GetListByDocKeywordId(DocKeywordId);
                if (list.Count > 0)
                {
                    RetVal = (DocKeywords)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocKeywords Get()
        {
            return Get(this.DocKeywordId);
        }
        //-------------------------------------------------------------- 
        public static DocKeywords Static_Get(int DocKeywordId)
        {
            return Static_Get(DocKeywordId);
        }
        //--------------------------------------------------------------     
        public List<DocKeywords> DocKeywords_GetList(int ActUserId, int DocId)
        {
            List<DocKeywords> RetVal = new List<DocKeywords>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocKeywords_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                RetVal = Init(cmd);
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