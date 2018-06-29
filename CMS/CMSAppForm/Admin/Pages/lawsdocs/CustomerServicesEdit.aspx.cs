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
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.common;
using sms.utils;
using Genders = ICSoft.CMSLib.Genders;
using Status = ICSoft.CMSLib.Status;

public partial class Admin_CustomerServicesEdit : System.Web.UI.Page
{
    private short ServicePackageId = 0;
    private int CustomerId = 0;
    private DateTime EndTime;
    private int ActUserId = 0;
    private int CustomerServiceId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CustomerServiceId"] != null && Request.Params["CustomerServiceId"].ToString() != "")
        {
            CustomerId = int.Parse(Request.Params["CustomerServiceId"].ToString());
        }
        if (Request.Params["CustomerId"] != null && Request.Params["CustomerId"].ToString() != "")
        {
            CustomerId = int.Parse(Request.Params["CustomerId"].ToString());
        }
        if (Request.Params["ServicePackageId"] != null && Request.Params["ServicePackageId"].ToString() != "")
        {
            ServicePackageId = short.Parse(Request.Params["ServicePackageId"].ToString());
        }
        if (Request.Params["EndTime"] != null && Request.Params["EndTime"].ToString() != "")
        {
            EndTime = DateTime.Parse(Request.Params["EndTime"].ToString());
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
                    lblCustomerNames.Text = m_Customers.CustomerName + " (" + m_Customers.CustomerFullName + ")";
                    txtEndTime.Text = EndTime.ToString("dd/MM/yyyy");
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
            byte SysMessageTypeId = 0;
            string message = "Cập nhật thành công.";
            CustomerServices m_CustomerServices = new CustomerServices();
            List<CustomerServices> l_CustomerServices = m_CustomerServices.CustomerServices_GetListByCustomerId(CustomerId);
            if(l_CustomerServices.Count > 0)
            {
                m_CustomerServices = l_CustomerServices[0];
            }

            m_CustomerServices.CustomerId = CustomerId;
            m_CustomerServices.ServicePackageId = ServicePackageId;
            m_CustomerServices.ServiceId = ServicePackageId;
            m_CustomerServices.BeginTime = DateTime.Now;
            m_CustomerServices.EndTime = DateTime.Parse(txtEndTime.Text);
            m_CustomerServices.InsertOrUpdate(1, ActUserId, ref SysMessageId);

            JSAlertHelpers.Alert(message, this);
            //JSAlertHelpers.AlertParent("divEdit", "Thông báo:", message, this);
            //JSAlertHelpers.close(this);
            //string script = @"<script language='JavaScript'>" +
            //    "window.parent.jQuery('#divEdit').dialog('close');" +
            //    "</script>";
            //ClientScriptManager csm = this.ClientScript;
            //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
   
}