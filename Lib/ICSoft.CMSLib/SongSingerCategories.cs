
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
    public class SongSingerCategories
    {
        private int _SongSingerCategoryId;
        private int _SongSingerId;
        private short _CategoryId;
        private int _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SongSingerCategories()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SongSingerCategories(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SongSingerCategories()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SongSingerCategoryId
        {
            get { return _SongSingerCategoryId; }
            set { _SongSingerCategoryId = value; }
        }
        //-----------------------------------------------------------------
        public int SongSingerId
        {
            get { return _SongSingerId; }
            set { _SongSingerId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<SongSingerCategories> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SongSingerCategories> l_SongSingerCategories = new List<SongSingerCategories>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SongSingerCategories m_SongSingerCategories = new SongSingerCategories(db.ConnectionString);
                    m_SongSingerCategories.SongSingerCategoryId = smartReader.GetInt32("SongSingerCategoryId");
                    m_SongSingerCategories.SongSingerId = smartReader.GetInt32("SongSingerId");
                    m_SongSingerCategories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_SongSingerCategories.DisplayOrder = smartReader.GetInt32("DisplayOrder");

                    l_SongSingerCategories.Add(m_SongSingerCategories);
                }
                reader.Close();
                return l_SongSingerCategories;
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
                SqlCommand cmd = new SqlCommand("SongSingerCategories_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@SongSingerCategoryId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerCategoryId = (cmd.Parameters["@SongSingerCategoryId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerCategoryId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongSingerCategories_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SongSingerCategoryId", this.SongSingerCategoryId));
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
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerCategories_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SongSingerCategoryId", this.SongSingerCategoryId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SongSingerCategoryId = (cmd.Parameters["@SongSingerCategoryId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@SongSingerCategoryId"].Value);
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
                SqlCommand cmd = new SqlCommand("SongSingerCategories_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerCategoryId", this.SongSingerCategoryId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
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
        public byte DeleteBySongSingerId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerCategories_DeleteBySongSingerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", this.SongSingerId));
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
        public byte DeleteByCategoryId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SongSingerCategories_DeleteByCategoryId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
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
        public List<SongSingerCategories> GetList(int SongSingerId)
        {
            List<SongSingerCategories> RetVal = new List<SongSingerCategories>();
            try
            {
                string sql = "SELECT * FROM SongSingerCategories WHERE SongSingerId=" + SongSingerId.ToString() + " ORDER BY DisplayOrder";
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
        public List<SongSingerCategories> GetListBySongSingerCategoryId(int SongSingerCategoryId)
        {
            List<SongSingerCategories> RetVal = new List<SongSingerCategories>();
            try
            {
                if (SongSingerCategoryId > 0)
                {
                    string sql = "SELECT * FROM SongSingerCategories WHERE (SongSingerCategoryId=" + SongSingerCategoryId.ToString() + ")";
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
        public SongSingerCategories Get(int SongSingerCategoryId)
        {
            SongSingerCategories RetVal = new SongSingerCategories();
            try
            {
                List<SongSingerCategories> list = GetListBySongSingerCategoryId(SongSingerCategoryId);
                if (list.Count > 0)
                {
                    RetVal = (SongSingerCategories)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public SongSingerCategories Get()
        {
            return Get(this.SongSingerCategoryId);
        }
        
    }
}

