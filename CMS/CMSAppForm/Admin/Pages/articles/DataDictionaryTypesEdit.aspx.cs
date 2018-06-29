
/* ********************************************************************************
'     Document    :  DataDictionaryTypesEdit.aspx.cs
' ********************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Reflection;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class admin_pages_DataDictionaryTypesEdit : System.Web.UI.Page
{
    String csName = "csbMessage";
    Type csType;
    ClientScriptManager cs;
    StringBuilder csText = new StringBuilder();
    private short DataDictionaryTypeId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DataDictionaryTypeId"] != null && Request.Params["DataDictionaryTypeId"].ToString() != "")
        {
            DataDictionaryTypeId = short.Parse(Request.Params["DataDictionaryTypeId"].ToString());
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
            if (DataDictionaryTypeId > 0)
            {
                DataDictionaryTypes m_DataDictionaryTypes = new DataDictionaryTypes();
                m_DataDictionaryTypes = m_DataDictionaryTypes.Get(DataDictionaryTypeId);
                if (m_DataDictionaryTypes.DataDictionaryTypeId > 0)
                {
                    txtDataDictionaryTypeName.Text = m_DataDictionaryTypes.DataDictionaryTypeName.ToString();
                    txtDataDictionaryTypeDesc.Text = m_DataDictionaryTypes.DataDictionaryTypeDesc.ToString();
                }
            }

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    //--------------------------------------------------------------------------------
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            short SysMessageId = 0;
            DataDictionaryTypes m_DataDictionaryTypes = new DataDictionaryTypes();
            m_DataDictionaryTypes.DataDictionaryTypeName = txtDataDictionaryTypeName.Text;
            m_DataDictionaryTypes.DataDictionaryTypeDesc = txtDataDictionaryTypeDesc.Text;
            m_DataDictionaryTypes.DataDictionaryTypeId = DataDictionaryTypeId;
            if (DataDictionaryTypeId > 0)
            {
                m_DataDictionaryTypes.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                m_DataDictionaryTypes.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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