
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
    public class PageTypes
    {
        private byte _PageTypeId;
        private string _PageTypeName;
        private string _PageTypeDesc;
        private string _DefaultUrl;
        private string _DataUrl;
        private DBAccess db;
        //-----------------------------------------------------------------
        public PageTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public PageTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~PageTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte PageTypeId
        {
            get { return _PageTypeId; }
            set { _PageTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string PageTypeName
        {
            get { return _PageTypeName; }
            set { _PageTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string PageTypeDesc
        {
            get { return _PageTypeDesc; }
            set { _PageTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public string DefaultUrl
        {
            get { return _DefaultUrl; }
            set { _DefaultUrl = value; }
        }
        //-----------------------------------------------------------------
        public string DataUrl
        {
            get { return _DataUrl; }
            set { _DataUrl = value; }
        }
        //-----------------------------------------------------------------

        private List<PageTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<PageTypes> l_PageTypes = new List<PageTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PageTypes m_PageTypes = new PageTypes(db.ConnectionString);
                    m_PageTypes.PageTypeId = smartReader.GetByte("PageTypeId");
                    m_PageTypes.PageTypeName = smartReader.GetString("PageTypeName");
                    m_PageTypes.PageTypeDesc = smartReader.GetString("PageTypeDesc");
                    m_PageTypes.DefaultUrl = smartReader.GetString("DefaultUrl");
                    m_PageTypes.DataUrl = smartReader.GetString("DataUrl");

                    l_PageTypes.Add(m_PageTypes);
                }
                reader.Close();
                return l_PageTypes;
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
                SqlCommand cmd = new SqlCommand("PageTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PageTypeName", this.PageTypeName));
                cmd.Parameters.Add(new SqlParameter("@DefaultUrl", this.DefaultUrl));
                cmd.Parameters.Add(new SqlParameter("@DataUrl", this.DataUrl));
                cmd.Parameters.Add("@PageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PageTypeId = Convert.ToByte((cmd.Parameters["@PageTypeId"].Value == null) ? 0 : (cmd.Parameters["@PageTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("PageTypes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PageTypeName", this.PageTypeName));
                cmd.Parameters.Add(new SqlParameter("@DefaultUrl", this.DefaultUrl));
                cmd.Parameters.Add(new SqlParameter("@DataUrl", this.DataUrl));
                cmd.Parameters.Add(new SqlParameter("@PageTypeId", this.PageTypeId));
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
                SqlCommand cmd = new SqlCommand("PageTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PageTypeId", this.PageTypeId));
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
        public List<PageTypes> GetList()
        {
            List<PageTypes> RetVal = new List<PageTypes>();
            try
            {
                string sql = "SELECT * FROM V$PageTypes";
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
        public static List<PageTypes> Static_GetList(string ConStr)
        {
            List<PageTypes> RetVal = new List<PageTypes>();
            try
            {
                PageTypes m_PageTypes = new PageTypes(ConStr);
                RetVal = m_PageTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<PageTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<PageTypes> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            PageTypes m_PageTypes = new PageTypes(ConStr);
            List<PageTypes> RetVal = m_PageTypes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_PageTypes.PageTypeName = TextOptionAll;
                m_PageTypes.PageTypeName = TextOptionAll;
                RetVal.Insert(0, m_PageTypes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<PageTypes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<PageTypes> GetListOrderBy(string OrderBy)
        {
            List<PageTypes> RetVal = new List<PageTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$PageTypes ";
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
        public static List<PageTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            PageTypes m_PageTypes = new PageTypes(ConStr);
            return m_PageTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<PageTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<PageTypes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<PageTypes> RetVal = new List<PageTypes>();
            PageTypes m_PageTypes = new PageTypes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_PageTypes.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_PageTypes.PageTypeName = TextOptionAll;
                    m_PageTypes.PageTypeName = TextOptionAll;
                    RetVal.Insert(0, m_PageTypes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<PageTypes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<PageTypes> GetListByPageTypeId(byte PageTypeId)
        {
            List<PageTypes> RetVal = new List<PageTypes>();
            try
            {
                if (PageTypeId > 0)
                {
                    string sql = "SELECT * FROM V$PageTypes WHERE (PageTypeId=" + PageTypeId.ToString() + ")";
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
        public PageTypes Get(byte PageTypeId)
        {
            PageTypes RetVal = new PageTypes(db.ConnectionString);
            try
            {
                List<PageTypes> list = GetListByPageTypeId(PageTypeId);
                if (list.Count > 0)
                {
                    RetVal = (PageTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public PageTypes Get()
        {
            return Get(this.PageTypeId);
        }
        //-------------------------------------------------------------- 
        public static PageTypes Static_Get(byte PageTypeId)
        {
            PageTypes RetVal = new PageTypes();
            RetVal = RetVal.Get(PageTypeId);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static PageTypes Static_Get(List<PageTypes> l_PageTypes, byte PageTypeId)
        {
            PageTypes RetVal;
            RetVal = l_PageTypes.Find(x => x.PageTypeId == PageTypeId);
            if (RetVal == null)
                RetVal = new PageTypes();
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
