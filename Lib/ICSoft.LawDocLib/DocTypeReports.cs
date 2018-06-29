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
    public class DocTypeReports
    {
        private byte _DocTypeId;
        private string _DocTypeDesc;
        private int _DaDuyet;
        private int _ChoDuyet;
        private int _Update;
        private int _Download;
        private int _NguonLVN;
        private int _NguonNgoai;
        private byte _DocGroupId;
        private DateTime _DateFrom;
        private DateTime _DateTo;
        private DBAccess db;

        //-----------------------------------------------------------------
        public DocTypeReports()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocTypeReports(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocTypeReports()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        public byte DocTypeId
        {
            get { return _DocTypeId; }
            set { _DocTypeId = value; }
        }

        public string DocTypeDesc
        {
            get { return _DocTypeDesc; }
            set { _DocTypeDesc = value; }
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

        public int NguonLVN
        {
            get { return _NguonLVN; }
            set { _NguonLVN = value; }
        }

        public int NguonNgoai
        {
            get { return _NguonNgoai; }
            set { _NguonNgoai = value; }
        }

        public byte DocGroupId
        {
            get { return _DocGroupId; }
            set { _DocGroupId = value; }
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

        private List<DocTypeReports> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocTypeReports> l_DocTypeReport = new List<DocTypeReports>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocTypeReports m_DocTypeReport = new DocTypeReports(db.ConnectionString);
                    m_DocTypeReport.DocTypeId = smartReader.GetByte("DocTypeId");
                    m_DocTypeReport.DocTypeDesc = smartReader.GetString("DocTypeDesc");
                    m_DocTypeReport.ChoDuyet = smartReader.GetInt32("ChoDuyet");
                    m_DocTypeReport.DaDuyet = smartReader.GetInt32("DaDuyet");
                    m_DocTypeReport.Update = smartReader.GetInt32("Update");
                    m_DocTypeReport.NguonLVN = smartReader.GetInt32("NguonLVN");
                    m_DocTypeReport.NguonNgoai = smartReader.GetInt32("NguonNgoai");
                    m_DocTypeReport.Download = smartReader.GetInt32("Download");

                    l_DocTypeReport.Add(m_DocTypeReport);
                }
                reader.Close();
                return l_DocTypeReport;
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
        public List<DocTypeReports> GetList(int ActUserId, byte DocGroupId, short Week, byte Month, int Year, string DateFrom, string DateTo, string OrderBy, int PageIndex, int RowAmount, ref int RowCount)
        {
            List<DocTypeReports> RetVal = new List<DocTypeReports>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocsTypeReport_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
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
        //-------------------------------------------------------------- 

    }
}
