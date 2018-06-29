using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_ArticleCategories : System.Web.UI.Page
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
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "...", SiteId.ToString());
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), DataTypeId, LanguageId, ApplicationTypeId, 0, 0, "&nbsp;&nbsp;&nbsp;");
            CheckBoxListHelpers.Bind(chkCategory, l_Category, "");
            for (int i = 0; i < chkCategory.Items.Count; i++)
            {
                if (!chkCategory.Items[i].Text.StartsWith("&nbsp"))
                {
                    chkCategory.Items[i].Attributes.Add("style", "font-weight:bold;");
                }
            }
            if (ArticleId > 0)
            {
                ArticleCategories m_ArticleCategories = new ArticleCategories();
                List<ArticleCategories> l_ArticleCategories = m_ArticleCategories.GetListByArticleId(ArticleId);
                Articles m_Articles = new Articles();
                m_Articles = m_Articles.Get(ArticleId, LanguageId, ApplicationTypeId);
                if (m_Articles.CategoryId > 0)
                {
                    CheckBoxListHelpers.SetSelected(chkCategory, m_Articles.CategoryId.ToString(), "Yellow");
                }
                for (int i = 0; i < l_ArticleCategories.Count; i++)
                {

                    CheckBoxListHelpers.SetSelected(chkCategory, l_ArticleCategories[i].CategoryId.ToString(), "Yellow");
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
                ArticleCategories m_ArticleCategories = new ArticleCategories();
                m_ArticleCategories.ArticleId = ArticleId;
                m_ArticleCategories.DeleteByArticleId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                for (int i = 0; i < chkCategory.Items.Count; i++)
                {
                    if (chkCategory.Items[i].Selected)
                    {
                        m_ArticleCategories.CategoryId = short.Parse(chkCategory.Items[i].Value);
                        m_ArticleCategories.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
                
            }
            BindData();
            lblMsg.Text = "Cập nhật chuyên mục cho bài viết thành công.";
            ////JSAlertHelpers.close(this);
            //string script = @"<script language='JavaScript'>" +
            //    "window.parent.jQuery('#divEdit').dialog('close');" +
            //    "</script>";
            //ClientScriptManager csm = this.ClientScript;
            //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
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