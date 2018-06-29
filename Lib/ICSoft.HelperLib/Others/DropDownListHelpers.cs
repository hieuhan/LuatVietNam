using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using System.Threading;
using sms.admin.security;
using Organs = ICSoft.LawDocsLib.Organs;

namespace ICSoft.HelperLib
{
	public class DropDownListHelpers
	{
        //---------------------------------------------------------------------------------------
        public static void BindDropDownList(DropDownList m_DropDownList, object l_Data, string CurentValue = "")
        {
            
            try
            {
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                if (string.IsNullOrEmpty(CurentValue) == false && CurentValue != "0")
                {
                    m_DropDownList.SelectedIndex = -1;
                    for (int index = 0; index < m_DropDownList.Items.Count; index++)
                    {
                        if (m_DropDownList.Items[index].Value == CurentValue)
                        {
                            m_DropDownList.Items[index].Selected = true;
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
        public static void DDL_SetSelected(DropDownList m_DropDownList, string m_SelectedValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(m_SelectedValue))
                {
                    m_DropDownList.SelectedIndex = -1;
                    for (int index = 0; index < m_DropDownList.Items.Count; index++)
                    {
                        if (m_DropDownList.Items[index].Value == m_SelectedValue)
                        {
                            m_DropDownList.Items[index].Selected = true;
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
        public static void DDL_AddItem(DropDownList m_DropDownList, string m_ItemText, string m_ItemValue)
        {

            try
            {
                m_ItemText = (m_ItemText == null) ? "" : m_ItemText;
                m_ItemValue = (m_ItemValue == null) ? "" : m_ItemValue;
                if (m_ItemText.Length > 0 && m_ItemValue.Length > 0)
                {
                    m_DropDownList.Items.Insert(0, new ListItem(m_ItemText, m_ItemValue));
                }
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDL_Bind(DropDownList m_DropDownList, object l_Data, string m_TextOptionAll)
        {

            try
            {
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDL_Bind(DropDownList m_DropDownList, object l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDL_Bind(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLReviewStatus_BindByCulture(DropDownList m_DropDownList, List<ReviewStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "ReviewStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "ReviewStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLServices_BindByCulture(DropDownList m_DropDownList, List<Services> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "ServiceDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "ServiceName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLCustomerGroups_BindByCulture(DropDownList m_DropDownList, List<CustomerGroups> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "CustomerGroupDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "CustomerGroupName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLTransactionStatus_BindByCulture(DropDownList m_DropDownList, List<TransactionStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "TransactionStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "TransactionStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLPaymentTypes_BindByCulture(DropDownList m_DropDownList, List<PaymentTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "PaymentTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "PaymentTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLServicePackages_BindByCulture(DropDownList m_DropDownList, List<ServicePackages> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "ServicePackageDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "ServicePackageName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDocTypes_BindByCulture(DropDownList m_DropDownList, List<DocTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "DocTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "DocTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDocTypeDisplays_BindByCulture(DropDownList m_DropDownList, List<DocTypeDisplays> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "DocTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "DocTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLFields_BindByCulture(DropDownList m_DropDownList, List<Fields> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "FieldDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "FieldName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLOrgans_BindByCulture(DropDownList m_DropDownList, List<Organs> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "OrganDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "OrganName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDataSources_BindByCulture(DropDownList m_DropDownList, List<DataSources> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "DataSourceDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "DataSourceName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLEffectStatus_BindByCulture(DropDownList m_DropDownList, List<EffectStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "EffectStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "EffectStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLUseStatus_BindByCulture(DropDownList m_DropDownList, List<UseStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "UseStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "UseStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLSigners_BindByCulture(DropDownList m_DropDownList, List<Signers> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "SignerName";
                }
                else
                {
                    m_DropDownList.DataTextField = "SignerNameClear";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLRegencies_BindByCulture(DropDownList m_DropDownList, List<Regencies> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "RegencyDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "RegencyName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLOrganTypes_BindByCulture(DropDownList m_DropDownList, List<OrganTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "OrganTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "OrganTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDisplayTypes_BindByCulture(DropDownList m_DropDownList, List<DisplayTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "DisplayTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "DisplayTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLRelateTypeGroups_BindByCulture(DropDownList m_DropDownList, List<RelateTypeGroups> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "RelateTypeGroupDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "RelateTypeGroupName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLRelateTypes_BindByCulture(DropDownList m_DropDownList, List<RelateTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "RelateTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "RelateTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLPageTypes_BindByCulture(DropDownList m_DropDownList, List<PageTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "PageTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "PageTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLProvinces_BindByCulture(DropDownList m_DropDownList, List<Provinces> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "ProvinceDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "ProvinceName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLInfoChannels_BindByCulture(DropDownList m_DropDownList, List<InfoChannels> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "InfoChannelDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "InfoChannelName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLOccupations_BindByCulture(DropDownList m_DropDownList, List<Occupations> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "OccupationDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "OccupationName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }

        //-------------------------------------------------------------------
        public static void DDLOrganOccupations_BindByCulture(DropDownList m_DropDownList, List<OrganOccupations> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "OrganOccupationDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "OrganOccupationName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLVoteTypes_BindByCulture(DropDownList m_DropDownList, List<VoteTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "VoteTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "VoteTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLVoteTypes_BindByCulture(DropDownList m_DropDownList, List<VoteTypes> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLVoteTypes_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLServiceTypes_BindByCulture(DropDownList m_DropDownList, List<ServiceTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "ServiceTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "ServiceTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDataTypes_BindByCulture(DropDownList m_DropDownList, List<DataTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "DataTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "DataTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDataTypes_BindByCulture(DropDownList m_DropDownList, List<DataTypes> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLDataTypes_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLFeedBackGroups_BindByCulture(DropDownList m_DropDownList, List<FeedBackGroups> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "FeedBackGroupDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "FeedBackGroupName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLFeedBackGroups_BindByCulture(DropDownList m_DropDownList, List<FeedBackGroups> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLFeedBackGroups_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLServicePakageStatus_BindByCulture(DropDownList m_DropDownList, List<ServicePakageStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "ServicePakageStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "ServicePakageStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLServiceRegistStatus_BindByCulture(DropDownList m_DropDownList, List<ServiceRegistStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "ServiceRegistStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "ServiceRegistStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }      

        //-------------------------------------------------------------------
        public static void DDLReviewStatus_BindByCulture(DropDownList m_DropDownList, List<ReviewStatus> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLReviewStatus_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLSponsorStatus_BindByCulture(DropDownList m_DropDownList, List<SponsorStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "SponsorStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "SponsorStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLMeetingStatus_BindByCulture(DropDownList m_DropDownList, List<MeetingStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "MeetingStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "MeetingStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLMemberStatus_BindByCulture(DropDownList m_DropDownList, List<MemberStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "MemberStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "MemberStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLSponsorStatus_BindByCulture(DropDownList m_DropDownList, List<SponsorStatus> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLSponsorStatus_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLMeetingGroupStatus_BindByCulture(DropDownList m_DropDownList, List<MeetingGroupStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "MeetingGroupStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "MeetingGroupStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLNewsletterStatus_BindByCulture(DropDownList m_DropDownList, List<NewsletterStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "NewsletterStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "NewsletterStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLNewsletterGroups_BindByCulture(DropDownList m_DropDownList, List<NewsletterGroups> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "NewsletterGroupDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "NewsletterGroupName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLNewsletterMemberStatus_BindByCulture(DropDownList m_DropDownList, List<NewsletterMemberStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "NewsletterMemberStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "NewsletterMemberStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLFaqTypes_BindByCulture(DropDownList m_DropDownList, List<FaqTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "FaqTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "FaqTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLFaqGroups_BindByCulture(DropDownList m_DropDownList, List<FaqGroups> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "FaqGroupDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "FaqGroupName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLSendMethods_BindByCulture(DropDownList m_DropDownList, List<SendMethods> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "SendMethodDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "SendMethodName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLSmsProcessStatus_BindByCulture(DropDownList m_DropDownList, List<SmsProcessStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "SmsProcessStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "SmsProcessStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLSmsServices_BindByCulture(DropDownList m_DropDownList, List<SmsServices> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "SmsServiceDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "SmsServiceName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLSendStatus_BindByCulture(DropDownList m_DropDownList, List<SendStatus> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "SendStatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "SendStatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLStatus_BindByCulture(DropDownList m_DropDownList, List<Status> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "StatusDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "StatusName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLActionTypes_BindByCulture(DropDownList m_DropDownList, List<ActionTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "ActionTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "ActionTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLAdverts_BindByCulture(DropDownList m_DropDownList, List<Adverts> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "AdvertName";
                }
                else
                {
                    m_DropDownList.DataTextField = "AdvertName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLAdvertPositions_BindByCulture(DropDownList m_DropDownList, List<AdvertPositions> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "PositionName";
                }
                else
                {
                    m_DropDownList.DataTextField = "PositionName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLLanguage_BindByCulture(DropDownList m_DropDownList, List<Languages> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "LanguageDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "LanguageName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLOrgans_BindByCulture(DropDownList m_DropDownList, List<ICSoft.LawDocsLib.Organs> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLOrgans_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDisplayTypes_BindByCulture(DropDownList m_DropDownList, List<DisplayTypes> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLDisplayTypes_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDocTypes_BindByCulture(DropDownList m_DropDownList, List<DocTypes> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLDocTypes_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLFields_BindByCulture(DropDownList m_DropDownList, List<Fields> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLFields_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLFaqGroups_BindByCulture(DropDownList m_DropDownList, List<FaqGroups> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLFaqGroups_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLLanguage_BindByCulture(DropDownList m_DropDownList, List<Languages> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLLanguage_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLAppType_BindByCulture(DropDownList m_DropDownList, List<ApplicationTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "ApplicationTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "ApplicationTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLAppType_BindByCulture(DropDownList m_DropDownList, List<ApplicationTypes> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLAppType_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLOrderBy_BindByCulture(DropDownList m_DropDownList, List<OrderByClauses> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "OrderByDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "OrderByName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLOrderBy_BindByCulture(DropDownList m_DropDownList, List<OrderByClauses> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLOrderBy_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLServiceTypes_BindByCulture(DropDownList m_DropDownList, List<ServiceTypes> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLServiceTypes_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
       
        //-------------------------------------------------------------------
        public static void DDLSite_BindByCulture(DropDownList m_DropDownList, List<Sites> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "SiteDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "SiteName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLSite_BindByCulture(DropDownList m_DropDownList, List<Sites> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLSite_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLFeatureGroups_BindByCulture(DropDownList m_DropDownList, List<FeatureGroups> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "FeatureGroupDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "FeatureGroupName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLFeatureGroups_BindByCulture(DropDownList m_DropDownList, List<FeatureGroups> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLFeatureGroups_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------

        //-------------------------------------------------------------------
        public static void DDLDataDictionaryType_BindByCulture(DropDownList m_DropDownList, List<DataDictionaryTypes> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "DataDictionaryTypeDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "DataDictionaryTypeName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDataDictionaryType_BindByCulture(DropDownList m_DropDownList, List<DataDictionaryTypes> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLDataDictionaryType_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLLawTerminGroup_BindByCulture(DropDownList m_DropDownList, List<LawTerminGroups> l_Data, string m_TextOptionAll)
        {

            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "LawTerminGroupDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "LawTerminGroupName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLLawTerminGroup_BindByCulture(DropDownList m_DropDownList, List<LawTerminGroups> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLLawTerminGroup_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDocGroups_BindByCulture(DropDownList m_DropDownList, List<DocGroups> l_Data, string m_TextOptionAll)
        {
            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "DocGroupDesc";
                }
                else
                {
                    m_DropDownList.DataTextField = "DocGroupName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLDocGroups_BindByCulture(DropDownList m_DropDownList, List<DocGroups> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLDocGroups_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }

        //-------------------------------------------------------------------
        public static void DDLUsers_BindByCulture(DropDownList m_DropDownList, List<Users> l_Data, string m_TextOptionAll)
        {
            try
            {
                m_DropDownList.DataTextField = "UserName";
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLUsers_BindByCulture(DropDownList m_DropDownList, List<Users> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLUsers_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLLawerGroups_BindByCulture(DropDownList m_DropDownList, List<LawerGroups> l_Data, string m_TextOptionAll)
        {
            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "LawerGroupName";
                }
                else
                {
                    m_DropDownList.DataTextField = "LawerGroupDesc";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLLawerGroups_BindByCulture(DropDownList m_DropDownList, List<LawerGroups> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLLawerGroups_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLLawers_BindByCulture(DropDownList m_DropDownList, List<Lawers> l_Data, string m_TextOptionAll)
        {
            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.Name;
                if (culture == ConstantHelpers.CULTURE_VN)
                {
                    m_DropDownList.DataTextField = "FullName";
                }
                else
                {
                    m_DropDownList.DataTextField = "FullName";
                }
                m_DropDownList.DataSource = l_Data;
                m_DropDownList.DataBind();
                DDL_AddItem(m_DropDownList, m_TextOptionAll, "0");
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
        //-------------------------------------------------------------------
        public static void DDLLawers_BindByCulture(DropDownList m_DropDownList, List<Lawers> l_Data, string m_TextOptionAll, string m_SelectedValue)
        {
            try
            {
                DDLLawers_BindByCulture(m_DropDownList, l_Data, m_TextOptionAll);
                DDL_SetSelected(m_DropDownList, m_SelectedValue);
            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
            }
        }
    }
}
