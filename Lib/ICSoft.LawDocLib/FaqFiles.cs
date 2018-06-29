
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
    /// <summary>
    /// class FaqFiles
    /// </summary>
    public class FaqFiles
    {
        private int _FaqFileId;
        private int _FaqId;
        private string _FileName;
        private string _FilePath;
        private int _FileSize;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public FaqFiles()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public FaqFiles(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FaqFiles()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int FaqFileId
        {
            get { return _FaqFileId; }
            set { _FaqFileId = value; }
        }
        //-----------------------------------------------------------------
        public int FaqId
        {
            get { return _FaqId; }
            set { _FaqId = value; }
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

        private List<FaqFiles> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FaqFiles> l_FaqFiles = new List<FaqFiles>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FaqFiles m_FaqFiles = new FaqFiles(db.ConnectionString);
                    m_FaqFiles.FaqFileId = smartReader.GetInt32("FaqFileId");
                    m_FaqFiles.FaqId = smartReader.GetInt32("FaqId");
                    m_FaqFiles.FileName = smartReader.GetString("FileName");
                    m_FaqFiles.FilePath = smartReader.GetString("FilePath");
                    m_FaqFiles.FileSize = smartReader.GetInt32("FileSize");
                    m_FaqFiles.CrUserId = smartReader.GetInt32("CrUserId");
                    m_FaqFiles.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_FaqFiles.Add(m_FaqFiles);
                }
                reader.Close();
                return l_FaqFiles;
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
                SqlCommand cmd = new SqlCommand("FaqFiles_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqId", this.FaqId));
                cmd.Parameters.Add(new SqlParameter("@FileName", this.FileName));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@FileSize", this.FileSize));
                cmd.Parameters.Add(new SqlParameter("@FaqFileId", this.FaqFileId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FaqFileId = Convert.ToInt32((cmd.Parameters["@FaqFileId"].Value == null) ? 0 : (cmd.Parameters["@FaqFileId"].Value));
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
                SqlCommand cmd = new SqlCommand("FaqFiles_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqFileId", this.FaqFileId));
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
        public List<FaqFiles> GetList()
        {
            List<FaqFiles> RetVal = new List<FaqFiles>();
            try
            {
                string sql = "SELECT * FROM V$FaqFiles";
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
        public static List<FaqFiles> Static_GetList(string ConStr)
        {
            List<FaqFiles> RetVal = new List<FaqFiles>();
            try
            {
                FaqFiles m_FaqFiles = new FaqFiles(ConStr);
                RetVal = m_FaqFiles.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<FaqFiles> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<FaqFiles> GetListByFaqFileId(int FaqFileId)
        {
            List<FaqFiles> RetVal = new List<FaqFiles>();
            try
            {
                if (FaqFileId > 0)
                {
                    string sql = "SELECT * FROM V$FaqFiles WHERE (FaqFileId=" + FaqFileId.ToString() + ")";
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
        public FaqFiles Get(int FaqFileId)
        {
            FaqFiles RetVal = new FaqFiles(db.ConnectionString);
            try
            {
                List<FaqFiles> list = GetListByFaqFileId(FaqFileId);
                if (list.Count > 0)
                {
                    RetVal = (FaqFiles)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public FaqFiles Get()
        {
            return Get(this.FaqFileId);
        }
        //-------------------------------------------------------------- 
        public static FaqFiles Static_Get(int FaqFileId)
        {
            return Static_Get(FaqFileId);
        }
        //--------------------------------------------------------------     
        public List<FaqFiles> FaqFiles_GetList(int ActUserId, string OrderBy, int FaqId)
        {
            List<FaqFiles> RetVal = new List<FaqFiles>();
            try
            {
                SqlCommand cmd = new SqlCommand("FaqFiles_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@FaqId", FaqId));
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