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

public partial class Admin_UserCategoriesEdit : System.Web.UI.Page
{
    private int ActUserId = 0;
    private int UserId = 0;
    protected List<ServiceRoles> l_ServiceRoles = new List<ServiceRoles>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["UserId"] != null && Request.Params["UserId"].ToString() != "")
        {
            UserId = int.Parse(Request.Params["UserId"].ToString());
        }
        if (!IsPostBack)
        {
            List<Categories> l_Category = Categories.Static_GetAllHierachy(ActUserId, "", 1, 1, 0, 0, "&nbsp;&nbsp;&nbsp;");
            CheckBoxListHelpers.Bind(chkCategories, l_Category, "");
             BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (UserId > 0)
            {
                lblUserNameShow.Text =(Request.QueryString["UserName"]!="" ? Request.QueryString["UserName"].ToString() : "");
                UserCategories m_UserCategories = new UserCategories();
                IList arrUserCategories = (IList)m_UserCategories.UserCategories_GetList(ActUserId,"",UserId, 0);
                string RetVal = "";
                if (arrUserCategories.Count > 0)
                {
                    for (int i = 0; i < arrUserCategories.Count; i++)
                    {
                        m_UserCategories = (UserCategories)arrUserCategories[i];
                        RetVal += m_UserCategories.CategoryId.ToString() + ",";
                    }
                }
                RetVal = "," + RetVal;
                for (int i = 0; i <= chkCategories.Items.Count - 1; i++)
                {

                    if (RetVal.IndexOf("," + chkCategories.Items[i].Value.ToString() + ",") >= 0)
                    {
                        chkCategories.Items[i].Selected = true;
                        chkCategories.Items[i].Attributes.Add("style", "font-weight:bold; color:Red");
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
            UserCategories m_UserCategories = new UserCategories();
            int UserId = int.Parse(Request.Params["UserId"]);
            int RetVal = 0;
            if (UserId > 0)
            {
                if ((chkCategories.Items.Count > 0) && UserId > 0)
                {
                    for (int i = 0; i < chkCategories.Items.Count; i++)
                    {
                        if (chkCategories.Items[i].Selected)
                        {
                            RetVal = RetVal + 1;
                        }
                    }

                    if (RetVal > 0)
                    {
                        m_UserCategories.UserId = UserId;
                        m_UserCategories.DeleteByUserId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                        for (int i = 0; i < chkCategories.Items.Count; i++)
                        {
                            if (chkCategories.Items[i].Selected)
                            {
                                m_UserCategories.UserId = UserId;
                                m_UserCategories.CategoryId = short.Parse(chkCategories.Items[i].Value);
                                m_UserCategories.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
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
                        lblMsg.Text = "Bạn phải chọn chuyên mục trước khi lưu thông tin";
                        return;
                    }

                }
                else
                {
                    lblMsg.Text = "Bạn phải thêm danh sách chuyên mục trước khi phân quyền";
                    return;
                }
            }
            
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}