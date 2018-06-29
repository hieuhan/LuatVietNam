
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSLib
{
    public class OrganOccupations
    {   
	    private short _OrganOccupationId;
	    private string _OrganOccupationName;
	    private string _OrganOccupationDesc;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        public byte LevelId { get; set; }
        //-----------------------------------------------------------------
		public OrganOccupations()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public OrganOccupations(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~OrganOccupations()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short OrganOccupationId
        {
		    get { return _OrganOccupationId; }
		    set { _OrganOccupationId = value; }
	    }
        //-----------------------------------------------------------------
        public string OrganOccupationName
		{
            get { return _OrganOccupationName; }
		    set { _OrganOccupationName = value; }
		}    
        //-----------------------------------------------------------------
        public string OrganOccupationDesc
		{
            get { return _OrganOccupationDesc; }
		    set { _OrganOccupationDesc = value; }
		}    
        //-----------------------------------------------------------------
        public int CrUserId
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
    
        private List<OrganOccupations> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<OrganOccupations> l_OrganOccupations = new List<OrganOccupations>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    OrganOccupations m_OrganOccupations = new OrganOccupations(db.ConnectionString);
                    m_OrganOccupations.OrganOccupationId = smartReader.GetInt16("OrganOccupationId");
                    m_OrganOccupations.OrganOccupationName = smartReader.GetString("OrganOccupationName");
                    m_OrganOccupations.OrganOccupationDesc = smartReader.GetString("OrganOccupationDesc");
                    m_OrganOccupations.CrUserId = smartReader.GetInt32("CrUserId");
                    m_OrganOccupations.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_OrganOccupations.Add(m_OrganOccupations);
                }
                reader.Close();
                return l_OrganOccupations;
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
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("OrganOccupations_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationName", this.OrganOccupationName));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationDesc", this.OrganOccupationDesc));
                cmd.Parameters.Add("@OrganOccupationId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.OrganOccupationId =Convert.ToInt16((cmd.Parameters["@OrganOccupationId"].Value == null) ? 0 : (cmd.Parameters["@OrganOccupationId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("OrganOccupations_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationName", this.OrganOccupationName));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationDesc", this.OrganOccupationDesc));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationId",this.OrganOccupationId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal =Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("OrganOccupations_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationId",this.OrganOccupationId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<OrganOccupations> GetList()
        {
            List<OrganOccupations> RetVal = new List<OrganOccupations>();
            try
            {
                string sql = "SELECT * FROM OrganOccupations";
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
        public static List<OrganOccupations> Static_GetList(string ConStr)
        {
            List<OrganOccupations> RetVal = new List<OrganOccupations>();
            try
            {
                OrganOccupations m_OrganOccupations = new OrganOccupations(ConStr);
                RetVal = m_OrganOccupations.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<OrganOccupations> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<OrganOccupations> Static_GetListAll(string ConStr ,string TextOptionAll = "...")
        {
            OrganOccupations m_OrganOccupations = new OrganOccupations(ConStr);
            List<OrganOccupations> RetVal = m_OrganOccupations.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_OrganOccupations.OrganOccupationName = TextOptionAll;
                m_OrganOccupations.OrganOccupationDesc = TextOptionAll;
                RetVal.Insert(0, m_OrganOccupations);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<OrganOccupations> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<OrganOccupations> GetListOrderBy(string OrderBy)
        {
            List<OrganOccupations> RetVal = new List<OrganOccupations>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM OrganOccupations " ;
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
        public static List<OrganOccupations> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            OrganOccupations m_OrganOccupations = new OrganOccupations(ConStr);
            return m_OrganOccupations.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<OrganOccupations> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<OrganOccupations> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<OrganOccupations> RetVal = new List<OrganOccupations>();
            OrganOccupations m_OrganOccupations = new OrganOccupations(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_OrganOccupations.GetListOrderBy(OrderBy);
                }               
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_OrganOccupations.OrganOccupationName = TextOptionAll;
                    m_OrganOccupations.OrganOccupationDesc = TextOptionAll;
                    RetVal.Insert(0, m_OrganOccupations);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<OrganOccupations> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<OrganOccupations> GetListByOrganOccupationId(short OrganOccupationId)
        {
            List<OrganOccupations> RetVal = new List<OrganOccupations>();
            try
            {
                if (OrganOccupationId > 0)
                {
                    string sql = "SELECT * FROM OrganOccupations WHERE (OrganOccupationId=" + OrganOccupationId.ToString() + ")";
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
        public OrganOccupations Get(short OrganOccupationId)
        {
            OrganOccupations RetVal = new OrganOccupations(db.ConnectionString);
            try
            {
                List<OrganOccupations> list = GetListByOrganOccupationId(OrganOccupationId);
                if (list.Count > 0)
                {
                    RetVal = (OrganOccupations)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public OrganOccupations Get()
        {
            return Get(this.OrganOccupationId);
        }
        //-------------------------------------------------------------- 
        public static OrganOccupations Static_Get(short OrganOccupationId)
        {
            return Static_Get(OrganOccupationId);
        }
        //--------------------------------------------------------------
    } 
}