using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Years
    {
        public short Year { get; set; }
        public int DocCount { get; set; }
        //-----------------------------------------------------------------
        public static List<Years> Static_Init(SmartDataReader smartReader)
        {
            List<Years> l_Years = new List<Years>();
            Years m_Years;
            while (smartReader.Read())
            {
                m_Years = new Years();
                m_Years.Year = smartReader.GetInt16("Year");
                m_Years.DocCount = smartReader.GetInt32("Soluong");

                l_Years.Add(m_Years);
            }
            return l_Years;
        }
    }
}
