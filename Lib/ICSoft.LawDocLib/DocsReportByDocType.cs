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
    public class DocsReportByDocType
    {
        private int _DocTypeId;
        private string _DocTypeName;
        private int _SumByStatus1;
        private int _SumByStatus2;
        private int _SumByStatus3;
        private int _SumBySource1;
        private int _SumBySource2;
        private int _SumBySource3;
        private int _SumByDownload1;
        private int _SumByDownload2;
        private int _SumByView1;
        private int _SumByView2;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocsReportByDocType()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocsReportByDocType(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocsReportByDocType()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------
        public int DocTypeId
        {
            get { return _DocTypeId; }
            set { _DocTypeId = value; }
        }
        
        //-----------------------------------------------------------------    
        public string DocTypeName
        {
            get { return _DocTypeName; }
            set { _DocTypeName = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus1
        {
            get { return _SumByStatus1; }
            set { _SumByStatus1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus2
        {
            get { return _SumByStatus2; }
            set { _SumByStatus2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByStatus3
        {
            get { return _SumByStatus3; }
            set { _SumByStatus3 = value; }
        }
        
        //-----------------------------------------------------------------
        public int SumBySource1
        {
            get { return _SumBySource1; }
            set { _SumBySource1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumBySource2
        {
            get { return _SumBySource2; }
            set { _SumBySource2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumBySource3
        {
            get { return _SumBySource3; }
            set { _SumBySource3 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByDownload1
        {
            get { return _SumByDownload1; }
            set { _SumByDownload1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByDownload2
        {
            get { return _SumByDownload2; }
            set { _SumByDownload2 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByView1
        {
            get { return _SumByView1; }
            set { _SumByView1 = value; }
        }
        //-----------------------------------------------------------------
        public int SumByView2
        {
            get { return _SumByView2; }
            set { _SumByView2 = value; }
        }
        //-----------------------------------------------------------------

        private List<DocsReportByDocType> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocsReportByDocType> l_DocsReportDataByDocType = new List<DocsReportByDocType>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocsReportByDocType m_DocsReportDataByDocType = new DocsReportByDocType(db.ConnectionString);
                    m_DocsReportDataByDocType.DocTypeId = smartReader.GetInt32("DocTypeId");
                    m_DocsReportDataByDocType.DocTypeName = smartReader.GetString("DocTypeName");
                    m_DocsReportDataByDocType.SumByStatus1 = smartReader.GetInt32("SumByStatus1");
                    m_DocsReportDataByDocType.SumByStatus2 = smartReader.GetInt32("SumByStatus2");
                    m_DocsReportDataByDocType.SumByStatus3 = smartReader.GetInt32("SumByStatus3");
                    m_DocsReportDataByDocType.SumBySource1 = smartReader.GetInt32("SumBySource1");
                    m_DocsReportDataByDocType.SumBySource2 = smartReader.GetInt32("SumBySource2");
                    m_DocsReportDataByDocType.SumBySource3 = smartReader.GetInt32("SumBySource3");
                    m_DocsReportDataByDocType.SumByDownload1 = smartReader.GetInt32("SumByDownload1");
                    m_DocsReportDataByDocType.SumByDownload2 = smartReader.GetInt32("SumByDownload2");
                    m_DocsReportDataByDocType.SumByView1 = smartReader.GetInt32("SumByView1");
                    m_DocsReportDataByDocType.SumByView2 = smartReader.GetInt32("SumByView2");
                    l_DocsReportDataByDocType.Add(m_DocsReportDataByDocType);
                }
                reader.Close();
                return l_DocsReportDataByDocType;
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
        public List<DocsReportByDocType> GetDocsReportDataByDocType(string DateFrom, string DateTo, string DateFromSame, string DateToSame, short ReviewStatusId, short DataSourceId, short DocGroupId, short OrganId, short FieldId)
        {
            List<DocsReportByDocType> RetVal = new List<DocsReportByDocType>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_ReportDataByDocType");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                if (DateFromSame != "") cmd.Parameters.Add(new SqlParameter("@DateFromSame", StringUtil.ConvertToDateTime(DateFromSame)));
                if (DateToSame != "") cmd.Parameters.Add(new SqlParameter("@DateToSame", StringUtil.ConvertToDateTime(DateToSame)));
                //cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                //RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}