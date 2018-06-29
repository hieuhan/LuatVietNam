using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using ICSoft.LawDocsLib;
using sms.admin.security;
using System.Globalization;

public partial class Admin_ShowGenBTHL : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected int DocRelateId = 0;
    protected byte DocTypeId = 0;
    protected short FieldId = 0;
    protected byte LanguageId = 0;
    protected string DateFrom = "";
    protected string DateTo = "";
    protected string RelateTypeId = "";
    protected string Content = "";
    protected string ActionType = "";
    protected List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["DateFrom"] != null && Request.Params["DateFrom"].ToString() != "")
        {
            DateFrom = Request.Params["DateFrom"].ToString();
        }
        if (Request.Params["DateTo"] != null && Request.Params["DateTo"].ToString() != "")
        {
            DateTo = Request.Params["DateTo"].ToString();
        }
        if (Request.Params["RelateTypeId"] != null && Request.Params["RelateTypeId"].ToString() != "")
        {
            RelateTypeId = Request.Params["RelateTypeId"].ToString();
        }
        if (Request.Params["ActionType"] != null && Request.Params["ActionType"].ToString() != "")
        {
            ActionType = Request.Params["ActionType"].ToString();
            if (ActionType == "Edit") 
            {
                if (Request.Params["Msg"] != null && Request.Params["Msg"].ToString() != "")
                {
                    lblMessage.Text = Request.Params["Msg"].ToString();
                }
            }
        }
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (Request.Params["DocRelateId"] != null && Request.Params["DocRelateId"].ToString() != "")
        {
            DocRelateId = int.Parse(Request.Params["DocRelateId"].ToString());
        } 
        if (!IsPostBack)
        {
            if (ActionType == "Del")
            {
                DocRelates m_DocRelates = new DocRelates();
                m_DocRelates.DocRelateArticles_Delete_Quick(ActUserId, ArticleId, DocRelateId);
            }
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_RelateTypes = RelateTypes.Static_GetList();
            DocRelates m_DocRelates = new DocRelates();
            string m_Orderby = "";
            byte m_LanguageId = LanguageId;
            int m_DocId = 0;
            int m_DocReferenceId = 0;
            string m_RelateTypeId = RelateTypeId;
            byte m_ReviewStatusId = 2;
            int m_CrUserId = 0;
            string m_SearchByDate = "";
            string m_DateFrom = DateFrom;
            string m_DateTo = DateTo;

            if (ActionType == "Create")
            {
                m_DocRelates.DocRelateArticles_Create(ActUserId, 0, 0, m_Orderby, m_LanguageId, 0, ArticleId, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo);
            }

            //ListFields.DataSource = m_DocRelates.DocRelates_GetField(ActUserId, m_Orderby, m_LanguageId, m_DocId, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo);
            ListFields.DataSource = m_DocRelates.DocRelateArticles_GetField(m_LanguageId, ArticleId);
            ListFields.DataBind();

            //m_grid.DataSource = m_DocRelates.DocRelates_GetField(ActUserId, m_Orderby, m_LanguageId, m_DocId, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo);
            m_grid.DataSource = m_DocRelates.DocRelateArticles_GetField(m_LanguageId, ArticleId);
            m_grid.DataBind();

            btnAddBTHL.Visible = true;
            btnExportFileWord.Visible = true;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    public string ToUpper( string Str)
    {
        Str = Str.ToUpper();
        return Str;
    }

    protected void m_grid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DocRelates m_DataItem = (DocRelates)e.Row.DataItem;
            short FieldId = m_DataItem.FieldId;
            Label lblDocRealates = (Label)e.Row.FindControl("lblDocRealates");
            lblDocRealates.Text = GetDataByFieldId(FieldId, LanguageId, DateFrom, DateTo, RelateTypeId);          
        }
    }

    private string GetDataByFieldId(short FieldId, byte LanguageId, string DateFrom, string DateTo, string RelateTypeId)
    {
        int RowCount = 0;
        string RetVal = "";
        RelateTypes m_RelateTypes = new RelateTypes();
        DocRelates m_DocRelates = new DocRelates();
        string m_RelateTypeId = RelateTypeId;
        byte m_LanguageId = LanguageId;
        byte m_ReviewStatusId = 2;
        string m_OrderBy = "";
        string m_SearchByDate = "";
        int m_CrUserId = 0;
        short m_Field = FieldId;
        string m_DateFrom = DateFrom;
        string m_DateTo = DateTo;
        int m_DocId = 0;
        int m_DocReferenceId = 0;
        //DataSet ds = m_DocRelates.DocRelates_GetPage(ActUserId, 100, 0, m_OrderBy, m_LanguageId, m_Field, m_DocId, m_DocReferenceId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_SearchByDate, m_DateFrom, m_DateTo, ref RowCount);
        DataSet ds = m_DocRelates.DocRelateArticles_GetByFieldId(m_LanguageId, ArticleId, m_Field);

        MessageTemplates m_MessageTemplates = new MessageTemplates();
        m_MessageTemplates = m_MessageTemplates.GetByMessageTemplateName("Ban tin hieu luc");

        byte RelateTypeIdTemp = 0;
        int i = 0;
        StringBuilder st = new StringBuilder();
        if (ds.Tables[0].Rows.Count > 0)
        {
            st.Append("<table class='bthl-width100' style='width:100%;' border='0' cellpadding='0' cellspacing='0'>");
            for (i = 0; i < ds.Tables[0].Rows.Count ; i++)
            {
                if (RelateTypeIdTemp != byte.Parse(ds.Tables[0].Rows[i]["RelateTypeId"].ToString()))
                {
                    if (RelateTypeIdTemp == 0)
                    {

                        st.Append("<tr><td class='bthl-title-rel2' style='background-color:#DEDEDE; text-align:center; width=50%; border-style: none; border-width: medium'><b>" + RelateTypes.Static_Get(byte.Parse(ds.Tables[0].Rows[i]["RelateTypeId"].ToString()), l_RelateTypes).RelateTypeDesc + "</b></td>");
                        st.Append("<td class='bthl-width1' style='width:1%;'></td>");
                        st.Append("<td class='bthl-title-rel2' style='background-color:#DEDEDE; text-align:center; width=49%; border-style: none; border-width: medium'><b>Văn bản thay thế</b></td>");
                        st.Append("</tr>");
                    }
                    else
                    {
                        st.Append("<tr><td class='bthl-tr' style='width:50%;height:8px; border-style: none; border-width: medium;'></td><td style='width:1%;height:8px;'></td><td style='width:49%;height:8px;'></td></tr>");
                        st.Append("<tr><td class='bthl-title-rel2' style='background-color:#DEDEDE; text-align:center; width=50%; border-style: none; border-width: medium'><b>" + RelateTypes.Static_Get(byte.Parse(ds.Tables[0].Rows[i]["RelateTypeId"].ToString()), l_RelateTypes).RelateTypeDesc + "</b></td>");
                        st.Append("<td class='bthl-width1;' style='text-align:center; width=1%;'></td>");
                        st.Append("<td class='bthl-title-rel2' style='background-color:#DEDEDE; text-align:center; width=49%; border-style: none; border-width: medium'><b>Văn bản thay thế</b></td>");
                        st.Append("</tr>");
                    } 
                    st.Append("<tr><td class='bthl-sms' style='text-align:left; vertical-align:top; width:50%;'>* (SMS " + ds.Tables[0].Rows[i]["DocIdentity"].ToString() + ") - <a href=\"" + m_MessageTemplates.SendFrom + ds.Tables[0].Rows[i]["DocUrl"].ToString() + "\" target=\"_blank\">" + ds.Tables[0].Rows[i]["DocName"].ToString() + "</a> (" + RelateTypes.Static_Get(byte.Parse(ds.Tables[0].Rows[i]["RelateTypeId"].ToString()), l_RelateTypes).RelateTypeDesc + " từ ngày " + (string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[i]["ExpireDate"]) == "" ? string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[i]["EffectDateRef"]) : string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[i]["ExpireDate"])) + ") ngày " + string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[i]["IssueDate"]) + "</td>");
                    if (ActionType=="Gen")
                    {
                        st.Append("<td class='bthl-width1' style='width:1%;'>&nbsp;</td>");
                    }
                    else
                    {
                        st.Append("<td style='vertical-align:top;width:1%;' class='bthl-width1-top'>&nbsp;<a class='bthl-title-bold' style=\"color:red; font-weight:bold;\" href=\"ShowGenBTHL.aspx?ActionType=Del&ArticleId=" + ArticleId.ToString() + "&DocRelateId=" + ds.Tables[0].Rows[i]["DocRelateId"].ToString() + "\">Xóa</a>&nbsp;</td>");
                    }
                    st.Append("<td class='bthl-title-rel1' style='background-color:#F0F0F0; text-align:left; vertical-align:top; width=49%;'>* (SMS " + ds.Tables[0].Rows[i]["DocIdentityRef"].ToString() + ") - <a href=\"" + m_MessageTemplates.SendFrom + ds.Tables[0].Rows[i]["DocUrlRef"].ToString() + "\" target=\"_blank\">" + ds.Tables[0].Rows[i]["DocNameRef"].ToString() + "</a></td>");
                    st.Append("</tr>");
                    RelateTypeIdTemp = byte.Parse(ds.Tables[0].Rows[i]["RelateTypeId"].ToString());
                }
                else
                {
                    st.Append("<tr><td class='bthl-sms' style='text-align:left; vertical-align:top; width:50%;'>* (SMS " + ds.Tables[0].Rows[i]["DocIdentity"].ToString() + ") - <a href=\"" + m_MessageTemplates.SendFrom + ds.Tables[0].Rows[i]["DocUrl"].ToString() + "\" target=\"_blank\">" + ds.Tables[0].Rows[i]["DocName"].ToString() + "</a> (" + RelateTypes.Static_Get(byte.Parse(ds.Tables[0].Rows[i]["RelateTypeId"].ToString()), l_RelateTypes).RelateTypeDesc + " từ ngày " + (string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[i]["ExpireDate"]) == "" ? string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[i]["EffectDateRef"]) : string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[i]["ExpireDate"])) + ") ngày " + string.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[i]["IssueDate"]) + "</td>");
                    if (ActionType == "Gen")
                    {
                        st.Append("<td class='bthl-width1' style='width:1%;'>&nbsp;</td>");
                    }
                    else
                    {
                        st.Append("<td style='vertical-align:top;width:1%;' class='bthl-width1-top'>&nbsp;<a class='bthl-title-bold' style=\"color:red; font-weight:bold;\" href=\"ShowGenBTHL.aspx?ActionType=Del&ArticleId=" + ArticleId.ToString() + "&DocRelateId=" + ds.Tables[0].Rows[i]["DocRelateId"].ToString() + "\">Xóa</a>&nbsp;</td>");
                    }
                    st.Append("<td style='background-color:#F0F0F0; text-align:left; vertical-align:top; width=49%;' class='bthl-title-rel1'>* (SMS " + ds.Tables[0].Rows[i]["DocIdentityRef"].ToString() + ") - <a href=\"" + m_MessageTemplates.SendFrom + ds.Tables[0].Rows[i]["DocUrlRef"].ToString() + "\" target=\"_blank\">" + ds.Tables[0].Rows[i]["DocNameRef"].ToString() + "</a></td>");
                    st.Append("</tr>");
                }
            }
            st.Append("</table>");       
            return st.ToString();
        }
        return RetVal;
    }


    protected void btnAddBTHL_Click(object sender, EventArgs e)
    {
        try
        {
            ActionType = "Gen";
            BindData();
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            ListFields.RenderControl(hw);
            var html1 = sb.ToString();

            sb = new StringBuilder();
            tw = new StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            m_grid.RenderControl(hw);
            var html = sb.ToString();
            //Response.Write(html1 + html);

            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int m_WeekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            MessageTemplates m_MessageTemplates = new MessageTemplates();
            m_MessageTemplates = m_MessageTemplates.GetByMessageTemplateName("Ban tin hieu luc");
            if (m_MessageTemplates.MessageTemplateId > 0) Content = m_MessageTemplates.MsgContent;
            else Content = "#Content#";
            Content = Content.Replace("#Content#", html1 + "<br />" + html);
            Content = Content.Replace("#Title#", "Số 24.2014 (506) ngày " + DateTime.Now.ToString("dd/MM/yyyy"));
            short SysMessageId = 0;
            Faqs m_Faqs = new Faqs();
            m_Faqs.FaqId = 0;
            m_Faqs.Question = "Số " + m_WeekNum.ToString() + "." + DateTime.Now.Year.ToString() + " (506) ngày " + DateTime.Now.ToString("dd/MM/yyyy");
            m_Faqs.Answer = Content;
            m_Faqs.FaqCode = "";
            m_Faqs.LanguageId = LanguageId;
            m_Faqs.FaqTypeId = 1;
            m_Faqs.FaqGroupId = 2;//BTHL
            m_Faqs.FieldId = 0;
            m_Faqs.DataSourceId = 0;
            m_Faqs.ReviewStatusId = 1;
            if (ActionType == "Edit")
            {
                m_Faqs.FaqId = ArticleId;
                m_Faqs = m_Faqs.Get();
            }
            //m_Faqs.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            Articles m_Articles = new Articles();
            if (ArticleId > 0) m_Articles = m_Articles.Get(ArticleId, LanguageId, 1);
            m_Articles.Title = "Số " + m_WeekNum.ToString() + "." + DateTime.Now.Year.ToString() + " (506) ngày " + DateTime.Now.ToString("dd/MM/yyyy");
            m_Articles.Summary = "";
            m_Articles.ImagePath = "";
            m_Articles.SourceUrl = "";
            m_Articles.SiteId = 1;
            m_Articles.ShowWeb = 1;
            m_Articles.ReviewStatusId = 1;
            m_Articles.ApplicationTypeId = 1;
            m_Articles.DataTypeId = 1;
            m_Articles.ArticleTypeId = 11;
            m_Articles.CategoryId = 221;
            m_Articles.ArticleContent = Content;
            m_Articles.InsertOrUpdate(1, ActUserId, ref SysMessageId);

            //Insert main category and parent
            ArticleCategories m_ArticleCategories = new ArticleCategories();
            m_ArticleCategories.ArticleId = m_Articles.ArticleId;
            m_ArticleCategories.CategoryId = m_Articles.CategoryId;
            m_ArticleCategories.InsertMainCate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

            DocRelates m_DocRelates = new DocRelates();
            m_DocRelates.DocRelateArticles_UpdateArticleId_Quick(ActUserId, m_Articles.ArticleId);

            JSAlertHelpers.Alert("Tạo bản tin thành công. Vào chức năng \"Bản tin hiệu lực\" để xem và chỉnh sửa.", this);
            Response.Redirect("ShowGenBTHL.aspx?ActionType=Edit&ArticleId=" + m_Articles.ArticleId.ToString() + "&Msg=Tạo bản tin thành công. Vào chức năng \"Bản tin hiệu lực\" để xem và chỉnh sửa.");
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void btnExportFileWord_Click(object sender, EventArgs e)
    {
        try
        {
            ActionType = "Gen";
            BindData();
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            ListFields.RenderControl(hw);
            var html1 = sb.ToString();

            sb = new StringBuilder();
            tw = new StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            m_grid.RenderControl(hw);
            var html = sb.ToString();
            //Response.Write(html1 + html);
            MessageTemplates m_MessageTemplates = new MessageTemplates();
            m_MessageTemplates = m_MessageTemplates.Get(8);
            Content = m_MessageTemplates.MsgContent;
            Content = Content.Replace("#Content#", html1 + "<br />" + html);
            Content = Content.Replace("#Title#", "Số 24.2014 (506) ngày " + DateTime.Now.ToString("dd/MM/yyyy"));

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=BTHL_ngay" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + ".doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            hw = new HtmlTextWriter(sw);
            m_grid.AllowPaging = false;
            m_grid.DataBind();
            m_grid.RenderControl(hw);
            Response.Output.Write((System.Web.HttpUtility.UrlDecode(Content)).ToString());
            Response.Flush();
            Response.End();
         }
         catch (Exception ex)
         {

             sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
             JSAlertHelpers.Alert(ex.Message.ToString(), this);
         }
    }
    protected void btnExportFilePDF_Click(object sender, EventArgs e)
    {
         try
        {
            ActionType = "Gen";
            BindData();
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            ListFields.RenderControl(hw);
            var html1 = sb.ToString();

            sb = new StringBuilder();
            tw = new StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            m_grid.RenderControl(hw);
            var html = sb.ToString();
            //Response.Write(html1 + html);
            MessageTemplates m_MessageTemplates = new MessageTemplates();
            m_MessageTemplates = m_MessageTemplates.Get(8);
            Content = m_MessageTemplates.MsgContent;
            Content = Content.Replace("#Content#", html1 + "<br />" + html);
            Content = Content.Replace("#Title#", "Số 24.2014 (506) ngày " + DateTime.Now.ToString("dd/MM/yyyy"));


            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=BTHL_ngay" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Content.ToString());
            Response.Flush();
            Response.End();
         }
         catch (Exception ex)
         {

             sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
             JSAlertHelpers.Alert(ex.Message.ToString(), this);
         }
    }
}

