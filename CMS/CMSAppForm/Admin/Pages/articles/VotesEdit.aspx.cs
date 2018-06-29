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

public partial class Admin_VotesEdit : System.Web.UI.Page
{
    String csName = "csbMessage";
    Type csType;
    ClientScriptManager cs;
    StringBuilder csText = new StringBuilder();
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int VoteId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["VoteId"] != null && Request.Params["VoteId"].ToString() != "")
        {
            VoteId = short.Parse(Request.Params["VoteId"].ToString());
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
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDLDataTypes_BindByCulture(ddlDataType, DataTypes.Static_GetList(), "");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", ApplicationTypeId.ToString());
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            DropDownListHelpers.DDLVoteTypes_BindByCulture(ddlVoteTypes, VoteTypes.Static_GetList(), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();

            if (Request.QueryString["VoteId"] == null)
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
            if (VoteId > 0)
            {
                Votes m_Votes = new Votes();
                m_Votes = m_Votes.Get(VoteId, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue));
                if (m_Votes.VoteId > 0)
                {
                    txtVoteName.Text = m_Votes.VoteName.ToString();
                    txtVoteDesc.Text = m_Votes.VoteDesc.ToString();
                    txtStartTime.Text = m_Votes.StartTime.ToString("dd/MM/yyyy");
                    txtEndTime.Text = m_Votes.EndTime.ToString("dd/MM/yyyy");
                    DropDownListHelpers.DDLVoteTypes_BindByCulture(ddlVoteTypes, VoteTypes.Static_GetList(), "", m_Votes.VoteTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Votes.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_Votes.SiteId.ToString());
                    List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
                    DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...", m_Votes.CategoryId.ToString());
                }
                else
                {
                    txtVoteName.Text = "";
                    txtVoteDesc.Text = "";
                    txtStartTime.Text = "";
                    txtEndTime.Text = "";
                    ddlParentCategory.SelectedIndex = -1;
                    ddlVoteTypes.SelectedIndex = -1;
                    ddlReviewStatus.SelectedIndex = -1;
                    ddlSite.SelectedIndex = -1;
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    //--------------------------------------------------------------------------------
    public void Alert(string Content)
    {
        csType = this.GetType();
        cs = Page.ClientScript;
        csText = new StringBuilder();
        Content = Content.Replace("'", "").Replace("\"", "").Replace("\n", "").Replace("\r", "");
        csText.Append("$.msgBox({title:'Thông báo', content: '" + Content + "' });");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), csText.ToString(), true);
    }
    //--------------------------------------------------------------------------------
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            byte SysMessageTypeId = 0;
            Votes m_Votes = new Votes();
            //if (txtVoteName.Text != "")
            //    m_Votes.VoteName = txtVoteName.Text;
            //else
            //{
            //    lblMsg.Text="Tên không thể để trống";
            //    return;
            //}
            m_Votes.VoteDesc = txtVoteDesc.Text;
            m_Votes.VoteName = txtVoteName.Text;
            m_Votes.VoteId = VoteId;
            m_Votes.StartTime =sms.utils.StringUtil.ConvertToDateTime(txtStartTime.Text.ToString());
            m_Votes.EndTime = sms.utils.StringUtil.ConvertToDateTime(txtEndTime.Text.ToString());
            m_Votes.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Votes.VoteTypeId = byte.Parse(ddlVoteTypes.SelectedValue);
            m_Votes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Votes.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_Votes.CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            m_Votes.SiteId = short.Parse(ddlSite.SelectedValue);
           SysMessageTypeId= m_Votes.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", sms.common.SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 2)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        VoteId > 0 ? "Cập nhật bình luận thành công." : "Thêm mới bình luận thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    VoteId > 0 ? "Cập nhật bình luận thành công." : "Thêm mới bình luận thành công.", this);
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


    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
    }
    private void ClearForm()
    {
        txtVoteDesc.Text = "";
        txtStartTime.Text = "";
        txtEndTime.Text = "";
        ddlLanguage.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        ddlVoteTypes.SelectedIndex = -1;
        ddlAppType.SelectedIndex = -1;
        ddlParentCategory.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
}