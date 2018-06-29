using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_AdvertPositionAdverts : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SiteId = 0;
    private int AdvertPositionId = 0;
    protected short CategoryId = 0;
    protected List<AdvertStatus> l_AdvertStatus = new List<AdvertStatus>();
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
            lblPositionName.Text = AdvertPositions.Static_Get(AdvertPositionId).PositionDesc.ToString();
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "", SiteId.ToString());
            DropDownListHelpers.DDL_Bind(ddlDataType, DataTypes.Static_GetList(), "");
            List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "",short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 1, 1, 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_AdvertStatus = AdvertStatus.Static_GetList();

            int RowAmount=200;
            int PageIndex=0;
            string OrderBy="";
            short SiteId=short.Parse(ddlSite.SelectedValue);
            CategoryId=short.Parse(ddlCategory.SelectedValue);
            byte ApplicationTypeId=0;
            string PositionName="";
            byte AdvertDisplayTypeId=0;
            int RowCount=0;
            //Adverts m_Adverts = new Adverts();
            //m_grid.DataSource = m_Adverts.GetListByAdvertPositionId(AdvertPositionId, short.Parse(ddlCategory.SelectedValue));
            AdvertPositionAdverts m_Adverts = new AdvertPositionAdverts();
            m_grid.DataSource = m_Adverts.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SiteId, CategoryId, AdvertPositionId, ApplicationTypeId, PositionName, AdvertDisplayTypeId, ref RowCount);
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();
            btnAdd.NavigateUrl = "AdvertPositionAdvertsAdd.aspx?SiteId=" + SiteId.ToString() + "&CategoryId=" + CategoryId.ToString() + "&AdvertPositionId=" + AdvertPositionId.ToString();
            btnAdd.ToolTip = "Chọn quảng cáo";
            btnAdd.Enabled = true;
            btnSavePosition.ToolTip = "";
            btnSavePosition.Enabled = true;
            if (CategoryId == 0)
            {
                //btnAdd.ToolTip = "Chọn chuyên mục trước khi chọn quảng cáo";
                //btnSavePosition.ToolTip = "Chọn chuyên mục trước khi sắp xếp vị trí";
                //btnSavePosition.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            Label lblCategoryId = (Label)m_grid.Rows[e.RowIndex].FindControl("lblCategoryId");
            Label lblAdvertName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblAdvertName");
            Label lblPositionName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblPositionName");
            AdvertPositionAdverts m_AdvertPositionAdverts = new AdvertPositionAdverts();
            m_AdvertPositionAdverts.AdvertId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_AdvertPositionAdverts.AdvertPositionId = AdvertPositionId;
            m_AdvertPositionAdverts.CategoryId = short.Parse(lblCategoryId.Text);
            m_AdvertPositionAdverts.SiteId = short.Parse(ddlSite.SelectedValue);
            SysMessageTypeId = m_AdvertPositionAdverts.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa : {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2)
            {
                deleteMessage = string.Format("Xóa quảng cáo <i>{0}</i> khỏi vị trí <i>{1}</i> thành công.", !string.IsNullOrEmpty(lblAdvertName.Text) ? lblAdvertName.Text : "", !string.IsNullOrEmpty(lblPositionName.Text) ? lblPositionName.Text : "");
            }
            ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: 'success', message: '" + deleteMessage + "' });", true);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnSavePosition_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            AdvertPositionAdverts m_AdvertPositionAdverts = new AdvertPositionAdverts();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                TextBox txtDisplayOrder = (TextBox)m_Row.FindControl("txtDisplayOrder");
                Label lblCategoryId = (Label)m_Row.FindControl("lblCategoryId");
                if (txtDisplayOrder != null)
                {
                    AdvertPositionAdverts mItem = (AdvertPositionAdverts)m_Row.DataItem;
                    m_AdvertPositionAdverts.AdvertId = short.Parse(m_grid.DataKeys[m_Row.DataItemIndex].Value.ToString());
                    m_AdvertPositionAdverts.AdvertPositionId = AdvertPositionId;
                    m_AdvertPositionAdverts.CategoryId = short.Parse(lblCategoryId.Text);//short.Parse(ddlCategory.SelectedValue);
                    m_AdvertPositionAdverts.SiteId = short.Parse(ddlSite.SelectedValue);//short.Parse(ddlCategory.SelectedValue);
                    m_AdvertPositionAdverts.DisplayOrder = int.Parse(txtDisplayOrder.Text);
                    m_AdvertPositionAdverts.Update(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 1, 1, 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...");
        BindData();
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "", short.Parse(ddlSite.SelectedValue), byte.Parse(ddlDataType.SelectedValue), 1, 1, 0, 0, "--");
        DropDownListHelpers.DDL_Bind(ddlCategory, l_ParentCategory, "...");
        BindData();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}