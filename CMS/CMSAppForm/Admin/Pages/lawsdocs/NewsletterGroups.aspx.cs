﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_NewsletterGroups : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short NewsletterGroupId = 0;
    //protected byte LanguageId = 0;
    protected List<Users> l_Users = new List<Users>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("NewsletterGroups"), "");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_NewsletterGroupName = txtSearch.Text.Trim();
            m_grid.DataSource = m_NewsletterGroups.NewsletterGroups_GetList(ActUserId, m_OrderBy, m_NewsletterGroupName, short.Parse(ddlSite.SelectedValue));
            m_grid.DataBind();
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
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }

            NewsletterGroupId = byte.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());
           
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
            //if (lblLanguageId != null)
            //{
                NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
                Label lblTermName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblGridColumnName");
                m_NewsletterGroups.NewsletterGroupId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_NewsletterGroups.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (SysMessageTypeId == 1)//error
                {
                    deleteMessage = string.Format("Lỗi khi xóa nhóm bản tin : {0}", sms.common.SysMessages.Static_GetDesc(SysMessageId));
                }
                else if (SysMessageTypeId == 0)
                {
                    deleteMessage = string.Format("Xóa nhóm bản tin <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblTermName.Text) ? lblTermName.Text : "");
                }
                JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);

            //}
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
            NewsletterGroups m_NewsletterGroups = new NewsletterGroups();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_NewsletterGroups.NewsletterGroupId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_NewsletterGroups.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) //error
                        {
                            countDeleteError++;
                        }
                        else if (SysMessageTypeId == 0) //success
                        {
                            countDeleteSuccess++;
                        }
                    }
                }
            }
            if (countDeleteSuccess > 0)
            {
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " nhóm bản tin.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> loại nhóm bản tin chưa được xóa.", countDeleteError);
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
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}