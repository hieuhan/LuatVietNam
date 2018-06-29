
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
    public class PaymentTypes
    {
        private byte _PaymentTypeId;
        private string _PaymentTypeName;
        private string _PaymentTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public PaymentTypes()
        {
            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
        }
        //-----------------------------------------------------------------        
        public PaymentTypes(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~PaymentTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte PaymentTypeId
        {
            get { return _PaymentTypeId; }
            set { _PaymentTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string PaymentTypeName
        {
            get { return _PaymentTypeName; }
            set { _PaymentTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string PaymentTypeDesc
        {
            get { return _PaymentTypeDesc; }
            set { _PaymentTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<PaymentTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<PaymentTypes> l_PaymentTypes = new List<PaymentTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PaymentTypes m_PaymentTypes = new PaymentTypes(db.ConnectionString);
                    m_PaymentTypes.PaymentTypeId = smartReader.GetByte("PaymentTypeId");
                    m_PaymentTypes.PaymentTypeName = smartReader.GetString("PaymentTypeName");
                    m_PaymentTypes.PaymentTypeDesc = smartReader.GetString("PaymentTypeDesc");

                    l_PaymentTypes.Add(m_PaymentTypes);
                }
                reader.Close();
                return l_PaymentTypes;
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
                SqlCommand cmd = new SqlCommand("PaymentTypes_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeName", this.PaymentTypeName));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeDesc", this.PaymentTypeDesc));
                cmd.Parameters.Add("@PaymentTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PaymentTypeId = Convert.ToByte((cmd.Parameters["@PaymentTypeId"].Value == null) ? 0 : (cmd.Parameters["@PaymentTypeId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTypes_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeName", this.PaymentTypeName));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeDesc", this.PaymentTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PaymentTypes_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", this.PaymentTypeId));
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
        public List<PaymentTypes> GetList()
        {
            List<PaymentTypes> RetVal = new List<PaymentTypes>();
            try
            {
                string sql = "SELECT * FROM PaymentTypes";
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
        public static List<PaymentTypes> Static_GetList(string ConStr)
        {
            List<PaymentTypes> RetVal = new List<PaymentTypes>();
            try
            {
                PaymentTypes m_PaymentTypes = new PaymentTypes(ConStr);
                RetVal = m_PaymentTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<PaymentTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<PaymentTypes> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            PaymentTypes m_PaymentTypes = new PaymentTypes(ConStr);
            List<PaymentTypes> RetVal = m_PaymentTypes.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_PaymentTypes.PaymentTypeName = TextOptionAll;
                m_PaymentTypes.PaymentTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_PaymentTypes);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<PaymentTypes> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<PaymentTypes> GetListOrderBy(string OrderBy)
        {
            List<PaymentTypes> RetVal = new List<PaymentTypes>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM PaymentTypes ";
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
        public static List<PaymentTypes> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            PaymentTypes m_PaymentTypes = new PaymentTypes(ConStr);
            return m_PaymentTypes.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<PaymentTypes> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<PaymentTypes> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<PaymentTypes> RetVal = new List<PaymentTypes>();
            PaymentTypes m_PaymentTypes = new PaymentTypes(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_PaymentTypes.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_PaymentTypes.PaymentTypeName = TextOptionAll;
                    m_PaymentTypes.PaymentTypeDesc = TextOptionAll;
                    RetVal.Insert(0, m_PaymentTypes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<PaymentTypes> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<PaymentTypes> GetListByPaymentTypeId(byte PaymentTypeId)
        {
            List<PaymentTypes> RetVal = new List<PaymentTypes>();
            try
            {
                if (PaymentTypeId > 0)
                {
                    string sql = "SELECT * FROM PaymentTypes WHERE (PaymentTypeId=" + PaymentTypeId.ToString() + ")";
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
        public PaymentTypes Get(byte PaymentTypeId)
        {
            PaymentTypes RetVal = new PaymentTypes(db.ConnectionString);
            try
            {
                List<PaymentTypes> list = GetListByPaymentTypeId(PaymentTypeId);
                if (list.Count > 0)
                {
                    RetVal = (PaymentTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public PaymentTypes Get()
        {
            return Get(this.PaymentTypeId);
        }
        //-------------------------------------------------------------- 
        public static PaymentTypes Static_Get(byte PaymentTypeId)
        {
            return new PaymentTypes().Get(PaymentTypeId);
        }
        //-----------------------------------------------------------------------------
        public static PaymentTypes Static_Get(byte PaymentTypeId, List<PaymentTypes> lList)
        {
            PaymentTypes RetVal = new PaymentTypes();
            if (PaymentTypeId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.PaymentTypeId == PaymentTypeId);
                if (RetVal == null) RetVal = new PaymentTypes();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}