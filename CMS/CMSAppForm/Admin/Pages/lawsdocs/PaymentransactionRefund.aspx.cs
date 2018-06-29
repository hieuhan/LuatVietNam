using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.common;
using sms.utils;
using ICSoft.PartnerLib;
using System.Web.Script.Serialization;

public partial class Admin_SignersEdit : System.Web.UI.Page
{
    private int PaymentTransactionId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["PaymentTransactionId"] != null && Request.Params["PaymentTransactionId"].ToString() != "")
        {
            PaymentTransactionId = int.Parse(Request.Params["PaymentTransactionId"].ToString());
        }
        
    }
    private void BindData()
    {
        try
        {
            

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
            short SysMessageId = 0;
            byte SysMessageTypeId = 0;
            ZaloPayRefundResponse m_ZaloPayRefundResponse = new ZaloPayRefundResponse();
            ZaloPay m_ZaloPay = new ZaloPay();
            m_ZaloPayRefundResponse = m_ZaloPay.reFund(PaymentTransactionId, txtSignerName.Text);
            divResult.InnerHtml = m_ZaloPayRefundResponse.refundid + " - " + m_ZaloPayRefundResponse.returncode.ToString() + ": " +  m_ZaloPayRefundResponse.returnmessage;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            divResult.InnerHtml = ex.Message.ToString();
        }
    }

    private void ClearForm()
    {
        txtSignerName.Text = "";
        
    }
}