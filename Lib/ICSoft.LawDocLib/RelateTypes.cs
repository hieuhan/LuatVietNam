
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    /// <summary>
    /// class RelateTypes
    /// </summary>
    public class RelateTypes
    {
        private byte _LanguageId;
        private byte _RelateTypeId;
        private string _RelateTypeName;
        private string _RelateTypeDesc;
        private string _RevertRelateTypeName;
        private string _RevertRelateTypeDesc;
        private byte _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private byte _RelateTypeGroupId;
        private byte _DocGroupId;
        private DBAccess db;

        private string _displayPosition;
        private int _rowCount;

        //-----------------------------------------------------------------
        public RelateTypes()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public RelateTypes(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~RelateTypes()
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
        public byte RelateTypeId
        {
            get { return _RelateTypeId; }
            set { _RelateTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string RelateTypeName
        {
            get { return _RelateTypeName; }
            set { _RelateTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string RelateTypeDesc
        {
            get { return _RelateTypeDesc; }
            set { _RelateTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public string RevertRelateTypeName
        {
            get { return _RevertRelateTypeName; }
            set { _RevertRelateTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string RevertRelateTypeDesc
        {
            get { return _RevertRelateTypeDesc; }
            set { _RevertRelateTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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

        public byte RelateTypeGroupId
        {
            get { return _RelateTypeGroupId; }
            set { _RelateTypeGroupId = value; }
        }
        //-----------------------------------------------------------------

        public byte DocGroupId
        {
            get { return _DocGroupId; }
            set { _DocGroupId = value; }
        }

        //-----------------------------------------------------------------
        public string DisplayPosition
        {
            get { return _displayPosition; }
            set { _displayPosition = value; }
        }

        public int RowCount
        {
            get { return _rowCount; }
            set { _rowCount = value; }
        }

        //-----------------------------------------------------------------
        private List<RelateTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RelateTypes m_RelateTypes = new RelateTypes(db.ConnectionString);
                    m_RelateTypes.LanguageId = smartReader.GetByte("LanguageId");
                    m_RelateTypes.RelateTypeId = smartReader.GetByte("RelateTypeId");
                    m_RelateTypes.RelateTypeName = smartReader.GetString("RelateTypeName");
                    m_RelateTypes.RelateTypeDesc = smartReader.GetString("RelateTypeDesc");
                    m_RelateTypes.RevertRelateTypeName = smartReader.GetString("RevertRelateTypeName");
                    m_RelateTypes.RevertRelateTypeDesc = smartReader.GetString("RevertRelateTypeDesc");
                    m_RelateTypes.RelateTypeGroupId = smartReader.GetByte("RelateTypeGroupId");
                    m_RelateTypes.DisplayOrder = smartReader.GetByte("DisplayOrder");
                    m_RelateTypes.DocGroupId = smartReader.GetByte("DocGroupId");
                    m_RelateTypes.CrUserId = smartReader.GetInt32("CrUserId");
                    m_RelateTypes.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_RelateTypes.Add(m_RelateTypes);
                }
                reader.Close();
                return l_RelateTypes;
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
        private List<RelateTypes> InitByDocGroupId(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RelateTypes m_RelateTypes =
                        new RelateTypes(db.ConnectionString)
                        {
                            RelateTypeId = smartReader.GetByte("RelateTypeId"),
                            RelateTypeName = smartReader.GetString("RelateTypeName"),
                            RelateTypeDesc = smartReader.GetString("RelateTypeDesc"),
                            RelateTypeGroupId = smartReader.GetByte("RelateTypeGroupId"),
                            DisplayPosition = smartReader.GetString("DisplayPosition")
                        };

                    l_RelateTypes.Add(m_RelateTypes);
                }
                reader.Close();
                return l_RelateTypes;
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
                SqlCommand cmd = new SqlCommand("RelateTypes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeName", this.RelateTypeName));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeDesc", this.RelateTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@RevertRelateTypeName", this.RevertRelateTypeName));
                cmd.Parameters.Add(new SqlParameter("@RevertRelateTypeDesc", this.RevertRelateTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeGroupId", this.RelateTypeGroupId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", this.RelateTypeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.RelateTypeId = Convert.ToByte((cmd.Parameters["@RelateTypeId"].Value == null) ? 0 : (cmd.Parameters["@RelateTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("RelateTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", this.RelateTypeId));
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
        public List<RelateTypes> GetList()
        {
            List<RelateTypes> RetVal = new List<RelateTypes>();
            try
            {
                string sql = "SELECT * FROM RelateTypes ORDER BY RelateTypeName";
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
        public static List<RelateTypes> Static_GetList(string ConStr)
        {
            List<RelateTypes> RetVal = new List<RelateTypes>();
            try
            {
                RelateTypes m_RelateTypes = new RelateTypes(ConStr);
                RetVal = m_RelateTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<RelateTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------    
        public List<RelateTypes> GetListOrderBy(string OrderBy)
        {
            List<RelateTypes> RetVal = new List<RelateTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM RelateTypes ";
                if (OrderBy.Length > 0)
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
        public static List<RelateTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            RelateTypes m_RelateTypes = new RelateTypes(ConStr);
            return m_RelateTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<RelateTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------   
        public List<RelateTypes> GetListByRelateTypeId(byte RelateTypeId, byte LanguageId)
        {
            List<RelateTypes> RetVal = new List<RelateTypes>();
            try
            {
                int ActUserId = 0;
                string OrderBy = "";
                byte ReferToDefaultLanguage = 0;
                RetVal = RelateTypes_GetList(ActUserId, OrderBy, LanguageId, RelateTypeId, ReferToDefaultLanguage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public RelateTypes Get(byte RelateTypeId, byte LanguageId)
        {
            RelateTypes RetVal = new RelateTypes(db.ConnectionString);
            try
            {
                List<RelateTypes> list = GetListByRelateTypeId(RelateTypeId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (RelateTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public RelateTypes Get()
        {
            return Get(this.RelateTypeId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static RelateTypes Static_Get(string Constr, byte RelateTypeId, byte LanguageId)
        {
            RelateTypes m_RelateTypes = new RelateTypes(Constr);
            return m_RelateTypes.Get(RelateTypeId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static RelateTypes Static_Get(byte RelateTypeId, byte LanguageId)
        {
            return Static_Get("", RelateTypeId, LanguageId);
        }
        //-----------------------------------------------------------------------------
        public static RelateTypes Static_Get(byte RelateTypeId, List<RelateTypes> lList)
        {
            RelateTypes RetVal = new RelateTypes();
            if (RelateTypeId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.RelateTypeId == RelateTypeId);
                if (RetVal == null) RetVal = new RelateTypes();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<RelateTypes> RelateTypes_GetList(int ActUserId, string OrderBy, byte LanguageId, byte RelateTypeId, byte ReferToDefaultLanguage)
        {
            List<RelateTypes> RetVal = new List<RelateTypes>();
            try
            {
                SqlCommand cmd = new SqlCommand("RelateTypes_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
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
        //-------------------------------------------------------------- 
        public List<RelateTypes> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("RelateTypes_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", this.RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeName", this.RelateTypeName));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeDesc", this.RelateTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@RevertRelateTypeName", this.RevertRelateTypeName));
                cmd.Parameters.Add(new SqlParameter("@RevertRelateTypeDesc", this.RevertRelateTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeGroupId", this.RelateTypeGroupId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<RelateTypes> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(byte RelateTypeId)
        {
            string RetVal = "";
            RelateTypes m_RelateTypes = new RelateTypes();
            m_RelateTypes.RelateTypeId = RelateTypeId;
            m_RelateTypes = m_RelateTypes.Get();
            RetVal = m_RelateTypes.RelateTypeName;
            return RetVal;
        }
        //--------------------------------------------------------------
        public List<RelateTypes> GetByGroupId(string DisplayPosition = "")
        {
            List<RelateTypes> retVal = new List<RelateTypes>();
            try
            {
                SqlCommand cmd = new SqlCommand("RelateTypes_GetByDocGroupId") {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@DisplayPosition", DisplayPosition));
                retVal = InitByDocGroupId(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

    }
}