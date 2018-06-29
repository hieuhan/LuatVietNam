using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ICSoft.CMSLib;
using System.Threading;
namespace ICSoft.HelperLib
{
    public class ListBoxListHelpers
	{
        //-------------------------------------------------------------------
        public static void Bind(ListBox m_ListBox, object l_Data, string m_TextOptionAll)
        {

            try
            {
                m_ListBox.DataSource = l_Data;
                m_ListBox.DataBind();
               // AddItem(m_ListBox, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void AddItem(ListBox m_ListBox, string m_ItemText, string m_ItemValue)
        {

            try
            {
                m_ItemText = (m_ItemText == null) ? "" : m_ItemText;
                m_ItemValue = (m_ItemValue == null) ? "" : m_ItemValue;
                if (m_ItemText.Length > 0 && m_ItemValue.Length > 0)
                {
                    m_ListBox.Items.Insert(0, new ListItem(m_ItemText, m_ItemValue));
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
	}
}
