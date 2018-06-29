using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
using sms.database;
using System.Data.SqlClient;
using System.Data;
public partial class Admin_Roles : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            Roles m_Roles = new Roles();
            m_grid.DataSource = m_Roles.GetAll();
            m_grid.DataBind();
            lblTong.Text = m_grid.Rows.Count.ToString();
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
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            Label lblActionStatusDesc = (Label)e.Row.FindControl("lblActionStatusDesc");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Roles m_Roles = new Roles();
            m_Roles.RoleId = short.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = Delete(m_Roles.RoleId, ActUserId, ref SysMessageId);
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
        BindData();
    }

    //--------------------------------------------------------------
    public byte Delete(int RoleId, int ActUserId, ref short SysMessageId)
    {
        byte RetVal = 0;
        try
        {
            DBAccess db;
            db = new DBAccess(ICSoft.LawDocsLib.DocConstants.CMS_CONSTR);
            SqlCommand cmd = new SqlCommand("Roles_Delete");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@RoleId", RoleId));
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