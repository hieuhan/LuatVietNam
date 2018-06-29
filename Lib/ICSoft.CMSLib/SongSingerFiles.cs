
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Data;

namespace ICSoft.CMSLib
{
    public class SongSingerFiles
    {
        private int _SongSingerFileId;
        private int _SongSingerId;
        private byte _SongFileTypeId;
        private string _FilePath;
        private int _FileSize;
        private string _EmbedScript;
        private short _DataSourceId;
        private byte _MusicContentTypeId;
        private int _Price;
        private int _PromotionPrice;
        private byte _IsOfficial;
        private byte _ReviewStatusId;
        private int _ViewCount;
        private int _DownloadCount;
        private int _ShareCount;
        private int _LikeCount;
        private int _PostUserId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongSingerFiles()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongSingerFiles(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongSingerFiles()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerFileId
        {
            get { return _SongSingerFileId; }
            set { _SongSingerFileId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public byte SongFileTypeId
        {
            get { return _SongFileTypeId; }
            set { _SongFileTypeId = value; }
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
        public string EmbedScript
        {
            get { return _EmbedScript; }
            set { _EmbedScript = value; }
        }
        //-----------------------------------------------------------------
        public short DataSourceId
        {
            get { return _DataSourceId; }
            set { _DataSourceId = value; }
        }
        //-----------------------------------------------------------------
        public byte MusicContentTypeId
        {
            get { return _MusicContentTypeId; }
            set { _MusicContentTypeId = value; }
        }
        //-----------------------------------------------------------------
        public int Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        //-----------------------------------------------------------------
        public int PromotionPrice
        {
            get { return _PromotionPrice; }
            set { _PromotionPrice = value; }
        }
        //-----------------------------------------------------------------
        public byte IsOfficial
        {
            get { return _IsOfficial; }
            set { _IsOfficial = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public int ViewCount
        {
            get { return _ViewCount; }
            set { _ViewCount = value; }
        }
        //-----------------------------------------------------------------
        public int DownloadCount
        {
            get { return _DownloadCount; }
            set { _DownloadCount = value; }
        }
        //-----------------------------------------------------------------
        public int ShareCount
        {
            get { return _ShareCount; }
            set { _ShareCount = value; }
        }
        //-----------------------------------------------------------------
        public int LikeCount
        {
            get { return _LikeCount; }
            set { _LikeCount = value; }
        }
        //-----------------------------------------------------------------
        public int PostUserId
        {
            get { return _PostUserId; }
            set { _PostUserId = value; }
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

        private List<SongSingerFiles> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongSingerFiles> l_SongSingerFiles = new List<SongSingerFiles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongSingerFiles m_SongSingerFiles = new SongSingerFiles(db.ConnectionString);
                    m_SongSingerFiles.SongSingerFileId = smartReader.GetInt32("SongSingerFileId");
                    m_SongSingerFiles.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerFiles.SongFileTypeId = smartReader.GetByte("SongFileTypeId");
                    m_SongSingerFiles.FilePath = smartReader.GetString("FilePath");
                    m_SongSingerFiles.FileSize = smartReader.GetInt32("FileSize");
                    m_SongSingerFiles.EmbedScript = smartReader.GetString("EmbedScript");
                    m_SongSingerFiles.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_SongSingerFiles.MusicContentTypeId = smartReader.GetByte("MusicContentTypeId");
                    m_SongSingerFiles.Price = smartReader.GetInt32("Price");
                    m_SongSingerFiles.PromotionPrice = smartReader.GetInt32("PromotionPrice");
                    m_SongSingerFiles.IsOfficial = smartReader.GetByte("IsOfficial");
                    m_SongSingerFiles.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_SongSingerFiles.ViewCount = smartReader.GetInt32("ViewCount");
                    m_SongSingerFiles.DownloadCount = smartReader.GetInt32("DownloadCount");
                    m_SongSingerFiles.ShareCount = smartReader.GetInt32("ShareCount");
                    m_SongSingerFiles.LikeCount = smartReader.GetInt32("LikeCount");
                    m_SongSingerFiles.PostUserId = smartReader.GetInt32("PostUserId");
                    m_SongSingerFiles.CrUserId = smartReader.GetInt32("CrUserId");
                    m_SongSingerFiles.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_SongSingerFiles.Add(m_SongSingerFiles);
                }
                reader.Close();
                return l_SongSingerFiles;
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
                SqlCommand cmd = new SqlCommand("SongSingerFiles_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@SongFileTypeId", this.SongFileTypeId));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@FileSize", this.FileSize));
                cmd.Parameters.Add(new SqlParameter("@EmbedScript", this.EmbedScript));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@MusicContentTypeId", this.MusicContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@PromotionPrice", this.PromotionPrice));
                cmd.Parameters.Add(new SqlParameter("@IsOfficial", this.IsOfficial));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ViewCount", this.ViewCount));
                cmd.Parameters.Add(new SqlParameter("@DownloadCount", this.DownloadCount));
                cmd.Parameters.Add(new SqlParameter("@ShareCount", this.ShareCount));
                cmd.Parameters.Add(new SqlParameter("@LikeCount", this.LikeCount));
                cmd.Parameters.Add(new SqlParameter("@PostUserId", this.PostUserId));
                cmd.Parameters.Add("@SongSingerFileId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerFileId = (cmd.Parameters["@SongSingerFileId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerFileId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongSingerFiles_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@SongFileTypeId", this.SongFileTypeId));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@FileSize", this.FileSize));
                cmd.Parameters.Add(new SqlParameter("@EmbedScript", this.EmbedScript));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@MusicContentTypeId", this.MusicContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@PromotionPrice", this.PromotionPrice));
                cmd.Parameters.Add(new SqlParameter("@IsOfficial", this.IsOfficial));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ViewCount", this.ViewCount));
                cmd.Parameters.Add(new SqlParameter("@DownloadCount", this.DownloadCount));
                cmd.Parameters.Add(new SqlParameter("@ShareCount", this.ShareCount));
                cmd.Parameters.Add(new SqlParameter("@LikeCount", this.LikeCount));
                cmd.Parameters.Add(new SqlParameter("@PostUserId", this.PostUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerFileId", this.SongSingerFileId));
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
                SqlCommand cmd = new SqlCommand("SongSingerFiles_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@SongFileTypeId", this.SongFileTypeId));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@FileSize", this.FileSize));
                cmd.Parameters.Add(new SqlParameter("@EmbedScript", this.EmbedScript));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@MusicContentTypeId", this.MusicContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@PromotionPrice", this.PromotionPrice));
                cmd.Parameters.Add(new SqlParameter("@IsOfficial", this.IsOfficial));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ViewCount", this.ViewCount));
                cmd.Parameters.Add(new SqlParameter("@DownloadCount", this.DownloadCount));
                cmd.Parameters.Add(new SqlParameter("@ShareCount", this.ShareCount));
                cmd.Parameters.Add(new SqlParameter("@LikeCount", this.LikeCount));
                cmd.Parameters.Add(new SqlParameter("@PostUserId", this.PostUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerFileId", this.SongSingerFileId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerFileId = (cmd.Parameters["@SongSingerFileId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerFileId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongSingerFiles_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerFileId", this.SongSingerFileId));
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
        public List<SongSingerFiles> GetList(int SongSingerId)
        {
            List<SongSingerFiles> RetVal = new List<SongSingerFiles>();
            try
            {
                string sql = "SELECT * FROM SongSingerFiles WHERE SongSingerId=" + SongSingerId.ToString();
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
        public List<SongSingerFiles> GetListBySongSingerFileId(int SongSingerFileId)
        {
            List<SongSingerFiles> RetVal = new List<SongSingerFiles>();
            try
            {
                if (SongSingerFileId > 0)
                {
                    string sql = "SELECT * FROM SongSingerFiles WHERE (SongSingerFileId=" + SongSingerFileId.ToString() + ")";
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
        public SongSingerFiles GetBySongSingerIdAndFileTypeId(int SongSingerId, byte SongFileTypeId)
        {
            List<SongSingerFiles> RetVal = new List<SongSingerFiles>();
            try
            {
                string sql = "SELECT * FROM SongSingerFiles WHERE (SongSingerId=" + SongSingerId.ToString() + " AND SongFileTypeId=" + SongFileTypeId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal.Count > 0 ? RetVal[0] : new SongSingerFiles();
        }
        //-------------------------------------------------------------- 
        public SongSingerFiles Get(int SongSingerFileId)
        {
            SongSingerFiles RetVal = new SongSingerFiles();
            try
            {
                List<SongSingerFiles> list = GetListBySongSingerFileId(SongSingerFileId);
                if (list.Count > 0)
                {
                    RetVal = (SongSingerFiles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static SongSingerFiles Static_Get(int SongSingerFileId, List<SongSingerFiles> list)
        {
            SongSingerFiles RetVal = new SongSingerFiles();
            try
            {
                RetVal = list.Find(i => i.SongSingerFileId == SongSingerFileId);
                if (RetVal == null) RetVal = new SongSingerFiles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static SongSingerFiles Static_Get(byte SongFileTypeId, List<SongSingerFiles> list)
        {
            SongSingerFiles RetVal = new SongSingerFiles();
            try
            {
                RetVal = list.Find(i => i.SongFileTypeId == SongFileTypeId);
                if (RetVal == null) RetVal = new SongSingerFiles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SongSingerFiles Get()
        {
            return Get(this.SongSingerFileId);
        }
        //-------------------------------------------------------------- 
        public static SongSingerFiles Static_Get(int SongSingerFileId)
        {
            return new SongSingerFiles().Get(SongSingerFileId);
        }
        //-------------------------------------------------------------- 
        public static List<SongSingerFiles> Static_GetList(int SongSingerId)
        {
            return new SongSingerFiles().GetList(SongSingerId);
        }
        
    }
}

