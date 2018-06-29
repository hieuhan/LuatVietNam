using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Reflection;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_CustomersChangeAvatar : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int CustomerId = 0;
    private int ActUserId = 0;
    protected List<Status> l_Status = new List<Status>();
    protected List<Genders> l_Genders = new List<Genders>();    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CustomerId"] != null && Request.Params["CustomerId"].ToString() != "")
        {
            CustomerId = int.Parse(Request.Params["CustomerId"].ToString());
        }
        if (!IsPostBack)
        {
           BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (CustomerId > 0)
            {
                Customers m_Customers = new Customers();
                m_Customers = m_Customers.Get(CustomerId);
                if (m_Customers.CustomerId > 0)
                {
                    lblCustomerNames.Text = m_Customers.CustomerName + "(" + m_Customers.CustomerFullName + ")";
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();      
    }   
   protected void SaveData()
    {
        try
        {
            short SysMessageId = 0;
            if (FileUpload1.HasFile)
            {
                string RootDir = Request.PhysicalApplicationPath;
                string FilePath = FileUploadHelpers.SaveFile(FileUpload1, RootDir, CmsConstants.MEDIA_PATH, false);
                Customers m_Customers = new Customers();
                m_Customers.CustomerId = CustomerId;
                m_Customers.Avatar = FilePath.Replace("\\", "/");
                m_Customers.UpdateAvatar(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                //cap nhat profile estate
                ICSoft.CMSLibEstate.ProfileAgencies m_ProfileAgencies = new ICSoft.CMSLibEstate.ProfileAgencies();
                m_ProfileAgencies = m_ProfileAgencies.GetByUserId(CustomerId);
                m_ProfileAgencies.Avatar = FilePath.Replace("\\", "/");
                m_ProfileAgencies.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            //JSAlertHelpers.close(this);
            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
   
}