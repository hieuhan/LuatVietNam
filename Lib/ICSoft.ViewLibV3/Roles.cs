using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Roles
    {
        public string RoleCode { get; set; }
        public string RoleDesc { get; set; }
        public byte RoleId { get; set; }
        public string RoleName { get; set; }
        //-----------------------------------------------------------------
        public static List<Roles> Static_Init(SmartDataReader smartReader)
        {
            List<Roles> list = new List<Roles>();
            Roles m_Object;
            while (smartReader.Read())
            {
                m_Object = new Roles();
                m_Object.RoleId = smartReader.GetByte("RoleId");
                m_Object.RoleCode = smartReader.GetString("RoleCode");
                m_Object.RoleName = smartReader.GetString("RoleName");
                m_Object.RoleDesc = smartReader.GetString("RoleDesc");

                list.Add(m_Object);
            }
            return list;
        }
    }
}
