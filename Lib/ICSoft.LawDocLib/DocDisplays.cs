
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
    /// class DocDisplays
    /// </summary>
    public class DocDisplays
    {
        private int _DocDisplayId;
        private short _DisplayTypeId;
        private byte _LanguageId;
        private int _DocId;
        private string _DocName;
        private DateTime _IssueDate;
        private DateTime _EffectDate;
        private DateTime _ExpireDate;
        private byte _ReviewStatusId;
        private string _EffectStatusName;
        private string _MetaTitle;
        private int _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;

        private string _docUrl;

        //-----------------------------------------------------------------
        public DocDisplays()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocDisplays(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocDisplays()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocDisplayId
        {
            get { return _DocDisplayId; }
            set { _DocDisplayId = value; }
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
        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }
        //-----------------------------------------------------------------
        public string DocName
        {
            get { return _DocName; }
            set { _DocName = value; }
        }
        public DateTime IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Ngày có hiệu lực.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu DateTime.
        /// </value>
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Gets or sets Ngày hết hiệu lực.
        /// </summary>
        /// <value>
        /// Kiểu dữ liệu DateTime.
        /// </value>
        public DateTime ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string EffectStatusName
        {
            get { return _EffectStatusName; }
            set { _EffectStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string MetaTitle
        {
            get { return _MetaTitle; }
            set { _MetaTitle = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
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
        public string DocUrl
        {
            get { return _docUrl; }
            set { _docUrl = value; }
        }
        //-----------------------------------------------------------------

        private List<DocDisplays> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocDisplays> l_DocDisplays = new List<DocDisplays>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocDisplays m_DocDisplays = new DocDisplays(db.ConnectionString);
                    m_DocDisplays.DocDisplayId = smartReader.GetInt32("DocDisplayId");
                    m_DocDisplays.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_DocDisplays.LanguageId = smartReader.GetByte("LanguageId");
                    m_DocDisplays.DocId = smartReader.GetInt32("DocId");
                    m_DocDisplays.DocName = smartReader.GetString("DocName");
                    m_DocDisplays.IssueDate = smartReader.GetDateTime("IssueDate");
                    m_DocDisplays.EffectDate = smartReader.GetDateTime("EffectDate");
                    m_DocDisplays.ExpireDate = smartReader.GetDateTime("ExpireDate");
                    m_DocDisplays.DocUrl = smartReader.GetString("DocUrl");
                    m_DocDisplays.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocDisplays.EffectStatusName = smartReader.GetString("EffectStatusName");
                    m_DocDisplays.MetaTitle = smartReader.GetString("MetaTitle");
                    m_DocDisplays.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_DocDisplays.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocDisplays.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DocDisplays.Add(m_DocDisplays);
                }
                reader.Close();
                return l_DocDisplays;
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
                SqlCommand cmd = new SqlCommand("DocDisplays_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                //cmd.Parameters.Add("@DocDisplayId", SqlDbType.Int).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //this.DocDisplayId = Convert.ToInt32((cmd.Parameters["@DocDisplayId"].Value == null) ? 0 : (cmd.Parameters["@DocDisplayId"].Value));
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = 1;// Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("DocDisplays_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DocDisplayId", this.DocDisplayId));
                //cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = 1;// Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("DocDisplays_UpdateDisplayOrder");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
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
        public byte ChangeOrder(string ActionOrder)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocDisplays_ChangeOrder");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@ActionOrder", ActionOrder));
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
        public byte Delete(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocDisplays_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
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
        public List<DocDisplays> GetList()
        {
            List<DocDisplays> RetVal = new List<DocDisplays>();
            try
            {
                string sql = "SELECT * FROM V$DocDisplays";
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
        public static List<DocDisplays> Static_GetList(string ConStr)
        {
            List<DocDisplays> RetVal = new List<DocDisplays>();
            try
            {
                DocDisplays m_DocDisplays = new DocDisplays(ConStr);
                RetVal = m_DocDisplays.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocDisplays> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<DocDisplays> GetListByDocDisplayId(int DocDisplayId, byte LanguageId)
        {
            List<DocDisplays> RetVal = new List<DocDisplays>();
            try
            {
                if (DocDisplayId > 0)
                {
                    string sql = "SELECT * FROM V$DocDisplays WHERE (DocDisplayId=" + DocDisplayId.ToString() + ") AND (LanguageId=" + LanguageId.ToString() + ")" ;
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
        public DocDisplays Get(int DocDisplayId, byte LanguageId)
        {
            DocDisplays RetVal = new DocDisplays(db.ConnectionString);
            try
            {
                List<DocDisplays> list = GetListByDocDisplayId(DocDisplayId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (DocDisplays)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocDisplays Get()
        {
            return Get(this.DocDisplayId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static DocDisplays Static_Get(string Constr, int DocDisplayId, byte LanguageId)
        {
            DocDisplays m_DocDisplays = new DocDisplays(Constr);
            return m_DocDisplays.Get(DocDisplayId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static DocDisplays Static_Get(int DocDisplayId, byte LanguageId)
        {
            return Static_Get("", DocDisplayId, LanguageId);
        }
        //--------------------------------------------------------------     
        public List<DocDisplays> DocDisplays_GetList(int ActUserId, string OrderBy, short DisplayTypeId, byte LanguageId)
        {
            List<DocDisplays> RetVal = new List<DocDisplays>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocDisplays_GetList");
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
        public List<DocDisplays> DocDisplays_GetListByFieldId(int ActUserId, string OrderBy, short DisplayTypeId, byte LanguageId, short FieldId)
        {
            List<DocDisplays> RetVal = new List<DocDisplays>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocDisplays_GetListByFieldId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
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