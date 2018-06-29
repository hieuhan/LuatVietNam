using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using sms.database;
namespace ICSoft.LawDocsLib
{
    public class DocGroups
    {   
    #region Private Properties
	    private byte _DocGroupId;
	    private string _DocGroupName;
	    private string _DocGroupDesc;

        
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public DocGroups()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public DocGroups(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~DocGroups()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public byte DocGroupId
        {
		    get
                {
			        return _DocGroupId;
		        }
		    set
            {
			    _DocGroupId = value;
		    }
	    }
    
        public string DocGroupName
		{
            get
            {
			    return _DocGroupName;
		    }
		    set
            {
			    _DocGroupName = value;
            }
		}    
        public string DocGroupDesc
		{
            get
            {
			    return _DocGroupDesc;
		    }
		    set
            {
			    _DocGroupDesc = value;
            }
		}    
    //
        public static byte VBPQ
        {
            get
            {
                return 1;
            }
            set
            {
                
            }
        }
        public static byte VBUBND
        {
            get
            {
                return 2;
            }
            set
            {

            }
        }
        public static byte TCVN
        {
            get
            {
                return 3;
            }
            set
            {

            }
        }
        public static byte TTHC
        {
            get
            {
                return 4;
            }
            set
            {

            }
        }
        public static byte VBHN
        {
            get
            {
                return 5;
            }
            set
            {

            }
        }
        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<DocGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocGroups> l_DocGroups = new List<DocGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocGroups m_DocGroups = new DocGroups();
                    m_DocGroups.DocGroupId = smartReader.GetByte("DocGroupId");
                    m_DocGroups.DocGroupName = smartReader.GetString("DocGroupName");
                    m_DocGroups.DocGroupDesc = smartReader.GetString("DocGroupDesc");
                    l_DocGroups.Add(m_DocGroups);
                }
                reader.Close();
                return l_DocGroups;
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
        public DocGroups Get()
        {
            DocGroups retVal = new DocGroups();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<DocGroups> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<DocGroups> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DocGroups_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupName", this.DocGroupName));
                cmd.Parameters.Add(new SqlParameter("@DocGroupDesc", this.DocGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<DocGroups> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(byte DocGroupId)
        {
            string RetVal = "";
            DocGroups m_DocGroups = new DocGroups();
            m_DocGroups.DocGroupId = DocGroupId;
            m_DocGroups = m_DocGroups.Get();
            RetVal = m_DocGroups.DocGroupName;
            return RetVal;
        }
        //-------------------------------------------------------------- 

        public static List<DocGroups> GetList()
        {
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 100;
            int PageNumber = 0;
            int RowCount = 0;
            try
            {
                DocGroups m_DocGroups = new DocGroups();
                List<DocGroups> list = m_DocGroups.GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    } 
}

