using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_MediasSelect : System.Web.UI.Page
{
    private short SysMessageId = 0;
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected string ImgDemo = "";
    protected string txtDemo = "";
    protected string CKEditor = "";
    protected string CKEditorFuncNum = "";
    protected string langCode = "";
    protected string SetMediaDomain = "";
    protected List<Users> l_Users = new List<Users>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
        }
        if (Request.Params["CKEditor"] != null && Request.Params["CKEditor"].ToString() != "")
        {
            CKEditor = Request.Params["CKEditor"].ToString();
        }
        if (Request.Params["CKEditorFuncNum"] != null && Request.Params["CKEditorFuncNum"].ToString() != "")
        {
            CKEditorFuncNum = Request.Params["CKEditorFuncNum"].ToString();
        }
        if (Request.Params["langCode"] != null && Request.Params["langCode"].ToString() != "")
        {
            ImgDemo = Request.Params["langCode"].ToString();
        }
        if (Request.Params["ImgDemo"] != null && Request.Params["ImgDemo"].ToString() != "")
        {
            ImgDemo = Request.Params["ImgDemo"].ToString();
        }
        if (Request.Params["txtDemo"] != null && Request.Params["txtDemo"].ToString() != "")
        {
            txtDemo = Request.Params["txtDemo"].ToString();
        }
        if (Request.Params["SetMediaDomain"] != null && Request.Params["SetMediaDomain"].ToString() != "")
        {
            SetMediaDomain = Request.Params["SetMediaDomain"].ToString();
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlMediaGroup, MediaGroups.Static_GetAllHierachy(ActUserId, 0, 0, "--"), "...");
            DropDownListHelpers.DDL_Bind(ddlMediaType, MediaTypes.Static_GetList(), "...");
            BindData();
        }
        else if (CustomPaging.ChangePage)
        {
            BindData();
        }
    }
    
    private void BindData()
    {
        try
        {
            Users m_Users = new Users();
            l_Users = m_Users.GetAll();

            int RowAmount = m_grid.PageSize;
            int PageIndex = CustomPaging.PageIndex - 1;
            string OrderBy="";
            string DateFrom="";
            string DateTo="";
            int RowCount = 0;

            Medias m_Medias = new Medias();
            m_Medias.MediaGroupId = short.Parse(ddlMediaGroup.SelectedValue);
            m_Medias.MediaTypeId = byte.Parse(ddlMediaType.SelectedValue);
            m_Medias.MediaName = txtSearch.Text;

            m_grid.DataSource = m_Medias.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DateFrom, DateTo, 0, ref RowCount);
            m_grid.DataBind();

            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
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
            
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Medias m_Medias = new Medias();
            m_Medias.MediaId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_Medias = m_Medias.Get();

            string script = @"<script language='JavaScript'>" +
               "var ImgDemo = window.opener.document.getElementById(\"" + ImgDemo + "\"); " +
               "var txtName = window.opener.document.getElementById(\"" + txtDemo + "\"); " +
               "txtName.value = '" + (m_Medias.FilePath.StartsWith("http") ? m_Medias.FilePath : CmsConstants.WEBSITE_MEDIADOMAIN + m_Medias.FilePath) + "';" +
               "ImgDemo.src = '" + (m_Medias.FilePath.StartsWith("http") ? m_Medias.FilePath : CmsConstants.WEBSITE_MEDIADOMAIN + m_Medias.FilePath) + "';" +
               " window.close(); " +
               "</script>";
            if (CKEditor != "")
            {
                script = @"<script language='JavaScript'>window.opener.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + (m_Medias.FilePath.StartsWith("http") ? m_Medias.FilePath : CmsConstants.WEBSITE_MEDIADOMAIN + m_Medias.FilePath) + "');window.close();</script>";
            }
            //Response.Write(script);
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close1", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
  
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Medias m_Medias = new Medias();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Medias.MediaId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        break;
                    }
                }
            }
            m_Medias = m_Medias.Get();
            string script = @"<script language='JavaScript'>" +
               "var ImgDemo = window.opener.document.getElementById(\"" + ImgDemo + "\"); " +
               "var txtName = window.opener.document.getElementById(\"" + txtDemo + "\"); " +
               "txtName.value = '" + CmsConstants.WEBSITE_MEDIADOMAIN + m_Medias.FilePath + "';" +
               "ImgDemo.src = '" + CmsConstants.WEBSITE_MEDIADOMAIN + m_Medias.FilePath + "';" +
               " window.close(); " +
               "</script>";

            if (CKEditor != "")
            {
                script = @"<script language='JavaScript'>window.opener.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + CmsConstants.WEBSITE_MEDIADOMAIN + m_Medias.FilePath + "');window.close();</script>";
            }
            //Response.Write(script);
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close1", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void ddlMediaGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void ddlMediaType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomPaging.PageIndex = 1;
        BindData();
    }
    protected void btnUpl_Click(object sender, EventArgs e)
    {
        if (fUpl.HasFile)
        {

            HttpFileCollection uploadFiles = Request.Files;

            for (int index = 0; index < uploadFiles.Count; index++)
            {
                HttpPostedFile postedFile = uploadFiles[index];

                string RootDir = Request.PhysicalApplicationPath;
                string FilePath = FileUploadHelpers.SaveFile(postedFile, RootDir, CmsConstants.MEDIA_PATH, true);

                Medias m_Medias = new Medias();
                m_Medias.MediaName = postedFile.FileName.Split('.')[0];
                m_Medias.MediaDesc = postedFile.FileName.Split('.')[0];

                m_Medias.MediaGroupId = short.Parse(ddlMediaGroup.SelectedValue);
                m_Medias.MediaTypeId = byte.Parse(ddlMediaType.SelectedValue);
                m_Medias.FilePath = FilePath.Replace("\\", "/");
                m_Medias.FileSize = postedFile.ContentLength;
                m_Medias.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

                if (ArticleId > 0)
                {
                    ArticleMedias m_ArticleMedias = new ArticleMedias();
                    m_ArticleMedias.ArticleId = ArticleId;
                    m_ArticleMedias.MediaTypeId = m_Medias.MediaTypeId;
                    m_ArticleMedias.FilePath = m_Medias.FilePath;
                    m_ArticleMedias.MediaId = m_Medias.MediaId;
                    m_ArticleMedias.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                }
            }

            txtSearch.Text = "";

        }
        BindData();
    }
}
