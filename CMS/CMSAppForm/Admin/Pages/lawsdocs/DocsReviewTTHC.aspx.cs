﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
using System.Data;
public partial class Admin_DocsReviewTTHC : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int DocId = 0;
    protected byte LanguageId = 0;
    public char chr;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<UseStatus> l_UseStatus = new List<UseStatus>();
    protected List<EffectStatus> l_EffectStatus = new List<EffectStatus>();
    protected List<ICSoft.LawDocsLib.Organs> l_Organs = new List<ICSoft.LawDocsLib.Organs>();
    protected List<Fields> l_Fields = new List<Fields>();
    string ReviewStatusIdList = "";
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
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlStatusUnCheck, ReviewStatus.Static_GetList(), "");
            ddlStatusUnCheck.SelectedValue = "1";
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
            ddlReviewStatus.SelectedValue = "1";
            bindCheckStatusV2();
            
            if (Session["TTHCRV-ddlDocTypes"] != null && Request.UrlReferrer != null && (Request.UrlReferrer.OriginalString.Contains("DocsEditInfo.aspx")
                || Request.UrlReferrer.OriginalString.Contains("DocsEditStep.aspx") || Request.UrlReferrer.OriginalString.Contains("DocRelatesEdit.aspx")
                || Request.UrlReferrer.OriginalString.Contains("DocsFielsEdit.aspx") || Request.UrlReferrer.OriginalString.Contains("DocsUpdateSummaryEdit.aspx")
                || Request.UrlReferrer.OriginalString.Contains("DocRelatesTTHCEdit.aspx") || Request.UrlReferrer.OriginalString.Contains("DocsContentRelation.aspx")
                || Request.UrlReferrer.OriginalString.Contains("DocsEditTTHC.aspx") || Request.UrlReferrer.OriginalString.Contains("DocsReviewTTHC.aspx")))
            {
                Section();   
            }
            else
            {
                CheckBoxListHelpers.SetSelected(cblReviewStatus, "1");
            }
            BindData();
        }
        if (!IsPostBack|| CustomPaging.ChangePage)
        {
            BindData();
        }
    }
    private void Section()
    {
        ddlDocTypes.SelectedValue = Session["TTHCRV-ddlDocTypes"].ToString();
        ddlCrUserId.SelectedValue = Session["TTHCRV-ddlCrUserId"].ToString();
        ddlDataSources.SelectedValue = Session["TTHCRV-ddlDataSources"].ToString();
        ddlEffectStatus.SelectedValue = Session["TTHCRV-ddlEffectStatus"].ToString();
        ddlFields.SelectedValue = Session["TTHCRV-ddlFields"].ToString();
        ddlLanguage.SelectedValue = Session["TTHCRV-ddlLanguage"].ToString();
        ddlOrderBy.SelectedValue = Session["TTHCRV-ddlOrderBy"].ToString();
        ddlOrgans.SelectedValue = Session["TTHCRV-ddlOrgans"].ToString();
        ddlPageSize.SelectedValue = Session["TTHCRV-ddlPageSize"].ToString();
        ddlProvinces.SelectedValue = Session["TTHCRV-ddlProvinces"].ToString();
        ddlReviewStatus.SelectedValue = Session["TTHCRV-ddlReviewStatus"].ToString();
        ddlRevUserId.SelectedValue = Session["TTHCRV-ddlRevUserId"].ToString();
        ddlSearchByDate.SelectedValue = Session["TTHCRV-ddlSearchByDate"].ToString();
        ddlUpdUserId.SelectedValue = Session["TTHCRV-ddlUpdUserId"].ToString();
        ddlUseStatus.SelectedValue = Session["TTHCRV-ddlUseStatus"].ToString();
        ReviewStatusIdList = Session["TTHCRV-ReviewStatusIdList"].ToString();
        string[] l_StatusId = ReviewStatusIdList.Split(',');
        if (l_StatusId != null)
            foreach (string m_StatusId in l_StatusId)
            {
                CheckBoxListHelpers.SetSelected(cblReviewStatus, m_StatusId);
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
            byte m_DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
            short m_DataSourceId = short.Parse(ddlDataSources.SelectedValue);
            short m_FieldId = short.Parse(ddlFields.SelectedValue);
            byte m_OrganTypeId = 0;
            short m_OrganId = short.Parse(ddlOrgans.SelectedValue);
            short m_SignerId = 0;
            int m_DocRelateId = 0;
            byte m_UseStatusId = byte.Parse(ddlUseStatus.SelectedValue);
            byte m_EffectStatusId = byte.Parse(ddlEffectStatus.SelectedValue);
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            short m_ProvinceId = short.Parse(ddlProvinces.SelectedValue);

            string m_SearchByDate = ddlSearchByDate.SelectedValue.ToString();
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();

            int m_CrUserId = int.Parse(ddlCrUserId.SelectedValue);
            int m_UpdUserId = int.Parse(ddlUpdUserId.SelectedValue);
            int m_RevUserId = int.Parse(ddlRevUserId.SelectedValue);
            //add
            m_Docs.DocGroupId = DocGroups.TTHC;
            m_grid.PageSize = int.Parse(ddlPageSize.SelectedValue);
            //string ReviewStatusIdList = "";
            foreach (ListItem m_ListItem in cblReviewStatus.Items)
            {
                if (m_ListItem.Selected)
                {
                    if (ReviewStatusIdList == "")
                    {
                        ReviewStatusIdList = m_ListItem.Value;
                    }
                    else
                    {
                        ReviewStatusIdList += "," + m_ListItem.Value;
                    }
                }
            }
            //List<Docs> l_Docs = new List<Docs>();
            //if (ddlProvinces.SelectedValue == "0")
            //{
            //    l_Docs = m_Docs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_SearchKeyword, m_IsSearchExact, m_HighlightKeyword,
            //                                    m_DocId, m_DocGUId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganTypeId, m_OrganId, m_SignerId, m_DocRelateId,
            //                                    m_DisplayTypeId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, m_HasDocFile, m_CrUserId, m_UpdUserId, m_RevUserId, IssueYear, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);


            //}
            //else
            //{
            //    short ProvicenId = short.Parse(ddlProvinces.SelectedValue);
            //    l_Docs = m_Docs.GetPageWithProvince(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_SearchKeyword, m_IsSearchExact, m_HighlightKeyword,
            //                                    m_DocId, m_DocGUId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganTypeId, m_OrganId, m_SignerId, m_DocRelateId,
            //                                    m_DisplayTypeId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, ProvicenId, m_HasDocFile, m_CrUserId, m_UpdUserId, m_RevUserId, IssueYear, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
            //} m_grid.DataSource = l_Docs;
            m_grid.DataSource = m_Docs.GetPageWithStatusList(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_SearchKeyword, m_IsSearchExact, m_HighlightKeyword,
                                                 m_DocId, m_DocGUId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganTypeId, m_OrganId, m_SignerId, m_DocRelateId,
                                                 m_DisplayTypeId, m_UseStatusId, m_EffectStatusId, ReviewStatusIdList, m_HasDocFile, m_CrUserId, m_UpdUserId, m_RevUserId, m_SearchByDate, m_DateFrom, m_DateTo, m_ProvinceId, DocGroups.TTHC, ref RowCount);

           

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
            SetCurentData();
            bindCheckStatusV2();
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private void bindCheckStatusV2()
    {
        Docs m_Docs = new Docs();
        int RowCount = 0;
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
        byte m_DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
        short m_DataSourceId = short.Parse(ddlDataSources.SelectedValue);
        short m_FieldId = short.Parse(ddlFields.SelectedValue);
        short m_OrganId = short.Parse(ddlOrgans.SelectedValue);
        byte m_OrganTypeId = 0;
        short m_SignerId = 0;
        int m_DocRelateId = 0;
        byte m_UseStatusId = byte.Parse(ddlUseStatus.SelectedValue);
        byte m_EffectStatusId = byte.Parse(ddlEffectStatus.SelectedValue);
        byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
        short m_ProvinceId = short.Parse(ddlProvinces.SelectedValue);
        //byte DocGroupId = 1;
        int m_CrUserId = int.Parse(ddlCrUserId.SelectedValue);
        int m_UpdUserId = int.Parse(ddlUpdUserId.SelectedValue);
        int m_RevUserId = int.Parse(ddlRevUserId.SelectedValue);
        string m_SearchByDate = ddlSearchByDate.SelectedValue.ToString();
        string m_DateFrom = txtDateFrom.Text.Trim();
        string m_DateTo = txtDateTo.Text.Trim();


        foreach (ListItem m_ListItem in cblReviewStatus.Items)
        {
            if (m_ListItem.Selected)
            {
                if (ReviewStatusIdList == "")
                {
                    ReviewStatusIdList = m_ListItem.Value;
                }
                else
                {
                    ReviewStatusIdList += "," + m_ListItem.Value;
                }
            }
        }
        m_grid.PageSize = int.Parse(ddlPageSize.SelectedValue);
        List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
        DataSet l_StatusDoc = m_Docs.StatisticByReviewStatusV2(ref RowCount, DocGroups.TTHC, 1, byte.Parse(ddlLanguage.SelectedValue), m_SearchKeyword,
                m_DocId, m_DocName, m_DocIdentity, byte.Parse(ddlDocTypes.SelectedValue), short.Parse(ddlDataSources.SelectedValue), short.Parse(ddlFields.SelectedValue), m_OrganId, m_SignerId, m_UseStatusId, m_EffectStatusId, 0,
                m_CrUserId, m_UpdUserId, m_RevUserId, m_SearchByDate, m_DateFrom, m_DateTo, CustomPaging.PageIndex - 1, m_OrderBy, m_DocGUId, m_IsSearchExact, m_HighlightKeyword, m_OrganTypeId, short.Parse(ddlProvinces.SelectedValue), m_DocRelateId,
                m_DisplayTypeId,"", m_HasDocFile);
        if (l_StatusDoc.Tables.Count > 0)
        {
            for (int index = 0; index < l_StatusDoc.Tables[0].Rows.Count; index++)
            {
                ReviewStatus m_ReviewStatus = new ReviewStatus();
                m_ReviewStatus.ReviewStatusId = byte.Parse(l_StatusDoc.Tables[0].Rows[index]["ReviewStatusId"].ToString());
                m_ReviewStatus.ReviewStatusDesc = l_StatusDoc.Tables[0].Rows[index]["Trang Thai"].ToString() + (l_StatusDoc.Tables[0].Rows[index]["So van ban"] == DBNull.Value ? " (0)" : "(" + l_StatusDoc.Tables[0].Rows[index]["So van ban"].ToString() + ")");
                l_ReviewStatus.Add(m_ReviewStatus);
            }

        }
        cblReviewStatus.DataSource = l_ReviewStatus;
        cblReviewStatus.DataBind();
        if (Session["TTHCRV-ddlDocTypes"] != null)
        {
            Section();
        }
    }
    private void bindCheckStatus()
    {
        Docs m_Docs = new Docs();
        int RowCount = 0;
        List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
        DataSet l_StatusDoc = m_Docs.StatisticByReviewStatus(ref RowCount, DocGroups.TTHC);
        if (l_StatusDoc.Tables.Count > 0)
        {
            for (int index = 0; index < l_StatusDoc.Tables[0].Rows.Count; index++)
            {
                ReviewStatus m_ReviewStatus = new ReviewStatus();
                m_ReviewStatus.ReviewStatusId = byte.Parse(l_StatusDoc.Tables[0].Rows[index]["ReviewStatusId"].ToString());
                m_ReviewStatus.ReviewStatusDesc = l_StatusDoc.Tables[0].Rows[index]["Trang Thai"].ToString() + (l_StatusDoc.Tables[0].Rows[index]["So van ban"] == DBNull.Value ? " (0)" : "(" + l_StatusDoc.Tables[0].Rows[index]["So van ban"].ToString() + ")");
                l_ReviewStatus.Add(m_ReviewStatus);
            }

        }
        cblReviewStatus.DataSource = l_ReviewStatus;
        cblReviewStatus.DataBind();
        if(Session["TTHCRV-ddlDocTypes"] != null)
        {
            Section();
        }
    }
    protected void SetCurentData()
    {
        Session["TTHCRV-ddlDocTypes"] = ddlDocTypes.SelectedValue;
        Session["TTHCRV-ddlCrUserId"] = ddlCrUserId.SelectedValue;
        Session["TTHCRV-ddlDataSources"] = ddlDataSources.SelectedValue;
        Session["TTHCRV-ddlEffectStatus"] = ddlEffectStatus.SelectedValue;
        Session["TTHCRV-ddlFields"] = ddlFields.SelectedValue;
        Session["TTHCRV-ddlLanguage"] = ddlLanguage.SelectedValue;
        Session["TTHCRV-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["TTHCRV-ddlOrgans"] = ddlOrgans.SelectedValue;
        Session["TTHCRV-ddlPageSize"] = ddlPageSize.SelectedValue;
        Session["TTHCRV-ddlProvinces"] = ddlProvinces.SelectedValue;
        Session["TTHCRV-ddlReviewStatus"] = ddlReviewStatus.SelectedValue;
        Session["TTHCRV-ddlRevUserId"] = ddlRevUserId.SelectedValue;
        Session["TTHCRV-ddlSearchByDate"] = ddlSearchByDate.SelectedValue;
        Session["TTHCRV-ddlUpdUserId"] = ddlUpdUserId.SelectedValue;
        Session["TTHCRV-ddlUseStatus"] = ddlUseStatus.SelectedValue;
        Session["TTHCRV-ReviewStatusIdList"] = ReviewStatusIdList;

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
        string Message = "";
        try
        {
            Docs m_Docs = new Docs();
            m_Docs = m_Docs.Get(short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString()), byte.Parse(ddlLanguage.SelectedValue));
            if (m_Docs.ReviewStatusId != 2)
            {
                m_Docs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                m_Docs.DocId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_Docs.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                Message = "Xóa thủ tục hành chính thành công.";
            }
            else Message = "Xóa thủ tục hành chính thất bại do đang ở trạng thái đã duyệt.";
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
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
                        m_Docs.DocId = DocId;
                        m_Docs.LanguageId = LanguageId;
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
            JSAlertHelpers.Alert(ex.Message, this);
        }
        Message = "Duyệt thành công " + CountSuc.ToString() + " thủ tục hành chính. ";
        if (ReviewStatusId != 2)
            Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " thủ tục hành chính.";
        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }

    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);
       
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        Review_Click(byte.Parse(ddlStatusUnCheck.SelectedValue));
       
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
            Docs m_Docs = new Docs();
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            int DelId = 0;
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
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
            JSAlertHelpers.Alert(ex.Message, this);
        }
        Message = "Xóa thành công " + CountSuc.ToString() + " thủ tục hành chính. ";
        if (CountFalseByStatus > 0)
        {
            Message += "Xóa thất bại " + CountFalseByStatus.ToString() + " thủ tục hành chính do đang ở trạng thái đã duyệt. ";
        }

        BindData();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    protected void cblReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = CustomPaging.PageIndex;
        BindData();
    }
    protected bool HasProperties(string property = "")
    {
        return !string.IsNullOrEmpty(property) && property.Equals("1");
    }
}

