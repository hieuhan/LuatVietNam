using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocSigners
    {
        public int DocId { get; set; }
        public short SignerId { get; set; }
        //-----------------------------------------------------------------
        public static List<DocSigners> Static_Init(SmartDataReader smartReader)
        {
            List<DocSigners> l_DocSigners = new List<DocSigners>();
            DocSigners m_DocSigners;
            while (smartReader.Read())
            {
                m_DocSigners = new DocSigners();
                m_DocSigners.DocId = smartReader.GetInt32("DocId");
                m_DocSigners.SignerId = smartReader.GetInt16("SignerId");

                l_DocSigners.Add(m_DocSigners);
            }
            return l_DocSigners;
        }
        //-----------------------------------------------------------------
        public static List<DocSigners> Static_GetByDocId(int DocId, List<DocSigners> list)
        {
            List<DocSigners> RetVal = list.FindAll(i => i.DocId == DocId);
            return RetVal == null ? new List<DocSigners>() : RetVal;
        }
    }
}
