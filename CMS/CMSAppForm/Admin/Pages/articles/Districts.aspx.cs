﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_Pages_articles_Districts : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short ProvinceId = 0;
    protected short CountryId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    protected List<Users> l_Users = new List<Users>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlCountries, Countries.Static_GetList(), "");
            DropDownListHelpers.DDL_Bind(ddlProvinces, Provinces.Static_GetList(short.Parse(ddlCountries.SelectedValue)), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Districts"), "");
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------
    private void BindData()
    {
        try
        {
            int RowCount = 0;
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            CountryId = short.Parse(ddlCountries.SelectedValue);
            ProvinceId = short.Parse(ddlProvinces.SelectedValue);

            Districts m_Districts = new Districts();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_DistrictName = txtContentSearch.Text.Trim();
            m_Districts.CountryId = CountryId;
            m_Districts.ProvinceId = ProvinceId;

            m_grid.DataSource = m_Districts.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, ref RowCount);
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------

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
    //----------------------------------------------------------------------------------------------------------------------------

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Districts m_Districts = new Districts();
            m_Districts.DistrictId = Int16.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Districts.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Districts m_Districts = new Districts();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Districts.DistrictId = Int16.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Districts.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
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
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        DropDownListHelpers.DDL_Bind(ddlProvinces, Provinces.Static_GetList(short.Parse(ddlCountries.SelectedValue)), "...");
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

    protected void ddlProvinces_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    //----------------------------------------------------------------------------------------------------------------------------

}