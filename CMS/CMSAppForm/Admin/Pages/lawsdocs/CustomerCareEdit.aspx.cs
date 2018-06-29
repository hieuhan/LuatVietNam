using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_CustomerCareEdit: System.Web.UI.Page
{
    private int CustomerId = 0;
    private int ActUserId = 0;

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
            txtInfo.Text = "";   
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int CustomerCareId = 0;
            int SysMessageId = 0;
            CustomerCare mCustomerCare = new CustomerCare();
            mCustomerCare.CustomerId = CustomerId;
            mCustomerCare.Info = txtInfo.Text.Trim();
            mCustomerCare.CustomerCareId = 0;
            CustomerCareId = mCustomerCare.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (CustomerCareId == 0)
            {
                JSAlertHelpers.ShowNotify("15", "error","Lỗi thêm mới nội dung", this);
            }
            else if (CustomerCareId > 0)
            {
                JSAlertHelpers.ShowNotifyOtherPage("15", "success","Thêm mới nội dung thành công.", this);
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
    private void ClearForm()
    {
        txtInfo.Text = "";
    }
}