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
public partial class Admin_DocRelatesEditandReview : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int DocRelateId = 0;
    protected byte LanguageId = 0;
    public char chr;
    protected List<Users> l_Users;
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
    private List<Languages> l_Language = new List<Languages>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            CheckBoxListHelpers.Bind(cblRelateTypes, RelateTypes.Static_GetListOrderBy("DisplayOrder ASC"), "");
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
            string m_RelateTypeId = "";
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
            string m_DocIdentity = txtDocIdentity.Text.Trim();
            m_grid.DataSource = m_DocRelates.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, m_LanguageId, m_Field, m_DocId,m_DocIdentity, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
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
            m_DocRelates.Get(short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString()));
            if (m_DocRelates.ReviewStatusId != 2)
            {
                m_DocRelates.DocRelateId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_DocRelates.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
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

    private void Review_Click(byte ReviewStatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            DocRelates m_DocRelates = new DocRelates();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_DocRelates.DocRelateId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_DocRelates.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_DocRelates.UpdateReviewStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
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
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            DocRelates m_DocRelates = new DocRelates();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_DocRelates= m_DocRelates.Get(int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString()));
                        if (m_DocRelates.ReviewStatusId != 2)
                        {
                            m_DocRelates.DocRelateId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                            SysMessageTypeId = m_DocRelates.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
}

