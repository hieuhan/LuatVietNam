using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;

public partial class Admin_Pages_articles_Features : System.Web.UI.Page
{
    protected int FeatureId = 0;
    protected short FeatureGroupId = 0;
    protected int ActUserId = 0;
    protected List<Users> l_User;
    protected List<InputTypes> l_InputTypes;
    protected List<DataDictionaryTypes> l_DataDicType;
    protected List<FeatureGroups> l_FeatureGroup;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlFeatureGroup, FeatureGroups.Static_GetList(), "");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            int RowCount = 0;
            Users m_Users = new Users();
            l_User = m_Users.GetAll();
            l_InputTypes = InputTypes.Static_GetList();
            l_DataDicType = DataDictionaryTypes.Static_GetList();
            //l_FeatureGroup = FeatureGroups.Static_GetList();
            FeatureGroupId = short.Parse(ddlFeatureGroup.SelectedValue);

            Features m_Features = new Features();
            m_Features.FeatureName = "";
            m_Features.FeatureGroupId = FeatureGroupId;
            List<Features> l_Features = m_Features.GetPage(ActUserId, m_grid.PageSize, 0, "", "", "", 0, 0, "&nbsp;&nbsp;&nbsp;&nbsp;", ref RowCount);
            m_grid.DataSource = l_Features;
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
            Features m_Features = new Features();
            m_Features.FeatureId = Int16.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_Features.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData();
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            Features m_Features = new Features();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_Features.FeatureId = System.Int16.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        SysMessageTypeId = m_Features.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
            BindData();
        }
        catch (Exception ex)
        {
            sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlFeatureGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    
}