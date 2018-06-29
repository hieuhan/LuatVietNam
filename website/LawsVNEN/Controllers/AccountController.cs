using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ICSoft.CMSLib;
using LawsVNEN.App_GlobalResources;
using LawsVNEN.Library;
using LawsVNEN.Models.Account;
using CustomerHelpers = ICSoft.CMSViewLib.CustomerHelpers;
using LawsVNEN.AppCode;
using System.Web.Security;
using ICSoft.CMSViewLib;
using Newtonsoft.Json;
using LawsVN.Library.Sercurity;
using LawsVNEN.Models;
using ICSoft.LawDocsLib;
using LawsVN.Library;

namespace LawsVNEN.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private byte _replicated = 0;
        private int _actUserId = 0;
        private short _sysMessageIdShort = 0;
        private readonly int _currentCustomerId = Extensions.GetCurrentCustomerId();
        private readonly string _curentCustomerName = Extensions.GetCurrentCustomerName();
        private readonly byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();

        #region Register - Login - ForgotPassword - Logout

        public ActionResult Register()
        {
            if (Extensions.IsAuthenticated)
            {
                return RedirectToAction("AccountProfile", "Account");
            }
            var mRegisterModel = new RegisterModel
            {
                WebsiteTitle = "Register account"
            };
            return View(mRegisterModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult Register(RegisterModel model)
        {
            if (!model.RegisterCaptcha.Equals(model.RegisterCaptchaCode))
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
            DateTime dateOfBirth;
            DateTime.TryParseExact(model.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dateOfBirth);
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
                OrganTaxCode = string.Empty,
                AccountNumber = model.AccountNumber,
                CustomerGroupId = 1,
                GenderId = model.GenderId,
                Avatar = string.Empty,
                OrganFax = model.OrganFax,
                RegisterNewsletter = model.RegisterNewsletter,
                ApplicationId = model.ServiceOfInterest, //Dịch vụ quan tâm
                SiteId = Constants.SiteId_TiengViet,
                Website = string.Empty,
                Facebook = string.Empty,
                CrDateTime = DateTime.Now
            };

            var registerResult = CustomerHelpers.Register(mCustomer, 1, 0, _currentLanguageId);

            if (registerResult.ActionStatus.Equals("OK"))
            {
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
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = registerResult.ActionMessage,
                    Completed = true,
                    ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/login.html")
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

        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var mLoginModel = new LoginModel
            {
                WebsiteTitle = Resource.Login,
                ReturnUrl = ReturnUrl
            };
            return View(mLoginModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult Login(WidgetUserModel model)
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
                    Roles = loginResult.lRoles.HasValue()
                        ? (from role in loginResult.lRoles
                           select role.RoleName).ToArray()
                        : new string[] { }
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
                HttpCookie formsAuthenticationTicketCookie =
                    new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                    {
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath,
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL
                    };
                Response.Cookies.Add(formsAuthenticationTicketCookie);

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
                        : CmsConstants.ROOT_PATH
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
        public ActionResult NewUserActive(int customerId = 0, string confirmCode = "")
        {
            var result = CustomerHelpers.ConfirmActive(_currentLanguageId, customerId, confirmCode);
            var model = new NewUserActiveModel
            {
                WebsiteTitle = "Active new user",
                ActiveStatus = result.ActionStatus.Equals("OK"),
                ActiveMessage = result.ActionMessage
            };
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            var model = new ForgotPasswordModel
            {
                WebsiteTitle = Resource.ForgotPassword,
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public AjaxResult ForgotPassword(ForgotPasswordModel model)
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
        public ActionResult ResetPassword(int customerId = 0, string confirmCode = "")
        {
            var model = new ConfirmResetPasswordModel
            {
                WebsiteTitle = "Reset password",
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

        [AllowAnonymous]
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
            model.Id = _currentCustomerId;
            model.Name = _curentCustomerName;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult ChangePassword(ChangePasswordModel model)
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
            var changePasswordResult = CustomerHelpers.ChangePass(model.LawsUser.CustomerName,
                model.Password, model.CurrentPassword, Request.UserHostAddress, Request.UserAgent, LawsVnLanguages.GetCurrentLanguageId());
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = changePasswordResult.ActionMessage,
                ReturnUrl = CmsConstants.ROOT_PATH,
                Completed = "OK".Equals(changePasswordResult.ActionStatus)
            };
        }

        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {
                Extensions.Logout(Session, Response);
            }
            return RedirectToAction("Index", "Home");
        }

        [LawsVnAuthorize]
        public ActionResult AccountProfile()
        {
            int countVbpl = 0, countVbhn = 0, countTcvn = 0;
            var model = new AccountProfileModel
            {
                WebsiteTitle = Resource.AccountInformation,
                mCustomersViewDetail = CustomerHelpers.LawsCustomer_GetViewDetail(_currentCustomerId,
                    Constants.DocGroupIdVbpq, 1, _currentLanguageId, ref countVbpl, ref countVbhn, ref countTcvn),
            };
            return View(model);
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
            DateTime.TryParseExact(model.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dateOfBirth);
            var mCustomers = new Customers().Get(_currentCustomerId);
            mCustomers.CustomerFullName = model.FullName;
            mCustomers.CustomerMail = model.Email;
            mCustomers.DateOfBirth = dateOfBirth;
            mCustomers.GenderId = model.GenderId;
            mCustomers.Address = model.Address;
            mCustomers.CustomerMobile = model.CustomerMobile;
            mCustomers.ProvinceId = model.ProvinceId;
            mCustomers.OccupationId = model.OccupationId;
            mCustomers.InsertOrUpdate(_replicated, _actUserId, ref _sysMessageIdShort);
            //if (_sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
            //{
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.UpdateSuccessfulAccountInformation,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html#thongtincanhan"),
                Completed = true
            };
            //}
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [HttpPost]
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
            mCustomers.OrganName = model.OrganName.TrimmedOrDefault(string.Empty);
            mCustomers.OrganAddress = model.OrganAddress.TrimmedOrDefault(string.Empty);
            mCustomers.OrganPhone = model.OrganPhone.TrimmedOrDefault(string.Empty);
            mCustomers.OrganFax = model.OrganFax.TrimmedOrDefault(string.Empty);
            mCustomers.AccountNumber = model.AccountNumber.TrimmedOrDefault(string.Empty);
            mCustomers.InsertOrUpdate(_replicated, _actUserId, ref _sysMessageIdShort);
            //if (_sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
            //{
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.SuccessfulBusinessInformationUpdates,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH,
                    "user/thong-tin-tai-khoan.html#thongtindoanhnghiep"),
                Completed = true
            };
            //}
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult EditSubscribeToNewsletters(SubscribeToNewsletters model)
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
            mCustomers.RegisterNewsletter = model.RegisterNewsletter;
            mCustomers.InsertOrUpdate(_replicated, _actUserId, ref _sysMessageIdShort);
            //if (_sysMessageTypeId == Constants.SysMesssageTypeIdInsertOrUpdateSuccessful)
            //{
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.SuccessfullyUpdatedTheNewsletter,
                ReturnUrl = string.Concat(CmsConstants.ROOT_PATH, "user/thong-tin-tai-khoan.html#dangkinhanbantin"),
                Completed = true
            };
            //}
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = Resource.PleaseTryAgainLater,
                Completed = false
            };
        }

        #endregion

        /// <summary>
        /// View Văn bản của tôi
        /// </summary>
        /// <returns></returns>
        [LawsVnAuthorize]
        public ActionResult MyDocuments(byte docGroupId = 0)
        {
            int rowCount = 0;
            var model = new MyDocuments
            {
                mDocsViewSearch = DocsViewHelpers.MyDocumentsEN_ViewGetPage(_currentCustomerId, docGroupId,
                    Constants.RegistTypeIdVbqt, _currentLanguageId, 0, 0, 0, 0, "", "", "", Constants.RowAmount20, 0, 0,
                    0, ref rowCount),
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
                    ControllerName = "Ajax",
                    ActionName = "MyDocuments_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "MyDocumentsBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };
            model.ListMyDocuments = model.mDocsViewSearch.lDocsView
                .GroupJoin(model.ListEffectStatus, a => a.EffectStatusId, b => b.EffectStatusId, (a, c) => new { a, c })
                .SelectMany(g => g.c.DefaultIfEmpty(), (g, d) => new MyDocumentsModel
                {
                    EffectStatusName = _currentLanguageId == 1
                        ? (d != null && !string.IsNullOrEmpty(d.EffectStatusDesc) ? d.EffectStatusDesc : string.Empty)
                        : (d != null && !string.IsNullOrEmpty(d.EffectStatusName) ? d.EffectStatusName : string.Empty),
                    DocsView = g.a
                });
            return View(model);
        }

        /// <summary>
        /// Đăng ký dịch vụ
        /// </summary>
        /// <returns></returns>
        [LawsVnAuthorize]
        public ActionResult Subscriber()
        {
            var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(Constants.ServiceIdTraCuuTiengAnh, 2);

            var listServicePackagesParent = listServicePackagesByServiceId.Where(item => item.ServicePackageParentId == 0)
                .OrderBy(item => item.ConcurrentLogin)
                .ToList();

            ServicePackages servicePackageSingle = null;
            if (listServicePackagesParent.HasValue())
            {
                //TODO gói dịch vụ 1 người dùng
                servicePackageSingle = listServicePackagesParent.FirstOrDefault(m => m.ConcurrentLogin == 1);
                if (servicePackageSingle != null)
                {
                    listServicePackagesParent.Remove(servicePackageSingle);
                }
            }

            var model = new SubscriberModel
            {
                WebsiteTitle = "Subscriber",
                Service = Services.Static_Get(Constants.ServiceIdTraCuuTiengAnh),
                ServicePackageSingle = servicePackageSingle,
                ListServicePackagesParent = listServicePackagesParent,
                ListServicePackages = new List<ServicePackages> { new ServicePackages { ServicePackageId = 0, ServicePackageName = Resource.PleaseSelectTheSubscriptionPeriod } }
            };
            return View(model);
        }

        /// <summary>
        /// Gia hạn  dịch vụ
        /// </summary>
        [LawsVnAuthorize]
        public ActionResult RenewalOfService()
        {
            SubscriberModel model;
            //TODO thong tin dich vu dang su dung
            var dtCustomerService = new CustomerServices().CustomerServices_LVN_GetList(_currentCustomerId, string.Empty, 1, _currentLanguageId);
            if (dtCustomerService.DataTableExists())
            {
                model = new SubscriberModel
                {
                    WebsiteTitle = "Renewal of service",
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
                    ListServicePackages = new List<ServicePackages>
                    {
                        new ServicePackages
                        {
                            ServicePackageId = 0,
                            ServicePackageName = Resource.PleaseSelectTheSubscriptionPeriod,
                            ServicePackageDesc = Resource.PleaseSelectTheSubscriptionPeriod
                        }
                    }
                };
                model.PriceVAT = model.Price * 10 / 100;
                model.Total = model.Price + model.PriceVAT;
                var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(model.ServiceId);
                model.ListServicePackagesParent = listServicePackagesByServiceId.Where(item => item.ServicePackageParentId == 0)
                    .OrderBy(item => item.ConcurrentLogin).ToList();

                //TODO gói dịch vụ hiện tại
                model.ServicePackage =
                    listServicePackagesByServiceId.FirstOrDefault(m => m.ServicePackageId == model.ServicePackageId);
                if (model.ServicePackage != null)
                {
                    //gói dịch vụ cha hiện tại
                    model.ServicePackagesParent = listServicePackagesByServiceId.FirstOrDefault(m =>
                        m.ServicePackageId == model.ServicePackage.ServicePackageParentId);
                    model.ServicePackageParentId = model.ServicePackagesParent != null ? model.ServicePackagesParent.ServicePackageId : model.ServicePackage.ServicePackageId;
                    
                        //ds gói dịch vụ con
                        model.ListServicePackages.AddRange(listServicePackagesByServiceId
                            .Where(item =>
                                item.ServicePackageParentId != 0 &&
                                item.ServicePackageParentId == model.ServicePackageParentId && item.ReviewStatusId == 2)
                            .OrderBy(item => item.NumMonthUse));

                        if (model.ServicePackageId == Constants.ServicePackageIdTheCaoLuat)
                        {
                            //gói miễn phí 3 tháng thẻ cào Luật
                            model.ServiceName = string.Format("{0}, ({1})", model.ServiceName,
                                model.ServicePackageName);
                            if (model.ListServicePackages.HasValue())
                            {
                                model.ServicePackage = model.ListServicePackages[0];
                                model.Price = model.ServicePackage.Price;
                                model.PriceVAT = model.ServicePackage.Price * 10 / 100;
                                model.Total = model.ServicePackage.Price + model.PriceVAT;
                            }
                        }
                }

                var listServicesId = new List<short>
                {
                    Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao
                };

                //TODO danh sách dịch vụ hỗ trợ chuyển đổi
                int index = listServicesId.IndexOf(model.ServiceId);
                if (index > -1)
                {
                    model.ListServicesIdUpgradeAllowed = listServicesId.SkipWhile(s => s <= model.ServiceId).ToList();
                }
            }
            else //Chua dang ky su dung dich vu
            {
                model = new SubscriberModel
                {
                    WebsiteTitle = "Renewal of service"
                };
            }
            return View(model);
        }

        /// <summary>
        /// Chuyển đổi dịch vụ
        /// </summary>
        [LawsVnAuthorize]
        public ActionResult ServiceConversion(short serviceId = 0)
        {
            SubscriberModel model;
            //TODO Lay thong tin dich vu dang su dung
            var dtCustomerService = new CustomerServices().CustomerServices_LVN_GetList(_currentCustomerId, string.Empty, 1, _currentLanguageId);
            if (dtCustomerService.DataTableExists())
            {
                model = new SubscriberModel
                {
                    WebsiteTitle = "Service conversion",
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
                    ListServicePackages = new List<ServicePackages>
                    {
                        new ServicePackages
                        {
                            ServicePackageId = 0,
                            ServicePackageName = Resource.PleaseSelectTheSubscriptionPeriod,
                            ServicePackageDesc = Resource.PleaseSelectTheSubscriptionPeriod
                        }
                    }
                };

                var listServicesId = new List<short>
                {
                    Constants.ServiceIdTraCuuTieuChuan, Constants.ServiceIdTraCuuTiengAnh, Constants.ServiceIdTraCuuNangCao
                };
                //TODO Kiểm tra điều kiện nâng cấp dv
                int index = listServicesId.IndexOf(model.ServiceIdUse);
                if (index > -1)
                {
                    model.ListServicesIdUpgradeAllowed = listServicesId.SkipWhile(s => s <= model.ServiceIdUse).ToList();
                }
                if (model.ListServicesIdUpgradeAllowed.HasValue())
                {
                    short first = model.ListServicesIdUpgradeAllowed.FirstOrDefault(s => s != model.ServiceIdUse && s == serviceId);
                    model.ServiceId = first > 0 ? first : model.ListServicesIdUpgradeAllowed.FirstOrDefault(s => s != model.ServiceIdUse);
                    model.Service = Services.Static_Get(model.ServiceId);
                    var listServicePackagesByServiceId = ServicePackages.Static_GetListByServiceId(model.ServiceId, 2);
                    var listServicePackagesParent = listServicePackagesByServiceId.Where(item => item.ServicePackageParentId == 0)
                        .OrderBy(item => item.ConcurrentLogin).ToList();

                    if (listServicePackagesParent.HasValue())
                    {
                        model.ListServicePackagesParent = listServicePackagesParent;
                    }

                }
            }
            else //Chua dang ky su dung dich vu
            {
                model = new SubscriberModel
                {
                    WebsiteTitle = "Service conversion"
                };
            }
            return View(model);
        }

        [LawsVnAuthorize]
        public ActionResult HistoryTransactions()
        {
            int rowCount = 0, totalMoney = 0;
            var dtCustomerService = new CustomerServices().CustomerServices_LVN_GetList(_currentCustomerId, string.Empty, 1, _currentLanguageId);
            var list = new PaymentTransactions
            {
                CustomerId = _currentCustomerId,
                SiteId = Constants.SiteId_TiengViet,
                TransactionDesc = string.Empty,
                TransactionCode = string.Empty,
                TransactionStatusId = Constants.TransactionStatusIdSuccess
            }.GetPageView(0, Constants.RowAmount20, 0, "", 0, "", "", ref rowCount, ref totalMoney);

            var model = new HistoryTransactionsModel
            {
                RowCount = rowCount,
                TotalMoney = totalMoney,
                TotalMoneyText = totalMoney > 0 ? (_currentLanguageId == LawsVnLanguages.AvailableLanguages[1].LanguageId ? string.Format("{0:#,###} {1}", totalMoney, CmsConstants.CURRENCY_VND) : string.Format("{0} {1:#,###} ", CmsConstants.CURRENCY, totalMoney)) : string.Empty,
                PartialPaginationAjax = new PartialPaginationAjax
                {
                    TotalPage = rowCount,
                    PageSize = Constants.RowAmount20,
                    LinkLimit = Constants.RowAmount5,
                    ShowNumberOfResults = Constants.RowAmount20,
                    TransactionStatusId = Constants.TransactionStatusIdSuccess,
                    UrlPaging = string.Empty,
                    ControllerName = "Ajax",
                    ActionName = "HistoryTransactions_GetPage",
                    LawsAjaxOptions = new AjaxOptions
                    {
                        UpdateTargetId = "HistoryTransactionsBox",
                        InsertionMode = InsertionMode.Replace
                    }
                }
            };

            model.ListPaymentTransactions = from paymentTransaction in list
                                            join servicePackage in model.ListServicePackages on paymentTransaction.ServicePackageId equals
                                                servicePackage.ServicePackageId into list1
                                            from l1 in list1.DefaultIfEmpty()
                                            select new PaymentTransactionsModel
                                            {
                                                PaymentTransactionId = paymentTransaction.PaymentTransactionId,
                                                TransactionDesc = LawsVnLanguages.GetCurrentLanguageId() == LawsVnLanguages.AvailableLanguages[1].LanguageId ? paymentTransaction.TransactionDesc : paymentTransaction.TransactionData,
                                                TransactionStatusId = paymentTransaction.TransactionStatusId,
                                                TransactionStatusDesc = paymentTransaction.TransactionStatusId.TransactionStatusDescGetById(),
                                                PaymentDate = paymentTransaction.PaymentDate.toString(),
                                                Amount =
                                                    paymentTransaction.TransactionStatusId != Constants.TransactionStatusIdPending ||
                                                    l1 != null && !Constants.ServiceId_NghiepVu.Any(x => x.Equals(l1.ServiceId))
                                                        ? (paymentTransaction.Amount > 0
                                                            ? (_currentLanguageId == 1 ? string.Format("{0:#,###} {1}", paymentTransaction.Amount, CmsConstants.CURRENCY_VND) : string.Format("{0} {1:#,###} ", CmsConstants.CURRENCY, paymentTransaction.Amount))
                                                            : "0")
                                                        : "0",
                                                PaymentTypeId = paymentTransaction.PaymentTypeId,
                                                ServiceId = (short)(l1 == null ? 0 : l1.ServiceId),
                                                ServicePackageId = paymentTransaction.ServicePackageId,
                                                ServicePackageDesc = l1 == null ? string.Empty : LawsVnLanguages.GetCurrentLanguageId() == LawsVnLanguages.AvailableLanguages[1].LanguageId ? l1.ServicePackageDesc.TrimmedOrDefault(string.Empty) : l1.ServicePackageName.TrimmedOrDefault(string.Empty)
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
                                                    ServiceDesc = l2 == null ? string.Empty : LawsVnLanguages.GetCurrentLanguageId() == LawsVnLanguages.AvailableLanguages[1].LanguageId ? l2.ServiceDesc.TrimmedOrDefault(string.Empty) : l2.ServiceName.TrimmedOrDefault(string.Empty),
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
                                                        PaymentTypeDesc = l3 == null ? string.Empty : LawsVnLanguages.GetCurrentLanguageId() == LawsVnLanguages.AvailableLanguages[1].LanguageId ? l3.PaymentTypeDesc.TrimmedOrDefault(string.Empty) : l3.PaymentTypeName.TrimmedOrDefault(string.Empty)
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
                                                            TransactionTypesDesc = l4 == null ? string.Empty : LawsVnLanguages.GetCurrentLanguageId() == LawsVnLanguages.AvailableLanguages[1].LanguageId ? l4.TransactionTypeDesc.TrimmedOrDefault(string.Empty) : l4.TransactionTypeName.TrimmedOrDefault(string.Empty)
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
                    EndTime = Extensions.GetColumnValue<DateTime>(dtCustomerService.Rows[0], "EndTime"),
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
            if(Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains(Constants.WebsiteDomainVi))
            {
                Extensions.UpdateUserData();
            }
            return View(model);
        }
    }
}
