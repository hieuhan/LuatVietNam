
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
    /// class DocTypeWithDefaultLanguage
    /// </summary>
    public class DocTypeWithDefaultLanguage
    {
        private byte _LanguageId;
        private byte _DocTypeId;
        private string _DocTypeName;
        private string _DocTypeDesc;
        private byte _DisplayOrder;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private string _DocTypeNameDefault;
        private string _DocTypeDescDefault;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocTypeWithDefaultLanguage()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocTypeWithDefaultLanguage(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocTypeWithDefaultLanguage()
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
        public string DocTypeNameDefault
        {
            get { return _DocTypeNameDefault; }
            set { _DocTypeNameDefault = value; }
        }
        //-----------------------------------------------------------------
        public string DocTypeDescDefault
        {
            get { return _DocTypeDescDefault; }
            set { _DocTypeDescDefault = value; }
        }
        //-----------------------------------------------------------------

        private List<DocTypeWithDefaultLanguage> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocTypeWithDefaultLanguage> l_DocTypes = new List<DocTypeWithDefaultLanguage>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocTypeWithDefaultLanguage m_DocTypeWithDefaultLanguage = new DocTypeWithDefaultLanguage(db.ConnectionString);
                    m_DocTypeWithDefaultLanguage.LanguageId = smartReader.GetByte("LanguageId");
                    m_DocTypeWithDefaultLanguage.DocTypeId = smartReader.GetByte("DocTypeId");
                    m_DocTypeWithDefaultLanguage.DocTypeName = smartReader.GetString("DocTypeName");
                    m_DocTypeWithDefaultLanguage.DocTypeDesc = smartReader.GetString("DocTypeDesc");
                    m_DocTypeWithDefaultLanguage.DisplayOrder = smartReader.GetByte("DisplayOrder");
                    m_DocTypeWithDefaultLanguage.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocTypeWithDefaultLanguage.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocTypeWithDefaultLanguage.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_DocTypeWithDefaultLanguage.DocTypeNameDefault = smartReader.GetString("DocTypeNameDefault");
                    m_DocTypeWithDefaultLanguage.DocTypeDescDefault = smartReader.GetString("DocTypeDescDefault");

                    l_DocTypes.Add(m_DocTypeWithDefaultLanguage);
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
        //--------------------------------------------------------------     
        public List<DocTypeWithDefaultLanguage> DocTypes_GetList(int ActUserId, string OrderBy, byte LanguageId, byte DocTypeId, byte ReviewStatusId, short DisplayTypeId, byte ReferToDefaultLanguage)
        {
            List<DocTypeWithDefaultLanguage> RetVal = new List<DocTypeWithDefaultLanguage>();
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
    }
}
