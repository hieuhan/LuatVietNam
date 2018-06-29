using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_AdvertPositionAdverts1 : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SiteId = 0;
    private int AdvertId = 0;
    protected short CategoryId = 0;
    protected List<AdvertStatus> l_AdvertStatus = new List<AdvertStatus>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["AdvertId"] != null && Request.Params["AdvertId"].ToString() != "")
        {
            AdvertId = int.Parse(Request.Params["AdvertId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (!IsPostBack)
        {
            lblPositionName.Text = Adverts.Static_Get(AdvertId).AdvertDesc;
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
            short SiteId=0;
            CategoryId=0;
            byte ApplicationTypeId=0;
            string PositionName="";
            byte AdvertDisplayTypeId=0;
            int AdvertPositionId = 0;
            int RowCount=0;
            AdvertPositionAdverts m_Adverts = new AdvertPositionAdverts();
            m_Adverts.AdvertId = AdvertId;
            m_grid.DataSource = m_Adverts.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SiteId, CategoryId, AdvertPositionId, ApplicationTypeId, PositionName, AdvertDisplayTypeId, ref RowCount);
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();
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
                lbDelete.OnClientClick = "return confirm('Bạn có muốn xóa dữ liệu này ?');";
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
            Label lblPositionName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblPositionName");
            AdvertPositionAdverts m_AdvertPositionAdverts = new AdvertPositionAdverts();
            m_AdvertPositionAdverts.AdvertPositionAdvertId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_AdvertPositionAdverts.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa : {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2)
            {
                deleteMessage = string.Format("Xóa quảng cáo khỏi vị trí <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblPositionName.Text) ? lblPositionName.Text : "");
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
}