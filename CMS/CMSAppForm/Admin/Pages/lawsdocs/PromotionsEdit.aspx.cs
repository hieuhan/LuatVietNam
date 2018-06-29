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

public partial class Admin_PromotionsEdit : System.Web.UI.Page
{
    private int PromotionId = 0;
    private int ActUserId = 0;
    private string DOC_CONSTR = System.Configuration.ConfigurationManager.AppSettings["DOC_CONSTR"] == null ? "" : System.Configuration.ConfigurationManager.AppSettings["DOC_CONSTR"];

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["PromotionId"] != null && Request.Params["PromotionId"].ToString() != "")
        {
            PromotionId = int.Parse(Request.Params["PromotionId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatusId, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (PromotionId > 0)
            {
                Promotions m_Promotions = new Promotions(DOC_CONSTR);
                m_Promotions = m_Promotions.Get(PromotionId);
                if (m_Promotions.PromotionId > 0)
                {
                    txtPromotionName.Text = m_Promotions.PromotionName;
                    txtPromotionDesc.Text = m_Promotions.PromotionDesc.ToString();
                    txtBeginTime.Text = m_Promotions.BeginTime.ToString("dd/MM/yyyy HH:mm:ss");
                    txtEndTime.Text = m_Promotions.EndTime.ToString("dd/MM/yyyy HH:mm:ss");
                    txtNumMonthFree.Text = m_Promotions.NumMonthFree.ToString();
                    txtNumDayFree.Text = m_Promotions.NumDayFree.ToString();
                    txtNumberOfCode.Text = m_Promotions.NumOfCode.ToString();
                    txtAmount.Text = m_Promotions.Amount.ToString();
                    txtPercentDecrease.Text = m_Promotions.PercentDecrease.ToString();
                    txtUsingCount.Text = m_Promotions.UsingCount.ToString();
                    txtLenOfCode.Text = m_Promotions.LenOfCode.ToString();

                    DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatusId, ReviewStatus.Static_GetList(), "", m_Promotions.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlUsingTypeId, m_Promotions.UsingTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_Promotions.SiteId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, m_Promotions.SiteId, 0), "", m_Promotions.ServicePackageId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlCodeGenType, m_Promotions.CodeGenType);
                }
            }
            else
            {
                txtPromotionName.Text = "";
                txtPromotionDesc.Text = "";
                txtBeginTime.Text = "";
                txtEndTime.Text = "";
                txtNumMonthFree.Text = "";
                txtNumDayFree.Text = "";
                txtNumberOfCode.Text = "100";
                txtLenOfCode.Text = "6";
                DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, 0, 0), "...", "");
                //DropDownListHelpers.DDL_Bind(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), 0), "");
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
        DropDownListHelpers.DDLServicePackages_BindByCulture(ddlServicePackages, ServicePackages.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue), 0), "");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            Promotions m_Promotions = new Promotions(DOC_CONSTR);
            m_Promotions.PromotionId = PromotionId;
            m_Promotions.PromotionName = txtPromotionName.Text;
            m_Promotions.PromotionDesc = txtPromotionDesc.Text;
            m_Promotions.BeginTime = (txtBeginTime.Text.Trim() != "") ? DateTime.Parse(txtBeginTime.Text) : DateTime.MinValue;
            m_Promotions.EndTime = (txtEndTime.Text.Trim() != "") ? DateTime.Parse(txtEndTime.Text) : DateTime.MinValue;
            m_Promotions.NumMonthFree = (txtNumMonthFree.Text.Trim() != "") ? short.Parse(txtNumMonthFree.Text) : short.Parse("0");
            m_Promotions.NumDayFree = (txtNumDayFree.Text.Trim() != "") ? short.Parse(txtNumDayFree.Text) : short.Parse("0");
            m_Promotions.Amount = (txtAmount.Text.Trim() != "") ? float.Parse(txtAmount.Text) : float.Parse("0");
            m_Promotions.PercentDecrease = (txtPercentDecrease.Text.Trim() != "") ? float.Parse(txtPercentDecrease.Text) : float.Parse("0");
            m_Promotions.UsingTypeId = byte.Parse(ddlUsingTypeId.SelectedValue);
            m_Promotions.ReviewStatusId = byte.Parse(ddlReviewStatusId.SelectedValue);
            m_Promotions.UsingCount = (txtUsingCount.Text.Trim() != "") ? int.Parse(txtUsingCount.Text) : int.Parse("0");
            m_Promotions.NumOfCode = (txtNumberOfCode.Text.Trim() != "") ? int.Parse(txtNumberOfCode.Text) : 0;
            m_Promotions.LenOfCode = (txtLenOfCode.Text.Trim() != "") ? byte.Parse(txtLenOfCode.Text) : (byte)0;
            m_Promotions.CodeGenType = ddlCodeGenType.SelectedValue;
            m_Promotions.ServicePackageId = short.Parse(ddlServicePackages.SelectedValue);
            m_Promotions.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Promotions.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (cblAddOther.Checked)
            {
                if (PromotionId > 0) { JSAlertHelpers.ShowNotify("15", "success", "Cập nhật thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới thành công.", this); }
                clearForm();
                return;
            }
            if (PromotionId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật thành công.", this); }
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
        txtAmount.Text = "";
        txtBeginTime.Text = "";
        txtEndTime.Text = "";
        txtNumberOfCode.Text = "";
        txtNumDayFree.Text = "";
        txtNumMonthFree.Text = "";
        txtPercentDecrease.Text = "";
        txtPromotionDesc.Text = "";
        txtPromotionName.Text = "";
        txtUsingCount.Text = "";
        ddlReviewStatusId.SelectedIndex = -1;
        ddlServicePackages.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
        ddlUsingTypeId.SelectedIndex = -1;
    }
}