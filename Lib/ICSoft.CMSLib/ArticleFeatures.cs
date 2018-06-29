
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;
using ICSoft.CMSViewLib;

namespace ICSoft.CMSLib
{
    public class ArticleFeatures
    {
        private int _ArticleFeatureId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private int _ArticleId;
        private short _FeatureId;
        private string _FeatureValue;
        private DBAccess db;
        //-----------------------------------------------------------------
        public ArticleFeatures()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public ArticleFeatures(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~ArticleFeatures()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ArticleFeatureId
        {
            get { return _ArticleFeatureId; }
            set { _ArticleFeatureId = value; }
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

        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }
        public short FeatureId
        {
            get { return _FeatureId; }
            set { _FeatureId = value; }
        }
        //-----------------------------------------------------------------
        public string FeatureValue
        {
            get { return _FeatureValue; }
            set { _FeatureValue = value; }
        }   
        private List<ArticleFeatures> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<ArticleFeatures> l_ArticleFeatures = new List<ArticleFeatures>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    ArticleFeatures m_ArticleFeatures = new ArticleFeatures(db.ConnectionString);
                    m_ArticleFeatures.ArticleFeatureId = smartReader.GetInt32("ArticleFeatureId");
                    m_ArticleFeatures.ArticleId = smartReader.GetInt32("ArticleId");
                    m_ArticleFeatures.FeatureId = smartReader.GetInt16("FeatureId");
                    m_ArticleFeatures.FeatureValue = smartReader.GetString("FeatureValue");
                    m_ArticleFeatures.CrUserId = smartReader.GetInt32("CrUserId");
                    m_ArticleFeatures.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_ArticleFeatures.Add(m_ArticleFeatures);
                }
                reader.Close();
                return l_ArticleFeatures;
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
                SqlCommand cmd = new SqlCommand("ArticleFeatures_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
                cmd.Parameters.Add(new SqlParameter("@FeatureId", this.FeatureId));
                cmd.Parameters.Add(new SqlParameter("@FeatureValue", this.FeatureValue));
                cmd.Parameters.Add("@ArticleFeatureId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ArticleFeatureId = Convert.ToInt32(cmd.Parameters["@ArticleFeatureId"].Value);
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
        public byte DeleteByArticleId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("ArticleFeatures_DeleteByArticleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ArticleId", this.ArticleId));
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
        public List<ArticleFeatures> GetListByArticleId(int ArticleId)
        {
            List<ArticleFeatures> RetVal = new List<ArticleFeatures>();
            try
            {
                string sql = "SELECT * FROM ArticleFeatures WHERE ArticleId=" + ArticleId.ToString();
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
        public List<FeaturesView> GetFeatureDataForUpdate(byte DataTypeId, short CategoryId, int ArticleId, string PaddingChar)
        {
            List<FeaturesView> RetVal = new List<FeaturesView>();
            try
            {
                ArticleFeatures m_ArticleFeatures = new ArticleFeatures();
                Features m_Features = new Features();
                m_Features.FeatureGroupId = 0;
                m_Features.IsData = 1;
                List<Features> l_Features = m_Features.GetListByFeatureGroupId(DataTypeId, CategoryId, PaddingChar);
                List<ArticleFeatures> l_ArticleFeatures = GetListByArticleId(ArticleId);
                if (l_Features != null)
                {
                    for (int i = 0; i < l_Features.Count; i++)
                    {
                        FeaturesView m_FeaturesView = new FeaturesView();
                        m_FeaturesView.DataDictionaryTypeId = l_Features[i].DataDictionaryTypeId;
                        m_FeaturesView.FeatureId = l_Features[i].FeatureId;
                        m_FeaturesView.FeatureName = l_Features[i].FeatureName;
                        m_FeaturesView.FeatureDesc = l_Features[i].FeatureDesc;
                        m_FeaturesView.IconPath = l_Features[i].IconPath;
                        m_FeaturesView.InputTypeId = l_Features[i].InputTypeId;
                        m_FeaturesView.ParentFeatureId = l_Features[i].ParentFeatureId;
                        m_FeaturesView.CrDateTime = l_Features[i].CrDateTime;
                        m_FeaturesView.CrUserId = l_Features[i].CrUserId;
                        m_FeaturesView.DisplayOrder = l_Features[i].DisplayOrder;
                        m_FeaturesView.FeatureGroupId = l_Features[i].FeatureGroupId;
                        m_FeaturesView.IsData = l_Features[i].IsData;
                        m_FeaturesView.IsDisplay = l_Features[i].IsDisplay;
                        m_FeaturesView.IsSearch = l_Features[i].IsSearch;

                        m_ArticleFeatures = l_ArticleFeatures.Find(j => j.FeatureId == l_Features[i].FeatureId);
                        if (m_ArticleFeatures != null)
                        {
                            m_FeaturesView.FeatureValue = m_ArticleFeatures.FeatureValue;
                        }
                        RetVal.Add(m_FeaturesView);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static ArticleFeatures Static_GetByFeatureId(short FeatureId, List<ArticleFeatures> list)
        {
            ArticleFeatures RetVal = new ArticleFeatures();
            RetVal = list.Find(i => i.FeatureId == FeatureId);
            if (RetVal == null) RetVal = new ArticleFeatures();
            return RetVal;
        }
    }
}

