using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;
using sms.common;

public partial class Admin_OrganTypesEdit : System.Web.UI.Page
{
    private byte OrganTypeId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["OrganTypeId"] != null && Request.Params["OrganTypeId"].ToString() != "")
        {
            OrganTypeId = byte.Parse(Request.Params["OrganTypeId"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["OrganTypeId"] == null)
            {
                cblAddOther.Visible = true;
            }
            else
            {
                cblAddOther.Visible = false;
                BindData();
            }
            
        }
    }
    private void BindData()
    {
        try
        {
            if (OrganTypeId > 0)
            {
                OrganTypes m_OrganTypes = new OrganTypes();
                m_OrganTypes = m_OrganTypes.Get(OrganTypeId);
                if (m_OrganTypes.OrganTypeId > 0)
                {
                    txtOrganTypeName.Text = m_OrganTypes.OrganTypeName;
                    txtOrganTypeDesc.Text = m_OrganTypes.OrganTypeDesc.ToString();
                    txtDisplayOrder.Text = m_OrganTypes.DisplayOrder.ToString();
                }
                else
                {
                    txtOrganTypeName.Text = "";
                    txtOrganTypeDesc.Text = "";
                    txtDisplayOrder.Text = "";
                }
            }
            
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
            OrganTypes m_OrganTypes = new OrganTypes();
            m_OrganTypes.OrganTypeName = txtOrganTypeName.Text.Trim();
            m_OrganTypes.OrganTypeId = OrganTypeId;
            m_OrganTypes.OrganTypeDesc = (txtOrganTypeDesc.Text.Trim() != "") ? txtOrganTypeDesc.Text: "";
            m_OrganTypes.DisplayOrder = (txtDisplayOrder.Text.Trim() != "") ? byte.Parse(txtDisplayOrder.Text) : byte.Parse("0");
            SysMessageTypeId = m_OrganTypes.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 0)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        OrganTypeId > 0 ? "Cập nhật loại cơ quan ban hành thành công." : "Thêm mới loại cơ quan ban hành thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    OrganTypeId > 0 ? "Cập nhật loại cơ quan ban hành thành công." : "Thêm mới loại cơ quan ban hành thành công.", this);
            }

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
        txtOrganTypeName.Text = "";
        txtOrganTypeDesc.Text = "";
        txtDisplayOrder.Text = "";
        cblAddOther.Checked = false;
    }
}