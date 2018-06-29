using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class Genders
    {
        private byte _GenderId;
        private string _GenderName;
        private string _GenderDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Genders()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Genders(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Genders()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte GenderId
        {
            get { return _GenderId; }
            set { _GenderId = value; }
        }
        //-----------------------------------------------------------------
        public string GenderName
        {
            get { return _GenderName; }
            set { _GenderName = value; }
        }
        //-----------------------------------------------------------------
        public string GenderDesc
        {
            get { return _GenderDesc; }
            set { _GenderDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<Genders> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Genders> l_Genders = new List<Genders>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Genders m_Genders = new Genders(db.ConnectionString);
                    m_Genders.GenderId = smartReader.GetByte("GenderId");
                    m_Genders.GenderName = smartReader.GetString("GenderName");
                    m_Genders.GenderDesc = smartReader.GetString("GenderDesc");

                    l_Genders.Add(m_Genders);
                }
                reader.Close();
                return l_Genders;
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
                SqlCommand cmd = new SqlCommand("Genders_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@GenderName", this.GenderName));
                cmd.Parameters.Add(new SqlParameter("@GenderDesc", this.GenderDesc));
                cmd.Parameters.Add("@GenderId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.GenderId = Convert.ToByte((cmd.Parameters["@GenderId"].Value == null) ? 0 : (cmd.Parameters["@GenderId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Genders_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@GenderName", this.GenderName));
                cmd.Parameters.Add(new SqlParameter("@GenderDesc", this.GenderDesc));
                cmd.Parameters.Add(new SqlParameter("@GenderId", this.GenderId));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Genders_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@GenderId", this.GenderId));
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
        public List<Genders> GetList()
        {
            List<Genders> RetVal = new List<Genders>();
            try
            {
                string sql = "SELECT * FROM V$Genders";
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
        public static List<Genders> Static_GetList(string ConStr)
        {
            List<Genders> RetVal = new List<Genders>();
            try
            {
                Genders m_Genders = new Genders(ConStr);
                RetVal = m_Genders.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Genders> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<Genders> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            Genders m_Genders = new Genders(ConStr);
            List<Genders> RetVal = m_Genders.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_Genders.GenderName = TextOptionAll;
                m_Genders.GenderDesc = TextOptionAll;
                RetVal.Insert(0, m_Genders);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Genders> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<Genders> GetListOrderBy(string OrderBy)
        {
            List<Genders> RetVal = new List<Genders>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$Genders ";
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
        public static List<Genders> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            Genders m_Genders = new Genders(ConStr);
            return m_Genders.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Genders> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<Genders> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<Genders> RetVal = new List<Genders>();
            Genders m_Genders = new Genders(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_Genders.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_Genders.GenderName = TextOptionAll;
                    m_Genders.GenderDesc = TextOptionAll;
                    RetVal.Insert(0, m_Genders);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<Genders> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<Genders> GetListByGenderId(byte GenderId)
        {
            List<Genders> RetVal = new List<Genders>();
            try
            {
                if (GenderId > 0)
                {
                    string sql = "SELECT * FROM V$Genders WHERE (GenderId=" + GenderId.ToString() + ")";
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
        public Genders Get(byte GenderId)
        {
            Genders RetVal = new Genders(db.ConnectionString);
            try
            {
                List<Genders> list = GetListByGenderId(GenderId);
                if (list.Count > 0)
                {
                    RetVal = (Genders)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Genders Get()
        {
            return Get(this.GenderId);
        }
        //-------------------------------------------------------------- 
        public static Genders Static_Get(byte GenderId)
        {
            return Static_Get(GenderId);
        }
        //-----------------------------------------------------------------------------
        public static Genders Static_Get(byte GenderId, List<Genders> lList)
        {
            Genders RetVal = new Genders();
            if (GenderId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.GenderId == GenderId);
                if (RetVal == null) RetVal = new Genders();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}