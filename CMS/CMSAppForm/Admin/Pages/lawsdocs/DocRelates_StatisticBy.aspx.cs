using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
public partial class Admin_DocRelates_StatisticBy : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLRelateTypes_BindByCulture(ddlRelateTypes, RelateTypes.Static_GetList(), "...");
            BindData();
        }
        
    }

    private void BindData()
    {
        try
        {
            int TotalCount = 0;
            DataSet ds;
            DocRelates m_DocRelates = new DocRelates();
            string m_RelateTypeId = ddlRelateTypes.SelectedValue.ToString();
            if (m_RelateTypeId == "0")
            {
                m_RelateTypeId = "";
            }
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            int CrUserId = 0;
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            switch (ddlSearchBy.SelectedValue)
            {
                case "CrDateTime":
                    //thống kê theo ngày tạo
                    ds = m_DocRelates.DocRelates_StatisticByCrDateTime(ActUserId,m_RelateTypeId, m_ReviewStatusId, CrUserId, DateFrom,DateTo, ref TotalCount);
                    break;
                case "CrUserId":
                    //Thống kê theo người tạo
                    ds = m_DocRelates.DocRelates_StatisticByCrUserId(ActUserId, m_RelateTypeId, m_ReviewStatusId, CrUserId, DateFrom, DateTo, ref TotalCount);
                    break;               
                default:
                    //Thống kê theo trạng thái sử dụng
                    ds = m_DocRelates.DocRelates_StatisticByRelateTypeId(ActUserId, m_RelateTypeId, m_ReviewStatusId, CrUserId, DateFrom, DateTo, ref TotalCount);
                    break;
            }
            m_grid.DataSource = ds;
            m_grid.DataBind();
            lblTong.Text = (string.Format("{0:#,#}", TotalCount) != "" ? string.Format("{0:#,#}", TotalCount) : "0");
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Start Header
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlSearchBy.SelectedValue == "CrDateTime")
            {
                e.Row.Cells[0].Text = "Ngày";
                e.Row.Cells[1].Text = "Số lượng";
            }

            if (ddlSearchBy.SelectedValue == "CrUserId")
            {
                e.Row.Cells[0].Text = "Người tạo";
                e.Row.Cells[1].Text = "Số lượng";
            }
            if (ddlSearchBy.SelectedValue == "RelateTypeId")
            {
                e.Row.Cells[0].Text = "Loại liên kết";
                e.Row.Cells[1].Text = "Số lượng";
            }
        }

        // End Header
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlSearchBy.SelectedValue == "CrDateTime")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                e.Row.Cells[0].Text = ((DateTime)
                row[0]).ToString("dd/MM/yyyy");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "CrUserId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "RelateTypeId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                e.Row.Cells[0].Text = ((string)
                string.Format("{0:}", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }
   protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }


   protected void ddlRelateTypes_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
}

