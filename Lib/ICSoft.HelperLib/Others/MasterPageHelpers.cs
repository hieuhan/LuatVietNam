using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;
namespace ICSoft.HelperLib
{
    public class MasterPageHelpers
	{
        //---------------------------------------------------------------------------------------
        public static string GetCurentPath(string BasePath)
        {
            
            string LanguageName = LanguageHelpers.GetCurentName();
            string ApplicationTypeName = ApplicationTypeHelpers.GetCurentName();

            return GetCurentPath(BasePath, LanguageName, ApplicationTypeName);
        }
        //---------------------------------------------------------------------------------------
        public static string GetCurentPath(string BasePath, string LanguageName, string ApplicationTypeName)
        {
            string RetVal = BasePath;// ConstantHelpers.DefaultApplicationType;
            string CurentPath = "";           
            try
            {
                if (LanguageName != ConstantHelpers.DefaultLanguage && ConstantHelpers.MASTER_CHANGE_BY_LANG > 0)
                {
                    CurentPath = LanguageName + "/";
                }
                if (ApplicationTypeName != ConstantHelpers.DefaultApplicationType && ConstantHelpers.MASTER_CHANGE_BY_APPTYPE > 0)
                {
                    CurentPath += ApplicationTypeName + "/";
                }
                if (string.IsNullOrEmpty(CurentPath) == false)
                {
                    RetVal = RetVal.Replace(ConstantHelpers.MasterPageUrl, ConstantHelpers.MasterPageUrl + CurentPath);
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
            return RetVal;
        }
        
	}//end ApplicationType

}//end namespace service
