using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using System.Data;
using System.IO;

public partial class Admin_AdvertPositionsViewJs : System.Web.UI.Page
{
    private short SiteId = 0;
    private int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
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
            string AdvFile = "";
            string m_CategoryId = "-1";
            int count = 0;
            DataRow mRowUrl;
            DataTable tblUrl = new DataTable();
            tblUrl.Columns.Add(new DataColumn("JsUrl"));
            tblUrl.Columns.Add(new DataColumn("SiteId"));
            tblUrl.Columns.Add(new DataColumn("CategoryId"));

            AdvertPositionAdverts m_AdvertPositionAdverts = new AdvertPositionAdverts();
            DataSet m_DataSet = m_AdvertPositionAdverts.GetScriptCode(SiteId, 0);
            if (m_DataSet.Tables.Count > 0)
            {
                foreach (DataRow mRow in m_DataSet.Tables[0].Rows)
                {
                    if (m_CategoryId != mRow["CategoryId"].ToString())
                    {
                        if (count > 0 && m_CategoryId != "0")
                        {
                            AdvFile = "js/advs/advs_cate_" + m_CategoryId.ToString() + ".js";
                            mRowUrl = tblUrl.NewRow();
                            mRowUrl[0] = AdvFile;
                            mRowUrl[1] = SiteId.ToString();
                            mRowUrl[2] = m_CategoryId.ToString();
                            tblUrl.Rows.Add(mRowUrl);
                        }

                        count++;
                        m_CategoryId = mRow["CategoryId"].ToString();
                    }

                }

                AdvFile = "js/advs/advs_site_" + SiteId.ToString() + ".js";
                mRowUrl = tblUrl.NewRow();
                mRowUrl[0] = AdvFile;
                mRowUrl[1] = SiteId.ToString();
                mRowUrl[2] = m_CategoryId.ToString();
                tblUrl.Rows.Add(mRowUrl);

                AdvFile = "js/advs/advs_cate_" + m_CategoryId.ToString() + ".js";
                mRowUrl = tblUrl.NewRow();
                mRowUrl[0] = AdvFile;
                mRowUrl[1] = SiteId.ToString();
                mRowUrl[2] = m_CategoryId.ToString();
                if (m_CategoryId != "0" && m_CategoryId != "-1") tblUrl.Rows.Add(mRowUrl);

                m_grid.DataSource = tblUrl;
                m_grid.DataBind();
            }
            
        }
        catch (Exception ex)
        {
            sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            JSAlertHelpers.Alert(ex.Message.ToString(), this);
        }
    }
}