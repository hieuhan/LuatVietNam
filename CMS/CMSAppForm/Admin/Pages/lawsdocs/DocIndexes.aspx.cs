using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using ICSoft.LawDocsLib;
using sms.admin.security;
using System.Text.RegularExpressions;
using System.Configuration;
using sms.common;
using HtmlAgilityPack;

public partial class Admin_DocIndexes : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int DocId = 0;
    protected int DocIndexId = 0;
    protected byte LanguageId = 0;
    protected List<Users> l_Users;
    private List<Languages> l_Language = new List<Languages>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
        {
            LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
        }
        if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
        {
            DocId = int.Parse(Request.Params["DocId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "", LanguageId.ToString());
            BindData();
        }
        if (!IsPostBack || CustomPaging.ChangePage)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            l_Language = Languages.Static_GetList();
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();
            DocIndexes mDocIndexes = new DocIndexes();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            string m_KeywordName = txtSearch.Text.Trim();
            mDocIndexes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            m_grid.DataSource = mDocIndexes.DocIndexes_GetList(ActUserId, DocId, m_OrderBy);
            m_grid.DataBind();
            RowCount = m_grid.Rows.Count;
           // m_grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblTong.Text = string.Format("{0:#,#}",RowCount);
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;

        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
        // doccontent
        try
        {
            if (DocId > 0)
            {
                int RowCount = 0;
                string m_OrderBy = ddlOrderBy.SelectedValue;
                Docs m_Docs = new Docs();
                DocIndexes mDocIndexes = new DocIndexes();
                mDocIndexes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                m_grid.DataSource = mDocIndexes.DocIndexes_GetList(ActUserId, DocId, m_OrderBy);
                RowCount = m_grid.Rows.Count;
                m_Docs = m_Docs.Get(DocId, byte.Parse(ddlLanguage.SelectedValue));
                if (m_Docs.DocId > 0)
                {
                    lblDocName.Text = String.Format("{0:}", m_Docs.DocName.ToString());
                    txtDocContent.Text = String.Format("{0:}", m_Docs.DocContent.ToString());
                    divDemo.InnerHtml = m_Docs.DocContent;
                    //if (m_Docs.ReviewStatusId == byte.Parse(ConfigurationManager.AppSettings["REVIEWSTATUSID"]) && m_Docs.DocContent != null && m_Docs.DocContent.ToString() != "" && RowCount>0)
                    //{

                    //    btnSave.Enabled = false;
                    //    btnSave.ToolTip = "Không thể sửa mục lục văn bản khi đã duyệt ";
                    //    JSAlertHelpers.ShowNotify("15", "warning", "Không thể sửa mục lục văn bản khi đã duyệt.", this);
                    //}
                    //else
                    //{
                    //    btnSave.Enabled = true;
                    //    //btnSave.ToolTip = "Lưu và tiếp tục sửa mục lục cho văn bản hiện tại";
                    //}
                }
            }

        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DocIndexes m_DataItem = (DocIndexes)e.Row.DataItem;
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }

            DocIndexId = m_DataItem.DocIndexId;
            Repeater rptLanguage = (Repeater)e.Row.FindControl("rptLanguage");
            if (rptLanguage != null)
            {
                rptLanguage.DataSource = l_Language; // Languages.Static_GetList();
                rptLanguage.DataBind();
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            DocIndexes mDocIndexes = new DocIndexes();
            mDocIndexes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            mDocIndexes.DocIndexId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = mDocIndexes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            DocIndexes mDocIndexes = new DocIndexes();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        mDocIndexes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
                        mDocIndexes.DocIndexId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = mDocIndexes.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }

    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveDocIndex();
            short SysMessageId = 0;
            byte SysMessageTypeId = 0;
            string messages = string.Empty;
            Docs m_Docs = new Docs();
            m_Docs.DocId = DocId;
            m_Docs.DocContent = txtDocContent.Text.ToString();
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            m_Docs.LanguageId = LanguageId;
            SysMessageTypeId = m_Docs.UpdateContent(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            switch (SysMessageTypeId)
            {
                case 1:
                    messages = string.Format("Cập nhật mục lục cho văn bản không thành công : {0}", SysMessages.Static_GetDesc(SysMessageId));
                    break;
                case 0:
                case 2:
                    messages = "Cập nhật mục lục cho văn bản thành công";
                    break;
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
            BindData();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private void SaveDocIndex()
    {
        short SysMessageId = 0;
        List<HtmlNode> l_HtmlNode;
        List<string> l_DocIndex = getIndexList2(out l_HtmlNode);
        List<string> l_DocIndexNew = new List<string>();
        DocIndexes m_DocIndexes = new DocIndexes();
        m_DocIndexes.DocId = DocId;
        m_DocIndexes.LanguageId = byte.Parse(ddlLanguage.SelectedValue);
        m_DocIndexes.Delete_ByDocId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        string DocContent = clearNewLine(txtDocContent.Text);
        string DocIndex = "";
        string DocIndexId = "";
        int countH4 = 0, countH5=0;
        byte levelId = 0;
        lblMsg.Text = "";
        for (int index = 0; index < l_DocIndex.Count; index++)
        {
            DocIndex = l_DocIndex[index];
            string RegexDocIndexRuleReplace = @"id=(.+)demuc([0-9]+)";
            RegexOptions m_RegexOptions = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            string RegexDocIndexRule = @"<(span|h[1-5]) class=(.*?)demuc([0-9]+)([^>]+)>(.+)</(span|h[1-5])>";
            DocIndexId = "id=\"demuc" + DocId + index;
            string DocIndexNew = Regex.Replace(DocIndex, RegexDocIndexRuleReplace, DocIndexId);
            Match m_Match = Regex.Match(DocIndex, RegexDocIndexRule, m_RegexOptions);
            if (m_Match.Success)
            {
                levelId = byte.Parse(m_Match.Groups[3].Value.Trim());
                if (levelId == 4) countH4++;
                else if (levelId == 5) countH5++;
                if (!DocIndexNew.Contains(" id = "))
                {
                    DocIndexNew = DocIndex.Replace("demuc" + levelId + "\"", "demuc" + levelId + "\" " + DocIndexId + "\""); //DocIndexNew = Regex.Replace(DocIndex, RegexDocIndexRuleReplace, DocIndexId);
                    Match m = Regex.Match(DocIndexNew, "(<span.+?</span>)", m_RegexOptions);
                    if (m.Success)
                    {
                        if (levelId <= 3 || levelId == 4 && countH4 <= 5 || levelId == 5 && countH5 <= 5)
                        {
                            DocIndexNew = ReplaceFirstTag(DocIndexNew, "<span", string.Concat("<h", levelId));
                            DocIndexNew = ReplaceLastTag(DocIndexNew, "</span>", string.Concat("</h", levelId, ">"));
                        }
                    }
                    else
                    {
                        if (levelId == 4 && countH4 > 5 || levelId == 5 && countH5 > 5)
                        {
                            DocIndexNew = ReplaceFirstTag(DocIndexNew, string.Concat("<h", levelId), "<span");
                            DocIndexNew = ReplaceLastTag(DocIndexNew, string.Concat("</h", levelId, ">"),"</span>");
                        }
                    }
                }
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + m_Match.Groups[3].Value);
                // lblMsg.Text += m_Match.Groups[2].Value + ":" + m_Match.Groups[4].Value + "<br />";
                m_DocIndexes = new DocIndexes
                {
                    DocId = DocId,
                    Bookmark = "demuc" + DocId + index,
                    DisplayOrder = (short) index,
                    LanguageId = LanguageId,
                    LevelId = levelId,
                    Title = System.Net.WebUtility.HtmlDecode(l_HtmlNode[index].InnerText)
                };
                // m_Match.Groups[4].Value;
                if (!string.IsNullOrEmpty(System.Net.WebUtility.HtmlDecode(l_HtmlNode[index].InnerText).Trim()) || System.Net.WebUtility.HtmlDecode(l_HtmlNode[index].InnerText).Trim().Length>0)
                { 
                    m_DocIndexes.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                } 
            }
            
            l_DocIndexNew.Add(DocIndexNew);
        }
        for (int index = 0; index < l_DocIndex.Count; index++)
        {
            DocContent = DocContent.Replace(l_DocIndex[index], l_DocIndexNew[index]);
        }
        
        txtDocContent.Text = DocContent;
    }
    
    private List<string> getIndexList()
    {
        List<string> RetVal = new List<string>();
        string DocContent = clearNewLine(txtDocContent.Text);
        int index = DocContent.IndexOf("<span class=\"demuc");
        while(index >= 0 & index < DocContent.Length)
        {
            int endItemIndex = DocContent.IndexOf("</span>", index);
            if(endItemIndex >= 0)
            {
                RetVal.Add(DocContent.Substring(index, endItemIndex - index + 7));
            }
            index = DocContent.IndexOf("<span class=\"demuc", index + 10);
        }
        return RetVal;
    }
    private List<string> getIndexList2(out List<HtmlNode> listHtmlNode)
    {
        List<string> retVal = new List<string>();
        string docContent = clearNewLine(txtDocContent.Text);
        HtmlDocument mHtmlDocument = new HtmlDocument();
        mHtmlDocument.LoadHtml(docContent);
        var xpath = "//*[self::span or self::h1 or self::h2 or self::h3 or self::h4 or self::h5]";
        HtmlNodeCollection listNodes = mHtmlDocument.DocumentNode.SelectNodes(xpath);
        listHtmlNode = new List<HtmlNode>();
        if (listNodes != null)
        {
            foreach (HtmlNode noteHeading in listNodes)
            {
                if (noteHeading.Attributes["class"] != null && noteHeading.Attributes["class"].Value.Contains("demuc"))
                {
                    retVal.Add(noteHeading.OuterHtml);
                    listHtmlNode.Add(noteHeading);
                }
            }
        }            
        return retVal;
    }
    private string clearNewLine(string InputStr)
    {
        string RetVal = InputStr;
        //RetVal = RetVal.Replace(Environment.NewLine + "<br />","<br />");
        //RetVal = RetVal.Replace("<br />" + Environment.NewLine, "<br />");
        //RetVal = RetVal.Replace(Environment.NewLine + "<br>", "<br />");
        //RetVal = RetVal.Replace("<br>" + Environment.NewLine, "<br />");
        RetVal = RetVal.Trim();
        //while (RetVal.Contains("\n\n") || RetVal.Contains("\r\n"))
        //{
        //    RetVal = RetVal.Replace("\n\n", "\n");
        //    RetVal = RetVal.Replace("\r\n", "\n");
        //}
        RetVal = RetVal.Replace(Environment.NewLine, "");
        return RetVal;
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

    public static string ReplaceFirstTag(string source, string find, string replace)
    {
        int index = source.IndexOf(find, StringComparison.Ordinal);
        string result = source.Remove(index, find.Length).Insert(index, replace);
        return result;
    }

    public static string ReplaceLastTag(string source, string find, string replace)
    {
        int index = source.LastIndexOf(find, StringComparison.Ordinal);
        string result = source.Remove(index, find.Length).Insert(index, replace);
        return result;
    }
}

