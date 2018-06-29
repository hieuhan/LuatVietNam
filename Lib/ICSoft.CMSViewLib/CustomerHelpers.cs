using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using sms.database;

namespace ICSoft.CMSViewLib
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
        public ServiceRoles lServiceRoles;
        public List<Roles> lRoles;
        public string ActionStatus = "";
        public string ActionMessage = "";
    }
    //-----------------------------------------------------------
	public class CustomerHelpers
	{
        private static DBAccess db;
        //-----------------------------------------------------------
        public static CustomerRegisterResult Register(Customers mCustomers, byte ConfirmByEmail, byte ConfirmBySms, byte LanguageId)
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
                cmd.Parameters.Add(new SqlParameter("@Avatar", mCustomers.Avatar));
                cmd.Parameters.Add(new SqlParameter("@RegisterNewsletter", mCustomers.RegisterNewsletter));
                cmd.Parameters.Add(new SqlParameter("@Website", mCustomers.Website));
                cmd.Parameters.Add(new SqlParameter("@Facebook", mCustomers.Facebook));
                cmd.Parameters.Add(new SqlParameter("@SiteId", mCustomers.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", mCustomers.CustomerMobile));
                cmd.Parameters.Add(new SqlParameter("@GenderId", mCustomers.GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", mCustomers.ProvinceId));
                if (mCustomers.DateOfBirth == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", mCustomers.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", mCustomers.CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", mCustomers.OccupationId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", mCustomers.ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@Address", mCustomers.Address));
                cmd.Parameters.Add(new SqlParameter("@ConfirmByEmail", ConfirmByEmail));
                cmd.Parameters.Add(new SqlParameter("@ConfirmBySms", ConfirmBySms));
                cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RegisterCode", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
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

	            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
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

        //-----------------------------------------------------------
        public static CustomerRegisterResult GetCodeResetPassword(string CustomerName, string CustomerMail, string CustomerMobile)
        {
            CustomerRegisterResult RetVal = new CustomerRegisterResult();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetCodeResetPassword");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@CustomerMail", CustomerMail));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", CustomerMobile));
                cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RegisterCode", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
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
        //-----------------------------------------------------------
        public static CustomerRegisterResult CheckActiveCode(int CustomerId, string RegisterCode)
        {
            CustomerRegisterResult RetVal = new CustomerRegisterResult();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_CheckActiveCode");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@RegisterCode", RegisterCode));
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);
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

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);
                if (cmd.Parameters["@CustomerName"].Value !=DBNull.Value) RetVal.CustomerName = cmd.Parameters["@CustomerName"].Value.ToString();
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

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<Customers> lCustomers = InitViewHelpers.InitCustomers(smartReader);
                RetVal.mCustomers = lCustomers.Count == 0 ? new Customers() : lCustomers[0];
                reader.NextResult();
                RetVal.lRoles = InitViewHelpers.InitRoles(smartReader);
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

	    public static void LogoutSession(string sessionId = "")
	    {
	        try
	        {
	            SqlCommand cmd = new SqlCommand("CustomerOnline_Logout")
	            {
	                CommandType = CommandType.StoredProcedure
	            };
                cmd.Parameters.Add(new SqlParameter("@SessionId", sessionId));
	            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
	            db.ExecuteSQL(cmd);
	        }
	        catch (Exception ex)
	        {
	            throw ex;
	        }
	        finally
	        {
	            db.closeConnection(db.getConnection());
	        }
	    }
        //-----------------------------------------------------------
        public static List<CustomerOnline> CustomerOnline_GetAll(string SessionId , string CustomerName, string FromIP)
        {
            List<CustomerOnline> RetVal = new List<CustomerOnline>();
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerOnline_GetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SessionId", SessionId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<CustomerOnline> lCustomerOnline = InitViewHelpers.InitCustomersOnline(smartReader);
                RetVal = lCustomerOnline;
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

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<Customers> lCustomers = InitViewHelpers.InitCustomers(smartReader);
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

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
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
        public static ActionResults Estate_Chuyenkhoan(int ActUserId, int Sotien, int FromCustomerId, int ToCustomerId)
        {
            ActionResults RetVal = new ActionResults();
            try
            {
                SqlCommand cmd = new SqlCommand("Estate_Chuyenkhoan");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@Sotien", Sotien));
                cmd.Parameters.Add(new SqlParameter("@FromCustomerId", FromCustomerId));
                cmd.Parameters.Add(new SqlParameter("@ToCustomerId", ToCustomerId));
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(CMSLibEstate.EstateConstants.CONN_ESTATE);
                db.ExecuteSQL(cmd);
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
        //-----------------------------------------------------------
        public static ActionResults Estate_NaptienMuaxu(int ActUserId, int Sotien, int CustomerId, short SiteId, byte PaymentTypeId)
        {
            ActionResults RetVal = new ActionResults();
            try
            {
                SqlCommand cmd = new SqlCommand("Estate_NaptienMuaxu");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@Sotien", Sotien));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(CMSLibEstate.EstateConstants.CONN_ESTATE);
                db.ExecuteSQL(cmd);
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
        //-----------------------------------------------------------
        public static ActionResults Estate_NaptienMuagoi(int ActUserId, string Magoi, int CustomerId, short SiteId, byte PaymentTypeId)
        {
            ActionResults RetVal = new ActionResults();
            try
            {
                SqlCommand cmd = new SqlCommand("Estate_NaptienMuagoi");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@Magoi", Magoi));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(CMSLibEstate.EstateConstants.CONN_ESTATE);
                db.ExecuteSQL(cmd);
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
        //-----------------------------------------------------------
        public static ActionResults Estate_NaptienGiftCode(int ActUserId, string GiftCode, int CustomerId, short SiteId, byte PaymentTypeId)
        {
            ActionResults RetVal = new ActionResults();
            try
            {
                SqlCommand cmd = new SqlCommand("Estate_NaptienGiftCode");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@GiftCode", GiftCode));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", PaymentTypeId));
                cmd.Parameters.Add("@ActionStatus", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ActionMessage", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;

                db = new DBAccess(CMSLibEstate.EstateConstants.CONN_ESTATE);
                db.ExecuteSQL(cmd);
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
        //-----------------------------------------------------------
        public static Customers GetByCustomerId(int CustomerId)
        {
            Customers RetVal = new Customers();
            try
            {
                string sql = "SELECT * FROM Customers WHERE CustomerId=" + CustomerId.ToString();
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<Customers> lCustomers = InitViewHelpers.InitCustomers(smartReader);
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
                string sql = "SELECT * FROM Customers WHERE CustomerName='" + sms.utils.StringUtil.InjectionString(CustomerName) + "'";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<Customers> lCustomers = InitViewHelpers.InitCustomers(smartReader);
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
        public static int GetTotalBalance(int CustomerId, string CustomerName = "", byte AccountTypeId = 0)
        {
            int RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerAccounts_GetTotalBalance");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@AccountTypeId", AccountTypeId));
                cmd.Parameters.Add("@TotalBalance", SqlDbType.Int).Direction = ParameterDirection.Output;

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);

                if (cmd.Parameters["@TotalBalance"].Value != DBNull.Value) RetVal = int.Parse(cmd.Parameters["@TotalBalance"].Value.ToString());
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
        public static byte GetAccountTypeId_SubMoney(int CustomerId, string CustomerName, int Amount)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerAccounts_GetAccountTypeId_SubMoney");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@Amount", Amount));
                cmd.Parameters.Add("@AccountTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);

                if (cmd.Parameters["@AccountTypeId"].Value != DBNull.Value) RetVal = byte.Parse(cmd.Parameters["@AccountTypeId"].Value.ToString());
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
        public void UpdateProfile(Customers mCustomers)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_UpdateProfile_Quick");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerFullName", mCustomers.CustomerFullName));
                cmd.Parameters.Add(new SqlParameter("@CustomerNickName", mCustomers.CustomerNickName));
                cmd.Parameters.Add(new SqlParameter("@CustomerMail", mCustomers.CustomerMail));
                cmd.Parameters.Add(new SqlParameter("@Website", mCustomers.Website));
                cmd.Parameters.Add(new SqlParameter("@Facebook", mCustomers.Facebook));
                cmd.Parameters.Add(new SqlParameter("@CustomerMobile", mCustomers.CustomerMobile));
                cmd.Parameters.Add(new SqlParameter("@GenderId", mCustomers.GenderId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", mCustomers.ProvinceId));
                if (mCustomers.DateOfBirth == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", mCustomers.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@OccupationId", mCustomers.OccupationId));
                cmd.Parameters.Add(new SqlParameter("@Address", mCustomers.Address));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", mCustomers.CustomerId));
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

	    public static CustomersViewDetail LawsCustomer_GetViewDetail(int customerId, byte docGroupId, byte registTypeId, byte languageId, ref int countVbpl, ref int countVbhn, ref int countTcvn)
	    {
	        DBAccess db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
	        SqlConnection con = db.getConnection();
            CustomersViewDetail retVal = new CustomersViewDetail();
	        try
	        {
	            SqlCommand cmd =
	                new SqlCommand("LawsCustomers_GetViewDetail") {CommandType = CommandType.StoredProcedure};
	            cmd.Parameters.Add(new SqlParameter("@CustomerId", customerId));
                cmd.Parameters.Add(new SqlParameter("@RegistTypeId", registTypeId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", docGroupId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", languageId));
                cmd.Parameters.Add("@CountVBPL", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountVBHN", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountTCVN", SqlDbType.Int).Direction = ParameterDirection.Output;

	            cmd.Connection = con;
	            con.Open();
	            SqlDataReader reader = cmd.ExecuteReader();
	            SmartDataReader smartReader = new SmartDataReader(reader);
	            retVal.mCustomers = InitViewHelpers.InitCustomersOne(smartReader);
	            reader.NextResult();
                retVal.ListCustomerFieldsView = InitViewHelpers.InitCustomerFieldsView(smartReader);
	            reader.NextResult();
                retVal.mCustomerServicesView = InitViewHelpers.InitCustomerServicesViewOne(smartReader);
	            reader.NextResult();
	            retVal.mCustomerAccessLogs = InitViewHelpers.InitCustomerAccessLogsOne(smartReader);
	            reader.Close();
                countVbpl = Convert.ToInt32((cmd.Parameters["@CountVBPL"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountVBPL"].Value);
                countVbhn = Convert.ToInt32((cmd.Parameters["@CountVBHN"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountVBHN"].Value);
                countTcvn = Convert.ToInt32((cmd.Parameters["@CountTCVN"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountTCVN"].Value);
	        }
	        catch (Exception ex)
	        {
	            throw ex;
	        }
	        finally
	        {
	            db.closeConnection(db.getConnection());
	        }
	        return retVal;
	    }

        public static void WidgetUser_GetViewCount(int customerId, byte languageId, byte getCountMessage, byte getCountCustomerDocs, byte getCountThongBaoHieuLuc, byte GetCountPaymentTransactionSuccess, byte GetEndTimeService, ref int countMessage, ref int countCustomerDocs, ref int countThongBaoHieuLuc, ref int CountPaymentTransactionSuccess, ref DateTime EndTime)
	    {
	        try
	        {
	            SqlCommand cmd = new SqlCommand("WidgetUser_GetViewCount") {CommandType = CommandType.StoredProcedure};
	            cmd.Parameters.Add(new SqlParameter("@CustomerId", customerId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", languageId));
                cmd.Parameters.Add(new SqlParameter("@GetCountMessage", getCountMessage));
                cmd.Parameters.Add(new SqlParameter("@GetCountCustomerDocs", getCountCustomerDocs));
                cmd.Parameters.Add(new SqlParameter("@GetCountThongBaoHieuLuc", getCountThongBaoHieuLuc));
                cmd.Parameters.Add(new SqlParameter("@GetCountPaymentTransactionSuccess", GetCountPaymentTransactionSuccess));
                cmd.Parameters.Add(new SqlParameter("@GetEndTimeService", GetEndTimeService));
                cmd.Parameters.Add("@CountMessage", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountCustomerDocs", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountThongBaoHieuLuc", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountPaymentTransactionSuccess", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@EndTimeService", SqlDbType.DateTime).Direction = ParameterDirection.Output;
	            db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
	            db.ExecuteSQL(cmd);
                countMessage = Convert.ToInt32((cmd.Parameters["@CountMessage"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountMessage"].Value);
                countCustomerDocs = Convert.ToInt32((cmd.Parameters["@CountCustomerDocs"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountCustomerDocs"].Value);
                countThongBaoHieuLuc = Convert.ToInt32((cmd.Parameters["@CountThongBaoHieuLuc"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountThongBaoHieuLuc"].Value);
                CountPaymentTransactionSuccess = Convert.ToInt32((cmd.Parameters["@CountPaymentTransactionSuccess"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountPaymentTransactionSuccess"].Value);
                EndTime = cmd.Parameters["@EndTimeService"].Value == DBNull.Value ? DateTime.MinValue : (DateTime)cmd.Parameters["@EndTimeService"].Value;
	        }
	        catch (Exception ex)
	        {
	            throw ex;
	        }
	        finally
	        {
	            db.closeConnection(db.getConnection());
	        }
	    }

        public static void WidgetUser_GetViewCountEN(int customerId, byte languageId, byte getCountMessage, byte getCountCustomerDocs, byte getCountThongBaoHieuLuc, byte GetCountPaymentTransactionSuccess, byte GetEndTimeService, ref int countMessage, ref int countCustomerDocs, ref int countThongBaoHieuLuc, ref int CountPaymentTransactionSuccess, ref DateTime EndTime)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("WidgetUser_GetViewCountEN") { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@CustomerId", customerId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", languageId));
                cmd.Parameters.Add(new SqlParameter("@GetCountMessage", getCountMessage));
                cmd.Parameters.Add(new SqlParameter("@GetCountCustomerDocs", getCountCustomerDocs));
                cmd.Parameters.Add(new SqlParameter("@GetCountThongBaoHieuLuc", getCountThongBaoHieuLuc));
                cmd.Parameters.Add(new SqlParameter("@GetCountPaymentTransactionSuccess", GetCountPaymentTransactionSuccess));
                cmd.Parameters.Add(new SqlParameter("@GetEndTimeService", GetEndTimeService));
                cmd.Parameters.Add("@CountMessage", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountCustomerDocs", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountThongBaoHieuLuc", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CountPaymentTransactionSuccess", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@EndTimeService", SqlDbType.DateTime).Direction = ParameterDirection.Output;
                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                db.ExecuteSQL(cmd);
                countMessage = Convert.ToInt32((cmd.Parameters["@CountMessage"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountMessage"].Value);
                countCustomerDocs = Convert.ToInt32((cmd.Parameters["@CountCustomerDocs"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountCustomerDocs"].Value);
                countThongBaoHieuLuc = Convert.ToInt32((cmd.Parameters["@CountThongBaoHieuLuc"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountThongBaoHieuLuc"].Value);
                CountPaymentTransactionSuccess = Convert.ToInt32((cmd.Parameters["@CountPaymentTransactionSuccess"].Value == DBNull.Value) ? "0" : cmd.Parameters["@CountPaymentTransactionSuccess"].Value);
                EndTime = cmd.Parameters["@EndTimeService"].Value == DBNull.Value ? DateTime.MinValue : (DateTime)cmd.Parameters["@EndTimeService"].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
        }

        //-----------------------------------------------------------
        public static CustomerLoginResult GetRolesByCustomerId(int customerId)
        {
            CustomerLoginResult RetVal = new CustomerLoginResult();
            try
            {
                SqlCommand cmd = new SqlCommand("Customers_GetRolesById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CustomerId", customerId));

                db = new DBAccess(DocConstants.CUSTOMER_CONSTR);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                List<Customers> lCustomers = InitViewHelpers.InitCustomers(smartReader);
                RetVal.mCustomers = lCustomers.Count == 0 ? new Customers() : lCustomers[0];
                reader.NextResult();
                RetVal.lRoles = InitViewHelpers.InitRoles(smartReader);
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
