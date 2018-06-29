using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class TagHelpers
    {
        public static List<Tags> Init(SmartDataReader smartReader)
        {
            List<Tags> l_Tags = new List<Tags>();
            Tags m_Tags;
            while (smartReader.Read())
            {
                m_Tags = new Tags();
                m_Tags.TagId = smartReader.GetInt16("TagId");
                m_Tags.TagName = smartReader.GetString("TagName");

                l_Tags.Add(m_Tags);
            }
            return l_Tags;
        }
        //-----------------------------------------------------------------
        public static Tags InitOne(SmartDataReader smartReader)
        {
            List<Tags> l_Tags = Init(smartReader);
            if (l_Tags.Count > 0) return l_Tags[0];
            return null;
        }
    }
}
