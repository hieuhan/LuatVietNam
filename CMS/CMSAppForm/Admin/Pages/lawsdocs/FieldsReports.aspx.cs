using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages_lawsdocs_FieldsReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlWeek.Items.Add(new ListItem("...", "0"));
            for (int index = 1; index <= 52; index++)
            {
                ListItem item = new ListItem(index.ToString(), index.ToString());
                ddlWeek.Items.Add(item);
            }

            ddlMonth.Items.Add(new ListItem("...", "0"));
            for (int index = 1; index <= 12; index++)
            {
                ListItem item = new ListItem(index.ToString(), index.ToString());
                ddlMonth.Items.Add(item);
            }

            ddlYear.Items.Add(new ListItem("...", "0"));
            for (int index = 1945; index <= 2099; index++)
            {
                ListItem item = new ListItem(index.ToString(), index.ToString());
                ddlYear.Items.Add(item);
            }
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            FieldsReports m_FieldReports = new FieldsReports();
            int RowCount = 0;
            short Week = 0;
            byte Month = 0;
            int Year = 0;
            string m_DateFrom = txtDateFrom.Text;
            string m_DateTo = txtDateTo.Text;
            Week = short.Parse(ddlWeek.SelectedValue);
            Month = byte.Parse(ddlMonth.SelectedValue);
            Year = int.Parse(ddlYear.SelectedValue);
            m_grid.DataSource = m_FieldReports.GetList(0,Week, Month, Year, m_DateFrom, m_DateTo, "", CustomPaging.PageIndex - 1, m_grid.PageSize, ref RowCount);
            m_grid.DataBind();
            m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
}