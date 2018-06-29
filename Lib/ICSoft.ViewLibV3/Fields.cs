using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ICSoft.ViewLibV3
{
    public class Fields
    {
        public short FieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldDesc { get; set; }
        public int DocCount { get; set; }
        //-----------------------------------------------------------------
        public static List<Fields> Static_Init(SmartDataReader smartReader, byte GetDocCount = 0)
        {
            List<Fields> l_Fields = new List<Fields>();
            Fields m_Fields;
            while (smartReader.Read())
            {
                m_Fields = new Fields();
                m_Fields.FieldId = smartReader.GetInt16("FieldId");
                m_Fields.FieldName = smartReader.GetString("FieldName");
                m_Fields.FieldDesc = smartReader.GetString("FieldDesc");
                if (GetDocCount == 1) m_Fields.DocCount = smartReader.GetInt32("Soluong");

                l_Fields.Add(m_Fields);
            }
            return l_Fields;
        }
        //-----------------------------------------------------------------
        public static Fields Static_InitOne(SmartDataReader smartReader)
        {
            List<Fields> l_Fields = Static_Init(smartReader);
            if (l_Fields.Count > 0) return l_Fields[0];
            return null;
        }
        //-----------------------------------------------------------------
        public static Fields Static_GetById(short FieldId, List<Fields> list)
        {
            Fields RetVal = list.Find(i => i.FieldId == FieldId);
            return RetVal == null ? new Fields() : RetVal;
        }
        //-----------------------------------------------------------
        public static List<Fields> Static_GetList(byte ReviewStatusId = 2, byte LanguageId = 1, string FieldSelect = "FieldId,FieldDesc", string OrderBy = "DisplayOrder")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<Fields> RetVal = new List<Fields>();
            try
            {
                string sql = "SELECT " + FieldSelect + " FROM " + (LanguageId == 1 ? "Fields" : "FieldLanguages") + " WHERE (1=1)";
                if (ReviewStatusId > 0) sql = sql + " AND (ReviewStatusId=" + ReviewStatusId.ToString() + ")";
                if (LanguageId > 1) sql = sql + " AND (LanguageId=" + LanguageId.ToString() + ")";
                sql = sql + " ORDER BY " + OrderBy;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Fields.Static_Init(smartReader, 0);

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
        public static List<Fields> Static_GetListByDisplayType(short DisplayTypeId, byte ReviewStatusId = 0, byte LanguageId = 1, string FieldSelect = "FieldId,FieldDesc", string OrderBy = "DisplayOrder")
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<Fields> RetVal = new List<Fields>();
            try
            {
                FieldSelect = "A." + FieldSelect;
                FieldSelect = FieldSelect.Replace(" ", "").Replace(",", ",A.");
                OrderBy = OrderBy.Replace("DisplayOrder", "B.DisplayOrder");
                OrderBy = OrderBy.Replace("FieldId", "A.FieldId");
                string sql = "SELECT " + FieldSelect + " FROM " + (LanguageId == 1 ? "Fields" : "FieldLanguages") + " A";
                sql = sql + " INNER JOIN FieldDisplays B ON A.FieldId=B.FieldId";
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
                RetVal = Fields.Static_Init(smartReader, 0);

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
        public static Fields Static_GetById(short FieldId, byte LanguageId = 1)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            Fields RetVal = new Fields();
            try
            {
                string sql = "SELECT FieldId,FieldName,FieldDesc FROM " + (LanguageId == 1 ? "Fields" : "FieldLanguages") + " WHERE (FieldId=" + FieldId.ToString() + ")";
                if (LanguageId > 1) sql = sql + " AND (LanguageId=" + LanguageId.ToString() + ")";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Fields.Static_InitOne(smartReader);

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
        //-------------------------------------------------------------- 
        public string GetUrl(byte DocGroupId = 1)
        {
            string RetVal = "";
            if (!string.IsNullOrEmpty(this.FieldName) && this.FieldId > 0)
            {
                RetVal = this.FieldName.Replace("-", " ");
                RetVal = StringUtil.RemoveSignature(RetVal);
                RetVal = Regex.Replace(RetVal, "(?:[^a-z0-9 _-]|(?<=['\"])s)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                RetVal = RetVal.Replace(" ", "-");
                RetVal = RetVal.ToLower();
                string FieldIdHex = this.FieldId.ToString("x");
                RetVal = string.Concat("van-ban-luat-",RetVal, "-", FieldIdHex, ".html");
                if (!RetVal.StartsWith("/")) RetVal = string.Concat("/", RetVal);
            }
            return RetVal;
        }
    }
}
