
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
    /// <summary>
    /// class ArticleFields
    /// </summary>
    public class ArticleFields
    {
        private int _ArticleFieldId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _ArticleId;
        private short _FieldId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticleFields()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticleFields(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticleFields()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleFieldId
        {
            get { return _ArticleFieldId; }
            set { _ArticleFieldId = value; }
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

        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        public short FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }
        private List<ArticleFields> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleFields> l_ArticleFields = new List<ArticleFields>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleFields m_ArticleFields = new ArticleFields(db.ConnectionString);
                    m_ArticleFields.ArticleFieldId = smartReader.GetInt32("ArticleFieldId");
                    m_ArticleFields.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleFields.FieldId = smartReader.GetInt16("FieldId");
                    m_ArticleFields.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ArticleFields.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_ArticleFields.Add(m_ArticleFields);
                }
                reader.Close();
                return l_ArticleFields;
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
                SqlCommand cmd = new SqlCommand("ArticleFields_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
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
                SqlCommand cmd = new SqlCommand("ArticleFields_InsertByFieldName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
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
                SqlCommand cmd = new SqlCommand("ArticleFields_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
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
        public byte DeleteByArticleId(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleFields_DeleteByArticleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
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
        public List<ArticleFields> GetList()
        {
            List<ArticleFields> RetVal = new List<ArticleFields>();
            try
            {
                string sql = "SELECT * FROM ArticleFields";
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
        public static List<ArticleFields> Static_GetList(string ConStr)
        {
            List<ArticleFields> RetVal = new List<ArticleFields>();
            try
            {
                ArticleFields m_ArticleFields = new ArticleFields(ConStr);
                RetVal = m_ArticleFields.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ArticleFields> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<ArticleFields> GetListByArticleFieldId(int ArticleFieldId)
        {
            List<ArticleFields> RetVal = new List<ArticleFields>();
            try
            {
                if (ArticleFieldId > 0)
                {
                    string sql = "SELECT * FROM ArticleFields WHERE (ArticleFieldId=" + ArticleFieldId.ToString() + ")";
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
        public ArticleFields Get(int ArticleFieldId)
        {
            ArticleFields RetVal = new ArticleFields(db.ConnectionString);
            try
            {
                List<ArticleFields> list = GetListByArticleFieldId(ArticleFieldId);
                if (list.Count > 0)
                {
                    RetVal = (ArticleFields)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ArticleFields Get()
        {
            return Get(this.ArticleFieldId);
        }
        //-------------------------------------------------------------- 
        public static ArticleFields Static_Get(int ArticleFieldId)
        {
            return Static_Get(ArticleFieldId);
        }
        //--------------------------------------------------------------     
        public List<ArticleFields> ArticleFields_GetList(int ActUserId, int ArticleId)
        {
            List<ArticleFields> RetVal = new List<ArticleFields>();
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleFields_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<ArticleFields> ArticleFields_GetListByListArticleId(string ListArticleId)
        {
            List<ArticleFields> RetVal = new List<ArticleFields>();
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleFields_GetListByListArticleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ListArticleId", ListArticleId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public string ArticleFields_GetFieldName(byte LanguageId, int ArticleId, ref string FieldName)
        {
           string RetVal = "";
           try
            {
                SqlCommand cmd = new SqlCommand("ArticleFields_GetFieldName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
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