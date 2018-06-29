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

public partial class Admin_RelateTypes : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected byte RelateTypeId = 0;
    protected byte LanguageId = 0;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<RelateTypeGroups> l_RelateTypeGroups = new List<RelateTypeGroups>();
    protected List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
    protected List<RelateTypeWithDefaultLanguage> l_RelateTypeWithDefaultLanguage = new List<RelateTypeWithDefaultLanguage>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("RelateTypes"), "");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLDocGroups_BindByCulture(ddlDocGroups, DocGroups.GetList(), "...");
            
            BindData();
        }
        if (ddlLanguage.SelectedValue == "1")
        {
            chkRelateTypeWithDefaultLanguage.Visible = false;
        }
        else
        {
            chkRelateTypeWithDefaultLanguage.Visible = true;
        }
    }

    private void BindData()
    {
        try
        {
            l_Language = Languages.Static_GetList();
            l_RelateTypeGroups = RelateTypeGroups.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            RelateTypes m_RelateTypes = new RelateTypes();
            RelateTypeWithDefaultLanguage m_RelateTypeWithDefaultLanguage = new RelateTypeWithDefaultLanguage();
            byte m_RelateTypeId=0;
            byte ReferToDefaultLanguage = 0;
            int RowCount = 0;
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            if (LanguageId == 1)
            {
                chkRelateTypeWithDefaultLanguage.Visible = false;
                ReferToDefaultLanguage = 0;
                m_RelateTypes.DocGroupId = byte.Parse(ddlDocGroups.SelectedValue);
                //l_RelateTypes = m_RelateTypes.RelateTypes_GetList(ActUserId, ddlOrderBy.SelectedValue, LanguageId, m_RelateTypeId, ReferToDefaultLanguage);
                l_RelateTypes = m_RelateTypes.GetPage("","", ddlOrderBy.SelectedValue, m_grid.PageSize, m_grid.PageIndex-1,ref RowCount);
                m_grid.DataSource = l_RelateTypes;
            }
            else
            {
                chkRelateTypeWithDefaultLanguage.Visible = true;
                ReferToDefaultLanguage = 1;
                m_RelateTypes.DocGroupId = byte.Parse(ddlDocGroups.SelectedValue);
                l_RelateTypeWithDefaultLanguage = m_RelateTypeWithDefaultLanguage.RelateTypes_GetList(ActUserId, ddlOrderBy.SelectedValue, LanguageId, m_RelateTypeId, ReferToDefaultLanguage);
                m_grid.DataSource = l_RelateTypeWithDefaultLanguage;
            }
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
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            if (ddlLanguage.SelectedValue == "1")
            {
                RelateTypes m_DataItem = (RelateTypes)e.Row.DataItem;
                RelateTypeId = m_DataItem.RelateTypeId;
                Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
                if (rptLanguage != null)
                {
                    rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                    rptLanguage.DataBind();
                }
            }
            else
            {
                RelateTypeWithDefaultLanguage m_DataItem = (RelateTypeWithDefaultLanguage)e.Row.DataItem;
                RelateTypeId = m_DataItem.RelateTypeId;
                Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
                if (rptLanguage != null)
                {
                    rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                    rptLanguage.DataBind();
                }
            }           
        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            Label lblRelateTypeName = (Label) m_grid.Rows[e.RowIndex].FindControl("lblRelateTypeName");
            RelateTypes m_RelateTypes = new RelateTypes();
            m_RelateTypes.RelateTypeId = byte.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_RelateTypes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa Kiểu liên kết VB: {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2)
            {
                deleteMessage = string.Format("Xóa Kiểu liên kết VB <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblRelateTypeName.Text) ? lblRelateTypeName.Text : "");
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
            RelateTypes m_RelateTypes = new RelateTypes();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_RelateTypes.RelateTypeId = byte.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_RelateTypes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " Kiểu liên kết VB.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> Kiểu liên kết VB chưa được xóa.", countDeleteError);
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
        BindData();
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue == "1")
        {
            chkRelateTypeWithDefaultLanguage.Checked = false;
        }
        else
        {
            chkRelateTypeWithDefaultLanguage.Checked = true;
        }
        BindData();
    }
    protected void chkRelateTypeWithDefaultLanguage_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
}

