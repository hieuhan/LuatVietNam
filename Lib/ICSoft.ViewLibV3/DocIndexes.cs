using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocIndexes
    {
        public string Title { get; set; }
        public string Bookmark { get; set; }
        public byte LevelId { get; set; }
        //-----------------------------------------------------------------
        public static List<DocIndexes> Static_Init(SmartDataReader smartReader)
        {
            List<DocIndexes> list = new List<DocIndexes>();
            DocIndexes m_Object;
            while (smartReader.Read())
            {
                m_Object = new DocIndexes();
                m_Object.Title = smartReader.GetString("Title");
                m_Object.Bookmark = smartReader.GetString("Bookmark");
                m_Object.LevelId = smartReader.GetByte("LevelId");

                list.Add(m_Object);
            }
            return list;
        }
    }
}
