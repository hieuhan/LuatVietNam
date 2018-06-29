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

public partial class Admin_Pages_articles_ArticleCommentsEdit : System.Web.UI.Page
{
    String csName = "csbMessage";
    Type csType;
    ClientScriptManager cs;
    StringBuilder csText = new StringBuilder();
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int ArticleCommentId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ArticleCommentId"] != null && Request.Params["ArticleCommentId"].ToString() != "")
        {
            ArticleCommentId = short.Parse(Request.Params["ArticleCommentId"].ToString());
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
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (ArticleCommentId > 0)
            {
                ArticleComments m_ArticleComments = new ArticleComments();
                //not have Get Function

                //
                if (m_ArticleComments.ArticleCommentId > 0)
                {
                    txtFullName.Text = m_ArticleComments.FullName.ToString();
                    txtEmail.Text = m_ArticleComments.Email.ToString();
                    txtPhoneNumber.Text = m_ArticleComments.PhoneNumber.ToString();
                    txtComment.Text = m_ArticleComments.Comment.ToString();
                    txtFromIP.Text = m_ArticleComments.FromIP.ToString();
                    txtUserAgent.Text = m_ArticleComments.UserAgent.ToString();

                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_ArticleComments.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_ArticleComments.SiteId.ToString());
                    

                }
                else
                {
                    txtFullName.Text = "";
                    txtEmail.Text = "";
                    txtPhoneNumber.Text = "";
                    txtComment.Text = "";
                    txtFromIP.Text = "";
                    txtUserAgent.Text = "";

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFullName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên không được để trống";
                return;
            }
            short SysMessageId = 0;
            ArticleComments m_ArticleComments = new ArticleComments();
            //if (txtFullName.Text != "")
            m_ArticleComments.FullName = txtFullName.Text;
            m_ArticleComments.Email = txtEmail.Text;
            m_ArticleComments.PhoneNumber = txtPhoneNumber.Text;
            m_ArticleComments.Comment = txtComment.Text;
            m_ArticleComments.FromIP = txtFromIP.Text;
            m_ArticleComments.UserAgent = txtUserAgent.Text;
            m_ArticleComments.ArticleCommentId = ArticleCommentId;
            m_ArticleComments.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_ArticleComments.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_ArticleComments.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_ArticleComments.SiteId = short.Parse(ddlSite.SelectedValue);


            m_ArticleComments.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
    }
}