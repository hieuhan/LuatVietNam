
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class SystemErrors
    {
        private int _SystemErrorId;
        private byte _ErrorLevelId;
        private string _SystemErrorInfo;
        private int _ObjectId;
        private string _ObjectName;
        private DateTime _CrdateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public SystemErrors()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public SystemErrors(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~SystemErrors()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int SystemErrorId
        {
            get { return _SystemErrorId; }
            set { _SystemErrorId = value; }
        }
        //-----------------------------------------------------------------
        public byte ErrorLevelId
        {
            get { return _ErrorLevelId; }
            set { _ErrorLevelId = value; }
        }
        //-----------------------------------------------------------------
        public string SystemErrorInfo
        {
            get { return _SystemErrorInfo; }
            set { _SystemErrorInfo = value; }
        }
        //-----------------------------------------------------------------
        public int ObjectId
        {
            get { return _ObjectId; }
            set { _ObjectId = value; }
        }
        //-----------------------------------------------------------------
        public string ObjectName
        {
            get { return _ObjectName; }
            set { _ObjectName = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrdateTime
        {
            get { return _CrdateTime; }
            set { _CrdateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<SystemErrors> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SystemErrors> l_SystemErrors = new List<SystemErrors>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SystemErrors m_SystemErrors = new SystemErrors(db.ConnectionString);
                    m_SystemErrors.SystemErrorId = smartReader.GetInt32("SystemErrorId");
                    m_SystemErrors.ErrorLevelId = smartReader.GetByte("ErrorLevelId");
                    m_SystemErrors.SystemErrorInfo = smartReader.GetString("SystemErrorInfo");
                    m_SystemErrors.ObjectId = smartReader.GetInt32("ObjectId");
                    m_SystemErrors.ObjectName = smartReader.GetString("ObjectName");
                    m_SystemErrors.CrdateTime = smartReader.GetDateTime("CrdateTime");

                    l_SystemErrors.Add(m_SystemErrors);
                }
                reader.Close();
                return l_SystemErrors;
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
        //-----------------------------------------------------------------    
        public DataSet GetTopRow(string myTable, int PageSize)
        {
            try
            {
                string sql = "SELECT TOP(" + PageSize.ToString() + ") * FROM " + myTable + " ORDER BY CrDateTime desc";
                SqlCommand cmd = new SqlCommand(sql);
                return db.getDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-----------------------------------------------------------
        public List<SystemErrors> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string SystemErrorInfo, string DateFrom, string DateTo, ref int RowCount)
        {
            List<SystemErrors> RetVal = new List<SystemErrors>();
            try
            {
                SqlCommand cmd = new SqlCommand("SystemErrors_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SystemErrorInfo", StringUtil.InjectionString(SystemErrorInfo)));
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

    }
}

