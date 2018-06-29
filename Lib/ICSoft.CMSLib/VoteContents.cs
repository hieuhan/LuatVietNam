
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSLib
{
    public class VoteContents
    {
        private byte _LanguageId;
        private int _VoteContentId;
        private int _VoteId;
        private string _VoteContent;
        private int _DisplayOrder;
        private int _VoteCounter;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public VoteContents()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public VoteContents(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~VoteContents()
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
        public int VoteContentId
        {
            get { return _VoteContentId; }
            set { _VoteContentId = value; }
        }
        //-----------------------------------------------------------------
        public int VoteId
        {
            get { return _VoteId; }
            set { _VoteId = value; }
        }
        //-----------------------------------------------------------------
        public string VoteContent
        {
            get { return _VoteContent; }
            set { _VoteContent = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------
        public int VoteCounter
        {
            get { return _VoteCounter; }
            set { _VoteCounter = value; }
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

        private List<VoteContents> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<VoteContents> l_VoteContents = new List<VoteContents>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    VoteContents m_VoteContents = new VoteContents(db.ConnectionString);
                    m_VoteContents.LanguageId = smartReader.GetByte("LanguageId");
                    m_VoteContents.VoteContentId = smartReader.GetInt32("VoteContentId");
                    m_VoteContents.VoteId = smartReader.GetInt32("VoteId");
                    m_VoteContents.VoteContent = smartReader.GetString("VoteContent");
                    m_VoteContents.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_VoteContents.VoteCounter = smartReader.GetInt32("VoteCounter");
                    m_VoteContents.CrUserId = smartReader.GetInt32("CrUserId");
                    m_VoteContents.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_VoteContents.Add(m_VoteContents);
                }
                reader.Close();
                return l_VoteContents;
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
                SqlCommand cmd = new SqlCommand("VoteContents_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@VoteId", this.VoteId));
                cmd.Parameters.Add(new SqlParameter("@VoteContent", this.VoteContent));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@VoteCounter", this.VoteCounter));
                cmd.Parameters.Add(new SqlParameter("@VoteContentId", this.VoteContentId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.VoteContentId = Convert.ToInt32((cmd.Parameters["@VoteContentId"].Value == null) ? 0 : (cmd.Parameters["@VoteContentId"].Value));
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
                SqlCommand cmd = new SqlCommand("VoteContents_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@VoteContentId", this.VoteContentId));
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
        public List<VoteContents> GetList()
        {
            List<VoteContents> RetVal = new List<VoteContents>();
            try
            {
                string sql = "SELECT * FROM V$VoteContents";
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
        public static List<VoteContents> Static_GetList(string ConStr)
        {
            List<VoteContents> RetVal = new List<VoteContents>();
            try
            {
                VoteContents m_VoteContents = new VoteContents(ConStr);
                RetVal = m_VoteContents.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<VoteContents> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<VoteContents> GetListOrderBy(string OrderBy)
        {
            List<VoteContents> RetVal = new List<VoteContents>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$VoteContents ";
                if (OrderBy.Length > 0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
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
        public static List<VoteContents> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            VoteContents m_VoteContents = new VoteContents(ConStr);
            return m_VoteContents.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<VoteContents> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<VoteContents> GetListByVoteContentId(int VoteId, int VoteContentId, byte LanguageId)
        {
            List<VoteContents> RetVal = new List<VoteContents>();
            try
            {
                int ActUserId = 0;
                string OrderBy = "";
                RetVal = VoteContents_GetList(ActUserId, OrderBy, LanguageId, VoteId, VoteContentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public VoteContents Get(int VoteId, int VoteContentId, byte LanguageId)
        {
            VoteContents RetVal = new VoteContents(db.ConnectionString);
            try
            {
                List<VoteContents> list = GetListByVoteContentId(VoteId, VoteContentId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (VoteContents)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public VoteContents Get()
        {
            return Get(this.VoteId, this.VoteContentId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static VoteContents Static_Get(string Constr, int VoteId, int VoteContentId, byte LanguageId)
        {
            VoteContents m_VoteContents = new VoteContents(Constr);
            return m_VoteContents.Get(VoteId, VoteContentId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static VoteContents Static_Get(int VoteId, int VoteContentId, byte LanguageId)
        {
            return Static_Get("", VoteId, VoteContentId, LanguageId);
        }
        //--------------------------------------------------------------     
        public List<VoteContents> VoteContents_GetList(int ActUserId, string OrderBy, byte LanguageId, int VoteId, int VoteContentId)
        {
            List<VoteContents> RetVal = new List<VoteContents>();
            try
            {
                SqlCommand cmd = new SqlCommand("VoteContents_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@VoteId", VoteId));
                cmd.Parameters.Add(new SqlParameter("@VoteContentId", VoteContentId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<VoteContents> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, int VoteContentId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<VoteContents> RetVal = new List<VoteContents>();
            try
            {
                SqlCommand cmd = new SqlCommand("VoteContents_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@VoteContentId", VoteContentId));
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
        public List<VoteContents> Search(int ActUserId, string OrderBy, byte LanguageId, int VoteContentId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, VoteContentId, DateFrom, DateTo, ref RowCount);
        }
    }
}