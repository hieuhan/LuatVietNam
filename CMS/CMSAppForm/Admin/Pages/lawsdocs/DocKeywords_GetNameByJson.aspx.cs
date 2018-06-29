using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Text;
using ICSoft.LawDocsLib;
using sms.utils;
public partial class Admin_Pages_lawsdocs_DocKeywords_GetNameByJson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = "";
        string result = "";
        Keywords m_Keywords = new Keywords();
        DataSet ds;
        if (Request.Params["term"] != null)
        {
            term = sms.utils.StringUtil.RemoveSign4VietnameseString(Request.Params["term"].ToString());
        }

        ds = m_Keywords.Keywords_GetNameByJson(term, int.Parse(ConfigurationManager.AppSettings["PAGESIZE_ORGANS_JSON"]), ref result);

        Response.Write(result);
    }
}