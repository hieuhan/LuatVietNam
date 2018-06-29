using System;
using System.Collections;
using System.Configuration;
using System.Text;
using ICSoft.LawDocsLib;

public partial class admin_Signers_GetNameByJson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = "";
        string result = "";
        Signers m_Signers = new Signers();
        if (Request.Params["term"] != null)
        {
            term = Request.Params["term"].ToString();
        }

        m_Signers.Signers_GetNameByJson(term, int.Parse(ConfigurationManager.AppSettings["PAGESIZE_SIGNERS_JSON"]), ref result);
        
        Response.Write(result);
    }
}