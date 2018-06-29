/* ********************************************************************************
'     Document    :  DocsProcessAutoEdit.aspx.cs
' ********************************************************************************/
using System;
using System.Web.UI;
using System.Text;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class admin_pages_DocsProcessAutoEdit : System.Web.UI.Page
{		
	
    private int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
	{
        
		try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (!IsPostBack)
            {
                if(Request.QueryString["id"] == null)
                {
                }
                else
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
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }
    private void bindData()
        {    
            System.Int32 EditId;
            if(Request.QueryString["id"] == null)
            {
                return;
            }
            else
            {
                EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
                DocsProcessAuto m_DocsProcessAuto = new DocsProcessAuto();
                m_DocsProcessAuto.DocProcessAutoId = EditId;
                m_DocsProcessAuto=m_DocsProcessAuto.Get();
                txDocId.Text = m_DocsProcessAuto.DocId.ToString();
                txDocName.Text = m_DocsProcessAuto.DocName.ToString();
                txDocIdentity.Text = m_DocsProcessAuto.DocIdentity.ToString();
                txDocFilePath.Text = m_DocsProcessAuto.DocFilePath.ToString();
                txProcessTime.Text = m_DocsProcessAuto.ProcessTime.ToString("dd/MM/yyyy");
                txProcessStatusId.Text = m_DocsProcessAuto.ProcessStatusId.ToString();
                txCrUserId.Text = m_DocsProcessAuto.CrUserId.ToString();
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        DocsProcessAuto m_DocsProcessAuto = new DocsProcessAuto(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_DocsProcessAuto.DocProcessAutoId = EditId;
            m_DocsProcessAuto = m_DocsProcessAuto.Get();
        }        
        try
		{
                        
            m_DocsProcessAuto.CrUserId = ActUserId;
            
            m_DocsProcessAuto.DocId = int.Parse(txDocId.Text);
            
            if(txDocName.Text != "")
				m_DocsProcessAuto.DocName = txDocName.Text;
            
            if(txDocIdentity.Text != "")
				m_DocsProcessAuto.DocIdentity = txDocIdentity.Text;
            
            if(txDocFilePath.Text != "")
				m_DocsProcessAuto.DocFilePath = txDocFilePath.Text;
            
            if(txProcessTime.Text != "")
                m_DocsProcessAuto.ProcessTime = DateTime.Parse(txProcessTime.Text);
            
            
            m_DocsProcessAuto.ProcessStatusId = byte.Parse(txProcessStatusId.Text);
            
            m_DocsProcessAuto.DocProcessAutoId = EditId;
            SysMessageTypeId = m_DocsProcessAuto.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
    
   
    
 }
   