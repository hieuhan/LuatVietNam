using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_MenuItemsEdit : System.Web.UI.Page
{
    private int MenuId = 0;
    private short SiteId = 0;
    private int MenuItemId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["MenuItemId"] != null && Request.Params["MenuItemId"].ToString() != "")
        {
            MenuItemId = int.Parse(Request.Params["MenuItemId"].ToString());
        }
        if (Request.Params["MenuId"] != null && Request.Params["MenuId"].ToString() != "")
        {
            MenuId = short.Parse(Request.Params["MenuId"].ToString());
        }
        if (!IsPostBack)
        {
            MenuItems m_MenuItems = new MenuItems();
            m_MenuItems.MenuId = MenuId;
            List<MenuItems> l_ParentCategory = m_MenuItems.GetAllHierachy("", "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            DropDownListHelpers.DDL_Bind(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (MenuItemId > 0)
            {
                MenuItems m_Menus = new MenuItems();
                m_Menus = m_Menus.Get(MenuItemId);
                txtCategoryName.Text = m_Menus.ItemName;
                txtCategoryDesc.Text = m_Menus.ItemDesc;
                txtUrl.Text = m_Menus.Url;
                txtDisplayOrder.Text = m_Menus.DisplayOrder.ToString();
                txtMetaTitle.Text = m_Menus.MetaTitle;
                txtMetaDesc.Text = m_Menus.MetaDesc;
                txtMetaKeyword.Text = m_Menus.MetaKeyword;
                txtDisplayOrder.Text = m_Menus.DisplayOrder.ToString();
                txtCanonicalTag.Text = m_Menus.CanonicalTag;
                txtH1Tag.Text = m_Menus.H1Tag;
                txtSeoFooter.Text = m_Menus.SeoFooter;

                if (!string.IsNullOrEmpty(m_Menus.IconPath))
                    txtIcon.Text = CmsConstants.WEBSITE_MEDIADOMAIN + m_Menus.IconPath;
                else
                    txtIcon.Text = "";

                if (txtIcon.Text != "") imgDemo.Src = txtIcon.Text;

                DropDownListHelpers.DDL_SetSelected(ddlParentCategory, m_Menus.ParentItemId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Menus.ReviewStatusId.ToString());
                cblAddOther.Visible = false;
            }
            else cblAddOther.Visible = true;
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
            MenuItems m_Menus = new MenuItems();
            m_Menus.MenuItemId = MenuItemId;
            if (m_Menus.MenuItemId>0) m_Menus = m_Menus.Get();
            m_Menus.MenuId = MenuId;
            m_Menus.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Menus.ItemDesc = txtCategoryDesc.Text;
            m_Menus.ItemName = txtCategoryName.Text;
            m_Menus.Url = txtUrl.Text;
            m_Menus.DisplayOrder = txtDisplayOrder.Text == "" ? 0 : int.Parse(txtDisplayOrder.Text);
            m_Menus.CrUserId = ActUserId;
            m_Menus.ParentItemId = int.Parse(ddlParentCategory.SelectedValue);
            m_Menus.MetaDesc = txtMetaDesc.Text;
            m_Menus.MetaKeyword = txtMetaKeyword.Text;
            m_Menus.MetaTitle = txtMetaTitle.Text;
            m_Menus.CanonicalTag = txtCanonicalTag.Text;
            m_Menus.H1Tag = txtH1Tag.Text;
            m_Menus.SeoFooter = txtSeoFooter.Text;

            if (txtIcon.Text.StartsWith(CmsConstants.WEBSITE_MEDIADOMAIN))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.WEBSITE_MEDIADOMAIN.Length);
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_Menus.IconPath = txtIcon.Text;

            SysMessageTypeId = m_Menus.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (m_Menus.MenuItemId > 0)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        MenuItemId > 0 ? "Cập nhật MenuItem thành công." : "Thêm mới MenuItem thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    MenuItemId > 0 ? "Cập nhật MenuItem thành công." : "Thêm mới MenuItem thành công.", this);
            }

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
        txtCategoryName.Text = "";
        txtCategoryDesc.Text = "";
        txtCanonicalTag.Text = "";
        txtDisplayOrder.Text = "";
        txtH1Tag.Text = "";
        txtIcon.Text = "";
        txtMetaDesc.Text = "";
        txtMetaKeyword.Text = "";
        txtMetaTitle.Text = "";
        txtSeoFooter.Text = "";
        txtUrl.Text = "";
        ddlParentCategory.SelectedIndex = -1;
        ddlReviewStatus.SelectedIndex = -1;
        cbDeleteImages.Checked = false;
        cblAddOther.Checked = false;
    }

}