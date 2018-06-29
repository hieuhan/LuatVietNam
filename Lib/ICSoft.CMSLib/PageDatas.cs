
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSLib
{
    public class PageDatas: Pages
    {
        private short _CategoryId;

        public short CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        
       
        private DBAccess db;
        //-----------------------------------------------------------------
		public PageDatas()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public PageDatas(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~PageDatas()
        {

		}
                
        private List<PageDatas> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<PageDatas> l_PageDatas = new List<PageDatas>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PageDatas m_PageDatas = new PageDatas(db.ConnectionString);
                    //inherit from PageDatas
                    m_PageDatas.LanguageId = smartReader.GetByte("LanguageId");
                    m_PageDatas.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_PageDatas.PageId = smartReader.GetInt16("PageId");
                    m_PageDatas.PageTypeId = smartReader.GetByte("PageTypeId");
                    m_PageDatas.PageName = smartReader.GetString("PageName");
                    m_PageDatas.PageTitle = smartReader.GetString("PageTitle");
                    m_PageDatas.PageKeyword = smartReader.GetString("PageKeyword");
                    m_PageDatas.PageDesciption = smartReader.GetString("PageDesciption");
                    m_PageDatas.IconPath = smartReader.GetString("IconPath");
                    m_PageDatas.Url = smartReader.GetString("Url");
                    m_PageDatas.ParentId = smartReader.GetInt16("ParentId");
                    m_PageDatas.LevelId = smartReader.GetByte("LevelId");
                    m_PageDatas.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_PageDatas.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_PageDatas.CrUserId = smartReader.GetInt32("CrUserId");
                    m_PageDatas.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    // other data
                    m_PageDatas.CategoryId = smartReader.GetInt16("CategoryId");
                   

                    l_PageDatas.Add(m_PageDatas);
                }
                reader.Close();
                return l_PageDatas;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
        }
        //--------------------------------------------------------------  
        public static PageDatas PageDatas_StaticGet(short PageId)
        {
            PageDatas RetVal = new PageDatas();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PageName = "";
                short DisplayOrder = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                byte LanguageId = 0;
                byte ApplicationTypeId = 0;
                byte MenuId = 0;
                byte ReviewStatusId = 0;
                List<PageDatas> l_PageDatas = RetVal.PageDatas_GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
                if (l_PageDatas.Count > 0)
                {
                    RetVal = l_PageDatas[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public static PageDatas PageDatas_StaticGet(short PageId, byte LanguageId, byte ApplicationTypeId)
        {
            PageDatas RetVal = new PageDatas();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PageName = "";
                short DisplayOrder = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                byte MenuId = 0;
                byte ReviewStatusId = 0;
                List<PageDatas> l_PageDatas = RetVal.PageDatas_GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
                if (l_PageDatas.Count > 0)
                {
                    RetVal = l_PageDatas[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static PageDatas PageDatas_StaticGetByMenuId(byte MenuId)
        {
            PageDatas RetVal = new PageDatas();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PageName = "";
                short DisplayOrder = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                byte LanguageId = 0;
                byte ApplicationTypeId = 0;
                short PageId = 0;
                byte ReviewStatusId = 0;
                List<PageDatas> l_PageDatas = RetVal.PageDatas_GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
                if (l_PageDatas.Count > 0)
                {
                    RetVal = l_PageDatas[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static PageDatas PageDatas_StaticGetByMenuId(byte MenuId, byte LanguageId, byte ApplicationTypeId)
        {
            PageDatas RetVal = new PageDatas();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string PageName = "";
                short DisplayOrder = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                short PageId = 0;
                byte ReviewStatusId = 0;
                List<PageDatas> l_PageDatas = RetVal.PageDatas_GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, ApplicationTypeId, MenuId, PageId, PageName, DisplayOrder, ReviewStatusId, DateFrom, DateTo, ref RowCount);
                if (l_PageDatas.Count > 0)
                {
                    RetVal = l_PageDatas[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<PageDatas> PageDatas_GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, byte ApplicationTypeId, byte MenuId, short PageId, string PageName, short DisplayOrder, byte ReviewStatusId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<PageDatas> RetVal = new List<PageDatas>();
            try
            {
                SqlCommand cmd = new SqlCommand("PageDatas_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@MenuId", MenuId));
                cmd.Parameters.Add(new SqlParameter("@PageId", PageId));
                cmd.Parameters.Add(new SqlParameter("@PageName", StringUtil.InjectionString(PageName)));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        
    } 
}
