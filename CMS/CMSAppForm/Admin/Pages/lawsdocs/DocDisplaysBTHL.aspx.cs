using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using ICSoft.LawDocsLib;
using sms.admin.security;

public partial class Admin_DocDisplaysBTHL : System.Web.UI.Page
{
    protected int EditIndex = -1;
    protected int ActUserId = 0;
    protected int DocDisplayId = 0;
    protected int DocId = 0;
    protected short DisplayTypeId = 0;
    protected byte LanguageId = 0;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<DisplayTypes> l_DisplayTypes = new List<DisplayTypes>();
    
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
            DropDownListHelpers.DDLDisplayTypes_BindByCulture(ddlDisplayTypes, DisplayTypes.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_EFFECT_NEWLSLETTERS"])), "");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("DocDisplays"), "");
            BindData(-1);
        }
    }

    private void BindData(int index)
    {
        try
        {
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            l_Language = Languages.Static_GetList();

            DocDisplays m_DocDisplays = new DocDisplays();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            short m_DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_grid.EditIndex = index;
            m_grid.DataSource = m_DocDisplays.DocDisplays_GetList(ActUserId, m_OrderBy, m_DisplayTypeId, LanguageId);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text =m_grid.Rows.Count.ToString();
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
            DocDisplays m_DataItem = (DocDisplays)e.Row.DataItem;
            DocDisplayId = m_DataItem.DocDisplayId;   
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lblDocId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblDocId");
            Label lblDisplayTypeId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblDisplayTypeId");        
            DocDisplays m_DocDisplays = new DocDisplays();
            m_DocDisplays.DocId = int.Parse(lblDocId.Text);
            m_DocDisplays.DisplayTypeId = short.Parse(lblDisplayTypeId.Text);
            m_DocDisplays.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_DocDisplays.Delete(ConstantHelpers.Replicated, ActUserId);
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData(-1);
    }

    //--------------------------------------------------------
    protected void m_grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        EditIndex = e.NewEditIndex;
        BindData(EditIndex);
    }
    //--------------------------------------------------------
    protected void m_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        BindData(-1);
    }
    //--------------------------------------------------------
    protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int id = e.RowIndex;
            m_grid.EditIndex = id;
            GridViewRow row = m_grid.Rows[id];
            short updateId = Int16.Parse(m_grid.DataKeys[id].Value.ToString());
            if (updateId > 0)
            {
                DocDisplays m_DocRelates = new DocDisplays();
                m_DocRelates = m_DocRelates.Get(updateId, byte.Parse(ddlLanguage.SelectedValue));
                DocId = m_DocRelates.DocId;
                DisplayTypeId = m_DocRelates.DisplayTypeId;
                m_DocRelates.DocId = DocId;
                m_DocRelates.DisplayTypeId = DisplayTypeId;
                m_DocRelates.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                m_DocRelates.DisplayOrder = int.Parse(((TextBox)row.FindControl("txtDisplayOrder")).Text);
                m_DocRelates.UpdateDisplayOrder(ConstantHelpers.Replicated, ActUserId);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
        BindData(-1);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      BindData(-1);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DocDisplays m_DocDisplays = new DocDisplays();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                Label lblDocId = (Label)m_Row.FindControl("lblDocId");
                Label lblDisplayTypeId = (Label)m_Row.FindControl("lblDisplayTypeId");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_DocDisplays.DocId = int.Parse(lblDocId.Text);
                        m_DocDisplays.DisplayTypeId = short.Parse(lblDisplayTypeId.Text);
                        m_DocDisplays.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        m_DocDisplays.Delete(ConstantHelpers.Replicated, ActUserId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData(-1);
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(-1);
    }
    protected void ddlOrganTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(-1);
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(-1);
    }
}

