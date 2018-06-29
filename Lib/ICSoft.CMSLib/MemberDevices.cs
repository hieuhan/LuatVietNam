
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
    public class MemberDevices
    {
        private int _MemberDeviceId;
        private string _DeviceToken;
        private byte _DeviceId;
        private int _MemberId;
        private DateTime _CrDateTime;        
        private DBAccess db;
        //-----------------------------------------------------------------
        public MemberDevices()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MemberDevices(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MemberDevices()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MemberDeviceId
        {
            get { return _MemberDeviceId; }
            set { _MemberDeviceId = value; }
        }
        //-----------------------------------------------------------------
        public string DeviceToken
        {
            get { return _DeviceToken; }
            set { _DeviceToken = value; }
        }
        //-----------------------------------------------------------------
        public byte DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
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
        private List<MemberDevices> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MemberDevices> l_MemberDevices = new List<MemberDevices>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MemberDevices m_MemberDevices = new MemberDevices(db.ConnectionString);
                    m_MemberDevices.MemberDeviceId = smartReader.GetInt32("MemberDeviceId");
                    m_MemberDevices.MemberId = smartReader.GetInt32("MemberId");
                    m_MemberDevices.DeviceToken = smartReader.GetString("DeviceToken");
                    m_MemberDevices.DeviceId = smartReader.GetByte("DeviceId");
                    m_MemberDevices.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_MemberDevices.Add(m_MemberDevices);
                }
                reader.Close();
                return l_MemberDevices;
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
                SqlCommand cmd = new SqlCommand("MemberDevices_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@DeviceToken", this.DeviceToken));
                cmd.Parameters.Add(new SqlParameter("@DeviceId", this.DeviceId));
                cmd.Parameters.Add("@MemberDeviceId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MemberDeviceId = Convert.ToInt32((cmd.Parameters["@MemberDeviceId"].Value == null) ? 0 : (cmd.Parameters["@MemberDeviceId"].Value));
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
                SqlCommand cmd = new SqlCommand("MemberDevices_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", this.MemberId));
                cmd.Parameters.Add(new SqlParameter("@DeviceToken", this.DeviceToken));
                cmd.Parameters.Add(new SqlParameter("@DeviceId", this.DeviceId));
                cmd.Parameters.Add(new SqlParameter("@MemberDeviceId", this.MemberDeviceId));
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
                SqlCommand cmd = new SqlCommand("MemberDevices_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MemberDeviceId", this.MemberDeviceId));
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
        public List<MemberDevices> GetList()
        {
            List<MemberDevices> RetVal = new List<MemberDevices>();
            try
            {
                string sql = "SELECT * FROM V$MemberDevices";
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
        public static List<MemberDevices> Static_GetList(string ConStr)
        {
            List<MemberDevices> RetVal = new List<MemberDevices>();
            try
            {
                MemberDevices m_MemberDevices = new MemberDevices(ConStr);
                RetVal = m_MemberDevices.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MemberDevices> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<MemberDevices> GetListOrderBy(string OrderBy)
        {
            List<MemberDevices> RetVal = new List<MemberDevices>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$MemberDevices ";
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
        public static List<MemberDevices> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            MemberDevices m_MemberDevices = new MemberDevices(ConStr);
            return m_MemberDevices.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<MemberDevices> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------  
        public List<MemberDevices> GetListByMemberDeviceId(int MemberDeviceId)
        {
            List<MemberDevices> RetVal = new List<MemberDevices>();
            try
            {
                if (MemberDeviceId > 0)
                {
                    string sql = "SELECT * FROM V$MemberDevices WHERE (MemberDeviceId=" + MemberDeviceId.ToString() + ")";
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
        public MemberDevices Get(int MemberDeviceId)
        {
            MemberDevices RetVal = new MemberDevices(db.ConnectionString);
            try
            {
                List<MemberDevices> list = GetListByMemberDeviceId(MemberDeviceId);
                if (list.Count > 0)
                {
                    RetVal = (MemberDevices)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MemberDevices Get()
        {
            return Get(this.MemberDeviceId);
        }
        //-------------------------------------------------------------- 
        public static MemberDevices Static_Get(int MemberDeviceId)
        {
            return Static_Get(MemberDeviceId);
        }
        //--------------------------------------------------------------     
        public List<MemberDevices> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int MemberId, string DeviceToken, byte DeviceId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<MemberDevices> RetVal = new List<MemberDevices>();
            try
            {
                SqlCommand cmd = new SqlCommand("MemberDevices_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MemberId", MemberId));
                cmd.Parameters.Add(new SqlParameter("@DeviceToken", StringUtil.InjectionString(DeviceToken)));
                cmd.Parameters.Add(new SqlParameter("@DeviceId", DeviceId));
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
        public static List<MemberDevices> Static_GetPage(  byte DeviceId)
        {
            int ActUserId = 0;
            int RowAmount = 0;
            int PageIndex = 0;
            string OrderBy = "";
            int MemberId = 0;
            string DeviceToken = "";
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            List<MemberDevices> RetVal = new List<MemberDevices>();
            try
            {
                MemberDevices m_MemberDevices = new MemberDevices();
                RetVal = m_MemberDevices.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberId, DeviceToken, DeviceId, DateFrom, DateTo, ref RowCount);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //--------------------------------------------------------------     
        public MemberDevices GetCurent( int MemberId)
        {
            int ActUserId = 0;
            int RowAmount = 1;
            int PageIndex = 0;
            string OrderBy = " CrDateTime DESC ";
            string DeviceToken = "";
            byte DeviceId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            MemberDevices RetVal = new MemberDevices();
            try
            {
                List<MemberDevices> l_Location = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberId, DeviceToken, DeviceId, DateFrom, DateTo, ref RowCount);
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
            MemberDevices m_MemberDevices = new MemberDevices(ConStr);
            m_MemberDevices = m_MemberDevices.GetCurent(MemberId);
            RetVal = m_MemberDevices.DeviceToken;
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static string Static_GetCurentLocation(int MemberId)
        {
            string ConStr = "";
            return Static_GetCurentLocation(ConStr, MemberId);
        }
        //--------------------------------------------------------------     
        public List<MemberDevices> Search(int ActUserId, string OrderBy, int MemberId, string DeviceToken, byte DeviceId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MemberId, DeviceToken, DeviceId, DateFrom, DateTo, ref RowCount);
        }
    }
}