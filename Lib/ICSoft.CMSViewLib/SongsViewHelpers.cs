using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;
using sms.database;
using sms.utils;
namespace ICSoft.CMSViewLib
{
    public class SongsViewHelpers
    {
        public static SongsViewDetail View_GetSongDetail(int SongSingerId, int RowSongOther, int RowVideoOther, int RowAlbum)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            SongsViewDetail RetVal = new SongsViewDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetViewDetail");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@RowSongOther", RowSongOther));
                cmd.Parameters.Add(new SqlParameter("@RowVideoOther", RowVideoOther));
                cmd.Parameters.Add(new SqlParameter("@RowAlbum", RowAlbum));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Get singer info
                RetVal.lSinger = InitViewHelpers.InitArticle(smartReader, false);
                
                byte GetSongSinger = 1;
                byte GetSongSingerRBT = 0;
                //Get list song
                reader.NextResult();
                List<SongsView> l_SongsView = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);

                //Get Albums
                reader.NextResult();
                RetVal.lAlbum = InitViewHelpers.InitArticle(smartReader, false);

                //Get MusicType
                reader.NextResult();
                List<SongMusicTypesView> l_MusicTypes = SongMusicTypesView.Init(smartReader, true);
                //Get Authors
                reader.NextResult();
                List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);
                //Get Singers
                reader.NextResult();
                List<SongSingerSingersView> l_Singers = SongSingerSingersView.Init(smartReader, true);
                //Get Albums
                reader.NextResult();
                List<SongSingerAlbumsView> l_Albums = SongSingerAlbumsView.Init(smartReader, true);
                //Get File
                reader.NextResult();
                List<SongSingerFilesView> l_Files = SongSingerFilesView.Init(smartReader);
                //Get RBT
                reader.NextResult();
                List<SongSingerRBTsView> l_RBTs = SongSingerRBTsView.Init(smartReader, l_SongsView);
            
                reader.Close();

                for (int i = 0; i < l_SongsView.Count; i++)
                {
                    l_SongsView[i].lMusicTypes = SongMusicTypesView.Static_GetList(l_SongsView[i].SongId, l_MusicTypes);
                    l_SongsView[i].lAuthors = SongAuthorsView.Static_GetList(l_SongsView[i].SongId, l_Authors);
                    l_SongsView[i].lSingers = SongSingerSingersView.Static_GetList(l_SongsView[i].SongSingerId, l_Singers);
                    l_SongsView[i].lAlbums = SongSingerAlbumsView.Static_GetList(l_SongsView[i].SongSingerId, l_Albums);
                    l_SongsView[i].lFiles = SongSingerFilesView.Static_GetList(l_SongsView[i].SongSingerId, l_Files);
                    l_SongsView[i].lRBTs = SongSingerRBTsView.Static_GetList(l_SongsView[i].SongSingerId, l_RBTs);
                }

                RetVal.mSongsView = SongsView.Static_GetByViewType(1, l_SongsView);
                RetVal.lSongOther = SongsView.Static_GetListByViewType(2, l_SongsView);
                RetVal.lVideoOther = SongsView.Static_GetListByViewType(3, l_SongsView);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static SongsViewDetail View_GetSongDetailRBT(int SongSingerRBTId, string RBTCode, int RowSongOther, int RowVideoOther, int RowAlbum)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            SongsViewDetail RetVal = new SongsViewDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetViewDetailRBT");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", RBTCode));
                cmd.Parameters.Add(new SqlParameter("@RowSongOther", RowSongOther));
                cmd.Parameters.Add(new SqlParameter("@RowVideoOther", RowVideoOther));
                cmd.Parameters.Add(new SqlParameter("@RowAlbum", RowAlbum));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Get singer info
                RetVal.lSinger = InitViewHelpers.InitArticle(smartReader, false);

                byte GetSongSinger = 1;
                byte GetSongSingerRBT = 0;
                //Get list song
                reader.NextResult();
                List<SongsView> l_SongsView = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);

                //Get Albums
                reader.NextResult();
                RetVal.lAlbum = InitViewHelpers.InitArticle(smartReader, false);

                //Get MusicType
                reader.NextResult();
                List<SongMusicTypesView> l_MusicTypes = SongMusicTypesView.Init(smartReader, true);
                //Get Authors
                reader.NextResult();
                List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);
                //Get Singers
                reader.NextResult();
                List<SongSingerSingersView> l_Singers = SongSingerSingersView.Init(smartReader, true);
                //Get Albums
                reader.NextResult();
                List<SongSingerAlbumsView> l_Albums = SongSingerAlbumsView.Init(smartReader, true);
                //Get File
                reader.NextResult();
                List<SongSingerFilesView> l_Files = SongSingerFilesView.Init(smartReader);
                //Get RBT
                reader.NextResult();
                List<SongSingerRBTsView> l_RBTs = SongSingerRBTsView.Init(smartReader, l_SongsView);

                reader.Close();

                for (int i = 0; i < l_SongsView.Count; i++)
                {
                    l_SongsView[i].lMusicTypes = SongMusicTypesView.Static_GetList(l_SongsView[i].SongId, l_MusicTypes);
                    l_SongsView[i].lAuthors = SongAuthorsView.Static_GetList(l_SongsView[i].SongId, l_Authors);
                    l_SongsView[i].lSingers = SongSingerSingersView.Static_GetList(l_SongsView[i].SongSingerId, l_Singers);
                    l_SongsView[i].lAlbums = SongSingerAlbumsView.Static_GetList(l_SongsView[i].SongSingerId, l_Albums);
                    l_SongsView[i].lFiles = SongSingerFilesView.Static_GetList(l_SongsView[i].SongSingerId, l_Files);
                    l_SongsView[i].lRBTs = SongSingerRBTsView.Static_GetList(l_SongsView[i].SongSingerId, l_RBTs);
                }

                RetVal.mSongsView = SongsView.Static_GetByViewType(1, l_SongsView);
                RetVal.lSongOther = SongsView.Static_GetListByViewType(2, l_SongsView);
                RetVal.lVideoOther = SongsView.Static_GetListByViewType(3, l_SongsView);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------

        public static SongsViewSinger View_GetSongBySinger(int SingerId, string SingerName, int RowAmount, int RowAmountVideo, int RowAmountAlbum)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            SongsViewSinger RetVal = new SongsViewSinger();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetViewSinger");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SingerId", SingerId));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@RowAmountVideo", RowAmountVideo));
                cmd.Parameters.Add(new SqlParameter("@RowAmountAlbum", RowAmountAlbum));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Get singer info
                RetVal.mSinger = InitViewHelpers.InitArticleOne(smartReader);

                byte GetSongSinger = 1;
                byte GetSongSingerRBT = 0;
                //Get list song
                reader.NextResult();
                List<SongsView> l_SongsView = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);

                //Get Albums
                reader.NextResult();
                RetVal.lAlbum = InitViewHelpers.InitArticle(smartReader, false);

                //Get MusicType
                reader.NextResult();
                List<SongMusicTypesView> l_MusicTypes = SongMusicTypesView.Init(smartReader, true);
                //Get Authors
                reader.NextResult();
                List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);
                //Get Singers
                reader.NextResult();
                List<SongSingerSingersView> l_Singers = SongSingerSingersView.Init(smartReader, true);
                //Get Albums
                reader.NextResult();
                List<SongSingerAlbumsView> l_Albums = SongSingerAlbumsView.Init(smartReader, true);
                //Get File
                reader.NextResult();
                List<SongSingerFilesView> l_Files = SongSingerFilesView.Init(smartReader);
                //Get RBT
                reader.NextResult();
                List<SongSingerRBTsView> l_RBTs = SongSingerRBTsView.Init(smartReader, l_SongsView);

                reader.Close();

                for (int i = 0; i < l_SongsView.Count; i++)
                {
                    l_SongsView[i].lMusicTypes = SongMusicTypesView.Static_GetList(l_SongsView[i].SongId, l_MusicTypes);
                    l_SongsView[i].lAuthors = SongAuthorsView.Static_GetList(l_SongsView[i].SongId, l_Authors);
                    l_SongsView[i].lSingers = SongSingerSingersView.Static_GetList(l_SongsView[i].SongSingerId, l_Singers);
                    l_SongsView[i].lAlbums = SongSingerAlbumsView.Static_GetList(l_SongsView[i].SongSingerId, l_Albums);
                    l_SongsView[i].lFiles = SongSingerFilesView.Static_GetList(l_SongsView[i].SongSingerId, l_Files);
                    l_SongsView[i].lRBTs = SongSingerRBTsView.Static_GetList(l_SongsView[i].SongSingerId, l_RBTs);
                }

                RetVal.lSong = SongsView.Static_GetListByViewType(1, l_SongsView);
                RetVal.lVideo = SongsView.Static_GetListByViewType(2, l_SongsView);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static SongsViewAuthor View_GetSongByAuthor(int AuthorId, string AuthorName, int RowAmount, int RowAmountVideo, int RowAmountAlbum)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            SongsViewAuthor RetVal = new SongsViewAuthor();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetViewAuthor");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AuthorId", AuthorId));
                cmd.Parameters.Add(new SqlParameter("@AuthorName", AuthorName));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@RowAmountVideo", RowAmountVideo));
                cmd.Parameters.Add(new SqlParameter("@RowAmountAlbum", RowAmountAlbum));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Get singer info
                RetVal.mAuthor = InitViewHelpers.InitArticleOne(smartReader);

                byte GetSongSinger = 1;
                byte GetSongSingerRBT = 0;
                //Get list song
                reader.NextResult();
                List<SongsView> l_SongsView = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);

                //Get Albums
                reader.NextResult();
                RetVal.lAlbum = InitViewHelpers.InitArticle(smartReader, false);

                //Get MusicType
                reader.NextResult();
                List<SongMusicTypesView> l_MusicTypes = SongMusicTypesView.Init(smartReader, true);
                //Get Authors
                reader.NextResult();
                List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);
                //Get Singers
                reader.NextResult();
                List<SongSingerSingersView> l_Singers = SongSingerSingersView.Init(smartReader, true);
                //Get Albums
                reader.NextResult();
                List<SongSingerAlbumsView> l_Albums = SongSingerAlbumsView.Init(smartReader, true);
                //Get File
                reader.NextResult();
                List<SongSingerFilesView> l_Files = SongSingerFilesView.Init(smartReader);
                //Get RBT
                reader.NextResult();
                List<SongSingerRBTsView> l_RBTs = SongSingerRBTsView.Init(smartReader, l_SongsView);

                reader.Close();

                for (int i = 0; i < l_SongsView.Count; i++)
                {
                    l_SongsView[i].lMusicTypes = SongMusicTypesView.Static_GetList(l_SongsView[i].SongId, l_MusicTypes);
                    l_SongsView[i].lAuthors = SongAuthorsView.Static_GetList(l_SongsView[i].SongId, l_Authors);
                    l_SongsView[i].lSingers = SongSingerSingersView.Static_GetList(l_SongsView[i].SongSingerId, l_Singers);
                    l_SongsView[i].lAlbums = SongSingerAlbumsView.Static_GetList(l_SongsView[i].SongSingerId, l_Albums);
                    l_SongsView[i].lFiles = SongSingerFilesView.Static_GetList(l_SongsView[i].SongSingerId, l_Files);
                    l_SongsView[i].lRBTs = SongSingerRBTsView.Static_GetList(l_SongsView[i].SongSingerId, l_RBTs);
                }

                RetVal.lSong = SongsView.Static_GetListByViewType(1, l_SongsView);
                RetVal.lVideo = SongsView.Static_GetListByViewType(2, l_SongsView);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static SongsViewAlbum View_GetSongByAlbum(int AlbumId, string AlbumName, int RowAmount, int RowAmountVideo)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            SongsViewAlbum RetVal = new SongsViewAlbum();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetViewAlbum");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AlbumId", AlbumId));
                cmd.Parameters.Add(new SqlParameter("@AlbumName", AlbumName));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@RowAmountVideo", RowAmountVideo));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Get album info
                List<AlbumsView> l_AlbumsView = AlbumsView.InitView(smartReader, true);
                reader.NextResult();
                List<AlbumSingersView> l_AlbumSingersView = AlbumSingersView.Init(smartReader, true);
                reader.NextResult();
                List<ArticleCategoriesView> l_ArticleCategoriesView = ArticleCategoriesView.Init(smartReader, true);

                RetVal.mAlbum = AlbumsView.Static_GetByViewType(1, l_AlbumsView);
                RetVal.mAlbum.lSingers = AlbumSingersView.Static_GetList(RetVal.mAlbum.ArticleId, l_AlbumSingersView);
                RetVal.mAlbum.lMusicType = ArticleCategoriesView.Static_GetList(RetVal.mAlbum.ArticleId, l_ArticleCategoriesView);
                RetVal.lAlbum = AlbumsView.Static_GetListByViewType(2, l_AlbumsView); //Album Khac
                for (int i = 0; i < RetVal.lAlbum.Count; i++)
                {
                    RetVal.lAlbum[i].lSingers = AlbumSingersView.Static_GetList(RetVal.lAlbum[i].ArticleId, l_AlbumSingersView);
                    RetVal.lAlbum[i].lMusicType = ArticleCategoriesView.Static_GetList(RetVal.lAlbum[i].ArticleId, l_ArticleCategoriesView);
                }

                byte GetSongSinger = 1;
                byte GetSongSingerRBT = 0;
                //Get list song
                reader.NextResult();
                List<SongsView> l_SongsView = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);
                
                //Get MusicType
                reader.NextResult();
                List<SongMusicTypesView> l_MusicTypes = SongMusicTypesView.Init(smartReader, true);
                //Get Authors
                reader.NextResult();
                List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);
                //Get Singers
                reader.NextResult();
                List<SongSingerSingersView> l_Singers = SongSingerSingersView.Init(smartReader, true);
                //Get File
                reader.NextResult();
                List<SongSingerFilesView> l_Files = SongSingerFilesView.Init(smartReader);
                //Get RBT
                reader.NextResult();
                List<SongSingerRBTsView> l_RBTs = SongSingerRBTsView.Init(smartReader, l_SongsView);

                reader.Close();

                for (int i = 0; i < l_SongsView.Count; i++)
                {
                    l_SongsView[i].lMusicTypes = SongMusicTypesView.Static_GetList(l_SongsView[i].SongId, l_MusicTypes);
                    l_SongsView[i].lAuthors = SongAuthorsView.Static_GetList(l_SongsView[i].SongId, l_Authors);
                    l_SongsView[i].lSingers = SongSingerSingersView.Static_GetList(l_SongsView[i].SongSingerId, l_Singers);
                    l_SongsView[i].lFiles = SongSingerFilesView.Static_GetList(l_SongsView[i].SongSingerId, l_Files);
                    l_SongsView[i].lRBTs = SongSingerRBTsView.Static_GetList(l_SongsView[i].SongSingerId, l_RBTs);
                }

                RetVal.lSong = SongsView.Static_GetListByViewType(1, l_SongsView);
                RetVal.lVideo = SongsView.Static_GetListByViewType(2, l_SongsView);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static SongsViewAlbumRBT View_GetSongByAlbumRBT(int AlbumId, string AlbumName, int RowAmount, int RowAmountVideo)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            SongsViewAlbumRBT RetVal = new SongsViewAlbumRBT();
            try
            {
                SqlCommand cmd = new SqlCommand("Songs_GetViewAlbumRBT");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AlbumId", AlbumId));
                cmd.Parameters.Add(new SqlParameter("@AlbumName", AlbumName));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@RowAmountVideo", RowAmountVideo));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Get album info
                List<AlbumsView> l_AlbumsView = AlbumsView.InitView(smartReader, true);
                reader.NextResult();
                List<AlbumSingersView> l_AlbumSingersView = AlbumSingersView.Init(smartReader, true);
                reader.NextResult();
                List<ArticleCategoriesView> l_ArticleCategoriesView = ArticleCategoriesView.Init(smartReader, true);

                RetVal.mAlbum = AlbumsView.Static_GetByViewType(1, l_AlbumsView);
                RetVal.mAlbum.lSingers = AlbumSingersView.Static_GetList(RetVal.mAlbum.ArticleId, l_AlbumSingersView);
                RetVal.mAlbum.lMusicType = ArticleCategoriesView.Static_GetList(RetVal.mAlbum.ArticleId, l_ArticleCategoriesView);
                RetVal.lAlbum = AlbumsView.Static_GetListByViewType(2, l_AlbumsView); //Album Khac
                for (int i = 0; i < RetVal.lAlbum.Count; i++)
                {
                    RetVal.lAlbum[i].lSingers = AlbumSingersView.Static_GetList(RetVal.lAlbum[i].ArticleId, l_AlbumSingersView);
                    RetVal.lAlbum[i].lMusicType = ArticleCategoriesView.Static_GetList(RetVal.lAlbum[i].ArticleId, l_ArticleCategoriesView);
                }

                byte GetSongSinger = 1;
                byte GetSongSingerRBT = 2;
                //Get list song
                reader.NextResult();
                List<SongsView> l_SongsView = SongsView.Init(smartReader, GetSongSinger, GetSongSingerRBT);

                //Get MusicType
                reader.NextResult();
                List<SongMusicTypesView> l_MusicTypes = SongMusicTypesView.Init(smartReader, true);
                //Get Authors
                reader.NextResult();
                List<SongAuthorsView> l_Authors = SongAuthorsView.Init(smartReader, true);
                //Get Singers
                reader.NextResult();
                List<SongSingerSingersView> l_Singers = SongSingerSingersView.Init(smartReader, true);
                //Get File
                //reader.NextResult();
                //List<SongSingerFilesView> l_Files = SongSingerFilesView.Init(smartReader);
                ////Get RBT
                //reader.NextResult();
                //List<SongSingerRBTsView> l_RBTs = SongSingerRBTsView.Init(smartReader, l_SongsView);

                reader.Close();

                for (int i = 0; i < l_SongsView.Count; i++)
                {
                    l_SongsView[i].lMusicTypes = SongMusicTypesView.Static_GetList(l_SongsView[i].SongId, l_MusicTypes);
                    l_SongsView[i].lAuthors = SongAuthorsView.Static_GetList(l_SongsView[i].SongId, l_Authors);
                    l_SongsView[i].lSingers = SongSingerSingersView.Static_GetList(l_SongsView[i].SongSingerId, l_Singers);
                    //l_SongsView[i].lFiles = SongSingerFilesView.Static_GetList(l_SongsView[i].SongSingerId, l_Files);
                    //l_SongsView[i].lRBTs = SongSingerRBTsView.Static_GetList(l_SongsView[i].SongSingerId, l_RBTs);
                }

                RetVal.lSong = SongsView.Static_GetListByViewType(1, l_SongsView);
                RetVal.lVideo = SongsView.Static_GetListByViewType(2, l_SongsView);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_SearchRBT(int RowAmount, int PageIndex, string OrderBy, short MusicTypeId, short CategoryId, int AlbumId, int SingerId, int AuthorId, byte TelcoId, string SearchBy, string Keyword, ref int RowCount)
        {
            int ActUserId = 0;
            int SongSingerId = 0;
            short DataSourceId = 0;
            byte MusicContentTypeId = 0;
            byte GetSongSinger = 1;
            byte GetSongSingerFile = 1;
            byte GetSongSingerRBT = 2;
            byte ShowVideo = 0;
            string DateFrom = "";
            string DateTo = "";

            Songs m_Songs = new Songs();
            m_Songs.SongName = Keyword;
            m_Songs.ReviewStatusId = 2;
            return m_Songs.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SongSingerId, MusicTypeId, CategoryId, AlbumId, SingerId, AuthorId, DataSourceId, TelcoId, MusicContentTypeId, ShowVideo, SearchBy, GetSongSinger, GetSongSingerFile, GetSongSingerRBT, DateFrom, DateTo, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetRBTByCategoryPaging(short CategoryId, int RowAmount, string OrderBy, int PageIndex, ref int RowCount)
        {
            short MusicTypeId = 0;
            byte TelcoId = 0;
            int AlbumId=0;
            int SingerId=0;
            int AuthorId = 0;
            string SearchBy = "";
            string Keyword = "";

            return View_SearchRBT(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, AlbumId, SingerId,AuthorId, TelcoId, SearchBy, Keyword, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_Search(int RowAmount, int PageIndex, string OrderBy, short MusicTypeId, short CategoryId, byte TelcoId, byte ShowVideo, string SearchBy, string Keyword, ref int RowCount)
        {
            int ActUserId=0;
            int SongSingerId=0;
            int AlbumId = 0;
            int SingerId = 0;
            int AuthorId = 0;
            short DataSourceId=0;
            byte MusicContentTypeId=0;
            byte GetSongSinger=1;
            byte GetSongSingerFile=1;
            byte GetSongSingerRBT=0;
            string DateFrom="";
            string DateTo="";

            Songs m_Songs = new Songs();
            m_Songs.SongName = Keyword;
            m_Songs.ReviewStatusId = 2;
            return m_Songs.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SongSingerId, MusicTypeId, CategoryId, AlbumId, SingerId, AuthorId, DataSourceId, TelcoId, MusicContentTypeId, ShowVideo, SearchBy, GetSongSinger, GetSongSingerFile, GetSongSingerRBT, DateFrom, DateTo, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_Search(int RowAmount, int PageIndex, string OrderBy, short MusicTypeId, short CategoryId, int AlbumId,int SingerId,int AuthorId, byte TelcoId, byte ShowVideo, string SearchBy, string Keyword, ref int RowCount)
        {
            int ActUserId = 0;
            int SongSingerId = 0;
            short DataSourceId = 0;
            byte MusicContentTypeId = 0;
            byte GetSongSinger = 1;
            byte GetSongSingerFile = 1;
            byte GetSongSingerRBT = 0;
            string DateFrom = "";
            string DateTo = "";

            Songs m_Songs = new Songs();
            m_Songs.SongName = Keyword;
            m_Songs.ReviewStatusId = 2;
            return m_Songs.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SongSingerId, MusicTypeId, CategoryId, AlbumId, SingerId, AuthorId, DataSourceId, TelcoId, MusicContentTypeId, ShowVideo, SearchBy, GetSongSinger, GetSongSingerFile, GetSongSingerRBT, DateFrom, DateTo, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_SearchByKeyword(int RowAmount, int PageIndex, string OrderBy, short MusicTypeId, short CategoryId, byte TelcoId, byte ShowVideo, string SearchBy, string Keyword, ref int RowCount)
        {
            int ActUserId = 0;
            int SongSingerId = 0;
            int AlbumId = 0;
            int SingerId = 0;
            int AuthorId = 0;
            short DataSourceId = 0;
            byte MusicContentTypeId = 0;
            byte GetSongSinger = 1;
            byte GetSongSingerFile = 0;
            byte GetSongSingerRBT = 0;
            string DateFrom = "";
            string DateTo = "";

            Songs m_Songs = new Songs();
            m_Songs.SongName = Keyword;
            m_Songs.ReviewStatusId = 2;
            return m_Songs.GetPageByKeyword(ActUserId, RowAmount, PageIndex, OrderBy, SongSingerId, MusicTypeId, CategoryId, AlbumId, SingerId, AuthorId, DataSourceId, TelcoId, MusicContentTypeId, ShowVideo, SearchBy, GetSongSinger, GetSongSingerFile, GetSongSingerRBT, DateFrom, DateTo, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_SearchByKeywordRBT(int RowAmount, int PageIndex, string OrderBy, short MusicTypeId, short CategoryId, byte TelcoId, byte ShowVideo, string SearchBy, string Keyword, ref int RowCount)
        {
            int ActUserId = 0;
            int SongSingerId = 0;
            int AlbumId = 0;
            int SingerId = 0;
            int AuthorId = 0;
            short DataSourceId = 0;
            byte MusicContentTypeId = 0;
            byte GetSongSinger = 1;
            byte GetSongSingerFile = 0;
            byte GetSongSingerRBT = 2;
            string DateFrom = "";
            string DateTo = "";

            Songs m_Songs = new Songs();
            m_Songs.SongName = Keyword;
            m_Songs.ReviewStatusId = 2;
            return m_Songs.GetPageByKeyword(ActUserId, RowAmount, PageIndex, OrderBy, SongSingerId, MusicTypeId, CategoryId, AlbumId, SingerId, AuthorId, DataSourceId, TelcoId, MusicContentTypeId, ShowVideo, SearchBy, GetSongSinger, GetSongSingerFile, GetSongSingerRBT, DateFrom, DateTo, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetByCategory(short CategoryId, byte ShowVideo, int RowAmount, string OrderBy)
        {
            int PageIndex=0;
            short MusicTypeId = 0;
            byte TelcoId=0;
            string SearchBy = "";
            string Keyword="";
            int RowCount=0;

            return View_Search(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, TelcoId, ShowVideo, SearchBy, Keyword, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetByCategoryPaging(short CategoryId, byte ShowVideo, int RowAmount, string OrderBy, int PageIndex, ref int RowCount)
        {
            short MusicTypeId = 0;
            byte TelcoId = 0;
            string SearchBy = "";
            string Keyword = "";

            return View_Search(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, TelcoId, ShowVideo, SearchBy, Keyword, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> GetListRBTPromotion(int RowAmount, int PageIndex, ref int RowCount)
        {
            int ActUserId = 0;
            int SongSingerId = 0;
            int AlbumId = 0;
            int SingerId = 0;
            int AuthorId = 0;
            short DataSourceId = 0;
            byte MusicContentTypeId = 3; //Nhac cho khuyen mai tinh theo thoi gian
            byte GetSongSinger = 1;
            byte GetSongSingerFile = 1;
            byte GetSongSingerRBT = 2;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            short MusicTypeId = 0;
            short CategoryId = 0;
            byte TelcoId = 0;
            byte ShowVideo = 0;
            string SearchBy = "SongName";

            Songs m_Songs = new Songs();
            m_Songs.SongName = "";
            m_Songs.ReviewStatusId = 2;
            return m_Songs.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SongSingerId, MusicTypeId, CategoryId, AlbumId, SingerId, AuthorId, DataSourceId, TelcoId, MusicContentTypeId, ShowVideo, SearchBy, GetSongSinger, GetSongSingerFile, GetSongSingerRBT, DateFrom, DateTo, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongSingerRBTsView> GetListRBTBySongSingerId(int SongSingerId, string OrderBy)
        {
            if (string.IsNullOrEmpty(OrderBy)) OrderBy = "SongSingerRBTId DESC";
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<SongSingerRBTsView> RetVal = new List<SongSingerRBTsView>();
            try
            {
                string sql = "SELECT * FROM SongSingerRBTs WHERE SongSingerId=" + SongSingerId.ToString() + " AND ReviewStatusId=2 ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = SongSingerRBTsView.Init(smartReader);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static SongSingerRBTsView GetSongSingerRBTById(int SongSingerRBTId)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            SongSingerRBTsView RetVal = new SongSingerRBTsView();
            try
            {
                string sql = "SELECT * FROM SongSingerRBTs WHERE SongSingerRBTId=" + SongSingerRBTId.ToString();
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                List <SongSingerRBTsView> list = SongSingerRBTsView.Init(smartReader);
                if (list != null && list.Count > 0) RetVal = list[0];
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetByMusicType(short MusicTypeId, byte ShowVideo, int RowAmount)
        {
            int PageIndex = 0;
            string OrderBy = "";
            short CategoryId = 0;
            byte TelcoId = 0;
            string SearchBy = "";
            string Keyword = "";
            int RowCount = 0;

            return View_Search(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, TelcoId, ShowVideo, SearchBy, Keyword, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetByMusicTypePaging(short MusicTypeId, byte ShowVideo, int RowAmount, int PageIndex, ref int RowCount)
        {
            string OrderBy = "";
            short CategoryId = 0;
            byte TelcoId = 0;
            string SearchBy = "";
            string Keyword = "";

            return View_Search(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, TelcoId, ShowVideo, SearchBy, Keyword, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetSongNewest(int RowAmount, byte ShowVideo)
        {
            int PageIndex = 0;
            string OrderBy="";
            short MusicTypeId = 0;
            short CategoryId = 0;
            byte TelcoId = 0;
            string SearchBy = "";
            string Keyword = "";
            int RowCount = 0;

            return View_Search(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, TelcoId, ShowVideo, SearchBy, Keyword, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetSongNewestPaging(byte ShowVideo, int RowAmount, int PageIndex, ref int RowCount)
        {
            string OrderBy = "";
            short MusicTypeId = 0;
            short CategoryId = 0;
            byte TelcoId = 0;
            string SearchBy = "";
            string Keyword = "";

            return View_Search(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, TelcoId, ShowVideo, SearchBy, Keyword, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetByAlbumPaging(int AlbumId, int RowAmount, int PageIndex, ref int RowCount)
        {
            short MusicTypeId = 0;
            byte TelcoId = 0;
            string SearchBy = "";
            string Keyword = "";
            string OrderBy = "";
            short CategoryId = 0;
            byte ShowVideo = 0;
            int SingerId = 0;
            int AuthorId = 0;

            return View_Search(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, AlbumId, SingerId, AuthorId, TelcoId, ShowVideo, SearchBy, Keyword, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetByAuthorPaging(int AuthorId, int RowAmount, int PageIndex, ref int RowCount)
        {
            short MusicTypeId = 0;
            byte TelcoId = 0;
            string SearchBy = "";
            string Keyword = "";
            string OrderBy = "";
            short CategoryId = 0;
            byte ShowVideo = 0;
            int SingerId = 0;
            int AlbumId = 0;

            return View_Search(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, AlbumId, SingerId, AuthorId, TelcoId, ShowVideo, SearchBy, Keyword, ref RowCount);
        }
        //-----------------------------------------------------------
        public static List<SongsView> View_GetBySingerPaging(int SingerId, byte ShowVideo, int RowAmount, int PageIndex, ref int RowCount)
        {
            short MusicTypeId = 0;
            byte TelcoId = 0;
            string SearchBy = "";
            string Keyword = "";
            string OrderBy = "";
            short CategoryId = 0;
            int AuthorId = 0;
            int AlbumId = 0;

            return View_Search(RowAmount, PageIndex, OrderBy, MusicTypeId, CategoryId, AlbumId, SingerId, AuthorId, TelcoId, ShowVideo, SearchBy, Keyword, ref RowCount);
        }
    }

}//end namespace service
