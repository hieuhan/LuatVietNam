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

public partial class Admin_SignersEdit : System.Web.UI.Page
{
    private short SignerId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SignerId"] != null && Request.Params["SignerId"].ToString() != "")
        {
            SignerId = short.Parse(Request.Params["SignerId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLRegencies_BindByCulture(ddlRegencies, Regencies.Static_GetList(), "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlOrgans, ICSoft.LawDocsLib.Organs.Static_GetList(), "...");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (SignerId > 0)
            {
                Signers m_Signers = new Signers();
                m_Signers = m_Signers.Get(SignerId);
                if (m_Signers.SignerId > 0)
                {
                    txtSignerName.Text = m_Signers.SignerName;
                    txtMissionYearFrom.Text = m_Signers.MissionYearFrom.ToString();
                    txtMissionYearTo.Text = m_Signers.MissionYearTo.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Signers.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlOrgans, m_Signers.OrganId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlRegencies, m_Signers.RegencyId.ToString());
                }
                else
                {
                    txtSignerName.Text = "";
                    ddlReviewStatus.SelectedIndex = -1;
                    ddlOrgans.SelectedIndex = -1;
                    ddlOrgans.SelectedIndex = -1;
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
            byte SysMessageTypeId = 0;
            Signers m_Signers = new Signers();
            m_Signers.SignerName = txtSignerName.Text.Trim();
            m_Signers.SignerNameClear = StringUtil.RemoveSign4VietnameseString(txtSignerName.Text.ToString());
            m_Signers.SignerId = SignerId;            
            m_Signers.MissionYearFrom = (txtMissionYearFrom.Text.Trim() != "") ? short.Parse(txtMissionYearFrom.Text) : short.Parse("0");
            m_Signers.MissionYearTo = (txtMissionYearTo.Text.Trim() != "") ? short.Parse(txtMissionYearTo.Text) : short.Parse("0");
            m_Signers.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Signers.OrganId = short.Parse(ddlOrgans.SelectedValue);
            m_Signers.RegencyId = byte.Parse(ddlRegencies.SelectedValue);
            SysMessageTypeId = m_Signers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 2)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        SignerId > 0 ? "Cập nhật Người ký thành công." : "Thêm mới Người ký thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    SignerId > 0 ? "Cập nhật Người ký thành công." : "Thêm mới Người ký thành công.", this);
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
        txtSignerName.Text = "";
        txtMissionYearFrom.Text = "";
        txtMissionYearTo.Text = "";
        ddlOrgans.SelectedIndex = -1;
        ddlRegencies.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
    }
}