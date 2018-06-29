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

public partial class Admin_OrganDisplaysEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int OrganDisplayId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["OrganDisplayId"] != null && Request.Params["OrganDisplayId"].ToString() != "")
        {
            OrganDisplayId = short.Parse(Request.Params["OrganDisplayId"].ToString());
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
            if (OrganDisplayId > 0)
            {
                OrganDisplays m_OrganDisplays = new OrganDisplays();
                m_OrganDisplays = m_OrganDisplays.Get(OrganDisplayId);
                if (m_OrganDisplays.OrganDisplayId > 0)
                {
                    txtDisplayOrder.Text = m_OrganDisplays.DisplayOrder.ToString();
                    DropDownListHelpers.DDLDisplayTypes_BindByCulture(ddlDisplayTypes, DisplayTypes.Static_GetList(),"", m_OrganDisplays.DisplayTypeId.ToString());
                    DropDownListHelpers.DDLOrgans_BindByCulture(ddlOrgans, ICSoft.LawDocsLib.Organs.Static_GetList(),"",  m_OrganDisplays.OrganId.ToString());
                    DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", m_OrganDisplays.LanguageId.ToString());
                }
                else
                {                    
                    txtDisplayOrder.Text = "";
                    ddlDisplayTypes.SelectedIndex = -1;
                    ddlOrgans.SelectedIndex = -1;
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
            OrganDisplays m_OrganDisplays = new OrganDisplays();
            m_OrganDisplays.DisplayOrder = (txtDisplayOrder.Text.Trim() != "") ? int.Parse(txtDisplayOrder.Text) : int.Parse("0");
            m_OrganDisplays.DisplayTypeId = short.Parse(ddlDisplayTypes.SelectedValue);
            m_OrganDisplays.OrganId = short.Parse(ddlOrgans.SelectedValue);
            m_OrganDisplays.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_OrganDisplays.UpdateDisplayOrder(ConstantHelpers.Replicated, ActUserId);
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