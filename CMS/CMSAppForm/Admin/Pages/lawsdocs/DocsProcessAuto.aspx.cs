/* ********************************************************************************
'     Document    :  DocsProcessAuto.aspx.cs
' ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class admin_pages_DocsProcessAuto : System.Web.UI.Page
{	
    protected int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
	{
        ActUserId = SessionHelpers.GetUserId();
		try
        {
            if (!IsPostBack)
            {
               
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
            DocsProcessAuto m_DocsProcessAuto = new DocsProcessAuto();
            m_DocsProcessAuto.DocName = txtSearch.Text;          
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            int RowCount = 0;
            m_DocsProcessAuto.ProcessStatusId = byte.Parse(ddlProcessStatusId.SelectedValue);
            List<DocsProcessAuto>  l_DocsProcessAuto = m_DocsProcessAuto.GetPage("", "", ddlOrderBy.SelectedValue, m_grid.PageSize, CustomPaging.PageIndex - 1, ref RowCount);
        
            m_grid.DataSource = l_DocsProcessAuto;
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
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("AdminResource", "DeleteConfirm").ToString() + "');";
            }                      
           
        }
    }   
    
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
        string message = "";
        int SysMessageId = 0;   
		try
		{
             DocsProcessAuto m_DocsProcessAuto = new DocsProcessAuto();
            m_DocsProcessAuto.DocProcessAutoId = System.Int32.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_DocsProcessAuto.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
           
		}
        catch (Exception ex)
		{
			sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
			
		}
        ShowGrid();
    }
       
    protected void ddlOrderBy_SelectedIndexChanged(object sender, System.EventArgs e)
	{
        CustomPaging.PageIndex = 1;
        ShowGrid();
    }

    
    protected void btnSearch_Click(object sender, System.EventArgs e)
	{       
        ShowGrid();
    }
	protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
		{
            int SysMessageId = 0;
            DocsProcessAuto m_DocsProcessAuto = new DocsProcessAuto();
            foreach(GridViewRow m_Row in m_grid.Rows)
			{
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if(chkAction != null)
				{
                    if(chkAction.Checked)
					{
                        m_DocsProcessAuto.DocProcessAutoId = System.Int32.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_DocsProcessAuto.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {

    }
    protected void lbReview_Click(object sender, EventArgs e)
    {

    }

    protected void ddlProcessStatusId_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        ShowGrid();
    }
    protected string getStatusDesc(object m_StatusId)
    {
        if (ddlProcessStatusId.Items.FindByValue(m_StatusId.ToString()) != null)
            return ddlProcessStatusId.Items.FindByValue(m_StatusId.ToString()).Text;
        else
            return m_StatusId.ToString();
    }
}
   