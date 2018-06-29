using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.admin.security;

public partial class Admin_Pages_lawsdocs_MessageSendRequests : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected byte RequestTypeIdReviewer = 2;
    protected List<Users> ListUsers = new List<Users>();
    protected List<RequestTypes> ListRequestTypes = new List<RequestTypes>();
    protected List<RequestStatus> ListRequestStatus = new List<RequestStatus>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        try
        {
            if (!IsPostBack || CustomPaging.ChangePage)
            {
                DropDownListHelpers.DDL_Bind(ddlUsers, new Users().GetList(), "...");
                DropDownListHelpers.DDL_Bind(ddlRequestStatus, RequestStatus.Static_GetList(), "...");
                DropDownListHelpers.DDL_Bind(ddlRequestTypes, RequestTypes.Static_GetList(), "...");
                DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("MessageSendRequests"), "");
                BindData();
            }
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);

        }
    }
    //--------------------------------------------------------------------------------

    private void BindData()
    {
        try
        {
            ListUsers = new Users().GetList();
            ListRequestTypes = RequestTypes.Static_GetList();
            ListRequestStatus = RequestStatus.Static_GetList();

            string dateFrom = txtDateFrom.Text, dateTo = txtDateTo.Text, orderBy = ddlOrderBy.SelectedValue, searchByDate = ddlSearchByDate.SelectedValue;
            int rowCount = 0, crUserId = int.Parse(ddlUsers.SelectedValue);
            byte requestTypeId = byte.Parse(ddlRequestTypes.SelectedValue), requestStatusId = byte.Parse(ddlRequestStatus.SelectedValue);
            List<MessageSendRequests> listMessageSendRequests =
                new MessageSendRequests
                {
                    RequestStatusId = requestStatusId,
                    RequestTypeId = requestTypeId,
                    CrUserId = crUserId
                }.GetPage(dateFrom, dateTo, orderBy, m_grid.PageSize, CustomPaging.PageIndex - 1,
                    ref rowCount, searchByDate);

            m_grid.DataSource = listMessageSendRequests;
            m_grid.DataBind();
            lblTong.Text = rowCount.ToString();
            CustomPaging.TotalPage = (rowCount == 0) ? 1 : (rowCount % m_grid.PageSize == 0) ? rowCount / m_grid.PageSize : (rowCount - rowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);

        }
    }
    // End Show Grid      

    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MessageSendRequests messageSendRequests = (MessageSendRequests)e.Row.DataItem;
                int messageSendRequestId = messageSendRequests.MessageSendRequestId;
                byte requestStatusId = messageSendRequests.RequestStatusId;
                LinkButton lbDelete = (LinkButton) e.Row.FindControl("lbDelete");
                Label lblXemThem = (Label)e.Row.FindControl("lblXemThem");
                Label lblTaoBanTin = (Label)e.Row.FindControl("lblTaoBanTin");
                LinkButton lbtMessageSendRequestsEdit = (LinkButton)e.Row.FindControl("lbtMessageSendRequestsEdit");
                LinkButton lbtReview = (LinkButton)e.Row.FindControl("lbtReview");
                LinkButton lbtDelete = (LinkButton)e.Row.FindControl("lbtDelete");
                if (lbDelete != null)
                {
                    lbDelete.OnClientClick = "return confirm('" +
                                             GetGlobalResourceObject("AdminResource", "DeleteConfirm").ToString() +
                                             "');";
                }

                lblXemThem.Text = string.Format(requestStatusId == RequestTypeIdReviewer ? "<a wheight='600' wwidth='1200' href='popupMessageSends.aspx?CampaignId={0}' class='popup2 docsproperties' title='Xem danh sách người nhận'>Xem</a>" : "<a wheight='600' wwidth='1200' href='CustomerFields_GetReportByRequestId.aspx?MessageSendRequestId={0}' class='popup2 docsproperties' title='Xem danh sách người nhận'>Xem</a>", messageSendRequestId);

                lblTaoBanTin.Text = messageSendRequests.GenStartTime != DateTime.MinValue ? string.Format("<p style='margin:0;font-weight:bold;'>Tạo bản tin:</p><p style='margin:0'>{0:dd/MM/yyyy HH:mm}</p>", messageSendRequests.GenStartTime) : "";
                lblTaoBanTin.Text += messageSendRequests.GenEndTime != DateTime.MinValue ? string.Format("<p style='margin:0;font-weight:bold;'>Hoàn thành:</p><p style='margin:0'>{0:dd/MM/yyyy HH:mm}</p>", messageSendRequests.GenEndTime) : "";
                if (messageSendRequests.RequestStatusId != RequestTypeIdReviewer)
                {
                    //Todo Edit Button
                    lbtMessageSendRequestsEdit.Attributes.Add("title", "Sửa");
                    lbtMessageSendRequestsEdit.Attributes.Add("class", "iconadmsua popup");
                    lbtMessageSendRequestsEdit.Attributes.Add("href", string.Concat("MessageSendRequestsEdit.aspx?id=", messageSendRequests.MessageSendRequestId));
                    lbtMessageSendRequestsEdit.Visible = true;
                    //Todo Review 
                    lbtReview.Attributes.Add("title", "Duyệt");
                    lbtReview.Attributes.Add("class", "duyettin");
                    lbtReview.Attributes.Add("style", "padding:12px");
                    lbtReview.CommandName = "Review";
                    lbtReview.CommandArgument = messageSendRequests.MessageSendRequestId.ToString();
                    lbtReview.OnClientClick = "return checkProcess();";
                    lbtReview.Visible = true;
                    //Todo Delete
                    lbtDelete.Attributes.Add("title", "Xóa");
                    lbtDelete.Attributes.Add("class", "iconadmxoa");
                    lbtDelete.CommandName = "Delete";
                    lbtDelete.OnClientClick = "return confirm('Bạn có chắc muốn xóa dữ liệu này?');";
                    lbtDelete.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(),
                ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
        }
    }


    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        short sysMessageId = 0;
        try
        {
            MessageSendRequests m_MessageSendRequests = new MessageSendRequests
            {
                MessageSendRequestId = System.Int32.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString())
            };
            m_MessageSendRequests.Delete(0,0, ref sysMessageId);
            JSAlertHelpers.ShowNotify("15", "success", "Xóa bản ghi thành công !", this);
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);

        }
        BindData();
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        BindData();
    }

    protected void btnSearch_Click(object sender, System.EventArgs e)
    {
        BindData();
    }

    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlRequestTypes_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlRequestStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void m_grid_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Review")
        {
            short sysMessageId = 0;
            try
            {
                int messageSendRequestId =
                    Convert.ToInt32(e.CommandArgument.ToString()); //m_grid.DataKeys[index].Value.ToString()
                new CustomerFields().CustomerFields_GenMessageByRequestId(messageSendRequestId);
                MessageSendRequests messageSendRequests =
                    new MessageSendRequests {MessageSendRequestId = messageSendRequestId}.Get();
                messageSendRequests.RequestStatusId = RequestTypeIdReviewer;
                messageSendRequests.InsertOrUpdate(0, ActUserId, ref sysMessageId);
                JSAlertHelpers.ShowNotify("15", "success", "Duyệt bản ghi thành công !", this);
                BindData();
            }
            catch (Exception ex)
            {
                sms.utils.Log.writeLog(ex.ToString(),
                    ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);

            }
        }
    }
}