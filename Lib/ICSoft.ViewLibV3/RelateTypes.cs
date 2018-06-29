using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class RelateTypes
    {
        public string DisplayPosition { get; set; }
        public byte RelateTypeId { get; set; }
        public string RelateTypeName { get; set; }
        public string RelateTypeDesc { get; set; }
        public string RevertRelateTypeDesc { get; set; }
        public string RevertRelateTypeName { get; set; }
        public int DocCount { get; set; }
        //-----------------------------------------------------------------
        public static List<RelateTypes> Static_Init(SmartDataReader smartReader, byte GetDocCount = 0)
        {
            List<RelateTypes> list = new List<RelateTypes>();
            RelateTypes m_Object;
            while (smartReader.Read())
            {
                m_Object = new RelateTypes();
                m_Object.RelateTypeId = smartReader.GetByte("RelateTypeId");
                m_Object.RelateTypeName = smartReader.GetString("RelateTypeName");
                m_Object.DisplayPosition = smartReader.GetString("DisplayPosition");
                if (GetDocCount == 1) m_Object.DocCount = smartReader.GetInt32("Soluong");

                list.Add(m_Object);
            }
            return list;
        }
        //-----------------------------------------------------------------
        public static RelateTypes Static_GetById(short RelateTypeId, List<RelateTypes> list)
        {
            RelateTypes RetVal = list.Find(i => i.RelateTypeId == RelateTypeId);
            return RetVal == null ? new RelateTypes() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<RelateTypes> Static_GetListByDisplayPosition(string DisplayPosition, List<RelateTypes> list)
        {
            List<RelateTypes> RetVal = list.FindAll(i => i.DisplayPosition == DisplayPosition);
            return RetVal == null ? new List<RelateTypes>() : RetVal;
        }
        //-----------------------------------------------------------
        //Danh sach Loai lien ket theo nhom van ban. Su dung trong trang hien thi luoc do van ban
        //Lay ve toan bo danh sach, roi loc theo ham Static_GetByDisplayPosition de tranh connect nhieu vao DB
        public static List<RelateTypes> Static_GetListByDocGroupId(byte DocGroupId = 1, string DisplayPosition = "")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<RelateTypes> RetVal = new List<RelateTypes>();
            try
            {
                SqlCommand cmd = new SqlCommand("RelateTypes_GetByDocGroupId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@DisplayPosition", DisplayPosition));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = RelateTypes.Static_Init(smartReader);
                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
    }
}
