using System.Web.Mvc;
using ICSoft.PartnerLib;
using LawsVN.Library.Sercurity;
using ICSoft.LawDocsLib;
using sms.utils;
using ICSoft.CMSLib;
using System;
using System.IO;
using Newtonsoft.Json;
using LawsVNEN.AppCode;

namespace LawsVNEN.Controllers
{
    public class PayController : Controller
    {

        
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
            byte LanguageId = Library.LawsVnLanguages.GetCurrentLanguageId();
            Partner123PayResponseCodes m_Partner123PayResponseCodes = new Partner123PayResponseCodes();

            Partner123PayTrans m_Partner123PayTrans = new Partner123PayTrans();
            m_Partner123PayTrans.PaymentTransactionId = int.Parse(transactionID.Trim());
            m_Partner123PayTrans = m_Partner123PayTrans.Get();
            if (m_Partner123PayTrans.Partner123PayTranId == 0)//ko tim thay
            {
                LogFiles.WriteLog("transactionID Not Found:" + transactionID);
                m_Partner123PayResponseCodes.PartnerCode = "-10";
                m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                if(LanguageId == 2)
                {
                    Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeName;
                }
                else
                {
                    Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                }
                return RedirectToRoute("HistoryTransactions");

            }
            else if (m_Partner123PayTrans.TransactionStatusId == 4)// chua xu ly
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
                    if (LanguageId == 2)
                    {
                        Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeName;
                    }
                    else
                    {
                        Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                    }
                    return RedirectToRoute("HistoryTransactions");
                }
                else
                {
                    string Partner123PayTransactionId = l_Result[1];
                    string transactionStatus = l_Result[2];
                    if (transactionStatus == "1")//giao dich thanh cong
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
                    if (LanguageId == 2)
                    {
                        Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeName;
                    }
                    else
                    {
                        Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                    }
                    return RedirectToRoute("HistoryTransactions");
                }
            }
            else// da xu ly
            {
                LogFiles.WriteLog("transactionID ReProcess:" + transactionID);
                m_Partner123PayResponseCodes.PartnerCode = m_Partner123PayTrans.OrderStatus;
                m_Partner123PayResponseCodes = m_Partner123PayResponseCodes.Get();
                if (LanguageId == 2)
                {
                    Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeName;
                }
                else
                {
                    Session["PayMessage"] = m_Partner123PayResponseCodes.PartnerCodeDesc;
                }
                Extensions.UpdateUserData();
                return RedirectToRoute("HistoryTransactions");
            }

        }
        
    }

}
