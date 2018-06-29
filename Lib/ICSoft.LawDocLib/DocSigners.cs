
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
    /// class DocSigners
    /// </summary>
    public class DocSigners
    {
        private int _DocSignerId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _DocId;
        private short _SignerId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocSigners()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocSigners(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocSigners()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocSignerId
        {
            get { return _DocSignerId; }
            set { _DocSignerId = value; }
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
        public short SignerId
        {
            get { return _SignerId; }
            set { _SignerId = value; }
        }
        private List<DocSigners> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocSigners> l_DocSigners = new List<DocSigners>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocSigners m_DocSigners = new DocSigners(db.ConnectionString);
                    m_DocSigners.DocSignerId = smartReader.GetInt32("DocSignerId");
                    m_DocSigners.DocId = smartReader.GetInt32("DocId");
                    m_DocSigners.SignerId = smartReader.GetInt16("SignerId");
                    m_DocSigners.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocSigners.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DocSigners.Add(m_DocSigners);
                }
                reader.Close();
                return l_DocSigners;
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
        //--------------------------------------------------------------
        public byte Insert(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocSigners_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", this.SignerId));
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
        public byte InsertBySignerName(byte Replicated, int ActUserId, string SignerName)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocSigners_InsertBySignerName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@SignerName", SignerName));
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
        public byte Delete(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocSigners_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", this.SignerId));
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
        public byte DeleteByDocId(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocSigners_DeleteByDocId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
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
        public List<DocSigners> GetList()
        {
            List<DocSigners> RetVal = new List<DocSigners>();
            try
            {
                string sql = "SELECT * FROM V$DocSigners";
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
        public static List<DocSigners> Static_GetList(string ConStr)
        {
            List<DocSigners> RetVal = new List<DocSigners>();
            try
            {
                DocSigners m_DocSigners = new DocSigners(ConStr);
                RetVal = m_DocSigners.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocSigners> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<DocSigners> GetListByDocSignerId(int DocSignerId)
        {
            List<DocSigners> RetVal = new List<DocSigners>();
            try
            {
                if (DocSignerId > 0)
                {
                    string sql = "SELECT * FROM V$DocSigners WHERE (DocSignerId=" + DocSignerId.ToString() + ")";
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
        public DocSigners Get(int DocSignerId)
        {
            DocSigners RetVal = new DocSigners(db.ConnectionString);
            try
            {
                List<DocSigners> list = GetListByDocSignerId(DocSignerId);
                if (list.Count > 0)
                {
                    RetVal = (DocSigners)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocSigners Get()
        {
            return Get(this.DocSignerId);
        }
        //-------------------------------------------------------------- 
        public static DocSigners Static_Get(int DocSignerId)
        {
            return Static_Get(DocSignerId);
        }
        //--------------------------------------------------------------     
        public List<DocSigners> DocSigners_GetList(int ActUserId, int DocId)
        {
            List<DocSigners> RetVal = new List<DocSigners>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocSigners_GetList");
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
        public string DocSigners_GetSignerName(byte LanguageId, int DocId, ref string SignerName)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("DocSigners_GetSignerName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add("@SignerName", SqlDbType.NVarChar,4000).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@SignerName"].Value);
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