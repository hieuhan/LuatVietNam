using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_PartnersEdit : System.Web.UI.Page
{
    //private byte LanguageId = 0;
    //private byte ApplicationTypeId = 0;
    private short PartnerId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["PartnerId"] != null && Request.Params["PartnerId"].ToString() != "")
        {
            PartnerId = short.Parse(Request.Params["PartnerId"].ToString());
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
            if (PartnerId > 0)
            {
                Partners m_Partners = new Partners();
                m_Partners = m_Partners.Get(PartnerId);
                if (m_Partners.PartnerId > 0)
                {
                    txtPartnerName.Text = m_Partners.PartnerName.ToString();
                    txtPartnerDesc.Text = m_Partners.PartnerDesc.ToString();
                    txtNotes.Text = m_Partners.Notes.ToString();
                }
                else
                {
                    txtPartnerName.Text = "";
                    txtPartnerDesc.Text = "";
                    txtNotes.Text = "";
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
            if (txtPartnerName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên khách hàng không được để trống";
                return;
            }
            short SysMessageId = 0;
            Partners m_Partners = new Partners();
            m_Partners.PartnerId = PartnerId;
            m_Partners.PartnerName = txtPartnerName.Text.ToString();
            m_Partners.PartnerDesc = txtPartnerDesc.Text.ToString();
            m_Partners.Notes = txtNotes.Text.ToString();
            m_Partners.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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