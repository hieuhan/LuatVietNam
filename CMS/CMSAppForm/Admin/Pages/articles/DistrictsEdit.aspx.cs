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

public partial class Admin_Pages_articles_DistrictsEdit : System.Web.UI.Page
{
    String csName = "csbMessage";
    Type csType;
    ClientScriptManager cs;
    StringBuilder csText = new StringBuilder();
    private byte LanguageId = 0;
    private byte ApplicationTypeId = 0;
    private short CountryId = 0;
    private short ProvinceId = 0;
    private short DistrictId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["CountryId"] != null && Request.Params["CountryId"].ToString() != "")
        {
            CountryId = short.Parse(Request.Params["CountryId"].ToString());
        }
        if (Request.Params["ProvinceId"] != null && Request.Params["ProvinceId"].ToString() != "")
        {
            ProvinceId = short.Parse(Request.Params["ProvinceId"].ToString());
        }
        if (Request.Params["DistrictId"] != null && Request.Params["DistrictId"].ToString() != "")
        {
            DistrictId = short.Parse(Request.Params["DistrictId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlCountries, Countries.Static_GetList(), "", CountryId.ToString());
            DropDownListHelpers.DDL_Bind(ddlProvinces, Provinces.Static_GetList(short.Parse(ddlCountries.SelectedValue)), "", ProvinceId.ToString());
            BindData();
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------
    private void BindData()
    {
        try
        {
            if (DistrictId > 0)
            {
                Districts m_Districts = new Districts();
                m_Districts = m_Districts.Get(DistrictId);
                if (m_Districts.DistrictId> 0)
                {
                    txtDistrictName.Text = m_Districts.DistrictName.ToString();
                    txtDistrictDesc.Text = m_Districts.DistrictDesc.ToString();
                    txtDisplayOrder.Text = m_Districts.DisplayOrder.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlCountries, m_Districts.CountryId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlProvinces, m_Districts.ProvinceId.ToString());
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
            if (txtDistrictName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên không được để trống";
                return;
            }
            short SysMessageId = 0;
            Districts m_Districts = new Districts();
            m_Districts.DistrictName = txtDistrictName.Text;
            m_Districts.DistrictDesc = txtDistrictDesc.Text == "" ? txtDistrictName.Text : txtDistrictDesc.Text;
            m_Districts.DistrictId = DistrictId;
            m_Districts.CountryId = short.Parse(ddlCountries.SelectedValue);
            m_Districts.ProvinceId = short.Parse(ddlProvinces.SelectedValue);
            m_Districts.DisplayOrder = txtDisplayOrder.Text.Trim() == "" ? (short)10 : short.Parse(txtDisplayOrder.Text);
            m_Districts.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    //-----------------------------------------------------------------------------------------------------------------------

    protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListHelpers.DDL_Bind(ddlProvinces, Provinces.Static_GetList(short.Parse(ddlCountries.SelectedValue)), "", ProvinceId.ToString());
    }
    //-----------------------------------------------------------------------------------------------------------------------
}