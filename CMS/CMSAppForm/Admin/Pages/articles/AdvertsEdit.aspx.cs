using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_AdvertsEdit : System.Web.UI.Page
{
    private int AdvertId = 0;
    private short SiteId = 0;
    private int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["AdvertId"] != null && Request.Params["AdvertId"].ToString() != "")
        {
            AdvertId = short.Parse(Request.Params["AdvertId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlPartner, Partners.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlType, AdvertContentTypes.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlAdvertStatus, AdvertStatus.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (AdvertId > 0)
            {
                Adverts m_Adverts = new Adverts();
                m_Adverts = m_Adverts.Get(AdvertId);
                if (m_Adverts.AdvertId > 0)
                {
                    txtDesc.Text = m_Adverts.AdvertDesc;
                    txtEndTime.Text = m_Adverts.EndTime==DateTime.MinValue ? "" : m_Adverts.EndTime.ToString("dd/MM/yyyy");
                    txtStartTime.Text = m_Adverts.StartTime == DateTime.MinValue ? "" : m_Adverts.StartTime.ToString("dd/MM/yyyy");
                    txtHeight.Text = m_Adverts.Height;
                    txtName.Text = m_Adverts.AdvertName;
                    txtScript.Text = m_Adverts.ScriptContent;
                    txtUrl.Text = m_Adverts.Url;
                    txtWidth.Text = m_Adverts.Width;
                    if (!string.IsNullOrEmpty(m_Adverts.ImagePath))
                        txtIcon.Text = m_Adverts.ImagePath.StartsWith("http://") ? m_Adverts.ImagePath : CmsConstants.ROOT_PATH + m_Adverts.ImagePath;
                    else
                        txtIcon.Text = "";

                    if (txtIcon.Text != "") imgDemo.Src = txtIcon.Text;

                    if (!string.IsNullOrEmpty(m_Adverts.ImageHoverPath))
                        txtIcon2.Text = m_Adverts.ImageHoverPath.StartsWith("http://") ? m_Adverts.ImageHoverPath : CmsConstants.ROOT_PATH + m_Adverts.ImageHoverPath;
                    else
                        txtIcon2.Text = "";

                    if (txtIcon2.Text != "") imgDemo2.Src = txtIcon2.Text;

                    DropDownListHelpers.DDL_SetSelected(ddlPartner, m_Adverts.PartnerId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlType, m_Adverts.AdvertContentTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlAdvertStatus, m_Adverts.AdvertStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_Adverts.SiteId.ToString());
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
            Adverts m_Adverts = new Adverts();
            m_Adverts = m_Adverts.Get(AdvertId);
            m_Adverts.AdvertContentTypeId = byte.Parse(ddlType.SelectedValue);
            m_Adverts.AdvertDesc = txtDesc.Text;
            m_Adverts.AdvertId = AdvertId;
            m_Adverts.AdvertName = txtName.Text;
            m_Adverts.AdvertStatusId = byte.Parse(ddlAdvertStatus.SelectedValue);
            m_Adverts.StartTime = txtStartTime.Text == "" ? DateTime.MinValue : sms.utils.StringUtil.ConvertToDateTime(txtStartTime.Text);
            m_Adverts.EndTime = txtEndTime.Text == "" ? DateTime.MinValue : sms.utils.StringUtil.ConvertToDateTime(txtEndTime.Text);
            m_Adverts.Height = txtHeight.Text;
            m_Adverts.PartnerId = short.Parse(ddlPartner.SelectedValue);
            m_Adverts.ScriptContent = txtScript.Text;
            m_Adverts.Url = txtUrl.Text;
            m_Adverts.SiteId = byte.Parse(ddlSite.SelectedValue);
            m_Adverts.Width = txtWidth.Text;
            if (txtIcon.Text.StartsWith(CmsConstants.ROOT_PATH))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.ROOT_PATH.Length);
            m_Adverts.ImagePath = txtIcon.Text;
            if (txtIcon2.Text.StartsWith(CmsConstants.ROOT_PATH))
                txtIcon2.Text = txtIcon2.Text.Substring(CmsConstants.ROOT_PATH.Length);
            m_Adverts.ImageHoverPath = txtIcon2.Text;

            if (AdvertId <= 0)
            {
                SysMessageTypeId = m_Adverts.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_Adverts.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }

            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 2)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        AdvertId > 0 ? "Cập nhật Quảng cáo thành công." : "Thêm mới Quảng cáo thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    AdvertId > 0 ? "Cập nhật Quảng cáo thành công." : "Thêm mới Quảng cáo thành công.", this);
            }

            //string ImagePath = FileUploadHelpers.SaveFile(FileUpload1, Request.PhysicalApplicationPath, "uploaded/Advert/");//SaveFile();
            //if (ImagePath.Length > 0)
            //{
            //    m_Adverts.ImagePath = ImagePath;
            //    m_Adverts.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            //}
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
        txtName.Text = "";
        txtDesc.Text = "";
        txtHeight.Text = "";
        txtWidth.Text = "";
        txtUrl.Text = "";
        txtIcon.Text = "";
        txtIcon2.Text = "";
        txtScript.Text = "";
        txtEndTime.Text = "";
        txtStartTime.Text = "";
        ddlAdvertStatus.SelectedIndex = -1;
        ddlPartner.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
        ddlType.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }

}