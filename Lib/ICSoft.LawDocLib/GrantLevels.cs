using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using sms.database;
namespace ICSoft.LawDocsLib
{
    public class GrantLevels
    {   
    #region Private Properties
	    private byte _GrantLevelId;
	    private string _GrantLevelName;
	    private string _GrantLevelDesc;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public GrantLevels()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public GrantLevels(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~GrantLevels()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public byte GrantLevelId
        {
		    get
                {
			        return _GrantLevelId;
		        }
		    set
            {
			    _GrantLevelId = value;
		    }
	    }
    
        public string GrantLevelName
		{
            get
            {
			    return _GrantLevelName;
		    }
		    set
            {
			    _GrantLevelName = value;
            }
		}    
        public string GrantLevelDesc
		{
            get
            {
			    return _GrantLevelDesc;
		    }
		    set
            {
			    _GrantLevelDesc = value;
            }
		}    
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<GrantLevels> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<GrantLevels> l_GrantLevels = new List<GrantLevels>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    GrantLevels m_GrantLevels = new GrantLevels();
                    m_GrantLevels.GrantLevelId = smartReader.GetByte("GrantLevelId");
                    m_GrantLevels.GrantLevelName = smartReader.GetString("GrantLevelName");
                    m_GrantLevels.GrantLevelDesc = smartReader.GetString("GrantLevelDesc");
                    l_GrantLevels.Add(m_GrantLevels);
                }
                reader.Close();
                return l_GrantLevels;
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
        
        //--------------------------------------------------------------     
        public GrantLevels Get()
        {
            GrantLevels retVal = new GrantLevels();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<GrantLevels> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<GrantLevels> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GrantLevels_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@GrantLevelId", this.GrantLevelId));
                cmd.Parameters.Add(new SqlParameter("@GrantLevelName", this.GrantLevelName));
                cmd.Parameters.Add(new SqlParameter("@GrantLevelDesc", this.GrantLevelDesc));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<GrantLevels> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(byte GrantLevelId)
        {
            string RetVal = "";
            GrantLevels m_GrantLevels = new GrantLevels();
            m_GrantLevels.GrantLevelId = GrantLevelId;
            m_GrantLevels = m_GrantLevels.Get();
            RetVal = m_GrantLevels.GrantLevelName;
            return RetVal;
        }
        public static List<GrantLevels> Static_GetList()
        {
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 100;
            int PageNumber = 0;
            int RowCount = 0;
            GrantLevels m_GrantLevels = new GrantLevels();
            return m_GrantLevels.GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
        }
    #endregion
    } 
}

