
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
    /// class RelateTypeGroups
    /// </summary>
    public class RelateTypeGroups
    {
        private byte _RelateTypeGroupId;
        private string _RelateTypeGroupName;
        private string _RelateTypeGroupDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public RelateTypeGroups()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public RelateTypeGroups(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~RelateTypeGroups()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte RelateTypeGroupId
        {
            get { return _RelateTypeGroupId; }
            set { _RelateTypeGroupId = value; }
        }
        //-----------------------------------------------------------------
        public string RelateTypeGroupName
        {
            get { return _RelateTypeGroupName; }
            set { _RelateTypeGroupName = value; }
        }
        //-----------------------------------------------------------------
        public string RelateTypeGroupDesc
        {
            get { return _RelateTypeGroupDesc; }
            set { _RelateTypeGroupDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------
        public static byte LienQuanHieuLuc
        {
            get
            {
                return 1;
            }
            set
            {

            }
        }
        //-----------------------------------------------------------------
        public static byte LienQuanNoiDung
        {
            get
            {
                return 2;
            }
            set
            {

            }
        }
        //-----------------------------------------------------------------
        public static byte LienQuanKhac
        {
            get
            {
                return 3;
            }
            set
            {

            }
        }
        //-----------------------------------------------------------------
        private List<RelateTypeGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<RelateTypeGroups> l_RelateTypeGroups = new List<RelateTypeGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RelateTypeGroups m_RelateTypeGroups = new RelateTypeGroups(db.ConnectionString);
                    m_RelateTypeGroups.RelateTypeGroupId = smartReader.GetByte("RelateTypeGroupId");
                    m_RelateTypeGroups.RelateTypeGroupName = smartReader.GetString("RelateTypeGroupName");
                    m_RelateTypeGroups.RelateTypeGroupDesc = smartReader.GetString("RelateTypeGroupDesc");
                    m_RelateTypeGroups.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_RelateTypeGroups.Add(m_RelateTypeGroups);
                }
                reader.Close();
                return l_RelateTypeGroups;
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
                SqlCommand cmd = new SqlCommand("RelateTypeGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeGroupName", this.RelateTypeGroupName));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeGroupDesc", this.RelateTypeGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeGroupId", this.RelateTypeGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.RelateTypeGroupId = Convert.ToByte((cmd.Parameters["@RelateTypeGroupId"].Value == null) ? 0 : (cmd.Parameters["@RelateTypeGroupId"].Value));
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
                SqlCommand cmd = new SqlCommand("RelateTypeGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeGroupId", this.RelateTypeGroupId));
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
        public List<RelateTypeGroups> GetList()
        {
            List<RelateTypeGroups> RetVal = new List<RelateTypeGroups>();
            try
            {
                string sql = "SELECT * FROM V$RelateTypeGroups";
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
        public static List<RelateTypeGroups> Static_GetList(string ConStr)
        {
            List<RelateTypeGroups> RetVal = new List<RelateTypeGroups>();
            try
            {
                RelateTypeGroups m_RelateTypeGroups = new RelateTypeGroups(ConStr);
                RetVal = m_RelateTypeGroups.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<RelateTypeGroups> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<RelateTypeGroups> GetListByRelateTypeGroupId(byte RelateTypeGroupId)
        {
            List<RelateTypeGroups> RetVal = new List<RelateTypeGroups>();
            try
            {
                if (RelateTypeGroupId > 0)
                {
                    string sql = "SELECT * FROM V$RelateTypeGroups WHERE (RelateTypeGroupId=" + RelateTypeGroupId.ToString() + ")";
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
        public RelateTypeGroups Get(byte RelateTypeGroupId)
        {
            RelateTypeGroups RetVal = new RelateTypeGroups(db.ConnectionString);
            try
            {
                List<RelateTypeGroups> list = GetListByRelateTypeGroupId(RelateTypeGroupId);
                if (list.Count > 0)
                {
                    RetVal = (RelateTypeGroups)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public RelateTypeGroups Get()
        {
            return Get(this.RelateTypeGroupId);
        }
        //-------------------------------------------------------------- 
        public static RelateTypeGroups Static_Get(byte RelateTypeGroupId)
        {
            return Static_Get(RelateTypeGroupId);
        }
        //-----------------------------------------------------------------------------
        public static RelateTypeGroups Static_Get(byte RelateTypeGroupId, List<RelateTypeGroups> lList)
        {
            RelateTypeGroups RetVal = new RelateTypeGroups();
            if (RelateTypeGroupId > 0 && lList.Count > 0)
            {
                RetVal = lList.Find(i => i.RelateTypeGroupId == RelateTypeGroupId);
                if (RetVal == null) RetVal = new RelateTypeGroups();
            }
            return RetVal;
        }
        //--------------------------------------------------------------
    }
}