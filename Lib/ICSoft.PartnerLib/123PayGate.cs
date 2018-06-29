using ICSoft.CMSLib;
using sms.utils;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Web;
using System.Collections.Generic;

namespace ICSoft.PartnerLib
{
    public class _123Pay
    {
        private string _mTransactionID;
        private string _merchantCode;
        private string _bankCode;
        private string _totalAmount;
        private string _clientIP;
        private string _custName;
        private string _custAddress;
        private string _custGender;/// M,F,U
        private string _custDOB;// dd/MM/yyyy
        private string _custPhone;
        private string _custMail;
        private string _description;
        private string _passcode;
        private string _cancelURL;
        private string _redirectURL;
        private string _errorURL;
        private string _secretKey;
        public string mTransactionID /// set input
        {
            get
            {
                return _mTransactionID;
            }

            set
            {
                _mTransactionID = value;
            }
        }

        public string merchantCode
        {
            get
            {
                return ConstantPartner._123PAY_MERCHANTCODE;
            }

            set
            {
                _merchantCode = value;
            }
        }

        public string bankCode/// set input
        {
            get
            {
                return _bankCode;
            }

            set
            {
                _bankCode = value;
            }
        }

        public string totalAmount/// set input
        {
            get
            {
                return _totalAmount;
            }

            set
            {
                _totalAmount = value;
            }
        }

        public string clientIP/// set input
        {
            get
            {
                return _clientIP;
            }

            set
            {
                _clientIP = value;
            }
        }

        public string custName/// set input
        {
            get
            {
                return _custName;
            }

            set
            {
                _custName = value;
            }
        }

        public string custAddress/// set input
        {
            get
            {
                return _custAddress;
            }

            set
            {
                _custAddress = value;
            }
        }

        public string custGender/// set input
        {
            get
            {
                return _custGender;
            }

            set
            {
                _custGender = value;
            }
        }

        public string custDOB/// set input dd/MM/yyyy
        {
            get
            {
                return _custDOB;
            }

            set
            {
                _custDOB = value;
            }
        }

        public string custPhone/// set input
        {
            get
            {
                return _custPhone;
            }

            set
            {
                _custPhone = value;
            }
        }

        public string custMail/// set input
        {
            get
            {
                return _custMail;
            }

            set
            {
                _custMail = value;
            }
        }

        public string description/// set input
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public string passcode/// set input
        {
            get
            {
                return _passcode;
            }

            set
            {
                _passcode = value;
            }
        }

        public string cancelURL/// set input
        {
            get
            {
                return _cancelURL;
            }

            set
            {
                _cancelURL = value;
            }
        }

        public string redirectURL/// set input
        {
            get
            {
                return _redirectURL;
            }

            set
            {
                _redirectURL = value;
            }
        }

        public string errorURL/// set input
        {
            get
            {
                return _errorURL;
            }

            set
            {
                _errorURL = value;
            }
        }

        //public string secretKey
        //{
        //    get
        //    {
        //        return ConstantPartner._123PAY_KEY;
        //    }

        //    set
        //    {
        //        _secretKey = value;
        //    }
        //}
        public string checksum = "";
        public string addInfo;
        public string[] createOrder(int CustomerId, int OrderId)
        {
            string secretKey = ConstantPartner._123PAY_KEY;
            string[] retVal = new string[] { };
            try
            {
                passcode = ConstantPartner._123PAY_PASSCODE;
                string strToSign = mTransactionID + merchantCode + bankCode + totalAmount + clientIP + custName + custAddress + custGender + custDOB + custPhone + custMail + cancelURL + redirectURL + errorURL + passcode + secretKey;
                LogFiles.WriteLog("strToSign: " + strToSign);
                checksum = CalculateSHA1(strToSign);
                
                string json = new JavaScriptSerializer().Serialize(this);
                //insert to partner trans
                int SysMessageId = 0;
                Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
                m_Partner123PayTrans.clientIP = HttpContext.Current.Request.UserHostAddress;
                m_Partner123PayTrans.CrDateTime = DateTime.Now;
                m_Partner123PayTrans.CustomerId = CustomerId;
                m_Partner123PayTrans.OrderId = OrderId;
                m_Partner123PayTrans.PaymentTransactionId = int.Parse(mTransactionID);
                m_Partner123PayTrans.totalAmount = int.Parse(totalAmount);
                m_Partner123PayTrans.Insert(1, 0, ref SysMessageId);
                //end
                LogFiles.WriteLog("json Request: " + json);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(ConstantPartner._123PAY_PAY_URL);
                request.KeepAlive = false;
                request.ProtocolVersion = System.Net.HttpVersion.Version10;
                request.Method = "POST";


                // turn our request string into a byte stream
                byte[] postBytes = Encoding.ASCII.GetBytes(json);

                // this is important - make sure you specify type this way
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.ContentLength = postBytes.Length;
                //request.CookieContainer = Cookies;
                //request.UserAgent = currentUserAgent;
                Stream requestStream = request.GetRequestStream();

                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                // grab te response and print it out to the console along with the status code
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                string result;
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }
                LogFiles.WriteLog("json Response: " + result);
                retVal = new JavaScriptSerializer().Deserialize<string[]>(result);
                if(retVal.Length > 2)// success
                {
                    string returnCode = retVal[0];
                    string Partner123PayTransactionId = retVal[1];
                    redirectURL = retVal[2];
                    checksum = retVal[3];
                    strToSign = returnCode + Partner123PayTransactionId + redirectURL + secretKey;
                    string ResponseSign = CalculateSHA1(strToSign);
                    if(checksum != ResponseSign)
                    {
                        LogFiles.WriteLog(returnCode + " - checksum:" + checksum + " - ResponseSign:" + ResponseSign);
                        retVal[0] = "6000";
                    }
                   
                    m_Partner123PayTrans.Partner123PayTransactionId = Partner123PayTransactionId;
                    m_Partner123PayTrans.redirectURL = redirectURL;
                    m_Partner123PayTrans.ResponseDateTime = DateTime.Now;
                    m_Partner123PayTrans.ResponseJson = result;
                    m_Partner123PayTrans.returnCode = returnCode;
                    m_Partner123PayTrans.returnDesc = "Thành công";
                    m_Partner123PayTrans.Update(1, 0, ref SysMessageId);
                }
                else
                {
                    string returnCode = retVal[0];
                    string returnDesc = retVal[1];
                    m_Partner123PayTrans.Partner123PayTransactionId = "";
                    m_Partner123PayTrans.redirectURL = "";
                    m_Partner123PayTrans.ResponseDateTime = DateTime.Now;
                    m_Partner123PayTrans.ResponseJson = result;
                    m_Partner123PayTrans.returnCode = returnCode;
                    m_Partner123PayTrans.returnDesc = returnDesc;
                    m_Partner123PayTrans.Update(1, 0, ref SysMessageId);
                }
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog(ex.ToString());
            }
            return retVal;
        }


        public string[] queryOrder()
        {
            string secretKey = ConstantPartner._123PAY_KEY;
            string[] retVal = new string[] { };
            try
            {
                passcode = ConstantPartner._123PAY_PASSCODE;
                string strToSign = mTransactionID + merchantCode + clientIP + passcode + secretKey;
                LogFiles.WriteLog("strToSign queryOrder: " + strToSign);
                checksum = CalculateSHA1(strToSign);
                QueryOrder123PayRequest m_Request = new QueryOrder123PayRequest();
                m_Request.checksum = checksum;
                m_Request.clientIP = clientIP;
                m_Request.merchantCode = merchantCode;
                m_Request.mTransactionID = mTransactionID;
                m_Request.passcode = passcode;
                
                string json = new JavaScriptSerializer().Serialize(m_Request);
               
                LogFiles.WriteLog("queryOrder json Request: " + json);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(ConstantPartner._123PAY_QUERY_URL);
                request.KeepAlive = false;
                request.ProtocolVersion = System.Net.HttpVersion.Version10;
                request.Method = "POST";


                // turn our request string into a byte stream
                byte[] postBytes = Encoding.ASCII.GetBytes(json);

                // this is important - make sure you specify type this way
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.ContentLength = postBytes.Length;
                //request.CookieContainer = Cookies;
                //request.UserAgent = currentUserAgent;
                Stream requestStream = request.GetRequestStream();

                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                // grab te response and print it out to the console along with the status code
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                string result;
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }
                LogFiles.WriteLog("queryOrder json Response: " + result);
                retVal = new JavaScriptSerializer().Deserialize<string[]>(result);
                //get partner trans
                //int SysMessageId = 0;
                //Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
                //m_Partner123PayTrans.Partner123PayTransactionId = this.mTransactionID;
                //int PaymentTransactionId = 0;
                //int.TryParse(this.mTransactionID, out PaymentTransactionId);
                //if(PaymentTransactionId > 0)
                //{
                //    m_Partner123PayTrans.PaymentTransactionId = PaymentTransactionId;
                //    m_Partner123PayTrans = m_Partner123PayTrans.Get();
                //}
                //else
                //{
                //    int RowCount = 0;
                //    string DateFrom = "";
                //    string DateTo = "";
                //    string OrderBy = "";
                //    int PageSize = 1;
                //    int PageNumber = 0;
                //    List<Partner123PayTrans> list = m_Partner123PayTrans.GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                //    if (list.Count > 0) m_Partner123PayTrans = list[0];
                //}
                
               
                //end
                //if (retVal.Length > 2)// success
                //{
                //    string returnCode = retVal[0];
                //    string Partner123PayTransactionId = retVal[1];
                //    string transactionStatus = retVal[2];
                //    totalAmount = retVal[3];
                //    string opAmount = retVal[4];
                //    bankCode = retVal[5];
                //    string tranDesc = retVal[6];
                //    string checksum = retVal[7];
                //    strToSign = returnCode + Partner123PayTransactionId + transactionStatus + totalAmount + opAmount + bankCode + secretKey;
                //    string ResponseSign = CalculateSHA1(strToSign);
                //    if (checksum != ResponseSign)
                //    {
                //        LogFiles.WriteLog("queryOrder: " + returnCode + " - checksum:" + checksum + " - ResponseSign:" + ResponseSign);
                //        retVal[0] = "6000";
                //    }
                //    if(transactionStatus == "1")// thanh cong
                //    {
                //        m_Partner123PayTrans.TransactionStatusId = 1;
                //        m_Partner123PayTrans.Update_TransactionStatusId(0);
                //    }
                //    else if (transactionStatus == "0")// thanh cong trang thai order moi tao
                //    {
                //        m_Partner123PayTrans.TransactionStatusId = 3;
                //        m_Partner123PayTrans.Update_TransactionStatusId(0);
                //    }
                //    else
                //    {
                //        m_Partner123PayTrans.TransactionStatusId = 2;
                //        m_Partner123PayTrans.Update_TransactionStatusId(0);
                //        //update desc
                //        m_Partner123PayTrans.returnDesc = tranDesc;
                //        m_Partner123PayTrans.Update(1, 0, ref SysMessageId);
                //    }
                    
                //}
                //else
                //{
                //    string returnCode = retVal[0];
                //    string returnDesc = retVal[1];
                //    LogFiles.WriteLog("queryOrder false: " + returnCode + " - returnDesc:" + returnDesc);
                //    //m_Partner123PayTrans.TransactionStatusId = 2;
                //    //m_Partner123PayTrans.Update_TransactionStatusId(0);
                //    ////update code and desc
                   
                //    //m_Partner123PayTrans.returnCode = returnCode;
                //    //m_Partner123PayTrans.returnDesc = returnDesc;
                //    //m_Partner123PayTrans.Update(1, 0, ref SysMessageId);
                //}
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog(ex.ToString());
            }
            return retVal;
        }
        //______________________________________________________________________________
        // SHA256 Hash Code
        
        public static string CalculateSHA1(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 =
            new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(
                cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

            return hash.ToLower();
        }
    }
    public class Notify123PayResponse
    {
        public string mTransactionID;
        public string returnCode;
        public string ts;
        public string checksum;
    }
    public class Notify123PayRequest
    {
        public string mTransactionID;
        public string bankCode;
        public string transactionStatus;
        public string description;
        public string ts;
        public string checksum;
    }
    public class QueryOrder123PayRequest
    {
        public string mTransactionID;
        public string merchantCode;
        public string clientIP;
        public string passcode;
        public string checksum;
    }
}
