using sms.common;
using sms.database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ICSoft.CMSLib
{
    public class ArticleLocation
    {
        #region Private Properties

        private int _ArticleLocationId;
        private int _ArticleId;
        private string _Address;
        private double _Longitude;
        private double _Latitude;
        private short _ProvinceId;
        private short _DistrictId;
        private int _WardId;
        private int _RowCount;
        private DBAccess db;

        #endregion

        #region Public Properties
        public SysMessages m_SysMessages = new SysMessages();
        //-----------------------------------------------------------------
        public ArticleLocation()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
            m_SysMessages = new SysMessages(db.ConnectionString);
        }
        //-----------------------------------------------------------------        
        public ArticleLocation(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
            m_SysMessages = new SysMessages(db.ConnectionString);
        }
        //-----------------------------------------------------------------
        ~ArticleLocation()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleLocationId
        {
            get
            {
                return _ArticleLocationId;
            }
            set
            {
                _ArticleLocationId = value;
            }
        }

        public int ArticleId
        {
            get
            {
                return _ArticleId;
            }
            set
            {
                _ArticleId = value;
            }
        }
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }
        public double Longitude
        {
            get
            {
                return _Longitude;
            }
            set
            {
                _Longitude = value;
            }
        }
        public double Latitude
        {
            get
            {
                return _Latitude;
            }
            set
            {
                _Latitude = value;
            }
        }

        public short ProvinceId
        {
            get
            {
                return _ProvinceId;
            }
            set
            {
                _ProvinceId = value;
            }
        }
        public short DistrictId
        {
            get
            {
                return _DistrictId;
            }
            set
            {
                _DistrictId = value;
            }
        }
        public int WardId
        {
            get
            {
                return _WardId;
            }
            set
            {
                _WardId = value;
            }
        }

        public int RowCount
        {
            get
            {
                return _RowCount;
            }
            set
            {
                _RowCount = value;
            }
        }
        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<ArticleLocation> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleLocation> l_ArticleLocation = new List<ArticleLocation>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleLocation m_ArticleLocation = new ArticleLocation();
                    m_ArticleLocation.ArticleLocationId = smartReader.GetInt32("ArticleLocationId");
                    m_ArticleLocation.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleLocation.ProvinceId = smartReader.GetInt16("ProvinceId");
                    m_ArticleLocation.DistrictId = smartReader.GetInt16("DistrictId");
                    m_ArticleLocation.WardId = smartReader.GetInt32("WardId");
                    m_ArticleLocation.Address = smartReader.GetString("Address");
                    m_ArticleLocation.Longitude = smartReader.GetDouble("Longitude");
                    m_ArticleLocation.Latitude = smartReader.GetDouble("Latitude");

                    l_ArticleLocation.Add(m_ArticleLocation);
                }
                reader.Close();
                return l_ArticleLocation;
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
        public short ArticleLocation_Insert()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleLocation_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DistrictId", this.DistrictId));
                cmd.Parameters.Add(new SqlParameter("@WardId", this.WardId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Longitude", this.Longitude));
                cmd.Parameters.Add(new SqlParameter("@Latitude", this.Latitude));
                cmd.Parameters.Add("@ArticleLocationId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleLocationId = int.Parse(cmd.Parameters["@ArticleLocationId"].Value.ToString());
                m_SysMessages.SysMessageId = short.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                m_SysMessages.SysMessageTypeId = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                throw ex;
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //-----------------------------------------------------------
        public short ArticleLocation_InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleLocation_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DistrictId", this.DistrictId));
                cmd.Parameters.Add(new SqlParameter("@WardId", this.WardId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Longitude", this.Longitude));
                cmd.Parameters.Add(new SqlParameter("@Latitude", this.Latitude));
                cmd.Parameters.Add(new SqlParameter("@ArticleLocationId", this.ArticleLocationId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleLocationId = Convert.ToInt32((cmd.Parameters["@ArticleLocationId"].Value == null) ? 0 : (cmd.Parameters["@ArticleLocationId"].Value));
                SysMessageId = short.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                m_SysMessages.SysMessageTypeId = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                throw ex;
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //--------------------------------------------------------------
        public short ArticleLocation_Update()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleLocation_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DistrictId", this.DistrictId));
                cmd.Parameters.Add(new SqlParameter("@WardId", this.WardId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Longitude", this.Longitude));
                cmd.Parameters.Add(new SqlParameter("@Latitude", this.Latitude));
                cmd.Parameters.Add(new SqlParameter("@ArticleLocationId", this.ArticleLocationId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                m_SysMessages.SysMessageId = short.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                m_SysMessages.SysMessageTypeId = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                throw ex;
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //--------------------------------------------------------------            
        public short ArticleLocation_Delete()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleLocation_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleLocationId", this.ArticleLocationId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                m_SysMessages.SysMessageId = short.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                m_SysMessages.SysMessageTypeId = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                throw ex;
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //--------------------------------------------------------------     
        public ArticleLocation ArticleLocation_Get()
        {
            ArticleLocation retVal = new ArticleLocation();
            try
            {
                string sql = "SELECT * FROM ArticleLocation WHERE (ArticleLocationId=" + this.ArticleLocationId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                List<ArticleLocation> list = Init(cmd);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //--------------------------------------------------------------     
        public ArticleLocation ArticleLocation_GetByArticleID(int ArticleId)
        {
            ArticleLocation retVal = new ArticleLocation();
            try
            {
                string sql = "SELECT * FROM ArticleLocation WHERE (ArticleId=" + ArticleId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                List<ArticleLocation> list = Init(cmd);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        //====
        //--------------------------------------------------------------     
        public static ArticleLocation Static_GetByArticleID(int ArticleId)
        {

            ArticleLocation m_ArticleLocation = new ArticleLocation();
            m_ArticleLocation = m_ArticleLocation.ArticleLocation_GetByArticleID(ArticleId);
            return m_ArticleLocation;

        }

        //-------------------------------------------------------------- 

        public List<ArticleLocation> ArticleLocation_Search(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleLocation_Search");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DistrictId", this.DistrictId));
                cmd.Parameters.Add(new SqlParameter("@WardId", this.WardId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Longitude", this.Longitude));
                cmd.Parameters.Add(new SqlParameter("@Latitude", this.Latitude));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<ArticleLocation> list = Init(cmd);
                this.RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------"Static method"---------------------------------------------- 

        private static List<ArticleLocation> InitStatic(SqlCommand cmd)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleLocation> l_ArticleLocation = new List<ArticleLocation>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleLocation m_ArticleLocation = new ArticleLocation();
                    m_ArticleLocation.ArticleLocationId = smartReader.GetInt32("ArticleLocationId");
                    m_ArticleLocation.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleLocation.ProvinceId = smartReader.GetInt16("ProvinceId");
                    m_ArticleLocation.DistrictId = smartReader.GetInt16("DistrictId");
                    m_ArticleLocation.WardId = smartReader.GetInt32("WardId");
                    m_ArticleLocation.Address = smartReader.GetString("Address");
                    m_ArticleLocation.Longitude = smartReader.GetDouble("Longitude");
                    m_ArticleLocation.Latitude = smartReader.GetDouble("Latitude");

                    l_ArticleLocation.Add(m_ArticleLocation);
                }
                reader.Close();
                return l_ArticleLocation;
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
        public static List<ArticleLocation> ArticleLocation_GetList_StaticALL()
        {
            List<ArticleLocation> list = new List<ArticleLocation>();
            try
            {
                string sql = "SELECT * FROM ArticleLocation ";
                SqlCommand cmd = new SqlCommand(sql);
                list = InitStatic(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        ////-------------------------------------------------------------- 
        //public static string GetArticleLocationName(Int32 ArticleLocationId)
        //{
        //    return ArticleLocation_GetStatic(ArticleLocationId).ArticleLocatioName;
        //}
        
        //--------------------------------------------------------------     
        public static ArticleLocation ArticleLocation_GetStatic(Int32 ArticleLocationId)
        {
            ArticleLocation m_ArticleLocation = new ArticleLocation();
            List<ArticleLocation> l_ArticleLocation = ArticleLocation.ArticleLocation_GetList_StaticALL();

            for (int index = 0; index < l_ArticleLocation.Count; index++)
            {
                m_ArticleLocation = (ArticleLocation)l_ArticleLocation[index];
                if (m_ArticleLocation.ArticleLocationId == ArticleLocationId)
                    return m_ArticleLocation;
            }
            return m_ArticleLocation;
        }

        #endregion
    }
}
