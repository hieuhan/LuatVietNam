
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
    /// class OfficeTypes
    /// </summary>
    public class OfficeTypes
    {
        private byte _OfficeTypeId;
        private string _OfficeTypeName;
        private string _OfficeTypeDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public OfficeTypes()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public OfficeTypes(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~OfficeTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte OfficeTypeId
        {
            get { return _OfficeTypeId; }
            set { _OfficeTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string OfficeTypeName
        {
            get { return _OfficeTypeName; }
            set { _OfficeTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string OfficeTypeDesc
        {
            get { return _OfficeTypeDesc; }
            set { _OfficeTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<OfficeTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<OfficeTypes> l_OfficeTypes = new List<OfficeTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    OfficeTypes m_OfficeTypes = new OfficeTypes(db.ConnectionString);
                    m_OfficeTypes.OfficeTypeId = smartReader.GetByte("OfficeTypeId");
                    m_OfficeTypes.OfficeTypeName = smartReader.GetString("OfficeTypeName");
                    m_OfficeTypes.OfficeTypeDesc = smartReader.GetString("OfficeTypeDesc");
                    m_OfficeTypes.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_OfficeTypes.Add(m_OfficeTypes);
                }
                reader.Close();
                return l_OfficeTypes;
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
                SqlCommand cmd = new SqlCommand("OfficeTypes_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OfficeTypeName", this.OfficeTypeName));
                cmd.Parameters.Add(new SqlParameter("@OfficeTypeDesc", this.OfficeTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@OfficeTypeId", this.OfficeTypeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.OfficeTypeId = Convert.ToByte((cmd.Parameters["@OfficeTypeId"].Value == null) ? 0 : (cmd.Parameters["@OfficeTypeId"].Value));
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
                SqlCommand cmd = new SqlCommand("OfficeTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OfficeTypeId", this.OfficeTypeId));
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
        public List<OfficeTypes> GetList()
        {
            List<OfficeTypes> RetVal = new List<OfficeTypes>();
            try
            {
                string sql = "SELECT * FROM V$OfficeTypes ORDER BY OfficeTypeName";
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
        public static List<OfficeTypes> Static_GetList(string ConStr)
        {
            List<OfficeTypes> RetVal = new List<OfficeTypes>();
            try
            {
                OfficeTypes m_OfficeTypes = new OfficeTypes(ConStr);
                RetVal = m_OfficeTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<OfficeTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<OfficeTypes> GetListByOfficeTypeId(byte OfficeTypeId)
        {
            List<OfficeTypes> RetVal = new List<OfficeTypes>();
            try
            {
                if (OfficeTypeId > 0)
                {
                    string sql = "SELECT * FROM V$OfficeTypes WHERE (OfficeTypeId=" + OfficeTypeId.ToString() + ")";
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
        public OfficeTypes Get(byte OfficeTypeId)
        {
            OfficeTypes RetVal = new OfficeTypes(db.ConnectionString);
            try
            {
                List<OfficeTypes> list = GetListByOfficeTypeId(OfficeTypeId);
                if (list.Count > 0)
                {
                    RetVal = (OfficeTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public OfficeTypes Get()
        {
            return Get(this.OfficeTypeId);
        }
        //-------------------------------------------------------------- 
        public static OfficeTypes Static_Get(byte OfficeTypeId)
        {
            return Static_Get(OfficeTypeId);
        }
        //-----------------------------------------------------------------------------
        public static OfficeTypes Static_Get(byte OfficeTypeId, List<OfficeTypes> lList)
        {
            OfficeTypes RetVal = new OfficeTypes();
            if (OfficeTypeId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.OfficeTypeId == OfficeTypeId);
                if (RetVal == null) RetVal = new OfficeTypes();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<OfficeTypes> OfficeTypes_GetList(int ActUserId, string OrderBy, string OfficeTypeName)
        {
            List<OfficeTypes> RetVal = new List<OfficeTypes>();
            try
            {
                SqlCommand cmd = new SqlCommand("OfficeTypes_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@OfficeTypeName", StringUtil.InjectionString(OfficeTypeName)));
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