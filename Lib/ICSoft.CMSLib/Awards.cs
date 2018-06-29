
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSLib
{
    public class Awards
    {
        private byte _LanguageId;
        private byte _AwardId;
        private string _AwardName;
        private string _AwardContent;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Awards()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Awards(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Awards()
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
        public byte AwardId
        {
            get { return _AwardId; }
            set { _AwardId = value; }
        }
        //-----------------------------------------------------------------
        public string AwardName
        {
            get { return _AwardName; }
            set { _AwardName = value; }
        }
        //-----------------------------------------------------------------
        public string AwardContent
        {
            get { return _AwardContent; }
            set { _AwardContent = value; }
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

        private List<Awards> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Awards> l_Awards = new List<Awards>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Awards m_Awards = new Awards(db.ConnectionString);
                    m_Awards.LanguageId = smartReader.GetByte("LanguageId");
                    m_Awards.AwardId = smartReader.GetByte("AwardId");
                    m_Awards.AwardName = smartReader.GetString("AwardName");
                    m_Awards.AwardContent = smartReader.GetString("AwardContent");
                    m_Awards.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Awards.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Awards.Add(m_Awards);
                }
                reader.Close();
                return l_Awards;
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
                SqlCommand cmd = new SqlCommand("Awards_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@AwardName", this.AwardName));
                cmd.Parameters.Add(new SqlParameter("@AwardContent", this.AwardContent));
                cmd.Parameters.Add(new SqlParameter("@AwardId", this.AwardId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.AwardId = Convert.ToByte((cmd.Parameters["@AwardId"].Value == null) ? 0 : (cmd.Parameters["@AwardId"].Value));
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
                SqlCommand cmd = new SqlCommand("Awards_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@AwardId", this.AwardId));
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
        public List<Awards> GetList()
        {
            List<Awards> RetVal = new List<Awards>();
            try
            {
                string sql = "SELECT * FROM V$Awards";
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
        public static List<Awards> Static_GetList(string ConStr)
        {
            List<Awards> RetVal = new List<Awards>();
            try
            {
                Awards m_Awards = new Awards(ConStr);
                RetVal = m_Awards.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Awards> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
        public static List<Awards> Static_GetList(byte LanguageId)
        {
            List<Awards> RetVal = new List<Awards>();
            try
            {
                Awards m_Awards = new Awards();
                RetVal = m_Awards.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------   
        public static List<Awards> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            Awards m_Awards = new Awards(ConStr);
            List<Awards> RetVal = m_Awards.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_Awards.AwardName = TextOptionAll;
                m_Awards.AwardName = TextOptionAll;
                RetVal.Insert(0, m_Awards);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Awards> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<Awards> GetListOrderBy(string OrderBy)
        {
            List<Awards> RetVal = new List<Awards>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Awards ";
                if (OrderBy.Length > 0)
                {
                    sql += "ORDER BY " + OrderBy;
                }
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
        public static List<Awards> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Awards m_Awards = new Awards(ConStr);
            return m_Awards.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Awards> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Awards> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<Awards> RetVal = new List<Awards>();
            Awards m_Awards = new Awards(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_Awards.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_Awards.AwardName = TextOptionAll;
                    m_Awards.AwardName = TextOptionAll;
                    RetVal.Insert(0, m_Awards);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Awards> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<Awards> GetListByAwardId(byte AwardId, byte LanguageId)
        {
            List<Awards> RetVal = new List<Awards>();
            try
            {
                int ActUserId = 0;
                string OrderBy = "";
                RetVal = GetList(ActUserId,OrderBy, LanguageId, AwardId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public Awards Get(byte AwardId, byte LanguageId)
        {
            Awards RetVal = new Awards(db.ConnectionString);
            try
            {
                List<Awards> list = GetListByAwardId(AwardId, LanguageId);
                if (list.Count > 0)
                {
                    RetVal = (Awards)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Awards Get()
        {
            return Get(this.AwardId, this.LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Awards Static_Get(string Constr, byte AwardId, byte LanguageId)
        {
            Awards m_Awards = new Awards(Constr);
            return m_Awards.Get(AwardId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Awards Static_Get(byte AwardId, byte LanguageId)
        {
            return Static_Get("", AwardId, LanguageId);
        }
        //-------------------------------------------------------------- 
        public static Awards Static_Get(List<Awards> l_Awards, byte AwardId)
        {
            Awards RetVal;
            RetVal = l_Awards.Find(x => x.AwardId == AwardId);
            if (RetVal == null)
                RetVal = new Awards();
            return RetVal;
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte AwardId, byte LanguageId)
        {
            string RetVal = "";
            Awards m_Awards = new Awards();
            try
            {
                RetVal = m_Awards.Get(AwardId, LanguageId).AwardName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Awards> GetList(int ActUserId, string OrderBy, byte LanguageId, byte AwardId)
        {
            int MemberId = 0;
           
            return GetList(ActUserId, OrderBy, LanguageId, AwardId, MemberId);
        }
        //--------------------------------------------------------------     
        public List<Awards> GetList(int ActUserId, string OrderBy, byte LanguageId, byte AwardId, int MemberId)
        {
            List<Awards> RetVal = new List<Awards>();
            try
            {
                SqlCommand cmd = new SqlCommand("Awards_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@AwardId", AwardId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", MemberId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Awards> GetList( byte LanguageId)
        {
            List<Awards> RetVal = new List<Awards>();
            try
            {
                int ActUserId = 0;
                string OrderBy = "";
                byte AwardId = 0;
                int MemberId = 0;
                SqlCommand cmd = new SqlCommand("Awards_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@AwardId", AwardId));
                cmd.Parameters.Add(new SqlParameter("@MemberId", MemberId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Awards> GetListByMemberId( byte LanguageId, int MemberId)
        {
            int ActUserId=0;
            string OrderBy = "";
            byte AwardId = 0;

            return GetList(ActUserId, OrderBy, LanguageId, AwardId, MemberId);
        }
        //--------------------------------------------------------------     
       
    }
}