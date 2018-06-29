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

public partial class Admin_Pages_articles_MediaGroupsEdit : System.Web.UI.Page
{
    private short MediaGroupId = 0;
    private int ActUserId = 0;
    protected short SiteId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["MediaGroupId"] != null && Request.Params["MediaGroupId"].ToString() != "")
        {
            MediaGroupId = short.Parse(Request.Params["MediaGroupId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            DropDownListHelpers.DDL_Bind(ddlParentGroup, MediaGroups.Static_GetList(), "...");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (MediaGroupId > 0)
            {
                MediaGroups m_MediaGroups = new MediaGroups();
                m_MediaGroups = m_MediaGroups.Get(MediaGroupId);
                if (m_MediaGroups.MediaGroupId > 0)
                {
                    txtMediaGroupName.Text = m_MediaGroups.MediaGroupName.ToString();
                    txtMediaGroupDesc.Text = m_MediaGroups.MediaGroupDesc.ToString();
                    ddlSite.SelectedValue = m_MediaGroups.SiteId.ToString();
                    ddlParentGroup.SelectedValue = m_MediaGroups.ParentGroupId.ToString();
                }
                else
                {
                    txtMediaGroupName.Text = "";
                    txtMediaGroupDesc.Text = "";
                }
            }

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
            if (txtMediaGroupName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên không được để trống";
                return;
            }
            if (txtMediaGroupDesc.Text.Trim() == "")
            {
                lblMsg.Text = "Mô tả không được để trống";
                return;
            }
            short SysMessageTypeId = 0;
            short SysMessageId = 0;
            MediaGroups m_MediaGroups = new MediaGroups();
            m_MediaGroups.MediaGroupId = MediaGroupId;
            m_MediaGroups.MediaGroupName = txtMediaGroupName.Text.ToString();
            m_MediaGroups.MediaGroupDesc = txtMediaGroupDesc.Text.ToString();
            m_MediaGroups.SiteId = short.Parse(ddlSite.SelectedValue);
            m_MediaGroups.ParentGroupId = short.Parse(ddlParentGroup.SelectedValue);
            if (MediaGroupId > 0)
            {
                m_MediaGroups.MediaGroupId = MediaGroupId;
                SysMessageTypeId = m_MediaGroups.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_MediaGroups.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId ==0 )
            {
                
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    MediaGroupId > 0 ? "Cập nhật nhóm media thành cồn." : "Thêm mới Nhóm media thành công.", this);
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