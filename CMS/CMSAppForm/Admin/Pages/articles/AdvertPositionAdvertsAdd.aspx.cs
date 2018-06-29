using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_AdvertPositionAdvertsAdd : System.Web.UI.Page
{
    protected int ActUserId = 0;
    private int AdvertPositionId = 0;
    private short CategoryId = 0;
    private short SiteId = 0;
    protected List<AdvertStatus> l_AdvertStatus = new List<AdvertStatus>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["AdvertPositionId"] != null && Request.Params["AdvertPositionId"].ToString() != "")
        {
            AdvertPositionId = int.Parse(Request.Params["AdvertPositionId"].ToString());
        }
        if (Request.Params["CategoryId"] != null && Request.Params["CategoryId"].ToString() != "")
        {
            CategoryId = short.Parse(Request.Params["CategoryId"].ToString());
        }
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            //if (CategoryId == 0)
            //{
            //    lblMsg.Text = "Chọn chuyên mục trước khi chọn quảng cáo.";
            //    return;
            //}

            Adverts m_Adverts = new Adverts();
            l_AdvertStatus = AdvertStatus.Static_GetList();
            
            string OrderBy = "";
            string DateFrom = "";
            string DateTo = "";
            string AdvertName = "";
            byte AdvertContentTypeId = 0;
            byte AdvertStatusId = 1;
            short PartnerId = 0;
            int RowCount = 0;
            m_Adverts.SiteId = SiteId;
            m_grid.DataSource = m_Adverts.Search(ActUserId, OrderBy, AdvertName, AdvertContentTypeId, PartnerId, AdvertStatusId, DateFrom, DateTo, ref RowCount);
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        short SysMessageId = 0;
        try
        {
            if (AdvertPositionId > 0)
            {
                AdvertPositionAdverts m_AdvertPositionAdverts = new AdvertPositionAdverts();
                foreach (GridViewRow m_Row in m_grid.Rows)
                {
                    CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                    if (chkAction != null)
                    {
                        if (chkAction.Checked)
                        {
                            m_AdvertPositionAdverts.AdvertPositionId = AdvertPositionId;
                            m_AdvertPositionAdverts.CategoryId = CategoryId;
                            m_AdvertPositionAdverts.SiteId = SiteId;
                            m_AdvertPositionAdverts.AdvertId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                            m_AdvertPositionAdverts.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        }
                    }
                }
            }
            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit1').dialog('close');" +
                "</script>";
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            lblMsg.Text = ex.Message;
        }
    }
}