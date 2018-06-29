using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.HelperLib;
using ICSoft.CMSLib;

public partial class Admin_Pages_articles_CountriesEdit : System.Web.UI.Page
{
    private int ActUserId = 0;
    private short CountryId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (Request.Params["CountryId"] != null && Request.Params["CountryId"].ToString() != "")
            {
                CountryId = short.Parse(Request.Params["CountryId"].ToString());
            }
            if (!IsPostBack)
            {
                
                bindData();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    //--------------------------------------------------------------------------------

    private void bindData()
    {
        try
        {
            //Users m_User = new Users();
            Countries m_Countries = new Countries();
            m_Countries = m_Countries.Get(CountryId);
            if (m_Countries.CountryId > 0)
            {
                txtName.Text = m_Countries.CountryName;
                txtDesc.Text = m_Countries.CountryDesc;
                txtDisplayOrder.Text = m_Countries.DisplayOrder.ToString();
                if (!string.IsNullOrEmpty(m_Countries.IconPath))
                    txtIcon.Text = CmsConstants.ROOT_PATH + m_Countries.IconPath;
                else
                    txtIcon.Text = "";

            }
            else
            {
                txtName.Text = "";
                txtDesc.Text = "";
                txtDisplayOrder.Text = "";
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (txtName.Text == "" || txtDesc.Text == "")
            {
                JSAlert.Alert(GetLocalResourceObject("err").ToString(), this);
                return;
            }
            short SysMessageTypeId = 0;
            short SysMessageId = 0;
            Countries m_Countries = new Countries();
            m_Countries.CountryName = txtName.Text;
            m_Countries.CountryDesc = txtDesc.Text;
            m_Countries.DisplayOrder = Int16.Parse(txtDisplayOrder.Text);
            if (txtIcon.Text.StartsWith(CmsConstants.ROOT_PATH))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.ROOT_PATH.Length);
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_Countries.IconPath = txtIcon.Text;
            if (CountryId > 0)
            {
                //m_Countries.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                m_Countries.CountryId = CountryId;
                SysMessageTypeId = m_Countries.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_Countries.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            if (SysMessageTypeId == 2)
            {
                JSAlert.Alert(GetLocalResourceObject("Success").ToString(), this);
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