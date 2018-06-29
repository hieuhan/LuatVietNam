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

public partial class Admin_Pages_lawsdocs_popupDocuments : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected byte LanguageId = 0;
    protected int docIdRequest = 0;
    protected List<Users> l_Users;
    protected List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
    private List<Languages> l_Language = new List<Languages>();
    protected List<EffectStatus> l_EffectStatus = new List<EffectStatus>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["docId"] != null && Request.Params["docId"].ToString() != "")
        {
            docIdRequest = int.Parse(Request.Params["docId"].ToString());
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
            Docs m_Docs = new Docs();
            int RowCount = 0;
            l_Language = Languages.Static_GetList();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            l_EffectStatus = EffectStatus.Static_GetList();
            //l_UseStatus = UseStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            string m_OrderBy = "";
            int m_DocId = 0;
            string m_DocGUId = "";
            short m_DisplayTypeId = 0;
            string m_SearchKeyword = "";
            string m_DocName = "";
            string m_DocIdentity = "";
            byte m_IsSearchExact = 0;
            byte UseStatusId = 0;
            if (ckbIsSearchExact.Checked == true)
            {
                m_IsSearchExact = 1;
            }
            byte m_HighlightKeyword = 1;
            byte m_HasDocFile = 0;
            switch (rblFindTypes.SelectedValue)
            {
                case "DocIdentity":
                    m_DocIdentity = txtSearchKeyword.Text.Trim();
                    break;
                case "DocName":
                    m_DocName = txtSearchKeyword.Text.Trim();
                    break;
                default:
                    m_SearchKeyword = txtSearchKeyword.Text.Trim();
                    break;
            }
            byte m_OrganTypeId = 0, EffectStatusId = 0;
            short m_SignerId = 0, DataSourceId = 0, FieldId = 0, OrganId = 0;
            int m_DocRelateId = 0, CrUserId= 0, IssueYear = 0;;
            byte m_ReviewStatusId = 0, DocTypeId= 0;
            int m_UpdUserId = 0;
            int m_RevUserId = 0;
            string m_SearchByDate = "";
            string m_DateFrom = "";
            string m_DateTo = "";
            m_Docs.DocGroupId = 0;
            List<Docs> l_Docs = new List<Docs>();
                l_Docs = m_Docs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_SearchKeyword, m_IsSearchExact, m_HighlightKeyword,
                                                m_DocId, m_DocGUId, m_DocName, m_DocIdentity, DocTypeId, DataSourceId, FieldId, m_OrganTypeId, OrganId, m_SignerId, m_DocRelateId,
                                                m_DisplayTypeId, UseStatusId, EffectStatusId, m_ReviewStatusId, m_HasDocFile, CrUserId, m_UpdUserId, m_RevUserId, IssueYear, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);

            m_grid.DataSource = l_Docs;
            m_grid.AllowPaging = m_grid.PageSize <= RowCount;
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

    protected void lbSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected bool HasProperties(string property = "")
    {
        return !string.IsNullOrEmpty(property) && property.Equals("1");
    }

    protected string getClassSeo(Object MetaTitle, Object UpdUserId)
    {
        int UpdUserIdOld = int.Parse(UpdUserId.ToString());

        if (UpdUserIdOld < 132 && !Request.Url.Host.ToString().Contains("luatvietnam"))
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

    protected void ckbIsSearchExact_CheckedChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void m_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int docId = (int)m_grid.DataKeys[e.Row.RowIndex].Value;
                string docName = DataBinder.Eval(e.Row.DataItem, "DocName").ToString();
                string Result = DataBinder.Eval(e.Row.DataItem, "Result").ToString();
                Label lblChkSelect = (Label)(e.Row.FindControl("lblChkSelect"));
                if (lblChkSelect == null)
                {
                    return;
                }
                lblChkSelect.Text = @"<input type='radio' name='ckuserid' " + (docIdRequest == docId ? "checked" : "") + " onclick='docSelect(this, " + docId + " , \"" + docName + "\",\"" + Result + "\")' />";
                if (docIdRequest == docId) {
                    hdfDocId.Value = docIdRequest.ToString();
                    hdfDocName.Value = docName;
                    hdfResult.Value = Result;
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}