using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.common;
using HtmlAgilityPack;
using sms.utils;

public partial class Admin_DocsUpdateContentEdit : System.Web.UI.Page
{
    private int DocId = 0;
    private byte LanguageId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
        {
            DocId = int.Parse(Request.Params["DocId"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());           
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (DocId > 0)
            {
                Docs m_Docs = new Docs();
                m_Docs = m_Docs.Get(DocId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_Docs.DocId > 0)
                {
                    lblDocNameEdit.Text = String.Format("{0:}", m_Docs.DocName.ToString());
                    txtDocContent.Text = String.Format("{0:}", m_Docs.DocContent.ToString());
                    //if (m_Docs.ReviewStatusId == 2 && m_Docs.DocContent!=null && m_Docs.DocContent.ToString()!="")
                    //{

                    //    btnSave.Enabled = false;
                    //    btnSave.ToolTip = "Không thể sửa nội dung văn bản khi đã duyệt ";
                    //    JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa nội dung văn bản khi đã duyệt.", this);
                    //}
                    //else
                    //{
                    //    btnSave.Enabled = true;
                    //    //btnSave.ToolTip = "Lưu và tiếp tục sửa văn bản hiện tại";
                    //}
                    //tam thoi khong check
                    btnSave.Enabled = true;
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
        try
        {
            ProcessFootNote();
            short SysMessageId = 0;
            byte SysMessageTypeId = 0;
            string messages = string.Empty;
            Docs m_Docs = new Docs();
            m_Docs.DocId = DocId;
            m_Docs.DocContent = txtDocContent.Text.ToString();
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            m_Docs.LanguageId = LanguageId;
            SysMessageTypeId = m_Docs.UpdateContent(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            switch (SysMessageTypeId)
            {
                case 1:
                    messages = string.Format("Cập nhật nội dung không thành công : {0}", SysMessages.Static_GetDesc(SysMessageId));
                    break;
                case 0:
                case 2:
                    messages = "Cập nhật nội dung thành công";
                    break;
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
            //JSAlertHelpers.close(this);
            //string script = @"<script language='JavaScript'>" +
            //    "window.parent.jQuery('#divEdit').dialog('close');" +
            //    "</script>";
            //ClientScriptManager csm = this.ClientScript;
            //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
            //JSAlertHelpers.Alert("Cập nhật nội dung thành công", this);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private void ProcessFootNote()
    {
        HtmlDocument m_HtmlDocument = new HtmlDocument();
        m_HtmlDocument.LoadHtml(txtDocContent.Text);
        HtmlNodeCollection l_NodeA = m_HtmlDocument.DocumentNode.SelectNodes("//a");
        if (l_NodeA != null)
        {
            for (int indexNode = 0; indexNode < l_NodeA.Count; indexNode++)
            {
                HtmlNode m_HtmlNode = l_NodeA[indexNode];
                try
                {
                    if (m_HtmlNode.Attributes["href"].Value.ToString().Contains("#") && !m_HtmlNode.InnerHtml.Contains("<sup") && m_HtmlNode.InnerText.Contains("[") && m_HtmlNode.InnerText.Contains("]"))
                    {
                        m_HtmlNode.InnerHtml = m_HtmlNode.InnerHtml.Replace(m_HtmlNode.InnerText, "<sup>" + m_HtmlNode.InnerText + "</sup>");
                    }

                }
                catch (Exception ex)
                {
                    sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
                }
            }

            txtDocContent.Text = m_HtmlDocument.DocumentNode.InnerHtml;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        
        Docs m_Docs = new Docs();
        m_Docs = m_Docs.Get(DocId, LanguageId);
        if (!String.IsNullOrEmpty(Request["backUrl"]))
        {
            string backUrl = Request["backUrl"];
            Response.Redirect(backUrl + "?LanguageId=" + LanguageId.ToString());
        }
        else
        {
            if (m_Docs.DocGroupId <= 2)
                Response.Redirect("Docs.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 3)
                Response.Redirect("VietNamStandards.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 4)
                Response.Redirect("DocsTTHC.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 5)
                Response.Redirect("DocsConsolidation.aspx?LanguageId=" + LanguageId.ToString());
        }
        
    }
}
