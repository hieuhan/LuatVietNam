
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
    public class SmsMts
    {
        private int _MtId;
        private int _MoId;
        private string _UserId;
        private string _PrefixId;
        private byte _TelcoId;
        private string _RequestId;
        private DateTime _RequestTime;
        private byte _SmsServiceId;
        private string _MessageIn;
        private string _MessageOut;
        private byte _MsgTypeId;
        private byte _FeeTypeId;
        private byte _SmsProcessStatusId;
        private DateTime _CrDateTime;
        private DateTime _SendTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SmsMts()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SmsMts(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SmsMts()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MtId
        {
            get { return _MtId; }
            set { _MtId = value; }
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
        public string MessageIn
        {
            get { return _MessageIn; }
            set { _MessageIn = value; }
        }
        //-----------------------------------------------------------------
        public string MessageOut
        {
            get { return _MessageOut; }
            set { _MessageOut = value; }
        }
        //-----------------------------------------------------------------
        public byte MsgTypeId
        {
            get { return _MsgTypeId; }
            set { _MsgTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte FeeTypeId
        {
            get { return _FeeTypeId; }
            set { _FeeTypeId = value; }
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
        public DateTime SendTime
        {
            get { return _SendTime; }
            set { _SendTime = value; }
        }
        //-----------------------------------------------------------------

        private List<SmsMts> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SmsMts> l_SmsMts = new List<SmsMts>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SmsMts m_SmsMts = new SmsMts(db.ConnectionString);
                    m_SmsMts.MtId = smartReader.GetInt32("MtId");
                    m_SmsMts.MoId = smartReader.GetInt32("MoId");
                    m_SmsMts.UserId = smartReader.GetString("UserId");
                    m_SmsMts.PrefixId = smartReader.GetString("PrefixId");
                    m_SmsMts.TelcoId = smartReader.GetByte("TelcoId");
                    m_SmsMts.RequestId = smartReader.GetString("RequestId");
                    m_SmsMts.RequestTime = smartReader.GetDateTime("RequestTime");
                    m_SmsMts.SmsServiceId = smartReader.GetByte("SmsServiceId");
                    m_SmsMts.MessageIn = smartReader.GetString("MessageIn");
                    m_SmsMts.MessageOut = smartReader.GetString("MessageOut");
                    m_SmsMts.MsgTypeId = smartReader.GetByte("MsgTypeId");
                    m_SmsMts.FeeTypeId = smartReader.GetByte("FeeTypeId");
                    m_SmsMts.SmsProcessStatusId = smartReader.GetByte("SmsProcessStatusId");
                    m_SmsMts.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_SmsMts.SendTime = smartReader.GetDateTime("SendTime");

                    l_SmsMts.Add(m_SmsMts);
                }
                reader.Close();
                return l_SmsMts;
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
                SqlCommand cmd = new SqlCommand("SmsMts_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MoId", this.MoId));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                cmd.Parameters.Add(new SqlParameter("@PrefixId", this.PrefixId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RequestId", this.RequestId));
                cmd.Parameters.Add(new SqlParameter("@RequestTime", this.RequestTime));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceId", this.SmsServiceId));
                cmd.Parameters.Add(new SqlParameter("@MessageIn", this.MessageIn));
                cmd.Parameters.Add(new SqlParameter("@MessageOut", this.MessageOut));
                cmd.Parameters.Add(new SqlParameter("@MsgTypeId", this.MsgTypeId));
                cmd.Parameters.Add(new SqlParameter("@FeeTypeId", this.FeeTypeId));
                cmd.Parameters.Add(new SqlParameter("@SmsProcessStatusId", this.SmsProcessStatusId));
                cmd.Parameters.Add(new SqlParameter("@SendTime", this.SendTime));
                cmd.Parameters.Add("@MtId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MtId = Convert.ToInt32((cmd.Parameters["@MtId"].Value == null) ? 0 : (cmd.Parameters["@MtId"].Value));
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
        public byte ReSend(byte Replicated, int ActUserId, int MtId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SmsMts_Resend");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MtId", MtId));
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
        public byte UpdateProcessStatusId(ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SmsMts_UpdateProcessStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MtId", this.MtId));
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
        public List<SmsMts> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int MoId, string UserId, string PrefixId, byte TelcoId, string RequestId,  
                                    byte SmsServiceId, string MessageOut, byte MsgTypeId, byte FeeTypeId, byte SmsProcessStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<SmsMts> RetVal = new List<SmsMts>();
            try
            {
                SqlCommand cmd = new SqlCommand("SmsMts_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MoId", MoId));
                cmd.Parameters.Add(new SqlParameter("@UserId", StringUtil.InjectionString(UserId)));
                cmd.Parameters.Add(new SqlParameter("@PrefixId", StringUtil.InjectionString(PrefixId)));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RequestId", StringUtil.InjectionString(RequestId)));
                cmd.Parameters.Add(new SqlParameter("@SmsServiceId", SmsServiceId));
                cmd.Parameters.Add(new SqlParameter("@MessageOut", StringUtil.InjectionString(MessageOut)));
                cmd.Parameters.Add(new SqlParameter("@MsgTypeId", MsgTypeId));
                cmd.Parameters.Add(new SqlParameter("@FeeTypeId", FeeTypeId));
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
        public List<SmsMts> Search(int ActUserId, string OrderBy, int MoId, string UserId, string PrefixId, byte TelcoId, string RequestId,
                                    byte SmsServiceId, string MessageOut, byte MsgTypeId, byte FeeTypeId, byte SmsProcessStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 200;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, MoId, UserId, PrefixId, TelcoId, RequestId,  
                                    SmsServiceId, MessageOut, MsgTypeId, FeeTypeId, SmsProcessStatusId, DateFrom, DateTo, ref RowCount);
        }
    }
}

