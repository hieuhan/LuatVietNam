using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using System.Threading;
using sms.common;
using sms.admin.security;
using System.Collections;
public partial class Admin_MasterPageAdmin : System.Web.UI.MasterPage
{
    protected string culture = "";
    private List<Actions> l_Actions;
    private int ActUserId = 0;
    protected Languages p_Language = new Languages();
    protected void Page_Init(object sender, EventArgs e)
    {
        int ActUserId = SessionHelpers.GetUserId();
        if (ActUserId == 0)
        {
            Response.Redirect(CmsConstants.PRJ_ROOT + "pages/security/Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.RawUrl), true);
        }
        else
        {
            Users m_Users = new Users();
            if (!m_Users.HasPriv(ActUserId, Request.Url.AbsolutePath))
            {
                Response.Redirect(CmsConstants.PRJ_ROOT + "errMsg.aspx", false);
            }
        }
        //if (!IsPostBack)
        //{
        //    try
        //    {


        //    }
        //    catch (Exception ex)
        //    {
        //        JSAlertHelpers.Alert(ex.Message.ToString(), this.Page);
        //        sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        //    }
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        lblTime.Text = DateTime.Now.ToString("HH:mm dd/MM/yyyy");
        if (!IsPostBack)
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
                    if (ActUserId == 0)
                    {
                        Response.Redirect(CmsConstants.PRJ_ROOT + "pages/security/Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.RawUrl), true);
                    }
                    else
                    {
                        Users m_Users = new Users();
                        if (!m_Users.HasPriv(ActUserId, Request.Url.AbsolutePath.Substring(CmsConstants.PRJ_ROOT.Length)))
                        {
                            Response.Redirect(CmsConstants.PRJ_ROOT + "errMsg.aspx", false);
                        }
                    }

                    culture = SessionHelpers.GetCulture();
                    DropDownListHelpers.DDL_Bind(ddlLanguage, Languages.Static_GetList(), "", culture);
                    ShowMenu();
                    ShowPageHeader();

                    Languages m_Language = new Languages();
                    p_Language = m_Language.GetByCode(Thread.CurrentThread.CurrentCulture.Name);
                    BindLanguage();
                }
                
            }
            catch (Exception ex)
            {
                JSAlertHelpers.AlertException(ex.Message.ToString(), this.Page);
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        ICSoft.HelperLib.JSAlertHelpers.ShowMessage("15");
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

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["CultureInfo"] = ddlLanguage.SelectedValue;
            Response.Cookies.Add(CookieHelpers.SetCookie("CultureInfo", ddlLanguage.SelectedValue, 15));
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(ddlLanguage.SelectedValue);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ddlLanguage.SelectedValue);
            Server.Transfer(Request.Path);
        }
        catch (Exception ex)
        {
            JSAlertHelpers.AlertException(ex.Message.ToString(), this.Page);
        }
    }

    private void ShowMenu()
    {
        Actions m_Actions = new Actions();
        l_Actions = m_Actions.GetNotRootForUser(ActUserId).Cast<Actions>().ToList<Actions>();      
        rptMenu.DataSource = m_Actions.GetActionsByUser(ActUserId);
        rptMenu.DataBind();
    }

    protected void rptMenu_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        Repeater rptSubMenu = (Repeater)e.Item.FindControl("rptSubMenu");
        short ParentId = ((Actions)e.Item.DataItem).ActionId;
        if (rptSubMenu != null)
        {
            Actions m_Actions = new Actions();
            rptSubMenu.DataSource = l_Actions.Where(i => i.ParentActionId == ParentId);
            rptSubMenu.DataBind();
        }
    }
    protected void rptSubMenu_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        try
        {
            Repeater rptSubMenu = (Repeater)e.Item.FindControl("rptMenuLevel2");
            if (rptSubMenu != null)
            {
                short ParentId = ((Actions)e.Item.DataItem).ActionId;
                Actions m_Actions = new Actions();
                List<Actions> listAction = new List<Actions>();
                listAction = l_Actions.Where(i => i.ParentActionId == ParentId).ToList<Actions>();
                if (listAction != null && listAction.Any())
                {
                    rptSubMenu.DataSource = listAction;
                    rptSubMenu.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            JSAlertHelpers.AlertException(ex.Message.ToString(), this.Page);
        }
    }
    protected string GetActiveClass(object m_ActionId)
    {
        string RetVal = "", originalUrl = Request.RawUrl;
        short ActionId = short.Parse(m_ActionId.ToString());
        Actions m_Actions = new Actions();
        List<Actions> l_ActionChild = new List<Actions>();
        m_Actions = l_Actions.Where(i => i.ActionId == ActionId).FirstOrDefault<Actions>();
        if (originalUrl.IndexOf("?", StringComparison.Ordinal) != -1)
            originalUrl = originalUrl.Substring(0, originalUrl.IndexOf("?", StringComparison.Ordinal));
        if (m_Actions != null && originalUrl.Equals("/admin/" + m_Actions.Url) && m_Actions.Url.Contains(".aspx"))
        {
            RetVal = "menuactive";
        }
        else
        {
            l_ActionChild = l_Actions.Where(i => i.ParentActionId == ActionId).ToList<Actions>();
            if(l_ActionChild != null)
            {
                foreach(Actions m_ActionsChild in l_ActionChild)
                {
                    if (originalUrl.Equals("/admin/" + m_ActionsChild.Url) && m_ActionsChild.Url.Contains(".aspx"))
                    {
                        RetVal = "menuactive";
                        break;
                    }
                }
            }
        }
        
        return RetVal;
    }
    protected string GetIconHaveChild(object m_ActionId)
    {
        string RetVal = "";
        short ActionId = short.Parse(m_ActionId.ToString());
        Actions m_Actions = new Actions();
        List<Actions> l_ActionChild = new List<Actions>();
        l_ActionChild = l_Actions.Where(i => i.ParentActionId == ActionId).ToList<Actions>();
        if (l_ActionChild.Count > 0)
            RetVal = "+";
        return RetVal;
    }
    private void ShowPageHeader()
    {
        string filePath = Request.AppRelativeCurrentExecutionFilePath.ToLower();
        filePath = filePath.Replace("~/", "");
        filePath = filePath.Replace(".aspx", "");
        filePath = filePath.Replace("/", "_");

        lblUser.Text = GetGlobalResourceObject("AdminResource", "Hello").ToString() + " " + SessionHelpers.GetUserName();
        try
        {
            if (Request.Params["ActionId"] != null && Request.Params["ActionId"].ToString() != "")
            {
                short ActionId = short.Parse(Request.Params["ActionId"].ToString());
                if (ActionId > 0)
                {
                    Actions m_Actions = new Actions();
                    m_Actions = m_Actions.Get(ActionId);
                    if (m_Actions != null) lblPageName.Text = m_Actions.ActionDesc;
                }
                else
                {
                    lblPageName.Text = GetGlobalResourceObject("AdminResource", filePath).ToString();
                }
            }
            else
            {
                string url = Request.RawUrl.ToLower();
                
                Actions m_Actions = new Actions();
                List<Actions> l_ActionTemp = m_Actions.GetList().Cast<Actions>().ToList<Actions>();
                m_Actions = l_ActionTemp.Find(i => (url.Contains(i.Url.ToLower()) && i.Url.Length > 1));
                if (m_Actions != null)
                {
                    lblPageName.Text = m_Actions.ActionDesc; // Request.AppRelativeCurrentExecutionFilePath;
                }
                else
                {
                    lblPageName.Text = GetGlobalResourceObject("AdminResource", filePath).ToString();
                }
                
            }
        }
        catch
        {
            if (Request.Params["ActionId"] != null && Request.Params["ActionId"].ToString() != "")
            {
                short ActionId = short.Parse(Request.Params["ActionId"].ToString());
                Actions m_Actions = new Actions();
                m_Actions = m_Actions.Get(ActionId);
                if (m_Actions != null) lblPageName.Text = m_Actions.ActionDesc;
            }
            else
            {
                string url = Request.RawUrl.ToLower();
                Actions m_Actions = l_Actions.Find(i => url.Contains(i.Url.ToLower()));
                if (m_Actions != null)
                {
                    lblPageName.Text = m_Actions.ActionDesc; // Request.AppRelativeCurrentExecutionFilePath;
                }
                else
                {
                    lblPageName.Text = Request.AppRelativeCurrentExecutionFilePath;
                }
            }
        }
    }
}
