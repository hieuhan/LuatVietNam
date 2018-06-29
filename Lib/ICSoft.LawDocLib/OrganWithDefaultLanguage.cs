
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;

namespace ICSoft.LawDocsLib
{
    /// <summary>
    /// class OrganWithDefaultLanguage
    /// </summary>
    public class OrganWithDefaultLanguage
    {   
        private byte _LanguageId;
	    private short _OrganId;
	    private string _OrganName;
	    private string _OrganDesc;
	    private short _ParentOrganId;
	    private short _DisplayOrder;
	    private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _OrganTypeId;
        private string _OrganNameDefault;
        private string _OrganDescDefault;
        private DBAccess db;
        //-----------------------------------------------------------------
        public OrganWithDefaultLanguage()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public OrganWithDefaultLanguage(string constr)
        {
            db = new DBAccess ((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~OrganWithDefaultLanguage()
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
	    public short OrganId
        {
		    get { return _OrganId; }
		    set { _OrganId = value; }
	    }
        //-----------------------------------------------------------------
        public string OrganName
		{
            get { return _OrganName; }
		    set { _OrganName = value; }
		}    
        //-----------------------------------------------------------------
        public string OrganDesc
		{
            get { return _OrganDesc; }
		    set { _OrganDesc = value; }
		}    
        //-----------------------------------------------------------------
        public short ParentOrganId
		{
            get { return _ParentOrganId; }
		    set { _ParentOrganId = value; }
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
	    public byte OrganTypeId
        {
		    get { return _OrganTypeId; }
		    set { _OrganTypeId = value; }
	    }
        //-----------------------------------------------------------------
        public string OrganNameDefault
        {
            get { return _OrganNameDefault; }
            set { _OrganNameDefault = value; }
        }
        //-----------------------------------------------------------------
        public string OrganDescDefault
        {
            get { return _OrganDescDefault; }
            set { _OrganDescDefault = value; }
        }
        //-----------------------------------------------------------------
        private List<OrganWithDefaultLanguage> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<OrganWithDefaultLanguage> l_OrgansWithDefaultLanguage = new List<OrganWithDefaultLanguage>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    OrganWithDefaultLanguage m_OrgansWithDefaultLanguage = new OrganWithDefaultLanguage(db.ConnectionString);
                    m_OrgansWithDefaultLanguage.LanguageId = smartReader.GetByte("LanguageId");
                    m_OrgansWithDefaultLanguage.OrganId = smartReader.GetInt16("OrganId");
                    m_OrgansWithDefaultLanguage.OrganName = smartReader.GetString("OrganName");
                    m_OrgansWithDefaultLanguage.OrganDesc = smartReader.GetString("OrganDesc");
                    m_OrgansWithDefaultLanguage.OrganTypeId = smartReader.GetByte("OrganTypeId");
                    m_OrgansWithDefaultLanguage.ParentOrganId = smartReader.GetInt16("ParentOrganId");
                    m_OrgansWithDefaultLanguage.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_OrgansWithDefaultLanguage.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_OrgansWithDefaultLanguage.CrUserId = smartReader.GetInt32("CrUserId");
                    m_OrgansWithDefaultLanguage.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_OrgansWithDefaultLanguage.OrganNameDefault = smartReader.GetString("OrganNameDefault");
                    m_OrgansWithDefaultLanguage.OrganDescDefault = smartReader.GetString("OrganDescDefault");

                    l_OrgansWithDefaultLanguage.Add(m_OrgansWithDefaultLanguage);
                }
                reader.Close();
                return l_OrgansWithDefaultLanguage;
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
        public List<OrganWithDefaultLanguage> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, short OrganId, string OrganName, byte OrganTypeId, 
                                     short DisplayTypeId, byte ReviewStatusId, byte ReferToDefaultLanguage, ref int RowCount)
        {
            List<OrganWithDefaultLanguage> RetVal = new List<OrganWithDefaultLanguage>();
            try
            {
                SqlCommand cmd = new SqlCommand("Organs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganName", StringUtil.InjectionString(OrganName)));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReferToDefaultLanguage", ReferToDefaultLanguage));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);                
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