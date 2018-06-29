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

public partial class Admin_ArticlesPreview : System.Web.UI.Page
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
                m_Articles.ApplicationTypeId = ApplicationTypeId;
                m_Articles = m_Articles.Get();
                if (m_Articles.ArticleId > 0)
                {
                   
                    txtContent.Text = m_Articles.ArticleContent;
                    txtSummaryPlain.Text = m_Articles.Summary;
                    txtTitle.Text = m_Articles.Title;
                    
                    txtSEOTitle.Text = m_Articles.MetaTitle;
                    txtSEODesc.Text = m_Articles.MetaDesc;
                    txtSEOKeyword.Text = m_Articles.MetaKeyword;
                    
                    imgDemo.Src = m_Articles.ImagePath.StartsWith("http") ? m_Articles.ImagePath : CmsConstants.WEBSITE_MEDIADOMAIN + m_Articles.ImagePath;

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
                    txtTag.Text = m_TagString;

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
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("Articles.aspx?SiteId=" + SiteId.ToString() + "&DataTypeId=" + DataTypeId.ToString() + "&EnableDataType=" + EnableDataType.ToString() + "&ActionId=" + ActionId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&AppTypeId=" + ApplicationTypeId.ToString() + "&CategoryId=" + CategoryId.ToString());
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("Articles.aspx?SiteId=" + SiteId.ToString() + "&DataTypeId=" + DataTypeId.ToString() + "&EnableDataType=" + EnableDataType.ToString() + "&ActionId=" + ActionId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&AppTypeId=" + ApplicationTypeId.ToString() + "&CategoryId=" + CategoryId.ToString());
    }
    
}