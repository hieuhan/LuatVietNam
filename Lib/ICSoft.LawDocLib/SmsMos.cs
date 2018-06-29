
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class SmsMos
    {
        private int _MoId;
        private string _UserId;
        private string _PrefixId;
        private byte _TelcoId;
        private string _MessageIn;
        private string _RequestId;
        private DateTime _RequestTime;
        private byte _SmsServiceId;
        private byte _SmsProcessStatusId;
        private DateTime _CrDateTime;
        private DateTime _ProcessTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SmsMos()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SmsMos(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SmsMos()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MoId
        {
            get { return _MoId; }
            set { _MoId = value; }
        }
        //-----------------------------------------------------------------
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        //-----------------------------------------------------------------
        public string PrefixId
        {
            get { return _PrefixId; }
            set { _PrefixId = value; }
        }
        //-----------------------------------------------------------------
        public byte TelcoId
        {
            get { return _TelcoId; }
            set { _TelcoId = value; }
        }
        //-----------------------------------------------------------------
        public string MessageIn
        {
            get { return _MessageIn; }
            set { _MessageIn = value; }
        }
        //-----------------------------------------------------------------
        public string RequestId
        {
            get { return _RequestId; }
            set { _RequestId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime RequestTime
        {
            get { return _RequestTime; }
            set { _RequestTime = value; }
        }
        //-----------------------------------------------------------------
        public byte SmsServiceId
        {
            get { return _SmsServiceId; }
            set { _SmsServiceId = value; }
        }
        //-----------------------------------------------------------------
        public byte SmsProcessStatusId
        {
            get { return _SmsProcessStatusId; }
            set { _SmsProcessStatusId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ProcessTime
        {
            get { return _ProcessTime; }
            set { _ProcessTime = value; }
        }
        //-----------------------------------------------------------------

        private List<SmsMos> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SmsMos> l_SmsMos = new List<SmsMos>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SmsMos m_SmsMos = new SmsMos(db.ConnectionString);
                    m_SmsMos.MoId = smartReader.GetInt32("MoId");
                    m_SmsMos.UserId = smartReader.GetString("UserId");
                    m_SmsMos.PrefixId = smartReader.GetString("PrefixId");
                    m_SmsMos.TelcoId = smartReader.GetByte("TelcoId");
                    m_SmsMos.MessageIn = smartReader.GetString("MessageIn");
                    m_SmsMos.RequestId = smartReader.GetString("RequestId");
                    m_SmsMos.RequestTime = smartReader.GetDateTime("RequestTime");
                    m_SmsMos.SmsServiceId = smartReader.GetByte("SmsServiceId");
                    m_SmsMos.SmsProcessStatusId = smartReader.GetByte("SmsProcessStatusId");
                    m_SmsMos.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_SmsMos.ProcessTime = smartReader.GetDateTime("ProcessTime");

                    l_SmsMos.Add(m_SmsMos);
                }
                reader.Close();
                return l_SmsMos;
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
                SqlCommand cmd = new SqlCommand("SmsMos_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                cmd.Parameters.Add(new SqlParameter("@PrefixId", this.PrefixId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@MessageIn", this.MessageIn));
                cmd.Parameters.Add(new SqlParameter("@RequestId", this.RequestId));
                cmd.Parameters.Add(new SqlParameter("@RequestTime", this.RequestTime));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceId", this.SmsServiceId));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusId", this.SmsProcessStatusId));
                cmd.Parameters.Add("@MoId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MoId = Convert.ToInt32((cmd.Parameters["@MoId"].Value == null) ? 0 : (cmd.Parameters["@MoId"].Value));
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
        public byte UpdateProcessStatusId(ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SmsMos_UpdateProcessStatusId");
                cmd.CommandType = CommandType.StoredProcedure;               
                cmd.Parameters.Add(new SqlParameter("@MoId", this.MoId));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusId", this.SmsProcessStatusId));
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
        public List<SmsMos> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string UserId, string PrefixId, byte TelcoId, string MessageIn, string RequestId, 
                                    byte SmsServiceId, byte SmsProcessStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<SmsMos> RetVal = new List<SmsMos>();
            try
            {
                SqlCommand cmd = new SqlCommand("SmsMos_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@UserId", StringUtil.InjectionString(UserId)));
                cmd.Parameters.Add(new SqlParameter("@PrefixId", StringUtil.InjectionString(PrefixId)));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", TelcoId));
                cmd.Parameters.Add(new SqlParameter("@MessageIn", StringUtil.InjectionString(MessageIn)));
                cmd.Parameters.Add(new SqlParameter("@RequestId", StringUtil.InjectionString(RequestId)));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceId", SmsServiceId));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusId", SmsProcessStatusId));
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
        public List<SmsMos> Search(int ActUserId, string OrderBy, string UserId, string PrefixId, byte TelcoId, string MessageIn, string RequestId,
                                    byte SmsServiceId, byte SmsProcessStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 200;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, UserId, PrefixId, TelcoId, MessageIn, RequestId, SmsServiceId, SmsProcessStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}

