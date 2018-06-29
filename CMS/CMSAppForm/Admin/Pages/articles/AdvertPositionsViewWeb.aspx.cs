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

public partial class Admin_AdvertPositionsViewWeb : System.Web.UI.Page
{
    private short SiteId = 0;
    private short CategoryId = 0;
    private int ActUserId = 0;
    protected string AdvFile = "";
    protected string AdvCode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ActUserId = SessionHelpers.GetUserId();
        if (Request.Params["SiteId"] != null && Request.Params["SiteId"].ToString() != "")
        {
            SiteId = short.Parse(Request.Params["SiteId"].ToString());
        }
        if (Request.Params["CategoryId"] != null && Request.Params["CategoryId"].ToString() != "")
        {
            CategoryId = short.Parse(Request.Params["CategoryId"].ToString());
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
            
            AdvertPositionAdverts m_AdvertPositionAdverts = new AdvertPositionAdverts();
            DataSet m_DataSet = m_AdvertPositionAdverts.GetScriptCode(SiteId, 0);
            if (m_DataSet.Tables.Count > 0)
            {
                foreach (DataRow mRow in m_DataSet.Tables[0].Rows)
                {
                    if (CategoryId.ToString() == mRow["CategoryId"].ToString())
                    {
                        if (AdvCode != "") AdvCode = AdvCode + "\r\n";
                        if (CategoryId == 0)
                        {
                            AdvFile = CmsConstants.ROOT_PATH + "js/advs/advs_site_" + SiteId.ToString() + ".js";
                            AdvCode = AdvCode + "<br /><br >Vi tri: " + AdvertPositions.Static_Get(int.Parse(mRow["AdvertPositionId"].ToString())).PositionDesc + "<br /><br >";
                            AdvCode = AdvCode + "<script type=\"text/javascript\">if (typeof advsite_" + mRow["AdvertPositionId"].ToString() + " != 'undefined') document.write(advsite_" + mRow["AdvertPositionId"].ToString() + ");</script>";
                        }
                        else
                        {
                            AdvFile = CmsConstants.ROOT_PATH + "js/advs/advs_cate_" + CategoryId.ToString() + ".js";
                            AdvCode = AdvCode + "<br /><br >Vi tri: " + AdvertPositions.Static_Get(int.Parse(mRow["AdvertPositionId"].ToString())).PositionDesc + "<br /><br >";
                            AdvCode = AdvCode + "<script type=\"text/javascript\">if (typeof advcate_" + mRow["AdvertPositionId"].ToString() + " != 'undefined') document.write(advsite_" + mRow["AdvertPositionId"].ToString() + ");</script>";
                        }
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
}