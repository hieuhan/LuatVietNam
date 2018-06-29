using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class Admin_CustomerField_Add : System.Web.UI.Page
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
            List<Fields> l_Fields = new Fields().GetListApproved();
            CheckBoxListHelpers.Bind(chkFields, l_Fields, "");
            //CheckBoxListHelpers.Bind(chkFieldsTCVN, l_Fields, "");
            for (int i = 0; i < chkFields.Items.Count; i++)
            {
                if (!chkFields.Items[i].Text.StartsWith("&nbsp"))
                {
                    chkFields.Items[i].Attributes.Add("style", "font-weight:bold;");
                }
            }
            if (CustomerId > 0)
            {
                CustomerFields mCustomerFields = new CustomerFields();
                List<CustomerFields> lCustomerFields = mCustomerFields.GetListFieldsById(CustomerId, 1, 2);
                List<CustomerFields> lCustomerFieldsTCVN = mCustomerFields.GetListFieldsById(CustomerId, 3, 2);
                for (int i = 0; i < lCustomerFields.Count; i++)
                {
                    CheckBoxListHelpers.SetSelected(chkFields, lCustomerFields[i].FieldId.ToString(), "Yellow");
                }
                for (int i = 0; i < lCustomerFieldsTCVN.Count; i++)
                {
                    CheckBoxListHelpers.SetSelected(chkFields, lCustomerFieldsTCVN[i].FieldId.ToString(), "Yellow");
                } 
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string listFieldId = "";
            if (CustomerId > 0)
            {
                CustomerFields mCustomerFields = new CustomerFields();
                mCustomerFields.CustomerId = CustomerId;
                mCustomerFields.DocGroupId = 1;
                mCustomerFields.DisplayOrder = 0;
                for (int i = 0; i < chkFields.Items.Count; i++)
                {
                    if (chkFields.Items[i].Selected)
                    {
                        listFieldId += chkFields.Items[i].Value;
                        listFieldId += ",";
                    }
                }
                if (string.IsNullOrEmpty(listFieldId))
                {
                    lblMsg.Text = "Bạn phải chọn ít nhất một lĩnh vực.";
                    return;
                }
                mCustomerFields.InsertByListId(listFieldId);
                //TCVN
                CustomerFields mCustomerFieldsTCVN = new CustomerFields();
                mCustomerFieldsTCVN.CustomerId = CustomerId;
                mCustomerFieldsTCVN.DocGroupId = 3;
                mCustomerFieldsTCVN.DisplayOrder = 0;
                mCustomerFieldsTCVN.InsertByListId(listFieldId);
            }
            BindData();
            lblMsg.Text = "Chọn lĩnh vực quan tâm thành công.";
            ////JSAlertHelpers.close(this);
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