
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
    public class MenuItems
    {
        private int _MenuItemId;
        private int _MenuId;
        private string _ItemName;
        private string _ItemDesc;
        private string _IconPath;
        private string _Url;
        private int _ParentItemId;
        private byte _ItemLevel;
        private int _DisplayOrder;
        private byte _ReviewStatusId;
        private int _CrUserId;
        private DateTime _CrDateTime;
        private string _MetaTitle;
        private string _MetaDesc;
        private string _MetaKeyword;
        private string _CanonicalTag;
        private string _H1Tag;
        private string _SeoFooter;
        private DBAccess db;
        //-----------------------------------------------------------------
        public MenuItems()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public MenuItems(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~MenuItems()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int MenuItemId
        {
            get { return _MenuItemId; }
            set { _MenuItemId = value; }
        }
        //-----------------------------------------------------------------
        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }
        //-----------------------------------------------------------------
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
        //-----------------------------------------------------------------
        public string ItemDesc
        {
            get { return _ItemDesc; }
            set { _ItemDesc = value; }
        }
        //-----------------------------------------------------------------
        public string IconPath
        {
            get { return _IconPath; }
            set { _IconPath = value; }
        }
        //-----------------------------------------------------------------
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        //-----------------------------------------------------------------
        public int ParentItemId
        {
            get { return _ParentItemId; }
            set { _ParentItemId = value; }
        }
        //-----------------------------------------------------------------
        public byte ItemLevel
        {
            get { return _ItemLevel; }
            set { _ItemLevel = value; }
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

        private List<MenuItems> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MenuItems> l_MenuItems = new List<MenuItems>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MenuItems m_MenuItems = new MenuItems(db.ConnectionString);
                    m_MenuItems.MenuItemId = smartReader.GetInt32("MenuItemId");
                    m_MenuItems.MenuId = smartReader.GetInt32("MenuId");
                    m_MenuItems.ItemName = smartReader.GetString("ItemName");
                    m_MenuItems.ItemDesc = smartReader.GetString("ItemDesc");
                    m_MenuItems.IconPath = smartReader.GetString("IconPath");
                    m_MenuItems.Url = smartReader.GetString("Url");
                    m_MenuItems.ParentItemId = smartReader.GetInt32("ParentItemId");
                    m_MenuItems.ItemLevel = smartReader.GetByte("ItemLevel");
                    m_MenuItems.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_MenuItems.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_MenuItems.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MenuItems.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_MenuItems.MetaTitle = smartReader.GetString("MetaTitle");
                    m_MenuItems.MetaDesc = smartReader.GetString("MetaDesc");
                    m_MenuItems.MetaKeyword = smartReader.GetString("MetaKeyword");
                    m_MenuItems.CanonicalTag = smartReader.GetString("CanonicalTag");
                    m_MenuItems.H1Tag = smartReader.GetString("H1Tag");
                    m_MenuItems.SeoFooter = smartReader.GetString("SeoFooter");

                    l_MenuItems.Add(m_MenuItems);
                }
                reader.Close();
                return l_MenuItems;
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
                SqlCommand cmd = new SqlCommand("MenuItems_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MenuId", this.MenuId));
                cmd.Parameters.Add(new SqlParameter("@ItemName", this.ItemName));
                cmd.Parameters.Add(new SqlParameter("@ItemDesc", this.ItemDesc));
                cmd.Parameters.Add(new SqlParameter("@IconPath", this.IconPath));
                cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
                cmd.Parameters.Add(new SqlParameter("@ParentItemId", this.ParentItemId));
                cmd.Parameters.Add(new SqlParameter("@ItemLevel", this.ItemLevel));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@MetaTitle", this.MetaTitle));
                cmd.Parameters.Add(new SqlParameter("@MetaDesc", this.MetaDesc));
                cmd.Parameters.Add(new SqlParameter("@MetaKeyword", this.MetaKeyword));
                cmd.Parameters.Add(new SqlParameter("@CanonicalTag", this.CanonicalTag));
                cmd.Parameters.Add(new SqlParameter("@H1Tag", this.H1Tag));
                cmd.Parameters.Add(new SqlParameter("@SeoFooter", this.SeoFooter));
                cmd.Parameters.Add(new SqlParameter("@MenuItemId", this.MenuItemId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MenuItemId = Convert.ToInt32((cmd.Parameters["@MenuItemId"].Value == null) ? 0 : (cmd.Parameters["@MenuItemId"].Value));
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
                SqlCommand cmd = new SqlCommand("MenuItems_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MenuItemId", this.MenuItemId));
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
        public void Delete_Q_ParentId(int ParentId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MenuItems_Delete_Q_ParentId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ParentId", ParentId));               
                db.ExecuteSQL(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        //--------------------------------------------------------------            
        public byte UpdateReviewStatusId(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("MenuItems_UpdateReviewStatusId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MenuItemId", this.MenuItemId));
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
        public List<MenuItems> GetList()
        {
            List<MenuItems> RetVal = new List<MenuItems>();
            try
            {
                string sql = "SELECT * FROM MenuItems";
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
        public List<MenuItems> GetList(string TextOptionAll = "...")
        {
            List<MenuItems> RetVal = GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                MenuItems m_MenuItems = new MenuItems();
                m_MenuItems.ItemName = TextOptionAll;
                m_MenuItems.ItemDesc = TextOptionAll;
                RetVal.Insert(0, m_MenuItems);
            }
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<MenuItems> GetListOrderBy(string OrderBy)
        {
            List<MenuItems> RetVal = new List<MenuItems>();
            try
            {
                string sql = "SELECT * FROM MenuItems ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<MenuItems> GetListOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            List<MenuItems> RetVal = GetListOrderBy(StringUtil.InjectionString(OrderBy));
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                MenuItems m_MenuItems = new MenuItems();
                m_MenuItems.ItemName = TextOptionAll;
                m_MenuItems.ItemDesc = TextOptionAll;
                RetVal.Insert(0, m_MenuItems);
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<MenuItems> GetListByMenuItemId(int MenuItemId)
        {
            List<MenuItems> RetVal = new List<MenuItems>();
            try
            {
                if (MenuItemId > 0)
                {
                    string sql = "SELECT * FROM MenuItems WHERE (MenuItemId=" + MenuItemId.ToString() + ")";
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
        public MenuItems Get(int MenuItemId)
        {
            MenuItems RetVal = new MenuItems(db.ConnectionString);
            try
            {
                List<MenuItems> list = GetListByMenuItemId(MenuItemId);
                if (list.Count > 0)
                {
                    RetVal = (MenuItems)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public MenuItems Get()
        {
            return Get(this.MenuItemId);
        }
        //-------------------------------------------------------------- 
        public static MenuItems Static_Get(string Constr, int MenuItemId)
        {
            MenuItems m_MenuItems = new MenuItems(Constr);
            return m_MenuItems.Get(MenuItemId);
        }
        //-------------------------------------------------------------- 
        public static MenuItems Static_Get(int MenuItemId)
        {
            return Static_Get("", MenuItemId);
        }
        //-------------------------------------------------------------- 
        public static List<MenuItems> Static_GetList(string ConStr)
        {
            List<MenuItems> RetVal = new List<MenuItems>();
            try
            {
                MenuItems m_MenuItems = new MenuItems(ConStr);
                RetVal = m_MenuItems.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<MenuItems> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
        public List<MenuItems> GetAllHierachy(string OrderBy, string PaddingChar)
        {
            List<MenuItems> RetVal = new List<MenuItems>();
            try
            {
                SqlCommand cmd = new SqlCommand("MenuItems_GetAllHierachy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@MenuId", this.MenuId));
                cmd.Parameters.Add(new SqlParameter("@ParentItemId", this.ParentItemId));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@PaddingChar", PaddingChar));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl()
        {
            string RetVal = _IconPath;
            if (string.IsNullOrEmpty(_IconPath))
            {
                RetVal = CmsConstants.NO_IMAGE_URL;
            }
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl_Original()
        {
            string RetVal = _IconPath;
            if (string.IsNullOrEmpty(_IconPath))
            {
                RetVal = CmsConstants.NO_IMAGE_URL;
            }
            if (!string.IsNullOrEmpty(RetVal) && !RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + "medias/original/" + RetVal;
            return RetVal;
        }
        //--------------------------------------------------------------  
        public string GetImageUrlWithHtmlTag(int width, int height)
        {
            string result = "";
            string imageUrl = GetImageUrl_Original();
            if (!string.IsNullOrEmpty(imageUrl))
            {
                result = "<a class=\"popup\" href=\"" + imageUrl + "\"><img style=\"width:" + width.ToString() + "px;height:" + height.ToString() + "px\" src=\"" + imageUrl + "\" /></a>";
            }
            return result;
        }
        //--------------------------------------------------------------   
    }
}