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

public partial class Admin_DocIndexEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int DocId = 0;
    private int DocIndexId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["DocIndexId"] != null && Request.Params["DocIndexId"].ToString() != "")
        {
            DocIndexId = int.Parse(Request.Params["DocIndexId"].ToString());
        }
        if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
        {
            DocId = int.Parse(Request.Params["DocId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (DocIndexId > 0)
            {
                DocIndexes mDocIndexes = new DocIndexes();
                mDocIndexes = mDocIndexes.Get(DocIndexId);
                if (mDocIndexes.DocIndexId > 0)
                {
                    txtTitle.Text = mDocIndexes.Title.ToString();
                    txtBookmark.Text = mDocIndexes.Bookmark.ToString();
                    ddlLevelId.SelectedValue = mDocIndexes.LevelId.ToString();
                    txtDisplayOrder.Text = mDocIndexes.DisplayOrder.ToString();
                }
                else
                {
                    txtTitle.Text = "";
                    txtBookmark.Text = "";
                    ddlLevelId.SelectedValue = "1";
                    txtDisplayOrder.Text = "";
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
            short SysMessageId = 0;
            DocIndexes mDocIndexes = new DocIndexes();
            mDocIndexes.Title = txtTitle.Text.Trim();
            mDocIndexes.Bookmark = txtBookmark.Text.Trim();
            mDocIndexes.LevelId = byte.Parse(ddlLevelId.SelectedValue);
            mDocIndexes.DisplayOrder = short.Parse((txtDisplayOrder.Text == "") ? "0" : txtDisplayOrder.Text.Trim());
            mDocIndexes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            mDocIndexes.DocId = DocId;
            mDocIndexes.DocIndexId = DocIndexId;
            mDocIndexes.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (SysMessageId > 0)
            {
                Response.Redirect(CmsConstants.PRJ_ROOT+"pages/lawsdocs/DocIndexes.aspx?DocId="+DocId+"&LanguageId="+LanguageId, false);
            }
            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit2').dialog('close');" +
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