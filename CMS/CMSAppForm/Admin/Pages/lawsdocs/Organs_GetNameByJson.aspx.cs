using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Text;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class admin_Organs_GetNameByJson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = "";
        string result = "";
        Organs m_Organs = new Organs();
        DataSet ds;
        if (Request.Params["term"] != null)
        {
            term = sms.utils.StringUtil.RemoveSign4VietnameseString(Request.Params["term"].ToString());
        }

        ds = m_Organs.Organs_GetNameByJson(term, int.Parse(ConfigurationManager.AppSettings["PAGESIZE_ORGANS_JSON"]), ref result);
        
       Response.Write(result);
    }
}