using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_Pages_articles_FeaturesEdit : System.Web.UI.Page
{
    private int ActUserId = 0;
    private short FeatureID = 0;
    private short FeatureGroupId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (Request.Params["FeatureId"] != null && Request.Params["FeatureId"].ToString() != "")
            {
                FeatureID = short.Parse(Request.Params["FeatureId"].ToString());
            }
            if (Request.Params["FeatureGroupId"] != null && Request.Params["FeatureGroupId"].ToString() != "")
            {
                FeatureGroupId = short.Parse(Request.Params["FeatureGroupId"].ToString());
            }
            if (!IsPostBack)
            {
                DropDownListHelpers.DDL_Bind(ddlDataDicType, DataDictionaryTypes.Static_GetList(), " ... ", "0");
                DropDownListHelpers.DDL_Bind(ddlFeatureGroup, FeatureGroups.Static_GetList(), "", FeatureGroupId.ToString());
                DropDownListHelpers.DDL_Bind(ddlInputType, InputTypes.Static_GetList(), " ... ", "0");

                int rowCount = 0;
                Features m_Features = new Features();
                m_Features.FeatureGroupId = short.Parse(ddlFeatureGroup.SelectedValue);
                List<Features> l_ParentCategory = m_Features.GetPage(ActUserId, 100, 0, "", "", "", 0, 0, "--", ref rowCount);
                DropDownListHelpers.DDL_Bind(ddlParentFeature, l_ParentCategory, "...", "0");
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
            Features m_Features = new Features();
            m_Features = m_Features.Get(FeatureID);
            if (m_Features.FeatureId > 0)
            {
                txtName.Text = m_Features.FeatureName;
                txtDesc.Text = m_Features.FeatureDesc;
                if (!string.IsNullOrEmpty(m_Features.IconPath))
                    txtIcon.Text = CmsConstants.ROOT_PATH + m_Features.IconPath;
                else
                    txtIcon.Text = "";
                txtDisplayOrder.Text = m_Features.DisplayOrder.ToString();
                DropDownListHelpers.DDL_SetSelected(ddlFeatureGroup, m_Features.FeatureGroupId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlDataDicType, m_Features.DataDictionaryTypeId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlInputType, m_Features.InputTypeId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlParentFeature, m_Features.ParentFeatureId.ToString());  
                ckbIsDisplay.Checked = m_Features.IsDisplay == 1 ? true : false;
                ckbIsData.Checked = m_Features.IsData == 1 ? true : false;
                ckbIsSearch.Checked = m_Features.IsSearch == 1 ? true : false;
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
            short SysMessageTypeId = 0;
            short SysMessageId = 0;
            Features m_Features = new Features();
            m_Features.FeatureName = txtName.Text;
            m_Features.FeatureDesc = txtDesc.Text;
            m_Features.DisplayOrder = txtDisplayOrder.Text.Trim() == "" ? (byte)10 : Convert.ToByte(txtDisplayOrder.Text);
            m_Features.InputTypeId = byte.Parse(ddlInputType.SelectedValue);
            m_Features.DataDictionaryTypeId = Int16.Parse(ddlDataDicType.SelectedValue);
            m_Features.FeatureGroupId = Int16.Parse(ddlFeatureGroup.SelectedValue);
            m_Features.ParentFeatureId = Int16.Parse(ddlParentFeature.SelectedValue);
            m_Features.IsDisplay = byte.Parse(ckbIsDisplay.Checked == true ? "1" : "0");
            m_Features.IsData = byte.Parse(ckbIsData.Checked == true ? "1" : "0");
            m_Features.IsSearch = byte.Parse(ckbIsSearch.Checked == true ? "1" : "0");


            if (txtIcon.Text.StartsWith(CmsConstants.ROOT_PATH))
                txtIcon.Text = txtIcon.Text.Substring(CmsConstants.ROOT_PATH.Length);
            if (cbDeleteImages.Checked)
                txtIcon.Text = "";
            m_Features.IconPath = txtIcon.Text;

            if (FeatureID > 0)
            {
                m_Features.FeatureId = FeatureID;
                SysMessageTypeId = m_Features.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_Features.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    
    protected void ddlFeatureGroup_SelectedIndexChanged1(object sender, EventArgs e)
    {
        int rowCount = 0;
        Features m_Features = new Features();
        m_Features.FeatureGroupId = short.Parse(ddlFeatureGroup.SelectedValue);
        List<Features> l_ParentCategory = m_Features.GetPage(ActUserId, 100, 0, "", "", "", 0, 0, "--", ref rowCount);
        DropDownListHelpers.DDL_Bind(ddlParentFeature, l_ParentCategory, "...", "0");
    }
}