using sms.database;
using sms.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.LawDocsLib
{
    public class FieldsReports
    {
        private int _FieldId;
        private string _FieldDesc;
        private int _DaDuyet;
        private int _ChoDuyet;
        private int _Update;
        private int _Download;
        private DateTime _DateFrom;
        private DateTime _DateTo;
        private DBAccess db;

        //-----------------------------------------------------------------
        public FieldsReports()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public FieldsReports(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FieldsReports()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        public int FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }

        public string FieldDesc
        {
            get { return _FieldDesc; }
            set { _FieldDesc = value; }
        }

        public int DaDuyet
        {
            get { return _DaDuyet; }
            set { _DaDuyet = value; }
        }

        public int ChoDuyet
        {
            get { return _ChoDuyet; }
            set { _ChoDuyet = value; }
        }

        public int Update
        {
            get { return _Update; }
            set { _Update = value; }
        }

        public int Download
        {
            get { return _Download; }
            set { _Download = value; }
        }

        public DateTime DateFrom
        {
            get { return _DateFrom; }
            set { _DateFrom = value; }
        }

        public DateTime DateTo
        {
            get { return _DateTo; }
            set { _DateTo = value; }
        }

        //-----------------------------------------------------------------

        private List<FieldsReports> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FieldsReports> l_FieldsReport = new List<FieldsReports>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FieldsReports m_FieldReport = new FieldsReports(db.ConnectionString);
                    m_FieldReport.FieldDesc = smartReader.GetString("FieldDesc");
                    m_FieldReport.ChoDuyet = smartReader.GetInt32("ChoDuyet");
                    m_FieldReport.DaDuyet = smartReader.GetInt32("DaDuyet");
                    m_FieldReport.Update = smartReader.GetInt32("Update");
                    m_FieldReport.Download = smartReader.GetInt32("Download");

                    l_FieldsReport.Add(m_FieldReport);
                }
                reader.Close();
                return l_FieldsReport;
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
        public List<FieldsReports> GetList(int ActUserId, short Week, byte Month, int Year, string DateFrom, string DateTo, string OrderBy, int PageIndex, int RowAmount, ref int RowCount)
        {
            List<FieldsReports> RetVal = new List<FieldsReports>();
            try
            {
                SqlCommand cmd = new SqlCommand("FieldsReport_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@Week", Week));
                cmd.Parameters.Add(new SqlParameter("@Month", Month));
                cmd.Parameters.Add(new SqlParameter("@Year", Year));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                //RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RetVal;
        }
    }
}
