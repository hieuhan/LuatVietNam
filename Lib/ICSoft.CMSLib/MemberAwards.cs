
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
    public class MemberAwards
    {
        private int _MemberAwardId;
        private byte _AwardId;
        private DateTime _AwardDate;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _MemberId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public MemberAwards()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MemberAwards(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MemberAwards()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MemberAwardId
        {
            get { return _MemberAwardId; }
            set { _MemberAwardId = value; }
        }
        //-----------------------------------------------------------------
        public byte AwardId
        {
            get { return _AwardId; }
            set { _AwardId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime AwardDate
        {
            get { return _AwardDate; }
            set { _AwardDate = value; }
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

        public int MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private List<MemberAwards> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MemberAwards> l_MemberAwards = new List<MemberAwards>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MemberAwards m_MemberAwards = new MemberAwards(db.ConnectionString);
                    m_MemberAwards.MemberAwardId = smartReader.GetInt32("MemberAwardId");
                    m_MemberAwards.MemberId = smartReader.GetInt32("MemberId");
                    m_MemberAwards.AwardId = smartReader.GetByte("AwardId");
                    m_MemberAwards.AwardDate = smartReader.GetDateTime("AwardDate");
                    m_MemberAwards.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MemberAwards.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_MemberAwards.Add(m_MemberAwards);
                }
                reader.Close();
                return l_MemberAwards;
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
                SqlCommand cmd = new SqlCommand("MemberAwards_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@AwardId", this.AwardId));
                cmd.Parameters.Add(new SqlParameter("@AwardDate", this.AwardDate));
                cmd.Parameters.Add("@MemberAwardId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MemberAwardId = Convert.ToInt32((cmd.Parameters["@MemberAwardId"].Value == null) ? 0 : (cmd.Parameters["@MemberAwardId"].Value));
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
                SqlCommand cmd = new SqlCommand("MemberAwards_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@AwardId", this.AwardId));
                cmd.Parameters.Add(new SqlParameter("@AwardDate", this.AwardDate));
                cmd.Parameters.Add(new SqlParameter("@MemberAwardId", this.MemberAwardId));
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
                SqlCommand cmd = new SqlCommand("MemberAwards_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberAwardId", this.MemberAwardId));
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
        public List<MemberAwards> GetList()
        {
            List<MemberAwards> RetVal = new List<MemberAwards>();
            try
            {
                string sql = "SELECT * FROM V$MemberAwards";
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
        public List<MemberAwards> GetListOrderBy(string OrderBy)
        {
            List<MemberAwards> RetVal = new List<MemberAwards>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$MemberAwards ";
                if (OrderBy.Length > 0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
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
        public static List<MemberAwards> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            MemberAwards m_MemberAwards = new MemberAwards(ConStr);
            return m_MemberAwards.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MemberAwards> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<MemberAwards> GetListByMemberAwardId(int MemberAwardId)
        {
            List<MemberAwards> RetVal = new List<MemberAwards>();
            try
            {
                if (MemberAwardId > 0)
                {
                    string sql = "SELECT * FROM V$MemberAwards WHERE (MemberAwardId=" + MemberAwardId.ToString() + ")";
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
        public List<MemberAwards> GetListByMemberId(int MemberId)
        {
            List<MemberAwards> RetVal = new List<MemberAwards>();
            try
            {
                if (MemberId > 0)
                {
                    string sql = "SELECT * FROM V$MemberAwards WHERE (MemberId=" + MemberId.ToString() + ")";
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
        public MemberAwards Get(int MemberAwardId)
        {
            MemberAwards RetVal = new MemberAwards(db.ConnectionString);
            try
            {
                List<MemberAwards> list = GetListByMemberAwardId(MemberAwardId);
                if (list.Count > 0)
                {
                    RetVal = (MemberAwards)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MemberAwards Get()
        {
            return Get(this.MemberAwardId);
        }
        //-------------------------------------------------------------- 
        public static MemberAwards Static_Get(int MemberAwardId)
        {
            return Static_Get(MemberAwardId);
        }
        //--------------------------------------------------------------     
        
    }
}

