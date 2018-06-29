
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
    public class Promotions
    {
        private int _PromotionId;
        private string _PromotionName;
        private string _PromotionDesc;
        private DateTime _BeginTime;
        private DateTime _EndTime;
        private short _NumMonthFree;
        private short _NumDayFree;
        private short _NumDownload;
        private float _Amount;
        private float _PercentDecrease;
        private int _NumOfCode;
        private byte _LenOfCode;
        private string _CodeGenType;
        private byte _UsingTypeId;
        private int _UsingCount;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private short _ServicePackageId;
        private short _SiteId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Promotions()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Promotions(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.CUSTOMER_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Promotions()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int PromotionId
        {
            get { return _PromotionId; }
            set { _PromotionId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public short NumDownload
        {
            get { return _NumDownload; }
            set { _NumDownload = value; }
        }
        //-----------------------------------------------------------------
        public string PromotionName
        {
            get { return _PromotionName; }
            set { _PromotionName = value; }
        }
        //-----------------------------------------------------------------
        public string PromotionDesc
        {
            get { return _PromotionDesc; }
            set { _PromotionDesc = value; }
        }
        //-----------------------------------------------------------------
        public DateTime BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }
        //-----------------------------------------------------------------
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        //-----------------------------------------------------------------
        public short NumMonthFree
        {
            get { return _NumMonthFree; }
            set { _NumMonthFree = value; }
        }
        //-----------------------------------------------------------------
        public short NumDayFree
        {
            get { return _NumDayFree; }
            set { _NumDayFree = value; }
        }
        //-----------------------------------------------------------------
        public float Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        //-----------------------------------------------------------------
        public float PercentDecrease
        {
            get { return _PercentDecrease; }
            set { _PercentDecrease = value; }
        }
        //-----------------------------------------------------------------
        public int NumOfCode
        {
            get { return _NumOfCode; }
            set { _NumOfCode = value; }
        }
        //-----------------------------------------------------------------
        public byte LenOfCode
        {
            get { return _LenOfCode; }
            set { _LenOfCode = value; }
        }
        //-----------------------------------------------------------------
        public string CodeGenType
        {
            get { return _CodeGenType; }
            set { _CodeGenType = value; }
        }
        //-----------------------------------------------------------------
        public byte UsingTypeId
        {
            get { return _UsingTypeId; }
            set { _UsingTypeId = value; }
        }
        //-----------------------------------------------------------------
        public int UsingCount
        {
            get { return _UsingCount; }
            set { _UsingCount = value; }
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

        public short ServicePackageId
        {
            get { return _ServicePackageId; }
            set { _ServicePackageId = value; }
        }
        private List<Promotions> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Promotions> l_Promotions = new List<Promotions>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Promotions m_Promotions = new Promotions(db.ConnectionString);
                    m_Promotions.PromotionId = smartReader.GetInt32("PromotionId");
                    m_Promotions.PromotionName = smartReader.GetString("PromotionName");
                    m_Promotions.PromotionDesc = smartReader.GetString("PromotionDesc");
                    m_Promotions.SiteId = smartReader.GetInt16("SiteId");
                    m_Promotions.ServicePackageId = smartReader.GetInt16("ServicePackageId");
                    m_Promotions.BeginTime = smartReader.GetDateTime("BeginTime");
                    m_Promotions.EndTime = smartReader.GetDateTime("EndTime");
                    m_Promotions.NumMonthFree = smartReader.GetInt16("NumMonthFree");
                    m_Promotions.NumDayFree = smartReader.GetInt16("NumDayFree");
                    m_Promotions.NumDownload = smartReader.GetInt16("NumDownload");

                    m_Promotions.Amount = smartReader.GetFloat("Amount");
                    m_Promotions.PercentDecrease = smartReader.GetFloat("PercentDecrease");
                    m_Promotions.NumOfCode = smartReader.GetInt32("NumOfCode");
                    m_Promotions.LenOfCode = smartReader.GetByte("LenOfCode");
                    m_Promotions.CodeGenType = smartReader.GetString("CodeGenType");
                    m_Promotions.UsingTypeId = smartReader.GetByte("UsingTypeId");
                    m_Promotions.UsingCount = smartReader.GetInt32("UsingCount");
                    m_Promotions.UsingTypeId = smartReader.GetByte("UsingTypeId");
                    m_Promotions.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Promotions.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Promotions.ReviewStatusId = smartReader.GetByte("ReviewStatusId");

                    l_Promotions.Add(m_Promotions);
                }
                reader.Close();
                return l_Promotions;
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
                SqlCommand cmd = new SqlCommand("Promotions_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PromotionName", this.PromotionName));
                cmd.Parameters.Add(new SqlParameter("@PromotionDesc", this.PromotionDesc));
                //cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", this.ServicePackageId));
                if (this.BeginTime == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@BeginTime", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@BeginTime", this.BeginTime));
                if (this.EndTime == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@EndTime", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@EndTime", this.EndTime));
                cmd.Parameters.Add(new SqlParameter("@NumMonthFree", this.NumMonthFree));
                cmd.Parameters.Add(new SqlParameter("@NumDayFree", this.NumDayFree));
                //cmd.Parameters.Add(new SqlParameter("@NumDownload", this.NumDownload));
                cmd.Parameters.Add(new SqlParameter("@Amount", this.Amount));
                cmd.Parameters.Add(new SqlParameter("@PercentDecrease", this.PercentDecrease));
                cmd.Parameters.Add(new SqlParameter("@NumOfCode", this.NumOfCode));
                cmd.Parameters.Add(new SqlParameter("@LenOfCode", this.LenOfCode));
                cmd.Parameters.Add(new SqlParameter("@CodeGenType", this.CodeGenType));
                cmd.Parameters.Add(new SqlParameter("@UsingCount", this.UsingCount));
                cmd.Parameters.Add(new SqlParameter("@UsingTypeId", this.UsingTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@PromotionId", this.PromotionId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PromotionId = Convert.ToInt32((cmd.Parameters["@PromotionId"].Value == null) ? 0 : (cmd.Parameters["@PromotionId"].Value));
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
                SqlCommand cmd = new SqlCommand("Promotions_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PromotionId", this.PromotionId));
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
        public List<Promotions> GetListByPromotionId(int PromotionId)
        {
            List<Promotions> RetVal = new List<Promotions>();
            try
            {
                if (PromotionId > 0)
                {
                    string sql = "SELECT * FROM Promotions WHERE (PromotionId=" + PromotionId.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = Init(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Promotions> Promotions_GetByPromotionCode(string PromotionCode)
        {
            List<Promotions> RetVal = new List<Promotions>();
            try
            {
                SqlCommand cmd = new SqlCommand("Promotions_GetByPromotionCode");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PromotionCode", PromotionCode));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public Promotions Get(int PromotionId)
        {
            Promotions RetVal = new Promotions(db.ConnectionString);
            try
            {
                List<Promotions> list = GetListByPromotionId(PromotionId);
                if (list.Count > 0)
                {
                    RetVal = (Promotions)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Promotions Get()
        {
            return Get(this.PromotionId);
        }
        //-------------------------------------------------------------- 
        public static Promotions Static_Get(int PromotionId)
        {
            Promotions m_Promotions = new Promotions();
            return m_Promotions.Get(PromotionId);
        }
        //--------------------------------------------------------------     
        public List<Promotions> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string PromotionName, short ServiceId, short ServicePackageId, string DateFrom, 
                                         string DateTo, ref int RowCount)
        {
            List<Promotions> RetVal = new List<Promotions>();
            try
            {
                SqlCommand cmd = new SqlCommand("Promotions_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@PromotionName", StringUtil.InjectionString(PromotionName)));
                //cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", ServiceId));
                cmd.Parameters.Add(new SqlParameter("@ServicePackageId", ServicePackageId));
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
        public List<Promotions> Search(int ActUserId, string OrderBy, string PromotionName, short ServiceId, short ServicePackageId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, PromotionName, ServiceId, ServicePackageId, DateFrom, DateTo, ref RowCount);
        }
    }
}