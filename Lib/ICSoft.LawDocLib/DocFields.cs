
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
    /// class DocFields
    /// </summary>
    public class DocFields
    {
        private int _DocFieldId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _DocId;
        private short _FieldId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocFields()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocFields(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocFields()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocFieldId
        {
            get { return _DocFieldId; }
            set { _DocFieldId = value; }
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
        public short FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }
        private List<DocFields> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocFields> l_DocFields = new List<DocFields>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocFields m_DocFields = new DocFields(db.ConnectionString);
                    m_DocFields.DocFieldId = smartReader.GetInt32("DocFieldId");
                    m_DocFields.DocId = smartReader.GetInt32("DocId");
                    m_DocFields.FieldId = smartReader.GetInt16("FieldId");
                    m_DocFields.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocFields.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DocFields.Add(m_DocFields);
                }
                reader.Close();
                return l_DocFields;
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
                SqlCommand cmd = new SqlCommand("DocFields_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
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
        public byte InsertByFieldName(byte Replicated, int ActUserId, string FieldName)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocFields_InsertByFieldName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@FieldName", FieldName)); // Lĩnh vực cách nhau dấu ;
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocFields_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
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
        public byte DeleteByDocId(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocFields_DeleteByDocId");
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
        public List<DocFields> GetList()
        {
            List<DocFields> RetVal = new List<DocFields>();
            try
            {
                string sql = "SELECT * FROM V$DocFields";
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
        public static List<DocFields> Static_GetList(string ConStr)
        {
            List<DocFields> RetVal = new List<DocFields>();
            try
            {
                DocFields m_DocFields = new DocFields(ConStr);
                RetVal = m_DocFields.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocFields> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<DocFields> GetListByDocFieldId(int DocFieldId)
        {
            List<DocFields> RetVal = new List<DocFields>();
            try
            {
                if (DocFieldId > 0)
                {
                    string sql = "SELECT * FROM V$DocFields WHERE (DocFieldId=" + DocFieldId.ToString() + ")";
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
        public DocFields Get(int DocFieldId)
        {
            DocFields RetVal = new DocFields(db.ConnectionString);
            try
            {
                List<DocFields> list = GetListByDocFieldId(DocFieldId);
                if (list.Count > 0)
                {
                    RetVal = (DocFields)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocFields Get()
        {
            return Get(this.DocFieldId);
        }
        //-------------------------------------------------------------- 
        public static DocFields Static_Get(int DocFieldId)
        {
            return Static_Get(DocFieldId);
        }
        //--------------------------------------------------------------     
        public List<DocFields> DocFields_GetList(int ActUserId, int DocId)
        {
            List<DocFields> RetVal = new List<DocFields>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocFields_GetList");
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
        public List<DocFields> DocFields_GetListByListDocId(string ListDocId)
        {
            List<DocFields> RetVal = new List<DocFields>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocFields_GetListByListDocId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ListDocId", ListDocId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public string DocFields_GetFieldName(byte LanguageId, int DocId, ref string FieldName)
        {
           string RetVal = "";
           try
            {
                SqlCommand cmd = new SqlCommand("DocFields_GetFieldName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add("@FieldName", SqlDbType.NVarChar,4000).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@FieldName"].Value);
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