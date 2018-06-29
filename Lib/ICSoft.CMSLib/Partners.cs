
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
    public class Partners
    {   
	    private short _PartnerId;
	    private string _PartnerName;
	    private string _PartnerDesc;
	    private string _Notes;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Partners()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Partners(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Partners()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short PartnerId
        {
		    get { return _PartnerId; }
		    set { _PartnerId = value; }
	    }
        //-----------------------------------------------------------------
        public string PartnerName
		{
            get { return _PartnerName; }
		    set { _PartnerName = value; }
		}    
        //-----------------------------------------------------------------
        public string PartnerDesc
		{
            get { return _PartnerDesc; }
		    set { _PartnerDesc = value; }
		}    
        //-----------------------------------------------------------------
        public string Notes
		{
            get { return _Notes; }
		    set { _Notes = value; }
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
    
        private List<Partners> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Partners> l_Partners = new List<Partners>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Partners m_Partners = new Partners(db.ConnectionString);
                    m_Partners.PartnerId = smartReader.GetInt16("PartnerId");
                    m_Partners.PartnerName = smartReader.GetString("PartnerName");
                    m_Partners.PartnerDesc = smartReader.GetString("PartnerDesc");
                    m_Partners.Notes = smartReader.GetString("Notes");
                    m_Partners.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Partners.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Partners.Add(m_Partners);
                }
                reader.Close();
                return l_Partners;
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
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
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
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Partners_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PartnerName", this.PartnerName));
                cmd.Parameters.Add(new SqlParameter("@PartnerDesc", this.PartnerDesc));
                cmd.Parameters.Add(new SqlParameter("@Notes", this.Notes));
                cmd.Parameters.Add(new SqlParameter("@PartnerId", this.PartnerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PartnerId = Convert.ToInt16((cmd.Parameters["@PartnerId"].Value == null) ? 0 : (cmd.Parameters["@PartnerId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("Partners_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PartnerId",this.PartnerId));
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
        public List<Partners> GetList()
        {
            List<Partners> RetVal = new List<Partners>();
            try
            {
                string sql = "SELECT * FROM V$Partners";
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
        public static List<Partners> Static_GetList(string ConStr)
        {
            List<Partners> RetVal = new List<Partners>();
            try
            {
                Partners m_Partners = new Partners(ConStr);
                RetVal = m_Partners.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Partners> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<Partners> Static_GetListAll(string ConStr ,string TextOptionAll = "...")
        {
            Partners m_Partners = new Partners(ConStr);
            List<Partners> RetVal = m_Partners.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_Partners.PartnerName = TextOptionAll;
                m_Partners.PartnerDesc = TextOptionAll;
                RetVal.Insert(0, m_Partners);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Partners> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<Partners> GetListOrderBy(string OrderBy)
        {
            List<Partners> RetVal = new List<Partners>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Partners " ;
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
        public static List<Partners> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Partners m_Partners = new Partners(ConStr);
            return m_Partners.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Partners> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Partners> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<Partners> RetVal = new List<Partners>();
            Partners m_Partners = new Partners(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_Partners.GetListOrderBy(OrderBy);
                }               
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_Partners.PartnerName = TextOptionAll;
                    m_Partners.PartnerDesc = TextOptionAll;
                    RetVal.Insert(0, m_Partners);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Partners> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<Partners> GetListByPartnerId(short PartnerId)
        {
            List<Partners> RetVal = new List<Partners>();
            try
            {
                if (PartnerId > 0)
                {
                    string sql = "SELECT * FROM V$Partners WHERE (PartnerId=" + PartnerId.ToString() + ")";
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
        public Partners Get(short PartnerId)
        {
            Partners RetVal = new Partners(db.ConnectionString);
            try
            {
                List<Partners> list = GetListByPartnerId(PartnerId);
                if (list.Count > 0)
                {
                    RetVal = (Partners)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Partners Get()
        {
            return Get(this.PartnerId);
        }
        //-------------------------------------------------------------- 
        public static Partners Static_Get(short PartnerId)
        {
            return new Partners().Get(PartnerId);
        }
         //--------------------------------------------------------------     
        public List<Partners> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string PartnerName, ref int RowCount)
        {
            List<Partners> RetVal = new List<Partners>();
            try
            {
                SqlCommand cmd = new SqlCommand("Partners_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@PartnerName", StringUtil.InjectionString(PartnerName)));               
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);                
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Partners> Search(int ActUserId, string OrderBy, string PartnerName, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, PartnerName, ref RowCount);
        }
        //--------------------------------------------------------------     
        
    } 
}