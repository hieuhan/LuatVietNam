using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
using sms.common;
public partial class Admin_DocRelatesEdit : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int DocId = 0;
    protected byte DocGroupId = 0;
    protected string SearchKeyword = string.Empty;
    protected int DocRelateId = 0;
    public byte RelateTypeId = 0;
    protected byte LanguageId = 0;
    protected int EditIndex = -1;
    public char chr;
    protected List<Users> l_Users;
    protected List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
    private List<Languages> l_Language = new List<Languages>();
    protected List<EffectStatus> l_EffectStatus = new List<EffectStatus>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<UseStatus> l_UseStatus = new List<UseStatus>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
        {
            DocId = int.Parse(Request.Params["DocId"].ToString());
        }
        if (Request.Params["DocGroupId"] != null && Request.Params["DocGroupId"].ToString() != "")
        {
            DocGroupId = byte.Parse(Request.Params["DocGroupId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["RelateTypeId"] != null && Request.Params["RelateTypeId"].ToString() != "")
        {
            RelateTypeId = byte.Parse(Request.Params["RelateTypeId"].ToString());
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
            chr = Convert.ToChar(39);
            Docs m_Docs = new Docs();
            int RowCount = 0;
            l_Language = Languages.Static_GetList();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            l_EffectStatus = EffectStatus.Static_GetList();
            l_UseStatus = UseStatus.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            string m_OrderBy = "";
            string mDocId = "";
            int m_DocId = 0;
            string m_DocGUId = "";
            short m_DisplayTypeId = 0;
            string m_SearchKeyword = "";
            string m_DocName = "";// txtSearchKeyword.Text;
            string m_DocIdentity = "";
            byte m_IsSearchExact = 0;
            byte m_HighlightKeyword = 0;
            byte m_HasDocFile = 0;
            byte m_DocTypeId = 0;
            short m_DataSourceId = 0;
            short m_FieldId = 0;
            byte m_OrganTypeId = 0;
            short m_OrganId = 0;
            short m_SignerId = 0;
            int m_DocRelateId = 0;
            byte m_UseStatusId = 2;
            byte m_EffectStatusId = 0;
            byte m_ReviewStatusId = 0;//ReviewStatusHelpers.Reviewed;
            int m_CrUserId = 0;
            string m_SearchByDate = "";
            string m_DateFrom = "";
            string m_DateTo = "";
            switch (rblFindTypes.SelectedValue)
            {
                case "DocIdentity":
                    m_DocIdentity = txtSearchKeyword.Text.Trim();
                    m_grid.DataSource = m_Docs.Docs_GetListByDocIdentity(ActUserId, int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), m_OrderBy, LanguageId, m_DocIdentity, m_ReviewStatusId);
                    break;
                //case "DocId":
                //    m_DocName = txtSearchKeyword.Text.Trim();
                //    break;
                default:
                    m_DocId = int.Parse(txtSearchKeyword.Text.Trim());
                    m_grid.DataSource = m_Docs.GetListByDocId(m_DocId, LanguageId);
                    break;
            }
            //m_Docs.DocGroupId = DocGroupId;
           
            m_grid.DataSource = m_Docs.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_SearchKeyword, m_IsSearchExact, m_HighlightKeyword,
                                                m_DocId, m_DocGUId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganTypeId, m_OrganId, m_SignerId, m_DocRelateId,
                                                m_DisplayTypeId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, m_HasDocFile, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
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
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
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

    protected void lbSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void lbAddDocRelates_Click(object sender, EventArgs e)
    {
        short SysMessageId = 0;
        byte SysMessageTypeId = 0;
        bool IsSelected = false;
        DocRelates m_DocRelates = new DocRelates();
        string message = "";
        int countSuccess = 0, countError = 0;
        try
        {
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        IsSelected = true;
                        m_DocRelates.DocId = DocId;
                        m_DocRelates.DocReferenceId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        if(Request["Revert"] != null)
                        {
                            m_DocRelates.DocId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                            m_DocRelates.DocReferenceId = DocId;
                        }
                        m_DocRelates.RelateTypeId = RelateTypeId;
                        m_DocRelates.ReviewStatusId = ConstantHelpers.REVIEW_STATUS_PENDING;
                        SysMessageTypeId = m_DocRelates.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        switch (SysMessageTypeId)
                        {
                            case 1:
                                countError++;
                                break;
                            case 0:
                            case 2:
                                countSuccess ++;
                                break;
                        }
                    }
                }
            }
            if (IsSelected)
            {
                if (countSuccess > 0)
                {
                    message += string.Format("Thêm thành công <i>{0}</i> {1}", countSuccess, " văn bản liên quan.");
                }
                if (countError > 0)
                {
                    message += string.Format("<i>{0}</i> văn bản liên quan chưa được thêm.", countError);
                }
                JSAlertHelpers.ShowNotify("15", "success", message, this);
                BindData();
            }
            else
            {
                JSAlertHelpers.ShowNotify("15", "warning", "Bạn chưa chọn văn bản liên quan.", this);
            }
            //JSAlertHelpers.Alert(Message, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
}

