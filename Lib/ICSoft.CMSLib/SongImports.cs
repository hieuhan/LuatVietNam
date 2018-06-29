
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
    public class SongImports
    {
        private int _SongImportId;
        private string _SongName;
        private string _Singer;
        private string _Author;
        private string _MusicType;
        private string _FileMp3;
        private string _FileLyric;
        private byte _ProcessStatus;
        private int _SongSingerId;
        public string FileMp4 { get; set; }
        public string FileImage { get; set; }
        public string DataType { get; set; }
        public string TelcoName { get; set; }
        public string RBTCode { get; set; }
        public string CPName { get; set; }
        public int Price { get; set; }
        public DateTime ExpireTime { get; set; }

        private DBAccess db;
        //-----------------------------------------------------------------
        public SongImports()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongImports(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongImports()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongImportId
        {
            get { return _SongImportId; }
            set { _SongImportId = value; }
        }
        //-----------------------------------------------------------------
        public string SongName
        {
            get { return _SongName; }
            set { _SongName = value; }
        }
        //-----------------------------------------------------------------
        public string Singer
        {
            get { return _Singer; }
            set { _Singer = value; }
        }
        //-----------------------------------------------------------------
        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }
        //-----------------------------------------------------------------
        public string MusicType
        {
            get { return _MusicType; }
            set { _MusicType = value; }
        }
        //-----------------------------------------------------------------
        public string FileMp3
        {
            get { return _FileMp3; }
            set { _FileMp3 = value; }
        }
        //-----------------------------------------------------------------
        public string FileLyric
        {
            get { return _FileLyric; }
            set { _FileLyric = value; }
        }
        //-----------------------------------------------------------------
        public byte ProcessStatus
        {
            get { return _ProcessStatus; }
            set { _ProcessStatus = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------

        private List<SongImports> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongImports> l_SongImports = new List<SongImports>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongImports m_SongImports = new SongImports(db.ConnectionString);
                    m_SongImports.SongImportId = smartReader.GetInt32("SongImportId");
                    m_SongImports.SongName = smartReader.GetString("SongName");
                    m_SongImports.Singer = smartReader.GetString("Singer");
                    m_SongImports.Author = smartReader.GetString("Author");
                    m_SongImports.MusicType = smartReader.GetString("MusicType");
                    m_SongImports.FileMp3 = smartReader.GetString("FileMp3");
                    m_SongImports.FileMp4 = smartReader.GetString("FileMp4");
                    m_SongImports.FileImage = smartReader.GetString("FileImage");
                    m_SongImports.FileLyric = smartReader.GetString("FileLyric");
                    m_SongImports.ProcessStatus = smartReader.GetByte("ProcessStatus");
                    m_SongImports.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongImports.DataType = smartReader.GetString("DataType");
                    m_SongImports.TelcoName = smartReader.GetString("TelcoName");
                    m_SongImports.CPName = smartReader.GetString("CPName");
                    m_SongImports.Price = smartReader.GetInt32("Price");
                    m_SongImports.ExpireTime = smartReader.GetDateTime("ExpireTime");

                    l_SongImports.Add(m_SongImports);
                }
                reader.Close();
                return l_SongImports;
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
                SqlCommand cmd = new SqlCommand("UPDATE SongImports SET ProcessStatus=" + ProcessStatus.ToString() + ",SongSingerId=" + SongSingerId.ToString()+ " WHERE SongImportId=" + this.SongImportId);
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
        public byte DeleteDataProcessed()
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE SongImports WHERE ProcessStatus>0");
                cmd.CommandType = CommandType.Text;
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public List<SongImports> GetListNotProcess(int RowAmount)
        {
            List<SongImports> RetVal = new List<SongImports>();
            try
            {
                string sql = "SELECT TOP(" + RowAmount.ToString() + ") * FROM SongImports WHERE ProcessStatus=0";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public List<SongImports> GetListByProcessStatus(int RowAmount, byte ProcessStatus)
        {
            List<SongImports> RetVal = new List<SongImports>();
            try
            {
                string sql = "SELECT TOP(" + RowAmount.ToString() + ") * FROM SongImports WHERE ProcessStatus=" + ProcessStatus.ToString();
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
        public List<SongImports> GetListOrderBy(string OrderBy)
        {
            List<SongImports> RetVal = new List<SongImports>();
            try
            {
                string sql = "SELECT * FROM SongImports ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<SongImports> GetListBySongImportId(int SongImportId)
        {
            List<SongImports> RetVal = new List<SongImports>();
            try
            {
                if (SongImportId > 0)
                {
                    string sql = "SELECT * FROM SongImports WHERE (SongImportId=" + SongImportId.ToString() + ")";
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
        public SongImports Get(int SongImportId)
        {
            SongImports RetVal = new SongImports();
            try
            {
                List<SongImports> list = GetListBySongImportId(SongImportId);
                if (list.Count > 0)
                {
                    RetVal = (SongImports)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static SongImports Static_Get(int SongImportId, List<SongImports> list)
        {
            SongImports RetVal = new SongImports();
            try
            {
                RetVal = list.Find(i => i.SongImportId == SongImportId);
                if (RetVal == null) RetVal = new SongImports();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SongImports Get()
        {
            return Get(this.SongImportId);
        }
        //-------------------------------------------------------------- 
        public static SongImports Static_Get(int SongImportId)
        {
            return Static_Get(SongImportId);
        }
    }
}

