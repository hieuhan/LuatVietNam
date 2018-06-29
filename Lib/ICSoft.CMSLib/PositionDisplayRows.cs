
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using System.Data;

namespace ICSoft.CMSLib
{
    public class PositionDisplayRows
    {
        private int _PositionDisplayRowId;
        private short _SiteId;
        private string _DisplayTypeName;
        private short _DisplayTypeId;
        private int _RowDisplay;
        private byte _UseSummary;
        private byte _UseArticleContent;
        private DBAccess db;
        //-----------------------------------------------------------------
        public PositionDisplayRows()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public PositionDisplayRows(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~PositionDisplayRows()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int PositionDisplayRowId
        {
            get { return _PositionDisplayRowId; }
            set { _PositionDisplayRowId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public short DisplayTypeId
        {
            get { return _DisplayTypeId; }
            set { _DisplayTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string DisplayTypeName
        {
            get { return _DisplayTypeName; }
            set { _DisplayTypeName = value; }
        }
        //-----------------------------------------------------------------
        public int RowDisplay
        {
            get { return _RowDisplay; }
            set { _RowDisplay = value; }
        }
        //-----------------------------------------------------------------
        public byte UseSummary
        {
            get { return _UseSummary; }
            set { _UseSummary = value; }
        }
        //-----------------------------------------------------------------
        public byte UseArticleContent
        {
            get { return _UseArticleContent; }
            set { _UseArticleContent = value; }
        }
        //-----------------------------------------------------------------

        private List<PositionDisplayRows> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<PositionDisplayRows> l_PositionDisplayRows = new List<PositionDisplayRows>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    PositionDisplayRows m_PositionDisplayRows = new PositionDisplayRows(db.ConnectionString);
                    m_PositionDisplayRows.PositionDisplayRowId = smartReader.GetInt32("PositionDisplayRowId");
                    m_PositionDisplayRows.SiteId = smartReader.GetInt16("SiteId");
                    m_PositionDisplayRows.DisplayTypeName = smartReader.GetString("DisplayTypeName");
                    m_PositionDisplayRows.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_PositionDisplayRows.RowDisplay = smartReader.GetInt32("RowDisplay");
                    m_PositionDisplayRows.UseSummary = smartReader.GetByte("UseSummary");
                    m_PositionDisplayRows.UseArticleContent = smartReader.GetByte("UseArticleContent");

                    l_PositionDisplayRows.Add(m_PositionDisplayRows);
                }
                reader.Close();
                return l_PositionDisplayRows;
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
                SqlCommand cmd = new SqlCommand("PositionDisplayRows_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowDisplay", this.RowDisplay));
                cmd.Parameters.Add(new SqlParameter("@UseSummary", this.UseSummary));
                cmd.Parameters.Add(new SqlParameter("@UseArticleContent", this.UseArticleContent));
                cmd.Parameters.Add("@PositionDisplayRowId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PositionDisplayRowId = (cmd.Parameters["@PositionDisplayRowId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@PositionDisplayRowId"].Value);
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
                SqlCommand cmd = new SqlCommand("PositionDisplayRows_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowDisplay", this.RowDisplay));
                cmd.Parameters.Add(new SqlParameter("@UseSummary", this.UseSummary));
                cmd.Parameters.Add(new SqlParameter("@UseArticleContent", this.UseArticleContent));
                cmd.Parameters.Add(new SqlParameter("@PositionDisplayRowId", this.PositionDisplayRowId));
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
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("PositionDisplayRows_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", this.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowDisplay", this.RowDisplay));
                cmd.Parameters.Add(new SqlParameter("@UseSummary", this.UseSummary));
                cmd.Parameters.Add(new SqlParameter("@UseArticleContent", this.UseArticleContent));
                cmd.Parameters.Add(new SqlParameter("@PositionDisplayRowId", this.PositionDisplayRowId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.PositionDisplayRowId = (cmd.Parameters["@PositionDisplayRowId"].Value == null) ? 0 : Convert.ToInt32(cmd.Parameters["@PositionDisplayRowId"].Value);
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
                SqlCommand cmd = new SqlCommand("PositionDisplayRows_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PositionDisplayRowId", this.PositionDisplayRowId));
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
        public List<PositionDisplayRows> GetList(short SiteId)
        {
            List<PositionDisplayRows> RetVal = new List<PositionDisplayRows>();
            try
            {
                SqlCommand cmd = new SqlCommand("PositionDisplayRows_GetBySiteId");
                cmd.CommandType = CommandType.StoredProcedure;
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

