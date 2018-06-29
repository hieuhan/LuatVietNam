using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Reflection;
using System.IO;
using ICSoft.HelperLib;
using sms.common;
using sms.utils;
using ICSoft.LawDocsLib;
using ICSoft.CMSLib;

public partial class Admin_Pages_lawsdocs_LawersEdit : System.Web.UI.Page
{
    private int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null)
                {
                    cblAddOther.Visible = true;
                    short ProvinceId = 0;
                    short DistrictId = 0;
                    short WardId = 0;
                    DropDownListHelpers.DDL_Bind(ddlProvince, ICSoft.CMSLib.Provinces.Static_GetList(1), "...", ProvinceId.ToString());

                    if (ddlProvince.Items.Count > 0)
                    {
                        ProvinceId = short.Parse(ddlProvince.SelectedValue);
                    }
                    DropDownListHelpers.DDL_Bind(ddlDistrict, ICSoft.CMSLib.Districts.Static_GetList(ProvinceId), "...", DistrictId.ToString());
                    if (ddlDistrict.Items.Count > 0)
                    {
                        DistrictId = short.Parse(ddlDistrict.SelectedValue);
                    }
                    DropDownListHelpers.DDL_Bind(ddlWard, Wards.Static_GetList(DistrictId), "...", WardId.ToString());
                    DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "", "0");
                    DropDownListHelpers.DDLLawerGroups_BindByCulture(ddlLawerGroups, LawerGroups.Static_GetList(), "...", "0");
                    DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(), "...", "0");
                }
                else
                {
                    cblAddOther.Visible = false;
                    bindData();
                }
                    
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    //--------------------------------------------------------------------------------
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }
    private void bindData()
    {
        System.Int32 EditId;
        short ProvinceId = 0;
        short DistrictId = 0;
        int WardId = 0;
        if (Request.QueryString["id"] == null)
        {
            
            return;
        }
        else
        {
            
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            Lawers m_Lawers = new Lawers();
            m_Lawers.LawerID = EditId;
            m_Lawers = m_Lawers.Get();
            txFullName.Text = m_Lawers.FullName.ToString();
            if (!string.IsNullOrEmpty(m_Lawers.ImagePath.ToString()))
                txtIcon.Text = m_Lawers.ImagePath.ToString().StartsWith("http") ? m_Lawers.ImagePath.ToString() : CmsConstants.WEBSITE_MEDIADOMAIN + m_Lawers.ImagePath.ToString();
            else
                txtIcon.Text = "";

            if (txtIcon.Text != "") imgDemo.Src = txtIcon.Text;

            txAddress.Text = m_Lawers.Address.ToString();
            if (m_Lawers.LawerID == 0)
            {m_Lawers.ProviceId = 0;
                }
            DropDownListHelpers.DDL_Bind(ddlProvince, ICSoft.CMSLib.Provinces.Static_GetList(1), "...", m_Lawers.ProviceId.ToString());

            if (ddlProvince.Items.Count > 0)
            {
                ProvinceId = short.Parse(ddlProvince.SelectedValue);
            }
            DropDownListHelpers.DDL_Bind(ddlDistrict, ICSoft.CMSLib.Districts.Static_GetList(ProvinceId), "...", m_Lawers.DistricId.ToString());
            if (ddlDistrict.Items.Count > 0)
            {
                DistrictId = short.Parse(ddlDistrict.SelectedValue);
            }
            DropDownListHelpers.DDL_Bind(ddlWard, Wards.Static_GetList(DistrictId), "...", m_Lawers.WardId.ToString());
            txPhoneNumber.Text = m_Lawers.PhoneNumber.ToString();
            txMobile.Text = m_Lawers.Mobile.ToString();
            txEmail.Text = m_Lawers.Email.ToString();
            txWebsite.Text = m_Lawers.Website.ToString();
            txLawOfficeName.Text = m_Lawers.LawOfficeName.ToString();
            txExperience.Text = m_Lawers.Experience.ToString();
            txEducation.Text = m_Lawers.Education.ToString();
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "", m_Lawers.ReviewStatusId.ToString());
            DropDownListHelpers.DDLLawerGroups_BindByCulture(ddlLawerGroups, LawerGroups.Static_GetList(), "...", m_Lawers.LawerGroupId.ToString());
            DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(), "...", m_Lawers.FieldId.ToString());
            txContent.Text = m_Lawers.Content.ToString();
        }
    }
    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        int SysMessageTypeId = 0;
        short SysMessageId = 0;
        System.Int32 EditId;
        Lawers m_Lawers = new Lawers();
        LawerFields mLawerFields = new LawerFields();
        if (Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_Lawers.LawerID = EditId;
            m_Lawers = m_Lawers.Get();
        }
        try
        {


            m_Lawers.CrUserId = (byte)ActUserId;

            if (txFullName.Text != "")
                m_Lawers.FullName = txFullName.Text;
            if (txtIcon.Text.StartsWith(CmsConstants.WEBSITE_MEDIADOMAIN))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.WEBSITE_MEDIADOMAIN.Length);
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_Lawers.ImagePath = txtIcon.Text;

            if (txAddress.Text != "")
                m_Lawers.Address = txAddress.Text;
            m_Lawers.ProviceId = int.Parse(ddlProvince.SelectedValue);
            m_Lawers.DistricId = int.Parse(ddlDistrict.SelectedValue);
            m_Lawers.WardId = int.Parse(ddlWard.SelectedValue);
            
                m_Lawers.PhoneNumber = txPhoneNumber.Text;
            
                m_Lawers.Mobile = txMobile.Text;

                m_Lawers.Email = txEmail.Text;

                m_Lawers.Website = txWebsite.Text;
            
                m_Lawers.LawOfficeName = txLawOfficeName.Text;
            
                m_Lawers.Experience = txExperience.Text;
            
                m_Lawers.Education = txEducation.Text;

            m_Lawers.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
         
            m_Lawers.LawerGroupId = byte.Parse(ddlLawerGroups.SelectedValue);
            m_Lawers.Content = txContent.Text;

            m_Lawers.LawerID = EditId;
            SysMessageTypeId = m_Lawers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            m_Lawers.CrUserId =(short) ActUserId;
            
            if (cblAddOther.Checked)
            {
                if (EditId > 0)
                {
                    mLawerFields.LawerId = EditId;
                    mLawerFields.FieldId = short.Parse(ddlFields.SelectedValue);
                    mLawerFields.DeleteByLawerId(ConstantHelpers.Replicated, ActUserId);
                    mLawerFields.Insert(ConstantHelpers.Replicated, ActUserId);
                    JSAlertHelpers.ShowNotify("15", "success", "Cập nhật Luật sư thành công.", this);
                }
                else
                {
                    mLawerFields.LawerId = m_Lawers.LawerID;
                    mLawerFields.FieldId = short.Parse(ddlFields.SelectedValue);
                    mLawerFields.Insert(ConstantHelpers.Replicated, ActUserId);
                    JSAlertHelpers.ShowNotify("15", "success", "Thêm mới Luật sư thành công.", this);
                }
                clearForm();
                return;
            }
            if (EditId > 0)
            {
                mLawerFields.LawerId = EditId;
                mLawerFields.FieldId = short.Parse(ddlFields.SelectedValue);
                mLawerFields.DeleteByLawerId(ConstantHelpers.Replicated, ActUserId);
                mLawerFields.Insert(ConstantHelpers.Replicated, ActUserId);
                JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật Luật sư thành công.", this);
            }
            else
            {
                mLawerFields.LawerId = m_Lawers.LawerID;
                mLawerFields.FieldId = short.Parse(ddlFields.SelectedValue);
                mLawerFields.Insert(ConstantHelpers.Replicated, ActUserId);
                JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới Luật sư thành công.", this);
            }
            StringBuilder csText = new StringBuilder();
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            csText.Clear();
            csText.Append("<script type=\"text/javascript\">");
            csText.Append("window.parent.jQuery('#divEdit').dialog('close');");
            csText.Append("</script>");
            cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "system_message", csText.ToString());

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    private void clearForm()
    {
        txAddress.Text = "";
        txContent.Text = "";
        txEducation.Text = "";
        txEmail.Text = "";
        txExperience.Text = "";
        txFullName.Text = "";
        txLawOfficeName.Text = "";
        txMobile.Text = "";
        txPhoneNumber.Text = "";
        txtIcon.Text = "";
        txWebsite.Text = "";
        ddlWard.SelectedIndex = -1;
        ddlDistrict.SelectedIndex = -1;
        ddlProvince.SelectedIndex = -1;
        ddlLawerGroups.SelectedIndex = -1;
    }
    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Districts> l_district = Districts.Static_GetList(short.Parse(ddlProvince.SelectedValue));
        DropDownListHelpers.DDL_Bind(ddlDistrict, l_district, "...", "0");
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Wards> l_Wards = Wards.Static_GetList(short.Parse(ddlDistrict.SelectedValue));
        DropDownListHelpers.DDL_Bind(ddlWard, l_Wards, "...", "0");
    }



    protected void lbtSaveAndNew_Click(object sender, EventArgs e)
    {
        int SysMessageTypeId = 0;
        short SysMessageId = 0;
        System.Int32 EditId;
        Lawers m_Lawers = new Lawers();
        if (Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_Lawers.LawerID = EditId;
            m_Lawers = m_Lawers.Get();
        }
        try
        {


            m_Lawers.CrUserId = (byte)ActUserId;

            if (txFullName.Text != "")
                m_Lawers.FullName = txFullName.Text;
            if (txtIcon.Text.StartsWith(CmsConstants.WEBSITE_MEDIADOMAIN))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.WEBSITE_MEDIADOMAIN.Length);
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_Lawers.ImagePath = txtIcon.Text;

            if (txAddress.Text != "")
                m_Lawers.Address = txAddress.Text;
            m_Lawers.ProviceId = int.Parse(ddlProvince.SelectedValue);
            m_Lawers.DistricId = int.Parse(ddlDistrict.SelectedValue);
            m_Lawers.WardId = int.Parse(ddlWard.SelectedValue);

            m_Lawers.PhoneNumber = txPhoneNumber.Text;

            m_Lawers.Mobile = txMobile.Text;

            m_Lawers.Email = txEmail.Text;

            m_Lawers.Website = txWebsite.Text;

            m_Lawers.LawOfficeName = txLawOfficeName.Text;

            m_Lawers.Experience = txExperience.Text;

            m_Lawers.Education = txEducation.Text;

            m_Lawers.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);

            m_Lawers.Content = txContent.Text.ToString();

            m_Lawers.LawerID = EditId;
            SysMessageTypeId = m_Lawers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            m_Lawers.CrUserId = (short)ActUserId;
           
            if (EditId == 0)
            {
                lblMessage.Text = "Thêm mới luật sư thành công";
                clearForm();
            }
            else
            {
                Response.Redirect("LawersEdit.aspx");
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    
}
