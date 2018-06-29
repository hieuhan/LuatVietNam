using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_CrawlerRulesEdit : System.Web.UI.Page
{
    String csName = "csbMessage";
    Type csType;
    ClientScriptManager cs;
    StringBuilder csText = new StringBuilder();
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int CrawlerRuleId = 0;
    private short SiteId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CrawlerRuleId"] != null && Request.Params["CrawlerRuleId"].ToString() != "")
        {
            CrawlerRuleId = short.Parse(Request.Params["CrawlerRuleId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
            {
                ApplicationTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
            }
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            DropDownListHelpers.DDL_Bind(ddlContentType, CrawlerContentTypes.Static_GetList(), "");
            DataSources m_DataSources = new DataSources();
            DropDownListHelpers.DDL_Bind(ddlDataSource, m_DataSources.GetListByDataTypeId(1), "");
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 1, 0, 0, 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlReviewStatus, Status.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (CrawlerRuleId > 0)
            {
                CrawlerRules m_CrawlerRules = new CrawlerRules();
                m_CrawlerRules.CrawlerRuleId = CrawlerRuleId;
                m_CrawlerRules = m_CrawlerRules.Get();
                if (m_CrawlerRules.CrawlerRuleId > 0)
                {
                    txtPosition.Text = m_CrawlerRules.DataPosition.ToString();
                    txtTagClass.Text = m_CrawlerRules.TagClass;
                    txtTagId.Text = m_CrawlerRules.TagId;
                    txtTagName.Text = m_CrawlerRules.TagName;
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_CrawlerRules.StatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, Categories.Static_Get(m_CrawlerRules.CategoryId, 0, 0).SiteId.ToString());
                    List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 1, 0, 0, 0, 0, "--");
                    DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...", m_CrawlerRules.CategoryId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlContentType, m_CrawlerRules.CrawlerContentTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlDataSource, m_CrawlerRules.DataSourceId.ToString());
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    //--------------------------------------------------------------------------------
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            short SysMessageId = 0;
            CrawlerRules m_CrawlerRules = new CrawlerRules();
            m_CrawlerRules.CrawlerRuleId = CrawlerRuleId;
            m_CrawlerRules.CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            m_CrawlerRules.CrUserId = ActUserId;
            m_CrawlerRules.DataSourceId = short.Parse(ddlDataSource.SelectedValue);
            m_CrawlerRules.CrawlerContentTypeId = byte.Parse(ddlContentType.SelectedValue);
            m_CrawlerRules.TagName = txtTagName.Text;
            m_CrawlerRules.TagId = txtTagId.Text;
            m_CrawlerRules.TagClass = txtTagClass.Text;
            m_CrawlerRules.DataPosition = txtPosition.Text == "" ? 0 : int.Parse(txtPosition.Text);
            m_CrawlerRules.StatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_CrawlerRules.CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            m_CrawlerRules.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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


    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 1, 0, 0, 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
    }
    
}