using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocFields
    {
        public int DocId { get; set; }
        public short FieldId { get; set; }
        public string FieldDesc { get; set; }
        //-----------------------------------------------------------------
        public static List<DocFields> Static_Init(SmartDataReader smartReader)
        {
            List<DocFields> l_DocFields = new List<DocFields>();
            DocFields m_DocFields;
            while (smartReader.Read())
            {
                m_DocFields = new DocFields();
                m_DocFields.DocId = smartReader.GetInt32("DocId");
                m_DocFields.FieldId = smartReader.GetInt16("FieldId");

                l_DocFields.Add(m_DocFields);
            }
            return l_DocFields;
        }
        //-----------------------------------------------------------------
        public static List<DocFields> Static_GetByDocId(int DocId, List<DocFields> list)
        {
            List<DocFields> RetVal = list.FindAll(i => i.DocId == DocId);
            return RetVal == null ? new List<DocFields>() : RetVal;
        }
    }
}
