using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using sms.database;

namespace ICSoft.CMSLib
{
    public class Partner123PayResponseCodes
    {   
    #region Private Properties
	    private short _Partner123PayResponseCodeId;
	    private string _PartnerCode;
	    private string _PartnerCodeName;
	    private string _PartnerCodeDesc;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public Partner123PayResponseCodes()
        {
            db = new DBAccess(CmsConstants.EXT_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Partner123PayResponseCodes(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Partner123PayResponseCodes()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public short Partner123PayResponseCodeId
        {
		    get
                {
			        return _Partner123PayResponseCodeId;
		        }
		    set
            {
			    _Partner123PayResponseCodeId = value;
		    }
	    }
    
        public string PartnerCode
		{
            get
            {
			    return _PartnerCode;
		    }
		    set
            {
			    _PartnerCode = value;
            }
		}    
        public string PartnerCodeName
		{
            get
            {
			    return _PartnerCodeName;
		    }
		    set
            {
			    _PartnerCodeName = value;
            }
		}    
        public string PartnerCodeDesc
		{
            get
            {
			    return _PartnerCodeDesc;
		    }
		    set
            {
			    _PartnerCodeDesc = value;
            }
		}    
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<Partner123PayResponseCodes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Partner123PayResponseCodes> l_Partner123PayResponseCodes = new List<Partner123PayResponseCodes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Partner123PayResponseCodes m_Partner123PayResponseCodes = new Partner123PayResponseCodes();
                    m_Partner123PayResponseCodes.Partner123PayResponseCodeId = smartReader.GetInt16("Partner123PayResponseCodeId");
                    m_Partner123PayResponseCodes.PartnerCode = smartReader.GetString("PartnerCode");
                    m_Partner123PayResponseCodes.PartnerCodeName = smartReader.GetString("PartnerCodeName");
                    m_Partner123PayResponseCodes.PartnerCodeDesc = smartReader.GetString("PartnerCodeDesc");
                    l_Partner123PayResponseCodes.Add(m_Partner123PayResponseCodes);
                }
                reader.Close();
                return l_Partner123PayResponseCodes;
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
                SqlCommand cmd = new SqlCommand("Partner123PayResponseCodes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PartnerCode", this.PartnerCode));
                cmd.Parameters.Add(new SqlParameter("@PartnerCodeName", this.PartnerCodeName));
                cmd.Parameters.Add(new SqlParameter("@PartnerCodeDesc", this.PartnerCodeDesc));
                cmd.Parameters.Add(new SqlParameter("@Partner123PayResponseCodeId", this.Partner123PayResponseCodeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.Partner123PayResponseCodeId = short.Parse(cmd.Parameters["@Partner123PayResponseCodeId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Partner123PayResponseCodes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@Partner123PayResponseCodeId",this.Partner123PayResponseCodeId));
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
        public Partner123PayResponseCodes Get()
        {
            Partner123PayResponseCodes retVal = new Partner123PayResponseCodes();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<Partner123PayResponseCodes> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

       

        //-------------------------------------------------------------- 

        public List<Partner123PayResponseCodes> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Partner123PayResponseCodes_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@Partner123PayResponseCodeId", this.Partner123PayResponseCodeId));
                cmd.Parameters.Add(new SqlParameter("@PartnerCode", this.PartnerCode));
                cmd.Parameters.Add(new SqlParameter("@PartnerCodeName", this.PartnerCodeName));
                cmd.Parameters.Add(new SqlParameter("@PartnerCodeDesc", this.PartnerCodeDesc));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Partner123PayResponseCodes> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(short Partner123PayResponseCodeId)
        {
            string RetVal = "";
            Partner123PayResponseCodes m_Partner123PayResponseCodes = new Partner123PayResponseCodes();
            m_Partner123PayResponseCodes.Partner123PayResponseCodeId = Partner123PayResponseCodeId;
            m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
            RetVal = m_Partner123PayResponseCodes.PartnerCodeDesc;
            return RetVal;
        }
        
    #endregion
    } 
}

