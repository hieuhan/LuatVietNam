
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
    public class AdvertPositions
    {
        private int _AdvertPositionId;
        private byte _ApplicationTypeId;
        private short _SiteId;
        private short _CategoryId;
        private string _PositionName;
        private string _PositionDesc;
        private string _Width;
        private string _Height;
        private string _OverflowWidth;
        private string _OverflowHeight;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private byte _AdvertDisplayTypeId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public AdvertPositions()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public AdvertPositions(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~AdvertPositions()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int AdvertPositionId
        {
            get { return _AdvertPositionId; }
            set { _AdvertPositionId = value; }
        }
        //-----------------------------------------------------------------
        public byte ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }  
        //-----------------------------------------------------------------
        public string PositionName
        {
            get { return _PositionName; }
            set { _PositionName = value; }
        }
        //-----------------------------------------------------------------
        public string PositionDesc
        {
            get { return _PositionDesc; }
            set { _PositionDesc = value; }
        }
        //-----------------------------------------------------------------
        public string Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        //-----------------------------------------------------------------
        public string Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        //-----------------------------------------------------------------
        public string OverflowWidth
        {
            get { return _OverflowWidth; }
            set { _OverflowWidth = value; }
        }
        //-----------------------------------------------------------------
        public string OverflowHeight
        {
            get { return _OverflowHeight; }
            set { _OverflowHeight = value; }
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

        public byte AdvertDisplayTypeId
        {
            get { return _AdvertDisplayTypeId; }
            set { _AdvertDisplayTypeId = value; }
        }
        private List<AdvertPositions> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<AdvertPositions> l_AdvertPositions = new List<AdvertPositions>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    AdvertPositions m_AdvertPositions = new AdvertPositions(db.ConnectionString);
                    m_AdvertPositions.AdvertPositionId = smartReader.GetInt32("AdvertPositionId");
                    m_AdvertPositions.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_AdvertPositions.SiteId = smartReader.GetInt16("SiteId");
                    m_AdvertPositions.CategoryId = smartReader.GetInt16("CategoryId");
                    m_AdvertPositions.PositionName = smartReader.GetString("PositionName");
                    m_AdvertPositions.PositionDesc = smartReader.GetString("PositionDesc");
                    m_AdvertPositions.Width = smartReader.GetString("Width");
                    m_AdvertPositions.Height = smartReader.GetString("Height");
                    m_AdvertPositions.OverflowWidth = smartReader.GetString("OverflowWidth");
                    m_AdvertPositions.OverflowHeight = smartReader.GetString("OverflowHeight");
                    m_AdvertPositions.AdvertDisplayTypeId = smartReader.GetByte("AdvertDisplayTypeId");
                    m_AdvertPositions.CrUserId = smartReader.GetInt32("CrUserId");
                    m_AdvertPositions.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_AdvertPositions.Add(m_AdvertPositions);
                }
                reader.Close();
                return l_AdvertPositions;
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
                SqlCommand cmd = new SqlCommand("AdvertPositions_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@PositionName", this.PositionName));
                cmd.Parameters.Add(new SqlParameter("@PositionDesc", this.PositionDesc));
                cmd.Parameters.Add(new SqlParameter("@Width", this.Width));
                cmd.Parameters.Add(new SqlParameter("@Height", this.Height));
                cmd.Parameters.Add(new SqlParameter("@OverflowWidth", this.OverflowWidth));
                cmd.Parameters.Add(new SqlParameter("@OverflowHeight", this.OverflowHeight));
                cmd.Parameters.Add(new SqlParameter("@AdvertDisplayTypeId", this.AdvertDisplayTypeId));
                cmd.Parameters.Add("@AdvertPositionId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.AdvertPositionId = Convert.ToInt32((cmd.Parameters["@AdvertPositionId"].Value == null) ? 0 : (cmd.Parameters["@AdvertPositionId"].Value));
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
                SqlCommand cmd = new SqlCommand("AdvertPositions_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@PositionName", this.PositionName));
                cmd.Parameters.Add(new SqlParameter("@PositionDesc", this.PositionDesc));
                cmd.Parameters.Add(new SqlParameter("@Width", this.Width));
                cmd.Parameters.Add(new SqlParameter("@Height", this.Height));
                cmd.Parameters.Add(new SqlParameter("@OverflowWidth", this.OverflowWidth));
                cmd.Parameters.Add(new SqlParameter("@OverflowHeight", this.OverflowHeight));
                cmd.Parameters.Add(new SqlParameter("@AdvertDisplayTypeId", this.AdvertDisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@AdvertPositionId", this.AdvertPositionId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("AdvertPositions_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@AdvertPositionId", this.AdvertPositionId));
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
        public List<AdvertPositions> GetList()
        {
            List<AdvertPositions> RetVal = new List<AdvertPositions>();
            try
            {
                string sql = "SELECT * FROM AdvertPositions";
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
        public static List<AdvertPositions> Static_GetList(string ConStr)
        {
            List<AdvertPositions> RetVal = new List<AdvertPositions>();
            try
            {
                AdvertPositions m_AdvertPositions = new AdvertPositions(ConStr);
                RetVal = m_AdvertPositions.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<AdvertPositions> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<AdvertPositions> GetListByAdvertPositionId(int AdvertPositionId)
        {
            List<AdvertPositions> RetVal = new List<AdvertPositions>();
            try
            {
                if (AdvertPositionId > 0)
                {
                    string sql = "SELECT * FROM AdvertPositions WHERE (AdvertPositionId=" + AdvertPositionId.ToString() + ")";
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
        public AdvertPositions Get(int AdvertPositionId)
        {
            AdvertPositions RetVal = new AdvertPositions(db.ConnectionString);
            try
            {
                List<AdvertPositions> list = GetListByAdvertPositionId(AdvertPositionId);
                if (list.Count > 0)
                {
                    RetVal = (AdvertPositions)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public AdvertPositions Get()
        {
            return Get(this.AdvertPositionId);
        }
        //-------------------------------------------------------------- 
        public static AdvertPositions Static_Get(int AdvertPositionId)
        {
            AdvertPositions m_AdvertPositions = new AdvertPositions();
            return m_AdvertPositions.Get(AdvertPositionId);
        }
        //--------------------------------------------------------------     
        public List<AdvertPositions> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte ApplicationTypeId, string PositionName, byte AdvertDisplayTypeId, ref int RowCount)
        {
            List<AdvertPositions> RetVal = new List<AdvertPositions>();
            try
            {
                SqlCommand cmd = new SqlCommand("AdvertPositions_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@PositionName", PositionName));
                cmd.Parameters.Add(new SqlParameter("@AdvertDisplayTypeId", AdvertDisplayTypeId));
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
        public List<AdvertPositions> Search(int ActUserId, string OrderBy, byte ApplicationTypeId, string PositionName, byte AdvertDisplayTypeId, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, ApplicationTypeId, PositionName, AdvertDisplayTypeId, ref RowCount);
        }
    }
}

