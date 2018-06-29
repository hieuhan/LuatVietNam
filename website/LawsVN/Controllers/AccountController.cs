using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;
using LawsVN.Library.Sercurity;
using LawsVN.Models;
using LawsVN.Models.Account;
using Newtonsoft.Json;
using CustomerHelpers = ICSoft.CMSViewLib.CustomerHelpers;
using ICSoft.LawDocsLib;
using LawsVN.AppCode.Attribute;

namespace LawsVN.Controllers
{
    public class AccountController : Controller
    {
        private byte _replicated = 0;
        private int _actUserId = 0;
        private short _sysMessageIdShort = 0;
        private readonly int _currentCustomerId = Extensions.GetCurrentCustomerId();
        private readonly byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();

        #region Register - Login - ForgotPassword - Logout

        [AllowAnonymous]
        [SEOAction]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var mLoginModel = new LoginModel
            {
                WebsiteTitle = "Đăng nhập tài khoản",
                ReturnUrl = ReturnUrl
            };
            return Extensions.GetViewMode("Login", mLoginModel);
        }

        [HttpPost]
        public AjaxResult DoLogin(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = string.Join("<br/>", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)),
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }
            var loginResult = CustomerHelpers.Login(model.CustomerName, model.CustomerPass, Request.UserHostAddress, string.Format("{0}||{1}", Request.UserAgent, Session.SessionID), _currentLanguageId);
            if (loginResult.ActionStatus.Equals("OK"))
            {
                var serializeModel = new AccountSerializerModel
                {
                    CustomerId = loginResult.mCustomers.CustomerId,
                    CustomerName = loginResult.mCustomers.CustomerName,
                    CustomerFullName = loginResult.mCustomers.CustomerFullName,
                    CustomerMail = loginResult.mCustomers.CustomerMail,
                    CustomerMobile = loginResult.mCustomers.CustomerMobile,
                    Avatar = loginResult.mCustomers.Avatar,
                    GenderId = loginResult.mCustomers.GenderId,
                    Roles = loginResult.lRoles.HasValue() ? (from role in loginResult.lRoles
                                                             select role.RoleName).ToArray() : new string[] { }
                };

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    model.CustomerName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                    model.RememberMe,
                    userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie formsAuthenticationTicketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                {
                    Domain = FormsAuthentication.CookieDomain,
                    Path = FormsAuthentication.FormsCookiePath,
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL
                };
                Response.Cookies.Add(formsAuthenticationTicketCookie);
                string returnUrl = string.Empty;
                if (string.IsNullOrEmpty(model.ReturnUrl) && Request.UrlReferrer != null && Request.UrlReferrer.PathAndQuery.IndexOf("user/dang-nhap-tai-khoan.html", StringComparison.Ordinal) < 0)
                {
                    returnUrl = Request.UrlReferrer.PathAndQuery;
                }
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    //loginResult.ActionMessage,
                    Completed = true,
                    ReturnUrl = Url.IsLocalUrl(model.ReturnUrl) && model.ReturnUrl.Length > 1 &&
                                model.ReturnUrl.StartsWith("/")
                                && !model.ReturnUrl.StartsWith("//") && !model.ReturnUrl.StartsWith("/\\")
                        ? model.ReturnUrl
                        : returnUrl
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = loginResult.ActionMessage,
                Completed = false
            };
        }

        [SEOAction]
        public ActionResult LoginSessionId(string sessionId = "")
        {
            if (!Extensions.IsAuthenticated && !string.IsNullOrEmpty(sessionId))
            {
                var loginResult = CustomerHelpers.Login(string.Empty, string.Empty, Request.UserHostAddress, string.Format("{0}||{1}", Request.UserAgent, sessionId), _currentLanguageId);
                if (loginResult.ActionStatus.Equals("OK"))
                {
                    var serializeModel = new AccountSerializerModel
                    {
                        CustomerId = loginResult.mCustomers.CustomerId,
                        CustomerName = loginResult.mCustomers.CustomerName,
                        CustomerFullName = loginResult.mCustomers.CustomerFullName,
                        CustomerMail = loginResult.mCustomers.CustomerMail,
                        CustomerMobile = loginResult.mCustomers.CustomerMobile,
                        Avatar = loginResult.mCustomers.Avatar,
                        Roles = loginResult.lRoles.HasValue()
                            ? (from role in loginResult.lRoles
                               select role.RoleName).ToArray()
                            : new string[] { }
                    };

                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        loginResult.mCustomers.CustomerName,
                        DateTime.Now,
                        DateTime.Now.AddHours(FormsAuthentication.Timeout.TotalMinutes),
                        false,
                        userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie formsAuthenticationTicketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                    {
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath,
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL
                    };
                    Response.Cookies.Add(formsAuthenticationTicketCookie);
                    Session["_LuatVietNamSessionId"] = sessionId;
                }
            }
            return Redirect(CmsConstants.ROOT_PATH);
        }

        [AllowAnonymous]
        [SEOAction]
        public ActionResult Register()
        {
            if (Extensions.IsAuthenticated)
            {
                return RedirectToRoute("AccountProfileInfomation");
            }
            var mRegisterModel = new RegisterModel
            {
                WebsiteTitle = "Đăng ký tài khoản",
                RegisterNewsletter = 1
            };
            return View(mRegisterModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult Register(RegisterModel model)
        {
            if (!model.IsMobile && !model.RegisterCaptcha.Equals(model.RegisterCaptchaCode))
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = Resource.TheSecurityCodeIsIncorrect,
                    Completed = false
                };
            }

            if (!model.IsMobile && !ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }

            DateTime dateOfBirth;
            DateTime.TryParseExact(model.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth);
            var mCustomer = new Customers
            {
                CustomerName = model.CustomerName,
                CustomerPass = model.Password,
                CustomerMail = model.Email,
                CustomerMobile = model.Phone,
                CustomerFullName = model.FullName,
                CustomerNickName = string.Empty,
                Address = model.Address,
                OccupationId = model.OccupationId,
                DateOfBirth = dateOfBirth,
                ProvinceId = model.ProvinceId,
                OrganName = model.OrganName,
                OrganAddress = model.OrganAddress,
                OrganPhone = model.OrganPhone,
                OrganTaxCode = model.OrganTaxCode,
                AccountNumber = model.AccountNumber,
                CustomerGroupId = 1,
                OrganOccupationId = model.OrganOccupationId,
                GenderId = model.GenderId,
                Avatar = model.Avatar,
                RegisterNewsletter = model.RegisterNewsletter,
                ApplicationId = model.ServiceOfInterest, //Dịch vụ quan tâm
                SiteId = Constants.SiteId,
                Website = string.Empty,
                Facebook = string.Empty,
                CrDateTime = DateTime.Now
            };

            var registerResult = CustomerHelpers.Register(mCustomer, 1, 0, _currentLanguageId);

            if (registerResult.ActionStatus.Equals("OK"))
            {
                //Lĩnh vực văn bản quan tâm
                if (model.FieldId != null && model.FieldId.Length > 0)
                {
                    var mCustomerFields = new CustomerFields
                    {
                        CustomerId = registerResult.CustomerId,
                        DocGroupId = Constants.DocGroupIdVbpq,
                        DisplayOrder = 0
                    };
                    mCustomerFields.InsertByListId(String.Join(",", model.FieldId));
                }

                //Đăng ký nhận tin văn bản mới
                if (model.RegisterNewsletter > 0)
                {
                    var mNewsletterMember = new NewsletterMembers
                    {
                        CustomerId = registerResult.CustomerId,
                        Email = mCustomer.CustomerMail,
                        NewsletterGroupId = model.NewsletterGroupId,
                        NewsletterMemberStatusId = 1,
                        CrDateTime = DateTime.Now,
                        CrUserId = 0
                    };
                    mNewsletterMember.Insert(_replicated, _actUserId, ref _sysMessageIdShort);
                }

                string msgCustomerServices = string.Empty;
                //Đăng ký dùng thử dịch vụ
                if (model.ServiceId == Constants.ServiceIdDungThu15Ngay)
                    new CustomerServices().CustomerServices_LVN_DangKyDungThu(registerResult.CustomerId, string.Empty,
                        Request.UserHostAddress, ref msgCustomerServices);
                else
                    new CustomerServices().CustomerServices_LVN_DangKyMienphi(registerResult.CustomerId, string.Empty,
                        Request.UserHostAddress, ref msgCustomerServices);
                TempData["RegisterMessages"] = registerResult.ActionMessage;
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = registerResult.ActionMessage,
                    Completed = true,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/dang-ky-tai-khoan-thanh-cong.html")
                };
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = registerResult.ActionMessage,
                Completed = false
            };
        }

        public ActionResult RegisterSuccessful()
        {
            if (TempData["RegisterMessages"] == null) return RedirectToAction("Index", "Home");
            var model = new RegisterSuccessfulModel
            {
                RegisterMessages = TempData["RegisterMessages"].ToString()
            };
            return Extensions.GetViewMode("RegisterSuccessful", model);
        }

        [AllowAnonymous]
        [SEOAction]
        public ActionResult NewUserActive(int customerId = 0, string registerCode = "")
        {
            var result = CustomerHelpers.ConfirmActive(_currentLanguageId, customerId, registerCode);
            var model = new NewUserActiveModel
            {
                WebsiteTitle = "Kích hoạt tài khoản",
                ActiveStatus = result.ActionStatus.Equals("OK"),
                ActiveMessage = result.ActionMessage
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public AjaxResult ForgotPassword(ForgotPasswordModel model)
        {

            if (!model.ForgotPasswordCaptcha.Equals(model.ForgotPasswordCaptchaCode))
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = Resource.TheSecurityCodeIsIncorrect,
                    Completed = false
                };
            }


            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }
            string messages =
                new Customers().Customers_ForgotPassword(_currentLanguageId, model.CustomerName);

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = messages,
                Completed = true
            };
        }

        [AllowAnonymous]
        [SEOAction]
        public ActionResult ResetPassword(int customerId = 0, string confirmCode = "")
        {
            var model = new ConfirmResetPasswordModel
            {
                WebsiteTitle = "Quên mật khẩu",
            };
            if (customerId > 0 && !string.IsNullOrEmpty(confirmCode))
            {
                var result = CustomerHelpers.CheckActiveCode(customerId, confirmCode);
                if (result.ActionStatus.Equals("OK"))
                {
                    model.CustomerId = customerId;
                    model.ConfirmCode = confirmCode;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult ConfirmResetPassword(ConfirmResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }
            var messages = new Customers().Customers_ResetPass(_currentLanguageId, model.CustomerId, string.Empty, model.ConfirmCode, model.Password, Request.UserHostAddress, Request.UserAgent);

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = messages,
                Completed = true
            };
        }

        [SEOAction]
        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {
                Extensions.Logout(Session, Response);
            }
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        [SEOAction]
        public ActionResult ChangePassword()
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = string.Join("<br/>", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)),
                    Completed = false
                };
            }
            ChangePasswordModel model = new ChangePasswordModel();
            if (Request.Browser.IsMobileDevice)
            {
                return View("~/Views/Account/ChangePassword.Mobile.cshtml", model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //return Extensions.GetViewMode("ChangePassword", model);
        }
        #endregion

        #region Account Information
        /// <summary>
        /// View Thông tin tài khoản
        /// </summary>
        /// <returns></returns>
        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult AccountProfile()
        {
            int countVbpl = 0, countVbhn = 0, countTcvn = 0;
            var model = new AccountProfileModel
            {
                WebsiteTitle = "Thông tin tài khoản",
                mCustomersViewDetail = CustomerHelpers.LawsCustomer_GetViewDetail(_currentCustomerId, Constants.DocGroupIdVbpq, 1, _currentLanguageId, ref countVbpl, ref countVbhn, ref countTcvn),
                CountVbpl = countVbpl,
                CountVbhn = countVbhn,
                CountTcvn = countTcvn,
                GetCountPaymentTransactionSuccess = 1,
                GetCountThongBaoHieuLuc = 1
            };
            if (model.mCustomersViewDetail.mCustomers.ProvinceId > 0)
            {
                var province =
                    model.ListProvinces.FirstOrDefault(m =>
                        m.ProvinceId == model.mCustomersViewDetail.mCustomers.ProvinceId) ??
                    new Provinces { ProvinceDesc = string.Empty };
                model.ProvinceDesc = province.ProvinceDesc;
            }
            model.EndTime = model.mCustomersViewDetail.mCustomerServicesView.EndTime;
            //TODO thời hạn sử dụng dịch vụ
            if (model.EndTime != DateTime.MinValue)
            {
                DateTime tmp = DateTime.Now;
                DateTime dtNow = new DateTime(tmp.Year, tmp.Month, tmp.Day);
                DateTime dtEndTime = new DateTime(model.EndTime.Year, model.EndTime.Month, model.EndTime.Day);
                int compare = DateTime.Compare(dtEndTime, dtNow);
                if (compare >= 0)
                {
                    var diff = (dtEndTime - dtNow).TotalDays;
                    model.MonthRemain = Math.Floor(diff / 30);
                    model.DayRemain = diff % 30;
                }
            }
            return Extensions.GetViewMode("AccountProfile", model);
        }

        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult AccountEditProfileMobile()
        {
            string mode = "edit";
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var mCustomer = new Customers().Get(currentCustomerId);
            var model = new AccountProfileModel
            {
                Mode = mode,
                CustomerName = mCustomer.CustomerName,
                FullName = mCustomer.CustomerFullName,
                GenderId = mCustomer.GenderId,
                CustomerMobile = mCustomer.CustomerMobile,
                Address = mCustomer.Address,
                Email = mCustomer.CustomerMail,
                Avatar = mCustomer.Avatar,
                ProvinceId = mCustomer.ProvinceId,
                DateOfBirth = mCustomer.DateOfBirth.toString()
            };
            return View(model);
        }

        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult AccountEditBusinessInfoMobile()
        {
            string mode = "edit";
            int currentCustomerId = Extensions.GetCurrentCustomerId();
            var mCustomer = new Customers().Get(currentCustomerId);
            var model = new BusinessInformation
            {
                Mode = mode,
                OrganName = mCustomer.OrganName,
                OrganAddress = mCustomer.OrganAddress,
                OrganPhone = mCustomer.OrganPhone,
                OrganTaxCode = mCustomer.OrganTaxCode,
                AccountNumber = mCustomer.AccountNumber
            };
            return View(model);
        }

        /// <summary>
        /// View Văn bản của tôi
        /// </summary>
        /// <returns></returns>
        [SEOAction]
        [LawsVnAuthorize]
        public ActionResult MyDocuments(byte docGroupId = 1)
        {
            int rowCount = 0;
            var model = new MyDocuments
            {
                mDocsViewSearch = DocsViewHelpers.MyDocuments_ViewGetPage(_currentCustomerId, docGroupId, Constants.RegistTypeIdVbqt, _currentLanguageId, 0, 0, 0, 0, "", "", "", Constants.RowAmount20, 0, 1, 1, ref rowCount),
                RowCount= rowCount,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    UrlPaging = string.Empty,
                    RegistTypeId = Constants.RegistTypeIdVbqt,
                    DocGroupId = docGroupId,
                    FieldId = 0,
                    OrganId = 0,
                    DocTypeId = 0,
                    EffectStatusId = 0,
                    LanguageId = _currentLanguageId,
                    ControllerName = "Ajax",
                    ActionName = "MyDocuments_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "MyDocumentsBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                GetCountPaymentTransactionSuccess = 1,
                GetCountThongBaoHieuLuc = 1
            };
            model.ListMyDocuments =
                from a in model.mDocsViewSearch.lDocsView
                join b in model.ListEffectStatus on a.EffectStatusId equals b.EffectStatusId into c
                from d in c.DefaultIfEmpty()
                select new MyDocumentsModel
                {
                    EffectStatusName = d != null && !string.IsNullOrEmpty(d.EffectStatusDesc) ? d.EffectStatusDesc : string.Empty,
                    DocsView = a
                };
            return Extensions.GetViewMode("MyDocuments", model, Request);
            //return View(model);
        }

        /// <summary>
        /// View Thông báo hiệu lực
        /// </summary>
        /// <returns></returns>
        [SEOAction]
        [LawsVnAuthorize]
        public ActionResult NoticeOfValidity()
        {
            int rowCount = 0;
            var model = new NoticeOfValidityModel
            {
                mDocsViewSearch = DocsViewHelpers.MyDocuments_ViewGetPage(_currentCustomerId, 0, Constants.RegistTypeIdTheoDoiHieuLuc, _currentLanguageId, 0, 0, 0, 0, "", "", "", Constants.RowAmount20, 0, 1, 1, ref rowCount),
                RowCount = rowCount,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    UrlPaging = string.Empty,
                    RegistTypeId = Constants.RegistTypeIdTheoDoiHieuLuc,
                    FieldId = 0,
                    LanguageId = _currentLanguageId,
                    ControllerName = "Ajax",
                    ActionName = "NoticeOfValidity_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "NoticeOfValidityBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                GetCountPaymentTransactionSuccess = 1,
                GetCountThongBaoHieuLuc = 1
            };
            model.ListMyDocuments = model.mDocsViewSearch.lDocsView.Join(model.ListEffectStatus, a => a.EffectStatusId,
                b => b.EffectStatusId, (a, b) => new MyDocumentsModel
                {
                    EffectStatusName = b.EffectStatusDesc,
                    DocsView = a
                });
            return Extensions.GetViewMode("NoticeOfValidity", model, Request);
            //return View(model);
        }

        /// <summary>
        /// View Tin nhắn
        /// </summary>
        /// <returns></returns>
        [SEOAction]
        [LawsVnAuthorize]
        public ActionResult MyMessage()
        {
            int rowCount = 0;
            var model = new MyMessagesModel
            {
                ListMessages = new Messages(Constants.EXT_CONSTR) { UserId = _currentCustomerId, HasRead = Constants.HasReadGetAllMessages, MessageTypeId = Constants.MessageTypeIdInbox }.GetPage(Constants.RowAmount20, 0, "", "", "", ref rowCount),
                RowCount = rowCount,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    UrlPaging = string.Empty,
                    MessageTypeId = Constants.MessageTypeIdInbox,
                    IsMyMessage = true,
                    ControllerName = "Ajax",
                    ActionName = "MyMessages_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "MyMessagesBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                GetCountPaymentTransactionSuccess = 1,
                GetCountThongBaoHieuLuc = 1
            };
            return Extensions.GetViewMode("MyMessage", model, Request);
            //return View(model);
        }

        [SEOAction]
        [LawsVnAuthorize]
        public ActionResult SavedMessages()
        {
            int rowCount = 0;
            var model = new MyMessagesModel
            {
                ListMessages = new Messages(Constants.EXT_CONSTR) { UserId = _currentCustomerId, HasRead = Constants.HasReadGetAllMessages, MessageTypeId = Constants.MessageTypeIdInbox, HasSave = 1 }.GetPage(Constants.RowAmount20, 0, "", "", "", ref rowCount),
                RowCount = rowCount,
                mPartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    UrlPaging = string.Empty,
                    MessageTypeId = Constants.MessageTypeIdInbox,
                    IsMyMessage = true,
                    ControllerName = "Ajax",
                    ActionName = "SavedMessages_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "SaveMessagesBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                GetCountPaymentTransactionSuccess = 1,
                GetCountThongBaoHieuLuc = 1
            };
            return Extensions.GetViewMode("SavedMessages", model, Request);
            //return View(model);
        }

        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult DetailMessage(int messageId = 0)
        {
            if (messageId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Bad Request");
            }
            var mMessages = new Messages(Constants.EXT_CONSTR).Get(messageId);
            if (mMessages.MessageId > 0)
            {
                mMessages.UpdateHasRead();
                var model = new MyMessagesModel
                {
                    mMessages = mMessages,
                    GetCountPaymentTransactionSuccess = 1,
                    GetCountThongBaoHieuLuc = 1
                };
                return Extensions.GetViewMode("DetailMessage", model);
            }
            throw new HttpException((int)HttpStatusCode.NotFound, "Page Not Found");
        }

        [SEOAction]
        [LawsVnAuthorize]
        public ActionResult HistoryTransactions()
        {
            int rowCount = 0, totalMoney = 0;
            var dtCustomerService = new CustomerServices().CustomerServices_LVN_GetList(_currentCustomerId, string.Empty, 1);
            var list = new PaymentTransactions
            {
                CustomerId = _currentCustomerId,
                SiteId = Constants.SiteId,
                TransactionDesc = string.Empty,
                TransactionCode = string.Empty,
                TransactionStatusId = Constants.TransactionStatusIdSuccess
            }.GetPageView(0, Constants.RowAmount20, 0,"",0,"","", ref rowCount, ref totalMoney);

            var model = new HistoryTransactionsModel
            {
                RowCount = rowCount,
                TotalMoney = totalMoney,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    TransactionStatusId = 1,
                    UrlPaging = string.Empty,
                    ControllerName = "Ajax",
                    ActionName = "HistoryTransactions_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "HistoryTransactionsBox",
                        InsertionMode = InsertionMode.Replace
                    }
                },
                GetCountPaymentTransactionSuccess = 1,
                GetCountThongBaoHieuLuc = 1
            };
            //ToDo Danh sách giao dịch full
            model.ListPaymentTransactions = from paymentTransaction in list
                                            join servicePackage in model.ListServicePackages on paymentTransaction.ServicePackageId equals
                                                servicePackage.ServicePackageId into list1
                                            from l1 in list1.DefaultIfEmpty()
                                            select new PaymentTransactionsModel
                                            {
                                                PaymentTransactionId = paymentTransaction.PaymentTransactionId,
                                                TransactionDesc = paymentTransaction.TransactionDesc,
                                                TransactionStatusId = paymentTransaction.TransactionStatusId,
                                                TransactionStatusDesc = paymentTransaction.TransactionStatusId.TransactionStatusDescGetById(),
                                                PaymentDate = paymentTransaction.PaymentDate.toString(),
                                                Amount =
                                                    paymentTransaction.TransactionStatusId != Constants.TransactionStatusIdPending ||
                                                    l1 != null && !Constants.ServiceId_NghiepVu.Any(x => x.Equals(l1.ServiceId))
                                                        ? (paymentTransaction.Amount > 0
                                                            ? string.Format("{0:#,###} {1}", paymentTransaction.Amount, CmsConstants.CURRENCY_VND)
                                                            : "0")
                                                        : "0",
                                                PaymentTypeId = paymentTransaction.PaymentTypeId,
                                                ServiceId = (short)(l1 == null ? 0 : l1.ServiceId),
                                                ServicePackageId = paymentTransaction.ServicePackageId,
                                                ServicePackageDesc = l1 == null ? string.Empty : l1.ServicePackageDesc.TrimmedOrDefault(string.Empty)
                                            }
                                                into list2

                                                join c in model.ListServices on list2.ServiceId equals c.ServiceId into list3
                                                from l2 in list3.DefaultIfEmpty()
                                                select new PaymentTransactionsModel
                                                {
                                                    PaymentTransactionId = list2.PaymentTransactionId,
                                                    TransactionDesc = list2.TransactionDesc,
                                                    TransactionStatusId = list2.TransactionStatusId,
                                                    TransactionStatusDesc = list2.TransactionStatusDesc,
                                                    Amount = list2.Amount,
                                                    PaymentTypeId = list2.PaymentTypeId,
                                                    PaymentDate = list2.PaymentDate,
                                                    ServiceId = list2.ServiceId,
                                                    ServiceDesc = l2 == null ? string.Empty : l2.ServiceDesc.TrimmedOrDefault(string.Empty),
                                                    ServicePackageId = list2.ServicePackageId,
                                                    ServicePackageDesc = list2.ServicePackageDesc
                                                }
                                                    into list4

                                                    join d in model.ListPaymentTypes on list4.PaymentTypeId equals d.PaymentTypeId into list5
                                                    from l3 in list5.DefaultIfEmpty()
                                                    select new PaymentTransactionsModel
                                                    {
                                                        PaymentTransactionId = list4.PaymentTransactionId,
                                                        TransactionDesc = list4.TransactionDesc,
                                                        TransactionStatusId = list4.TransactionStatusId,
                                                        TransactionStatusDesc = list4.TransactionStatusDesc,
                                                        Amount = list4.Amount,
                                                        PaymentTypeId = list4.PaymentTypeId,
                                                        PaymentDate = list4.PaymentDate,
                                                        ServiceId = list4.ServiceId,
                                                        ServiceDesc = list4.ServiceDesc,
                                                        ServicePackageId = list4.ServicePackageId,
                                                        ServicePackageDesc = list4.ServicePackageDesc,
                                                        PaymentTypeDesc = l3 == null ? string.Empty : l3.PaymentTypeDesc.TrimmedOrDefault(string.Empty)
                                                    }
                                                        into list6

                                                        join e in model.ListTransactionTypes on list6.TransactionTypeId equals e.TransactionTypeId into list7
                                                        from l4 in list7.DefaultIfEmpty()
                                                        select new PaymentTransactionsModel
                                                        {
                                                            PaymentTransactionId = list6.PaymentTransactionId,
                                                            TransactionDesc = list6.TransactionDesc,
                                                            TransactionStatusId = list6.TransactionStatusId,
                                                            TransactionStatusDesc = list6.TransactionStatusDesc,
                                                            Amount = list6.Amount,
                                                            PaymentTypeId = list6.PaymentTypeId,
                                                            PaymentDate = list6.PaymentDate,
                                                            ServiceId = list6.ServiceId,
                                                            ServiceDesc = list6.ServiceDesc,
                                                            ServicePackageId = list6.ServicePackageId,
                                                            ServicePackageDesc = list6.ServicePackageDesc,
                                                            PaymentTypeDesc = list6.PaymentTypeDesc,
                                                            TransactionTypesDesc = l4 == null ? string.Empty : l4.TransactionTypeDesc.TrimmedOrDefault(string.Empty)
                                                        };

            if (Session["PayMessage"] != null)
            {
                model.PayMessage = Session["PayMessage"].ToString();
                Session["PayMessage"] = null;
            }
            else
            {
                model.PayMessage = "";
            }
            if (dtCustomerService.DataTableExists())
            {
                model.DtCustomerServices = new DtCustomerServices
                {
                    ServiceId = Extensions.GetColumnValue<short>(dtCustomerService.Rows[0], "ServiceId"),
                    ServiceName = Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServiceName"),
                    ServicePackageId =
                        Extensions.GetColumnValue<short>(dtCustomerService.Rows[0], "ServicePackageId"),
                    ServicePackageName =
                        Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServicePackageName"),
                    ServicePackageTime =
                        Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServicePackageTime"),
                    CurrentLogin = Extensions.GetColumnValue<short>(dtCustomerService.Rows[0], "ConcurrentLogin"),
                    Price = Extensions.GetColumnValue<int>(dtCustomerService.Rows[0], "Price"),
                    BeginTime = Extensions.GetColumnValue<DateTime>(dtCustomerService.Rows[0], "BeginTime"),
                    EndTime = Extensions.GetColumnValue<DateTime>(dtCustomerService.Rows[0], "EndTime")
                };
                //TODO thời hạn sử dụng dịch vụ
                if (model.DtCustomerServices.EndTime != DateTime.MinValue)
                {
                    DateTime tmp = DateTime.Now;
                    DateTime dtNow = new DateTime(tmp.Year, tmp.Month, tmp.Day);
                    DateTime dtEndTime = new DateTime(model.DtCustomerServices.EndTime.Year, model.DtCustomerServices.EndTime.Month, model.DtCustomerServices.EndTime.Day);
                    int compare = DateTime.Compare(dtEndTime, dtNow);
                    if (compare >= 0)
                    {
                        var diff = (dtEndTime - dtNow).TotalDays;
                        model.MonthRemain = Math.Floor(diff / 30);
                        model.DayRemain = diff % 30;
                    }
                }
            }
            else model.DtCustomerServices = new DtCustomerServices();
            return Extensions.GetViewMode("HistoryTransactions", model);
        }

        [HttpPost]
        [LawsVnAuthorize]
        public AjaxResult EditAccountProfile(AccountProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }
            DateTime dateOfBirth;
            DateTime.TryParseExact(model.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth);
            var mCustomers = new Customers(DocConstants.CUSTOMER_CONSTR).Get(_currentCustomerId);
            mCustomers.CustomerFullName = model.FullName.TrimmedOrDefault(string.Empty);
            mCustomers.DateOfBirth = dateOfBirth;
            mCustomers.GenderId = model.GenderId;
            mCustomers.Address = model.Address;
            mCustomers.CustomerMobile = model.CustomerMobile;
            mCustomers.ProvinceId = model.ProvinceId;
            mCustomers.Avatar = model.Avatar;
            //mCustomers.CustomerMail = model.Email;
            mCustomers.InsertOrUpdate(_replicated, _actUserId, ref _sysMessageIdShort);
            //if (_sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
            //{
            //cập nhật lại thông tin người dùng vào cookie
            Extensions.UpdateUserData();
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.UpdateSuccessfulAccountInformation,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html#thongtincanhan"),
                Completed = true
            };
            //}
            //return new AjaxResult
            //{
            //    StatusCode = 200,
            //    AllowGet = true,
            //    Message = Resource.PleaseTryAgainLater,
            //    Completed = false
            //};
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult EditBusinessInformation(BusinessInformation model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }
            var mCustomers = new Customers().Get(_currentCustomerId);
            mCustomers.OrganName = model.OrganName;
            mCustomers.OrganAddress = model.OrganAddress;
            mCustomers.OrganPhone = model.OrganPhone;
            mCustomers.OrganTaxCode = model.OrganTaxCode;
            mCustomers.AccountNumber = model.AccountNumber;
            mCustomers.InsertOrUpdate(_replicated, _actUserId, ref _sysMessageIdShort);
            //if (_sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
            //{
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.SuccessfulBusinessInformationUpdates,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html#thongtindoanhnghiep"),
                Completed = true
            };
            //}
            //return new AjaxResult
            //{
            //    StatusCode = 200,
            //    AllowGet = true,
            //    Message = Resource.PleaseTryAgainLater,
            //    Completed = false
            //};
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult EditCustomerFields(LawsCustomerFields model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = Resource.PleaseSelectAtLeastOneField,
                    Completed = false
                };
            }

            if (model.FieldId != null && model.FieldId.Length > 0)
            {
                var mCustomerFields = new CustomerFields
                {
                    CustomerId = _currentCustomerId,
                    DocGroupId = Constants.DocGroupIdVbpq,
                    DisplayOrder = 0
                };
                mCustomerFields.InsertByListId(String.Join(",", model.FieldId));
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = Resource.UpdateTheFieldOfSuccess,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html#linhvucvanbanquantam"),
                    Completed = true
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult EditCustomerFieldsTCVN(LawsCustomerFields model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = Resource.PleaseSelectAtLeastOneField,
                    Completed = false
                };
            }

            if (model.FieldId != null && model.FieldId.Length > 0)
            {
                var mCustomerFields = new CustomerFields
                {
                    CustomerId = _currentCustomerId,
                    DocGroupId = Constants.DocGroupIdTcvn,
                    DisplayOrder = 0
                };
                mCustomerFields.InsertByListId(String.Join(",", model.FieldId));
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = Resource.UpdateTheFieldOfSuccess,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html#linhvucvanbanquantam"),
                    Completed = true
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult EditCustomerFieldsV2(LawsCustomerFieldsV2 model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }

            //Lĩnh vực văn bản quan tâm
            if (model.FieldIdVb != null && model.FieldIdVb.Length > 0)
            {
                var mCustomerFields = new CustomerFields
                {
                    CustomerId = _currentCustomerId,
                    DocGroupId = Constants.DocGroupIdVbpq,
                    DisplayOrder = 0
                };
                mCustomerFields.InsertByListId(String.Join(",", model.FieldIdVb));
            }

            //Lĩnh vực Tiêu chuẩn VN quan tâm
            if (model.FieldIdTcvn != null && model.FieldIdTcvn.Length > 0)
            {
                var mCustomerFields = new CustomerFields
                {
                    CustomerId = _currentCustomerId,
                    DocGroupId = Constants.DocGroupIdTcvn,
                    DisplayOrder = 0
                };
                mCustomerFields.InsertByListId(String.Join(",", model.FieldIdTcvn));
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.UpdateTheFieldOfSuccess,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html#linhvucvanbanquantam"),
                Completed = true
            };
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult EditCustomerProvinces(LawsCustomerProvinces model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = Resource.PleaseSelectAtLeastOneField,
                    Completed = false
                };
            }

            if (model.provinceId != null && model.provinceId.Length > 0)
            {
                var mCustomerProvinces = new CustomerProvinces
                {
                    CustomerId = _currentCustomerId
                };
                mCustomerProvinces.InsertByListId(String.Join(",", model.provinceId));
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = Resource.UpdateProvinceOfSuccess,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html#linhvucvanbanquantam"),
                    Completed = true
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [AllowAnonymous]
        [SEOAction]
        public ActionResult RegisterPersonalInterface()
        {
            RegisterPersonalInterface model;
            if (_currentCustomerId > 0)
            {
                var customer = new Customers().Get(_currentCustomerId);
                model = new RegisterPersonalInterface
                {
                    WebsiteTitle = "Đăng ký giao diện cá nhân",
                    CustomerName = customer.CustomerName,
                    Email = customer.CustomerMail,
                    DateOfBirth = customer.DateOfBirth.ToString("dd/MM/yyyy"),
                    GenderId = customer.GenderId,
                    Address = customer.Address,
                    Phone = customer.CustomerMobile,
                    ProvinceId = customer.ProvinceId,
                    OccupationId = customer.OccupationId,
                    ServiceOfInterest = customer.ApplicationId,
                    ListCustomerFields = new CustomerFields().GetListFieldsByCustomerId2(_currentCustomerId, Constants.ReviewStatusIdDaDuyet)
                };
            }
            else
            {
                model = new RegisterPersonalInterface
                {
                    WebsiteTitle = "Đăng ký giao diện cá nhân"
                };

            }
            return View(model);
        }

        [HttpPost]
        [LawsVnAuthorize]
        [ValidateAntiForgeryToken]
        [SEOAction]
        public AjaxResult RegisterPersonalInterface(RegisterPersonalInterface model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }
            DateTime dateOfBirth;
            DateTime.TryParseExact(model.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth);
            var customer = new Customers().Get(_currentCustomerId);
            customer.DateOfBirth = dateOfBirth;
            customer.GenderId = model.GenderId;
            customer.CustomerMobile = model.Phone;
            customer.Address = model.Address.TrimmedOrDefault(string.Empty);
            customer.ProvinceId = model.ProvinceId;
            customer.OccupationId = model.OccupationId;
            customer.ApplicationId = model.ServiceOfInterest;
            customer.InsertOrUpdate(_replicated, _actUserId, ref _sysMessageIdShort);
            //if (_sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
            //{
            //Lĩnh vực văn bản quan tâm
            if (model.FieldIdVb != null && model.FieldIdVb.Length > 0)
            {
                var mCustomerFields = new CustomerFields
                {
                    CustomerId = customer.CustomerId,
                    DocGroupId = Constants.DocGroupIdVbpq,
                    DisplayOrder = 0
                };
                mCustomerFields.InsertByListId(String.Join(",", model.FieldIdVb));
            }

            //Lĩnh vực Tiêu chuẩn VN quan tâm
            if (model.FieldIdTcvn != null && model.FieldIdTcvn.Length > 0)
            {
                var mCustomerFields = new CustomerFields
                {
                    CustomerId = customer.CustomerId,
                    DocGroupId = Constants.DocGroupIdTcvn,
                    DisplayOrder = 0
                };
                mCustomerFields.InsertByListId(String.Join(",", model.FieldIdTcvn));
            }

            //Lĩnh vực thủ tục hành chính quan tâm
            //if (model.FieldIdTthc != null && model.FieldIdTthc.Length > 0)
            //{
            //    var mCustomerFields = new CustomerFields
            //    {
            //        CustomerId = customer.CustomerId,
            //        DocGroupId = Constants.DocGroupIdTthc,
            //        DisplayOrder = 0
            //    };
            //    mCustomerFields.InsertByListId(String.Join(",", model.FieldIdTthc).Replace("0", string.Empty));
            //}
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html"),
                Message = "Đăng ký giao diện cá nhân thành công.",
                Completed = true
            };
            //}
            //return new AjaxResult
            //{
            //    StatusCode = 200,
            //    AllowGet = true,
            //    Message = Resource.PleaseTryAgainLater,
            //    Completed = false
            //};
        }

        [AllowAnonymous]
        [SEOAction]
        public ActionResult Unsubscribe()
        {
            var model = new UnsubscribeModel
            {
                WebsiteTitle = "Hủy đăng ký nhận bản tin hàng tuần"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult Unsubscribe(UnsubscribeModel model)
        {
            if (!model.UnsubcribeCaptcha.Equals(model.UnsubcribeCaptchaCode))
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = Resource.TheSecurityCodeIsIncorrect,
                    Completed = false
                };
            }

            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "ModelStateInValid",
                    Data = ModelState.Errors(),
                    Completed = false
                };
            }
            new NewsletterEmails(CmsConstants.CMS_CONSTR)
            {
                Email = model.Email,
                IsReceiveNews = 0
            }.UpdateIsReceiveNews_ByEmail();

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Hủy đăng ký nhận bản tin hàng tuần thành công.",
                Completed = true
            };
        }

        #endregion

        #region Services
        [LawsVnAuthorize]
        public ActionResult RegisterFreeService()
        {
            byte isRegistService = 0;
            short serviceIdProcess = 0, serviceId = Constants.ServiceIdTraCuuMienPhi;
            string feeType = string.Empty,
                actionButton = string.Empty,
                msg = string.Empty;
            new CustomerServices(CmsConstants.CMS_CONSTR).CustomerServices_LVN_KiemtraDvDangKy(_currentCustomerId, string.Empty,
                serviceId, ref isRegistService, ref serviceIdProcess, ref feeType, ref actionButton, ref msg);

            var model = new CustomerServicesNotice
            {
                Messages = msg,
                ServiceId = serviceIdProcess,
                IsRegistService = isRegistService,
                FeeType = feeType,
                ActionButton = actionButton
            };
            return Extensions.GetViewMode("RegisterFreeService", model);
        }

        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult RegisterService(short serviceId = 15)
        {
            short[] servicesArray = { Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao };
            if (!Array.Exists(servicesArray, s => s == serviceId))
            {
                serviceId = Constants.ServiceIdTraCuuTieuChuan;
            }

            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(serviceId, 2);
            var listServicePackagesParent = new List<ServicePackages>
            {
                new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn số người dùng tại một thời điểm"}
            };
            var listServicePackagesChild = new List<ServicePackages>
            {
                new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn thời hạn thuê bao"}
            };
            listServicePackagesParent.AddRange(listServicePackagesByServiceId.Where(item => item.ServicePackageParentId == 0)
                .OrderBy(item => item.ConcurrentLogin));

            var model = new RegisterServicePackagesModel
            {
                WebsiteTitle = "Đăng ký gói dịch vụ tra cứu văn bản",
                ListServicePackagesParent = listServicePackagesParent,
                ListServicePackages = listServicePackagesChild,
                ServiceId = serviceId,
                mServices = Services.Static_Get(serviceId)
            };
            return Extensions.GetViewMode("RegisterService", model);
        }

        /// <summary>
        /// Gia hạn  dịch vụ
        /// </summary>
        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult RenewalOfService()
        {
            RegisterServicePackagesModel model;
            //TODO thong tin dich vu dang su dung
            var dtCustomerService = new CustomerServices().CustomerServices_LVN_GetList(_currentCustomerId, string.Empty, 1);
            if (dtCustomerService.DataTableExists())
            {
                model = new RegisterServicePackagesModel
                {
                    WebsiteTitle = "Gia hạn dịch vụ tra cứu văn bản",
                    ServiceId = Extensions.GetColumnValue<short>(dtCustomerService.Rows[0], "ServiceId"),
                    ServiceName = Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServiceName"),
                    ServicePackageId =
                        Extensions.GetColumnValue<short>(dtCustomerService.Rows[0], "ServicePackageId"),
                    ServicePackageName =
                        Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServicePackageName"),
                    ServicePackageTime =
                        Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServicePackageTime"),
                    ServiceTimeRemain =
                        Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServiceTimeRemain"),
                    CurrentLogin = Extensions.GetColumnValue<short>(dtCustomerService.Rows[0], "ConcurrentLogin"),
                    Price = Extensions.GetColumnValue<int>(dtCustomerService.Rows[0], "Price"),
                    BeginTime = Extensions.GetColumnValue<DateTime>(dtCustomerService.Rows[0], "BeginTime"),
                    EndTime = Extensions.GetColumnValue<DateTime>(dtCustomerService.Rows[0], "EndTime"),
                    FeeType = Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "FeeType"),
                    ActionButton = Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ActionButton"),
                    MsgChangeService = Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "MsgChangeService"),
                    ListServicePackagesParent = new List<ServicePackages>
                    {
                        new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn số người dùng tại một thời điểm"}
                    },
                    ListServicePackages = new List<ServicePackages>
                    {
                        new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn thời hạn thuê bao"}
                    }
                };
                model.PriceVAT = model.Price * 10 / 100;
                model.Total = model.Price + model.PriceVAT;
                var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(model.ServiceId);
                model.ListServicePackagesParent.AddRange(listServicePackagesByServiceId.Where(item => item.ServicePackageParentId == 0)
                    .OrderBy(item => item.ConcurrentLogin));

                //TODO gói dịch vụ hiện tại
                model.mServicePackages =
                    listServicePackagesByServiceId.FirstOrDefault(m => m.ServicePackageId == model.ServicePackageId);
                if (model.mServicePackages != null)
                {
                    //TODO gói dịch vụ cha hiện tại
                    model.mServicePackagesParent = listServicePackagesByServiceId.FirstOrDefault(m =>
                        m.ServicePackageId == model.mServicePackages.ServicePackageParentId);
                    model.ServicePackageParentId = model.mServicePackagesParent == null ? model.mServicePackages.ServicePackageId : model.mServicePackagesParent.ServicePackageId;

                    //TODO danh sách gói dịch vụ con
                    model.ListServicePackages.AddRange(listServicePackagesByServiceId
                        .Where(item =>
                            item.ServicePackageParentId != 0 &&
                            item.ServicePackageParentId == model.ServicePackageParentId && item.ReviewStatusId == 2)
                        .OrderBy(item => item.NumMonthUse).ToList());

                    //TODO gói miễn phí 3 tháng thẻ cào Luật
                    if (model.ServicePackageId == Constants.ServicePackageIdTheCaoLuat)
                    {
                        model.ServiceName = string.Format("{0}, ({1})", model.ServiceName,
                            model.ServicePackageName);
                        if (model.ListServicePackages.Count > 1)
                        {
                            model.mServicePackages = model.ListServicePackages[0];
                            model.Price = model.mServicePackages.Price;
                            model.PriceVAT = model.mServicePackages.Price * 10 / 100;
                            model.Total = model.mServicePackages.Price + model.PriceVAT;
                        }
                    }
                }

                var listServicesId = new List<short>
                {
                    Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao
                };

                //TODO danh sách dịch vụ hỗ trợ nâng cấp
                int index = listServicesId.IndexOf(model.ServiceId);
                if (index > -1)
                {
                    model.ListServicesIdUpgradeAllowed = listServicesId.SkipWhile(s => s <= model.ServiceId).ToList();
                }
            }
            else //TODO chua dang ky su dung dich vu
            {
                model = new RegisterServicePackagesModel
                {
                    WebsiteTitle = "Gia hạn dịch vụ tra cứu văn bản",
                };
            }
            return Extensions.GetViewMode("RenewalOfService", model);
        }

        /// <summary>
        /// Chuyển đổi dịch vụ
        /// </summary>
        [LawsVnAuthorize]
        [SEOAction]
        public ActionResult ServiceConversion(short serviceId = 0)
        {
            RegisterServicePackagesModel model;
            //TODO Lay thong tin dich vu dang su dung
            var dtCustomerService = new CustomerServices().CustomerServices_LVN_GetList(_currentCustomerId, string.Empty, 1);
            if (dtCustomerService.DataTableExists())
            {
                model = new RegisterServicePackagesModel
                {
                    WebsiteTitle = "Chuyển đổi dịch vụ tra cứu văn bản",
                    ServiceIdUse = Extensions.GetColumnValue<short>(dtCustomerService.Rows[0], "ServiceId"),
                    ServiceName = Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServiceName"),
                    ServicePackageId =
                        Extensions.GetColumnValue<short>(dtCustomerService.Rows[0], "ServicePackageId"),
                    ServicePackageName =
                        Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServicePackageName"),
                    ServicePackageTime =
                        Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServicePackageTime"),
                    ServiceTimeRemain =
                        Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ServiceTimeRemain"),
                    CurrentLogin = Extensions.GetColumnValue<short>(dtCustomerService.Rows[0], "ConcurrentLogin"),
                    Price = Extensions.GetColumnValue<int>(dtCustomerService.Rows[0], "Price"),
                    BeginTime = Extensions.GetColumnValue<DateTime>(dtCustomerService.Rows[0], "BeginTime"),
                    EndTime = Extensions.GetColumnValue<DateTime>(dtCustomerService.Rows[0], "EndTime"),
                    FeeType = Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "FeeType"),
                    ActionButton = Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "ActionButton"),
                    MsgChangeService = Extensions.GetColumnValue<string>(dtCustomerService.Rows[0], "MsgChangeService"),
                    ListServicePackagesParent = new List<ServicePackages>
                    {
                        new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn số người dùng tại một thời điểm"}
                    },
                    ListServicePackages = new List<ServicePackages>
                    {
                        new ServicePackages {ServicePackageId = 0, ServicePackageDesc = "Chọn thời hạn thuê bao"}
                    }
                };

                var listServicesId = new List<short>
                {
                    Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao
                };
                //TODO danh sách dịch vụ hỗ trợ nâng cấp
                int index = listServicesId.IndexOf(model.ServiceIdUse);
                if (index > -1)
                {
                    model.ListServicesIdUpgradeAllowed = listServicesId.SkipWhile(s => s <= model.ServiceIdUse).ToList();
                }
                if (model.ListServicesIdUpgradeAllowed.HasValue())
                {
                    short first = model.ListServicesIdUpgradeAllowed.FirstOrDefault(s => s != model.ServiceIdUse && s == serviceId);
                    model.ServiceId = first > 0 ? first : model.ListServicesIdUpgradeAllowed.FirstOrDefault(s => s != model.ServiceIdUse);
                    model.mServices = Services.Static_Get(model.ServiceId);
                    var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(model.ServiceId, 2);
                    model.ListServicePackagesParent.AddRange(listServicePackagesByServiceId.Where(item => item.ServicePackageParentId == 0)
                        .OrderBy(item => item.ConcurrentLogin));
                }
            }
            else //TODO Chua dang ky su dung dich vu
            {
                model = new RegisterServicePackagesModel
                {
                    WebsiteTitle = "Chuyển đổi dịch vụ tra cứu văn bản"
                };
            }
            return Extensions.GetViewMode("ServiceConversion", model);
        }

        /// <summary>
        /// Thông báo kết quả đăng ký,gia hạn, chuyển đổi dịch vụ
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceNotice()
        {
            string messages = TempData.Get<string>("ServicePackagesMessages");
            if (string.IsNullOrEmpty(messages)) return RedirectToAction("Index", "Home");
            var model = new RegisterSuccessfulModel
            {
                RegisterMessages = messages
            };
            return Extensions.GetViewMode("ServiceNotice", model);
        }

        /// <summary>
        /// Thông báo kết quả đăng ký dịch vụ miễn phí
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceFreeNotice()
        {
            string messages = TempData.Get<string>("ServicePackagesMessages");
            if (string.IsNullOrEmpty(messages)) return RedirectToAction("Index", "Home");
            var model = new RegisterSuccessfulModel
            {
                RegisterMessages = messages
            };
            return Extensions.GetViewMode("ServiceFreeNotice", model);
        }

        #endregion
    }
}
