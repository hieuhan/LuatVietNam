
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
    /// class OrganDisplays
    /// </summary>
    public class OrganDisplays
    {
        private int _OrganDisplayId;
        private short _DisplayTypeId;
        private byte _LanguageId;
        private short _OrganId;
        private int _DisplayOrder;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;

        private string _organName;
        private string _organDesc;

        //-----------------------------------------------------------------
        public OrganDisplays()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public OrganDisplays(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~OrganDisplays()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int OrganDisplayId
        {
            get { return _OrganDisplayId; }
            set { _OrganDisplayId = value; }
        }
        //-----------------------------------------------------------------
        public short DisplayTypeId
        {
            get { return _DisplayTypeId; }
            set { _DisplayTypeId = value; }
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
        public int DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
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
        public string OrganName
        {
            get { return _organName; }
            set { _organName = value; }
        }

        //-----------------------------------------------------------------
        public string OrganDesc
        {
            get { return _organDesc; }
            set { _organDesc = value; }
        }
        //----------------------------------------------------------------

        private List<OrganDisplays> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<OrganDisplays> l_OrganDisplays = new List<OrganDisplays>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    OrganDisplays m_OrganDisplays = new OrganDisplays(db.ConnectionString);
                    m_OrganDisplays.OrganDisplayId = smartReader.GetInt32("OrganDisplayId");
                    m_OrganDisplays.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_OrganDisplays.LanguageId = smartReader.GetByte("LanguageId");
                    m_OrganDisplays.OrganId = smartReader.GetInt16("OrganId");
                    m_OrganDisplays.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_OrganDisplays.CrUserId = smartReader.GetInt32("CrUserId");
                    m_OrganDisplays.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_OrganDisplays.OrganName = smartReader.GetString("OrganName");
                    m_OrganDisplays.OrganDesc = smartReader.GetString("OrganDesc");

                    l_OrganDisplays.Add(m_OrganDisplays);
                }
                reader.Close();
                return l_OrganDisplays;
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
        public byte Insert(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("OrganDisplays_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                db.ExecuteSQL(cmd);
                RetVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte UpdateDisplayOrder(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("OrganDisplays_UpdateDisplayOrder");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                db.ExecuteSQL(cmd);
                RetVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("OrganDisplays_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", this.OrganId));
                db.ExecuteSQL(cmd);
                RetVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<OrganDisplays> GetList()
        {
            List<OrganDisplays> RetVal = new List<OrganDisplays>();
            try
            {
                string sql = "SELECT * FROM V$OrganDisplays";
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
        public static List<OrganDisplays> Static_GetList(string ConStr)
        {
            List<OrganDisplays> RetVal = new List<OrganDisplays>();
            try
            {
                OrganDisplays m_OrganDisplays = new OrganDisplays(ConStr);
                RetVal = m_OrganDisplays.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<OrganDisplays> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public List<OrganDisplays> GetListByOrganDisplayId(int OrganDisplayId)
        {
            List<OrganDisplays> RetVal = new List<OrganDisplays>();
            try
            {
                if (OrganDisplayId > 0)
                {
                    string sql = "SELECT * FROM V$OrganDisplays WHERE (OrganDisplayId=" + OrganDisplayId.ToString() + ")";
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
        public OrganDisplays Get(int OrganDisplayId)
        {
            OrganDisplays RetVal = new OrganDisplays(db.ConnectionString);
            try
            {
                List<OrganDisplays> list = GetListByOrganDisplayId(OrganDisplayId);
                if (list.Count > 0)
                {
                    RetVal = (OrganDisplays)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public OrganDisplays Get()
        {
            return Get(this.OrganDisplayId);
        }
        //-------------------------------------------------------------- 
        public static OrganDisplays Static_Get(int OrganDisplayId)
        {
            return Static_Get(OrganDisplayId);
        }
        //--------------------------------------------------------------     
        public List<OrganDisplays> OrganDisplays_GetList(int ActUserId, string OrderBy, short DisplayTypeId, byte LanguageId)
        {
            List<OrganDisplays> RetVal = new List<OrganDisplays>();
            try
            {
                SqlCommand cmd = new SqlCommand("OrganDisplays_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
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