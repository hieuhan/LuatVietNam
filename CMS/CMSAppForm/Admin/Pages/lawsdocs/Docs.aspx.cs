using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
public partial class Admin_Docs : System.Web.UI.Page
{
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }

        if (Request.Params["CrUserId"] != null && Request.Params["CrUserId"].ToString() != "")
        {
            CrUserId = int.Parse(Request.Params["CrUserId"].ToString());
        }

        if (Request.Params["UseStatusId"] != null && Request.Params["UseStatusId"].ToString() != "")
        {
            UseStatusId = byte.Parse(Request.Params["UseStatusId"].ToString());
        }

        if (Request.Params["DocTypeId"] != null && Request.Params["DocTypeId"].ToString() != "")
        {
            DocTypeId = byte.Parse(Request.Params["DocTypeId"].ToString());
        }

        if (Request.Params["DataSourceId"] != null && Request.Params["DataSourceId"].ToString() != "")
        {
            DataSourceId = short.Parse(Request.Params["DataSourceId"].ToString());
        }

        if (Request.Params["EffectStatusId"] != null && Request.Params["EffectStatusId"].ToString() != "")
        {
            EffectStatusId = byte.Parse(Request.Params["EffectStatusId"].ToString());
        }

        if (Request.Params["FieldId"] != null && Request.Params["FieldId"].ToString() != "")
        {
            FieldId = short.Parse(Request.Params["FieldId"].ToString());
        }

        if (Request.Params["IssueYear"] != null && Request.Params["IssueYear"].ToString() != "")
        {
            IssueYear = short.Parse(Request.Params["IssueYear"].ToString());
        }

        if (Request.Params["OrganId"] != null && Request.Params["OrganId"].ToString() != "")
        {
            OrganId = short.Parse(Request.Params["OrganId"].ToString());
        }

        if (Request.Params["UseStatusId"] != null && Request.Params["UseStatusId"].ToString() != "")
        {
            UseStatusId = byte.Parse(Request.Params["UseStatusId"].ToString());
        }

        if (Request.Params["CrDatetime"] != null && Request.Params["CrDatetime"].ToString() != "")
        {
            CrDatetime = Request.Params["CrDatetime"].ToString();
            CrDatetime = DateTime.TryParseExact(CrDatetime, pattern, null, DateTimeStyles.None, out parsedDate) ? parsedDate.ToString("dd-MM-yyyy") : string.Empty;
        }

        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLDocTypes_BindByCulture(ddlDocTypes, DocTypes.Static_GetList(), "...");
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
            //selected
            if (Session["Docs-ddlDocTypes"] != null && Request.UrlReferrer != null && (Request.UrlReferrer.OriginalString.Contains("DocsUpdateContentEdit.aspx") || Request.UrlReferrer.OriginalString.Contains("DocsEdit.aspx") || Request.UrlReferrer.OriginalString.Contains("DocsConsolidationEdit.aspx") || Request.UrlReferrer.OriginalString.Contains("DocsContentRelation.aspx") || Request.UrlReferrer.OriginalString.Contains("DocIndexes.aspx")))
            {
                ddlDocTypes.SelectedValue = Session["Docs-ddlDocTypes"].ToString();
                ddlCrUserId.SelectedValue = Session["Docs-ddlCrUserId"].ToString();
                ddlDataSources.SelectedValue = Session["Docs-ddlDataSources"].ToString()  ;
                ddlDocGroups.SelectedValue = Session["Docs-ddlDocGroups"].ToString() ;
                ddlDocTypes.SelectedValue= Session["Docs-ddlDocTypes"].ToString() ;
                ddlEffectStatus.SelectedValue=Session["Docs-ddlEffectStatus"].ToString() ;
                ddlFields.SelectedValue=Session["Docs-ddlFields"].ToString() ;
                ddlLanguage.SelectedValue=Session["Docs-ddlLanguage"].ToString() ;
                ddlOrderBy.SelectedValue=Session["Docs-ddlOrderBy"].ToString() ;
                ddlOrgans.SelectedValue=Session["Docs-ddlOrgans"].ToString() ;
                ddlPageSize.SelectedValue=Session["Docs-ddlPageSize"].ToString() ;
                ddlProvinces.SelectedValue=Session["Docs-ddlProvinces"].ToString() ;
                ddlReviewStatus.SelectedValue=Session["Docs-ddlReviewStatus"].ToString() ;
                ddlRevUserId.SelectedValue=Session["Docs-ddlRevUserId"].ToString() ;
                ddlSearchByDate.SelectedValue=Session["Docs-ddlSearchByDate"].ToString() ;
                ddlUpdUserId.SelectedValue=Session["Docs-ddlUpdUserId"].ToString() ;
                ddlUseStatus.SelectedValue= Session["Docs-ddlUseStatus"].ToString() ;
                txtDateFrom.Text = Session["Docs-txtDateFrom"].ToString();
                txtDateTo.Text = Session["Docs-txtDateTo"].ToString();
                txtSearch.Text = Session["Docs-txtSearch"].ToString();
                rblFindTypes.SelectedValue = Session["Docs-rblFindTypes"].ToString();
                ckbIsSearchExact.Checked = bool.Parse(Session["Docs-ckbIsSearchExact"].ToString());
            }
            
            BindData();
        }
        if (!IsPostBack|| CustomPaging.ChangePage)
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
            int RowCount=0;
            l_Language = Languages.Static_GetList();
            l_ReviewStatus=ReviewStatus.Static_GetList();
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
                txtDateFrom.Text = CrDatetime.Replace("-","/");
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
            if(ddlProvinces.SelectedValue == "0")
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
            m_grid.AllowPaging = m_grid.PageSize <= RowCount;
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            rptStatusCount.DataSource = m_Docs.StatisticByReviewStatusV2(ref RowCount, byte.Parse(ddlDocGroups.SelectedValue), 1, byte.Parse(ddlLanguage.SelectedValue), m_SearchKeyword,
                m_DocId, m_DocName, m_DocIdentity, byte.Parse(ddlDocTypes.SelectedValue), short.Parse(ddlDataSources.SelectedValue), short.Parse(ddlFields.SelectedValue), OrganId, m_SignerId, UseStatusId, EffectStatusId, m_ReviewStatusId,
                CrUserId, m_UpdUserId, m_RevUserId, m_SearchByDate, m_DateFrom, m_DateTo, CustomPaging.PageIndex - 1, m_OrderBy, m_DocGUId, m_IsSearchExact, m_HighlightKeyword, m_OrganTypeId, short.Parse(ddlProvinces.SelectedValue), m_DocRelateId,
                m_DisplayTypeId, "", m_HasDocFile);
                
                //m_Docs.StatisticByReviewStatus(ref RowCount, byte.Parse(ddlDocGroups.SelectedValue), 1, byte.Parse(ddlLanguage.SelectedValue));
            rptStatusCount.DataBind(); 
            
            SetCurentData();
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
    }
    protected void SetCurentData()
    {
        Session["Docs-ddlDocTypes"] = ddlDocTypes.SelectedValue;        
        Session["Docs-ddlCrUserId"] = ddlCrUserId.SelectedValue;
        Session["Docs-ddlDataSources"] = ddlDataSources.SelectedValue;
        Session["Docs-ddlDocGroups"] = ddlDocGroups.SelectedValue;
        Session["Docs-ddlEffectStatus"] = ddlEffectStatus.SelectedValue;
        Session["Docs-ddlFields"] = ddlFields.SelectedValue;
        Session["Docs-ddlLanguage"] = ddlLanguage.SelectedValue;
        Session["Docs-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["Docs-ddlOrgans"] = ddlOrgans.SelectedValue;
        Session["Docs-ddlPageSize"] = ddlPageSize.SelectedValue;
        Session["Docs-ddlProvinces"] = ddlProvinces.SelectedValue;
        Session["Docs-ddlReviewStatus"] = ddlReviewStatus.SelectedValue;
        Session["Docs-ddlRevUserId"] = ddlRevUserId.SelectedValue;
        Session["Docs-ddlSearchByDate"] = ddlSearchByDate.SelectedValue;
        Session["Docs-ddlUpdUserId"] = ddlUpdUserId.SelectedValue;
        Session["Docs-ddlUseStatus"] = ddlUseStatus.SelectedValue;
        Session["Docs-txtDateFrom"] = txtDateFrom.Text;
        Session["Docs-txtDateTo"] = txtDateTo.Text;
        Session["Docs-txtSearch"] = txtSearch.Text;
        Session["Docs-rblFindTypes"] = rblFindTypes.SelectedValue;
        Session["Docs-ckbIsSearchExact"] = ckbIsSearchExact.Checked;
        
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
            Docs m_DataItem = (Docs)e.Row.DataItem;
            DocId = m_DataItem.DocId;
            Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
            if (rptLanguage != null)
            {
                rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                rptLanguage.DataBind();
            }

        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        
        try
        {
            Docs m_Docs = new Docs();            
            m_Docs.Get(short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString()), byte.Parse(ddlLanguage.SelectedValue));
            if (m_Docs.ReviewStatusId != 2)
            {
                m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                m_Docs.DocId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_Docs.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
        BindData();
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

    private void Review_Click(byte ReviewStatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
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
                        byte LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        int DocId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Docs = m_Docs.Get(DocId, LanguageId);
                        m_Docs.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_Docs.UpdateReviewStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        CountSuc++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
        Message = "Duyệt thành công " + CountSuc.ToString() + " văn bản. ";
        if(ReviewStatusId != 2)
            Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " văn bản. ";
        BindData();
        //lblMsg.Text = Message;
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }

    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);
        BindData();
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
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
        string Message = "";
        int CountSuc = 0;
        int CountFalse = 0;
        int CountFalseByStatus = 0;
        try
        {
            
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            int DelId = 0;
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                Docs m_Docs = new Docs();
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        DelId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Docs.DocId = DelId;
                        m_Docs.LanguageId = LanguageId;
                        m_Docs = m_Docs.Get();
                        if (m_Docs.ReviewStatusId != 2)
                        {
                            m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                            m_Docs.DocId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                            SysMessageTypeId = m_Docs.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                            CountSuc++;
                        }
                        else
                        {
                            CountFalseByStatus++;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.AlertException(ex.Message.ToString(), this);
        }
        Message = "Xóa thành công " + CountSuc.ToString() + " văn bản. ";
        if (CountFalseByStatus > 0)
        {
            Message += "Xóa thất bại " + CountFalseByStatus.ToString() + " văn bản do đang ở trạng thái đã duyệt. ";
        }

        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
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
        DropDownListHelpers.DDLDocTypeDisplays_BindByCulture(ddlDocTypes, DocTypeDisplays.Static_GetList_ByDisplayTypeId(displayTypeId), "...");
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

    protected bool HasProperties(string property="")
    {
        return !string.IsNullOrEmpty(property) && property.Equals("1");
    }

    protected string getClassSeo(Object MetaTitle, Object UpdUserId)
    {
        int UpdUserIdOld = int.Parse(UpdUserId.ToString());
        
        if(UpdUserIdOld < 132 && !Request.Url.Host.ToString().Contains("luatvietnam"))
        {
            return "notmodify";
        }
        else if (String.IsNullOrEmpty(MetaTitle.ToString()))
        {
            return "active";
        }
        else
        {
            return "";
        }

    }
}

