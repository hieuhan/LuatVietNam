using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.HelperLib;

public partial class Admin_ArticleFeatures : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private int ArticleId = 0;
    private short SiteId = 0;
    private byte DataTypeId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
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
            if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
            {
                SiteId = short.Parse(Request.Params["SiteId"].ToString());
            }
            if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
            {
                DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
            }
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            Articles m_Articles = new Articles();
            m_Articles.ArticleId = ArticleId;
            m_Articles.LanguageId = LanguageId;
            m_Articles.ApplicationTypeId = ApplicationTypeId;
            m_Articles = m_Articles.Get();
            BindFeatures(m_Articles.DataTypeId, m_Articles.CategoryId, m_Articles.ArticleId);
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
            
            short SysMessageId = 0;
            Articles m_Articles = new Articles();
            m_Articles.ArticleId = ArticleId;
            m_Articles.LanguageId = LanguageId;
            m_Articles.ApplicationTypeId = ApplicationTypeId;
            m_Articles = m_Articles.Get();
            if (ArticleId > 0)
            {
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
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}