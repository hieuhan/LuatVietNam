using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using sms.database;
namespace ICSoft.LawDocsLib
{
    public class LawerGroups
    {   
    #region Private Properties
	    private byte _LawerGroupId;
	    private string _LawerGroupName;
	    private string _LawerGroupDesc;

        
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public LawerGroups()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public LawerGroups(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~LawerGroups()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public byte LawerGroupId
        {
		    get
                {
			        return _LawerGroupId;
		        }
		    set
            {
			    _LawerGroupId = value;
		    }
	    }
    
        public string LawerGroupName
		{
            get
            {
			    return _LawerGroupName;
		    }
		    set
            {
			    _LawerGroupName = value;
            }
		}    
        public string LawerGroupDesc
		{
            get
            {
			    return _LawerGroupDesc;
		    }
		    set
            {
			    _LawerGroupDesc = value;
            }
		}    
        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<LawerGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<LawerGroups> l_LawerGroups = new List<LawerGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    LawerGroups m_LawerGroups = new LawerGroups();
                    m_LawerGroups.LawerGroupId = smartReader.GetByte("LawerGroupId");
                    m_LawerGroups.LawerGroupName = smartReader.GetString("LawerGroupName");
                    m_LawerGroups.LawerGroupDesc = smartReader.GetString("LawerGroupDesc");
                    l_LawerGroups.Add(m_LawerGroups);
                }
                reader.Close();
                return l_LawerGroups;
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
        public List<LawerGroups> GetList()
        {
            List<LawerGroups> RetVal = new List<LawerGroups>();
            try
            {
                string sql = "SELECT * FROM LawerGroups";
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
        public static List<LawerGroups> Static_GetList()
        {
            List<LawerGroups> RetVal = new List<LawerGroups>();
            try
            {
                LawerGroups m_LawerGroups = new LawerGroups();
                RetVal = m_LawerGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------   
        public static List<LawerGroups> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            LawerGroups m_LawerGroups = new LawerGroups(ConStr);
            List<LawerGroups> RetVal = m_LawerGroups.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_LawerGroups.LawerGroupName = TextOptionAll;
                m_LawerGroups.LawerGroupDesc = TextOptionAll;
                RetVal.Insert(0, m_LawerGroups);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<LawerGroups> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        #endregion
    } 
}

