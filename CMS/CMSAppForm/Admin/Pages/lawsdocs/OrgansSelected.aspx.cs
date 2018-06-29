using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using ICSoft.LawDocsLib;
using sms.admin.security;

public partial class Admin_OrgansSelected : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short OrganId = 0;
    protected byte LanguageId = 0;
    protected short DisplayTypeId = 0;
    //protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<OrganTypes> l_OrganTypes = new List<OrganTypes>();
    protected List<ICSoft.LawDocsLib.Organs> l_Organs = new List<ICSoft.LawDocsLib.Organs>();
    protected List<OrganWithDefaultLanguage> l_OrganWithDefaultLanguage = new List<OrganWithDefaultLanguage>();
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
            DropDownListHelpers.DDLDisplayTypes_BindByCulture(ddlDisplayTypes, DisplayTypes.Static_GetList(),"", DisplayTypeId.ToString());
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrganTypes_BindByCulture(ddlOrganTypes, OrganTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Organs"), "");
            BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
        if (ddlLanguage.SelectedValue == "1")
        {
            chkOrganWithDefaultLanguage.Visible = false;
        }
        else
        {
            chkOrganWithDefaultLanguage.Visible = true;
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            l_Language = Languages.Static_GetList();
            l_ReviewStatus=ReviewStatus.Static_GetList();
            l_OrganTypes = OrganTypes.Static_GetList();
            //Users m_Users = new Users();
            //l_Users = m_Users.GetAll();
            ICSoft.LawDocsLib.Organs m_Organs = new ICSoft.LawDocsLib.Organs();
            OrganWithDefaultLanguage m_OrganWithDefaultLanguage = new OrganWithDefaultLanguage();

            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            string m_OrderBy = ddlOrderBy.SelectedValue;
            short m_OrganId = 0;
            string m_OrganName = txtSearch.Text.Trim();
            byte m_OrganTypeId = byte.Parse(ddlOrganTypes.SelectedValue);
            short m_DisplayTypeId = 0;
            byte ReferToDefaultLanguage = 0;
            if (LanguageId == 1)
            {
                chkOrganWithDefaultLanguage.Visible = false;
                ReferToDefaultLanguage = 0;
                l_Organs = m_Organs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_OrganId, m_OrganName, m_OrganTypeId, m_DisplayTypeId, 
                                             m_ReviewStatusId, ReferToDefaultLanguage, ref RowCount);
                m_grid.DataSource = l_Organs;
            }
            else
            {
                chkOrganWithDefaultLanguage.Visible = true;
                ReferToDefaultLanguage = 1;
                l_OrganWithDefaultLanguage = m_OrganWithDefaultLanguage.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_OrganId, 
                                                                                m_OrganName, m_OrganTypeId, m_DisplayTypeId, m_ReviewStatusId, ReferToDefaultLanguage, ref RowCount);
                m_grid.DataSource = l_OrganWithDefaultLanguage;
            }            
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
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

            if (ddlLanguage.SelectedValue == "1")
            {
                ICSoft.LawDocsLib.Organs m_DataItem = (ICSoft.LawDocsLib.Organs)e.Row.DataItem;
                OrganId = m_DataItem.OrganId;
                Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
                if (rptLanguage != null)
                {
                    rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                    rptLanguage.DataBind();
                }
            }
            else
            {
                OrganWithDefaultLanguage m_DataItem = (OrganWithDefaultLanguage)e.Row.DataItem;
                OrganId = m_DataItem.OrganId;
                Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
                if (rptLanguage != null)
                {
                    rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                    rptLanguage.DataBind();
                }
            }            
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedValue == "1")
        {
            chkOrganWithDefaultLanguage.Checked = false;
        }
        else
        {
            chkOrganWithDefaultLanguage.Checked = true;
        }
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlOrganTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void chkOrganWithDefaultLanguage_CheckedChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    private void SelectedCQBH_Click()
    {
        try
        {
            //if (Request.Params["DisplayTypeId"] != null && Request.Params["DisplayTypeId"].ToString() != "")
            //{
            //    DisplayTypeId = short.Parse(Request.Params["DisplayTypeId"].ToString());
            //}
            OrganDisplays m_OrganDisplays = new OrganDisplays();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_OrganDisplays.DisplayOrder = 0;
                        m_OrganDisplays.DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
                        m_OrganDisplays.OrganId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_OrganDisplays.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        m_OrganDisplays.Insert(ConstantHelpers.Replicated, ActUserId);
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
    protected void lbSelectedOrgans_Click(object sender, EventArgs e)
    {
        SelectedCQBH_Click();
    }
    protected void lbSelectedOrgans2_Click(object sender, EventArgs e)
    {
        SelectedCQBH_Click();
    }
}

