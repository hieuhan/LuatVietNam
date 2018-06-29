
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
    public class InfoChannels
    {   
	    private short _InfoChannelId;
	    private string _InfoChannelName;
	    private string _InfoChannelDesc;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public InfoChannels()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public InfoChannels(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~InfoChannels()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short InfoChannelId
        {
		    get { return _InfoChannelId; }
		    set { _InfoChannelId = value; }
	    }
        //-----------------------------------------------------------------
        public string InfoChannelName
		{
            get { return _InfoChannelName; }
		    set { _InfoChannelName = value; }
		}    
        //-----------------------------------------------------------------
        public string InfoChannelDesc
		{
            get { return _InfoChannelDesc; }
		    set { _InfoChannelDesc = value; }
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
    
        private List<InfoChannels> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<InfoChannels> l_InfoChannels = new List<InfoChannels>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    InfoChannels m_InfoChannels = new InfoChannels(db.ConnectionString);
                    m_InfoChannels.InfoChannelId = smartReader.GetInt16("InfoChannelId");
                    m_InfoChannels.InfoChannelName = smartReader.GetString("InfoChannelName");
                    m_InfoChannels.InfoChannelDesc = smartReader.GetString("InfoChannelDesc");
                    m_InfoChannels.CrUserId = smartReader.GetInt32("CrUserId");
                    m_InfoChannels.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_InfoChannels.Add(m_InfoChannels);
                }
                reader.Close();
                return l_InfoChannels;
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
                SqlCommand cmd = new SqlCommand("InfoChannels_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelName", this.InfoChannelName));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelDesc", this.InfoChannelDesc));
                cmd.Parameters.Add("@InfoChannelId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.InfoChannelId =Convert.ToInt16((cmd.Parameters["@InfoChannelId"].Value == null) ? 0 : (cmd.Parameters["@InfoChannelId"].Value));
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("InfoChannels_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelName", this.InfoChannelName));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelDesc", this.InfoChannelDesc));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId",this.InfoChannelId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal =Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("InfoChannels_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@InfoChannelId",this.InfoChannelId));
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
        public List<InfoChannels> GetList()
        {
            List<InfoChannels> RetVal = new List<InfoChannels>();
            try
            {
                string sql = "SELECT * FROM V$InfoChannels";
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
        public static List<InfoChannels> Static_GetList(string ConStr)
        {
            List<InfoChannels> RetVal = new List<InfoChannels>();
            try
            {
                InfoChannels m_InfoChannels = new InfoChannels(ConStr);
                RetVal = m_InfoChannels.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<InfoChannels> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------   
        public static List<InfoChannels> Static_GetListAll(string ConStr ,string TextOptionAll = "...")
        {
            InfoChannels m_InfoChannels = new InfoChannels(ConStr);
            List<InfoChannels> RetVal = m_InfoChannels.GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                m_InfoChannels.InfoChannelName = TextOptionAll;
                m_InfoChannels.InfoChannelDesc = TextOptionAll;
                RetVal.Insert(0, m_InfoChannels);
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<InfoChannels> Static_GetListAll(string TextOptionAll = "...")
        {
            return Static_GetListAll("", TextOptionAll);
        }
        //--------------------------------------------------------------    
        public List<InfoChannels> GetListOrderBy(string OrderBy)
        {
            List<InfoChannels> RetVal = new List<InfoChannels>();
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                string sql = "SELECT * FROM V$InfoChannels " ;
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
        public static List<InfoChannels> Static_GetListOrderBy(string ConStr, string OrderBy)
        {
            InfoChannels m_InfoChannels = new InfoChannels(ConStr);
            return m_InfoChannels.GetListOrderBy(OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<InfoChannels> Static_GetListOrderBy(string OrderBy)
        {
            return Static_GetListOrderBy("", OrderBy);
        }
        //--------------------------------------------------------------    
        public static List<InfoChannels> Static_GetListAllOrderBy(string ConStr, string OrderBy, string TextOptionAll = "...")
        {
            List<InfoChannels> RetVal = new List<InfoChannels>();
            InfoChannels m_InfoChannels = new InfoChannels(ConStr);
            try
            {
                OrderBy = StringUtil.InjectionString(OrderBy).Trim();
                if (OrderBy.Length > 0)
                {
                    RetVal = m_InfoChannels.GetListOrderBy(OrderBy);
                }               
                TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
                if (TextOptionAll.Length > 0)
                {
                    m_InfoChannels.InfoChannelName = TextOptionAll;
                    m_InfoChannels.InfoChannelDesc = TextOptionAll;
                    RetVal.Insert(0, m_InfoChannels);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static List<InfoChannels> Static_GetListAllOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            return Static_GetListAllOrderBy("", OrderBy, TextOptionAll);
        }
        //--------------------------------------------------------------  
        public List<InfoChannels> GetListByInfoChannelId(short InfoChannelId)
        {
            List<InfoChannels> RetVal = new List<InfoChannels>();
            try
            {
                if (InfoChannelId > 0)
                {
                    string sql = "SELECT * FROM V$InfoChannels WHERE (InfoChannelId=" + InfoChannelId.ToString() + ")";
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
        public InfoChannels Get(short InfoChannelId)
        {
            InfoChannels RetVal = new InfoChannels(db.ConnectionString);
            try
            {
                List<InfoChannels> list = GetListByInfoChannelId(InfoChannelId);
                if (list.Count > 0)
                {
                    RetVal = (InfoChannels)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public InfoChannels Get()
        {
            return Get(this.InfoChannelId);
        }
        //-------------------------------------------------------------- 
        public static InfoChannels Static_Get(short InfoChannelId)
        {
            return Static_Get(InfoChannelId);
        }
        //-----------------------------------------------------------------------------
        public static InfoChannels Static_Get(short InfoChannelId, List<InfoChannels> lList)
        {
            InfoChannels RetVal = new InfoChannels();
            if (InfoChannelId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.InfoChannelId == InfoChannelId);
                if (RetVal == null) RetVal = new InfoChannels();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    } 
}