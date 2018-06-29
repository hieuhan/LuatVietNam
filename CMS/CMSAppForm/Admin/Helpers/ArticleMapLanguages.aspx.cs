using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_ArticleMapLanguages : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected short CategoryId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Categories> l_Categories = new List<Categories>();
    protected List<Articles> l_Articles = new List<Articles>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            if (Request.Params["CategoryId"] != null && Request.Params["CategoryId"].ToString() != "")
            {
                CategoryId = short.Parse(Request.Params["CategoryId"].ToString());
            }
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
            {
                AppTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
            }
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", AppTypeId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Articles"), "");
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...", CategoryId.ToString());
            DropDownListHelpers.DDL_Bind(ddlDataSource, DataSources.Static_GetListOrderBy("DataSourceDesc"), "...");
            BindData();
        }
        else if (CustomPaging.ChangePage)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_Language = Languages.Static_GetList();
            l_ReviewStatus=ReviewStatus.Static_GetList();
            Articles m_Articles = new Articles();
            string OrderBy = ddlOrderBy.SelectedValue;
            short CategoryId = short.Parse(ddlCategory.SelectedValue);
            byte ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            int RowAmount = m_grid.PageSize;
            int PageIndex = CustomPaging.PageIndex - 1;
            short DataSourceId = short.Parse(ddlDataSource.SelectedValue);
            short TagId = 0;
            short EventStreamId = 0;
            byte DisplayTypeId = 0;
            string Title = txtSearch.Text;
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            int RowCount = 0;
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            ArticleId = 0;
            l_Categories=Categories.Static_GetAllHierachy(ActUserId, "", LanguageId, AppTypeId, 0, 0, "");

            l_Articles = m_Articles.GetPage(ActUserId, RowAmount *2, PageIndex, OrderBy, 1, 1, ArticleId, Title, DataSourceId, ReviewStatusId, 0, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
            Articles m_ArticleTemp = new Articles();
            m_ArticleTemp.ArticleId = 0;
            m_ArticleTemp.Title = "...";
            l_Articles.Insert(0, m_ArticleTemp);
            m_grid.DataSource = m_Articles.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, 2, 1, ArticleId, Title, DataSourceId, ReviewStatusId, 0, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
            m_grid.DataBind();

            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;

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
            DropDownList ddlArticleMap = (DropDownList)e.Row.FindControl("ddlArticleMap");
            ddlArticleMap.DataSource = l_Articles;
            ddlArticleMap.DataBind();
            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Label lblLanguageId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblLanguageId");
            Label lblAppTypeId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblAppTypeId");
            if (lblLanguageId != null)
            {
                Articles m_Articles = new Articles();
                m_Articles.LanguageId = byte.Parse(lblLanguageId.Text);
                m_Articles.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                m_Articles.ArticleId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_Articles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
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
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...");
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...");
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    private void Review_Click(byte ReviewStatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                
                DropDownList ddlArticleMap = (DropDownList)m_Row.FindControl("ddlArticleMap");
                Articles m_Articles = new Articles();
                if (ddlArticleMap.SelectedValue != "0")
                {
                    
                    m_Articles.LanguageId = 2;
                    m_Articles.ApplicationTypeId = 1;
                    m_Articles.ArticleId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString()); 
                    SysMessageTypeId = m_Articles.UpdateArticleId(int.Parse(ddlArticleMap.SelectedValue),ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    
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
            Articles m_Articles = new Articles();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                        Label lblAppTypeId = (Label)m_Row.FindControl("lblAppTypeId");
                        m_Articles.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_Articles.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                        m_Articles.ArticleId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Articles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        CategoryId = short.Parse(ddlCategory.SelectedValue);
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}
