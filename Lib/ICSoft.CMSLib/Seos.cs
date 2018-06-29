
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
    public class Seos
    {
        private int _SeoId;
        private short _SiteId;
        private string _SeoName;
        private string _Url;
        private string _MetaTitle;
        private string _MetaDesc;
        private string _MetaKeyword;
        private string _CanonicalTag;
        private string _H1Tag;
        private string _SeoFooter;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Seos()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Seos(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Seos()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SeoId
        {
            get { return _SeoId; }
            set { _SeoId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        } 
        //-----------------------------------------------------------------
        public string SeoName
        {
            get { return _SeoName; }
            set { _SeoName = value; }
        }
        //-----------------------------------------------------------------
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        //-----------------------------------------------------------------
        public string MetaTitle
        {
            get { return _MetaTitle; }
            set { _MetaTitle = value; }
        }
        //-----------------------------------------------------------------
        public string MetaDesc
        {
            get { return _MetaDesc; }
            set { _MetaDesc = value; }
        }
        //-----------------------------------------------------------------
        public string MetaKeyword
        {
            get { return _MetaKeyword; }
            set { _MetaKeyword = value; }
        }
        //-----------------------------------------------------------------
        public string CanonicalTag
        {
            get { return _CanonicalTag; }
            set { _CanonicalTag = value; }
        }
        //-----------------------------------------------------------------
        public string H1Tag
        {
            get { return _H1Tag; }
            set { _H1Tag = value; }
        }
        //-----------------------------------------------------------------
        public string SeoFooter
        {
            get { return _SeoFooter; }
            set { _SeoFooter = value; }
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

        private List<Seos> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Seos> l_Seos = new List<Seos>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Seos m_Seos = new Seos(db.ConnectionString);
                    m_Seos.SeoId = smartReader.GetInt32("SeoId");
                    m_Seos.SiteId = smartReader.GetInt16("SiteId");
                    m_Seos.SeoName = smartReader.GetString("SeoName");
                    m_Seos.Url = smartReader.GetString("Url");
                    m_Seos.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Seos.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Seos.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Seos.CanonicalTag = smartReader.GetString("CanonicalTag");
                    m_Seos.H1Tag = smartReader.GetString("H1Tag");
                    m_Seos.SeoFooter = smartReader.GetString("SeoFooter");
                    m_Seos.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Seos.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Seos.Add(m_Seos);
                }
                reader.Close();
                return l_Seos;
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
                SqlCommand cmd = new SqlCommand("Seos_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@SeoName", this.SeoName));
                cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
                cmd.Parameters.Add(new SqlParameter("@MetaTitle", this.MetaTitle));
                cmd.Parameters.Add(new SqlParameter("@MetaDesc", this.MetaDesc));
                cmd.Parameters.Add(new SqlParameter("@MetaKeyword", this.MetaKeyword));
                cmd.Parameters.Add(new SqlParameter("@CanonicalTag", this.CanonicalTag));
                cmd.Parameters.Add(new SqlParameter("@H1Tag", this.H1Tag));
                cmd.Parameters.Add(new SqlParameter("@SeoFooter", this.SeoFooter));
                cmd.Parameters.Add("@SeoId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SeoId = (cmd.Parameters["@SeoId"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["@SeoId"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("Seos_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@SeoName", this.SeoName));
                cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
                cmd.Parameters.Add(new SqlParameter("@MetaTitle", this.MetaTitle));
                cmd.Parameters.Add(new SqlParameter("@MetaDesc", this.MetaDesc));
                cmd.Parameters.Add(new SqlParameter("@MetaKeyword", this.MetaKeyword));
                cmd.Parameters.Add(new SqlParameter("@CanonicalTag", this.CanonicalTag));
                cmd.Parameters.Add(new SqlParameter("@H1Tag", this.H1Tag));
                cmd.Parameters.Add(new SqlParameter("@SeoFooter", this.SeoFooter));
                cmd.Parameters.Add(new SqlParameter("@SeoId", this.SeoId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("Seos_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@SeoName", this.SeoName));
                cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
                cmd.Parameters.Add(new SqlParameter("@MetaTitle", this.MetaTitle));
                cmd.Parameters.Add(new SqlParameter("@MetaDesc", this.MetaDesc));
                cmd.Parameters.Add(new SqlParameter("@MetaKeyword", this.MetaKeyword));
                cmd.Parameters.Add(new SqlParameter("@CanonicalTag", this.CanonicalTag));
                cmd.Parameters.Add(new SqlParameter("@H1Tag", this.H1Tag));
                cmd.Parameters.Add(new SqlParameter("@SeoFooter", this.SeoFooter));
                cmd.Parameters.Add(new SqlParameter("@SeoId", this.SeoId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SeoId = (cmd.Parameters["@SeoId"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["@SeoId"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("Seos_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SeoId", this.SeoId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Seos> GetListBySeoId(int SeoId)
        {
            List<Seos> RetVal = new List<Seos>();
            try
            {
                if (SeoId > 0)
                {
                    string sql = "SELECT * FROM Seos WHERE (SeoId=" + SeoId.ToString() + ")";
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
        public Seos GetByUrl(string Url)
        {
            List<Seos> RetVal = new List<Seos>();
            try
            {
                string sql = "SELECT * FROM Seos WHERE (Url='" + StringUtil.InjectionString(Url) + "')";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal.Count > 0 ? RetVal[0] : new Seos();
        }
        //-------------------------------------------------------------- 
        public Seos Get(int SeoId)
        {
            Seos RetVal = new Seos();
            try
            {
                List<Seos> list = GetListBySeoId(SeoId);
                if (list.Count > 0)
                {
                    RetVal = (Seos)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Seos Static_Get(int SeoId, List<Seos> list)
        {
            Seos RetVal = new Seos();
            try
            {
                RetVal = list.Find(i => i.SeoId == SeoId);
                if (RetVal == null) RetVal = new Seos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Seos Get()
        {
            return Get(this.SeoId);
        }
        //-------------------------------------------------------------- 
        public static Seos Static_Get(int SeoId)
        {
            return new Seos().Get(SeoId);
        }
        //--------------------------------------------------------------     
        public List<Seos> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, ref int RowCount)
        {
            List<Seos> RetVal = new List<Seos>();
            try
            {
                SqlCommand cmd = new SqlCommand("Seos_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@SeoName", this.SeoName));
                cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
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
    }
}

