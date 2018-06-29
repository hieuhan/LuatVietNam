using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocRelates
    {
        public string DisplayPosition { get; set; }
        public byte DocGroupId { get; set; }
        public int DocId { get; set; }
        public string DocIdentity { get; set; }
        public string DocName { get; set; }
        public string DocUrl { get; set; }
        public DateTime EffectDate { get; set; }
        public byte EffectStatusId { get; set; }
        public string EffectStatusName { get; set; }
        public DateTime IssueDate { get; set; }
        public short IssueYear { get; set; }
        public byte RelateTypeId { get; set; }
        public string RelateTypeName { get; set; }
        //-----------------------------------------------------------------
        public static List<DocRelates> Static_Init(SmartDataReader smartReader)
        {
            List<DocRelates> list = new List<DocRelates>();
            DocRelates m_Object;
            while (smartReader.Read())
            {
                m_Object = new DocRelates();
                m_Object.DocId = smartReader.GetInt32("DocId");
                m_Object.RelateTypeId = smartReader.GetByte("RelateTypeId");
                m_Object.DisplayPosition = smartReader.GetString("DisplayPosition");
                m_Object.RelateTypeName = smartReader.GetString("RelateTypeName");
                m_Object.DocName = smartReader.GetString("DocName");
                m_Object.DocUrl = smartReader.GetString("DocUrl");
                m_Object.DocGroupId = smartReader.GetByte("DocGroupId");
                m_Object.IssueYear = smartReader.GetInt16("IssueYear");
                m_Object.IssueDate = smartReader.GetDateTime("IssueDate");
                m_Object.EffectDate = smartReader.GetDateTime("EffectDate");
                m_Object.EffectStatusId = smartReader.GetByte("EffectStatusId");

                list.Add(m_Object);
            }
            return list;
        }
        //-----------------------------------------------------------------
        public static List<DocRelates> Static_GetByRelateTypeId(byte RelateTypeId, List<DocRelates> list)
        {
            List<DocRelates> RetVal = list.FindAll(i => i.RelateTypeId == RelateTypeId);
            return RetVal == null ? new List<DocRelates>() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<DocRelates> Static_GetByDisplayPosition(string DisplayPosition, List<DocRelates> list)
        {
            List<DocRelates> RetVal = list.FindAll(i => i.DisplayPosition == DisplayPosition);
            return RetVal == null ? new List<DocRelates>() : RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetDocUrl(string TabName)
        {
            string RetVal = this.DocUrl + "#" + TabName;
            if (!RetVal.StartsWith("http") && !RetVal.StartsWith("/")) RetVal = string.Concat(Constants.ROOT_PATH, RetVal);
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static string Static_GetDocUrl(string DocUrl, string TabName)
        {
            string RetVal = DocUrl + "#" + TabName;
            if (!RetVal.StartsWith("http") && !RetVal.StartsWith("/")) RetVal = string.Concat(Constants.ROOT_PATH, RetVal);
            return RetVal;
        }
        //-------------------------------------------------------------- 
    }
}
