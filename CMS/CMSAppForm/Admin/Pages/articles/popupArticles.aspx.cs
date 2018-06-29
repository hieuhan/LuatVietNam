using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using sms.common;
using sms.utils;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using System.Configuration;
public partial class Admin_Pages_articles_popupArticles : System.Web.UI.Page
{
    protected string CON_PRIMARY_CONSTR = "";
    protected string pagingString = "";
    protected int articleId = 0;
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected short CategoryId = 0;
    protected short SiteId = 0;
    protected byte DataTypeId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users = new List<Users>();
    protected List<Categories> l_Categories = new List<Categories>();
    //--------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActUserId = (Session["userId"] == null) ? 0 : Int32.Parse(Session["userId"].ToString());
            hdfParent.Value = Request["parent"] == null ? "" : Request["parent"].ToString();
            articleId = int.Parse(Request["articleId"] == null ? "0" : Request["articleId"].ToString());
            if (ActUserId > 0)
            {

                if (!this.IsPostBack)
                {
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
                    if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
                    {
                        LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
                    }
                    if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
                    {
                        AppTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
                    }
                    if (articleId > 0)
                    {
                        hdfSelectedItems.Value = getRelateArticles();
                    }
                    DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...", SiteId.ToString());
                    DropDownListHelpers.BindDropDownList(ddlDataType, DataTypes.Static_GetList(), DataTypeId.ToString());
                    DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
                    DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", AppTypeId.ToString());
                    DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
                    DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Articles"), "");
                    List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
                    DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...", CategoryId.ToString());
                    DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_ARTICLES"])), "...");
                    BindData();
                }
                else if (CustomPaging.ChangePage)
                {
                    BindData();
                }
            }
            else
            {
                Response.Redirect(CmsConstants.PRJ_ROOT + "/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
        }
    }

    private string getRelateArticles()
    {
        string retVal = "";
        Articles m_Articles = new Articles(CON_PRIMARY_CONSTR);
        try
        {
            List<Articles> l_Articles = m_Articles.GetListByArticleId(articleId, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue));
            if (l_Articles.Count > 0)
            {
                foreach (Articles mArticles in l_Articles)
                {
                    if (!string.IsNullOrEmpty(retVal))
                    {
                        retVal += ",";
                    }
                    retVal += mArticles.ArticleId.ToString();
                }
            }
            if (!string.IsNullOrEmpty(retVal))
            {
                retVal = "," + retVal + ",";
            }
        }
        catch (Exception ex)
        {
            Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
        }
        return retVal;
    }

    private void BindData()
    {
        try
        {
            l_Language = Languages.Static_GetList();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
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
            SiteId = short.Parse(ddlSite.SelectedValue);
            DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            ArticleId = 0;
            l_Categories = Categories.Static_GetAllHierachy(ActUserId, "", SiteId, DataTypeId, LanguageId, AppTypeId, 0, 0, "");

            m_Articles.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Articles.DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            m_Articles.IsVerify = chkIsVerify.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowApp = chkShowApp.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowBottom = chkShowBottom.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowTop = chkShowTop.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowWap = chkShowWap.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowWeb = chkShowWeb.Checked == true ? (byte)1 : (byte)0;

            m_grid.DataSource = m_Articles.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, AppTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
            m_grid.DataBind();
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
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void m_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblChkSelect = (Label)(e.Row.FindControl("lblChkSelect"));
        if (lblChkSelect == null)
        {
            return;
        }
        if (hdfSelectedItems.Value.IndexOf("," + articleId + ",") >= 0)
        {
            lblChkSelect.Text = "<input type='checkbox' checked='checked' onclick='itemCheck(this, " + m_grid.DataKeys[e.Row.RowIndex].Value.ToString() + ")' />";
        }
        else
        {
            lblChkSelect.Text = "<input type='checkbox' onclick='itemCheck(this, " + m_grid.DataKeys[e.Row.RowIndex].Value.ToString() + ")' />";
        }
        JSAlert.Confirm(articleId.ToString(), true, this);
    }

}