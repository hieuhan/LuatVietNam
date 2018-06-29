using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_MenuPages : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short ParentId = 0;
    protected short PageId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected byte MenuId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users = new List<Users>();
    protected List<Pages> l_Pages = new List<Pages>();
    protected List<Pages> l_ParentPage = new List<Pages>();
    protected List<PageTypes> l_PageTypes = new List<PageTypes>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "");
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "");
            DropDownListHelpers.BindDropDownList(ddlMenu, Menus.Static_GetListBySiteId(1), "");
            Pages m_Pages = new Pages();
            l_ParentPage = m_Pages.GetListByPageId(byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "└─ ");
            DropDownListHelpers.DDL_Bind(ddlParentPage, l_ParentPage, "...");
            BindData();
            
        }
        MenuId = byte.Parse(ddlMenu.SelectedValue);
    }

    private void BindData()
    {
        try
        {
            l_Language = Languages.Static_GetList();
            l_ReviewStatus=ReviewStatus.Static_GetList();
            l_PageTypes = PageTypes.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            Pages m_Pages = new Pages();

            ParentId = short.Parse(ddlParentPage.SelectedValue);            
            string PaddingChar = "└─ ";
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            byte MenuId = byte.Parse(ddlMenu.SelectedValue);

            l_Pages = m_Pages.GetListByMenuId(LanguageId, AppTypeId, MenuId, ParentId);
            m_grid.DataSource = l_Pages;
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = m_grid.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Pages m_DataItem = (Pages)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            Label lbCateNameTree = (Label)e.Row.FindControl("lbCateNameTree");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            
            PageId = m_DataItem.PageId;
            Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
            if (rptLanguage != null)
            {
                rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                rptLanguage.DataBind();
            }

            if (m_DataItem.ParentId == 0)
            {
                e.Row.Cells[1].Font.Bold = true;
            }
            //Bind For Filter
            for (int index = e.Row.RowIndex; index < l_Pages.Count; index++)
            {
                m_DataItem = l_Pages[index];
                if (m_DataItem.ParentId == PageId || m_DataItem.PageId == PageId)
                {
                    lbCateNameTree.Text += "," + m_DataItem.PageName;
                }
                else
                {
                    break;
                }
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            MenuPages m_MenuPages = new MenuPages();
            m_MenuPages.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_MenuPages.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_MenuPages.PageId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_MenuPages.MenuId = byte.Parse(ddlMenu.SelectedValue);            
            SysMessageTypeId = m_MenuPages.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

   
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Pages m_Pages = new Pages();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {                        
                        
                        MenuPages m_MenuPages = new MenuPages();
                        m_MenuPages.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        m_MenuPages.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
                        m_MenuPages.PageId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_MenuPages.MenuId = byte.Parse(ddlMenu.SelectedValue);                        
                        SysMessageTypeId = m_MenuPages.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlParentPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ParentId = short.Parse(ddlParentPage.SelectedValue);
        BindData();
    }


    protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
        MenuId = byte.Parse(ddlMenu.SelectedValue);
    }
}

