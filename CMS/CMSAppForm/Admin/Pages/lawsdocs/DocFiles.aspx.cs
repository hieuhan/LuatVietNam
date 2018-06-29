using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.common;
using sms.admin.security;

public partial class Admin_Pages_lawsdocs_DocFiles : System.Web.UI.Page
{
    private byte LanguageId = 0;
    private int DocId = 0;
    private int ActUserId = 0;
    protected int WordFileId = 0, PdfFileId = 0, WordFileIdEn = 0, PdfFileIdEn = 0;
    protected List<DataSources> l_DataSources = new List<DataSources>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<Users> l_Users;
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
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("DocFiles"), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLDataSources_BindByCulture(ddlDataSources, DataSources.Static_GetListByDataTypeId(3), "...");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlStatusUnCheck, ReviewStatus.Static_GetList(), "");
            ddlStatusUnCheck.SelectedValue = "3";

            if (Session["DocFiles-ddlOrderBy"] != null && Request.UrlReferrer != null && (Request.UrlReferrer.OriginalString.Contains("DocFiles.aspx")))
            {
                ddlOrderBy.SelectedValue = Session["DocFiles-ddlOrderBy"].ToString();
                ddlReviewStatus.SelectedValue = Session["DocFiles-ddlReviewStatus"].ToString();
                txtDateFrom.Text = Session["DocFiles-txtDateFrom"].ToString();
                txtDateTo.Text = Session["DocFiles-txtDateTo"].ToString();
                txtSearch.Text = Session["DocFiles-txtSearch"].ToString();
                ddlDataSources.SelectedValue = Session["DocFiles-ddlDataSources"].ToString();
            }
            bindFiles();
        }
        else if (CustomPaging.ChangePage)
        {
            bindFiles();
        }
    }
    protected void SetCurentData()
    {
        Session["DocFiles-ddlOrderBy"] = ddlOrderBy.SelectedValue;
        Session["DocFiles-ddlReviewStatus"] = ddlReviewStatus.SelectedValue;
        Session["DocFiles-txtDateFrom"] = txtDateFrom.Text;
        Session["DocFiles-txtDateTo"] = txtDateTo.Text;
        Session["DocFiles-txtSearch"] = txtSearch.Text;
        Session["DocFiles-ddlDataSources"] = ddlDataSources.SelectedValue;
    }
    private void bindFiles()
    {
        Users m_Users = new Users();
        l_Users = m_Users.GetAll();
        l_ReviewStatus = ReviewStatus.Static_GetList();
        DocFiles m_DocFiles = new DocFiles();
        DataSources m_DataSources = new DataSources();
        List<DocFiles> l_DocFiles = new List<DocFiles>();
        l_DataSources = m_DataSources.GetList();
        string m_OrderBy = ddlOrderBy.SelectedValue;
        m_DocFiles.LanguageId = LanguageId;
        short DataSourceId = short.Parse(ddlDataSources.SelectedValue);
        byte ReviewStatusId = byte.Parse(ddlReviewStatus.SelectedValue);
        string DateFrom = txtDateFrom.Text.Trim();
        string DateTo = txtDateTo.Text.Trim();
        string textSearch = txtSearch.Text.Trim();
        string DocIdentity = txtDocIdentity.Text.Trim();
        int DocIDsearch = 0;
        try
        {
            if (textSearch.Length > 0)
            {
                DocIDsearch = int.Parse(textSearch);
            }
        }
        catch (Exception ex){ }
        int CrUserId = 0;
        int RowCountActUserId = 0;
        try
        {
            l_DocFiles = m_DocFiles.GetPage(ActUserId, m_grid_File.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, 0, DocIDsearch,DocIdentity, 0, LanguageId, DataSourceId, 
                                        ReviewStatusId, CrUserId, DateFrom, DateTo, ref RowCountActUserId);
            m_grid_File.DataSource = l_DocFiles;
            m_grid_File.DataBind();
            CustomPaging.TotalPage = (RowCountActUserId == 0) ? 1 : (RowCountActUserId % m_grid_File.PageSize == 0) ? RowCountActUserId / m_grid_File.PageSize : (RowCountActUserId - RowCountActUserId % m_grid_File.PageSize) / m_grid_File.PageSize + 1;
            lblTong.Text = RowCountActUserId.ToString();
            SetCurentData();
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    private void Review_Click(byte ReviewStatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
        try
        {
            DocFiles m_DocFiles = new DocFiles();
            foreach (GridViewRow m_Row in m_grid_File.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        int DocFilesId = int.Parse(m_grid_File.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_DocFiles.DocId = DocFilesId;
                        m_DocFiles.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_DocFiles.UpdateReviewStatusId(ActUserId, ref SysMessageId);
                        CountSuc++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        Message = "Duyệt thành công " + CountSuc.ToString() + " file. ";
        if (ReviewStatusId != 2)
            Message = "Bỏ duyệt thành công " + CountSuc.ToString() + " file. ";
        bindFiles();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
        //JSAlertHelpers.ShowNotify("10", "", Message, this);
        //lblMsg.Text = Message;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        bindFiles();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            bindFiles();
        }
    }
    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);

    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
        Review_Click(byte.Parse(ddlStatusUnCheck.SelectedValue));

    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        bindFiles();
    }
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        bindFiles();
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string Message = "";
        int CountSuc = 0;
        int CountFalse = 0;
        int CountFalseByStatus = 0;
        try
        {
            DocFiles m_DocFiles = new DocFiles();
            int DelId = 0;
            foreach (GridViewRow m_Row in m_grid_File.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        DelId = int.Parse(m_grid_File.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_DocFiles.DocFileId = DelId;
                        m_DocFiles = m_DocFiles.Get();
                        if (m_DocFiles.ReviewStatusId != 2)
                        {
                            m_DocFiles.DocId = int.Parse(m_grid_File.DataKeys[m_Row.RowIndex].Value.ToString());
                            SysMessageTypeId = m_DocFiles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                            CountSuc++;
                        }
                        else
                        {
                            CountFalseByStatus++;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        Message = "Xóa thành công " + CountSuc.ToString() + " file. ";
        if (CountFalseByStatus > 0)
        {
            Message += "Xóa thất bại " + CountFalseByStatus.ToString() + " file do đang ở trạng thái đã duyệt. ";
        }

        bindFiles();
        JSAlertHelpers.ShowNotify("15", "success", Message, this);
    }
    protected void m_grid_File_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        string deleteMessage = string.Empty;
        DocFiles m_DocFiles = new DocFiles();
        try
        {
            int delId = int.Parse(m_grid_File.DataKeys[e.RowIndex].Value.ToString());
            if (delId > 0)
            {
                Label lblDocFileName = (Label)m_grid_File.Rows[e.RowIndex].FindControl("lblDocFileName");
                m_DocFiles.LanguageId = LanguageId;
                m_DocFiles.DocFileId = delId;
                m_DocFiles = m_DocFiles.Get();
                string filepath = Request.PhysicalApplicationPath + m_DocFiles.FilePath.ToString().Replace("/", "\\");
                SysMessageTypeId = m_DocFiles.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                switch (SysMessageTypeId)
                {
                    case 1:
                        deleteMessage = string.Format("Xóa file : {0} không thành công.", !string.IsNullOrEmpty(lblDocFileName.Text) ? lblDocFileName.Text : "");
                        break;
                    case 0:
                    case 2:
                        deleteMessage = string.Format("Xóa file <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblDocFileName.Text) ? lblDocFileName.Text : "");
                        break;
                }
                JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
                bindFiles();

                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }

    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    HttpPostedFile file = FileUpload1.PostedFile;
    //    string VirtualPath = ConfigurationManager.AppSettings["DirectoryPath"] + "VIETLAWFILE/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
    //    short SysMessageId = 0;
    //    byte SysMessageTypeId = 0;
    //    string messages = string.Empty;
    //    string DirectoryPath = "";
    //    string FilePath = "";
    //    if (file.ContentLength > 0)
    //    {
    //        DocFiles m_DocFiles = new DocFiles();
    //        string fileName = file.FileName; //.Substring(file.FileName.LastIndexOf("\\"));
    //        if (file.FileName.LastIndexOf("\\") > 0) fileName = file.FileName.Substring(fileName.LastIndexOf("\\") + 1);

    //        //m_DocFiles.FileName = fileName;
    //        fileName = fileName.Replace(" ", "_").Replace("#", "").Replace("$", "").Replace("@", "");
    //        fileName = fileName.Insert(fileName.LastIndexOf("."), "_" + DateTime.Now.ToString("ddMMyyHHmmss"));

    //        m_DocFiles.DocId = DocId;
    //        m_DocFiles.DocFileName = String.IsNullOrEmpty(TextBox1.Text) ? fileName : TextBox1.Text;
    //        m_DocFiles.FileTypeId = 6;
    //        m_DocFiles.DataSourceId = short.Parse(DropDownList1.SelectedValue);
    //        m_DocFiles.LanguageId = LanguageId;
    //        m_DocFiles.FilePath = VirtualPath + fileName;
    //        m_DocFiles.FileSize = file.ContentLength;
    //        m_DocFiles.ReviewStatusId = 2;
    //        SysMessageTypeId = m_DocFiles.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

    //        DirectoryPath = Request.PhysicalApplicationPath + VirtualPath.Replace("/", "\\");
    //        if (!Directory.Exists(DirectoryPath))
    //        {
    //            Directory.CreateDirectory(DirectoryPath);
    //        }

    //        FilePath = DirectoryPath + fileName;
    //        file.SaveAs(FilePath);

    //        switch (SysMessageTypeId)
    //        {
    //            case 1:
    //                messages = string.Format("Lỗi khi lưu file : {0}", SysMessages.Static_GetDesc(SysMessageId));
    //                break;
    //            case 0:
    //            case 2:
    //                messages = string.Format("Lưu file <i>{0}</i> thành công.", m_DocFiles.DocFileName);
    //                break;
    //        }
    //        JSAlertHelpers.ShowNotify("15", "success", messages, this);
    //        bindFiles();
    //    }
    //    else JSAlertHelpers.ShowNotify("15", "warning", "Bạn chưa chọn file cần upload.", this);
    //}
}