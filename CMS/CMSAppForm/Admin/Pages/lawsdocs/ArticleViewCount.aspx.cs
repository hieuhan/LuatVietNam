using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
public partial class Admin_ArticleViewCount : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected short CategoryId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users = new List<Users>();
    protected List<Categories> l_Categories = new List<Categories>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
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
        if (!IsPostBack)
        {
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDL_Bind(ddlDataType, DataTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", AppTypeId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("ArticleViewCounts"), "");
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
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
            int RowCount=0;
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            Articles m_Articles = new Articles();
            string OrderBy = ddlOrderBy.SelectedValue.ToString();
            short SiteId = short.Parse(ddlSite.SelectedValue);
            byte DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            short CategoryId = short.Parse(ddlCategory.SelectedValue);
            byte ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            int RowAmount = m_grid.PageSize;
            int PageIndex = CustomPaging.PageIndex - 1;
            short DataSourceId = short.Parse(ddlDataSource.SelectedValue);
            int CrUserId = 0;
            string Title = txtSearch.Text;
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            ArticleId = 0;
            DataSet ds = m_Articles.ArticleViewCount_GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, AppTypeId, SiteId, DataTypeId, ArticleId, CategoryId, Title, DataSourceId, ReviewStatusId, CrUserId, DateFrom, DateTo, ref RowCount);
            m_grid.DataSource = ds;
            m_grid.DataBind();
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

   protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

   protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
   {
       CustomPaging.PageIndex = 1;
       BindData();
   }
   protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
   {
       CustomPaging.PageIndex = 1;
       BindData();
   }
   protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
   {
       CustomPaging.PageIndex = 1;
       BindData();
   }
   protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
   {
       CustomPaging.PageIndex = 1;
       BindData();
   }
   protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
   {
       CustomPaging.PageIndex = 1;
       BindData();
   }
   protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
   {
       CustomPaging.PageIndex = 1;
       BindData();
   }
   protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
   {
       List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
       DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
       CustomPaging.PageIndex = 1;
       BindData();
   }
   protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
   {
       List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
       DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
       CustomPaging.PageIndex = 1;
       BindData();
   }
}

