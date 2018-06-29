using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_Pages_articles_FeatureGroupsEdit : System.Web.UI.Page
{
    private short FeatureGroupId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["FeatureGroupId"] != null && Request.Params["FeatureGroupId"].ToString() != "")
        {
            FeatureGroupId = short.Parse(Request.Params["FeatureGroupId"].ToString());
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
            if (FeatureGroupId > 0)
            {
                FeatureGroups m_FeatureGroups = new FeatureGroups();
                m_FeatureGroups = m_FeatureGroups.Get(FeatureGroupId);
                if (m_FeatureGroups.FeatureGroupId > 0)
                {
                    txtFeatureGroupName.Text = m_FeatureGroups.FeatureGroupName.ToString();
                    txtFeatureGroupDesc.Text = m_FeatureGroups.FeatureGroupDesc.ToString();
                }
                else
                {
                    txtFeatureGroupName.Text = "";
                    txtFeatureGroupDesc.Text = "";
                }
            }

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (txtFeatureGroupName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên không được để trống";
                return;
            }
            if (txtFeatureGroupDesc.Text.Trim() == "")
            {
                lblMsg.Text = "Mô tả  không được để trống";
                return;
            }
            short SysMessageTypeId = 0;
            short SysMessageId = 0;
            FeatureGroups m_FeatureGroups = new FeatureGroups();
            m_FeatureGroups.FeatureGroupId = FeatureGroupId;
            m_FeatureGroups.FeatureGroupName = txtFeatureGroupName.Text.ToString();
            m_FeatureGroups.FeatureGroupDesc = txtFeatureGroupDesc.Text.ToString();
            if (FeatureGroupId > 0)
            {
                m_FeatureGroups.FeatureGroupId = FeatureGroupId;
                SysMessageTypeId = m_FeatureGroups.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_FeatureGroups.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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