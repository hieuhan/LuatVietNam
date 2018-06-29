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
	public class RadioButtonListHelpers
	{
        //-------------------------------------------------------------------
        public static void Bind(RadioButtonList m_RadioButtonList, object l_Data, string m_TextOptionAll)
        {

            try
            {
                m_RadioButtonList.DataSource = l_Data;
                m_RadioButtonList.DataBind();
                AddItem(m_RadioButtonList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //---------------------------------------------------------------------------------------
        public static void SetSelected(RadioButtonList m_RadioButtonList, string m_SelectedValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(m_SelectedValue))
                {
                    m_RadioButtonList.SelectedIndex = -1;
                    for (int index = 0; index < m_RadioButtonList.Items.Count; index++)
                    {
                        if (m_RadioButtonList.Items[index].Value == m_SelectedValue)
                        {
                            m_RadioButtonList.Items[index].Selected = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void AddItem(RadioButtonList m_RadioButtonList, string m_ItemText, string m_ItemValue)
        {

            try
            {
                m_ItemText = (m_ItemText == null) ? "" : m_ItemText;
                m_ItemValue = (m_ItemValue == null) ? "" : m_ItemValue;
                if (m_ItemText.Length > 0 && m_ItemValue.Length > 0)
                {
                    m_RadioButtonList.Items.Insert(0, new ListItem(m_ItemText, m_ItemValue));
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
