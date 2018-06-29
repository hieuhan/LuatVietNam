using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_ArticlesEditRevTime : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int ArticleId = 0;
    private int ActUserId = 0;
    private short CategoryId = 0;
    private short SiteId = 0;
    private byte DataTypeId = 0;
    private byte EnableDataType = 1;
    private short ActionId = 0;
    private short SysMessageId = 0;
    private byte SysMessageTypeId = 0;
    private string messages = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();

        if (Request.Params["ActionId"] != null && Request.Params["ActionId"].ToString() != "")
        {
            ActionId = short.Parse(Request.Params["ActionId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (Request.Params["EnableDataType"] != null && Request.Params["EnableDataType"].ToString() != "")
        {
            EnableDataType = byte.Parse(Request.Params["EnableDataType"].ToString());
        }
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (Request.Params["CategoryId"] != null && Request.Params["CategoryId"].ToString() != "")
        {
            CategoryId = short.Parse(Request.Params["CategoryId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
        {
            ApplicationTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
        }
        if (!IsPostBack)
        {
            if (ArticleId > 0)
            {
                BindData();
            }
            
        }

    }

    private void BindData()
    {
        try
        {
            bool IsAlert = false;
            if (ArticleId > 0)
            {
                Articles m_Articles = new Articles();
                m_Articles.ArticleId = ArticleId;
                m_Articles.LanguageId = LanguageId;
                m_Articles.ApplicationTypeId = ApplicationTypeId;
                m_Articles = m_Articles.Get();
                if (m_Articles.ArticleId > 0)
                {
                    txtRevDateTime.Text = m_Articles.PublishTime == DateTime.MinValue ? "" : m_Articles.PublishTime.ToString("dd/MM/yyyy HH:mm:ss");
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    
    protected void SaveData()
    {
        try
        {
            Articles m_Articles = new Articles();
            m_Articles.ArticleId = ArticleId;
            m_Articles.LanguageId = LanguageId;
            m_Articles.ApplicationTypeId = ApplicationTypeId;
            m_Articles = m_Articles.Get();
            m_Articles.PublishTime = txtRevDateTime.Text != "" ? DateTime.Parse(txtRevDateTime.Text) : DateTime.MinValue;
            SysMessageTypeId = m_Articles.UpdatePublicTime(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1) //error
            {
                messages = string.Format("Lỗi khi chọn ngày xuất bản: {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2) // success
            {
                messages = "Chọn ngày xuất bản thành công.";
            }
            JSAlertHelpers.ShowNotifyOtherPage("15", "success", messages, this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
        string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
        ClientScriptManager csm = this.ClientScript;
        csm.RegisterClientScriptBlock(this.GetType(), "close", script);
    }
    
}