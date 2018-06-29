using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
public partial class Admin_FieldsSelected : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short ParentFieldId = 0;
    protected short FieldId = 0;
    protected byte LanguageId = 0;
    protected short DisplayTypeId = 0;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Fields> l_Fields = new List<Fields>();    
    protected List<Fields> l_ParentField = new List<Fields>();
    protected List<FieldWithDefaultLanguage> l_FieldWithDefaultLanguage = new List<FieldWithDefaultLanguage>();    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ParentFieldId"] != null && Request.Params["ParentFieldId"].ToString() != "")
        {
            ParentFieldId = short.Parse(Request.Params["ParentFieldId"].ToString());
        }
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
            DropDownListHelpers.DDLDisplayTypes_BindByCulture(ddlDisplayTypes, DisplayTypes.Static_GetList(), "", DisplayTypeId.ToString());
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            Fields m_Fields = new Fields();
            l_ParentField = m_Fields.GetListByFieldId(byte.Parse(ddlLanguage.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentField, l_ParentField, "...", ParentFieldId.ToString());
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Fields"), "");
            BindData();
        }
        if (ddlLanguage.SelectedValue == "1")
        {
           chkFieldWithDefaultLanguage.Visible = false;
        }
        else
        {
            chkFieldWithDefaultLanguage.Visible = true;
        }
    }

    private void BindData()
    {
        try
        {
            l_Language = Languages.Static_GetList();
            l_ReviewStatus=ReviewStatus.Static_GetList();
            Fields m_Fields = new Fields();
            FieldWithDefaultLanguage m_FieldWithDefaultLanguage = new FieldWithDefaultLanguage();

            ParentFieldId = short.Parse(ddlParentField.SelectedValue);
            byte ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            short m_FielId = 0;
            short m_DisplayTypeId = 0;
            byte ReferToDefaultLanguage = 0;
            string m_FielName = txtSearch.Text.Trim();
            if (LanguageId == 1)
            {
                chkFieldWithDefaultLanguage.Visible = false;               
                ReferToDefaultLanguage = 0;
                l_Fields = m_Fields.Fields_GetList(ActUserId, "", LanguageId, m_FielId, m_FielName, ParentFieldId, m_DisplayTypeId, ReviewStatusId, ReferToDefaultLanguage);
                m_grid.DataSource = l_Fields; 
            }
            else
            {
                chkFieldWithDefaultLanguage.Visible = true;
                ReferToDefaultLanguage = 1;
                l_FieldWithDefaultLanguage=m_FieldWithDefaultLanguage.Fields_GetList(ActUserId, "", LanguageId, m_FielId, m_FielName, ParentFieldId, m_DisplayTypeId, ReviewStatusId, ReferToDefaultLanguage);
                m_grid.DataSource = l_FieldWithDefaultLanguage; 
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
            Label lbFieldNameTree = (Label)e.Row.FindControl("lbFieldNameTree");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            if (ddlLanguage.SelectedValue == "1")
            {
                Fields m_DataItem = (Fields)e.Row.DataItem;
                FieldId = m_DataItem.FieldId;
                Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
                if (rptLanguage != null)
                {
                    rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                    rptLanguage.DataBind();
                }

                //if (m_DataItem.ParentFieldId == 0)
                //{
                //    e.Row.Cells[1].Font.Bold = true;
                //}
                //Bind For Filter
                for (int index = e.Row.RowIndex; index < l_Fields.Count; index++)
                {
                    m_DataItem = l_Fields[index];
                    if (m_DataItem.ParentFieldId == FieldId || m_DataItem.FieldId == FieldId)
                    {
                        lbFieldNameTree.Text += "," + m_DataItem.FieldName;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                FieldWithDefaultLanguage m_DataItem = (FieldWithDefaultLanguage)e.Row.DataItem;
                FieldId = m_DataItem.FieldId;
                Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
                if (rptLanguage != null)
                {
                    rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                    rptLanguage.DataBind();
                }

                //if (m_DataItem.ParentFieldId == 0)
                //{
                //    e.Row.Cells[1].Font.Bold = true;
                //}
                //Bind For Filter
                for (int index = e.Row.RowIndex; index < l_FieldWithDefaultLanguage.Count; index++)
                {
                    m_DataItem = l_FieldWithDefaultLanguage[index];
                    if (m_DataItem.ParentFieldId == FieldId || m_DataItem.FieldId == FieldId)
                    {
                        lbFieldNameTree.Text += "," + m_DataItem.FieldName;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

   protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue == "1")
        {
            chkFieldWithDefaultLanguage.Checked = false;
        }
        else
        {
            chkFieldWithDefaultLanguage.Checked = true;
        }
        ParentFieldId = short.Parse(ddlParentField.SelectedValue);
        Fields m_Fields = new Fields();
        l_ParentField = m_Fields.GetListByFieldId(byte.Parse(ddlLanguage.SelectedValue),0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentField, l_ParentField, "...", ParentFieldId.ToString());
        BindData();
       
    }

    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
   
    protected void ddlParentField_SelectedIndexChanged(object sender, EventArgs e)
    {
        ParentFieldId = short.Parse(ddlParentField.SelectedValue);
        BindData();
    }
    protected void chkFieldWithDefaultLanguage_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }

    private void SelectedFields_Click()
    {
        try
        {
            FieldDisplays m_FieldDisplays = new FieldDisplays();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_FieldDisplays.DisplayOrder = 0;
                        m_FieldDisplays.DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
                        m_FieldDisplays.FieldId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_FieldDisplays.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        m_FieldDisplays.Insert(ConstantHelpers.Replicated, ActUserId);
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

    protected void lbSelectedFields_Click(object sender, EventArgs e)
    {
        SelectedFields_Click();       
    }
    protected void lbSelectedFields2_Click(object sender, EventArgs e)
    {
        SelectedFields_Click();
    }
}

