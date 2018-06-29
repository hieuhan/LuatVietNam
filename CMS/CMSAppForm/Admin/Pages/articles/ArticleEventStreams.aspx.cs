using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_ArticleEventStreams : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private short SiteId = 0;
    private int ArticleId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
        {
            ApplicationTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            int RowAmount = 200;
            int PageIndex = 0;
            string OrderBy = "";
            int EventStreamId = 0;
            short CategoryId = 0;
            string EventStreamName = "";
            byte ReviewStatusId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            EventStreams m_EventStreams = new EventStreams();
            m_EventStreams.SiteId = short.Parse(ddlSite.SelectedValue);
            List<EventStreams> l_EventStreams = m_EventStreams.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, EventStreamId, CategoryId, EventStreamName, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            CheckBoxListHelpers.Bind(chkEventStreams, l_EventStreams, "");
            for (int i = 0; i < chkEventStreams.Items.Count; i++)
            {
                if (!chkEventStreams.Items[i].Text.StartsWith("&nbsp"))
                {
                    chkEventStreams.Items[i].Attributes.Add("style", "font-weight:bold;");
                }
            }
            if (ArticleId > 0)
            {
                ArticleEventStreams m_ArticleEventStreams = new ArticleEventStreams();
                List<ArticleEventStreams> l_ArticleEventStreams = m_ArticleEventStreams.GetListByArticleId(ArticleId);
                for (int i = 0; i < l_ArticleEventStreams.Count; i++)
                {

                    CheckBoxListHelpers.SetSelected(chkEventStreams, l_ArticleEventStreams[i].EventStreamId.ToString(), "Yellow");
                }

            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            short SysMessageId = 0;
            if (ArticleId > 0)
            {
                ArticleEventStreams m_ArticleEventStreams = new ArticleEventStreams();
                m_ArticleEventStreams.ArticleId = ArticleId;
                m_ArticleEventStreams.DeleteByArticleId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                for (int i = 0; i < chkEventStreams.Items.Count; i++)
                {
                    if (chkEventStreams.Items[i].Selected)
                    {
                        m_ArticleEventStreams.EventStreamId = short.Parse(chkEventStreams.Items[i].Value);
                        m_ArticleEventStreams.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
                
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
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}