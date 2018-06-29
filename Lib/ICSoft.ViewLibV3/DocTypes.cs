using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocTypes
    {
        public byte DocTypeId { get; set; }
        public string DocTypeName { get; set; }
        public string DocTypeDesc { get; set; }
        public int DocCount { get; set; }
        //-----------------------------------------------------------------
        public static List<DocTypes> Static_Init(SmartDataReader smartReader, byte GetDocCount = 0)
        {
            List<DocTypes> l_DocTypes = new List<DocTypes>();
            DocTypes m_DocTypes;
            while (smartReader.Read())
            {
                m_DocTypes = new DocTypes();
                m_DocTypes.DocTypeId = smartReader.GetByte("DocTypeId");
                m_DocTypes.DocTypeName = smartReader.GetString("DocTypeName");
                m_DocTypes.DocTypeDesc = smartReader.GetString("DocTypeDesc");
                if (GetDocCount == 1) m_DocTypes.DocCount = smartReader.GetInt32("Soluong");

                l_DocTypes.Add(m_DocTypes);
            }
            return l_DocTypes;
        }
        //-----------------------------------------------------------------
        public static DocTypes Static_InitOne(SmartDataReader smartReader)
        {
            List<DocTypes> l_DocTypes = Static_Init(smartReader);
            return l_DocTypes.Count > 0 ? l_DocTypes[0] : new DocTypes();
        }
        //-----------------------------------------------------------------
        public static DocTypes Static_GetById(byte DocTypeId, List<DocTypes> list)
        {
            DocTypes RetVal = list.Find(i => i.DocTypeId == DocTypeId);
            return RetVal == null ? new DocTypes() : RetVal;
        }
        //-----------------------------------------------------------
        public static List<DocTypes> Static_GetList(byte ReviewStatusId = 2, byte LanguageId = 1, string FieldSelect = "DocTypeId,DocTypeDesc", string OrderBy = "DisplayOrder")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<DocTypes> RetVal = new List<DocTypes>();
            try
            {
                string sql = "SELECT " + FieldSelect + " FROM " + (LanguageId == 1 ? "DocTypes" : "DocTypeLanguages") + " WHERE (1=1)";
                if (ReviewStatusId > 0) sql = sql + " AND (ReviewStatusId=" + ReviewStatusId.ToString() + ")";
                if (LanguageId > 1) sql = sql + " AND (LanguageId=" + LanguageId.ToString() + ")";
                sql = sql + " ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = DocTypes.Static_Init(smartReader, 0);

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
        //-----------------------------------------------------------
        public static List<DocTypes> Static_GetListByDisplayType(short DisplayTypeId, byte ReviewStatusId = 0, byte LanguageId = 1, string FieldSelect = "DocTypeId,DocTypeDesc", string OrderBy = "DisplayOrder")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<DocTypes> RetVal = new List<DocTypes>();
            try
            {
                FieldSelect = "A." + FieldSelect;
                FieldSelect = FieldSelect.Replace(" ", "").Replace(",", ",A.");
                OrderBy = OrderBy.Replace("DisplayOrder", "B.DisplayOrder");
                OrderBy = OrderBy.Replace("FieldId", "A.FieldId");
                string sql = "SELECT " + FieldSelect + " FROM " + (LanguageId == 1 ? "DocTypes" : "DocTypeLanguages") + " A";
                sql = sql + " INNER JOIN DocTypeDisplays B ON A.DocTypeId=B.DocTypeId";
                sql = sql + " WHERE DisplayTypeId=" + DisplayTypeId.ToString();
                if (ReviewStatusId > 0) sql = sql + " AND (ReviewStatusId=" + ReviewStatusId.ToString() + ")";
                if (LanguageId > 1) sql = sql + " AND (B.LanguageId=" + LanguageId.ToString() + ")";
                sql = sql + " ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = DocTypes.Static_Init(smartReader, 0);

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
        //-----------------------------------------------------------
        public static DocTypes Static_GetById(byte DocTypeId, byte LanguageId = 1)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            DocTypes RetVal = new DocTypes();
            try
            {
                string sql = "SELECT DocTypeId,DocTypeName,DocTypeDesc FROM " + (LanguageId == 1 ? "DocTypes" : "DocTypeLanguages") + " WHERE (DocTypeId=" + DocTypeId.ToString() + ")";
                if (LanguageId > 1) sql = sql + " AND (LanguageId=" + LanguageId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = DocTypes.Static_InitOne(smartReader);

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
