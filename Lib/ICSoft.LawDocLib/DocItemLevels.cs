
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
    /// class DocItemLevels
    /// </summary>
    public class DocItemLevels
    {
        private byte _DocItemLevelId;
        private string _DocItemLevelName;
        private string _DocItemLevelDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocItemLevels()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocItemLevels(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocItemLevels()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte DocItemLevelId
        {
            get { return _DocItemLevelId; }
            set { _DocItemLevelId = value; }
        }
        //-----------------------------------------------------------------
        public string DocItemLevelName
        {
            get { return _DocItemLevelName; }
            set { _DocItemLevelName = value; }
        }
        //-----------------------------------------------------------------
        public string DocItemLevelDesc
        {
            get { return _DocItemLevelDesc; }
            set { _DocItemLevelDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<DocItemLevels> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocItemLevels> l_DocItemLevels = new List<DocItemLevels>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocItemLevels m_DocItemLevels = new DocItemLevels(db.ConnectionString);
                    m_DocItemLevels.DocItemLevelId = smartReader.GetByte("DocItemLevelId");
                    m_DocItemLevels.DocItemLevelName = smartReader.GetString("DocItemLevelName");
                    m_DocItemLevels.DocItemLevelDesc = smartReader.GetString("DocItemLevelDesc");
                    m_DocItemLevels.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_DocItemLevels.Add(m_DocItemLevels);
                }
                reader.Close();
                return l_DocItemLevels;
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
                SqlCommand cmd = new SqlCommand("DocItemLevels_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocItemLevelName", this.DocItemLevelName));
                cmd.Parameters.Add(new SqlParameter("@DocItemLevelDesc", this.DocItemLevelDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@DocItemLevelId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocItemLevelId = Convert.ToByte((cmd.Parameters["@DocItemLevelId"].Value == null) ? 0 : (cmd.Parameters["@DocItemLevelId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocItemLevels_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocItemLevelName", this.DocItemLevelName));
                cmd.Parameters.Add(new SqlParameter("@DocItemLevelDesc", this.DocItemLevelDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DocItemLevelId", this.DocItemLevelId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocItemLevels_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocItemLevelId", this.DocItemLevelId));
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
        public List<DocItemLevels> GetList()
        {
            List<DocItemLevels> RetVal = new List<DocItemLevels>();
            try
            {
                string sql = "SELECT * FROM V$DocItemLevels";
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
        public static List<DocItemLevels> Static_GetList(string ConStr)
        {
            List<DocItemLevels> RetVal = new List<DocItemLevels>();
            try
            {
                DocItemLevels m_DocItemLevels = new DocItemLevels(ConStr);
                RetVal = m_DocItemLevels.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocItemLevels> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<DocItemLevels> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            DocItemLevels m_DocItemLevels = new DocItemLevels(ConStr);
            List<DocItemLevels> RetVal = m_DocItemLevels.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_DocItemLevels.DocItemLevelName = TextOptionAll;
                m_DocItemLevels.DocItemLevelDesc = TextOptionAll;
                RetVal.Insert(0, m_DocItemLevels);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<DocItemLevels> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<DocItemLevels> GetListOrderBy(string OrderBy)
        {
            List<DocItemLevels> RetVal = new List<DocItemLevels>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$DocItemLevels ";
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
        public static List<DocItemLevels> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            DocItemLevels m_DocItemLevels = new DocItemLevels(ConStr);
            return m_DocItemLevels.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<DocItemLevels> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<DocItemLevels> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<DocItemLevels> RetVal = new List<DocItemLevels>();
            DocItemLevels m_DocItemLevels = new DocItemLevels(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_DocItemLevels.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_DocItemLevels.DocItemLevelName = TextOptionAll;
                    m_DocItemLevels.DocItemLevelDesc = TextOptionAll;
                    RetVal.Insert(0, m_DocItemLevels);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<DocItemLevels> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<DocItemLevels> GetListByDocItemLevelId(byte DocItemLevelId)
        {
            List<DocItemLevels> RetVal = new List<DocItemLevels>();
            try
            {
                if (DocItemLevelId > 0)
                {
                    string sql = "SELECT * FROM V$DocItemLevels WHERE (DocItemLevelId=" + DocItemLevelId.ToString() + ")";
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
        public DocItemLevels Get(byte DocItemLevelId)
        {
            DocItemLevels RetVal = new DocItemLevels(db.ConnectionString);
            try
            {
                List<DocItemLevels> list = GetListByDocItemLevelId(DocItemLevelId);
                if (list.Count > 0)
                {
                    RetVal = (DocItemLevels)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocItemLevels Get()
        {
            return Get(this.DocItemLevelId);
        }
        //-------------------------------------------------------------- 
        public static DocItemLevels Static_Get(byte DocItemLevelId)
        {
            return Static_Get(DocItemLevelId);
        }
        //--------------------------------------------------------------
    }
}