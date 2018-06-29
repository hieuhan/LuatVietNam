using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_VoteContentsEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    //private byte ApplicationTypeId = 0;
    private int VoteContentId = 0;
    public int VoteId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["VoteContentId"] != null && Request.Params["VoteContentId"].ToString() != "")
        {
            VoteContentId = short.Parse(Request.Params["VoteContentId"].ToString());
        }
        if (Request.Params["VoteId"] != null && Request.Params["VoteId"].ToString() != "")
        {
            VoteId = int.Parse(Request.Params["VoteId"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            if (Request.QueryString["VoteContentId"] == null)
            {
                cblAddOther.Visible = true;
            }
            else
            {
                cblAddOther.Visible = false;
                BindData();
            }
        }
    }
    private void BindData()
    {
        try
        {
            if (VoteContentId > 0)
            {
                VoteContents m_VoteContents = new VoteContents();
                m_VoteContents = m_VoteContents.Get(VoteId, VoteContentId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_VoteContents.VoteContentId > 0)
                {
                    txtVoteContent.Text = m_VoteContents.VoteContent.ToString();
                    txtDisplayOrder.Text = m_VoteContents.DisplayOrder.ToString();
                    txtVoteCounter.Text = m_VoteContents.VoteCounter.ToString();                    
                }
                else
                {
                    txtVoteContent.Text = "";
                    txtDisplayOrder.Text = "";
                    txtVoteCounter.Text = "";     
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtVoteContent.Text.Trim() == "")
            //{
            //    lblMsg.Text = "Nội dung bình chọn không được để trống";
            //    return;
            //}
            short SysMessageId = 0;
            byte SysMessageTypeId = 0;
            VoteContents m_VoteContents = new VoteContents();
            m_VoteContents.VoteContentId = VoteContentId;
            m_VoteContents.VoteId = VoteId;
            m_VoteContents.VoteContent = txtVoteContent.Text.ToString() ;
            m_VoteContents.DisplayOrder = txtDisplayOrder.Text=="" ? 0 : int.Parse(txtDisplayOrder.Text.ToString());
            m_VoteContents.VoteCounter = txtVoteCounter.Text =="" ? 0 : int.Parse(txtVoteCounter.Text.ToString());
            m_VoteContents.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            SysMessageTypeId=m_VoteContents.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", sms.common.SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 2)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        VoteContentId > 0 ? "Cập nhật nội dung bình luận thành công." : "Thêm mới nội dung bình luận thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    VoteContentId > 0 ? "Cập nhật nội dung bình luận thành công." : "Thêm mới nội dung bình luận thành công.", this);
            }
            //JSAlertHelpers.close(this);
            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private void ClearForm()
    {
        txtVoteContent.Text = "";
        txtDisplayOrder.Text = "";
        txtVoteCounter.Text = "";
        ddlLanguage.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
}