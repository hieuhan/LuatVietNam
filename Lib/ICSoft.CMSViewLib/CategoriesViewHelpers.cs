using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;
using sms.database;
using sms.utils;
namespace ICSoft.CMSViewLib
{
    public class CategoriesViewHelpers
    {
        public static CategoriesViewHome View_GetViewHome(byte LanguageId, byte ApplicationTypeId, byte DataTypeId, short SiteId, string PaddingChar)
        {
            CategoriesViewHome RetVal = new CategoriesViewHome();
            short ParentCategoryId = 0;
            short DisplayTypeId=0;
            byte ShowTop=0;
            byte ShowBottom=0;
            byte ShowWeb=0;
            byte ShowWap=0;
            byte ShowApp=0;
            List<CategoriesView> l_CategoriesView = View_GetViewDisplay(LanguageId, ApplicationTypeId, DataTypeId, SiteId, ParentCategoryId, DisplayTypeId, ShowTop, ShowBottom, ShowWeb, ShowWap, ShowApp, PaddingChar);
            RetVal.lCategoriesShowTop = CategoriesView.Static_GetByShowTop(l_CategoriesView);
            RetVal.lCategoriesShowBottom = CategoriesView.Static_GetByShowBottom(l_CategoriesView);
            RetVal.lCategoriesShowWeb = CategoriesView.Static_GetByShowWeb(l_CategoriesView);
            RetVal.lCategoriesShowWap = CategoriesView.Static_GetByShowWap(l_CategoriesView);
            RetVal.lCategoriesShowApp = CategoriesView.Static_GetByShowApp(l_CategoriesView);

            return RetVal;
        }
        public static List<CategoriesView> View_GetViewDisplay(byte LanguageId, byte ApplicationTypeId, byte DataTypeId, short SiteId, short ParentCategoryId, short DisplayTypeId, byte ShowTop, byte ShowBottom, byte ShowWeb, byte ShowWap, byte ShowApp, string PaddingChar)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<CategoriesView> RetVal = new List<CategoriesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetViewDisplay");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@ParentCategoryId", ParentCategoryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", ShowApp));
                cmd.Parameters.Add(new SqlParameter("@PaddingChar", PaddingChar));  

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                while (smartReader.Read())
                {
                    CategoriesView m_Categories = new CategoriesView();
                    //m_Categories.LanguageId = smartReader.GetByte("LanguageId");
                    //m_Categories.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Categories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_Categories.CategoryName = smartReader.GetString("CategoryName");
                    m_Categories.CategoryDesc = smartReader.GetString("CategoryDesc");
                    m_Categories.CategoryUrl = smartReader.GetString("CategoryUrl");
                    m_Categories.DataTypeId = smartReader.GetInt16("DataTypeId");
                    m_Categories.SiteId = smartReader.GetInt16("SiteId");
                    m_Categories.FeatureGroupId = smartReader.GetInt16("FeatureGroupId");
                    m_Categories.ShowTop = smartReader.GetByte("ShowTop");
                    m_Categories.ShowBottom = smartReader.GetByte("ShowBottom");
                    m_Categories.ShowWeb = smartReader.GetByte("ShowWeb");
                    m_Categories.ShowWap = smartReader.GetByte("ShowWap");
                    m_Categories.ShowApp = smartReader.GetByte("ShowApp");
                    m_Categories.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Categories.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Categories.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Categories.ParentCategoryId = smartReader.GetInt32("ParentCategoryId");
                    m_Categories.CategoryLevel = smartReader.GetByte("CategoryLevel");
                    m_Categories.ImagePath = smartReader.GetString("ImagePath");
                    m_Categories.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Categories.JsonData = smartReader.GetString("JsonData");
                    m_Categories.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Categories.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Categories.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    RetVal.Add(m_Categories);
                }
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

        public static List<CategoriesView> GetDisplayAll(byte LanguageId, byte ApplicationTypeId, byte DataTypeId, short SiteId)
        {
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            List<CategoriesView> RetVal = new List<CategoriesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetViewDisplayAll");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                while (smartReader.Read())
                {
                    CategoriesView m_Categories = new CategoriesView();
                    m_Categories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_Categories.CategoryName = smartReader.GetString("CategoryName");
                    m_Categories.CategoryDesc = smartReader.GetString("CategoryDesc");
                    m_Categories.CategoryUrl = smartReader.GetString("CategoryUrl");
                    m_Categories.DataTypeId = smartReader.GetInt16("DataTypeId");
                    m_Categories.SiteId = smartReader.GetInt16("SiteId");
                    m_Categories.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Categories.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Categories.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Categories.ParentCategoryId = smartReader.GetInt32("ParentCategoryId");
                    m_Categories.CategoryLevel = smartReader.GetByte("CategoryLevel");
                    m_Categories.ImagePath = smartReader.GetString("ImagePath");
                    m_Categories.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_Categories.DisplayTypeName = smartReader.GetString("DisplayTypeName");

                    RetVal.Add(m_Categories);
                }
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

        public static CategoriesView View_GetByCategoryId(short CategoryId)
        {
            byte LanguageId = 0;
            byte ApplicationTypeId = 0;
            return new Categories().GetView(CategoryId, LanguageId, ApplicationTypeId);
        }
        public static CategoriesView View_GetByCategoryUrl(short CategoryId,string CategoryUrl)
        {
            byte LanguageId = 0;
            byte ApplicationTypeId = 0;
            return new Categories().GetView(CategoryId, CategoryUrl, LanguageId, ApplicationTypeId);
        }
        public static List<CategoriesView> View_GetListByParentId(short ParentCategoryId)
        {
            short SiteId=0;
            byte LanguageId=0;
            byte ApplicationTypeId = 0;
            return new Categories().GetListByParentView(SiteId, LanguageId, ApplicationTypeId, ParentCategoryId);
        }
    }

}//end namespace service
