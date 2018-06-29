using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using ICSoft.CMSLib;
using sms.database;
namespace ICSoft.LawDocsLib
{
    public class DocProvinces
    {   
    #region Private Properties
	    private int _DocProvinceId;
	    private int _DocId;
	    private short _ProvinceId;
	    private int _CrUserId;
	    private DateTime _CrDatetime;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public DocProvinces()
        {
            db = new DBAccess(DocConstants.DOC_CONSTR);
		}
        //-----------------------------------------------------------------        
        public DocProvinces(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~DocProvinces()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int DocProvinceId
        {
		    get
                {
			        return _DocProvinceId;
		        }
		    set
            {
			    _DocProvinceId = value;
		    }
	    }
    
        public int DocId
		{
            get
            {
			    return _DocId;
		    }
		    set
            {
			    _DocId = value;
            }
		}    
        public short ProvinceId
		{
            get
            {
			    return _ProvinceId;
		    }
		    set
            {
			    _ProvinceId = value;
            }
		}    
        public int CrUserId
		{
            get
            {
			    return _CrUserId;
		    }
		    set
            {
			    _CrUserId = value;
            }
		}    
        public DateTime CrDatetime
		{
            get
            {
			    return _CrDatetime;
		    }
		    set
            {
			    _CrDatetime = value;
            }
		}    
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<DocProvinces> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<DocProvinces> l_DocProvinces = new List<DocProvinces>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    DocProvinces m_DocProvinces = new DocProvinces();
                    m_DocProvinces.DocProvinceId = smartReader.GetInt32("DocProvinceId");
                    m_DocProvinces.DocId = smartReader.GetInt32("DocId");
                    m_DocProvinces.ProvinceId = smartReader.GetInt16("ProvinceId");
                    m_DocProvinces.CrUserId = smartReader.GetInt32("CrUserId");
                    m_DocProvinces.CrDatetime = smartReader.GetDateTime("CrDatetime");
                    l_DocProvinces.Add(m_DocProvinces);
                }
                reader.Close();
                return l_DocProvinces;
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
        public byte Insert(byte Replicated, int ActUserId, ref int SysMessageId)
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
        public byte DeleteByDocId(int DocId)
        {
            Byte RetVal = 0;
            try
            {
                string sql = "DELETE FROM DocProvinces WHERE (DocId=" + DocId.ToString() + ")";
                db.ExecuteSQL(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertByProvinceName(byte Replicated, int ActUserId, string ProvinceName)
        {
            byte RetVal = 0;
            int SysMessageId = 0;
            try
            {
                this.DeleteByDocId(this.DocId);
                short CountryId = 1;
                List<Provinces> l_Provinces = Provinces.Static_GetList(CountryId);
                foreach(Provinces m_Provices in l_Provinces)
                {
                    if (ProvinceName.Contains(m_Provices.ProvinceDesc))
                    {
                        this.ProvinceId = m_Provices.ProvinceId;
                        Insert(Replicated, ActUserId, ref SysMessageId);
                    }
                }
                RetVal = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte Update(byte Replicated, int ActUserId, ref int SysMessageId)
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
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocProvinces_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@CrDatetime", this.CrDatetime));
                cmd.Parameters.Add(new SqlParameter("@DocProvinceId", this.DocProvinceId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.DocProvinceId = int.Parse(cmd.Parameters["@DocProvinceId"].Value.ToString());
                SysMessageId = Convert.ToInt32((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("DocProvinces_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocProvinceId",this.DocProvinceId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = int.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                RetVal = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public DocProvinces Get()
        {
            DocProvinces retVal = new DocProvinces();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<DocProvinces> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<DocProvinces> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DocProvinces_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@DocProvinceId", this.DocProvinceId));
                cmd.Parameters.Add(new SqlParameter("@DocId", this.DocId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", this.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<DocProvinces> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //-------------------------------------------------------------- 
        public static string Static_GetProviceDesc(int DocId)
        {
            string RetVal = "";
            if (DocId <= 0)
            {
                return RetVal;
            }
            try
            {
                DocProvinces m_DocProvinces = new DocProvinces();
                string DateFrom = "";
                string DateTo = "";
                string OrderBy = "";
                int RowCount = 0;
                int PageSize = 100;
                int PageNum = 0;
                m_DocProvinces.DocId = DocId;
                List<DocProvinces> l_DocProvinces = m_DocProvinces.GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNum, ref RowCount);
                short CountryId = 1;
                List<Provinces> l_Provinces = Provinces.Static_GetList(CountryId);
                for(int index=0; index < l_DocProvinces.Count; index++)
                {
                    m_DocProvinces = l_DocProvinces[index];
                    for(int indexProvice=0; indexProvice < l_Provinces.Count; indexProvice++)
                    {
                        Provinces m_Provinces = l_Provinces[indexProvice];
                        if (m_DocProvinces.ProvinceId == m_Provinces.ProvinceId)
                        {
                            if (String.IsNullOrEmpty(RetVal))
                            {
                                RetVal = m_Provinces.ProvinceDesc;
                            }
                            else
                            {
                                RetVal += "; " + m_Provinces.ProvinceDesc;
                            }
                            break;
                        }
                    }

                }
                return RetVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }  
            
            
        }

        #endregion
    }
}

