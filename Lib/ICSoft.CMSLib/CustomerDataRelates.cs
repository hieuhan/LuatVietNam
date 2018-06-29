
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSViewLib;

namespace ICSoft.CMSLib
{
    public class CustomerDataRelates
    {
        private int _CustomerDataRelateId;
        private int _CustomerId;
        private int _DataId;
        private byte _DataTypeId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CustomerDataRelates()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerDataRelates(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CustomerDataRelates()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CustomerDataRelateId
        {
            get { return _CustomerDataRelateId; }
            set { _CustomerDataRelateId = value; }
        }
        //-----------------------------------------------------------------
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        //-----------------------------------------------------------------
        public int DataId
        {
            get { return _DataId; }
            set { _DataId = value; }
        }
        //-----------------------------------------------------------------
        public byte DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        private List<CustomerDataRelates> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerDataRelates> l_CustomerDataRelates = new List<CustomerDataRelates>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerDataRelates m_CustomerDataRelates = new CustomerDataRelates(db.ConnectionString);
                    m_CustomerDataRelates.CustomerDataRelateId = smartReader.GetInt32("CustomerDataRelateId");
                    m_CustomerDataRelates.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerDataRelates.DataId = smartReader.GetInt32("DataId");
                    m_CustomerDataRelates.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_CustomerDataRelates.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_CustomerDataRelates.Add(m_CustomerDataRelates);
                }
                reader.Close();
                return l_CustomerDataRelates;
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
                SqlCommand cmd = new SqlCommand("CustomerDataRelates_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DataId", this.DataId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add("@CustomerDataRelateId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerDataRelateId = (cmd.Parameters["@CustomerDataRelateId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@CustomerDataRelateId"].Value);
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
                SqlCommand cmd = new SqlCommand("CustomerDataRelates_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DataId", this.DataId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
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
        public List<SongsView> GetListSongByCustomerId(int CustomerId)
        {
            List<SongsView> RetVal = new List<SongsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetListByCustomerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    byte GetSongSinger = 1;
                    byte GetSongSingerRBT = 0;
                    RetVal = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);

                    //Get Authors
                    reader.NextResult();
                    List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);

                    List<SongSingerSingersView> l_Singers = null;
                    List<SongSingerFilesView> l_Files = null;
                    List<SongSingerRBTsView> l_RBTs = null;
                    if (GetSongSinger == 1)
                    {
                        //Get Singers
                        reader.NextResult();
                        l_Singers = SongSingerSingersView.Init(smartReader, true);
                        //Get Files
                        reader.NextResult();
                        l_Files = SongSingerFilesView.Init(smartReader);
                        //Get RBT
                        reader.NextResult();
                        l_RBTs = SongSingerRBTsView.Init(smartReader);
                    }

                    reader.Close();

                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].lAuthors = SongAuthorsView.Static_GetList(RetVal[i].SongId, l_Authors);
                        RetVal[i].lSingers = SongSingerSingersView.Static_GetList(RetVal[i].SongSingerId, l_Singers);
                        RetVal[i].lFiles = SongSingerFilesView.Static_GetList(RetVal[i].SongSingerId, l_Files);
                        RetVal[i].lRBTs = SongSingerRBTsView.Static_GetList(RetVal[i].SongSingerId, l_RBTs);
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<SongsView> GetListVideoByCustomerId(int CustomerId)
        {
            List<SongsView> RetVal = new List<SongsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetListVideoByCustomerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    byte GetSongSinger = 1;
                    byte GetSongSingerRBT = 0;
                    RetVal = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);

                    //Get Authors
                    reader.NextResult();
                    List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);

                    List<SongSingerSingersView> l_Singers = null;
                    List<SongSingerFilesView> l_Files = null;
                    List<SongSingerRBTsView> l_RBTs = null;
                    if (GetSongSinger == 1)
                    {
                        //Get Singers
                        reader.NextResult();
                        l_Singers = SongSingerSingersView.Init(smartReader, true);
                        //Get Files
                        reader.NextResult();
                        l_Files = SongSingerFilesView.Init(smartReader);
                        //Get RBT
                        reader.NextResult();
                        l_RBTs = SongSingerRBTsView.Init(smartReader);
                    }

                    reader.Close();

                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].lAuthors = SongAuthorsView.Static_GetList(RetVal[i].SongId, l_Authors);
                        RetVal[i].lSingers = SongSingerSingersView.Static_GetList(RetVal[i].SongSingerId, l_Singers);
                        RetVal[i].lFiles = SongSingerFilesView.Static_GetList(RetVal[i].SongSingerId, l_Files);
                        RetVal[i].lRBTs = SongSingerRBTsView.Static_GetList(RetVal[i].SongSingerId, l_RBTs);
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<SongsView> GetListRBTByCustomerId(int CustomerId)
        {
            List<SongsView> RetVal = new List<SongsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetListRBTByCustomerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    byte GetSongSinger = 1;
                    byte GetSongSingerRBT = 2;
                    RetVal = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);

                    //Get Authors
                    reader.NextResult();
                    List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);

                    List<SongSingerSingersView> l_Singers = null;
                    List<SongSingerFilesView> l_Files = null;
                    //List<SongSingerRBTsView> l_RBTs = null;
                    if (GetSongSinger == 1)
                    {
                        //Get Singers
                        reader.NextResult();
                        l_Singers = SongSingerSingersView.Init(smartReader, true);
                        //Get Files
                        reader.NextResult();
                        l_Files = SongSingerFilesView.Init(smartReader);
                    }

                    reader.Close();

                    for (int i = 0; i < RetVal.Count; i++)
                    {
                        RetVal[i].lAuthors = SongAuthorsView.Static_GetList(RetVal[i].SongId, l_Authors);
                        RetVal[i].lSingers = SongSingerSingersView.Static_GetList(RetVal[i].SongSingerId, l_Singers);
                        RetVal[i].lFiles = SongSingerFilesView.Static_GetList(RetVal[i].SongSingerId, l_Files);
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<AlbumsView> GetListAlbumByCustomerId(int CustomerId)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<AlbumsView> RetVal = new List<AlbumsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Albums_GetListByCustomerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = AlbumsView.InitView(smartReader);
                //Get Singers
                reader.NextResult();
                List<AlbumSingersView> l_Singers = AlbumSingersView.Init(smartReader, true);

                reader.Close();

                for (int i = 0; i < RetVal.Count; i++)
                {
                    RetVal[i].lSingers = AlbumSingersView.Static_GetList(RetVal[i].ArticleId, l_Singers);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
    }
}

