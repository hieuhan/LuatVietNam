using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_EventStreams : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int EventStreamId = 0;
    protected short SiteId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users = new List<Users>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "");
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "");
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("EventStreams"), "");
        if (Session["Event-ddlLanguage"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("EventStreamsEdit.aspx"))
        {
            ddlLanguage.SelectedValue = Session["Event-ddlLanguage"].ToString();
            ddlParentCategory.SelectedValue = Session["Event-ddlCategory"].ToString();
            ddlOrderBy.SelectedValue = Session["Event-ddlOrderBy"].ToString();
            ddlSite.SelectedValue = Session["Event-ddlSite"].ToString();
            txtDateFrom.Text = Session["Event-DateFrom"].ToString();
            txtDateFrom.Text = Session["Event-DateFrom"].ToString();
            txtDateFrom.Text = Session["Event-Key"].ToString();
            ddlReviewStatus.SelectedValue = Session["Event-ddlReviewStatus"].ToString();
        }

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
            l_Language = Languages.Static_GetList();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            SiteId = short.Parse(ddlSite.SelectedValue);

            EventStreams m_EventStreams = new EventStreams();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            short m_CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            string m_EventStreamName = txtContentSearch.Text.Trim();
            string DateFrom = txtDateFrom.Text.Trim();
            string DateTo = txtDateTo.Text.Trim();
            m_EventStreams.SiteId = SiteId;

            m_grid.DataSource = m_EventStreams.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, AppTypeId, EventStreamId, m_CategoryId, m_EventStreamName, m_ReviewStatusId, DateFrom, DateTo, ref RowCount);
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            SetCurentData();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void SetCurentData()
    {
        Session["Event-ddlLanguage"] = ddlLanguage.SelectedValue;
        Session["Event-ddlCategory"] = ddlParentCategory.SelectedValue;
        Session["Event-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["Event-ddlSite"] = ddlSite.SelectedValue;
        Session["Event-DateFrom"] = txtDateFrom.Text;
        Session["Event-DateFrom"] = txtDateTo.Text;
        Session["Event-Key"] = txtContentSearch.Text;
        Session["Event-ddlReviewStatus"] = ddlReviewStatus.SelectedValue;

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

            EventStreamId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
            Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
            if (rptLanguage != null)
            {
                rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                rptLanguage.DataBind();
            }
          
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
                EventStreams m_EventStreams = new EventStreams();
                m_EventStreams.LanguageId = byte.Parse(lblLanguageId.Text);
                m_EventStreams.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                m_EventStreams.EventStreamId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_EventStreams.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageId > 0)
                {
                    Message = "Xóa sự kiện thành công";
                }
                else if(m_EventStreams.EventStreamId>0)
                    Message = "Xóa sự kiện thất bại";
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }

        JSAlertHelpers.ShowNotify("15", "success", Message, this);
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
        string Message = "";
        int CountSuc = 0;
        try
        {
            EventStreams m_EventStreams = new EventStreams();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                        Label lblAppTypeId = (Label)m_Row.FindControl("lblAppTypeId");
                        m_EventStreams.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_EventStreams.ApplicationTypeId = byte.Parse(lblAppTypeId.Text);
                        m_EventStreams.EventStreamId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_EventStreams.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if(m_EventStreams.EventStreamId>0) CountSuc++;
                    }
                }
            }
            Message = "Xóa thành công " + CountSuc.ToString() + " sự kiện. ";
            //lblMsg.Text = Message;
            JSAlertHelpers.ShowNotify("15", "success", Message, this);
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
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
        CustomPaging.PageIndex = 1;
        BindData();
    }
}