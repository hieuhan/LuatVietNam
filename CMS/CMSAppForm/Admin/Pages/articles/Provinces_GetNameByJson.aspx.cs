using System;
using System.Collections;
using System.Configuration;
using System.Text;
using ICSoft.CMSLib;

public partial class admin_Provinces_GetNameByJson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string term = "";
        string result = "";
        Provinces m_Provinces = new Provinces();
        if (Request.Params["term"] != null)
        {
            term = Request.Params["term"].ToString();
        }

        m_Provinces.Provinces_GetNameByJson(term, int.Parse(ConfigurationManager.AppSettings["PAGESIZE_FIELDS_JSON"]), ref result);
        
        Response.Write(result);
    }
}