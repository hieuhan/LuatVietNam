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
public partial class Admin_Articles_StatisticBy : System.Web.UI.Page
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
            DropDownListHelpers.DDL_Bind(ddlDataType, DataTypes.Static_GetList(), "");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", AppTypeId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "",short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...", CategoryId.ToString());
            DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_ARTICLES"])), "...");
            BindData();
        }
        
    }

    private void BindData()
    {
        try
        {
            int TotalCount = 0;
            DataSet ds;
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            Articles m_Articles = new Articles();
            short SiteId = short.Parse(ddlSite.SelectedValue);
            byte DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            short CategoryId = short.Parse(ddlCategory.SelectedValue);
            byte ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            short DataSourceId = short.Parse(ddlDataSource.SelectedValue);
            int CrUserId = 0;
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            ArticleId = 0;
            switch (ddlReportBy.SelectedValue)
            {
                case "CategoryId":
                    //Thống kê chuyên mục
                    ds = m_Articles.Articles_StatisticByCategoryId(ActUserId, LanguageId, AppTypeId, SiteId, DataTypeId, CategoryId, DataSourceId, ReviewStatusId, CrUserId, DateFrom, DateTo, ref TotalCount);
                    break;
                case "CrDateTime":
                    //thống kê theo ngày tạo
                    ds = m_Articles.Articles_StatisticByCrDateTime(ActUserId, LanguageId, AppTypeId, SiteId, DataTypeId, CategoryId, DataSourceId, ReviewStatusId, CrUserId, DateFrom, DateTo, ref TotalCount);
                    break;
                case "CrUserId":
                    //Thống kê theo người tạo
                    ds = m_Articles.Articles_StatisticByCrUserId(ActUserId, LanguageId, AppTypeId, SiteId, DataTypeId, CategoryId, DataSourceId, ReviewStatusId, CrUserId, DateFrom, DateTo, ref TotalCount);
                    break;
                default:
                    //Thống kê theo nguồn bài viết
                    ds = m_Articles.Articles_StatisticByDataSourceId(ActUserId, LanguageId, AppTypeId, SiteId, DataTypeId, CategoryId, DataSourceId, ReviewStatusId, CrUserId, DateFrom, DateTo, ref TotalCount);
                    break;
            }
            m_grid.DataSource = ds;
            m_grid.DataBind();
            lblTong.Text = (string.Format("{0:#,#}", TotalCount) != "" ? string.Format("{0:#,#}", TotalCount) : "0");
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Start Header
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlReportBy.SelectedValue == "CategoryId")
            {
                e.Row.Cells[0].Text = "Chuyên mục";
                e.Row.Cells[1].Text = "Số bài viết";
            }

            if (ddlReportBy.SelectedValue == "CrDateTime")
            {
                e.Row.Cells[0].Text = "Ngày";
                e.Row.Cells[1].Text = "Số bài viết";
            }
            if (ddlReportBy.SelectedValue == "CrUserId")
            {
                e.Row.Cells[0].Text = "Người tạo";
                e.Row.Cells[1].Text = "Số bài viết";
            }

            if (ddlReportBy.SelectedValue == "DataSourceId")
            {
                e.Row.Cells[0].Text = "Nguồn";
                e.Row.Cells[1].Text = "Số bài viết";
            }

        }
        // End Header
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlReportBy.SelectedValue == "CategoryId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlReportBy.SelectedValue == "CrDateTime")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[0].Text = ((DateTime)
                row[0]).ToString("dd/MM/yyyy");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlReportBy.SelectedValue == "CrUserId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlReportBy.SelectedValue == "DataSourceId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            } 
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
   protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
   {
      BindData();
   }
   protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }

   protected void ddlReportBy_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
   {
       List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
       DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
       BindData();
   }
   protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
   {
       List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
       DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
       BindData();
   }
}

