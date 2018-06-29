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

public partial class Admin_ArticlesPreviewAMP : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int ArticleId = 0;
    private int ActUserId = 0;
    protected string MetaTitle = "";
    protected string MetaDesc = "";
    protected string MetaKey = "";
    protected string Url = "";
    protected string Image = "";
    protected string Content = "";
    protected string Tag = "";
    protected string PublishTime = "";
    protected string Title = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();

        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (!IsPostBack)
        {
            if (ArticleId > 0)
            {
                BindData();
            }
            else
            {
                return;
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
                m_Articles = m_Articles.Get();
                if (m_Articles.ArticleId > 0)
                {
                   
                    Content = GoogleAmp.Convert(m_Articles.ArticleContent);
                    MetaDesc = string.IsNullOrEmpty(m_Articles.MetaDesc) ? m_Articles.Summary.Replace("\"", "") : m_Articles.MetaDesc.Replace("\"", "");
                    MetaTitle = string.IsNullOrEmpty(m_Articles.MetaTitle) ? m_Articles.Title.Replace("\"", "") : m_Articles.MetaTitle.Replace("\"", "");
                    MetaKey = string.IsNullOrEmpty(m_Articles.MetaKeyword) ? m_Articles.Summary.Replace("\"", "") : m_Articles.MetaKeyword;
                    Url = m_Articles.ArticleUrl.StartsWith("http") ? m_Articles.ArticleUrl : CmsConstants.WEBSITE_DOMAIN + m_Articles.ArticleUrl;
                    Image = m_Articles.ImagePath.StartsWith("http") ? m_Articles.ImagePath : CmsConstants.WEBSITE_MEDIADOMAIN + m_Articles.ImagePath;
                    Title = m_Articles.Title;
                    PublishTime = DateTimeHelpers.GetDateAndTime(m_Articles.PublishTime).ToString(); 
                    //Bind ArticleTags
                    int RowAmount = 0;
                    int PageIndex = 0;
                    string OrderBy = "";
                    int TagId = 0;
                    string TagName = "";
                    byte ReviewStatusId = 0;
                    string DateFrom = "";
                    string DateTo = "";
                    int RowCount = 0;
                    string m_TagString = "";
                    Tags m_Tags = new Tags();
                    List<Tags> l_Tags = m_Tags.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, m_Articles.LanguageId, TagId, m_Articles.ArticleId, TagName, ReviewStatusId, DateFrom, DateTo, ref RowCount);
                    for (int i = 0; i < l_Tags.Count; i++)
                    {
                        if (i > 0) m_TagString = m_TagString + ", ";
                        m_TagString = m_TagString + l_Tags[i].TagName;
                    }
                    Tag = m_TagString;

                    if (IsAlert)
                    {
                        JSAlertHelpers.ShowNotify("10", "", "Dữ liệu được lấy từ bài viết có ngôn ngữ và nền tảng mặc định", this);
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    
}