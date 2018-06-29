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
using System.Configuration;
public partial class Admin_ArticleDocUpdate : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected short SiteId = 0;
    protected byte DataTypeId = 0;
    protected short CategoryId = 0;
    protected int DocId = 0;
    protected int CrUserId = 0;
    protected byte UseStatusId = 0;
    protected string CrDatetime = string.Empty;
    protected short DataSourceId = 0;
    protected byte DocTypeId = 0;
    protected byte EffectStatusId = 0;
    protected short FieldId = 0;
    protected short OrganId = 0;
    protected int IssueYear = 0;
    protected string GenType = string.Empty;
    private const string pattern = "dd-MM-yyyy";
    private DateTime parsedDate;
    public char chr;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<UseStatus> l_UseStatus = new List<UseStatus>();
    protected List<EffectStatus> l_EffectStatus = new List<EffectStatus>();
    protected List<ICSoft.LawDocsLib.Organs> l_Organs = new List<ICSoft.LawDocsLib.Organs>();
    protected List<Fields> l_Fields = new List<Fields>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (Request.Params["CategoryId"] != null && Request.Params["CategoryId"].ToString() != "")
        {
            CategoryId = short.Parse(Request.Params["CategoryId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
        {
            AppTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
        }
        if (Request.Params["GenType"] != null && Request.Params["GenType"].ToString() != "")
        {
            GenType = Request.Params["GenType"].ToString();
        }
        if (SiteId == 2) LanguageId = 2;
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...", "2");
            //DropDownListHelpers.DDLDocTypes_BindByCulture(ddlDocTypes, DocTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(), "...");
            //DropDownListHelpers.DDLOrgans_BindByCulture(ddlOrgans, ICSoft.LawDocsLib.Organs.Static_GetList(), "...");
            //DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSources, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_LEGAL_DOCUMENTS"])), "...");
            DropDownListHelpers.DDLEffectStatus_BindByCulture(ddlEffectStatus, EffectStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLUseStatus_BindByCulture(ddlUseStatus, UseStatus.Static_GetList(), "...");
            //DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Docs"), "");
            //DropDownListHelpers.DDLUsers_BindByCulture(ddlCrUserId, new Users().GetAll(), "...");
            //DropDownListHelpers.DDLUsers_BindByCulture(ddlUpdUserId, new Users().GetAll(), "...");
            //DropDownListHelpers.DDLUsers_BindByCulture(ddlRevUserId, new Users().GetAll(), "...");
            DropDownListHelpers.DDL_Bind(ddlProvinces, Provinces.Static_GetList(1), "...");// only vietnam
            BindData();
            BindRelate();
        }
        else if (CustomPaging.ChangePage)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            lblMsg.Text = "";
            chr = Convert.ToChar(39);
            Docs m_Docs = new Docs();
            DocArticles m_DocArticles = new DocArticles();
            int RowCount = 0;
            l_Language = Languages.Static_GetList();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            l_EffectStatus = EffectStatus.Static_GetList();
            l_UseStatus = UseStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();


            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            string m_OrderBy = "";
            int m_DocId = 0;
            string m_DocGUId = "";
            short m_DisplayTypeId = 0;
            string m_SearchKeyword = "";
            string m_DocName = "";
            string m_DocIdentity = "";
            byte m_IsSearchExact = 0;
            if (ckbIsSearchExact.Checked == true)
            {
                m_IsSearchExact = 1;
            }
            byte m_HighlightKeyword = 1;
            byte m_HasDocFile = 0;
            switch (rblFindTypes.SelectedValue)
            {
                case "DocIdentity":
                    m_DocIdentity = txtSearch.Text.Trim();
                    break;
                case "DocName":
                    m_DocName = txtSearch.Text.Trim();
                    break;
                default:
                    m_SearchKeyword = txtSearch.Text.Trim();
                    break;
            }
            byte m_OrganTypeId = 0;
            short m_SignerId = 0;
            int m_DocRelateId = 0;
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            if (UseStatusId > 0)
            {
                ddlUseStatus.ClearSelection();
                ddlUseStatus.Items.FindByValue(UseStatusId.ToString()).Selected = true;
            }
            else UseStatusId = byte.Parse(ddlUseStatus.SelectedValue);

            if (EffectStatusId > 0)
            {
                ddlEffectStatus.ClearSelection();
                ddlEffectStatus.Items.FindByValue(EffectStatusId.ToString()).Selected = true;
            }
            else EffectStatusId = byte.Parse(ddlEffectStatus.SelectedValue);

            if (FieldId > 0)
            {
                ddlFields.ClearSelection();
                ddlFields.Items.FindByValue(FieldId.ToString()).Selected = true;
            }
            else FieldId = short.Parse(ddlFields.SelectedValue);

            if (!string.IsNullOrEmpty(CrDatetime))
            {
                ddlSearchByDate.ClearSelection();
                ddlSearchByDate.Items.FindByValue("CrDateTime").Selected = true;
                txtDateFrom.Text = CrDatetime.Replace("-", "/");
                txtDateTo.Text = CrDatetime.Replace("-", "/");
            }

            int m_UpdUserId = 0;
            int m_RevUserId = 0;
            string m_SearchByDate = ddlSearchByDate.SelectedValue.ToString();
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_grid.PageSize = 50;
            m_Docs.DocGroupId = byte.Parse(ddlDocGroups.SelectedValue);
            List<Docs> l_Docs = new List<Docs>();
            if (ddlProvinces.SelectedValue == "0")
            {
                l_Docs = m_DocArticles.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_SearchKeyword, m_IsSearchExact, m_HighlightKeyword,
                                                ArticleId, m_DocGUId, m_DocName, m_DocIdentity, DocTypeId, DataSourceId, FieldId, m_OrganTypeId, OrganId, m_SignerId, m_Docs.DocGroupId, m_DocRelateId,
                                                m_DisplayTypeId, UseStatusId, EffectStatusId, m_ReviewStatusId, m_HasDocFile, CrUserId, m_UpdUserId, m_RevUserId, IssueYear, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
            }
            else
            {
                short ProvicenId = short.Parse(ddlProvinces.SelectedValue);
                l_Docs = m_Docs.GetPageWithProvince(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_SearchKeyword, m_IsSearchExact, m_HighlightKeyword,
                                                m_DocId, m_DocGUId, m_DocName, m_DocIdentity, DocTypeId, DataSourceId, FieldId, m_OrganTypeId, OrganId, m_SignerId, m_DocRelateId,
                                                m_DisplayTypeId, UseStatusId, EffectStatusId, m_ReviewStatusId, ProvicenId, m_HasDocFile, CrUserId, m_UpdUserId, m_RevUserId, IssueYear, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
            }
            m_grid.DataSource = l_Docs;
            if (m_grid.PageSize > RowCount)
            {
                m_grid.AllowPaging = false;
            }
            else
            {
                m_grid.AllowPaging = true;
            }
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }

    private void BindRelate()
    {
        try
        { 
            DocArticles m_DocArticles=new DocArticles();
            m_gridRelate.DataSource = m_DocArticles.GetListByArticleId(ArticleId, LanguageId);
            m_gridRelate.DataBind();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
        short SysMessageId = 0;
        try
        {
            DocArticles m_DocArticles = new DocArticles();
            m_DocArticles.ArticleId = ArticleId;
            m_DocArticles.DocId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_DocArticles.DisplayOrder = 1;
            m_DocArticles.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
        BindRelate();
    }
    protected void m_gridRelate_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                //lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
        }
    }

    protected void m_gridRelate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        short SysMessageId = 0;
        try
        {
            Label lblDocId = (Label)m_gridRelate.Rows[e.RowIndex].FindControl("lblDocId");
            DocArticles m_DocArticles = new DocArticles();
            //m_DocArticles.DocArticleId = int.Parse(m_gridRelate.DataKeys[e.RowIndex].Value.ToString());
            m_DocArticles.ArticleId = ArticleId;
            m_DocArticles.DocId = int.Parse(lblDocId.Text);
            m_DocArticles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindRelate();
        BindData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        
        short SysMessageId = 0;
        try
        {
            DocArticles m_DocArticles = new DocArticles();
            foreach (GridViewRow m_Row in m_gridRelate.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        Label lblDocId = (Label)m_Row.FindControl("lblDocId");
                        m_DocArticles.ArticleId = ArticleId;
                        m_DocArticles.DocId = int.Parse(lblDocId.Text);
                        m_DocArticles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindRelate();
        BindData();
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();

    }

    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlCrUserId_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlUpdUserId_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlRevUserId_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    public string DocOrgans_GetOrganName(byte LanguageId, int DocId)
    {
        try
        {
            string RetVal = "";
            Docs m_Docs = new Docs();
            RetVal = m_Docs.DocOrgans_GetOrganName(LanguageId, DocId, ref RetVal);
            return RetVal;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string DocFields_GetFieldName(byte LanguageId, int DocId)
    {
        try
        {
            string RetVal = "";
            Docs m_Docs = new Docs();
            RetVal = m_Docs.DocFields_GetFieldName(LanguageId, DocId, ref RetVal);
            return RetVal;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlDocGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        //foreach (ListItem item in ddlSearchByDate.Items)
        //{
        //    if (item.Value == "IssueDate" && ddlDocGroups.SelectedValue == "5")
        //    {
        //        item.Text = "Ngày ký xác thực";
        //    }
        //    if (item.Value == "IssueDate" && ddlDocGroups.SelectedValue != "5")
        //    {
        //        item.Text = "Ngày ban hành";
        //    }
        //}
        short docgroupId = short.Parse(ddlDocGroups.SelectedValue);
        short displayTypeId = (short)(docgroupId == 1 ? 62 : (docgroupId == 2 ? 5 : (docgroupId == 5 ? 54 : (docgroupId == 6 ? 67 : 61))));
        //DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList_ByDisplayTypeId(displayTypeId), "...");
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSearchByDate_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void ddlDataSources_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlEffectStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlUseStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ckbIsSearchExact_CheckedChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = CustomPaging.PageIndex;
        BindData();
    }
}
