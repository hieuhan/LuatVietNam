
/* ********************************************************************************
'     Document    :  DataDictionaries.aspx.cs
' ********************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Reflection;
using System.IO;
using ICSoft.HelperLib;
using sms.common;
using sms.utils;
using ICSoft.CMSLib;
using sms.admin.security;
using System.Globalization;

public partial class admin_pages_DataDictionaries : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short DataDictionaryTypeId = 0;

    protected List<DataDictionaryTypes> l_DataDictionaryTypes = new List<DataDictionaryTypes>();
    protected List<Users> l_Users = new List<Users>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        try
        {
            if (!IsPostBack)
            {
                DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("DataDictionaries"), "");
                DropDownListHelpers.DDL_Bind(ddlDataDictionaryTypes, DataDictionaryTypes.Static_GetList(), "", "0");
                ShowGrid();
            }
            else if (CustomPaging.ChangePage)
            {
                ShowGrid();
            }
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);

        }
    }
    //--------------------------------------------------------------------------------

    private void ShowGrid()
    {
        try
        {
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            l_DataDictionaryTypes = DataDictionaryTypes.Static_GetList();
            DataDictionaryTypeId = short.Parse(ddlDataDictionaryTypes.SelectedValue);

            DataDictionaries m_DataDictionaries = new DataDictionaries();
            m_DataDictionaries.DataDictionaryName = txtSearch.Text;
            string m_OrderBy = ddlOrderBy.SelectedValue;
            m_DataDictionaries.DataDictionaryTypeId = DataDictionaryTypeId;
            string DateFrom = "";
            string DateTo = "";
            int RowCount = 0;
            List<DataDictionaries> l_DataDictionaries = m_DataDictionaries.GetPage(ActUserId, m_grid.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, DateFrom, DateTo, ref RowCount);

            m_grid.DataSource = l_DataDictionaries;
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);

        }
    }
    // End Show Grid   
    //--------------------------------------------------------------------------------

    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
         try
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
         catch (Exception ex)
         {
             sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
             JSAlertHelpers.Alert(ex.Message, this);
         }
    }
    //--------------------------------------------------------------------------------

    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
                DataDictionaries m_DataDictionaries = new DataDictionaries();
                m_DataDictionaries.DataDictionaryId = System.Int32.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
                SysMessageTypeId = m_DataDictionaries.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        ShowGrid();
    }
    //--------------------------------------------------------------------------------

    protected void ddlOrderBy_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        ShowGrid();
    }
    //--------------------------------------------------------------------------------

    protected void ddlDataDictionaryTypes_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ShowGrid();
    }
    //--------------------------------------------------------------------------------

    protected void btnSearch_Click(object sender, System.EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        ShowGrid();
    }
    //--------------------------------------------------------------------------------

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            DataDictionaries m_DataDictionaries = new DataDictionaries();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        
                        m_DataDictionaries.DataDictionaryId = System.Int32.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_DataDictionaries.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        //m_DataDictionaries.Delete();
                    }
                }
            }
            ShowGrid();
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);

        }
    }
    //--------------------------------------------------------------------------------

}
