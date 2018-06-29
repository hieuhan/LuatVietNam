
/* ********************************************************************************
'     Document    :  DataDictionariesEdit.aspx.cs
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
public partial class admin_pages_DataDictionariesEdit : System.Web.UI.Page
{

    private int ActUserId = 0;
    protected short DataDictionaryTypeId = 0;
    protected int DataDictionaryId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (Request.Params["DataDictionaryTypeId"] != null && Request.Params["DataDictionaryTypeId"].ToString() != "")
            {
                DataDictionaryTypeId = short.Parse(Request.Params["DataDictionaryTypeId"].ToString());
            }
            if (Request.Params["id"] != null && Request.Params["id"].ToString() != "")
            {
                DataDictionaryId = int.Parse(Request.Params["id"].ToString());
            }
            if (!IsPostBack)
            {
                DropDownListHelpers.DDL_Bind(ddlDataDictionaryTypeId, DataDictionaryTypes.Static_GetList(), "", DataDictionaryTypeId.ToString());
                bindData();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    //--------------------------------------------------------------------------------

    private void bindData()
    {
        if (DataDictionaryId > 0)
        {
            DataDictionaries m_DataDictionaries = new DataDictionaries();
            m_DataDictionaries.DataDictionaryId = DataDictionaryId;
            m_DataDictionaries = m_DataDictionaries.Get();

            DropDownListHelpers.DDL_SetSelected(ddlDataDictionaryTypeId, m_DataDictionaries.DataDictionaryTypeId.ToString());
            txDataDictionaryName.Text = m_DataDictionaries.DataDictionaryName.ToString();
            txDataDictionaryDesc.Text = m_DataDictionaries.DataDictionaryDesc.ToString();
            txMinValue.Text = m_DataDictionaries.MinValue.ToString();
            txMaxValue.Text = m_DataDictionaries.MaxValue.ToString();
            txDisplayOrder.Text = m_DataDictionaries.DisplayOrder.ToString();
        }
    }
    //--------------------------------------------------------------------------------
    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        short SysMessageId = 0;
        try
        {
            DataDictionaries m_DataDictionaries = new DataDictionaries();
            m_DataDictionaries.DataDictionaryId = DataDictionaryId;
            m_DataDictionaries.CrUserId = ActUserId;
            m_DataDictionaries.DataDictionaryTypeId = System.Int16.Parse(ddlDataDictionaryTypeId.SelectedItem.Value);
            m_DataDictionaries.DataDictionaryName = txDataDictionaryName.Text;
            m_DataDictionaries.DataDictionaryDesc = txDataDictionaryDesc.Text;
            m_DataDictionaries.MinValue = txMinValue.Text == "" ? 0 : int.Parse(txMinValue.Text);
            m_DataDictionaries.MaxValue = txMaxValue.Text == "" ? 0 : int.Parse(txMaxValue.Text);
            m_DataDictionaries.DisplayOrder = txDisplayOrder.Text == "" ? 10 : int.Parse(txDisplayOrder.Text);
            //m_DataDictionaries.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (DataDictionaryId > 0)
            {
                m_DataDictionaries.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                m_DataDictionaries.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            StringBuilder csText = new StringBuilder();
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            csText.Clear();
            csText.Append("<script type=\"text/javascript\">");
            csText.Append("window.parent.jQuery('#divEdit').dialog('close');");
            csText.Append("</script>");
            cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "system_message", csText.ToString());
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    //--------------------------------------------------------------------------------

    
}