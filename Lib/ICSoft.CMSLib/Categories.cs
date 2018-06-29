
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sms.database;
using sms.utils;
using ICSoft.CMSViewLib;

namespace ICSoft.CMSLib
{
    public class Categories
    {   
        private byte _LanguageId;
        private byte _ApplicationTypeId;
	    private short _CategoryId;
	    private string _CategoryName;
	    private string _CategoryDesc;
	    private string _CategoryUrl;
        private short _DataTypeId;
        private short _SiteId;
        private short _FeatureGroupId;
        private byte _ShowTop;
        private byte _ShowBottom;
        private byte _ShowWeb;
        private byte _ShowWap;
        private byte _ShowApp;
        private string _UrlRewriteType;
	    private string _MetaTitle;
	    private string _MetaDesc;
	    private string _MetaKeyword;
        private string _CanonicalTag;
        private string _H1Tag;
        private string _SeoFooter;
	    private int _ParentCategoryId;
	    private byte _CategoryLevel;
	    private string _ImagePath;
	    private int _DisplayOrder;
	    private byte _ReviewStatusId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private string _JsonData;

        private DBAccess db;
        //-----------------------------------------------------------------
		public Categories()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Categories(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Categories()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        //-----------------------------------------------------------------    
        public byte ApplicationTypeId
        {
            get { return _ApplicationTypeId; }
            set { _ApplicationTypeId = value; }
        }
        //-----------------------------------------------------------------    
	    public short CategoryId
        {
		    get { return _CategoryId; }
		    set { _CategoryId = value; }
	    }
        //-----------------------------------------------------------------
        public string CategoryName
		{
            get { return _CategoryName; }
		    set { _CategoryName = value; }
		}    
        //-----------------------------------------------------------------
        public string CategoryDesc
		{
            get { return _CategoryDesc; }
		    set { _CategoryDesc = value; }
		}    
        //-----------------------------------------------------------------
        public string CategoryUrl
		{
            get { return _CategoryUrl; }
		    set { _CategoryUrl = value; }
		}
        //-----------------------------------------------------------------
        public short DataTypeId
        {
            get { return _DataTypeId; }
            set { _DataTypeId = value; }
        }
        //-----------------------------------------------------------------
        public short SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }
        //-----------------------------------------------------------------
        public short FeatureGroupId
        {
            get { return _FeatureGroupId; }
            set { _FeatureGroupId = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowTop
        {
            get { return _ShowTop; }
            set { _ShowTop = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowBottom
        {
            get { return _ShowBottom; }
            set { _ShowBottom = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowWeb
        {
            get { return _ShowWeb; }
            set { _ShowWeb = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowWap
        {
            get { return _ShowWap; }
            set { _ShowWap = value; }
        }
        //-----------------------------------------------------------------
        public byte ShowApp
        {
            get { return _ShowApp; }
            set { _ShowApp = value; }
        }
        //-----------------------------------------------------------------
        public string UrlRewriteType
        {
            get { return _UrlRewriteType; }
            set { _UrlRewriteType = value; }
        }   
        //-----------------------------------------------------------------
        public string MetaTitle
		{
            get { return _MetaTitle; }
		    set { _MetaTitle = value; }
		}    
        //-----------------------------------------------------------------
        public string MetaDesc
		{
            get { return _MetaDesc; }
		    set { _MetaDesc = value; }
		}    
        //-----------------------------------------------------------------
        public string MetaKeyword
		{
            get { return _MetaKeyword; }
		    set { _MetaKeyword = value; }
		}
        //-----------------------------------------------------------------
        public string CanonicalTag
        {
            get { return _CanonicalTag; }
            set { _CanonicalTag = value; }
        }
        //-----------------------------------------------------------------
        public string H1Tag
        {
            get { return _H1Tag; }
            set { _H1Tag = value; }
        }
        //-----------------------------------------------------------------
        public string SeoFooter
        {
            get { return _SeoFooter; }
            set { _SeoFooter = value; }
        }    
        //-----------------------------------------------------------------
        public int ParentCategoryId
		{
            get { return _ParentCategoryId; }
		    set { _ParentCategoryId = value; }
		}    
        //-----------------------------------------------------------------
        public byte CategoryLevel
		{
            get { return _CategoryLevel; }
		    set { _CategoryLevel = value; }
		}    
        //-----------------------------------------------------------------
        public string ImagePath
		{
            get { return _ImagePath; }
		    set { _ImagePath = value; }
		}    
        //-----------------------------------------------------------------
        public int DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
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
        public string JsonData
        {
            get { return _JsonData; }
            set { _JsonData = value; }
        }    
        //-----------------------------------------------------------------
        private List<Categories> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Categories> l_Categories = new List<Categories>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Categories m_Categories = new Categories(db.ConnectionString);
                    m_Categories.LanguageId = smartReader.GetByte("LanguageId");
                    m_Categories.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Categories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_Categories.CategoryName = smartReader.GetString("CategoryName");
                    m_Categories.CategoryDesc = smartReader.GetString("CategoryDesc");
                    m_Categories.CategoryUrl = smartReader.GetString("CategoryUrl");
                    m_Categories.DataTypeId = smartReader.GetInt16("DataTypeId");
                    m_Categories.SiteId = smartReader.GetInt16("SiteId");
                    m_Categories.FeatureGroupId = smartReader.GetInt16("FeatureGroupId");
                    m_Categories.ShowTop = smartReader.GetByte("ShowTop");
                    m_Categories.ShowBottom = smartReader.GetByte("ShowBottom");
                    m_Categories.ShowWeb = smartReader.GetByte("ShowWeb");
                    m_Categories.ShowWap = smartReader.GetByte("ShowWap");
                    m_Categories.ShowApp = smartReader.GetByte("ShowApp");
                    m_Categories.UrlRewriteType = smartReader.GetString("UrlRewriteType");
                    m_Categories.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Categories.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Categories.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Categories.CanonicalTag = smartReader.GetString("CanonicalTag");
                    m_Categories.H1Tag = smartReader.GetString("H1Tag");
                    m_Categories.SeoFooter = smartReader.GetString("SeoFooter");
                    m_Categories.ParentCategoryId = smartReader.GetInt32("ParentCategoryId");
                    m_Categories.CategoryLevel = smartReader.GetByte("CategoryLevel");
                    m_Categories.ImagePath = smartReader.GetString("ImagePath");
                    m_Categories.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Categories.JsonData = smartReader.GetString("JsonData");
                    m_Categories.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Categories.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Categories.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Categories.Add(m_Categories);
                }
                reader.Close();
                return l_Categories;
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
        //-----------------------------------------------------------------
        private List<CategoriesView> InitView(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CategoriesView> l_Categories = new List<CategoriesView>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CategoriesView m_Categories = new CategoriesView();
                    m_Categories.LanguageId = smartReader.GetByte("LanguageId");
                    m_Categories.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                    m_Categories.CategoryId = smartReader.GetInt16("CategoryId");
                    m_Categories.CategoryName = smartReader.GetString("CategoryName");
                    m_Categories.CategoryDesc = smartReader.GetString("CategoryDesc");
                    m_Categories.CategoryUrl = smartReader.GetString("CategoryUrl");
                    m_Categories.DataTypeId = smartReader.GetInt16("DataTypeId");
                    m_Categories.SiteId = smartReader.GetInt16("SiteId");
                    m_Categories.FeatureGroupId = smartReader.GetInt16("FeatureGroupId");
                    m_Categories.ShowTop = smartReader.GetByte("ShowTop");
                    m_Categories.ShowBottom = smartReader.GetByte("ShowBottom");
                    m_Categories.ShowWeb = smartReader.GetByte("ShowWeb");
                    m_Categories.ShowWap = smartReader.GetByte("ShowWap");
                    m_Categories.ShowApp = smartReader.GetByte("ShowApp");
                    m_Categories.MetaTitle = smartReader.GetString("MetaTitle");
                    if (String.IsNullOrEmpty(m_Categories.MetaTitle))
                        m_Categories.MetaTitle = m_Categories.CategoryName;
                    m_Categories.MetaDesc = smartReader.GetString("MetaDesc");
                    if (String.IsNullOrEmpty(m_Categories.MetaDesc))
                        m_Categories.MetaTitle = m_Categories.CategoryDesc;
                    m_Categories.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_Categories.CanonicalTag = smartReader.GetString("CanonicalTag");
                    m_Categories.H1Tag = smartReader.GetString("H1Tag");
                    m_Categories.SeoFooter = smartReader.GetString("SeoFooter");
                    m_Categories.ParentCategoryId = smartReader.GetInt32("ParentCategoryId");
                    m_Categories.CategoryLevel = smartReader.GetByte("CategoryLevel");
                    m_Categories.ImagePath = smartReader.GetString("ImagePath");
                    m_Categories.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Categories.JsonData = smartReader.GetString("JsonData");
                    m_Categories.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Categories.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Categories.CrDateTime = smartReader.GetDateTime("CrDateTime");

                    l_Categories.Add(m_Categories);
                }
                reader.Close();
                return l_Categories;
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
                SqlCommand cmd = new SqlCommand("Categories_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryName", this.CategoryName));
                cmd.Parameters.Add(new SqlParameter("@CategoryDesc", this.CategoryDesc));
                cmd.Parameters.Add(new SqlParameter("@CategoryUrl", this.CategoryUrl));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@FeatureGroupId", this.FeatureGroupId));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
                cmd.Parameters.Add(new SqlParameter("@UrlRewriteType", this.UrlRewriteType));
                cmd.Parameters.Add(new SqlParameter("@MetaTitle", this.MetaTitle));
                cmd.Parameters.Add(new SqlParameter("@MetaDesc", this.MetaDesc));
                cmd.Parameters.Add(new SqlParameter("@MetaKeyword", this.MetaKeyword));
                cmd.Parameters.Add(new SqlParameter("@CanonicalTag", this.CanonicalTag));
                cmd.Parameters.Add(new SqlParameter("@H1Tag", this.H1Tag));
                cmd.Parameters.Add(new SqlParameter("@SeoFooter", this.SeoFooter));
                cmd.Parameters.Add(new SqlParameter("@ParentCategoryId", this.ParentCategoryId));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CategoryId =Convert.ToInt16((cmd.Parameters["@CategoryId"].Value == null) ? 0 : (cmd.Parameters["@CategoryId"].Value));
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
        public byte UpdateReviewStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
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
                SqlCommand cmd = new SqlCommand("Categories_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId",this.CategoryId));
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
        public Categories Get(short CategoryId, byte LanguageId, byte ApplicationTypeId)
        {
            Categories RetVal = new Categories(db.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_Get");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                List<Categories> l_Categories = new List<Categories>();
                l_Categories = Init(cmd);
                if (l_Categories.Count > 0)
                {
                    RetVal = (Categories)l_Categories[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public CategoriesView GetView(short CategoryId, byte LanguageId, byte ApplicationTypeId)
        {
            CategoriesView RetVal = new CategoriesView();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_Get");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                List<CategoriesView> l_Categories = new List<CategoriesView>();
                l_Categories = InitView(cmd);
                if (l_Categories.Count > 0)
                {
                    RetVal = (CategoriesView)l_Categories[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public CategoriesView GetView(short CategoryId, string CategoryUrl, byte LanguageId, byte ApplicationTypeId)
        {
            CategoriesView RetVal = new CategoriesView();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetByUrl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@CategoryUrl", CategoryUrl));
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //Lay category chi tiet
                RetVal = InitViewHelpers.InitCateOne(smartReader, null);

                //Lay category toi root
                reader.NextResult();
                RetVal.lCategoriesViewToRoot = InitViewHelpers.InitCate(smartReader, null);
                reader.Close();
                //List<CategoriesView> l_Categories = new List<CategoriesView>();
                //l_Categories = InitView(cmd);
                //if (l_Categories.Count > 0)
                //{
                //    RetVal = (CategoriesView)l_Categories[0];
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Categories Get()
        {
            return Get(this.CategoryId, this.LanguageId, this.ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Categories Static_Get(string Constr, short CategoryId, byte LanguageId, byte ApplicationTypeId)
        {
            Categories m_Categories = new Categories(Constr);
            return m_Categories.Get(CategoryId, LanguageId, ApplicationTypeId);
        }
        //-------------------------------------------------------------- 
        public static Categories Static_Get(short CategoryId, byte LanguageId, byte ApplicationTypeId)
        {
            return Static_Get("", CategoryId, LanguageId, ApplicationTypeId);
        }
        //--------------------------------------------------------------     
        public List<Categories> GetAllHierachy(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, short CategoryId, byte ReviewStatusId, string PaddingChar)
        {
            List<Categories> RetVal = new List<Categories>();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetAllHierachy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowWeb", this.ShowWeb));
                cmd.Parameters.Add(new SqlParameter("@ShowWap", this.ShowWap));
                cmd.Parameters.Add(new SqlParameter("@ShowApp", this.ShowApp));
                cmd.Parameters.Add(new SqlParameter("@ParentCategoryId", this.ParentCategoryId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@PaddingChar", PaddingChar));                
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        /*--------------------------------------------------------------     
         * GetListByParent: Added by Vu.The
        --------------------------------------------------------------*/
        public List<Categories> GetListByParent(short SiteId, byte LanguageId, byte ApplicationTypeId, short ParentCategoryId)
        {
            List<Categories> RetVal = new List<Categories>();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetListByParent");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ParentCategoryId", ParentCategoryId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------*/
        public List<CategoriesView> GetListByParentView(short SiteId, byte LanguageId, byte ApplicationTypeId, short ParentCategoryId)
        {
            List<CategoriesView> RetVal = new List<CategoriesView>();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetListByParent");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", SiteId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ParentCategoryId", ParentCategoryId));
                RetVal = InitView(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public List<Categories> GetListByDisplayType(short DisplayTypeId)
        {
            List<Categories> RetVal = new List<Categories>();
            try
            {
                SqlCommand cmd = new SqlCommand("Categories_GetByDisplayType");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", this.ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@DataTypeId", this.DataTypeId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", DisplayTypeId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<Categories> Static_GetAllHierachy(int ActUserId, string OrderBy, byte LanguageId, byte ApplicationTypeId, short CategoryId, byte ReviewStatusId, string PaddingChar)
        {
            Categories m_Categories=new Categories();
            return m_Categories.GetAllHierachy(ActUserId, OrderBy, LanguageId, ApplicationTypeId, CategoryId, ReviewStatusId, PaddingChar);
        }
        //--------------------------------------------------------------     
        public static List<Categories> Static_GetAllHierachy(int ActUserId, string OrderBy, short SiteId, byte DataTypeId, byte LanguageId, byte ApplicationTypeId, short CategoryId, byte ReviewStatusId, string PaddingChar)
        {
            Categories m_Categories = new Categories();
            m_Categories.SiteId = SiteId;
            m_Categories.DataTypeId = DataTypeId;
            return m_Categories.GetAllHierachy(ActUserId, OrderBy, LanguageId, ApplicationTypeId, CategoryId, ReviewStatusId, PaddingChar);
        }
        //--------------------------------------------------------------     
        public static Categories Static_Get(short CategoryId, List<Categories> list)
        {
            Categories RetVal = new Categories();
            RetVal = list.Find(i => i.CategoryId == CategoryId);
            if (RetVal == null) RetVal = new Categories();
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static string Static_GetListDesc(string ListCategoryId, List<Categories> list)
        {
            string RetVal = "";
            List<string> l_CategoryId = ListCategoryId.Split(',').ToList<string>();
            for (int i = 0; i < l_CategoryId.Count; i++)
            {
                Categories m_Categories = Static_Get(short.Parse(l_CategoryId[i].ToString()), list);
                if (m_Categories.CategoryDesc != null && m_Categories.CategoryDesc != "")
                {
                    if (RetVal != "") RetVal += ", ";
                    RetVal += m_Categories.CategoryDesc;
                }
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Categories> GetListCategoryParent( short SiteId)
        {
            List<Categories> RetVal = new List<Categories>();

            try
            {
                string sql = "SELECT CategoryId, CategoryDesc FROM Categories WHERE CategoryId IN (SELECT ParentCategoryId AS CategoryId FROM Categories where SiteId = "+SiteId+")";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        public static List<Categories> Static_GetListCategoryParent(short SiteId)
        {
            Categories m_Categories = new Categories();
            try
            {
                return m_Categories.GetListCategoryParent(SiteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    } 
}