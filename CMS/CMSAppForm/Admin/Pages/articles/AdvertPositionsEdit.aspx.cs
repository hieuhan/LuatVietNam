using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_AdvertPositionsEdit : System.Web.UI.Page
{
    private int AdvertPositionId = 0;
    private short SiteId = 0;
    private int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["AdvertPositionId"] != null && Request.Params["AdvertPositionId"].ToString() != "")
        {
            AdvertPositionId = int.Parse(Request.Params["AdvertPositionId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlAppType, ApplicationTypes.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlType, AdvertDisplayTypes.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            //List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, 0, 0, 0, 0, "--");
            //DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (AdvertPositionId > 0)
            {
                AdvertPositions m_AdvertPositions = new AdvertPositions();
                m_AdvertPositions = m_AdvertPositions.Get(AdvertPositionId);
                if (m_AdvertPositions.AdvertPositionId > 0)
                {
                    txtDesc.Text = m_AdvertPositions.PositionDesc;
                    txtHeight.Text = m_AdvertPositions.Height;
                    txtName.Text = m_AdvertPositions.PositionName;
                    txtOverHeight.Text = m_AdvertPositions.OverflowHeight;
                    txtOverWidth.Text = m_AdvertPositions.OverflowWidth;
                    txtWidth.Text = m_AdvertPositions.Width;
                    DropDownListHelpers.DDL_SetSelected(ddlType, m_AdvertPositions.AdvertDisplayTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlAppType, m_AdvertPositions.ApplicationTypeId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlSite, m_AdvertPositions.SiteId.ToString());
                    //List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, 0, 0, 0, 0, "--");
                    //DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...", m_AdvertPositions.CategoryId.ToString());
                }
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
            AdvertPositions m_AdvertPositions = new AdvertPositions();
            m_AdvertPositions.AdvertDisplayTypeId = byte.Parse(ddlType.SelectedValue);
            m_AdvertPositions.AdvertPositionId = AdvertPositionId;
            m_AdvertPositions.ApplicationTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_AdvertPositions.Height = txtHeight.Text;
            m_AdvertPositions.OverflowHeight = txtOverHeight.Text;
            m_AdvertPositions.OverflowWidth = txtOverWidth.Text;
            m_AdvertPositions.PositionDesc = txtDesc.Text;
            m_AdvertPositions.PositionName = txtName.Text;
            m_AdvertPositions.Width = txtWidth.Text;
            m_AdvertPositions.SiteId = byte.Parse(ddlSite.SelectedValue);
            //m_AdvertPositions.CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            if (AdvertPositionId <= 0)
            {
                SysMessageTypeId = m_AdvertPositions.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            else
            {
                SysMessageTypeId = m_AdvertPositions.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
            
            if (SysMessageTypeId == 1)
            {
                JSAlertHelpers.ShowNotify("15", "error", SysMessages.Static_GetDesc(SysMessageId), this);
            }
            else if (m_AdvertPositions.AdvertPositionId > 0 )
            {
                if (cblAddOther.Checked)
                {
                    JSAlertHelpers.ShowNotify("15", "success",
                        AdvertPositionId > 0 ? "Cập nhật Vị trí quảng cáo thành công." : "Thêm mới Vị trí quảng cáo thành công.", this);
                    ClearForm();
                    return;
                }
                JSAlertHelpers.ShowNotifyOtherPage("15", "success",
                    AdvertPositionId > 0 ? "Cập nhật Vị trí quảng cáo thành công." : "Thêm mới Vị trí quảng cáo thành công.", this);
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
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), 0, 0, 0, 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
    }

    private void ClearForm()
    {
        txtName.Text = "";
        txtDesc.Text = "";
        txtHeight.Text = "";
        txtWidth.Text = "";
        txtOverHeight.Text = "";
        txtOverWidth.Text = "";
        ddlSite.SelectedIndex = -1;
        ddlAppType.SelectedIndex = -1;
        ddlParentCategory.SelectedIndex = -1;
        ddlType.SelectedIndex = -1;
        cblAddOther.Checked = false;
    }
}