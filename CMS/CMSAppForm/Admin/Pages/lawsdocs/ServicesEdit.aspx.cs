using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.common;
using sms.utils;

public partial class Admin_ServicesEdit : System.Web.UI.Page
{
    private short ServiceId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ServiceId"] != null && Request.Params["ServiceId"].ToString() != "")
        {
            ServiceId = short.Parse(Request.Params["ServiceId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (ServiceId > 0)
            {
                Services m_Services = new Services();
                m_Services = m_Services.Get(ServiceId);
                if (m_Services.ServiceId > 0)
                {
                    txtServiceName.Text = m_Services.ServiceName;
                    txtServiceDesc.Text = m_Services.ServiceDesc.ToString();
                    txtServiceCode.Text = m_Services.ServiceCode.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Services.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_Services.SiteId.ToString());
                }
                else
                {
                    txtServiceName.Text = "";
                    txtServiceDesc.Text = "";
                    txtServiceCode.Text = "";
                    ddlReviewStatus.SelectedIndex = -1;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            int SysMessageTypeId = 0;
            Services m_Services = new Services();
            m_Services.ServiceId = ServiceId;
            m_Services.ServiceName = txtServiceName.Text.Trim();
            m_Services.ServiceDesc = (txtServiceDesc.Text.Trim() != "") ? txtServiceDesc.Text : "";
            m_Services.ServiceCode = (txtServiceCode.Text.Trim() != "") ? txtServiceCode.Text : "";
            m_Services.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Services.SiteId = short.Parse(ddlSite.SelectedValue);
            SysMessageTypeId = m_Services.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 0)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        ServiceId > 0 ? "Cập nhật dịch vụ thành công." : "Thêm mới dịch vụ thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    ServiceId > 0 ? "Cập nhật dịch vụ thành công." : "Thêm mới dịch vụ thành công.", this);
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
        txtServiceCode.Text = "";
        txtServiceDesc.Text = "";
        txtServiceName.Text = "";
        ddlReviewStatus.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
    }
}