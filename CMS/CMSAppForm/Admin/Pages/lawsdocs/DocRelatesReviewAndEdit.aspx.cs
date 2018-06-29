using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ICSoft.CMSLib;
using ICSoft.HelperLib;

using sms.admin.security;
using ICSoft.LawDocsLib;
public partial class Admin_DocRelatesReviewAndEdit : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected int DocId = 0;
    protected int DocRelateId = 0;
    protected byte LanguageId = 0;
    protected int EditIndex = -1;
    public char chr;
    protected List<Users> l_Users;
    protected List<RelateTypes> l_RelateTypes = new List<RelateTypes>();
    private List<Languages> l_Language = new List<Languages>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (!IsPostBack)
        {
            if (Request.Params["LanguageId"] != null && Request.Params["LanguageId"].ToString() != "")
            {
                LanguageId = byte.Parse(Request.Params["LanguageId"].ToString());
            }
            if (Request.Params["DocId"] != null && Request.Params["DocId"].ToString() != "")
            {
                DocId = int.Parse(Request.Params["DocId"].ToString());
                lblDocId.Text = Request.Params["DocId"].ToString();
            }
            SetState("DocRef", null);
            BindData(-1);
        }
        if (!IsPostBack || CustomPaging.ChangePage )
        {
            SetState("DocRef", null);
            BindData(-1);
        }
    }

    private void BindData(int index)
    {
        try
        {
            Docs m_Docs = new Docs();
            DocRelates m_DocRelates = new DocRelates();
            l_RelateTypes = RelateTypes.Static_GetList();
            chr = Convert.ToChar(39);
            DataSet ds;
            //Users m_Users = new Users();
            //l_Users = m_Users.GetAll();

            ds = m_Docs.Docs_GetProperties(ActUserId, LanguageId, int.Parse(lblDocId.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblDocNameEdit.Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                lblDocNameEditREL.Text = ds.Tables[0].Rows[0]["DocName"].ToString();
            }
            else
            {
                lblDocNameEdit.Text = "";
                lblDocNameEditREL.Text = "";
            }
            int RowCount = 0;
            string m_OrderBy = "";
            int m_DocId = int.Parse(lblDocId.Text);
            byte m_ReviewStatusId = 0;
            string m_RelateTypeId = "";
            int m_CrUserId = 0;
            string m_DateFrom="";
            string m_DateTo = "";
            m_grid_REL.EditIndex = index;
            m_grid_REL.DataSource = m_DocRelates.DocRelates_GetPageByDocId(ActUserId, m_grid_REL.PageSize, CustomPaging.PageIndex - 1, m_OrderBy, LanguageId, m_DocId, m_RelateTypeId, m_ReviewStatusId, m_CrUserId, m_DateFrom, m_DateTo, ref RowCount);
            m_grid_REL.DataBind();
            m_grid_REL.HeaderRow.TableSection = TableRowSection.TableHeader;
            //lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid_REL.PageSize == 0) ? RowCount / m_grid_REL.PageSize : (RowCount - RowCount % m_grid_REL.PageSize) / m_grid_REL.PageSize + 1;
           
        }
        catch (Exception ex)
        {
            
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    //--------------------------------------------------------
    protected void m_grid_REL_RowEditing(object sender, GridViewEditEventArgs e)
    {
        EditIndex = e.NewEditIndex;
        BindData(EditIndex);
    }
    //--------------------------------------------------------
    protected void m_grid_REL_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        BindData(-1);
    }
    //--------------------------------------------------------
    protected void m_grid_REL_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            short SysMessageId=0;
            int id = e.RowIndex;
            m_grid_REL.EditIndex = id;
            GridViewRow row = m_grid_REL.Rows[id];
            int updateId = int.Parse(m_grid_REL.DataKeys[id].Value.ToString());
            if (updateId > 0)
            {
                DocRelates m_DocRelates = new DocRelates();
                m_DocRelates.DocRelateId = updateId;
                m_DocRelates.RelateTypeId = Byte.Parse(((DropDownList)row.FindControl("ddlRelateTypes")).SelectedValue);
                m_DocRelates.UpdateRelateTypeId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
        BindData(-1);
    }
    protected void m_grid_DocLinkList_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }

            DocId = int.Parse(m_grid_DocLinkList.DataKeys[e.Row.RowIndex].Value.ToString());

        }
    }

    protected void m_grid_DocLinkList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = (DataSet)GetState("DocRef");
            ds.Tables[0].Rows.RemoveAt(e.RowIndex);
            ds.Tables[0].AcceptChanges();
            SetState("DocRef", ds);
            GridRefBind();
           
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData(-1);
    }

    private void SetState(string StateName, object StateValue)
    {
         Session.Contents[StateName] = StateValue;
    }

    private object GetState(string StateName)
    {
        return Session.Contents[StateName];
    }
    protected void lbAddVBCancu_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBCancu.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_BASE"]));
            txtVBCancu.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    public void AddDatasetTemp(int RowAmount, string DocIdentity, byte ReviewStatusId, byte LanguageId, byte RelateTypeId)
    {
        l_RelateTypes = RelateTypes.Static_GetList();
        Docs m_Docs = new Docs();
        DataSet ds;
        ds = m_Docs.Docs_GetTopByDocIdentity(ActUserId, RowAmount , "", LanguageId, DocIdentity.Replace(",",";") , ReviewStatusId);
        DatasetAddByColumn(ds, RelateTypeId);
        GridRefInsert(ds);
    
    }
    public void GridRefInsert(DataSet ds)
    {
        if (GetState("DocRef") == null)
        {
            SetState("DocRef", ds);
        }
        else
        {
            DataSet _ds = default(DataSet);
            _ds =(DataSet)GetState("DocRef");
            DataRow d = null;
            foreach (DataRow _myrow in ds.Tables[0].Rows)
            {
                bool ok = true;
                foreach (DataRow _mycurRow in _ds.Tables[0].Rows)
                {
                    if ((_myrow["DocId"] == _mycurRow["DocId"]) )
                    {
                        //Xoá liên kết văn bản đã chọn và thêm vào liên kết mới nhất
                        ok = false;
                        d = _mycurRow;
                        if ((d != null))
                        {
                            _ds.Tables[0].Rows.Remove(d);
                            _ds.Tables[0].AcceptChanges();
                        }

                        _ds.Tables[0].ImportRow(_myrow);
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (ok)
                {
                    _ds.Tables[0].ImportRow(_myrow);
                }
            }
            _ds.Dispose();
        }

        DataSet dsView = default(DataSet);
        if ((GetState("DocRef") != null))
        {
            dsView =(DataSet)GetState("DocRef");           
            DataView dv = new DataView(dsView.Tables[0]);
            dv.Sort = "RelateTypeId";

            m_grid_DocLinkList.DataSource = dv;

            m_grid_DocLinkList.DataBind();
            m_grid_DocLinkList.AllowSorting = true;
            m_grid_DocLinkList.Visible = true;
            dsView.Dispose();
        }

       // GridRefBind(RelateTypeId);
    }
    public void DatasetAddByColumn(DataSet ds, byte RelateTypeId)
    {
        DataColumnCollection columns = ds.Tables[0].Columns;
        if (columns.Contains("RelateTypeId") == false)
        {
            ds.Tables[0].Columns.Add("RelateTypeId", typeof(byte));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["RelateTypeId"] = RelateTypeId;
            }
        }
        else
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["RelateTypeId"] = RelateTypeId;
            }
        }
    }
    public void GridRefBind()
    {
        DataSet dsView = default(DataSet);
        if ((GetState("DocRef") != null))
        {
            dsView = (DataSet)GetState("DocRef");
            DataView dv = new DataView(dsView.Tables[0]);
            dv.Sort = "RelateTypeId";

            m_grid_DocLinkList.DataSource = dv;

            m_grid_DocLinkList.DataBind();
            m_grid_DocLinkList.AllowSorting = true;
            m_grid_DocLinkList.Visible = true;
            dsView.Dispose();
        }
    }
    protected void lbAdd_Click(object sender, EventArgs e)
    {
        short SysMessageId=0;
        DataSet ds;
        DocRelates m_DocRelates = new DocRelates();
        if ((GetState("DocRef") != null))
        {
            ds = (DataSet)GetState("DocRef");
            if (int.Parse(lblDocId.Text) > 0)
            {
                foreach (DataRow myrow in ds.Tables[0].Rows)
                {
                    m_DocRelates.DocId = int.Parse(lblDocId.Text);
                    m_DocRelates.DocReferenceId = int.Parse(myrow["DocId"].ToString());
                    m_DocRelates.RelateTypeId = byte.Parse(myrow["RelateTypeId"].ToString());
                    m_DocRelates.ReviewStatusId = ConstantHelpers.REVIEW_STATUS_PENDING;
                    m_DocRelates.Insert(ConstantHelpers.Replicated, ActUserId,ref SysMessageId);
                }

                BindData(-1);
            }
            else
            {
                return;
            }

        }
        else
        {
            return;
        }

    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {
            DataSet ds;
            ds =(DataSet) GetState("DocRef");
            if ((ds != null))
            {
                ds.Tables[0].Clear();
                ds.Tables[0].AcceptChanges();
            }
            SetState("DocRef", ds);
            txtVBCancu.Text = "";
            txtVBHethieuluc.Text = "";
            txtVBHethieulucmotphan.Text = "";
            txtVBBisuadoi.Text = "";
            txtVBDuochuongdan.Text = "";
            txtVBDuocthamchieu.Text = "";
            txtVBLienquan.Text = "";
            GridRefBind();
    }
   protected void lbAddVBHethieuluc_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBHethieuluc.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_EXPIRED"]));
            txtVBHethieuluc.Text = "";           
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void lbAddVBHethieulucmotphan_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBHethieulucmotphan.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_PARTEXPIRED"]));
            txtVBHethieulucmotphan.Text = "";           
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lbAddVBBisuadoi_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBBisuadoi.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_MODIFY"]));
            txtVBBisuadoi.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lbAddVBDuochuongdan_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBDuochuongdan.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_GUIDE"]));
            txtVBDuochuongdan.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lbAddVBDuocthamchieu_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBDuocthamchieu.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_REF"]));
            txtVBDuocthamchieu.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lbAddVBlienquan_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBLienquan.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_REL"]));
            txtVBLienquan.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }

    protected void m_grid_REL_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            DocRelates m_DocRelates = new DocRelates();
            m_DocRelates.DocRelateId = int.Parse(m_grid_REL.DataKeys[e.RowIndex].Value.ToString());
            SysMessageTypeId = m_DocRelates.Delete(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
        BindData(-1);
    }

    protected void m_grid_REL_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDeleteREL = (LinkButton)e.Row.FindControl("lbDeleteREL");
            if (lbDeleteREL != null)
            {
                lbDeleteREL.OnClientClick = "return confirm('" + GetLocalResourceObject("DeleteConfirm").ToString() + "');";
            }

            DocRelateId = int.Parse(m_grid_REL.DataKeys[e.Row.RowIndex].Value.ToString());

        }
    }
    protected void lbtnShowDDL_Click(object sender, EventArgs e)
    {         
        if (tblREL.Visible)
        {
            tblREL.Visible=true;
            lbtnShowDDL.Text = lbtnShowDDL.Text.Replace("[-]", "[+]");
            BindData(-1);
        }
        else
        {
            tblREL.Visible = true;
            lbtnShowDDL.Text = lbtnShowDDL.Text.Replace("[+]", "[-]");
            BindData(-1);
        }

    }
    protected void lbAddVBDuochopnhatgoc_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBDuochopnhatgoc.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_MER"]));
            txtVBDuochopnhatgoc.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lbAddVBDuochopnhatsuadoibosung_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBDuochopnhatsuadoibosung.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_MERS"]));
            txtVBDuochopnhatsuadoibosung.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lbAddVBBidinhchimotphanhieuluc_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBBidinhchimotphanhieuluc.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_SUSPENDONE"]));
            txtVBBidinhchimotphanhieuluc.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lbAddVBBidinhchihieuluc_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBBidinhchihieuluc.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_SUSPEND"]));
            txtVBBidinhchihieuluc.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lbAddVBDuocdinhchinh_Click(object sender, EventArgs e)
    {
        try
        {
            AddDatasetTemp(int.Parse(ConfigurationManager.AppSettings["PAGESIZE_GET_TOP_BY_IDENTITY"]), txtVBDuocdinhchinh.Text, ConstantHelpers.REVIEW_STATUS_REVIEW, LanguageId, byte.Parse(ConfigurationManager.AppSettings["DOC_CORRECTION"]));
            txtVBDuocdinhchinh.Text = "";
        }
        catch (Exception ex)
        {

            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
    protected void lbReview_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_REVIEW);
        BindData(-1);
    }
    protected void lbUnCheckLK_Click(object sender, EventArgs e)
    {
        Review_Click(ConstantHelpers.REVIEW_STATUS_UNREVIEW);
        BindData(-1);
    }

    private void Review_Click(byte ReviewStatusId)
    {
        byte SysMessageTypeId = 0;
        short SysMessageId = 0;
        try
        {
            DocRelates m_DocRelates = new DocRelates();
            foreach (GridViewRow m_Row in m_grid_REL.Rows)
            {
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if (chkAction != null)
                {
                    if (chkAction.Checked)
                    {
                        m_DocRelates.DocRelateId = int.Parse(m_grid_REL.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_DocRelates.ReviewStatusId = ReviewStatusId;
                        SysMessageTypeId = m_DocRelates.UpdateReviewStatusId(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message, this);
        }
    }
}

