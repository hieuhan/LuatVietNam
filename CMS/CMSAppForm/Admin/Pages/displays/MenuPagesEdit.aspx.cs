using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
public partial class Admin_PagesEdit : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short ParentId = 0;
    protected short PageId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected byte MenuId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Pages> l_Pages = new List<Pages>();
    protected List<Pages> l_ParentPage = new List<Pages>();
    protected List<PageTypes> l_PageTypes = new List<PageTypes>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["MenuId"] != null && Request.Params["MenuId"].ToString() != "")
        {
            MenuId = byte.Parse(Request.Params["MenuId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
        {
            AppTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
        }
        
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", AppTypeId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            Pages m_Pages = new Pages();
            l_ParentPage = m_Pages.GetListByPageId(byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "└─ ");
            DropDownListHelpers.DDL_Bind(ddlParentPage, l_ParentPage, "...");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_Language = Languages.Static_GetList();
            l_ReviewStatus=ReviewStatus.Static_GetList();
            l_PageTypes = PageTypes.Static_GetList();
            Pages m_Pages = new Pages();

            ParentId = short.Parse(ddlParentPage.SelectedValue);
            byte ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            string PaddingChar = "└─ ";
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            l_ParentPage = m_Pages.GetListByPageId(byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), ParentId, 0, PaddingChar);
            l_Pages = m_Pages.GetListByMenuId(LanguageId, AppTypeId, MenuId);
            
            List<Pages> l_PagesTemp = new List<Pages>();
            foreach (Pages m_PageTemp in l_ParentPage)
            {
                if (l_Pages.Find(x => x.PageId == m_PageTemp.PageId) == null)
                    l_PagesTemp.Add(m_PageTemp);
            }
            m_grid.DataSource = l_ParentPage;
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = l_PagesTemp.Count.ToString();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Pages m_DataItem = (Pages)e.Row.DataItem;
                if (l_Pages.Find(x => x.PageId == m_DataItem.PageId) != null)
                {
                    e.Row.Visible = false;
                    return;
                }
                LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
                Label lbCateNameTree = (Label)e.Row.FindControl("lbCateNameTree");
                CheckBox chkAction = (CheckBox)e.Row.FindControl("chkAction");
                if (lbDelete != null)
                {
                    lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
                }

                PageId = m_DataItem.PageId;

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
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
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

    
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlParentPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ParentId = short.Parse(ddlParentPage.SelectedValue);
        BindData();
    }
    private void SaveData()
    {
        try
        {
            byte SysMessageTypeId = 0;
            short SysMessageId = 0;
            foreach (GridViewRow m_Rows in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Rows.FindControl("chkAction");
                if (chkAction.Checked)
                {
                    MenuPages m_MenuPages = new MenuPages();
                    m_MenuPages.MenuId = MenuId;
                    m_MenuPages.PageId = short.Parse(m_grid.DataKeys[m_Rows.RowIndex].Value.ToString());
                    m_MenuPages.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                    m_MenuPages.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
                    SysMessageTypeId = m_MenuPages.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
        SaveData();
        //JSAlertHelpers.Close(Page);
        string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
        ClientScriptManager csm = this.ClientScript;
        csm.RegisterClientScriptBlock(this.GetType(), "close", script);
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        SaveData();
        BindData();
    }
}

