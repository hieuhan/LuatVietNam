
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
    public class FaqGroups
    {
        private byte _FaqGroupId;
        private string _FaqGroupName;
        private string _FaqGroupDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public FaqGroups()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public FaqGroups(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FaqGroups()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte FaqGroupId
        {
            get { return _FaqGroupId; }
            set { _FaqGroupId = value; }
        }
        //-----------------------------------------------------------------
        public string FaqGroupName
        {
            get { return _FaqGroupName; }
            set { _FaqGroupName = value; }
        }
        //-----------------------------------------------------------------
        public string FaqGroupDesc
        {
            get { return _FaqGroupDesc; }
            set { _FaqGroupDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<FaqGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FaqGroups> l_FaqGroups = new List<FaqGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FaqGroups m_FaqGroups = new FaqGroups(db.ConnectionString);
                    m_FaqGroups.FaqGroupId = smartReader.GetByte("FaqGroupId");
                    m_FaqGroups.FaqGroupName = smartReader.GetString("FaqGroupName");
                    m_FaqGroups.FaqGroupDesc = smartReader.GetString("FaqGroupDesc");

                    l_FaqGroups.Add(m_FaqGroups);
                }
                reader.Close();
                return l_FaqGroups;
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
                SqlCommand cmd = new SqlCommand("FaqGroups_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqGroupName", this.FaqGroupName));
                cmd.Parameters.Add(new SqlParameter("@FaqGroupDesc", this.FaqGroupDesc));
                cmd.Parameters.Add("@FaqGroupId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.FaqGroupId = Convert.ToByte((cmd.Parameters["@FaqGroupId"].Value == null) ? 0 : (cmd.Parameters["@FaqGroupId"].Value));
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
                SqlCommand cmd = new SqlCommand("FaqGroups_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqGroupName", this.FaqGroupName));
                cmd.Parameters.Add(new SqlParameter("@FaqGroupDesc", this.FaqGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@FaqGroupId", this.FaqGroupId));
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
                SqlCommand cmd = new SqlCommand("FaqGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqGroupId", this.FaqGroupId));
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
        public List<FaqGroups> GetList()
        {
            List<FaqGroups> RetVal = new List<FaqGroups>();
            try
            {
                string sql = "SELECT * FROM V$FaqGroups";
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
        public static List<FaqGroups> Static_GetList(string ConStr)
        {
            List<FaqGroups> RetVal = new List<FaqGroups>();
            try
            {
                FaqGroups m_FaqGroups = new FaqGroups(ConStr);
                RetVal = m_FaqGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<FaqGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<FaqGroups> Static_GetListAll(string ConStr, string TextOptionAll = "...")
        {
            FaqGroups m_FaqGroups = new FaqGroups(ConStr);
            List<FaqGroups> RetVal = m_FaqGroups.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_FaqGroups.FaqGroupName = TextOptionAll;
                m_FaqGroups.FaqGroupDesc = TextOptionAll;
                RetVal.Insert(0, m_FaqGroups);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<FaqGroups> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<FaqGroups> GetListOrderBy(string OrderBy)
        {
            List<FaqGroups> RetVal = new List<FaqGroups>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$FaqGroups ";
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
        public static List<FaqGroups> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            FaqGroups m_FaqGroups = new FaqGroups(ConStr);
            return m_FaqGroups.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<FaqGroups> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<FaqGroups> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<FaqGroups> RetVal = new List<FaqGroups>();
            FaqGroups m_FaqGroups = new FaqGroups(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_FaqGroups.GetListOrderBy(OrderBy);
                }
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_FaqGroups.FaqGroupName = TextOptionAll;
                    m_FaqGroups.FaqGroupDesc = TextOptionAll;
                    RetVal.Insert(0, m_FaqGroups);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<FaqGroups> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<FaqGroups> GetListByFaqGroupId(byte FaqGroupId)
        {
            List<FaqGroups> RetVal = new List<FaqGroups>();
            try
            {
                if (FaqGroupId > 0)
                {
                    string sql = "SELECT * FROM V$FaqGroups WHERE (FaqGroupId=" + FaqGroupId.ToString() + ")";
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
        public FaqGroups Get(byte FaqGroupId)
        {
            FaqGroups RetVal = new FaqGroups(db.ConnectionString);
            try
            {
                List<FaqGroups> list = GetListByFaqGroupId(FaqGroupId);
                if (list.Count > 0)
                {
                    RetVal = (FaqGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public FaqGroups Get()
        {
            return Get(this.FaqGroupId);
        }
        //-------------------------------------------------------------- 
        public static FaqGroups Static_Get(byte FaqGroupId)
        {
            return Static_Get(FaqGroupId);
        }
        //-----------------------------------------------------------------------------
        public static FaqGroups Static_Get(byte FaqGroupId, List<FaqGroups> lList)
        {
            FaqGroups RetVal = new FaqGroups();
            if (FaqGroupId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.FaqGroupId == FaqGroupId);
                if (RetVal == null) RetVal = new FaqGroups();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}
