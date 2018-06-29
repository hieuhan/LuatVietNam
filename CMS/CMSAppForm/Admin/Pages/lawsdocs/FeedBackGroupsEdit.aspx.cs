using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_Pages_lawsdocs_FeedBackGroupsEdit : System.Web.UI.Page
{
    private short FeedBackGroupId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["FeedBackGroupId"] != null && Request.Params["FeedBackGroupId"].ToString() != "")
        {
            FeedBackGroupId = short.Parse(Request.Params["FeedBackGroupId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (FeedBackGroupId > 0)
            {
                FeedBackGroups m_FeedBackGroups = new FeedBackGroups();
                m_FeedBackGroups = m_FeedBackGroups.Get(FeedBackGroupId);
                if (m_FeedBackGroups.FeedBackGroupId > 0)
                {
                    txtFeedBackGroupName.Text = m_FeedBackGroups.FeedBackGroupName.ToString();
                    txtFeedBackGroupDesc.Text = m_FeedBackGroups.FeedBackGroupDesc.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_FeedBackGroups.SiteId.ToString());
                }
                else
                {
                    txtFeedBackGroupName.Text = "";
                    txtFeedBackGroupDesc.Text = "";
                }
                cblAddOther.Visible = false;
            }
            else cblAddOther.Visible = true;

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (txtFeedBackGroupName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên không được để trống";
                return;
            }
            if (txtFeedBackGroupDesc.Text.Trim() == "")
            {
                lblMsg.Text = "Mô tả  không được để trống";
                return;
            }
            short SysMessageTypeId = 0;
            short SysMessageId = 0;
            FeedBackGroups m_FeedBackGroups = new FeedBackGroups();
            m_FeedBackGroups.FeedBackGroupId = FeedBackGroupId;
            m_FeedBackGroups.FeedBackGroupName = txtFeedBackGroupName.Text.ToString();
            m_FeedBackGroups.FeedBackGroupDesc = txtFeedBackGroupDesc.Text.ToString();
            m_FeedBackGroups.SiteId = short.Parse(ddlSite.SelectedValue);
            if (FeedBackGroupId > 0)
            {
                m_FeedBackGroups.FeedBackGroupId = FeedBackGroupId;
                SysMessageTypeId = m_FeedBackGroups.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_FeedBackGroups.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 2)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        FeedBackGroupId > 0 ? "Cập nhật nhóm phản hồi thành công." : "Thêm mới nhóm phản hồi thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    FeedBackGroupId > 0 ? "Cập nhật nhóm phản hồi thành công." : "Thêm mới nhóm phản hồi thành công.", this);
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

    private void ClearForm()
    {
        txtFeedBackGroupDesc.Text = "";
        txtFeedBackGroupName.Text = "";
        ddlSite.SelectedIndex = -1;
    }

}