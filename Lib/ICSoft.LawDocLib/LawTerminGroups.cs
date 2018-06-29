
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
    public class LawTerminGroups
    {   
	    private short _LawTerminGroupId;
	    private string _LawTerminGroupName;
	    private string _LawTerminGroupDesc;
        private DBAccess db;
         //-----------------------------------------------------------------
        public LawTerminGroups()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public LawTerminGroups(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~LawTerminGroups()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short LawTerminGroupId
        {
		    get { return _LawTerminGroupId; }
		    set { _LawTerminGroupId = value; }
	    }
        //-----------------------------------------------------------------
        public string LawTerminGroupName
		{
            get { return _LawTerminGroupName; }
		    set { _LawTerminGroupName = value; }
		}    
        //-----------------------------------------------------------------
        public string LawTerminGroupDesc
		{
            get { return _LawTerminGroupDesc; }
		    set { _LawTerminGroupDesc = value; }
		}
        //-----------------------------------------------------------------
       
        //-----------------------------------------------------------------
    
        private List<LawTerminGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<LawTerminGroups> l_LawTerminGroups = new List<LawTerminGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    LawTerminGroups m_LawTerminGroups = new LawTerminGroups(db.ConnectionString);
                    m_LawTerminGroups.LawTerminGroupId = smartReader.GetInt16("LawTerminGroupId");
                    m_LawTerminGroups.LawTerminGroupName = smartReader.GetString("LawTerminGroupName");
                    m_LawTerminGroups.LawTerminGroupDesc = smartReader.GetString("LawTerminGroupDesc");
                   
         
                    l_LawTerminGroups.Add(m_LawTerminGroups);
                }
                reader.Close();
                return l_LawTerminGroups;
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
                SqlCommand cmd = new SqlCommand("LawTerminGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LawTerminGroupName", this.LawTerminGroupName));
                cmd.Parameters.Add(new SqlParameter("@LawTerminGroupDesc", this.LawTerminGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@LawTerminGroupId", this.LawTerminGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.LawTerminGroupId =Convert.ToInt16((cmd.Parameters["@LawTerminGroupId"].Value == null) ? 0 : (cmd.Parameters["@LawTerminGroupId"].Value));
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
                SqlCommand cmd = new SqlCommand("LawTerminGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LawTerminGroupId",this.LawTerminGroupId));
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
        public List<LawTerminGroups> GetList()
        {
            List<LawTerminGroups> RetVal = new List<LawTerminGroups>();
            try
            {
                string sql = "SELECT * FROM LawTerminGroups";
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
        public static List<LawTerminGroups> Static_GetList(string ConStr)
        {
            List<LawTerminGroups> RetVal = new List<LawTerminGroups>();
            try
            {
                LawTerminGroups m_LawTerminGroups = new LawTerminGroups(ConStr);
                RetVal = m_LawTerminGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<LawTerminGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<LawTerminGroups> Static_GetListAll(string ConStr ,string TextOptionAll = "...")
        {
            LawTerminGroups m_LawTerminGroups = new LawTerminGroups(ConStr);
            List<LawTerminGroups> RetVal = m_LawTerminGroups.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_LawTerminGroups.LawTerminGroupName = TextOptionAll;
                m_LawTerminGroups.LawTerminGroupDesc = TextOptionAll;
                RetVal.Insert(0, m_LawTerminGroups);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<LawTerminGroups> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<LawTerminGroups> GetListOrderBy(string OrderBy)
        {
            List<LawTerminGroups> RetVal = new List<LawTerminGroups>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM LawTerminGroups " ;
                if (OrderBy.Length >0)
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
        public static List<LawTerminGroups> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            LawTerminGroups m_LawTerminGroups = new LawTerminGroups(ConStr);
            return m_LawTerminGroups.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<LawTerminGroups> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<LawTerminGroups> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<LawTerminGroups> RetVal = new List<LawTerminGroups>();
            LawTerminGroups m_LawTerminGroups = new LawTerminGroups(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_LawTerminGroups.GetListOrderBy(OrderBy);
                }               
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_LawTerminGroups.LawTerminGroupName = TextOptionAll;
                    m_LawTerminGroups.LawTerminGroupDesc = TextOptionAll;
                    RetVal.Insert(0, m_LawTerminGroups);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<LawTerminGroups> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<LawTerminGroups> GetListByLawTerminGroupId(short LawTerminGroupId)
        {
            List<LawTerminGroups> RetVal = new List<LawTerminGroups>();
            try
            {
                if (LawTerminGroupId > 0)
                {
                    string sql = "SELECT * FROM LawTerminGroups WHERE (LawTerminGroupId=" + LawTerminGroupId.ToString() + ")";
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
        public LawTerminGroups Get(short LawTerminGroupId)
        {
            LawTerminGroups RetVal = new LawTerminGroups(db.ConnectionString);
            try
            {
                List<LawTerminGroups> list = GetListByLawTerminGroupId(LawTerminGroupId);
                if (list.Count > 0)
                {
                    RetVal = (LawTerminGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public LawTerminGroups Get()
        {
            return Get(this.LawTerminGroupId);
        }
        //-------------------------------------------------------------- 
        public static LawTerminGroups Static_Get(short LawTerminGroupId)
        {
            return new LawTerminGroups().Get(LawTerminGroupId);
        }
        //-----------------------------------------------------------------------------
        public static LawTerminGroups Static_Get(short LawTerminGroupId, List<LawTerminGroups> lList)
        {
            LawTerminGroups RetVal = new LawTerminGroups();
            if (LawTerminGroupId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.LawTerminGroupId == LawTerminGroupId);
                if (RetVal == null) RetVal = new LawTerminGroups();
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<LawTerminGroups> LawTerminGroups_GetList(int ActUserId, string OrderBy, string LawTerminGroupName, short SiteId)
        {
            List<LawTerminGroups> RetVal = new List<LawTerminGroups>();
            try
            {
                SqlCommand cmd = new SqlCommand("LawTerminGroups_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LawTerminGroupName", StringUtil.InjectionString(LawTerminGroupName)));
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return RetVal;
        }   
   } 
}

