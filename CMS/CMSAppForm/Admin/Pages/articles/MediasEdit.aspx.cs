using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.common;

public partial class Admin_MediasEdit : System.Web.UI.Page
{
    String csName = "csbMessage";
    Type csType;
    ClientScriptManager cs;
    StringBuilder csText = new StringBuilder();
    private int MediaId = 0;
    private int ActUserId = 0;
    private short SysMessageId = 0;
    private byte SysMessageTypeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["MediaId"] != null && Request.Params["MediaId"].ToString() != "")
        {
            MediaId = int.Parse(Request.Params["MediaId"].ToString());
        }
        if (!IsPostBack)
        {
            if (MediaId == 0)
            {
                DropDownListHelpers.DDL_Bind(ddlMediaGroup, MediaGroups.Static_GetAllHierachy(ActUserId, 0, 0, "--"), "...");
                DropDownListHelpers.DDL_Bind(ddlMediaType, MediaTypes.Static_GetList(), "...");
                cblAddOther.Visible = true;
            }
            else
            {
                cblAddOther.Visible = false;
                BindData();
            }
        }
    }
    private void BindData()
    {
        try
        {
            Medias m_Medias = new Medias();
            m_Medias.MediaId = MediaId;
            m_Medias = m_Medias.Get();
            DropDownListHelpers.DDL_Bind(ddlMediaGroup, MediaGroups.Static_GetAllHierachy(ActUserId, 0, 0, "--"), "...", m_Medias.MediaGroupId.ToString());
            DropDownListHelpers.DDL_Bind(ddlMediaType, MediaTypes.Static_GetList(), "...", m_Medias.MediaTypeId.ToString());
            txtMediaDesc.Text = m_Medias.MediaDesc;
            txtMediaName.Text = m_Medias.MediaName;
            txtFilePath.Text = m_Medias.FilePath;
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
            int countSuccess = 0, countError = 0;
            string messages = string.Empty;
            Medias m_Medias = new Medias();
            m_Medias.MediaId = MediaId;
            m_Medias.FilePath = "";
            if (MediaId>0) m_Medias = m_Medias.Get();

            if (fUpl.HasFile)
            {
                if(MediaId == 0)
                {
                    HttpFileCollection uploadFiles = Request.Files;
                    for (int index = 0; index < uploadFiles.Count; index++)
                    {
                        HttpPostedFile postedFile = uploadFiles[index];
                        string mediaName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                        string RootDir = Request.PhysicalApplicationPath;
                        string FilePath = FileUploadHelpers.SaveFile(postedFile, RootDir, CmsConstants.MEDIA_PATH, true);

                        m_Medias = new Medias();
                        m_Medias.MediaName = !string.IsNullOrEmpty(txtMediaName.Text) ? string.Concat(txtMediaName.Text, "-", (index + 1)) : mediaName;
                        m_Medias.MediaDesc = !string.IsNullOrEmpty(txtMediaDesc.Text) ? string.Concat(txtMediaDesc.Text, "-", (index + 1)) : mediaName;
                        //if(String.IsNullOrEmpty(txtMediaName.Text))
                        //{
                        //    m_Medias.MediaName = postedFile.FileName.Split('.')[0];
                        //    m_Medias.MediaDesc = postedFile.FileName.Split('.')[0];
                        //}
                        m_Medias.MediaGroupId = short.Parse(ddlMediaGroup.SelectedValue);
                        m_Medias.MediaTypeId = byte.Parse(ddlMediaType.SelectedValue);
                        m_Medias.FilePath = FilePath.Replace("\\", "/");
                        m_Medias.FileSize = postedFile.ContentLength;
                        SysMessageTypeId = m_Medias.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        if (SysMessageTypeId == 1) //error
                        {
                            countError++;
                        }
                        else if (SysMessageTypeId == 0 || SysMessageTypeId == 2) //success
                        {
                            countSuccess++;
                        }
                    }
                    if (countSuccess > 0)
                    {
                        messages += string.Format("Upload thành công <i>{0}</i> {1}", countSuccess, " file Media.");
                    }
                    if (countError > 0)
                    {
                        messages += string.Format("<i>{0}</i> file Media chưa được Upload.", countError);
                    }
                }
                else
                {
                    string RootDir = Request.PhysicalApplicationPath;
                    string FilePath = FileUploadHelpers.SaveFile(fUpl, RootDir, CmsConstants.MEDIA_PATH, true);
                    m_Medias.FilePath = FilePath.Replace("\\", "/");
                    m_Medias.MediaName = txtMediaName.Text;
                    m_Medias.MediaDesc = txtMediaDesc.Text;
                    m_Medias.MediaGroupId = short.Parse(ddlMediaGroup.SelectedValue);
                    m_Medias.MediaTypeId = byte.Parse(ddlMediaType.SelectedValue);
                    SysMessageTypeId = m_Medias.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    switch (SysMessageTypeId)
                    {
                        case 1:
                            messages = string.Format("Lỗi khi cập nhật file Media : {0}", SysMessages.Static_GetDesc(SysMessageId));
                            break;
                        case 2:
                        case 0:
                            messages = string.Format("Cập nhật file Media <i>{0}</i> thành công.", !string.IsNullOrEmpty(txtMediaName.Text) ? txtMediaName.Text : "");
                            break;
                    }
                }
                
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFilePath.Text))
                {
                    m_Medias.FilePath = txtFilePath.Text;
                    m_Medias.MediaName = txtMediaName.Text;
                    m_Medias.MediaDesc = txtMediaDesc.Text;
                    m_Medias.MediaGroupId = short.Parse(ddlMediaGroup.SelectedValue);
                    m_Medias.MediaTypeId = byte.Parse(ddlMediaType.SelectedValue);
                    SysMessageTypeId = m_Medias.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    switch (SysMessageTypeId)
                    {
                        case 1:
                            messages = string.Format("Lỗi khi lưu file Media : {0}",
                                SysMessages.Static_GetDesc(SysMessageId));
                            break;
                        case 2:
                        case 0:
                            messages = string.Format("Lưu file Media <i>{0}</i> thành công.",
                                !string.IsNullOrEmpty(txtMediaName.Text) ? txtMediaName.Text : "");
                            break;
                    }
                }
                else
                {
                    JSAlertHelpers.ShowNotify("15", "success", "Bạn chưa chọn file cần upload.", this);
                    return;
                }
            }

            if (cblAddOther.Checked)
            {
                JSAlertHelpers.ShowNotify("15", "success", messages, this);
                ClearForm();
                return;
            }
            JSAlertHelpers.ShowNotifyOtherPage("15", "success", messages, this);

            string script = @"<script language='JavaScript'>" +
                "window.parent.jQuery('#divEdit').dialog('close');" +
                "</script>";
            ClientScriptManager csm = this.ClientScript;
            csm.RegisterClientScriptBlock(this.GetType(), "close", script);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    private void ClearForm()
    {
        txtMediaName.Text = "";
        txtMediaDesc.Text = "";
        txtFilePath.Text = "";
        ddlMediaGroup.SelectedIndex = -1;
        ddlMediaType.SelectedIndex = -1;
    }
  
}