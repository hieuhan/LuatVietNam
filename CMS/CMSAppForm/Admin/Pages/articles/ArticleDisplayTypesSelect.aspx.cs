using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_ArticleDisplayTypeSelect : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int ArticleId = 0;
    private short SiteId = 0;
    private byte DataTypeId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
            {
                ApplicationTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
            }
            CheckBoxListHelpers.Bind(chkDisplayType, DisplayTypes.Static_GetListByDataTypeId(DataTypeId), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            Articles m_Articles = new Articles();
            m_Articles.ArticleId = ArticleId;
            m_Articles.LanguageId = LanguageId;
            m_Articles.ApplicationTypeId = ApplicationTypeId;
            m_Articles = m_Articles.Get();
            if (m_Articles.ArticleId > 0)
            {
                chkShowTop.Checked = m_Articles.ShowTop == 1 ? true : false;
                chkShowBottom.Checked = m_Articles.ShowBottom == 1 ? true : false;
                chkShowWeb.Checked = m_Articles.ShowWeb == 1 ? true : false;
                chkShowWap.Checked = m_Articles.ShowWap == 1 ? true : false;
                chkShowApp.Checked = m_Articles.ShowApp == 1 ? true : false;

                ArticleDisplays m_ArticleDisplays = new ArticleDisplays();
                List<ArticleDisplays> l_ArticleDisplays = m_ArticleDisplays.GetList(m_Articles.ArticleId, m_Articles.LanguageId, m_Articles.ApplicationTypeId);
                for (int i = 0; i < l_ArticleDisplays.Count; i++)
                {
                    CheckBoxListHelpers.SetSelected(chkDisplayType, l_ArticleDisplays[i].DisplayTypeId.ToString());
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
                Articles m_Articles = new Articles();
                m_Articles.ArticleId = ArticleId;
                m_Articles.LanguageId = LanguageId;
                m_Articles.ApplicationTypeId = ApplicationTypeId;
                if (ArticleId > 0)
                {
                    m_Articles = m_Articles.Get();//refresh data
                    m_Articles.ShowApp = chkShowApp.Checked == true ? (byte)1 : (byte)0;
                    m_Articles.ShowBottom = chkShowBottom.Checked == true ? (byte)1 : (byte)0;
                    m_Articles.ShowTop = chkShowTop.Checked == true ? (byte)1 : (byte)0;
                    m_Articles.ShowWap = chkShowWap.Checked == true ? (byte)1 : (byte)0;
                    m_Articles.ShowWeb = chkShowWeb.Checked == true ? (byte)1 : (byte)0;
                    m_Articles.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    
                    ArticleDisplays m_ArticleDisplays = new ArticleDisplays();
                    m_ArticleDisplays.ArticleId = m_Articles.ArticleId;
                    m_ArticleDisplays.ApplicationTypeId = m_Articles.ApplicationTypeId;
                    m_ArticleDisplays.LanguageId = m_Articles.LanguageId;
                    m_ArticleDisplays.DisplayOrder=10;
                    m_ArticleDisplays.CrUserId = ActUserId;
                    for (int i = 0; i < chkDisplayType.Items.Count; i++)
                    {
                        m_ArticleDisplays.DisplayTypeId = short.Parse(chkDisplayType.Items[i].Value);
                        if (chkDisplayType.Items[i].Selected)
                        {
                            m_ArticleDisplays.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        }
                        else
                        {
                            m_ArticleDisplays.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        }
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