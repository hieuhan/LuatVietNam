
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
    /// class DocTypeDisplays
    /// </summary>
    public class DocTypeDisplays
    {   
	    private short _DocTypeDisplayId;
	    private short _DisplayTypeId;
        private byte _LanguageId;
	    private byte _DocTypeId;
	    private short _DisplayOrder;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private string _DocTypeName;
        private string _DocTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
		public DocTypeDisplays()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public DocTypeDisplays(string constr)
        {
            db = new DBAccess ((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocTypeDisplays()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short DocTypeDisplayId
        {
		    get { return _DocTypeDisplayId; }
		    set { _DocTypeDisplayId = value; }
	    }
        //-----------------------------------------------------------------
        public short DisplayTypeId
		{
            get { return _DisplayTypeId; }
		    set { _DisplayTypeId = value; }
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
        public short DisplayOrder
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
    
        private List<DocTypeDisplays> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocTypeDisplays> l_DocTypeDisplays = new List<DocTypeDisplays>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocTypeDisplays m_DocTypeDisplays = new DocTypeDisplays(db.ConnectionString);
                    m_DocTypeDisplays.DocTypeDisplayId = smartReader.GetInt16("DocTypeDisplayId");
                    m_DocTypeDisplays.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_DocTypeDisplays.LanguageId = smartReader.GetByte("LanguageId");
                    m_DocTypeDisplays.DocTypeId = smartReader.GetByte("DocTypeId");
                    m_DocTypeDisplays.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_DocTypeDisplays.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocTypeDisplays.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_DocTypeDisplays.DocTypeName = smartReader.GetString("DocTypeName");
                    m_DocTypeDisplays.DocTypeDesc = smartReader.GetString("DocTypeDesc");
                    l_DocTypeDisplays.Add(m_DocTypeDisplays);
                }
                reader.Close();
                return l_DocTypeDisplays;
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
                SqlCommand cmd = new SqlCommand("DocTypeDisplays_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", this.DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
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
        public byte UpdateDisplayOrder(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocTypeDisplays_UpdateDisplayOrder");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", this.DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
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
        public byte Delete(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocTypeDisplays_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", this.DocTypeId));
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
        public List<DocTypeDisplays> GetList()
        {
            List<DocTypeDisplays> RetVal = new List<DocTypeDisplays>();
            try
            {
                string sql = "SELECT * FROM V$DocTypeDisplays";
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
        public static List<DocTypeDisplays> Static_GetList(string ConStr)
        {
            List<DocTypeDisplays> RetVal = new List<DocTypeDisplays>();
            try
            {
                DocTypeDisplays m_DocTypeDisplays = new DocTypeDisplays(ConStr);
                RetVal = m_DocTypeDisplays.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocTypeDisplays> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<DocTypeDisplays> GetListByDocTypeDisplayId(short DocTypeDisplayId)
        {
            List<DocTypeDisplays> RetVal = new List<DocTypeDisplays>();
            try
            {
                if (DocTypeDisplayId > 0)
                {
                    string sql = "SELECT * FROM V$DocTypeDisplays WHERE (DocTypeDisplayId=" + DocTypeDisplayId.ToString() + ")";
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
        public static List<DocTypeDisplays> Static_GetList_ByDisplayTypeId(short DisplayTypeId)
        {
            List<DocTypeDisplays> RetVal = new List<DocTypeDisplays>();
            try
            {
                DocTypeDisplays m_DocTypeDisplays = new DocTypeDisplays();
                RetVal = m_DocTypeDisplays.GetList_ByDisplayTypeId(DisplayTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<DocTypeDisplays> GetList_ByDisplayTypeId(short DisplayTypeId)
        {
            List<DocTypeDisplays> RetVal = new List<DocTypeDisplays>();
            try
            {
                if (DisplayTypeId > 0)
                {
                    string sql = "SELECT * FROM V$DocTypeDisplays WHERE (DisplayTypeId=" + DisplayTypeId.ToString() + ")";
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
        public DocTypeDisplays Get(short DocTypeDisplayId)
        {
            DocTypeDisplays RetVal = new DocTypeDisplays(db.ConnectionString);
            try
            {
                List<DocTypeDisplays> list = GetListByDocTypeDisplayId(DocTypeDisplayId);
                if (list.Count > 0)
                {
                    RetVal = (DocTypeDisplays)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocTypeDisplays Get()
        {
            return Get(this.DocTypeDisplayId);
        }
        //-------------------------------------------------------------- 
        public static DocTypeDisplays Static_Get(short DocTypeDisplayId)
        {
            return Static_Get(DocTypeDisplayId);
        }
        //--------------------------------------------------------------     
        public List<DocTypeDisplays> DocTypeDisplays_GetList(int ActUserId, string OrderBy, short DisplayTypeId, byte LanguageId)
        {
            List<DocTypeDisplays> RetVal = new List<DocTypeDisplays>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocTypeDisplays_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
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