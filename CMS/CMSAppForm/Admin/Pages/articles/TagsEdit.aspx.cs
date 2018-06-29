using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_TagsEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int TagId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["TagId"] != null && Request.Params["TagId"].ToString() != "")
        {
            TagId = short.Parse(Request.Params["TagId"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
            {
                ApplicationTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
            }
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (TagId > 0)
            {
                Tags m_Tags = new Tags();
                m_Tags = m_Tags.Get(TagId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_Tags.TagId > 0)
                {
                    txtTagName.Text = m_Tags.TagName;
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Tags.ReviewStatusId.ToString());
                }
                else
                {
                    txtTagName.Text = "";                 
                    ddlReviewStatus.SelectedIndex = -1;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            Tags m_Tags = new Tags();
            m_Tags.TagId = TagId;
            m_Tags.TagName = txtTagName.Text;
            m_Tags.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Tags.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Tags.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (cblAddOther.Checked)
            {
                if (TagId > 0) { JSAlertHelpers.ShowNotify("15", "success", "Cập nhật Tags thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới Tags thành công.", this); }
                clearForm();
                return;
            }
            if (TagId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật Tags thành công.", this); }
            else { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới Tags thành công.", this); }

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
	        txtTagName.Text = "";
	        ddlLanguage.SelectedIndex = -1;
	        ddlReviewStatus.SelectedIndex = -1;
        }
}