using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using System.Data;
using System.IO;
using sms.common;

public partial class Admin_AdvertPositions : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SiteId = 0;
    protected List<ApplicationTypes> l_ApplicationTypes = new List<ApplicationTypes>();
    protected List<Users> l_Users;
    protected List<Sites> l_Sites;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDL_Bind(ddlAppType, ApplicationTypes.Static_GetList(), "...");
            //List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "",short.Parse(ddlSite.SelectedValue), 0, 0, byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            //DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            AdvertPositions m_AdvertPositions = new AdvertPositions();
            l_ApplicationTypes = ApplicationTypes.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            l_Sites = Sites.Static_GetList(ActUserId);

            string OrderBy = "";
            byte AdvertDisplayTypeId = 0;
            int RowCount = 0;
            SiteId = short.Parse(ddlSite.SelectedValue);
            m_AdvertPositions.SiteId = SiteId;
            //m_AdvertPositions.CategoryId = short.Parse(ddlParentCategory.SelectedValue);
            m_grid.DataSource = m_AdvertPositions.Search(ActUserId, OrderBy, byte.Parse(ddlAppType.SelectedValue), txtSearch.Text, AdvertDisplayTypeId, ref RowCount);
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
            Label lblPositionName = (Label) m_grid.Rows[e.RowIndex].FindControl("lblPositionName");
            AdvertPositions m_AdvertPositions = new AdvertPositions();
            m_AdvertPositions.AdvertPositionId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_AdvertPositions.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            m_AdvertPositions = m_AdvertPositions.Get();
            if (m_AdvertPositions.AdvertPositionId > 0)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa Vị trí quảng cáo: {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else
            {
                deleteMessage = string.Format("Xóa Vị trí quảng cáo <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblPositionName.Text) ? lblPositionName.Text : "");
            }
            JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
            //ScriptManager.RegisterStartupScript(this.upn_Grid, this.GetType(), "showNotification", "showNotification({duration:'15', type: 'success', message: '" + deleteMessage + "' });", true);
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
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Categories> l_ParentCategory = Categories.Static_GetAllHierachy(ActUserId, "",short.Parse(ddlSite.SelectedValue), 0, 0, byte.Parse(ddlAppType.SelectedValue), 0, 0, "--");
            DropDownListHelpers.DDL_Bind(ddlParentCategory, l_ParentCategory, "...");
        BindData();
    }
    protected void lbReview_Click(object sender, EventArgs e)
    {
        try
        {
            string AdvSiteCode = "";
            string AdvCateCode = "";
            string AdvFile = "";
            string m_CategoryId = "-1";
            int count = 0;
            AdvertPositionAdverts m_AdvertPositionAdverts = new AdvertPositionAdverts();
            DataSet m_DataSet = m_AdvertPositionAdverts.GetScriptCode(short.Parse(ddlSite.SelectedValue), byte.Parse(ddlAppType.SelectedValue));
            if (m_DataSet.Tables.Count > 0)
            {
                foreach (DataRow mRow in m_DataSet.Tables[0].Rows)
                {
                    if (m_CategoryId != mRow["CategoryId"].ToString())
                    {
                        if (count > 0 && m_CategoryId != "0") 
                        {
                            AdvFile = Path.Combine(Request.PhysicalApplicationPath, "js/advs/advs_cate_" + m_CategoryId.ToString() + ".js");
                            FileUploadHelpers.CreateTextFile(AdvFile, AdvCateCode);
                        }

                        count++;
                        AdvCateCode = "";
                        m_CategoryId = mRow["CategoryId"].ToString();
                    }

                    if (mRow["CategoryId"].ToString() == "0")
                    {
                        if (AdvSiteCode != "") AdvSiteCode = AdvSiteCode + "\r\n";
                        AdvSiteCode = AdvSiteCode + "var advsite_" + mRow["AdvertPositionId"].ToString() + " = '" + mRow["Script"].ToString() + "';";
                    }
                    else
                    {
                        if (AdvCateCode != "") AdvCateCode = AdvCateCode + "\r\n";
                        AdvCateCode = AdvCateCode + "var advcate_" + mRow["AdvertPositionId"].ToString() + " = '" + mRow["Script"].ToString() + "';";
                    }
                }

                AdvFile = Path.Combine(Request.PhysicalApplicationPath, "js/advs/advs_site_" + ddlSite.SelectedValue + ".js");
                FileUploadHelpers.CreateTextFile(AdvFile, AdvSiteCode);

                AdvFile = Path.Combine(Request.PhysicalApplicationPath, "js/advs/advs_cate_" + m_CategoryId.ToString() + ".js");
                if (m_CategoryId != "0" && m_CategoryId != "-1") FileUploadHelpers.CreateTextFile(AdvFile, AdvCateCode);

                lblMsg.Text = "Gen script advert ok!";
            }
            BindData();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
}