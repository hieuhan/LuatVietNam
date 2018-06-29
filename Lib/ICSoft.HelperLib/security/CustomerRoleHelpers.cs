using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;

namespace ICSoft.HelperLib
{
    public class CustomerRoleHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte VB
        {
            get { return 9; }
            set { }
        }
        public static byte VBE
        {
            get { return 2; }
            set { }
        }
        public static byte HL
        {
            get { return 3; }
            set { }
        }
        public static byte FREE
        {
            get { return 4; }
            set { }
        }
        public static string GetCurentList()
        {
            string RetVal = "";
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    RetVal = (HttpContext.Current.Session["CustomerRoleList"] == null) ? "" : HttpContext.Current.Session["CustomerRoleList"].ToString();
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;

        }
        public static string GetCurentList(HttpContext context)
        {
            string RetVal = "";
            try
            {
                if (context.Session != null)
                {
                    RetVal = (context.Session["CustomerRoleList"] == null) ? "" : context.Session["CustomerRoleList"].ToString();
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;

        }
        public static void SetCurentList(string RoleList)
        {
            
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session["CustomerRoleList"] = RoleList;
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        public static void ClearCurentList()
        {

            try
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session["CustomerRoleList"] = null;
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        public static string GetCurentDataAccess()
        {
            string RetVal = "";
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    RetVal = (HttpContext.Current.Session["CurentDataAccess"] == null) ? "" : HttpContext.Current.Session["CurentDataAccess"].ToString();
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;

        }
        public static void SetCurentDataAccess(byte DataTypeId, int DataId)
        {
            
            try
            {
                string DataAccess = GetCurentDataAccess();
                if (DataAccess == "")
                    DataAccess = DataTypeId.ToString() + "-" + DataId.ToString();
                else if (("," + DataAccess + ",").Contains("," + DataTypeId.ToString() + "-" + DataId.ToString() + ",") == false)
                    DataAccess += "," + DataTypeId.ToString() + "-" + DataId.ToString();
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session["CurentDataAccess"] = DataAccess;
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        public static void ClearCurentDataAccess()
        {

            try
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session["CurentDataAccess"] = null;
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        public static bool IsInRole(byte RoleId, byte DataTypeId, int DataId, DateTime DataTime)
        {
            bool RetVal = false;
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    string CurentRoleList = GetCurentList();
                    CurentRoleList = "," + CurentRoleList + ",";
                    if (CurentRoleList.Contains(RoleId.ToString()) && CurentRoleList != ",,")
                        RetVal = true;
                    if (!RetVal)
                    {
                        string DataAccess = GetCurentDataAccess();
                        DataAccess = "," + DataAccess + ",";
                        if (DataAccess.Contains("," + DataTypeId.ToString() + "-" + DataId.ToString() + ","))
                            RetVal = true;
                    }
                    if (DataTime < DateTime.Now.AddYears(ConstantHelpers.YEARFREEVIEWDOC * -1))
                        RetVal = true;
                }
                else
                {
                    HttpContext context = HttpContext.Current;
                    if (context.Session == null && context.Request.Cookies != null && context.Request.Cookies["AuthCode"] != null && context.Request.Cookies["ASP.NET_SessionId"] != null)// get second in session
                    {
                        string SessionId = context.Request.Cookies["ASP.NET_SessionId"].Value;
                        string AuthCode = context.Request.Cookies["AuthCode"].Value;
                        string CustomerIdString = AuthCode.Substring(32);
                        //sms.utils.LogFiles.WriteLog(CustomerIdString);
                        if (sms.utils.md5.MD5Hash(ConstantHelpers.MEMBERDEFAULTPASS + CustomerIdString + SessionId) == AuthCode.Substring(0, 32))
                        {
                            Customers m_Customers = new Customers();
                            m_Customers.CustomerId = int.Parse(CustomerIdString);
                            m_Customers = m_Customers.Get();
                            //role
                            int ActUserId = 0;
                            string OrderBy = "[EndTime] DESC";
                            short ServiceId = 0;
                            string ExpriedTime = "";
                            CustomerServices m_CustomerServices = new CustomerServices();
                            List<CustomerServices> l_CustomerServices = m_CustomerServices.CustomerServices_GetList(ActUserId, OrderBy, m_Customers.CustomerId, ServiceId);
                            if (l_CustomerServices.Count > 0)
                            {
                                m_CustomerServices = l_CustomerServices[0];
                            }
                            if (m_CustomerServices.EndTime > DateTime.MinValue)
                            {
                                ExpriedTime = Themes.GetItem("ENDTIME") + DateTimeHelpers.GetDateAndTimeOnly(m_CustomerServices.EndTime);
                            }
                            else
                            {
                                ExpriedTime = Themes.GetItem("FREEACCOUNT");
                            }
                            //CustomerHelpers.SetCurentExpiredTime(ExpriedTime);
                            //Set Role List
                            foreach (CustomerServices m_CustomerServicesTemp in l_CustomerServices)
                            {
                                if (RetVal)
                                    break;
                                if (m_CustomerServicesTemp.EndTime > DateTime.Now)
                                {                                    
                                    OrderBy = "";
                                    ServiceRoles m_ServiceRoles = new ServiceRoles();
                                    List<ServiceRoles> l_ServiceRoles = m_ServiceRoles.ServiceRoles_GetList(ActUserId, OrderBy, m_CustomerServicesTemp.ServiceId, RoleId);

                                    foreach (ServiceRoles m_ServiceRolesTemp in l_ServiceRoles)
                                    {
                                        if (RoleId == m_ServiceRolesTemp.RoleId)
                                        {
                                            RetVal = true;
                                            break;
                                        }
                                        
                                    }
                                }
                            }
                            // end role
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;

        }
        public static bool IsInRoleVB(byte RoleId, HttpContext context)
        {
            bool RetVal = false;
            try
            {
                if (context.Session != null)
                {
                    string CurentRoleList = GetCurentList(context);
                    CurentRoleList = "," + CurentRoleList + ",";
                    if (CurentRoleList.Contains(RoleId.ToString()) && CurentRoleList != ",,")
                        RetVal = true;                    
                }
                else
                {                   
                    if (context.Session == null && context.Request.Cookies != null && context.Request.Cookies["AuthCode"] != null && context.Request.Cookies["ASP.NET_SessionId"] != null)// get second in session
                    {
                        string SessionId = context.Request.Cookies["ASP.NET_SessionId"].Value;
                        string AuthCode = context.Request.Cookies["AuthCode"].Value;
                        string CustomerIdString = AuthCode.Substring(32);
                        //sms.utils.LogFiles.WriteLog(CustomerIdString);
                        if (sms.utils.md5.MD5Hash(ConstantHelpers.MEMBERDEFAULTPASS + CustomerIdString + SessionId) == AuthCode.Substring(0, 32))
                        {
                            Customers m_Customers = new Customers();
                            m_Customers.CustomerId = int.Parse(CustomerIdString);
                            m_Customers = m_Customers.Get();
                            //role
                            int ActUserId = 0;
                            string OrderBy = "[EndTime] DESC";
                            short ServiceId = 0;
                            string ExpriedTime = "";
                            CustomerServices m_CustomerServices = new CustomerServices();
                            List<CustomerServices> l_CustomerServices = m_CustomerServices.CustomerServices_GetList(ActUserId, OrderBy, m_Customers.CustomerId, ServiceId);
                            if (l_CustomerServices.Count > 0)
                            {
                                m_CustomerServices = l_CustomerServices[0];
                            }
                            if (m_CustomerServices.EndTime > DateTime.MinValue)
                            {
                                ExpriedTime = Themes.GetItem("ENDTIME") + DateTimeHelpers.GetDateAndTimeOnly(m_CustomerServices.EndTime);
                            }
                            else
                            {
                                ExpriedTime = Themes.GetItem("FREEACCOUNT");
                            }
                            //CustomerHelpers.SetCurentExpiredTime(ExpriedTime);
                            //Set Role List
                            foreach (CustomerServices m_CustomerServicesTemp in l_CustomerServices)
                            {
                                if (RetVal)
                                    break;
                                if (m_CustomerServicesTemp.StatusId == StatusHelpers.Active)
                                {
                                    OrderBy = "";
                                    ServiceRoles m_ServiceRoles = new ServiceRoles();
                                    List<ServiceRoles> l_ServiceRoles = m_ServiceRoles.ServiceRoles_GetList(ActUserId, OrderBy, m_CustomerServicesTemp.ServiceId, RoleId);

                                    foreach (ServiceRoles m_ServiceRolesTemp in l_ServiceRoles)
                                    {
                                        if (RoleId == m_ServiceRolesTemp.RoleId)
                                        {
                                            RetVal = true;
                                            break;
                                        }

                                    }
                                }
                            }
                            // end role
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;

        }
	}//end ApplicationType
    
}//end namespace service
