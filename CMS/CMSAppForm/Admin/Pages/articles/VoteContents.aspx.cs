using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_VoteContents : System.Web.UI.Page
{
    protected byte LanguageId = 0;
    protected int VoteId = 0;
    protected int VoteContentId = 0;
    private int ActUserId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["VoteId"] != null && Request.Params["VoteId"].ToString() != "")
        {
            VoteId = short.Parse(Request.Params["VoteId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (!IsPostBack)
        {
           // DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (VoteId > 0)
            {
                VoteContents m_VoteContents = new VoteContents();
                int m_VoteContentId = 0;
                m_grid.DataSource = m_VoteContents.VoteContents_GetList(ActUserId, "DisplayOrder", LanguageId, VoteId, m_VoteContentId);
                m_grid.DataBind();
                lblTong.Text =m_grid.Rows.Count.ToString();                
            }
            
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

            VoteContentId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());           
        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            VoteContents m_VoteContents = new VoteContents();
            m_VoteContents.LanguageId = LanguageId;
            m_VoteContents.VoteContentId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            Literal lblTermName = (Literal)m_grid.Rows[e.RowIndex].FindControl("ltVoteContent");
            SysMessageTypeId = m_VoteContents.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa nội dung bình luận : {0}", sms.common.SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2)
            {
                deleteMessage = string.Format("Xóa nội dung bình luận <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblTermName.Text) ? lblTermName.Text : "");
            }
            JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);

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
        BindData();
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            VoteContents m_VoteContents = new VoteContents();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_VoteContents.LanguageId = LanguageId;
                        m_VoteContents.VoteContentId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_VoteContents.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
}