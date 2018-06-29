
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
    public class AdvertPositionAdverts
    {
        private int _AdvertPositionAdvertId;
        private int _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _AdvertPositionId;
        private short _CategoryId;
        private short _SiteId;
        private int _AdvertId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public AdvertPositionAdverts()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public AdvertPositionAdverts(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~AdvertPositionAdverts()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int AdvertPositionAdvertId
        {
            get { return _AdvertPositionAdvertId; }
            set { _AdvertPositionAdvertId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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

        public int AdvertPositionId
        {
            get { return _AdvertPositionId; }
            set { _AdvertPositionId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }  
        public int AdvertId
        {
            get { return _AdvertId; }
            set { _AdvertId = value; }
        }
        private List<AdvertPositionAdverts> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<AdvertPositionAdverts> l_AdvertPositionAdverts = new List<AdvertPositionAdverts>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    AdvertPositionAdverts m_AdvertPositionAdverts = new AdvertPositionAdverts(db.ConnectionString);
                    m_AdvertPositionAdverts.AdvertPositionAdvertId = smartReader.GetInt32("AdvertPositionAdvertId");
                    m_AdvertPositionAdverts.AdvertPositionId = smartReader.GetInt32("AdvertPositionId");
                    m_AdvertPositionAdverts.CategoryId = smartReader.GetInt16("CategoryId");
                    m_AdvertPositionAdverts.SiteId = smartReader.GetInt16("SiteId");
                    m_AdvertPositionAdverts.AdvertId = smartReader.GetInt32("AdvertId");
                    m_AdvertPositionAdverts.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_AdvertPositionAdverts.CrUserId = smartReader.GetInt32("CrUserId");
                    m_AdvertPositionAdverts.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_AdvertPositionAdverts.Add(m_AdvertPositionAdverts);
                }
                reader.Close();
                return l_AdvertPositionAdverts;
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
                SqlCommand cmd = new SqlCommand("AdvertPositionAdverts_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@AdvertPositionId", this.AdvertPositionId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@AdvertId", this.AdvertId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@AdvertPositionAdvertId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.AdvertPositionAdvertId = Convert.ToInt32((cmd.Parameters["@AdvertPositionAdvertId"].Value == null) ? 0 : (cmd.Parameters["@AdvertPositionAdvertId"].Value));
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
                SqlCommand cmd = new SqlCommand("AdvertPositionAdverts_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@AdvertPositionId", this.AdvertPositionId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@AdvertId", this.AdvertId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
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
                SqlCommand cmd = new SqlCommand("AdvertPositionAdverts_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@AdvertId", this.AdvertId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@AdvertPositionId", this.AdvertPositionId));
                cmd.Parameters.Add(new SqlParameter("@AdvertPositionAdvertId", this.AdvertPositionAdvertId));
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
        public List<AdvertPositionAdvertsView> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, short SiteId, short CategoryId, int AdvertPositionId, byte ApplicationTypeId, string PositionName, byte AdvertDisplayTypeId, ref int RowCount)
        {
            List<AdvertPositionAdvertsView> RetVal = new List<AdvertPositionAdvertsView>();
            try
            {
                SqlCommand cmd = new SqlCommand("AdvertPositionAdverts_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@AdvertId", this.AdvertId));
                cmd.Parameters.Add(new SqlParameter("@AdvertPositionId", AdvertPositionId));
                cmd.Parameters.Add(new SqlParameter("@PositionName", PositionName));
                cmd.Parameters.Add(new SqlParameter("@AdvertDisplayTypeId", AdvertDisplayTypeId));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    while (smartReader.Read())
                    {
                        AdvertPositionAdvertsView m_AdvertPositionAdverts = new AdvertPositionAdvertsView();
                        m_AdvertPositionAdverts.AdvertPositionAdvertId = smartReader.GetInt32("AdvertPositionAdvertId");
                        m_AdvertPositionAdverts.AdvertPositionId = smartReader.GetInt32("AdvertPositionId");
                        m_AdvertPositionAdverts.CategoryId = smartReader.GetInt16("CategoryId");
                        m_AdvertPositionAdverts.AdvertId = smartReader.GetInt32("AdvertId");
                        m_AdvertPositionAdverts.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                        m_AdvertPositionAdverts.CrUserId = smartReader.GetInt32("CrUserId");
                        m_AdvertPositionAdverts.CrDateTime = smartReader.GetDateTime("CrDateTime");
                        m_AdvertPositionAdverts.AdvertName = smartReader.GetString("AdvertName");
                        m_AdvertPositionAdverts.PositionName = smartReader.GetString("PositionName");
                        m_AdvertPositionAdverts.CategoryName = smartReader.GetString("CategoryName");
                        m_AdvertPositionAdverts.ImagePath = smartReader.GetString("ImagePath");
                        m_AdvertPositionAdverts.AdvertStatusId = smartReader.GetByte("AdvertStatusId");

                        RetVal.Add(m_AdvertPositionAdverts);
                    }
                    reader.Close();
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
        public List<AdvertPositionAdverts> GetList()
        {
            List<AdvertPositionAdverts> RetVal = new List<AdvertPositionAdverts>();
            try
            {
                string sql = "SELECT * FROM AdvertPositionAdverts";
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
        public DataSet GetScriptCode(short SiteId, byte ApplicationTypeId)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("AdvertPositionAdverts_GetScript");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));

                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}

