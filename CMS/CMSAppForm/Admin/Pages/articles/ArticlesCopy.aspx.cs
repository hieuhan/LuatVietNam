﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_ArticlesCopy : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected short SiteId = 0;
    protected byte DataTypeId = 0;
    protected short CategoryId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users = new List<Users>();
    protected List<Categories> l_Categories = new List<Categories>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
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
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            DropDownListHelpers.DDL_Bind(ddlDataType, DataTypes.Static_GetList(), "", DataTypeId.ToString());
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", AppTypeId.ToString());
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "",short.Parse(ddlSite.SelectedValue),byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...", CategoryId.ToString());
            List<Categories> l_CategoryCopy = new List<Categories>();
            Categories m_Categories = new Categories(WebConstans.CONNECTION_STRING_COPY);
            l_CategoryCopy = m_Categories.GetAllHierachy(ActUserId, "",  byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlCategoryCopy, l_CategoryCopy, "...",WebConstans.CATEGOY_COPY.ToString());
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
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            Articles m_Articles = new Articles();
            string OrderBy = "";
            short CategoryId = short.Parse(ddlCategory.SelectedValue);
            byte ReviewStatusId = 0;
            int RowAmount = m_grid.PageSize;
            int PageIndex = CustomPaging.PageIndex - 1;
            short DataSourceId = 0;
            short TagId = 0;
            short EventStreamId = 0;
            byte DisplayTypeId = 0;
            string Title = txtSearch.Text;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_Articles.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Articles.DataTypeId = byte.Parse(ddlDataType.SelectedValue);

            m_grid.DataSource = m_Articles.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, AppTypeId, 0, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
            m_grid.DataBind();

            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            // bind relate article
            m_Articles = new Articles(WebConstans.CONNECTION_STRING_COPY);
            RowAmount = m_gridRelate.PageSize;
            PageIndex =0;
            OrderBy="";
            CategoryId = 0;// WebConstans.CATEGOY_COPY;
            DataSourceId = WebConstans.DATASOURCE_COPY;
            List<Articles> l_ArticleRelates = m_Articles.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, AppTypeId, 0, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
            m_gridRelate.DataSource = l_ArticleRelates;
            m_gridRelate.DataBind();
            
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
       
        
        try
        {
            Articles m_Articles = new Articles();
            m_Articles.ArticleId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_Articles = m_Articles.Get();
            SaveData(m_Articles);

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void SaveData(Articles m_ArticlesForCopy)
    {
        try
        {
            short SysMessageId = 0;
            Articles m_Articles = new Articles(WebConstans.CONNECTION_STRING_COPY);
            
            m_Articles.CategoryId = short.Parse(ddlCategoryCopy.SelectedValue);
            m_Articles.ApplicationTypeId = m_ArticlesForCopy.ApplicationTypeId;
            m_Articles.LanguageId = m_ArticlesForCopy.LanguageId;
            m_Articles.DataSourceId = WebConstans.DATASOURCE_COPY;
            m_Articles.SiteId = WebConstans.SITE_COPY;
            m_Articles.DataTypeId = m_ArticlesForCopy.DataTypeId;
            m_Articles.ArticleTypeId = m_ArticlesForCopy.ArticleTypeId;
            m_Articles.IconStatusId = m_ArticlesForCopy.IconStatusId;
            m_Articles.InventoryStatusId = m_ArticlesForCopy.InventoryStatusId;
            m_Articles.CurrencyId = m_ArticlesForCopy.CurrencyId;
            m_Articles.Title = m_ArticlesForCopy.Title;
            m_Articles.SourceUrl = "";
            m_Articles.Summary = m_ArticlesForCopy.Summary;
            m_Articles.ArticleUrl = "";
            m_Articles.DisplayStartTime = m_ArticlesForCopy.DisplayStartTime;
            m_Articles.DisplayEndTime = m_ArticlesForCopy.DisplayEndTime;
            m_Articles.DisplayOrder = m_ArticlesForCopy.DisplayOrder;
            m_Articles.ArticleCode = m_ArticlesForCopy.ArticleCode;

            m_Articles.OriginalPrice = m_ArticlesForCopy.OriginalPrice;
            m_Articles.SalePrice = m_ArticlesForCopy.SalePrice;
            m_Articles.ContactPrice = m_ArticlesForCopy.ContactPrice;
            m_Articles.MetaTitle = m_ArticlesForCopy.MetaTitle;
            m_Articles.MetaDesc = m_ArticlesForCopy.MetaDesc;
            m_Articles.MetaKeyword = m_ArticlesForCopy.MetaKeyword;
            m_Articles.IsVerify = m_ArticlesForCopy.IsVerify;
            m_Articles.ShowApp = m_ArticlesForCopy.ShowApp;
            m_Articles.ShowBottom = m_ArticlesForCopy.ShowBottom;
            m_Articles.ShowTop = m_ArticlesForCopy.ShowTop;
            m_Articles.ShowWap = m_ArticlesForCopy.ShowWap;
            m_Articles.ShowWeb = m_ArticlesForCopy.ShowWeb;
            m_Articles.ArticleContent = m_ArticlesForCopy.ArticleContent;

            m_Articles.SourceUrl = m_ArticlesForCopy.SourceUrl;
            m_Articles.ImagePath = CmsConstants.WEBSITE_IMAGEDOMAIN + m_ArticlesForCopy.ImagePath;
            m_Articles.ReviewStatusId = cbPublicCopy.Checked ? m_ArticlesForCopy.ReviewStatusId : (byte)1;
            m_Articles.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (m_Articles.ArticleId > 0)
            {
                ArticleCategories m_ArticleCategories = new ArticleCategories(WebConstans.CONNECTION_STRING_COPY);
                m_ArticleCategories.ArticleId = m_Articles.ArticleId;
                m_ArticleCategories.CategoryId = short.Parse(ddlCategoryCopy.SelectedValue);
                m_ArticleCategories.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                //Insert main category and parent
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void m_gridRelate_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
        }
    }

    protected void m_gridRelate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        short SysMessageId = 0;
        try
        {
            ArticleRelates m_ArticleRelates = new ArticleRelates();
            m_ArticleRelates.ArticleId = ArticleId;
            m_ArticleRelates.ArticleReferenceId = int.Parse(m_gridRelate.DataKeys[e.RowIndex].Value.ToString());
            m_ArticleRelates.ArticleRelateId = int.Parse(m_gridRelate.DataKeys[e.RowIndex].Value.ToString());
            m_ArticleRelates.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        
        short SysMessageId = 0;
        try
        {
            Articles m_Articles = new Articles();
            foreach (GridViewRow m_Row in m_gridRelate.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        ArticleRelates m_ArticleRelates = new ArticleRelates();
                        m_ArticleRelates.ArticleId = ArticleId;
                        m_ArticleRelates.ArticleReferenceId = int.Parse(m_gridRelate.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_ArticleRelates.ArticleRelateId = int.Parse(m_gridRelate.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_ArticleRelates.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
