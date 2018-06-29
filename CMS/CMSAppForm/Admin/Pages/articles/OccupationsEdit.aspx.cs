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

public partial class Admin_Pages_articles_OccupationsEdit : System.Web.UI.Page
{
    private short OccupationId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["OccupationId"] != null && Request.Params["OccupationId"].ToString() != "")
        {
            OccupationId = short.Parse(Request.Params["OccupationId"].ToString());
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
            if (OccupationId > 0)
            {
                Occupations m_Occupations = new Occupations();
                m_Occupations = m_Occupations.Get(OccupationId);
                if (m_Occupations.OccupationId > 0)
                {
                    txtOccupationName.Text = m_Occupations.OccupationName.ToString();
                    txtOccupationDesc.Text = m_Occupations.OccupationDesc.ToString();
                }
                else
                {
                    txtOccupationName.Text = "";
                    txtOccupationDesc.Text = "";
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
            if (txtOccupationName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên không được để trống";
                return;
            }
            if (txtOccupationDesc.Text.Trim() == "")
            {
                lblMsg.Text = "Mô tả  không được để trống";
                return;
            }
            short SysMessageTypeId = 0;
            short SysMessageId = 0;
            Occupations m_Occupations = new Occupations();
            m_Occupations.OccupationId = OccupationId;
            m_Occupations.OccupationName = txtOccupationName.Text.ToString();
            m_Occupations.OccupationDesc = txtOccupationDesc.Text.ToString();
            if (OccupationId > 0)
            {
                m_Occupations.OccupationId = OccupationId;
                SysMessageTypeId = m_Occupations.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_Occupations.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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