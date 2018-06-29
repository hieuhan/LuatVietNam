using ICSoft.CMSLibEstate;
using ICSoft.CMSViewLib;
using sms.utils;
using System;
using System.Web;
namespace ICSoft.PartnerLib
{
    public class OnePayReponse
    {
        public OnePayTrans HandleUrl(System.Collections.Specialized.NameValueCollection QueryString,int CustomerId)
        {
            Log.writeLog(HttpContext.Current.Request.Url.ToString(), "HandleUrl");
            OnePayTrans RetVal = new OnePayTrans();
            string SECURE_SECRET = ConstantPartner.ONEPAY_SECURE_SECRET;
            string hashvalidateResult = "";
            // Khoi tao lop thu vien
            VPCRequest conn = new VPCRequest("http://onepay.vn");
            conn.SetSecureSecret(SECURE_SECRET);
            // Xu ly tham so tra ve va kiem tra chuoi du lieu ma hoa
            hashvalidateResult = conn.Process3PartyResponse(QueryString);

            // Lay gia tri tham so tra ve tu cong thanh toan
            String vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");
            string amount = conn.GetResultField("vpc_Amount", "Unknown");
            string localed = conn.GetResultField("vpc_Locale", "Unknown");
            string command = conn.GetResultField("vpc_Command", "Unknown");
            string version = conn.GetResultField("vpc_Version", "Unknown");
            string cardBin = conn.GetResultField("vpc_Card", "Unknown");
            string orderInfo = conn.GetResultField("vpc_OrderInfo", "Unknown");
            string merchantID = conn.GetResultField("vpc_Merchant", "Unknown");
            string authorizeID = conn.GetResultField("vpc_AuthorizeId", "Unknown");
            string merchTxnRef = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
            string transactionNo = conn.GetResultField("vpc_TransactionNo", "Unknown");
            string txnResponseCode = vpc_TxnResponseCode;
            string message = conn.GetResultField("vpc_Message", "Unknown");
            // Update OnePayTrans
            byte Replicated = 0;
            int ActUserId = 1;
            int SysMessageId = 0;
            if(hashvalidateResult != "CORRECTED")
            {
                Log.writeLog("Invalid hash: " + hashvalidateResult, "HandleUrl");
                OnePayTrans m_OnePayTrans = new OnePayTrans();
                m_OnePayTrans.TransId = int.Parse(merchTxnRef);
                m_OnePayTrans = m_OnePayTrans.Get();
                if(m_OnePayTrans.TransId > 0)
                {
                    RetVal = m_OnePayTrans;
                }
                else
                {
                    m_OnePayTrans.vpc_TxnResponseCode = 100;
                    RetVal = m_OnePayTrans;
                }
                return RetVal;
            }
            if(merchTxnRef != "Unknown")
            {
                OnePayTrans m_OnePayTrans = new OnePayTrans();
                m_OnePayTrans.TransId = int.Parse(merchTxnRef);
                m_OnePayTrans = m_OnePayTrans.Get();
                
                if(m_OnePayTrans.vpc_TxnResponseCode == -1)
                {
                    m_OnePayTrans.vpc_Amount = int.Parse(amount);
                    m_OnePayTrans.ResponseTime = DateTime.Now;
                    m_OnePayTrans.vpc_TxnResponseCode = int.Parse(vpc_TxnResponseCode);
                    //get response desc
                    OnePayResponseCodes m_OnePayResponseCodes = new OnePayResponseCodes();
                    m_OnePayResponseCodes.OnePayResponseCode = m_OnePayTrans.vpc_TxnResponseCode;
                    m_OnePayResponseCodes = m_OnePayResponseCodes.Get();
                    //end get respons
                    m_OnePayTrans.vcp_Message = m_OnePayResponseCodes.ResponseCodeDesc;
                    m_OnePayTrans.Update(Replicated, ActUserId, ref SysMessageId);
                    //insert to PaymentTransactions
                    if(vpc_TxnResponseCode == OnePayResponseCodes.OnePaySuccesCode)
                    {
                        short SiteId = 0;
                        byte PaymentTypeId = 12;// OnePay
                        CustomerHelpers.Estate_NaptienMuaxu(ActUserId, int.Parse(amount)/100, m_OnePayTrans.CustomerId, SiteId, PaymentTypeId);
                        
                    }
                   else
                    {
                        Log.writeLog("Not Success Trans: CustomerId-" + CustomerId.ToString() + "-merchTxnRef-" + merchTxnRef + "vpc_Amount:" + amount + "-vpc_TxnResponseCode:" + vpc_TxnResponseCode, "HandleUrl");
                    }
                    //end insert to PaymentTransactions

                }
                else
                {
                    Log.writeLog("Dublicate Trans: CustomerId-" + CustomerId.ToString() + "-merchTxnRef-" + merchTxnRef + "-vpc_Amount:" + amount , "HandleUrl");
                }

                RetVal = m_OnePayTrans;
            }
            else
            {
                RetVal.vpc_TxnResponseCode = 100;
                LogFiles.WriteLog("Trans Wrong: CustomerId-" + CustomerId.ToString() + "-merchTxnRef-" + merchTxnRef + "-vpc_Amount:" + amount + "-QueryString:" + QueryString.ToString() + Environment.NewLine + "HandleUrl");
            }
            
            return RetVal;
        }
        public OnePayTrans HandleUrlQT(System.Collections.Specialized.NameValueCollection QueryString, int CustomerId)
        {
            Log.writeLog(HttpContext.Current.Request.Url.ToString(), "HandleUrlQT");
            OnePayTrans RetVal = new OnePayTrans();
            string SECURE_SECRET = ConstantPartner.ONEPAY_SECURE_SECRET_QT;
            string hashvalidateResult = "";
            // Khoi tao lop thu vien
            VPCRequest conn = new VPCRequest("http://onepay.vn");
            conn.SetSecureSecret(SECURE_SECRET);
            // Xu ly tham so tra ve va kiem tra chuoi du lieu ma hoa
            hashvalidateResult = conn.Process3PartyResponse(QueryString);
            if (hashvalidateResult != "CORRECTED")
            {
                Log.writeLog("Invalid hash: " + hashvalidateResult, "HandleUrl");
                RetVal.vpc_TxnResponseCode = 100;
                return RetVal;
            }
            // Lay gia tri tham so tra ve tu cong thanh toan
            String vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");
            string amount = conn.GetResultField("vpc_Amount", "Unknown");
            string localed = conn.GetResultField("vpc_Locale", "Unknown");
            string command = conn.GetResultField("vpc_Command", "Unknown");
            string version = conn.GetResultField("vpc_Version", "Unknown");
            string cardBin = conn.GetResultField("vpc_Card", "Unknown");
            string orderInfo = conn.GetResultField("vpc_OrderInfo", "Unknown");
            string merchantID = conn.GetResultField("vpc_Merchant", "Unknown");
            string authorizeID = conn.GetResultField("vpc_AuthorizeId", "Unknown");
            string merchTxnRef = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
            string transactionNo = conn.GetResultField("vpc_TransactionNo", "Unknown");
            string txnResponseCode = vpc_TxnResponseCode;
            string message = conn.GetResultField("vpc_Message", "Unknown");
            // Update OnePayTrans
            byte Replicated = 0;
            int ActUserId = 1;
            int SysMessageId = 0;
            if (merchTxnRef != "Unknown")
            {
                OnePayTrans m_OnePayTrans = new OnePayTrans();
                m_OnePayTrans.TransId = int.Parse(merchTxnRef);
                m_OnePayTrans = m_OnePayTrans.Get();

                if (m_OnePayTrans.vpc_TxnResponseCode == -1)
                {
                    m_OnePayTrans.vpc_Amount = int.Parse(amount);
                    m_OnePayTrans.ResponseTime = DateTime.Now;
                    m_OnePayTrans.vpc_TxnResponseCode = int.Parse(vpc_TxnResponseCode);
                    //get response desc
                    OnePayResponseCodes m_OnePayResponseCodes = new OnePayResponseCodes();
                    m_OnePayResponseCodes.OnePayResponseCode = m_OnePayTrans.vpc_TxnResponseCode;
                    m_OnePayResponseCodes = m_OnePayResponseCodes.Get();
                    //end get respons
                    m_OnePayTrans.vcp_Message = m_OnePayResponseCodes.ResponseCodeDesc;
                    m_OnePayTrans.Update(Replicated, ActUserId, ref SysMessageId);
                    //insert to PaymentTransactions
                    if (vpc_TxnResponseCode == OnePayResponseCodes.OnePaySuccesCode)
                    {
                        short SiteId = 0;
                        byte PaymentTypeId = 13;// OnePay
                        CustomerHelpers.Estate_NaptienMuaxu(ActUserId, int.Parse(amount) / 100, m_OnePayTrans.CustomerId, SiteId, PaymentTypeId);

                    }
                    else
                    {
                        Log.writeLog("Not Success Trans: CustomerId-" + CustomerId.ToString() + "-merchTxnRef-" + merchTxnRef + "vpc_Amount:" + amount + "-vpc_TxnResponseCode:" + vpc_TxnResponseCode, "HandleUrl");
                    }
                    //end insert to PaymentTransactions

                }
                else
                {
                    //m_OnePayTrans.vpc_TxnResponseCode = 100;
                    LogFiles.WriteLog("Dublicate Trans: CustomerId-" + CustomerId.ToString() + "-merchTxnRef-" + merchTxnRef + "-vpc_Amount:" + amount + Environment.NewLine + "HandleUrl");
                }

                RetVal = m_OnePayTrans;
            }
            else
            {
                RetVal.vpc_TxnResponseCode = 100;
                LogFiles.WriteLog("Trans Wrong: CustomerId-" + CustomerId.ToString() + "-merchTxnRef-" + merchTxnRef + "-vpc_Amount:" + amount + "-QueryString:" + QueryString.ToString() + Environment.NewLine + "HandleUrl");
            }
            
            return RetVal;
        }
    }
}
