
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
    public class Features
    {   
	    private short _FeatureId;
        private short _FeatureGroupId;
	    private string _FeatureName = "";
	    private string _FeatureDesc = "";
	    private short _ParentFeatureId;
	    private string _IconPath;
	    private byte _DisplayOrder;
	    private byte _IsData;
	    private byte _IsSearch;
	    private byte _IsDisplay;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _InputTypeId;
	    private short _DataDictionaryTypeId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Features()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Features(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Features()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short FeatureId
        {
		    get { return _FeatureId; }
		    set { _FeatureId = value; }
	    }
        //-----------------------------------------------------------------
        public short FeatureGroupId
		{
            get { return _FeatureGroupId; }
            set { _FeatureGroupId = value; }
		}    
        //-----------------------------------------------------------------
        public string FeatureName
		{
            get { return _FeatureName; }
		    set { _FeatureName = value; }
		}    
        //-----------------------------------------------------------------
        public string FeatureDesc
		{
            get { return _FeatureDesc; }
		    set { _FeatureDesc = value; }
		}    
        //-----------------------------------------------------------------
        public short ParentFeatureId
		{
            get { return _ParentFeatureId; }
		    set { _ParentFeatureId = value; }
		}    
        //-----------------------------------------------------------------
        public string IconPath
		{
            get { return _IconPath; }
		    set { _IconPath = value; }
		}    
        //-----------------------------------------------------------------
        public byte DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
		}    
        //-----------------------------------------------------------------
        public byte IsData
		{
            get { return _IsData; }
		    set { _IsData = value; }
		}    
        //-----------------------------------------------------------------
        public byte IsSearch
		{
            get { return _IsSearch; }
		    set { _IsSearch = value; }
		}    
        //-----------------------------------------------------------------
        public byte IsDisplay
		{
            get { return _IsDisplay; }
		    set { _IsDisplay = value; }
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
    
	    public byte InputTypeId
        {
		    get { return _InputTypeId; }
		    set { _InputTypeId = value; }
	    }
	    public short DataDictionaryTypeId
        {
		    get { return _DataDictionaryTypeId; }
		    set { _DataDictionaryTypeId = value; }
	    }
        private List<Features> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Features> l_Features = new List<Features>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Features m_Features = new Features(db.ConnectionString);
                    m_Features.FeatureId = smartReader.GetInt16("FeatureId");
                    m_Features.FeatureGroupId = smartReader.GetInt16("FeatureGroupId");
                    m_Features.FeatureName = smartReader.GetString("FeatureName");
                    m_Features.FeatureDesc = smartReader.GetString("FeatureDesc");
                    m_Features.ParentFeatureId = smartReader.GetInt16("ParentFeatureId");
                    m_Features.InputTypeId = smartReader.GetByte("InputTypeId");
                    m_Features.DataDictionaryTypeId = smartReader.GetInt16("DataDictionaryTypeId");
                    m_Features.IconPath = smartReader.GetString("IconPath");
                    m_Features.DisplayOrder = smartReader.GetByte("DisplayOrder");
                    m_Features.IsData = smartReader.GetByte("IsData");
                    m_Features.IsSearch = smartReader.GetByte("IsSearch");
                    m_Features.IsDisplay = smartReader.GetByte("IsDisplay");
                    m_Features.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Features.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Features.Add(m_Features);
                }
                reader.Close();
                return l_Features;
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
                SqlCommand cmd = new SqlCommand("Features_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupId", this.FeatureGroupId));
                cmd.Parameters.Add(new SqlParameter("@FeatureName", this.FeatureName));
                cmd.Parameters.Add(new SqlParameter("@FeatureDesc", this.FeatureDesc));
                cmd.Parameters.Add(new SqlParameter("@ParentFeatureId", this.ParentFeatureId));
                cmd.Parameters.Add(new SqlParameter("@InputTypeId", this.InputTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId", this.DataDictionaryTypeId));
                cmd.Parameters.Add(new SqlParameter("@IconPath", this.IconPath));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@IsData", this.IsData));
                cmd.Parameters.Add(new SqlParameter("@IsSearch", this.IsSearch));
                cmd.Parameters.Add(new SqlParameter("@IsDisplay", this.IsDisplay));
                cmd.Parameters.Add("@FeatureId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FeatureId = (cmd.Parameters["@FeatureId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@FeatureId"].Value);
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
                SqlCommand cmd = new SqlCommand("Features_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupId", this.FeatureGroupId));
                cmd.Parameters.Add(new SqlParameter("@FeatureName", this.FeatureName));
                cmd.Parameters.Add(new SqlParameter("@FeatureDesc", this.FeatureDesc));
                cmd.Parameters.Add(new SqlParameter("@ParentFeatureId", this.ParentFeatureId));
                cmd.Parameters.Add(new SqlParameter("@InputTypeId", this.InputTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId", this.DataDictionaryTypeId));
                cmd.Parameters.Add(new SqlParameter("@IconPath", this.IconPath));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@IsData", this.IsData));
                cmd.Parameters.Add(new SqlParameter("@IsSearch", this.IsSearch));
                cmd.Parameters.Add(new SqlParameter("@IsDisplay", this.IsDisplay));
                cmd.Parameters.Add(new SqlParameter("@FeatureId",this.FeatureId));
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
                SqlCommand cmd = new SqlCommand("Features_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupId", this.FeatureGroupId));
                cmd.Parameters.Add(new SqlParameter("@FeatureName", this.FeatureName));
                cmd.Parameters.Add(new SqlParameter("@FeatureDesc", this.FeatureDesc));
                cmd.Parameters.Add(new SqlParameter("@ParentFeatureId", this.ParentFeatureId));
                cmd.Parameters.Add(new SqlParameter("@InputTypeId", this.InputTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId", this.DataDictionaryTypeId));
                cmd.Parameters.Add(new SqlParameter("@IconPath", this.IconPath));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@IsData", this.IsData));
                cmd.Parameters.Add(new SqlParameter("@IsSearch", this.IsSearch));
                cmd.Parameters.Add(new SqlParameter("@IsDisplay", this.IsDisplay));
                cmd.Parameters.Add(new SqlParameter("@FeatureId", this.FeatureId).Direction = ParameterDirection.InputOutput);
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FeatureId = (cmd.Parameters["@FeatureId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@FeatureId"].Value);
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
                SqlCommand cmd = new SqlCommand("Features_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FeatureId",this.FeatureId));
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
        public List<Features> GetList()
        {
            List<Features> RetVal = new List<Features>();
            try
            {
                string sql = "SELECT * FROM Features";
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
        public List<Features> GetList(string TextOptionAll = "...")
        {
            List<Features> RetVal = GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                Features m_Features = new Features();
                m_Features.FeatureName = TextOptionAll;
                m_Features.FeatureDesc = TextOptionAll;
                RetVal.Insert(0, m_Features);
            } 
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<Features> GetListOrderBy(string OrderBy)
        {
            List<Features> RetVal = new List<Features>();
            try
            {
                string sql = "SELECT * FROM Features ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<Features> GetListOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            List<Features> RetVal = GetListOrderBy(OrderBy);
            if (TextOptionAll == null) TextOptionAll = "";
            if (TextOptionAll.Length > 0)
            {
                Features m_Features = new Features();
                m_Features.FeatureName = TextOptionAll;
                m_Features.FeatureDesc = TextOptionAll;
                RetVal.Insert(0, m_Features);
            } 
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Features> GetListByFeatureId(short FeatureId)
        {
            List<Features> RetVal = new List<Features>();
            try
            {
                if (FeatureId > 0)
                {
                    string sql = "SELECT * FROM Features WHERE (FeatureId=" + FeatureId.ToString() + ")";
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
        public Features Get(short FeatureId)
        {
            Features RetVal = new Features();
            try
            {
                List<Features> list = GetListByFeatureId(FeatureId);
                if (list.Count > 0)
                {
                    RetVal = (Features)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Features Static_Get(short FeatureId, List<Features> list)
        {
            Features RetVal = new Features();
            try
            {
                RetVal = list.Find(i => i.FeatureId == FeatureId);
                if (RetVal == null) RetVal = new Features();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Features Get()
        {
            return Get(this.FeatureId);
        }
        //-------------------------------------------------------------- 
        public static Features Static_Get(string Constr, short FeatureId)
        {
            Features m_Features = new Features(Constr);
            return m_Features.Get(FeatureId);
        }
        //-------------------------------------------------------------- 
        public static Features Static_Get(short FeatureId)
        {
            return Static_Get("",FeatureId);
        }
        //-------------------------------------------------------------- 
        public static List<Features> Static_GetList(string ConStr)
        {
            List<Features> RetVal = new List<Features>();
            try
            {
                Features m_Features = new Features(ConStr);
                RetVal = m_Features.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Features> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
        public List<Features> GetListByFeatureGroupId(byte DataTypeId, short CategoryId, string PaddingChar)
        {
            List<Features> RetVal = new List<Features>();
            try
            {
                SqlCommand cmd = new SqlCommand("Features_GetByFeatureGroupId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupId", this.FeatureGroupId));
                cmd.Parameters.Add(new SqlParameter("@IsData", this.IsData));
                cmd.Parameters.Add(new SqlParameter("@IsSearch", this.IsSearch));
                cmd.Parameters.Add(new SqlParameter("@IsDisplay", this.IsDisplay));
                cmd.Parameters.Add(new SqlParameter("@PaddingChar", PaddingChar));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Features> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, byte DataTypeId, short CategoryId, string PaddingChar, ref int RowCount)
        {
            List<Features> RetVal = new List<Features>();
            try
            {
                SqlCommand cmd = new SqlCommand("Features_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupId", this.FeatureGroupId));
                cmd.Parameters.Add(new SqlParameter("@FeatureName", this.FeatureName));
                cmd.Parameters.Add(new SqlParameter("@FeatureDesc", this.FeatureDesc));
                cmd.Parameters.Add(new SqlParameter("@ParentFeatureId", this.ParentFeatureId));
                cmd.Parameters.Add(new SqlParameter("@InputTypeId", this.InputTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataDictionaryTypeId", this.DataDictionaryTypeId));
                cmd.Parameters.Add(new SqlParameter("@IsData", this.IsData));
                cmd.Parameters.Add(new SqlParameter("@IsSearch", this.IsSearch));
                cmd.Parameters.Add(new SqlParameter("@IsDisplay", this.IsDisplay));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@PaddingChar", PaddingChar));
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

