using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using ICSoft.HelperLib;
using sms.admin.security;
using sms.common;

public partial class Admin_NewsletterMembers : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int NewsletterId = 0;
    protected List<NewsletterMemberStatus> l_NewsletterMemberStatus = new List<NewsletterMemberStatus>();
    protected List<NewsletterGroups> l_NewsletterGroups = new List<NewsletterGroups>();
    protected List<NewsletterMembers> l_NewsletterMembers = new List<NewsletterMembers>();
    protected List<Users> l_Users = new List<Users>();
    public short myGroupId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            List<NewsletterGroups> listNewsletterGroups = NewsletterGroups.Static_GetList();
            NewsletterGroups mNewsletterGroupsWeb = new NewsletterGroups();
            mNewsletterGroupsWeb.NewsletterGroupId = 8;
            mNewsletterGroupsWeb.NewsletterGroupDesc = "ĐK qua Web";
            listNewsletterGroups.Add(mNewsletterGroupsWeb);
            DropDownListHelpers.DDLNewsletterGroups_BindByCulture(ddlNewsletterGroups, listNewsletterGroups, "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("NewsletterMembers"), "");
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
            l_NewsletterMemberStatus = NewsletterMemberStatus.Static_GetList();
            l_NewsletterGroups = NewsletterGroups.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            int m_NewsletterMemberStatusId = int.Parse(rbtIsReceiveNews.SelectedValue);
            NewsletterMembers m_NewsletterMembers = new NewsletterMembers();
            int m_CustomerId = 0;
            string m_OrderBy = ddlOrderBy.SelectedValue;
            short m_NewsletterGroupId = short.Parse(ddlNewsletterGroups.SelectedValue);
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            string m_Email = txtEmail.Text;
            if(m_NewsletterGroupId==8)
            {
                myGroupId = 8;
                if (m_NewsletterMemberStatusId ==0)
                {
                    m_NewsletterMemberStatusId--;
                }
                else if (m_NewsletterMemberStatusId == 2)
                {
                    m_NewsletterMemberStatusId =0;
                }
                m_NewsletterGroupId = 0;
                NewsletterEmails m_NewsletterEmails = new NewsletterEmails();
                m_NewsletterEmails.SiteId = short.Parse(ddlSite.SelectedValue);
                List<NewsletterMembers> listNewsletterMembers = new List<NewsletterMembers>();
                m_OrderBy = "CrDateTime Desc";
                List<NewsletterEmails> listdata = m_NewsletterEmails.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_NewsletterGroupId, m_CustomerId, m_Email, m_NewsletterMemberStatusId, m_DateFrom, m_DateTo, ref RowCount);
                foreach(NewsletterEmails mNewsletterEmails in listdata)
                {
                    NewsletterMembers mNewsletterMembers = new NewsletterMembers();
                    mNewsletterMembers.NewsletterMemberId = mNewsletterEmails.NewsletterEmailId;
                    mNewsletterMembers.CustomerId = mNewsletterEmails.CustomerId;
                    mNewsletterMembers.CustomerName = mNewsletterEmails.CustomerName;
                    mNewsletterMembers.Email = mNewsletterEmails.Email;
                    mNewsletterMembers.NewsletterGroupId = 0;
                    mNewsletterMembers.NewsletterMemberStatusId = (byte)(mNewsletterEmails.IsReceiveNews == 0 ? 2 : mNewsletterEmails.IsReceiveNews);
                    mNewsletterMembers.CrDateTime = mNewsletterEmails.CrDateTime;
                    mNewsletterMembers.CrUserId =0;
                    listNewsletterMembers.Add(mNewsletterMembers);
                }
                m_grid.DataSource = listNewsletterMembers;
                m_grid.DataBind();
            }
            else
            {
                myGroupId = 0;
                m_grid.DataSource = m_NewsletterMembers.GetPageWithStatus(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_CustomerId, m_Email, m_NewsletterGroupId, m_NewsletterMemberStatusId, m_DateFrom, m_DateTo, ref RowCount);
                m_grid.DataBind();
            }
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

            NewsletterId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            myGroupId = short.Parse(ddlNewsletterGroups.SelectedValue);
            if(myGroupId == 8)
            {
                int CountFail = 0;
                int CountSuc = 0;
                NewsletterEmails m_NewsletterEmails = new NewsletterEmails();
                m_NewsletterEmails.NewsletterEmailId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_NewsletterEmails.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1) CountFail++;
                else CountSuc++;
                deleteMessage = "Xóa thành công " + CountSuc.ToString() + " bản ghi và thất bại " + CountFail.ToString() + " bản ghi";
            }
            else
            {
                Label lblEmail = (Label)m_grid.Rows[e.RowIndex].FindControl("lblEmail");
                NewsletterMembers m_NewsletterMembers = new NewsletterMembers();
                m_NewsletterMembers.NewsletterMemberId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_NewsletterMembers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1)//error
                {
                    deleteMessage = string.Format("Lỗi khi xóa người dùng đăng ký nhận bản tin : {0}", SysMessages.Static_GetDesc(SysMessageId));
                }
                else if (SysMessageTypeId == 2)
                {
                    deleteMessage = string.Format("Xóa người dùng đăng ký nhận bản tin : <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblEmail.Text) ? lblEmail.Text : "");
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
        //ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: 'success', message: '" + deleteMessage + "' });", true);
        BindData();
    }
    protected void rbtIsReceiveNews_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            BindData();
        }
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        try
        {
            myGroupId = short.Parse(ddlNewsletterGroups.SelectedValue);
            NewsletterMembers m_NewsletterMembers = new NewsletterMembers();
            NewsletterEmails m_NewsletterEmails = new NewsletterEmails();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        if (myGroupId == 8)
                        {
                            m_NewsletterEmails.NewsletterEmailId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                            SysMessageTypeId = m_NewsletterEmails.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        }
                        else
                        {
                            m_NewsletterMembers.NewsletterMemberId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                            SysMessageTypeId = m_NewsletterMembers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                                
                        }
                        if (SysMessageTypeId == 1) //error
                        {
                            countDeleteError++;
                        }
                        else if (SysMessageTypeId == 2) //success
                        {
                            countDeleteSuccess++;
                        }
                    }
                }
                
                if (countDeleteSuccess > 0)
                {
                    messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " người dùng đăng ký nhận bản tin.");
                }
                if (countDeleteError > 0)
                {
                    messages += string.Format("<i>{0}</i> người dùng đăng ký nhận bản tin chưa được xóa.", countDeleteError);
                }
            }
            
           
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
            //ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: 'success', message: '" + messages + "' });", true);
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
    protected void ddlNewsletterGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        myGroupId = short.Parse(ddlNewsletterGroups.SelectedValue);
        CustomPaging.PageIndex = 1;
        NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
        List<NewsletterGroups> listNewsletterGroups = m_NewsletterGroups.NewsletterGroups_GetList(ActUserId, "", "", short.Parse(ddlSite.SelectedValue));
        NewsletterGroups mNewsletterGroupsWeb = new NewsletterGroups();
        mNewsletterGroupsWeb.NewsletterGroupId = 8;
        mNewsletterGroupsWeb.NewsletterGroupDesc = "ĐK qua Web";
        listNewsletterGroups.Add(mNewsletterGroupsWeb);
        DropDownListHelpers.DDLNewsletterGroups_BindByCulture(ddlNewsletterGroups,listNewsletterGroups, "...");
        if (myGroupId==8)
        {
            ddlNewsletterGroups.SelectedIndex = listNewsletterGroups.Count;
        }
        BindData();
    }
}