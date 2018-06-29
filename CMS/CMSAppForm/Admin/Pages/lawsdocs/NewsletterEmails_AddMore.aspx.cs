using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

using sms.admin.security;

public partial class Admin_NewsletterEmails_AddMore : System.Web.UI.Page
{
    protected int CustomerId = 0;
    private int ActUserId = 0;
    
    protected short ServiceId = 0;
    protected List<Users> l_Users = new List<Users>();
    private List<CustomerServices> l_CustomerServices = new List<CustomerServices>();
    protected Customers mCustomers = new Customers();
    protected List<NewsletterMemberStatus> l_NewsletterMemberStatus = new List<NewsletterMemberStatus>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CustomerId"] != null && Request.Params["CustomerId"].ToString() != "")
        {
            CustomerId = Int32.Parse(Request.Params["CustomerId"].ToString());
        }
        if (CustomerId > 0) {
            l_CustomerServices = new CustomerServices().CustomerServices_GetListByCustomerId(CustomerId);
            if (l_CustomerServices.Count > 0)
            {
                ServiceId = l_CustomerServices[0].ServiceId;
            }
            mCustomers = Customers.Static_Get(CustomerId);
        }
        if (!IsPostBack)
        {
            BindData();
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
            NewsletterMembers m_NewsletterMembers = new NewsletterMembers();
            l_NewsletterMemberStatus = NewsletterMemberStatus.Static_GetList();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            short m_NewsletterGroupId = 0;
            string m_Email = "";
            string m_DateFrom = "";
            string m_DateTo = "";
            m_grid.DataSource = m_NewsletterMembers.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, CustomerId, m_Email, m_NewsletterGroupId, m_DateFrom, m_DateTo, ref RowCount);
            
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

    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NewsletterMembers m_DataItem = (NewsletterMembers)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            CheckBox chkAction = (CheckBox)e.Row.FindControl("chkAction");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('Bạn có chắc muốn xóa Email này?');";
            }
            if (m_DataItem.Email.Contains(mCustomers.CustomerMail))
            {
                lbDelete.Visible = false;
                chkAction.Visible = false;
            }
        }
    }
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int CountFail = 0;
        int CountSuc = 0;
        try
        {
            NewsletterMembers m_NewsletterMembers = new NewsletterMembers();
            m_NewsletterMembers.NewsletterMemberId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_NewsletterMembers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            m_NewsletterMembers = m_NewsletterMembers.Get();
            if (m_NewsletterMembers.NewsletterMemberId > 0) CountFail++;
            else CountSuc++;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", "Xóa thành công " + CountSuc.ToString() + " bản ghi và thất bại " + CountFail.ToString() + " bản ghi", this);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
        int CountFalseByStatus = 0;
        try
        {
            NewsletterMembers m_NewsletterMembers = new NewsletterMembers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_NewsletterMembers.NewsletterMemberId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_NewsletterMembers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        m_NewsletterMembers = m_NewsletterMembers.Get();
                        if (m_NewsletterMembers.NewsletterMemberId > 0) CountFalseByStatus++;
                        else CountSuc++;
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
        JSAlertHelpers.ShowNotify("15", "success", "Xóa thành công " + CountSuc.ToString() + " bản ghi và thất bại " + CountFalseByStatus.ToString() + " bản ghi", this);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short countAdd = 0;
            string[] l_Email = txtEmail.Text.Trim().Split(';');
            if (l_Email.Count() > 0)
            {
                short NewsletterGroupId = 1;
                //if (ServiceId == 15 || ServiceId == 25)// Gói TA or NVTA
                //{
                //    NewsletterGroupId = 4;
                //}
                //else if (ServiceId == 17 || ServiceId == 26)// Gói TA or NVTA
                //{
                //    NewsletterGroupId = 5;
                //}
                //else if (ServiceId == 23 || ServiceId == 24)// Gói TA or NVTA
                //{
                //    NewsletterGroupId = 6;
                //}
                //else
                //{
                //    NewsletterGroupId = 1;
                //}
                foreach (string Email in l_Email)
                {
                    if (!string.IsNullOrEmpty(Email))
                    {
                        byte SysMessageTypeId = 0;
                        short SysMessageId = 0;
                        NewsletterMembers m_NewsletterMembers = new NewsletterMembers();
                        m_NewsletterMembers.NewsletterMemberId = 0;
                        m_NewsletterMembers.CustomerId = CustomerId;
                        m_NewsletterMembers.Email = Email.Trim();
                        m_NewsletterMembers.NewsletterMemberStatusId = 1;// Trạng thái nhận tin
                        m_NewsletterMembers.NewsletterGroupId = NewsletterGroupId;
                        SysMessageTypeId = m_NewsletterMembers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (m_NewsletterMembers.NewsletterMemberId > 0)
                        {
                            countAdd++;
                        }
                    }
                }
                BindData();
                txtEmail.Text = "";
                if (l_Email.Count() > 0 && l_Email.Count() > countAdd)
                {
                    JSAlertHelpers.ShowNotify("15", "success", "Thêm thành công " + countAdd.ToString() + " Email và có "+(l_Email.Count()-countAdd)+ " bị trùng. ", this);
                }
                JSAlertHelpers.ShowNotify("15", "success", "Thêm thành công " + countAdd.ToString() + " Email", this);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string script = @"<script language='JavaScript'>" +
            "window.parent.jQuery('#divEdit').dialog('close');" +
            "</script>";
        ClientScriptManager csm = this.ClientScript;
        csm.RegisterClientScriptBlock(this.GetType(), "close", script);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            BindData();
        }
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}   