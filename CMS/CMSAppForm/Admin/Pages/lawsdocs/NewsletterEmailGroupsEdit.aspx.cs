using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_NewsletterEmailGroupsEdit : System.Web.UI.Page
{
    private int NewsletterEmailId = 0;
    protected string StrDateTime="";
    private int ActUserId = 0;
    protected List<NewsletterGroups> l_NewsletterGroups = new List<NewsletterGroups>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["NewsletterEmailId"] != null && Request.Params["NewsletterEmailId"].ToString() != "")
        {
            NewsletterEmailId = int.Parse(Request.Params["NewsletterEmailId"].ToString());
        }
        if (!IsPostBack)
        {
           BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (NewsletterEmailId > 0)
            {
                StrDateTime ="01/01/" + (DateTime.Now.Year + 1).ToString();
                NewsletterEmails m_NewsletterEmails = new NewsletterEmails();
                m_NewsletterEmails = m_NewsletterEmails.Get(NewsletterEmailId);
                lblEmailShow.Text = m_NewsletterEmails.Email;
                l_NewsletterGroups = NewsletterGroups.Static_GetList();
                NewsletterEmailGroups m_NewsletterEmailGroups = new NewsletterEmailGroups();
                m_grid.DataSource = m_NewsletterEmailGroups.GetByAllGroup(ActUserId, NewsletterEmailId);
                m_grid.DataBind();
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
            short SysMessageId = 0;
            if (Request.Params["NewsletterEmailId"] != null && Request.Params["NewsletterEmailId"].ToString() != "")
            {
                NewsletterEmailId = int.Parse(Request.Params["NewsletterEmailId"].ToString());
            }
            NewsletterEmailGroups m_NewsletterEmailGroups = new NewsletterEmailGroups();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                TextBox txtExpireTime = (TextBox)m_Row.FindControl("txtExpireTime");
                Label lblNewsletterGroupId = (Label)m_Row.FindControl("lblNewsletterGroupId");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_NewsletterEmailGroups.NewsletterEmailId = NewsletterEmailId;
                        m_NewsletterEmailGroups.NewsletterGroupId = short.Parse(lblNewsletterGroupId.Text);
                        m_NewsletterEmailGroups.ExpireTime = txtExpireTime.Text != "" ? Convert.ToDateTime(txtExpireTime.Text) : DateTime.MinValue;
                        m_NewsletterEmailGroups.NewsletterEmailGroupId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_NewsletterEmailGroups.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId); 
                    }
                    else
                    {
                        // Xóa không check
                        m_NewsletterEmailGroups.NewsletterEmailGroupId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageId = m_NewsletterEmailGroups.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }

                }
                else
                {
                    // Xóa không check
                    m_NewsletterEmailGroups.NewsletterEmailGroupId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                    SysMessageId = m_NewsletterEmailGroups.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                }
               
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
}