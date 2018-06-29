using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using sms.admin.security;
public partial class Admin_CategoriesPosition : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected short CategoryId = 0;
    protected byte LanguageId = 0;
    protected byte AppTypeId = 0;
    private byte PositionDataTypeId = 9;
   
    private List<Languages> l_Language = new List<Languages>();
    protected List<ReviewStatus> l_ReviewStatus = new List<ReviewStatus>();
    protected List<DisplayTypes> l_DisplayTypes = new List<DisplayTypes>();
    protected List<CategoryDisplays> l_CategoryDisplays = new List<CategoryDisplays>();
    protected List<Users> l_Users = new List<Users>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["PositionDataTypeId"] != null && Request.Params["PositionDataTypeId"].ToString() != "")
        {
            PositionDataTypeId = byte.Parse(Request.Params["PositionDataTypeId"].ToString());
        }
        if (!IsPostBack)
        {
            DropDownListHelpers.DDL_Bind(ddlSite, Sites.Static_GetList(ActUserId), "");
            DropDownListHelpers.DDLLanguage_BindByCulture(ddlLanguage, Languages.Static_GetList(), "");
            DropDownListHelpers.DDLAppType_BindByCulture(ddlAppType, ApplicationTypes.Static_GetList(), "");
            DropDownListHelpers.DDLReviewStatus_BindByCulture(ddlReviewStatus, ReviewStatus.Static_GetList(), "...");
            DropDownListHelpers.DDLOrderBy_BindByCulture(ddlOrderBy, OrderByClauses.Static_GetList("Categories"), "");
            DropDownListHelpers.DDLDataTypes_BindByCulture(ddlDataType, DataTypes.Static_GetList(), "");
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            l_Language = Languages.Static_GetList();
            l_ReviewStatus = ReviewStatus.Static_GetList();
            DisplayTypes m_DisplayTypes = new DisplayTypes();
            l_DisplayTypes = m_DisplayTypes.GetListByDataTypeId(PositionDataTypeId);
            CategoryDisplays m_CategoryDisplays = new CategoryDisplays();
            l_CategoryDisplays=m_CategoryDisplays.GetListBySiteId(short.Parse(ddlSite.SelectedValue));
            Users m_Users = new Users(); 
			 l_Users = m_Users.GetAll();

            Categories m_Categories = new Categories();
            string m_OrderBy = ddlOrderBy.SelectedValue;
            short m_CategoryId = 0;
            byte m_ReviewStatus = byte.Parse(ddlReviewStatus.SelectedValue);
            LanguageId = byte.Parse(ddlLanguage.SelectedValue);
            AppTypeId = byte.Parse(ddlAppType.SelectedValue);
            m_Categories.DataTypeId = byte.Parse(ddlDataType.SelectedValue);
            m_Categories.SiteId = byte.Parse(ddlSite.SelectedValue);
            m_grid.DataSource = m_Categories.GetAllHierachy(ActUserId, m_OrderBy, LanguageId, AppTypeId, m_CategoryId,  m_ReviewStatus, "&nbsp;&nbsp;&nbsp;&nbsp;");
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
            Categories m_DataItem = (Categories)e.Row.DataItem;
            CheckBoxList chkDisplayType = (CheckBoxList)e.Row.FindControl("chkDisplayType");

            CheckBoxListHelpers.Bind(chkDisplayType, l_DisplayTypes, "");
            CategoryDisplays m_CategoryDisplays = new CategoryDisplays();
            List<CategoryDisplays> l_CategoryDisplays1 = CategoryDisplays.Static_GetByCategoryId(m_DataItem.CategoryId, l_CategoryDisplays);
            for (int i = 0; i < l_CategoryDisplays1.Count; i++)
            {
                CheckBoxListHelpers.SetSelected(chkDisplayType, l_CategoryDisplays1[i].DisplayTypeId.ToString(), "Yellow");
            }

            if (m_DataItem.ParentCategoryId == 0)
            {
                e.Row.Cells[1].Font.Bold = true;
            }
        }
    }
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            
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
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlReviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    private void Review_Click(byte ReviewStatusId)
    {
        short SysMessageId = 0;
        try
        {
            
            Categories m_Categories = new Categories();
            foreach (GridViewRow m_Row in m_grid.Rows)
            {
                Categories m_DataItem = (Categories)m_Row.DataItem;

                CheckBox ckbShowTop = (CheckBox)m_Row.FindControl("ckbShowTop");
                CheckBox ckbShowBottom = (CheckBox)m_Row.FindControl("ckbShowBottom");
                CheckBox ckbShowWeb = (CheckBox)m_Row.FindControl("ckbShowWeb");
                CheckBox ckbShowWap = (CheckBox)m_Row.FindControl("ckbShowWap");
                CheckBox ckbShowApp = (CheckBox)m_Row.FindControl("ckbShowApp");

                m_Categories.CategoryId = short.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                m_Categories = m_Categories.Get();
                m_Categories.ShowApp = byte.Parse(ckbShowApp.Checked == true ? "1" : "0");
                m_Categories.ShowBottom = byte.Parse(ckbShowBottom.Checked == true ? "1" : "0");
                m_Categories.ShowTop = byte.Parse(ckbShowTop.Checked == true ? "1" : "0");
                m_Categories.ShowWap = byte.Parse(ckbShowWap.Checked == true ? "1" : "0");
                m_Categories.ShowWeb = byte.Parse(ckbShowWeb.Checked == true ? "1" : "0");
                
                m_Categories.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);

                CheckBoxList chkDisplayType = (CheckBoxList)m_Row.FindControl("chkDisplayType");
                CategoryDisplays m_CategoryDisplays = new CategoryDisplays();
                m_CategoryDisplays.CategoryId = m_Categories.CategoryId;
                m_CategoryDisplays.CrUserId = ActUserId;
                string m_DisplayTypeId_Add = "";
                string m_DisplayTypeId_Del = "";
                for (int i = 0; i < chkDisplayType.Items.Count; i++)
                {
                    m_CategoryDisplays.DisplayTypeId = short.Parse(chkDisplayType.Items[i].Value);
                    if (chkDisplayType.Items[i].Selected)
                    {
                        if (m_DisplayTypeId_Add != "") m_DisplayTypeId_Add = m_DisplayTypeId_Add + ",";
                        m_DisplayTypeId_Add = m_DisplayTypeId_Add + m_CategoryDisplays.DisplayTypeId.ToString();

                        m_CategoryDisplays.DisplayOrder = m_Categories.DisplayOrder;
                        //m_CategoryDisplays.Insert(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                    else
                    {
                        if (m_DisplayTypeId_Del != "") m_DisplayTypeId_Del = m_DisplayTypeId_Del + ",";
                        m_DisplayTypeId_Del = m_DisplayTypeId_Del + m_CategoryDisplays.DisplayTypeId.ToString();

                        //m_CategoryDisplays.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
                m_CategoryDisplays.AddAndDel(ConstantHelpers.Replicated, ActUserId, m_DisplayTypeId_Add, m_DisplayTypeId_Del, ref SysMessageId);
            }
            lblMsg.Text = "Done";
            lblMsg.Visible = true;
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }

    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);
        BindData();
    }
   
    protected void ddlOrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
