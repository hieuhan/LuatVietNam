using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class CustomerRegisterResult
    {
        public int CustomerId = 0;
        public string CustomerName = "";
        public string RegisterCode = "";
        public string ActionStatus = "";
        public string ActionMessage = "";
    }
    //-----------------------------------------------------------
    public class CustomerLoginResult
    {
        public Customers mCustomers;
        public List<Roles> lRoles;
        public string ActionStatus = "";
        public string ActionMessage = "";
    }
    public class CustomerHelpers
    {
        private static DBAccess db;
        public static CustomerRegisterResult CustomerRegister(Customers mCustomers, byte ConfirmByEmail, byte ConfirmBySms, byte LanguageId)
        {
            CustomerRegisterResult RetVal = new CustomerRegisterResult();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_Register");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", mCustomers.CustomerName));
                cmd.Parameters.Add(new SqlParameter("@CustomerPass", mCustomers.CustomerPass));
                cmd.Parameters.Add(new SqlParameter("@CustomerFullName", mCustomers.CustomerFullName));
                cmd.Parameters.Add(new SqlParameter("@CustomerNickName", mCustomers.CustomerNickName));
                cmd.Parameters.Add(new SqlParameter("@CustomerMail", mCustomers.CustomerMail));
                cmd.Parameters.Add(new SqlParameter("@Website", mCustomers.Website));
                cmd.Parameters.Add(new SqlParameter("@Facebook", mCustomers.Facebook));
                cmd.Parameters.Add(new SqlParameter("@Avatar", mCustomers.Avatar));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", mCustomers.RegisterNewsletter));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", mCustomers.ApplicationId)); //Dịch vụ quan tâm
                cmd.Parameters.Add(new SqlParameter("@SiteId", mCustomers.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", mCustomers.CustomerMobile));
                cmd.Parameters.Add(new SqlParameter("@GenderId", mCustomers.GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", mCustomers.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@OrganName", mCustomers.OrganName));
                cmd.Parameters.Add(new SqlParameter("@OrganAddress", mCustomers.OrganAddress));
                cmd.Parameters.Add(new SqlParameter("@OrganPhone", mCustomers.OrganPhone));
                cmd.Parameters.Add(new SqlParameter("@OrganTaxCode", mCustomers.OrganTaxCode));
                cmd.Parameters.Add(new SqlParameter("@AccountNumber", mCustomers.AccountNumber));
                cmd.Parameters.Add(mCustomers.DateOfBirth == DateTime.MinValue
                    ? new SqlParameter("@DateOfBirth", DBNull.Value)
                    : new SqlParameter("@DateOfBirth", mCustomers.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", mCustomers.CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", mCustomers.OccupationId));
                cmd.Parameters.Add(new SqlParameter("@Address", mCustomers.Address));
                cmd.Parameters.Add(new SqlParameter("@ConfirmByEmail", ConfirmByEmail));
                cmd.Parameters.Add(new SqlParameter("@ConfirmBySms", ConfirmBySms));
                cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RegisterCode", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);
                RetVal.CustomerId = Convert.ToInt32((cmd.Parameters["@CustomerId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CustomerId"].Value);
                RetVal.RegisterCode = cmd.Parameters["@RegisterCode"].Value.ToString();
                RetVal.ActionStatus = cmd.Parameters["@ActionStatus"].Value.ToString();
                RetVal.ActionMessage = cmd.Parameters["@ActionMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //------------------------------------------------------------------------
        public static byte Update(Customers mCustomers)
        {
            byte Replicated=1;
            int ActUserId=0;
            short SysMessageId=0;
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", mCustomers.CustomerName));
                cmd.Parameters.Add(new SqlParameter("@CustomerPass", mCustomers.CustomerPass));
                cmd.Parameters.Add(new SqlParameter("@CustomerFullName", mCustomers.CustomerFullName));
                cmd.Parameters.Add(new SqlParameter("@CustomerNickName", mCustomers.CustomerNickName));
                cmd.Parameters.Add(new SqlParameter("@CustomerMail", mCustomers.CustomerMail));
                cmd.Parameters.Add(new SqlParameter("@Website", mCustomers.Website));
                cmd.Parameters.Add(new SqlParameter("@Facebook", mCustomers.Facebook));
                if (!string.IsNullOrEmpty(mCustomers.Avatar))
                {
                    cmd.Parameters.Add(new SqlParameter("@Avatar", mCustomers.Avatar));
                }
                cmd.Parameters.Add(new SqlParameter("@SiteId", mCustomers.SiteId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", mCustomers.StatusId));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", mCustomers.CustomerMobile));
                cmd.Parameters.Add(new SqlParameter("@GenderId", mCustomers.GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", mCustomers.ProvinceId));
                if (mCustomers.DateOfBirth == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", mCustomers.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@Identifier", mCustomers.Identifier));
                cmd.Parameters.Add(new SqlParameter("@EmailAuto", mCustomers.EmailAuto));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", mCustomers.ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", mCustomers.CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", mCustomers.OccupationId));
                cmd.Parameters.Add(new SqlParameter("@OrganOccupationId", mCustomers.OrganOccupationId));
                cmd.Parameters.Add(new SqlParameter("@Address", mCustomers.Address));
                cmd.Parameters.Add(new SqlParameter("@OrganTaxCode", mCustomers.OrganTaxCode));
                cmd.Parameters.Add(new SqlParameter("@AccountNumber", mCustomers.AccountNumber));
                cmd.Parameters.Add(new SqlParameter("@OrganName", mCustomers.OrganName));
                cmd.Parameters.Add(new SqlParameter("@OrganPhone", mCustomers.OrganPhone));
                cmd.Parameters.Add(new SqlParameter("@OrganFax", mCustomers.OrganFax));
                cmd.Parameters.Add(new SqlParameter("@OrganAddress", mCustomers.OrganAddress));
                cmd.Parameters.Add(new SqlParameter("@Note", mCustomers.Note));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId", mCustomers.InfoChannelId));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", mCustomers.RegisterNewsletter));
                cmd.Parameters.Add(new SqlParameter("@MaxConcurrentLogin", mCustomers.MaxConcurrentLogin));
                cmd.Parameters.Add(new SqlParameter("@LockPassword", mCustomers.LockPassword));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", mCustomers.CustomerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);
                mCustomers.CustomerId = Convert.ToInt32((cmd.Parameters["@CustomerId"].Value == null) ? 0 : (cmd.Parameters["@CustomerId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static CustomerRegisterResult ConfirmActive(byte LanguageId, int CustomerId, string ActiveCode)
        {
            CustomerRegisterResult RetVal = new CustomerRegisterResult();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_RegisterActive");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@RegisterCode", ActiveCode));
                cmd.Parameters.Add("@RegisterStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RegisterMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;

                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);
                if (cmd.Parameters["@CustomerName"].Value != DBNull.Value) RetVal.CustomerName = cmd.Parameters["@CustomerName"].Value.ToString();
                if (cmd.Parameters["@RegisterStatus"].Value != DBNull.Value) RetVal.ActionStatus = cmd.Parameters["@RegisterStatus"].Value.ToString();
                if (cmd.Parameters["@RegisterMessage"].Value != DBNull.Value) RetVal.ActionMessage = cmd.Parameters["@RegisterMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static CustomerLoginResult Login(string CustomerName, string CustomerPass, string FromIP, string UserAgent, byte LanguageId)
        {
            CustomerLoginResult RetVal = new CustomerLoginResult();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_Login");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@CustomerPass", CustomerPass));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", UserAgent));
                cmd.Parameters.Add("@LoginStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@LoginMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<Customers> lCustomers = Customers.Static_Init(smartReader);
                RetVal.mCustomers = lCustomers.Count == 0 ? new Customers() : lCustomers[0];
                reader.NextResult();
                RetVal.lRoles = Roles.Static_Init(smartReader);
                reader.Close();

                if (cmd.Parameters["@LoginStatus"].Value != DBNull.Value) RetVal.ActionStatus = cmd.Parameters["@LoginStatus"].Value.ToString();
                if (cmd.Parameters["@LoginMessage"].Value != DBNull.Value) RetVal.ActionMessage = cmd.Parameters["@LoginMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static CustomerLoginResult LoginOpenId(short SiteId, byte OpenAuthTypeId, string OpenId, string Email, string Name, string FromIP, string UserAgent)
        {
            CustomerLoginResult RetVal = new CustomerLoginResult();
            try
            {
                SqlCommand cmd = new SqlCommand("OpenAuths_Check");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@OpenAuthTypeId", OpenAuthTypeId));
                cmd.Parameters.Add(new SqlParameter("@OpenId", OpenId));
                cmd.Parameters.Add(new SqlParameter("@Email", Email));
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", UserAgent));
                cmd.Parameters.Add("@LoginStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@LoginMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<Customers> lCustomers = Customers.Static_Init(smartReader);
                RetVal.mCustomers = lCustomers.Count == 0 ? new Customers() : lCustomers[0];

                reader.Close();

                if (cmd.Parameters["@LoginStatus"].Value != DBNull.Value) RetVal.ActionStatus = cmd.Parameters["@LoginStatus"].Value.ToString();
                if (cmd.Parameters["@LoginMessage"].Value != DBNull.Value) RetVal.ActionMessage = cmd.Parameters["@LoginMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static CustomerLoginResult ChangePass(string CustomerName, string CustomerPass, string CustomerPassOld, string FromIP, string UserAgent, byte LanguageId)
        {
            CustomerLoginResult RetVal = new CustomerLoginResult();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ChangePass");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@CustomerPass", CustomerPass));
                cmd.Parameters.Add(new SqlParameter("@CustomerPassOld", CustomerPassOld));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", UserAgent));
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);

                if (cmd.Parameters["@ActionStatus"].Value != DBNull.Value) RetVal.ActionStatus = cmd.Parameters["@ActionStatus"].Value.ToString();
                if (cmd.Parameters["@ActionMessage"].Value != DBNull.Value) RetVal.ActionMessage = cmd.Parameters["@ActionMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static string ResetPass(byte LanguageId, int CustomerId, string CustomerName, string ConfirmCode, string CustomerPass, string FromIP, string UserAgent)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ResetPass");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@ConfirmCode", ConfirmCode));
                cmd.Parameters.Add(new SqlParameter("@CustomerPass", CustomerPass));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", UserAgent));
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@ActionMessage"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static string ForgotPassword(byte LanguageId, string CustomerName)
        {
            string RetVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_ForgotPassword");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToString(cmd.Parameters["@ActionMessage"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static Customers GetByCustomerId(int CustomerId)
        {
            Customers RetVal = new Customers();
            try
            {
                string sql = "SELECT * FROM Customers WHERE CustomerId=" + CustomerId.ToString();
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<Customers> lCustomers = Customers.Static_Init(smartReader);
                RetVal = lCustomers.Count == 0 ? new Customers() : lCustomers[0];
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public static Customers GetByCustomerName(string CustomerName)
        {
            Customers RetVal = new Customers();
            try
            {
                string sql = "SELECT * FROM Customers WHERE CustomerName='" + StringUtil.injectionString(CustomerName) + "'";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                db = new DBAccess(Constants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<Customers> lCustomers = Customers.Static_Init(smartReader);
                RetVal = lCustomers.Count == 0 ? new Customers() : lCustomers[0];
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
    }
}
