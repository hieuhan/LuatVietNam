using System;
using System.Collections;
using System.Text;
using sms.common;
using ICSoft.CMSLib;

public partial class admin_pages_TagsJson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //LawCategories m_LawCategories = new LawCategories();
            //if (Request.Params["term"] != null) m_LawCategories.LawCategoryName = Request.Params["term"].ToString();
            //IList list = m_LawCategories.LawCategories_Search("LawCategoryName", 500, 0);
            Tags m_Tags = new Tags();
            if (Request.Params["term"] != null)
                m_Tags.TagName = Request.Params["term"].ToString();
            int ActUserId = 0;
            int RowAmount = 10;
            int PageIndex = 0;
            string OrderBy = "";
            byte LanguageId = 0;
            int TagId = 0;
            int ArticleId = 0;
            byte ReviewStatusId = 0;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            IList l_Tags = m_Tags.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, TagId, ArticleId, m_Tags.TagName, ReviewStatusId, DateFrom, DateTo, ref RowCount);
            StringBuilder result = new StringBuilder();
            result.Append("[\r\n");
            for (int index = 0; index < l_Tags.Count; index++)
            {
                m_Tags = (Tags)l_Tags[index];
                if (index == l_Tags.Count - 1)
                    result.Append("\"" + m_Tags.TagName + "\"\r\n");
                else if (l_Tags.Count != 1)
                    result.Append("\"" + m_Tags.TagName + "\",\r\n");
            }
            result.Append("]");
            Response.Write(result);
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name, ex.ToString());
            Response.Write("[]");
        }
    }
}