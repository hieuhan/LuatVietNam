
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
    public class DisplayTypes
    {   
	    private short _DisplayTypeId;
	    private string _DisplayTypeName;
	    private string _DisplayTypeDesc;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _DataTypeId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public DisplayTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public DisplayTypes(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DisplayTypes()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short DisplayTypeId
        {
		    get { return _DisplayTypeId; }
		    set { _DisplayTypeId = value; }
	    }
        //-----------------------------------------------------------------
        public string DisplayTypeName
		{
            get { return _DisplayTypeName; }
		    set { _DisplayTypeName = value; }
		}    
        //-----------------------------------------------------------------
        public string DisplayTypeDesc
		{
            get { return _DisplayTypeDesc; }
		    set { _DisplayTypeDesc = value; }
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
    
	    public byte DataTypeId
        {
		    get { return _DataTypeId; }
		    set { _DataTypeId = value; }
	    }
        private List<DisplayTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DisplayTypes> l_DisplayTypes = new List<DisplayTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DisplayTypes m_DisplayTypes = new DisplayTypes(db.ConnectionString);
                    m_DisplayTypes.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_DisplayTypes.DisplayTypeName = smartReader.GetString("DisplayTypeName");
                    m_DisplayTypes.DisplayTypeDesc = smartReader.GetString("DisplayTypeDesc");
                    m_DisplayTypes.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_DisplayTypes.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DisplayTypes.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_DisplayTypes.Add(m_DisplayTypes);
                }
                reader.Close();
                return l_DisplayTypes;
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
                SqlCommand cmd = new SqlCommand("DisplayTypes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeName", this.DisplayTypeName));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeDesc", this.DisplayTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DisplayTypeId = Convert.ToInt16((cmd.Parameters["@DisplayTypeId"].Value == null) ? 0 : (cmd.Parameters["@DisplayTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("DisplayTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
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
        public List<DisplayTypes> GetListByArticleId(int ArticleId, byte LanguageId, byte ApplicationTypeId)
        {
            List<DisplayTypes> RetVal = new List<DisplayTypes>();
            try
            {
                string sql = "SELECT * FROM DisplayTypes WHERE DisplayTypeId IN (SELECT DisplayTypeId FROM ArticleDisplay WHERE ArticleId=" + ArticleId.ToString() + " AND LanguageId=" + LanguageId.ToString() + " AND ApplicationTypeId=" + ApplicationTypeId.ToString() + ") ORDER BY DisplayTypeName";
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
        public List<DisplayTypes> GetList()
        {
            List<DisplayTypes> RetVal = new List<DisplayTypes>();
            try
            {
                string sql = "SELECT * FROM DisplayTypes ORDER BY DisplayTypeName";
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
        public static List<DisplayTypes> Static_GetList(string ConStr)
        {
            List<DisplayTypes> RetVal = new List<DisplayTypes>();
            try
            {
                DisplayTypes m_DisplayTypes = new DisplayTypes(ConStr);
                RetVal = m_DisplayTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DisplayTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<DisplayTypes> Static_GetListAll(string ConStr ,string TextOptionAll = "...")
        {
            DisplayTypes m_DisplayTypes = new DisplayTypes(ConStr);
            List<DisplayTypes> RetVal = m_DisplayTypes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_DisplayTypes.DisplayTypeName = TextOptionAll;
                m_DisplayTypes.DisplayTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_DisplayTypes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<DisplayTypes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<DisplayTypes> GetListOrderBy(string OrderBy)
        {
            List<DisplayTypes> RetVal = new List<DisplayTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM DisplayTypes " ;
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
        public static List<DisplayTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            DisplayTypes m_DisplayTypes = new DisplayTypes(ConStr);
            return m_DisplayTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<DisplayTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<DisplayTypes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<DisplayTypes> RetVal = new List<DisplayTypes>();
            DisplayTypes m_DisplayTypes = new DisplayTypes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_DisplayTypes.GetListOrderBy(OrderBy);
                }               
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_DisplayTypes.DisplayTypeName = TextOptionAll;
                    m_DisplayTypes.DisplayTypeDesc = TextOptionAll;
                    RetVal.Insert(0, m_DisplayTypes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<DisplayTypes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<DisplayTypes> GetListByDisplayTypeId(short DisplayTypeId)
        {
            List<DisplayTypes> RetVal = new List<DisplayTypes>();
            try
            {
                if (DisplayTypeId > 0)
                {
                    string sql = "SELECT * FROM DisplayTypes WHERE (DisplayTypeId=" + DisplayTypeId.ToString() + ")";
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
        public List<DisplayTypes> GetListByDataTypeId(byte DataTypeId)
        {
            List<DisplayTypes> RetVal = new List<DisplayTypes>();
            try
            {
                if (DataTypeId > 0)
                {
                    string sql = "SELECT * FROM DisplayTypes WHERE (DataTypeId=" + DataTypeId.ToString() + ") ORDER BY DisplayTypeName";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = Init(cmd);
                }
                else
                {
                    string sql = "SELECT * FROM DisplayTypes ORDER BY DisplayTypeName";
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
        public static List<DisplayTypes> Static_GetListByDataTypeId(string ConStr, byte DataTypeId)
        {
            List<DisplayTypes> RetVal = new List<DisplayTypes>();
            try
            {
                DisplayTypes m_DisplayTypes = new DisplayTypes(ConStr);
                RetVal = m_DisplayTypes.GetListByDataTypeId(DataTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DisplayTypes> Static_GetListByDataTypeId(byte DataTypeId)
        {
            return Static_GetListByDataTypeId("",DataTypeId);
        }
        //-------------------------------------------------------------- 
        public DisplayTypes Get(short DisplayTypeId)
        {
            DisplayTypes RetVal = new DisplayTypes(db.ConnectionString);
            try
            {
                List<DisplayTypes> list = GetListByDisplayTypeId(DisplayTypeId);
                if (list.Count > 0)
                {
                    RetVal = (DisplayTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DisplayTypes Get()
        {
            return Get(this.DisplayTypeId);
        }
        //-------------------------------------------------------------- 
        public static DisplayTypes Static_Get(short DisplayTypeId)
        {
            return Static_Get(DisplayTypeId);
        }
        //-----------------------------------------------------------------------------
        public static DisplayTypes Static_Get(short DisplayTypeId, List<DisplayTypes> lList)
        {
            DisplayTypes RetVal = new DisplayTypes();
            if (DisplayTypeId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.DisplayTypeId == DisplayTypeId);
                if (RetVal == null) RetVal = new DisplayTypes();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    } 
}
