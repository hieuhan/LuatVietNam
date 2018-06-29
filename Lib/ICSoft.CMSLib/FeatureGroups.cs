
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
    public class FeatureGroups
    {   
	    private short _FeatureGroupId;
        private string _FeatureGroupName = "";
        private string _FeatureGroupDesc = "";
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public FeatureGroups()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public FeatureGroups(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FeatureGroups()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short FeatureGroupId
        {
		    get { return _FeatureGroupId; }
		    set { _FeatureGroupId = value; }
	    }
        //-----------------------------------------------------------------
        public string FeatureGroupName
		{
            get { return _FeatureGroupName; }
		    set { _FeatureGroupName = value; }
		}    
        //-----------------------------------------------------------------
        public string FeatureGroupDesc
		{
            get { return _FeatureGroupDesc; }
		    set { _FeatureGroupDesc = value; }
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
    
        private List<FeatureGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FeatureGroups> l_FeatureGroups = new List<FeatureGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FeatureGroups m_FeatureGroups = new FeatureGroups(db.ConnectionString);
                    m_FeatureGroups.FeatureGroupId = smartReader.GetInt16("FeatureGroupId");
                    m_FeatureGroups.FeatureGroupName = smartReader.GetString("FeatureGroupName");
                    m_FeatureGroups.FeatureGroupDesc = smartReader.GetString("FeatureGroupDesc");
                    m_FeatureGroups.CrUserId = smartReader.GetInt32("CrUserId");
                    m_FeatureGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_FeatureGroups.Add(m_FeatureGroups);
                }
                reader.Close();
                return l_FeatureGroups;
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
                SqlCommand cmd = new SqlCommand("FeatureGroups_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupName", this.FeatureGroupName));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupDesc", this.FeatureGroupDesc));
                cmd.Parameters.Add("@FeatureGroupId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FeatureGroupId = (cmd.Parameters["@FeatureGroupId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@FeatureGroupId"].Value);
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
                SqlCommand cmd = new SqlCommand("FeatureGroups_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupName", this.FeatureGroupName));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupDesc", this.FeatureGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupId",this.FeatureGroupId));
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("FeatureGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupName", this.FeatureGroupName));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupDesc", this.FeatureGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupId", this.FeatureGroupId).Direction = ParameterDirection.InputOutput);
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FeatureGroupId = (cmd.Parameters["@FeatureGroupId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@FeatureGroupId"].Value);
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("FeatureGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupId",this.FeatureGroupId));
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
        public List<FeatureGroups> GetList()
        {
            List<FeatureGroups> RetVal = new List<FeatureGroups>();
            try
            {
                string sql = "SELECT * FROM FeatureGroups";
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
        public List<FeatureGroups> GetList(string TextOptionAll = "...")
        {
            List<FeatureGroups> RetVal = GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                FeatureGroups m_FeatureGroups = new FeatureGroups();
                m_FeatureGroups.FeatureGroupName = TextOptionAll;
                m_FeatureGroups.FeatureGroupDesc = TextOptionAll;
                RetVal.Insert(0, m_FeatureGroups);
            } 
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<FeatureGroups> GetListOrderBy(string OrderBy)
        {
            List<FeatureGroups> RetVal = new List<FeatureGroups>();
            try
            {
                string sql = "SELECT * FROM FeatureGroups ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<FeatureGroups> GetListOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            List<FeatureGroups> RetVal = GetListOrderBy(OrderBy);
            if (TextOptionAll == null) TextOptionAll = "";
            if (TextOptionAll.Length > 0)
            {
                FeatureGroups m_FeatureGroups = new FeatureGroups();
                m_FeatureGroups.FeatureGroupName = TextOptionAll;
                m_FeatureGroups.FeatureGroupDesc = TextOptionAll;
                RetVal.Insert(0, m_FeatureGroups);
            } 
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<FeatureGroups> GetListByFeatureGroupId(short FeatureGroupId)
        {
            List<FeatureGroups> RetVal = new List<FeatureGroups>();
            try
            {
                if (FeatureGroupId > 0)
                {
                    string sql = "SELECT * FROM FeatureGroups WHERE (FeatureGroupId=" + FeatureGroupId.ToString() + ")";
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
        public FeatureGroups Get(short FeatureGroupId)
        {
            FeatureGroups RetVal = new FeatureGroups();
            try
            {
                List<FeatureGroups> list = GetListByFeatureGroupId(FeatureGroupId);
                if (list.Count > 0)
                {
                    RetVal = (FeatureGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static FeatureGroups Static_Get(short FeatureGroupId, List<FeatureGroups> list)
        {
            FeatureGroups RetVal = new FeatureGroups();
            try
            {
                RetVal = list.Find(i => i.FeatureGroupId == FeatureGroupId);
                if (RetVal == null) RetVal = new FeatureGroups();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public FeatureGroups Get()
        {
            return Get(this.FeatureGroupId);
        }
        //-------------------------------------------------------------- 
        public static FeatureGroups Static_Get(string Constr, short FeatureGroupId)
        {
            FeatureGroups m_FeatureGroups = new FeatureGroups(Constr);
            return m_FeatureGroups.Get(FeatureGroupId);
        }
        //-------------------------------------------------------------- 
        public static FeatureGroups Static_Get(short FeatureGroupId)
        {
            return Static_Get("",FeatureGroupId);
        }
        //-------------------------------------------------------------- 
        public static List<FeatureGroups> Static_GetList(string ConStr)
        {
            List<FeatureGroups> RetVal = new List<FeatureGroups>();
            try
            {
                FeatureGroups m_FeatureGroups = new FeatureGroups(ConStr);
                RetVal = m_FeatureGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<FeatureGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
        public List<FeatureGroups> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            List<FeatureGroups> RetVal = new List<FeatureGroups>();
            try
            {
                SqlCommand cmd = new SqlCommand("FeatureGroups_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupName", this.FeatureGroupName));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupDesc", this.FeatureGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                if (DateFrom!="") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo!="") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
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
    } 
}

