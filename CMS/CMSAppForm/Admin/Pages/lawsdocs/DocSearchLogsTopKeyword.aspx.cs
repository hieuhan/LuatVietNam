using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.common;
using sms.admin.security;

public partial class Admin_Pages_lawsdocs_DocSearchLogsTopKeyword : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private string DateFrom = "";
    private string DateTo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["DateFrom"] != null && Request.Params["DateFrom"].ToString() != "")
        {
            DateFrom = Request.Params["DateFrom"].ToString();
        }
        if (Request.Params["DateTo"] != null && Request.Params["DateTo"].ToString() != "")
        {
            DateTo = Request.Params["DateTo"].ToString();
        }
        if (!IsPostBack)
        {
            bindData();
        }
    }
    private void bindData()
    {
        DocSearchLogs m_DocSearchLogs = new DocSearchLogs();
        m_grid.DataSource = m_DocSearchLogs.GetTopKeyword(DateFrom, DateTo, LanguageId);
        m_grid.DataBind();
    }
}