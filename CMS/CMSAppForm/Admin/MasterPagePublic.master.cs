using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using System.Threading;
using sms.common;
namespace ICSoft.AppForm.Admin
{
    public partial class MasterPagePublic : System.Web.UI.MasterPage
    {
        protected Languages p_Language=new Languages();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Params["Culture"] != null)
                {
                    string culture = Request.Params["Culture"].ToString();
                    if (culture == "") culture = Thread.CurrentThread.CurrentCulture.Name;
                    Session["CultureInfo"] = culture;
                    Response.Cookies.Add(CookieHelpers.SetCookie("CultureInfo", culture, 15));
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
                    Response.Redirect(Request.Params["ReturnUrl"].ToString());
                }
                else
                {
                    //divDateTime.InnerText = DateTime.Now.ToString("MM-dd-yyyy HH:mm");
                    if (!Page.IsPostBack)
                    {
                        Languages m_Language = new Languages();
                        p_Language = m_Language.GetByCode(Thread.CurrentThread.CurrentCulture.Name);
                        BindLanguage();
                    }
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        private void BindLanguage()
        {
            List<Languages> l_Languages = new List<Languages>();
            foreach (Languages m_Language in Languages.Static_GetList())
            {
                if (m_Language.LanguageName != p_Language.LanguageName)
                {
                    l_Languages.Add(m_Language);
                }
            }
            rptLanguage.DataSource = l_Languages;
            rptLanguage.DataBind();
        }

    }
}
