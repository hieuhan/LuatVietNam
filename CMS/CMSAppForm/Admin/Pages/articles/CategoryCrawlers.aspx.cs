using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_CategoryCrawlers : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SiteId = 0;
    protected List<Status> l_Status = new List<Status>();
    protected List<Users> l_Users = new List<Users>();
    protected List<Categories> l_Cate = new List<Categories>();
    protected List<DataSources> l_DataSource = new List<DataSources>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DataSources m_DataSources = new DataSources();
            DropDownListHelpers.DDL_Bind(ddlDataSource, m_DataSources.GetListByDataTypeId(1), "...");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlReviewStatus, Status.Static_GetList(), "...");
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            l_Status = Status.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            l_Cate = Categories.Static_GetAllHierachy(ActUserId, "", 0, 0, 0, 0, "");
            l_DataSource = DataSources.Static_GetList();
            SiteId = short.Parse(ddlSite.SelectedValue);

            CategoryCrawlers m_CategoryCrawlers = new CategoryCrawlers();
            string m_OrderBy = "";
            m_CategoryCrawlers.StatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_CategoryCrawlers.DataSourceId = short.Parse(ddlDataSource.SelectedValue);

            m_grid.DataSource = m_CategoryCrawlers.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, SiteId, ref RowCount);
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
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('Ban co muon xoa du lieu nay?');";
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            CategoryCrawlers m_CategoryCrawlers = new CategoryCrawlers();
            m_CategoryCrawlers.CategoryCrawlerId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_CategoryCrawlers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    
   protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            CategoryCrawlers m_CategoryCrawlers = new CategoryCrawlers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_CategoryCrawlers.CategoryCrawlerId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_CategoryCrawlers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}