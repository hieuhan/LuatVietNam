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
    /// class Regencies
    /// </summary>
    public class Regencies
    {
        private byte _RegencyId;
        private string _RegencyName;
        private string _RegencyDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public Regencies()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Regencies(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Regencies()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte RegencyId
        {
            get { return _RegencyId; }
            set { _RegencyId = value; }
        }
        //-----------------------------------------------------------------
        public string RegencyName
        {
            get { return _RegencyName; }
            set { _RegencyName = value; }
        }
        //-----------------------------------------------------------------
        public string RegencyDesc
        {
            get { return _RegencyDesc; }
            set { _RegencyDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<Regencies> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Regencies> l_Regencies = new List<Regencies>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Regencies m_Regencies = new Regencies(db.ConnectionString);
                    m_Regencies.RegencyId = smartReader.GetByte("RegencyId");
                    m_Regencies.RegencyName = smartReader.GetString("RegencyName");
                    m_Regencies.RegencyDesc = smartReader.GetString("RegencyDesc");
                    m_Regencies.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_Regencies.Add(m_Regencies);
                }
                reader.Close();
                return l_Regencies;
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
                SqlCommand cmd = new SqlCommand("Regencies_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RegencyName", this.RegencyName));
                cmd.Parameters.Add(new SqlParameter("@RegencyDesc", this.RegencyDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@RegencyId", this.RegencyId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.RegencyId = Convert.ToByte((cmd.Parameters["@RegencyId"].Value == null) ? 0 : (cmd.Parameters["@RegencyId"].Value));
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
                SqlCommand cmd = new SqlCommand("Regencies_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RegencyId", this.RegencyId));
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
        public List<Regencies> GetList()
        {
            List<Regencies> RetVal = new List<Regencies>();
            try
            {
                string sql = "SELECT * FROM V$Regencies ORDER BY RegencyName";
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
        public static List<Regencies> Static_GetList(string ConStr)
        {
            List<Regencies> RetVal = new List<Regencies>();
            try
            {
                Regencies m_Regencies = new Regencies(ConStr);
                RetVal = m_Regencies.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Regencies> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<Regencies> GetListByRegencyId(byte RegencyId)
        {
            List<Regencies> RetVal = new List<Regencies>();
            try
            {
                if (RegencyId > 0)
                {
                    string sql = "SELECT * FROM V$Regencies WHERE (RegencyId=" + RegencyId.ToString() + ")";
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
        public Regencies Get(byte RegencyId)
        {
            Regencies RetVal = new Regencies(db.ConnectionString);
            try
            {
                List<Regencies> list = GetListByRegencyId(RegencyId);
                if (list.Count > 0)
                {
                    RetVal = (Regencies)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Regencies Get()
        {
            return Get(this.RegencyId);
        }
        //-------------------------------------------------------------- 
        public static Regencies Static_Get(byte RegencyId)
        {
            return Static_Get(RegencyId);
        }
        //-----------------------------------------------------------------------------
        public static Regencies Static_Get(byte RegencyId, List<Regencies> lList)
        {
            Regencies RetVal = new Regencies();
            if (RegencyId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.RegencyId == RegencyId);
                if (RetVal == null) RetVal = new Regencies();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Regencies> Regencies_GetList(int ActUserId, string OrderBy, byte RegencyId, string RegencyName)
        {
            List<Regencies> RetVal = new List<Regencies>();
            try
            {
                SqlCommand cmd = new SqlCommand("Regencies_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@RegencyId", RegencyId));
                cmd.Parameters.Add(new SqlParameter("@RegencyName", StringUtil.InjectionString(RegencyName)));                
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public static string Static_GetDescByCulture(byte RegencyId)
        {
            string RetVal = "";
            Regencies m_Regencies = new Regencies();
            m_Regencies = m_Regencies.Get(RegencyId);
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == CmsConstants.CULTURE_VN)
            {
                RetVal = m_Regencies.RegencyDesc;
            }
            else
            {
                RetVal = m_Regencies.RegencyName;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
       
    }
}