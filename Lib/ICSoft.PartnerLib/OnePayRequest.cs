using ICSoft.CMSLibEstate;
using System;
using System.Net;
using System.Web;
using System.IO;
using System.Text;
namespace ICSoft.PartnerLib
{
    public class OnePayRequest
    {
        public string GetPayUrl(int CustomerId, string vpc_OrderInfo, string vpc_Amount, string vpc_ReturnURL, string vpc_TicketNo)
        {
            String url = "";
            try
            {
                string SECURE_SECRET = ConstantPartner.ONEPAY_SECURE_SECRET;
                string vpc_MerchTxnRef = "";
                // insert to OnePayTrans
                byte Replicated = 0;
                int ActUserId = 1;
                int SysMessageId = 0;
                OnePayTrans m_OnePayTrans = new OnePayTrans();
                m_OnePayTrans.vpc_Amount = int.Parse(vpc_Amount);
                m_OnePayTrans.vcp_Message = "";
                m_OnePayTrans.vpc_OrderInfo = vpc_OrderInfo;
                m_OnePayTrans.vpc_TicketNo = vpc_TicketNo;
                m_OnePayTrans.vpc_TransactionNo = "";
                m_OnePayTrans.vpc_TxnResponseCode = -1;
                m_OnePayTrans.AgainLink = vpc_ReturnURL;
                m_OnePayTrans.CustomerId = CustomerId;
                m_OnePayTrans.ResponseTime = DateTime.MinValue;
                m_OnePayTrans.Insert(Replicated, ActUserId, ref SysMessageId);
                vpc_MerchTxnRef = m_OnePayTrans.TransId.ToString();
                // Khoi tao lop thu vien va gan gia tri cac tham so gui sang cong thanh toan
                VPCRequest conn = new VPCRequest(ConstantPartner.ONEPAY_PAY_URL);
                conn.SetSecureSecret(SECURE_SECRET);
                // Add the Digital Order Fields for the functionality you wish to use
                // Core Transaction Fields
                conn.AddDigitalOrderField("Title", "onepay paygate");
                conn.AddDigitalOrderField("vpc_Locale", "vn");//Chon ngon ngu hien thi tren cong thanh toan (vn/en)
                conn.AddDigitalOrderField("vpc_Version", ConstantPartner.ONEPAY_VERSION);
                conn.AddDigitalOrderField("vpc_Command", ConstantPartner.ONEPAY_COMMAND);
                conn.AddDigitalOrderField("vpc_Merchant", ConstantPartner.ONEPAY_MERCHANT);
                conn.AddDigitalOrderField("vpc_AccessCode", ConstantPartner.ONEPAY_ACCESSCODE);
                conn.AddDigitalOrderField("vpc_MerchTxnRef", vpc_MerchTxnRef);
                conn.AddDigitalOrderField("vpc_OrderInfo", vpc_OrderInfo);
                conn.AddDigitalOrderField("vpc_Amount", vpc_Amount);
                conn.AddDigitalOrderField("vpc_Currency", ConstantPartner.ONEPAY_CURENCY);
                conn.AddDigitalOrderField("vpc_ReturnURL", vpc_ReturnURL);
                // Thong tin them ve khach hang. De trong neu khong co thong tin
                conn.AddDigitalOrderField("vpc_SHIP_Street01", "");
                conn.AddDigitalOrderField("vpc_SHIP_Provice", "");
                conn.AddDigitalOrderField("vpc_SHIP_City", "");
                conn.AddDigitalOrderField("vpc_SHIP_Country", "");
                conn.AddDigitalOrderField("vpc_Customer_Phone", "");
                conn.AddDigitalOrderField("vpc_Customer_Email", "");
                conn.AddDigitalOrderField("vpc_Customer_Id", "");
                // Dia chi IP cua khach hang
                conn.AddDigitalOrderField("vpc_TicketNo", vpc_TicketNo);
                // Chuyen huong trinh duyet sang cong thanh toan
                url = conn.Create3PartyQueryString();

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.WriteLog(ex.ToString() + Environment.NewLine + ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return url;
        }
        //
        public string GetPayUrlQT(int CustomerId, string vpc_OrderInfo, string vpc_Amount, string vpc_ReturnURL, string vpc_TicketNo)
        {
            String url = "";
            try
            {
                string SECURE_SECRET = ConstantPartner.ONEPAY_SECURE_SECRET_QT;
                string vpc_MerchTxnRef = "";
                // insert to OnePayTrans
                byte Replicated = 0;
                int ActUserId = 1;
                int SysMessageId = 0;
                OnePayTrans m_OnePayTrans = new OnePayTrans();
                m_OnePayTrans.vpc_Amount = int.Parse(vpc_Amount);
                m_OnePayTrans.vcp_Message = "";
                m_OnePayTrans.vpc_OrderInfo = vpc_OrderInfo;
                m_OnePayTrans.vpc_TicketNo = vpc_TicketNo;
                m_OnePayTrans.vpc_TransactionNo = "";
                m_OnePayTrans.vpc_TxnResponseCode = -1;
                m_OnePayTrans.AgainLink = vpc_ReturnURL;
                m_OnePayTrans.CustomerId = CustomerId;
                m_OnePayTrans.ResponseTime = DateTime.MinValue;
                m_OnePayTrans.Insert(Replicated, ActUserId, ref SysMessageId);
                vpc_MerchTxnRef = m_OnePayTrans.TransId.ToString();
                

                // Khoi tao lop thu vien va gan gia tri cac tham so gui sang cong thanh toan
                VPCRequest conn = new VPCRequest(ConstantPartner.ONEPAY_PAY_URL_QT);
                conn.SetSecureSecret(SECURE_SECRET);
                // Add the Digital Order Fields for the functionality you wish to use
                // Core Transaction Fields
                conn.AddDigitalOrderField("AgainLink", ICSoft.CMSLib.CmsConstants.WEBSITE_DOMAIN + "user/nap-tien.html");
                conn.AddDigitalOrderField("Title", "onepay paygate");
                conn.AddDigitalOrderField("vpc_Locale", "vn");//Chon ngon ngu hien thi tren cong thanh toan (vn/en)
                conn.AddDigitalOrderField("vpc_Version", ConstantPartner.ONEPAY_VERSION);
                conn.AddDigitalOrderField("vpc_Command", ConstantPartner.ONEPAY_COMMAND);
                conn.AddDigitalOrderField("vpc_Merchant", ConstantPartner.ONEPAY_MERCHANT_QT);
                conn.AddDigitalOrderField("vpc_AccessCode", ConstantPartner.ONEPAY_ACCESSCODE_QT);
                conn.AddDigitalOrderField("vpc_MerchTxnRef", vpc_MerchTxnRef);
                conn.AddDigitalOrderField("vpc_OrderInfo", vpc_OrderInfo);
                conn.AddDigitalOrderField("vpc_Amount", vpc_Amount);
                conn.AddDigitalOrderField("vpc_ReturnURL", vpc_ReturnURL);
                // Thong tin them ve khach hang. De trong neu khong co thong tin
                conn.AddDigitalOrderField("vpc_SHIP_Street01", "");
                conn.AddDigitalOrderField("vpc_SHIP_Provice", "");
                conn.AddDigitalOrderField("vpc_SHIP_City", "");
                conn.AddDigitalOrderField("vpc_SHIP_Country", "");
                conn.AddDigitalOrderField("vpc_Customer_Phone", "");
                conn.AddDigitalOrderField("vpc_Customer_Email", "");
                conn.AddDigitalOrderField("vpc_Customer_Id", "");
                // Dia chi IP cua khach hang
                conn.AddDigitalOrderField("vpc_TicketNo", vpc_TicketNo);
                // Chuyen huong trinh duyet sang cong thanh toan
                url = conn.Create3PartyQueryString();

            }
            catch (Exception ex)
            {
                sms.utils.LogFiles.WriteLog(ex.ToString() + Environment.NewLine + ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
            }
            return url;
        }
        public string Query(string vpc_MerchTxnRef)
        {
            string postData = "";
            string seperator = "";
            string resQS = "";
            int paras = 7;
            string vpcURL = ConstantPartner.ONEPAY_QUERY_URL;


            string[,] MyArray =
            {
            {"vpc_AccessCode",ConstantPartner.ONEPAY_ACCESSCODE},
            {"vpc_Command","queryDR" },
            {"vpc_MerchTxnRef",vpc_MerchTxnRef},
            {"vpc_Merchant",ConstantPartner.ONEPAY_MERCHANT },
            {"vpc_Password",ConstantPartner.ONEPAY_PASS},
            {"vpc_User",ConstantPartner.ONEPAY_USER},
            {"vpc_Version",ConstantPartner.ONEPAY_VERSION}
            };
            for (int i = 0; i < paras; i++)
            {
                postData = postData + seperator + HttpContext.Current.Server.UrlEncode(MyArray[i, 0]) + "=" + HttpContext.Current.Server.UrlEncode(MyArray[i, 1]);
                seperator = "&";
            }

            resQS = doPost(vpcURL, postData);
            return resQS;
        }
        public string QueryQT(string vpc_MerchTxnRef)
        {
            string postData = "";
            string seperator = "";
            string resQS = "";
            int paras = 7;
            string vpcURL = ConstantPartner.ONEPAY_QUERY_URL_QT;


            string[,] MyArray =
            {
            {"vpc_AccessCode",ConstantPartner.ONEPAY_ACCESSCODE_QT},
            {"vpc_Command","queryDR" },
            {"vpc_MerchTxnRef",vpc_MerchTxnRef},
            {"vpc_Merchant",ConstantPartner.ONEPAY_MERCHANT_QT },
            {"vpc_Password",ConstantPartner.ONEPAY_PASS_QT},
            {"vpc_User",ConstantPartner.ONEPAY_USER_QT},
            {"vpc_Version",ConstantPartner.ONEPAY_VERSION}
            };
            for (int i = 0; i < paras; i++)
            {
                postData = postData + seperator + HttpContext.Current.Server.UrlEncode(MyArray[i, 0]) + "=" + HttpContext.Current.Server.UrlEncode(MyArray[i, 1]);
                seperator = "&";
            }

            resQS = doPost(vpcURL, postData);
            return resQS;
        }
        /// <summary>
        /// Together with the AcceptAllCertifications method right
        /// below this causes to bypass errors caused by SLL-Errors.
        /// </summary>
        public static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        }

        /// <summary>
        /// In Short: the Method solves the Problem of broken Certificates.
        /// Sometime when requesting Data and the sending Webserverconnection
        /// is based on a SSL Connection, an Error is caused by Servers whoes
        /// Certificate(s) have Errors. Like when the Cert is out of date
        /// and much more... So at this point when calling the method,
        /// this behaviour is prevented
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certification"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns>true</returns>
        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public static string doPost(string vpc_Host, string postData)
        {
            string page = "";
            IgnoreBadCertificates();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(vpc_Host);

            //  WebRequest request = WebRequest.Create(vpc_Host);
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            //string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.         
            request.UserAgent = "HTTP Client";
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            //    WebResponse response = request.GetResponse();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            //   Response.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            page = page + responseFromServer;
            // Display the content.
            //  Response.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return page;
        }
    }
}
