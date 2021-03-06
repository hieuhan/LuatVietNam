
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Configuration;
using Devshock.Net;

namespace ICSoft.LawDocsLib
{
    /// <summary>
    /// class DocFiles
    /// </summary>
    public class DocFiles
    {
        private byte _LanguageId;
        private int _DocFileId;
        private byte _FileTypeId;
        private string _FilePath;
        private int _FileSize;
        private short _DataSourceId;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _DocId;
        private string _DocFileName;
        private DBAccess db;
        private int _RevUserId;
        private DateTime _RevDateTime;
        //-----------------------------------------------------------------
        public DocFiles()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocFiles(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocFiles()
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
        public int DocFileId
        {
            get { return _DocFileId; }
            set { _DocFileId = value; }
        }
        //-----------------------------------------------------------------
        public byte FileTypeId
        {
            get { return _FileTypeId; }
            set { _FileTypeId = value; }
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
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
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
        //-----------------------------------------------------------------
        public DateTime RevDateTime
        {
            get { return _RevDateTime; }
            set { _RevDateTime = value; }
        }
        //-----------------------------------------------------------------
        public int RevUserId
        {
            get { return _RevUserId; }
            set { _RevUserId = value; }
        }
        //-----------------------------------------------------------------
        public string DocFileName
        {
            get { return _DocFileName; }
            set { _DocFileName = value; }
        }
        //-----------------------------------------------------------------
        private List<DocFiles> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocFiles> l_DocFiles = new List<DocFiles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocFiles m_DocFiles = new DocFiles(db.ConnectionString);
                    m_DocFiles.LanguageId = smartReader.GetByte("LanguageId");
                    m_DocFiles.DocFileId = smartReader.GetInt32("DocFileId");
                    m_DocFiles.DocId = smartReader.GetInt32("DocId");
                    m_DocFiles.DocFileName = smartReader.GetString("DocFileName");
                    m_DocFiles.FileTypeId = smartReader.GetByte("FileTypeId");
                    m_DocFiles.FilePath = smartReader.GetString("FilePath");
                    m_DocFiles.FileSize = smartReader.GetInt32("FileSize");
                    m_DocFiles.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_DocFiles.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocFiles.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocFiles.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_DocFiles.RevUserId = smartReader.GetInt32("RevUserId");
                    m_DocFiles.RevDateTime = smartReader.GetDateTime("RevDateTime");

                    l_DocFiles.Add(m_DocFiles);
                }
                reader.Close();
                return l_DocFiles;
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
        public byte UpdateReviewStatusId(int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocFiles_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocFilesId", this.DocId));
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocFiles_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocFileName", this.DocFileName));
                cmd.Parameters.Add(new SqlParameter("@FileTypeId", this.FileTypeId));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@FileSize", this.FileSize));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocFileId", this.DocFileId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocFileId = Convert.ToInt32((cmd.Parameters["@DocFileId"].Value == null) ? 0 : (cmd.Parameters["@DocFileId"].Value));
               // SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
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
                SqlCommand cmd = new SqlCommand("DocFiles_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocFileId", this.DocFileId));
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
        public List<DocFiles> GetList()
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                string sql = "SELECT * FROM V$DocFiles";
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
        public static List<DocFiles> Static_GetList(string ConStr)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                DocFiles m_DocFiles = new DocFiles(ConStr);
                RetVal = m_DocFiles.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocFiles> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<DocFiles> GetListByDocFileId(int DocFileId, byte LanguageId)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                int DocId = 0;
                byte FileTypeId = 0;
                short DataSourceId = 0;
                byte ReviewStatusId = 0;
                int CrUserId = 0;
                string OrderBy = "";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DocFileId, DocId, FileTypeId, LanguageId, DataSourceId,
                                        ReviewStatusId, CrUserId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public DocFiles Get(int DocFileId, byte LanguageId)
        {
            DocFiles RetVal = new DocFiles(db.ConnectionString);
            try
            {
                List<DocFiles> list = GetListByDocFileId(DocFileId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (DocFiles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocFiles Get()
        {
            return Get(this.DocFileId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static DocFiles Static_Get(string Constr, int DocFileId, byte LanguageId)
        {
            DocFiles m_DocFiles = new DocFiles(Constr);
            return m_DocFiles.Get(DocFileId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static DocFiles Static_Get(int DocFileId, byte LanguageId)
        {
            return Static_Get("", DocFileId, LanguageId);
        }
        //--------------------------------------------------------------     
        public List<DocFiles> GetListByDocId(int docId = 0)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                string sql = "SELECT * FROM V$DocFiles WHERE ReviewStatusId = 2 AND DocId ="+docId;
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
        public List<DocFiles> GetListSizeEmpty(int RowAmount)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                string sql = "SELECT top " + RowAmount + " * FROM V$DocFiles WHERE FileSize = 0";
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
        public List<DocFiles> GetAllByDocId(int docId)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                string sql = "SELECT * FROM V$DocFiles WHERE DocId = " + docId + " ORDER BY DocFileId";
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
        public static List<DocFiles> Static_GetListByDocId(int docId = 0)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                DocFiles m_DocFiles = new DocFiles();
                RetVal = m_DocFiles.GetListByDocId(docId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<DocFiles> DocFiles_GetList(int ActUserId, string OrderBy, int DocId)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                int RowAmount = 0;
                int PageIndex = 0;
                byte FileTypeId = 0;
                short DataSourceId = 0;
                byte ReviewStatusId = 0;
                int CrUserId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DocFileId, DocId, FileTypeId, LanguageId, DataSourceId,
                                        ReviewStatusId, CrUserId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<DocFiles> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int DocFileId, int DocId, byte FileTypeId, byte LanguageId, short DataSourceId, 
                                        byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocFiles_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DocFileId", DocFileId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@FileTypeId", FileTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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
        public List<DocFiles> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int DocFileId, int DocId, string DocIdentity, byte FileTypeId, byte LanguageId, short DataSourceId,
                                        byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocFiles_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DocFileId", DocFileId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", DocIdentity));
                cmd.Parameters.Add(new SqlParameter("@FileTypeId", FileTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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
        public List<DocFiles> GetView(int DocId)
        {
            List<DocFiles> RetVal = new List<DocFiles>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocFiles_GetView");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<DocFiles> Search(int ActUserId, string OrderBy, int DocFileId, int DocId, byte FileTypeId, byte LanguageId, short DataSourceId,
                                        byte ReviewStatusId, int CrUserId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DocFileId, DocId, FileTypeId, LanguageId, DataSourceId, 
                                        ReviewStatusId, CrUserId, DateFrom, DateTo, ref RowCount);
        }
        //------------- for sync-------------------------------------------------     
        public static void PutNofitySync(int DocFileId = 0)
        {
            //Put nofity sync file
            if (ConfigurationManager.AppSettings["SyncFileEnable"].ToString() == "1")
            {
                SockClient SocketClient = new SockClient();
                SocketClient.RemoteHost = ConfigurationManager.AppSettings["SyncFileRemoteHost"].ToString();
                SocketClient.RemotePort = Convert.ToInt32(ConfigurationManager.AppSettings["SyncFileRemotePort"].ToString());
                try
                {
                    SocketClient.Connect();
                    SocketClient.Send("SYNCDOC " + DocFileId.ToString() + Environment.NewLine);
                    if (SocketClient.Connected) SocketClient.Disconnect();
                }
                catch (Exception ex)
                {
                    Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                }
            }
        }
        public static void PutNofitySyncFile(string FileName)
        {
            //Put nofity sync file
            if (ConfigurationManager.AppSettings["SyncFileEnable"].ToString() == "1")
            {
                SockClient SocketClient = new SockClient();
                SocketClient.RemoteHost = ConfigurationManager.AppSettings["SyncFileRemoteHost"].ToString();
                SocketClient.RemotePort = Convert.ToInt32(ConfigurationManager.AppSettings["SyncFileRemotePort"].ToString());
                try
                {
                    SocketClient.Connect();
                    SocketClient.Send("SYNCDOCFILE " + FileName + Environment.NewLine);
                    if (SocketClient.Connected) SocketClient.Disconnect();
                }
                catch (Exception ex)
                {
                    Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
                }
            }
        }

        public void UpdateSyncStatus(int DocFileId, byte SyncStatusId)
        {
            SqlConnection con = new SqlConnection(db.ConnectionString);
            try
            {                
                string sql = "UPDATE DocFiles SET SyncStatusId=" + SyncStatusId.ToString() + " WHERE DocFileId=" + DocFileId.ToString();
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();               
            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}