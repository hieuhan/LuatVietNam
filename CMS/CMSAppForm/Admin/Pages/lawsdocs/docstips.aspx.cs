using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSoft.LawDocsLib;
using ICSoft.HelperLib;
using sms.utils;
using sms.database;

public partial class Admin_Pages_lawsdocs_docstips : System.Web.UI.Page
{
    protected int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
         ActUserId = SessionHelpers.GetUserId();
         if (Request.Params["DocId"] != "" && Request.Params["LanguageId"]!="")
         {
             lbTip.Text = GetProperties(ActUserId, byte.Parse(Request.Params["LanguageId"]), int.Parse(Request.Params["DocId"]));
         }

    }

    private string  GetProperties(int ActUserId, byte LanguageId, int DocId)
    {
        string RetVal="";
        string textval = "Ngày ban hành:";
        Docs m_Docs = new Docs();
        DataSet ds= m_Docs.Docs_GetProperties(ActUserId,LanguageId,DocId);

        if (ds.Tables[0].Rows.Count > 0)
        {
            string a = ds.Tables[0].Rows[0]["DocGroupId"].ToString();
            if (ds.Tables[0].Rows[0]["DocGroupId"].ToString() == "5")
            {
                textval = "Ngày ký xác thực";
            }
                StringBuilder st = new StringBuilder();
                st.Append("<table class='VL_Tip_Property_Tb' cellspacing='0' cellpadding='2'>");
                st.Append("<tr><td class='VL_Tip_Property_C1'>Văn bản:</td>");
                st.Append("<td class='VL_Tip_Property_C2'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["DocName"]) + "</td>");
                st.Append("</tr>");
                st.Append("");
                st.Append("<tr><td class='VL_Tip_Property_C1'>Cơ quan ban hành:</td>");
                st.Append("<td class='VL_Tip_Property_C2'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["OrganName"]) + "</td>");
                st.Append("</tr>");
                st.Append("<tr><td class='VL_Tip_Property_C1'>Lĩnh vực:</td>");
                st.Append("<td class='VL_Tip_Property_C2'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["FieldName"]) + "</td>");
                st.Append("</tr>");
                st.Append("<tr><td class='VL_Tip_Property_C1'>"+textval+"</td>");
            st.Append("<td class='VL_Tip_Property_C2'>" + ( String.IsNullOrEmpty( ds.Tables[0].Rows[0]["IssueDate"].ToString())?  ds.Tables[0].Rows[0]["Fee"]: String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["IssueDate"])) + "</td>");
                st.Append("</tr>");
                st.Append("<tr><td class='VL_Tip_Property_C1'>Ngày hiệu lực:</td>");
                st.Append("<td class='VL_Tip_Property_C2'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["EffectDate"]) + "</td>");
                st.Append("</tr>");
                st.Append("<tr><td class='VL_Tip_Property_C1'>Ngày hết hiệu lực:</td>");
                st.Append("<td class='VL_Tip_Property_C2'>" + String.Format("{0:dd/MM/yyyy}", ds.Tables[0].Rows[0]["ExpireDate"]) + "</td>");
                st.Append("</tr>");
                st.Append("<tr><td class='VL_Tip_Property_C1'>Loại văn bản:</td>");
                st.Append("<td class='VL_Tip_Property_C2'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["DocTypeName"]) + "</td>");
                st.Append("</tr>");
                st.Append("<tr><td class='VL_Tip_Property_C1'>Số hiệu:</td>");
                st.Append("<td class='VL_Tip_Property_C2'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["DocIdentity"]) + "</td>");
                st.Append("</tr>");
                st.Append("<tr><td class='VL_Tip_Property_C1'>Người ký:</td>");
                st.Append("<td class='VL_Tip_Property_C2'>" + String.Format("{0:}", ds.Tables[0].Rows[0]["SignerName"]) + "</td>");
                st.Append("</tr>");
                st.Append("</table>");

                return st.ToString();
        
        }
        return RetVal;
    }
}