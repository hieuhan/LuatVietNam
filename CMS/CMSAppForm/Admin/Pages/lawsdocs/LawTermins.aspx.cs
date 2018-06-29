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

public partial class Admin_LawTermins : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short LawTerminId = 0;
    protected byte LanguageId = 0;
    private List<ICSoft.CMSLib.Languages> l_Language = new List<ICSoft.CMSLib.Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<LawTermins> l_LawTermins = new List<LawTermins>();
    protected List<Users> l_Users = new List<Users>();    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("LawTermins"), "");
            DropDownListHelpers.DDLLawTerminGroup_BindByCulture(ddlLawTerminGroupID, LawTerminGroups.Static_GetList(), "...");
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
            l_ReviewStatus = ReviewStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            LawTermins m_LawTermins = new LawTermins();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_TermName=txtSearch.Text.Trim();
            byte m_ReviewStatus = byte.Parse(ddlReviewStatus.SelectedValue);
            byte m_LawTerminGroup = byte.Parse(ddlLawTerminGroupID.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            int m_LawTerminId=0;
            m_grid.DataSource = m_LawTermins.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_LawTerminId, m_TermName, m_ReviewStatus, m_LawTerminGroup, m_DateFrom, m_DateTo, ref RowCount);
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

            LawTerminId = short.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
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
        string deleteMessage = string.Empty;
        short SysMessageId = 0;
        try
        {
            Label lblLanguageId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblLanguageId");
            Label lblTermName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblTermName");
            if (lblLanguageId != null)
            {
                LawTermins m_LawTermins = new LawTermins();
                m_LawTermins.LanguageId = byte.Parse(lblLanguageId.Text);
                m_LawTermins.LawTerminId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_LawTermins.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1)//error
                {
                    deleteMessage = string.Format("Lỗi khi xóa Thuật ngữ pháp lý : {0}", SysMessages.Static_GetDesc(SysMessageId));
                }else if (SysMessageTypeId == 2)
                {
                    deleteMessage = string.Format("Xóa thuật ngữ pháp lý <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblTermName.Text) ? lblTermName.Text : "");
                }
                JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
        //ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: 'success', message: '" + deleteMessage + "' });", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
  
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlLawTerminGroupID_SelectedIndexChanged(object sender, EventArgs e)
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
            LawTermins m_LawTermins = new LawTermins();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_LawTermins.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_LawTermins.LawTerminId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_LawTermins.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
                messages += string.Format("Xóa thành công <i>{0}</i> {1}",countDeleteSuccess," thuật ngữ pháp lý.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> thuật ngữ pháp lý chưa được xóa.", countDeleteError);
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

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}