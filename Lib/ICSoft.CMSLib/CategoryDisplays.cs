
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
    public class CategoryDisplays
    {
        private int _CategoryDisplayId;
        private short _CategoryId;
        private short _DisplayTypeId;
        private int _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public CategoryDisplays()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CategoryDisplays(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CategoryDisplays()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int CategoryDisplayId
        {
            get { return _CategoryDisplayId; }
            set { _CategoryDisplayId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        //-----------------------------------------------------------------
        public short DisplayTypeId
        {
            get { return _DisplayTypeId; }
            set { _DisplayTypeId = value; }
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

        private List<CategoryDisplays> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CategoryDisplays> l_CategoryDisplays = new List<CategoryDisplays>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CategoryDisplays m_CategoryDisplays = new CategoryDisplays(db.ConnectionString);
                    m_CategoryDisplays.CategoryDisplayId = smartReader.GetInt32("CategoryDisplayId");
                    m_CategoryDisplays.CategoryId = smartReader.GetInt16("CategoryId");
                    m_CategoryDisplays.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_CategoryDisplays.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_CategoryDisplays.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CategoryDisplays.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_CategoryDisplays.Add(m_CategoryDisplays);
                }
                reader.Close();
                return l_CategoryDisplays;
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
                SqlCommand cmd = new SqlCommand("CategoryDisplays_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@CategoryDisplayId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CategoryDisplayId = (cmd.Parameters["@CategoryDisplayId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@CategoryDisplayId"].Value);
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
                SqlCommand cmd = new SqlCommand("CategoryDisplays_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@CategoryDisplayId", this.CategoryDisplayId));
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
                SqlCommand cmd = new SqlCommand("CategoryDisplays_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@CategoryDisplayId", this.CategoryDisplayId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CategoryDisplayId = (cmd.Parameters["@CategoryDisplayId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@CategoryDisplayId"].Value);
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
                SqlCommand cmd = new SqlCommand("CategoryDisplays_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
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
        public byte AddAndDel(byte Replicated, int ActUserId, string DisplayTypeId_Add, string DisplayTypeId_Del, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CategoryDisplays_AddAndDel");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId_Add", DisplayTypeId_Add));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId_Del", DisplayTypeId_Del));
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
        public List<CategoryDisplays> GetListByDisplayTypeId(short DisplayTypeId)
        {
            List<CategoryDisplays> RetVal = new List<CategoryDisplays>();
            try
            {
                string sql = "SELECT * FROM CategoryDisplays WHERE DisplayTypeId=" + DisplayTypeId.ToString();
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
        public List<CategoryDisplays> GetListByCategoryId(short CategoryId)
        {
            List<CategoryDisplays> RetVal = new List<CategoryDisplays>();
            try
            {
                string sql = "SELECT * FROM CategoryDisplays WHERE CategoryId=" + CategoryId.ToString();
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
        public List<CategoryDisplays> GetListBySiteId(short SiteId)
        {
            List<CategoryDisplays> RetVal = new List<CategoryDisplays>();
            try
            {
                string sql = "SELECT * FROM CategoryDisplays WHERE CategoryId IN (SELECT CategoryId FROM Categories WHERE SiteId=" + SiteId.ToString() + ")";
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
        public static List<CategoryDisplays> Static_GetByCategoryId(short CategoryId, List<CategoryDisplays> list)
        {
            List<CategoryDisplays> RetVal = new List<CategoryDisplays>();
            RetVal = list.FindAll(i => i.CategoryId == CategoryId);
            return RetVal;
        }
    }
}

