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

public partial class Admin_ArticlesEdit : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int ArticleId = 0;
    private int ActUserId = 0;
    protected short CategoryId = 0;
    protected short SiteId = 0;
    protected byte DataTypeId = 0;
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
            ddlDataType.Enabled = EnableDataType == 1 ? true : false;
            ShowHideInput();
            if (DataTypeId != 12) divImageCover.Visible = false; //khong phai Ca sy

            if (ArticleId > 0)
            {
                BindData();
            }
            else
            {
                DropDownListHelpers.BindDropDownList(ddlSite, Sites.Static_GetList(ActUserId), SiteId.ToString());
                DropDownListHelpers.BindDropDownList(ddlDataType, DataTypes.Static_GetList(), DataTypeId.ToString());
                DropDownListHelpers.DDL_Bind(ddlArticleType, ArticleTypes.Static_GetList(byte.Parse(ddlDataType.SelectedValue)), "...", "1");
                DropDownListHelpers.DDL_Bind(ddlIconStatus, IconStatus.Static_GetList(), "...");
                DropDownListHelpers.DDL_Bind(ddlInventoryStatus, InventoryStatus.Static_GetList(), "");
                DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
                DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", ApplicationTypeId.ToString());
                List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
                DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", CategoryId.ToString());
                //l_Category = Categories.Static_GetAllHierachy(ActUserId, "", 0, byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "&nbsp;&nbsp;&nbsp;");
                CheckBoxListHelpers.Bind(chkCategory, l_Category, "");
                DropDownListHelpers.DDLDataSources_BindByCulture(ddlSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ddlDataType.SelectedValue)), "...");

                DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "");
                DropDownListHelpers.DDL_Bind(ddlCurrency, Currencies.Static_GetList(), "");

                if (DataTypeId == 7 || DataTypeId == 1)
                {
                    short ProvinceId = 0;
                    short DistrictId = 0;
                    int WardId = 0;
                    DropDownListHelpers.DDL_Bind(ddlProvince, Provinces.Static_GetList(1), "...", ProvinceId.ToString());
                    if (ddlProvince.Items.Count > 0)
                    {
                        ProvinceId = short.Parse(ddlProvince.SelectedValue);
                    }
                    DropDownListHelpers.DDL_Bind(ddlDistrict, Districts.Static_GetList(byte.Parse(ProvinceId.ToString())), "...", DistrictId.ToString());
                    if (ddlDistrict.Items.Count > 0)
                    {
                        DistrictId = short.Parse(ddlDistrict.SelectedValue);
                    }
                    DropDownListHelpers.DDL_Bind(ddlWard, Wards.Static_GetList(byte.Parse(DistrictId.ToString())), "...", WardId.ToString());
                    TextBoxAddress.Text = "";
                    TextBoxLongitude.Value = "0";
                    TextBoxLatitude.Value = "0";
                }

                BindFeatures(byte.Parse(ddlDataType.SelectedValue), short.Parse(ddlCategory.SelectedValue), ArticleId);
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
            string FieldName = "";
            if (ArticleId > 0)
            {
                Articles m_Articles = new Articles();
                ArticleFields m_ArticleFields = new ArticleFields();
                m_Articles.ArticleId = ArticleId;
                m_Articles.LanguageId = LanguageId;
                m_Articles.ApplicationTypeId = ApplicationTypeId;
                m_Articles = m_Articles.Get();
                if (m_Articles.ArticleId > 0)
                {
                    CategoryId = m_Articles.CategoryId;
                    DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", m_Articles.SiteId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlDataType, DataTypes.Static_GetList(), "", m_Articles.DataTypeId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlArticleType, ArticleTypes.Static_GetList(byte.Parse(ddlDataType.SelectedValue)), "...", m_Articles.ArticleTypeId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlIconStatus, IconStatus.Static_GetList(), "...", m_Articles.IconStatusId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlInventoryStatus, InventoryStatus.Static_GetList(), "", m_Articles.InventoryStatusId.ToString());
                    DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", m_Articles.LanguageId.ToString());
                    DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "", m_Articles.ApplicationTypeId.ToString());
                    List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "- ");
                    DropDownListHelpers.DDL_Bind(ddlCategory, l_Category, "...", m_Articles.CategoryId.ToString());
                    //l_Category = Categories.Static_GetAllHierachy(ActUserId, "", 0, byte.Parse(ddlDataType.SelectedValue), byte.Parse(ddlLanguage.SelectedValue), byte.Parse(ddlAppType.SelectedValue), 0, 0, "&nbsp;&nbsp;&nbsp;");
                    CheckBoxListHelpers.Bind(chkCategory, l_Category, "");
                    //DropDownListHelpers.DDLDataSources_BindByCulture(ddlSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_ARTICLES"])), "...");
                    DropDownListHelpers.DDLDataSources_BindByCulture(ddlSource, DataSources.Static_GetListByDataTypeId(byte.Parse(ddlDataType.SelectedValue)), "...");
                    DropDownListHelpers.DDL_SetSelected(ddlSource, m_Articles.DataSourceId.ToString());
                    DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "", m_Articles.ReviewStatusId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlCurrency, Currencies.Static_GetList(), "", m_Articles.CurrencyId.ToString());
                    if (DataTypeId == 7 || DataTypeId == 1)
                    {
                        string strprovinces = "";
                        ArticleLocation m_ArticleLocation = new ArticleLocation();
                        m_ArticleLocation.ArticleId = ArticleId;
                        List<ArticleLocation> l_ArticleLocation = m_ArticleLocation.ArticleLocation_Search("", "", "", 50, 0);
                        foreach (ArticleLocation mArticleLocation in l_ArticleLocation)
                        {
                            strprovinces += Provinces.Static_Get(mArticleLocation.ProvinceId).ProvinceDesc + "; ";
                        }
                        txtProvices.Text = strprovinces;
                        txtFields.Text = m_ArticleFields.ArticleFields_GetFieldName(1, ArticleId, ref FieldName);
                        short ProvinceId = 0;
                        short DistrictId = 0;
                        int WardId = 0;

                        DropDownListHelpers.DDL_Bind(ddlProvince, Provinces.Static_GetList(1), "...", m_ArticleLocation.ProvinceId.ToString());

                        if (ddlProvince.Items.Count > 0)
                        {
                            ProvinceId = short.Parse(ddlProvince.SelectedValue);
                        }
                        DropDownListHelpers.DDL_Bind(ddlDistrict, Districts.Static_GetList(ProvinceId), "...", m_ArticleLocation.DistrictId.ToString());
                        if (ddlDistrict.Items.Count > 0)
                        {
                            DistrictId = short.Parse(ddlDistrict.SelectedValue);
                        }
                        DropDownListHelpers.DDL_Bind(ddlWard, Wards.Static_GetList(DistrictId), "...", m_ArticleLocation.WardId.ToString());
                        TextBoxAddress.Text = m_ArticleLocation.Address;
                        TextBoxLongitude.Value = m_ArticleLocation.Longitude.ToString();
                        TextBoxLatitude.Value = m_ArticleLocation.Latitude.ToString();
                    }

                    HightLight(ddlCategory);
                    HightLight(chkCategory);

                    chkIsVerify.Checked = m_Articles.IsVerify == 1 ? true : false;
                    chkShowTop.Checked = m_Articles.ShowTop == 1 ? true : false;
                    chkShowBottom.Checked = m_Articles.ShowBottom == 1 ? true : false;
                    chkShowWeb.Checked = m_Articles.ShowWeb == 1 ? true : false;
                    chkShowWap.Checked = m_Articles.ShowWap == 1 ? true : false;
                    chkShowApp.Checked = m_Articles.ShowApp == 1 ? true : false;
                    txtContent.Text = m_Articles.ArticleContent;
                    txtSummary.Text = m_Articles.Summary;
                    txtSummaryPlain.Text = m_Articles.Summary;
                    txtTitle.Text = m_Articles.Title;
                    txtUrl.Text = m_Articles.SourceUrl;
                    txtArticleUrl.Text = m_Articles.ArticleUrl;
                    txtDisplayStartTime.Text = m_Articles.DisplayStartTime == DateTime.MinValue ? "" : m_Articles.DisplayStartTime.ToString("dd/MM/yyyy HH:mm:ss");
                    txtDisplayEndTime.Text = m_Articles.DisplayEndTime == DateTime.MinValue ? "" : m_Articles.DisplayEndTime.ToString("dd/MM/yyyy HH:mm:ss");
                    txtDisplayOrder.Text = m_Articles.DisplayOrder.ToString();
                    txtSEOTitle.Text = m_Articles.MetaTitle;
                    if(CategoryId==553)
                    {
                        txtNewsTitle.Text = m_Articles.ArticleCode;
                    }
                    txtSEODesc.Text = m_Articles.MetaDesc;
                    txtSEOKeyword.Text = m_Articles.MetaKeyword;
                    txtArticleCode.Text = m_Articles.ArticleCode;
                    txtOriginalPrice.Text = m_Articles.OriginalPrice.ToString();
                    txtSalePrice.Text = m_Articles.SalePrice.ToString();
                    txtContactPrice.Text = m_Articles.ContactPrice;
                    if (!string.IsNullOrEmpty(m_Articles.ImagePath))
                        txtIcon.Text = m_Articles.ImagePath.StartsWith("http") ? m_Articles.ImagePath : CmsConstants.WEBSITE_MEDIADOMAIN + m_Articles.ImagePath;
                    else
                        txtIcon.Text = "";

                    if (txtIcon.Text != "") imgDemo.Src = txtIcon.Text;

                    if (DataTypeId == 12)
                    {
                        if (!string.IsNullOrEmpty(m_Articles.SourceUrl))
                            txtIcon1.Text = m_Articles.SourceUrl.StartsWith("http") ? m_Articles.SourceUrl : CmsConstants.WEBSITE_MEDIADOMAIN + m_Articles.SourceUrl;
                        else
                            txtIcon1.Text = "";
                        if (txtIcon1.Text != "") imgDemo1.Src = txtIcon1.Text;
                    }
                    ArticleCategories m_ArticleCategories = new ArticleCategories();
                    List<ArticleCategories> l_ArticleCategories = m_ArticleCategories.GetListByArticleId(m_Articles.ArticleId);
                    CheckBoxListHelpers.SetSelected(chkCategory, m_Articles.CategoryId.ToString(), "Yellow");
                    for (int i = 0; i < l_ArticleCategories.Count; i++)
                    {
                        CheckBoxListHelpers.SetSelected(chkCategory, l_ArticleCategories[i].CategoryId.ToString(), "Yellow");
                    }

                    //Bind ArticleTags
                    int RowAmount = 0;
                    int PageIndex = 0;
                    string OrderBy = "";
                    int TagId = 0;
                    string TagName = "";
                    byte ReviewStatusId = 0;
                    string DateFrom = "";
                    string DateTo = "";
                    int RowCount = 0;
                    string m_TagString = "";
                    Tags m_Tags = new Tags();
                    List<Tags> l_Tags = m_Tags.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, m_Articles.LanguageId, TagId, m_Articles.ArticleId, TagName, ReviewStatusId, DateFrom, DateTo, ref RowCount);
                    for (int i = 0; i < l_Tags.Count; i++)
                    {
                        if (i > 0) m_TagString = m_TagString + ", ";
                        m_TagString = m_TagString + l_Tags[i].TagName;
                    }
                    txtTag.Text = m_TagString;

                    BindFeatures(m_Articles.DataTypeId, m_Articles.CategoryId, m_Articles.ArticleId);
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

    private void BindFeatures(byte DataTypeId, short CategoryId, int ArticleId)
    {
        string PaddingChar = "&nbsp;&nbsp;&nbsp;";
        ArticleFeatures m_ArticleFeatures = new ArticleFeatures();
        FeaturesView m_FeaturesView = new FeaturesView();
        List<FeaturesView> l_FeaturesView = m_ArticleFeatures.GetFeatureDataForUpdate(DataTypeId, CategoryId, ArticleId, PaddingChar);
        InputTypes m_InputTypes = new InputTypes();
        List<InputTypes> l_InputTypes = InputTypes.Static_GetList();
        mGridFeature.DataSource = l_FeaturesView;
        mGridFeature.DataBind();
        foreach (GridViewRow m_Row in mGridFeature.Rows)
        {
            TextBox txtFeatureValue = (TextBox)m_Row.FindControl("txtFeatureValue");
            DropDownList ddlFeatureValue = (DropDownList)m_Row.FindControl("ddlFeatureValue");
            CheckBoxList cblFeatureValue = (CheckBoxList)m_Row.FindControl("cblFeatureValue");
            RadioButtonList rblFeatureValue = (RadioButtonList)m_Row.FindControl("rblFeatureValue");

            m_FeaturesView = FeaturesView.Static_Get(short.Parse(mGridFeature.DataKeys[m_Row.RowIndex].Value.ToString()), l_FeaturesView);
            m_InputTypes = InputTypes.Static_Get(m_FeaturesView.InputTypeId, l_InputTypes);
            if (m_InputTypes.InputTypeName == "TextBox")
            {
                txtFeatureValue.Visible = true;
                txtFeatureValue.Text = m_FeaturesView.FeatureValue;
            }
            else if (m_InputTypes.InputTypeName == "DropDownList")
            {
                if (m_FeaturesView.DataDictionaryTypeId > 0)
                {
                    ddlFeatureValue.Visible = true;
                    List<DataDictionaries> l_DataDictionaries = DataDictionaries.Static_GetList(m_FeaturesView.DataDictionaryTypeId);
                    DropDownListHelpers.DDL_Bind(ddlFeatureValue, l_DataDictionaries, "...", m_FeaturesView.FeatureValue);
                }
                else
                {
                    txtFeatureValue.Visible = true;
                    txtFeatureValue.Text = m_FeaturesView.FeatureValue;
                }
            }
            else if (m_InputTypes.InputTypeName == "CheckBoxList")
            {
                if (m_FeaturesView.DataDictionaryTypeId > 0)
                {
                    cblFeatureValue.Visible = true;
                    List<DataDictionaries> l_DataDictionaries = DataDictionaries.Static_GetList(m_FeaturesView.DataDictionaryTypeId);
                    CheckBoxListHelpers.Bind(cblFeatureValue, l_DataDictionaries, "");
                    if (!string.IsNullOrEmpty(m_FeaturesView.FeatureValue))
                    {
                        string[] arrFeatureValue = m_FeaturesView.FeatureValue.Split(';');
                        for (int j = 0; j < arrFeatureValue.Length; j++)
                        {
                            for (int i = 0; i < cblFeatureValue.Items.Count; i++)
                            {
                                if (cblFeatureValue.Items[i].Value == arrFeatureValue[j]) cblFeatureValue.Items[i].Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    txtFeatureValue.Visible = true;
                    txtFeatureValue.Text = m_FeaturesView.FeatureValue;
                }
            }
            else if (m_InputTypes.InputTypeName == "RadioButtonList")
            {
                if (m_FeaturesView.DataDictionaryTypeId > 0)
                {
                    rblFeatureValue.Visible = true;
                    List<DataDictionaries> l_DataDictionaries = DataDictionaries.Static_GetList(m_FeaturesView.DataDictionaryTypeId);
                    RadioButtonListHelpers.Bind(rblFeatureValue, l_DataDictionaries, "");
                    RadioButtonListHelpers.SetSelected(rblFeatureValue, m_FeaturesView.FeatureValue);
                }
                else
                {
                    txtFeatureValue.Visible = true;
                    txtFeatureValue.Text = m_FeaturesView.FeatureValue;
                }
            }
        }
    }

    private void ShowHideInput()
    {
        trArticleUrl.Visible = true;
        trCategory.Visible = true;
        trLanguage.Visible = true;
        trProduct.Visible = false;
        trSource.Visible = true;
        trSummary.Visible = true;
        trTag.Visible = true;
        trTime.Visible = true;
        trSourceUrl.Visible = true;
        divCategory.Visible = true;
        lblCategoryOther.Visible = true;
        lblTitle.Text = "Tiêu đề";
        if (",11,12,13,".IndexOf("," + DataTypeId.ToString() + ",") >= 0) //Nhac sy, Ca sy, Album
        {
            //trArticleUrl.Visible = false;
            //trCategory.Visible = false;
            trLanguage.Visible = false;
            trProduct.Visible = false;
            trSource.Visible = false;
            //trSummary.Visible = false;
            trTag.Visible = false;
            trTime.Visible = false;
            trSourceUrl.Visible = false;
            trArticleUrl.Visible = false;
            //divCategory.Visible = false;
            //lblCategoryOther.Visible = false;
            lblTitle.Text = "Tên";
        }
        if (",7,".IndexOf("," + DataTypeId.ToString() + ",") >= 0) //Nhac sy, Ca sy, Album
        {
            trProduct.Visible = true;
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
        ListItem m_CurentParent = new ListItem();
        for (int i = 0; i < cbl.Items.Count; i++)
        {
            //if (!cbl.Items[i].Text.StartsWith("&nbsp"))
            //{
            //    cbl.Items[i].Attributes.Add("style", "font-weight:bold;");
            //}

            if (!cbl.Items[i].Text.StartsWith("-"))
            {
                cbl.Items[i].Attributes.Add("style", "font-weight:bold;");
                m_CurentParent = cbl.Items[i];
            }
            else
            {
                cbl.Items[i].Attributes.Add("onclick", "checkItem('" + m_CurentParent.Value + "')");
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
    protected int SaveData()
    {
        int Retval = ArticleId;
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
            m_Articles.InventoryStatusId = byte.Parse(ddlInventoryStatus.SelectedValue);
            m_Articles.CurrencyId = byte.Parse(ddlCurrency.SelectedValue);
            m_Articles.Title = txtTitle.Text;
            m_Articles.SourceUrl = txtUrl.Text;
            m_Articles.Summary = txtSummaryPlain.Text;
            m_Articles.ArticleUrl = txtArticleUrl.Text;
            m_Articles.DisplayStartTime = txtDisplayStartTime.Text != "" ? DateTime.Parse(txtDisplayStartTime.Text) : DateTime.MinValue;
            m_Articles.DisplayEndTime = txtDisplayEndTime.Text != "" ? DateTime.Parse(txtDisplayEndTime.Text) : DateTime.MinValue;
            m_Articles.DisplayOrder = txtDisplayOrder.Text != "" ? int.Parse(txtDisplayOrder.Text) : 100;
            m_Articles.ArticleCode = txtArticleCode.Text;
            m_Articles.OriginalPrice = txtOriginalPrice.Text != "" ? int.Parse(txtOriginalPrice.Text) : 0;
            m_Articles.SalePrice = txtSalePrice.Text != "" ? int.Parse(txtSalePrice.Text) : 0;
            m_Articles.ContactPrice = txtContactPrice.Text;
            if (m_Articles.CategoryId == 553)
            {
                m_Articles.ArticleCode = txtNewsTitle.Text;
            }
            m_Articles.MetaTitle = txtSEOTitle.Text.Replace("\"","");
            m_Articles.MetaDesc = txtSEODesc.Text.Replace("\"", "");
            m_Articles.MetaKeyword = txtSEOKeyword.Text;
            m_Articles.IsVerify = chkIsVerify.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowApp = chkShowApp.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowBottom = chkShowBottom.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowTop = chkShowTop.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowWap = chkShowWap.Checked == true ? (byte)1 : (byte)0;
            m_Articles.ShowWeb = chkShowWeb.Checked == true ? (byte)1 : (byte)0;
            //m_Articles.ArticleContent = txtContent.Text;
            if (cbInsertAutoInternalLinks.Checked == true)
            {
                m_Articles.ArticleContent = ICSoft.HelperLib.InternalLinkHelper.getContentInternalLinks(Convert.ToInt16(ddlSite.SelectedValue), txtContent.Text);
                txtContent.Text = m_Articles.ArticleContent;
            }
            else
            {
                m_Articles.ArticleContent = txtContent.Text;
            }
            if (txtIcon.Text.StartsWith(CmsConstants.WEBSITE_MEDIADOMAIN))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.WEBSITE_MEDIADOMAIN.Length);
            if (DataTypeId == 12)
            {
                if (txtIcon1.Text.StartsWith(CmsConstants.WEBSITE_MEDIADOMAIN))
                    txtIcon1.Text = txtIcon1.Text.Substring(CmsConstants.WEBSITE_MEDIADOMAIN.Length);
                if (cbDeleteImages.Checked) txtIcon1.Text = "";
                m_Articles.SourceUrl = txtIcon1.Text;
            }
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_Articles.ImagePath = txtIcon.Text;
            m_Articles.ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            m_Articles.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            Retval = m_Articles.ArticleId;
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
                //Insert main category and parent
                m_ArticleCategories.CategoryId = m_Articles.CategoryId;
                m_ArticleCategories.InsertMainCate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

                //Insert Media
                if (txtIcon.Text != "")
                {
                    ArticleMedias m_ArticleMedias = new ArticleMedias();
                    m_ArticleMedias.ArticleId = m_Articles.ArticleId;
                    m_ArticleMedias.FilePath = txtIcon.Text;
                    m_ArticleMedias.MediaId = 0;
                    m_ArticleMedias.MediaTypeId = 0;
                    m_ArticleMedias.ArticleMediaId = 0;
                    m_ArticleMedias.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                }

                ArticleFields m_ArticleFields = new ArticleFields();
                if (byte.Parse(ddlLanguage.SelectedValue) == 1)
                {
                    m_ArticleFields.ArticleId = m_Articles.ArticleId;
                    m_ArticleFields.DeleteByArticleId(ConstantHelpers.Replicated, ActUserId);
                    m_ArticleFields.InsertByFieldName(ConstantHelpers.Replicated, ActUserId, txtFields.Text.Trim());
                }
                //Insert Tag
                ArticleTags m_ArticleTags = new ArticleTags();
                m_ArticleTags.ArticleId = m_Articles.ArticleId;
                m_ArticleTags.DeleteByArticleId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                if (!string.IsNullOrEmpty(txtTag.Text))
                {
                    m_ArticleTags.InsertByString(ConstantHelpers.Replicated, ActUserId, txtTag.Text.Replace(";", ","));
                }

                //Insert feature data
                ArticleFeatures m_ArticleFeatures = new ArticleFeatures();
                m_ArticleFeatures.ArticleId = m_Articles.ArticleId;
                m_ArticleFeatures.DeleteByArticleId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

                FeaturesView m_FeaturesView = new FeaturesView();
                List<FeaturesView> l_FeaturesView = m_ArticleFeatures.GetFeatureDataForUpdate(m_Articles.DataTypeId, m_Articles.CategoryId, m_Articles.ArticleId, "");
                InputTypes m_InputTypes = new InputTypes();
                List<InputTypes> l_InputTypes = InputTypes.Static_GetList();
                foreach (GridViewRow m_Row in mGridFeature.Rows)
                {
                    TextBox txtFeatureValue = (TextBox)m_Row.FindControl("txtFeatureValue");
                    DropDownList ddlFeatureValue = (DropDownList)m_Row.FindControl("ddlFeatureValue");
                    CheckBoxList cblFeatureValue = (CheckBoxList)m_Row.FindControl("cblFeatureValue");
                    RadioButtonList rblFeatureValue = (RadioButtonList)m_Row.FindControl("rblFeatureValue");

                    m_FeaturesView = FeaturesView.Static_Get(short.Parse(mGridFeature.DataKeys[m_Row.RowIndex].Value.ToString()), l_FeaturesView);
                    m_InputTypes = InputTypes.Static_Get(m_FeaturesView.InputTypeId, l_InputTypes);

                    m_ArticleFeatures.FeatureId = m_FeaturesView.FeatureId;
                    m_ArticleFeatures.FeatureValue = txtFeatureValue.Text.Trim();

                    if (m_InputTypes.InputTypeName == "DropDownList")
                    {
                        if (ddlFeatureValue.SelectedValue != "0") m_ArticleFeatures.FeatureValue = ddlFeatureValue.SelectedValue;
                    }
                    else if (m_InputTypes.InputTypeName == "CheckBoxList")
                    {
                        string mValue = "";
                        for (int i = 0; i < cblFeatureValue.Items.Count; i++)
                        {
                            if (cblFeatureValue.Items[i].Selected)
                            {
                                if (i > 0) mValue = mValue + ";";
                                mValue = mValue + cblFeatureValue.Items[i].Value;
                            }
                        }
                        m_ArticleFeatures.FeatureValue = mValue;
                    }
                    else if (m_InputTypes.InputTypeName == "RadioButtonList")
                    {
                        if (!string.IsNullOrEmpty(rblFeatureValue.SelectedValue)) m_ArticleFeatures.FeatureValue = rblFeatureValue.SelectedValue;
                    }

                    if (!string.IsNullOrEmpty(m_ArticleFeatures.FeatureValue))
                    {
                        m_ArticleFeatures.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }


                }
                //ArticleLocation _ArticleLocation = new ArticleLocation();
                if (DataTypeId == 7 || DataTypeId == 1)
                {
                    //ArticleLocation m_ArticleLocation = new ArticleLocation();
                    //m_ArticleLocation = m_ArticleLocation.ArticleLocation_GetByArticleID(ArticleId);
                    //_ArticleLocation.ProvinceId = byte.Parse(ddlProvince.SelectedValue);
                    //_ArticleLocation.DistrictId = byte.Parse(ddlDistrict.SelectedValue);
                    //_ArticleLocation.WardId = int.Parse(ddlWard.SelectedValue);
                    //_ArticleLocation.Address = TextBoxAddress.Text;
                    //_ArticleLocation.Longitude = double.Parse(TextBoxLongitude.Value.Replace(" ", "").Replace(".", ","));
                    //_ArticleLocation.Latitude = double.Parse(TextBoxLatitude.Value.Replace(" ", "").Replace(".", ","));
                    //_ArticleLocation.ArticleId = m_Articles.ArticleId;
                    //_ArticleLocation.ArticleLocationId = m_ArticleLocation.ArticleLocationId;
                    //_ArticleLocation.ArticleLocation_InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                   
                    ArticleLocation m_ArticleLocation1 = new ArticleLocation();
                    m_ArticleLocation1.ArticleId = ArticleId;
                    m_ArticleLocation1.ArticleLocationId = ArticleId;
                    m_ArticleLocation1.ArticleLocation_Delete();
                    string[] ProvinceDesc = txtProvices.Text.Trim().Split(';');

                    List<Provinces> lProvinces = Provinces.Static_GetList(1);

                    for (int i = 0; i < ProvinceDesc.Length; i++)
                    {
                        foreach (Provinces mProvince in lProvinces)
                        {
                            string provice_ = ProvinceDesc[i];
                            if (!string.IsNullOrEmpty(provice_))
                            {
                                provice_ =  provice_.Trim();
                            }
                            if (string.IsNullOrEmpty(provice_))
                            {
                                break;
                            }
                            if (mProvince.ProvinceDesc.Contains(provice_))
                            {
                                ArticleLocation m_ArticleLocation = new ArticleLocation();
                                m_ArticleLocation.ArticleLocationId = 0;
                                m_ArticleLocation.ProvinceId = mProvince.ProvinceId;
                                m_ArticleLocation.DistrictId = byte.Parse(ddlDistrict.SelectedValue);
                                m_ArticleLocation.WardId = int.Parse(ddlWard.SelectedValue);
                                m_ArticleLocation.Address = TextBoxAddress.Text;
                                m_ArticleLocation.Longitude = double.Parse(TextBoxLongitude.Value.Replace(" ", "").Replace(".", ","));
                                m_ArticleLocation.Latitude = double.Parse(TextBoxLatitude.Value.Replace(" ", "").Replace(".", ","));
                                m_ArticleLocation.ArticleId = m_Articles.ArticleId;
                                SysMessageId = m_ArticleLocation.ArticleLocation_Insert();
                                break;
                            }
                        }
                    }

                    
                }
            }
            if (ArticleId == 0)
            {
                JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Thêm mới bài viết thành công.", this);
            }
            else
            {
                JSAlertHelpers.ShowNotifyOtherPage("15", "success", "Cập nhật bài viết thành công.", this);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
        return Retval;
    }
    private void ClearForm()
    {
        txtContent.Text = "";
        txtSummary.Text = "";
        txtTitle.Text = "";
        txtUrl.Text = "";
        txtArticleUrl.Text = "";
        txtDisplayStartTime.Text = "";
        txtDisplayEndTime.Text = "";
        txtDisplayOrder.Text = "";
        txtOriginalPrice.Text = "";
        txtArticleCode.Text = "";
        txtSalePrice.Text = "";
        txtContactPrice.Text = "";
        txtSEOTitle.Text = "";
        txtNewsTitle.Text = "";
        txtSEODesc.Text = "";
        txtSEOKeyword.Text = "";
        txtTag.Text = "";
        chkIsVerify.Checked = false;
        chkShowApp.Checked = false;
        chkShowBottom.Checked = false;
        chkShowTop.Checked = false;
        chkShowWap.Checked = false;
        chkShowWeb.Checked = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ArticleId = SaveData();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (ArticleId > 0)
        {
            Response.Redirect("ArticlesEdit.aspx?ArticleId=" + ArticleId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&DataTypeId=" + DataTypeId.ToString());
        }
        else
        {
            BindData();
        }
    }
    protected void btnSaveAndNew_Click(object sender, EventArgs e)
    {
        SaveData();
        clearForm();
        Response.Redirect("ArticlesEdit.aspx?SiteId=" + SiteId.ToString() + "&DataTypeId=" + DataTypeId.ToString() + "&EnableDataType=0&ActionId=" + ActionId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&AppTypeId=" + ApplicationTypeId.ToString() + "&CategoryId=" + CategoryId.ToString());

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //if (CategoryId == 0)
        //    CategoryId = short.Parse(ddlCategory.SelectedValue);
        Response.Redirect("Articles.aspx?SiteId=" + SiteId.ToString() + "&DataTypeId=" + DataTypeId.ToString() + "&EnableDataType=" + EnableDataType.ToString() + "&ActionId=" + ActionId.ToString() + "&LanguageId=" + LanguageId.ToString() + "&AppTypeId=" + ApplicationTypeId.ToString() + "&CategoryId=" + CategoryId.ToString());
    }
    private void clearForm()
    {
        txtArticleCode.Text = "";
        txtArticleUrl.Text = "";
        txtContactPrice.Text = "";
        txtContent.Text = "";
        txtDisplayEndTime.Text = "";
        txtDisplayOrder.Text = "";
        txtDisplayStartTime.Text = "";
        txtIcon.Text = "";
        txtIcon1.Text = "";
        txtOriginalPrice.Text = "";
        txtSalePrice.Text = "";
        txtSEODesc.Text = "";
        txtSEOKeyword.Text = "";
        txtSEOTitle.Text = "";
        txtNewsTitle.Text = "";
        txtSummary.Text = "";
        txtSummaryPlain.Text = "";
        txtTag.Text = "";
        txtTitle.Text = "";
        txtUrl.Text = "";
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        HightLight(ddlCategory);
        HightLight(chkCategory);
        BindFeatures(byte.Parse(ddlDataType.SelectedValue), short.Parse(ddlCategory.SelectedValue), ArticleId);
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
        ShowHideInput();
        BindFeatures(byte.Parse(ddlDataType.SelectedValue), short.Parse(ddlCategory.SelectedValue), ArticleId);
    }

    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Districts> l_district = Districts.Static_GetList(byte.Parse(ddlProvince.SelectedValue));
        DropDownListHelpers.DDL_Bind(ddlDistrict, l_district, "...", "0");
        HightLight(ddlDistrict);
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Wards> l_Wards = Wards.Static_GetList(byte.Parse(ddlDistrict.SelectedValue));
        DropDownListHelpers.DDL_Bind(ddlWard, l_Wards, "...", "0");
        HightLight(ddlWard);
    }
}