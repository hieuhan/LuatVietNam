using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_CategoriesEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private short CategoryId = 0;
    private short SiteId = 0;
    private int ActUserId = 0;
    private byte PositionDataTypeId = 0;
    protected short DataTypeId = 0;
    protected byte EnableDataType = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["CategoryId"] != null && Request.Params["CategoryId"].ToString() != "")
        {
            CategoryId = short.Parse(Request.Params["CategoryId"].ToString());
        }
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = short.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (Request.Params["EnableDataType"] != null && Request.Params["EnableDataType"].ToString() != "")
        {
            EnableDataType = byte.Parse(Request.Params["EnableDataType"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.Params["PositionDataTypeId"] != null && Request.Params["PositionDataTypeId"].ToString() != "")
            {
                PositionDataTypeId = byte.Parse(Request.Params["PositionDataTypeId"].ToString());
            }
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
            {
                ApplicationTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
            }
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            DropDownListHelpers.DDLDataTypes_BindByCulture(ddlDataType, DataTypes.Static_GetList(), "", DataTypeId.ToString());
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", ApplicationTypeId.ToString());
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), chkShowAllCate.Checked == false ? byte.Parse(ddlDataType.SelectedValue) : (byte)0, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlFeatureGroup, FeatureGroups.Static_GetList(), "...");
            CheckBoxListHelpers.Bind(chkDisplayType, DisplayTypes.Static_GetListByDataTypeId(PositionDataTypeId), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            ddlDataType.Enabled = EnableDataType == 1 ? true : false;
            bool IsAlert = false;
            if (CategoryId > 0)
            {
                Categories m_Categories = new Categories();
                m_Categories = m_Categories.Get(CategoryId, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue));
                if (m_Categories.CategoryId == 0)
                {
                    m_Categories = m_Categories.Get(CategoryId, byte.Parse(ddlLanguage.SelectedValue), 0);
                    IsAlert = true;
                }
                if (m_Categories.CategoryId == 0)
                {
                    m_Categories = m_Categories.Get(CategoryId, 0, 0);
                    IsAlert = true;
                }
                if (m_Categories.CategoryId > 0)
                {
                    //ddlDataType.Enabled = false;
                    txtCategoryName.Text = m_Categories.CategoryName;
                    txtCategoryDesc.Text = m_Categories.CategoryDesc;
                    txtUrl.Text = m_Categories.CategoryUrl;
                    txtUrlRewrite.Text = m_Categories.UrlRewriteType;
                    txtMetaTitle.Text = m_Categories.MetaTitle;
                    txtMetaDesc.Text = m_Categories.MetaDesc;
                    txtMetaKeyword.Text = m_Categories.MetaKeyword;
                    txtDisplayOrder.Text = m_Categories.DisplayOrder.ToString();
                    txtCanonicalTag.Text = m_Categories.CanonicalTag;
                    txtH1Tag.Text = m_Categories.H1Tag;
                    txtSeoFooter.Text = m_Categories.SeoFooter;

                    if (!string.IsNullOrEmpty(m_Categories.ImagePath))
                        txtIcon.Text = CmsConstants.WEBSITE_MEDIADOMAIN + m_Categories.ImagePath;
                    else
                        txtIcon.Text = "";

                    if (txtIcon.Text != "") imgDemo.Src = txtIcon.Text;

                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Categories.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDataType, m_Categories.DataTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlFeatureGroup, m_Categories.FeatureGroupId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_Categories.SiteId.ToString());
                    List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), chkShowAllCate.Checked == false ? byte.Parse(ddlDataType.SelectedValue) : (byte)0, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
                    DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...", m_Categories.ParentCategoryId.ToString());
                    ckbShowApp.Checked = m_Categories.ShowApp == 1 ? true : false;
                    ckbShowBottom.Checked = m_Categories.ShowBottom == 1 ? true : false;
                    ckbShowTop.Checked = m_Categories.ShowTop == 1 ? true : false;
                    ckbShowWap.Checked = m_Categories.ShowWap == 1 ? true : false;
                    ckbShowWeb.Checked = m_Categories.ShowWeb == 1 ? true : false;
                    CategoryDisplays m_CategoryDisplays = new CategoryDisplays();
                    List<CategoryDisplays> l_CategoryDisplays = m_CategoryDisplays.GetListByCategoryId(m_Categories.CategoryId);
                    for (int i = 0; i < l_CategoryDisplays.Count; i++)
                    {
                        CheckBoxListHelpers.SetSelected(chkDisplayType, l_CategoryDisplays[i].DisplayTypeId.ToString());
                    }

                    if (IsAlert)
                    {
                        JSAlertHelpers.ShowNotify("8", "", "Dữ liệu được lấy từ ngôn ngữ và nền tảng mặc định", this);
                    }
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
            Categories m_Categories = new Categories();
            m_Categories.CategoryId = CategoryId;
            m_Categories = m_Categories.Get();
            m_Categories.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_Categories.CategoryDesc = txtCategoryDesc.Text;
            m_Categories.CategoryName = txtCategoryName.Text;
            m_Categories.CategoryUrl = txtUrl.Text;
            m_Categories.UrlRewriteType = txtUrlRewrite.Text;
            m_Categories.CrUserId = ActUserId;
            m_Categories.DisplayOrder = txtDisplayOrder.Text == "" ? 0 : int.Parse(txtDisplayOrder.Text); ;
            m_Categories.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Categories.MetaDesc = txtMetaDesc.Text;
            m_Categories.MetaKeyword = txtMetaKeyword.Text;
            m_Categories.MetaTitle = txtMetaTitle.Text;
            m_Categories.CanonicalTag = txtCanonicalTag.Text;
            m_Categories.H1Tag = txtH1Tag.Text;
            m_Categories.SeoFooter = txtSeoFooter.Text;
            m_Categories.DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            m_Categories.SiteId = byte.Parse(ddlSite.SelectedValue);
            m_Categories.FeatureGroupId = short.Parse(ddlFeatureGroup.SelectedValue);
            m_Categories.ShowApp = byte.Parse(ckbShowApp.Checked == true ? "1" : "0");
            m_Categories.ShowBottom = byte.Parse(ckbShowBottom.Checked == true ? "1" : "0");
            m_Categories.ShowTop = byte.Parse(ckbShowTop.Checked == true ? "1" : "0");
            m_Categories.ShowWap = byte.Parse(ckbShowWap.Checked == true ? "1" : "0");
            m_Categories.ShowWeb = byte.Parse(ckbShowWeb.Checked == true ? "1" : "0");

            if (txtIcon.Text.StartsWith(CmsConstants.WEBSITE_MEDIADOMAIN))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.WEBSITE_MEDIADOMAIN.Length);
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_Categories.ImagePath = txtIcon.Text;

            m_Categories.ParentCategoryId = short.Parse(ddlParentCategory.SelectedValue);
            m_Categories.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Categories.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            CategoryDisplays m_CategoryDisplays = new CategoryDisplays();
            m_CategoryDisplays.CategoryId = m_Categories.CategoryId;
            m_CategoryDisplays.CrUserId = ActUserId;
            for (int i = 0; i < chkDisplayType.Items.Count; i++)
            {
                m_CategoryDisplays.DisplayTypeId = short.Parse(chkDisplayType.Items[i].Value);
                if (chkDisplayType.Items[i].Selected)
                {
                    m_CategoryDisplays.DisplayOrder = m_Categories.DisplayOrder;
                    m_CategoryDisplays.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                }
                else
                {
                    m_CategoryDisplays.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                }
            }
            if (cblAddOther.Checked)
            {
                if (CategoryId > 0) { JSAlertHelpers.ShowNotify("15", "success", "Cập nhật thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới thành công.", this); }
                clearForm();
                return;
            }
            if (CategoryId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật thành công.", this); }
            else { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới thành công.", this); }

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
        txtCanonicalTag.Text = "";
        txtCategoryDesc.Text = "";
        txtCategoryName.Text = "";
        txtDisplayOrder.Text = "";
        txtH1Tag.Text = "";
        txtIcon.Text = "";
        txtMetaDesc.Text = "";
        txtMetaKeyword.Text = "";
        txtMetaTitle.Text = "";
        txtSeoFooter.Text = "";
        txtUrl.Text = "";
        txtUrlRewrite.Text = "";
        ddlAppType.SelectedIndex = -1;
        ddlDataType.SelectedIndex = -1;
        ddlFeatureGroup.SelectedIndex = -1;
        ddlLanguage.SelectedIndex = -1;
        ddlParentCategory.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), chkShowAllCate.Checked == false ? byte.Parse(ddlDataType.SelectedValue) : (byte)0, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSite_SelectedIndexChanged(sender, e);
    }
    protected void chkShowAllCate_CheckedChanged(object sender, EventArgs e)
    {
        ddlSite_SelectedIndexChanged(sender, e);
    }
}