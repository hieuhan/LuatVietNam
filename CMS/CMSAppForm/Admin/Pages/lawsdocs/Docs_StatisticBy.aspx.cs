using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
using sms.common;

public partial class Admin_Docs_StatisticBy : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected byte LanguageId = 0;
    // protected byte AppTypeId = 0;
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<UseStatus> l_UseStatus = new List<UseStatus>();
    protected List<EffectStatus> l_EffectStatus = new List<EffectStatus>();
    protected List<ICSoft.LawDocsLib.Organs> l_Organs = new List<ICSoft.LawDocsLib.Organs>();
    protected List<Fields> l_Fields = new List<Fields>();
    private  List<Users> lUsers = new List<Users>();
    private List<DataSources> lDataSources = new List<DataSources>();
    private List<DocTypes> lDocTypes = new List<DocTypes>();
    private List<EffectStatus> lEffectStatus = new List<EffectStatus>();
    private List<Fields> lFields = new List<Fields>();
    private List<ICSoft.LawDocsLib.Organs> lOrgans = new List<ICSoft.LawDocsLib.Organs>();
    private List<UseStatus> lUseStatus = new List<UseStatus>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            txtDateFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLDocTypes_BindByCulture(ddlDocTypes, DocTypes.Static_GetList(), "...");
            DropDownListHelpers.DDLFields_BindByCulture(ddlFields, Fields.Static_GetList(), "...");
            DropDownListHelpers.DDLOrgans_BindByCulture(ddlOrgans, ICSoft.LawDocsLib.Organs.Static_GetList(), "...");
            DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSources, DataSources.Static_GetListByDataTypeId(byte.Parse(ConfigurationManager.AppSettings["DATATYPE_LEGAL_DOCUMENTS"])), "...");
            DropDownListHelpers.DDLEffectStatus_BindByCulture(ddlEffectStatus, EffectStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLUseStatus_BindByCulture(ddlUseStatus, UseStatus.Static_GetList(), "...");
            BindData();
        }
        
    }

    private void BindData()
    {
        try
        {
            int TotalCount = 0;
            DataSet ds;
            Docs m_Docs = new Docs();
            byte ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            string m_SearchKeyword = "";
            string m_DocName = "";
            string m_DocIdentity = "";
            byte m_DocTypeId = byte.Parse(ddlDocTypes.SelectedValue);
            short m_DataSourceId = short.Parse(ddlDataSources.SelectedValue);
            short m_FieldId = short.Parse(ddlFields.SelectedValue);
            short m_OrganId = short.Parse(ddlOrgans.SelectedValue);
            short m_SignerId = 0;
            byte m_UseStatusId = byte.Parse(ddlUseStatus.SelectedValue);
            byte m_EffectStatusId = byte.Parse(ddlEffectStatus.SelectedValue);
            byte m_ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
            int CrUserId = 0;
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            lUsers = new Users().GetAll();
            lDataSources = DataSources.Static_GetList();
            lDocTypes = DocTypes.Static_GetList();
            lEffectStatus = EffectStatus.Static_GetList();
            lFields = Fields.Static_GetList();
            lOrgans = ICSoft.LawDocsLib.Organs.Static_GetList();
            lUseStatus = UseStatus.Static_GetList();
            int m_DocId = 0;
            switch (ddlSearchBy.SelectedValue)
            {
                case "CrDateTime":
                    //thống kê theo ngày tạo
                    ds = m_Docs.Docs_StatisticByCrDateTime(ActUserId, LanguageId, m_SearchKeyword, m_DocId,m_DocName,m_DocIdentity,m_DocTypeId,m_DataSourceId,m_FieldId,m_OrganId,m_SignerId,m_UseStatusId,m_EffectStatusId,m_ReviewStatusId, CrUserId,"", DateFrom,DateTo, ref TotalCount);
                    break;
                case "CrUserId":
                    //Thống kê theo người tạo
                    ds = m_Docs.Docs_StatisticByCrUserId(ActUserId, LanguageId, m_SearchKeyword, m_DocId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganId, m_SignerId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, CrUserId, "", DateFrom, DateTo, ref TotalCount);
                    break;
                case "DataSourceId":
                    //Thống kê theo nguồn văn bản
                    ds = m_Docs.Docs_StatisticByDataSourceId(ActUserId, LanguageId, m_SearchKeyword, m_DocId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganId, m_SignerId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, CrUserId, "", DateFrom, DateTo, ref TotalCount);
                    break;
                case "DocTypeId":
                    //Thống kê theo loại văn bản
                    ds = m_Docs.Docs_StatisticByDocTypeId(ActUserId, LanguageId, m_SearchKeyword, m_DocId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganId, m_SignerId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, CrUserId, "", DateFrom, DateTo, ref TotalCount);
                    break;
                case "EffectStatusId":
                    //Thống kê theo trạng thái hiệu lực
                    ds = m_Docs.Docs_StatisticByEffectStatusId(ActUserId, LanguageId, m_SearchKeyword, m_DocId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganId, m_SignerId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, CrUserId, "", DateFrom, DateTo, ref TotalCount);
                    break;
                case "FieldId":
                    //Thống kê theo lĩnh vực
                    ds = m_Docs.Docs_StatisticByFieldId(ActUserId, LanguageId, m_SearchKeyword, m_DocId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganId, m_SignerId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, CrUserId, "", DateFrom, DateTo, ref TotalCount);
                    break;
                case "IssueYear":
                    //Thống kê theo năm ban hành
                    ds = m_Docs.Docs_StatisticByIssueYear(ActUserId, LanguageId, m_SearchKeyword, m_DocId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganId, m_SignerId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, CrUserId, "", DateFrom, DateTo, ref TotalCount);
                    break;
                case "OrganId":
                    //Thống kê theo cơ quan ban hành
                    ds = m_Docs.Docs_StatisticByOrganId(ActUserId, LanguageId, m_SearchKeyword, m_DocId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganId, m_SignerId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, CrUserId, "", DateFrom, DateTo, ref TotalCount);
                    break;
                default:
                    //Thống kê theo trạng thái sử dụng
                    ds = m_Docs.Docs_StatisticByUseStatusId(ActUserId, LanguageId, m_SearchKeyword, m_DocId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganId, m_SignerId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, CrUserId, "", DateFrom, DateTo, ref TotalCount);
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
                e.Row.Cells[1].Text = "Số văn bản";
            }

            if (ddlSearchBy.SelectedValue == "CrUserId")
            {
                e.Row.Cells[0].Text = "Người biên tập";
                e.Row.Cells[1].Text = "Số văn bản";
            }
            if (ddlSearchBy.SelectedValue == "DataSourceId")
            {
                e.Row.Cells[0].Text = "Nguồn";
                e.Row.Cells[1].Text = "Số văn bản";
            }

            if (ddlSearchBy.SelectedValue == "DocTypeId")
            {
                e.Row.Cells[0].Text = "Loại văn bản";
                e.Row.Cells[1].Text = "Số văn bản";
            }

            if (ddlSearchBy.SelectedValue == "EffectStatusId")
            {
                e.Row.Cells[0].Text = "Trạng thái hiệu lực";
                e.Row.Cells[1].Text = "Số văn bản";
            }

            if (ddlSearchBy.SelectedValue == "FieldId")
            {
                e.Row.Cells[0].Text = "Lĩnh vực";
                e.Row.Cells[1].Text = "Số văn bản";
            }

            if (ddlSearchBy.SelectedValue == "IssueYear")
            {
                e.Row.Cells[0].Text = "Năm";
                e.Row.Cells[1].Text = "Số văn bản";
            }

            if (ddlSearchBy.SelectedValue == "OrganId")
            {
                e.Row.Cells[0].Text = "Cơ quan ban hành";
                e.Row.Cells[1].Text = "Số văn bản";
            }

            if (ddlSearchBy.SelectedValue == "UseStatusId")
            {
                e.Row.Cells[0].Text = "Trạng thái sử dụng";
                e.Row.Cells[1].Text = "Số văn bản";
            }

        }

        // End Header

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlSearchBy.SelectedValue == "CrDateTime")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                string query = ((DateTime)row[0]).ToString("dd-MM-yyyy");
                string crDateTime = ((DateTime)row[0]).ToString("dd/MM/yyyy");
                e.Row.Cells[0].Text =
                    string.Format(
                        "<a href=\"../../Pages/lawsdocs/Docs.aspx?CrDateTime={0}\" title=\"Link văn bản\">{1}</a>", query, crDateTime);
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }

            if (ddlSearchBy.SelectedValue == "CrUserId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                var mUser = lUsers.FirstOrDefault(u => u.Username.Equals(row[0].ToString()));
                string crUserId = mUser == null ? "0" : mUser.UserId.ToString();

                e.Row.Cells[0].Text = ((string)
                string.Format("<a href=\"../../Pages/lawsdocs/Docs.aspx?CrUserId={0}\" title=\"Link văn bản\">{1}</a>", crUserId, row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "DataSourceId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                var mDataSource = lDataSources.FirstOrDefault(u => u.DataSourceDesc.Equals(row[0].ToString()));
                string dataSourceId = mDataSource == null ? "0" : mDataSource.DataSourceId.ToString();

                e.Row.Cells[0].Text = ((string)
                string.Format("<a href=\"../../Pages/lawsdocs/Docs.aspx?DataSourceId={0}\" title=\"Link văn bản\">{1:#,#}</a>", dataSourceId, row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "DocTypeId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                var mDocTyper = lDocTypes.FirstOrDefault(u => u.DocTypeDesc.Equals(row[0].ToString()));
                string docTypeId = mDocTyper == null ? "0" : mDocTyper.DocTypeId.ToString();

                e.Row.Cells[0].Text = ((string)
                string.Format("<a href=\"../../Pages/lawsdocs/Docs.aspx?DocTypeId={0}\" title=\"Link văn bản\">{1:#,#}</a>", docTypeId, row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

            }

            if (ddlSearchBy.SelectedValue == "EffectStatusId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                var mEffectStatus = lEffectStatus.FirstOrDefault(u => u.EffectStatusDesc.Equals(row[0].ToString()));
                string effectStatusId = mEffectStatus == null ? "0" : mEffectStatus.EffectStatusId.ToString();

                e.Row.Cells[0].Text = ((string)
                string.Format("<a href=\"../../Pages/lawsdocs/Docs.aspx?EffectStatusId={0}\" title=\"Link văn bản\">{1:#,#}</a>", effectStatusId, row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "FieldId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                var mField = lFields.FirstOrDefault(u => u.FieldDesc.Equals(row[0].ToString()));
                string fieldId = mField == null ? "0" : mField.FieldId.ToString();

                e.Row.Cells[0].Text = ((string)
                string.Format("<a href=\"../../Pages/lawsdocs/Docs.aspx?FieldId={0}\" title=\"Link văn bản\">{1:#,#}</a>", fieldId, row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }

            if (ddlSearchBy.SelectedValue == "IssueYear")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                e.Row.Cells[0].Text = ((string)
                string.Format("<a href=\"../../Pages/lawsdocs/Docs.aspx?IssueYear={0}\" title=\"Link văn bản\">{0}</a>", row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }

            if (ddlSearchBy.SelectedValue == "OrganId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                var mOrgan = lOrgans.FirstOrDefault(u => u.OrganDesc.Equals(row[0].ToString()));
                string organId = mOrgan == null ? "0" : mOrgan.OrganId.ToString();

                e.Row.Cells[0].Text = ((string)
                string.Format("<a href=\"../../Pages/lawsdocs/Docs.aspx?OrganId={0}\" title=\"Link văn bản\">{1:#,#}</a>", organId, row[0]).ToString());
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[1].Text = ((string)
                string.Format("{0:#,#}", row[1]).ToString());
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
            if (ddlSearchBy.SelectedValue == "UseStatusId")
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;

                var mUseStatus = lUseStatus.Cast<UseStatus>().SingleOrDefault(u => u.UseStatusDesc.Equals(row[0].ToString()));
                string userStatusId = mUseStatus == null ? "0" : mUseStatus.UseStatusId.ToString();

                e.Row.Cells[0].Text = ((string)
                string.Format("<a href=\"../../Pages/lawsdocs/Docs.aspx?UseStatusId={0}\" title=\"Link văn bản\">{1:#,#}</a>", userStatusId, row[0]).ToString());
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

   protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   
   protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
   {
      BindData();
   }
   protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }

   protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlEffectStatus_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlUseStatus_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlOrgans_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlDocTypes_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlDataSources_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
   protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
   {
       BindData();
   }
}

