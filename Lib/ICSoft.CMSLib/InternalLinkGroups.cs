using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using sms.utils;
using sms.database;
using sms.common;

namespace ICSoft.CMSLib
{
    public class InternalLinkGroups
    {
        private short _InternalLinkGroupId;
        private string _InternalLinkGroupName;
        private string _InternalLinkGroupDesc;
        private DateTime _CrDateTime;
        private byte _StatusId;
        private short _SiteId;
        private DBAccess db;
        public SysMessages m_SysMessages;

        //-----------------------------------------------------------
        public InternalLinkGroups()
        {
            db = new DBAccess(CmsConstants.CONNECTION_STRING);
            m_SysMessages = new SysMessages(db.ConnectionString);
        }
        //-----------------------------------------------------------
        public InternalLinkGroups(string connString)
        {
            db = new DBAccess(connString);
            m_SysMessages = new SysMessages(connString);
        }
        //-----------------------------------------------------------
        ~InternalLinkGroups() { }
        //-----------------------------------------------------------
        public short InternalLinkGroupId
        {
            get
            {
                return _InternalLinkGroupId;
            }
            set
            {
                _InternalLinkGroupId = value;
            }
        }
        //-----------------------------------------------------------
        public string InternalLinkGroupName
        {
            get
            {
                return _InternalLinkGroupName;
            }
            set
            {
                _InternalLinkGroupName = value;
            }
        }
        //-----------------------------------------------------------
        public string InternalLinkGroupDesc
        {
            get
            {
                return _InternalLinkGroupDesc;
            }
            set
            {
                _InternalLinkGroupDesc = value;
            }
        }
        //-----------------------------------------------------------
        public DateTime CrDateTime
        {
            get
            {
                return _CrDateTime;
            }
            set
            {
                _CrDateTime = value;
            }
        }
        //-----------------------------------------------------------
        public byte StatusId
        {
            get
            {
                return _StatusId;
            }
            set
            {
                _StatusId = value;
            }
        }
        //-----------------------------------------------------------
        public short SiteId
        {
            get
            {
                return _SiteId;
            }
            set
            {
                _SiteId = value;
            }
        }
        
        //-----------------------------------------------------------
        private IList Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            IList l_InternalLinkGroups = new ArrayList();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    InternalLinkGroups m_InternalLinkGroups = new InternalLinkGroups(db.ConnectionString);
                    m_InternalLinkGroups.InternalLinkGroupId = smartReader.GetInt16("InternalLinkGroupId");
                    m_InternalLinkGroups.InternalLinkGroupName = smartReader.GetString("InternalLinkGroupName");
                    m_InternalLinkGroups.InternalLinkGroupDesc = smartReader.GetString("InternalLinkGroupDesc");
                    m_InternalLinkGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_InternalLinkGroups.StatusId = smartReader.GetByte("StatusId");
                    m_InternalLinkGroups.SiteId = smartReader.GetInt16("SiteId");
                    l_InternalLinkGroups.Add(m_InternalLinkGroups);
                }
                reader.Close();
            }
            catch (SqlException err)
            {
                Log.writeLog(err.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            finally
            {
                db.closeConnection(con);
            }
            return l_InternalLinkGroups;
        }
        //-----------------------------------------------------------
        public short Insert()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InternalLinkGroups_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InternalLinkGroupName", this.InternalLinkGroupName));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkGroupDesc", this.InternalLinkGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add("@InternalLinkGroupId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.InternalLinkGroupId = Int16.Parse(cmd.Parameters["@InternalLinkGroupId"].Value.ToString());
                m_SysMessages.SysMessageId = (short)cmd.Parameters["@SysMessageId"].Value;
                m_SysMessages.SysMessageTypeId = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //-----------------------------------------------------------
        public short Update()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InternalLinkGroups_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InternalLinkGroupId", this.InternalLinkGroupId));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkGroupName", this.InternalLinkGroupName));
                cmd.Parameters.Add(new SqlParameter("@InternalLinkGroupDesc", this.InternalLinkGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                m_SysMessages.SysMessageId = (short)cmd.Parameters["@SysMessageId"].Value;
                m_SysMessages.SysMessageTypeId = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //-----------------------------------------------------------
        public short Delete()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InternalLinkGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InternalLinkGroupId", this.InternalLinkGroupId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                m_SysMessages.SysMessageId = (short)cmd.Parameters["@SysMessageId"].Value;
                m_SysMessages.SysMessageTypeId = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                m_SysMessages.SysMessageTypeId = 0;
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return m_SysMessages.SysMessageTypeId;
        }
        //-----------------------------------------------------------
        public IList GetList(short siteId)
        {
            IList list = new ArrayList();
            try
            {
                string sql = "SELECT * FROM InternalLinkGroups WHERE SiteId=" + siteId;
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public IList GetList()
        {
            IList list = new ArrayList();
            try
            {
                string sql = "SELECT * FROM InternalLinkGroups";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public IList GetListNA()
        {
            IList list = new ArrayList();
            try
            {
                string sql = "SELECT * FROM InternalLinkGroups";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public IList GetListAll()
        {
            IList list = new ArrayList();
            try
            {
                string sql = "SELECT * FROM InternalLinkGroups";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;
        }
        //-----------------------------------------------------------
        public IList GetListAllNA()
        {
            IList list = new ArrayList();
            try
            {
                string sql = "SELECT * FROM InternalLinkGroups";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                list = Init(cmd);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return list;

        }
        //-----------------------------------------------------------
        public InternalLinkGroups Get()
        {
            InternalLinkGroups m_InternalLinkGroups = new InternalLinkGroups();
            try
            {
                string sql = "SELECT * FROM InternalLinkGroups WHERE (InternalLinkGroupId = " + this.InternalLinkGroupId + ")";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                IList list = Init(cmd);
                if (list.Count == 1)
                {
                    m_InternalLinkGroups = (InternalLinkGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog( ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + ":" +  ex.ToString());
            }
            return m_InternalLinkGroups;
        }
    }
}
