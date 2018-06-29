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
    public class OrganReports
    {
        private int _OrganId;
        private string _OrganDesc;
        private int _DaDuyet;
        private int _ChoDuyet;
        private int _Update;
        private int _Download;
        private DateTime _DateFrom;
        private DateTime _DateTo;
        private DBAccess db;

        //-----------------------------------------------------------------
        public OrganReports()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public OrganReports(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~OrganReports()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        public int OrganId
        {
            get { return _OrganId; }
            set { _OrganId = value; }
        }

        public string OrganDesc
        {
            get { return _OrganDesc; }
            set { _OrganDesc = value; }
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

        private List<OrganReports> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<OrganReports> l_OrganReports = new List<OrganReports>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    OrganReports m_OrganReport = new OrganReports(db.ConnectionString);
                    m_OrganReport.OrganDesc = smartReader.GetString("OrganDesc");
                    m_OrganReport.ChoDuyet = smartReader.GetInt32("ChoDuyet");
                    m_OrganReport.DaDuyet = smartReader.GetInt32("DaDuyet");
                    m_OrganReport.Update = smartReader.GetInt32("Update");
                    m_OrganReport.Download = smartReader.GetInt32("Download");

                    l_OrganReports.Add(m_OrganReport);
                }
                reader.Close();
                return l_OrganReports;
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
        public List<OrganReports> GetList(int ActUserId, short Week, byte Month, int Year, string DateFrom, string DateTo, string OrderBy, int PageIndex, int RowAmount, ref int RowCount)
        {
            List<OrganReports> RetVal = new List<OrganReports>();
            try
            {
                SqlCommand cmd = new SqlCommand("OrganReport_GetPage");
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
                RetVal = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RetVal;
        }
    }
}
