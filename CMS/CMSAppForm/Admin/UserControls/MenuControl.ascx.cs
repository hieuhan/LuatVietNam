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
using ICSoft.HelperLib;
using ICSoft.CMSLib;
using sms.admin.security;
namespace ICSoft.AppForm.Admin
{
    public partial class MenuControl : System.Web.UI.UserControl
    {
        private string userName;
        protected string strMenu;
        private int ActUserId;
        private string CON_PRIMARY_CONSTR = "";
        protected string LanguageName = ICSoft.HelperLib.LanguageHelpers.GetCurentName();
        protected List<Actions> l_ActionForUser = new List<Actions>();
        protected List<Actions> l_ActionLevel1 = new List<Actions>();
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
                    l_ActionLevel1 = (List<Actions>)m_Action.GetActionsByUser(ActUserId);
                    l_ActionForUser = (List<Actions>)m_Action.GetNotRootForUser(ActUserId);
                   
                    rptItemLevel1.DataSource = l_ActionLevel1;
                    rptItemLevel1.DataBind();
                    
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        private List<Actions> GetActionsListByParentId(short ParentActionId)
        {
            l_ActionChild = new List<Actions>();
            foreach (Actions m_Action in l_ActionForUser)
            {
                if (m_Action.ParentActionId == ParentActionId && m_Action.Display > 0)
                {
                    l_ActionChild.Add(m_Action);
                }
            }
            return l_ActionChild;
        }
        protected void rptItemLevel1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptItemLevel2 = (Repeater)e.Item.FindControl("rptItemLevel2");
                HiddenField hdfActionId = (HiddenField)e.Item.FindControl("hdfActionId");
                short ParentId = short.Parse(hdfActionId.Value);
                rptItemLevel2.DataSource = GetActionsListByParentId(ParentId);
                rptItemLevel2.DataBind();
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());

            }

        }
        protected void rptItemLevel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptItemLevel3 = (Repeater)e.Item.FindControl("rptItemLevel3");
                HiddenField hdfActionId = (HiddenField)e.Item.FindControl("hdfActionId");
                short ParentId = short.Parse(hdfActionId.Value);

                rptItemLevel3.DataSource = GetActionsListByParentId(ParentId); 
                rptItemLevel3.DataBind();
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
    }
}
