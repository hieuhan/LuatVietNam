using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using System.Data;

public partial class Admin_ArticleDocUpdateGen : System.Web.UI.Page
{
    protected int ArticleId = 0;
    private int ActUserId = 0;
    protected byte LanguageId = 0;
    protected string GenType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["GenType"] != null && Request.Params["GenType"].ToString() != "")
        {
            GenType = Request.Params["GenType"].ToString();
        }
        if (!IsPostBack)
        {
            if (GenType == "Hot") BindDataHot();
            else if (GenType == "Newest") BindDataNewest();
            else BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (ArticleId > 0)
            {
                //Ghep vao template (Bang MessageTemplates)
                MessageTemplates m_MessageTemplates = new MessageTemplates();
                m_MessageTemplates = m_MessageTemplates.GetByMessageTemplateName(LanguageId == 2 ? "Ban tin van ban cap nhat hang tuan - EN" : "Ban tin van ban cap nhat hang tuan");
                string m_Template = "#Content#";
                string m_Domain = "";
                if (m_MessageTemplates.MessageTemplateId > 0)
                {
                    m_Template = m_MessageTemplates.MsgContent;
                    m_Domain = m_MessageTemplates.SendFrom;
                }

                DocArticles m_DocArticles = new DocArticles();
                DataSet ds = m_DocArticles.GetDataGenByArticleId(ArticleId, LanguageId);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //D/s linh vuc
                //sb.Append(@"<link href=\'Styles/style.css' rel=\'stylesheet' type=\'text/css' />");
                sb.Append(@"<br/><br/><div class=""rows item7"">
                                <div class=""rows"">
                                    <div class=""tie-ms"" style=""color: #d81c22; font-size: 13px; text-align: center;"">");
                sb.Append(LanguageId == 2 ? "Update field list" : "Danh sách lĩnh vực cập nhật");
                sb.Append(@"</div>
                                </div>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i % 3 == 0)
                    {
                        if (i > 0)
                        {
                            sb.Append("</div>");
                        }
                        int m_EventOdd = i / 3;
                        if (i % 2 == 0)
                        {
                            sb.Append(@"<div class=""rows-ms"">");
                        }
                        else
                        {
                            sb.Append(@"<div class=""rows-ms bgf6f6f6"">");
                        }
                    }

                    sb.Append(@"<div class=""colx4"">");
                    sb.Append("<a href=\"#field" + ds.Tables[0].Rows[i]["FieldId"].ToString() + "\" class=\"iconluat colora67942\">");
                    sb.Append(ds.Tables[0].Rows[i]["FieldDesc"].ToString());
                    sb.Append(@"<span class=""colora67942""> (");
                    sb.Append(ds.Tables[0].Rows[i]["Soluong"].ToString());
                    sb.Append(@")</span> </a>
                                </div>");
                                    
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sb.Append("</div>");
                }
                sb.Append("</div>");
                //D/s UBND
                if (ds.Tables[1].Rows.Count > 0)
                {
                    sb.Append(@"<div class=""rows item7"">
                                <div class=""rows"">
                                    <div class=""tie-ms"" style=""color: #d81c22; font-size: 13px; text-align: center;"">Văn bản ủy ban nhân dân</div>
                                </div>");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (i % 3 == 0)
                        {
                            if (i > 0)
                            {
                                sb.Append("</div>");
                            }
                            int m_EventOdd = i / 3;
                            if (i % 2 == 0)
                            {
                                sb.Append(@"<div class=""rows-ms"">");
                            }
                            else
                            {
                                sb.Append(@"<div class=""rows-ms bgf6f6f6"">");
                            }
                        }

                        sb.Append(@"<div class=""colx4"">");
                        sb.Append("<a href=\"#organ" + ds.Tables[1].Rows[i]["OrganId"].ToString() + "\" class=\"iconluat colora67942\">");
                        sb.Append(ds.Tables[1].Rows[i]["OrganDesc"].ToString());
                        sb.Append(@"<span class=""colora67942""> (");
                        sb.Append(ds.Tables[1].Rows[i]["Soluong"].ToString());
                        sb.Append(@")</span> </a>
                                </div>");

                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        sb.Append("</div>");
                    }
                    sb.Append("</div>");
                }
                //Ds VB theo linh vuc
                sb.Append(@"<div class=""box-content boder"" style=""padding: 20px;""> 
                                <div class=""box-content"">");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sb.Append(@"<div class=""rows item7"">
                                <div class=""rows"">
                                    <div class=""tie-ms"" style=""text-transform: none;"">");
                    sb.Append("<a name=\"field" + ds.Tables[0].Rows[i]["FieldId"].ToString() + "\"></a>");
                    sb.Append(ds.Tables[0].Rows[i]["FieldDesc"].ToString());
                    sb.Append(@"</div></div>");
                    DataRow[] rows = ds.Tables[2].Select("FieldId=" + ds.Tables[0].Rows[i]["FieldId"].ToString());
                    for (int j = 0; j < rows.Length; j++)
                    {
                        if (j % 2 == 0)
                        {
                            sb.Append(@"<div class=""rows-ms"">");
                        }
                        else
                        {
                            sb.Append(@"<div class=""rows-ms bgf6f6f6"">");
                        }
                        sb.Append(@"<div class=""tie-en-left vb2"">");
                        sb.Append(rows[j]["DocIdentity"].ToString());
                        sb.Append(@"</div>
                                    <div class=""tie-en-right vb2"">
                                    <a target=""_blank"" href=""");
                        sb.Append(m_Domain + rows[j]["DocUrl"].ToString());
                        if (LanguageId == 2)
                        {
                            sb.Append(@""">Date ");
                        }
                        else
                        {
                            sb.Append(@""">Ngày ");
                        }
                        if (rows[j]["IssueDate"] != DBNull.Value) sb.Append(Convert.ToDateTime(rows[j]["IssueDate"]).ToString("dd/MM/yyyy") + ", ");
                        sb.Append(rows[j]["DocName"].ToString());
                        sb.Append(@"</a></div>
                            </div>");
                    }
                    sb.Append(@"</div>");
                }
                //Ds VB theo UBND
                if (ds.Tables[1].Rows.Count > 0)
                {
                    sb.Append(@"<div class=""rows item6"" style=""text-align: center; font-weight: bold; text-transform: uppercase; font-size: 16px;""> VĂN BẢN CỦA ỦY BAN NHÂN DÂN </div>");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        sb.Append(@"<div class=""rows item7"">
                                <div class=""rows"">
                                    <div class=""tie-ms"" style=""text-transform: none;"">");
                        sb.Append("<a name=\"organ"+ds.Tables[1].Rows[i]["OrganId"].ToString()+"\"></a>");
                        sb.Append(ds.Tables[1].Rows[i]["OrganDesc"].ToString());
                        sb.Append(@"</div></div>");
                        DataRow[] rows = ds.Tables[3].Select("OrganId=" + ds.Tables[1].Rows[i]["OrganId"].ToString());
                        for (int j = 0; j < rows.Length; j++)
                        {
                            if (j % 2 == 0)
                            {
                                sb.Append(@"<div class=""rows-ms"">");
                            }
                            else
                            {
                                sb.Append(@"<div class=""rows-ms bgf6f6f6"">");
                            }
                            sb.Append(@"<div class=""tie-en-left vb2"">");
                            sb.Append(rows[j]["DocIdentity"].ToString());
                            sb.Append(@"</div>
                                    <div class=""tie-en-right vb2"">
                                    <a target=""_blank"" href=""");
                            sb.Append(m_Domain + rows[j]["DocUrl"].ToString());
                            sb.Append(@""">Ngày ");
                            if (rows[j]["IssueDate"] != DBNull.Value) sb.Append(Convert.ToDateTime(rows[j]["IssueDate"]).ToString("dd/MM/yyyy") + ", ");
                            sb.Append(rows[j]["DocName"].ToString());
                            sb.Append(@"</a></div>
                            </div>");
                        }
                        sb.Append(@"</div>");
                    }
                }
                sb.Append(@"
                                </div>
                            </div>");

                lblGen.Text = m_Template.Replace("#Content#", sb.ToString());
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    private void BindDataHot()
    {
        try
        {
            if (ArticleId > 0)
            {
                //Ghep vao template (Bang MessageTemplates)
                MessageTemplates m_MessageTemplates = new MessageTemplates();
                m_MessageTemplates = m_MessageTemplates.GetByMessageTemplateName(LanguageId == 2 ? "Ban tin van ban cap nhat hang tuan - EN" : "Ban tin van ban cap nhat hang tuan");
                string m_Template = "#Content#";
                string m_Domain = "";
                if (m_MessageTemplates.MessageTemplateId > 0)
                {
                    m_Template = m_MessageTemplates.MsgContent;
                    m_Domain = m_MessageTemplates.SendFrom;
                }
                DocArticles m_DocArticles = new DocArticles();
                DataSet ds = m_DocArticles.GetDataGenHotByArticleId(ArticleId, LanguageId);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //VB Theo loai
                //sb.Append(@"<link href=\'Styles/style.css' rel=\'stylesheet' type=\'text/css' />");
                if (LanguageId == 2)
                {
                    sb.Append(@"<br/><br/><div class=""box-content boder"">  
                        <div class=""cat-box-content"">                  
                        <div class=""rows item6"">
                            <div class=""stt1"" style=""width: 5%""> No.</div>
                            <div class=""stt1"" style=""width: 59%;"">Document</div>
                            <div class=""stt1"" style=""width: 15%;"">Document number</div>
                             <div class=""stt1"" style=""width: 20%"">Date</div>
                        </div>");
                }
                else {
                    sb.Append(@"<br/><br/><div class=""box-content boder"">  
                        <div class=""cat-box-content"">                  
                        <div class=""rows item6"">
                            <div class=""stt1"" style=""width: 5%""> STT</div>
                            <div class=""stt1"" style=""width: 59%;"">Tên văn bản</div>
                            <div class=""stt1"" style=""width: 15%;"">Ký hiệu</div>
                             <div class=""stt1"" style=""width: 20%"">Ngày</div>
                        </div>");
                }
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sb.Append(@"<div class=""rows "">
                                <div class=""rows"">
                                    <div class=""tie-ms""  style=""background: #d9d9d9""><span class=""left-x"">");
                    sb.Append(ds.Tables[0].Rows[i]["DocTypeDesc"].ToString());
                    sb.Append(@"</span></div>
                                </div>
                        </div>");
                    
                    sb.Append(@"<div class=""rows"">
                            <table class=""table table-bordered boder0"">
                            <tbody>");
                    DataRow[] rows = ds.Tables[1].Select("DocTypeId=" + ds.Tables[0].Rows[i]["DocTypeId"].ToString());
                    for (int j = 0; j < rows.Length; j++)
                    {
                        if (j % 2 == 0)
                        {
                            sb.Append(@"<tr>");
                        }
                        else
                        {
                            sb.Append(@"<tr class=""bgcolor"">");
                        }
                        sb.Append("<td><strong>" + (j + 1).ToString() + "</strong></td>");
                        sb.Append(@"<td style=""width:60%"">");
                        sb.Append("<a target=\"_blank\" href=\"" + m_Domain + rows[j]["DocURL"].ToString() + "\">");
                        sb.Append(rows[j]["DocName"].ToString());
                        sb.Append(@"</a>
                            </td> 
                            <td><strong style=""color: #a67942;font-size: 13px;"">");
                        sb.Append(rows[j]["DocIdentity"].ToString());
                        sb.Append(@"</strong></td> 
                            <td style=""white-space:nowrap;"">
                                <p style=""color: #333;font-size: 13px;"">");
                        sb.Append(LanguageId == 2 ? "Date issued: " : "Ban hành: ");
                        sb.Append(@"<font class=""color2"">");
                        if (rows[j]["IssueDate"] != DBNull.Value) sb.Append(Convert.ToDateTime(rows[j]["IssueDate"]).ToString("dd/MM/yyyy"));
                        sb.Append(@"</font></p>
                                <p style=""color: #333;font-size: 13px;""> ");
                        sb.Append(LanguageId == 2 ? "Apply: " : "Hiệu lực: ");
                        sb.Append(@"<font class=""color2"">");
                        if (rows[j]["EffectDate"] != DBNull.Value) sb.Append(Convert.ToDateTime(rows[j]["EffectDate"]).ToString("dd/MM/yyyy"));
                        sb.Append(@"</font> </p>
                            </td>
                        </tr>");
                    }
                    sb.Append(@"</tbody> 
                            </table>   
                        </div>");
                }
                sb.Append(@"</div>
                </div>");

                lblGen.Text = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    private void BindDataNewest()
    {
        try
        {
            if (ArticleId > 0)
            {
                //Ghep vao template (Bang MessageTemplates)
                MessageTemplates m_MessageTemplates = new MessageTemplates();
                m_MessageTemplates = m_MessageTemplates.GetByMessageTemplateName(LanguageId == 2 ? "Ban tin van ban moi - EN" : "Ban tin van ban moi");
                string m_Template = "#Content#";
                string m_Domain = "";
                if (m_MessageTemplates.MessageTemplateId > 0)
                {
                    m_Template = m_MessageTemplates.MsgContent;
                    m_Domain = m_MessageTemplates.SendFrom;
                }
                int m_Count = 0;
                DocArticles m_DocArticles = new DocArticles();
                DataSet ds = m_DocArticles.GetDataGenNewestByArticleId(ArticleId, LanguageId);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                //VB Theo linh vuc
                if (LanguageId == 2)
                {
                    sb.Append(@"<!--Bengin cat-box content-wrap-->
                            <div class=""cat-box content-wrap"">
                            <!--Bengin content-left -->
                                <div class=""content-left-btin"">
                                        <div class=""box-content"">
                                            <div class=""rows item6"">
                                                <div class=""tie-en-1 s123""> # </div>
                                                <div class=""tie-en-1"">NUMBER</div>
                                                <div class=""tie-en-2"">TITLE</div>
                                            </div>");
                }
                else
                {
                    sb.Append(@"<!--Bengin cat-box content-wrap-->
                            <div class=""cat-box content-wrap"">
                            <!--Bengin content-left -->
                                <div class=""content-left-btin"">
                                        <div class=""box-content"">
                                            <div class=""rows item6"">
                                                <div class=""tie-en-1 s123""> # </div>
                                                <div class=""tie-en-1"">KÝ HIỆU</div>
                                                <div class=""tie-en-2"">vĂN BẢN</div>
                                            </div>");
                }
                
                sb.Append(@"<div class=""rows item7"">");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sb.Append(@"<div class=""rows"">
                                <div class=""tie-ms"">");
                    sb.Append(ds.Tables[0].Rows[i]["FieldDesc"].ToString());
                    sb.Append(@"</div>
                            </div>");
                    DataRow[] rows = ds.Tables[1].Select("FieldId=" + ds.Tables[0].Rows[i]["FieldId"].ToString());
                    for (int j = 0; j < rows.Length; j++)
                    {
                        m_Count++;
                        if (j % 2 == 0)
                        {
                            sb.Append(@"<div class=""rows-ms"">");
                        }
                        else
                        {
                            sb.Append(@"<div class=""rows-ms bgf6f6f6"">");
                        }
                        sb.Append(@"<div class=""count123"">");
                        sb.Append(m_Count.ToString());
                        sb.Append(@"</div>
                                <div class=""tie-en-left"">");
                        sb.Append(rows[j]["DocIdentity"].ToString());
                        sb.Append(@"</div>
                                <div class=""tie-en-right"">");
                        sb.Append(rows[j]["DocName"].ToString());
                        sb.Append(@"</div></div>");
                    }
                }
                sb.Append(@"</div></div>");
                sb.Append(@"</div>
                            <!--End content left-->
                            <!--Bengin content-right-->
                                 <div class=""content-right-btin"">
                                    <div class=""tie-right-en left""><b>");
                sb.Append(LanguageId == 2 ? "In This Updates" : "Trong số này:");
                sb.Append(@"</b></div>
                                    <div class=""box-right-en"">");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sb.Append(@"<div class=""rows item7"">
                        <div class=""tie-ms"" style=""background: #e4c484; color: #c92025; font-style: italic;"">");
                    sb.Append(ds.Tables[0].Rows[i]["FieldDesc"].ToString());
                    sb.Append(@"</div>");
                    DataRow[] rows = ds.Tables[1].Select("FieldId=" + ds.Tables[0].Rows[i]["FieldId"].ToString());
                    for (int j = 0; j < rows.Length; j++)
                    {
                        sb.Append(@"<div class=""rows-ms2"">
                            <div class=""rows-col1""> <a href=""#doc");
                        sb.Append(rows[j]["DocId"].ToString());
                        sb.Append(@""" class=""tie-post-en""><img class=""iproced"" src=""http://cms.luatvietnam.vn/images/iproced.png""/>");
                        if (rows[j]["Result"].ToString() != "")
                        {
                            sb.Append(rows[j]["Result"].ToString());
                        }
                        else
                        {
                            sb.Append(rows[j]["DocName"].ToString());
                        }
                        sb.Append(@"</a></div>
                            <div class=""rows-col2""><span class=""page-en"">");

                        sb.Append(LanguageId == 2 ? "Page xx" : "Trang xx");
                        sb.Append(@"</span></div>
                        </div>");
                    }
                    sb.Append(@"</div>");
                }
                sb.Append(@"</div></div>
                            <!--End content-right-->
                        </div>
                <!--End cat-box content-wrap-->");
                //Tom tat van ban
                sb.Append(@"<div class=""rows item6"" style=""margin-top: 10px;"">
                            <span style=""font-size: 18px; text-transform: uppercase; font-weight: bold"">");
                sb.Append(LanguageId == 2 ? "SUMMARY:" : "TÓM TẮT VĂN BẢN:");
                sb.Append(@"</span>
                        </div>");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sb.Append(@"<div class=""box-content"">
                        <div class=""rows item7"">
                            <div class=""rows"">
                                    <div class=""tie-ms"">");
                    sb.Append(ds.Tables[0].Rows[i]["FieldDesc"].ToString());
                    sb.Append(@"</div>
                            </div>");
                    DataRow[] rows = ds.Tables[1].Select("FieldId=" + ds.Tables[0].Rows[i]["FieldId"].ToString());
                    for (int j = 0; j < rows.Length; j++)
                    {
                        sb.Append("<a name=\"doc"+rows[j]["DocId"].ToString()+"\"></a>");
                        sb.Append(@"<div class=""rows item7"">
                                <div class=""rows"" class=""rows-item10"" style=""text-align: center; padding: 16px 0; text-transform: uppercase; font-size: 16px; font-weight: bold;"">");
                        if (rows[j]["Result"].ToString() != "")
                        {
                            sb.Append(rows[j]["Result"].ToString());
                        }
                        else
                        {
                            sb.Append(rows[j]["DocName"].ToString());
                        }
                        sb.Append(@"</div>
                                <div class=""rows""><table style=""width:100%""><tr><td style=""width:49%"">
                                    <div class=""col-x21"">
                                        <div class=""entry-en"">");
                        if (rows[j]["DocSummary"].ToString() != "") sb.Append(rows[j]["DocSummary"].ToString());
                        else sb.Append("Chưa có tóm tắt nội dung");                
                        sb.Append(@"</div>
                                    </div></td><td style=""width:15px"">&nbsp;</td><td style=""vertical-align:top"">
                                    <div class=""col-x21"">
                                        <div class=""entry-en"">
                                        Copy dữ liệu chia cột vào đây.
                                        </div>
                                    </div></td></tr></table>
                                </div>
                        </div>");
                    }                     
                    sb.Append(@"</div>
                    </div>");
                }
                lblGen.Text = m_Template.Replace("#Content#", sb.ToString()).Replace("#Content2#", sb2.ToString());
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    private void BindDataNewest1()
    {
        try
        {
            if (ArticleId > 0)
            {
                //Ghep vao template (Bang MessageTemplates)
                MessageTemplates m_MessageTemplates = new MessageTemplates();
                m_MessageTemplates = m_MessageTemplates.GetByMessageTemplateName("Ban tin van ban moi");
                string m_Template = "#Content#";
                string m_Domain = "";
                if (m_MessageTemplates.MessageTemplateId > 0)
                {
                    m_Template = m_MessageTemplates.MsgContent;
                    m_Domain = m_MessageTemplates.SendFrom;
                }
                int m_Count = 0;
                DocArticles m_DocArticles = new DocArticles();
                DataSet ds = m_DocArticles.GetDataGenNewestByArticleId(ArticleId, LanguageId);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                //VB Theo linh vuc
                sb.Append(@"<div align=""center"">
                            <table class=""MsoNormalTable"" border=""1"" cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""width: 100%; border-collapse: collapse; border: medium none"">
                                <tbody>
                                    <tr style=""height: 15.95pt"">
                                        <td width=""29"" valign=""top"" style=""width: 21.95pt; height: 15.95pt; border-left: medium none; border-right: medium none; border-top: 1.0pt solid black; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #F2F2F2"">
                                        <p class=""MsoNormal"" align=""center"" style=""margin-top:6.0pt;margin-right:0in;&#10;  margin-bottom:0in;margin-left:0in;margin-bottom:.0001pt;text-align:center;&#10;  line-height:normal""><b><i> 				<span style=""font-size:9.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:gray""> 				#</span></i></b></p>
                                        </td>
                                        <td width=""109"" valign=""top"" style=""width: 81.4pt; height: 15.95pt; border-left: medium none; border-right: medium none; border-top: 1.0pt solid black; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #F2F2F2"">
                                        <p class=""MsoNormal"" align=""center"" style=""margin-top:6.0pt;margin-right:0in;&#10;  margin-bottom:0in;margin-left:0in;margin-bottom:.0001pt;text-align:center;&#10;  line-height:normal""><b> 				<span style=""font-size:9.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;&#10;  color:gray"">K&Yacute; HIỆU </span></b></p>
                                        </td>
                                        <td width=""306"" valign=""top"" style=""width: 229.5pt; height: 15.95pt; border-left: medium none; border-right: medium none; border-top: 1.0pt solid black; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #F2F2F2"">
                                        <p class=""MsoNormal"" align=""center"" style=""margin-top:6.0pt;margin-right:0in;&#10;  margin-bottom:0in;margin-left:0in;margin-bottom:.0001pt;text-align:center;&#10;  line-height:normal""><b> 				<span style=""font-size:9.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;&#10;  color:gray"">VĂN BẢN</span></b></p>
                                        </td>
                                        <td width=""17"" valign=""top"" style=""width: 12.45pt; height: 15.95pt; border: medium none; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: white"">
                                        <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;line-height:normal""><b> 				<span style=""font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#996600""> 				&nbsp;</span></b></p>
                                        </td>
                                        <td width=""325"" colspan=""2"" style=""width: 244.05pt; height: 15.95pt; border: medium none; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #ECD6AA"">
                                        <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;line-height:normal""><b><i> 				<span style=""font-size: 13.0pt; font-family: Arial,sans-serif; color: maroon; text-decoration:underline""> 				Trong số n&agrave;y:</span></i></b></p>
                                        </td>
                                    </tr>");


                string m_FieldName = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (m_FieldName != ds.Tables[0].Rows[i]["FieldName"].ToString())
                    {
                        //m_Count = 0;
                        if (m_FieldName != "")
                        {
                            sb2.Append("</tbody></table>");
                        }

                        m_FieldName = ds.Tables[0].Rows[i]["FieldName"].ToString();
                        sb.Append(@"<tr style=""height: .05in"">
                                        <td width=""444"" colspan=""3"" valign=""top"" style=""width: 332.85pt; height: .05in; border-left: medium none; border-right: medium none; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #ECD6AA"">
                                        <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;line-height:normal""><b> 				<span style=""font-size: 9.0pt; font-family: Arial,sans-serif; color: gray""> 				");
                        sb.Append(m_FieldName.ToUpper());
                        sb.Append(@"</span></b></p>
                                        </td>
                                        <td width=""17"" valign=""top"" style=""width: 12.45pt; height: .05in; border: medium none; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: white"">
                                        <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;line-height:normal""><i> 				<span style=""font-size:10.0pt;font-family:&#10;  &quot;Arial&quot;,&quot;sans-serif&quot;;color:black"">&nbsp;</span></i></p>
                                        </td>
                                        <td width=""325"" colspan=""2"" valign=""top"" style=""width: 244.05pt; height: .05in; border: medium none; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #ECD6AA"">
                                        <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;line-height:normal""><b><i> 				<span style=""font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:red; text-decoration:underline""> 				");
                        sb.Append(m_FieldName.ToUpper());
                        sb.Append(@"</span></i></b></p>
                                        </td>
                                    </tr>");

                        sb2.Append(@"<table border=""1"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: collapse; border-width: 0"" bordercolor=""#111111"" width=""100%"" id=""table14"" height=""135"">
                                <tbody>");
                        sb2.Append(@"<tr>
                                        <td style=""border: medium none"" width=""100%"" colspan=""3"" height=""30"" bgcolor=""#C0C0C0""><span style=""color: #6C0000""><font size=""4""><font face=""Wingdings"">&Uuml;</font><i><b><span style=""font-family: Arial,sans-serif"">       <u>");
                        sb2.Append(m_FieldName.ToUpper() + ":");
                        sb2.Append(@"</u></span></b></i></font></span></td>
                                    </tr>");
                    }
                    m_Count = m_Count + 1;
                    sb.Append(@"<tr>
                                    <td width=""29"" valign=""top"" style=""width: 21.95pt; border-left: medium none; border-right: medium none; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #F2F2F2"">
                                    <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;line-height:normal""><b> 				<span style=""font-size:9.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:gray""> 				");
                    sb.Append(m_Count.ToString());
                    sb.Append(@"</span></b></p>
                                    </td>
                                    <td width=""109"" valign=""top"" style=""width: 81.4pt; border-left: medium none; border-right: medium none; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #F2F2F2"">
                                    <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;text-align:justify;line-height:&#10;  normal""><span style=""font-size: 9.0pt; font-family: Arial,sans-serif; color: gray""> 				<a style=""color: blue; text-decoration: underline; text-underline: single"" target=""_blank"" href=""");
                    sb.Append(m_Domain + ds.Tables[0].Rows[i]["DocUrl"].ToString());
                    sb.Append(@"""> 				");
                    sb.Append(ds.Tables[0].Rows[i]["DocIdentity"].ToString());
                    sb.Append(@"</a></span></p>
                                    </td>
                                    <td width=""306"" valign=""top"" style=""width: 229.5pt; border-left: medium none; border-right: medium none; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #F2F2F2"">
                                    <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;text-align:justify;line-height:&#10;  normal""><span style=""font-size:9.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;&#10;  color:gray"">");
                    sb.Append(ds.Tables[0].Rows[i]["DocName"].ToString());
                    sb.Append(@"</span></p>
                                    </td>
                                    <td width=""17"" style=""width: 12.45pt; border: medium none; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: white"">
                                    <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;line-height:normal""><b> 				<span style=""font-size:10.0pt;font-family:&#10;  &quot;Arial&quot;,&quot;sans-serif&quot;;color:#996600"">&nbsp;</span></b></p>
                                    </td>
                                    <td width=""241"" valign=""top"" style=""width: 181.05pt; border: medium none; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #ECD6AA"">
                                    <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;text-align:justify;line-height:&#10;  normal""><b><i><span style=""font-size:10.0pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;&#10;  color:maroon"">* ");
                    if (ds.Tables[0].Rows[i]["Result"].ToString() != "")
                    {
                        sb.Append(ds.Tables[0].Rows[i]["Result"].ToString());
                    }
                    else
                    {
                        sb.Append(ds.Tables[0].Rows[i]["DocName"].ToString());
                    }
                    sb.Append(@"</span> 				</i></b></p>
                                    </td>
                                    <td width=""84"" valign=""top"" style=""width: 63.0pt; border: medium none; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in; background: #ECD6AA"">
                                    <p class=""MsoNormal"" style=""margin-top:6.0pt;margin-right:0in;margin-bottom:&#10;  0in;margin-left:0in;margin-bottom:.0001pt;line-height:normal""><i> 				<span style=""font-size: 8.0pt; font-family: Arial,sans-serif; color: #6C0000""> 				Trang xx</span></i></p>
                                    </td>
                                </tr>");

                    sb2.Append(@"<tr>
                                    <td style=""border: medium none"" width=""100%"" colspan=""3"" height=""39"">
                                    <p class=""MsoNormal"" align=""center"" style=""margin-top:10.0pt;margin-right:0in;&#10;margin-bottom:0in;margin-left:0in;margin-bottom:.0001pt;text-align:center;&#10;line-height:normal""><b> 	<span style=""font-family: Arial,sans-serif; color: #CC9900""><font size=""4""> 	");
                    if (ds.Tables[0].Rows[i]["Result"].ToString() != "")
                    {
                        sb2.Append(ds.Tables[0].Rows[i]["Result"].ToString());
                    }
                    else
                    {
                        sb2.Append(ds.Tables[0].Rows[i]["DocName"].ToString());
                    }
                    sb2.Append(@"</font></span></b></p>
                                    </td>
                                </tr>");
                    sb2.Append(@"<tr>
                                    <td width=""49%"" style=""border-style: none; border-width: medium"" align=""justify"">");
                    if (ds.Tables[0].Rows[i]["DocSummary"].ToString() != "") sb2.Append(ds.Tables[0].Rows[i]["DocSummary"].ToString());
                    else sb2.Append("Chưa có tóm tắt nội dung");
                    sb2.Append(@"</td>
                                    <td width=""3%"" style=""border-style: none; border-width: medium"" align=""justify"">&nbsp;</td>
                                    <td width=""48%"" style=""border-style: none; border-width: medium"" align=""justify"">
                                    &nbsp;</td>
                                </tr>");
                }
                sb.Append("</tbody></table></div>");
                sb2.Append("</tbody></table>");
                lblGen.Text = m_Template.Replace("#Content#", sb.ToString()).Replace("#Content2#", sb2.ToString());
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            short SysMessageId = 0;
            if (ArticleId > 0)
            {
                Articles m_Articles = new Articles();
                m_Articles = m_Articles.Get(ArticleId, 1, 1);
                m_Articles.ArticleContent = lblGen.Text;
                m_Articles.Update(1, ActUserId, ref SysMessageId);
            }
            lblMsg.Text = "Tạo bản tin thành công.";
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}