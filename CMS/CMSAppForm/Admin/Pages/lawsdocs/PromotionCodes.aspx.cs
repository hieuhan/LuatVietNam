/* ********************************************************************************
'     Document    :  PromotionCodes.aspx.cs
' ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class admin_pages_PromotionCodes : System.Web.UI.Page
{	
    protected int ActUserId = 0;
    protected int PromotionId = 0;
    protected void Page_Load(object sender, EventArgs e)
	{
        ActUserId = SessionHelpers.GetUserId();
        if (Request.QueryString["PromotionId"] != null)
        {
            PromotionId = int.Parse(Request.QueryString["PromotionId"]);
        }
        try
        {
            if (!IsPostBack)
            {

                if (Session["proCode-ddlOrderBy"] != null && Request.UrlReferrer != null && Request.UrlReferrer.OriginalString.Contains("PromotionCodesEdit.aspx"))
                {
                    ddlOrderBy.SelectedValue = Session["proCode-ddlOrderBy"].ToString();
                    txtDateFrom.Text = Session["proCode-txtDateFrom"].ToString();
                    txtDateTo.Text = Session["proCode-txtDateTo"].ToString();
                    txtSearch.Text = Session["proCode-Key"].ToString();
                }
				ShowGrid();
                Promotions m_Promotions = Promotions.Static_Get(PromotionId);
                lblPromotion.Text = m_Promotions.PromotionName;
                lbAddAuto.OnClientClick = "return confirm('Hệ thống sẽ tự động tạo thêm " + m_Promotions.NumOfCode.ToString() + " mã mới, mã là dạng " + (m_Promotions.CodeGenType == "DigitOnly" ? "chữ số" : "chữ cái và số") + " và có độ dài là " + m_Promotions.LenOfCode.ToString() + " . Bạn có chắc muốn tạo mã không?')";
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
            PromotionCodes m_PromotionCodes = new PromotionCodes();
            m_PromotionCodes.PromotionId = PromotionId;
            m_PromotionCodes.PromotionCode = txtSearch.Text;          
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            int RowCount = 0;
            List<PromotionCodes>  l_PromotionCodes = m_PromotionCodes.GetPage("", "", ddlOrderBy.SelectedValue, m_grid.PageSize, CustomPaging.PageIndex - 1, ref RowCount);
        
            m_grid.DataSource = l_PromotionCodes;
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
            SetCurentData();
		}
        catch (Exception ex)
		{
			sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
			
		}
    }
    protected void SetCurentData()
    {
        Session["proCode-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["proCode-txtDateFrom"] = txtDateFrom.Text;
        Session["proCode-txtDateTo"] = txtDateTo.Text;
        Session["proCode-Key"] = txtSearch.Text;
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
        byte SysMessageTypeId = 0;
        string Message = "";
        try
		{
             PromotionCodes m_PromotionCodes = new PromotionCodes();
            m_PromotionCodes.PromotionCodeId = System.Int32.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_PromotionCodes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
             if (SysMessageTypeId != 2) Message = "Xóa mã khuyến mại thất bại";
             else Message = "Xóa mã khuyến mại thành công";
           
		}
        catch (Exception ex)
		{
			sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
			
		}
        ShowGrid();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }
       
    protected void ddlOrderBy_SelectedIndexChanged(object sender, System.EventArgs e)
	{
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
            PromotionCodes m_PromotionCodes = new PromotionCodes();
            byte SysMessageTypeId = 0;
            int SysMessageId = 0;
            string Message = "";
            int CountSuc = 0;
            int CountFail = 0;
            foreach (GridViewRow m_Row in m_grid.Rows)
			{
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if(chkAction != null)
				{
                    if(chkAction.Checked)
					{
                        m_PromotionCodes.PromotionCodeId = System.Int32.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_PromotionCodes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId != 2) CountFail++;
                        else CountSuc++;
                    }
                }
            }
            Message = "Xóa thành công " + CountSuc + " mã khuyến mại";
            if (CountFail > 0) Message += " và thất bại " + CountFail + " mã khuyến mại ";
            JSAlertHelpers.ShowNotify("15", "success", Message, this);
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

    protected void lbAddAuto_Click(object sender, EventArgs e)
    {
        int m_CodeGenSuccess = 0;
        PromotionCodes m_PromotionCodes = new PromotionCodes();
        m_PromotionCodes.PromotionId = PromotionId;
        m_CodeGenSuccess = m_PromotionCodes.InsertAuto(ConstantHelpers.Replicated, ActUserId, 0);
        ShowGrid();
        JSAlertHelpers.ShowNotify("15", "success", "Tạo thành công " + m_CodeGenSuccess.ToString() + " mã.", this);
    }
}
   