using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class RequestStatus
    {
        private byte _RequestStatusId;
        private string _RequestStatusName;
        private string _RequestStatusDesc;
        private DBAccess db;
        //-----------------------------------------------------------------
        public RequestStatus()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public RequestStatus(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~RequestStatus()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte RequestStatusId
        {
            get { return _RequestStatusId; }
            set { _RequestStatusId = value; }
        }
        //-----------------------------------------------------------------
        public string RequestStatusName
        {
            get { return _RequestStatusName; }
            set { _RequestStatusName = value; }
        }
        //-----------------------------------------------------------------
        public string RequestStatusDesc
        {
            get { return _RequestStatusDesc; }
            set { _RequestStatusDesc = value; }
        }
        //-----------------------------------------------------------------

        private List<RequestStatus> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<RequestStatus> l_RequestStatus = new List<RequestStatus>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    RequestStatus m_RequestStatus =
                        new RequestStatus(db.ConnectionString)
                        {
                            RequestStatusId = smartReader.GetByte("RequestStatusId"),
                            RequestStatusName = smartReader.GetString("RequestStatusName"),
                            RequestStatusDesc = smartReader.GetString("RequestStatusDesc")
                        };

                    l_RequestStatus.Add(m_RequestStatus);
                }
                reader.Close();
                return l_RequestStatus;
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
        public List<RequestStatus> GetAll()
        {
            List<RequestStatus> result;
            try
            {
                string cmdText = "SELECT * FROM RequestStatus";
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
        public static List<RequestStatus> Static_GetList(string ConStr)
        {
            List<RequestStatus> RetVal = new List<RequestStatus>();
            try
            {
                RequestStatus m_RequestStatus = new RequestStatus(ConStr);
                RetVal = m_RequestStatus.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<RequestStatus> Static_GetList()
        {
            return Static_GetList("");
        }

        public RequestStatus Get(byte id)
        {
            RequestStatus result;
            try
            {
                string cmdText = "SELECT * FROM RequestStatus WHERE (RequestStatusId=" + id + ")";
                List<RequestStatus> list = this.Init(new SqlCommand(cmdText)
                {
                    CommandType = CommandType.Text
                });
                result = list.Count == 1 ? list[0] : new RequestStatus();
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
                RequestStatus requestStatus = new RequestStatus();
                requestStatus = requestStatus.Get(Id);
                if (requestStatus.RequestStatusDesc != null)
                {
                    result = ((requestStatus.RequestStatusId == 1) ? ("<font color=blue>" + requestStatus.RequestStatusDesc.Trim() + "</font>") : requestStatus.RequestStatusDesc.Trim());
                }
            }
            catch
            {
            }
            return result;
        }

        public static RequestStatus Static_Get(int requestStatusId, List<RequestStatus> listRequestStatus)
        {
            var retVal = listRequestStatus.Find(x => x.RequestStatusId == requestStatusId) ?? new RequestStatus();
            return retVal;
        }
        //--------------------------------------------------------------
    }
}
