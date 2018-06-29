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

public partial class Admin_DisplayTypesEdit : System.Web.UI.Page
{
    private short DisplayTypeId = 0;
    protected byte DataTypeId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DisplayTypeId"] != null && Request.Params["DisplayTypeId"].ToString() != "")
        {
            DisplayTypeId = short.Parse(Request.Params["DisplayTypeId"].ToString());
        }
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLDataTypes_BindByCulture(ddlDataTypes, DataTypes.Static_GetList(), "", DataTypeId.ToString());
            if (Request.QueryString["DataTypeId"] != null)
            {
                cblAddOther.Visible = true;
            }
            else
            {
                cblAddOther.Visible = false;
                BindData();
            }
        }
    }
    private void BindData()
    {
        try
        {
            if (DisplayTypeId > 0)
            {
                DisplayTypes m_DisplayTypes = new DisplayTypes();
                m_DisplayTypes = m_DisplayTypes.Get(DisplayTypeId);
                if (m_DisplayTypes.DisplayTypeId > 0)
                {
                    txtDisplayTypeName.Text = m_DisplayTypes.DisplayTypeName;
                    txtDisplayTypeDesc.Text = m_DisplayTypes.DisplayTypeDesc.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlDataTypes, m_DisplayTypes.DataTypeId.ToString());
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
            byte SysMessageTypeId = 0;
            DisplayTypes m_DisplayTypes = new DisplayTypes();
            m_DisplayTypes.DisplayTypeName = txtDisplayTypeName.Text.Trim();
            m_DisplayTypes.DisplayTypeDesc = txtDisplayTypeDesc.Text.Trim();
            m_DisplayTypes.DisplayTypeId = DisplayTypeId;              
            m_DisplayTypes.DataTypeId = byte.Parse(ddlDataTypes.SelectedValue);
            SysMessageTypeId = m_DisplayTypes.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", sms.common.SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (SysMessageTypeId == 0)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        DisplayTypeId > 0 ? "Cập nhật danh sách loại hiển thị thành công." : "Thêm mới loại hiển thị thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    DisplayTypeId > 0 ? "Cập nhật loại hiển thị thành công." : "Thêm mới loại hiển thị thành công.", this);
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
        txtDisplayTypeName.Text = "";
        txtDisplayTypeDesc.Text = "";
        cblAddOther.Checked = false;
    }
}