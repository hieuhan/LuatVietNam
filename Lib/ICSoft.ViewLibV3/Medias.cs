using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Medias
    {
        public int MediaId { get; set; }
        byte MediaTypeId { get; set; }
        public string MediaName { get; set; }
        public string MediaDesc { get; set; }
        public string FilePath { get; set; }
        //-----------------------------------------------------------------
        public static List<Medias> Static_Init(SmartDataReader smartReader)
        {
            List<Medias> list = new List<Medias>();
            Medias m_Data;
            while (smartReader.Read())
            {
                m_Data = new Medias();
                m_Data.MediaId = smartReader.GetInt32("MediaId");
                m_Data.MediaTypeId = smartReader.GetByte("MediaTypeId");
                m_Data.MediaName = smartReader.GetString("MediaName");
                m_Data.MediaDesc = smartReader.GetString("MediaDesc");
                m_Data.FilePath = smartReader.GetString("FilePath");

                list.Add(m_Data);
            }
            return list;
        }
        //-------------------------------------------------------------- 
        public string GetUrl()
        {
            string RetVal = FilePath;
            if (!RetVal.StartsWith("http")) RetVal = Constants.WEBSITE_IMAGEDOMAIN + RetVal;

            return RetVal;
        }
    }
}
