using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_RegenciesEdit : System.Web.UI.Page
{
    private byte RegencyId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["RegencyId"] != null && Request.Params["RegencyId"].ToString() != "")
        {
            RegencyId = byte.Parse(Request.Params["RegencyId"].ToString());
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
            if (RegencyId > 0)
            {
                Regencies m_Regencies = new Regencies();
                m_Regencies = m_Regencies.Get(RegencyId);
                if (m_Regencies.RegencyId > 0)
                {
                    txtRegencyName.Text = m_Regencies.RegencyName;
                    txtRegencyDesc.Text = m_Regencies.RegencyDesc.ToString();
                    txtDisplayOrder.Text = m_Regencies.DisplayOrder.ToString();
                }
                else
                {
                    txtRegencyName.Text = "";
                    txtRegencyDesc.Text = "";
                    txtDisplayOrder.Text = "";
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
            short SysMessageId = 0;
            Regencies m_Regencies = new Regencies();
            m_Regencies.RegencyName = txtRegencyName.Text.Trim();
            m_Regencies.RegencyId = RegencyId;
            m_Regencies.RegencyDesc = (txtRegencyDesc.Text.Trim() != "") ? txtRegencyDesc.Text : "";
            m_Regencies.DisplayOrder = (txtDisplayOrder.Text.Trim() != "") ? byte.Parse(txtDisplayOrder.Text) : byte.Parse("0");
            m_Regencies.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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