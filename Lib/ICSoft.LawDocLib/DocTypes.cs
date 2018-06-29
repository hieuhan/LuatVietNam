
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
    /// class DocTypes
    /// </summary>
    public class DocTypes
    {
        private byte _LanguageId;
        private byte _DocTypeId;
        private string _DocTypeName;
        private string _DocTypeDesc;
        private byte _DisplayOrder;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private int _SoLuong;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocTypes()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocTypes(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocTypes()
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
        public byte DocTypeId
        {
            get { return _DocTypeId; }
            set { _DocTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string DocTypeName
        {
            get { return _DocTypeName; }
            set { _DocTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string DocTypeDesc
        {
            get { return _DocTypeDesc; }
            set { _DocTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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
        public int SoLuong
        {
            get { return _SoLuong; }
            set { _SoLuong = value; }
        }
        //-----------------------------------------------------------------

        private List<DocTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocTypes> l_DocTypes = new List<DocTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocTypes m_DocTypes = new DocTypes(db.ConnectionString);
                    m_DocTypes.LanguageId = smartReader.GetByte("LanguageId");
                    m_DocTypes.DocTypeId = smartReader.GetByte("DocTypeId");
                    m_DocTypes.DocTypeName = smartReader.GetString("DocTypeName");
                    m_DocTypes.DocTypeDesc = smartReader.GetString("DocTypeDesc");
                    m_DocTypes.DisplayOrder = smartReader.GetByte("DisplayOrder");
                    m_DocTypes.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocTypes.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocTypes.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DocTypes.Add(m_DocTypes);
                }
                reader.Close();
                return l_DocTypes;
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
                SqlCommand cmd = new SqlCommand("DocTypes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeName", this.DocTypeName));
                cmd.Parameters.Add(new SqlParameter("@DocTypeDesc", this.DocTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", this.DocTypeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocTypeId = Convert.ToByte((cmd.Parameters["@DocTypeId"].Value == null) ? 0 : (cmd.Parameters["@DocTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("DocTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", this.DocTypeId));
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
        public List<DocTypes> GetList()
        {
            List<DocTypes> RetVal = new List<DocTypes>();
            try
            {
                string sql = "SELECT * FROM V$DocTypes ORDER BY DocTypeName";
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
        public static List<DocTypes> Static_GetList(string ConStr)
        {
            List<DocTypes> RetVal = new List<DocTypes>();
            try
            {
                DocTypes m_DocTypes = new DocTypes(ConStr);
                RetVal = m_DocTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //-----------------------------------------------------------------------------
        public static DocTypes Static_Get(byte DocTypeId, List<DocTypes> lList)
        {
            DocTypes RetVal = new DocTypes();
            if (DocTypeId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.DocTypeId == DocTypeId);
                if (RetVal == null) RetVal = new DocTypes();
            }
            return RetVal;
        }
        //--------------------------------------------------------------   
        public List<DocTypes> GetListByDocTypeId(byte DocTypeId, byte LanguageId)
        {
            List<DocTypes> RetVal = new List<DocTypes>();
            try
            {
                int ActUserId = 0;
                string OrderBy = "";
                short DisplayTypeId = 0;
                byte ReferToDefaultLanguage = 0;
                RetVal = DocTypes_GetList(ActUserId, OrderBy, LanguageId, DocTypeId,this.ReviewStatusId, DisplayTypeId, ReferToDefaultLanguage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public DocTypes Get(byte DocTypeId, byte LanguageId)
        {
            DocTypes RetVal = new DocTypes(db.ConnectionString);
            try
            {
                List<DocTypes> list = GetListByDocTypeId(DocTypeId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (DocTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocTypes Get()
        {
            return Get(this.DocTypeId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static DocTypes Static_Get(string Constr, byte DocTypeId, byte LanguageId)
        {
            DocTypes m_DocTypes = new DocTypes(Constr);
            return m_DocTypes.Get(DocTypeId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static DocTypes Static_Get(byte DocTypeId, byte LanguageId)
        {
            return Static_Get("", DocTypeId, LanguageId);
        }
        //--------------------------------------------------------------     
        public List<DocTypes> DocTypes_GetList(int ActUserId, string OrderBy, byte LanguageId, byte DocTypeId, byte ReviewStatusId, short DisplayTypeId, byte ReferToDefaultLanguage)
        {
            List<DocTypes> RetVal = new List<DocTypes>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocTypes_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReferToDefaultLanguage", ReferToDefaultLanguage));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public List<DocTypes> GetListbyDisplayTypeId(short DisplayTypeId = 0)
        {
            List<DocTypes> RetVal = new List<DocTypes>();
            try
            {
                string sql = "SELECT * FROM DocTypes where DocTypeId IN (select DocTypeId from DocTypeDisplays where DisplayTypeId=" + DisplayTypeId + ") Order by DocTypeName";
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
        public static List<DocTypes> Static_GetListbyDisplayTypeId(short DisplayTypeId = 0)
        {
            List<DocTypes> RetVal = new List<DocTypes>();
            try
            {
                DocTypes m_DocTypes = new DocTypes();
                RetVal = m_DocTypes.GetListbyDisplayTypeId(DisplayTypeId);
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
