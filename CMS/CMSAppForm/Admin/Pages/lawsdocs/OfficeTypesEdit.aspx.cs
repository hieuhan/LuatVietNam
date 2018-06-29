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

public partial class Admin_OfficeTypesEdit : System.Web.UI.Page
{
    private byte OfficeTypeId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["OfficeTypeId"] != null && Request.Params["OfficeTypeId"].ToString() != "")
        {
            OfficeTypeId = byte.Parse(Request.Params["OfficeTypeId"].ToString());
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
            if (OfficeTypeId > 0)
            {
                OfficeTypes m_OfficeTypes = new OfficeTypes();
                m_OfficeTypes = m_OfficeTypes.Get(OfficeTypeId);
                if (m_OfficeTypes.OfficeTypeId > 0)
                {
                    txtOfficeTypeName.Text = m_OfficeTypes.OfficeTypeName;
                    txtOfficeTypeDesc.Text = m_OfficeTypes.OfficeTypeDesc.ToString();
                    txtDisplayOrder.Text = m_OfficeTypes.DisplayOrder.ToString();
                }
                else
                {
                    txtOfficeTypeName.Text = "";
                    txtOfficeTypeDesc.Text = "";
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
            OfficeTypes m_OfficeTypes = new OfficeTypes();
            m_OfficeTypes.OfficeTypeName = txtOfficeTypeName.Text.Trim();
            m_OfficeTypes.OfficeTypeDesc = txtOfficeTypeDesc.Text.Trim();
            m_OfficeTypes.OfficeTypeId = OfficeTypeId;
            m_OfficeTypes.DisplayOrder = (txtDisplayOrder.Text.Trim() != "") ? byte.Parse(txtDisplayOrder.Text) : byte.Parse("0");
            m_OfficeTypes.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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