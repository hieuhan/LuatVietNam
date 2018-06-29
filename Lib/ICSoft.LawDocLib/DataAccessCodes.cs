
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class DataAccessCodes
    {
        private int _DataAccessCodeId;
        private int _DataId;
        private byte _DataTypeId;
        private string _AccessCode;
        private DateTime _ExpiredTime;
        private string _Description;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DataAccessCodes()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DataAccessCodes(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DataAccessCodes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DataAccessCodeId
        {
            get { return _DataAccessCodeId; }
            set { _DataAccessCodeId = value; }
        }
        //-----------------------------------------------------------------
        public int DataId
        {
            get { return _DataId; }
            set { _DataId = value; }
        }
        //-----------------------------------------------------------------
        public byte DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string AccessCode
        {
            get { return _AccessCode; }
            set { _AccessCode = value; }
        }
        //-----------------------------------------------------------------
        public DateTime ExpiredTime
        {
            get { return _ExpiredTime; }
            set { _ExpiredTime = value; }
        }
        //-----------------------------------------------------------------
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<DataAccessCodes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DataAccessCodes> l_DataAccessCodes = new List<DataAccessCodes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DataAccessCodes m_DataAccessCodes = new DataAccessCodes(db.ConnectionString);
                    m_DataAccessCodes.DataAccessCodeId = smartReader.GetInt32("DataAccessCodeId");
                    m_DataAccessCodes.DataId = smartReader.GetInt32("DataId");
                    m_DataAccessCodes.DataTypeId = smartReader.GetByte("DataTypeId");
                    m_DataAccessCodes.AccessCode = smartReader.GetString("AccessCode");
                    m_DataAccessCodes.ExpiredTime = smartReader.GetDateTime("ExpiredTime");
                    m_DataAccessCodes.Description = smartReader.GetString("Description");
                    m_DataAccessCodes.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DataAccessCodes.Add(m_DataAccessCodes);
                }
                reader.Close();
                return l_DataAccessCodes;
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
        public List<DataAccessCodes> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int DataId, byte DataTypeId, string AccessCode, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DataAccessCodes> RetVal = new List<DataAccessCodes>();
            try
            {
                SqlCommand cmd = new SqlCommand("DataAccessCodes_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DataId", DataId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@AccessCode", StringUtil.InjectionString(AccessCode)));
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
        //--------------------------------------------------------------     
        public List<DataAccessCodes> Search(int ActUserId, string OrderBy, int DataId, byte DataTypeId, string AccessCode, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DataId, DataTypeId, AccessCode, DateFrom, DateTo, ref RowCount);
        }
    }
}