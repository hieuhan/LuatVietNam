<%@ Application Language="C#" Inherits="System.Web.HttpApplication" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    void Application_BeginRequest(Object sender, EventArgs e)
    {
        try
        {
            string culture = CookieHelpers.GetCookieValue(HttpContext.Current.Request.Cookies["CultureInfo"]);
            if (culture == "")
            {
                culture = ConstantHelpers.DEFAULT_CULTURE;
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
        }
        catch(Exception ex)
        {
            sms.utils.LogFiles.LogInfo(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
    }
    public override string GetVaryByCustomString(HttpContext context, string arg)
    {
        return arg;
        string retValue = arg;
        byte DocLanguageId = LanguageHelpers.GetCurentDocLanguageId();
        retValue += "-" + LanguageHelpers.GetCurentName();
        retValue += "-" + ApplicationTypeHelpers.GetCurentName();
        retValue += "-" + DocLanguageId.ToString();
        arg = "," + arg + ",";
        
        try
        {
            if (arg.Contains("CPDocDetail"))// tree version of one DocDetail: member, member not permition, and other
            {
                int CustomerId = CustomerHelpers.GetCurentId();
                //if (context.Session != null)
                //{
                //    CustomerId = context.Session["CustomerId"] == null ? 0 : int.Parse(context.Session["CustomerId"].ToString());
                //}
                int IsMember = CustomerId > 0 ? 1 : 0;
                byte RoleId = CustomerRoleHelpers.VB;
                if (DocLanguageId != LanguageHelpers.Vietnamese)
                    RoleId = CustomerRoleHelpers.VBE;
                byte DataTypeId = DataTypeHelpers.Doc;
                bool IsInRole = false;
                if (CustomerId > 0)
                {
                    string DocGUId = "";
                    ICSoft.LawDocsLib.Docs m_Docs = new ICSoft.LawDocsLib.Docs();
                    if (context.Request["DocGUId"] != null)
                    {
                        DocGUId = context.Request["DocGUId"];
                    }
                    m_Docs.DocGUId = DocGUId;
                    m_Docs = m_Docs.GetByDocGUId(DocGUId, DocLanguageId);
                    IsInRole = CustomerRoleHelpers.IsInRole(RoleId, DataTypeId, m_Docs.DocId, m_Docs.IssueDate);
                }
                retValue += "-" + IsMember.ToString() + "-" + IsInRole.ToString();
            }
            foreach (string requestKey in context.Request.QueryString.AllKeys)
            {
                string tempRequestKey = "," + requestKey + ",";
                if( arg.Contains(tempRequestKey) && string.IsNullOrEmpty(requestKey) == false && string.IsNullOrEmpty(context.Request[requestKey]) == false )
                    retValue += "-" + context.Request[requestKey];
            }

        }//end try
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
        }
        sms.utils.LogFiles.LogInfo(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + retValue);
        return retValue;

    }
       
</script>
