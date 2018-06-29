
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
    public class TelcoCpRbtCodes
    {
        private int _TelcoCpRbtCodeId;
        private byte _TelcoId;
        private string _RBTCode;
        private short _CpId;
        private byte _StatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public TelcoCpRbtCodes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public TelcoCpRbtCodes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~TelcoCpRbtCodes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int TelcoCpRbtCodeId
        {
            get { return _TelcoCpRbtCodeId; }
            set { _TelcoCpRbtCodeId = value; }
        }
        //-----------------------------------------------------------------
        public byte TelcoId
        {
            get { return _TelcoId; }
            set { _TelcoId = value; }
        }
        //-----------------------------------------------------------------
        public string RBTCode
        {
            get { return _RBTCode; }
            set { _RBTCode = value; }
        }
        //-----------------------------------------------------------------
        public short CpId
        {
            get { return _CpId; }
            set { _CpId = value; }
        }
        //-----------------------------------------------------------------
        public byte StatusId
        {
            get { return _StatusId; }
            set { _StatusId = value; }
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

        private List<TelcoCpRbtCodes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<TelcoCpRbtCodes> l_TelcoCpRbtCodes = new List<TelcoCpRbtCodes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    TelcoCpRbtCodes m_TelcoCpRbtCodes = new TelcoCpRbtCodes(db.ConnectionString);
                    m_TelcoCpRbtCodes.TelcoCpRbtCodeId = smartReader.GetInt32("TelcoCpRbtCodeId");
                    m_TelcoCpRbtCodes.TelcoId = smartReader.GetByte("TelcoId");
                    m_TelcoCpRbtCodes.RBTCode = smartReader.GetString("RBTCode");
                    m_TelcoCpRbtCodes.CpId = smartReader.GetInt16("CpId");
                    m_TelcoCpRbtCodes.StatusId = smartReader.GetByte("StatusId");
                    m_TelcoCpRbtCodes.CrUserId = smartReader.GetInt32("CrUserId");
                    m_TelcoCpRbtCodes.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_TelcoCpRbtCodes.Add(m_TelcoCpRbtCodes);
                }
                reader.Close();
                return l_TelcoCpRbtCodes;
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
                SqlCommand cmd = new SqlCommand("TelcoCpRbtCodes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
                cmd.Parameters.Add(new SqlParameter("@CpId", this.CpId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add("@TelcoCpRbtCodeId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.TelcoCpRbtCodeId = (cmd.Parameters["@TelcoCpRbtCodeId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@TelcoCpRbtCodeId"].Value);
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
                SqlCommand cmd = new SqlCommand("TelcoCpRbtCodes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
                cmd.Parameters.Add(new SqlParameter("@CpId", this.CpId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@TelcoCpRbtCodeId", this.TelcoCpRbtCodeId));
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
                SqlCommand cmd = new SqlCommand("TelcoCpRbtCodes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
                cmd.Parameters.Add(new SqlParameter("@CpId", this.CpId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@TelcoCpRbtCodeId", this.TelcoCpRbtCodeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.TelcoCpRbtCodeId = (cmd.Parameters["@TelcoCpRbtCodeId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@TelcoCpRbtCodeId"].Value);
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
                SqlCommand cmd = new SqlCommand("TelcoCpRbtCodes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TelcoCpRbtCodeId", this.TelcoCpRbtCodeId));
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
        public void DeleteByTelcoIdAndCpId(byte Replicated, int ActUserId)
        {
            
            try
            {
                SqlCommand cmd = new SqlCommand("TelcoCpRbtCodes_Delete_QuickBy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@TelcoId", this.TelcoId));
                cmd.Parameters.Add(new SqlParameter("@CpId", this.CpId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-------------------------------------------------------------- 
        public List<TelcoCpRbtCodes> GetListByCpIdAndTelcoId()
        {
            List<TelcoCpRbtCodes> RetVal = new List<TelcoCpRbtCodes>();
            try
            {
                string sql = "SELECT * FROM TelcoCpRbtCodes WHERE CpId=" + CpId.ToString() + " AND TelcoId=" + TelcoId.ToString();
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
        public List<TelcoCpRbtCodes> GetListByTelcoCpRbtCodeId(int TelcoCpRbtCodeId)
        {
            List<TelcoCpRbtCodes> RetVal = new List<TelcoCpRbtCodes>();
            try
            {
                if (TelcoCpRbtCodeId > 0)
                {
                    string sql = "SELECT * FROM TelcoCpRbtCodes WHERE (TelcoCpRbtCodeId=" + TelcoCpRbtCodeId.ToString() + ")";
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
        public TelcoCpRbtCodes Get(int TelcoCpRbtCodeId)
        {
            TelcoCpRbtCodes RetVal = new TelcoCpRbtCodes();
            try
            {
                List<TelcoCpRbtCodes> list = GetListByTelcoCpRbtCodeId(TelcoCpRbtCodeId);
                if (list.Count > 0)
                {
                    RetVal = (TelcoCpRbtCodes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static TelcoCpRbtCodes Static_Get(int TelcoCpRbtCodeId, List<TelcoCpRbtCodes> list)
        {
            TelcoCpRbtCodes RetVal = new TelcoCpRbtCodes();
            try
            {
                RetVal = list.Find(i => i.TelcoCpRbtCodeId == TelcoCpRbtCodeId);
                if (RetVal == null) RetVal = new TelcoCpRbtCodes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public TelcoCpRbtCodes Get()
        {
            return Get(this.TelcoCpRbtCodeId);
        }
        //-------------------------------------------------------------- 
        public static TelcoCpRbtCodes Static_Get(int TelcoCpRbtCodeId)
        {
            return Static_Get(TelcoCpRbtCodeId);
        }
    }
}

