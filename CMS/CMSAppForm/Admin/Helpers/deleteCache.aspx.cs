using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ICSoft.CMSLib;

public partial class deleteCache : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //CacheUtil.removeAllCache();
        string redirect;
        if (Request.UrlReferrer == null)
            redirect = CmsConstants.ROOT_PATH;
        else
            redirect = Request.UrlReferrer.AbsolutePath;
        // output cache
        string[] allfiles = System.IO.Directory.GetFiles(  AppDomain.CurrentDomain.BaseDirectory + "sites/", "*.as*x", System.IO.SearchOption.AllDirectories);
        string OutputCacheKey = "";
        for (int index = 0; index < allfiles.Length; index++)
        {
            OutputCacheKey = allfiles[index].Replace(AppDomain.CurrentDomain.BaseDirectory, "/").Replace("\\", "/");
            HttpResponse.RemoveOutputCacheItem(OutputCacheKey);
            
        }
        
        //end output cache
        Response.Redirect(redirect,true);
        
    }
}
