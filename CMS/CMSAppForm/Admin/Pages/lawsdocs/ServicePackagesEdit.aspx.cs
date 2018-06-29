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

public partial class Admin_ServicePackagesEdit : System.Web.UI.Page
{
    private short SiteId = 0;
    private short ServiceId = 0;
    private short ServicePackageId = 0;
    private byte ViewOnly = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["ServiceId"] != null && Request.Params["ServiceId"].ToString() != "")
        {
            ServiceId = short.Parse(Request.Params["ServiceId"].ToString());
        }
        if (Request.Params["ServicePackageId"] != null && Request.Params["ServicePackageId"].ToString() != "")
        {
            ServicePackageId = short.Parse(Request.Params["ServicePackageId"].ToString());
        }
        if (Request.Params["ViewOnly"] != null && Request.Params["ViewOnly"].ToString() != "")
        {
            ViewOnly = byte.Parse(Request.Params["ViewOnly"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "", ServiceId.ToString());
            DropDownListHelpers.DDL_Bind(ddlParent, ServicePackages.Static_GetListByServiceIdAndParentId(short.Parse(string.IsNullOrEmpty(ddlServices.SelectedValue) ? "0" : ddlServices.SelectedValue), 0), "...");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (ServicePackageId > 0)
            {
                ServicePackages m_ServicePackages = new ServicePackages();
                m_ServicePackages = m_ServicePackages.Get(ServicePackageId);
                if (m_ServicePackages.ServicePackageId > 0)
                {
                    txtServicePackageName.Text = m_ServicePackages.ServicePackageName;
                    txtServicePackageDesc.Text = m_ServicePackages.ServicePackageDesc.ToString();
                    txtNumMonthUse.Text = m_ServicePackages.NumMonthUse.ToString();
                    txtNumDayUse.Text = m_ServicePackages.NumDayUse.ToString();
                    txtNumDownload.Text = m_ServicePackages.NumDownload.ToString();
                    txtConcurrentLogin.Text = m_ServicePackages.ConcurrentLogin.ToString();
                    txtPrice.Text = m_ServicePackages.Price.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_ServicePackages.SiteId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_ServicePackages.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "", m_ServicePackages.ServiceId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlParent, ServicePackages.Static_GetListByServiceIdAndParentId(short.Parse(ddlServices.SelectedValue), 0), "...", m_ServicePackages.ServicePackageParentId.ToString());
                }
            }
            else
            {
                txtServicePackageName.Text = "";
                txtServicePackageDesc.Text = "";
                txtNumMonthUse.Text = "";
                txtNumDayUse.Text = "";
                txtConcurrentLogin.Text = "";
                txtPrice.Text = "";
                ddlReviewStatus.SelectedIndex = -1;
            }
            if (ViewOnly == 1)
            {
                btnSave.Visible = false;
                cblAddOther.Visible = false;
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "");
        DropDownListHelpers.DDL_Bind(ddlParent, ServicePackages.Static_GetListByServiceIdAndParentId(short.Parse(ddlServices.SelectedValue), 0), "...");
    }

    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListHelpers.DDL_Bind(ddlParent, ServicePackages.Static_GetListByServiceIdAndParentId(short.Parse(ddlServices.SelectedValue), 0), "...");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            ServicePackages m_ServicePackages = new ServicePackages();
            m_ServicePackages.ServicePackageId = ServicePackageId;
            m_ServicePackages.ServicePackageName = txtServicePackageName.Text.Trim();
            m_ServicePackages.ServicePackageDesc = txtServicePackageDesc.Text.Trim();
            m_ServicePackages.NumMonthUse = (txtNumMonthUse.Text.Trim() != "") ? short.Parse(txtNumMonthUse.Text) : short.Parse("0");
            m_ServicePackages.NumDayUse = (txtNumDayUse.Text.Trim() != "") ? short.Parse(txtNumDayUse.Text) : short.Parse("0");
            m_ServicePackages.NumDownload = (txtNumDownload.Text.Trim() != "") ? short.Parse(txtNumDownload.Text) : short.Parse("0");
            m_ServicePackages.ConcurrentLogin = (txtConcurrentLogin.Text.Trim() != "") ? byte.Parse(txtConcurrentLogin.Text) : byte.Parse("0");
            m_ServicePackages.Price = (txtPrice.Text.Trim() != "") ? int.Parse(txtPrice.Text) : int.Parse("0");
            m_ServicePackages.ServiceId = short.Parse(ddlServices.SelectedValue);
            m_ServicePackages.ServicePackageParentId = short.Parse(ddlParent.SelectedValue);
            m_ServicePackages.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_ServicePackages.SiteId = short.Parse(ddlSite.SelectedValue);
            m_ServicePackages.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (cblAddOther.Checked)
            {
                if (ServicePackageId > 0) { JSAlertHelpers.ShowNotify("15", "success", "Cập nhật thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới thành công.", this); }
                //clearForm();
                return;
            }
            if (ServicePackageId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật thành công.", this); }
            else { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới thành công.", this); }

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
        txtConcurrentLogin.Text = "";
        txtNumDayUse.Text = "";
        txtNumDownload.Text = "";
        txtNumMonthUse.Text = "";
        txtPrice.Text = "";
        txtServicePackageDesc.Text = "";
        txtServicePackageName.Text = "";
        ddlReviewStatus.SelectedIndex = -1;
        ddlServices.SelectedIndex = -1;
        ddlParent.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
    }
}