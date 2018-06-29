using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Reflection;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

public partial class Admin_DocsContentRelation : System.Web.UI.Page
{
    public byte LanguageId = 0;
    protected int DocId = 0;
    private int ActUserId = 0;
    public char chr;
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<Docs> l_Docs = new List<Docs>();
    protected List<DocRelates> l_DocRelates = new List<DocRelates>();
    Docs m_Docs = new Docs();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
        {
            DocId = int.Parse(Request.Params["DocId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (!IsPostBack)
        {
            if (Request.Params["DocId"] != "" && Request.Params["LanguageId"] != "")
            {
                
                lbTip.Text = GetProperties(ActUserId, byte.Parse(Request.Params["LanguageId"]), int.Parse(Request.Params["DocId"]));
                GetPropertiesEn(ActUserId, (byte.Parse(Request.Params["LanguageId"]) == 1 ? byte.Parse("2") : byte.Parse("1")), int.Parse(Request.Params["DocId"]));
            }
            m_Docs.DocId = DocId;
            m_Docs = m_Docs.Get();
            bindRepeater();
        }
        
    }

    private void bindRepeater()
    {
        int RowCount = 0;
        RelateTypes m_RelateTypes = new RelateTypes
        {
            DocGroupId = m_Docs.DocGroupId
        };
        string m_OrderBy = "";       
        string m_DateFrom = "";
        string m_DateTo = "";
        int PageSize = 100;
        int PageIndex = 0;
        string textvbhn = "";

        DocRelates m_DocRelates = new DocRelates();
        l_DocRelates = m_DocRelates.Docs_GetViewRelates(LanguageId, DocId, 0, "",0 , 500, 0, 0, ref RowCount);

        List<RelateTypes> lRelateTypesAll = m_RelateTypes.GetByGroupId();
        rptRelateType1.DataSource = from rt in lRelateTypesAll where rt.DisplayPosition.Equals("Left") select rt;
        rptRelateType1.DataBind();
        rptRelateType3.DataSource = from rt in lRelateTypesAll where rt.DisplayPosition.Equals("Right") select rt;
        rptRelateType3.DataBind();
        rptRelateType2.DataSource = from rt in lRelateTypesAll where rt.DisplayPosition.Equals("Bottom") select rt;
        rptRelateType2.DataBind();
        switch (m_Docs.DocGroupId)
        {
            case 3:
                rptRelateTypeTop1.DataSource = from rt in lRelateTypesAll where rt.DisplayPosition.Equals("Top1") select rt;
                rptRelateTypeTop1.DataBind();
                rptRelateTypeTop2.DataSource = from rt in lRelateTypesAll where rt.DisplayPosition.Equals("Top2") select rt;
                rptRelateTypeTop2.DataBind();
                tblTop1.Visible = true;
                tblTop2.Visible = true;
                tblVbNgonNguLienQuan.Visible = false;
                break;
            case 5:
                tblVbNgonNguLienQuan.Visible = false;
                break;
        }
    }
    protected void rptRelateType1_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {

        // This event is raised for the header, the footer, separators, and items.

        // Execute the following logic for Items and Alternating Items.
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView m_grid = (GridView)e.Item.FindControl("m_grid");
            Label lbRelateTypeName = (Label)e.Item.FindControl("lbRelateTypeName");
            HiddenField hdfRelateTypeId = (HiddenField)e.Item.FindControl("hdfRelateTypeId");
            int RowCount = 0;
            chr = Convert.ToChar(39);
            DocRelates m_DocRelates = new DocRelates();
            string m_OrderBy = "";
            byte m_LanguageId = LanguageId;
            int m_DocId = DocId;
            int m_DocReferenceId = 0;// DocId;
            string m_RelateTypeId = hdfRelateTypeId.Value;
            byte m_ReviewStatusId = 0;
            int m_CrUserId = 0;
            short m_FieldId = 0;
            string m_DateFrom = "";
            string m_DateTo = "";
            string m_SearchByDate = "";
            //m_grid.DataSource = m_DocRelates.GetPage(ActUserId, m_grid.PageSize, m_grid.PageIndex - 1, m_OrderBy, m_LanguageId, m_FieldId, m_DocId, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
            List<DocRelates> l_DocRelatesByRelateTypeId = l_DocRelates.FindAll(i => i.RelateTypeId == byte.Parse(hdfRelateTypeId.Value) && i.DisplayPosition == "Left");
            m_grid.DataSource = l_DocRelatesByRelateTypeId;
            m_grid.DataBind();
            lbRelateTypeName.Text = lbRelateTypeName.Text + " (<span class='DocRowCount' >" + m_grid.Rows.Count.ToString() + "</span>)";
        }
    }
    protected void rptRelateType2_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {

        // This event is raised for the header, the footer, separators, and items.

        // Execute the following logic for Items and Alternating Items.
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView m_grid = (GridView)e.Item.FindControl("m_grid");
            Label lbRelateTypeName = (Label)e.Item.FindControl("lbRelateTypeName");
            HiddenField hdfRelateTypeId = (HiddenField)e.Item.FindControl("hdfRelateTypeId");
            int RowCount = 0;
            chr = Convert.ToChar(39);
            DocRelates m_DocRelates = new DocRelates();
            string m_OrderBy = "";
            byte m_LanguageId = LanguageId;
            int m_DocId = DocId;
            int m_DocReferenceId = 0;
            string m_RelateTypeId = hdfRelateTypeId.Value;
            byte m_ReviewStatusId = 0;
            int m_CrUserId = 0;
            short m_FieldId = 0;
            string m_DateFrom = "";
            string m_DateTo = "";
            string m_SearchByDate = "";
            List<DocRelates> l_DocRelates = m_DocRelates.DocRelates_GetPageByDocId(ActUserId, m_grid.PageSize, m_grid.PageIndex - 1, m_OrderBy, m_LanguageId, m_DocId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_DateFrom, m_DateTo, ref RowCount);
            foreach(DocRelates m_DocRelateTemp in l_DocRelates)
            {
                string DocName = m_DocRelateTemp.DocName;
                int DocIdTemp = m_DocRelateTemp.DocId;
                //swap to bind
                if(m_DocRelateTemp.DocReferenceId == DocId)
                {
                    m_DocRelateTemp.DocName = m_DocRelateTemp.DocNameRef;
                    m_DocRelateTemp.DocId = m_DocRelateTemp.DocReferenceId;
                    m_DocRelateTemp.DocNameRef = DocName;
                    m_DocRelateTemp.DocReferenceId = DocIdTemp;
                }
                
            }
            m_grid.DataSource = l_DocRelates;
            m_grid.DataBind();
            lbRelateTypeName.Text = lbRelateTypeName.Text + " (<span class='DocRowCount' >" + RowCount.ToString() + "</span>)";
        }
    }
    protected void rptRelateType3_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {

        // This event is raised for the header, the footer, separators, and items.

        // Execute the following logic for Items and Alternating Items.
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView m_grid = (GridView)e.Item.FindControl("m_grid");
            Label lbRelateTypeName = (Label)e.Item.FindControl("lbRelateTypeName");
            HiddenField hdfRelateTypeId = (HiddenField)e.Item.FindControl("hdfRelateTypeId");
            int RowCount = 0;
            chr = Convert.ToChar(39);
            DocRelates m_DocRelates = new DocRelates();
            string m_OrderBy = "";
            byte m_LanguageId = LanguageId;
            int m_DocId = 0;
            int m_DocReferenceId = DocId;
            string m_RelateTypeId = hdfRelateTypeId.Value;
            byte m_ReviewStatusId = 0;
            int m_CrUserId = 0;
            short m_FieldId = 0;
            string m_DateFrom = "";
            string m_DateTo = "";
            string m_SearchByDate = "";
            //m_grid.DataSource = m_DocRelates.GetPage(ActUserId, m_grid.PageSize, m_grid.PageIndex - 1, m_OrderBy, m_LanguageId, m_FieldId, m_DocId, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
            List<DocRelates> l_DocRelatesByRelateTypeId = l_DocRelates.FindAll(i => i.RelateTypeId == byte.Parse(hdfRelateTypeId.Value) && i.DisplayPosition == "Right");
            m_grid.DataSource = l_DocRelatesByRelateTypeId;
            m_grid.DataBind();
            lbRelateTypeName.Text = lbRelateTypeName.Text + " (<span class='DocRowCount' >" + m_grid.Rows.Count.ToString() + "</span>)";
        }
    }
    protected void rptRelateTypeTop1_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {

        // This event is raised for the header, the footer, separators, and items.

        // Execute the following logic for Items and Alternating Items.
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView m_grid = (GridView)e.Item.FindControl("m_grid");
            Label lbRelateTypeName = (Label)e.Item.FindControl("lbRelateTypeName");
            HiddenField hdfRelateTypeId = (HiddenField)e.Item.FindControl("hdfRelateTypeId");
            int RowCount = 0;
            chr = Convert.ToChar(39);
            DocRelates m_DocRelates = new DocRelates();
            string m_OrderBy = "";
            byte m_LanguageId = LanguageId;
            int m_DocId = DocId;
            int m_DocReferenceId = 0;// DocId;
            string m_RelateTypeId = hdfRelateTypeId.Value;
            byte m_ReviewStatusId = 0;
            int m_CrUserId = 0;
            short m_FieldId = 0;
            string m_DateFrom = "";
            string m_DateTo = "";
            string m_SearchByDate = "";
            m_grid.DataSource = m_DocRelates.GetPage(ActUserId, m_grid.PageSize, m_grid.PageIndex - 1, m_OrderBy, m_LanguageId, m_FieldId, m_DocId, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            lbRelateTypeName.Text = lbRelateTypeName.Text + " (<span class='DocRowCount' >" + RowCount.ToString() + "</span>)";
        }
    }
    protected void rptRelateTypeTop2_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {

        // This event is raised for the header, the footer, separators, and items.

        // Execute the following logic for Items and Alternating Items.
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView m_grid = (GridView)e.Item.FindControl("m_grid");
            Label lbRelateTypeName = (Label)e.Item.FindControl("lbRelateTypeName");
            HiddenField hdfRelateTypeId = (HiddenField)e.Item.FindControl("hdfRelateTypeId");
            int RowCount = 0;
            chr = Convert.ToChar(39);
            DocRelates m_DocRelates = new DocRelates();
            string m_OrderBy = "";
            byte m_LanguageId = LanguageId;
            int m_DocId = DocId;
            int m_DocReferenceId = 0;// DocId;
            string m_RelateTypeId = hdfRelateTypeId.Value;
            byte m_ReviewStatusId = 0;
            int m_CrUserId = 0;
            short m_FieldId = 0;
            string m_DateFrom = "";
            string m_DateTo = "";
            string m_SearchByDate = "";
            m_grid.DataSource = m_DocRelates.GetPage(ActUserId, m_grid.PageSize, m_grid.PageIndex - 1, m_OrderBy, m_LanguageId, m_FieldId, m_DocId, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            lbRelateTypeName.Text = lbRelateTypeName.Text + " (<span class='DocRowCount' >" + RowCount.ToString() + "</span>)";
        }
    }
    private void BindData(GridView m_grid, Label lblVBName, Label lblshowEmptyVB, string RelateTypeId, byte ReviewStatusId, int DocId, int DocReferenceId, byte LanguageId)
    {
        try
        {
            int RowCount = 0;
            chr = Convert.ToChar(39);
            DocRelates m_DocRelates = new DocRelates();
            string m_OrderBy = "";
            byte m_LanguageId = LanguageId;
            int m_DocId = DocId;
            int m_DocReferenceId = DocReferenceId;
            string m_RelateTypeId = RelateTypeId;
            byte m_ReviewStatusId = ReviewStatusId;
            int m_CrUserId = 0;
            short m_FieldId = 0;
            string m_DateFrom = "";
            string m_DateTo = "";
            string m_SearchByDate = "";
            m_grid.DataSource = m_DocRelates.GetPage(ActUserId, m_grid.PageSize, m_grid.PageIndex - 1, m_OrderBy, m_LanguageId, m_FieldId, m_DocId, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
            m_grid.DataBind();
            lblVBName.Text = lblVBName.Text + " (<span class='DocRowCount' >" + RowCount.ToString() + "</span>)";
            if (RowCount > 0)
            {
                lblshowEmptyVB.Visible = false;
            }
            else
            {
                lblshowEmptyVB.Visible = false;
            }

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
   
    private string GetProperties(int ActUserId, byte LanguageId, int DocId)
    {
        string RetVal = "";
        Docs m_Docs = new Docs();
        DataSet ds = m_Docs.Docs_GetProperties(ActUserId, LanguageId, DocId);

        if (ds.Tables[0].Rows.Count > 0)
        {
            StringBuilder st = new StringBuilder();
            lblDocName.Text ="<Span class='VL_DocName'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["DocName"]) + "</Span>";
            st.Append("<table width='100%' class='VL_Tip_Property_Tb' cellspacing='0' cellpadding='2' style='border-collapse:collapse; border : 1px solid #FF5C00;'>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00; border-bottom: 1px solid #FF5C00; text-align:left;'>Cơ quan ban hành:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["OrganName"]) + "</td>");
            st.Append("</tr>");
            

            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Số hiệu:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["DocIdentity"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Loại văn bản:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["DocTypeName"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Ngày ban hành:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["IssueDate"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Ngày hiệu lực:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["EffectDate"]) + "</td>");
            st.Append("</tr>");

            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Lĩnh vực:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["FieldName"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Ngày công báo:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["GazetteDate"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Số công báo:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["GazetteNumber"]) + "</td>");
            st.Append("</tr>");

            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Người ký:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["SignerName"]) + "</td>");
            st.Append("</tr>");

            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Ngày hết hiệu lực:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["ExpireDate"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Tình trạng hiệu lực:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["EffectStatusName"]) + "</td>");
            st.Append("</tr>");



            st.Append("</table>");

            return st.ToString();

        }
        return RetVal;
    }

    private void GetPropertiesEn(int ActUserId, byte LanguageId, int DocId)
    {
        Docs m_Docs = new Docs();
        DataSet ds = m_Docs.Docs_GetProperties(ActUserId, LanguageId, DocId);
        int RetVal = 0;
        if (ds.Tables[0].Rows.Count > 0)
        {
            StringBuilder st = new StringBuilder();
            lblshowEmptyVBNgonngu.Text = "<Span class='VL_DocName'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["DocName"]) + "</Span>";
            RetVal = ds.Tables[0].Rows.Count;            
        }
        else
        {
            //lblDocRelateEnDetail.Text = "...";
            RetVal = 0;
        }
        lblDocRelateEn.Text = lblDocRelateEn.Text + " (<span class='DocRowCount' >" + RetVal.ToString() + "</span>)";

        int RowCount = 0;
        string m_OrderBy = "";
        int m_DocId = DocId;
        string m_DocGUId = "";
        short m_DisplayTypeId = 0;
        string m_SearchKeyword = "";
        string m_DocName = "";
        string m_DocIdentity = "";
        byte m_IsSearchExact = 0;
        byte m_HighlightKeyword = 1;
        byte m_HasDocFile = 0;
        byte m_DocTypeId = 0;
        short m_DataSourceId = 0;
        short m_FieldId = 0;
        byte m_OrganTypeId = 0;
        short m_OrganId = 0;
        short m_SignerId = 0;
        int m_DocRelateId = 0;
        byte m_UseStatusId = 0;
        byte m_EffectStatusId = 0;
        byte m_ReviewStatusId = 0;
        int m_CrUserId = 0;
        string m_SearchByDate = "";
        string m_DateFrom = "";
        string m_DateTo = "";
        m_grid_ngonngu.DataSource = m_Docs.GetPage(ActUserId, m_grid_ngonngu.PageSize, 0, m_OrderBy, LanguageId, m_SearchKeyword, m_IsSearchExact, m_HighlightKeyword,
                                            m_DocId, m_DocGUId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OrganTypeId, m_OrganId, m_SignerId, m_DocRelateId,
                                            m_DisplayTypeId, m_UseStatusId, m_EffectStatusId, m_ReviewStatusId, m_HasDocFile, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
        m_grid_ngonngu.DataBind();
    }

   protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        Docs m_Docs = new Docs();
        m_Docs = m_Docs.Get(DocId, LanguageId);
        if (!String.IsNullOrEmpty(Request["backUrl"]))
        {
            string backUrl = Request["backUrl"];
            Response.Redirect(backUrl + "?LanguageId=" + LanguageId.ToString());
        }
        else
        {
            if (m_Docs.DocGroupId <= 2)
                Response.Redirect("Docs.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 3)
                Response.Redirect("VietNamStandards.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 4)
                Response.Redirect("DocsTTHC.aspx?LanguageId=" + LanguageId.ToString());
            else if (m_Docs.DocGroupId == 5)
                Response.Redirect("DocsConsolidation.aspx?LanguageId=" + LanguageId.ToString());
        }
    }
}