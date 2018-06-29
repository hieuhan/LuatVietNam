
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
    public class DataDictionaryTypes
    {   
	    private short _DataDictionaryTypeId;
        private string _DataDictionaryTypeName = "";
        private string _DataDictionaryTypeDesc = "";
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public DataDictionaryTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public DataDictionaryTypes(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DataDictionaryTypes()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short DataDictionaryTypeId
        {
		    get { return _DataDictionaryTypeId; }
		    set { _DataDictionaryTypeId = value; }
	    }
        //-----------------------------------------------------------------
        public string DataDictionaryTypeName
		{
            get { return _DataDictionaryTypeName; }
		    set { _DataDictionaryTypeName = value; }
		}    
        //-----------------------------------------------------------------
        public string DataDictionaryTypeDesc
		{
            get { return _DataDictionaryTypeDesc; }
		    set { _DataDictionaryTypeDesc = value; }
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
    
        private List<DataDictionaryTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DataDictionaryTypes> l_DataDictionaryTypes = new List<DataDictionaryTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DataDictionaryTypes m_DataDictionaryTypes = new DataDictionaryTypes(db.ConnectionString);
                    m_DataDictionaryTypes.DataDictionaryTypeId = smartReader.GetInt16("DataDictionaryTypeId");
                    m_DataDictionaryTypes.DataDictionaryTypeName = smartReader.GetString("DataDictionaryTypeName");
                    m_DataDictionaryTypes.DataDictionaryTypeDesc = smartReader.GetString("DataDictionaryTypeDesc");
                    m_DataDictionaryTypes.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DataDictionaryTypes.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_DataDictionaryTypes.Add(m_DataDictionaryTypes);
                }
                reader.Close();
                return l_DataDictionaryTypes;
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
                SqlCommand cmd = new SqlCommand("DataDictionaryTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeName", this.DataDictionaryTypeName));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeDesc", this.DataDictionaryTypeDesc));
                cmd.Parameters.Add("@DataDictionaryTypeId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DataDictionaryTypeId = (cmd.Parameters["@DataDictionaryTypeId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@DataDictionaryTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("DataDictionaryTypes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeName", this.DataDictionaryTypeName));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeDesc", this.DataDictionaryTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId",this.DataDictionaryTypeId));
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
                SqlCommand cmd = new SqlCommand("DataDictionaryTypes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeName", this.DataDictionaryTypeName));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeDesc", this.DataDictionaryTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId", this.DataDictionaryTypeId).Direction = ParameterDirection.InputOutput);
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DataDictionaryTypeId = (cmd.Parameters["@DataDictionaryTypeId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@DataDictionaryTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("DataDictionaryTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId",this.DataDictionaryTypeId));
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
        public List<DataDictionaryTypes> GetList()
        {
            List<DataDictionaryTypes> RetVal = new List<DataDictionaryTypes>();
            try
            {
                string sql = "SELECT * FROM DataDictionaryTypes ORDER BY DataDictionaryTypeName";
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
        public List<DataDictionaryTypes> GetList(string TextOptionAll = "...")
        {
            List<DataDictionaryTypes> RetVal = GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                DataDictionaryTypes m_DataDictionaryTypes = new DataDictionaryTypes();
                m_DataDictionaryTypes.DataDictionaryTypeName = TextOptionAll;
                m_DataDictionaryTypes.DataDictionaryTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_DataDictionaryTypes);
            } 
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<DataDictionaryTypes> GetListOrderBy(string OrderBy)
        {
            List<DataDictionaryTypes> RetVal = new List<DataDictionaryTypes>();
            try
            {
                string sql = "SELECT * FROM DataDictionaryTypes ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<DataDictionaryTypes> GetListOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            List<DataDictionaryTypes> RetVal = GetListOrderBy(OrderBy);
            if (TextOptionAll == null) TextOptionAll = "";
            if (TextOptionAll.Length > 0)
            {
                DataDictionaryTypes m_DataDictionaryTypes = new DataDictionaryTypes();
                m_DataDictionaryTypes.DataDictionaryTypeName = TextOptionAll;
                m_DataDictionaryTypes.DataDictionaryTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_DataDictionaryTypes);
            } 
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<DataDictionaryTypes> GetListByDataDictionaryTypeId(short DataDictionaryTypeId)
        {
            List<DataDictionaryTypes> RetVal = new List<DataDictionaryTypes>();
            try
            {
                if (DataDictionaryTypeId > 0)
                {
                    string sql = "SELECT * FROM DataDictionaryTypes WHERE (DataDictionaryTypeId=" + DataDictionaryTypeId.ToString() + ")";
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
        public DataDictionaryTypes Get(short DataDictionaryTypeId)
        {
            DataDictionaryTypes RetVal = new DataDictionaryTypes();
            try
            {
                List<DataDictionaryTypes> list = GetListByDataDictionaryTypeId(DataDictionaryTypeId);
                if (list.Count > 0)
                {
                    RetVal = (DataDictionaryTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static DataDictionaryTypes Static_Get(short DataDictionaryTypeId, List<DataDictionaryTypes> list)
        {
            DataDictionaryTypes RetVal = new DataDictionaryTypes();
            try
            {
                RetVal = list.Find(i => i.DataDictionaryTypeId == DataDictionaryTypeId);
                if (RetVal == null) RetVal = new DataDictionaryTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataDictionaryTypes Get()
        {
            return Get(this.DataDictionaryTypeId);
        }
        //-------------------------------------------------------------- 
        public static DataDictionaryTypes Static_Get(string Constr, short DataDictionaryTypeId)
        {
            DataDictionaryTypes m_DataDictionaryTypes = new DataDictionaryTypes(Constr);
            return m_DataDictionaryTypes.Get(DataDictionaryTypeId);
        }
        //-------------------------------------------------------------- 
        public static DataDictionaryTypes Static_Get(short DataDictionaryTypeId)
        {
            return Static_Get("",DataDictionaryTypeId);
        }
        //-------------------------------------------------------------- 
        public static List<DataDictionaryTypes> Static_GetList(string ConStr)
        {
            List<DataDictionaryTypes> RetVal = new List<DataDictionaryTypes>();
            try
            {
                DataDictionaryTypes m_DataDictionaryTypes = new DataDictionaryTypes(ConStr);
                RetVal = m_DataDictionaryTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DataDictionaryTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
        public List<DataDictionaryTypes> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DataDictionaryTypes> RetVal = new List<DataDictionaryTypes>();
            try
            {
                SqlCommand cmd = new SqlCommand("DataDictionaryTypes_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeName", this.DataDictionaryTypeName));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeDesc", this.DataDictionaryTypeDesc));
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

