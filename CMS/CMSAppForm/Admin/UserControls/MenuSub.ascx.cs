using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
namespace ICSoft.AppForm.Admin
{
    public partial class MenuSub : System.Web.UI.UserControl
    {
        private string userName;
        protected string strMenu;
        private int ActUserId;
        private string CON_PRIMARY_CONSTR = "";
        protected string LanguageName = ICSoft.HelperLib.LanguageHelpers.GetCurentName();
        protected List<Actions> l_ActionForUser = new List<Actions>();
        protected List<Actions> l_ActionChild = new List<Actions>();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                ActUserId = ICSoft.HelperLib.UserHelpers.GetCurentId();
                if (ActUserId > 0)
                {
                    CON_PRIMARY_CONSTR = ICSoft.HelperLib.HostHelpers.Get_PRIMARY_CONSTR();
                    Actions m_Action = new Actions(CON_PRIMARY_CONSTR);
                    l_ActionForUser = (List<Actions>) m_Action.GetNotRootForUser(ActUserId);
                    foreach (Actions m_ActionTemp in l_ActionForUser)
                    {
                        if(Request.RawUrl.Contains(m_ActionTemp.Url)){
                            m_Action = m_ActionTemp;
                            break;
                        }
                    }
                    while(m_Action.ActionId >0)
                    {
                        l_ActionChild.Insert(0, m_Action);
                        m_Action = m_Action.Get(m_Action.ParentActionId);
                    }
                    rptMenu.DataSource = l_ActionChild;
                    rptMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        
        protected void lbtDeleteCache_Click(object sender, EventArgs e)
        {
            //ICSoft.CMSLib.utils.CacheUtil.removeAllCache();
        }
        protected string GetActionUrl(string ActionUrl)
        {
            string RetVal = ActionUrl;
            if (string.IsNullOrEmpty(ActionUrl.Trim()) || ActionUrl.Trim() == "#")
                RetVal = "#";
            else
                RetVal = CmsConstants.PRJ_ROOT + ActionUrl;
            return RetVal;
        }
        protected string GetClass(string Url, string cssclass, string cssclassdefault)
        {
            if (Request.RawUrl.Contains(Url))
                return cssclass;
            else
                return cssclassdefault;
        }
    }
}
