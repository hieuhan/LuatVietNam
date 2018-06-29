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
    /// class Signers
    /// </summary>
    public class Signers
    {   
	    private short _SignerId;
	    private string _SignerName;
	    private string _SignerNameClear;
	    private short _MissionYearFrom;
	    private short _MissionYearTo;
	    private short _OrganId;
	    private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _RegencyId;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Signers()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Signers(string constr)
        {
            db = new DBAccess ((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Signers()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short SignerId
        {
		    get { return _SignerId; }
		    set { _SignerId = value; }
	    }
        //-----------------------------------------------------------------
        public string SignerName
		{
            get { return _SignerName; }
		    set { _SignerName = value; }
		}    
        //-----------------------------------------------------------------
        public string SignerNameClear
		{
            get { return _SignerNameClear; }
		    set { _SignerNameClear = value; }
		}    
        //-----------------------------------------------------------------
        public short MissionYearFrom
		{
            get { return _MissionYearFrom; }
		    set { _MissionYearFrom = value; }
		}    
        //-----------------------------------------------------------------
        public short MissionYearTo
		{
            get { return _MissionYearTo; }
		    set { _MissionYearTo = value; }
		}    
        //-----------------------------------------------------------------
        public short OrganId
		{
            get { return _OrganId; }
		    set { _OrganId = value; }
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
    
	    public byte RegencyId
        {
		    get { return _RegencyId; }
		    set { _RegencyId = value; }
	    }
        private List<Signers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Signers> l_Signers = new List<Signers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Signers m_Signers = new Signers(db.ConnectionString);
                    m_Signers.SignerId = smartReader.GetInt16("SignerId");
                    m_Signers.SignerName = smartReader.GetString("SignerName");
                    m_Signers.SignerNameClear = smartReader.GetString("SignerNameClear");
                    m_Signers.RegencyId = smartReader.GetByte("RegencyId");
                    m_Signers.MissionYearFrom = smartReader.GetInt16("MissionYearFrom");
                    m_Signers.MissionYearTo = smartReader.GetInt16("MissionYearTo");
                    m_Signers.OrganId = smartReader.GetInt16("OrganId");
                    m_Signers.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Signers.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Signers.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Signers.Add(m_Signers);
                }
                reader.Close();
                return l_Signers;
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
                SqlCommand cmd = new SqlCommand("Signers_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SignerName", this.SignerName));
                cmd.Parameters.Add(new SqlParameter("@SignerNameClear", this.SignerNameClear));
                cmd.Parameters.Add(new SqlParameter("@RegencyId", this.RegencyId));
                cmd.Parameters.Add(new SqlParameter("@MissionYearFrom", this.MissionYearFrom));
                cmd.Parameters.Add(new SqlParameter("@MissionYearTo", this.MissionYearTo));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", this.SignerId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SignerId =Convert.ToInt16((cmd.Parameters["@SignerId"].Value == null) ? 0 : (cmd.Parameters["@SignerId"].Value));
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("Signers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SignerId",this.SignerId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<Signers> GetList()
        {
            List<Signers> RetVal = new List<Signers>();
            try
            {
                string sql = "SELECT * FROM V$Signers ORDER BY SignerName ASC";
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
        public static List<Signers> Static_GetList(string ConStr)
        {
            List<Signers> RetVal = new List<Signers>();
            try
            {
                Signers m_Signers = new Signers(ConStr);
                RetVal = m_Signers.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Signers> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<Signers> GetListBySignerId(short SignerId)
        {
            List<Signers> RetVal = new List<Signers>();
            try
            {
                if (SignerId > 0)
                {
                    string sql = "SELECT * FROM V$Signers WHERE (SignerId=" + SignerId.ToString() + ")";
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
        public Signers Get(short SignerId)
        {
            Signers RetVal = new Signers(db.ConnectionString);
            try
            {
                List<Signers> list = GetListBySignerId(SignerId);
                if (list.Count > 0)
                {
                    RetVal = (Signers)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Signers Get()
        {
            return Get(this.SignerId);
        }
        //-------------------------------------------------------------- 
        public static Signers Static_Get(short SignerId)
        {
            Signers m_Signers = new Signers();
            m_Signers = m_Signers.Get(SignerId);
            return m_Signers;
        }
        //--------------------------------------------------------------     
        public List<Signers> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string SignerName, byte RegencyId, short OrganId, byte ReviewStatusId, ref int RowCount)
        {
            List<Signers> RetVal = new List<Signers>();
            try
            {
                SqlCommand cmd = new SqlCommand("Signers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@SignerName", StringUtil.InjectionString(SignerName)));
                cmd.Parameters.Add(new SqlParameter("@RegencyId", RegencyId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
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
        public List<Signers> Search(int ActUserId, string OrderBy, string SignerName, byte RegencyId, short OrganId, byte ReviewStatusId, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, SignerName, RegencyId, OrganId, ReviewStatusId, ref RowCount);
        }
        //-------------------------------------------------------------- 
        public DataSet Signers_GetNameByJson(string Name, int RowAmount, ref string Result)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Signers_GetNameByJson");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar,4000).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                Result = Convert.ToString(cmd.Parameters["@Result"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        //-------------------------------------------------------------- 
        public DataSet Signers_GetIdAndNameByJson(string Name, int RowAmount, ref string Result)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Signers_GetIdAndNameByJson");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                Result = Convert.ToString(cmd.Parameters["@Result"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    } 
}