using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ICSoft.CMSLib;
using sms.admin.security;
using System.Collections;

namespace ICSoft.HelperLib
{
    public class UserHelpers
    {
        public static int GetCurentId()
        {
            int RetVal = 0;
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    RetVal = (HttpContext.Current.Session["userId"] == null) ? 0 : Int32.Parse(HttpContext.Current.Session["userId"].ToString());
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
            
        }
        public static int GetCurentId(ref bool IsInPageRole)
        {
            int RetVal = GetCurentId();
            IsInPageRole = false;
            try
            {
                if (RetVal > 0)
                {
                    Users m_Users = new Users(HostHelpers.Get_PRIMARY_CONSTR());
                    IsInPageRole = m_Users.HasPriv(RetVal, HttpContext.Current.Request.Url.AbsolutePath);
                }
                else
                    IsInPageRole = false;
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
            
        }
        public static bool isInRole(int UserId, byte RoleId)
        {
            bool RetVal = false;
            
            try
            {
                Users m_Users = new Users();
                UserRoles m_UserRoles = new UserRoles();
                IList l_UserRoles= m_UserRoles.GetListByUserId(UserId);
                foreach(UserRoles m_UserRolesTemp in l_UserRoles)
                {
                    if(m_UserRolesTemp.RoleId == RoleId || m_UserRolesTemp.RoleId == RoleHelpers.SystemAdmin)
                    {
                        RetVal = true;
                        break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;

        }
        public static string GetCurentFullName()
        {
            if (HttpContext.Current.Session == null)
                return "";
            return (HttpContext.Current.Session["UserFullName"] == null) ? "" : HttpContext.Current.Session["UserFullName"].ToString();
        }
        public static void SetCurentId(int UserId)
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["userId"] = UserId.ToString();
        }
        public static void SetCurentFullName(string UserFullName)
        {
            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["UserFullName"] = UserFullName;
        }
        public static Users Static_Get(int UserId, List<Users> l_Users, string Username = "")
        {
            Users RetVal;
            RetVal = l_Users.Find(x => x.UserId == UserId);
            if (RetVal == null)
                RetVal = new Users();
            return RetVal;
        }
        
    }
}
