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
using sms.common;

public partial class Admin_Services : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short ServiceId = 0;
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users = new List<Users>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Services"), "");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_ReviewStatus = ReviewStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            Services m_Services = new Services();
            string m_ServiceName = txtSearch.Text.Trim();
            string m_ServiceCode = txtServiceCode.Text.Trim();
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Services.SiteId = short.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_Services.Services_GetList(ActUserId,ddlOrderBy.SelectedValue,m_ServiceName,m_ServiceCode, m_ReviewStatusId);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = m_grid.Rows.Count.ToString();
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
            Services m_DataItem = (Services)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            ServiceId = m_DataItem.ServiceId;          
        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string messages = string.Empty;
        try
        {
            Label lblServiceName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblServiceName");
            Services m_Services = new Services();
            m_Services.ServiceId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Services.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1) //error
            {
                messages = string.Format("Lỗi khi xóa dịch vụ : {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2) // success
            {
                messages = string.Format("Xóa dịch vụ <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblServiceName.Text) ? lblServiceName.Text : string.Empty); ;
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
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
            Services m_Services = new Services();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Services.ServiceId = byte.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Services.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
            }
            if (countDeleteSuccess > 0)
            {
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " dịch vụ.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> dịch vụ chưa được xóa.", countDeleteError);
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
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
        BindData();
    }

    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);
        BindData();
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
        BindData();
    }
    
    private void Review_Click(byte ReviewStatusId)
    {
        short SysMessageId = 0;
        int countReviewSuccess = 0;
        string messages = string.Empty;
        try
        {
            Services m_Services = new Services();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {

                        short ServiceId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Services.ServiceId = ServiceId;
                        m_Services.ReviewStatusId = ReviewStatusId;
                        m_Services.UpdateReviewStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        countReviewSuccess++;
                    }
                }
            }

            messages = string.Format("{0} thành công <i>{1}</i> {2}", (ReviewStatusId == ConstantHelpers.REVIEW_STATUS_REVIEW ? "Duyệt" : "Bỏ duyệt"), countReviewSuccess, " Dịch vụ.");
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}

