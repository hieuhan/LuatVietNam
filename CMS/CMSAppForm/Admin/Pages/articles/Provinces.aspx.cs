using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using sms.common;
using Countries = ICSoft.CMSLib.Countries;
using Provinces = ICSoft.CMSLib.Provinces;

public partial class Admin_Pages_articles_Provinces : System.Web.UI.Page
{
    protected short CountryId = 0;
    protected int ProvinceId = 0;
    protected int ActUserId = 0;
    protected List<Users> l_Users;
    protected List<Countries> l_Country;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Provinces"), "");
            DropDownListHelpers.DDL_Bind(ddlCountries, Countries.Static_GetList(), "", "0");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            Countries m_Country = new Countries();
            l_Country = m_Country.GetList();
            CountryId = short.Parse(ddlCountries.SelectedValue);

            Provinces m_Provinces = new Provinces();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            List<Provinces> l_Provinces = m_Provinces.Provinces_GetList(ActUserId, ddlOrderBy.SelectedValue, txtKey.Text.Trim(), CountryId);
            m_grid.DataSource = l_Provinces;
            m_grid.DataBind();
            lblTong.Text = l_Provinces.Count.ToString();
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

            ProvinceId = int.Parse(m_grid.DataKeys[e.Row.RowIndex].Value.ToString());

        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        try
        {
            Provinces m_Provinces = new Provinces();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Provinces.ProvinceId = System.Int16.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Provinces.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) //error
                        {
                            countDeleteError++;
                        }
                        else if (SysMessageTypeId == 2) //success
                        {
                            countDeleteSuccess++;
                        }
                    }
                }
            }
            if (countDeleteSuccess > 0)
            {
                messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " Thành phố.");
            }
            if (countDeleteError > 0)
            {
                messages += string.Format("<i>{0}</i> Thành phố chưa được xóa.", countDeleteError);
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
            BindData();
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);

        }
    }

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        try
        {
            Label lblProvinceName = (Label) m_grid.Rows[e.RowIndex].FindControl("lblProvinceName");
            Provinces m_Provinces = new Provinces();
            m_Provinces.ProvinceId = Int16.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Provinces.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa Thành phố : {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 2) //success
            {
                //JSAlert.Alert(GetLocalResourceObject("DeleteSuccess").ToString(), this);
                deleteMessage = string.Format("Xóa Thành phố <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblProvinceName.Text) ? lblProvinceName.Text : "");
            }
            JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
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
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

}