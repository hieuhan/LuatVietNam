
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
    public class LawersViewDetail 
    {
        private Lawers _mLawersDetail;
        private List<Lawers> _lLawersOther;
        public Lawers mLawersDetail
        {
            get { return _mLawersDetail; }
            set { _mLawersDetail = value; }
        }
        public List<Lawers> lLawersOther
        {
            get { return _lLawersOther; }
            set { _lLawersOther = value; }
        }
        private DBAccess db;
        //-----------------------------------------------------------------
        public LawersViewDetail()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public LawersViewDetail(string constr)
        {

        }
        //-----------------------------------------------------------------
        ~LawersViewDetail()
        {

        }
        //--------------------------------------------------------------     
        public LawersViewDetail GetViewDetail(int LawerId, ref int RowCount)
        {
            DBAccess db = new DBAccess(DocConstants.DOC_CONSTR);
            SqlConnection con = db.getConnection();
            LawersViewDetail RetVal = new LawersViewDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("Lawers_GetViewDetail");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@LawerId", LawerId));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    SmartDataReader smartReader = new SmartDataReader(reader);
                    RetVal.mLawersDetail = Inits(smartReader);
                    reader.NextResult();
                    RetVal.lLawersOther = InitLawers(smartReader);
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

        private Lawers Inits(SmartDataReader smartReader)
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
                if (l_Lawers.Count > 0)
                {
                    return (Lawers)l_Lawers[0];
                }
                else return new Lawers();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
    }
}