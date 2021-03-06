using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using sms.utils;
using sms.common;
namespace ICSoft.AppForm.Admin
{
    public partial class PageChangeProfile : System.Web.UI.Page
    {
        private Users m_User;
        private int ActUserId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ActUserId = SessionHelpers.GetUserId();
                if (ActUserId > 0)
                {
                    if (!IsPostBack)
                    {
                        m_User = new Users();
                        m_User = m_User.Get(ActUserId);
                        Organs m_Organs = new Organs();
                        Ranks m_Ranks = new Ranks();
                        sms.common.Genders m_Genders = new sms.common.Genders();
                        if (m_User.UserId > 0)
                        {
                            cboActions.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "ActionDesc" : "ActionName";
                            cboGenders.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "GenderDesc" : "GenderName";
                            cboOrgans.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "OrganDesc" : "OrganName";
                            cboRanks.DataTextField = LanguageHelpers.IsCultureVietnamese() ? "RankDesc" : "RankName";
                            Actions m_Actions = new Actions();
                            DropDownListHelpers.DDL_Bind(cboOrgans, m_Organs.GetList(), "...", m_User.OrganId.ToString());
                            DropDownListHelpers.DDL_Bind(cboRanks, m_Ranks.GetAll(), "...", m_User.RankId.ToString());
                            DropDownListHelpers.DDL_Bind(cboActions, m_Actions.GetNotRootForUser(ActUserId), "...", m_User.DefaultActionId.ToString());
                            DropDownListHelpers.DDL_Bind(cboGenders, m_Genders.GetAll(), "", m_User.GenderId.ToString());
                            txtUsername.Text = m_User.Username;
                            txtUsername.Enabled = false;
                            txtPass.Text = "";
                            txtFullname.Text = m_User.Fullname;
                            txtDatebirth.Text = m_User.Birthday.ToString("MM/dd/yyyy");
                            txtTelephone.Text = m_User.Telephone;
                            txtEmail.Text = m_User.Email;
                            txtDesc.Text = m_User.Comments;
                            txtAddress.Text = m_User.Address;
                            txtMobile.Text = m_User.Mobile;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                m_User = new Users();
                m_User = m_User.Get(ActUserId);
                string mess, messDistibuted = "";
                short SysMessageId = 0;
                if (m_User.UserId > 0)
                {
                    m_User.Fullname = txtFullname.Text;
                    m_User.Birthday = Convert.ToDateTime(txtDatebirth.Text);
                    m_User.GenderId = Byte.Parse(cboGenders.SelectedValue);
                    m_User.OrganId = Int16.Parse(cboOrgans.SelectedValue);
                    m_User.RankId = Convert.ToByte(cboRanks.SelectedValue);
                    m_User.Telephone = txtTelephone.Text;
                    m_User.Email = txtEmail.Text;
                    m_User.Comments = txtDesc.Text;
                    m_User.Password = (txtPass.Text == "") ? "" : md5.MD5Hash(txtPass.Text);
                    m_User.Address = txtAddress.Text;
                    m_User.Mobile = txtMobile.Text;
                    m_User.DefaultActionId = Convert.ToInt16(cboActions.SelectedValue.ToString());
                    short retVal = m_User.Update(ActUserId, ref SysMessageId);
                    mess = GetLocalResourceObject("ChangeSucess").ToString();
                    if (messDistibuted.Length > 0) mess += messDistibuted;
                    JSAlertHelpers.Alert(mess, this);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog(ex.ToString(), "admin_changeProfile btnUpdate_Click");
                lblMsg.Text = ex.Message;
            }
        }

    }
}
