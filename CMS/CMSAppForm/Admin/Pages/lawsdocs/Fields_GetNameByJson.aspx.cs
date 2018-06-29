using System;
using System.Collections;
using System.Configuration;
using System.Text;
using ICSoft.LawDocsLib;

public partial class admin_Fields_GetNameByJson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = "";
        string result = "";
        Fields m_Fields = new Fields();
        if (Request.Params["term"] != null)
        {
            term = Request.Params["term"].ToString();
        }

        m_Fields.Fields_GetNameByJson(term, int.Parse(ConfigurationManager.AppSettings["PAGESIZE_FIELDS_JSON"]), ref result);
        
        Response.Write(result);
    }
}