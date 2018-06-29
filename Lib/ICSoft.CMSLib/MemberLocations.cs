
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
    public class MemberLocations
    {
        private int _MemberLocationId;
        private string _MemberLocation;
        private byte _MemberDisplayStatusId;
        private int _MemberId;
        private DateTime _CrDateTime;        
        private DBAccess db;
        //-----------------------------------------------------------------
        public MemberLocations()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MemberLocations(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MemberLocations()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MemberLocationId
        {
            get { return _MemberLocationId; }
            set { _MemberLocationId = value; }
        }
        //-----------------------------------------------------------------
        public string MemberLocation
        {
            get { return _MemberLocation; }
            set { _MemberLocation = value; }
        }
        //-----------------------------------------------------------------
        public byte MemberDisplayStatusId
        {
            get { return _MemberDisplayStatusId; }
            set { _MemberDisplayStatusId = value; }
        }
        //-----------------------------------------------------------------

        public int MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        private List<MemberLocations> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MemberLocations> l_MemberLocations = new List<MemberLocations>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MemberLocations m_MemberLocations = new MemberLocations(db.ConnectionString);
                    m_MemberLocations.MemberLocationId = smartReader.GetInt32("MemberLocationId");
                    m_MemberLocations.MemberId = smartReader.GetInt32("MemberId");
                    m_MemberLocations.MemberLocation = smartReader.GetString("MemberLocation");
                    m_MemberLocations.MemberDisplayStatusId = smartReader.GetByte("MemberDisplayStatusId");
                    m_MemberLocations.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_MemberLocations.Add(m_MemberLocations);
                }
                reader.Close();
                return l_MemberLocations;
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
                SqlCommand cmd = new SqlCommand("MemberLocations_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@MemberLocation", this.MemberLocation));
                cmd.Parameters.Add(new SqlParameter("@MemberDisplayStatusId", this.MemberDisplayStatusId));
                cmd.Parameters.Add("@MemberLocationId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MemberLocationId = Convert.ToInt32((cmd.Parameters["@MemberLocationId"].Value == null) ? 0 : (cmd.Parameters["@MemberLocationId"].Value));
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
                SqlCommand cmd = new SqlCommand("MemberLocations_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@MemberLocation", this.MemberLocation));
                cmd.Parameters.Add(new SqlParameter("@MemberDisplayStatusId", this.MemberDisplayStatusId));
                cmd.Parameters.Add(new SqlParameter("@MemberLocationId", this.MemberLocationId));
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
                SqlCommand cmd = new SqlCommand("MemberLocations_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberLocationId", this.MemberLocationId));
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
        public List<MemberLocations> GetList()
        {
            List<MemberLocations> RetVal = new List<MemberLocations>();
            try
            {
                string sql = "SELECT * FROM V$MemberLocations";
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
        public static List<MemberLocations> Static_GetList(string ConStr)
        {
            List<MemberLocations> RetVal = new List<MemberLocations>();
            try
            {
                MemberLocations m_MemberLocations = new MemberLocations(ConStr);
                RetVal = m_MemberLocations.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MemberLocations> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<MemberLocations> GetListOrderBy(string OrderBy)
        {
            List<MemberLocations> RetVal = new List<MemberLocations>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$MemberLocations ";
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
        public static List<MemberLocations> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            MemberLocations m_MemberLocations = new MemberLocations(ConStr);
            return m_MemberLocations.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MemberLocations> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<MemberLocations> GetListByMemberLocationId(int MemberLocationId)
        {
            List<MemberLocations> RetVal = new List<MemberLocations>();
            try
            {
                if (MemberLocationId > 0)
                {
                    string sql = "SELECT * FROM V$MemberLocations WHERE (MemberLocationId=" + MemberLocationId.ToString() + ")";
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
        public MemberLocations Get(int MemberLocationId)
        {
            MemberLocations RetVal = new MemberLocations(db.ConnectionString);
            try
            {
                List<MemberLocations> list = GetListByMemberLocationId(MemberLocationId);
                if (list.Count > 0)
                {
                    RetVal = (MemberLocations)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MemberLocations Get()
        {
            return Get(this.MemberLocationId);
        }
        //-------------------------------------------------------------- 
        public static MemberLocations Static_Get(int MemberLocationId)
        {
            return Static_Get(MemberLocationId);
        }
        //--------------------------------------------------------------     
        public List<MemberLocations> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int MemberId, string MemberLocation, byte MemberDisplayStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<MemberLocations> RetVal = new List<MemberLocations>();
            try
            {
                SqlCommand cmd = new SqlCommand("MemberLocations_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MemberId", MemberId));
                cmd.Parameters.Add(new SqlParameter("@MemberLocation", StringUtil.InjectionString(MemberLocation)));
                cmd.Parameters.Add(new SqlParameter("@MemberDisplayStatusId", MemberDisplayStatusId));
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
        public MemberLocations GetCurent( int MemberId)
        {
            int ActUserId = 0;
            int RowAmount = 1;
            int PageIndex = 0;
            string OrderBy = " CrDateTime DESC ";
            string MemberLocation = "";
            byte MemberDisplayStatusId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            MemberLocations RetVal = new MemberLocations();
            try
            {
                List<MemberLocations> l_Location = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberId, MemberLocation, MemberDisplayStatusId, DateFrom, DateTo, ref RowCount);
                if (l_Location.Count > 0)
                    RetVal = l_Location[0];
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static string Static_GetCurentLocation(string ConStr, int MemberId)
        {
            string RetVal = "";
            MemberLocations m_MemberLocations = new MemberLocations(ConStr);
            m_MemberLocations = m_MemberLocations.GetCurent(MemberId);
            RetVal = m_MemberLocations.MemberLocation;
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static string Static_GetCurentLocation(int MemberId)
        {
            string ConStr = "";
            return Static_GetCurentLocation(ConStr, MemberId);
        }
        //--------------------------------------------------------------     
        public List<MemberLocations> Search(int ActUserId, string OrderBy, int MemberId, string MemberLocation, byte MemberDisplayStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberId, MemberLocation, MemberDisplayStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}