using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_MessageSendShowContent : System.Web.UI.Page
{
    private int MessageSendId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["MessageSendId"] != null && Request.Params["MessageSendId"].ToString() != "")
        {
            MessageSendId = int.Parse(Request.Params["MessageSendId"].ToString());
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
            if (MessageSendId > 0)
            {
                MessageSends m_MessageSends = new MessageSends();
                m_MessageSends = m_MessageSends.Get(MessageSendId);
                lblTitleEdit.Text = m_MessageSends.Title;
                txtContent.Text = m_MessageSends.MsgContent.ToString();                
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void lbResend_Click(object sender, EventArgs e)
    {
        MessageSends m_MessageSends = new MessageSends();
        m_MessageSends.MessageSends_Resend(MessageSendId);
        JSAlertHelpers.Alert("Tạo email gửi lại thành công.", this);
    }
}