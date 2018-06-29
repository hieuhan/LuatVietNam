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
using ICSoft.CMSViewLib;

public partial class Admin_Articles : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected short CategoryId = 0;
    protected short SiteId = 0;
    protected byte DataTypeId = 0;
    protected byte EnableDataType = 0;
    protected short ActionId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users = new List<Users>();
    protected List<Categories> l_Categories = new List<Categories>();
    Users m_Users = new Users();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ActionId"] != null && Request.Params["ActionId"].ToString() != "")
        {
            ActionId = short.Parse(Request.Params["ActionId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (Request.Params["EnableDataType"] != null && Request.Params["EnableDataType"].ToString() != "")
        {
            EnableDataType = byte.Parse(Request.Params["EnableDataType"].ToString());
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
            DropDownListHelpers.BindDropDownList(ddlDataType, DataTypes.Static_GetList(), DataTypeId.ToString());
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", AppTypeId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Articles"), "");
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...", CategoryId.ToString());
            DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_ARTICLES"])), "...");

            DropDownListHelpers.DDL_Bind(ddlArticleType, ArticleTypes.Static_GetList(1), "...", "0");
            DropDownListHelpers.DDL_Bind(ddlIconStatus, IconStatus.Static_GetList(), "...", "0");

            ddlUser.DataSource = m_Users.GetListBySiteIdOrderByUserName(short.Parse(ddlSite.SelectedValue), 1);
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("Tất cả", "0"));
            if (Session["Article-ddlLanguage"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("ArticlesEdit.aspx"))
            {
                ddlAppType.SelectedValue = Session["Article-ddlAppType"].ToString();
                ddlArticleType.SelectedValue = Session["Article-ddlArticleType"].ToString();
                ddlCategory.SelectedValue = Session["Article-ddlCategory"].ToString();
                ddlDataSource.SelectedValue = Session["Article-ddlDataSource"].ToString();
                ddlDataType.SelectedValue = Session["Article-ddlDataType"].ToString();
                ddlIconStatus.SelectedValue = Session["Article-ddlIconStatus"].ToString();
                ddlLanguage.SelectedValue = Session["Article-ddlLanguage"].ToString();
                ddlOrderBy.SelectedValue = Session["Article-ddlOrderBy"].ToString();
                ddlSite.SelectedValue = Session["Article-ddlSite"].ToString();
                ddlUser.SelectedValue = Session["Article-ddlUser"].ToString();
                ddlReviewStatus.SelectedValue = Session["Article-ddlReviewStatus"].ToString();
                txtSearch.Text = Session["Article-Key"].ToString();
                txtDateFrom.Text = Session["Article-DateFrom"].ToString();
                txtDateTo.Text = Session["Article-DateTo"].ToString();
                chkIsVerify.Checked = Session["Article-chkIsVerify"].ToString() == "1" ? true : false;
                chkShowApp.Checked = Session["Article-chkShowApp"].ToString() == "1" ? true : false;
                chkShowBottom.Checked = Session["Article-chkShowBottom"].ToString() == "1" ? true : false;
                chkShowTop.Checked = Session["Article-chkShowTop"].ToString() == "1" ? true : false;
                chkShowWap.Checked = Session["Article-chkShowWap"].ToString() == "1" ? true : false;
                chkShowWeb.Checked = Session["Article-chkShowWeb"].ToString() == "1" ? true : false;
            }
            
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
            lblMsg.Text = "";
            l_Language = Languages.Static_GetList();
            l_ReviewStatus=ReviewStatus.Static_GetList();
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
            l_Categories=Categories.Static_GetAllHierachy(ActUserId, "",SiteId, DataTypeId, LanguageId, AppTypeId, 0, 0, "");

            m_Articles.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Articles.DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            m_Articles.IsVerify = chkIsVerify.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowApp = chkShowApp.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowBottom = chkShowBottom.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowTop = chkShowTop.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowWap = chkShowWap.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowWeb = chkShowWeb.Checked == true ? (byte)1 : (byte)0;
            m_Articles.LanguageId = LanguageId;
            m_Articles.ApplicationTypeId = AppTypeId;
            m_Articles.ArticleId = ArticleId;
            m_Articles.Title = Title;
            m_Articles.DataSourceId = DataSourceId;
            m_Articles.ReviewStatusId = ReviewStatusId;
            m_Articles.CategoryId = CategoryId;

            m_Articles.IconStatusId = byte.Parse(ddlIconStatus.SelectedValue);
            m_Articles.ArticleTypeId = byte.Parse(ddlArticleType.SelectedValue);
            //m_grid.DataSource = m_Articles.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, AppTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
            m_Articles.CrUserId = int.Parse(ddlUser.SelectedValue);
            m_grid.DataSource = m_Articles.GetPageView(ActUserId, RowAmount, PageIndex, OrderBy, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);

            m_grid.DataBind();
           
            SetCurentData();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;

            ddlDataType.Enabled = EnableDataType == 1 ? true : false;
            trLanguage.Visible = false;
            trCategory.Visible = true;
            m_grid.Columns[2].Visible = true; //Category
            if (",11,12,13,".IndexOf("," + DataTypeId.ToString() + ",") >= 0) //Nhac sy, Ca sy, Album
            {
                trLanguage.Visible = false;
                chkShowTop.Visible = false;
                chkShowBottom.Visible = false;
                chkIsVerify.Visible = false;
                //trCategory.Visible = false;
                m_grid.Columns[1].HeaderText = "Tên";
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void SetCurentData()
    {
        Session["Article-ddlAppType"] = ddlAppType.SelectedValue;
        Session["Article-ddlArticleType"] = ddlArticleType.SelectedValue;
        Session["Article-ddlCategory"] = ddlCategory.SelectedValue;
        Session["Article-ddlDataSource"] = ddlDataSource.SelectedValue;
        Session["Article-ddlDataType"] = ddlDataType.SelectedValue;
        Session["Article-ddlIconStatus"] = ddlIconStatus.SelectedValue;
        Session["Article-ddlLanguage"] = ddlLanguage.SelectedValue;
        Session["Article-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["Article-ddlSite"] = ddlSite.SelectedValue;
        Session["Article-ddlUser"] = ddlUser.SelectedValue;
        Session["Article-Key"] = txtSearch.Text;
        Session["Article-DateFrom"] = txtDateFrom.Text;
        Session["Article-DateTo"] = txtDateTo.Text;
        Session["Article-ddlReviewStatus"] = ddlReviewStatus.SelectedValue;
        Session["Article-chkIsVerify"] = chkIsVerify.Text;
        Session["Article-chkShowApp"] = chkShowApp.Text;
        Session["Article-chkShowTop"] = chkShowTop.Text;
        Session["Article-chkShowBottom"] = chkShowBottom.Text;
        Session["Article-chkShowWap"] = chkShowWap.Text;
        Session["Article-chkShowWeb"] = chkShowWeb.Text;
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ArticlesView m_DataItem = (ArticlesView)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }

            Label lblImage = (Label)e.Row.FindControl("lblImage");
            lblImage.Text = m_DataItem.GetImageUrl_IconWithHtmlTag(40, 40);

            ArticleId = m_DataItem.ArticleId;
            DataTypeId = m_DataItem.DataTypeId;
            //Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
            //if (rptLanguage != null)
            //{
            //    rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
            //    rptLanguage.DataBind();
            //}
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
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
                if (m_Articles.ArticleId > 0) Message = "Xóa bài viết thất bại";
                else Message = "Xóa tag thành công";
            }
            JSAlertHelpers.ShowNotify("15", "success", Message, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
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
        string TempCategoryId = ddlCategory.SelectedValue;
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...", TempCategoryId);
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string TempCategoryId = ddlCategory.SelectedValue;
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...", TempCategoryId);
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
        int CountSuc = 0;
        string Message = "";
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
                        ArticleId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        if (ReviewStatusId == 2)
                        {
                            m_Articles = m_Articles.Get(ArticleId, 0, 0);
                            if (!string.IsNullOrEmpty(m_Articles.DisplayStartTime.ToString().Trim()))
                            {
                                ReviewStatusId = 12;//duyệt chờ xuất bản
                            }
                        }
                        m_Articles.ArticleId = ArticleId;
                        m_Articles.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_Articles.UpdateReviewStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        CountSuc++;
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
        Message = "Duyệt thành công " + CountSuc.ToString() + " Bài viết. ";
        if (ReviewStatusId != 2)
            Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " Bài viết. ";
        //lblMsg.Text = Message;
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }

    protected void lbReview_Click(object sender, EventArgs e)
    {
        if(!UserHelpers.isInRole(ActUserId, RoleHelpers.ReviewArticle))
        {
            JSAlertHelpers.ShowNotify("15", "success", "Bạn chưa được cấp quyền duyệt bài viết", this);
            BindData();
            return;
        }
        Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        if (!UserHelpers.isInRole(ActUserId, RoleHelpers.ReviewArticle))
        {
            JSAlertHelpers.ShowNotify("15", "success", "Bạn chưa được cấp quyền duyệt bài viết", this);
            BindData();
            return;
        }
        Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int CountSuc = 0;
        int CountFalse = 0;
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
                        m_Articles = m_Articles.Get();
                        if(m_Articles.ReviewStatusId == 2)
                        {
                            CountFalse++;
                            continue;
                        }
                        SysMessageTypeId = m_Articles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        CountSuc++;
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
        string Message = "Xóa thành công " + CountSuc.ToString() + " Bài viết. ";
        if(CountFalse > 0)
        {
            Message += "Xóa thất bại " + CountFalse.ToString() + " Bài viết do đang ở trạng thái đã duyệt. ";
        }

        JSAlertHelpers.ShowNotify("15", "success", Message, this);
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
        List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
        ddlUser.DataSource = m_Users.GetListBySiteIdOrderByUserName(short.Parse(ddlSite.SelectedValue), 1);
        ddlUser.DataBind();
        ddlUser.Items.Insert(0, new ListItem("Tất cả", "0"));
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

    protected void ddlArticleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlIconStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //---------------------------------------------------------------------------------------
    protected void m_grid_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "OrderTop")
        {
            Articles m_Articles = new Articles();
            m_Articles.Move(int.Parse(m_grid.DataKeys[index].Value.ToString()), short.Parse(ddlCategory.SelectedValue), "top");
            BindData();
        }
        else if (e.CommandName == "OrderBottom")
        {
            Articles m_Articles = new Articles();
            m_Articles.Move(int.Parse(m_grid.DataKeys[index].Value.ToString()), short.Parse(ddlCategory.SelectedValue), "bottom");
            BindData();
        }
        else if (e.CommandName == "OrderUp")
        {
            Articles m_Articles = new Articles();
            m_Articles.Move(int.Parse(m_grid.DataKeys[index].Value.ToString()), short.Parse(ddlCategory.SelectedValue), "up");
            BindData();
        }
        else if (e.CommandName == "OrderDown")
        {
            Articles m_Articles = new Articles();
            m_Articles.Move(int.Parse(m_grid.DataKeys[index].Value.ToString()), short.Parse(ddlCategory.SelectedValue), "down");
            BindData();
        }
    }
}
