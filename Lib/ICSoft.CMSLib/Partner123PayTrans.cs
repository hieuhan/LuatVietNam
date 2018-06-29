using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using sms.database;

namespace ICSoft.CMSLib
{
    public class Partner123PayTrans
    {   
    #region Private Properties
	    private int _Partner123PayTranId;
	    private int _CustomerId;
	    private int _OrderId;
	    private int _PaymentTransactionId;
	    private string _Partner123PayTransactionId;
	    private string _clientIP;
	    private int _totalAmount;
	    private string _ResponseJson;
	    private string _redirectURL;
	    private string _returnCode;
	    private string _returnDesc;
	    private DateTime _CrDateTime;
	    private DateTime _ResponseDateTime;
        private DateTime _StatusUpdateTime;
        private byte _TransactionStatusId;
        private string _OrderStatus;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public Partner123PayTrans()
        {
            db = new DBAccess(CmsConstants.EXT_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Partner123PayTrans(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Partner123PayTrans()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int Partner123PayTranId
        {
		    get
                {
			        return _Partner123PayTranId;
		        }
		    set
            {
			    _Partner123PayTranId = value;
		    }
	    }
    
        public int CustomerId
		{
            get
            {
			    return _CustomerId;
		    }
		    set
            {
			    _CustomerId = value;
            }
		}    
        public int OrderId
		{
            get
            {
			    return _OrderId;
		    }
		    set
            {
			    _OrderId = value;
            }
		}    
        public int PaymentTransactionId
		{
            get
            {
			    return _PaymentTransactionId;
		    }
		    set
            {
			    _PaymentTransactionId = value;
            }
		}    
        public string Partner123PayTransactionId
		{
            get
            {
			    return _Partner123PayTransactionId;
		    }
		    set
            {
			    _Partner123PayTransactionId = value;
            }
		}    
        public string clientIP
		{
            get
            {
			    return _clientIP;
		    }
		    set
            {
			    _clientIP = value;
            }
		}    
        public int totalAmount
		{
            get
            {
			    return _totalAmount;
		    }
		    set
            {
			    _totalAmount = value;
            }
		}    
        public string ResponseJson
		{
            get
            {
			    return _ResponseJson;
		    }
		    set
            {
			    _ResponseJson = value;
            }
		}    
        public string redirectURL
		{
            get
            {
			    return _redirectURL;
		    }
		    set
            {
			    _redirectURL = value;
            }
		}    
        public string returnCode
		{
            get
            {
			    return _returnCode;
		    }
		    set
            {
			    _returnCode = value;
            }
		}    
        public string returnDesc
		{
            get
            {
			    return _returnDesc;
		    }
		    set
            {
			    _returnDesc = value;
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
        public DateTime ResponseDateTime
		{
            get
            {
			    return _ResponseDateTime;
		    }
		    set
            {
			    _ResponseDateTime = value;
            }
		}

        public DateTime StatusUpdateTime
        {
            get
            {
                return _StatusUpdateTime;
            }

            set
            {
                _StatusUpdateTime = value;
            }
        }

        public byte TransactionStatusId
        {
            get
            {
                return _TransactionStatusId;
            }

            set
            {
                _TransactionStatusId = value;
            }
        }

        public string OrderStatus
        {
            get
            {
                return _OrderStatus;
            }

            set
            {
                _OrderStatus = value;
            }
        }



        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<Partner123PayTrans> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Partner123PayTrans> l_Partner123PayTrans = new List<Partner123PayTrans>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
                    m_Partner123PayTrans.Partner123PayTranId = smartReader.GetInt32("Partner123PayTranId");
                    m_Partner123PayTrans.CustomerId = smartReader.GetInt32("CustomerId");
                    m_Partner123PayTrans.OrderId = smartReader.GetInt32("OrderId");
                    m_Partner123PayTrans.PaymentTransactionId = smartReader.GetInt32("PaymentTransactionId");
                    m_Partner123PayTrans.Partner123PayTransactionId = smartReader.GetString("Partner123PayTransactionId");
                    m_Partner123PayTrans.clientIP = smartReader.GetString("clientIP");
                    m_Partner123PayTrans.totalAmount = smartReader.GetInt32("totalAmount");
                    m_Partner123PayTrans.ResponseJson = smartReader.GetString("ResponseJson");
                    m_Partner123PayTrans.redirectURL = smartReader.GetString("redirectURL");
                    m_Partner123PayTrans.returnCode = smartReader.GetString("returnCode");
                    m_Partner123PayTrans.returnDesc = smartReader.GetString("returnDesc");
                    m_Partner123PayTrans.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Partner123PayTrans.ResponseDateTime = smartReader.GetDateTime("ResponseDateTime");
                    m_Partner123PayTrans.StatusUpdateTime = smartReader.GetDateTime("StatusUpdateTime");
                    m_Partner123PayTrans.TransactionStatusId = smartReader.GetByte("TransactionStatusId");
                    m_Partner123PayTrans.OrderStatus = smartReader.GetString("OrderStatus");
                    l_Partner123PayTrans.Add(m_Partner123PayTrans);
                }
                reader.Close();
                return l_Partner123PayTrans;
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Partner123PayTrans_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTransactionId", this.PaymentTransactionId));
                cmd.Parameters.Add(new SqlParameter("@Partner123PayTransactionId", this.Partner123PayTransactionId));
                cmd.Parameters.Add(new SqlParameter("@clientIP", this.clientIP));
                cmd.Parameters.Add(new SqlParameter("@totalAmount", this.totalAmount));
                cmd.Parameters.Add(new SqlParameter("@ResponseJson", this.ResponseJson));
                cmd.Parameters.Add(new SqlParameter("@redirectURL", this.redirectURL));
                cmd.Parameters.Add(new SqlParameter("@returnCode", this.returnCode));
                cmd.Parameters.Add(new SqlParameter("@returnDesc", this.returnDesc));
                cmd.Parameters.Add(new SqlParameter("@Partner123PayTranId", this.Partner123PayTranId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Partner123PayTranId = int.Parse(cmd.Parameters["@Partner123PayTranId"].Value.ToString());
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
        public byte Update_TransactionStatusId(int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Partner123PayTrans_Update_TransactionStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Partner123PayTransactionId", this.Partner123PayTransactionId));
                cmd.Parameters.Add(new SqlParameter("@OrderStatus", this.OrderStatus));
                cmd.Parameters.Add(new SqlParameter("@TransactionStatusId", this.TransactionStatusId));
                cmd.Parameters.Add(new SqlParameter("@Partner123PayTranId", this.Partner123PayTranId));
                db.ExecuteSQL(cmd);

                RetVal = 1;
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
                SqlCommand cmd = new SqlCommand("Partner123PayTrans_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@Partner123PayTranId",this.Partner123PayTranId));
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
        public Partner123PayTrans Get()
        {
            Partner123PayTrans retVal = new Partner123PayTrans();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<Partner123PayTrans> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        
        //-------------------------------------------------------------- 

        public List<Partner123PayTrans> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Partner123PayTrans_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@Partner123PayTranId", this.Partner123PayTranId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@OrderId", this.OrderId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTransactionId", this.PaymentTransactionId));
                cmd.Parameters.Add(new SqlParameter("@Partner123PayTransactionId", this.Partner123PayTransactionId));
                cmd.Parameters.Add(new SqlParameter("@clientIP", this.clientIP));
                cmd.Parameters.Add(new SqlParameter("@totalAmount", this.totalAmount));
                cmd.Parameters.Add(new SqlParameter("@ResponseJson", this.ResponseJson));
                cmd.Parameters.Add(new SqlParameter("@redirectURL", this.redirectURL));
                cmd.Parameters.Add(new SqlParameter("@returnCode", this.returnCode));
                cmd.Parameters.Add(new SqlParameter("@returnDesc", this.returnDesc));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Partner123PayTrans> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int Partner123PayTranId)
        {
            string RetVal = "";
            Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
            m_Partner123PayTrans.Partner123PayTranId = Partner123PayTranId;
            m_Partner123PayTrans = m_Partner123PayTrans.Get();
            RetVal = m_Partner123PayTrans.returnDesc;
            return RetVal;
        }
        
    #endregion
    } 
}

