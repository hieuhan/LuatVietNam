
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using sms.database;
using sms.utils;
using ICSoft.CMSLib;

namespace ICSoft.LawDocsLib
{
    /// <summary>
    /// class Organs
    /// </summary>
    public class Organs
    {   
        private byte _LanguageId;
	    private short _OrganId;
	    private string _OrganName;
	    private string _OrganDesc;
	    private short _ParentOrganId;
	    private short _DisplayOrder;
	    private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
	    private byte _OrganTypeId;
        private int _SoLuong;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Organs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Organs(string constr)
        {
            db = new DBAccess ((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Organs()
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
	    public short OrganId
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
        public string OrganDesc
		{
            get { return _OrganDesc; }
		    set { _OrganDesc = value; }
		}    
        //-----------------------------------------------------------------
        public short ParentOrganId
		{
            get { return _ParentOrganId; }
		    set { _ParentOrganId = value; }
		}    
        //-----------------------------------------------------------------
        public short DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
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
    
	    public byte OrganTypeId
        {
		    get { return _OrganTypeId; }
		    set { _OrganTypeId = value; }
	    }
        //-----------------------------------------------------------------    
        public int SoLuong
        {
            get { return _SoLuong; }
            set { _SoLuong = value; }
        }
        //-----------------------------------------------------------------    
        private List<Organs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Organs> l_Organs = new List<Organs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Organs m_Organs = new Organs(db.ConnectionString);
                    m_Organs.LanguageId = smartReader.GetByte("LanguageId");
                    m_Organs.OrganId = smartReader.GetInt16("OrganId");
                    m_Organs.OrganName = smartReader.GetString("OrganName");
                    m_Organs.OrganDesc = smartReader.GetString("OrganDesc");
                    m_Organs.OrganTypeId = smartReader.GetByte("OrganTypeId");
                    m_Organs.ParentOrganId = smartReader.GetInt16("ParentOrganId");
                    m_Organs.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_Organs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Organs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Organs.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Organs.Add(m_Organs);
                }
                reader.Close();
                return l_Organs;
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
                SqlCommand cmd = new SqlCommand("Organs_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OrganName", this.OrganName));
                cmd.Parameters.Add(new SqlParameter("@OrganDesc", this.OrganDesc));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", this.OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@ParentOrganId", this.ParentOrganId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.OrganId =Convert.ToInt16((cmd.Parameters["@OrganId"].Value == null) ? 0 : (cmd.Parameters["@OrganId"].Value));
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
                SqlCommand cmd = new SqlCommand("Organs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId));
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
        public List<Organs> GetList()
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                string sql = "SELECT * FROM V$Organs ORDER BY OrganName";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public List<Organs> GetList_OrderBy(string OrderBy = "")
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                string sql = "SELECT * FROM V$Organs ";
                if (!string.IsNullOrEmpty(OrderBy)) { sql += "ORDER BY " + OrderBy; }
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public List<Organs> GetListbyDisplayTypeId(short DisplayTypeId = 0)
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                string sql = "SELECT * FROM V$Organs where OrganId IN (select OrganId from OrganDisplays where DisplayTypeId=" + DisplayTypeId + ") Order by OrganName";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public List<Organs> GetListbyDisplayTypeId(short DisplayTypeId = 0, string OrderBy = "")
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                string sql = "SELECT * FROM V$Organs where OrganId IN (select OrganId from OrganDisplays where DisplayTypeId=" + DisplayTypeId + ")";
                if (!string.IsNullOrEmpty(OrderBy)) { sql += "ORDER BY " + OrderBy; }
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public static List<Organs> Static_GetListbyDisplayTypeId(short DisplayTypeId = 0)
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                Organs m_Organs = new Organs();
                RetVal = m_Organs.GetListbyDisplayTypeId(DisplayTypeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        public static List<Organs> Static_GetListbyDisplayTypeId(short DisplayTypeId = 0, string OrderBy="")
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                Organs m_Organs = new Organs();
                RetVal = m_Organs.GetListbyDisplayTypeId(DisplayTypeId,OrderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static List<Organs> Static_GetListOrderBy(string OrderBy="")
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                Organs m_Organs = new Organs();
                RetVal = m_Organs.GetList_OrderBy(OrderBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public static List<Organs> Static_GetList(string ConStr)
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                Organs m_Organs = new Organs(ConStr);
                RetVal = m_Organs.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Organs> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<Organs> GetListByOrganId(short OrganId, byte LanguageId)
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                int RowCount=0;
                byte ReviewStatusId = 0;
                byte OrganTypeId = 0;
                short DisplayTypeId = 0;
                string OrganName = "";
                byte ReferToDefaultLanguage = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, OrganId, OrganName, OrganTypeId, DisplayTypeId, ReviewStatusId, ReferToDefaultLanguage, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        
        //-------------------------------------------------------------- 
        public Organs Get(short OrganId, byte LanguageId)
        {
            Organs RetVal = new Organs(db.ConnectionString);
            try
            {
                List<Organs> list = GetListByOrganId(OrganId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (Organs)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Organs Get()
        {
            return Get(this.OrganId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Organs Static_Get(string Constr, short OrganId, byte LanguageId)
        {
            Organs m_Organs = new Organs(Constr);
            return m_Organs.Get(OrganId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Organs Static_Get(short OrganId, byte LanguageId)
        {
            return Static_Get("",OrganId, LanguageId);
        }
        //--------------------------------------------------------------     
        public static Organs Static_Get(short OrganId, List<Organs> list)
        {
            Organs RetVal = new Organs();
            RetVal = list.Find(i => i.OrganId == OrganId);
            if (RetVal == null) RetVal = new Organs();
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Organs> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, byte LanguageId, short OrganId, string OrganName, byte OrganTypeId, 
                                     short DisplayTypeId, byte ReviewStatusId, byte ReferToDefaultLanguage, ref int RowCount)
        {
            List<Organs> RetVal = new List<Organs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Organs_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@OrganName", StringUtil.InjectionString(OrganName)));
                cmd.Parameters.Add(new SqlParameter("@OrganTypeId", OrganTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@ReferToDefaultLanguage", ReferToDefaultLanguage));
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
        public List<Organs> Search(int ActUserId, string OrderBy,  byte LanguageId, short OrganId, string OrganName, byte OrganTypeId, short DisplayTypeId, byte ReviewStatusId, 
                                    byte ReferToDefaultLanguage, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, LanguageId, OrganId, OrganName, OrganTypeId, DisplayTypeId, ReviewStatusId, ReferToDefaultLanguage, ref RowCount);
        }
        //-------------------------------------------------------------- 
        public DataSet Organs_GetNameByJson(string Name, int RowAmount, ref string Result)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Organs_GetNameByJson");
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
    } 
}