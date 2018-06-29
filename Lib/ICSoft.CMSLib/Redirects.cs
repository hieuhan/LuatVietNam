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
    public class Redirects
    {   
    #region Private Properties
	    private int _RedirectId;
	    private string _RedirectName;
	    private string _SourceUrl;
	    private string _DestUrl;
	    private int _RedirectCount;
	    private DateTime _RedirectLastTime;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public Redirects()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Redirects(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Redirects()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int RedirectId
        {
		    get
                {
			        return _RedirectId;
		        }
		    set
            {
			    _RedirectId = value;
		    }
	    }
    
        public string RedirectName
		{
            get
            {
			    return _RedirectName;
		    }
		    set
            {
			    _RedirectName = value;
            }
		}    
        public string SourceUrl
		{
            get
            {
			    return _SourceUrl;
		    }
		    set
            {
			    _SourceUrl = value;
            }
		}    
        public string DestUrl
		{
            get
            {
			    return _DestUrl;
		    }
		    set
            {
			    _DestUrl = value;
            }
		}    
        public int RedirectCount
		{
            get
            {
			    return _RedirectCount;
		    }
		    set
            {
			    _RedirectCount = value;
            }
		}    
        public DateTime RedirectLastTime
		{
            get
            {
			    return _RedirectLastTime;
		    }
		    set
            {
			    _RedirectLastTime = value;
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
        private List<Redirects> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Redirects> l_Redirects = new List<Redirects>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Redirects m_Redirects = new Redirects();
                    m_Redirects.RedirectId = smartReader.GetInt32("RedirectId");
                    m_Redirects.RedirectName = smartReader.GetString("RedirectName");
                    m_Redirects.SourceUrl = smartReader.GetString("SourceUrl");
                    m_Redirects.DestUrl = smartReader.GetString("DestUrl");
                    m_Redirects.RedirectCount = smartReader.GetInt32("RedirectCount");
                    m_Redirects.RedirectLastTime = smartReader.GetDateTime("RedirectLastTime");
                    m_Redirects.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Redirects.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_Redirects.Add(m_Redirects);
                }
                reader.Close();
                return l_Redirects;
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
                SqlCommand cmd = new SqlCommand("Redirects_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RedirectName", this.RedirectName));
                cmd.Parameters.Add(new SqlParameter("@SourceUrl", this.SourceUrl));
                cmd.Parameters.Add(new SqlParameter("@DestUrl", this.DestUrl));
                cmd.Parameters.Add(new SqlParameter("@RedirectCount", this.RedirectCount));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@RedirectId", this.RedirectId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.RedirectId = int.Parse(cmd.Parameters["@RedirectId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Redirects_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RedirectId",this.RedirectId));
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
        public Redirects Get()
        {
            Redirects retVal = new Redirects();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<Redirects> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<Redirects> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Redirects_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@RedirectId", this.RedirectId));
                cmd.Parameters.Add(new SqlParameter("@RedirectName", this.RedirectName));
                cmd.Parameters.Add(new SqlParameter("@SourceUrl", this.SourceUrl));
                cmd.Parameters.Add(new SqlParameter("@DestUrl", this.DestUrl));
                cmd.Parameters.Add(new SqlParameter("@RedirectCount", this.RedirectCount));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Redirects> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //-------------------------------------------------------------- 

        public Redirects GetBySourceUrl(string SourceUrl)
        {
            Redirects RetVal = new Redirects();
            try
            {
                SqlCommand cmd = new SqlCommand("Redirects_GetBySourceUrl");
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.Add(new SqlParameter("@SourceUrl", SourceUrl));
                List<Redirects> list = Init(cmd);
                if(list.Count > 0)
                {
                    RetVal = list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int RedirectId)
        {
            string RetVal = "";
            Redirects m_Redirects = new Redirects();
            m_Redirects.RedirectId = RedirectId;
            m_Redirects = m_Redirects.Get();
            RetVal = m_Redirects.RedirectName;
            return RetVal;
        }
        
    #endregion
    } 
}

