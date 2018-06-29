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

public partial class Admin_DocTypeDisplaysEdit : System.Web.UI.Page
{
    private short DocTypeDisplayId = 0;
    private byte DocTypeId = 0;
    private byte LanguageId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DocTypeDisplayId"] != null && Request.Params["DocTypeDisplayId"].ToString() != "")
        {
            DocTypeDisplayId = short.Parse(Request.Params["DocTypeDisplayId"].ToString());
        }
        if (Request.Params["DocTypeId"] != null && Request.Params["DocTypeId"].ToString() != "")
        {
            DocTypeId = byte.Parse(Request.Params["DocTypeId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
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
            if (DocTypeDisplayId > 0)
            {
                DocTypeDisplays m_DocTypeDisplays = new DocTypeDisplays();
                m_DocTypeDisplays = m_DocTypeDisplays.Get(DocTypeDisplayId);
                if (m_DocTypeDisplays.DocTypeDisplayId > 0)
                {
                    txtDisplayOrder.Text = m_DocTypeDisplays.DisplayOrder.ToString();
                    DropDownListHelpers.DDLDisplayTypes_BindByCulture(ddlDisplayTypes, DisplayTypes.Static_GetList(),"", m_DocTypeDisplays.DisplayTypeId.ToString());
                    DropDownListHelpers.DDLDocTypes_BindByCulture(ddlDocTypes, DocTypes.Static_GetList(),"",  m_DocTypeDisplays.DocTypeId.ToString());
                    DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", m_DocTypeDisplays.LanguageId.ToString());
                }
                else
                {                    
                    txtDisplayOrder.Text = "";
                    ddlDisplayTypes.SelectedIndex = -1;
                    ddlDocTypes.SelectedIndex = -1;
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
            DocTypeDisplays m_DocTypeDisplays = new DocTypeDisplays();
            m_DocTypeDisplays.DisplayOrder = (txtDisplayOrder.Text.Trim() != "") ? short.Parse(txtDisplayOrder.Text) : short.Parse("0");
            m_DocTypeDisplays.DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
            m_DocTypeDisplays.DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
            m_DocTypeDisplays.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_DocTypeDisplays.UpdateDisplayOrder(ConstantHelpers.Replicated, ActUserId);
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