using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
namespace ICSoft.HelperLib
{
    public class EffectStatusHelpers
	{
        //---------------------------------------------------------------------------------------

        public static byte Expired
        {
            get { return 1; }
            set { }
        }
        public static byte PartExpired
        {
            get { return 2; }
            set { }
        }
        public static byte Changed
        {
            get { return 3; }
            set { }
        }
        public static byte Inforced
        {
            get { return 4; }
            set { }
        }
        public static byte Ambiguity
        {
            get { return 5; }
            set { }
        }
        public static string GetDisplayName(object objEffectStatusId, List<EffectStatus> l_EffectStatus)
        {
            string RetVal = "";
            EffectStatus m_EffectStatus = EffectStatus.Static_Get(byte.Parse(objEffectStatusId.ToString()), l_EffectStatus);
            if (LanguageHelpers.GetCurentId() == LanguageHelpers.Vietnamese)
            {
                RetVal = m_EffectStatus.EffectStatusDesc;
            }
            else
            {
                RetVal = m_EffectStatus.EffectStatusName;
            }
            return RetVal;

        }
	}//end ApplicationType
    
}//end namespace service
