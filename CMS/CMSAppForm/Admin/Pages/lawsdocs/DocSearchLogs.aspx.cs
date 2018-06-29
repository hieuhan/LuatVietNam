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
public partial class Admin_DocSearchLogs : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int DocSearchLogId = 0;
    protected byte LanguageId = 0;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<Fields> l_Fields = new List<Fields>();
    protected List<ICSoft.LawDocsLib.Organs> l_Organs = new List<ICSoft.LawDocsLib.Organs>();
    protected List<DocTypes> l_DocTypes = new List<DocTypes>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
       
        if (!IsPostBack)
        {
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "...", LanguageId.ToString());
            DropDownListHelpers.DDLDocTypes_BindByCulture(ddlDocTypes, DocTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(), "...");
            DropDownListHelpers.DDLOrgans_BindByCulture(ddlOrgans, ICSoft.LawDocsLib.Organs.Static_GetList(), "...");
            DropDownListHelpers.DDLSigners_BindByCulture(ddlSigners, Signers.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("DocSearchLogs"), "");
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
            int RowCount=0;
            l_Language = Languages.Static_GetList();
            l_Fields = Fields.Static_GetList();
            //l_Organs = ICSoft.LawDocsLib.Organs.Static_GetList();
            l_DocTypes = DocTypes.Static_GetList();

            DocSearchLogs m_DocSearchLogs = new DocSearchLogs();
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_SearchKeyword = txtSearchKeyword.Text.Trim();
            byte m_DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
            short m_FieldId = short.Parse(ddlFields.SelectedValue);
            short m_OrganId = short.Parse(ddlOrgans.SelectedValue);
            short m_SignerId = short.Parse(ddlSigners.SelectedValue.ToString());
            int m_CustomerId = 0;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_grid.DataSource = m_DocSearchLogs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy,m_SearchKeyword, m_DocTypeId, m_OrganId, m_SignerId, m_FieldId, LanguageId,m_CustomerId, m_DateFrom, m_DateTo,ref RowCount);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

   protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlDocTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlOrgans_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSigners_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
   
}

