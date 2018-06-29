using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_Pages : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short ParentId = 0;
    protected short PageId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Pages> l_Pages = new List<Pages>();
    protected List<Pages> l_ParentPage = new List<Pages>();
    protected List<Sites> l_Sites = new List<Sites>();
    protected List<PageTypes> l_PageTypes = new List<PageTypes>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ParentId"] != null && Request.Params["ParentId"].ToString() != "")
        {
            ParentId = short.Parse(Request.Params["ParentId"].ToString());
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
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(1), "...");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", AppTypeId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            Pages m_Pages = new Pages();
            l_ParentPage = m_Pages.GetListByPageId(byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "└─ ");
            DropDownListHelpers.DDL_Bind(ddlParentPage, l_ParentPage, "...", ParentId.ToString());
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            l_Language = Languages.Static_GetList();
            l_ReviewStatus=ReviewStatus.Static_GetList();
            l_PageTypes = PageTypes.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            Pages m_Pages = new Pages();
            m_Pages.SiteId = short.Parse(ddlSite.SelectedValue == "" ? "0" : ddlSite.SelectedValue);
            ParentId = short.Parse(ddlParentPage.SelectedValue);
            byte ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            string PaddingChar = "└─ ";
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            //l_Pages = m_Pages.GetListByPageId(LanguageId, AppTypeId, ParentId, ReviewStatusId, PaddingChar);
            l_Pages = m_Pages.GetPage(ActUserId,0,0,"",LanguageId,AppTypeId,0,0,"",0,ReviewStatusId,"","",ref RowCount);
            m_grid.DataSource = l_Pages;
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = RowCount.ToString();
            //lblTong.Text = m_grid.Rows.Count.ToString();
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
            if (ddlLevle.SelectedValue != "0")
            {
                if (m_DataItem.LevelId > byte.Parse(ddlLevle.SelectedValue))
                {
                    e.Row.Visible = false;
                    return;
                }
            }
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
            Pages m_Pages = new Pages();
            m_Pages.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Pages.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_Pages.PageId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Pages.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
        ParentId = short.Parse(ddlParentPage.SelectedValue);
        Pages m_Pages = new Pages();
        l_ParentPage = m_Pages.GetListByPageId(byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "└─ ");
        DropDownListHelpers.DDL_Bind(ddlParentPage, l_ParentPage, "...", ParentId.ToString());
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ParentId = short.Parse(ddlParentPage.SelectedValue);
        Pages m_Pages = new Pages();
        l_ParentPage = m_Pages.GetListByPageId(byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "└─ ");
        DropDownListHelpers.DDL_Bind(ddlParentPage, l_ParentPage, "...", ParentId.ToString());
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    private void Review_Click(byte ReviewStatusId)
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
                        
                        byte LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        byte ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
                        short PageId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Pages = m_Pages.Get(LanguageId, ApplicationTypeId, PageId);
                        m_Pages.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_Pages.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }

    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);
        BindData();
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
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
                        m_Pages.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        m_Pages.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
                        m_Pages.PageId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Pages.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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


    protected void ddlLevle_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}

