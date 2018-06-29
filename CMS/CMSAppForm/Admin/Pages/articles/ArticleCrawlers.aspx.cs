using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_ArticleCrawlers : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected short CategoryId = 0;
    protected short SiteId = 0;
    protected byte DataTypeId = 0;
    protected List<Users> l_Users = new List<Users>();
    protected List<Categories> l_Categories = new List<Categories>();
    protected List<DataSources> l_DataSources;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (Request.Params["CategoryId"] != null && Request.Params["CategoryId"].ToString() != "")
        {
            CategoryId = short.Parse(Request.Params["CategoryId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...", SiteId.ToString());
            DropDownListHelpers.BindDropDownList(ddlDataType, DataTypes.Static_GetList(), DataTypeId.ToString());
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 0, 0, 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...", CategoryId.ToString());
            DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_ARTICLES"])), "...");
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
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            ArticleCrawlers m_Articles = new ArticleCrawlers();
            string OrderBy = "";
            int RowAmount = m_grid.PageSize;
            int PageIndex = CustomPaging.PageIndex - 1;
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            int RowCount = 0;
            SiteId = short.Parse(ddlSite.SelectedValue);
            DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            l_DataSources = DataSources.Static_GetListByDataTypeId(DataTypeId);
            l_Categories=Categories.Static_GetAllHierachy(ActUserId, "",SiteId, DataTypeId, 0, 0, 0, 0, "");

            m_Articles.DataTypeId = DataTypeId;
            m_Articles.Category = ddlCategory.SelectedValue;
            m_Articles.DataSourceId = short.Parse(ddlDataSource.SelectedValue);
            m_Articles.isSync = byte.Parse(ddlReviewStatus.SelectedValue);
            m_grid.DataSource = m_Articles.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SiteId, DateFrom, DateTo, ref RowCount);
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
            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            ArticleCrawlers m_Articles = new ArticleCrawlers();
            m_Articles.ArticleId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Articles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void lbReview_Click(object sender, EventArgs e)
    {
        short SysMessageId = 0;
        ArticleCrawlers m_Articles = new ArticleCrawlers();
        foreach (GridViewRow m_Row in m_grid.Rows)
        {
            CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
            if (chkAction != null)
            {
                if (chkAction.Checked)
                {
                    m_Articles.ArticleId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                    m_Articles.Sync(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                }
            }
        }
        BindData();
    }
   
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            ArticleCrawlers m_Articles = new ArticleCrawlers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
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
    
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        CategoryId = short.Parse(ddlCategory.SelectedValue);
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 0, 0, 0, 0, "- ");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 0, 0, 0, 0, "- ");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 0, 0, 0, 0, "- ");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
        CustomPaging.PageIndex = 1;
        BindData();
    }
}
