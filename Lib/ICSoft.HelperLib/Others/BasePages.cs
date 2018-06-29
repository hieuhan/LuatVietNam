using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSoft.CMSLib;
namespace ICSoft.HelperLib
{
    public class BasePage: System.Web.UI.Page
    {
        string MasterPath;
        public Pages m_Pages = new Pages();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPath = MasterPageHelpers.GetCurentPath(this.MasterPageFile);
            if (MasterPath != this.MasterPageFile)
            {
                this.MasterPageFile = MasterPath;
            }            
            // page info
            short PageId = 0;
            if (Request["PageId"] != null)
                PageId = short.Parse(Request["PageId"]);
            if (PageId > 0)
            {
                byte LanguageId = LanguageHelpers.GetCurentId();
                byte ApplicationTypeId = ApplicationTypeHelpers.GetCurentId();
                m_Pages = new Pages();
                m_Pages = m_Pages.Get(PageId, LanguageId, ApplicationTypeId);
                this.Title = m_Pages.PageTitle;
                this.MetaDescription = m_Pages.PageDesciption;
                this.MetaKeywords = m_Pages.PageKeyword;
            }
            
        }
       
    }
}
