
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
    public class Adverts
    {
        private int _AdvertId;
        private string _AdvertName;
        private string _AdvertDesc;
        private string _Url;
        private string _ImagePath;
        private string _ImageHoverPath;
        private string _ScriptContent;
        private string _Width;
        private string _Height;
        private DateTime _StartTime;
        private DateTime _EndTime;
        private short _PartnerId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private byte _AdvertContentTypeId;
        private short _SiteId;
        private byte _AdvertStatusId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Adverts()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Adverts(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Adverts()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int AdvertId
        {
            get { return _AdvertId; }
            set { _AdvertId = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertName
        {
            get { return _AdvertName; }
            set { _AdvertName = value; }
        }
        //-----------------------------------------------------------------
        public string AdvertDesc
        {
            get { return _AdvertDesc; }
            set { _AdvertDesc = value; }
        }
        //-----------------------------------------------------------------
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        //-----------------------------------------------------------------
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        //-----------------------------------------------------------------
        public string ImageHoverPath
        {
            get { return _ImageHoverPath; }
            set { _ImageHoverPath = value; }
        }
        //-----------------------------------------------------------------
        public string ScriptContent
        {
            get { return _ScriptContent; }
            set { _ScriptContent = value; }
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
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        //-----------------------------------------------------------------
        public short PartnerId
        {
            get { return _PartnerId; }
            set { _PartnerId = value; }
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
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }    
        //-----------------------------------------------------------------

        public byte AdvertContentTypeId
        {
            get { return _AdvertContentTypeId; }
            set { _AdvertContentTypeId = value; }
        }
        public byte AdvertStatusId
        {
            get { return _AdvertStatusId; }
            set { _AdvertStatusId = value; }
        }
        private List<Adverts> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Adverts> l_Adverts = new List<Adverts>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Adverts m_Adverts = new Adverts(db.ConnectionString);
                    m_Adverts.AdvertId = smartReader.GetInt32("AdvertId");
                    m_Adverts.AdvertName = smartReader.GetString("AdvertName");
                    m_Adverts.AdvertDesc = smartReader.GetString("AdvertDesc");
                    m_Adverts.Url = smartReader.GetString("Url");
                    m_Adverts.ImagePath = smartReader.GetString("ImagePath");
                    m_Adverts.ImageHoverPath = smartReader.GetString("ImageHoverPath");
                    m_Adverts.ScriptContent = smartReader.GetString("ScriptContent");
                    m_Adverts.SiteId = smartReader.GetInt16("SiteId");
                    m_Adverts.AdvertContentTypeId = smartReader.GetByte("AdvertContentTypeId");
                    m_Adverts.Width = smartReader.GetString("Width");
                    m_Adverts.Height = smartReader.GetString("Height");
                    m_Adverts.StartTime = smartReader.GetDateTime("StartTime");
                    m_Adverts.EndTime = smartReader.GetDateTime("EndTime");
                    m_Adverts.PartnerId = smartReader.GetInt16("PartnerId");
                    m_Adverts.AdvertStatusId = smartReader.GetByte("AdvertStatusId");
                    m_Adverts.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Adverts.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Adverts.Add(m_Adverts);
                }
                reader.Close();
                return l_Adverts;
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
                SqlCommand cmd = new SqlCommand("Adverts_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@AdvertName", this.AdvertName));
                cmd.Parameters.Add(new SqlParameter("@AdvertDesc", this.AdvertDesc));
                cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@ImageHoverPath", this.ImageHoverPath));
                cmd.Parameters.Add(new SqlParameter("@ScriptContent", this.ScriptContent));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@AdvertContentTypeId", this.AdvertContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@Width", this.Width));
                cmd.Parameters.Add(new SqlParameter("@Height", this.Height));
                if (this.StartTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@StartTime", this.StartTime));
                if (this.EndTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@PartnerId", this.PartnerId));
                cmd.Parameters.Add(new SqlParameter("@AdvertStatusId", this.AdvertStatusId));
                cmd.Parameters.Add("@AdvertId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.AdvertId = Convert.ToInt32((cmd.Parameters["@AdvertId"].Value == null) ? 0 : (cmd.Parameters["@AdvertId"].Value));
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
                SqlCommand cmd = new SqlCommand("Adverts_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@AdvertName", this.AdvertName));
                cmd.Parameters.Add(new SqlParameter("@AdvertDesc", this.AdvertDesc));
                cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@ImageHoverPath", this.ImageHoverPath));
                cmd.Parameters.Add(new SqlParameter("@ScriptContent", this.ScriptContent));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@AdvertContentTypeId", this.AdvertContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@Width", this.Width));
                cmd.Parameters.Add(new SqlParameter("@Height", this.Height));
                if (this.StartTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@StartTime", this.StartTime));
                if (this.EndTime != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@PartnerId", this.PartnerId));
                cmd.Parameters.Add(new SqlParameter("@AdvertStatusId", this.AdvertStatusId));
                cmd.Parameters.Add(new SqlParameter("@AdvertId", this.AdvertId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Adverts_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@AdvertId", this.AdvertId));
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
        public List<Adverts> GetList()
        {
            List<Adverts> RetVal = new List<Adverts>();
            try
            {
                string sql = "SELECT * FROM Adverts";
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
        public static List<Adverts> Static_GetList(string ConStr)
        {
            List<Adverts> RetVal = new List<Adverts>();
            try
            {
                Adverts m_Adverts = new Adverts(ConStr);
                RetVal = m_Adverts.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Adverts> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<Adverts> GetListByAdvertPositionId(int AdvertPositionId, short CategoryId)
        {
            List<Adverts> RetVal = new List<Adverts>();
            try
            {
                if (AdvertPositionId > 0)
                {
                    string sql = "SELECT A.* FROM Adverts A INNER JOIN AdvertPositionAdverts P ON A.AdvertId=P.AdvertId WHERE (AdvertPositionId=" + AdvertPositionId.ToString() + ") ORDER BY DisplayOrder";
                    if (CategoryId > 0)
                    {
                        sql = "SELECT A.* FROM Adverts A INNER JOIN AdvertPositionAdverts P ON A.AdvertId=P.AdvertId WHERE (AdvertPositionId=" + AdvertPositionId.ToString() + " AND CategoryId=" + CategoryId.ToString() + ") ORDER BY DisplayOrder";
                    }
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
        public List<Adverts> GetListByAdvertId(int AdvertId)
        {
            List<Adverts> RetVal = new List<Adverts>();
            try
            {
                if (AdvertId > 0)
                {
                    string sql = "SELECT * FROM Adverts WHERE (AdvertId=" + AdvertId.ToString() + ")";
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
        public Adverts Get(int AdvertId)
        {
            Adverts RetVal = new Adverts(db.ConnectionString);
            try
            {
                List<Adverts> list = GetListByAdvertId(AdvertId);
                if (list.Count > 0)
                {
                    RetVal = (Adverts)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Adverts Get()
        {
            return Get(this.AdvertId);
        }
        //-------------------------------------------------------------- 
        public static Adverts Static_Get(int AdvertId)
        {
            Adverts m_Adverts = new Adverts();
            return m_Adverts.Get(AdvertId);
        }
        //--------------------------------------------------------------     
        public List<Adverts> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string AdvertName, byte AdvertContentTypeId, short PartnerId, byte AdvertStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Adverts> RetVal = new List<Adverts>();
            try
            {
                SqlCommand cmd = new SqlCommand("Adverts_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@AdvertName", AdvertName));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@AdvertContentTypeId", AdvertContentTypeId));
                cmd.Parameters.Add(new SqlParameter("@PartnerId", PartnerId));
                cmd.Parameters.Add(new SqlParameter("@AdvertStatusId", AdvertStatusId));
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
        public List<Adverts> Search(int ActUserId, string OrderBy, string AdvertName, byte AdvertContentTypeId, short PartnerId, byte AdvertStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, AdvertName, AdvertContentTypeId, PartnerId, AdvertStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}

