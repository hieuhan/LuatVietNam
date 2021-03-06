
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
    /// class DocOrgans
    /// </summary>
    public class DocOrgans
    {
        private int _DocOrganId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _DocId;
        private short _OrganId;
        private byte _OrganTypeId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocOrgans()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocOrgans(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocOrgans()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocOrganId
        {
            get { return _DocOrganId; }
            set { _DocOrganId = value; }
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
        public short OrganId
        {
            get { return _OrganId; }
            set { _OrganId = value; }
        }
        public byte OrganTypeId
        {
            get { return _OrganTypeId; }
            set { _OrganTypeId = value; }
        }
        private List<DocOrgans> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocOrgans> l_DocOrgans = new List<DocOrgans>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocOrgans m_DocOrgans = new DocOrgans(db.ConnectionString);
                    m_DocOrgans.DocOrganId = smartReader.GetInt32("DocOrganId");
                    m_DocOrgans.DocId = smartReader.GetInt32("DocId");
                    m_DocOrgans.OrganId = smartReader.GetInt16("OrganId");
                    m_DocOrgans.OrganTypeId = smartReader.GetByte("OrganTypeId");
                    m_DocOrgans.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocOrgans.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DocOrgans.Add(m_DocOrgans);
                }
                reader.Close();
                return l_DocOrgans;
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
        public byte Insert(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocOrgans_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                db.ExecuteSQL(cmd);
                RetVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertByOrganName(byte Replicated, int ActUserId, string OrganName)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocOrgans_InsertByOrganName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrganName", OrganName));
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
                SqlCommand cmd = new SqlCommand("DocOrgans_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId));

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
                SqlCommand cmd = new SqlCommand("DocOrgans_DeleteByDocId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
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
        public List<DocOrgans> GetList()
        {
            List<DocOrgans> RetVal = new List<DocOrgans>();
            try
            {
                string sql = "SELECT * FROM DocOrgans";
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
        public static List<DocOrgans> Static_GetList(string ConStr)
        {
            List<DocOrgans> RetVal = new List<DocOrgans>();
            try
            {
                DocOrgans m_DocOrgans = new DocOrgans(ConStr);
                RetVal = m_DocOrgans.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocOrgans> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<DocOrgans> GetListByDocId(int DocId)
        {
            List<DocOrgans> RetVal = new List<DocOrgans>();
            try
            {
                int ActUserId = 0;
                RetVal = DocOrgans_GetList(ActUserId, DocId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<DocOrgans> GetListByDocOrganId(int DocOrganId)
        {
            List<DocOrgans> RetVal = new List<DocOrgans>();
            try
            {
                if (DocOrganId > 0)
                {
                    string sql = "SELECT * FROM DocOrgans WHERE (DocOrganId=" + DocOrganId.ToString() + ")";
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
        public DocOrgans Get(int DocOrganId)
        {
            DocOrgans RetVal = new DocOrgans(db.ConnectionString);
            try
            {
                List<DocOrgans> list = GetListByDocOrganId(DocOrganId);
                if (list.Count > 0)
                {
                    RetVal = (DocOrgans)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocOrgans Get()
        {
            return Get(this.DocOrganId);
        }
        //-------------------------------------------------------------- 
        public static DocOrgans Static_Get(int DocOrganId)
        {
            return Static_Get(DocOrganId);
        }
        //--------------------------------------------------------------     
        public List<DocOrgans> DocOrgans_GetList(int ActUserId, int DocId)
        {
            List<DocOrgans> RetVal = new List<DocOrgans>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocOrgans_GetList");
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
        public string DocOrgans_GetOrganName(byte LanguageId, int DocId, ref string OrganName)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("DocOrgans_GetOrganName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add("@OrganName", SqlDbType.NVarChar,4000).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@OrganName"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string DocOrgans_GetOrganName(byte LanguageId, int DocId, byte OrganTypeId, ref string OrganName)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("DocOrgans_GetOrganName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add("@OrganName", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@OrganName"].Value);
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