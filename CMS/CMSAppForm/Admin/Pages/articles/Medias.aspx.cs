using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Medias : System.Web.UI.Page
{
    protected string LanguageCode = "en-US";
    protected void Page_Load(object sender, EventArgs e)
    {
        LanguageCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    }

    
}