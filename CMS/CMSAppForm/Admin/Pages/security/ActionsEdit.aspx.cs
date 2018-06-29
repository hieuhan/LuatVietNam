using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_ActionsEdit : System.Web.UI.Page
{
    private short ActionId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ActionId"] != null && Request.Params["ActionId"].ToString() != "")
        {
            ActionId = short.Parse(Request.Params["ActionId"].ToString());
        }
        if (!IsPostBack)
        {
            Actions m_Actions = new Actions();

            List<Actions> l_Actions = m_Actions.GetAllHierachy2("--").Cast<Actions>().ToList<Actions>();
            DropDownListHelpers.DDL_Bind(ddlParentAction, l_Actions, "...");
            DropDownListHelpers.DDL_Bind(ddlActionStatus, ActionStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (ActionId > 0)
            {
                Actions m_Actions = new Actions();
                m_Actions = m_Actions.Get(ActionId);
                if (m_Actions.ActionId > 0)
                {
                    txtActionName.Text = m_Actions.ActionName;
                    txtActionDesc.Text = m_Actions.ActionDesc;
                    txtUrl.Text = m_Actions.Url;
                    txtDisplayOrder.Text = m_Actions.ActionOrder.ToString();
                    chkDisplay.Checked = (m_Actions.Display==1) ? true : false;
                    DropDownListHelpers.DDL_SetSelected(ddlActionStatus, m_Actions.ActionStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlParentAction, m_Actions.ParentActionId.ToString());
                }
                else
                {
                    txtActionName.Text = "";
                    txtActionDesc.Text = "";
                    txtUrl.Text = "";
                    txtDisplayOrder.Text = "";
                    chkDisplay.Checked = true;
                    ddlActionStatus.SelectedIndex = -1;
                    ddlParentAction.SelectedIndex = -1;
                    //ddlParentActionLevel2.SelectedIndex = -1;
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlParentAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        //short value = short.Parse(ddlParentAction.SelectedValue);
        //Actions m_Actions = new Actions();
        //List<Actions> l_Actions = m_Actions.GetChildActionByUser(ActUserId, value).Cast<Actions>().ToList<Actions>().OrderBy(i => i.ActionOrder).ToList();
        //DropDownListHelpers.DDL_Bind(ddlParentActionLevel2, l_Actions, "...");
        //BindData();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            Actions m_Actions = new Actions();
            m_Actions = m_Actions.Get(ActionId);
            m_Actions.ActionDesc = txtActionDesc.Text;
            m_Actions.ActionName = txtActionName.Text;
            m_Actions.Url = txtUrl.Text;
            m_Actions.ActionOrder = txtDisplayOrder.Text == "" ? (short)0 : short.Parse(txtDisplayOrder.Text);
            m_Actions.Display = chkDisplay.Checked ? (byte)1 : (byte)0;
            short ParentActionId = ddlParentAction.SelectedValue == "" ? (short)0 : short.Parse(ddlParentAction.SelectedValue);
            m_Actions.ParentActionId = ParentActionId;
            m_Actions.ActionStatusId = byte.Parse(ddlActionStatus.SelectedValue);
            short levelID = 1;
            string title = ddlParentAction.SelectedItem.Text;
            
            if (ParentActionId > 0)
            {
                Actions ParentAction = m_Actions.Get(ParentActionId);
                if (ParentAction.ActionId>0)
                {
                    levelID = (short)(ParentAction.LevelId + 1);
                }
            }
            m_Actions.LevelId = levelID;
            if (ActionId > 0)
            {
                m_Actions.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                m_Actions.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            //JSAlertHelpers.close(this);
            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}