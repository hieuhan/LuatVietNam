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

public partial class Admin_Signers : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SignerId = 0;
    //protected byte LanguageId = 0;
    //private List<ICSoft.CMSLib.Languages> l_Language = new List<ICSoft.CMSLib.Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<ICSoft.LawDocsLib.Organs> l_Organs = new List<ICSoft.LawDocsLib.Organs>();
    protected List<Regencies> l_Regencies = new List<Regencies>();
    protected List<Users> l_Users = new List<Users>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLRegencies_BindByCulture(ddlRegencies, Regencies.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Signers"), "");
            DropDownListHelpers.DDL_Bind(ddlOrgans,ICSoft.LawDocsLib.Organs.Static_GetList(),"...");
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
            l_ReviewStatus = ReviewStatus.Static_GetList();
            l_Organs = ICSoft.LawDocsLib.Organs.Static_GetList();
            l_Regencies = Regencies.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            Signers m_Signers = new Signers();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_SignerName=txtSearch.Text.Trim();
            byte m_ReviewStatus = byte.Parse(ddlReviewStatus.SelectedValue);
            byte m_RegencieId = byte.Parse(ddlRegencies.SelectedValue);
            short m_OrganId= short.Parse(ddlOrgans.SelectedValue);
            m_grid.DataSource = m_Signers.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_SignerName,m_RegencieId, m_OrganId,m_ReviewStatus , ref RowCount);
            m_grid.DataBind();
            lblTong.Text = string.Format("{0:#,#}",RowCount);
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

            SignerId = short.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
           
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            //Label lblLanguageId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblLanguageId");
            //Label lblAppTypeId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblAppTypeId");
            Label lblSignerName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblSignerName");
            //if (lblLanguageId != null)
            //{
                Signers m_Signers = new Signers();               
                m_Signers.SignerId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_Signers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1) //error
                {
                    deleteMessage =  string.Format("Lỗi khi xóa Người ký: {0}", SysMessages.Static_GetDesc(SysMessageId));
                }
                else if (SysMessageTypeId == 0 || SysMessageTypeId == 2) // success
                {
                    deleteMessage = string.Format("Xóa Người ký <i>{0}</i> thành công." , !string.IsNullOrEmpty(lblSignerName.Text) ? lblSignerName.Text : string.Empty);
                }
            //}
                JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
                //ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: '" + messageType + "', message: '" + deleteMessage + "' });", true);
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
        bool isSelected = false;
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        try
        {
            Signers m_Signers = new Signers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        isSelected = true;
                        m_Signers.SignerId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Signers.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        switch (SysMessageTypeId)
                        {
                            case 1:
                                countDeleteError++;
                                break;
                            case 2:
                            case 0:
                                countDeleteSuccess++;
                                break;
                        }
                    }
                }
            }
            if (isSelected)
            {
                if (countDeleteSuccess > 0)
                {
                    messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " Người ký.");
                }
                if (countDeleteError > 0)
                {
                    messages += string.Format("<i>{0}</i> Người ký chưa được xóa.", countDeleteError);
                }
                JSAlertHelpers.ShowNotify("15", "success", messages, this);
            }
            else
            {
                JSAlertHelpers.ShowNotify("15", "success", "Bạn chưa chọn Người ký cần xóa.", this);
            }

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
    protected void ddlOrgans_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlRegencies_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
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
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        bool isSelected = false;
        try
        {
            Signers m_Signers = new Signers();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        isSelected = true;
                        short SignerId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Signers = m_Signers.Get(SignerId);
                        m_Signers.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_Signers.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
            if (isSelected)
            {
                if (countDeleteSuccess > 0)
                {
                    messages += string.Format("{0} thành công <i>{1}</i> {2}",
                        (ReviewStatusId == ConstantHelpers.REVIEW_STATUS_REVIEW ? "Duyệt" : "Bỏ duyệt"),
                        countDeleteSuccess, " Người ký.");
                }
                if (countDeleteError > 0)
                {
                    messages += string.Format("<i>{0}</i> Người ký chưa được {1}.", countDeleteError,
                        (ReviewStatusId == ConstantHelpers.REVIEW_STATUS_REVIEW ? "duyệt" : "bỏ duyệt"));
                }
                JSAlertHelpers.ShowNotify("15", "success", messages, this);
            }
            else
            {
                JSAlertHelpers.ShowNotify("15", "success",
                    string.Format("Bạn chưa chọn Người ký cần {0} ",
                        (ReviewStatusId == ConstantHelpers.REVIEW_STATUS_REVIEW ? "duyệt" : "bỏ duyệt")), this);
            }
            //ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: 'success', message: '" + messages + "' });", true);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
}