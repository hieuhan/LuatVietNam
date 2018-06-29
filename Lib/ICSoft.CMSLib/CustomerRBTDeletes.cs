using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Web.UI.WebControls;
using System.Threading;
using sms.common;
using sms.utils;
using sms.database;
namespace ICSoft.CMSLib
{
    public class CustomerRBTDeletes
    {   
    #region Private Properties
	    private int _CustomerRBTDeleteId;
	    private string _PhoneNumber;
	    private byte _ProcessStatusId;
	    private string _RBTCode;
	    private string _ReturnCode;
	    private string _ReturnDesc;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public CustomerRBTDeletes()
        {
            db = new DBAccess(CmsConstants.CONNECTION_STRING);
		}
        //-----------------------------------------------------------------        
        public CustomerRBTDeletes(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~CustomerRBTDeletes()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int CustomerRBTDeleteId
        {
		    get
                {
			        return _CustomerRBTDeleteId;
		        }
		    set
            {
			    _CustomerRBTDeleteId = value;
		    }
	    }
    
        public string PhoneNumber
		{
            get
            {
			    return _PhoneNumber;
		    }
		    set
            {
			    _PhoneNumber = value;
            }
		}    
        public byte ProcessStatusId
		{
            get
            {
			    return _ProcessStatusId;
		    }
		    set
            {
			    _ProcessStatusId = value;
            }
		}    
        public string RBTCode
		{
            get
            {
			    return _RBTCode;
		    }
		    set
            {
			    _RBTCode = value;
            }
		}    
        public string ReturnCode
		{
            get
            {
			    return _ReturnCode;
		    }
		    set
            {
			    _ReturnCode = value;
            }
		}    
        public string ReturnDesc
		{
            get
            {
			    return _ReturnDesc;
		    }
		    set
            {
			    _ReturnDesc = value;
            }
		}    
        public int CrUserId
		{
            get
            {
			    return _CrUserId;
		    }
		    set
            {
			    _CrUserId = value;
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
        private List<CustomerRBTDeletes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerRBTDeletes> l_CustomerRBTDeletes = new List<CustomerRBTDeletes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerRBTDeletes m_CustomerRBTDeletes = new CustomerRBTDeletes();
                    m_CustomerRBTDeletes.CustomerRBTDeleteId = smartReader.GetInt32("CustomerRBTDeleteId");
                    m_CustomerRBTDeletes.PhoneNumber = smartReader.GetString("PhoneNumber");
                    m_CustomerRBTDeletes.ProcessStatusId = smartReader.GetByte("ProcessStatusId");
                    m_CustomerRBTDeletes.RBTCode = smartReader.GetString("RBTCode");
                    m_CustomerRBTDeletes.ReturnCode = smartReader.GetString("ReturnCode");
                    m_CustomerRBTDeletes.ReturnDesc = smartReader.GetString("ReturnDesc");
                    m_CustomerRBTDeletes.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CustomerRBTDeletes.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_CustomerRBTDeletes.Add(m_CustomerRBTDeletes);
                }
                reader.Close();
                return l_CustomerRBTDeletes;
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
                SqlCommand cmd = new SqlCommand("CustomerRBTDeletes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@ProcessStatusId", this.ProcessStatusId));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
                cmd.Parameters.Add(new SqlParameter("@ReturnCode", this.ReturnCode));
                cmd.Parameters.Add(new SqlParameter("@ReturnDesc", this.ReturnDesc));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerRBTDeleteId", this.CustomerRBTDeleteId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerRBTDeleteId = int.Parse(cmd.Parameters["@CustomerRBTDeleteId"].Value.ToString());
                SysMessageId = Convert.ToInt32((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);                
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
                SqlCommand cmd = new SqlCommand("CustomerRBTDeletes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerRBTDeleteId",this.CustomerRBTDeleteId));
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
        public CustomerRBTDeletes Get()
        {
            CustomerRBTDeletes retVal = new CustomerRBTDeletes();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<CustomerRBTDeletes> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<CustomerRBTDeletes> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerRBTDeletes_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@ProcessStatusId", this.ProcessStatusId));
                cmd.Parameters.Add(new SqlParameter("@RBTCode", this.RBTCode));
                cmd.Parameters.Add(new SqlParameter("@ReturnCode", this.ReturnCode));
                cmd.Parameters.Add(new SqlParameter("@ReturnDesc", this.ReturnDesc));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<CustomerRBTDeletes> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int CustomerRBTDeleteId)
        {
            string RetVal = "";
            CustomerRBTDeletes m_CustomerRBTDeletes = new CustomerRBTDeletes();
            m_CustomerRBTDeletes.CustomerRBTDeleteId = CustomerRBTDeleteId;
            m_CustomerRBTDeletes = m_CustomerRBTDeletes.Get();
            RetVal = m_CustomerRBTDeletes.ReturnDesc;
            return RetVal;
        }
        
    #endregion
    } 
}

