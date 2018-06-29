using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using System.IO;
using sms.common;

public partial class Admin_ArticleMediasDb : System.Web.UI.Page
{
    private short SysMessageId = 0;
    private byte SysMessageTypeId = 0;
    protected int ActUserId = 0;
    protected int ArticleId = 0;
    protected List<Users> l_Users = new List<Users>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ArticleId"] != null && Request.Params["ArticleId"].ToString() != "")
        {
            ArticleId = int.Parse(Request.Params["ArticleId"].ToString());
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
            string deleteMessage = string.Empty;
            Label lblMediaName = (Label)m_grid.Rows[e.RowIndex].FindControl("lblMediaName");
            Medias m_Medias = new Medias();
            m_Medias.MediaId = int.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_Medias = m_Medias.Get();
            string RootDir = Request.PhysicalApplicationPath;
            string FilePath = Path.Combine(RootDir,m_Medias.FilePath).Replace("/","\\"); 
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            SysMessageTypeId = m_Medias.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            if (SysMessageTypeId == 1)//error
            {
                deleteMessage = string.Format("Lỗi khi xóa file Media : {0}", SysMessages.Static_GetDesc(SysMessageId));
            }
            else if (SysMessageTypeId == 0 || SysMessageTypeId == 2)
            {
                deleteMessage = string.Format("Xóa file Media <i>{0}</i> thành công.", !string.IsNullOrEmpty(lblMediaName.Text) ? lblMediaName.Text : "");
            }
            JSAlertHelpers.ShowNotify("15", "success", deleteMessage, this);
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
        if (IsPostBack)
        {
            CustomPaging.PageIndex = 1;
            BindData();
        }
    }
  
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        int countDeleteSuccess = 0, countDeleteError = 0;
        string messages = string.Empty;
        bool isSelected = false;
        try
        {
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        isSelected = true;
                        Medias m_Medias = new Medias();
                        m_Medias.MediaId = int.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Medias = m_Medias.Get();
                        string RootDir = Request.PhysicalApplicationPath;
                        string FilePath = Path.Combine(RootDir, m_Medias.FilePath).Replace("/", "\\");
                        if (File.Exists(FilePath))
                        {
                            File.Delete(FilePath);
                        }
                       SysMessageTypeId = m_Medias.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                       if (SysMessageTypeId == 1) //error
                       {
                           countDeleteError++;
                       }
                       else if (SysMessageTypeId == 0 || SysMessageTypeId == 2) //success
                       {
                           countDeleteSuccess++;
                       }
                    }
                }
            }
            if (isSelected)
            {
                if (countDeleteSuccess > 0)
                {
                    messages += string.Format("Xóa thành công <i>{0}</i> {1}", countDeleteSuccess, " file Media.");
                }
                if (countDeleteError > 0)
                {
                    messages += string.Format("<i>{0}</i> file Media chưa được xóa.", countDeleteError);
                }
                JSAlertHelpers.ShowNotify("15", "success", messages, this);
            }
            else JSAlertHelpers.ShowNotify("15", "success", "Bạn chưa chọn file cần xóa.", this);
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
            int countSuccess = 0, countError = 0;
            string messages = string.Empty;

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
                SysMessageTypeId = m_Medias.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

                if (SysMessageTypeId == 1) //error
                {
                    countError++;
                }
                else if (SysMessageTypeId == 2) //success
                {
                    countSuccess++;
                }

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
            if (countSuccess > 0)
            {
                messages += string.Format("Upload thành công <i>{0}</i> {1}", countSuccess, " file Media.");
            }
            if (countError > 0)
            {
                messages += string.Format("<i>{0}</i> file Media chưa được upload.", countError);
            }
            JSAlertHelpers.ShowNotify("15", "success", messages, this);
            txtSearch.Text = "";
            BindData();
        }
        else
        {
            JSAlertHelpers.ShowNotify("15", "success", "Bạn chưa chọn file cần upload.", this);
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            BindData();
        }
    }
}
