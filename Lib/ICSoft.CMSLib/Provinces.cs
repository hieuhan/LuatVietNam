
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class Provinces
    {   
	    private short _ProvinceId;
	    private string _ProvinceName;
	    private string _ProvinceDesc;
	    private short _CountryId;
	    private short _DisplayOrder;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private int _SoLuong;

        public int CustomerProvinceId
        {
            get { return _customerProvinceId; }
            set { _customerProvinceId = value; }
        }

        private DBAccess db;

        private int _customerProvinceId;

        //-----------------------------------------------------------------
		public Provinces()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Provinces(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Provinces()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short ProvinceId
        {
		    get { return _ProvinceId; }
		    set { _ProvinceId = value; }
	    }
        //-----------------------------------------------------------------
        public string ProvinceName
		{
            get { return _ProvinceName; }
		    set { _ProvinceName = value; }
		}    
        //-----------------------------------------------------------------
        public string ProvinceDesc
		{
            get { return _ProvinceDesc; }
		    set { _ProvinceDesc = value; }
		}    
        //-----------------------------------------------------------------
        public short CountryId
		{
            get { return _CountryId; }
		    set { _CountryId = value; }
		}    
        //-----------------------------------------------------------------
        public short DisplayOrder
		{
            get { return _DisplayOrder; }
		    set { _DisplayOrder = value; }
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
        public int SoLuong
        {
            get { return _SoLuong; }
            set { _SoLuong = value; }
        }
        //-----------------------------------------------------------------
    
        private List<Provinces> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Provinces> l_Provinces = new List<Provinces>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Provinces m_Provinces = new Provinces(db.ConnectionString);
                    m_Provinces.ProvinceId = smartReader.GetInt16("ProvinceId");
                    m_Provinces.ProvinceName = smartReader.GetString("ProvinceName");
                    m_Provinces.ProvinceDesc = smartReader.GetString("ProvinceDesc");
                    m_Provinces.CountryId = smartReader.GetInt16("CountryId");
                    m_Provinces.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_Provinces.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Provinces.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Provinces.Add(m_Provinces);
                }
                reader.Close();
                return l_Provinces;
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
                SqlCommand cmd = new SqlCommand("Provinces_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceName", this.ProvinceName));
                cmd.Parameters.Add(new SqlParameter("@ProvinceDesc", this.ProvinceDesc));
                cmd.Parameters.Add(new SqlParameter("@CountryId", this.CountryId));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.ProvinceId =Convert.ToInt16((cmd.Parameters["@ProvinceId"].Value == null) ? 0 : (cmd.Parameters["@ProvinceId"].Value));
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
        public byte Delete(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Provinces_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId",this.ProvinceId));
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
        public List<Provinces> GetList(short CountryId)
        {
            List<Provinces> RetVal = new List<Provinces>();
            try
            {
                string sql = "SELECT * FROM Provinces" + (CountryId == 0 ? "" : " WHERE CountryId=" + CountryId.ToString()) + " ORDER BY DisplayOrder, ProvinceName";
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
        public static List<Provinces> Static_GetList(short CountryId)
        {
            List<Provinces> RetVal = new List<Provinces>();
            try
            {
                Provinces m_Provinces = new Provinces();
                RetVal = m_Provinces.GetList(CountryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<Provinces> GetListByProvinceId(short ProvinceId)
        {
            List<Provinces> RetVal = new List<Provinces>();
            try
            {
                if (ProvinceId > 0)
                {
                    string sql = "SELECT * FROM Provinces WHERE (ProvinceId=" + ProvinceId.ToString() + ")";
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
        public static Provinces Static_Get(short ProvinceId, List<Provinces> list)
        {
            Provinces RetVal = new Provinces();
            RetVal = list.Find(i => i.ProvinceId == ProvinceId);
            if (RetVal == null) RetVal = new Provinces();
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Provinces Get(short ProvinceId)
        {
            Provinces RetVal = new Provinces(db.ConnectionString);
            try
            {
                List<Provinces> list = GetListByProvinceId(ProvinceId);
                if (list.Count > 0)
                {
                    RetVal = (Provinces)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Provinces Get()
        {
            return Get(this.ProvinceId);
        }
        //-------------------------------------------------------------- 
        public static Provinces Static_Get(short ProvinceId)
        {
            return new Provinces().Get(ProvinceId);
        }
        //--------------------------------------------------------------     
        public List<Provinces> Provinces_GetList(int ActUserId, string OrderBy, string ProvinceName, short CountryId)
        {
            List<Provinces> RetVal = new List<Provinces>();
            try
            {
                SqlCommand cmd = new SqlCommand("Provinces_GetList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", StringUtil.InjectionString(OrderBy)));
                cmd.Parameters.Add(new SqlParameter("@ProvinceName", StringUtil.InjectionString(ProvinceName)));
                cmd.Parameters.Add(new SqlParameter("@CountryId", CountryId));
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DataSet Provinces_GetNameByJson(string Name, int RowAmount, ref string Result)
        {
            DataSet RetVal = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Provinces_GetNameByJson");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                RetVal = db.getDataSet(cmd);
                Result = Convert.ToString(cmd.Parameters["@Result"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public string GetUrl(string name = "van-ban-uy-ban-nhan-dan")
        {
            string retVal = string.Empty;
            if (!string.IsNullOrEmpty(this.ProvinceDesc) && this.ProvinceId > 0)
            {
                retVal = StringUtil.RemoveSignature(this.ProvinceDesc);
                retVal = Regex.Replace(retVal, "(?:[^a-z0-9 _-]|(?<=['\"])s)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                retVal = retVal.Replace(" ", "-");
                retVal = new Regex(@"-{2,}", RegexOptions.Compiled).Replace(retVal, "-");
                retVal = retVal.ToLower();
                retVal = retVal.Trim('-');
                retVal = string.Concat(name, "-", retVal, "-", this.ProvinceId, ".html");
                if (!retVal.StartsWith(CmsConstants.ROOT_PATH))
                {
                    retVal = string.Concat(CmsConstants.ROOT_PATH , retVal);
                }
            }
            return retVal;
        }

    } 
}