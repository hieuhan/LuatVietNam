
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
    /// class RelateTypeWithDefaultLanguage
    /// </summary>
    public class RelateTypeWithDefaultLanguage
    {
        private byte _LanguageId;
        private byte _RelateTypeId;
        private string _RelateTypeName;
        private string _RelateTypeDesc;
        private string _RevertRelateTypeName;
        private string _RevertRelateTypeDesc;
        private byte _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private byte _RelateTypeGroupId;
        private byte _DocGroupId;
        private string _RelateTypeNameDefault;
        private string _RelateTypeDescDefault;
        private DBAccess db;
        //-----------------------------------------------------------------
        public RelateTypeWithDefaultLanguage()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public RelateTypeWithDefaultLanguage(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~RelateTypeWithDefaultLanguage()
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
        public byte RelateTypeId
        {
            get { return _RelateTypeId; }
            set { _RelateTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string RelateTypeName
        {
            get { return _RelateTypeName; }
            set { _RelateTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string RelateTypeDesc
        {
            get { return _RelateTypeDesc; }
            set { _RelateTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public string RevertRelateTypeName
        {
            get { return _RevertRelateTypeName; }
            set { _RevertRelateTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string RevertRelateTypeDesc
        {
            get { return _RevertRelateTypeDesc; }
            set { _RevertRelateTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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
        public string RelateTypeNameDefault
        {
            get { return _RelateTypeNameDefault; }
            set { _RelateTypeNameDefault = value; }
        }
        //-----------------------------------------------------------------
        public string RelateTypeDescDefault
        {
            get { return _RelateTypeDescDefault; }
            set { _RelateTypeDescDefault = value; }
        }
        //-----------------------------------------------------------------

        public byte RelateTypeGroupId
        {
            get { return _RelateTypeGroupId; }
            set { _RelateTypeGroupId = value; }
        }
        //-----------------------------------------------------------------

        public byte DocGroupId
        {
            get { return _DocGroupId; }
            set { _DocGroupId = value; }
        }
        private List<RelateTypeWithDefaultLanguage> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<RelateTypeWithDefaultLanguage> l_RelateTypeWithDefaultLanguage = new List<RelateTypeWithDefaultLanguage>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RelateTypeWithDefaultLanguage m_RelateTypeWithDefaultLanguage = new RelateTypeWithDefaultLanguage(db.ConnectionString);
                    m_RelateTypeWithDefaultLanguage.LanguageId = smartReader.GetByte("LanguageId");
                    m_RelateTypeWithDefaultLanguage.RelateTypeId = smartReader.GetByte("RelateTypeId");
                    m_RelateTypeWithDefaultLanguage.RelateTypeName = smartReader.GetString("RelateTypeName");
                    m_RelateTypeWithDefaultLanguage.RelateTypeDesc = smartReader.GetString("RelateTypeDesc");
                    m_RelateTypeWithDefaultLanguage.RevertRelateTypeName = smartReader.GetString("RevertRelateTypeName");
                    m_RelateTypeWithDefaultLanguage.RevertRelateTypeDesc = smartReader.GetString("RevertRelateTypeDesc");
                    m_RelateTypeWithDefaultLanguage.RelateTypeGroupId = smartReader.GetByte("RelateTypeGroupId");
                    m_RelateTypeWithDefaultLanguage.DocGroupId = smartReader.GetByte("DocGroupId");
                    m_RelateTypeWithDefaultLanguage.DisplayOrder = smartReader.GetByte("DisplayOrder");
                    m_RelateTypeWithDefaultLanguage.CrUserId = smartReader.GetInt32("CrUserId");
                    m_RelateTypeWithDefaultLanguage.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_RelateTypeWithDefaultLanguage.RelateTypeNameDefault = smartReader.GetString("RelateTypeNameDefault");
                    m_RelateTypeWithDefaultLanguage.RelateTypeDescDefault = smartReader.GetString("RelateTypeDescDefault");

                    l_RelateTypeWithDefaultLanguage.Add(m_RelateTypeWithDefaultLanguage);
                }
                reader.Close();
                return l_RelateTypeWithDefaultLanguage;
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
        public List<RelateTypeWithDefaultLanguage> RelateTypes_GetList(int ActUserId, string OrderBy, byte LanguageId, byte RelateTypeId, byte ReferToDefaultLanguage)
        {
            List<RelateTypeWithDefaultLanguage> RetVal = new List<RelateTypeWithDefaultLanguage>();
            try
            {
                SqlCommand cmd = new SqlCommand("RelateTypes_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
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