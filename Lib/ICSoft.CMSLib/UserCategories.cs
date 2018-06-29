
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
    public class UserCategories
    {
        private int _UserCategoryId;
        private int _UserId;
        private short _CategoryId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public UserCategories()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public UserCategories(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~UserCategories()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int UserCategoryId
        {
            get { return _UserCategoryId; }
            set { _UserCategoryId = value; }
        }
        //-----------------------------------------------------------------
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //-----------------------------------------------------------------
        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
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

        private List<UserCategories> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<UserCategories> l_UserCategories = new List<UserCategories>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    UserCategories m_UserCategories = new UserCategories(db.ConnectionString);
                    m_UserCategories.UserCategoryId = smartReader.GetInt32("UserCategoryId");
                    m_UserCategories.UserId = smartReader.GetInt32("UserId");
                    m_UserCategories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_UserCategories.CrUserId = smartReader.GetInt32("CrUserId");
                    m_UserCategories.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_UserCategories.Add(m_UserCategories);
                }
                reader.Close();
                return l_UserCategories;
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
                SqlCommand cmd = new SqlCommand("UserCategories_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add("@UserCategoryId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.UserCategoryId = Convert.ToInt32((cmd.Parameters["@UserCategoryId"].Value == null) ? 0 : (cmd.Parameters["@UserCategoryId"].Value));
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
                SqlCommand cmd = new SqlCommand("UserCategories_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
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
        public byte DeleteByUserId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("UserCategories_DeleteByUserId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
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
        //-----------------------------------------------------------------
        public string UserCategories_GetListCategoryId(int UserId)
        {
            string RetVal = "";
            try
            {
                string sql = "DECLARE @Result NVARCHAR(MAX);SELECT @Result = COALESCE(@Result + ', ', '') + convert(nvarchar(10),CategoryId) FROM UserCategories WHERE UserId=" + UserId.ToString() + ";SELECT @Result";
                RetVal = db.ExecuteScalarReturnObj(sql).ToString();                
            }
            catch (Exception ex)
            {
               throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public static string Static_UserCategories_GetListCategoryName(int UserId)
        {
            try
            {
                UserCategories m_UserCategories = new UserCategories();
                return m_UserCategories.UserCategories_GetListCategoryName(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-----------------------------------------------------------------
        public string UserCategories_GetListCategoryName(int UserId)
        {
            try
            {
                string sql = "DECLARE @Result NVARCHAR(MAX);SELECT @Result = COALESCE(@Result + ', ', '') + CategoryDesc FROM UserCategories INNER JOIN Categories ON UserCategories.CategoryId=Categories.CategoryId WHERE UserId=" + UserId.ToString() + ";SELECT @Result";
                string result = db.ExecuteScalarReturnObj(sql).ToString();
                return result;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        //--------------------------------------------------------------     
        public List<UserCategories> UserCategories_GetList(int ActUserId, string OrderBy, int UserId, short CategoryId)
        {
            List<UserCategories> RetVal = new List<UserCategories>();
            try
            {
                SqlCommand cmd = new SqlCommand("UserCategories_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
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

