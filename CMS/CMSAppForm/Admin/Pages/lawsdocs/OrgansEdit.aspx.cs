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

public partial class Admin_OrgansEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private short OrganId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["OrganId"] != null && Request.Params["OrganId"].ToString() != "")
        {
            OrganId = short.Parse(Request.Params["OrganId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLOrganTypes_BindByCulture(ddlOrganTypes, OrganTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (OrganId > 0)
            {
                Organs m_Organs = new Organs();
                m_Organs = m_Organs.Get(OrganId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_Organs.OrganId > 0)
                {
                    txtOrganName.Text = m_Organs.OrganName;
                    txtOrganDesc.Text = m_Organs.OrganDesc.ToString();
                    txtDisplayOrder.Text = m_Organs.DisplayOrder.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Organs.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlOrganTypes, m_Organs.OrganTypeId.ToString());
                }
                else
                {
                    txtOrganName.Text = "";
                    txtOrganDesc.Text = "";
                    txtDisplayOrder.Text = "";
                    ddlReviewStatus.SelectedIndex = -1;
                    ddlOrganTypes.SelectedIndex = -1;
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
            Organs m_Organs = new Organs();
            m_Organs.OrganName = txtOrganName.Text.Trim();  
            m_Organs.OrganId = OrganId;            
            m_Organs.OrganDesc = (txtOrganDesc.Text.Trim() != "") ? txtOrganDesc.Text : "";
            m_Organs.DisplayOrder = (txtDisplayOrder.Text.Trim() != "") ? short.Parse(txtDisplayOrder.Text) : short.Parse("0");
            m_Organs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Organs.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Organs.OrganTypeId = byte.Parse(ddlOrganTypes.SelectedValue);
            m_Organs.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (cblAddOther.Checked)
            {
                if (OrganId > 0) { JSAlertHelpers.ShowNotify("15", "success", "Cập nhật CQBH thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới CQBH thành công.", this); }
                clearForm();
                return;
            }
            if (OrganId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật CQBH thành công.", this); }
            else { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới CQBH thành công.", this); }
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
    private void clearForm()
    {
        txtDisplayOrder.Text = "";
        txtOrganDesc.Text = "";
        txtOrganName.Text = "";
        ddlLanguage.SelectedIndex = -1;
        ddlOrganTypes.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
    }
}