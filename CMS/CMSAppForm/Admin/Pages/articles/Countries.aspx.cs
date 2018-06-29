using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.HelperLib;
using ICSoft.CMSLib;
using sms.admin.security;

public partial class Admin_Pages_articles_Countries : System.Web.UI.Page
{
    protected int CountryId = 0;
    protected int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            Countries m_Countries = new Countries();
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Countries"), "");
            BindData();
        }
        else if (CustomPaging.ChangePage)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            Countries m_Countries = new Countries();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            List<Countries> l_Countries = m_Countries.Countries_GetList(ActUserId, ddlOrderBy.SelectedValue, txtKey.Text);
                //m_Countries.Countries_GetList(ActUserId, ddlOrderBy.SelectedValue, txtKey.Text.Trim(), Int16.Parse(ddlCountries.SelectedIndex.ToString()));
            m_grid.DataSource = l_Countries;
            m_grid.DataBind();
            lblTong.Text = l_Countries.Count.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
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
        try
        {

            Countries m_Countries = new Countries();

            m_Countries.CountryId = Int16.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Countries.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 2)
            {
                JSAlert.Alert(GetLocalResourceObject("DeleteSuccess").ToString(), this);
            }
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
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected string getUserName(short userId)
    {
         Users m_Users = new Users();
        if (userId > 0)
            return m_Users.Get(userId).Username;
        else
            return "";
    }
}