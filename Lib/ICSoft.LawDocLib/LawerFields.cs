
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
    /// class LawerFields
    /// </summary>
    public class LawerFields
    {
        private int _LawerFieldId;
        private int _LawerId;
        private short _FieldId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public LawerFields()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public LawerFields(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~LawerFields()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int LawerFieldId
        {
            get { return _LawerFieldId; }
            set { _LawerFieldId = value; }
        }
        //-----------------------------------------------------------------

        public int LawerId
        {
            get { return _LawerId; }
            set { _LawerId = value; }
        }
        public short FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }
        private List<LawerFields> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<LawerFields> l_LawerFields = new List<LawerFields>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    LawerFields m_LawerFields = new LawerFields(db.ConnectionString);
                    m_LawerFields.LawerFieldId = smartReader.GetInt32("LawerFieldId");
                    m_LawerFields.LawerId = smartReader.GetInt32("LawerId");
                    m_LawerFields.FieldId = smartReader.GetInt16("FieldId");

                    l_LawerFields.Add(m_LawerFields);
                }
                reader.Close();
                return l_LawerFields;
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
                SqlCommand cmd = new SqlCommand("LawerFields_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LawerId", this.LawerId));
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
                SqlCommand cmd = new SqlCommand("LawerFields_InsertByFieldName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LawerId", this.LawerId));
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
                SqlCommand cmd = new SqlCommand("LawerFields_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LawerId", this.LawerId));
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
        public byte DeleteByLawerId(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("LawerFields_DeleteByLawerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LawerId", this.LawerId));
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
        public List<LawerFields> GetList()
        {
            List<LawerFields> RetVal = new List<LawerFields>();
            try
            {
                string sql = "SELECT * FROM V$LawerFields";
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
        public static List<LawerFields> Static_GetList(string ConStr)
        {
            List<LawerFields> RetVal = new List<LawerFields>();
            try
            {
                LawerFields m_LawerFields = new LawerFields(ConStr);
                RetVal = m_LawerFields.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<LawerFields> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<LawerFields> GetListByLawerFieldId(int LawerFieldId)
        {
            List<LawerFields> RetVal = new List<LawerFields>();
            try
            {
                if (LawerFieldId > 0)
                {
                    string sql = "SELECT * FROM V$LawerFields WHERE (LawerFieldId=" + LawerFieldId.ToString() + ")";
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
        public LawerFields Get(int LawerFieldId)
        {
            LawerFields RetVal = new LawerFields(db.ConnectionString);
            try
            {
                List<LawerFields> list = GetListByLawerFieldId(LawerFieldId);
                if (list.Count > 0)
                {
                    RetVal = (LawerFields)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public LawerFields Get()
        {
            return Get(this.LawerFieldId);
        }
        //-------------------------------------------------------------- 
        public static LawerFields Static_Get(int LawerFieldId)
        {
            return Static_Get(LawerFieldId);
        }
        //--------------------------------------------------------------     
        public List<LawerFields> LawerFields_GetList(int ActUserId, int LawerId)
        {
            List<LawerFields> RetVal = new List<LawerFields>();
            try
            {
                SqlCommand cmd = new SqlCommand("LawerFields_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LawerId", LawerId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<LawerFields> LawerFields_GetListByListLawerId(string ListLawerId)
        {
            List<LawerFields> RetVal = new List<LawerFields>();
            try
            {
                SqlCommand cmd = new SqlCommand("LawerFields_GetListByListLawerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ListLawerId", ListLawerId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public string LawerFields_GetFieldName(byte LanguageId, int LawerId, ref string FieldName)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("LawerFields_GetFieldName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@LawerId", LawerId));
                cmd.Parameters.Add("@FieldName", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
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
        public byte InsertByListId(string listFieldId = "")
        {
            byte retVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("LawerField_InsertByListId") { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@LawerId", this.LawerId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", listFieldId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                retVal = Convert.ToByte(cmd.Parameters["@SysMessageId"].Value ?? "0");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

    }
}