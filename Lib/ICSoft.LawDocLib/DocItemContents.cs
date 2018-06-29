
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
    /// class DocItemContents
    /// </summary>
    public class DocItemContents
    {
        private int _DocItemContentId;
        private int _DocItemContentOrder;
        private string _Bullet;
        private string _AfterBullet;
        private string _DocParagraph;
        private string _PlainParagraph;
        private byte _MediaTypeId;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _DocItemId;
        private DBAccess db;
        //-----------------------------------------------------------------
        public DocItemContents()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public DocItemContents(string constr)
        {
            db = new DBAccess((constr == "") ? DocConstants.DOC_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~DocItemContents()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int DocItemContentId
        {
            get { return _DocItemContentId; }
            set { _DocItemContentId = value; }
        }
        //-----------------------------------------------------------------
        public int DocItemContentOrder
        {
            get { return _DocItemContentOrder; }
            set { _DocItemContentOrder = value; }
        }
        //-----------------------------------------------------------------
        public string Bullet
        {
            get { return _Bullet; }
            set { _Bullet = value; }
        }
        //-----------------------------------------------------------------
        public string AfterBullet
        {
            get { return _AfterBullet; }
            set { _AfterBullet = value; }
        }
        //-----------------------------------------------------------------
        public string DocParagraph
        {
            get { return _DocParagraph; }
            set { _DocParagraph = value; }
        }
        //-----------------------------------------------------------------
        public string PlainParagraph
        {
            get { return _PlainParagraph; }
            set { _PlainParagraph = value; }
        }
        //-----------------------------------------------------------------
        public byte MediaTypeId
        {
            get { return _MediaTypeId; }
            set { _MediaTypeId = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
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

        public int DocItemId
        {
            get { return _DocItemId; }
            set { _DocItemId = value; }
        }
        private List<DocItemContents> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocItemContents> l_DocItemContents = new List<DocItemContents>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocItemContents m_DocItemContents = new DocItemContents(db.ConnectionString);
                    m_DocItemContents.DocItemContentId = smartReader.GetInt32("DocItemContentId");
                    m_DocItemContents.DocItemId = smartReader.GetInt32("DocItemId");
                    m_DocItemContents.DocItemContentOrder = smartReader.GetInt32("DocItemContentOrder");
                    m_DocItemContents.Bullet = smartReader.GetString("Bullet");
                    m_DocItemContents.AfterBullet = smartReader.GetString("AfterBullet");
                    m_DocItemContents.DocParagraph = smartReader.GetString("DocParagraph");
                    m_DocItemContents.PlainParagraph = smartReader.GetString("PlainParagraph");
                    m_DocItemContents.MediaTypeId = smartReader.GetByte("MediaTypeId");
                    m_DocItemContents.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_DocItemContents.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocItemContents.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_DocItemContents.Add(m_DocItemContents);
                }
                reader.Close();
                return l_DocItemContents;
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
                SqlCommand cmd = new SqlCommand("DocItemContents_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocItemId", this.DocItemId));
                cmd.Parameters.Add(new SqlParameter("@DocItemContentOrder", this.DocItemContentOrder));
                cmd.Parameters.Add(new SqlParameter("@Bullet", this.Bullet));
                cmd.Parameters.Add(new SqlParameter("@AfterBullet", this.AfterBullet));
                cmd.Parameters.Add(new SqlParameter("@DocParagraph", this.DocParagraph));
                cmd.Parameters.Add(new SqlParameter("@PlainParagraph", this.PlainParagraph));
                cmd.Parameters.Add(new SqlParameter("@MediaTypeId", this.MediaTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocItemContentId", this.DocItemContentId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocItemContentId = Convert.ToInt32((cmd.Parameters["@DocItemContentId"].Value == null) ? 0 : (cmd.Parameters["@DocItemContentId"].Value));
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
                SqlCommand cmd = new SqlCommand("DocItemContents_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocItemContentId", this.DocItemContentId));
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
        public List<DocItemContents> GetList()
        {
            List<DocItemContents> RetVal = new List<DocItemContents>();
            try
            {
                string sql = "SELECT * FROM V$DocItemContents";
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
        public static List<DocItemContents> Static_GetList(string ConStr)
        {
            List<DocItemContents> RetVal = new List<DocItemContents>();
            try
            {
                DocItemContents m_DocItemContents = new DocItemContents(ConStr);
                RetVal = m_DocItemContents.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<DocItemContents> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------  
        public List<DocItemContents> GetListByDocItemContentId(int DocItemContentId)
        {
            List<DocItemContents> RetVal = new List<DocItemContents>();
            try
            {
                int ActUserId = 0;
                int RowAmount = 0;
                int PageIndex = 0;
                string OrderBy = "";
                string DateFrom = "";
                string DateTo = "";
                int RowCount = 0;
                RetVal = GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DocItemContentId, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocItemContents Get(int DocItemContentId)
        {
            DocItemContents RetVal = new DocItemContents(db.ConnectionString);
            try
            {
                List<DocItemContents> list = GetListByDocItemContentId(DocItemContentId);
                if (list.Count > 0)
                {
                    RetVal = (DocItemContents)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public DocItemContents Get()
        {
            return Get(this.DocItemContentId);
        }
        //-------------------------------------------------------------- 
        public static DocItemContents Static_Get(int DocItemContentId)
        {
            return Static_Get(DocItemContentId);
        }
        //--------------------------------------------------------------     
        public List<DocItemContents> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, int DocItemContentId, string DateFrom, string DateTo, ref int RowCount)
        {
            List<DocItemContents> RetVal = new List<DocItemContents>();
            try
            {
                SqlCommand cmd = new SqlCommand("DocItemContents_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@DocItemContentId", DocItemContentId));
                cmd.Parameters.Add(new SqlParameter("@DocItemContentOrder", this.DocItemContentOrder));
                cmd.Parameters.Add(new SqlParameter("@Bullet", this.Bullet));
                cmd.Parameters.Add(new SqlParameter("@AfterBullet", this.AfterBullet));
                cmd.Parameters.Add(new SqlParameter("@DocParagraph", this.DocParagraph));
                cmd.Parameters.Add(new SqlParameter("@PlainParagraph", this.PlainParagraph));
                cmd.Parameters.Add(new SqlParameter("@MediaTypeId", this.MediaTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", StringUtil.ConvertToDateTime(DateFrom)));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", StringUtil.ConvertToDateTime(DateTo)));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                RetVal = Init(cmd);
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<DocItemContents> Search(int ActUserId, string OrderBy, int DocItemContentId, string DateFrom, string DateTo, ref int RowCount)
        {
            int RowAmount = 0;
            int PageIndex = 0;
            return GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DocItemContentId, DateFrom, DateTo, ref RowCount);
        }
    }
}