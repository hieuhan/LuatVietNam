using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_ArticleDisplaysBTHL : System.Web.UI.Page
{
    protected int EditIndex = -1;
    protected int ActUserId = 0;
    protected int DocDisplayId = 0;
    protected int ArticleId = 0;
    protected short DisplayTypeId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<DisplayTypes> l_DisplayTypes = new List<DisplayTypes>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
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
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", AppTypeId.ToString());
            DropDownListHelpers.DDLDisplayTypes_BindByCulture(ddlDisplayTypes, DisplayTypes.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_ARTICLES_EFFECT"])), "");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("ArticleDisplays"), "");
            BindData(-1);
        }
        else if (CustomPaging.ChangePage)
        {
            BindData(-1);
        }
    }

    private void BindData(int index)
    {
        try
        {
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            l_Language = Languages.Static_GetList();

            Articles m_Articles = new Articles();
            string OrderBy = ddlOrderBy.SelectedValue;
            short CategoryId = 0;
            byte ReviewStatusId = 0;
            int RowAmount = m_grid.PageSize;
            int PageIndex = CustomPaging.PageIndex - 1;
            short DataSourceId = 0;
            short TagId = 0;
            short EventStreamId = 0;
            short DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
            string Title = "";
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            ArticleId = 0;
            m_grid.EditIndex = index;
            m_grid.DataSource = m_Articles.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, AppTypeId, ArticleId, Title, DataSourceId, ReviewStatusId, CategoryId, TagId, EventStreamId, DisplayTypeId, DateFrom, DateTo, ref RowCount);
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
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            short SysMessageId=0;
            Label lblArticleId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblArticleId");
            Label lblLanguageId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblLanguageId");
            Label lblAppTypeId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblAppTypeId");
            ArticleDisplays m_ArticleDisplays = new ArticleDisplays();
            m_ArticleDisplays.ArticleId = int.Parse(lblArticleId.Text);
            m_ArticleDisplays.DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
            m_ArticleDisplays.LanguageId = byte.Parse(lblLanguageId.Text);
            m_ArticleDisplays.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
            m_ArticleDisplays.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData(-1);
    }

    //--------------------------------------------------------
    protected void m_grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        EditIndex = e.NewEditIndex;
        BindData(EditIndex);
    }
    //--------------------------------------------------------
    protected void m_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
       BindData(-1);
    }
    //--------------------------------------------------------
    protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            short SysMessageId=0;
            int id = e.RowIndex;
            m_grid.EditIndex = id;
            GridViewRow row = m_grid.Rows[id];
            int updateId = Int32.Parse(m_grid.DataKeys[id].Value.ToString());
            if (updateId > 0)
            {
                ArticleDisplays m_ArticleDisplays = new ArticleDisplays();
                m_ArticleDisplays.ArticleId = updateId;
                m_ArticleDisplays.DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
                m_ArticleDisplays.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
                m_ArticleDisplays.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                m_ArticleDisplays.DisplayOrder = int.Parse(((TextBox)row.FindControl("txtDisplayOrder")).Text);
                m_ArticleDisplays.UpdateDisplayOrder(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
        BindData(-1);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      CustomPaging.PageIndex = 1;
      BindData(-1);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            ArticleDisplays m_ArticleDisplays = new ArticleDisplays();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                Label lblArticleId = (Label)m_Row.FindControl("lblArticleId");
                Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                Label lblAppTypeId = (Label)m_Row.FindControl("lblAppTypeId");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_ArticleDisplays.ArticleId = int.Parse(lblArticleId.Text);
                        m_ArticleDisplays.DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
                        m_ArticleDisplays.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_ArticleDisplays.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                        m_ArticleDisplays.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData(-1);
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData(-1);
    }
    protected void ddlOrganTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData(-1);
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData(-1);
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData(-1);
    }
}

