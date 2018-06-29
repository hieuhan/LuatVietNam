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

public partial class Admin_ArticleCrawlersEdit : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();

        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (!IsPostBack)
        {
            ArticleCrawlers m_Articles = new ArticleCrawlers();
            m_Articles = m_Articles.Get(ArticleId);
            lblTitle.Text = m_Articles.ArticleTitle;
            lblSummary.Text = m_Articles.ArticleSummary;
            lblContent.Text = m_Articles.ArticleContent;
        }

    }

}