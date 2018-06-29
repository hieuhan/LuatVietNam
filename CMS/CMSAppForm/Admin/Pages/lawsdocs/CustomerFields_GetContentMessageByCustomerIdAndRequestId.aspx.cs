using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class Admin_Pages_lawsdocs_CustomerFields_GetContentMessageByCustomerIdAndRequestId : System.Web.UI.Page
{
    private int _customerId = 0, _messageSendRequestId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["CustomerId"] != null && Request.Params["CustomerId"].ToString() != "")
        {
            _customerId = int.Parse(Request.Params["CustomerId"].ToString());
        }
        if (Request.Params["MessageSendRequestId"] != null && Request.Params["MessageSendRequestId"].ToString() != "")
        {
            _messageSendRequestId = int.Parse(Request.Params["MessageSendRequestId"].ToString());
        }
        if (!IsPostBack)
        {
            if (_customerId > 0 && _messageSendRequestId > 0)
            {
                BindData();
            }
        }
    }

    private void BindData()
    {
        try
        {
            string Content =  new CustomerFields().CustomerFields_GetContentMessageByCustomerIdAndRequestId(_customerId,
                    _messageSendRequestId);
            ltContent.Text = string.IsNullOrEmpty(Content) ? "<span style='color:red; font-weight: bold;'>Nội dung mail trống (Không có văn bản nào thuộc lĩnh vực khách hàng này quan tâm).</span>" : Content;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}