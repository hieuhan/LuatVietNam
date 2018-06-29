using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using sms.database;
using ICSoft.CMSLib;
using System.Data.SqlClient;
using System.Data;

namespace ICSoft.CMSViewLib
{
    public class ArticlesViewJsonHelpers
    {
        public static string GetListByCategoryId(short CategoryId, int RowAmount)
        {
            StringBuilder m_Json = new StringBuilder();
            string m_str = "";
            int i = 0;
            m_Json.AppendLine("{");
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();
            try
            {
                ArticlesViewCate RetVal = new ArticlesViewCate();
                SqlCommand cmd = new SqlCommand("Articles_MobileView_GetByCateId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay chi tiet chuyen muc
                smartReader.Read();
                m_str = JsonConvert.ToString("CategoryId") + ":" + smartReader.GetInt16("CategoryId").ToString();
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("CategoryName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay danh sach tin
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("ArticleList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("ArticleId") + ":" + smartReader.GetString("ArticleId") + "\n";
                    //m_str = m_str + ", " + JsonConvert.ToString("CategoryId") + ":" + smartReader.GetString("CategoryId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Title") + ":" + JsonConvert.ToString(smartReader.GetString("Title")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("PublishTime") + ":" + JsonConvert.ToString(smartReader.GetDateTime("PublishTime").ToString("HH:mm dd/MM/yyyy")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Thumb(smartReader.GetString("ImagePath"))) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Summary") + ":" + JsonConvert.ToString(smartReader.GetString("Summary")) + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(con);
            }
            m_Json.AppendLine("}");
            return m_Json.ToString();
        }
        //-----------------------
        public static string GetListByCategoryId_Page(short CategoryId, int RowAmount, int PageIndex)
        {
            StringBuilder m_Json = new StringBuilder();
            string m_str = "";
            int i = 0;
            m_Json.AppendLine("{");
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            try
            {
                ArticlesViewCate RetVal = new ArticlesViewCate();
                SqlCommand cmd = new SqlCommand("Articles_MobileView_GetByCateId_Page");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay chi tiet chuyen muc
                smartReader.Read();
                m_str = JsonConvert.ToString("CategoryId") + ":" + smartReader.GetInt16("CategoryId").ToString();
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("CategoryName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay danh sach tin
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("ArticleList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("ArticleId") + ":" + smartReader.GetString("ArticleId") + "\n";
                    //m_str = m_str + ", " + JsonConvert.ToString("CategoryId") + ":" + smartReader.GetString("CategoryId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Title") + ":" + JsonConvert.ToString(smartReader.GetString("Title")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("PublishTime") + ":" + JsonConvert.ToString(smartReader.GetDateTime("PublishTime").ToString("HH:mm dd/MM/yyyy")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Thumb(smartReader.GetString("ImagePath"))) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Summary") + ":" + JsonConvert.ToString(smartReader.GetString("Summary")) + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                reader.Close();

                int m_RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
                m_str = ", " + JsonConvert.ToString("RowCount") + ":" + m_RowCount.ToString();
                m_Json.AppendLine(m_str);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(con);
            }
            m_Json.AppendLine("}");
            return m_Json.ToString();
        }
        //-----------------------
        public static string GetByArticleId(int ArticleId, short CategoryId, int RowAmountOther)
        {
            StringBuilder m_Json = new StringBuilder();
            string m_str = "";
            int i = 0;
            m_Json.AppendLine("{");
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            try
            {
                ArticlesViewCate RetVal = new ArticlesViewCate();
                SqlCommand cmd = new SqlCommand("Articles_MobileView_GetById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ArticleId", ArticleId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmountOther", RowAmountOther));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay chi tiet chuyen muc
                smartReader.Read();
                m_str = JsonConvert.ToString("CategoryId") + ":" + smartReader.GetInt16("CategoryId").ToString();
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("CategoryName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay chi tiet tin
                reader.NextResult();
                smartReader.Read();
                m_str = ", " + JsonConvert.ToString("ArticleId") + ":" + smartReader.GetString("ArticleId") + "\n";
                m_str = m_str + ", " + JsonConvert.ToString("Title") + ":" + JsonConvert.ToString(smartReader.GetString("Title")) + "\n";
                m_str = m_str + ", " + JsonConvert.ToString("PublishTime") + ":" + JsonConvert.ToString(smartReader.GetDateTime("PublishTime").ToString("HH:mm dd/MM/yyyy")) + "\n";
                m_str = m_str + ", " + JsonConvert.ToString("ImageUrl") + ":" + JsonConvert.ToString(GetImageUrl(smartReader.GetString("ImagePath"))) + "\n";
                m_str = m_str + ", " + JsonConvert.ToString("ArticleContent") + ":" + JsonConvert.ToString(smartReader.GetString("ArticleContent")) + "\n";
                m_Json.AppendLine(m_str);

                //Lay danh sach tag
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("TagList") + ":" + "[";
                m_Json.AppendLine(m_str);
                i = 0;
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("TagId") + ":" + smartReader.GetString("TagId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("TagName") + ":" + JsonConvert.ToString(smartReader.GetString("TagName")) + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay danh sach tin lien quan
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("ArticleRelateList") + ":" + "[";
                m_Json.AppendLine(m_str);
                i = 0;
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("ArticleId") + ":" + smartReader.GetString("ArticleId") + "\n";
                    //m_str = m_str + ", " + JsonConvert.ToString("CategoryId") + ":" + smartReader.GetString("CategoryId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Title") + ":" + JsonConvert.ToString(smartReader.GetString("Title")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("PublishTime") + ":" + JsonConvert.ToString(smartReader.GetDateTime("PublishTime").ToString("HH:mm dd/MM/yyyy")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Thumb(smartReader.GetString("ImagePath"))) + "\n";
                    //m_str = m_str + ", " + JsonConvert.ToString("Summary") + ":" + JsonConvert.ToString(smartReader.GetString("Summary")) + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay danh sach tin khac
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("ArticleOtherList") + ":" + "[";
                m_Json.AppendLine(m_str);
                i = 0;
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("ArticleId") + ":" + smartReader.GetString("ArticleId") + "\n";
                    //m_str = m_str + ", " + JsonConvert.ToString("CategoryId") + ":" + smartReader.GetString("CategoryId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Title") + ":" + JsonConvert.ToString(smartReader.GetString("Title")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("PublishTime") + ":" + JsonConvert.ToString(smartReader.GetDateTime("PublishTime").ToString("HH:mm dd/MM/yyyy")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Thumb(smartReader.GetString("ImagePath"))) + "\n";
                    //m_str = m_str + ", " + JsonConvert.ToString("Summary") + ":" + JsonConvert.ToString(smartReader.GetString("Summary")) + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(con);
            }
            m_Json.AppendLine("}");
            return m_Json.ToString();
        }
        //-----------------------
        private static string GetImageUrl(string Url)
        {
            string RetVal = Url;
            if (string.IsNullOrEmpty(Url))
            {
                RetVal = CmsConstants.NO_IMAGE_URL;
            }
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
            return RetVal;
        }
        //--------------------------------------------------------------  
        private static string GetImageUrl_Thumb(string Url)
        {
            return GetImageUrl(Url).Replace("/Original/", "/Thumb/");
        }
        //--------------------------------------------------------------  
        private static string GetImageUrl_Standard(string Url)
        {
            return GetImageUrl(Url).Replace("/Original/", "/Standard/");
        }
        //--------------------------------------------------------------  
        private static string GetImageUrl_Mobile(string Url)
        {
            return GetImageUrl(Url).Replace("/Original/", "/Mobile/");
        }
    }
}
