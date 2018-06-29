using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_MenusEdit : System.Web.UI.Page
{
    private short MenuId = 0;
    private short SiteId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["MenuId"] != null && Request.Params["MenuId"].ToString() != "")
        {
            MenuId = short.Parse(Request.Params["MenuId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, 0, 0, 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (MenuId > 0)
            {
                Menus m_Menus = new Menus();
                m_Menus = m_Menus.Get(MenuId);
                txtCategoryName.Text = m_Menus.MenuName;
                txtCategoryDesc.Text = m_Menus.MenuDesc;
                DropDownListHelpers.DDL_SetSelected(ddlSite, m_Menus.SiteId.ToString());
                DropDownListHelpers.DDL_SetSelected(ddlParentCategory, m_Menus.CategoryId.ToString());
                cblAddOther.Visible = false;
            }
            else cblAddOther.Visible = true;
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
            Menus m_Menus = new Menus();
            m_Menus.MenuId = MenuId;
            if (m_Menus.MenuId>0) m_Menus = m_Menus.Get();
            m_Menus.SiteId = short.Parse(ddlSite.SelectedValue);
            m_Menus.CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            m_Menus.MenuDesc = txtCategoryDesc.Text;
            m_Menus.MenuName = txtCategoryName.Text;
            m_Menus.CrUserId = ActUserId;

            SysMessageTypeId = m_Menus.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (m_Menus.MenuId > 0)
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        MenuId > 0 ? "Cập nhật Menu thành công." : "Thêm mới Menu thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    MenuId > 0 ? "Cập nhật Menu thành công." : "Thêm mới Menu thành công.", this);
            }

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
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, 0, 0, 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
    }

    private void ClearForm()
    {
        txtCategoryName.Text = "";
        txtCategoryDesc.Text = "";
        ddlParentCategory.SelectedIndex = -1;
        ddlSite.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
}