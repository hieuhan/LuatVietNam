using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using sms.database;
using ICSoft.CMSLib;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace ICSoft.CMSViewLib
{
    public class SongsViewJsonHelpers
    {
        public static string GetListBySingerId(int SingerId, string SingerName, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetBySingerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SingerId", SingerId));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin ca sy
                smartReader.Read();
                m_str = JsonConvert.ToString("SingerId") + ":" + smartReader.GetString("ArticleId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("SingerName") + ":" + JsonConvert.ToString(smartReader.GetString("Title"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("ImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ImagePath")));
                m_Json.AppendLine(m_str);
                //m_str = ", " + JsonConvert.ToString("Profile") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ArticleContent")));
                //m_Json.AppendLine(m_str);

                //Lay danh bai hat
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SingerImageUrl") + ":" + JsonConvert.ToString("SingerImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }
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
        public static string GetListByCategoryId(int CategoryId, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetByCategoryId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin ca sy
                smartReader.Read();
                m_str = JsonConvert.ToString("CategoryId") + ":" + smartReader.GetString("CategoryId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("CategoryName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay danh bai hat
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SingerImageUrl") + ":" + JsonConvert.ToString("SingerImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
        public static string GetListNewest(int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetNewest");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay danh bai hat
                m_str = JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SingerImageUrl") + ":" + JsonConvert.ToString("SingerImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
        public static string GetListByMusicTypeId(int MusicTypeId, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetByMusicTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MusicTypeId", MusicTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin ca sy
                smartReader.Read();
                m_str = JsonConvert.ToString("MusicTypeId") + ":" + smartReader.GetString("CategoryId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("MusicTypeName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay danh bai hat
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SingerImageUrl") + ":" + JsonConvert.ToString("SingerImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
        public static string GetBySongSingerId(int SongSingerId)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin bai hat
                smartReader.Read();
                m_str = JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Author") + ":" + JsonConvert.ToString("Author" + smartReader.GetString("SongId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("MusicType") + ":" + JsonConvert.ToString("MusicType" + smartReader.GetString("SongId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("SingerImageUrl") + ":" + JsonConvert.ToString("SingerImageUrl" + smartReader.GetString("SongSingerId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Mp3Url") + ":" + JsonConvert.ToString("Mp3Url" + smartReader.GetString("SongSingerId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Lyric") + ":" + JsonConvert.ToString(smartReader.GetString("Lyric"));
                m_Json.AppendLine(m_str);

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (string.IsNullOrEmpty(m_SingerImage)) m_SingerImage = smartReader.GetString("ImagePath");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_Singer != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        m_Singer = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_Singer != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));

                //Lay music type theo bai hat
                reader.NextResult();
                string m_MusicType = "";
                string m_SongId = "";
                string m_SongId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongId = smartReader.GetString("SongId");
                    if (m_SongId != m_SongId_Pre)
                    {
                        if (m_SongId_Pre != "") m_Json = m_Json.Replace("MusicType" + m_SongId_Pre, m_MusicType);
                        m_MusicType = "";
                    }
                    if (m_MusicType != "") m_MusicType = m_MusicType + ", ";
                    m_MusicType = m_MusicType + smartReader.GetString("MusicTypeName");
                    m_SongId_Pre = m_SongId;
                }
                if (m_SongId_Pre != "") m_Json = m_Json.Replace("MusicType" + m_SongId_Pre, m_MusicType);

                //Lay author theo bai hat
                reader.NextResult();
                string m_Author = "";
                m_SongId = "";
                m_SongId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongId = smartReader.GetString("SongId");
                    if (m_SongId != m_SongId_Pre)
                    {
                        if (m_SongId_Pre != "") m_Json = m_Json.Replace("Author" + m_SongId_Pre, m_Author);
                        m_Author = "";
                    }
                    if (m_Author != "") m_Author = m_Author + ", ";
                    m_Author = m_Author + smartReader.GetString("AuthorName");
                    m_SongId_Pre = m_SongId;
                }
                if (m_SongId_Pre != "") m_Json = m_Json.Replace("Author" + m_SongId_Pre, m_Author);

                //Lay file
                reader.NextResult();
                string m_Mp3File = "";
                m_SongSingerId = "";
                while (smartReader.Read())
                {
                    if (smartReader.GetString("SongFileTypeId") == "1")
                    {
                        m_SongSingerId = smartReader.GetString("SongSingerId");
                        m_Mp3File = smartReader.GetString("FilePath");
                        break;
                    }
                }

                //Lay RBT
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("RBTList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    if (string.IsNullOrEmpty(m_Mp3File))
                    {
                        m_SongSingerId = smartReader.GetString("SongSingerId");
                        m_Mp3File = smartReader.GetString("FilePath");
                    }

                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerRBTId") + ":" + smartReader.GetString("SongSingerRBTId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTName") + ":" + JsonConvert.ToString(smartReader.GetString("RBTName")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTCode") + ":" + JsonConvert.ToString(smartReader.GetString("RBTCode")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Price") + ":" + smartReader.GetString("Price") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTUrl") + ":" + JsonConvert.ToString(GetFullUrl(smartReader.GetString("FilePath"))) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("DownloadCount") + ":" + smartReader.GetString("DownloadCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json = m_Json.Replace("Mp3Url" + m_SongSingerId, GetFullUrl(m_Mp3File));
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
            string m_JsonReg = m_Json.ToString();
            m_JsonReg = Regex.Replace(m_JsonReg, @"Author\d+|MusicType\d+|SingerImageUrl\d+|Mp3Url\d+", "");
            return m_JsonReg;
        }
        //-----------------------
        public static string GetListVideoNewest(int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetVideoNewest");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Lay danh bai hat
                m_str = JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("VideoImageUrl") + ":" + JsonConvert.ToString("VideoImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay file theo bai hat
                reader.NextResult();
                string m_VideoImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    if (smartReader.GetString("SongFileTypeId") == "3") //Anh video
                    {
                        m_SongSingerId = smartReader.GetString("SongSingerId");
                        if (m_SongSingerId != m_SongSingerId_Pre)
                        {
                            if (m_VideoImage != "") m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_VideoImage));
                            m_VideoImage = "";
                        }
                        m_VideoImage = smartReader.GetString("FilePath");
                        m_SongSingerId_Pre = m_SongSingerId;
                    }
                }
                if (m_VideoImage != "")
                {
                    m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_VideoImage));
                }

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                m_SongSingerId = "";
                m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
        public static string GetListVideoByCategoryId(int CategoryId, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetVideoByCategoryId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin ca sy
                smartReader.Read();
                m_str = JsonConvert.ToString("CategoryId") + ":" + smartReader.GetString("CategoryId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("CategoryName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay danh bai hat
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("VideoImageUrl") + ":" + JsonConvert.ToString("VideoImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay file theo bai hat
                reader.NextResult();
                string m_VideoImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    if (smartReader.GetString("SongFileTypeId") == "3") //Anh video
                    {
                        m_SongSingerId = smartReader.GetString("SongSingerId");
                        if (m_SongSingerId != m_SongSingerId_Pre)
                        {
                            if (m_VideoImage != "") m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_VideoImage));
                            m_VideoImage = "";
                        }
                        m_VideoImage = smartReader.GetString("FilePath");
                        m_SongSingerId_Pre = m_SongSingerId;
                    }
                }
                if (m_VideoImage != "")
                {
                    m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_VideoImage));
                }

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                m_SongSingerId = "";
                m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
        public static string GetListVideoBySingerId(int SingerId, string SingerName, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetVideoBySingerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SingerId", SingerId));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin ca sy
                smartReader.Read();
                m_str = JsonConvert.ToString("SingerId") + ":" + smartReader.GetString("ArticleId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("SingerName") + ":" + JsonConvert.ToString(smartReader.GetString("Title"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("ImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ImagePath")));
                m_Json.AppendLine(m_str);

                //Lay danh bai hat
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("VideoImageUrl") + ":" + JsonConvert.ToString("VideoImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay file theo bai hat
                reader.NextResult();
                string m_VideoImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    if (smartReader.GetString("SongFileTypeId") == "3") //Anh video
                    {
                        m_SongSingerId = smartReader.GetString("SongSingerId");
                        if (m_SongSingerId != m_SongSingerId_Pre)
                        {
                            if (m_VideoImage != "") m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_VideoImage));
                            m_VideoImage = "";
                        }
                        m_VideoImage = smartReader.GetString("FilePath");
                        m_SongSingerId_Pre = m_SongSingerId;
                    }
                }
                if (m_VideoImage != "")
                {
                    m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_VideoImage));
                }

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                m_SongSingerId = "";
                m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
        public static string GetListVideoByMusicTypeId(int MusicTypeId, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetVideoByMusicTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MusicTypeId", MusicTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin ca sy
                smartReader.Read();
                m_str = JsonConvert.ToString("MusicTypeId") + ":" + smartReader.GetString("CategoryId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("MusicTypeName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay danh bai hat
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("VideoImageUrl") + ":" + JsonConvert.ToString("VideoImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay file theo bai hat
                reader.NextResult();
                string m_VideoImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    if (smartReader.GetString("SongFileTypeId") == "3") //Anh video
                    {
                        m_SongSingerId = smartReader.GetString("SongSingerId");
                        if (m_SongSingerId != m_SongSingerId_Pre)
                        {
                            if (m_VideoImage != "") m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_VideoImage));
                            m_VideoImage = "";
                        }
                        m_VideoImage = smartReader.GetString("FilePath");
                        m_SongSingerId_Pre = m_SongSingerId;
                    }
                }
                if (m_VideoImage != "")
                {
                    m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_VideoImage));
                }

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                m_SongSingerId = "";
                m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("VideoImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
        public static string GetVideoBySongSingerId(int SongSingerId)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetVideoById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SongSingerId", SongSingerId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin bai hat
                smartReader.Read();
                m_str = JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Author") + ":" + JsonConvert.ToString("Author" + smartReader.GetString("SongId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("MusicType") + ":" + JsonConvert.ToString("MusicType" + smartReader.GetString("SongId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("SingerImageUrl") + ":" + JsonConvert.ToString("SingerImageUrl" + smartReader.GetString("SongSingerId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Mp4Url") + ":" + JsonConvert.ToString("Mp4Url" + smartReader.GetString("SongSingerId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Lyric") + ":" + JsonConvert.ToString(smartReader.GetString("Lyric"));
                m_Json.AppendLine(m_str);
                
                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (string.IsNullOrEmpty(m_SingerImage)) m_SingerImage = smartReader.GetString("ImagePath");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_Singer != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        m_Singer = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_Singer != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));

                //Lay music type theo bai hat
                reader.NextResult();
                string m_MusicType = "";
                string m_SongId = "";
                string m_SongId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongId = smartReader.GetString("SongId");
                    if (m_SongId != m_SongId_Pre)
                    {
                        if (m_SongId_Pre != "") m_Json = m_Json.Replace("MusicType" + m_SongId_Pre, m_MusicType);
                        m_MusicType = "";
                    }
                    if (m_MusicType != "") m_MusicType = m_MusicType + ", ";
                    m_MusicType = m_MusicType + smartReader.GetString("MusicTypeName");
                    m_SongId_Pre = m_SongId;
                }
                if (m_SongId_Pre != "") m_Json = m_Json.Replace("MusicType" + m_SongId_Pre, m_MusicType);

                //Lay author theo bai hat
                reader.NextResult();
                string m_Author = "";
                m_SongId = "";
                m_SongId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongId = smartReader.GetString("SongId");
                    if (m_SongId != m_SongId_Pre)
                    {
                        if (m_SongId_Pre != "") m_Json = m_Json.Replace("Author" + m_SongId_Pre, m_Author);
                        m_Author = "";
                    }
                    if (m_Author != "") m_Author = m_Author + ", ";
                    m_Author = m_Author + smartReader.GetString("AuthorName");
                    m_SongId_Pre = m_SongId;
                }
                if (m_SongId_Pre != "") m_Json = m_Json.Replace("Author" + m_SongId_Pre, m_Author);

                //Lay file
                reader.NextResult();
                string m_Mp4File = "";
                m_SongSingerId = "";
                while (smartReader.Read())
                {
                    if (smartReader.GetString("SongFileTypeId") == "2")
                    {
                        m_SongSingerId = smartReader.GetString("SongSingerId");
                        m_Mp4File = smartReader.GetString("FilePath");
                        break;
                    }
                }

                //Lay RBT
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("RBTList") + ":" + "[";
                m_Json = m_Json.Replace("Mp4Url" + m_SongSingerId, GetFullUrl(m_Mp4File));
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerRBTId") + ":" + smartReader.GetString("SongSingerRBTId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTName") + ":" + JsonConvert.ToString(smartReader.GetString("RBTName")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTCode") + ":" + JsonConvert.ToString(smartReader.GetString("RBTCode")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Price") + ":" + smartReader.GetString("Price") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTUrl") + ":" + JsonConvert.ToString(GetFullUrl(smartReader.GetString("FilePath"))) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("DownloadCount") + ":" + smartReader.GetString("DownloadCount") + "\n";
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
            string m_JsonReg = m_Json.ToString();
            m_JsonReg = Regex.Replace(m_JsonReg, @"Author\d+|MusicType\d+|SingerImageUrl\d+|Mp3Url\d+", "");
            return m_JsonReg;
        }
        //-----------------------
        public static string GetListAlbumByCategoryId(int CategoryId, byte DataTypeId, byte AlbumTypeId, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetAlbumByCategoryId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@AlbumTypeId", AlbumTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin cate
                smartReader.Read();
                m_str = JsonConvert.ToString("CategoryId") + ":" + (string.IsNullOrEmpty(smartReader.GetString("CategoryId")) ? "0" : smartReader.GetString("CategoryId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("CategoryName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay danh sach album
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("AlbumList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("AlbumId") + ":" + smartReader.GetString("ArticleId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumCode") + ":" + JsonConvert.ToString(smartReader.GetString("ArticleCode")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumName") + ":" + JsonConvert.ToString(smartReader.GetString("Title")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("ArticleId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ImagePath"))) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + (string.IsNullOrEmpty(smartReader.GetString("ViewCount")) ? "0" : smartReader.GetString("ViewCount")) + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_AlbumId = "";
                string m_AlbumId_Pre = "";
                while (smartReader.Read())
                {
                    m_AlbumId = smartReader.GetString("ArticleId");
                    if (m_AlbumId != m_AlbumId_Pre)
                    {
                        if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                        //if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_AlbumId_Pre = m_AlbumId;
                }
                if (m_AlbumId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                    //m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
            string m_JsonReg = m_Json.ToString();
            m_JsonReg = Regex.Replace(m_JsonReg, @"Singer\d+", "");
            return m_JsonReg;
        }
        //-----------------------
        public static string GetListAlbumByMusicTypeId(int MusicTypeId, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetAlbumByMusicTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MusicTypeId", MusicTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin cate
                smartReader.Read();
                m_str = JsonConvert.ToString("MusicTypeId") + ":" + smartReader.GetString("CategoryId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("MusicTypeName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay danh sach album
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("AlbumList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("AlbumId") + ":" + smartReader.GetString("ArticleId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumCode") + ":" + JsonConvert.ToString(smartReader.GetString("ArticleCode")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumName") + ":" + JsonConvert.ToString(smartReader.GetString("Title")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("ArticleId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ImagePath"))) + "\n";
                    //m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_AlbumId = "";
                string m_AlbumId_Pre = "";
                while (smartReader.Read())
                {
                    m_AlbumId = smartReader.GetString("ArticleId");
                    if (m_AlbumId != m_AlbumId_Pre)
                    {
                        if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                        //if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_AlbumId_Pre = m_AlbumId;
                }
                if (m_AlbumId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                    //m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
            string m_JsonReg = m_Json.ToString();
            m_JsonReg = Regex.Replace(m_JsonReg, @"Singer\d+", "");
            return m_JsonReg;
        }
        //-----------------------
        public static string GetListAlbumNewest(int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetAlbumNewest");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                m_str = JsonConvert.ToString("AlbumList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("AlbumId") + ":" + smartReader.GetString("ArticleId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumCode") + ":" + JsonConvert.ToString(smartReader.GetString("ArticleCode")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumName") + ":" + JsonConvert.ToString(smartReader.GetString("Title")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("ArticleId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ImagePath"))) + "\n";
                    //m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_AlbumId = "";
                string m_AlbumId_Pre = "";
                while (smartReader.Read())
                {
                    m_AlbumId = smartReader.GetString("ArticleId");
                    if (m_AlbumId != m_AlbumId_Pre)
                    {
                        if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                        //if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_AlbumId_Pre = m_AlbumId;
                }
                if (m_AlbumId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                    //m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
            string m_JsonReg = m_Json.ToString();
            m_JsonReg = Regex.Replace(m_JsonReg, @"Singer\d+", "");
            return m_JsonReg;
        }
        //-----------------------
        public static string GetListAlbumBySingerId(int SingerId, string SingerName, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetAlbumBySingerId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SingerId", SingerId));
                cmd.Parameters.Add(new SqlParameter("@SingerName", SingerName));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin singer
                smartReader.Read();
                m_str = JsonConvert.ToString("SingerId") + ":" + smartReader.GetString("ArticleId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("SingerName") + ":" + JsonConvert.ToString(smartReader.GetString("Title"));
                m_Json.AppendLine(m_str);

                //Lay danh sach album
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("AlbumList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("AlbumId") + ":" + smartReader.GetString("ArticleId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumCode") + ":" + JsonConvert.ToString(smartReader.GetString("ArticleCode")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumName") + ":" + JsonConvert.ToString(smartReader.GetString("Title")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("ArticleId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("AlbumImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ImagePath"))) + "\n";
                    //m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_AlbumId = "";
                string m_AlbumId_Pre = "";
                while (smartReader.Read())
                {
                    m_AlbumId = smartReader.GetString("ArticleId");
                    if (m_AlbumId != m_AlbumId_Pre)
                    {
                        if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                        //if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_AlbumId_Pre = m_AlbumId;
                }
                if (m_AlbumId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                    //m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
            string m_JsonReg = m_Json.ToString();
            m_JsonReg = Regex.Replace(m_JsonReg, @"Singer\d+", "");
            return m_JsonReg;
        }
        //-----------------------
        public static string GetAlbumByAlbumId(int AlbumId)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetAlbumById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AlbumId", AlbumId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin album
                smartReader.Read();
                m_str = JsonConvert.ToString("AlbumId") + ":" + (string.IsNullOrEmpty(smartReader.GetString("ArticleId")) ? "0" : smartReader.GetString("ArticleId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("AlbumCode") + ":" + JsonConvert.ToString(smartReader.GetString("ArticleCode"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("AlbumName") + ":" + JsonConvert.ToString(smartReader.GetString("Title"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("ArticleId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("AlbumImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ImagePath")));
                m_Json.AppendLine(m_str);

                //Lay casy
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_AlbumId = "";
                string m_AlbumId_Pre = "";
                while (smartReader.Read())
                {
                    m_AlbumId = smartReader.GetString("ArticleId");
                    if (m_AlbumId != m_AlbumId_Pre)
                    {
                        if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                        //if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_AlbumId_Pre = m_AlbumId;
                }
                if (m_AlbumId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                    //m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }


                //Lay danh bai hat
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerId") + ":" + smartReader.GetString("SongSingerId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SongName") + ":" + JsonConvert.ToString(smartReader.GetString("SongDesc")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SingerImageUrl") + ":" + JsonConvert.ToString("SingerImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy theo bai hat
                reader.NextResult();
                m_Singer = "";
                m_SingerImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
            string m_JsonReg = m_Json.ToString();
            m_JsonReg = Regex.Replace(m_JsonReg, @"Singer\d+", "");
            return m_JsonReg;
        }
        //-----------------------
        public static string GetAlbumRBTByAlbumId(int AlbumId)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetAlbumRBTById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AlbumId", AlbumId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin album
                smartReader.Read();
                m_str = JsonConvert.ToString("AlbumId") + ":" + smartReader.GetString("ArticleId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("AlbumCode") + ":" + JsonConvert.ToString(smartReader.GetString("ArticleCode"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("AlbumName") + ":" + JsonConvert.ToString(smartReader.GetString("Title"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("ArticleId"));
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("AlbumImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ImagePath")));
                m_Json.AppendLine(m_str);

                //Lay casy
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_AlbumId = "";
                string m_AlbumId_Pre = "";
                while (smartReader.Read())
                {
                    m_AlbumId = smartReader.GetString("ArticleId");
                    if (m_AlbumId != m_AlbumId_Pre)
                    {
                        if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                        //if (m_AlbumId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_AlbumId_Pre = m_AlbumId;
                }
                if (m_AlbumId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_AlbumId_Pre, m_Singer);
                    //m_Json = m_Json.Replace("SingerImageUrl" + m_AlbumId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }


                //Lay danh bai hat
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerRBTId") + ":" + smartReader.GetString("SongSingerRBTId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTName") + ":" + JsonConvert.ToString(smartReader.GetString("RBTName")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SingerImageUrl") + ":" + JsonConvert.ToString("SingerImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTCode") + ":" + JsonConvert.ToString(smartReader.GetString("RBTCode")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Price") + ":" + smartReader.GetString("Price") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Mp3Url") + ":" + JsonConvert.ToString(GetFullUrl(smartReader.GetString("FilePath"))) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy theo bai hat
                reader.NextResult();
                m_Singer = "";
                m_SingerImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
            string m_JsonReg = m_Json.ToString();
            m_JsonReg = Regex.Replace(m_JsonReg, @"Singer\d+", "");
            return m_JsonReg;
        }
        //-----------------------
        public static string GetListRBT_Promotion()
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetListRBT_Promotion");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //Thong tin album
                smartReader.Read();
                m_str = JsonConvert.ToString("CategoryId") + ":" + smartReader.GetString("CategoryId");
                m_Json.AppendLine(m_str);
                m_str = ", " + JsonConvert.ToString("CategoryName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                m_Json.AppendLine(m_str);

                //Lay danh bai hat
                reader.NextResult();
                m_str = "," + JsonConvert.ToString("SongList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{" + "\n";
                    m_str = m_str + JsonConvert.ToString("SongSingerRBTId") + ":" + smartReader.GetString("SongSingerRBTId") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTName") + ":" + JsonConvert.ToString(smartReader.GetString("RBTName")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Singer") + ":" + JsonConvert.ToString("Singer" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("SingerImageUrl") + ":" + JsonConvert.ToString("SingerImageUrl" + smartReader.GetString("SongSingerId")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("RBTCode") + ":" + JsonConvert.ToString(smartReader.GetString("RBTCode")) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Price") + ":" + smartReader.GetString("Price") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("PromotionPrice") + ":" + smartReader.GetString("PromotionPrice") + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("Mp3Url") + ":" + JsonConvert.ToString(GetFullUrl(smartReader.GetString("FilePath"))) + "\n";
                    m_str = m_str + ", " + JsonConvert.ToString("ViewCount") + ":" + smartReader.GetString("ViewCount") + "\n";
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
                m_str = "]";
                m_Json.AppendLine(m_str);

                //Lay casy theo bai hat
                reader.NextResult();
                string m_Singer = "";
                string m_SingerImage = "";
                string m_SongSingerId = "";
                string m_SongSingerId_Pre = "";
                while (smartReader.Read())
                {
                    m_SongSingerId = smartReader.GetString("SongSingerId");
                    if (m_SongSingerId != m_SongSingerId_Pre)
                    {
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                        if (m_SongSingerId_Pre != "") m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                        m_Singer = "";
                        m_SingerImage = "";
                    }
                    if (m_Singer != "") m_Singer = m_Singer + ", ";
                    m_Singer = m_Singer + smartReader.GetString("SingerName");
                    if (m_SingerImage == "") m_SingerImage = smartReader.GetString("ImagePath");
                    m_SongSingerId_Pre = m_SongSingerId;
                }
                if (m_SongSingerId_Pre != "")
                {
                    m_Json = m_Json.Replace("Singer" + m_SongSingerId_Pre, m_Singer);
                    m_Json = m_Json.Replace("SingerImageUrl" + m_SongSingerId_Pre, GetImageUrl_Mobile(m_SingerImage));
                }

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
            string m_JsonReg = m_Json.ToString();
            m_JsonReg = Regex.Replace(m_JsonReg, @"Singer\d+", "");
            return m_JsonReg;
        }
        //-----------------------
        public static string GetListSinger(int CategoryId, int RowAmount, int PageIndex)
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
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetSingerByCategoryId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //Thong tin ca sy
                m_str = JsonConvert.ToString("SingerList") + ":" + "[";
                m_Json.AppendLine(m_str);
                while (smartReader.Read())
                {
                    m_str = "{";
                    m_str = m_str + JsonConvert.ToString("SingerId") + ":" + smartReader.GetString("ArticleId");
                    m_str = m_str + ", " + JsonConvert.ToString("SingerName") + ":" + JsonConvert.ToString(smartReader.GetString("Title"));
                    m_str = m_str + ", " + JsonConvert.ToString("ImageUrl") + ":" + JsonConvert.ToString(GetImageUrl_Mobile(smartReader.GetString("ImagePath")));
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
        public static string GetListMusicType()
        {
            StringBuilder m_Json = new StringBuilder();
            string m_str = "";
            int i = 0;
            m_Json.AppendLine("[");
            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();
            try
            {
                ArticlesViewCate RetVal = new ArticlesViewCate();
                SqlCommand cmd = new SqlCommand("Songs_MobileView_GetListMusicType");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                
                while (smartReader.Read())
                {
                    m_str = "{";
                    m_str = m_str + JsonConvert.ToString("MusicTypeId") + ":" + smartReader.GetString("CategoryId");
                    m_str = m_str + ", " + JsonConvert.ToString("MusicTypeName") + ":" + JsonConvert.ToString(smartReader.GetString("CategoryDesc"));
                    m_str = m_str + "}";
                    if (i > 0) m_str = "," + m_str;
                    m_Json.AppendLine(m_str);
                    i++;
                }
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
            m_Json.AppendLine("]");
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
        //-----------------------
        private static string GetFullUrl(string Url)
        {
            string RetVal = Url;
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http")) RetVal = CmsConstants.WEBSITE_MEDIADOMAIN + RetVal;
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
