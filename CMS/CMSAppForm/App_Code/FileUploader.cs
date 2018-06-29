using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

/// <summary>
/// Summary description for FileUploader
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class FileUploader : System.Web.Services.WebService {

    public FileUploader () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string UploadFile(byte[] buffer, string fileName, string appKey, bool createImageThumbnail = false)
    {
        string retVal = "unauthorized";
        try
        {
            if (appKey.ToLower().Equals(string.Concat("luatvietnam@$%#^&2017", DateTime.Now.ToString("ddMMyyyHH"))))
            {
                retVal = FileUploadHelpers.SaveFile(buffer, fileName, CmsConstants.MEDIA_PATH, createImageThumbnail);
                retVal = string.Concat(CmsConstants.WEBSITE_IMAGEDOMAIN, retVal.Replace("\\", "/"));
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
        return retVal;
    }

    [WebMethod]
    public string LawyersUploadFile(byte[] buffer, string fileName, string lawsId, string directory, string appKey, bool createImageThumbnail = false)
    {
        string retVal = "unauthorized";
        try
        {
            if (appKey.ToLower().Equals(string.Concat("luatvietnam@$%#^&2017", DateTime.Now.ToString("ddMMyyyHH"))))
            {
                retVal = FileUploadHelpers.SaveFile(buffer, fileName, CmsConstants.MEDIA_PATH, lawsId, directory, createImageThumbnail);
                retVal = string.Concat(CmsConstants.WEBSITE_IMAGEDOMAIN, retVal.Replace("\\", "/"));
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
        return retVal;
    }
    
}
