
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ICSoft.CMSLib;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    /// <summary>
    /// class Fields
    /// </summary>
    public class Fields
    {   
        private byte _LanguageId;
	    private short _FieldId;
	    private string _FieldName;
	    private string _FieldDesc;
	    private short _ParentFieldId;
	    private byte _FieldLevel;
	    private short _DisplayOrder;
	    private byte _ReviewStatusId;
	    private int _CrUserId;
        private int _SoLuong;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Fields()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Fields(string constr)
        {
            db = new DBAccess ((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Fields()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------    
	    public short FieldId
        {
		    get { return _FieldId; }
		    set { _FieldId = value; }
	    }
        //-----------------------------------------------------------------
        public string FieldName
		{
            get { return _FieldName; }
		    set { _FieldName = value; }
		}    
        //-----------------------------------------------------------------
        public string FieldDesc
		{
            get { return _FieldDesc; }
		    set { _FieldDesc = value; }
		}    
        //-----------------------------------------------------------------
        public short ParentFieldId
		{
            get { return _ParentFieldId; }
		    set { _ParentFieldId = value; }
		}    
        //-----------------------------------------------------------------
        public byte FieldLevel
		{
            get { return _FieldLevel; }
		    set { _FieldLevel = value; }
		}    
        //-----------------------------------------------------------------
        public short DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
		}    
        //-----------------------------------------------------------------
        public byte ReviewStatusId
		{
            get { return _ReviewStatusId; }
		    set { _ReviewStatusId = value; }
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
        public int SoLuong
        {
            get { return _SoLuong; }
            set { _SoLuong = value; }
        }
        //-----------------------------------------------------------------
    
        private List<Fields> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Fields> l_Fields = new List<Fields>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Fields m_Fields = new Fields(db.ConnectionString);
                    m_Fields.LanguageId = smartReader.GetByte("LanguageId");
                    m_Fields.FieldId = smartReader.GetInt16("FieldId");
                    m_Fields.FieldName = smartReader.GetString("FieldName");
                    m_Fields.FieldDesc = smartReader.GetString("FieldDesc");
                    m_Fields.ParentFieldId = smartReader.GetInt16("ParentFieldId");
                    m_Fields.FieldLevel = smartReader.GetByte("FieldLevel");
                    m_Fields.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_Fields.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Fields.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Fields.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    if (reader.GetSchemaTable().Columns["Soluong"] != null)
                    {

                        m_Fields.SoLuong = smartReader.GetInt32("Soluong"); ;
                       
                    }
                    l_Fields.Add(m_Fields);
                }
                reader.Close();
                return l_Fields;
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
                SqlCommand cmd = new SqlCommand("Fields_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldName", this.FieldName));
                cmd.Parameters.Add(new SqlParameter("@FieldDesc", this.FieldDesc));
                cmd.Parameters.Add(new SqlParameter("@ParentFieldId", this.ParentFieldId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FieldId =Convert.ToInt16((cmd.Parameters["@FieldId"].Value == null) ? 0 : (cmd.Parameters["@FieldId"].Value));
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
                SqlCommand cmd = new SqlCommand("Fields_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));

                cmd.Parameters.Add(new SqlParameter("@FieldId",this.FieldId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
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
        public List<Fields> GetList()
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                string sql = "SELECT * FROM V$Fields ORDER BY FieldName";
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
        public List<Fields> GetListApproved()
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                string sql = "SELECT * FROM V$Fields WHERE ReviewStatusId = 2 ORDER BY FieldName";
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
        public List<Fields> GetListByLanguageID(int LanguageId)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                string table = "V$Fields";
                if(LanguageId ==2)
                {
                    table = "V$FieldLanguages";
                }
                string sql = "SELECT * FROM "+table+" ORDER BY FieldName";
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
        public List<Fields> GetListbyDisplayTypeId(short DisplayTypeId = 0)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                string sql = "SELECT * FROM Fields where FieldId IN (select FieldId from FieldDisplays where DisplayTypeId=" + DisplayTypeId + ") Order by FieldName";
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
        public static List<Fields> Static_GetListbyDisplayTypeId(short DisplayTypeId = 0)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                Fields m_Fields = new Fields();
                RetVal = m_Fields.GetListbyDisplayTypeId(DisplayTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static List<Fields> Static_GetList(string ConStr)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                Fields m_Fields = new Fields(ConStr);
                RetVal = m_Fields.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static List<Fields> Static_GetListApproved(string ConStr)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                Fields m_Fields = new Fields(ConStr);
                RetVal = m_Fields.GetListApproved();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Fields> Static_GetListApproved()
        {
            return Static_GetListApproved("");
        }
        //--------------------------------------------------------------
        public static List<Fields> Static_GetListByLanguage(string ConStr,int LanguageId)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                Fields m_Fields = new Fields(ConStr);
                RetVal = m_Fields.GetListByLanguageID(LanguageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Fields> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
        public static List<Fields> Static_GetListByLanguage(int LanguageId)
        {
            return Static_GetListByLanguage("", LanguageId);
        }
        
        //--------------------------------------------------------------   
        public List<Fields> GetListByFieldId(short FieldId, byte LanguageId)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                int ActUserId = 0;
                string OrderBy = "";
                string FieldName="";
                short ParentFieldId=0;
                short DisplayTypeId = 0;
                byte ReviewStatusId=0;
                byte ReferToDefaultLanguage = 0;
                RetVal = Fields_GetList(ActUserId, OrderBy, LanguageId, FieldId, FieldName, ParentFieldId, DisplayTypeId, ReviewStatusId, ReferToDefaultLanguage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        
        //-------------------------------------------------------------- 
        public Fields Get(short FieldId, byte LanguageId)
        {
            Fields RetVal = new Fields(db.ConnectionString);
            try
            {
                List<Fields> list = GetListByFieldId(FieldId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (Fields)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Fields Get()
        {
            return Get(this.FieldId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Fields Static_Get(string Constr, short FieldId, byte LanguageId)
        {
            Fields m_Fields = new Fields(Constr);
            return m_Fields.Get(FieldId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Fields Static_Get(short FieldId, byte LanguageId)
        {
            return Static_Get("",FieldId, LanguageId);
        }
        //-----------------------------------------------------------------------------
        public static Fields Static_Get(short FieldId, List<Fields> lList)
        {
            Fields RetVal = new Fields();
            if (FieldId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.FieldId == FieldId);
                if (RetVal == null) RetVal = new Fields();
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Fields> GetListByFieldId(byte LanguageId, short FieldId)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                int ActUserId = 0;
                string OrderBy = "";
                string FieldName = "";
                short ParentFieldId = 0;
                short DisplayTypeId = 0;
                byte ReferToDefaultLanguage = 0;
                RetVal = Fields_GetList(ActUserId, OrderBy, LanguageId, FieldId, FieldName, ParentFieldId, DisplayTypeId, ReviewStatusId, ReferToDefaultLanguage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Fields> GetListByFieldId(byte LanguageId, short FieldId, byte ReviewStatusId, string PaddingChar)
        {
            List<Fields> RetVal = GetListByFieldId(LanguageId, FieldId, ReviewStatusId);
            try
            {
                foreach (Fields m_Fields in RetVal)
                {
                    for (int index = 1; index < m_Fields.FieldLevel; index++)
                    {
                        m_Fields.FieldName = PaddingChar + m_Fields.FieldName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Fields> GetListByFieldId(byte LanguageId, short FieldId, byte ReviewStatusId)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                int ActUserId = 0;
                string OrderBy = "FieldName";
                string FieldName = "";
                short ParentFieldId = 0;
                short DisplayTypeId = 0;
                byte ReferToDefaultLanguage = 0;
                RetVal = Fields_GetList(ActUserId, OrderBy, LanguageId, FieldId, FieldName, ParentFieldId, DisplayTypeId, ReviewStatusId, ReferToDefaultLanguage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Fields> Fields_GetList(int ActUserId, string OrderBy, byte LanguageId, short FieldId, string FieldName, short ParentFieldId, short DisplayTypeId, byte ReviewStatusId, byte ReferToDefaultLanguage)
        {
            List<Fields> RetVal = new List<Fields>();
            try
            {
                SqlCommand cmd = new SqlCommand("Fields_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@FieldName", StringUtil.InjectionString(FieldName)));
                cmd.Parameters.Add(new SqlParameter("@ParentFieldId", ParentFieldId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReferToDefaultLanguage", ReferToDefaultLanguage));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataSet Fields_GetNameByJson(string Name, int RowAmount, ref string Result)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Fields_GetNameByJson");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar,4000).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                Result = Convert.ToString(cmd.Parameters["@Result"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetUrl(byte DocGroupId = 1)
        {
            string RetVal = "";
            if (!string.IsNullOrEmpty(this.FieldName) && this.FieldId > 0)
            {
                RetVal = StringUtil.RemoveSignature(this.FieldName);
                RetVal = Regex.Replace(RetVal, "(?:[^a-z0-9 _-]|(?<=['\"])s)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                RetVal = RetVal.Replace(" ", "-");
                RetVal = RetVal.ToLower();
                RetVal = string.Concat(RetVal, "-", this.FieldId, "-", "f", DocGroupId, ".html");

            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        
    } 
}