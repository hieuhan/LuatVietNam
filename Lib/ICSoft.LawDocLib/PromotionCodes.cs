using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Globalization;
using sms.database;
namespace ICSoft.LawDocsLib
{
    public class PromotionCodes
    {   
    #region Private Properties
	    private int _PromotionCodeId;
	    private int _PromotionId;
	    private string _PromotionCode;
        private int _UsingCount;
        private DateTime _UsingTime;
        private int _UsingCustomerId;
        private DateTime _CrDateTime;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public PromotionCodes()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public PromotionCodes(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~PromotionCodes()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int PromotionCodeId
        {
		    get
                {
			        return _PromotionCodeId;
		        }
		    set
            {
			    _PromotionCodeId = value;
		    }
	    }
    
        public int PromotionId
		{
            get
            {
			    return _PromotionId;
		    }
		    set
            {
			    _PromotionId = value;
            }
		}    
        public string PromotionCode
		{
            get
            {
			    return _PromotionCode;
		    }
		    set
            {
			    _PromotionCode = value;
            }
		}    
        public int UsingCount
		{
            get
            {
			    return _UsingCount;
		    }
		    set
            {
			    _UsingCount = value;
            }
		}
        public int UsingCustomerId
        {
            get
            {
                return _UsingCustomerId;
            }
            set
            {
                _UsingCustomerId = value;
            }
        }
        public DateTime UsingTime
        {
            get
            {
                return _UsingTime;
            }
            set
            {
                _UsingTime = value;
            }
        }
        public DateTime CrDateTime
        {
            get
            {
                return _CrDateTime;
            }
            set
            {
                _CrDateTime = value;
            }
        }    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<PromotionCodes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<PromotionCodes> l_PromotionCodes = new List<PromotionCodes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PromotionCodes m_PromotionCodes = new PromotionCodes();
                    m_PromotionCodes.PromotionCodeId = smartReader.GetInt32("PromotionCodeId");
                    m_PromotionCodes.PromotionId = smartReader.GetInt32("PromotionId");
                    m_PromotionCodes.PromotionCode = smartReader.GetString("PromotionCode");
                    m_PromotionCodes.UsingCount = smartReader.GetInt32("UsingCount");
                    m_PromotionCodes.UsingCustomerId = smartReader.GetInt32("UsingCustomerId");
                    m_PromotionCodes.UsingTime = smartReader.GetDateTime("UsingTime");
                    m_PromotionCodes.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_PromotionCodes.Add(m_PromotionCodes);
                }
                reader.Close();
                return l_PromotionCodes;
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
        public byte Insert(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte Update(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public void PromotionCodes_UpdateUsing(string PromotionCode, int CustomerId, string CustomerName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PromotionCodes_UpdateUsing");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PromotionCode", PromotionCode));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------            
        public string PromotionCodes_Check(string PromotionCode, ref float Amount, ref float PercentDecrease)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PromotionCodes_Check");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PromotionCode", PromotionCode));
                cmd.Parameters.Add("@Amount", SqlDbType.Float).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@PercentDecrease", SqlDbType.Float).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Msg", SqlDbType.NVarChar,500).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                Amount = Convert.ToSingle((cmd.Parameters["@Amount"].Value ?? "0").ToString());
                PercentDecrease = Convert.ToSingle((cmd.Parameters["@PercentDecrease"].Value ?? "0").ToString());
                return cmd.Parameters["@Msg"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PromotionCodes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PromotionId", this.PromotionId));
                cmd.Parameters.Add(new SqlParameter("@PromotionCode", this.PromotionCode));
                cmd.Parameters.Add(new SqlParameter("@UsingCount", this.UsingCount));
                cmd.Parameters.Add(new SqlParameter("@PromotionCodeId", this.PromotionCodeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PromotionCodeId = int.Parse(cmd.Parameters["@PromotionCodeId"].Value.ToString());
                SysMessageId = Convert.ToInt32((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public int InsertAuto(byte Replicated, int ActUserId, byte CodeLength)
        {
            int RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PromotionCodes_InsertAuto");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PromotionId", this.PromotionId));
                cmd.Parameters.Add(new SqlParameter("@CodeLength", CodeLength));
                cmd.Parameters.Add("@NumOfCodeSuccess", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToInt32((cmd.Parameters["@NumOfCodeSuccess"].Value == null) ? "0" : cmd.Parameters["@NumOfCodeSuccess"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PromotionCodes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PromotionCodeId",this.PromotionCodeId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = int.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                RetVal = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public PromotionCodes Get()
        {
            PromotionCodes retVal = new PromotionCodes();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<PromotionCodes> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<PromotionCodes> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("PromotionCodes_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@PromotionCodeId", this.PromotionCodeId));
                cmd.Parameters.Add(new SqlParameter("@PromotionId", this.PromotionId));
                cmd.Parameters.Add(new SqlParameter("@PromotionCode", this.PromotionCode));
                cmd.Parameters.Add(new SqlParameter("@UsingCount", this.UsingCount));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<PromotionCodes> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int PromotionCodeId)
        {
            string RetVal = "";
            PromotionCodes m_PromotionCodes = new PromotionCodes();
            m_PromotionCodes.PromotionCodeId = PromotionCodeId;
            m_PromotionCodes = m_PromotionCodes.Get();
            RetVal = m_PromotionCodes.PromotionCode;
            return RetVal;
        }
        
    #endregion
    } 
}

