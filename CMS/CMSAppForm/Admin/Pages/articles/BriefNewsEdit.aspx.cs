using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.HelperLib;

public partial class Admin_BriefNewsEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int ArticleId = 0;
    private int ActUserId = 0;
    private short CategoryId = 0;
    private short SiteId = 0;
    protected byte DataTypeId = 10;
    private byte EnableDataType = 1;
    private short ActionId = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();


        if (Request.Params["ActionId"] != null && Request.Params["ActionId"].ToString() != "")
        {
            ActionId = short.Parse(Request.Params["ActionId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (Request.Params["EnableDataType"] != null && Request.Params["EnableDataType"].ToString() != "")
        {
            EnableDataType = byte.Parse(Request.Params["EnableDataType"].ToString());
        }
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (Request.Params["CategoryId"] != null && Request.Params["CategoryId"].ToString() != "")
        {
            CategoryId = short.Parse(Request.Params["CategoryId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["AppTypeId"] != null && Request.Params["AppTypeId"].ToString() != "")
        {
            ApplicationTypeId = byte.Parse(Request.Params["AppTypeId"].ToString());
        }
        if (!IsPostBack)
        {
            if (ArticleId > 0)
            {
                BindData();
            }
            else
            {
                DropDownListHelpers.BindDropDownList(ddlSite, Sites.Static_GetList(ActUserId), SiteId.ToString());
                DropDownListHelpers.BindDropDownList(ddlDataType, DataTypes.Static_GetList(), DataTypeId.ToString());
                DropDownListHelpers.DDL_Bind(ddlArticleType, ArticleTypes.Static_GetList(byte.Parse(ddlDataType.SelectedValue)), "...","1");
                DropDownListHelpers.DDL_Bind(ddlIconStatus, IconStatus.Static_GetList(), "...");
                DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
                DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", ApplicationTypeId.ToString());
                List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
                DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
                //l_Category = Categories.Static_GetAllHierachy(ActUserId, "", 0, byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "&nbsp;&nbsp;&nbsp;");
                CheckBoxListHelpers.Bind(chkCategory, l_Category, "");
                DropDownListHelpers.DDLDataSources_BindByCulture(ddlSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ddlDataType.SelectedValue)), "...");
                
                DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");

                ddlCategory.SelectedValue = "502";

                HightLight(ddlCategory);
                HightLight(chkCategory);
                ClearForm();
                chkShowWeb.Checked = true;
            }
        }

    }

    private void BindData()
    {
        try
        {
            bool IsAlert = false;
            if (ArticleId > 0)
            {
                Articles m_Articles = new Articles();
                m_Articles.ArticleId = ArticleId;
                m_Articles.LanguageId = LanguageId;
                m_Articles.ApplicationTypeId = ApplicationTypeId;
                m_Articles = m_Articles.Get();
                if (m_Articles.ArticleId > 0)
                {
                    DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", m_Articles.SiteId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlDataType, DataTypes.Static_GetList(), "", m_Articles.DataTypeId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlArticleType, ArticleTypes.Static_GetList(byte.Parse(ddlDataType.SelectedValue)), "...", m_Articles.ArticleTypeId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlIconStatus, IconStatus.Static_GetList(), "...", m_Articles.IconStatusId.ToString());
                    DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", m_Articles.LanguageId.ToString());
                    DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", m_Articles.ApplicationTypeId.ToString());
                    List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
                    DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", m_Articles.CategoryId.ToString());
                    ddlCategory.SelectedValue = "502";
                    //l_Category = Categories.Static_GetAllHierachy(ActUserId, "", 0, byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "&nbsp;&nbsp;&nbsp;");
                    CheckBoxListHelpers.Bind(chkCategory, l_Category, "");
                    //DropDownListHelpers.DDLDataSources_BindByCulture(ddlSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_ARTICLES"])), "...");
                    DropDownListHelpers.DDLDataSources_BindByCulture(ddlSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ddlDataType.SelectedValue)), "...");
                    DropDownListHelpers.DDL_SetSelected(ddlSource, m_Articles.DataSourceId.ToString());
                    DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "", m_Articles.ReviewStatusId.ToString());
                    
                    HightLight(ddlCategory);
                    HightLight(chkCategory);

                    chkIsVerify.Checked = m_Articles.IsVerify == 1 ? true : false;
                    chkShowTop.Checked = m_Articles.ShowTop == 1 ? true : false;
                    chkShowBottom.Checked = m_Articles.ShowBottom == 1 ? true : false;
                    chkShowWeb.Checked = m_Articles.ShowWeb == 1 ? true : false;
                    chkShowWap.Checked = m_Articles.ShowWap == 1 ? true : false;
                    chkShowApp.Checked = m_Articles.ShowApp == 1 ? true : false;
                    txtSummary.Text = m_Articles.Summary;
                    txtSummaryPlain.Text = m_Articles.Summary;
                    txtTitle.Text = m_Articles.Title;
                    txtUrl.Text = m_Articles.SourceUrl;
                    txtArticleUrl.Text = m_Articles.ArticleUrl;
                    txtDisplayStartTime.Text = m_Articles.DisplayStartTime==DateTime.MinValue ? "" : m_Articles.DisplayStartTime.ToString("dd/MM/yyyy HH:mm:ss");
                    txtDisplayEndTime.Text = m_Articles.DisplayEndTime == DateTime.MinValue ? "" : m_Articles.DisplayEndTime.ToString("dd/MM/yyyy HH:mm:ss");
                    txtDisplayOrder.Text = m_Articles.DisplayOrder.ToString();
                    ArticleCategories m_ArticleCategories = new ArticleCategories();
                    List<ArticleCategories> l_ArticleCategories = m_ArticleCategories.GetListByArticleId(m_Articles.ArticleId);
                    CheckBoxListHelpers.SetSelected(chkCategory, m_Articles.CategoryId.ToString(), "Yellow");
                    for (int i = 0; i < l_ArticleCategories.Count; i++)
                    {
                        CheckBoxListHelpers.SetSelected(chkCategory, l_ArticleCategories[i].CategoryId.ToString(), "Yellow");
                    }
                    if (IsAlert)
                    {
                        JSAlertHelpers.ShowNotify("10", "", "Dữ liệu được lấy từ bài viết có ngôn ngữ và nền tảng mặc định", this);
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    private void HightLight(DropDownList ddl)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            if (!ddl.Items[i].Text.StartsWith("-"))
            {
                ddl.Items[i].Attributes.Add("style", "font-weight:bold;");
            }
        }
    }
    private void HightLight(CheckBoxList cbl)
    {
        for (int i = 0; i < cbl.Items.Count; i++)
        {
            //if (!cbl.Items[i].Text.StartsWith("&nbsp"))
            //{
            //    cbl.Items[i].Attributes.Add("style", "font-weight:bold;");
            //}
            if (!cbl.Items[i].Text.StartsWith("-"))
            {
                cbl.Items[i].Attributes.Add("style", "font-weight:bold;");
            }
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
    protected void SaveData()
    {
        try
        {
            short SysMessageId = 0;
            Articles m_Articles = new Articles();
            m_Articles.ArticleId = ArticleId;
            m_Articles.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_Articles.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            if (ArticleId > 0) m_Articles = m_Articles.Get();//refresh data
            
            m_Articles.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_Articles.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_Articles.CategoryId = short.Parse(ddlCategory.SelectedValue);
            m_Articles.DataSourceId = short.Parse(ddlSource.SelectedValue);
            m_Articles.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Articles.DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            m_Articles.ArticleTypeId = byte.Parse(ddlArticleType.SelectedValue);
            m_Articles.IconStatusId = byte.Parse(ddlIconStatus.SelectedValue);
            m_Articles.InventoryStatusId = 0;
            m_Articles.CurrencyId = 0;
            m_Articles.Title = txtTitle.Text;
            m_Articles.SourceUrl = txtUrl.Text;
            m_Articles.Summary = txtSummaryPlain.Text;
            m_Articles.ArticleUrl = txtArticleUrl.Text;
            m_Articles.DisplayStartTime = txtDisplayStartTime.Text != "" ? DateTime.Parse(txtDisplayStartTime.Text) : DateTime.MinValue;
            m_Articles.DisplayEndTime = txtDisplayEndTime.Text != "" ? DateTime.Parse(txtDisplayEndTime.Text) : DateTime.MinValue;
            m_Articles.DisplayOrder = txtDisplayOrder.Text != "" ? int.Parse(txtDisplayOrder.Text) : 100;
            m_Articles.ArticleCode = "";
            m_Articles.OriginalPrice = 0;
            m_Articles.SalePrice =  0;
            m_Articles.ContactPrice = "";
            m_Articles.MetaTitle = "";
            m_Articles.MetaDesc = "";
            m_Articles.MetaKeyword = "";
            m_Articles.IsVerify = chkIsVerify.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowApp = 1;
            m_Articles.ShowBottom = 1;
            m_Articles.ShowTop = 1;
            m_Articles.ShowWap = 1;
            m_Articles.ShowWeb = 1;

                m_Articles.ArticleContent = "";
                m_Articles.SourceUrl = "";
            m_Articles.ImagePath = "";
            m_Articles.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Articles.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (m_Articles.ArticleId > 0)
            {
                ArticleCategories m_ArticleCategories = new ArticleCategories();
                m_ArticleCategories.ArticleId = m_Articles.ArticleId;
                m_ArticleCategories.DeleteByArticleId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                for (int i = 0; i < chkCategory.Items.Count; i++)
                {
                    if (chkCategory.Items[i].Selected)
                    {
                        m_ArticleCategories.CategoryId = short.Parse(chkCategory.Items[i].Value);
                        m_ArticleCategories.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
                m_ArticleCategories.CategoryId = m_Articles.CategoryId;
                m_ArticleCategories.InsertMainCate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            }
            if (cblAddOther.Checked)
            {
                if (ArticleId > 0) { JSAlertHelpers.ShowNotify("15", "success", "Cập nhật thành công.", this); }
                else { JSAlertHelpers.ShowNotify("15", "success", "Thêm mới thành công.", this); }
                ClearForm();
                return;
            }
            if (ArticleId > 0) { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật thành công.", this); }
            else { JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới thành công.", this); }
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
    private void ClearForm()
    {
        txtSummary.Text = "";
        txtTitle.Text = "";
        txtUrl.Text = "";
        txtArticleUrl.Text = "";
        txtDisplayStartTime.Text = "";
        txtDisplayEndTime.Text = "";
        txtDisplayOrder.Text = "";
        chkIsVerify.Checked = false;
        chkShowApp.Checked = false;
        chkShowBottom.Checked = false;
        chkShowTop.Checked = false;
        chkShowWap.Checked = false;
        chkShowWeb.Checked = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
        //string script = @"<script language='JavaScript'>" +
        //       "window.parent.jQuery('#divEdit').dialog('close');" +
        //       "</script>";
        //ClientScriptManager csm = this.ClientScript;
        //csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        //if (CategoryId == 0)
        //    CategoryId = short.Parse(ddlCategory.SelectedValue);
        // Response.Redirect("BriefNews.aspx?SiteId=" + SiteId.ToString() + "&DataTypeId=" + DataTypeId.ToString() + "&EnableDataType=" + EnableDataType.ToString() + "&ActionId=" + ActionId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&AppTypeId=" + ApplicationTypeId.ToString() + "&CategoryId=" + CategoryId.ToString());
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        SaveData();
        Response.Redirect("BriefNewsEdit.aspx?SiteId=" + SiteId.ToString() + "&DataTypeId=" + DataTypeId.ToString() + "&EnableDataType=0&ActionId=" + ActionId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&AppTypeId=" + ApplicationTypeId.ToString());
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //if (CategoryId == 0)
        //    CategoryId = short.Parse(ddlCategory.SelectedValue);
        //Response.Redirect("BriefNews.aspx?SiteId=" + SiteId.ToString() + "&DataTypeId=" + DataTypeId.ToString() + "&EnableDataType=" + EnableDataType.ToString() + "&ActionId=" + ActionId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&AppTypeId=" + ApplicationTypeId.ToString() + "&CategoryId=" + CategoryId.ToString());
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        HightLight(ddlCategory);
        HightLight(chkCategory);
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
        CheckBoxListHelpers.Bind(chkCategory, l_Category, "");
        HightLight(ddlCategory);
        HightLight(chkCategory);
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
        CheckBoxListHelpers.Bind(chkCategory, l_Category, "");
        DropDownListHelpers.DDL_Bind(ddlArticleType, ArticleTypes.Static_GetList(byte.Parse(ddlDataType.SelectedValue)), "...");
        HightLight(ddlCategory);
        HightLight(chkCategory);
    }

}