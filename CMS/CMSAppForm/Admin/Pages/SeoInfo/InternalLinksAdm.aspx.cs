using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using sms.common;
using sms.utils;
using ICSoft.CMSLib;

public partial class admin_pages_articles_InternalLinksAdm : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int TagId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = (Session["userId"] == null) ? 0 : Int32.Parse(Session["userId"].ToString());
        if (!IsPostBack)
        {


        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindDDL();
            BindData();
        }
    }

    private void BindDDL()
    {
        Sites m_Sites = new Sites();
        InternalLinkGroups m_InternalLinkGroups = new InternalLinkGroups();
        try
        {
            ddlSites.DataSource = m_Sites.GetList(ActUserId);
            ddlSites.DataBind();
            BindGroupsDDL(Convert.ToInt16(ddlSites.SelectedValue));
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
        }
    }

    private void BindGroupsDDL(short siteId)
    {
        InternalLinkGroups m_InternalLinkGroups = new InternalLinkGroups();
        IList l_InternalLinkGroups = new ArrayList();
        try
        {
            l_InternalLinkGroups = m_InternalLinkGroups.GetList(siteId);
            m_InternalLinkGroups.InternalLinkGroupId = 0;
            m_InternalLinkGroups.InternalLinkGroupName = "";
            m_InternalLinkGroups.InternalLinkGroupDesc = "";
            l_InternalLinkGroups.Insert(0, m_InternalLinkGroups);
            ddlInternalLinkGroups.DataSource = l_InternalLinkGroups;
            ddlInternalLinkGroups.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            InternalLinks m_InternalLinks = new InternalLinks();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_OrderType = ddlOrderTypes.SelectedValue;
            short siteId = Convert.ToInt16(ddlSites.SelectedValue);
            short groupId =  Convert.ToInt16(ddlInternalLinkGroups.SelectedValue);
            string dateFrom=string.Empty, dateTo=string.Empty;
            string keywords = txtSearch.Text;
            m_grid.DataSource = m_InternalLinks.Search(siteId, groupId, keywords, dateFrom, dateTo, m_OrderBy, m_OrderType, ref RowCount, m_grid.PageSize, CustomPaging.PageIndex);
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
        }
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('Bạn có chắc muốn xóa tag này');";
            }

        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            InternalLinks m_InternalLinks = new InternalLinks();
            m_InternalLinks.InternalLinkId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_InternalLinks.Delete();
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
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
            InternalLinks m_InternalLinks = new InternalLinks();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                        Label lblAppTypeId = (Label)m_Row.FindControl("lblAppTypeId");
                        m_InternalLinks.InternalLinkId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_InternalLinks.Delete();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
        }
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlOrderTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlInternalLinkGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlddlSites_SelectedIndexChanged(object sender, EventArgs e)
    {
        short siteId = Convert.ToInt16(ddlSites.SelectedValue);
        BindGroupsDDL(siteId);
        BindData();
    }
}