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

public partial class Admin_CategoryCrawlersEdit : System.Web.UI.Page
{
    String csName = "csbMessage";
    Type csType;
    ClientScriptManager cs;
    StringBuilder csText = new StringBuilder();
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int CategoryCrawlerId = 0;
    private short SiteId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CategoryCrawlerId"] != null && Request.Params["CategoryCrawlerId"].ToString() != "")
        {
            CategoryCrawlerId = short.Parse(Request.Params["CategoryCrawlerId"].ToString());
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
            DropDownListHelpers.DDLDataTypes_BindByCulture(ddlDataType, DataTypes.Static_GetList(), "");
            DataSources m_DataSources = new DataSources();
            DropDownListHelpers.DDL_Bind(ddlDataSource, m_DataSources.GetListByDataTypeId(byte.Parse(ddlDataType.SelectedValue)), "");
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 0, 0, 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            DropDownListHelpers.DDLStatus_BindByCulture(ddlReviewStatus, Status.Static_GetList(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (CategoryCrawlerId > 0)
            {
                CategoryCrawlers m_CategoryCrawlers = new CategoryCrawlers();
                m_CategoryCrawlers.CategoryCrawlerId = CategoryCrawlerId;
                m_CategoryCrawlers = m_CategoryCrawlers.Get();
                if (m_CategoryCrawlers.CategoryCrawlerId > 0)
                {
                    txtMaxItem.Text = m_CategoryCrawlers.MaxItem.ToString();
                    txtUrl.Text = m_CategoryCrawlers.LinkGetList;
                    DropDownListHelpers.DDL_SetSelected(ddlReviewStatus, m_CategoryCrawlers.StatusId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, Categories.Static_Get(m_CategoryCrawlers.CategoryId,0,0).SiteId.ToString());
                    List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 0, 0, 0, 0, "--");
                    DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...", m_CategoryCrawlers.CategoryId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlLinkType, m_CategoryCrawlers.LinkType);
                    DropDownListHelpers.DDL_SetSelected(ddlDataSource, m_CategoryCrawlers.DataSourceId.ToString());
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
            CategoryCrawlers m_CategoryCrawlers = new CategoryCrawlers();
            m_CategoryCrawlers.CategoryCrawlerId = CategoryCrawlerId;
            m_CategoryCrawlers.CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            m_CategoryCrawlers.CrUserId = ActUserId;
            m_CategoryCrawlers.DataSourceId = short.Parse(ddlDataSource.SelectedValue);
            m_CategoryCrawlers.DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            m_CategoryCrawlers.LinkGetList = txtUrl.Text;
            m_CategoryCrawlers.LinkType = ddlLinkType.SelectedValue;
            m_CategoryCrawlers.MaxItem = txtMaxItem.Text == "" ? 5 : int.Parse(txtMaxItem.Text);
            m_CategoryCrawlers.StatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_CategoryCrawlers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 0, 0, 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSources m_DataSources = new DataSources();
        DropDownListHelpers.DDL_Bind(ddlDataSource, m_DataSources.GetListByDataTypeId(byte.Parse(ddlDataType.SelectedValue)), "");

        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 0, 0, 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
    }
}