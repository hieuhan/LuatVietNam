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

public partial class Admin_Adverts : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short SiteId = 0;
    protected List<AdvertStatus> l_AdvertStatus = new List<AdvertStatus>();
    protected List<Users> l_Users;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlPartner, Partners.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlType, AdvertContentTypes.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlStatus, AdvertStatus.Static_GetList(), "...");
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            Adverts m_Adverts = new Adverts();
            l_AdvertStatus = AdvertStatus.Static_GetList();
            Users m_Users = new Users(); 
            l_Users = m_Users.GetAll();
            
            string OrderBy = "";
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            SiteId = short.Parse(ddlSite.SelectedValue);
            m_Adverts.SiteId = SiteId;
            m_grid.DataSource = m_Adverts.Search(ActUserId, OrderBy, txtSearch.Text, byte.Parse(ddlType.SelectedValue), short.Parse(ddlPartner.SelectedValue), byte.Parse(ddlStatus.SelectedValue), DateFrom, DateTo, ref RowCount);
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
            //Adverts mAdverts = (Adverts) e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
            //Label lblImagePath = (Label)e.Row.FindControl("lblImagePath");
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            Label lblAdvertName = (Label) m_grid.Rows[e.RowIndex].FindControl("lblAdvertName");
            Adverts m_Adverts = new Adverts();
            m_Adverts.AdvertId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Adverts.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa Quảng cáo: {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2)
            {
                deleteMessage = string.Format("Xóa Quảng cáo <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblAdvertName.Text) ? lblAdvertName.Text : "");
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
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlPartner_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            DataSet m_DataSet = m_AdvertPositionAdverts.GetScriptCode(short.Parse(ddlSite.SelectedValue), 0);
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