using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using sms.utils;

public partial class Admin_ServiceRolesEdit : System.Web.UI.Page
{
    private short ServiceId = 0;
    private int ActUserId = 0;
    protected List<ServiceRoles> l_ServiceRoles = new List<ServiceRoles>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["ServiceId"] != null && Request.Params["ServiceId"].ToString() != "")
        {
            ServiceId = short.Parse(Request.Params["ServiceId"].ToString());
        }
        if (!IsPostBack)
        {
             CheckBoxListHelpers.Bind(chkRoles, Roles.Static_GetList(), "");
             BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (ServiceId > 0)
            {
                ServiceRoles m_ServiceRoles = new ServiceRoles();
                IList arrServiceRoles = (IList)m_ServiceRoles.ServiceRoles_GetList(ActUserId, "", ServiceId, 0);
                string RetVal = "";
                if (arrServiceRoles.Count > 0)
                {
                    for (int i = 0; i < arrServiceRoles.Count; i++)
                    {
                        m_ServiceRoles=(ServiceRoles)arrServiceRoles[i];
                        RetVal += m_ServiceRoles.RoleId.ToString() + ",";
                    }
                }
                RetVal = "," + RetVal;
                for (int i = 0; i <= chkRoles.Items.Count - 1; i++)
                {

                    if (RetVal.IndexOf("," + chkRoles.Items[i].Value.ToString() + ",") >= 0)
                    {
                        chkRoles.Items[i].Selected = true;
                        chkRoles.Items[i].Attributes.Add("style", "font-weight:bold; color:Red");
                    }
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
            ServiceRoles m_ServiceRoles = new ServiceRoles();
            short ServiceId = short.Parse(Request.Params["ServiceId"]);
            int RetVal = 0;

            if ((chkRoles.Items.Count > 0) && ServiceId>0)
            {
                for (int i = 0; i < chkRoles.Items.Count; i++)
                {
                    if (chkRoles.Items[i].Selected)
                    {
                        RetVal = RetVal + 1;
                    }
                }

                if (RetVal > 0)
                {
                    m_ServiceRoles.ServiceId = ServiceId;
                    m_ServiceRoles.DeleteByServiceId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    for (int i = 0; i < chkRoles.Items.Count; i++)
                    {
                        if (chkRoles.Items[i].Selected)
                        {
                            m_ServiceRoles.ServiceId = ServiceId;
                            m_ServiceRoles.RoleId = byte.Parse(chkRoles.Items[i].Value);
                            m_ServiceRoles.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        }
                    }
                    lblMsg.Text = "";
                    //JSAlertHelpers.close(this);
                    string script = @"<script language='JavaScript'>" +
                        "window.parent.jQuery('#divEdit').dialog('close');" +
                        "</script>";
                    ClientScriptManager csm = this.ClientScript;
                    csm.RegisterClientScriptBlock(this.GetType(), "close", script);
                }
                else
                {
                    lblMsg.Text = "Bạn phải chọn quyền trước khi lưu thông tin";
                    return;
                }
                
            }
            else 
            {
                lblMsg.Text = "Bạn phải thêm danh sách quyền trước khi phân quyền";
                return;
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}