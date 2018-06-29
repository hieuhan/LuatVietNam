
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
    public class SongSingerRBTs
    {
        private int _SongSingerRBTId;
        private int _SongSingerId;
        private byte _TelcoId;
        private string _RBTName;
        private string _RBTCode;
        private int _Price;
        private int _PromotionPrice;
        private DateTime _ExpireDateTime;
        private string _FilePath="";
        private byte _MusicContentTypeId;
        private short _DataSourceId;
        private byte _ReviewStatusId;
        private int _ViewCount;
        private int _DownloadCount;
        private int _ShareCount;
        private int _LikeCount;
        private int _CrUserId;
        public DateTime PromotionEndDate { get; set; }
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongSingerRBTs()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongSingerRBTs(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongSingerRBTs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerRBTId
        {
            get { return _SongSingerRBTId; }
            set { _SongSingerRBTId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public byte TelcoId
        {
            get { return _TelcoId; }
            set { _TelcoId = value; }
        }
        //-----------------------------------------------------------------
        public string RBTName
        {
            get { return _RBTName; }
            set { _RBTName = value; }
        }
        //-----------------------------------------------------------------
        public string RBTCode
        {
            get { return _RBTCode; }
            set { _RBTCode = value; }
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
        public DateTime ExpireDateTime
        {
            get { return _ExpireDateTime; }
            set { _ExpireDateTime = value; }
        }
        //-----------------------------------------------------------------
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        //-----------------------------------------------------------------
        public byte MusicContentTypeId
        {
            get { return _MusicContentTypeId; }
            set { _MusicContentTypeId = value; }
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

        private List<SongSingerRBTs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongSingerRBTs> l_SongSingerRBTs = new List<SongSingerRBTs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongSingerRBTs m_SongSingerRBTs = new SongSingerRBTs(db.ConnectionString);
                    m_SongSingerRBTs.SongSingerRBTId = smartReader.GetInt32("SongSingerRBTId");
                    m_SongSingerRBTs.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerRBTs.TelcoId = smartReader.GetByte("TelcoId");
                    m_SongSingerRBTs.RBTName = smartReader.GetString("RBTName");
                    m_SongSingerRBTs.RBTCode = smartReader.GetString("RBTCode");
                    m_SongSingerRBTs.Price = smartReader.GetInt32("Price");
                    m_SongSingerRBTs.PromotionPrice = smartReader.GetInt32("PromotionPrice");
                    m_SongSingerRBTs.PromotionEndDate = smartReader.GetDateTime("PromotionEndDate");
                    m_SongSingerRBTs.ExpireDateTime = smartReader.GetDateTime("ExpireDateTime");
                    m_SongSingerRBTs.FilePath = smartReader.GetString("FilePath");
                    m_SongSingerRBTs.MusicContentTypeId = smartReader.GetByte("MusicContentTypeId");
                    m_SongSingerRBTs.DataSourceId = smartReader.GetInt16("DataSourceId");
                    m_SongSingerRBTs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_SongSingerRBTs.ViewCount = smartReader.GetInt32("ViewCount");
                    m_SongSingerRBTs.DownloadCount = smartReader.GetInt32("DownloadCount");
                    m_SongSingerRBTs.ShareCount = smartReader.GetInt32("ShareCount");
                    m_SongSingerRBTs.LikeCount = smartReader.GetInt32("LikeCount");
                    m_SongSingerRBTs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_SongSingerRBTs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_SongSingerRBTs.Add(m_SongSingerRBTs);
                }
                reader.Close();
                return l_SongSingerRBTs;
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
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RBTName", this.RBTName));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@PromotionPrice", this.PromotionPrice));
                if (this.ExpireDateTime != DateTime.MinValue)  cmd.Parameters.Add(new SqlParameter("@ExpireDateTime", this.ExpireDateTime));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@MusicContentTypeId", this.MusicContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ViewCount", this.ViewCount));
                cmd.Parameters.Add(new SqlParameter("@DownloadCount", this.DownloadCount));
                cmd.Parameters.Add(new SqlParameter("@ShareCount", this.ShareCount));
                cmd.Parameters.Add(new SqlParameter("@LikeCount", this.LikeCount));
                cmd.Parameters.Add("@SongSingerRBTId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerRBTId = (cmd.Parameters["@SongSingerRBTId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerRBTId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RBTName", this.RBTName));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@PromotionPrice", this.PromotionPrice));
                if (this.ExpireDateTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@ExpireDateTime", this.ExpireDateTime));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@MusicContentTypeId", this.MusicContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ViewCount", this.ViewCount));
                cmd.Parameters.Add(new SqlParameter("@DownloadCount", this.DownloadCount));
                cmd.Parameters.Add(new SqlParameter("@ShareCount", this.ShareCount));
                cmd.Parameters.Add(new SqlParameter("@LikeCount", this.LikeCount));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
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
       public void UpdateFilePath(byte Replicated, int ActUserId, int FunringId, byte HasFile)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_UpdateFilePath");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@FunringId", FunringId));
                cmd.Parameters.Add(new SqlParameter("@HasFile", HasFile));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       //--------------------------------------------------------------
       public void UpdateFilePathMp3(byte Replicated, int ActUserId)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("SongSingerRBTs_UpdateFilePathMp3");
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
               cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
               cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
               cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
               db.ExecuteSQL(cmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       //--------------------------------------------------------------
       public void UpdateSongSingerId(byte Replicated, int ActUserId)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("SongSingerRBTs_UpdateSongSingerId");
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
               cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
               cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
               cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
               db.ExecuteSQL(cmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RBTName", this.RBTName));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
                cmd.Parameters.Add(new SqlParameter("@Price", this.Price));
                cmd.Parameters.Add(new SqlParameter("@PromotionPrice", this.PromotionPrice));
                if (this.ExpireDateTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@ExpireDateTime", this.ExpireDateTime));
                cmd.Parameters.Add(new SqlParameter("@FilePath", this.FilePath));
                cmd.Parameters.Add(new SqlParameter("@MusicContentTypeId", this.MusicContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", this.DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ViewCount", this.ViewCount));
                cmd.Parameters.Add(new SqlParameter("@DownloadCount", this.DownloadCount));
                cmd.Parameters.Add(new SqlParameter("@ShareCount", this.ShareCount));
                cmd.Parameters.Add(new SqlParameter("@LikeCount", this.LikeCount));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerRBTId = (cmd.Parameters["@SongSingerRBTId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerRBTId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
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
        public byte UpdateReviewStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
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
        public byte UpdatePromotion(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_UpdatePromotion_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@PromotionPrice", this.PromotionPrice));
                if (PromotionEndDate != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@PromotionEndDate", this.PromotionEndDate));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte UpdatePriority(byte Replicated, int ActUserId, int Priority, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_UpdatePriority_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerRBTId", this.SongSingerRBTId));
                cmd.Parameters.Add(new SqlParameter("@Priority", Priority));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<SongSingerRBTs> GetListConvertToMp3(int RowAmount)
        {
            List<SongSingerRBTs> RetVal = new List<SongSingerRBTs>();
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_GetConvertToMp3");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataTable GetFilesBySongSingerId(int SongSingerId)
        {
            DataTable RetVal = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerRBTs_GetFiles");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId));
                RetVal = db.getDataTable(cmd);
                RetVal.Columns.Add(new DataColumn("FileName"));
                for (int i = 0; i < RetVal.Rows.Count; i++)
                {
                    RetVal.Rows[i].BeginEdit();
                    RetVal.Rows[i]["FileName"] = RetVal.Rows[i][0].ToString().Substring(RetVal.Rows[i][0].ToString().LastIndexOf("/") + 1);
                    RetVal.Rows[i].EndEdit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<SongSingerRBTs> GetList(int SongSingerId)
        {
            List<SongSingerRBTs> RetVal = new List<SongSingerRBTs>();
            try
            {
                string sql = "SELECT * FROM SongSingerRBTs WHERE SongSingerId=" + SongSingerId.ToString() + " ORDER BY SongSingerRBTId DESC";
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
        public List<SongSingerRBTs> GetListOrderByPriority(int SongSingerId)
        {
            List<SongSingerRBTs> RetVal = new List<SongSingerRBTs>();
            try
            {
                string sql = "SELECT * FROM SongSingerRBTs WHERE SongSingerId=" + SongSingerId.ToString() + " ORDER BY Priority, SongSingerRBTId DESC";
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
        public List<SongSingerRBTs> GetListBySongSingerRBTId(int SongSingerRBTId)
        {
            List<SongSingerRBTs> RetVal = new List<SongSingerRBTs>();
            try
            {
                if (SongSingerRBTId > 0)
                {
                    string sql = "SELECT * FROM SongSingerRBTs WHERE (SongSingerRBTId=" + SongSingerRBTId.ToString() + ")";
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
        public SongSingerRBTs GetByRBTCode(string RBTCode)
        {
            SongSingerRBTs RetVal = new SongSingerRBTs();
            try
            {
                if (!String.IsNullOrEmpty(RBTCode))
                {
                    string sql = "SELECT * FROM SongSingerRBTs WHERE (RBTCode='" + RBTCode + "')";
                    SqlCommand cmd = new SqlCommand(sql);
                    List<SongSingerRBTs> l_RBT = Init(cmd);
                    if (l_RBT.Count > 0)
                        RetVal = l_RBT[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SongSingerRBTs Get(int SongSingerRBTId)
        {
            SongSingerRBTs RetVal = new SongSingerRBTs();
            try
            {
                List<SongSingerRBTs> list = GetListBySongSingerRBTId(SongSingerRBTId);
                if (list.Count > 0)
                {
                    RetVal = (SongSingerRBTs)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static SongSingerRBTs Static_Get(int SongSingerRBTId, List<SongSingerRBTs> list)
        {
            SongSingerRBTs RetVal = new SongSingerRBTs();
            try
            {
                RetVal = list.Find(i => i.SongSingerRBTId == SongSingerRBTId);
                if (RetVal == null) RetVal = new SongSingerRBTs();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SongSingerRBTs Get()
        {
            return Get(this.SongSingerRBTId);
        }
    }
}

