using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ICSoft.PartnerLib
{
    public class ConstantPartner
    {

       
        public static string ONEPAY_SECURE_SECRET = (ConfigurationManager.AppSettings["ONEPAY_SECURE_SECRET"] == null) ? "CTC" : ConfigurationManager.AppSettings["ONEPAY_SECURE_SECRET"];
        public static string ONEPAY_SECURE_SECRET_QT = (ConfigurationManager.AppSettings["ONEPAY_SECURE_SECRET_QT"] == null) ? "CTC" : ConfigurationManager.AppSettings["ONEPAY_SECURE_SECRET_QT"];
        public static string ONEPAY_PAY_URL = (ConfigurationManager.AppSettings["ONEPAY_PAY_URL"] == null) ? "https://mtf.onepay.vn/onecomm-pay/vpc.op" : ConfigurationManager.AppSettings["ONEPAY_PAY_URL"];
        public static string ONEPAY_PAY_URL_QT = (ConfigurationManager.AppSettings["ONEPAY_PAY_URL_QT"] == null) ? "https://mtf.onepay.vn/onecomm-pay/vpc.op" : ConfigurationManager.AppSettings["ONEPAY_PAY_URL_QT"];
        public static string ONEPAY_QUERY_URL = (ConfigurationManager.AppSettings["ONEPAY_QUERY_URL"] == null) ? "http://mtf.onepay.vn/onecomm-pay/Vpcdps.oppcdps.op" : ConfigurationManager.AppSettings["ONEPAY_QUERY_URL"];
        public static string ONEPAY_QUERY_URL_QT = (ConfigurationManager.AppSettings["ONEPAY_QUERY_URL_QT"] == null) ? "https://mtf.onepay.vn/onecomm-pay/vpc.op" : ConfigurationManager.AppSettings["ONEPAY_QUERY_URL_QT"];

        public static string ONEPAY_VERSION = (ConfigurationManager.AppSettings["ONEPAY_VERSION"] == null) ? "2" : ConfigurationManager.AppSettings["ONEPAY_VERSION"];
        public static string ONEPAY_COMMAND = (ConfigurationManager.AppSettings["ONEPAY_COMMAND"] == null) ? "pay" : ConfigurationManager.AppSettings["ONEPAY_COMMAND"];
        public static string ONEPAY_MERCHANT = (ConfigurationManager.AppSettings["ONEPAY_MERCHANT"] == null) ? "danhgianhadat" : ConfigurationManager.AppSettings["ONEPAY_MERCHANT"];
        public static string ONEPAY_MERCHANT_QT = (ConfigurationManager.AppSettings["ONEPAY_MERCHANT_QT"] == null) ? "danhgianhadat" : ConfigurationManager.AppSettings["ONEPAY_MERCHANT_QT"];
        public static string ONEPAY_ACCESSCODE = (ConfigurationManager.AppSettings["ONEPAY_ACCESSCODE"] == null) ? "danhgia" : ConfigurationManager.AppSettings["ONEPAY_ACCESSCODE"];
        public static string ONEPAY_ACCESSCODE_QT = (ConfigurationManager.AppSettings["ONEPAY_ACCESSCODE_QT"] == null) ? "danhgia" : ConfigurationManager.AppSettings["ONEPAY_ACCESSCODE_QT"];
        public static string ONEPAY_CURENCY = (ConfigurationManager.AppSettings["ONEPAY_CURENCY"] == null) ? "VND" : ConfigurationManager.AppSettings["ONEPAY_CURENCY"];
        public static string ONEPAY_CURENCY_QT = (ConfigurationManager.AppSettings["ONEPAY_CURENCY_QT"] == null) ? "VND" : ConfigurationManager.AppSettings["ONEPAY_CURENCY_QT"];

        public static string ONEPAY_USER = (ConfigurationManager.AppSettings["ONEPAY_USER"] == null) ? "op01" : ConfigurationManager.AppSettings["ONEPAY_USER"];
        public static string ONEPAY_USER_QT = (ConfigurationManager.AppSettings["ONEPAY_USER_QT"] == null) ? "op01" : ConfigurationManager.AppSettings["ONEPAY_USER_QT"];
        public static string ONEPAY_PASS = (ConfigurationManager.AppSettings["ONEPAY_PASS"] == null) ? "op123456" : ConfigurationManager.AppSettings["ONEPAY_PASS"];
        public static string ONEPAY_PASS_QT = (ConfigurationManager.AppSettings["ONEPAY_PASS_QT"] == null) ? "op123456" : ConfigurationManager.AppSettings["ONEPAY_PASS_QT"];


        public static string MOBI_CHARGINGURL = (ConfigurationManager.AppSettings["MOBI_CHARGINGURL"] == null) ? "http://mpay.mobifone.com.vn/sp/confirmsub" : ConfigurationManager.AppSettings["MOBI_CHARGINGURL"];
        public static string MOBI_SHORTCODE = (ConfigurationManager.AppSettings["MOBI_SHORTCODE"] == null) ? "111" : ConfigurationManager.AppSettings["MOBI_SHORTCODE"];

        public static string MOBI_SUCCESCODE = (ConfigurationManager.AppSettings["MOBI_SUCCESCODE"] == null) ? "WCG-0000" : ConfigurationManager.AppSettings["MOBI_SUCCESCODE"];


        public static string _123PAY_PAY_URL = (ConfigurationManager.AppSettings["_123PAY_PAY_URL"] == null) ? "https://sandbox.123pay.vn/miservice/createOrder1" : ConfigurationManager.AppSettings["_123PAY_PAY_URL"];        
        public static string _123PAY_QUERY_URL = (ConfigurationManager.AppSettings["_123PAY_QUERY_URL"] == null) ? "https://sandbox.123pay.vn/miservice/queryOrder1" : ConfigurationManager.AppSettings["_123PAY_QUERY_URL"];
        public static string _123PAY_KEY = (ConfigurationManager.AppSettings["_123PAY_KEY"] == null) ? "MIKEY" : ConfigurationManager.AppSettings["_123PAY_KEY"];
        public static string _123PAY_PASSCODE = (ConfigurationManager.AppSettings["_123PAY_PASSCODE"] == null) ? "MIPASSCODE" : ConfigurationManager.AppSettings["_123PAY_PASSCODE"];
        public static string _123PAY_MERCHANTCODE = (ConfigurationManager.AppSettings["_123PAY_MERCHANTCODE"] == null) ? "MICODE01" : ConfigurationManager.AppSettings["_123PAY_MERCHANTCODE"];

        public static string ZALOPAY_PAY_URL = (ConfigurationManager.AppSettings["ZALOPAY_PAY_URL"] == null) ? "https://sandbox.123pay.vn/miservice/createOrder1" : ConfigurationManager.AppSettings["ZALOPAY_PAY_URL"];
        public static string ZALOPAY_QUERY_URL = (ConfigurationManager.AppSettings["ZALOPAY_QUERY_URL"] == null) ? "https://sandbox.123pay.vn/miservice/queryOrder1" : ConfigurationManager.AppSettings["ZALOPAY_QUERY_URL"];
        public static string ZALOPAY_REFUND_URL = (ConfigurationManager.AppSettings["ZALOPAY_REFUND_URL"] == null) ? "https://sandbox.zalopay.com.vn/v001/tpe/partialrefund" : ConfigurationManager.AppSettings["ZALOPAY_REFUND_URL"];
        public static string ZALOPAY_KEY1 = (ConfigurationManager.AppSettings["ZALOPAY_KEY1"] == null) ? "MIKEY" : ConfigurationManager.AppSettings["ZALOPAY_KEY1"];
        public static string ZALOPAY_KEY2 = (ConfigurationManager.AppSettings["ZALOPAY_KEY2"] == null) ? "MIPASSCODE" : ConfigurationManager.AppSettings["ZALOPAY_KEY2"];
        public static string ZALOPAY_APPID = (ConfigurationManager.AppSettings["ZALOPAY_APPID"] == null) ? "MICODE01" : ConfigurationManager.AppSettings["ZALOPAY_APPID"];
    }
}
