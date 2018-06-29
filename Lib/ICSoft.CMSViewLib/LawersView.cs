
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;
using ICSoft.HelperLib;
using ICSoft.LawDocsLib;

namespace ICSoft.CMSViewLib
{

    /// <summary>
    /// class Lawers
    /// </summary>
    public class LawersView 
    {
        private List<Lawers> _lLawers;
        private List<Lawers> _lLawersMostView;
        private List<Lawers> _lLawersByGroupView;
        private List<Provinces> _lProvinces;
        public List<Lawers> lLawers
        {
            get { return _lLawers; }
            set { _lLawers = value; }
        }
        public List<Lawers> lLawersMostView
        {
            get { return _lLawersMostView; }
            set { _lLawersMostView = value; }
        }
        public List<Lawers> lLawersByGroupView
        {
            get { return _lLawersByGroupView; }
            set { _lLawersByGroupView = value; }
        }
        public List<Provinces> lProvinces
        {
            get { return _lProvinces; }
            set { _lProvinces = value; }
        }
        private DBAccess db;
        //-----------------------------------------------------------------
        public LawersView()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public LawersView(string constr)
        {

        }
        //-----------------------------------------------------------------
        ~LawersView()
        {

        }
       
        //-------------------------------------------------------------- 
        public static string GetLawerUrl(int LawerId, string LawerName)
        {
            string RetVal = sms.utils.StringUtil.RemoveSign4VietnameseString(LawerName.ToLower());
            RetVal = RetVal.Replace(" ", "-");
            RetVal = RetVal.Replace("/", "-");
            RetVal = RetVal.Replace("?", "");
            RetVal = RetVal.Replace(":", "");
            RetVal = RetVal.Replace(",", "");
            while (RetVal.Contains("--"))
            {
                RetVal = RetVal.Replace("--", "-");
            }
            RetVal = RetVal + "-" + LawerId.ToString() + "-lawer.html";
            if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }
        
        //--------------------------------------------------------------     
        public LawersView GetPageView(int ActUserId, string Keyword, string StartName,byte LawerGroupId, short FieldId,int ProviceId, int DistricId, int WardId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, byte IsCountProvice, byte IsGetMostView, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            LawersView RetVal = new LawersView();
            try
            {
                SqlCommand cmd = new SqlCommand("Lawers_GetPageView");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@LawerGroupId", LawerGroupId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@ProviceId", ProviceId));
                cmd.Parameters.Add(new SqlParameter("@DistricId", DistricId));
                cmd.Parameters.Add(new SqlParameter("@WardId", WardId));
                cmd.Parameters.Add(new SqlParameter("@Keyword", Keyword));
                cmd.Parameters.Add(new SqlParameter("@StartName", StartName));
                cmd.Parameters.Add(new SqlParameter("@IsCountProvice", IsCountProvice));
                cmd.Parameters.Add(new SqlParameter("@IsGetMostView", IsGetMostView));

                cmd.Parameters.Add(new SqlParameter("@PageSize", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    RetVal.lLawers = InitLawers(smartReader);
                    if (IsCountProvice > 0)
                    {
                        reader.NextResult();
                        RetVal.lProvinces = InitViewHelpers.InitProvincesSearchView(smartReader);
                    }
                    if (IsGetMostView > 0)
                    {
                        reader.NextResult();
                        RetVal.lLawersMostView = InitLawers(smartReader);
                    }
                    if (LawerGroupId > 0)
                    {
                        reader.NextResult();
                        RetVal.lLawersByGroupView = InitLawers(smartReader);
                    }
                }
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value == DBNull.Value ? "0" : cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        private List<Lawers> InitLawers(SmartDataReader smartReader)
        {
            List<Lawers> l_Lawers = new List<Lawers>();
            Lawers m_Lawers;
            try
            {
                while (smartReader.Read())
                {
                    m_Lawers = new Lawers();
                    m_Lawers.LawerID = smartReader.GetInt32("LawerID");
                    m_Lawers.FullName = smartReader.GetString("FullName");
                    m_Lawers.ImagePath = smartReader.GetString("ImagePath");
                    m_Lawers.Address = smartReader.GetString("Address");
                    m_Lawers.ProviceId = smartReader.GetInt16("ProviceId");
                    m_Lawers.DistricId = smartReader.GetInt16("DistricId");
                    m_Lawers.WardId = smartReader.GetInt16("WardId");
                    m_Lawers.PhoneNumber = smartReader.GetString("PhoneNumber");
                    m_Lawers.Mobile = smartReader.GetString("Mobile");
                    m_Lawers.Email = smartReader.GetString("Email");
                    m_Lawers.Website = smartReader.GetString("Website");
                    m_Lawers.LawOfficeName = smartReader.GetString("LawOfficeName");
                    m_Lawers.Experience = smartReader.GetString("Experience");
                    m_Lawers.Education = smartReader.GetString("Education");
                    m_Lawers.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Lawers.FieldId = smartReader.GetInt16("FieldId");
                    m_Lawers.LawerGroupId = smartReader.GetByte("LawerGroupId");
                    m_Lawers.Content = smartReader.GetString("Content");
                    m_Lawers.CrUserId = smartReader.GetByte("CrUserId");
                    m_Lawers.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Lawers.LawerUrl = smartReader.GetString("LawerUrl");
                    m_Lawers.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Lawers.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Lawers.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Lawers.FieldsName = smartReader.GetString("FieldsName");
                    l_Lawers.Add(m_Lawers);
                }
                return l_Lawers;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
    }
}