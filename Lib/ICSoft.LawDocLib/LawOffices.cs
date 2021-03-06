
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
    /// <summary>
    /// class LawOffices
    /// </summary>
    public class LawOffices
    {
        private byte _LanguageId;
        private int _LawOfficeId;
        private string _OfficeName;
        private string _OfficeNameEn;
        private string _ShortName;
        private byte _OfficeTypeId;
        private short _CountryId;
        private short _ProvinceId;
        private string _Address;
        private string _PhoneNumber;
        private string _Fax;
        private string _Email;
        private string _Website;
        private string _ConsultancySector;
        private string _OtherInfo;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public LawOffices()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public LawOffices(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~LawOffices()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------    
        public int LawOfficeId
        {
            get { return _LawOfficeId; }
            set { _LawOfficeId = value; }
        }
        //-----------------------------------------------------------------
        public string OfficeName
        {
            get { return _OfficeName; }
            set { _OfficeName = value; }
        }
        //-----------------------------------------------------------------
        public string OfficeNameEn
        {
            get { return _OfficeNameEn; }
            set { _OfficeNameEn = value; }
        }
        //-----------------------------------------------------------------
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        //-----------------------------------------------------------------
        public byte OfficeTypeId
        {
            get { return _OfficeTypeId; }
            set { _OfficeTypeId = value; }
        }
        //-----------------------------------------------------------------
        public short CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        //-----------------------------------------------------------------
        public short ProvinceId
        {
            get { return _ProvinceId; }
            set { _ProvinceId = value; }
        }
        //-----------------------------------------------------------------
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        //-----------------------------------------------------------------
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        //-----------------------------------------------------------------
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }
        //-----------------------------------------------------------------
        public string ConsultancySector
        {
            get { return _ConsultancySector; }
            set { _ConsultancySector = value; }
        }
        //-----------------------------------------------------------------
        public string OtherInfo
        {
            get { return _OtherInfo; }
            set { _OtherInfo = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public int CrUserId
        {
            get { return _CrUserId; }
            set { _CrUserId = value; }
        }
        //-----------------------------------------------------------------
        public DateTime CrDateTime
        {
            get { return _CrDateTime; }
            set { _CrDateTime = value; }
        }
        //-----------------------------------------------------------------

        private List<LawOffices> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<LawOffices> l_LawOffices = new List<LawOffices>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    LawOffices m_LawOffices = new LawOffices(db.ConnectionString);
                    m_LawOffices.LanguageId = smartReader.GetByte("LanguageId");
                    m_LawOffices.LawOfficeId = smartReader.GetInt32("LawOfficeId");
                    m_LawOffices.OfficeName = smartReader.GetString("OfficeName");
                    m_LawOffices.OfficeNameEn = smartReader.GetString("OfficeNameEn");
                    m_LawOffices.ShortName = smartReader.GetString("ShortName");
                    m_LawOffices.OfficeTypeId = smartReader.GetByte("OfficeTypeId");
                    m_LawOffices.CountryId = smartReader.GetInt16("CountryId");
                    m_LawOffices.ProvinceId = smartReader.GetInt16("ProvinceId");
                    m_LawOffices.Address = smartReader.GetString("Address");
                    m_LawOffices.PhoneNumber = smartReader.GetString("PhoneNumber");
                    m_LawOffices.Fax = smartReader.GetString("Fax");
                    m_LawOffices.Email = smartReader.GetString("Email");
                    m_LawOffices.Website = smartReader.GetString("Website");
                    m_LawOffices.ConsultancySector = smartReader.GetString("ConsultancySector");
                    m_LawOffices.OtherInfo = smartReader.GetString("OtherInfo");
                    m_LawOffices.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_LawOffices.CrUserId = smartReader.GetInt32("CrUserId");
                    m_LawOffices.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_LawOffices.Add(m_LawOffices);
                }
                reader.Close();
                return l_LawOffices;
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
        //-----------------------------------------------------------
        public byte Insert(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("LawOffices_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OfficeName", this.OfficeName));
                cmd.Parameters.Add(new SqlParameter("@OfficeNameEn", this.OfficeNameEn));
                cmd.Parameters.Add(new SqlParameter("@ShortName", this.ShortName));
                cmd.Parameters.Add(new SqlParameter("@OfficeTypeId", this.OfficeTypeId));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@Fax", this.Fax));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Website", this.Website));
                cmd.Parameters.Add(new SqlParameter("@ConsultancySector", this.ConsultancySector));
                cmd.Parameters.Add(new SqlParameter("@OtherInfo", this.OtherInfo));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@LawOfficeId", this.LawOfficeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.LawOfficeId = Convert.ToInt32((cmd.Parameters["@LawOfficeId"].Value == null) ? 0 : (cmd.Parameters["@LawOfficeId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("LawOffices_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@LawOfficeId", this.LawOfficeId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<LawOffices> GetList()
        {
            List<LawOffices> RetVal = new List<LawOffices>();
            try
            {
                string sql = "SELECT * FROM V$LawOffices";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static List<LawOffices> Static_GetList(string ConStr)
        {
            List<LawOffices> RetVal = new List<LawOffices>();
            try
            {
                LawOffices m_LawOffices = new LawOffices(ConStr);
                RetVal = m_LawOffices.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<LawOffices> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<LawOffices> GetListByLawOfficeId(int LawOfficeId, byte LanguageId)
        {
            List<LawOffices> RetVal = new List<LawOffices>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string OfficeName = "";
                byte OfficeTypeId = 0;
                short CountryId = 0;
                short ProvinceId = 0;
                string Address = "";
                string PhoneNumber = "";
                string Fax = "";
                string Email = "";
                string Website = "";
                byte ReviewStatusId = 0;
                int CrUserId = 0;
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, LawOfficeId, OfficeName, OfficeTypeId, CountryId, ProvinceId, Address, PhoneNumber, Fax, 
                                    Email, Website, ReviewStatusId, CrUserId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public LawOffices Get(int LawOfficeId, byte LanguageId)
        {
            LawOffices RetVal = new LawOffices(db.ConnectionString);
            try
            {
                List<LawOffices> list = GetListByLawOfficeId(LawOfficeId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (LawOffices)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public LawOffices Get()
        {
            return Get(this.LawOfficeId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static LawOffices Static_Get(string Constr, int LawOfficeId, byte LanguageId)
        {
            LawOffices m_LawOffices = new LawOffices(Constr);
            return m_LawOffices.Get(LawOfficeId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static LawOffices Static_Get(int LawOfficeId, byte LanguageId)
        {
            return Static_Get("", LawOfficeId, LanguageId);
        }
        //--------------------------------------------------------------     
        public List<LawOffices> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, int LawOfficeId, string OfficeName, byte OfficeTypeId, 
                                        short CountryId, short ProvinceId, string Address, string PhoneNumber, string Fax, string Email, string Website, byte ReviewStatusId, 
                                        int CrUserId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<LawOffices> RetVal = new List<LawOffices>();
            try
            {
                SqlCommand cmd = new SqlCommand("LawOffices_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@LawOfficeId", LawOfficeId));
                cmd.Parameters.Add(new SqlParameter("@OfficeName", StringUtil.InjectionString(OfficeName)));
                cmd.Parameters.Add(new SqlParameter("@OfficeTypeId", OfficeTypeId));
                cmd.Parameters.Add(new SqlParameter("@CountryId", CountryId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@Address", StringUtil.InjectionString(Address)));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", StringUtil.InjectionString(PhoneNumber)));
                cmd.Parameters.Add(new SqlParameter("@Fax", StringUtil.InjectionString(Fax)));
                cmd.Parameters.Add(new SqlParameter("@Email", StringUtil.InjectionString(Email)));
                cmd.Parameters.Add(new SqlParameter("@Website", StringUtil.InjectionString(Website)));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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
        public List<LawOffices> Search(int ActUserId, string OrderBy, byte LanguageId, int LawOfficeId, string OfficeName, byte OfficeTypeId, short CountryId, short ProvinceId, 
                                        string Address, string PhoneNumber, string Fax, string Email, string Website, byte ReviewStatusId, int CrUserId, string DateFrom, 
                                        string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, LawOfficeId, OfficeName, OfficeTypeId, CountryId, ProvinceId, Address, PhoneNumber, Fax, Email, 
                            Website, ReviewStatusId, CrUserId, DateFrom, DateTo, ref RowCount);
        }
    }
}