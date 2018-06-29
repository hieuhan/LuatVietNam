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

public partial class Admin_DocsContentRelationReview : System.Web.UI.Page
{
    public byte LanguageId = 0;
    private int DocId = 0;
    private int ActUserId = 0;
    public char chr;
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<Docs> l_Docs = new List<Docs>();
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
            //LEFT
            BindData(m_grid_Duoccancu, lblVBDuocCanCu, lblshowEmptyVBDuoccancu, ConfigurationManager.AppSettings["DOC_BASE"], 2, DocId, 0, LanguageId); //1
            BindData(m_grid_Duochuongdan, lblVBDuochuongdan, lblshowEmptyVBDuochuongdan, ConfigurationManager.AppSettings["DOC_GUIDE"], 2, DocId, 0, LanguageId); //3
            BindData(m_grid_Hethieuluc, lblVBHethieuluc, lblshowEmptyVBHethieuluc, ConfigurationManager.AppSettings["DOC_EXPIRED"], 2, DocId, 0, LanguageId);//2
            BindData(m_grid_Bisuadoi, lblVBBisuadoi, lblshowEmptyVBBisuadoi, ConfigurationManager.AppSettings["DOC_MODIFY"], 2, DocId, 0, LanguageId);//4
            BindData(m_grid_Duocthamchieu, lblVBDuocthamchieu, lblshowEmptyVBDuocthamchieu, ConfigurationManager.AppSettings["DOC_REF"], 2, DocId, 0, LanguageId);//5
            BindData(m_grid_Duoclienquan, lblVBDuoclienquan, lblshowEmptyVBDuoclienquan,  ConfigurationManager.AppSettings["DOC_REL"], 2, DocId, 0, LanguageId);//6
            BindData(m_grid_Hethieulucmotphan, lblVBHethieulucmotphan, lblshowEmptyVBHethieulucmotphan, ConfigurationManager.AppSettings["DOC_PARTEXPIRED"], 2, DocId, 0, LanguageId);//7
            BindData(m_grid_duocdinhchinh, lblVBDuocdinhchinh, lblshowEmptyVBduocdinhchinh, ConfigurationManager.AppSettings["DOC_CORRECTION"], 2, DocId, 0, LanguageId);//8
            BindData(m_grid_DuochopnhatGoc, lblVBDuochopnhatgoc, lblshowEmptyVBDuochopnhatgoc, ConfigurationManager.AppSettings["DOC_MER"], 2, DocId, 0, LanguageId);//9
            BindData(m_grid_DuochopnhatSuadoi, lblVBDuochopnhatsuadoi, lblshowEmptyVBDuochopnhatsuadoi, ConfigurationManager.AppSettings["DOC_MERS"], 2, DocId, 0, LanguageId);//10
            BindData(m_grid_Bidinhchihieuluc, lblVBBidinhchihieuluc, lblshowEmptyVBBidinhchihieuluc, ConfigurationManager.AppSettings["DOC_SUSPEND"], 2, DocId, 0, LanguageId);//11
            BindData(m_grid_Bidinhchimotphanhieuluc, lblVBBidinhchimotphanhieuluc, lblshowEmptyVBBidinhchimotphanhieuluc, ConfigurationManager.AppSettings["DOC_SUSPENDONE"], 2, DocId, 0, LanguageId);//12
            
            //RIGHT
            BindData(m_grid_Cancu, lblVBCancu, lblshowEmptyVBCancu, ConfigurationManager.AppSettings["DOC_BASE"], 2, 0, DocId, LanguageId);//1
            BindData(m_grid_Huongdan, lblVBHuongdan, lblshowEmptyVBhuongdan, ConfigurationManager.AppSettings["DOC_GUIDE"], 2, 0, DocId, LanguageId);//3
            BindData(m_grid_thaythe, lblVBThaythe, lblshowEmptyVBThaythe, ConfigurationManager.AppSettings["DOC_EXPIRED"], 2, 0, DocId, LanguageId);//2
            BindData(m_grid_Suadoi, lblVBSuadoi, lblshowEmptyVBSuadoi, ConfigurationManager.AppSettings["DOC_MODIFY"], 2, 0, DocId, LanguageId);//4
            BindData(m_grid_Thamchieu, lblVBThamchieu, lblshowEmptyVBThamchieu, ConfigurationManager.AppSettings["DOC_REF"], 2, 0, DocId, LanguageId);//5
            BindData(m_grid_lienquan, lblVBLienquan, lblshowEmptyVBLienquan, ConfigurationManager.AppSettings["DOC_REL"], 2, 0, DocId, LanguageId);//6
            BindData(m_grid_thaythemotphan, lblVBThaythemotphan, lblshowEmptyVBThaythemotphan, ConfigurationManager.AppSettings["DOC_PARTEXPIRED"], 2, 0, DocId, LanguageId);//7
            BindData(m_grid_dinhchinh, lblVBDinhchinh, lblshowEmptyVBDinhchinh, ConfigurationManager.AppSettings["DOC_CORRECTION"], 2, 0, DocId, LanguageId);//8
            BindData(m_grid_hopnhatgoc, lblVBHopnhatgoc, lblshowEmptyVBHopnhatgoc, ConfigurationManager.AppSettings["DOC_MER"], 2, 0, DocId, LanguageId);//9
            BindData(m_grid_hopnhatsuadoi, lblVBHopnhatsuadoi, lblshowEmptyVBHopnhatsuadoi, ConfigurationManager.AppSettings["DOC_MERS"], 2, 0, DocId, LanguageId);//10
            BindData(m_grid_dinhchihieuluc, lblVBDinhchihieuluc, lblshowEmptyVBDinhchihieuluc, ConfigurationManager.AppSettings["DOC_SUSPEND"], 2, 0, DocId, LanguageId);//11
            BindData(m_grid_dinhchimotphanhieuluc, lblVBDinhchimotphanhieuluc, lblshowEmptyVBDinhchimotphanhieuluc, ConfigurationManager.AppSettings["DOC_SUSPENDONE"], 2, 0, DocId, LanguageId);//12
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
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Lĩnh vực:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["FieldName"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Ngày ban hành:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["IssueDate"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Ngày hiệu lực:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["EffectDate"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Ngày hết hiệu lực:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["ExpireDate"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Loại văn bản:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["DocTypeName"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Số hiệu:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["DocIdentity"]) + "</td>");
            st.Append("</tr>");
            st.Append("<tr><td class='VL_Tip_Property_C1' style='border-right:1px solid #FF5C00;border-bottom: 1px solid #FF5C00; text-align:left;'>Người ký:</td>");
            st.Append("<td class='VL_Tip_Property_C2' style='border-bottom: 1px solid #FF5C00; text-align:left;'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["SignerName"]) + "</td>");
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
       byte m_OraganTypeId = 0;
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
                                           m_DocId, m_DocGUId, m_DocName, m_DocIdentity, m_DocTypeId, m_DataSourceId, m_FieldId, m_OraganTypeId, m_OrganId, m_SignerId, m_DocRelateId,
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