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

public partial class Admin_OrganDisplays : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int OrganDisplayId = 0;
    protected byte LanguageId = 0;
    protected short DisplayTypeId = 0;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<DisplayTypes> l_DisplayTypes = new List<DisplayTypes>();
    protected List<ICSoft.LawDocsLib.Organs> l_Organs = new List<ICSoft.LawDocsLib.Organs>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["DisplayTypeId"] != null && Request.Params["DisplayTypeId"].ToString() != "")
        {
            DisplayTypeId = short.Parse(Request.Params["DisplayTypeId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLDisplayTypes_BindByCulture(ddlDisplayTypes, DisplayTypes.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_LEGAL_DOCUMENTS"])), "");
            ddlDisplayTypes.SelectedValue = DisplayTypeId.ToString();
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("OrganDisplays"), "");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_Organs = ICSoft.LawDocsLib.Organs.Static_GetList();
            //l_DisplayTypes = DisplayTypes.Static_GetList();            
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            OrganDisplays m_OrganDisplays = new OrganDisplays();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            short m_DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
            byte m_LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_grid.DataSource = m_OrganDisplays.OrganDisplays_GetList(ActUserId, m_OrderBy, m_DisplayTypeId, m_LanguageId);
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
            OrganDisplays m_DataItem = (OrganDisplays)e.Row.DataItem;
            OrganDisplayId = m_DataItem.OrganDisplayId;   
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lblOrganId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblOrganId");
            Label lblDisplayTypeId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblDisplayTypeId");
            Label lblLanguageId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblLanguageId");
            OrganDisplays m_OrganDisplays = new OrganDisplays();
            m_OrganDisplays.OrganId = short.Parse(lblOrganId.Text);
            m_OrganDisplays.DisplayTypeId = short.Parse(lblDisplayTypeId.Text);
            m_OrganDisplays.LanguageId = byte.Parse(lblLanguageId.Text);
            m_OrganDisplays.Delete(ConstantHelpers.Replicated, ActUserId);
            
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
        try
        {
            OrganDisplays m_OrganDisplays = new OrganDisplays();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                Label lblOrganId = (Label)m_Row.FindControl("lblOrganId");
                Label lblDisplayTypeId = (Label)m_Row.FindControl("lblDisplayTypeId");
                Label lblLanguageId = (Label)m_Row.FindControl("lblLanguageId");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_OrganDisplays.OrganId = short.Parse(lblOrganId.Text);
                        m_OrganDisplays.DisplayTypeId = short.Parse(lblDisplayTypeId.Text);
                        m_OrganDisplays.LanguageId = byte.Parse(lblLanguageId.Text);
                        m_OrganDisplays.Delete(ConstantHelpers.Replicated, ActUserId);
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
        BindData();
    }
   
    protected void ddlDisplayTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}

