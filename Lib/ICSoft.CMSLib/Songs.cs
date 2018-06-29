
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Data;
using ICSoft.CMSViewLib;

namespace ICSoft.CMSLib
{
    public class Songs
    {
        private int _SongSingerId;
        private string _SongSingerName;
        private int _SongId;
        private string _SongName;
        private string _SongDesc;
        private string _Lyric;
        private short _CountryId;
        private byte _ReviewStatusId;
        public byte IsHot { get; set; }
        public byte IsVideoHot { get; set; }
        public byte VideoStatusId { get; set; }
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Songs()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Songs(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Songs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongId
        {
            get { return _SongId; }
            set { _SongId = value; }
        }
        //-----------------------------------------------------------------    
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public string SongSingerName
        {
            get { return _SongSingerName; }
            set { _SongSingerName = value; }
        }
        //-----------------------------------------------------------------
        public string SongName
        {
            get { return _SongName; }
            set { _SongName = value; }
        }
        //-----------------------------------------------------------------
        public string SongDesc
        {
            get { return _SongDesc; }
            set { _SongDesc = value; }
        }
        //-----------------------------------------------------------------
        public string Lyric
        {
            get { return _Lyric; }
            set { _Lyric = value; }
        }
        //-----------------------------------------------------------------
        public short CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
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

        private List<Songs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Songs> l_Songs = new List<Songs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Songs m_Songs = new Songs(db.ConnectionString);
                    m_Songs.SongId = smartReader.GetInt32("SongId");
                    m_Songs.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_Songs.SongSingerName = smartReader.GetString("SongSingerName");
                    m_Songs.SongName = smartReader.GetString("SongName");
                    m_Songs.SongDesc = smartReader.GetString("SongDesc");
                    m_Songs.Lyric = smartReader.GetString("Lyric");
                    m_Songs.CountryId = smartReader.GetInt16("CountryId");
                    m_Songs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Songs.VideoStatusId = smartReader.GetByte("VideoStatusId");
                    m_Songs.IsHot = smartReader.GetByte("IsHot");
                    m_Songs.IsVideoHot = smartReader.GetByte("IsVideoHot");
                    m_Songs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Songs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Songs.Add(m_Songs);
                }
                reader.Close();
                return l_Songs;
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
        public byte SyncFunring(int FrId, string FrToneCode,string FrToneName,string FrSongName, string FrSingerName, int FrPrice, byte  FrStatus, int FrViewCount, int FrDownloadCount, DateTime  FrExpiredTime, string FrUrlListen, int FrHashCode, byte hasFile)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_SyncFunring");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FrId", FrId));
                cmd.Parameters.Add(new SqlParameter("@FrToneCode", FrToneCode));
                cmd.Parameters.Add(new SqlParameter("@FrToneName", FrToneName));
                cmd.Parameters.Add(new SqlParameter("@FrSongName", FrSongName));
                cmd.Parameters.Add(new SqlParameter("@FrSingerName", FrSingerName));
                cmd.Parameters.Add(new SqlParameter("@FrPrice", FrPrice));
                cmd.Parameters.Add(new SqlParameter("@FrStatus", FrStatus));
                cmd.Parameters.Add(new SqlParameter("@FrViewCount", FrViewCount));
                cmd.Parameters.Add(new SqlParameter("@FrDownloadCount", FrDownloadCount));
                cmd.Parameters.Add(new SqlParameter("@FrExpiredTime", FrExpiredTime));
                cmd.Parameters.Add(new SqlParameter("@FrUrlListen", FrUrlListen));
                cmd.Parameters.Add(new SqlParameter("@FrHashCode", FrHashCode));
                cmd.Parameters.Add(new SqlParameter("@hasFile", hasFile));
                cmd.Parameters.Add("@IsSync", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToByte((cmd.Parameters["@IsSync"].Value == null) ? "0" : cmd.Parameters["@IsSync"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte SyncNhacSo(byte Replicated, int ActUserId, string SongName, string SongDesc, short CountryId, string Lyric, string AuthorName, string SingerName, string SongSingerName, string MusicType, string SongFilePath, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_InsertOrUpdateNhacSo");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongName", SongName));
                cmd.Parameters.Add(new SqlParameter("@SongDesc", SongDesc));
                cmd.Parameters.Add(new SqlParameter("@CountryId", CountryId));
                cmd.Parameters.Add(new SqlParameter("@Lyric", Lyric));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@AuthorName", AuthorName));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@SongSingerName", SongSingerName));
                cmd.Parameters.Add(new SqlParameter("@MusicType", MusicType));
                cmd.Parameters.Add(new SqlParameter("@SongFilePath", SongFilePath));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongId = (cmd.Parameters["@SongId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongId"].Value);
                this.SongSingerId = (cmd.Parameters["@SongSingerId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerId"].Value);
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
        public byte SyncVideo(byte Replicated, int ActUserId, string SongName, string SongDesc, short CountryId, string Lyric, string AuthorName, string SingerName, string SongSingerName, string MusicType, string SongFilePath, string SongFilePathImage, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_InsertOrUpdateVideo");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongName", SongName));
                cmd.Parameters.Add(new SqlParameter("@SongDesc", SongDesc));
                cmd.Parameters.Add(new SqlParameter("@CountryId", CountryId));
                cmd.Parameters.Add(new SqlParameter("@Lyric", Lyric));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@AuthorName", AuthorName));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@SongSingerName", SongSingerName));
                cmd.Parameters.Add(new SqlParameter("@MusicType", MusicType));
                cmd.Parameters.Add(new SqlParameter("@SongFilePath", SongFilePath));
                cmd.Parameters.Add(new SqlParameter("@SongFilePathImage", SongFilePathImage));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongId = (cmd.Parameters["@SongId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongId"].Value);
                this.SongSingerId = (cmd.Parameters["@SongSingerId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerId"].Value);
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
        public byte SyncAlbum(byte Replicated, int ActUserId, string AlbumName, string SingerName, string MusicType, string AlbumInfo, string ImageFilePath, string SongList, ref int AlbumId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_InsertOrUpdateAlbum");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@AlbumName", AlbumName));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@MusicType", MusicType));
                cmd.Parameters.Add(new SqlParameter("@AlbumInfo", AlbumInfo));
                cmd.Parameters.Add(new SqlParameter("@ImageFilePath", ImageFilePath));
                cmd.Parameters.Add(new SqlParameter("@SongList", SongList));
                cmd.Parameters.Add(new SqlParameter("@AlbumId", AlbumId)).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                AlbumId = (cmd.Parameters["@AlbumId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@AlbumId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte Insert(byte Replicated, int ActUserId, string AuthorName, string SingerName, string SongSingerName, string AlbumName, ref int SongSingerId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongName", this.SongName));
                cmd.Parameters.Add(new SqlParameter("@SongDesc", this.SongDesc));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@Lyric", this.Lyric));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@AuthorName", AuthorName));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@SongSingerName", SongSingerName));
                cmd.Parameters.Add(new SqlParameter("@AlbumName", AlbumName));
                cmd.Parameters.Add("@SongId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SongSingerId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongId = (cmd.Parameters["@SongId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongId"].Value);
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
        public byte Update(byte Replicated, int ActUserId, string AuthorName, string SingerName, string SongSingerName, string AlbumName, ref int SongSingerId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongName", this.SongName));
                cmd.Parameters.Add(new SqlParameter("@SongDesc", this.SongDesc));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@Lyric", this.Lyric));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@AuthorName", AuthorName));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@SongSingerName", SongSingerName));
                cmd.Parameters.Add(new SqlParameter("@AlbumName", AlbumName));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId));
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
        public byte InsertOrUpdate(byte Replicated, int ActUserId, string AuthorName, string SingerName, string SongSingerName, string AlbumName, ref int SongSingerId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongName", this.SongName));
                cmd.Parameters.Add(new SqlParameter("@SongDesc", this.SongDesc));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@Lyric", this.Lyric));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@AuthorName", AuthorName));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@SongSingerName", SongSingerName));
                cmd.Parameters.Add(new SqlParameter("@AlbumName", AlbumName));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongId = (cmd.Parameters["@SongId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongId"].Value);
                SongSingerId = (cmd.Parameters["@SongSingerId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerId"].Value);
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
                SqlCommand cmd = new SqlCommand("Songs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId));
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
        public byte UpdateLyric(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_UpdateLyric");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId));
                cmd.Parameters.Add(new SqlParameter("@Lyric", this.Lyric));
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
        public byte UpdateSingerImage(string NameHasSign, string FilePath, string ImageCover, string Profile)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Singers_UpdateImage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@NameHasSign", NameHasSign));
                cmd.Parameters.Add(new SqlParameter("@FilePath", FilePath));
                cmd.Parameters.Add(new SqlParameter("@ImageCover", ImageCover));
                cmd.Parameters.Add(new SqlParameter("@Profile", Profile));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte UpdateIsHot(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingers_UpdateIsHot_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@IsHot", this.IsHot));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte UpdateIsNew(byte Replicated, int ActUserId, byte IsNew, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingers_UpdateIsNew_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@IsNew", IsNew));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte UpdateIsVideoHot(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingers_UpdateIsVideoHot_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@IsHot", this.IsHot));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public void ChangeMusicType(int ActUserId, short MusicTypeIdFrom, short MusicTypeIdTo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_ChangeMusicType_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MusicTypeIdFrom", MusicTypeIdFrom));
                cmd.Parameters.Add(new SqlParameter("@MusicTypeIdTo", MusicTypeIdTo));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------            
        public void ChangeSinger(int ActUserId, int SingerIdFrom, int SingerIdTo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_ChangeSinger_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SingerIdFrom", SingerIdFrom));
                cmd.Parameters.Add(new SqlParameter("@SingerIdTo", SingerIdTo));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------------------- 
        public List<Songs> GetListBySongId(int SongId)
        {
            List<Songs> RetVal = new List<Songs>();
            try
            {
                if (SongId > 0)
                {
                    string sql = "SELECT * FROM Songs WHERE (SongId=" + SongId.ToString() + ")";
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
        public Songs Get(int SongId)
        {
            Songs RetVal = new Songs();
            try
            {
                List<Songs> list = GetListBySongId(SongId);
                if (list.Count > 0)
                {
                    RetVal = (Songs)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Songs Static_Get(int SongId, List<Songs> list)
        {
            Songs RetVal = new Songs();
            try
            {
                RetVal = list.Find(i => i.SongId == SongId);
                if (RetVal == null) RetVal = new Songs();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Songs Get()
        {
            return Get(this.SongId);
        }
        //-------------------------------------------------------------- 
        public static Songs Static_Get(string Constr, int SongId)
        {
            Songs m_Songs = new Songs(Constr);
            return m_Songs.Get(SongId);
        }
        //-------------------------------------------------------------- 
        public static Songs Static_Get(int SongId)
        {
            return Static_Get("", SongId);
        }
        //-------------------------------------------------------------- 
        public List<SongsView> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int SongSingerId, short MusicTypeId, short CategoryId, int AlbumId, int SingerId, int AuthorId, short DataSourceId, byte TelcoId, byte MusicContentTypeId, byte ShowVideo, string SearchBy, byte GetSongSinger, byte GetSongSingerFile, byte GetSongSingerRBT, string DateFrom, string DateTo, ref int RowCount)
        {
            List<SongsView> RetVal = new List<SongsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@MusicTypeId", MusicTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@AlbumId", AlbumId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", TelcoId));
                cmd.Parameters.Add(new SqlParameter("@SingerId", SingerId));
                cmd.Parameters.Add(new SqlParameter("@AuthorId", AuthorId));
                cmd.Parameters.Add(new SqlParameter("@MusicContentTypeId", MusicContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ShowVideo", ShowVideo));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@GetSongSinger", GetSongSinger));
                cmd.Parameters.Add(new SqlParameter("@GetSongSingerFile", GetSongSingerFile));
                cmd.Parameters.Add(new SqlParameter("@GetSongSingerRBT", GetSongSingerRBT));
                cmd.Parameters.Add(new SqlParameter("@SearchBy", SearchBy));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId));
                cmd.Parameters.Add(new SqlParameter("@SongName", this.SongName));
                cmd.Parameters.Add(new SqlParameter("@SongDesc", this.SongDesc));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    RetVal = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);

                    //Get MusicType
                    reader.NextResult();
                    List<SongMusicTypesView> l_MusicTypes = SongMusicTypesView.Init(smartReader, true);

                    //Get Authors
                    reader.NextResult();
                    List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);

                    //Get Category
                    reader.NextResult();
                    List<SongSingerCategoriesView> l_Category = null;
                    List<SongSingerRBTCategoriesView> l_CategoryRBT = null;
                    if (GetSongSingerRBT == 2) l_CategoryRBT = SongSingerRBTCategoriesView.Init(smartReader, true);
                    else l_Category = SongSingerCategoriesView.Init(smartReader, true);

                    List<SongSingerSingersView> l_Singers = null;
                    List<SongSingerAlbumsView> l_Albums = null;
                    List<SongSingerFilesView> l_Files = null;
                    List<SongSingerRBTsView> l_RBTs = null;
                    if (GetSongSinger == 1)
                    {
                        //Get Singers
                        reader.NextResult();
                        l_Singers = SongSingerSingersView.Init(smartReader, true);
                        //Get Albums
                        reader.NextResult();
                        l_Albums = SongSingerAlbumsView.Init(smartReader, true);
                        //Get Albums
                        if (GetSongSingerFile == 1)
                        {
                            reader.NextResult();
                            l_Files = SongSingerFilesView.Init(smartReader);
                        }
                        //Get RBT
                        if (GetSongSingerRBT == 1)
                        {
                            reader.NextResult();
                            l_RBTs = SongSingerRBTsView.Init(smartReader);
                        }
                    }

                    reader.Close();

                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].lMusicTypes = SongMusicTypesView.Static_GetList(RetVal[i].SongId, l_MusicTypes);
                        RetVal[i].lAuthors = SongAuthorsView.Static_GetList(RetVal[i].SongId, l_Authors);
                        if (GetSongSingerRBT == 2) RetVal[i].lCategoryRBT = SongSingerRBTCategoriesView.Static_GetListBySongSingerRBTId(RetVal[i].SongSingerRBTId, l_CategoryRBT);
                        else RetVal[i].lCategory = SongSingerCategoriesView.Static_GetListBySongSingerId(RetVal[i].SongSingerId, l_Category);
                        
                        if (GetSongSinger == 1)
                        {
                            RetVal[i].lSingers = SongSingerSingersView.Static_GetList(RetVal[i].SongSingerId, l_Singers);
                            RetVal[i].lAlbums = SongSingerAlbumsView.Static_GetList(RetVal[i].SongSingerId, l_Albums);
                            if (GetSongSingerFile == 1) RetVal[i].lFiles = SongSingerFilesView.Static_GetList(RetVal[i].SongSingerId, l_Files);
                            if (GetSongSingerRBT == 1) RetVal[i].lRBTs = SongSingerRBTsView.Static_GetList(RetVal[i].SongSingerId, l_RBTs);
                        }
                    }
                }
                catch (SqlException err)
                {
                    throw new ApplicationException("Data error: " + err.Message);
                }
                finally
                {
                    db.closeConnection(con);
                }

                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public List<SongsView> GetPageByKeyword(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int SongSingerId, short MusicTypeId, short CategoryId, int AlbumId, int SingerId, int AuthorId, short DataSourceId, byte TelcoId, byte MusicContentTypeId, byte ShowVideo, string SearchBy, byte GetSongSinger, byte GetSongSingerFile, byte GetSongSingerRBT, string DateFrom, string DateTo, ref int RowCount)
        {
            List<SongsView> RetVal = new List<SongsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetPageByKeyword");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@MusicTypeId", MusicTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@AlbumId", AlbumId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", TelcoId));
                cmd.Parameters.Add(new SqlParameter("@SingerId", SingerId));
                cmd.Parameters.Add(new SqlParameter("@AuthorId", AuthorId));
                cmd.Parameters.Add(new SqlParameter("@MusicContentTypeId", MusicContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@ShowVideo", ShowVideo));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@GetSongSinger", GetSongSinger));
                cmd.Parameters.Add(new SqlParameter("@GetSongSingerFile", GetSongSingerFile));
                cmd.Parameters.Add(new SqlParameter("@GetSongSingerRBT", GetSongSingerRBT));
                cmd.Parameters.Add(new SqlParameter("@SearchBy", SearchBy));
                cmd.Parameters.Add(new SqlParameter("@SongId", this.SongId));
                cmd.Parameters.Add(new SqlParameter("@SongName", this.SongName));
                cmd.Parameters.Add(new SqlParameter("@SongDesc", this.SongDesc));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    RetVal = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);
                    SongsView m_SongsView = new SongsView();
                    RetVal.Insert(0, m_SongsView);
                    //Get MusicType
                    //reader.NextResult();
                    //List<SongMusicTypesView> l_MusicTypes = SongMusicTypesView.Init(smartReader, true);

                    ////Get Authors
                    //reader.NextResult();
                    //List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);

                    ////Get Category
                    //reader.NextResult();
                    //List<SongSingerCategoriesView> l_Category = SongSingerCategoriesView.Init(smartReader, true);

                    List<SongSingerSingersView> l_Singers = null;
                    List<SongSingerSingersView> l_SingersForSong = null;
                    List<SongSingerAlbumsView> l_Albums = null;
                    List<SongSingerFilesView> l_Files = null;
                    List<SongSingerRBTsView> l_RBTs = null;
                    if (GetSongSinger == 1)
                    {
                        //Get Singers
                        reader.NextResult();
                        l_Singers = SongSingerSingersView.Init(smartReader, true);
                        //Get Albums
                        reader.NextResult();
                        l_Albums = SongSingerAlbumsView.Init(smartReader, true);
                        //Get file video
                        reader.NextResult();
                        l_Files = SongSingerFilesView.Init(smartReader);
                        //Get Singers for song
                        reader.NextResult();
                        l_SingersForSong = SongSingerSingersView.Init(smartReader, true);
                        //Get RBT 
                        reader.NextResult();
                        l_RBTs = SongSingerRBTsView.Init(smartReader, RetVal);
                    }

                    reader.Close();

                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        if (i == 0)
                        {
                            RetVal[i].lMusicTypes = new List<SongMusicTypesView>();// SongMusicTypesView.Static_GetList(RetVal[i].SongId, l_MusicTypes);
                            RetVal[i].lAuthors = new List<SongAuthorsView>();// SongAuthorsView.Static_GetList(RetVal[i].SongId, l_Authors);
                            RetVal[i].lCategory = new List<SongSingerCategoriesView>();// SongSingerCategoriesView.Static_GetListBySongSingerId(RetVal[i].SongSingerId, l_Category);
                            RetVal[i].lSingers = l_Singers;// SongSingerSingersView.Static_GetList(RetVal[i].SongSingerId, l_Singers);
                            RetVal[i].lAlbums = l_Albums;// SongSingerAlbumsView.Static_GetList(RetVal[i].SongSingerId, l_Albums);
                            RetVal[i].lFiles = l_Files;// SongSingerFilesView.Static_GetList(RetVal[i].SongSingerId, l_Files);
                            RetVal[i].lRBTs = l_RBTs;
                        }
                        else
                        {
                            RetVal[i].lMusicTypes = new List<SongMusicTypesView>();// SongMusicTypesView.Static_GetList(RetVal[i].SongId, l_MusicTypes);
                            RetVal[i].lAuthors = new List<SongAuthorsView>();// SongAuthorsView.Static_GetList(RetVal[i].SongId, l_Authors);
                            RetVal[i].lCategory = new List<SongSingerCategoriesView>();// SongSingerCategoriesView.Static_GetListBySongSingerId(RetVal[i].SongSingerId, l_Category);
                            RetVal[i].lSingers = SongSingerSingersView.Static_GetList(RetVal[i].SongSingerId, l_SingersForSong);
                            //RetVal[i].lAlbums =  SongSingerAlbumsView.Static_GetList(RetVal[i].SongSingerId, l_Albums);
                            RetVal[i].lFiles = SongSingerFilesView.Static_GetList(RetVal[i].SongSingerId, l_Files);
                        }
                    }
                }
                catch (SqlException err)
                {
                    throw new ApplicationException("Data error: " + err.Message);
                }
                finally
                {
                    db.closeConnection(con);
                }

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

