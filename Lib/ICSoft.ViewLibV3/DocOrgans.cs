using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocOrgans
    {
        public int DocId { get; set; }
        public short OrganId { get; set; }
        //-----------------------------------------------------------------
        public static List<DocOrgans> Static_Init(SmartDataReader smartReader)
        {
            List<DocOrgans> l_DocOrgans = new List<DocOrgans>();
            DocOrgans m_DocOrgans;
            while (smartReader.Read())
            {
                m_DocOrgans = new DocOrgans();
                m_DocOrgans.DocId = smartReader.GetInt32("DocId");
                m_DocOrgans.OrganId = smartReader.GetInt16("OrganId");

                l_DocOrgans.Add(m_DocOrgans);
            }
            return l_DocOrgans;
        }
        //-----------------------------------------------------------------
        public static List<DocOrgans> Static_GetByDocId(int DocId, List<DocOrgans> list)
        {
            List<DocOrgans> RetVal = list.FindAll(i => i.DocId == DocId);
            return RetVal == null ? new List<DocOrgans>() : RetVal;
        }
    }
}
