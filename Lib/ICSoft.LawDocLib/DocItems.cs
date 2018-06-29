
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
    /// class DocItems
    /// </summary>
    public class DocItems
    {
        private int _DocItemId;
        private byte _LanguageId;
        private string _DocItemName;
        private string _DocItemLead;
        private int _DocItemOrder;
        private int _ParentDocItemId;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _DocId;
        private byte _DocItemLevelId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocItems()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocItems(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocItems()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocItemId
        {
            get { return _DocItemId; }
            set { _DocItemId = value; }
        }
        //-----------------------------------------------------------------
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------
        public string DocItemName
        {
            get { return _DocItemName; }
            set { _DocItemName = value; }
        }
        //-----------------------------------------------------------------
        public string DocItemLead
        {
            get { return _DocItemLead; }
            set { _DocItemLead = value; }
        }
        //-----------------------------------------------------------------
        public int DocItemOrder
        {
            get { return _DocItemOrder; }
            set { _DocItemOrder = value; }
        }
        //-----------------------------------------------------------------
        public int ParentDocItemId
        {
            get { return _ParentDocItemId; }
            set { _ParentDocItemId = value; }
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

        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }
        public byte DocItemLevelId
        {
            get { return _DocItemLevelId; }
            set { _DocItemLevelId = value; }
        }
        private List<DocItems> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocItems> l_DocItems = new List<DocItems>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocItems m_DocItems = new DocItems(db.ConnectionString);
                    m_DocItems.DocItemId = smartReader.GetInt32("DocItemId");
                    m_DocItems.LanguageId = smartReader.GetByte("LanguageId");
                    m_DocItems.DocId = smartReader.GetInt32("DocId");
                    m_DocItems.DocItemName = smartReader.GetString("DocItemName");
                    m_DocItems.DocItemLead = smartReader.GetString("DocItemLead");
                    m_DocItems.DocItemOrder = smartReader.GetInt32("DocItemOrder");
                    m_DocItems.DocItemLevelId = smartReader.GetByte("DocItemLevelId");
                    m_DocItems.ParentDocItemId = smartReader.GetInt32("ParentDocItemId");
                    m_DocItems.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocItems.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocItems.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DocItems.Add(m_DocItems);
                }
                reader.Close();
                return l_DocItems;
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
                SqlCommand cmd = new SqlCommand("DocItems_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocItemName", this.DocItemName));
                cmd.Parameters.Add(new SqlParameter("@DocItemLead", this.DocItemLead));
                cmd.Parameters.Add(new SqlParameter("@DocItemOrder", this.DocItemOrder));
                cmd.Parameters.Add(new SqlParameter("@DocItemLevelId", this.DocItemLevelId));
                cmd.Parameters.Add(new SqlParameter("@ParentDocItemId", this.ParentDocItemId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocItemId", this.DocItemId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocItemId = Convert.ToInt32((cmd.Parameters["@DocItemId"].Value == null) ? 0 : (cmd.Parameters["@DocItemId"].Value));
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
                SqlCommand cmd = new SqlCommand("DocItems_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocItemId", this.DocItemId));
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
        public List<DocItems> GetList()
        {
            List<DocItems> RetVal = new List<DocItems>();
            try
            {
                string sql = "SELECT * FROM V$DocItems";
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
        public static List<DocItems> Static_GetList(string ConStr)
        {
            List<DocItems> RetVal = new List<DocItems>();
            try
            {
                DocItems m_DocItems = new DocItems(ConStr);
                RetVal = m_DocItems.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocItems> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<DocItems> GetListByDocItemId(int DocItemId)
        {
            List<DocItems> RetVal = new List<DocItems>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DocItemId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocItems Get(int DocItemId)
        {
            DocItems RetVal = new DocItems(db.ConnectionString);
            try
            {
                List<DocItems> list = GetListByDocItemId(DocItemId);
                if (list.Count > 0)
                {
                    RetVal = (DocItems)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocItems Get()
        {
            return Get(this.DocItemId);
        }
        //-------------------------------------------------------------- 
        public static DocItems Static_Get(int DocItemId)
        {
            return Static_Get(DocItemId);
        }
        //--------------------------------------------------------------     
        public List<DocItems> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int DocItemId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocItems> RetVal = new List<DocItems>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocItems_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DocItemId", DocItemId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocItemName", this.DocItemName));
                cmd.Parameters.Add(new SqlParameter("@DocItemLead", this.DocItemLead));
                cmd.Parameters.Add(new SqlParameter("@DocItemOrder", this.DocItemOrder));
                cmd.Parameters.Add(new SqlParameter("@DocItemLevelId", this.DocItemLevelId));
                cmd.Parameters.Add(new SqlParameter("@ParentDocItemId", this.ParentDocItemId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
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
        public List<DocItems> GetList(int ActUserId, string OrderBy, byte LanguageId)
        {
            List<DocItems> RetVal = new List<DocItems>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocItems_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocItemName", this.DocItemName));
                cmd.Parameters.Add(new SqlParameter("@DocItemLevelId", this.DocItemLevelId));
                cmd.Parameters.Add(new SqlParameter("@ParentDocItemId", this.ParentDocItemId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<DocItems> Search(int ActUserId, string OrderBy, int DocItemId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DocItemId, DateFrom, DateTo, ref RowCount);
        }
    }
}