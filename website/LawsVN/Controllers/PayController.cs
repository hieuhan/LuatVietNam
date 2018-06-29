using System.Web.Mvc;
using LawsVN.Models;
using LawsVN.Library;
using ICSoft.CMSViewLib;
using ICSoft.PartnerLib;
using LawsVN.Library.Sercurity;
using ICSoft.LawDocsLib;
using sms.utils;
using ICSoft.CMSLib;
using System;
using System.IO;
using Newtonsoft.Json;
using LawsVN.AppCode.Attribute;

namespace LawsVN.Controllers
{
    public class PayController : Controller
    {

        [SEOAction]
        public ActionResult _123PayRequest(string mTransactionID, string bankCode, string totalAmount)
        {
           
            LawsVnPrincipal lawsVnUser = (LawsVnPrincipal)System.Web.HttpContext.Current.User;
            Customers m_Customers = new Customers();
            m_Customers = m_Customers.Get(lawsVnUser.CustomerId);
            _123Pay m_Pay = new _123Pay();
            m_Pay.addInfo = null;
            m_Pay.bankCode = bankCode;
            m_Pay.cancelURL = "";
            m_Pay.checksum = "";
            m_Pay.clientIP = Request.UserHostAddress;
            m_Pay.custAddress = m_Customers.Address;
            m_Pay.custDOB = m_Customers.DateOfBirth.ToString("dd/MM/yyyy");
            m_Pay.custGender = m_Customers.GenderId == 1 ? "M" : m_Customers.GenderId == 0? "U" :"F";
            m_Pay.custMail = m_Customers.CustomerMail;
            m_Pay.custName = m_Customers.CustomerFullName;
            m_Pay.custPhone = m_Customers.CustomerMobile;
            m_Pay.description = "";
            m_Pay.errorURL = "";
            m_Pay.mTransactionID = mTransactionID;
            m_Pay.redirectURL = "";
            
            m_Pay.totalAmount = totalAmount;
            
            string[] responseObj = m_Pay.createOrder(1,1);
            return View();
        }
        
        [SEOAction]
        public ActionResult Result123Pay()
        {
            _123Pay m_Pay = new _123Pay();
            string transactionID = Request["transactionID"];
            string time = Request["time"];
            string status = Request["status"];
            string ticket = Request["ticket"];
            string str_MD5 = status + time + transactionID + ConstantPartner._123PAY_KEY;
            string md5Sign = sms.utils.md5.getMd5(str_MD5).Replace("-", "").ToLower();
            if (md5Sign != ticket)
            {
                LogFiles.WriteLog("md5 <> tickit:" + md5Sign + " <> " + ticket);
                return RedirectToRoute("HistoryTransactions");
            }
            Partner123PayResponseCodes m_Partner123PayResponseCodes = new Partner123PayResponseCodes();

            Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
            m_Partner123PayTrans.PaymentTransactionId = int.Parse(transactionID.Trim());
            m_Partner123PayTrans = m_Partner123PayTrans.Get();
            if(m_Partner123PayTrans.Partner123PayTranId == 0)//ko tim thay
            {
                LogFiles.WriteLog("transactionID Not Found:" + transactionID);
                m_Partner123PayResponseCodes.PartnerCode = "-10";
                m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                return RedirectToRoute("HistoryTransactions");

            }
            else if(m_Partner123PayTrans.TransactionStatusId == 4)// chua xu ly
            {
                //query order
                m_Pay.clientIP = Request.UserHostAddress;
                m_Pay.mTransactionID = transactionID;
                string[] l_Result = m_Pay.queryOrder();
                string returnCode = l_Result[0];
                if (returnCode != "1")//query loi
                {
                    m_Partner123PayResponseCodes.PartnerCode = l_Result[0];
                    m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                    Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                    return RedirectToRoute("HistoryTransactions");
                }
                else
                {
                    string Partner123PayTransactionId = l_Result[1];
                    string transactionStatus = l_Result[2];
                    if(transactionStatus == "1")//giao dich thanh cong
                    {
                        m_Partner123PayTrans.TransactionStatusId = 1;
                        //Đăng ký dịch vụ
                        string messages = "";
                        short ServiceId = 0;
                        Orders m_Orders = new Orders();
                        m_Orders.OrderId = m_Partner123PayTrans.OrderId;
                        m_Orders = m_Orders.Get();
                        PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
                        m_PaymentTransactions.PaymentTransactionId = m_Partner123PayTrans.PaymentTransactionId;
                        m_PaymentTransactions = m_PaymentTransactions.Get();
                        new CustomerServices().CustomerServices_LVN_DangKyThuebao(m_Partner123PayTrans.CustomerId, string.Empty,
                            ServiceId, m_PaymentTransactions.ServicePackageId, m_Orders.CouponCode,
                            m_PaymentTransactions.PaymentTransactionId, Request.UserHostAddress, ref messages);
                        Extensions.UpdateUserData();
                    }
                    else
                    {
                        m_Partner123PayTrans.TransactionStatusId = 2;
                        // update order and payment tran
                        var mOrder = new Orders().Get(m_Partner123PayTrans.OrderId);
                        short sysMessageId = 0;
                        if (mOrder.OrderId > 0)
                        {
                            mOrder.OrderStatusId = 3; //Hủy 
                            var sysMessageTypeId = mOrder.InsertOrUpdate(1, 0, ref sysMessageId);
                        }
                        var paymentTransactions = new PaymentTransactions().Get(m_Partner123PayTrans.PaymentTransactionId);
                        if (paymentTransactions.PaymentTransactionId > 0)
                        {
                            paymentTransactions.TransactionStatusId = 2; //Lỗi
                            var sysMessageTypeId = paymentTransactions.InsertOrUpdate(1, 0, ref sysMessageId);                            
                        }
                    }
                    m_Partner123PayTrans.OrderStatus = transactionStatus;
                    m_Partner123PayTrans.Update_TransactionStatusId(0);
                    m_Partner123PayResponseCodes.PartnerCode = transactionStatus;
                    m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                    Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                    return RedirectToRoute("HistoryTransactions");
                }
            }
            else// da xu ly
            {
                LogFiles.WriteLog("transactionID ReProcess:" + transactionID);
                m_Partner123PayResponseCodes.PartnerCode = m_Partner123PayTrans.OrderStatus;
                m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                Extensions.UpdateUserData();
                return RedirectToRoute("HistoryTransactions");
            }
            
        }

        public ActionResult ResultZaloPay()
        {
            try
            {
                ZaloPayResposeCallback m_ZaloPayResposeCallback = new ZaloPayResposeCallback();
                m_ZaloPayResposeCallback.returncode = 0;
                m_ZaloPayResposeCallback.returnmessage = "";
                LogFiles.WriteLog(Request.RawUrl);
                string apptransid = Request["apptransid"];
                string appid = Request["appid"];
                string bankcode = Request["bankcode"];
                string pmcid = Request["pmcid"];
                string amount = Request["amount"];
                string discountamount = Request["discountamount"];
                string status = Request["status"];
                string checksum = Request["checksum"];
                string curentChecksum = appid + "|" + apptransid + "|" + pmcid + "|" + bankcode + "|" + amount + "|" + discountamount + "|" + status;
                curentChecksum = ZaloPay.CreateTokenHexStringByKey(curentChecksum, ConstantPartner.ZALOPAY_KEY2);
                Partner123PayResponseCodes m_Partner123PayResponseCodes = new Partner123PayResponseCodes();
                if (curentChecksum != checksum)
                {
                    LogFiles.WriteLog("checksum <> curentChecksum:" + checksum + " <> " + curentChecksum);                    
                    return RedirectToRoute("HistoryTransactions");
                }
                else
                {
                    string PaymentTransactionId = apptransid.Substring(6);
                    Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
                    m_Partner123PayTrans.PaymentTransactionId = int.Parse(PaymentTransactionId);
                    m_Partner123PayTrans = m_Partner123PayTrans.Get();
                    if (m_Partner123PayTrans.Partner123PayTranId == 0)//ko tim thay
                    {
                        LogFiles.WriteLog("PaymentTransactionId Not Found:" + PaymentTransactionId);
                        return RedirectToRoute("HistoryTransactions");
                    }
                    else if (m_Partner123PayTrans.TransactionStatusId == 4)// chua xu ly
                    {
                        Orders m_Orders = new Orders();
                        m_Orders.OrderId = m_Partner123PayTrans.OrderId;
                        m_Orders = m_Orders.Get();
                        if (status == "1")//giao dich thanh cong
                        {
                            m_Partner123PayTrans.TransactionStatusId = 1;
                            //Đăng ký dịch vụ
                            string messages = "";
                            short ServiceId = 0;
                           
                            PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
                            m_PaymentTransactions.PaymentTransactionId = m_Partner123PayTrans.PaymentTransactionId;
                            m_PaymentTransactions = m_PaymentTransactions.Get();
                            new CustomerServices().CustomerServices_LVN_DangKyThuebao(m_Partner123PayTrans.CustomerId, string.Empty,
                                ServiceId, m_PaymentTransactions.ServicePackageId, m_Orders.CouponCode,
                                m_PaymentTransactions.PaymentTransactionId, Request.UserHostAddress, ref messages);
                            Extensions.UpdateUserData();
                        }
                        else
                        {
                            m_Partner123PayTrans.TransactionStatusId = 2;
                            // update order and payment tran
                           
                            short sysMessageId = 0;
                            if (m_Orders.OrderId > 0)
                            {
                                m_Orders.OrderStatusId = 3; //Hủy 
                                var sysMessageTypeId = m_Orders.InsertOrUpdate(1, 0, ref sysMessageId);
                            }
                            var paymentTransactions = new PaymentTransactions().Get(m_Partner123PayTrans.PaymentTransactionId);
                            if (paymentTransactions.PaymentTransactionId > 0)
                            {
                                paymentTransactions.TransactionStatusId = 2; //Lỗi
                                var sysMessageTypeId = paymentTransactions.InsertOrUpdate(1, 0, ref sysMessageId);
                            }
                        }

                        m_Partner123PayResponseCodes.PartnerCode = status;
                        m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                        //update partner trans
                        ZaloPayRequestCallback m_ZaloPay = new ZaloPayRequestCallback();
                        m_ZaloPay.amount = int.Parse(amount);
                        m_ZaloPay.appid = int.Parse(appid);
                        m_ZaloPay.apptransid = apptransid;
                        m_ZaloPay.bankcode = bankcode;
                        m_ZaloPay.channel = int.Parse(pmcid);
                        m_ZaloPay.discountamount = int.Parse(discountamount);
                        int SysMessageId = 0;
                        m_Partner123PayTrans.Partner123PayTransactionId = "";
                        m_Partner123PayTrans.ResponseDateTime = DateTime.Now;
                        m_Partner123PayTrans.ResponseJson = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(m_ZaloPay);
                        m_Partner123PayTrans.returnCode = status;
                        m_Partner123PayTrans.returnDesc = m_Partner123PayResponseCodes.PartnerCodeDesc;
                        m_Partner123PayTrans.Update(1, 0, ref SysMessageId);

                        m_Partner123PayTrans.OrderStatus = status;
                        m_Partner123PayTrans.Update_TransactionStatusId(1);
                        //=======================                        
                        Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                        if(m_Orders.SiteId == 2)
                        {
                            return Redirect(Constants.WebsiteDomainEnglish + "user/transactions-history.html");
                        }
                        else
                        {
                            return RedirectToRoute("HistoryTransactions");
                        }

                    }
                    else// da xu ly
                    {
                        Orders m_Orders = new Orders();
                        m_Orders.OrderId = m_Partner123PayTrans.OrderId;
                        m_Orders = m_Orders.Get();
                        LogFiles.WriteLog("transactionID ReProcess:" + PaymentTransactionId);
                        m_Partner123PayResponseCodes.PartnerCode = m_Partner123PayTrans.OrderStatus;
                        m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                        Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                        Extensions.UpdateUserData();
                        if (m_Orders.SiteId == 2)
                        {
                            return Redirect(Constants.WebsiteDomainEnglish + "user/transactions-history.html");
                        }
                        else
                        {
                            return RedirectToRoute("HistoryTransactions");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                LogFiles.WriteLog(ex.ToString());
                return RedirectToRoute("HistoryTransactions");
            }

        }

        public string NotifyZalo()
        {
            ZaloPayResposeCallback m_ZaloPayResposeCallback = new ZaloPayResposeCallback();
            
            try
            {
                if (!string.IsNullOrEmpty(Request["apptransid"]))
                {
                    string apptransid = Request["apptransid"];
                    ZaloPay m_ZaloPay = new ZaloPay();
                    m_ZaloPay.apptransid = apptransid;
                    ZaloPayQueryOrderResponse m_ZaloPayQueryOrderResponse = m_ZaloPay.queryOrder();
                    LogFiles.WriteLog("queryOrder: " + apptransid + ":" + m_ZaloPayQueryOrderResponse.returncode + ":" + m_ZaloPayQueryOrderResponse.returnmessage);
                    return "";
                    //string jsontest = "{\"data\":\"{\"appid\":3,\"zptransid\":160520000000081,\"apptransid\":\"160520176021926423825\",\"apptime\":1463711618132,\"appuser\":\"160514000002501\",\"item\":\"[{\"itemID\":\"it002\",\"itemName\":\"Color 50K\",\"itemQuantity\":1,\"itemPrice\":50000}]\",\"amount\":1000,\"embeddata\":\"{\"promotioninfo\":\"\",\"merchantinfo\":\"du lieu rieng cua ung dung\"}\",\"servertime\":1463711619269,\"channel\":38,\"merchantuserid\":\"rSVW3nBDryiJ6eN7h4L8ZjFn1OAbTaPoBm0I0JbB9zo\",\"userfeeamount\":220}\",\"mac\":\"ff441cae786b1f3c0419f6d9e6974f2c6f8981c6a06d34a514375daff833d3da\"}";
                    //ZaloPayRequestCallbackData m_ZaloPayRequestCallbackDataTest = new ZaloPayRequestCallbackData();
                    //m_ZaloPayRequestCallbackDataTest = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ZaloPayRequestCallbackData>(jsontest);
                    //string datatest = "{\"appid\":3,\"zptransid\":160520000000081,\"apptransid\":\"160520176021926423825\",\"apptime\":1463711618132,\"appuser\":\"160514000002501\",\"item\":\"[{\"itemID\":\"it002\",\"itemName\":\"Color 50K\",\"itemQuantity\":1,\"itemPrice\":50000}]\",\"amount\":1000,\"embeddata\":\"{\"promotioninfo\":\"\",\"merchantinfo\":\"du lieu rieng cua ung dung\"}\",\"servertime\":1463711619269,\"channel\":38,\"merchantuserid\":\"rSVW3nBDryiJ6eN7h4L8ZjFn1OAbTaPoBm0I0JbB9zo\",\"userfeeamount\":220}";
                    //string testmac = ZaloPay.CreateTokenHexStringByKey(datatest, ConstantPartner.ZALOPAY_KEY2);
                    //string testmacrequest = ZaloPay.CreateTokenHexStringByKey(m_ZaloPayRequestCallbackDataTest.data, ConstantPartner.ZALOPAY_KEY2);
                    //LogFiles.WriteLog(testmac);
                    //LogFiles.WriteLog(testmacrequest + "  <>  " + m_ZaloPayRequestCallbackDataTest.mac);
                    //return "";
                }
                LogFiles.WriteLog(Request.RawUrl);
                Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string json = new StreamReader(req).ReadToEnd();
                LogFiles.WriteLog("Notify: " + json);
               
                m_ZaloPayResposeCallback.returncode = 1;
                m_ZaloPayResposeCallback.returnmessage = "Thành công";
                ZaloPayRequestCallbackData m_ZaloPayRequestCallbackData = new ZaloPayRequestCallbackData();
                if (!string.IsNullOrEmpty(json))
                {
                    m_ZaloPayRequestCallbackData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ZaloPayRequestCallbackData>(json);
                }
                string curentMac = "";
                if (m_ZaloPayRequestCallbackData != null  && m_ZaloPayRequestCallbackData.data != null)
                {
                    curentMac = ZaloPay.CreateTokenHexStringByKey(m_ZaloPayRequestCallbackData.data, ConstantPartner.ZALOPAY_KEY2);
                }
                
                if (curentMac != m_ZaloPayRequestCallbackData.mac)
                {
                    LogFiles.WriteLog("mac <> curentMac:" + m_ZaloPayRequestCallbackData.mac + " <> " + curentMac);
                }
                else
                {
                    ZaloPayRequestCallback m_ZaloPayRequestCallback = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ZaloPayRequestCallback>(m_ZaloPayRequestCallbackData.data);
                    string PaymentTransactionId = m_ZaloPayRequestCallback.apptransid.Substring(6);
                    Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
                    m_Partner123PayTrans.PaymentTransactionId = int.Parse(PaymentTransactionId);
                    m_Partner123PayTrans = m_Partner123PayTrans.Get();
                    if (m_Partner123PayTrans.Partner123PayTranId == 0)//ko tim thay
                    {
                        LogFiles.WriteLog("PaymentTransactionId Not Found:" + PaymentTransactionId);
                        m_ZaloPayResposeCallback.returncode = 4;
                        m_ZaloPayResposeCallback.returnmessage = "Không tìm thấy giao dịch";
                    }
                    else if (m_Partner123PayTrans.TransactionStatusId == 4)// chua xu ly
                    {
                        LogFiles.WriteLog("PaymentTransactionId Success:" + PaymentTransactionId);
                        m_Partner123PayTrans.TransactionStatusId = 1;
                        //Đăng ký dịch vụ
                        string messages = "";
                        short ServiceId = 0;
                        Orders m_Orders = new Orders();
                        m_Orders.OrderId = m_Partner123PayTrans.OrderId;
                        m_Orders = m_Orders.Get();
                        PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
                        m_PaymentTransactions.PaymentTransactionId = m_Partner123PayTrans.PaymentTransactionId;
                        m_PaymentTransactions = m_PaymentTransactions.Get();
                        new CustomerServices().CustomerServices_LVN_DangKyThuebao(m_Partner123PayTrans.CustomerId, string.Empty,
                            ServiceId, m_PaymentTransactions.ServicePackageId, m_Orders.CouponCode,
                            m_PaymentTransactions.PaymentTransactionId, Request.UserHostAddress, ref messages);
                        Extensions.UpdateUserData();
                        Partner123PayResponseCodes m_Partner123PayResponseCodes = new Partner123PayResponseCodes();
                        m_Partner123PayResponseCodes.PartnerCode = "1";
                        m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                        //update partner trans

                        int SysMessageId = 0;
                        m_Partner123PayTrans.Partner123PayTransactionId = "";
                        m_Partner123PayTrans.ResponseDateTime = DateTime.Now;
                        m_Partner123PayTrans.ResponseJson = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(m_ZaloPayRequestCallback);
                        m_Partner123PayTrans.returnCode = "1";
                        m_Partner123PayTrans.returnDesc = m_Partner123PayResponseCodes.PartnerCodeDesc;
                        m_Partner123PayTrans.Update(1, 0, ref SysMessageId);

                        m_Partner123PayTrans.OrderStatus = "1";
                        m_Partner123PayTrans.Update_TransactionStatusId(1);
                        //=======================


                    }
                    else if (m_Partner123PayTrans.returnCode != "1")// da xu ly nhung trang thai la khong thanh cong
                    {
                        LogFiles.WriteLog("transactionID ReProcess:" + m_Partner123PayTrans.PaymentTransactionId.ToString() + ": " + m_Partner123PayTrans.returnCode);
                        //Đăng ký dịch vụ
                        string messages = "";
                        short ServiceId = 0;
                        Orders m_Orders = new Orders();
                        m_Orders.OrderId = m_Partner123PayTrans.OrderId;
                        m_Orders = m_Orders.Get();
                        PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
                        m_PaymentTransactions.PaymentTransactionId = m_Partner123PayTrans.PaymentTransactionId;
                        m_PaymentTransactions = m_PaymentTransactions.Get();
                        new CustomerServices().CustomerServices_LVN_DangKyThuebao(m_Partner123PayTrans.CustomerId, string.Empty,
                            ServiceId, m_PaymentTransactions.ServicePackageId, m_Orders.CouponCode,
                            m_PaymentTransactions.PaymentTransactionId, Request.UserHostAddress, ref messages);
                        Extensions.UpdateUserData();
                        Partner123PayResponseCodes m_Partner123PayResponseCodes = new Partner123PayResponseCodes();
                        m_Partner123PayResponseCodes.PartnerCode = "1";
                        m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                        //update partner trans

                        int SysMessageId = 0;
                        m_Partner123PayTrans.Partner123PayTransactionId = "";
                        m_Partner123PayTrans.ResponseDateTime = DateTime.Now;
                        m_Partner123PayTrans.ResponseJson = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(m_ZaloPayRequestCallback);
                        m_Partner123PayTrans.returnCode = "1";
                        m_Partner123PayTrans.returnDesc = m_Partner123PayTrans.returnDesc + " Notify: " + DateTime.Now.toString("dd-MM-yyyy HH:mm:ss") + " - " + m_Partner123PayTrans.returnCode;
                        m_Partner123PayTrans.Update(1, 0, ref SysMessageId);

                        m_Partner123PayTrans.OrderStatus = "1";
                        m_Partner123PayTrans.Update_TransactionStatusId(1);
                        //=======================

                        Extensions.UpdateUserData();
                    }
                    else// da xu ly
                    {
                        LogFiles.WriteLog("transactionID ReProcess:" + PaymentTransactionId);
                        Extensions.UpdateUserData();
                        m_ZaloPayResposeCallback.returncode = 2;
                        m_ZaloPayResposeCallback.returnmessage = "trùng mã giao dịch ZaloPay zptransid hoặc apptransid ( đã cung cấp dịch vụ cho user trước đó)";
                    }
                }
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.ContentType = "text/json";
                return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(m_ZaloPayResposeCallback);
            }
            catch(Exception ex)
            {
                LogFiles.WriteLog("NotifyZalo: " + ex);
                m_ZaloPayResposeCallback.returncode = 4;
                m_ZaloPayResposeCallback.returnmessage = "Lỗi hệ thống nhận";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.ContentType = "text/json";
                return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(m_ZaloPayResposeCallback);
            }
            

        }

        public string Notify()
        {
            Notify123PayRequest m_Notify123PayPost = new Notify123PayRequest();
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();
            LogFiles.WriteLog("Notify: " + json);
            if (json.StartsWith("{"))
            {
                try
                {
                    m_Notify123PayPost = JsonConvert.DeserializeObject<Notify123PayRequest>(json);
                }
                catch (Exception ex)
                {
                    LogFiles.WriteLog(ex.ToString());
                }
            }
            else
            {
                string[] l_Param = json.Split('&');
                m_Notify123PayPost = new Notify123PayRequest();
                for (int index = 0; index < l_Param.Length; index++)
                {
                    if (l_Param[index].Contains("mTransactionID="))
                    {
                        m_Notify123PayPost.mTransactionID = l_Param[index].Replace("mTransactionID=", "");
                    }
                    if (l_Param[index].Contains("bankCode="))
                    {
                        m_Notify123PayPost.bankCode = l_Param[index].Replace("bankCode=", "");
                    }
                    if (l_Param[index].Contains("transactionStatus="))
                    {
                        m_Notify123PayPost.transactionStatus = l_Param[index].Replace("transactionStatus=", "");
                    }
                    if (l_Param[index].Contains("ts="))
                    {
                        m_Notify123PayPost.ts = l_Param[index].Replace("ts=", "");
                    }
                    if (l_Param[index].Contains("checksum="))
                    {
                        m_Notify123PayPost.checksum = l_Param[index].Replace("checksum=", "");
                    }
                    if (l_Param[index].Contains("description="))
                    {
                        m_Notify123PayPost.description = l_Param[index].Replace("description=", "");
                    }
                }
            }
            if (m_Notify123PayPost == null)
            {
                m_Notify123PayPost = new Notify123PayRequest();
            }
            Notify123PayResponse m_Notify123Pay = new Notify123PayResponse();           
            _123Pay m_Pay = new _123Pay();
            
            string str_MD5 = m_Notify123PayPost.mTransactionID + m_Notify123PayPost.bankCode  + m_Notify123PayPost.transactionStatus  + m_Notify123PayPost.ts + ConstantPartner._123PAY_KEY;
            string md5Sign = _123Pay.CalculateSHA1(str_MD5);
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            
            if (md5Sign != m_Notify123PayPost.checksum || String.IsNullOrEmpty(m_Notify123PayPost.ts) || String.IsNullOrEmpty(m_Notify123PayPost.checksum))
            {
                LogFiles.WriteLog("md5 <> tickit:" + md5Sign + " <> " + m_Notify123PayPost.checksum);
                m_Notify123Pay.mTransactionID = m_Notify123PayPost.mTransactionID;
                m_Notify123Pay.returnCode = "-1";
                m_Notify123Pay.ts = unixTimestamp.ToString();
                str_MD5 = m_Notify123Pay.mTransactionID + m_Notify123Pay.returnCode + m_Notify123Pay.ts + ConstantPartner._123PAY_KEY;
                m_Notify123Pay.checksum = _123Pay.CalculateSHA1(str_MD5);
                Response.Clear();
                Response.ContentType = "application/json";
                return Newtonsoft.Json.JsonConvert.SerializeObject(m_Notify123Pay);
            }
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(double.Parse(m_Notify123PayPost.ts)).ToLocalTime();
            if(dt >= DateTime.Now || dt < DateTime.Now.AddMinutes(-5))
            {
                LogFiles.WriteLog("time expired: " + dt.ToString("dd-MM-yyyy HH:mm:ss") + " <> " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                m_Notify123Pay.mTransactionID = m_Notify123PayPost.mTransactionID;
                m_Notify123Pay.returnCode = "-2";
                m_Notify123Pay.ts = unixTimestamp.ToString();
                str_MD5 = m_Notify123Pay.mTransactionID + m_Notify123Pay.returnCode + m_Notify123Pay.ts + ConstantPartner._123PAY_KEY;
                m_Notify123Pay.checksum = _123Pay.CalculateSHA1(str_MD5);
                Response.Clear();
                Response.ContentType = "application/json";
                return Newtonsoft.Json.JsonConvert.SerializeObject(m_Notify123Pay);
            }
            Partner123PayResponseCodes m_Partner123PayResponseCodes = new Partner123PayResponseCodes();

            Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
            m_Partner123PayTrans.PaymentTransactionId = int.Parse(m_Notify123PayPost.mTransactionID);
            m_Partner123PayTrans = m_Partner123PayTrans.Get();
            if (m_Partner123PayTrans.Partner123PayTranId == 0)//ko tim thay
            {
                LogFiles.WriteLog("transactionID Not Found:" + m_Notify123PayPost.mTransactionID);
                m_Notify123Pay.mTransactionID = m_Notify123PayPost.mTransactionID;
                m_Notify123Pay.returnCode = "-3";
                m_Notify123Pay.ts = unixTimestamp.ToString();
                str_MD5 = m_Notify123Pay.mTransactionID + m_Notify123Pay.returnCode + m_Notify123Pay.ts + ConstantPartner._123PAY_KEY;
                m_Notify123Pay.checksum = _123Pay.CalculateSHA1(str_MD5);
                Response.Clear();
                Response.ContentType = "application/json";
                return Newtonsoft.Json.JsonConvert.SerializeObject(m_Notify123Pay);

            }
            else if (m_Partner123PayTrans.TransactionStatusId == 4 )// chua xu ly
            {
                
                if (m_Notify123PayPost.transactionStatus == "1")//giao dich thanh cong
                {
                    m_Partner123PayTrans.TransactionStatusId = 1;
                    //Đăng ký dịch vụ
                    string messages = "";
                    short ServiceId = 0;
                    Orders m_Orders = new Orders();
                    m_Orders.OrderId = m_Partner123PayTrans.OrderId;
                    m_Orders = m_Orders.Get();
                    PaymentTransactions m_PaymentTransactions = new PaymentTransactions();
                    m_PaymentTransactions.PaymentTransactionId = m_Partner123PayTrans.PaymentTransactionId;
                    m_PaymentTransactions = m_PaymentTransactions.Get();
                    new CustomerServices().CustomerServices_LVN_DangKyThuebao(m_Partner123PayTrans.CustomerId, string.Empty,
                        ServiceId, m_PaymentTransactions.ServicePackageId, m_Orders.CouponCode,
                        m_PaymentTransactions.PaymentTransactionId, Request.UserHostAddress, ref messages);
                }
                else
                {
                    m_Partner123PayTrans.TransactionStatusId = 2;
                    // update order and payment tran
                    var mOrder = new Orders().Get(m_Partner123PayTrans.OrderId);
                    short sysMessageId = 0;
                    if (mOrder.OrderId > 0)
                    {
                        mOrder.OrderStatusId = 3; //Hủy 
                        var sysMessageTypeId = mOrder.InsertOrUpdate(1, 0, ref sysMessageId);
                    }
                    var paymentTransactions = new PaymentTransactions().Get(m_Partner123PayTrans.PaymentTransactionId);
                    if (paymentTransactions.PaymentTransactionId > 0)
                    {
                        paymentTransactions.TransactionStatusId = 2; //Lỗi
                        var sysMessageTypeId = paymentTransactions.InsertOrUpdate(1, 0, ref sysMessageId);
                    }
                }
                m_Partner123PayTrans.OrderStatus = m_Notify123PayPost.transactionStatus;
                m_Partner123PayTrans.Update_TransactionStatusId(0);                

                m_Notify123Pay.mTransactionID = m_Notify123PayPost.mTransactionID;
                m_Notify123Pay.returnCode = "1";
                m_Notify123Pay.ts = unixTimestamp.ToString();
                str_MD5 = m_Notify123Pay.mTransactionID + m_Notify123Pay.returnCode + m_Notify123Pay.ts + ConstantPartner._123PAY_KEY;
                m_Notify123Pay.checksum = _123Pay.CalculateSHA1(str_MD5);
                Response.Clear();
                Response.ContentType = "application/json";
                return Newtonsoft.Json.JsonConvert.SerializeObject(m_Notify123Pay);
            }
            else if (m_Partner123PayTrans.OrderStatus != m_Notify123PayPost.transactionStatus)// da xu ly nhung Notify trang thai khac
            {
                LogFiles.WriteLog("transactionID ReProcess:" + m_Notify123PayPost.mTransactionID + ": " + m_Notify123PayPost.transactionStatus);

                m_Partner123PayTrans.returnDesc = m_Partner123PayTrans.returnDesc + " Notify: " + DateTime.Now.toString("dd-MM-yyyy HH:mm:ss") + " - " + m_Notify123PayPost.transactionStatus;

                m_Notify123Pay.mTransactionID = m_Notify123PayPost.mTransactionID;
                m_Notify123Pay.returnCode = "2";
                m_Notify123Pay.ts = unixTimestamp.ToString();
                str_MD5 = m_Notify123Pay.mTransactionID + m_Notify123Pay.returnCode + m_Notify123Pay.ts + ConstantPartner._123PAY_KEY;
                m_Notify123Pay.checksum = _123Pay.CalculateSHA1(str_MD5);
                Response.Clear();
                Response.ContentType = "application/json";
                return Newtonsoft.Json.JsonConvert.SerializeObject(m_Notify123Pay);
            }
            else// da xu ly
            {
                LogFiles.WriteLog("transactionID ReProcess:" + m_Notify123PayPost.mTransactionID + ": " + m_Notify123PayPost.transactionStatus);
                m_Notify123Pay.mTransactionID = m_Notify123PayPost.mTransactionID;
                m_Notify123Pay.returnCode = "2";
                m_Notify123Pay.ts = unixTimestamp.ToString();
                str_MD5 = m_Notify123Pay.mTransactionID + m_Notify123Pay.returnCode + m_Notify123Pay.ts + ConstantPartner._123PAY_KEY;
                m_Notify123Pay.checksum = _123Pay.CalculateSHA1(str_MD5);
                Response.ContentType = "application/json";
                return Newtonsoft.Json.JsonConvert.SerializeObject(m_Notify123Pay);
            }
            
        }
    }
    
}
