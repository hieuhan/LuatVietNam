using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using sms.admin.security;
namespace ICSoft.AppForm 
{
    public partial class RegistAction : System.Web.UI.Page
    {
        protected List<Actions> l_Action = new List<Actions>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                    bindData();
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        private void bindData()
        {
            
            string curentFolder = string.IsNullOrEmpty(Request["Folder"]) ? "\\" : "\\" + Request["Folder"] + "\\";
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "Admin\\Pages" + curentFolder;
            string[] l_ActionFile;// = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);  
            string FolderName = "", FileName = "", FilePath = "";
            if (string.IsNullOrEmpty(Request["Folder"]))
            {
                l_ActionFile = Directory.GetDirectories(folderPath);
                
                for (int index = 0; index < l_ActionFile.Length; index++)
                {
                    FolderName = getFolderName(l_ActionFile[index]);
                    Actions m_Action = new Actions();
                    m_Action.ActionName = FolderName;
                    m_Action.ActionDesc = FolderName;
                    m_Action.ActionOrder = short.Parse(index.ToString());
                    m_Action.ActionStatusId = 1;
                    m_Action.Display = 1;
                    m_Action.ParentActionId = 0;
                    m_Action.Url = "#" + FolderName;
                    l_Action.Add(m_Action);
                }
                m_Grid.DataSource = l_Action;
                m_Grid.DataBind();
                m_GridFile.Visible = false;
            }
            else
            {
                short ParentId = 0;
                l_ActionFile = Directory.GetFiles(folderPath, "*.aspx", SearchOption.AllDirectories);
                Actions m_Action = new Actions();
                IList l_ParentAction = m_Action.GetRoots();
                for (int index = 0; index < l_ParentAction.Count; index++)
                {
                    m_Action = (Actions)l_ParentAction[index];
                    if (m_Action.ActionName == Request["Folder"])
                    {
                        ParentId = m_Action.ActionId;
                        break;
                    }
                }
               
                for (int index = 0; index < l_ActionFile.Length; index++)
                {
                    FileName = getFileName(l_ActionFile[index]);
                    FilePath = getFilePath(l_ActionFile[index]);
                    m_Action = new Actions();
                    m_Action.ActionName = FileName;
                    m_Action.ActionDesc = FileName;
                    m_Action.ActionOrder = short.Parse(index.ToString());
                    m_Action.ActionStatusId = 1;
                    m_Action.Display = 1;
                    m_Action.ParentActionId = ParentId;
                    m_Action.Url = FilePath;
                    l_Action.Add(m_Action);
                }
                m_GridFile.DataSource = l_Action;
                m_GridFile.DataBind();
                m_Grid.Visible = false;
            }
            
            
        }
        protected string getFolderName(string FolderPath)
        {
            string RetVal = "";
            if (FolderPath.Contains("\\"))
                RetVal = FolderPath.Substring(FolderPath.LastIndexOf("\\") + 1);
            else
                RetVal = FolderPath;
            return RetVal;
        }
        protected string getFileName(string FilePath)
        {
            
            string RetVal = "";
            FileInfo m_FileInfo = new FileInfo(FilePath);
            RetVal = m_FileInfo.Name.Replace(m_FileInfo.Extension,"");
            return RetVal;
        }
        protected string getFilePath(string FilePath)
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "Admin\\";
            string RetVal = FilePath.Replace(folderPath,"").Replace("\\","/");
            
            return RetVal;
        }
        //==========================================================================================
        protected string ActionStatus_GetDesc(byte id)
        {
            try
            {
                ActionStatus m_ActionStatus = new ActionStatus();
                m_ActionStatus = m_ActionStatus.Get(id);
                return m_ActionStatus.ActionStatusDesc;
            }
            catch
            {
                return "";
            }
        }
        //-------------------------------------------------------------------------
        protected string Actions_GetDesc(short actionId)
        {
            string s = "";
            try
            {
                Actions m_Action = new Actions();
                m_Action = m_Action.Get(actionId);
                s = m_Action.ActionDesc;
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());

            }
            return s;
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            Actions m_Action = new Actions();
            if (string.IsNullOrEmpty(Request["Folder"]))
            {
                foreach (GridViewRow m_Row in m_Grid.Rows)
                {

                    CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                    CheckBox chkDisplay = (CheckBox)m_Row.FindControl("chkDisplay");
                    TextBox tbActionName, tbActionDesc, tbActionOrder, tbActionStatusId, tbParentActionId, tbUrl;
                    tbActionName = (TextBox)m_Row.FindControl("tbActionName");
                    tbActionDesc = (TextBox)m_Row.FindControl("tbActionDesc");
                    tbActionOrder = (TextBox)m_Row.FindControl("tbActionOrder");
                    tbActionStatusId = (TextBox)m_Row.FindControl("tbActionStatusId");
                    tbParentActionId = (TextBox)m_Row.FindControl("tbParentActionId");
                    tbUrl = (TextBox)m_Row.FindControl("tbUrl");
                    if (chkAction != null)
                    {
                        if (chkAction.Checked)
                        {
                            m_Action = new Actions();
                            m_Action.ActionName = tbActionName.Text;
                            m_Action.ActionDesc = tbActionDesc.Text;
                            m_Action.ActionOrder = short.Parse(tbActionOrder.Text);
                            m_Action.ActionStatusId = 1;
                            m_Action.Display = chkDisplay.Checked ? (byte)1 : (byte)0;
                            m_Action.ParentActionId = short.Parse(tbParentActionId.Text);
                            m_Action.Url = tbUrl.Text;
                            short SysMessageId = 0;
                            byte SystemMessageTypeId;
                            SystemMessageTypeId = m_Action.Insert(1, 1, ref SysMessageId);
                            if (SystemMessageTypeId != 1)
                            {
                                
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (GridViewRow m_Row in m_GridFile.Rows)
                {
                    CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                    CheckBox chkDisplay = (CheckBox)m_Row.FindControl("chkDisplay");
                    TextBox tbActionName, tbActionDesc, tbActionOrder, tbActionStatusId, tbParentActionId, tbUrl;
                    tbActionName = (TextBox)m_Row.FindControl("tbActionName");
                    tbActionDesc = (TextBox)m_Row.FindControl("tbActionDesc");
                    tbActionOrder = (TextBox)m_Row.FindControl("tbActionOrder");
                    tbActionStatusId = (TextBox)m_Row.FindControl("tbActionStatusId");
                    tbParentActionId = (TextBox)m_Row.FindControl("tbParentActionId");
                    tbUrl = (TextBox)m_Row.FindControl("tbUrl");
                    if (chkAction != null)
                    {
                        if (chkAction.Checked)
                        {
                            m_Action = new Actions();
                            m_Action.ActionName = tbActionName.Text;
                            m_Action.ActionDesc = tbActionDesc.Text;
                            m_Action.ActionOrder = short.Parse(tbActionOrder.Text);
                            m_Action.ActionStatusId = 1;
                            m_Action.Display = chkDisplay.Checked ? (byte)1 : (byte)0;
                            m_Action.ParentActionId = short.Parse(tbParentActionId.Text);
                            m_Action.Url = tbUrl.Text;
                            short SysMessageId = 0;
                            byte SystemMessageTypeId;
                            SystemMessageTypeId = m_Action.Insert(1, 1, ref SysMessageId);
                            if (SystemMessageTypeId != 1)
                            {
                                
                            }
                        }
                    }
                }
            }
        }
}
}