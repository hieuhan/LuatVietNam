using ICSoft.HelperLib;
using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages_lawsdocs_Docs_Valid_Identity_IssueDate : System.Web.UI.Page
{
    private string Identity { get; set; }
    private string IssueDate { get; set; }

    private byte DocGroupId { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Identity"] != null && Request.Params["Identity"].ToString() != "")
        {
            Identity = Request.Params["Identity"].ToString();
        }

        if (Request.Params["IssueDate"] != null && Request.Params["IssueDate"].ToString() != "")
        {
            IssueDate = Request.Params["IssueDate"].ToString();
        }
        if (Request.Params["DocGroupId"] != null && Request.Params["DocGroupId"].ToString() != "")
        {
            DocGroupId = byte.Parse(Request.Params["DocGroupId"].ToString());
        }
        List<Docs> listDocs = new List<Docs>();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Identity) && !string.IsNullOrEmpty(IssueDate))
            {

                int RowCount = 0, ActUserId = SessionHelpers.GetUserId();
                int RowAmount = 10; string OrderBy = string.Empty;
                byte LanguageVNId = 1, ReviewStatusId = 0;
                Identity = Identity.Trim();
                Identity = Identity.Replace("Đ", "D");
                Identity = Identity.Replace("đ", "d");
                Identity = Identity.Replace("u", "ư");
                Identity = Identity.Replace("U", "Ư");
                //IssueDate = "";
                listDocs = new Docs().GetPage(ActUserId, RowAmount, 0, OrderBy, LanguageVNId, "", 0, 0,
                                                                    0, "", "", Identity, 0, 0, 0, 0, 0, 0, 0,
                                                                    0, 0, 0, ReviewStatusId, 0, 0, 0, 0, 0, "IssueDate", IssueDate, IssueDate, ref RowCount);
                    
            }
        } 
    }
    [System.Web.Services.WebMethod]
    public static string checkData(string Identity_, string IssueDate_, byte DocGroupId_)
    {
        if (!string.IsNullOrEmpty(Identity_) && !string.IsNullOrEmpty(IssueDate_))
        {
            List<Docs> listDocs = new List<Docs>();
            Docs mDocs = new Docs();
            int RowCount = 0, ActUserId = SessionHelpers.GetUserId();
            int RowAmount = 10; string OrderBy = string.Empty;
            byte LanguageVNId = 1, ReviewStatusId = 0;
            Identity_ = Identity_.Trim();
            Identity_ = Identity_.Replace("Đ", "D");
            Identity_ = Identity_.Replace("đ", "d");
            Identity_ = Identity_.Replace("u", "ư");
            Identity_ = Identity_.Replace("U", "Ư");
            //IssueDate_ = "";
            mDocs.DocGroupId = DocGroupId_;
            listDocs = mDocs.GetPage(ActUserId, RowAmount, 0, OrderBy, LanguageVNId, "", 0, 0,
                                                                0, "", "", Identity_, 0, 0, 0, 0, 0, 0, 0,
                                                                0, 0, 0, ReviewStatusId, 0, 0, 0, 0, 0, "IssueDate", IssueDate_, IssueDate_, ref RowCount);
            if (listDocs.Count > 0)
            {
                return "true"; 
            } 
        } 
        return "";
    }
}