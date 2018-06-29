
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
    public class CPs
    {   
	    private short _CPId;
	    private string _CpName;
	    private string _CpDesc;
	    private string _PhoneNumber;
	    private string _MobileNumber;
	    private string _Email;
	    private string _Email2;
        public string CpInfo { get; set; }
	    private string _ContractNo;
	    private DateTime _ContractStartDate;
	    private DateTime _ContractEndDate;
	    private short _DisplayOrder;
	    private byte _StatusId;
        public int UserId { get; set; }
        private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public CPs()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public CPs(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~CPs()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short CPId
        {
		    get { return _CPId; }
		    set { _CPId = value; }
	    }
        //-----------------------------------------------------------------
        public string CpName
		{
            get { return _CpName; }
		    set { _CpName = value; }
		}    
        //-----------------------------------------------------------------
        public string CpDesc
		{
            get { return _CpDesc; }
		    set { _CpDesc = value; }
		}    
        //-----------------------------------------------------------------
        public string PhoneNumber
		{
            get { return _PhoneNumber; }
		    set { _PhoneNumber = value; }
		}    
        //-----------------------------------------------------------------
        public string MobileNumber
		{
            get { return _MobileNumber; }
		    set { _MobileNumber = value; }
		}    
        //-----------------------------------------------------------------
        public string Email
		{
            get { return _Email; }
		    set { _Email = value; }
		}    
        //-----------------------------------------------------------------
        public string Email2
		{
            get { return _Email2; }
		    set { _Email2 = value; }
		}    
        //-----------------------------------------------------------------
        public string ContractNo
		{
            get { return _ContractNo; }
		    set { _ContractNo = value; }
		}    
        //-----------------------------------------------------------------
        public DateTime ContractStartDate
		{
            get { return _ContractStartDate; }
		    set { _ContractStartDate = value; }
		}    
        //-----------------------------------------------------------------
        public DateTime ContractEndDate
		{
            get { return _ContractEndDate; }
		    set { _ContractEndDate = value; }
		}    
        //-----------------------------------------------------------------
        public short DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
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
    
        private List<CPs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CPs> l_CPs = new List<CPs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CPs m_CPs = new CPs(db.ConnectionString);
                    m_CPs.CPId = smartReader.GetInt16("CPId");
                    m_CPs.CpName = smartReader.GetString("CpName");
                    m_CPs.CpDesc = smartReader.GetString("CpDesc");
                    m_CPs.CpInfo = smartReader.GetString("CpInfo");
                    m_CPs.PhoneNumber = smartReader.GetString("PhoneNumber");
                    m_CPs.MobileNumber = smartReader.GetString("MobileNumber");
                    m_CPs.Email = smartReader.GetString("Email");
                    m_CPs.Email2 = smartReader.GetString("Email2");
                    m_CPs.ContractNo = smartReader.GetString("ContractNo");
                    m_CPs.ContractStartDate = smartReader.GetDateTime("ContractStartDate");
                    m_CPs.ContractEndDate = smartReader.GetDateTime("ContractEndDate");
                    m_CPs.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_CPs.UserId = smartReader.GetInt32("UserId");
                    m_CPs.StatusId = smartReader.GetByte("StatusId");
                    m_CPs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CPs.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_CPs.Add(m_CPs);
                }
                reader.Close();
                return l_CPs;
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
                SqlCommand cmd = new SqlCommand("CPs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CpName", this.CpName));
                cmd.Parameters.Add(new SqlParameter("@CpDesc", this.CpDesc));
                cmd.Parameters.Add(new SqlParameter("@CpInfo", this.CpInfo));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", this.MobileNumber));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Email2", this.Email2));
                cmd.Parameters.Add(new SqlParameter("@ContractNo", this.ContractNo));
                if (this.ContractStartDate != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@ContractStartDate", this.ContractStartDate));
                if (this.ContractEndDate != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@ContractEndDate", this.ContractEndDate));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add("@CPId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CPId = (cmd.Parameters["@CPId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@CPId"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("CPs_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CpName", this.CpName));
                cmd.Parameters.Add(new SqlParameter("@CpDesc", this.CpDesc));
                cmd.Parameters.Add(new SqlParameter("@CpInfo", this.CpInfo));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", this.MobileNumber));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Email2", this.Email2));
                cmd.Parameters.Add(new SqlParameter("@ContractNo", this.ContractNo));
                if (this.ContractStartDate != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@ContractStartDate", this.ContractStartDate));
                if (this.ContractEndDate != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@ContractEndDate", this.ContractEndDate));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CPId",this.CPId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal =Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("CPs_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CpName", this.CpName));
                cmd.Parameters.Add(new SqlParameter("@CpDesc", this.CpDesc));
                cmd.Parameters.Add(new SqlParameter("@CpInfo", this.CpInfo));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", this.MobileNumber));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Email2", this.Email2));
                cmd.Parameters.Add(new SqlParameter("@ContractNo", this.ContractNo));
                if (this.ContractStartDate != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@ContractStartDate", this.ContractStartDate));
                if (this.ContractEndDate != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@ContractEndDate", this.ContractEndDate));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@UserId", this.UserId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CPId", this.CPId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CPId = (cmd.Parameters["@CPId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@CPId"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("CPs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CPId",this.CPId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<CPs> GetList()
        {
            List<CPs> RetVal = new List<CPs>();
            try
            {
                string sql = "SELECT * FROM CPs ORDER BY CpName";
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
        public List<CPs> GetListByCPId(short CPId)
        {
            List<CPs> RetVal = new List<CPs>();
            try
            {
                if (CPId > 0)
                {
                    string sql = "SELECT * FROM CPs WHERE (CPId=" + CPId.ToString() + ")";
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
        public CPs GetByUserId(int UserId)
        {
            CPs RetVal = new CPs();
            try
            {
                string sql = "SELECT * FROM CPs WHERE (UserId=" + UserId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                List<CPs> list = Init(cmd);
                if (list != null && list.Count > 0) RetVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public CPs Get(short CPId)
        {
            CPs RetVal = new CPs();
            try
            {
                List<CPs> list = GetListByCPId(CPId);
                if (list.Count > 0)
                {
                    RetVal = (CPs)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static CPs Static_Get(short CPId, List<CPs> list)
        {
            CPs RetVal = new CPs();
            try
            {
                RetVal = list.Find(i => i.CPId == CPId);
                if (RetVal == null) RetVal = new CPs();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public CPs Get()
        {
            return Get(this.CPId);
        }
        //-------------------------------------------------------------- 
        public static CPs Static_Get(short CPId)
        {
            return Static_Get(CPId);
        }
        //--------------------------------------------------------------     
        public static List<CPs> Static_GetList()
        {
            List<CPs> RetVal = new List<CPs>();
            try
            {
                CPs m_CPs = new CPs();
                RetVal = m_CPs.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<CPs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            List<CPs> RetVal = new List<CPs>();
            try
            {
                SqlCommand cmd = new SqlCommand("CPs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@CpName", this.CpName));
                cmd.Parameters.Add(new SqlParameter("@CpDesc", this.CpDesc));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@ContractNo", this.ContractNo));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                if (DateFrom!="") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo!="") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
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
        public List<CPs> GetExpire(int ActUserId, int RowAmount, int PageIndex, int DayLeft, ref int RowCount)
        {
            List<CPs> RetVal = new List<CPs>();
            try
            {
                SqlCommand cmd = new SqlCommand("CPs_GetExpire");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@CpName", this.CpName));
                cmd.Parameters.Add(new SqlParameter("@DayLeft", DayLeft));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
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
    } 
}

