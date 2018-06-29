
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
    public class Languages
    {
        private byte _LanguageId;
        private string _LanguageName;
        private string _LanguageDesc;
        private string _LanguageCode;
        private string _IconPath;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Languages()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Languages(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Languages()
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
        public string LanguageName
        {
            get { return _LanguageName; }
            set { _LanguageName = value; }
        }
        //-----------------------------------------------------------------
        public string LanguageDesc
        {
            get { return _LanguageDesc; }
            set { _LanguageDesc = value; }
        }
        //-----------------------------------------------------------------
        public string LanguageCode
        {
            get { return _LanguageCode; }
            set { _LanguageCode = value; }
        }
        //-----------------------------------------------------------------
        public string IconPath
        {
            get { return _IconPath; }
            set { _IconPath = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<Languages> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Languages> l_Languages = new List<Languages>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Languages m_Languages = new Languages(db.ConnectionString);
                    m_Languages.LanguageId = smartReader.GetByte("LanguageId");
                    m_Languages.LanguageName = smartReader.GetString("LanguageName");
                    m_Languages.LanguageDesc = smartReader.GetString("LanguageDesc");
                    m_Languages.LanguageCode = smartReader.GetString("LanguageCode");
                    m_Languages.IconPath = smartReader.GetString("IconPath");
                    m_Languages.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_Languages.Add(m_Languages);
                }
                reader.Close();
                return l_Languages;
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
                SqlCommand cmd = new SqlCommand("Languages_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageName", this.LanguageName));
                cmd.Parameters.Add(new SqlParameter("@LanguageDesc", this.LanguageDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@LanguageId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.LanguageId = Convert.ToByte((cmd.Parameters["@LanguageId"].Value == null) ? 0 : (cmd.Parameters["@LanguageId"].Value));
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
                SqlCommand cmd = new SqlCommand("Languages_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageName", this.LanguageName));
                cmd.Parameters.Add(new SqlParameter("@LanguageDesc", this.LanguageDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
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
                SqlCommand cmd = new SqlCommand("Languages_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
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
        public List<Languages> GetList()
        {
            List<Languages> RetVal = new List<Languages>();
            try
            {
                string sql = "SELECT * FROM V$Languages";
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
        public Languages GetByCode(string LanguageCode)
        {
            Languages RetVal = new Languages();
            try
            {
                string sql = "SELECT * FROM V$Languages WHERE LanguageCode='" + StringUtil.InjectionString(LanguageCode) + "'";
                SqlCommand cmd = new SqlCommand(sql);
                List<Languages> list = Init(cmd);
                if (list.Count > 0) RetVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static List<Languages> Static_GetList(string ConStr)
        {
            List<Languages> RetVal = new List<Languages>();
            try
            {
                Languages m_Languages = new Languages(ConStr);
                RetVal = m_Languages.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Languages> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<Languages> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            Languages m_Languages = new Languages(ConStr);
            List<Languages> RetVal = m_Languages.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_Languages.LanguageName = TextOptionAll;
                m_Languages.LanguageDesc = TextOptionAll;
                RetVal.Insert(0, m_Languages);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Languages> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<Languages> GetListOrderBy(string OrderBy)
        {
            List<Languages> RetVal = new List<Languages>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Languages ";
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
        public static List<Languages> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Languages m_Languages = new Languages(ConStr);
            return m_Languages.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Languages> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Languages> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<Languages> RetVal = new List<Languages>();
            Languages m_Languages = new Languages(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_Languages.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_Languages.LanguageName = TextOptionAll;
                    m_Languages.LanguageDesc = TextOptionAll;
                    RetVal.Insert(0, m_Languages);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Languages> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<Languages> GetListByLanguageId(byte LanguageId)
        {
            List<Languages> RetVal = new List<Languages>();
            try
            {
                if (LanguageId > 0)
                {
                    string sql = "SELECT * FROM V$Languages WHERE (LanguageId=" + LanguageId.ToString() + ")";
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
        public Languages Get(byte LanguageId)
        {
            Languages RetVal = new Languages(db.ConnectionString);
            try
            {
                List<Languages> list = GetListByLanguageId(LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (Languages)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Languages Get()
        {
            return Get(this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Languages Static_Get(byte LanguageId)
        {
            return Static_Get(LanguageId);
        }
        //--------------------------------------------------------------
    }
}
