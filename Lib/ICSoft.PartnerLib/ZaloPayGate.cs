using ICSoft.CMSLib;
using sms.utils;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Web;
using System.Collections.Generic;
using System.Net;

namespace ICSoft.PartnerLib
{
    public class ZaloPay
    {
        private string _appid;
        private string _appuser;
        private string _apptime;
        private int _amount;
        private string _apptransid;
        private string _embeddata;
        private string _item;
        private string _mac;

        public string appid
        {
            get
            {
                return ConstantPartner.ZALOPAY_APPID;
            }

            set
            {
                _appid = value;
            }
        }

        public string appuser
        {
            get
            {
                return _appuser;
            }

            set
            {
                _appuser = value;
            }
        }

        public string apptime
        {
            get
            {
                return GetCurrentUnixTimestampMillis().ToString();
            }

            set
            {
                _apptime = value;
            }
        }

        public int amount
        {
            get
            {
                return _amount;
            }

            set
            {
                _amount = value;
            }
        }

        public string apptransid
        {
            get
            {
                return _apptransid;
            }

            set
            {
                _apptransid = value;
            }
        }

        public string embeddata
        {
            get
            {
                return "{\"promotioninfo\":\"\",\"merchantinfo\":\"" + appuser + "\"}";
            }

            set
            {
                _embeddata = value;
            }
        }

        public string item
        {
            get
            {
                return "{\"itemid\":\"01\",\"itemname\":\"Dịch vụ Luật Việt Nam\"}";
            }

            set
            {
                _item = value;
            }
        }

        public string mac
        {
            get
            {
                return CreateTokenHexString(appid + "|" + apptransid + "|" + appuser + "|" + amount.ToString() + "|" + apptime + "|" + embeddata + "|" + item);
            }

            set
            {
                _mac = value;
            }
        }



        /// <summary>
        /// Set apptransid = yymmdd + PaymentTransactionId
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="OrderId"></param>
        /// <returns>string[2] code,url redirect or error desciption</returns>
        public string[] createOrder(int CustomerId, int OrderId)
        {            
            string[] retVal = new string[] {"1","Tạo đơn hàng thành công" };
            try
            {
                this.appuser = CustomerId.ToString();

                string json = new JavaScriptSerializer().Serialize(this);
                LogFiles.WriteLog("json reqquest:" + json);
                string urlRedirect = ConstantPartner.ZALOPAY_PAY_URL + HttpContext.Current.Server.UrlEncode(Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(json)));
                //insert to partner trans
                int SysMessageId = 0;
                Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
                m_Partner123PayTrans.clientIP = HttpContext.Current.Request.UserHostAddress;
                m_Partner123PayTrans.CrDateTime = DateTime.Now;
                m_Partner123PayTrans.CustomerId = CustomerId;
                m_Partner123PayTrans.OrderId = OrderId;
                m_Partner123PayTrans.PaymentTransactionId = int.Parse(this.apptransid.Substring(6));
                m_Partner123PayTrans.totalAmount = amount;
                m_Partner123PayTrans.redirectURL = urlRedirect;
                m_Partner123PayTrans.Insert(1, 0, ref SysMessageId);
                //end
                
                LogFiles.WriteLog("urlRedirect:" + urlRedirect);
                retVal = new string[] { "1", urlRedirect };
            }
            catch (Exception ex)
            {
                retVal = new string[] { "2", "Có lỗi xảy ra khi tạo đơn hàng, mời bạn thử lại sau, hoặc liên hệ với Chăn sóc khách hàng để được trợ giúp." };
                LogFiles.WriteLog(ex.ToString());
            }
            return retVal;
        }


        public ZaloPayQueryOrderResponse queryOrder()
        {

            ZaloPayQueryOrderResponse retVal = new ZaloPayQueryOrderResponse();
            try
            {
                string queryUrl = ConstantPartner.ZALOPAY_QUERY_URL;
                queryUrl += "?appid=" + appid;
                queryUrl += "&apptransid=" + apptransid;
                string mac = CreateTokenHexString(appid + "|" + apptransid + "|" + ConstantPartner.ZALOPAY_KEY1);
               
                queryUrl += "&mac=" + mac;
                LogFiles.WriteLog("queryUrl: " + queryUrl);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string result = client.DownloadString(queryUrl);
                LogFiles.WriteLog("queryOrder json Response: " + result);
                retVal = new JavaScriptSerializer().Deserialize<ZaloPayQueryOrderResponse>(result);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog(ex.ToString());
            }
            return retVal;
        }

        /// <summary>
        /// Set apptransid = yymmdd + PaymentTransactionId
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="OrderId"></param>
        /// <returns>string[2] code,url redirect or error desciption</returns>
        public ZaloPayRefundResponse reFund(int PaymentTransactionId, string description)
        {

            ZaloPayRefundResponse retVal = new ZaloPayRefundResponse();
            try
            {
                string queryUrl = ConstantPartner.ZALOPAY_REFUND_URL;

                ZaloPayRequestCallback m_ZaloPay = new ZaloPayRequestCallback();
                ZaloPayRefundRequest m_ZaloPayRefundRequest = new ZaloPayRefundRequest();
                Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
                m_Partner123PayTrans.PaymentTransactionId = PaymentTransactionId;
                m_Partner123PayTrans = m_Partner123PayTrans.Get();
                m_ZaloPay = new JavaScriptSerializer().Deserialize<ZaloPayRequestCallback>(m_Partner123PayTrans.ResponseJson);
                m_ZaloPayRefundRequest.amount = m_Partner123PayTrans.totalAmount;
                m_ZaloPayRefundRequest.appid = m_ZaloPay.appid.ToString();
                m_ZaloPayRefundRequest.description = description;
                m_ZaloPayRefundRequest.mrefundid = m_Partner123PayTrans.CrDateTime.ToString("yyMMdd") + "_" + m_ZaloPayRefundRequest.appid + "_" + m_Partner123PayTrans.PaymentTransactionId.ToString();
                m_ZaloPayRefundRequest.timestamp = GetCurrentUnixTimestampSeconds().ToString();
                m_ZaloPayRefundRequest.zptransid = m_ZaloPay.zptransid;
                m_ZaloPayRefundRequest.mac = CreateTokenHexString(m_ZaloPayRefundRequest.appid + "|" + m_ZaloPayRefundRequest.zptransid + "|" + m_ZaloPayRefundRequest.amount.ToString() + "|" + description + "|" + m_ZaloPayRefundRequest.timestamp);
                string jsonRequest = new JavaScriptSerializer().Serialize(m_ZaloPayRefundRequest);
                LogFiles.WriteLog("reFund json Reqqest: " + jsonRequest);
                queryUrl += "?appid=" + m_ZaloPay.appid.ToString();
                queryUrl += "&amount=" + m_ZaloPay.amount.ToString();
                queryUrl += "&description=" + description;
                queryUrl += "&mrefundid=" + m_ZaloPayRefundRequest.mrefundid;
                queryUrl += "&timestamp=" + m_ZaloPayRefundRequest.timestamp;
                queryUrl += "&zptransid=" + m_ZaloPayRefundRequest.zptransid.ToString();
                queryUrl += "&mac=" + m_ZaloPayRefundRequest.mac;
                LogFiles.WriteLog("queryUrl: " + queryUrl);
                string result =OnePayRequest.doPost(queryUrl, jsonRequest);
                LogFiles.WriteLog("reFund json Response: " + result);
                retVal = new JavaScriptSerializer().Deserialize<ZaloPayRefundResponse>(result);
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog(ex.ToString());
            }
            return retVal;
        }
        //______________________________________________________________________________
        // SHA256 Hash Code
        private string CreateToken(string message)
        {
            string secret = ConstantPartner.ZALOPAY_KEY1;
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        private string CreateTokenHexString(string message)
        {
            string secret = ConstantPartner.ZALOPAY_KEY1;
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return HexStringUtil.byteArrayToHexString(hashmessage);
            }
        }

        public static string CreateTokenHexStringByKey(string message, string key)
        {
            string secret = key;
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return HexStringUtil.byteArrayToHexString(hashmessage);
            }
        }
        // Unix TimeStemp
        private static readonly DateTime UnixEpoch =    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentUnixTimestampMillis()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public static DateTime DateTimeFromUnixTimestampMillis(long millis)
        {
            return UnixEpoch.AddMilliseconds(millis);
        }

        public static long GetCurrentUnixTimestampSeconds()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
        }

        public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
        {
            return UnixEpoch.AddSeconds(seconds);
        }

    }
    public class ZaloPayResposeCallback
    {
        public int returncode;
        public string returnmessage;
    }
   
    public class ZaloPayQueryOrderResponse
    {
        public string returncode;
        public string returnmessage;
        public bool isprocessing;
        public long amount;
        public long zptransid;
    }
    public class ZaloPayRequestCallbackData
    {
        public string data;
        public string mac;
    }
    public class ZaloPayRequestCallback
    {
        public int appid { get; set; }
        public long zptransid { get; set; }
        public string apptransid { get; set; }
        public long apptime { get; set; }
        public string appuser { get; set; }
        public string item { get; set; }
        public int amount { get; set; }
        public string embeddata { get; set; }
        public long servertime { get; set; }
        public int channel { get; set; }
        public string merchantuserid { get; set; }
        public int userfeeamount { get; set; }
        public int discountamount { get; set; }
        public string bankcode { get; set; }
    }

    public class ZaloPayRefundRequest
    {
        public string mrefundid;
        public string appid;
        public long zptransid;
        public long amount;
        public string timestamp;
        public string description;
        public string mac;
    }
    public class ZaloPayRefundResponse
    {
        public int returncode;
        public string returnmessage;
        public string refundid;
    }
}
