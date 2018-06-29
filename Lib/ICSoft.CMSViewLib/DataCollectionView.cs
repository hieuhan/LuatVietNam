
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;


namespace ICSoft.CMSViewLib
{
    public class DataCollectionView
    {
        public byte GetReviewStatus = 0;
        public byte GetUser = 0;
        public byte GetCountry = 0;
        public byte GetTelco = 0;

        private List<KeyValuesView> _lReviewStatus;
        private List<KeyValuesView> _lUser;
        private List<KeyValuesView> _lCountry;
        private List<KeyValuesView> _lTelco;

        //-----------------------------------------------------------------
        public DataCollectionView()
        {
        }
        //-----------------------------------------------------------------
        ~DataCollectionView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public List<KeyValuesView> lReviewStatus
        {
            get { return _lReviewStatus; }
            set { _lReviewStatus = value; }
        }
        //-----------------------------------------------------------------    
        public List<KeyValuesView> lUser
        {
            get { return _lUser; }
            set { _lUser = value; }
        }
        //-----------------------------------------------------------------    
        public List<KeyValuesView> lCountry
        {
            get { return _lCountry; }
            set { _lCountry = value; }
        }
        //-----------------------------------------------------------------    
        public List<KeyValuesView> lTelco
        {
            get { return _lTelco; }
            set { _lTelco = value; }
        }
        //-----------------------------------------------------------------    
        public void Init()
        {
            int DataCollectionCount = 0;
            if (GetReviewStatus == 1) DataCollectionCount++;
            if (GetUser == 1) DataCollectionCount++;
            if (GetCountry == 1) DataCollectionCount++;
            if (GetTelco == 1) DataCollectionCount++;

            DBAccess db = new DBAccess(CmsConstants.CMS_CONSTR);
            SqlConnection con = db.getConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("DataCollection_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@GetReviewStatus", GetReviewStatus));
                cmd.Parameters.Add(new SqlParameter("@GetUser", GetUser));
                cmd.Parameters.Add(new SqlParameter("@GetCountry", GetCountry));
                cmd.Parameters.Add(new SqlParameter("@GetTelco", GetTelco));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                for (int i = 0; i < DataCollectionCount; i++)
                {
                    if (i > 0) reader.NextResult();
                    if (GetReviewStatus == 1 && _lReviewStatus == null) _lReviewStatus = KeyValuesView.Init(smartReader);
                    else if (GetUser == 1 && _lUser == null) _lUser = KeyValuesView.Init(smartReader);
                    else if (GetCountry == 1 && _lCountry == null) _lCountry = KeyValuesView.Init(smartReader);
                    else if (GetTelco == 1 && _lTelco == null) _lTelco = KeyValuesView.Init(smartReader);
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
        }
    }
}