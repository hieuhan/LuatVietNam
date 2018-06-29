using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sms.database;
using sms.utils;

namespace ICSoft.LawDocsLib
{
    public class DocsReportLoaiVB_GetPage
    {
        private int _DocId;
        private string _Loaivanban;
        private int _DaDuyet;
        private int _ChoDuyet;
        private int _TacDongThayDoi;
        private int _NguonLVN;
        private int _NguonNgoai;
        private int _LuotTai;
        private byte _LanguageId;
        private DateTime _DateFrom;
        private DateTime _DateTo;
        private DBAccess db;

        //-----------------------------------------------------------------
        public DocsReportLoaiVB_GetPage()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocsReportLoaiVB_GetPage(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocsReportLoaiVB_GetPage()
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

        public string Loaivanban
        {
            get { return _Loaivanban; }
            set { _Loaivanban = value; }
        }
            public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
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

        public int TacDongThayDoi
        {
            get { return _TacDongThayDoi; }
            set { _TacDongThayDoi = value; }
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

        public int LuotTai
        {
            get { return _LuotTai; }
            set { _LuotTai = value; }
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

        private List<DocsReportLoaiVB_GetPage> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocsReportLoaiVB_GetPage> l_DocReport = new List<DocsReportLoaiVB_GetPage>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocsReportLoaiVB_GetPage m_DocReport = new DocsReportLoaiVB_GetPage(db.ConnectionString);
                    m_DocReport.Loaivanban = smartReader.GetString("Loaivanban");
                    m_DocReport.ChoDuyet = smartReader.GetInt32("ChoDuyet");
                    m_DocReport.DaDuyet = smartReader.GetInt32("DaDuyet");
                    m_DocReport.TacDongThayDoi = smartReader.GetInt32("TacDongThayDoi");
                    m_DocReport.NguonLVN = smartReader.GetInt32("NguonLVN");
                    m_DocReport.NguonNgoai = smartReader.GetInt32("NguonNgoai");
                    m_DocReport.LuotTai = smartReader.GetInt32("LuotTai");
                    l_DocReport.Add(m_DocReport);
                }
                reader.Close();
                return l_DocReport;
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
        public List<DocsReportLoaiVB_GetPage> GetList(int ActUserId, int DocId, byte LanguageId, short Week, byte Month, int Year, string DateFrom, string DateTo)
        {
            List<DocsReportLoaiVB_GetPage> RetVal = new List<DocsReportLoaiVB_GetPage>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocsReportLoaiVB_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@Week", Week));
                cmd.Parameters.Add(new SqlParameter("@Month", Month));
                cmd.Parameters.Add(new SqlParameter("@Year", Year));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
               // cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                //cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
               // cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
               // cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
               // RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                RetVal = Init(cmd);
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
