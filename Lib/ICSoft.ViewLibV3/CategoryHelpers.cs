using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class CategoryHelpers
    {
        //-----------------------------------------------------------------
        public static List<Categories> Init(SmartDataReader smartReader, string FieldSelect = "*")
        {
            List<Categories> l_Categories = new List<Categories>();
            Categories m_Categories;
            while (smartReader.Read())
            {
                m_Categories = new Categories();
                if (FieldSelect == "*" || FieldSelect.Contains("CategoryId")) m_Categories.CategoryId = smartReader.GetInt16("CategoryId");
                if (FieldSelect == "*" || FieldSelect.Contains("CategoryName")) m_Categories.CategoryName = smartReader.GetString("CategoryName");
                if (FieldSelect == "*" || FieldSelect.Contains("CategoryDesc")) m_Categories.CategoryDesc = smartReader.GetString("CategoryDesc");
                if (FieldSelect == "*" || FieldSelect.Contains("CategoryUrl")) m_Categories.CategoryUrl = smartReader.GetString("CategoryUrl");
                if (FieldSelect == "*" || FieldSelect.Contains("ParentCategoryId")) m_Categories.ParentCategoryId = smartReader.GetInt32("ParentCategoryId");
                if (FieldSelect == "*" || FieldSelect.Contains("CategoryLevel")) m_Categories.CategoryLevel = smartReader.GetByte("CategoryLevel");
                if (FieldSelect == "*" || FieldSelect.Contains("ImagePath")) m_Categories.ImagePath = smartReader.GetString("ImagePath");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaTitle")) m_Categories.MetaTitle = smartReader.GetString("MetaTitle");
                if (String.IsNullOrEmpty(m_Categories.MetaTitle)) m_Categories.MetaTitle = m_Categories.CategoryName;
                if (FieldSelect == "*" || FieldSelect.Contains("MetaDesc")) m_Categories.MetaDesc = smartReader.GetString("MetaDesc");
                if (String.IsNullOrEmpty(m_Categories.MetaDesc)) m_Categories.MetaTitle = m_Categories.CategoryDesc;
                if (FieldSelect == "*" || FieldSelect.Contains("MetaKeyword")) m_Categories.MetaKeyword = smartReader.GetString("MetaKeyword");
                if (FieldSelect == "*" || FieldSelect.Contains("CanonicalTag")) m_Categories.CanonicalTag = smartReader.GetString("CanonicalTag");
                if (FieldSelect == "*" || FieldSelect.Contains("H1Tag")) m_Categories.H1Tag = smartReader.GetString("H1Tag");
                if (FieldSelect == "*" || FieldSelect.Contains("SeoFooter")) m_Categories.SeoFooter = smartReader.GetString("SeoFooter");

                l_Categories.Add(m_Categories);
            }
            return l_Categories;
        }
        //-----------------------------------------------------------------
        public static Categories InitOne(SmartDataReader smartReader, string FieldSelect = "*")
        {
            List<Categories> l_Categories = Init(smartReader, FieldSelect);
            if (l_Categories.Count > 0) return l_Categories[0];
            return null;
        }
        //-----------------------------------------------------------
        public static List<Categories> GetBySiteId(short SiteId, string FieldSelect = "CategoryId,CategoryDesc,CategoryUrl")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<Categories> RetVal = new List<Categories>();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FieldSelect", FieldSelect));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Init(smartReader, FieldSelect);

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
        public static List<Categories> GetByParentItemId(int ParentItemId, string FieldSelect = "CategoryId,CategoryDesc,CategoryUrl")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<Categories> RetVal = new List<Categories>();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FieldSelect", FieldSelect));
                cmd.Parameters.Add(new SqlParameter("@ParentItemId", ParentItemId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Init(smartReader, FieldSelect);

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
        public static Categories GetById(short CategoryId, string FieldSelect = "CategoryId,CategoryName,CategoryDesc,CategoryUrl,ImagePath,MetaTitle,MetaDesc,MetaKeyword,CanonicalTag,H1Tag,SeoFooter")
        {
            DBAccess db = new DBAccess(Constants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<Categories> RetVal = new List<Categories>();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FieldSelect", FieldSelect));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Init(smartReader, FieldSelect);

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
            return RetVal.Count > 0 ? RetVal[0] : new Categories();
        }
        //-----------------------------------------------------------------
        public static Categories GetById(short CategoryId, List<Categories> list)
        {
            Categories RetVal = list.Find(i => i.CategoryId == CategoryId);
            return RetVal == null ? new Categories() : RetVal;
        }
        //-----------------------------------------------------------------
        public static List<Categories> GetListByParentItemId(int ParentItemId, List<Categories> list)
        {
            List<Categories> RetVal = list.FindAll(i => i.ParentCategoryId == ParentItemId);
            return RetVal == null ? new List<Categories>() : RetVal;
        }
    }
}
