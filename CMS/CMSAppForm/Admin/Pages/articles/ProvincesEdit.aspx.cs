using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.HelperLib;
using sms.common;
using Countries = ICSoft.CMSLib.Countries;
using Provinces = ICSoft.CMSLib.Provinces;

public partial class Admin_Pages_articles_ProvincesEdit : System.Web.UI.Page
{
    private int ActUserId = 0;
    private short CountryId;
    private short ProvinceId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
            if (!IsPostBack)
            {
                DropDownListHelpers.DDL_Bind(ddlCountries, Countries.Static_GetList(), "", CountryId.ToString());
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
            Provinces m_Provinces = new Provinces();
            m_Provinces = m_Provinces.Get(ProvinceId);
            if (m_Provinces.ProvinceId > 0)
            {
                txtProvinceName.Text = m_Provinces.ProvinceName;
                txtProvinceDesc.Text = m_Provinces.ProvinceDesc;
                txtDisplayOrder.Text = m_Provinces.DisplayOrder.ToString();
                DropDownListHelpers.DDL_SetSelected(ddlCountries, m_Provinces.CountryId.ToString());
                cblAddOther.Visible = false;
            }
            else cblAddOther.Visible = true;
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
            Provinces m_Provinces = new Provinces();
            m_Provinces.ProvinceName = txtProvinceName.Text;
            m_Provinces.ProvinceDesc = txtProvinceDesc.Text;
            m_Provinces.DisplayOrder = txtDisplayOrder.Text.Trim() == "" ? (short)10 : Int16.Parse(txtDisplayOrder.Text.Trim());
            m_Provinces.CountryId = Int16.Parse(ddlCountries.SelectedValue);
            if (ProvinceId > 0)
            {
                m_Provinces.ProvinceId = ProvinceId;
                SysMessageTypeId = m_Provinces.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_Provinces.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 0)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        ProvinceId > 0 ? "Cập nhật Thành phố thành công." : "Thêm mới Thành phố thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    ProvinceId > 0 ? "Cập nhật Thành phố thành công." : "Thêm mới Thành phố thành công.", this);
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

    private void ClearForm()
    {
        txtProvinceName.Text = "";
        txtProvinceDesc.Text = "";
        txtDisplayOrder.Text = "";
        ddlCountries.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
}