
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Text.RegularExpressions;

namespace ICSoft.LawDocsLib
{
    /// <summary>
    /// class FieldDisplays
    /// </summary>
    public class FieldDisplays
    {   
	    private short _FieldDisplayId;
	    private short _DisplayTypeId;
        private byte _LanguageId;
	    private short _FieldId;
	    private short _DisplayOrder;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;

        private string _fieldName;
        private string _fieldDesc;

        //-----------------------------------------------------------------
		public FieldDisplays()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public FieldDisplays(string constr)
        {
            db = new DBAccess ((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FieldDisplays()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short FieldDisplayId
        {
		    get { return _FieldDisplayId; }
		    set { _FieldDisplayId = value; }
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
        public short FieldId
		{
            get { return _FieldId; }
		    set { _FieldId = value; }
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
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        //-----------------------------------------------------------------
        public string FiedlDesc
        {
            get { return _fieldDesc; }
            set { _fieldDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<FieldDisplays> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FieldDisplays> l_FieldDisplays = new List<FieldDisplays>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FieldDisplays m_FieldDisplays = new FieldDisplays(db.ConnectionString);
                    m_FieldDisplays.FieldDisplayId = smartReader.GetInt16("FieldDisplayId");
                    m_FieldDisplays.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_FieldDisplays.LanguageId = smartReader.GetByte("LanguageId");
                    m_FieldDisplays.FieldId = smartReader.GetInt16("FieldId");
                    m_FieldDisplays.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_FieldDisplays.CrUserId = smartReader.GetInt32("CrUserId");
                    m_FieldDisplays.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_FieldDisplays.FieldName = smartReader.GetString("FieldName");
                    m_FieldDisplays.FiedlDesc = smartReader.GetString("FiedlDesc");
         
                    l_FieldDisplays.Add(m_FieldDisplays);
                }
                reader.Close();
                return l_FieldDisplays;
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
                SqlCommand cmd = new SqlCommand("FieldDisplays_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("FieldDisplays_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@FieldDisplayId",this.FieldDisplayId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal =Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("FieldDisplays_UpdateDisplayOrder");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
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
                SqlCommand cmd = new SqlCommand("FieldDisplays_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
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
        //-------------------------------------------------------------- 
        public List<FieldDisplays> GetList()
        {
            List<FieldDisplays> RetVal = new List<FieldDisplays>();
            try
            {
                string sql = "SELECT * FROM V$FieldDisplays";
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
        public static List<FieldDisplays> Static_GetList(string ConStr)
        {
            List<FieldDisplays> RetVal = new List<FieldDisplays>();
            try
            {
                FieldDisplays m_FieldDisplays = new FieldDisplays(ConStr);
                RetVal = m_FieldDisplays.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<FieldDisplays> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<FieldDisplays> GetListByFieldDisplayId(short FieldDisplayId)
        {
            List<FieldDisplays> RetVal = new List<FieldDisplays>();
            try
            {
                if (FieldDisplayId > 0)
                {
                    string sql = "SELECT * FROM V$FieldDisplays WHERE (FieldDisplayId=" + FieldDisplayId.ToString() + ")";
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
        public FieldDisplays Get(short FieldDisplayId)
        {
            FieldDisplays RetVal = new FieldDisplays(db.ConnectionString);
            try
            {
                List<FieldDisplays> list = GetListByFieldDisplayId(FieldDisplayId);
                if (list.Count > 0)
                {
                    RetVal = (FieldDisplays)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public FieldDisplays Get()
        {
            return Get(this.FieldDisplayId);
        }
        //-------------------------------------------------------------- 
        public static FieldDisplays Static_Get(short FieldDisplayId)
        {
            return Static_Get(FieldDisplayId);
        }
        //--------------------------------------------------------------     
        public List<FieldDisplays> FieldDisplays_GetList(int ActUserId, string OrderBy, short DisplayTypeId, byte LanguageId, byte ReviewStatusId = 2)
        {
            List<FieldDisplays> RetVal = new List<FieldDisplays>();
            try
            {
                SqlCommand cmd = new SqlCommand("FieldDisplays_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return RetVal;
        }
        //--------------------------------------------------------------     
        //-------------------------------------------------------------- 
        public string GetUrl(byte DocGroupId = 1)
        {
            string RetVal = "";
            if (!string.IsNullOrEmpty(this.FieldName) && this.FieldId > 0)
            {
                RetVal = StringUtil.RemoveSignature(this.FieldName);
                RetVal = Regex.Replace(RetVal,"(?:[^a-z0-9 _-]|(?<=['\"])s)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                RetVal = RetVal.Replace(" ", "-");
                RetVal = RetVal.ToLower();
                RetVal = string.Concat(RetVal, "-", this.FieldId, "-", "f", DocGroupId, ".html");
                if (!RetVal.StartsWith(CMSLib.CmsConstants.ROOT_PATH))
                {
                    RetVal = CMSLib.CmsConstants.ROOT_PATH + RetVal;
                }
            }
            return RetVal;
        }
    } 
}

