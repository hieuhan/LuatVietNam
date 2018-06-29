using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_PagesEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private short PageId = 0;
    private short ParentId = 0;
    private int ActUserId = 0;
    private List<Categories> l_Categories = new List<Categories>();
    private PageCategories m_PageCategories = new PageCategories();
    private PageMeetingGroups m_PageMeetingGroups = new PageMeetingGroups();
    
    Pages m_Pages = new Pages();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["PageId"] != null && Request.Params["PageId"].ToString() != "")
        {
            PageId = short.Parse(Request.Params["PageId"].ToString());
        }
        if (PageId > 0)
            btnSaveAndNew.Visible = false;
        if (Request.Params["ParentId"] != null && Request.Params["ParentId"].ToString() != "")
        {
            ParentId = short.Parse(Request.Params["ParentId"].ToString());
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
            Pages m_Pages = new Pages();
            List<Pages> l_ParentPage = m_Pages.GetListByPageId(byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "└─ ");
            DropDownListHelpers.DDL_Bind(ddlParentPage, l_ParentPage, "...", ParentId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
            DropDownListHelpers.DDLPageTypes_BindByCulture(ddlPageTypeId, PageTypes.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlSites, Sites.Static_GetList(1), "");
            DropDownListHelpers.DDL_Bind(ddlOtherLink, l_ParentPage, "...");
            CheckBoxListHelpers.Bind(chkMenu, Menus.Static_GetListBySiteId(byte.Parse(ddlSites.SelectedValue)), "");
            BindData();
        }
        else
        {
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
        }
    }
    private void BindData()
    {
        try
        {
            if (PageId > 0)
            {
                bool IsAlert = false;
                m_Pages = m_Pages.Get(LanguageId, ApplicationTypeId, PageId);
                if (m_Pages.PageId == 0)
                {
                    m_Pages = m_Pages.Get(LanguageId, 0, PageId);
                    IsAlert = true;
                }
                if (m_Pages.PageId == 0)
                {
                    m_Pages = m_Pages.Get(0, 0, PageId);
                    IsAlert = true;
                }
                if (m_Pages.PageId > 0)
                {
                    txtPageName.Text = m_Pages.PageName;
                    txtUrl.Text = m_Pages.Url;
                    txtMetaTitle.Text = m_Pages.PageTitle;
                    txtMetaDesc.Text = m_Pages.PageDesciption;
                    txtMetaKeyword.Text = m_Pages.PageKeyword;
                    txtDisplayOrder.Text = m_Pages.DisplayOrder.ToString();
                    if (!string.IsNullOrEmpty(m_Pages.IconPath))
                        txtIcon.Text = CmsConstants.ROOT_PATH + m_Pages.IconPath;
                    else
                        txtIcon.Text = "";

                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_Pages.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlParentPage, m_Pages.ParentId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlPageTypeId, m_Pages.PageTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSites, m_Pages.SiteId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlOtherLink, m_Pages.PageId.ToString());
                    if (IsAlert)
                    {
                        JSAlertHelpers.ShowNotify("8", "", "Dữ liệu được lấy từ ngôn ngữ và nền tảng mặc định", this);
                    }
                }
                else
                {
                    txtPageName.Text = "";
                    txtUrl.Text = "";
                    txtMetaTitle.Text = "";
                    txtMetaDesc.Text = "";
                    txtMetaKeyword.Text = "";
                    txtDisplayOrder.Text = "";
                    ddlReviewStatus.SelectedIndex = -1;
                    ddlParentPage.SelectedIndex = -1;
                }
            }
            BindRelateData();
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_GridCategories_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Categories m_DataItem = (Categories)e.Row.DataItem;
            CheckBox chkAction = (CheckBox)e.Row.FindControl("chkAction");
            Label lbCateNameTree = (Label)e.Row.FindControl("lbCateNameTree");
            short CategoryId = m_DataItem.CategoryId;
            if (m_DataItem.CategoryId == m_PageCategories.CategoryId)
            {
                chkAction.Checked = true;
            }
            //Bind For Filter
            for (int index = e.Row.RowIndex; index < l_Categories.Count; index++)
            {
                m_DataItem = l_Categories[index];
                
                if (m_DataItem.CategoryId == CategoryId || m_DataItem.ParentCategoryId == CategoryId)
                {
                    lbCateNameTree.Text += "," + m_DataItem.CategoryName;
                }
                else
                {
                    break;
                }
            }
        }
    }
    private void BindRelateData()
    {
        if (m_Pages.PageTypeId.ToString() == PageTypeHelpers.NewsList || m_Pages.PageTypeId.ToString() == PageTypeHelpers.NewsDetail)
        {
            m_PageCategories = PageCategories.Static_GetByPageId(PageId);
        }
        if (m_Pages.PageTypeId.ToString() == PageTypeHelpers.MeetingList || m_Pages.PageTypeId.ToString() == PageTypeHelpers.MemberDetail)
        {
            m_PageMeetingGroups = PageMeetingGroups.Static_GetByPageId(PageId);
        }
        byte Replicated = ConstantHelpers.Replicated;
        string OrderBy = "";
        short CategoryId = 0;
        byte ReviewStatusId = 0;
        string PaddingChar = "&nbsp;&nbsp;&nbsp;&nbsp;";
        l_Categories = Categories.Static_GetAllHierachy(ActUserId, OrderBy, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), CategoryId, ReviewStatusId, PaddingChar);
        m_GridCategories.DataSource = l_Categories;
        m_GridCategories.DataBind();        
        chkMeetingGroup.DataSource = MeetingGroups.Static_GetList(LanguageId, ApplicationTypeId);
        chkMeetingGroup.DataBind();
        if (PageId > 0)
        {
            foreach (ListItem m_Item in chkMeetingGroup.Items)
            {
                if (m_Item.Value == m_PageMeetingGroups.MeetingGroupId.ToString())
                {
                    m_Item.Selected = true;
                    break;
                }
            }
            // menu
            MenuPages m_MenuPages = new MenuPages();
            List<MenuPages> l_MenuPages = new List<MenuPages>();
            l_MenuPages = m_MenuPages.GetListByPageId(PageId, LanguageId, ApplicationTypeId);
            foreach (MenuPages m_MenuPageTemp in l_MenuPages)
            {
                foreach (ListItem m_Item in chkMenu.Items)
                {
                    if (m_Item.Value == m_MenuPageTemp.MenuId.ToString() && PageId > 0)
                    {
                        m_Item.Selected = true;
                        break;
                    }

                }
            }
        }
    }
    private void SaveRelateData(short PageId)
    {
        short SysMessageId = 0;
        byte Replicated = ConstantHelpers.Replicated;
        short CategoryId = 0;
        bool isUncheck = true;
        if (ddlPageTypeId.SelectedValue == PageTypeHelpers.NewsList || ddlPageTypeId.SelectedValue == PageTypeHelpers.NewsDetail )
        {
            m_PageCategories = PageCategories.Static_GetByPageId(PageId);   
            
            foreach(GridViewRow m_Rows in m_GridCategories.Rows)
            {
                CategoryId = short.Parse(m_GridCategories.DataKeys[m_Rows.RowIndex].Value.ToString());
                if (CategoryId == 0)
                {
                    
                    continue;
                }
                CheckBox chkAction = (CheckBox)m_Rows.FindControl("chkAction");
                if(chkAction.Checked)
                {
                    isUncheck = false;
                    PageCategories m_PageCategoriesTemp = new PageCategories();
                    if(m_PageCategories.CategoryId == CategoryId)
                    {
                        
                        break;
                    }
                    else
                    {
                        m_PageCategoriesTemp.CategoryId = CategoryId;
                        m_PageCategoriesTemp.PageId = PageId;
                        m_PageCategories.PageCategoryId = 0;
                        m_PageCategoriesTemp.Insert(Replicated, ActUserId, ref SysMessageId);
                        break;
                    }
                }
            }
            if(isUncheck)
            {
                m_PageCategories.Delete(Replicated,ActUserId,ref SysMessageId);
            }
        }
        isUncheck = true;
        if (ddlPageTypeId.SelectedValue == PageTypeHelpers.MeetingList || ddlPageTypeId.SelectedValue == PageTypeHelpers.MemberDetail)
        {
            m_PageMeetingGroups = PageMeetingGroups.Static_GetByPageId(PageId);
            if (PageId > 0)
            {
                foreach (ListItem m_Item in chkMeetingGroup.Items)
                {
                    if (m_Item.Selected)
                    {
                        isUncheck = false;
                        if (m_Item.Value == m_PageMeetingGroups.MeetingGroupId.ToString())
                            break;
                        else
                        {
                            m_PageMeetingGroups.MeetingGroupId = short.Parse(m_Item.Value);
                            m_PageMeetingGroups.PageId = PageId;
                            m_PageMeetingGroups.PageMeetingGroupId = 0;
                            m_PageMeetingGroups.Insert(Replicated, ActUserId, ref SysMessageId);
                            break;
                        }
                    }
                }
                if (isUncheck)
                {
                    m_PageMeetingGroups.Delete(Replicated, ActUserId, ref SysMessageId);
                }
            }
        }
        // menu 
        isUncheck = true;
        if (PageId > 0)
        {
            MenuPages m_MenuPages = new MenuPages();
            foreach (ListItem m_Item in chkMenu.Items)
            {
                if (m_Item.Selected)
                {
                    isUncheck = false;
                    m_MenuPages.MenuId = byte.Parse(m_Item.Value);
                    m_MenuPages.PageId = PageId;
                    m_MenuPages.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                    m_MenuPages.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
                    m_MenuPages.MenuPageId = 0;
                    m_MenuPages.Insert(Replicated, ActUserId, ref SysMessageId);
                }
            }
            if (isUncheck)
            {
                m_MenuPages.Delete(Replicated, ActUserId, ref SysMessageId);
            }
        }
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        LanguageId = byte.Parse(ddlLanguage.SelectedValue);
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
        BindData();
    }
    private void Save()
    {
        try
        {

            short SysMessageId = 0;
            Pages m_Pages = new Pages();
            m_Pages.PageId = PageId;
            if (PageId > 0)
            {
                m_Pages = m_Pages.Get(LanguageId, ApplicationTypeId, PageId);
                if(m_Pages.PageId == 0)
                    m_Pages.PageId = PageId;
            }

            m_Pages.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_Pages.PageName = txtPageName.Text;
            m_Pages.Url = txtUrl.Text;
            m_Pages.CrUserId = ActUserId;
            m_Pages.DisplayOrder = txtDisplayOrder.Text == "" ? (short)0 : short.Parse(txtDisplayOrder.Text);
            m_Pages.PageTypeId = byte.Parse(ddlPageTypeId.SelectedValue);
            m_Pages.SiteId = short.Parse(ddlSites.SelectedValue);
            m_Pages.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Pages.PageDesciption = txtMetaDesc.Text;
            m_Pages.PageKeyword = txtMetaKeyword.Text;
            m_Pages.PageTitle = txtMetaTitle.Text;
            
            if (txtIcon.Text.StartsWith(CmsConstants.ROOT_PATH))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.ROOT_PATH.Length);
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_Pages.IconPath = txtIcon.Text;
            m_Pages.ParentId = short.Parse(ddlParentPage.SelectedValue);
            m_Pages.SiteId = short.Parse(ddlSites.SelectedValue);
            m_Pages.DisplayOrder = short.Parse(txtDisplayOrder.Text);
            m_Pages.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Pages.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (m_Pages.PageTypeId.ToString() != PageTypeHelpers.OtherLink)
                m_Pages.Url = LinkHelpers.getRewriteUrlPages(PageTypes.Static_Get(m_Pages.PageTypeId).DefaultUrl, m_Pages.PageName, m_Pages.PageId.ToString());
            else
                m_Pages.Url = txtUrl.Text;
            m_Pages.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            // save relate data
            SaveRelateData(m_Pages.PageId);
            JSAlertHelpers.ShowNotify("5000", "", SysMessages.Static_GetDesc(SysMessageId), this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    
    protected void ddlPageTypeId_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    private void ClearForm()
    {
        txtIcon.Text = "";
        txtMetaDesc.Text = "";
        txtMetaKeyword.Text = "";
        txtMetaTitle.Text = "";
        txtPageName.Text = "";
        txtUrl.Text = "";

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
        Response.Redirect("Pages.aspx?LanguageId=" + LanguageId.ToString() + "&AppTypeId=" + ApplicationTypeId.ToString() + "&ParentId=" + ParentId.ToString());
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        try
        {
            Save();
            if (PageId > 0)
            {
                BindData();
            }
            else
                ClearForm();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pages.aspx?LanguageId=" + LanguageId.ToString() + "&AppTypeId=" + ApplicationTypeId.ToString() + "&ParentId=" + ParentId.ToString());
    }
    protected void ddlLanguage_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BindData();

    }
    protected void ddlAppType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlSites_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BindData();
    }
}