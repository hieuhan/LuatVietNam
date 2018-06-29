using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_FeedBacksEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int FeedBackId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["FeedBackId"] != null && Request.Params["FeedBackId"].ToString() != "")
        {
            FeedBackId = short.Parse(Request.Params["FeedBackId"].ToString());
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
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", ApplicationTypeId.ToString());
            //DropDownListHelpers.DDLFeedBackGroups_BindByCulture(ddlFeedBackGroups, FeedBackGroups.Static_GetList(), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDL_Bind(ddlFeedBackGroups, FeedBackGroups.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (FeedBackId > 0)
            {
                FeedBacks m_FeedBacks = new FeedBacks();
                m_FeedBacks = m_FeedBacks.Get(FeedBackId);
                if (m_FeedBacks.FeedBackId > 0)
                {
                    txtTitle.Text = m_FeedBacks.Title.ToString();
                    txtComment.Text = m_FeedBacks.Comment.ToString();
                    txtUserId.Text = m_FeedBacks.UserId.ToString();
                    txtOrganName.Text = m_FeedBacks.OrganName.ToString();
                    txtFullName.Text = m_FeedBacks.FullName.ToString();
                    txtPhoneNumber.Text = m_FeedBacks.PhoneNumber.ToString();
                    txtEmail.Text = m_FeedBacks.Email.ToString();
                    txtAddress.Text = m_FeedBacks.Address.ToString();
                    txtRatingScore.Text = m_FeedBacks.RatingScore.ToString();
                    txtFromIP.Text = m_FeedBacks.FromIP.ToString();
                    //DropDownListHelpers.DDLFeedBackGroups_BindByCulture(ddlFeedBackGroups, FeedBackGroups.Static_GetList(), "", m_FeedBacks.FeedBackGroupId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlFeedBackGroups,
                        FeedBackGroups.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...",
                        m_FeedBacks.FeedBackGroupId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_FeedBacks.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlLanguage, m_FeedBacks.LanguageId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlAppType, m_FeedBacks.ApplicationTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_FeedBacks.SiteId.ToString());

                }
                else
                {
                    txtTitle.Text = "";
                    txtComment.Text = "";
                    txtUserId.Text = "";
                    txtOrganName.Text = "";
                    txtFullName.Text = "";
                    txtPhoneNumber.Text = "";
                    txtEmail.Text = "";
                    txtAddress.Text = "";
                    txtRatingScore.Text = "";
                    txtFromIP.Text = "";
                    ddlFeedBackGroups.SelectedIndex = -1;
                    ddlReviewStatus.SelectedIndex = -1;
                    ddlSite.SelectedIndex = -1;
                }
                cblAddOther.Visible = false;
            }
            else
                cblAddOther.Visible = true;

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
    //--------------------------------------------------------------------------------
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            byte SysMessageTypeId = 0;
            FeedBacks m_FeedBacks = new FeedBacks();
            m_FeedBacks.UserId =int.Parse(!string.IsNullOrEmpty(txtUserId.Text) ? txtUserId.Text :"0");
            m_FeedBacks.Title = txtTitle.Text;
            m_FeedBacks.Comment = txtComment.Text;
            m_FeedBacks.FullName = txtFullName.Text;
            m_FeedBacks.PhoneNumber = txtPhoneNumber.Text;
            m_FeedBacks.Email = txtEmail.Text;
            m_FeedBacks.OrganName = txtOrganName.Text;
            m_FeedBacks.RatingScore = txtRatingScore.Text.Trim()=="" ? byte.Parse("0") : byte.Parse(txtRatingScore.Text);
            m_FeedBacks.FromIP = txtFromIP.Text;
            m_FeedBacks.Address = txtAddress.Text;
            m_FeedBacks.FeedBackId = FeedBackId;
            m_FeedBacks.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_FeedBacks.FeedBackGroupId = short.Parse(ddlFeedBackGroups.SelectedValue);
            m_FeedBacks.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_FeedBacks.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_FeedBacks.SiteId = byte.Parse(ddlSite.SelectedValue);
            SysMessageTypeId = m_FeedBacks.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 2)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        FeedBackId > 0 ? "Cập nhật phản hồi thành công." : "Thêm mới phản hồi thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    FeedBackId > 0 ? "Cập nhật phản hồi thành công." : "Thêm mới phản hồi thành công.", this);
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
        txtAddress.Text = "";
        txtComment.Text = "";
        txtEmail.Text = "";
        txtFromIP.Text = "";
        txtFullName.Text = "";
        txtOrganName.Text = "";
        txtPhoneNumber.Text = "";
        txtRatingScore.Text = "";
        txtTitle.Text = "";
        txtUserId.Text = "";
        ddlAppType.SelectedIndex = -1;
        ddlFeedBackGroups.SelectedIndex = -1;
        ddlLanguage.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
    }
  
}