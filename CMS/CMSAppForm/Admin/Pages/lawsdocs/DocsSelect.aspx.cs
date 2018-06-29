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
public partial class Admin_DocsSelect : System.Web.UI.Page
{
    protected short DisplayTypeId = 0;
    protected int ActUserId = 0;
    protected int DocId = 0;
    protected byte LanguageId = 0;
    protected int CrUserId = 0;
    protected byte UseStatusId = 0;
    protected string CrDatetime = string.Empty;
    protected short DataSourceId = 0;
    protected byte DocTypeId = 0;
    protected byte EffectStatusId = 0;
    protected short FieldId = 0;
    protected short OrganId = 0;
    protected int IssueYear = 0;
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
    private short SysMessageId = 0;
    protected string ImgDemo = "";
    protected string txtDemo = "";
    protected string CKEditor = "";
    protected string CKEditorFuncNum = "";
    protected string langCode = "";
    protected string SetMediaDomain = "";
    protected string script = "";
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
        if (Request.Params["CKEditor"] != null && Request.Params["CKEditor"].ToString() != "")
        {
            CKEditor = Request.Params["CKEditor"].ToString();
        }
        if (Request.Params["CKEditorFuncNum"] != null && Request.Params["CKEditorFuncNum"].ToString() != "")
        {
            CKEditorFuncNum = Request.Params["CKEditorFuncNum"].ToString();
        }
        if (Request.Params["ImgDemo"] != null && Request.Params["ImgDemo"].ToString() != "")
        {
            ImgDemo = Request.Params["ImgDemo"].ToString();
        }
        if (Request.Params["txtDemo"] != null && Request.Params["txtDemo"].ToString() != "")
        {
            txtDemo = Request.Params["txtDemo"].ToString();
        }
        if (Request.Params["SetMediaDomain"] != null && Request.Params["SetMediaDomain"].ToString() != "")
        {
            SetMediaDomain = Request.Params["SetMediaDomain"].ToString();
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLDocGroups_BindByCulture(ddlDocGroups, DocGroups.GetList(), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            short docgroupId = short.Parse(ddlDocGroups.SelectedValue);
            short displayTypeId;
            switch (docgroupId)
            {
                case 1:
                    displayTypeId = 62;
                    break;
                case 2:
                    displayTypeId = 5;
                    break;
                case 3:
                    displayTypeId = 52;
                    break;
                case 4:
                    displayTypeId = 53;
                    break;
                case 5:
                    displayTypeId = 54;
                    break;
                case 7:
                    displayTypeId = 63;
                    break;
                case 8:
                    displayTypeId = 61;
                    break;
                default:
                    displayTypeId = 0;
                    break;
            }
            DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList_ByDisplayTypeId(displayTypeId), "...");
            DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(), "...");
            DropDownListHelpers.DDLOrgans_BindByCulture(ddlOrgans, ICSoft.LawDocsLib.Organs.Static_GetList(), "...");
            DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSources, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_LEGAL_DOCUMENTS"])), "...");
            DropDownListHelpers.DDLEffectStatus_BindByCulture(ddlEffectStatus, EffectStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLUseStatus_BindByCulture(ddlUseStatus, UseStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Docs"), "");
            DropDownListHelpers.DDLUsers_BindByCulture(ddlCrUserId, new Users().GetAll(), "...");
            DropDownListHelpers.DDLUsers_BindByCulture(ddlUpdUserId, new Users().GetAll(), "...");
            DropDownListHelpers.DDLUsers_BindByCulture(ddlRevUserId, new Users().GetAll(), "...");
            DropDownListHelpers.DDL_Bind(ddlProvinces, Provinces.Static_GetList(1), "...");// only vietnam
            ddlDocGroups.SelectedValue = "0";
            ddlReviewStatus.SelectedValue = "2";
            ddlReviewStatus.Enabled = false;
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
            lblMsg.Text = "";
            chr = Convert.ToChar(39);
            Docs m_Docs = new Docs();
            int RowCount = 0;
            l_Language = Languages.Static_GetList();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            l_EffectStatus = EffectStatus.Static_GetList();
            l_UseStatus = UseStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();


            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            string m_OrderBy = ddlOrderBy.SelectedValue;
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
            if (CrUserId > 0)
            {
                ddlCrUserId.ClearSelection();
                ddlCrUserId.Items.FindByValue(CrUserId.ToString()).Selected = true;
            }
            else
                CrUserId = int.Parse(ddlCrUserId.SelectedValue);
            if (UseStatusId > 0)
            {
                ddlUseStatus.ClearSelection();
                ddlUseStatus.Items.FindByValue(UseStatusId.ToString()).Selected = true;
            }
            else UseStatusId = byte.Parse(ddlUseStatus.SelectedValue);

            if (DataSourceId > 0)
            {
                ddlDataSources.ClearSelection();
                ddlDataSources.Items.FindByValue(DataSourceId.ToString()).Selected = true;
            }
            else DataSourceId = short.Parse(ddlDataSources.SelectedValue);

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

            if (OrganId > 0)
            {
                ddlOrgans.ClearSelection();
                ddlOrgans.Items.FindByValue(OrganId.ToString()).Selected = true;
            }
            else OrganId = short.Parse(ddlOrgans.SelectedValue);

            if (DocTypeId > 0)
            {
                ddlDocTypes.ClearSelection();
                ddlDocTypes.Items.FindByValue(DocTypeId.ToString()).Selected = true;
            }
            else DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
            if (!string.IsNullOrEmpty(CrDatetime))
            {
                ddlSearchByDate.ClearSelection();
                ddlSearchByDate.Items.FindByValue("CrDateTime").Selected = true;
                txtDateFrom.Text = CrDatetime.Replace("-", "/");
                txtDateTo.Text = CrDatetime.Replace("-", "/");
            }

            int m_UpdUserId = int.Parse(ddlUpdUserId.SelectedValue);
            int m_RevUserId = int.Parse(ddlRevUserId.SelectedValue);
            string m_SearchByDate = ddlSearchByDate.SelectedValue.ToString();
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_grid.PageSize = int.Parse(ddlPageSize.SelectedValue);
            m_Docs.DocGroupId = byte.Parse(ddlDocGroups.SelectedValue);
            List<Docs> l_Docs = new List<Docs>();
            if (ddlProvinces.SelectedValue == "0")
            {
                l_Docs = m_Docs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_SearchKeyword, m_IsSearchExact, m_HighlightKeyword,
                                                m_DocId, m_DocGUId, m_DocName, m_DocIdentity, DocTypeId, DataSourceId, FieldId, m_OrganTypeId, OrganId, m_SignerId, m_DocRelateId,
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
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Docs m_Docs = new Docs();
            m_Docs = m_Docs.Get(int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString()), byte.Parse(ddlLanguage.SelectedValue));
            string docUrl = string.IsNullOrEmpty(m_Docs.DocUrl) ? "" : m_Docs.DocUrl;
            if (CKEditor != "")
            {
                script = @"<script language='JavaScript'>window.opener.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + (docUrl.StartsWith("http") ? docUrl : WebConstans.DOMAIN_WEB + docUrl) + "');window.close();</script>";
            }
            //Response.Write(script);
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close1", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
  
    //protected void lbDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Medias m_Medias = new Medias();
    //        foreach (GridViewRow m_Row in m_grid.Rows)
    //        {
    //            CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
    //            if (chkAction != null)
    //            {
    //                if (chkAction.Checked)
    //                {
    //                    m_Medias.MediaId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
    //                    break;
    //                }
    //            }
    //        }
    //        m_Medias = m_Medias.Get();
    //        string script = @"<script language='JavaScript'>" +
    //           "var ImgDemo = window.opener.document.getElementById(\"" + ImgDemo + "\"); " +
    //           "var txtName = window.opener.document.getElementById(\"" + txtDemo + "\"); " +
    //           "txtName.value = '" + CmsConstants.WEBSITE_MEDIADOMAIN + m_Medias.FilePath + "';" +
    //           "ImgDemo.src = '" + CmsConstants.WEBSITE_MEDIADOMAIN + m_Medias.FilePath + "';" +
    //           " window.close(); " +
    //           "</script>";

    //        if (CKEditor != "")
    //        {
    //            script = @"<script language='JavaScript'>window.opener.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + CmsConstants.WEBSITE_MEDIADOMAIN + m_Medias.FilePath + "');window.close();</script>";
    //        }
    //        //Response.Write(script);
    //        ClientScriptManager csm = this.ClientScript;
    //        csm.RegisterClientScriptBlock(this.GetType(), "close1", script);
    //    }
    //    catch (Exception ex)
    //    {
    //        sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
    //        JSAlertHelpers.Alert(ex.Message, this);
    //    }
    //    BindData();
    //}

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CustomPaging.PageIndex = 1;
        BindData();

    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CustomPaging.PageIndex = 1;
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
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Docs m_Docs = new Docs();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Docs.Get(int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString()), byte.Parse(ddlLanguage.SelectedValue));
                        if (m_Docs.ReviewStatusId != 2)
                        {
                            m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                            m_Docs.DocId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                            SysMessageTypeId = m_Docs.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        }

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
        // CustomPaging.PageIndex = 1;
        BindData();
    }


    protected void ddlSearchByDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlDocTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlOrgans_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSigners_SelectedIndexChanged(object sender, EventArgs e)
    {
        //CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlDataSources_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlEffectStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlUseStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        // CustomPaging.PageIndex = 1;
        BindData();
    }
    
    protected void ddlDocGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        short docgroupId = short.Parse(ddlDocGroups.SelectedValue);
        short displayTypeId;
        switch (docgroupId)
        {
            case 1:
                displayTypeId = 62;
                break;
            case 2:
                displayTypeId = 5;
                break;
            case 3:
                displayTypeId = 52;
                break;
            case 4:
                displayTypeId = 53;
                break;
            case 5:
                displayTypeId = 54;
                break;
            case 7:
                displayTypeId = 63;
                break;
            case 8:
                displayTypeId = 61;
                break;
            default:
                displayTypeId = 0;
                break;
        }
        DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList_ByDisplayTypeId(displayTypeId), "...");
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

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ckbIsSearchExact_CheckedChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

}
