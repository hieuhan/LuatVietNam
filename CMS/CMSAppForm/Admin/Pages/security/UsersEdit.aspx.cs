using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using sms.common;
using sms.utils;
public partial class Admin_UsersEdit : System.Web.UI.Page
{
    private short UserId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["UserId"] != null && Request.Params["UserId"].ToString() != "")
        {
            UserId = short.Parse(Request.Params["UserId"].ToString());
        }
        if (!IsPostBack)
        {
            rdoGender.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "GenderDesc" : "GenderName";
            ddlOrgan.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "OrganDesc" : "OrganName";
            ddlRank.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "RankDesc" : "RankName";
            ddlAction.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "ActionDesc" : "ActionName";
            ddlUserStatus.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "UserStatusDesc" : "UserStatusName";
            chkRole.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "RoleDesc" : "RoleName";
            sms.common.Genders m_Genders = new sms.common.Genders();
            Organs m_Organs = new Organs();
            Ranks m_Ranks = new Ranks();
            Actions m_Actions = new Actions();
            UserStatus m_UserStatus = new UserStatus();
            Roles m_Roles = new Roles();
            RadioButtonListHelpers.Bind(rdoGender, m_Genders.GetAll(), "");
            DropDownListHelpers.DDL_Bind(ddlOrgan, m_Organs.GetAll(), "...");
            DropDownListHelpers.DDL_Bind(ddlRank, m_Ranks.GetAll(), "...");
            DropDownListHelpers.DDL_Bind(ddlAction, m_Actions.GetAllHierachy(), "");
            DropDownListHelpers.DDL_Bind(ddlUserStatus, m_UserStatus.GetAll(), "");
            CheckBoxListHelpers.Bind(chkRole, m_Roles.GetAll(), "");
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (UserId > 0)
            {
                Users m_Users = new Users();
                m_Users = m_Users.Get(UserId);
                if (m_Users.UserId > 0)
                {
                    txtUserName.Text = m_Users.Username;
                    txtFullname.Text = m_Users.Fullname;
                    txtEmail.Text = m_Users.Email;
                    txtAddress.Text = m_Users.Address;
                    txtBirthDay.Text = m_Users.Birthday.ToString("dd/MM/yyyy");
                    txtComment.Text = m_Users.Comments;
                    txtMobile.Text = m_Users.Mobile;
                    RadioButtonListHelpers.SetSelected(rdoGender, m_Users.GenderId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlOrgan, m_Users.OrganId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlRank, m_Users.RankId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlAction, m_Users.DefaultActionId.ToString());
                    DropDownListHelpers.DDL_SetSelected(ddlUserStatus, m_Users.UserStatusId.ToString());
                    UserRoles m_UserRoles = new UserRoles();
                    List<UserRoles> l_UserRole = m_UserRoles.GetListByUserId(m_Users.UserId).Cast<UserRoles>().ToList<UserRoles>();
                    for (int i = 0; i < l_UserRole.Count; i++)
                    {
                        CheckBoxListHelpers.SetSelected(chkRole, l_UserRole[i].RoleId.ToString());
                    }
                }
            }
            else
            {
                DropDownListHelpers.DDL_SetSelected(ddlAction, "10");
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
            Users m_Users = new Users();
            m_Users.UserId = UserId;
            m_Users.Username = txtUserName.Text;
            m_Users.Address = txtAddress.Text;
            m_Users.Birthday = DateTime.Parse(txtBirthDay.Text);
            m_Users.Comments = txtComment.Text;
            m_Users.DefaultActionId = short.Parse(ddlAction.SelectedValue);
            m_Users.Email = txtEmail.Text;
            m_Users.Fullname = txtFullname.Text;
            m_Users.GenderId = rdoGender.SelectedIndex>=0 ? byte.Parse(rdoGender.SelectedValue) : (byte)0;
            m_Users.Mobile = txtMobile.Text;
            m_Users.OrganId = short.Parse(ddlOrgan.SelectedValue);
            m_Users.Password = (txtPassword.Text == "") ? "" : md5.MD5Hash(txtPassword.Text);
            m_Users.Telephone = "";
            m_Users.RankId = byte.Parse(ddlRank.SelectedValue);
            m_Users.UserStatusId = byte.Parse(ddlUserStatus.SelectedValue);
            if (UserId > 0)
            {
                m_Users.Update(ActUserId, ref SysMessageId);
            }
            else
            {
                m_Users.Insert(ActUserId, ref SysMessageId);
            }
            //Add role
            if (m_Users.UserId > 0)
            {
                UserRoles m_UserRoles = new UserRoles();
                m_UserRoles.UserId = m_Users.UserId;
                m_UserRoles.DeleteQuickBy(ActUserId);
                for (int index = 0; index < chkRole.Items.Count; index++)
                {
                    m_UserRoles.RoleId = short.Parse(chkRole.Items[index].Value);
                    if (chkRole.Items[index].Selected)
                    {
                        m_UserRoles.InsertQuick(ActUserId);
                    }
                }
            }
            //JSAlertHelpers.close(this);
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
}