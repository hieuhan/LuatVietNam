
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
    /// class FaqDocs
    /// </summary>
    public class FaqDocs
    {
        private int _FaqDocId;
        private int _FaqId;
        private int _DocId;
        private string _Remark;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
        public FaqDocs()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public FaqDocs(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~FaqDocs()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int FaqDocId
        {
            get { return _FaqDocId; }
            set { _FaqDocId = value; }
        }
        //-----------------------------------------------------------------
        public int FaqId
        {
            get { return _FaqId; }
            set { _FaqId = value; }
        }
        //-----------------------------------------------------------------
        public int DocId
        {
            get { return _DocId; }
            set { _DocId = value; }
        }
        //-----------------------------------------------------------------
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
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

        private List<FaqDocs> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<FaqDocs> l_FaqDocs = new List<FaqDocs>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    FaqDocs m_FaqDocs = new FaqDocs(db.ConnectionString);
                    m_FaqDocs.FaqDocId = smartReader.GetInt32("FaqDocId");
                    m_FaqDocs.FaqId = smartReader.GetInt32("FaqId");
                    m_FaqDocs.DocId = smartReader.GetInt32("DocId");
                    m_FaqDocs.Remark = smartReader.GetString("Remark");
                    m_FaqDocs.CrUserId = smartReader.GetInt32("CrUserId");
                    m_FaqDocs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_FaqDocs.Add(m_FaqDocs);
                }
                reader.Close();
                return l_FaqDocs;
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
                SqlCommand cmd = new SqlCommand("FaqDocs_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqId", this.FaqId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@Remark", this.Remark));
                //cmd.Parameters.Add(new SqlParameter("@FaqDocId", this.FaqDocId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                //this.FaqDocId = Convert.ToInt32((cmd.Parameters["@FaqDocId"].Value == null) ? 0 : (cmd.Parameters["@FaqDocId"].Value));
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
                SqlCommand cmd = new SqlCommand("FaqDocs_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqId", this.FaqId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
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
        public List<FaqDocs> GetList()
        {
            List<FaqDocs> RetVal = new List<FaqDocs>();
            try
            {
                string sql = "SELECT * FROM V$FaqDocs";
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
        public static List<FaqDocs> Static_GetList(string ConStr)
        {
            List<FaqDocs> RetVal = new List<FaqDocs>();
            try
            {
                FaqDocs m_FaqDocs = new FaqDocs(ConStr);
                RetVal = m_FaqDocs.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<FaqDocs> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<FaqDocs> GetListByFaqDocId(int FaqDocId)
        {
            List<FaqDocs> RetVal = new List<FaqDocs>();
            try
            {
                if (FaqDocId > 0)
                {
                    string sql = "SELECT * FROM V$FaqDocs WHERE (FaqDocId=" + FaqDocId.ToString() + ")";
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
        public FaqDocs Get(int FaqDocId)
        {
            FaqDocs RetVal = new FaqDocs(db.ConnectionString);
            try
            {
                List<FaqDocs> list = GetListByFaqDocId(FaqDocId);
                if (list.Count > 0)
                {
                    RetVal = (FaqDocs)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public FaqDocs Get()
        {
            return Get(this.FaqDocId);
        }
        //-------------------------------------------------------------- 
        public static FaqDocs Static_Get(int FaqDocId)
        {
            return Static_Get(FaqDocId);
        }
        //--------------------------------------------------------------     
        public List<FaqDocs> FaqDocs_GetList(int ActUserId, int FaqId)
        {
            List<FaqDocs> RetVal = new List<FaqDocs>();
            try
            {
                SqlCommand cmd = new SqlCommand("FaqDocs_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FaqId", FaqId));
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