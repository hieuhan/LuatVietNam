using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

public partial class Admin_DataSourcesEdit : System.Web.UI.Page
{
   // private byte LanguageId = 0;
    //private byte ApplicationTypeId = 0;
    private short DataSourceId = 0;
    private int ActUserId = 0;
    private byte DataTypeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["DataTypeId"] != null && Request.Params["DataTypeId"].ToString() != "")
        {
            DataTypeId = byte.Parse(Request.Params["DataTypeId"].ToString());
        }
        if (Request.Params["DataSourceId"] != null && Request.Params["DataSourceId"].ToString() != "")
        {
            DataSourceId = short.Parse(Request.Params["DataSourceId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDLDataTypes_BindByCulture(ddlDataTypes, DataTypes.Static_GetList(),"",DataTypeId.ToString());
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (DataSourceId > 0)
            {
                DataSources m_DataSources = new DataSources();
                m_DataSources = m_DataSources.Get(DataSourceId);
                if (m_DataSources.DataSourceId > 0)
                {
                    txtDataSourceName.Text = m_DataSources.DataSourceName.ToString();
                    txtDataSourceDesc.Text = m_DataSources.DataSourceDesc.ToString();
                    txtDisplayOrder.Text = m_DataSources.DisplayOrder.ToString();
                    DropDownListHelpers.DDL_SetSelected(ddlDataTypes, m_DataSources.DataTypeId.ToString());
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
            if (txtDataSourceName.Text.Trim() == "")
            {
                lblMsg.Text = "Tên nguồn dữ liệu không được để trống";
                return;
            }
            if (txtDataSourceDesc.Text.Trim() == "")
            {
                lblMsg.Text = "Mô tả nguồn dữ liệu không được để trống";
                return;
            }
            short SysMessageId = 0;            
            DataSources m_DataSources = new DataSources();           
            m_DataSources.DataSourceId = DataSourceId;
            m_DataSources.DataSourceName = txtDataSourceName.Text.ToString();           
            m_DataSources.DataSourceDesc = txtDataSourceDesc.Text.ToString();
            m_DataSources.DisplayOrder =  txtDisplayOrder.Text =="" ? short.Parse("0") : short.Parse(txtDisplayOrder.Text);
            m_DataSources.DataTypeId = byte.Parse(ddlDataTypes.SelectedValue);
            m_DataSources.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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