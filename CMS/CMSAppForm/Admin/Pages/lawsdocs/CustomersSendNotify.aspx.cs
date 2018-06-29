using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
using System.Net;
using System.Text;
using System.IO;

public partial class Admin_CustomersSendNotify : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int CustomerId = 0;
    protected List<Status> l_Status = new List<Status>();
    protected List<CustomerGroups> l_CustomerGroups = new List<CustomerGroups>();
    protected List<Customers> l_Customers = new List<Customers>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (rblSendType.SelectedValue == "0")
            ListCustomerPannel.Visible = false;
        else
            ListCustomerPannel.Visible = true;
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
            DropDownListHelpers.DDLCustomerGroups_BindByCulture(ddlCustomerGroups, CustomerGroups.Static_GetList(), "...");
            DropDownListHelpers.DDL_SetSelected(ddlCustomerGroups, "3");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlStatus, Status.Static_GetListOrderBy("StatusName"), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Customers"), "");
            BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }
    private void BindData2()
    {
        try
        {
            if (CustomerId > 0)
            {
                Customers m_Customers = new Customers();
                m_Customers = m_Customers.Get(CustomerId);
                if (m_Customers.CustomerId > 0)
                {

                }
                else
                {
                    ClearForm();
                }
            }

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private void ClearForm()
    {
        txtTitle.Text = "";
        txtContent.Text = "";
    }
    private List<Customers> BindDataCustomer(int PageIndex,out int TotalPage)
    {
        List<Customers> RetVal = new List<Customers>();
        TotalPage = 0;
        try
        {
           
            
            Customers m_Customers = new Customers();
            //string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_CustomerName = "";
            string m_CustomerMail = "";
            string m_CustomerFullName = "";
            string m_CustomerMobile = "";
            switch (rblFindTypes.SelectedValue)
            {
                case "CustomerName":
                    m_CustomerName = txtSearch.Text.Trim();
                    break;
                case "CustomerMail":
                    m_CustomerMail = txtSearch.Text.Trim();
                    break;
                case "CustomerFullName":
                    m_CustomerFullName = txtSearch.Text.Trim();
                    break;
                default:
                    m_CustomerMobile = txtSearch.Text.Trim();
                    break;
            }
            string m_CustomerNickName = "";
            short m_ServiceId = 0;// short.Parse(ddlServices.SelectedValue);
            byte m_StatusId = byte.Parse(ddlStatus.SelectedValue);
            byte m_GenderId = 0;
            short m_ProvinceId = 0;
            int m_BusinessApplicationPlatformId = 0;
            short m_ApplicationId = short.Parse(ddlServices.SelectedValue);
            short m_PlatformId = 0;
            short m_BusinessPartnerId = 0;
            short m_AppPlatformId = 0;
            int m_MapCustomerId = 0;
            short m_CustomerGroupId = short.Parse(ddlCustomerGroups.SelectedValue);
            short m_OccupationId = 0;
            short m_InfoChannelId = 0;
            byte m_RegisterNewsletter = 0;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            string OrderBy = "";

            RetVal = m_Customers.GetPage(ActUserId, 1000, PageIndex, OrderBy, m_CustomerName, m_CustomerFullName, m_CustomerNickName,
                                                    m_CustomerMail, m_ServiceId, m_StatusId, m_CustomerMobile, m_GenderId, m_ProvinceId, m_BusinessApplicationPlatformId,
                                                    m_ApplicationId, m_PlatformId, m_BusinessPartnerId, m_AppPlatformId, m_MapCustomerId, m_CustomerGroupId, m_OccupationId,0,
                                                    m_InfoChannelId, m_RegisterNewsletter,"", m_DateFrom, m_DateTo, ref TotalPage);

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
        TotalPage = TotalPage / 1000;
        return RetVal;
    }
    private void BindData()
    {
        try
        {
            int RowCount = 0;
            l_Status = Status.Static_GetList();
            l_CustomerGroups = CustomerGroups.Static_GetList();
            Customers m_Customers = new Customers();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_CustomerName = "";
            string m_CustomerMail = "";
            string m_CustomerFullName = "";
            string m_CustomerMobile = "";
            switch (rblFindTypes.SelectedValue)
            {
                case "CustomerName":
                    m_CustomerName = txtSearch.Text.Trim();
                    break;
                case "CustomerMail":
                    m_CustomerMail = txtSearch.Text.Trim();
                    break;
                case "CustomerFullName":
                    m_CustomerFullName = txtSearch.Text.Trim();
                    break;
                default:
                    m_CustomerMobile = txtSearch.Text.Trim();
                    break;
            }
            string m_CustomerNickName = "";
            short m_ServiceId = short.Parse(ddlServices.SelectedValue);
            byte m_StatusId = byte.Parse(ddlStatus.SelectedValue);
            byte m_GenderId = 0;
            short m_ProvinceId = 0;
            int m_BusinessApplicationPlatformId = 0;
            short m_ApplicationId = 0;
            short m_PlatformId = 0;
            short m_BusinessPartnerId = 0;
            short m_AppPlatformId = 0;
            int m_MapCustomerId = 0;
            short m_CustomerGroupId = short.Parse(ddlCustomerGroups.SelectedValue);
            short m_OccupationId = 0;
            short m_InfoChannelId = 0;
            byte m_RegisterNewsletter = 0;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_Customers.SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_Customers.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerName,m_CustomerFullName, m_CustomerNickName, 
                                                    m_CustomerMail, m_ServiceId, m_StatusId, m_CustomerMobile, m_GenderId, m_ProvinceId, m_BusinessApplicationPlatformId, 
                                                    m_ApplicationId,m_PlatformId, m_BusinessPartnerId, m_AppPlatformId, m_MapCustomerId, m_CustomerGroupId, m_OccupationId,0, 
                                                    m_InfoChannelId, m_RegisterNewsletter,"", m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();

        //JSAlertHelpers.close(this);
        //string script = @"<script language='JavaScript'>" +
        //    "window.parent.jQuery('#divEdit').dialog('close');" +
        //    "</script>";
        //ClientScriptManager csm = this.ClientScript;
        //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Customers.aspx");
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        SaveData();
        ClearForm();
    }
    public string SendNotification(string UserId,string title, string message)
    {
        string SERVER_API_KEY = "AIzaSyBLGk82_xlkL_z4VT58OXaBDbRNhWAT22M";
        var SENDER_ID = "135676527840";
        var value = message;
        WebRequest tRequest;
        string pustServerUrl = "https://android.googleapis.com/gcm/send";
       
        tRequest = WebRequest.Create(pustServerUrl);
        tRequest.Method = "post";
        tRequest.ContentType = "application/json";
        tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
        //tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

        string postData = "{\"registration_ids\":[" + UserId + "],\"notification\" : { \"body\" : \"" + message + "\", \"title\" : \"" + title + "\", \"icon\" : \"/Notify/images/icon.png\", \"tag\" : \"thongbao\" },\"data\" : { \"body\" : \"" + message + "\", \"title\" : \"alodoctor.vn thông báo\", \"icon\" : \"/Notify/images/icon.png\", \"tag\" : \"thongbao\" }}";
        //postData = "{\"to\": " + UserId + ",\"notification\": {\"body\": \"Exciting match!\",\"title\": \"India vs. Pakistan\",\"click_action\": \"com.apurv.fcmtest.OPEN_ACTIVITY_1\"},\"data\": {\"custom_key\": \"custom_value\",\"Match\": \"IndiaVSPakistan\"}} ";
        Response.Write(postData);
        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;

        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        WebResponse tResponse = tRequest.GetResponse();

        dataStream = tResponse.GetResponseStream();

        StreamReader tReader = new StreamReader(dataStream);

        String sResponseFromServer = tReader.ReadToEnd();


        tReader.Close();
        dataStream.Close();
        tResponse.Close();
        return sResponseFromServer;
    }
    public string SendNotification(List<Customers> l_CustomersSend)
    {
        string RetVal = "";
        
        return RetVal;
    }
    protected void SaveData()
    {
        try
        {
            short SysMessageId = 0;
            byte Replicated = 0;
            int TotalPage = 0;

            if (rblSendType.Text == "0")
            {
                l_Customers = BindDataCustomer(0, out TotalPage);
                for(int PageIndex = 1; PageIndex < TotalPage; PageIndex++)
                {

                }
            }
            else if (rblSendType.Text == "1")
            {
                
                foreach (Customers m_Customers in l_Customers)
                {
                    

                }
            }
            else if (rblSendType.Text == "2")
            {
                foreach (GridViewRow m_Row in m_grid.Rows)
                {
                    CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                    if (chkAction != null)
                    {
                        if (chkAction.Checked)
                        {
                            
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    //-------------------------------------------------------------------
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Customers m_DataItem = (Customers)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            CustomerId = m_DataItem.CustomerId;          
        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Customers m_Customers = new Customers();
            m_Customers.CustomerId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Customers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Customers m_Customers = new Customers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Customers.CustomerId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Customers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlCustomerGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_PENDING);
        BindData();
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
        BindData();
    }

    private void Review_Click(byte StatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Customers m_Customers = new Customers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Customers.CustomerId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());                        
                        m_Customers.StatusId = StatusId;
                        SysMessageTypeId = m_Customers.UpdateStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        DropDownListHelpers.DDLServices_BindByCulture(ddlServices, Services.Static_GetList(ActUserId, short.Parse(ddlSite.SelectedValue)), "...");
        BindData();
    }
    protected void rblSendType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

