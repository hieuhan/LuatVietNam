using ICSoft.CMSLib;
using sms.database;
using sms.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.LawDocsLib
{
    public class Lawers
    {
        private int _LawerID;
        private string _FullName;
        private string _ImagePath;
        private string _Address;
        private int _ProviceId;
        private int _DistricId;
        private int _WardId;
        private string _PhoneNumber;
        private string _Mobile;
        private string _Email;
        private string _Website;
        private string _LawOfficeName;
        private string _Experience;
        private string _Education;
        private byte _ReviewStatusId;
        private byte _LawerGroupId;
        private short _FieldId;
        private string _Content;
        private short _CrUserId;
        private DateTime _CrDateTime;
        private string _LawerUrl;
        private string _MetaTitle;
        private string _MetaDesc;
        private string _MetaKeyword;
        private DBAccess db;

        private string _fieldsName;

        //-----------------------------------------------------------------
        public Lawers()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
        }
        //-----------------------------------------------------------------        
        public Lawers(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Lawers()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int LawerID
        {
            get { return _LawerID; }
            set { _LawerID = value; }
        }
        //-----------------------------------------------------------------
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        //-----------------------------------------------------------------
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        //-----------------------------------------------------------------
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        //-----------------------------------------------------------------
        public int ProviceId
        {
            get { return _ProviceId; }
            set { _ProviceId = value; }
        }
        //-----------------------------------------------------------------
        public int DistricId
        {
            get { return _DistricId; }
            set { _DistricId = value; }
        }
        //-----------------------------------------------------------------
        public int WardId
        {
            get { return _WardId; }
            set { _WardId = value; }
        }
        //-----------------------------------------------------------------
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        //-----------------------------------------------------------------
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        //-----------------------------------------------------------------
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //-----------------------------------------------------------------
        public string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }
        //-----------------------------------------------------------------
        public string LawOfficeName
        {
            get { return _LawOfficeName; }
            set { _LawOfficeName = value; }
        }
        //-----------------------------------------------------------------
        public string Experience
        {
            get { return _Experience; }
            set { _Experience = value; }
        }
        //-----------------------------------------------------------------
        public string Education
        {
            get { return _Education; }
            set { _Education = value; }
        }
        //-----------------------------------------------------------------
        public byte ReviewStatusId
        {
            get { return _ReviewStatusId; }
            set { _ReviewStatusId = value; }
        }
        //-----------------------------------------------------------------
        public byte LawerGroupId
        {
            get { return _LawerGroupId; }
            set { _LawerGroupId = value; }
        }
        //-----------------------------------------------------------------
        public short FieldId
        {
            get { return _FieldId; }
            set { _FieldId = value; }
        }
        //-----------------------------------------------------------------
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
        //-----------------------------------------------------------------
        public short CrUserId
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
        public string MetaTitle
        {
            get{ return _MetaTitle;}
            set{ _MetaTitle = value; }
        }

        public string MetaDesc
        {
            get{ return _MetaDesc;}
            set{ _MetaDesc = value;}
        }

        public string MetaKeyword
        {
            get{ return _MetaKeyword;}
            set{ _MetaKeyword = value; }
        }

        public string LawerUrl
        {
            get{ return _LawerUrl;}
            set { _LawerUrl = value; }
        }

        public string FieldsName
        {
            get { return _fieldsName; }
            set { _fieldsName = value; }
        }

        //-----------------------------------------------------------------

        private List<Lawers> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Lawers> l_Lawers = new List<Lawers>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Lawers m_Lawers = new Lawers(db.ConnectionString);
                    m_Lawers.LawerID = smartReader.GetInt32("LawerID");
                    m_Lawers.FullName = smartReader.GetString("FullName");
                    m_Lawers.ImagePath = smartReader.GetString("ImagePath");
                    m_Lawers.Address = smartReader.GetString("Address");
                    m_Lawers.ProviceId = smartReader.GetInt16("ProviceId");
                    m_Lawers.DistricId = smartReader.GetInt16("DistricId");
                    m_Lawers.WardId = smartReader.GetInt16("WardId");
                    m_Lawers.PhoneNumber = smartReader.GetString("PhoneNumber");
                    m_Lawers.Mobile = smartReader.GetString("Mobile");
                    m_Lawers.Email = smartReader.GetString("Email");
                    m_Lawers.Website = smartReader.GetString("Website");
                    m_Lawers.LawOfficeName = smartReader.GetString("LawOfficeName");
                    m_Lawers.Experience = smartReader.GetString("Experience");
                    m_Lawers.Education = smartReader.GetString("Education");
                    m_Lawers.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Lawers.LawerGroupId = smartReader.GetByte("LawerGroupId");
                    m_Lawers.FieldId = smartReader.GetInt16("FieldId");
                    m_Lawers.Content = smartReader.GetString("Content");
                    m_Lawers.CrUserId = smartReader.GetByte("CrUserId");
                    m_Lawers.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Lawers.LawerUrl = smartReader.GetString("LawerUrl");
                    m_Lawers.MetaTitle = smartReader.GetString("MetaTitle");
                    m_Lawers.MetaDesc = smartReader.GetString("MetaDesc");
                    m_Lawers.MetaKeyword = smartReader.GetString("MetaKeyword");

                    l_Lawers.Add(m_Lawers);
                }
                reader.Close();
                return l_Lawers;
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
                SqlCommand cmd = new SqlCommand("Lawers_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@ProviceId", this.ProviceId));
                cmd.Parameters.Add(new SqlParameter("@DistricId", this.DistricId));
                cmd.Parameters.Add(new SqlParameter("@WardId", this.WardId));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Website", this.Website));
                cmd.Parameters.Add(new SqlParameter("@LawOfficeName", this.LawOfficeName));
                cmd.Parameters.Add(new SqlParameter("@Experience", this.Experience));
                cmd.Parameters.Add(new SqlParameter("@Education", this.Education));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@LawerGroupId", this.LawerGroupId));
                cmd.Parameters.Add(new SqlParameter("@Content", this.Content));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add("@LawerID", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd); 
                this.LawerID = (cmd.Parameters["@LawerID"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["@LawerID"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("Lawers_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@ProviceId", this.ProviceId));
                cmd.Parameters.Add(new SqlParameter("@DistricId", this.DistricId));
                cmd.Parameters.Add(new SqlParameter("@WardId", this.WardId));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Website", this.Website));
                cmd.Parameters.Add(new SqlParameter("@LawOfficeName", this.LawOfficeName));
                cmd.Parameters.Add(new SqlParameter("@Experience", this.Experience));
                cmd.Parameters.Add(new SqlParameter("@Education", this.Education));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@LawerGroupId", this.LawerGroupId));
                cmd.Parameters.Add(new SqlParameter("@Content", this.Content));
                cmd.Parameters.Add(new SqlParameter("@LawerID", this.LawerID));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public int InsertOrUpdate(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            int RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Lawers_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@ProviceId", this.ProviceId));
                cmd.Parameters.Add(new SqlParameter("@DistricId", this.DistricId));
                cmd.Parameters.Add(new SqlParameter("@WardId", this.WardId));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Website", this.Website));
                cmd.Parameters.Add(new SqlParameter("@LawOfficeName", this.LawOfficeName));
                cmd.Parameters.Add(new SqlParameter("@Experience", this.Experience));
                cmd.Parameters.Add(new SqlParameter("@Education", this.Education));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@LawerGroupId", this.LawerGroupId));
                cmd.Parameters.Add(new SqlParameter("@Content", this.Content));
                cmd.Parameters.Add(new SqlParameter("@LawerID", this.LawerID)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add("@SystemMessageID", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SystemMessageTypeID", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.LawerID = (cmd.Parameters["@LawerID"].Value == DBNull.Value) ? 0 : Convert.ToInt32(cmd.Parameters["@LawerID"].Value);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SystemMessageID"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SystemMessageID"].Value);
                RetVal = Convert.ToInt16((cmd.Parameters["@SystemMessageTypeID"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SystemMessageTypeID"].Value);
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
                SqlCommand cmd = new SqlCommand("Lawers_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@LawerID", this.LawerID));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == DBNull.Value) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<Lawers> GetList()
        {
            List<Lawers> RetVal = new List<Lawers>();
            try
            {
                string sql = "SELECT * FROM Lawers ORDER BY FullName";
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
        public List<Lawers> GetListOrderBy(string OrderBy)
        {
            List<Lawers> RetVal = new List<Lawers>();
            try
            {
                string sql = "SELECT * FROM Lawers ORDER BY " + StringUtil.InjectionString(OrderBy);
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
        public List<Lawers> GetListByLawerID(int LawerID)
        {
            List<Lawers> RetVal = new List<Lawers>();
            try
            {
                if (LawerID > 0)
                {
                    string sql = "SELECT * FROM Lawers Left Join LawerField on Lawers.LawerId = LawerField.LawerId WHERE (Lawers.LawerID=" + LawerID.ToString() + ")";
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
        public Lawers Get(int LawerID)
        {
            Lawers RetVal = new Lawers();
            try
            {
                List<Lawers> list = GetListByLawerID(LawerID);
                if (list.Count > 0)
                {
                    RetVal = (Lawers)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Lawers Static_Get(int LawerID, List<Lawers> list)
        {
            Lawers RetVal = new Lawers();
            try
            {
                RetVal = list.Find(i => i.LawerID == LawerID);
                if (RetVal == null) RetVal = new Lawers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Lawers Get()
        {
            return Get(this.LawerID);
        }
        //-------------------------------------------------------------- 
        public static Lawers Static_Get(int LawerID)
        {
            return new Lawers().Get(LawerID);
        }
        //--------------------------------------------------------------     
        public static List<Lawers> Static_GetList()
        {
            List<Lawers> RetVal = new List<Lawers>();
            try
            {
                Lawers m_Lawers = new Lawers();
                RetVal = m_Lawers.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public List<Lawers> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Lawers> RetVal = new List<Lawers>();
            try
            {
                SqlCommand cmd = new SqlCommand("Lawers_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@PageSize", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@FullName", this.FullName));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@ProviceId", this.ProviceId));
                cmd.Parameters.Add(new SqlParameter("@DistricId", this.DistricId));
                cmd.Parameters.Add(new SqlParameter("@WardId", this.WardId));
                cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.PhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Website", this.Website));
                cmd.Parameters.Add(new SqlParameter("@LawOfficeName", this.LawOfficeName));
                cmd.Parameters.Add(new SqlParameter("@Experience", this.Experience));
                cmd.Parameters.Add(new SqlParameter("@Education", this.Education));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@LawerGroupId", this.LawerGroupId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", this.FieldId));
                //cmd.Parameters.Add(new SqlParameter("@Content", this.Content));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                //cmd.Parameters.Add(new SqlParameter("@CrDateTime", this.CrDateTime));
                if (DateFrom != "") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo != "") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
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
        
        public string GetImageUrl()
        {
            string RetVal = _ImagePath;
            if (string.IsNullOrEmpty(_ImagePath))
            {
                RetVal = CmsConstants.NO_IMAGE_URL;
            }
            else
            {
                if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
                {
                    RetVal = "";
                    string[] l_Path = _ImagePath.Split('/');
                    for (int index = 0; index < l_Path.Length; index++)
                    {

                        if (index < l_Path.Length - 1)
                        {
                            RetVal += l_Path[index] + "/";
                        }
                        else
                        {
                            RetVal += "550x500/" + l_Path[index];
                        }
                    }
                    if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                }
                else
                {
                    if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;
                }

            }

            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetImageUrlOriginal()
        {
            string RetVal = _ImagePath;
            if (string.IsNullOrEmpty(_ImagePath))
            {
                RetVal = CmsConstants.NO_IMAGE_URL;
            }
            else
            {
                if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.WEBSITE_IMAGEDOMAIN + RetVal;

            }

            return RetVal;
        }
       
        //--------------------------------------------------------------  
        public string GetImageUrl_Icon()
        {
            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/97x75_crop/");
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Icon/");
            }
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_IconWithHtmlTag(int width, int height)
        {
            string result = "";
            string imageUrl = GetImageUrl_Icon();
            if (!string.IsNullOrEmpty(imageUrl))
            {
                result = "<a class=\"popup\" href=\"" + GetImageUrl() + "\"><img style=\"width:" + width.ToString() + "px;height:" + height.ToString() + "px\" src=\"" + imageUrl + "\" /></a>";
            }
            return result;
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 97x75
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Thumb()
        {

            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/97x75_crop/");
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Thumb/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 150x100
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Thumb2()
        {

            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/150x100_crop/");
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Thumb/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 200x110_crop
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Thumb3()
        {

            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/200x110_crop/");
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Thumb/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 100x100_crop
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Thumb4()
        {

            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/100x100_crop/");
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Thumb/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 550x500
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Standard()
        {

            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl();
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Standard/");
            }
        }
        //-------------------------------------------------------------- 

        /// <summary>
        /// 362x235
        /// </summary>
        /// <returns></returns>

        public string GetImageUrl_Mobile()
        {

            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/362x235_crop/");
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Mobile/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 300x200
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Mobile2()
        {

            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/300x200_crop/");
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Mobile/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 465x325
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Mobile3()
        {

            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/465x325_crop/");
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Mobile/");
            }
        }
        //--------------------------------------------------------------  
        /// <summary>
        /// 360x200_crop
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl_Mobile4()
        {

            if (_ImagePath.Contains("uploads/medias/"))// old tinnong media
            {
                return GetImageUrl().Replace("/550x500/", "/360x200_crop/");
            }
            else
            {
                return GetImageUrl().Replace("/Original/", "/Mobile/");
            }
        }
        public static string GetLawerUrl(int LawerId, string LawerName)
        {
            string RetVal = sms.utils.StringUtil.RemoveSign4VietnameseString(LawerName);
            RetVal = RetVal.Replace(" ", "-");
            RetVal = RetVal.Replace("/", "-");
            RetVal = RetVal.Replace("?", "");
            RetVal = RetVal.Replace(":", "");
            RetVal = RetVal.Replace(",", "");
            while (RetVal.Contains("--"))
            {
                RetVal = RetVal.Replace("--", "-");
            }
            RetVal = RetVal + "-" + LawerId.ToString() + "-lawer.html";
            if (!RetVal.StartsWith("http://")) RetVal = CmsConstants.ROOT_PATH + RetVal;
            return RetVal;
        }
    } 
}
