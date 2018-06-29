using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class CustomerFields
    {
        #region Private Properties
        private int _CustomerFieldId;
        private int _CustomerId;
        private short _FieldId;
        private byte _DocGroupId;
        private short _DisplayOrder;
        private DateTime _CrDateTime;
        private DBAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public CustomerFields()
        {
            db = new DBAccess(Constants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public CustomerFields(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~CustomerFields()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public int CustomerFieldId
        {
            get
            {
                return _CustomerFieldId;
            }
            set
            {
                _CustomerFieldId = value;
            }
        }

        public int CustomerId
        {
            get
            {
                return _CustomerId;
            }
            set
            {
                _CustomerId = value;
            }
        }
        public short FieldId
        {
            get
            {
                return _FieldId;
            }
            set
            {
                _FieldId = value;
            }
        }
        public byte DocGroupId
        {
            get
            {
                return _DocGroupId;
            }
            set
            {
                _DocGroupId = value;
            }
        }
        public short DisplayOrder
        {
            get
            {
                return _DisplayOrder;
            }
            set
            {
                _DisplayOrder = value;
            }
        }
        public DateTime CrDateTime
        {
            get
            {
                return _CrDateTime;
            }
            set
            {
                _CrDateTime = value;
            }
        }



        #endregion
        //-----------------------------------------------------------
        #region Method
        private List<CustomerFields> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerFields> l_CustomerFields = new List<CustomerFields>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerFields m_CustomerFields = new CustomerFields();
                    m_CustomerFields.CustomerFieldId = smartReader.GetInt32("CustomerFieldId");
                    m_CustomerFields.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerFields.FieldId = smartReader.GetInt16("FieldId");
                    m_CustomerFields.DocGroupId = smartReader.GetByte("DocGroupId");
                    m_CustomerFields.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_CustomerFields.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_CustomerFields.Add(m_CustomerFields);
                }
                reader.Close();
                return l_CustomerFields;
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
        public byte Insert(byte Replicated, int ActUserId, ref int SysMessageId)
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
        public byte Update(byte Replicated, int ActUserId, ref int SysMessageId)
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
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerFields_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@CustomerFieldId", this.CustomerFieldId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerFieldId = int.Parse(cmd.Parameters["@CustomerFieldId"].Value.ToString());
                SysMessageId = Convert.ToInt32((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerFields_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerFieldId", this.CustomerFieldId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = int.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                RetVal = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public CustomerFields Get()
        {
            CustomerFields retVal = new CustomerFields();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {

                List<CustomerFields> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //-------------------------------------------------------------- 
        public List<CustomerFields> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerFields_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerFieldId", this.CustomerFieldId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<CustomerFields> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------  
        public List<CustomerFields> GetListFieldsByCustomerId(int CustomerId)
        {
            List<CustomerFields> RetVal = new List<CustomerFields>();
            try
            {
                if (CustomerId > 0)
                {
                    string sql = "SELECT * FROM CustomerFields WHERE (CustomerId=" + CustomerId.ToString() + ")";
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
        public List<CustomerFields> GetListFieldsByCustomerId(int CustomerId, byte ReviewStatusId = 2)
        {
            List<CustomerFields> RetVal = new List<CustomerFields>();
            try
            {
                if (CustomerId > 0)
                {
                    string sql = "SELECT * FROM CustomerFields WHERE (CustomerId=" + CustomerId + ") AND FieldId IN (SELECT FieldId FROM Fields WHERE ReviewStatusId = "+ ReviewStatusId +")";
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
        //-----------------------------------------------------------
        public byte InsertByListId(string listFieldId = "")
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerFields_InsertByListId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", this.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", listFieldId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", this.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToByte(cmd.Parameters["@SysMessageId"].Value ?? "0");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DataSet CustomerFields_GetCount(int CustomerId, byte LanguageId, int DocGroupId, byte GetCountByField, byte GetCountByProvince)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerFields_GetCount");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@GetCountByField", GetCountByField));
                cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", GetCountByProvince));
                RetVal = db.getDataSet(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static DataTable Static_GetCount_GetListProvince(DataSet ds)
        {
            DataTable RetVal = new DataTable();
            try
            {
                RetVal = ds.Tables[1];
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static DataTable Static_GetCount_GetListField(DataSet ds)
        {
            DataTable RetVal = new DataTable();
            try
            {
                RetVal = ds.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 

        #endregion
    } 
}
