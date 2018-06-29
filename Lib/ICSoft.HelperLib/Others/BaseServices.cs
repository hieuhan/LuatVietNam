﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSoft.CMSLib;
namespace ICSoft.HelperLib
{
    public class BaseServices : System.Web.UI.Page
    {
        string MasterPath;
        public Pages m_Pages;      
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPath = MasterPageHelpers.GetCurentPath(this.MasterPageFile);
            if (MasterPath != this.MasterPageFile)
            {
                this.MasterPageFile = MasterPath;
            }            
            // page info
            m_Pages = new Pages();
            Response.ContentType = "application/json; charset=utf-8";
            
        }
       
    }
}
