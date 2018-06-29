using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class Admin_Pages_lawsdocs_MessageSendRequestsEdit : System.Web.UI.Page
{
    private int ActUserId = 0, MessageSendRequestId = 0;
    protected byte RequestTypeIdReviewer = 2;
    protected int docId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (Request.Params["id"] != null && Request.Params["id"].ToString() != "")
            {
                MessageSendRequestId = int.Parse(Request.Params["id"].ToString());
            }
            if (!IsPostBack)
            {
                DropDownListHelpers.BindDropDownList(ddlRequestTypes, RequestTypes.Static_GetList());
                //DropDownListHelpers.BindDropDownList(ddlRequestStatus, RequestStatus.Static_GetList());
                BindData();
                CheckRequestTypes();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    //--------------------------------------------------------------------------------
    private void BindData(bool setValueRequestTypes = true)
    {
        if (MessageSendRequestId > 0)
        {
            MessageSendRequests m_MessageSendRequests =
                new MessageSendRequests {MessageSendRequestId = MessageSendRequestId}.Get();
            if (m_MessageSendRequests.MessageSendRequestId > 0)
            {
                if (m_MessageSendRequests.RequestStatusId == RequestTypeIdReviewer)
                {
                    JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa bản ghi khi đã duyệt.", this);
                    btnSave.Enabled = false;
                    btnSave.ToolTip = @"Không thể sửa bản ghi khi đã duyệt ";
                    ddlRequestTypes.Enabled = false;
                    txtBeginDateTime.Enabled = false;
                    txtEndDateTime.Enabled = false;
                    txtEventName.Enabled = false;
                    return;
                }
                if (setValueRequestTypes)
                DropDownListHelpers.DDL_SetSelected(ddlRequestTypes, m_MessageSendRequests.RequestTypeId.ToString());
                txtBeginDateTime.Text = m_MessageSendRequests.BeginDateTime != DateTime.MinValue
                    ? m_MessageSendRequests.BeginDateTime.ToString("dd-MM-yyyy HH:mm")
                    : string.Empty;
                txtEndDateTime.Text = m_MessageSendRequests.EndDateTime != DateTime.MinValue
                    ? m_MessageSendRequests.EndDateTime.ToString("dd-MM-yyyy HH:mm")
                    : string.Empty;
                if (m_MessageSendRequests.DocId > 0)
                {
                    var m_Docs = new Docs().Get(m_MessageSendRequests.DocId, 1);
                    if (m_Docs.DocId > 0)
                    {
                        txtDocName.Text = m_Docs.DocName;
                        txtDocId.Value = m_Docs.DocId.ToString();
                        txtEventName.Text = string.IsNullOrEmpty(m_MessageSendRequests.EventName) ? m_Docs.Result : m_MessageSendRequests.EventName;
                        docId = m_MessageSendRequests.DocId;
                    }
                }
            }
        }
        else
        {
            var currentDate = DateTime.Now;
            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0);
            txtBeginDateTime.Text = currentDate.ToString("dd-MM-yyyy HH:mm");
            txtEndDateTime.Text = ddlRequestTypes.SelectedValue.Equals("3") ? currentDate.AddDays(7).AddHours(23).AddMinutes(59).ToString("dd-MM-yyyy HH:mm") : currentDate.AddHours(23).AddMinutes(59).ToString("dd-MM-yyyy HH:mm");
        }
    }

    private void ResetForm()
    {
        //ddlRequestStatus.SelectedIndex = 0;
        ddlRequestTypes.SelectedIndex = 0;
        txtBeginDateTime.Text = "";
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        short SysMessageId = 0;
        try
        {
            new MessageSendRequests
            {
                MessageSendRequestId = MessageSendRequestId,
                RequestTypeId = byte.Parse(ddlRequestTypes.SelectedValue),
                RequestStatusId = 1,//chờ duyệt //byte.Parse(ddlRequestStatus.SelectedValue),
                EventName = txtEventName.Text.Trim(),
                DocId = int.Parse(txtDocId.Value),
                BeginDateTime = txtBeginDateTime.Text != "" ? DateTime.ParseExact(txtBeginDateTime.Text,
                    "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture) : DateTime.MinValue,
                EndDateTime = txtEndDateTime.Text != "" ? DateTime.ParseExact(txtEndDateTime.Text,
                    "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture) : DateTime.MinValue,
                CrUserId = ActUserId
            }.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            JSAlertHelpers.ShowNotifyOtherPage("15", "success", string.Format("{0} bản ghi thành công.", MessageSendRequestId == 0 ? "Thêm mới" : "Cập nhật"), this);
            StringBuilder csText = new StringBuilder();
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            csText.Clear();
            csText.Append("<script type=\"text/javascript\">");
            csText.Append("window.parent.jQuery('#divEdit').dialog('close');");
            csText.Append("</script>");
            cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "system_message", csText.ToString());
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }

    protected void ddlRequestTypes_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(false);
        CheckRequestTypes();
    }

    private void CheckRequestTypes()
    {
        rfvtxtDocName.Enabled = trDocName.Visible = trEvent.Visible = SelectDoc.Visible = ddlRequestTypes.SelectedValue == RequestTypeIdReviewer.ToString();
        if (ddlRequestTypes.SelectedValue != RequestTypeIdReviewer.ToString())
        {
            rfvtxtDocName.Text = "";
            txtDocId.Value = "0";
        }
    }
}