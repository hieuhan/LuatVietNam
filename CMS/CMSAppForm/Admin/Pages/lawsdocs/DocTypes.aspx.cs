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

public partial class Admin_DocTypes : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected byte DocTypeId = 0;
    protected byte LanguageId = 0;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<DisplayTypes> l_DisplayTypes = new List<DisplayTypes>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<DocTypes> l_DocTypes = new List<DocTypes>();
    protected List<DocTypeWithDefaultLanguage> l_DocTypeWithDefaultLanguage = new List<DocTypeWithDefaultLanguage>();
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
            DropDownListHelpers.DDLDisplayTypes_BindByCulture(ddlDisplayTypes, DisplayTypes.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_LEGAL_DOCUMENTS"])), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("DocTypes"), "");            
            BindData();
        }
        if (ddlLanguage.SelectedValue == "1")
        {
            chkDocTypeWithDefaultLanguage.Visible = false;
        }
        else
        {
            chkDocTypeWithDefaultLanguage.Visible = true;
        }
    }

    private void BindData()
    {
        try
        {
            byte ReferToDefaultLanguage = 0;
            l_Language = Languages.Static_GetList();
            l_DisplayTypes = DisplayTypes.Static_GetList();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            DocTypes m_DocTypes = new DocTypes();
            DocTypeWithDefaultLanguage m_DocTypeWithDefaultLanguage = new DocTypeWithDefaultLanguage();
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            string m_OrderBy = ddlOrderBy.SelectedValue; ;
            short m_DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            byte m_DocTypeId = 0;
            if (LanguageId == 1)
            {
                chkDocTypeWithDefaultLanguage.Visible = false;
                ReferToDefaultLanguage = 0;
                l_DocTypes = m_DocTypes.DocTypes_GetList(ActUserId,m_OrderBy, LanguageId, m_DocTypeId,m_ReviewStatusId, m_DisplayTypeId,ReferToDefaultLanguage);
                m_grid.DataSource = l_DocTypes;
            }
            else
            {
                chkDocTypeWithDefaultLanguage.Visible = true;
                ReferToDefaultLanguage = 1;
                l_DocTypeWithDefaultLanguage = m_DocTypeWithDefaultLanguage.DocTypes_GetList(ActUserId, m_OrderBy, LanguageId, m_DocTypeId, m_ReviewStatusId, m_DisplayTypeId, ReferToDefaultLanguage);
                m_grid.DataSource = l_DocTypeWithDefaultLanguage;
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
                DocTypes m_DataItem = (DocTypes)e.Row.DataItem;
                DocTypeId = m_DataItem.DocTypeId;
                Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
                if (rptLanguage != null)
                {
                    rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                    rptLanguage.DataBind();
                }
               
            }
            else
            {
                DocTypeWithDefaultLanguage m_DataItem = (DocTypeWithDefaultLanguage)e.Row.DataItem;
                DocTypeId = m_DataItem.DocTypeId;
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
        try
        {
            DocTypes m_DocTypes = new DocTypes();
            m_DocTypes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_DocTypes.DocTypeId = byte.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_DocTypes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue == "1")
        {
            chkDocTypeWithDefaultLanguage.Checked = false;
        }
        else
        {
            chkDocTypeWithDefaultLanguage.Checked = true;
        }
       BindData();
    }
   

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            DocTypes m_DocTypes = new DocTypes();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_DocTypes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        m_DocTypes.DocTypeId = byte.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_DocTypes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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


    protected void ddlOrganTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        BindData();
    }
    protected void ddlDisplayTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void chkDocTypeWithDefaultLanguage_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}

