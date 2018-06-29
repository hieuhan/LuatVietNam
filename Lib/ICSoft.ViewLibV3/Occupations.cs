
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;

namespace ICSoft.ViewLibV3
{
    public class Occupations
    {   
	    private short _OccupationId;
	    private string _OccupationName;
	    private string _OccupationDesc;
	    private string _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Occupations()
        {
            db = new DBAccess(Constants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Occupations(string constr)
        {
            db = new DBAccess ((constr == "") ? Constants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Occupations()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short OccupationId
        {
		    get { return _OccupationId; }
		    set { _OccupationId = value; }
	    }
        //-----------------------------------------------------------------
        public string OccupationName
		{
            get { return _OccupationName; }
		    set { _OccupationName = value; }
		}    
        //-----------------------------------------------------------------
        public string OccupationDesc
		{
            get { return _OccupationDesc; }
		    set { _OccupationDesc = value; }
		}    
        //-----------------------------------------------------------------
        public string CrUserId
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
    
        private List<Occupations> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Occupations> l_Occupations = new List<Occupations>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Occupations m_Occupations = new Occupations(db.ConnectionString);
                    m_Occupations.OccupationId = smartReader.GetInt16("OccupationId");
                    m_Occupations.OccupationName = smartReader.GetString("OccupationName");
                    m_Occupations.OccupationDesc = smartReader.GetString("OccupationDesc");
                    m_Occupations.CrUserId = smartReader.GetString("CrUserId");
                    m_Occupations.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Occupations.Add(m_Occupations);
                }
                reader.Close();
                return l_Occupations;
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
        public List<Occupations> GetListOrderBy(string OrderBy)
        {
            List<Occupations> RetVal = new List<Occupations>();
            try
            {
                OrderBy = StringUtil.injectionString(OrderBy).Trim();
                string sql = "SELECT * FROM Occupations " ;
                if (OrderBy.Length >0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
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
        public static List<Occupations> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Occupations m_Occupations = new Occupations(ConStr);
            return m_Occupations.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Occupations> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Occupations> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<Occupations> RetVal = new List<Occupations>();
            Occupations m_Occupations = new Occupations(ConStr);
            try
            {
                OrderBy = StringUtil.injectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_Occupations.GetListOrderBy(OrderBy);
                }               
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_Occupations.OccupationName = TextOptionAll;
                    m_Occupations.OccupationDesc = TextOptionAll;
                    RetVal.Insert(0, m_Occupations);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Occupations> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<Occupations> GetListByOccupationId(short OccupationId)
        {
            List<Occupations> RetVal = new List<Occupations>();
            try
            {
                if (OccupationId > 0)
                {
                    string sql = "SELECT * FROM Occupations WHERE (OccupationId=" + OccupationId.ToString() + ")";
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
        public Occupations Get(short OccupationId)
        {
            Occupations RetVal = new Occupations(db.ConnectionString);
            try
            {
                List<Occupations> list = GetListByOccupationId(OccupationId);
                if (list.Count > 0)
                {
                    RetVal = (Occupations)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Occupations Get()
        {
            return Get(this.OccupationId);
        }
        //-------------------------------------------------------------- 
        public static Occupations Static_Get(short OccupationId)
        {
            return Static_Get(OccupationId);
        }
        //-----------------------------------------------------------------------------
        public static Occupations Static_Get(short OccupationId, List<Occupations> lList)
        {
            Occupations RetVal = new Occupations();
            if (OccupationId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.OccupationId == OccupationId);
                if (RetVal == null) RetVal = new Occupations();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    } 
}