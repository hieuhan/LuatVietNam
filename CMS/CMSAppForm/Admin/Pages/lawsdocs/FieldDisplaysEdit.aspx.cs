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

public partial class Admin_FieldDisplaysEdit : System.Web.UI.Page
{
    private short FieldDisplayId = 0;
    private short FieldId = 0;
    private byte LanguageId = 0;
    private short DisplayTypeId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["FieldDisplayId"] != null && Request.Params["FieldDisplayId"].ToString() != "")
        {
            FieldDisplayId = short.Parse(Request.Params["FieldDisplayId"].ToString());
        }
        if (Request.Params["DisplayTypeId"] != null && Request.Params["DisplayTypeId"].ToString() != "")
        {
            DisplayTypeId = short.Parse(Request.Params["DisplayTypeId"].ToString());
        }
        if (Request.Params["FieldId"] != null && Request.Params["FieldId"].ToString() != "")
        {
            FieldId = short.Parse(Request.Params["FieldId"].ToString());
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
            if (FieldDisplayId > 0)
            {
                FieldDisplays m_FieldDisplays = new FieldDisplays();
                m_FieldDisplays = m_FieldDisplays.Get(FieldDisplayId);
                if (m_FieldDisplays.FieldDisplayId > 0)
                {
                    txtDisplayOrder.Text = m_FieldDisplays.DisplayOrder.ToString();
                    DropDownListHelpers.DDLDisplayTypes_BindByCulture(ddlDisplayTypes, DisplayTypes.Static_GetList(),"", m_FieldDisplays.DisplayTypeId.ToString());
                    DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(),"",  m_FieldDisplays.FieldId.ToString());
                    DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", m_FieldDisplays.LanguageId.ToString());
                }
                else
                {                    
                    txtDisplayOrder.Text = "";
                    ddlDisplayTypes.SelectedIndex = -1;
                    ddlFields.SelectedIndex = -1;
                    ddlLanguage.SelectedIndex = -1;
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
            FieldDisplays m_FieldDisplays = new FieldDisplays();
            m_FieldDisplays.DisplayOrder = (txtDisplayOrder.Text.Trim() != "") ? short.Parse(txtDisplayOrder.Text) : short.Parse("0");
            m_FieldDisplays.DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
            m_FieldDisplays.FieldId = short.Parse(ddlFields.SelectedValue);
            m_FieldDisplays.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_FieldDisplays.UpdateDisplayOrder(ConstantHelpers.Replicated, ActUserId);
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