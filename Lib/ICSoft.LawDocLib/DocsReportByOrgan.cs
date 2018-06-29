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
    public class DocsReportByOrgan
    {
        private int _OrganId;
        private string _OrganName;
        public int _OrganTypeId;
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
        public DocsReportByOrgan()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocsReportByOrgan(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocsReportByOrgan()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------
        public int OrganId
        {
            get { return _OrganId; }
            set { _OrganId = value; }
        }
        
        //-----------------------------------------------------------------    
        public string OrganName
        {
            get { return _OrganName; }
            set { _OrganName = value; }
        }
        //-----------------------------------------------------------------
        public int OrganTypeId
        {
            get { return _OrganTypeId; }
            set { _OrganTypeId = value; }
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

        private List<DocsReportByOrgan> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocsReportByOrgan> l_DocsReportByOrgan = new List<DocsReportByOrgan>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocsReportByOrgan m_DocsReportDataByDocType = new DocsReportByOrgan(db.ConnectionString);
                    m_DocsReportDataByDocType.OrganId = smartReader.GetInt32("OrganId");
                    m_DocsReportDataByDocType.OrganName = smartReader.GetString("OrganName");
                    m_DocsReportDataByDocType.OrganTypeId = smartReader.GetInt32("OrganTypeId");
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
                    l_DocsReportByOrgan.Add(m_DocsReportDataByDocType);
                }
                reader.Close();
                return l_DocsReportByOrgan;
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
        public List<DocsReportByOrgan> GetDocsReportDataByOrgan(string DateFrom, string DateTo, string DateFromSame, string DateToSame, short ReviewStatusId, short DataSourceId, short DocGroupId, short FieldId, short DocTypeId)
        {
            List<DocsReportByOrgan> RetVal = new List<DocsReportByOrgan>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_ReportDataByOrgan");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                if (DateFromSame != "") cmd.Parameters.Add(new SqlParameter("@DateFromSame", StringUtil.ConvertToDateTime(DateFromSame)));
                if (DateToSame != "") cmd.Parameters.Add(new SqlParameter("@DateToSame", StringUtil.ConvertToDateTime(DateToSame)));
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
