/* ********************************************************************************
'     Document    :  NotifyMessagesEdit.aspx.cs
' ********************************************************************************/
using System;
using System.Web.UI;
using System.Text;
using ICSoft.HelperLib;
using ICSoft.CMSLibWebNotify;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;

public partial class admin_pages_NotifyMessagesEdit : System.Web.UI.Page
{		
	
    private int ActUserId = 0;
    private int ArticleId = 0;
    private int DocId = 0;
    private byte LanguageId = 0;
    private string BackUrl = "Pages/Articles/Articles.aspx";
    private Articles mArticles = new Articles();
    private Docs mDocs = new Docs();
    protected string domainWEB = "https://luatvietnam.vn/";
    protected void Page_Load(object sender, EventArgs e)
	{
        
		try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
            {
                ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
            }
            if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
            {
                DocId = int.Parse(Request.Params["DocId"].ToString());
            }
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            if (Request.Params["BackUrl"] != null && Request.Params["BackUrl"].ToString() != "")
            {
                BackUrl = Request.Params["BackUrl"].ToString();
            }
            if (!IsPostBack)
            {
                    DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...");
                    DropDownListHelpers.DDL_Bind(ddlNotifyMessageTypeId, NotifyMessageTypes.Static_GetList(""),  " ... ", "0");
                    
            }
            ddlSite.Enabled = false;
            bindData();
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
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }
    private void bindData()
        {
            if (ArticleId > 0)
            {
                mArticles = mArticles.Get(ArticleId, 0, 0);
                if (mArticles.ArticleId <= 0)
                { Response.Redirect(CmsConstants.PRJ_ROOT + BackUrl); }
                ddlSite.SelectedValue = mArticles.SiteId.ToString();
                 DropDownListHelpers.DDL_Bind(ddlNotifyMessageTypeId, NotifyMessageTypes.Static_GetList(""), " ... ", "2"); //2: Gửi một lần
                if(ddlSite.SelectedValue != "1"){
                    domainWEB = "https://english.luatvietnam.vn/";
                }
                txTitle.Text = "Luatvietnam.vn";
                txMessageContent.Text = mArticles.Title;
                txMessageUrl.Text = mArticles.ArticleUrl.Contains("http") ? mArticles.ArticleUrl : domainWEB + mArticles.ArticleUrl;
                txMessageIcon.Text = mArticles.ImagePath.StartsWith("http") ? mArticles.ImagePath : CmsConstants.WEBSITE_MEDIADOMAIN + mArticles.ImagePath;
                txScheduleTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            else if (DocId > 0)
            {
                mDocs = mDocs.Get(DocId, LanguageId);
                if (mDocs.DocId <= 0)
                { Response.Redirect(CmsConstants.PRJ_ROOT + BackUrl); }
                if (LanguageId == 2)
                {
                    domainWEB = "https://english.luatvietnam.vn/";
                    ddlSite.SelectedValue = "2";
                }
                else ddlSite.SelectedValue = "1";
                DropDownListHelpers.DDL_Bind(ddlNotifyMessageTypeId, NotifyMessageTypes.Static_GetList(""), " ... ", "2"); //2: Gửi một lần
                txTitle.Text = "Luatvietnam.vn";
                txMessageContent.Text = mDocs.DocName;
                txMessageUrl.Text = mDocs.DocUrl.Contains("http") ? mDocs.DocUrl : domainWEB + mDocs.DocUrl;
                txMessageIcon.Text = "https://luatvietnam.vn/assets/images/logo.png";
                txScheduleTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                Response.Redirect(CmsConstants.PRJ_ROOT + BackUrl);
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        NotifyMessages m_NotifyMessages = new NotifyMessages(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_NotifyMessages.NotifyMessageId = EditId;
            m_NotifyMessages = m_NotifyMessages.Get();
        }        
        try
		{
            if(int.Parse(ddlNotifyMessageTypeId.SelectedItem.Value) <= 0)
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            
            if(txTitle.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_NotifyMessages.CrUserId = ActUserId;
            
            m_NotifyMessages.NotifyMessageTypeId = System.Byte.Parse(ddlNotifyMessageTypeId.SelectedItem.Value);
            
            m_NotifyMessages.SiteId = 69; //SiteId in cms.icsoft.vn
            
            if(txTitle.Text != "")
				m_NotifyMessages.Title = txTitle.Text;
            
            if(txMessageContent.Text != "")
				m_NotifyMessages.MessageContent = txMessageContent.Text;
            
            if(txMessageUrl.Text != "")
				m_NotifyMessages.MessageUrl = txMessageUrl.Text;
            
            if(txMessageIcon.Text != "")
				m_NotifyMessages.MessageIcon = txMessageIcon.Text;

            if (txScheduleTime.Text.Trim().Length > 0)
                m_NotifyMessages.ScheduleTime = sms.utils.StringUtil.ConvertToDateTime(txScheduleTime.Text);
            else
                m_NotifyMessages.ScheduleTime = DateTime.Now;
            m_NotifyMessages.CrDateTime = DateTime.Now;
            if (m_NotifyMessages.SendStatusId == 0)
                m_NotifyMessages.SendStatusId = 1;
            
            m_NotifyMessages.NotifyMessageId = EditId;
            SysMessageTypeId = m_NotifyMessages.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)
            {
                lblMesg.Text= "Cài đặt gửi Notify thành công.";
            }
            else {
                lblMesg.Text= "Có lỗi xảy ra, bạn vui lòng thử lại.";
            }
            //string script = @"<script language='JavaScript'>" +
            //     "window.parent.jQuery('#divEdit').dialog('close');" +
            //     "</script>";
            //ClientScriptManager csm = this.ClientScript;
            //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
		}
        catch (Exception ex)
		{
			sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
			JSAlertHelpers.Alert(ex.Message, this);
		}
    }
    
   
    
 }
   