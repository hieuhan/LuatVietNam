using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using sms.database;

namespace ICSoft.CMSLib
{
    public class RequestTypes
    {
        private byte _RequestTypeId;
        private string _RequestTypeName;
        private string _RequestTypeDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public RequestTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public RequestTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~RequestTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte RequestTypeId
        {
            get { return _RequestTypeId; }
            set { _RequestTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string RequestTypeName
        {
            get { return _RequestTypeName; }
            set { _RequestTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string RequestTypeDesc
        {
            get { return _RequestTypeDesc; }
            set { _RequestTypeDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<RequestTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<RequestTypes> l_RequestTypes = new List<RequestTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RequestTypes m_RequestType =
                        new RequestTypes(db.ConnectionString)
                        {
                            RequestTypeId = smartReader.GetByte("RequestTypeId"),
                            RequestTypeName = smartReader.GetString("RequestTypeName"),
                            RequestTypeDesc = smartReader.GetString("RequestTypeDesc")
                        };

                    l_RequestTypes.Add(m_RequestType);
                }
                reader.Close();
                return l_RequestTypes;
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
        public List<RequestTypes> GetAll()
        {
            List<RequestTypes> result;
            try
            {
                string cmdText = "SELECT * FROM RequestTypes";
                SqlCommand cmd = new SqlCommand(cmdText);
                result = this.Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        //--------------------------------------------------------------
        public static List<RequestTypes> Static_GetList(string ConStr)
        {
            List<RequestTypes> RetVal = new List<RequestTypes>();
            try
            {
                RequestTypes m_RequestTypes = new RequestTypes(ConStr);
                RetVal = m_RequestTypes.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<RequestTypes> Static_GetList()
        {
            return Static_GetList("");
        }
        //--------------------------------------------------------------     
        public RequestTypes Get(byte id)
        {
            RequestTypes result;
            try
            {
                string cmdText = "SELECT * FROM RequestTypes WHERE (RequestTypeId=" + id + ")";
                List<RequestTypes> list = this.Init(new SqlCommand(cmdText)
                {
                    CommandType = CommandType.Text
                });
                result = list.Count == 1 ? list[0] : new RequestTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static string Static_GetDesc(byte Id)
        {
            string result = "";
            try
            {
                RequestTypes requestType = new RequestTypes();
                requestType = requestType.Get(Id);
                if (requestType.RequestTypeDesc != null)
                {
                    result = ((requestType.RequestTypeId == 1) ? ("<font color=blue>" + requestType.RequestTypeDesc.Trim() + "</font>") : requestType.RequestTypeDesc.Trim());
                }
            }
            catch
            {
            }
            return result;
        }

        public static RequestTypes Static_Get(int requestTypeId, List<RequestTypes> listRequestTypes)
        {
            var retVal = listRequestTypes.Find(x => x.RequestTypeId == requestTypeId) ?? new RequestTypes();
            return retVal;
        }
        //--------------------------------------------------------------
    }
}
