using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using System.Data.SqlClient;
using System.Data;
using sms.database;
public partial class Admin_RolesEdit : System.Web.UI.Page
{
    private short RoleId = 0;
    private int ActUserId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["RoleId"] != null && Request.Params["RoleId"].ToString() != "")
        {
            RoleId = short.Parse(Request.Params["RoleId"].ToString());
        }
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (RoleId > 0)
            {
                Roles m_Roles = new Roles();
                m_Roles = m_Roles.Get(RoleId);
                if (m_Roles.RoleId > 0)
                {
                    txtRoleName.Text = m_Roles.RoleName;
                    txtRoleDesc.Text = m_Roles.RoleDesc;
                    chkBuildIn.Checked = (m_Roles.BuildIn == 1) ? true : false;
                }
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
            Roles m_Roles = new Roles();
            m_Roles.RoleId = RoleId;
            m_Roles = m_Roles.Get(RoleId);
            m_Roles.RoleDesc = txtRoleDesc.Text;
            m_Roles.RoleName = txtRoleName.Text;
            m_Roles.BuildIn = chkBuildIn.Checked ? (byte)1 : (byte)0;
            if (RoleId > 0)
            {
                Update(m_Roles, ActUserId, ref SysMessageId);
            }
            else
            {
                m_Roles.Insert(ActUserId, ref SysMessageId);
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
    //--------------------------------------------------------------
    public byte Update(Roles m_Roles , int ActUserId, ref short SysMessageId)
    {
        byte RetVal = 0;
        try
        {
            DBAccess db;
            db = new DBAccess(ICSoft.LawDocsLib.DocConstants.CMS_CONSTR);
            SqlCommand cmd = new SqlCommand("Roles_Update");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RoleName", m_Roles.RoleName));
            cmd.Parameters.Add(new SqlParameter("@RoleDesc", m_Roles.RoleDesc));
            cmd.Parameters.Add(new SqlParameter("@RoleId", m_Roles.RoleId));
            cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
            cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            db.ExecuteSQL(cmd);
            SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
            RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return RetVal;
    }
}