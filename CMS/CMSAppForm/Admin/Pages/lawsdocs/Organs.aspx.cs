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

public partial class Admin_Organs : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short OrganId = 0;
    protected byte LanguageId = 0;
    protected List<Users> l_Users;
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
       
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrganTypes_BindByCulture(ddlOrganTypes, OrganTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Organs"), "");
            if (Session["Organ-ddlLanguage"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("OrgansEdit.aspx"))
            {
                ddlLanguage.SelectedValue = Session["Organ-ddlLanguage"].ToString();
                ddlLanguage.SelectedValue = Session["Organ-ddlOrderBy"].ToString();
                ddlLanguage.SelectedValue = Session["Organ-ddlOrganTypes"].ToString();
                ddlLanguage.SelectedValue = Session["Organ-ddlReviewStatus"].ToString();
                txtSearch.Text = Session["Organ-Key"].ToString();
            }

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
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
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
            lblTong.Text = (string.Format("{0:#,#}", RowCount) != "" ? string.Format("{0:#,#}", RowCount) : "0");
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            SetCurentData();
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void SetCurentData()
    {
        Session["Organ-ddlLanguage"] = ddlLanguage.SelectedValue;
        Session["Organ-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["Organ-ddlOrganTypes"] = ddlOrganTypes.SelectedValue;
        Session["Organ-ddlReviewStatus"] = ddlReviewStatus.SelectedValue;
        Session["Organ-Key"] = txtSearch.Text;
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
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        try
        {
            ICSoft.LawDocsLib.Organs m_Organs = new ICSoft.LawDocsLib.Organs();
            m_Organs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Organs.OrganId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Organs.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 2) Message = "Xóa thành công CQBH. ";
            else Message = "Xóa thất bại do cơ quan ban hành đã được sử dụng";
            //lblMsg.Text = Message;
            JSAlertHelpers.ShowNotify("15", "success", Message, this);
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
        //CustomPaging.PageIndex = 1;
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

    private void Review_Click(byte ReviewStatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
        try
        {
            ICSoft.LawDocsLib.Organs m_Organs = new ICSoft.LawDocsLib.Organs();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        
                        byte LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        short OrganId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Organs = m_Organs.Get(OrganId, LanguageId);
                        m_Organs.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_Organs.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        CountSuc++;
                    }
                }
            }
            Message = "Duyệt thành công " + CountSuc.ToString() + " CQBH. ";
            if (ReviewStatusId != 2)
                Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " CQBH. ";
            BindData();
            //lblMsg.Text = Message;
            JSAlertHelpers.ShowNotify("15", "success", Message, this);

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
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
        int CountFail = 0;
        try
        {
            ICSoft.LawDocsLib.Organs m_Organs = new ICSoft.LawDocsLib.Organs();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Organs.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        m_Organs.OrganId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Organs.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) CountFail++;
                        else CountSuc++;
                    }
                }
            }
            Message = "Xóa thành công " + CountSuc.ToString() + " CQBH. ";
            //lblMsg.Text = Message;
            JSAlertHelpers.ShowNotify("15", "success", Message, this);

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
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
}

