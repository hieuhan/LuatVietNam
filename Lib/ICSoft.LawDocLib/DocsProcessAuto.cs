using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using sms.database;

namespace ICSoft.LawDocsLib
{
    public class DocsProcessAuto
    {   
    #region Private Properties
	    private int _DocProcessAutoId;
	    private int _DocId;
	    private string _DocName;
	    private string _DocIdentity;
	    private string _DocFilePath;
	    private DateTime _ProcessTime;
	    private byte _ProcessStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public DocsProcessAuto()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public DocsProcessAuto(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~DocsProcessAuto()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int DocProcessAutoId
        {
		    get
                {
			        return _DocProcessAutoId;
		        }
		    set
            {
			    _DocProcessAutoId = value;
		    }
	    }
    
        public int DocId
		{
            get
            {
			    return _DocId;
		    }
		    set
            {
			    _DocId = value;
            }
		}    
        public string DocName
		{
            get
            {
			    return _DocName;
		    }
		    set
            {
			    _DocName = value;
            }
		}    
        public string DocIdentity
		{
            get
            {
			    return _DocIdentity;
		    }
		    set
            {
			    _DocIdentity = value;
            }
		}    
        public string DocFilePath
		{
            get
            {
			    return _DocFilePath;
		    }
		    set
            {
			    _DocFilePath = value;
            }
		}    
        public DateTime ProcessTime
		{
            get
            {
			    return _ProcessTime;
		    }
		    set
            {
			    _ProcessTime = value;
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
        private List<DocsProcessAuto> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocsProcessAuto> l_DocsProcessAuto = new List<DocsProcessAuto>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocsProcessAuto m_DocsProcessAuto = new DocsProcessAuto();
                    m_DocsProcessAuto.DocProcessAutoId = smartReader.GetInt32("DocProcessAutoId");
                    m_DocsProcessAuto.DocId = smartReader.GetInt32("DocId");
                    m_DocsProcessAuto.DocName = smartReader.GetString("DocName");
                    m_DocsProcessAuto.DocIdentity = smartReader.GetString("DocIdentity");
                    m_DocsProcessAuto.DocFilePath = smartReader.GetString("DocFilePath");
                    m_DocsProcessAuto.ProcessTime = smartReader.GetDateTime("ProcessTime");
                    m_DocsProcessAuto.ProcessStatusId = smartReader.GetByte("ProcessStatusId");
                    m_DocsProcessAuto.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocsProcessAuto.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_DocsProcessAuto.Add(m_DocsProcessAuto);
                }
                reader.Close();
                return l_DocsProcessAuto;
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
                SqlCommand cmd = new SqlCommand("DocsProcessAuto_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocName", this.DocName));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", this.DocIdentity));
                cmd.Parameters.Add(new SqlParameter("@DocFilePath", this.DocFilePath));
                if (this.ProcessTime > DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@ProcessTime", this.ProcessTime));
                cmd.Parameters.Add(new SqlParameter("@ProcessStatusId", this.ProcessStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DocProcessAutoId", this.DocProcessAutoId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocProcessAutoId = int.Parse(cmd.Parameters["@DocProcessAutoId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("DocsProcessAuto_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocProcessAutoId",this.DocProcessAutoId));
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
        public DocsProcessAuto Get()
        {
            DocsProcessAuto retVal = new DocsProcessAuto();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<DocsProcessAuto> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<DocsProcessAuto> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DocsProcessAuto_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@DocProcessAutoId", this.DocProcessAutoId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@DocName", this.DocName));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", this.DocIdentity));
                cmd.Parameters.Add(new SqlParameter("@DocFilePath", this.DocFilePath));
                cmd.Parameters.Add(new SqlParameter("@ProcessStatusId", this.ProcessStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<DocsProcessAuto> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int DocProcessAutoId)
        {
            string RetVal = "";
            DocsProcessAuto m_DocsProcessAuto = new DocsProcessAuto();
            m_DocsProcessAuto.DocProcessAutoId = DocProcessAutoId;
            m_DocsProcessAuto = m_DocsProcessAuto.Get();
            RetVal = m_DocsProcessAuto.DocName;
            return RetVal;
        }
        
    #endregion
    } 
}

