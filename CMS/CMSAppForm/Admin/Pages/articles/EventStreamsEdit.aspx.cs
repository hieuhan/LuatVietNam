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

public partial class Admin_EventStreamsEdit : System.Web.UI.Page
{
    String csName = "csbMessage";
    Type csType;
    ClientScriptManager cs;
    StringBuilder csText = new StringBuilder();
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int EventStreamId = 0;
    private short SiteId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["EventStreamId"] != null && Request.Params["EventStreamId"].ToString() != "")
        {
            EventStreamId = short.Parse(Request.Params["EventStreamId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
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
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            DropDownListHelpers.DDLDataTypes_BindByCulture(ddlDataType, DataTypes.Static_GetList(), "");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", ApplicationTypeId.ToString());
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (EventStreamId > 0)
            {
                EventStreams m_EventStreams = new EventStreams();
                m_EventStreams = m_EventStreams.Get(EventStreamId, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue));
                if (m_EventStreams.EventStreamId > 0)
                {
                    txtEventStreamName.Text = m_EventStreams.EventStreamName.ToString();
                    txtEventStreamDesc.Text = m_EventStreams.EventStreamDesc.ToString();
                    txtStartTime.Text = m_EventStreams.DisplayStartTime.ToString("dd/MM/yyyy");
                    txtEndTime.Text = m_EventStreams.DisplayEndTime.ToString("dd/MM/yyyy");
                    if (!string.IsNullOrEmpty(m_EventStreams.ImagePath))
                        txtIcon.Text = CmsConstants.ROOT_PATH+ m_EventStreams.ImagePath;
                    else
                        txtIcon.Text = "";
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_EventStreams.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_EventStreams.SiteId.ToString());
                    List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
                    DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...", m_EventStreams.CategoryId.ToString());
                    ckbShowApp.Checked = m_EventStreams.ShowApp == 1 ? true : false;
                    ckbShowBottom.Checked = m_EventStreams.ShowBottom == 1 ? true : false;
                    ckbShowTop.Checked = m_EventStreams.ShowTop == 1 ? true : false;
                    ckbShowWap.Checked = m_EventStreams.ShowWap == 1 ? true : false;
                    ckbShowWeb.Checked = m_EventStreams.ShowWeb == 1 ? true : false;

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
            if (txtEventStreamName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên dòng sự kiên không được để trống";
                return;
            }
            short SysMessageId = 0;            
            EventStreams m_EventStreams = new EventStreams();
            m_EventStreams.EventStreamName = txtEventStreamName.Text;            
            m_EventStreams.EventStreamDesc = txtEventStreamDesc.Text =="" ? "" : txtEventStreamDesc.Text;
            m_EventStreams.EventStreamId = EventStreamId;
            m_EventStreams.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_EventStreams.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_EventStreams.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_EventStreams.CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            m_EventStreams.SiteId = short.Parse(ddlSite.SelectedValue);
            m_EventStreams.DisplayStartTime = sms.utils.StringUtil.ConvertToDateTime(txtStartTime.Text.ToString());
            m_EventStreams.DisplayEndTime = sms.utils.StringUtil.ConvertToDateTime(txtEndTime.Text.ToString());
            if (txtIcon.Text.StartsWith(CmsConstants.ROOT_PATH))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.ROOT_PATH.Length);
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_EventStreams.ImagePath = txtIcon.Text;
            m_EventStreams.ShowApp = byte.Parse(ckbShowApp.Checked == true ? "1" : "0");
            m_EventStreams.ShowBottom = byte.Parse(ckbShowBottom.Checked == true ? "1" : "0");
            m_EventStreams.ShowTop = byte.Parse(ckbShowTop.Checked == true ? "1" : "0");
            m_EventStreams.ShowWap = byte.Parse(ckbShowWap.Checked == true ? "1" : "0");
            m_EventStreams.ShowWeb = byte.Parse(ckbShowWeb.Checked == true ? "1" : "0");

            m_EventStreams.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (cblAddOther.Checked)
            {
                if (EventStreamId > 0) {JSAlertHelpers.ShowNotify("10", "success", "Cập nhật thành công.", this);}
                else {JSAlertHelpers.ShowNotify("10", "success", "Thêm mới thành công.", this);}
                clearForm();
                return;
            }
            if (EventStreamId > 0) {JSAlertHelpers.ShowNotifyOtherPage("10", "success", "Cập nhật thành công.", this);}
            else {JSAlertHelpers.ShowNotifyOtherPage("10", "success", "Thêm mới thành công.", this); }


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
        txtEventStreamName.Text = "";
        txtEventStreamDesc.Text = "";
        txtEndTime.Text = "";
        txtIcon.Text = "";
        txtStartTime.Text = "";
        ddlAppType.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
        ddlDataType.SelectedIndex = -1;
        ddlLanguage.SelectedIndex = -1;
        ddlParentCategory.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
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
}