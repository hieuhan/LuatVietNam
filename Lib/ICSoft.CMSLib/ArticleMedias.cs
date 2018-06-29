
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
    public class ArticleMedias
    {
        private int _ArticleMediaId;
        private int _MediaId;
        private byte _MediaTypeId;
        private string _FilePath;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _ArticleId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticleMedias()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticleMedias(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticleMedias()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleMediaId
        {
            get { return _ArticleMediaId; }
            set { _ArticleMediaId = value; }
        }
        //-----------------------------------------------------------------
        public int MediaId
        {
            get { return _MediaId; }
            set { _MediaId = value; }
        }
        //-----------------------------------------------------------------
        public byte MediaTypeId
        {
            get { return _MediaTypeId; }
            set { _MediaTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
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

        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        private List<ArticleMedias> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleMedias> l_ArticleMedias = new List<ArticleMedias>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleMedias m_ArticleMedias = new ArticleMedias(db.ConnectionString);
                    m_ArticleMedias.ArticleMediaId = smartReader.GetInt32("ArticleMediaId");
                    m_ArticleMedias.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleMedias.MediaId = smartReader.GetInt32("MediaId");
                    m_ArticleMedias.MediaTypeId = smartReader.GetByte("MediaTypeId");
                    m_ArticleMedias.FilePath = smartReader.GetString("FilePath");
                    m_ArticleMedias.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ArticleMedias.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_ArticleMedias.Add(m_ArticleMedias);
                }
                reader.Close();
                return l_ArticleMedias;
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
                SqlCommand cmd = new SqlCommand("ArticleMedias_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@MediaId", this.MediaId));
                cmd.Parameters.Add(new SqlParameter("@MediaTypeId", this.MediaTypeId));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add("@ArticleMediaId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleMediaId = Convert.ToInt32(cmd.Parameters["@ArticleMediaId"].Value);
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
                SqlCommand cmd = new SqlCommand("ArticleMedias_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@MediaId", this.MediaId));
                cmd.Parameters.Add(new SqlParameter("@MediaTypeId", this.MediaTypeId));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@ArticleMediaId", this.ArticleMediaId));
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
                SqlCommand cmd = new SqlCommand("ArticleMedias_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@MediaId", this.MediaId));
                cmd.Parameters.Add(new SqlParameter("@ArticleMediaId", this.ArticleMediaId));
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
        public List<ArticleMedias> GetList(int ArticleId)
        {
            List<ArticleMedias> RetVal = new List<ArticleMedias>();
            try
            {
                string sql = "SELECT * FROM ArticleMedias WHERE ArticleId=" + ArticleId.ToString();
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
        public List<ArticleMedias> GetListByArticleMediaId(int ArticleMediaId)
        {
            List<ArticleMedias> RetVal = new List<ArticleMedias>();
            try
            {
                if (ArticleMediaId > 0)
                {
                    string sql = "SELECT * FROM ArticleMedias WHERE (ArticleMediaId=" + ArticleMediaId.ToString() + ")";
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
        public ArticleMedias Get(int ArticleMediaId)
        {
            ArticleMedias RetVal = new ArticleMedias(db.ConnectionString);
            try
            {
                List<ArticleMedias> list = GetListByArticleMediaId(ArticleMediaId);
                if (list.Count > 0)
                {
                    RetVal = (ArticleMedias)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public ArticleMedias Get()
        {
            return Get(this.ArticleMediaId);
        }
        //-------------------------------------------------------------- 
        public static ArticleMedias Static_Get(string Constr, int ArticleMediaId)
        {
            ArticleMedias m_ArticleMedias = new ArticleMedias(Constr);
            return m_ArticleMedias.Get(ArticleMediaId);
        }
        //-------------------------------------------------------------- 
        public static ArticleMedias Static_Get(short ArticleMediaId)
        {
            return Static_Get("", ArticleMediaId);
        }
        //-------------------------------------------------------------- 
        public static List<ArticleMedias> Static_GetList(string ConStr, int ArticleId)
        {
            List<ArticleMedias> RetVal = new List<ArticleMedias>();
            try
            {
                ArticleMedias m_ArticleMedias = new ArticleMedias(ConStr);
                RetVal = m_ArticleMedias.GetList(ArticleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<ArticleMedias> Static_GetList(int ArticleId)
        {
            return Static_GetList("", ArticleId);
        }
        //--------------------------------------------------------------     
    }
}

