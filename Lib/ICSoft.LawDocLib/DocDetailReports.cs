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
    public class DocDetailReports
    {
        private int _DocId;
        private string _DocName;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _UpdUserId;
        private DateTime _UpdDateTime;
        private int _RevUserId;
        private DateTime _RevDateTime;
        private int _Download;
        private string _OrganDesc;
        private string _DataSourceDesc;
        private string _FieldName;
        private string _SearchByDate;
        private byte _ReviewStatusId;
        private byte _LanguageId;
        private DateTime _GazetteDate;
        private DateTime _DateFrom;
        private DateTime _DateTo;
        private DBAccess db;

        //-----------------------------------------------------------------
        public DocDetailReports()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocDetailReports(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocDetailReports()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }

        public string DocName
        {
            get { return _DocName; }
            set { _DocName = value; }
        }

        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }

        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }

        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }

        public int UpdUserId
        {
            get { return _UpdUserId; }
            set { _UpdUserId = value; }
        }

        public DateTime UpdDateTime
        {
            get { return _UpdDateTime; }
            set { _UpdDateTime = value; }
        }

        public int RevUserId
        {
            get { return _RevUserId; }
            set { _RevUserId = value; }
        }

        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }

        public DateTime RevDateTime
        {
            get { return _RevDateTime; }
            set { _RevDateTime = value; }
        }

        public string OrganDesc
        {
            get { return _OrganDesc; }
            set { _OrganDesc = value; }
        }

        public string DataSourceDesc
        {
            get { return _DataSourceDesc; }
            set { _DataSourceDesc = value; }
        }

        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        public int Download
        {
            get { return _Download; }
            set { _Download = value; }
        }

        public string SearchByDate
        {
            get { return _SearchByDate; }
            set { _SearchByDate = value; }
        }

        public DateTime GazetteDate
        {
            get { return _GazetteDate; }
            set { _GazetteDate = value; }
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

        private List<DocDetailReports> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocDetailReports> l_DocDetailReport = new List<DocDetailReports>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocDetailReports m_DocDetailReport = new DocDetailReports(db.ConnectionString);
                    m_DocDetailReport.DocId = smartReader.GetInt32("DocId");
                    m_DocDetailReport.DocName = smartReader.GetString("DocName");
                    m_DocDetailReport.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocDetailReport.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_DocDetailReport.UpdUserId = smartReader.GetInt32("UpdUserId");
                    m_DocDetailReport.UpdDateTime = smartReader.GetDateTime("UpdDateTime");
                    m_DocDetailReport.RevUserId = smartReader.GetInt32("RevUserId");
                    m_DocDetailReport.RevDateTime = smartReader.GetDateTime("RevDateTime");
                    m_DocDetailReport.Download = smartReader.GetInt32("Download");
                    m_DocDetailReport.OrganDesc = smartReader.GetString("OrganDesc");
                    m_DocDetailReport.DataSourceDesc = smartReader.GetString("DataSourceDesc");
                    m_DocDetailReport.FieldName = smartReader.GetString("FieldName");
                    m_DocDetailReport.GazetteDate = smartReader.GetDateTime("GazetteDate");
                    m_DocDetailReport.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocDetailReport.LanguageId = smartReader.GetByte("LanguageId");

                    l_DocDetailReport.Add(m_DocDetailReport);
                }
                reader.Close();
                return l_DocDetailReport;
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
        public List<DocDetailReports> GetPage(int ActUserId, string SearchByDate, short FieldId, byte OrganTypeId, short OrganId, byte ReviewStatusId, int CrUserId, int RevUserId, short DataSourceId, short Week, byte Month, int Year, string DateFrom, string DateTo, string OrderBy, int PageIndex, int RowAmount, ref int CountPending, ref int CountReviewed, ref int CountChanged, ref int RowCount)
        {
            List<DocDetailReports> RetVal = new List<DocDetailReports>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_ReportDetail_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
                cmd.Parameters.Add(new SqlParameter("@RevUserId", RevUserId));
                cmd.Parameters.Add(new SqlParameter("@DataSourceId", DataSourceId));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", SearchByDate));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@CountPending", SqlDbType.Int).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@CountReviewed", SqlDbType.Int).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@CountChanged", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                //CountPending = int.Parse(cmd.Parameters["@CountPending"].Value.ToString());
                //CountReviewed = int.Parse(cmd.Parameters["@CountReviewed"].Value.ToString());
                //CountChanged = int.Parse(cmd.Parameters["@CountChanged"].Value.ToString());
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
