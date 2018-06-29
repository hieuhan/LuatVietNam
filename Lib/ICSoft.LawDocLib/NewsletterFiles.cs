
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
    public class NewsletterFiles
    {
        private int _NewsletterFileId;
        private int _NewsletterId;
        private string _FileName;
        private string _FilePath;
        private int _FileSize;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public NewsletterFiles()
        {
            db = new DBAccess(DocConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public NewsletterFiles(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~NewsletterFiles()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int NewsletterFileId
        {
            get { return _NewsletterFileId; }
            set { _NewsletterFileId = value; }
        }
        //-----------------------------------------------------------------
        public int NewsletterId
        {
            get { return _NewsletterId; }
            set { _NewsletterId = value; }
        }
        //-----------------------------------------------------------------
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        //-----------------------------------------------------------------
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        //-----------------------------------------------------------------
        public int FileSize
        {
            get { return _FileSize; }
            set { _FileSize = value; }
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

        private List<NewsletterFiles> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<NewsletterFiles> l_NewsletterFiles = new List<NewsletterFiles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    NewsletterFiles m_NewsletterFiles = new NewsletterFiles(db.ConnectionString);
                    m_NewsletterFiles.NewsletterFileId = smartReader.GetInt32("NewsletterFileId");
                    m_NewsletterFiles.NewsletterId = smartReader.GetInt32("NewsletterId");
                    m_NewsletterFiles.FileName = smartReader.GetString("FileName");
                    m_NewsletterFiles.FilePath = smartReader.GetString("FilePath");
                    m_NewsletterFiles.FileSize = smartReader.GetInt32("FileSize");
                    m_NewsletterFiles.CrUserId = smartReader.GetInt32("CrUserId");
                    m_NewsletterFiles.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_NewsletterFiles.Add(m_NewsletterFiles);
                }
                reader.Close();
                return l_NewsletterFiles;
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
                SqlCommand cmd = new SqlCommand("NewsletterFiles_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterId", this.NewsletterId));
                cmd.Parameters.Add(new SqlParameter("@FileName", this.FileName));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@FileSize", this.FileSize));
                cmd.Parameters.Add(new SqlParameter("@NewsletterFileId", this.NewsletterFileId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.NewsletterFileId = Convert.ToInt32((cmd.Parameters["@NewsletterFileId"].Value == null) ? 0 : (cmd.Parameters["@NewsletterFileId"].Value));
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
                SqlCommand cmd = new SqlCommand("NewsletterFiles_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@NewsletterFileId", this.NewsletterFileId));
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
        public List<NewsletterFiles> GetList()
        {
            List<NewsletterFiles> RetVal = new List<NewsletterFiles>();
            try
            {
                string sql = "SELECT * FROM NewsletterFiles";
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
        public static List<NewsletterFiles> Static_GetList(string ConStr)
        {
            List<NewsletterFiles> RetVal = new List<NewsletterFiles>();
            try
            {
                NewsletterFiles m_NewsletterFiles = new NewsletterFiles(ConStr);
                RetVal = m_NewsletterFiles.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<NewsletterFiles> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<NewsletterFiles> GetListByNewsletterFileId(int NewsletterFileId)
        {
            List<NewsletterFiles> RetVal = new List<NewsletterFiles>();
            try
            {
                if (NewsletterFileId > 0)
                {
                    string sql = "SELECT * FROM NewsletterFiles WHERE (NewsletterFileId=" + NewsletterFileId.ToString() + ")";
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
        public NewsletterFiles Get(int NewsletterFileId)
        {
            NewsletterFiles RetVal = new NewsletterFiles(db.ConnectionString);
            try
            {
                List<NewsletterFiles> list = GetListByNewsletterFileId(NewsletterFileId);
                if (list.Count > 0)
                {
                    RetVal = (NewsletterFiles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public NewsletterFiles Get()
        {
            return Get(this.NewsletterFileId);
        }
        //-------------------------------------------------------------- 
        public static NewsletterFiles Static_Get(int NewsletterFileId)
        {
            return new NewsletterFiles().Get(NewsletterFileId);
        }
        //--------------------------------------------------------------     
        public List<NewsletterFiles> NewsletterFiles_GetList(int ActUserId, string OrderBy, int NewsletterId)
        {
            List<NewsletterFiles> RetVal = new List<NewsletterFiles>();
            try
            {
                SqlCommand cmd = new SqlCommand("NewsletterFiles_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@NewsletterId", NewsletterId));
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

