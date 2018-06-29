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
public partial class Admin_DocRelatesGenBTHLSelect : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected int DocRelateId = 0;
    protected byte LanguageId = 0;
    public string m_RelateTypeId = "";
    public char chr;
    protected List<Users> l_Users;
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
    private List<Languages> l_Language = new List<Languages>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...", "2");
            int m_RowCount = 0;
            RelateTypes m_RelateTypes = new RelateTypes();
            m_RelateTypes.DocGroupId = 1;
            List<RelateTypes> l_RelateTypes = m_RelateTypes.GetPage("", "", "", 100, 0, ref m_RowCount);

            CheckBoxListHelpers.Bind(cblRelateTypes, l_RelateTypes, "");
            CheckBoxListHelpers.SetSelected(cblRelateTypes, "2"); //Het hieu luc
            CheckBoxListHelpers.SetSelected(cblRelateTypes, "7"); //Het hieu luc 1 phan
            CheckBoxListHelpers.SetSelected(cblRelateTypes, "4"); //Bi sua doi
            txtDateFrom.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
            txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
            chr = Convert.ToChar(39);
            DocRelates m_DocRelates = new DocRelates();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            l_RelateTypes = RelateTypes.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            int RowCount = 0;
            int m_DocId = 0;
            int m_DocReferenceId = 0;
            m_RelateTypeId = "";
            for (int i = 0; i <= cblRelateTypes.Items.Count - 1; i++)
            {
                if (cblRelateTypes.Items[i].Selected == true)
                {
                    if (m_RelateTypeId != "")
                    {
                        m_RelateTypeId = m_RelateTypeId + "," + cblRelateTypes.Items[i].Value;
                    }
                    else
                    {
                        m_RelateTypeId = cblRelateTypes.Items[i].Value;
                    }
                }
            }
            byte m_LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            string m_OrderBy = "";
            string m_SearchByDate= ddlSearchByDate.SelectedValue;
            int m_CrUserId = 0;
            short m_Field = 0;
            string m_DateFrom = txtDateFrom.Text.Trim();
            string m_DateTo = txtDateTo.Text.Trim();
            m_grid.DataSource = m_DocRelates.DocRelateArticles_SearchDocRelate(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_LanguageId, m_Field, ArticleId,"", m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
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
            
            DocRelates m_DataItem = (DocRelates)e.Row.DataItem;
            DocRelateId = m_DataItem.DocRelateId;
        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            DocRelates m_DocRelates = new DocRelates();
            m_DocRelates.DocRelateArticles_Insert_Quick(ActUserId, ArticleId, int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString()));
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        lblMsg.Text = "Thêm văn bản thành công.";
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

   
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlSearchByDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void cblRelateTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        m_RelateTypeId = "";
        for (int i = 0; i <= cblRelateTypes.Items.Count - 1; i++)
        {
            if (cblRelateTypes.Items[i].Selected == true)
            {
                if (m_RelateTypeId != "")
                {
                    m_RelateTypeId = m_RelateTypeId + "," + cblRelateTypes.Items[i].Value;
                }
                else
                {
                    m_RelateTypeId = cblRelateTypes.Items[i].Value;
                }
            }
        }
    }
}

