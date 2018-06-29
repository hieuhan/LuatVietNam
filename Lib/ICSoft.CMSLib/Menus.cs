
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
    public class Menus
    {   
	    private short _MenuId;
	    private string _MenuName="";
        private string _MenuDesc = "";
	    private short _SiteId;
	    private short _CategoryId;
	    private byte _ShowTop;
	    private byte _ShowBottom;
	    private byte _ShowLeft;
	    private byte _ShowRight;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        //-----------------------------------------------------------------
		public Menus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Menus(string constr)
        {
            db = new DBAccess ((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~Menus()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public short MenuId
        {
		    get { return _MenuId; }
		    set { _MenuId = value; }
	    }
        //-----------------------------------------------------------------
        public string MenuName
		{
            get { return _MenuName; }
		    set { _MenuName = value; }
		}    
        //-----------------------------------------------------------------
        public string MenuDesc
		{
            get { return _MenuDesc; }
		    set { _MenuDesc = value; }
		}    
        //-----------------------------------------------------------------
        public short SiteId
		{
            get { return _SiteId; }
		    set { _SiteId = value; }
		}    
        //-----------------------------------------------------------------
        public short CategoryId
		{
            get { return _CategoryId; }
		    set { _CategoryId = value; }
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
        public byte ShowLeft
		{
            get { return _ShowLeft; }
		    set { _ShowLeft = value; }
		}    
        //-----------------------------------------------------------------
        public byte ShowRight
		{
            get { return _ShowRight; }
		    set { _ShowRight = value; }
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
    
        private List<Menus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Menus> l_Menus = new List<Menus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Menus m_Menus = new Menus(db.ConnectionString);
                    m_Menus.MenuId = smartReader.GetInt16("MenuId");
                    m_Menus.MenuName = smartReader.GetString("MenuName");
                    m_Menus.MenuDesc = smartReader.GetString("MenuDesc");
                    m_Menus.SiteId = smartReader.GetInt16("SiteId");
                    m_Menus.CategoryId = smartReader.GetInt16("CategoryId");
                    m_Menus.ShowTop = smartReader.GetByte("ShowTop");
                    m_Menus.ShowBottom = smartReader.GetByte("ShowBottom");
                    m_Menus.ShowLeft = smartReader.GetByte("ShowLeft");
                    m_Menus.ShowRight = smartReader.GetByte("ShowRight");
                    m_Menus.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Menus.CrDateTime = smartReader.GetDateTime("CrDateTime");
         
                    l_Menus.Add(m_Menus);
                }
                reader.Close();
                return l_Menus;
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
                SqlCommand cmd = new SqlCommand("Menus_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MenuName", this.MenuName));
                cmd.Parameters.Add(new SqlParameter("@MenuDesc", this.MenuDesc));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowLeft", this.ShowLeft));
                cmd.Parameters.Add(new SqlParameter("@ShowRight", this.ShowRight));
                cmd.Parameters.Add("@MenuId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MenuId = (cmd.Parameters["@MenuId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@MenuId"].Value);
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
        public byte Update(byte Replicated, int ActUserId, ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Menus_Update");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MenuName", this.MenuName));
                cmd.Parameters.Add(new SqlParameter("@MenuDesc", this.MenuDesc));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowLeft", this.ShowLeft));
                cmd.Parameters.Add(new SqlParameter("@ShowRight", this.ShowRight));
                cmd.Parameters.Add(new SqlParameter("@MenuId",this.MenuId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = Convert.ToInt16((cmd.Parameters["@SysMessageId"].Value==null) ? "0": cmd.Parameters["@SysMessageId"].Value);
                RetVal =Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value==null)? "0":cmd.Parameters["@SysMessageTypeId"].Value);
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
                SqlCommand cmd = new SqlCommand("Menus_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MenuName", this.MenuName));
                cmd.Parameters.Add(new SqlParameter("@MenuDesc", this.MenuDesc));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowLeft", this.ShowLeft));
                cmd.Parameters.Add(new SqlParameter("@ShowRight", this.ShowRight));
                cmd.Parameters.Add(new SqlParameter("@MenuId", this.MenuId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MenuId = (cmd.Parameters["@MenuId"].Value == null) ? (short)0 : Convert.ToInt16(cmd.Parameters["@MenuId"].Value);
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
                SqlCommand cmd = new SqlCommand("Menus_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MenuId",this.MenuId));
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
        public List<Menus> GetListBySiteId(short SiteId)
        {
            List<Menus> RetVal = new List<Menus>();
            try
            {
                string sql = "SELECT * FROM Menus WHERE SiteId=" + SiteId.ToString();
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
        public List<Menus> GetListByMenuId(short MenuId)
        {
            List<Menus> RetVal = new List<Menus>();
            try
            {
                if (MenuId > 0)
                {
                    string sql = "SELECT * FROM Menus WHERE (MenuId=" + MenuId.ToString() + ")";
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
        public Menus Get(short MenuId)
        {
            Menus RetVal = new Menus();
            try
            {
                List<Menus> list = GetListByMenuId(MenuId);
                if (list.Count > 0)
                {
                    RetVal = (Menus)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static Menus Static_Get(short MenuId, List<Menus> list)
        {
            Menus RetVal = new Menus();
            try
            {
                RetVal = list.Find(i => i.MenuId == MenuId);
                if (RetVal == null) RetVal = new Menus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public Menus Get()
        {
            return Get(this.MenuId);
        }
        //-------------------------------------------------------------- 
        public static Menus Static_Get(string Constr, short MenuId)
        {
            Menus m_Menus = new Menus(Constr);
            return m_Menus.Get(MenuId);
        }
        //-------------------------------------------------------------- 
        public static Menus Static_Get(short MenuId)
        {
            return Static_Get("",MenuId);
        }
        //-------------------------------------------------------------- 
        public static List<Menus> Static_GetListBySiteId(short SiteId)
        {
            return new Menus().GetListBySiteId(SiteId);
        }
        //--------------------------------------------------------------     
        public List<Menus> GetPage(int ActUserId, int RowAmount, int PageIndex, string OrderBy, string DateFrom, string DateTo, ref int RowCount)
        {
            List<Menus> RetVal = new List<Menus>();
            try
            {
                SqlCommand cmd = new SqlCommand("Menus_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@MenuName", this.MenuName));
                cmd.Parameters.Add(new SqlParameter("@MenuDesc", this.MenuDesc));
                cmd.Parameters.Add(new SqlParameter("@SiteId", this.SiteId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@ShowTop", this.ShowTop));
                cmd.Parameters.Add(new SqlParameter("@ShowBottom", this.ShowBottom));
                cmd.Parameters.Add(new SqlParameter("@ShowLeft", this.ShowLeft));
                cmd.Parameters.Add(new SqlParameter("@ShowRight", this.ShowRight));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                if (DateFrom!="") cmd.Parameters.Add(new SqlParameter("@DateFrom", DateTime.Parse(DateFrom, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
                if (DateTo!="") cmd.Parameters.Add(new SqlParameter("@DateTo", DateTime.Parse(DateTo, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))));
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
        
    } 
}

