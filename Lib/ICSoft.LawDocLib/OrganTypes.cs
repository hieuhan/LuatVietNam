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
    /// class OrganTypes
    /// </summary>
    public class OrganTypes
    {
        private byte _OrganTypeId;
        private string _OrganTypeName;
        private string _OrganTypeDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public OrganTypes()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public OrganTypes(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~OrganTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte OrganTypeId
        {
            get { return _OrganTypeId; }
            set { _OrganTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string OrganTypeName
        {
            get { return _OrganTypeName; }
            set { _OrganTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string OrganTypeDesc
        {
            get { return _OrganTypeDesc; }
            set { _OrganTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<OrganTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<OrganTypes> l_OrganTypes = new List<OrganTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    OrganTypes m_OrganTypes = new OrganTypes(db.ConnectionString);
                    m_OrganTypes.OrganTypeId = smartReader.GetByte("OrganTypeId");
                    m_OrganTypes.OrganTypeName = smartReader.GetString("OrganTypeName");
                    m_OrganTypes.OrganTypeDesc = smartReader.GetString("OrganTypeDesc");
                    m_OrganTypes.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_OrganTypes.Add(m_OrganTypes);
                }
                reader.Close();
                return l_OrganTypes;
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
                SqlCommand cmd = new SqlCommand("OrganTypes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeName", this.OrganTypeName));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeDesc", this.OrganTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", this.OrganTypeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.OrganTypeId = Convert.ToByte((cmd.Parameters["@OrganTypeId"].Value == null) ? 0 : (cmd.Parameters["@OrganTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("OrganTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", this.OrganTypeId));
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
        public List<OrganTypes> GetList()
        {
            List<OrganTypes> RetVal = new List<OrganTypes>();
            try
            {
                string sql = "SELECT * FROM V$OrganTypes ORDER BY OrganTypeName";
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
        public static List<OrganTypes> Static_GetList(string ConStr)
        {
            List<OrganTypes> RetVal = new List<OrganTypes>();
            try
            {
                OrganTypes m_OrganTypes = new OrganTypes(ConStr);
                RetVal = m_OrganTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<OrganTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<OrganTypes> GetListByOrganTypeId(byte OrganTypeId)
        {
            List<OrganTypes> RetVal = new List<OrganTypes>();
            try
            {
                if (OrganTypeId > 0)
                {
                    string sql = "SELECT * FROM V$OrganTypes WHERE (OrganTypeId=" + OrganTypeId.ToString() + ")";
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
        public OrganTypes Get(byte OrganTypeId)
        {
            OrganTypes RetVal = new OrganTypes(db.ConnectionString);
            try
            {
                List<OrganTypes> list = GetListByOrganTypeId(OrganTypeId);
                if (list.Count > 0)
                {
                    RetVal = (OrganTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public OrganTypes Get()
        {
            return Get(this.OrganTypeId);
        }
        //-------------------------------------------------------------- 
        public static OrganTypes Static_Get(byte OrganTypeId)
        {
            return Static_Get(OrganTypeId);
        }
        //-----------------------------------------------------------------------------
        public static OrganTypes Static_Get(byte OrganTypeId, List<OrganTypes> lList)
        {
            OrganTypes RetVal = new OrganTypes();
            if (OrganTypeId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.OrganTypeId == OrganTypeId);
                if (RetVal == null) RetVal = new OrganTypes();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<OrganTypes> OrganTypes_GetList(int ActUserId, string OrderBy, byte OrganTypeId, string OrganTypeName)
        {
            List<OrganTypes> RetVal = new List<OrganTypes>();
            try
            {
                SqlCommand cmd = new SqlCommand("OrganTypes_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeName", StringUtil.InjectionString(OrganTypeName)));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
   }
}