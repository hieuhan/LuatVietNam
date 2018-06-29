
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
    public class SongAlbumImports
    {
        private int _SongAlbumImportId;
        private string _AlbumName;
        private string _Singer;
        private string _MusicType;
        private string _FileSong;
        private string _FileInfo;
        private string _FileImage;
        private byte _ProcessStatus;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongAlbumImports()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongAlbumImports(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongAlbumImports()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongAlbumImportId
        {
            get { return _SongAlbumImportId; }
            set { _SongAlbumImportId = value; }
        }
        //-----------------------------------------------------------------
        public string AlbumName
        {
            get { return _AlbumName; }
            set { _AlbumName = value; }
        }
        //-----------------------------------------------------------------
        public string Singer
        {
            get { return _Singer; }
            set { _Singer = value; }
        }
        //-----------------------------------------------------------------
        public string MusicType
        {
            get { return _MusicType; }
            set { _MusicType = value; }
        }
        //-----------------------------------------------------------------
        public string FileSong
        {
            get { return _FileSong; }
            set { _FileSong = value; }
        }
        //-----------------------------------------------------------------
        public string FileInfo
        {
            get { return _FileInfo; }
            set { _FileInfo = value; }
        }
        //-----------------------------------------------------------------
        public string FileImage
        {
            get { return _FileImage; }
            set { _FileImage = value; }
        }
        //-----------------------------------------------------------------
        public byte ProcessStatus
        {
            get { return _ProcessStatus; }
            set { _ProcessStatus = value; }
        }
        //-----------------------------------------------------------------
        public int AlbumId { set; get; }
        //-----------------------------------------------------------------

        private List<SongAlbumImports> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongAlbumImports> l_SongAlbumImports = new List<SongAlbumImports>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongAlbumImports m_SongAlbumImports = new SongAlbumImports(db.ConnectionString);
                    m_SongAlbumImports.SongAlbumImportId = smartReader.GetInt32("SongAlbumImportId");
                    m_SongAlbumImports.AlbumName = smartReader.GetString("AlbumName");
                    m_SongAlbumImports.Singer = smartReader.GetString("Singer");
                    m_SongAlbumImports.MusicType = smartReader.GetString("MusicType");
                    m_SongAlbumImports.FileSong = smartReader.GetString("FileSong");
                    m_SongAlbumImports.FileInfo = smartReader.GetString("FileInfo");
                    m_SongAlbumImports.FileImage = smartReader.GetString("FileImage");
                    m_SongAlbumImports.ProcessStatus = smartReader.GetByte("ProcessStatus");
                    m_SongAlbumImports.AlbumId = smartReader.GetByte("AlbumId");

                    l_SongAlbumImports.Add(m_SongAlbumImports);
                }
                reader.Close();
                return l_SongAlbumImports;
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
        //--------------------------------------------------------------
        public byte UpdateProcessStatus()
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE SongAlbumImports SET ProcessStatus=" + ProcessStatus.ToString() + ",AlbumId=" + AlbumId.ToString() + " WHERE SongAlbumImportId=" + this.SongAlbumImportId);
                cmd.CommandType = CommandType.Text;
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<SongAlbumImports> GetListNotProcess(int RowAmout)
        {
            List<SongAlbumImports> RetVal = new List<SongAlbumImports>();
            try
            {
                string sql = "SELECT TOP(" +RowAmout.ToString() + ") * FROM SongAlbumImports WHERE ProcessStatus=0";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

