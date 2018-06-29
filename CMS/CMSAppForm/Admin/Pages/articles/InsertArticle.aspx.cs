using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using sms.common;
using sms.utils;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class fckplugins_InsertArticle : System.Web.UI.Page
{
    private int ActUserId = 0;
    protected string CON_PRIMARY_CONSTR = "";
    protected string pagingString = "";
    protected int articleId = 0;
    protected List<Categories> l_Categories = new List<Categories>();
    //--------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActUserId = (Session["userId"] == null) ? 0 : Int32.Parse(Session["userId"].ToString());
            articleId = int.Parse(Request["articleId"] == null ? "0" : Request["articleId"].ToString());
            if (ActUserId > 0)
            {
                GetPrimaryConnStr(Constants.PRIVATE_PUBLIC);
                if (!this.IsPostBack)
                {
                    DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
                    bindDDL();
                    hdfSelectedItems.Value = "";
                   
                    bindData(-1);
                }
            }
            else
            {
                Response.Redirect(CmsConstants.PRJ_ROOT + "/pages/security/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    //--------------------------------------------------------------
    protected void bindDDL()
    {
        Categories m_Categories = new Categories(CON_PRIMARY_CONSTR);
        IList l_Categories = new ArrayList();
        IList l_ApproveStatus = new ArrayList();
        
        short SiteId = short.Parse(ddlSite.SelectedValue);
        try
        {
            l_Categories =  Categories.Static_GetAllHierachy(ActUserId, "", SiteId, 1, 1, 1, 0, 0, "");
            l_Categories = designCategories(l_Categories);
            m_Categories.CategoryId = 0;
            m_Categories.CategoryName = "";
            l_Categories.Insert(0, m_Categories);
            ddlCategories.DataSource = l_Categories;
            ddlCategories.DataTextField = "CategoryName";
            ddlCategories.DataValueField = "CategoryId";
            ddlCategories.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    //--------------------------------------------------------------
    private void bindData(int index)
    {
        short categoryId = 0;
        byte approveStatusId = 0;
        string keyword = "";
        string dateFrom = "";
        string dateTo = "";
        int pageIndex = 0;
        short rowsAmount = (short)m_grid.PageSize;
        int pagesCount = 0;
        
        string url = "";
        try
        {
            categoryId = Convert.ToInt16(ddlCategories.SelectedValue);
            approveStatusId = 2;
            keyword = txtKeyword.Text;
            dateFrom = txtDateTimeFrom.Text;
            dateTo = txtDateTimeTo.Text;
            pageIndex = m_grid.PageIndex;
            Articles m_Articles = new Articles(CON_PRIMARY_CONSTR);
            string OrderBy = "";
           
            byte ReviewStatusId = 2;
            short DataSourceId =0;
            short TagId = 0;
            short EventStreamId = 0;
            byte DisplayTypeId = 0;
            string Title = txtKeyword.Text;
            string DateFrom = txtDateTimeFrom.Text;
            string DateTo = txtDateTimeTo.Text;
            int RowCount = 0;
           short SiteId = short.Parse(ddlSite.SelectedValue);
           byte DataTypeId = 1;
           byte LanguageId = 1;
           byte AppTypeId = 1;
            int ArticleId = 0;
           l_Categories = Categories.Static_GetAllHierachy(ActUserId, "", SiteId, DataTypeId, LanguageId, AppTypeId, 0, 0, "");

            m_Articles.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Articles.DataTypeId = 0;
            m_Articles.IsVerify = 0;
            m_Articles.ShowApp = 0;
            m_Articles.ShowBottom = 0;
            m_Articles.ShowTop = 0;
            m_Articles.ShowWap = 0;
            m_Articles.ShowWeb = 0;
            m_Articles.LanguageId = LanguageId;
            m_Articles.ApplicationTypeId = AppTypeId;
            m_Articles.ArticleId = ArticleId;
            m_Articles.Title = Title;
            m_Articles.DataSourceId = DataSourceId;
            m_Articles.ReviewStatusId = ReviewStatusId;
            m_Articles.CategoryId = categoryId;

            m_Articles.IconStatusId = 0;
            m_Articles.ArticleTypeId = 0;
            //m_grid.DataSource = m_Articles.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, AppTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
            int rowAmount = m_grid.PageSize;
            List< Articles> l_Article = m_Articles.GetPage(ActUserId, rowAmount, pageIndex, OrderBy,LanguageId,AppTypeId, ArticleId, Title, DataSourceId, ReviewStatusId,categoryId, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
            pagesCount = RowCount / rowsAmount;
            if (RowCount % rowsAmount > 0)
            {
                pagesCount += 1;
            }
            lblCurrentPage.Text = "Page " + (m_grid.PageIndex + 1) + " / " + pagesCount;
            m_grid.EditIndex = index;
            lblRecord.Text = "Số bản ghi tìm thấy: " + RowCount;
            if (l_Article.Count == 0)
            {
                l_Article.Add(m_Articles);
                GridViewUtil.BuidNoRecord(l_Article, m_grid);
            }
            else
            {
                m_grid.DataSource = l_Article;
                m_grid.DataBind();                
            }
            bindDataSelected();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    //--------------------------------------------------------------
    private void bindDataSelected()
    {
       
        try
        {
            Articles m_Articles = new Articles(CON_PRIMARY_CONSTR);
            IList l_Article = new ArrayList();
            string[] l_ArticleId = hdfSelectedItems.Value.Split(',');
            int ArticleIdTemp = 0;
            for (int index = 0; index < l_ArticleId.Length; index++)
            {
                if (int.TryParse(l_ArticleId[index], out ArticleIdTemp))
                {
                    m_Articles = new Articles(CON_PRIMARY_CONSTR);
                    m_Articles.ArticleId = ArticleIdTemp;
                    m_Articles = m_Articles.Get();
                    l_Article.Add(m_Articles);
                }
            }
            m_gridSelected.DataSource = l_Article;
            m_gridSelected.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    //--------------------------------------------------------
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int addId = (int)m_grid.DataKeys[e.RowIndex].Value;
            if (addId != 0)
            {
                if (hdfSelectedItems.Value == "")
                    hdfSelectedItems.Value = addId.ToString();
                else
                    hdfSelectedItems.Value += "," + addId.ToString();
            }
            bindData(-1);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    //--------------------------------------------------------
    protected void m_gridSelected_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string addId = m_gridSelected.DataKeys[e.RowIndex].Value.ToString();
            if (hdfSelectedItems.Value == addId)
                hdfSelectedItems.Value = "";
            else
            {
                if (hdfSelectedItems.Value.StartsWith(addId.ToString() + ","))
                    hdfSelectedItems.Value = hdfSelectedItems.Value.Replace(addId.ToString() + ",", "");
                else if (hdfSelectedItems.Value.EndsWith("," + addId.ToString()))
                    hdfSelectedItems.Value = hdfSelectedItems.Value.Replace("," + addId.ToString(), "");
                else
                    hdfSelectedItems.Value = hdfSelectedItems.Value.Replace("," + addId.ToString() + ",", ",");
            }
            bindDataSelected();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    
    //--------------------------------------------------------
    protected void m_grid_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        try
        {
            m_grid.PageIndex = e.NewPageIndex;
            bindData(-1);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    
    //---------------------------------------------------------------------------------------
    void GetPrimaryConnStr(byte privatePublic)
    {
        try
        {
            CON_PRIMARY_CONSTR = CmsConstants.ARTICLES_CONNECTION_STRING;
        }
        catch
        {
            CON_PRIMARY_CONSTR = "";
        }
    }
   
    //---------------------------------------------------------------------------------------
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            bindData(-1);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    //---------------------------------------------------------------------------------------
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string[] l_ArticleId = hdfSelectedItems.Value.Split(',');
        string strMediaReturn = "";
        try
        {
            if (l_ArticleId.Length > 0)
            {
               
                int ArticleId = 0;
                strMediaReturn += "<div class='articlerelatepannel' >";
                for (int i = 0; i < l_ArticleId.Length; i++)
                {

                    ArticleId = Convert.ToInt32(l_ArticleId[i]);
                    if (ArticleId > 0)
                    {
                        Articles m_Articles = new Articles(CON_PRIMARY_CONSTR);
                        m_Articles.ArticleId = ArticleId;
                        m_Articles = m_Articles.Get();
                        if (!String.IsNullOrEmpty(CmsConstants.WEBSITE_DOMAIN))
                            m_Articles.ArticleUrl = CmsConstants.WEBSITE_DOMAIN + m_Articles.ArticleUrl;
                        else
                            m_Articles.ArticleUrl = "/" + m_Articles.ArticleUrl;
                        if (m_Articles.ArticleId > 0)
                        {
                            if (cbInsertImage.Checked == false)
                            {
                                
                                strMediaReturn += "<div class='articlerelate' >";
                                strMediaReturn += "<a href='" + m_Articles.ArticleUrl + "' >" + m_Articles.Title.Replace("'", "").Replace("\"", "") + "</a>";
                                strMediaReturn += "</div>";
                               
                            }
                            else
                            {
                                strMediaReturn += "<div class='articlerelate2' >";
                                strMediaReturn += "<a href='" + m_Articles.ArticleUrl + "' ><img src='" + CmsConstants.WEBSITE_IMAGEDOMAIN + CmsConstants.MEDIA_THUMNAIL_PATH + m_Articles.ImagePath + "' /></a>";
                                strMediaReturn += "<a href='" + m_Articles.ArticleUrl + "' >" + m_Articles.Title.Replace("'", "").Replace("\"", "") + "</a><br />";
                                strMediaReturn += "<span>" + m_Articles.Summary.Replace("'", "").Replace("\"", "").Replace(Environment.NewLine,"") + "</span>";
                                strMediaReturn += "</div>";
                            }
                        }
                    }
                }
                strMediaReturn += "</div>";
                strMediaReturn = strMediaReturn.Replace("\"", "");
                if (!string.IsNullOrEmpty(strMediaReturn))
                {
                    string strJS = "";
                    strJS += "<script type=" + (char)34 + "text/javascript" + (char)34 + ">";
                    strJS += "InsertHTML(" + (char)34 + "" + strMediaReturn + "" + (char)34 + "); ";
                    strJS += "</script>";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "ArticleReturn", strJS);
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (m_grid.PageIndex > 0)
        {
            m_grid.PageIndex--;
            bindData(-1);
        }
        if (m_grid.PageIndex == 0)
        {
            btnPrev.Enabled = false;
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        m_grid.PageIndex++;
        bindData(-1);
        if (m_grid.PageIndex > 0)
        {
            btnPrev.Enabled = true;
        }
    }
    //-----------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------
    protected IList designCategories(IList input)
    {
        IList output = new ArrayList();
        try
        {
            foreach (Categories m_Categories in input)
            {
                if (m_Categories.CategoryLevel == 1)
                {
                    m_Categories.CategoryName = ":--" + m_Categories.CategoryName;
                }
                else if (m_Categories.CategoryLevel == 2)
                {
                    m_Categories.CategoryName = ":--:--" + m_Categories.CategoryName;
                }
                else if (m_Categories.CategoryLevel == 3)
                {
                    m_Categories.CategoryName = ":--:--:--" + m_Categories.CategoryName;
                }
                else if (m_Categories.CategoryLevel == 4)
                {
                    m_Categories.CategoryName = ":--:--:--:--" + m_Categories.CategoryName;
                }
                output.Add(m_Categories);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
        return output;
    }

    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {

        m_grid.PageIndex =0;
        bindDDL();
        bindData(-1);
    }

    protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_grid.PageIndex = 0;
        bindData(-1);
    }
}