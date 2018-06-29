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
	public class CheckBoxListHelpers
	{
        //-------------------------------------------------------------------
        public static void Bind(CheckBoxList m_CheckBoxList, object l_Data, string m_TextOptionAll)
        {

            try
            {
                m_CheckBoxList.DataSource = l_Data;
                m_CheckBoxList.DataBind();
                AddItem(m_CheckBoxList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //---------------------------------------------------------------------------------------
        public static void SetSelected(CheckBoxList m_CheckBoxList, string m_SelectedValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(m_SelectedValue))
                {
                    if (m_SelectedValue.Contains(","))
                    {
                        string[] arrValue = m_SelectedValue.Split(',');
                        for (int j = 0; j < arrValue.Length; j++)
                        {
                            for (int index = 0; index < m_CheckBoxList.Items.Count; index++)
                            {
                                if (m_CheckBoxList.Items[index].Value == arrValue[j])
                                {
                                    m_CheckBoxList.Items[index].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int index = 0; index < m_CheckBoxList.Items.Count; index++)
                        {
                            if (m_CheckBoxList.Items[index].Value == m_SelectedValue)
                            {
                                m_CheckBoxList.Items[index].Selected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //---------------------------------------------------------------------------------------
        public static void SetSelected(CheckBoxList m_CheckBoxList, string m_SelectedValue, string m_HighLightSelectedColor)
        {
            try
            {
                if (!string.IsNullOrEmpty(m_SelectedValue))
                {
                    //m_CheckBoxList.SelectedIndex = -1;
                    for (int index = 0; index < m_CheckBoxList.Items.Count; index++)
                    {
                        if (m_CheckBoxList.Items[index].Value == m_SelectedValue)
                        {
                            m_CheckBoxList.Items[index].Selected = true;
                            string style = m_CheckBoxList.Items[index].Attributes["style"];
                            style += "background:" + m_HighLightSelectedColor + ";";
                            m_CheckBoxList.Items[index].Attributes["style"] = style;
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
        public static void AddItem(CheckBoxList m_CheckBoxList, string m_ItemText, string m_ItemValue)
        {

            try
            {
                m_ItemText = (m_ItemText == null) ? "" : m_ItemText;
                m_ItemValue = (m_ItemValue == null) ? "" : m_ItemValue;
                if (m_ItemText.Length > 0 && m_ItemValue.Length > 0)
                {
                    m_CheckBoxList.Items.Insert(0, new ListItem(m_ItemText, m_ItemValue));
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
