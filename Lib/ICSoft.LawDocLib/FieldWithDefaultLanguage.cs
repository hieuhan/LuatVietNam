
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
    /// class Fields
    /// </summary>
    public class FieldWithDefaultLanguage
    {   
        private byte _LanguageId;
	    private short _FieldId;
	    private string _FieldName;
	    private string _FieldDesc;
	    private short _ParentFieldId;
	    private byte _FieldLevel;
	    private short _DisplayOrder;
	    private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private string _FieldNameDefault;
        private string _FieldDescDefault;
        private DBAccess db;
        //-----------------------------------------------------------------
		public FieldWithDefaultLanguage()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public FieldWithDefaultLanguage(string constr)
        {
            db = new DBAccess ((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FieldWithDefaultLanguage()
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
	    public short FieldId
        {
		    get { return _FieldId; }
		    set { _FieldId = value; }
	    }
        //-----------------------------------------------------------------
        public string FieldName
		{
            get { return _FieldName; }
		    set { _FieldName = value; }
		}    
        //-----------------------------------------------------------------
        public string FieldDesc
		{
            get { return _FieldDesc; }
		    set { _FieldDesc = value; }
		}    
        //-----------------------------------------------------------------
        public short ParentFieldId
		{
            get { return _ParentFieldId; }
		    set { _ParentFieldId = value; }
		}    
        //-----------------------------------------------------------------
        public byte FieldLevel
		{
            get { return _FieldLevel; }
		    set { _FieldLevel = value; }
		}    
        //-----------------------------------------------------------------
        public short DisplayOrder
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
        public string FieldNameDefault
        {
            get { return _FieldNameDefault; }
            set { _FieldNameDefault = value; }
        }
        //-----------------------------------------------------------------
        public string FieldDescDefault
        {
            get { return _FieldDescDefault; }
            set { _FieldDescDefault = value; }
        }    
        //-----------------------------------------------------------------

        private List<FieldWithDefaultLanguage> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FieldWithDefaultLanguage> l_Fields = new List<FieldWithDefaultLanguage>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FieldWithDefaultLanguage m_Fields = new FieldWithDefaultLanguage(db.ConnectionString);
                    m_Fields.LanguageId = smartReader.GetByte("LanguageId");
                    m_Fields.FieldId = smartReader.GetInt16("FieldId");
                    m_Fields.FieldName = smartReader.GetString("FieldName");
                    m_Fields.FieldDesc = smartReader.GetString("FieldDesc");
                    m_Fields.ParentFieldId = smartReader.GetInt16("ParentFieldId");
                    m_Fields.FieldLevel = smartReader.GetByte("FieldLevel");
                    m_Fields.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_Fields.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Fields.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Fields.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Fields.FieldNameDefault = smartReader.GetString("FieldNameDefault");
                    m_Fields.FieldDescDefault = smartReader.GetString("FieldDescDefault");
         
                    l_Fields.Add(m_Fields);
                }
                reader.Close();
                return l_Fields;
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
        public List<FieldWithDefaultLanguage> Fields_GetList(int ActUserId, string OrderBy, byte LanguageId, short FieldId, string FieldName, short ParentFieldId, short DisplayTypeId, byte ReviewStatusId, byte ReferToDefaultLanguage)
        {
            List<FieldWithDefaultLanguage> RetVal = new List<FieldWithDefaultLanguage>();
            try
            {
                SqlCommand cmd = new SqlCommand("Fields_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@FieldName", StringUtil.InjectionString(FieldName)));
                cmd.Parameters.Add(new SqlParameter("@ParentFieldId", ParentFieldId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
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