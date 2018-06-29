using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ICSoft.ViewLibV3;
using Newtonsoft.Json;
using VanBanLuat.Librarys;
using VanBanLuat.Models;
using VanBanLuatVN.AppCode.Attributes;
using VanBanLuatVN.Library.Sercurity;
using Constants = VanBanLuat.Librarys.Constants;
using VanBanLuat.AppCode.Attribute;
using VanBanLuatVN.Models;

namespace VanBanLuat.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        #region Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new AccountModel.LoginModel
            {
                WebsiteTitle = "Đăng nhập tài khoản",
                ReturnUrl = Request.UrlReferrer != null ? Request.UrlReferrer.PathAndQuery : Constants.ROOT_PATH
            };
            return View(model);
        }

        [HttpPost]
        public AjaxResult Login(AccountModel.LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }

            string returnUrl = string.Empty;
            var loginResult = CustomerHelpers.Login(model.CustomerName, model.CustomerPass, Request.UserHostAddress, Request.UserAgent, Constants.LanguageId);
            if (loginResult.ActionStatus.Equals("OK"))
            {
                SetAuthCookie(loginResult.mCustomers,model.RememberMe);
                if (string.IsNullOrEmpty(model.ReturnUrl) && Request.UrlReferrer != null)
                {
                    returnUrl = Server.UrlDecode(Request.UrlReferrer.PathAndQuery);
                    if (returnUrl != null && returnUrl.IndexOf("user/dang-nhap.html", StringComparison.Ordinal) > 0)
                    {
                        returnUrl = returnUrl.Replace("/user/dang-nhap.html?ReturnUrl=", "");
                    }
                }
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
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

        [HttpPost]
        public AjaxResult GoogleLogin(AccountModel.GoogleAccountMode model)
        {
            CustomerLoginResult result = CustomerHelpers.LoginOpenId(Constants.SiteId, Constants.OpenAuthTypeIdGoogle,
                model.GoogleId, model.Email, model.Name, Request.UserHostAddress, Request.UserAgent);
            if (result.ActionStatus.Equals("OK"))
            {
                string returnUrl = string.Empty;
                SetAuthCookie(result.mCustomers);
                if (string.IsNullOrEmpty(model.ReturnUrl) && Request.UrlReferrer != null )
                {
                    returnUrl = Server.UrlDecode(Request.UrlReferrer.PathAndQuery);
                    if (returnUrl != null && returnUrl.IndexOf("user/dang-nhap.html", StringComparison.Ordinal) > 0)
                    {
                        returnUrl = returnUrl.Replace("/user/dang-nhap.html?ReturnUrl=", "");
                    }
                }
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
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
                Completed = false,
                Message = result.ActionMessage
            };
        }

        [HttpPost]
        public AjaxResult FacebookLogin(AccountModel.FacebookAccountMode model)
        {
            CustomerLoginResult result = CustomerHelpers.LoginOpenId(Constants.SiteId, Constants.OpenAuthTypeIdFacebook,
                model.FacebookId, model.Email, model.Name, Request.UserHostAddress, Request.UserAgent);
            if (result.ActionStatus.Equals("OK"))
            {
                string returnUrl = string.Empty;
                SetAuthCookie(result.mCustomers);
                if (string.IsNullOrEmpty(model.ReturnUrl) && Request.UrlReferrer != null)
                {
                    returnUrl = Server.UrlDecode(Request.UrlReferrer.PathAndQuery);
                    if (returnUrl != null && returnUrl.IndexOf("user/dang-nhap.html", StringComparison.Ordinal) > 0)
                    {
                        returnUrl = returnUrl.Replace("/user/dang-nhap.html?ReturnUrl=", "");
                    }
                }
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
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
                Completed = false,
                Message = result.ActionMessage
            };
        }

        public void SetAuthCookie(Customers customer, bool remember = false)
        {
            var serializeModel = new AccountSerializerModel
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                CustomerFullName = customer.CustomerFullName,
                CustomerMail = customer.CustomerMail,
                CustomerMobile = customer.CustomerMobile,
                OpenId = customer.Note.Equals("OpenId")
            };

            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1,
                customer.CustomerName,
                DateTime.Now,
                DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                remember,
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
        }

        public ActionResult Logout()
        {
            Extensions.Logout(Response);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (Extensions.IsAuthenticated)
            {
                return RedirectToRoute("Profile");
            }
            var model = new AccountModel.RegisterModel
            {
                WebsiteTitle = "Đăng ký tài khoản"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult HeaderRegister(AccountModel.RegisterHeaderModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }
            var mCustomer = new Customers
            {
                CustomerName = model.CustomerName,
                CustomerPass = model.Password,
                CustomerMail = model.EmailRegister,
                CustomerFullName = string.Empty,
                CustomerGroupId = 1,
                SiteId = Constants.SiteId,
                Website = string.Empty,
                Facebook = string.Empty,
                CrDateTime = DateTime.Now
            };

            var registerResult = CustomerHelpers.CustomerRegister(mCustomer, 0, 0, Constants.LanguageId);

            if (registerResult.ActionStatus.Equals("OK"))
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = registerResult.ActionMessage,
                    Completed = true,
                    ReturnUrl = Constants.ROOT_PATH
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public AjaxResult Register(AccountModel.RegisterModel model)    
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng chọn lĩnh vực văn bản quan tâm.",
                    Completed = false
                };
            }
            DateTime dateOfBirth = new DateTime();
            if (model.Day > 0 && model.Month > 0 && model.Year > 0)
            {
                dateOfBirth = new DateTime(model.Year,model.Month,model.Day);
            }
            var mCustomer = new Customers
            {
                CustomerName = model.CustomerName,
                CustomerPass = model.Password,
                CustomerMail = model.EmailRegister,
                CustomerMobile = model.Phone,
                CustomerFullName = model.FullName,
                CustomerNickName = string.Empty,
                Address = model.Address,
                DateOfBirth = dateOfBirth,
                OrganPhone = model.OrganPhone,
                CustomerGroupId = 1,
                GenderId = model.GenderId,
                SiteId = Constants.SiteId,
                Website = string.Empty,
                Facebook = string.Empty,
                CrDateTime = DateTime.Now
            };
            //Dịch vụ quan tâm)
            if (model.ServiceOfInterest != null && model.ServiceOfInterest.Length > 0)
            {
                mCustomer.ApplicationId = model.ServiceOfInterest.Length > 1 ? Constants.ViEnNewsletterGroupId : model.ServiceOfInterest[0];
            }

            var registerResult = CustomerHelpers.CustomerRegister(mCustomer, 1, 0, Constants.LanguageId);

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
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = registerResult.ActionMessage,
                    Completed = true,
                    ReturnUrl = Constants.ROOT_PATH
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

        [SEOAction]
        [AllowAnonymous]
        public ActionResult NewUserActive(int customerId = 0, string registerCode = "")
        {
            var result = CustomerHelpers.ConfirmActive(Constants.LanguageId, customerId, registerCode);
            var model = new AccountModel.NewUserActiveModel
            {
                WebsiteTitle = "Kích hoạt tài khoản",
                ActiveStatus = result.ActionStatus.Equals("OK"),
                ActiveMessage = result.ActionMessage
            };
            return View(model);
        }

        #endregion

        #region ChangePassword
        [VbLuatAuthorize]
        public ActionResult ChangePassword()
        {
            var model = new AccountModel.ChangePasswordModel
            {
                WebsiteTitle = "Đổi mật khẩu"
            };
            return View(model);
        }

        [HttpPost]
        [VbLuatAuthorize]
        public AjaxResult ChangePassword(AccountModel.ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }

            var result = CustomerHelpers.ChangePass(model.VbluatUser.CustomerName, model.Password,
                model.CurrentPassword, Request.UserHostAddress, Request.UserAgent, Constants.LanguageId);
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = result.ActionMessage,
                Completed = "OK".Equals(result.ActionStatus)
            };
        }

        #endregion

        #region ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            var model = new AccountModel.ForgotPasswordModel
            {
                WebsiteTitle = "Lấy lại mật khẩu"
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public AjaxResult ForgotPassword(AccountModel.ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }
            string messages = CustomerHelpers.ForgotPassword(Constants.LanguageId,model.ForgotPasswordEmail);

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
            var model = new AccountModel.ConfirmResetPasswordModel
            {
                WebsiteTitle = "Quên mật khẩu"
            };
            if (customerId > 0 && !string.IsNullOrEmpty(confirmCode))
            {
                var result = CustomerHelpers.ConfirmActive(Constants.LanguageId, customerId, confirmCode);
                if (result.ActionStatus.Equals("OK"))
                {
                    model.CustomerId = customerId;
                    model.ConfirmCode = confirmCode;
                }
            }
            return View(model);
        }

        [HttpPost]
        public AjaxResult ConfirmResetPassword(AccountModel.ConfirmResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }
            var messages = CustomerHelpers.ResetPass(Constants.LanguageId, model.CustomerId, string.Empty, model.ConfirmCode, model.Password, Request.UserHostAddress, Request.UserAgent);

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = messages,
                Completed = true
            };
        }

        #region AccountInformation

        [SEOAction]
        [VbLuatAuthorize]
        public ActionResult AccountProfile()
        {
            int customerId = Extensions.GetCurrentCustomerId();
            Customers customer = CustomerHelpers.GetByCustomerId(customerId);
            var model = new AccountModel.AccountProfileModel
            {
                WebsiteTitle = "Thông tin tài khoản",
                lCustomerFields = new CustomerFields().GetListFieldsByCustomerId(customerId),
                CustomerName = customer.CustomerName,
                Email = customer.CustomerMail,
                CustomerMobile = customer.CustomerMobile,
                FullName = customer.CustomerFullName,
                OrganAddress = customer.Address,
                DateOfBirth = customer.DateOfBirth.toString(),
                OrganPhone = customer.OrganPhone,
                OrganName = customer.OrganName,
                OrganFax = customer.OrganFax,
                Password = customer.CustomerPass,
                GenderId = customer.GenderId,
                ApplicationId = customer.ApplicationId,
                OccupationId = customer.OccupationId
            };
            if (customer.DateOfBirth != DateTime.MinValue)
            {
                model.Day = customer.DateOfBirth.Day;
                model.Month = customer.DateOfBirth.Month;
                model.Year = customer.DateOfBirth.Year;
            }
            else
            {
                model.Day = DateTime.Now.Day;
                model.Month = DateTime.Now.Month;
                model.Year = DateTime.Now.Year;
            }
            return View(model);
        }

        [HttpPost]
        [VbLuatAuthorize]
        [ValidateAntiForgeryToken]
        public AjaxResult AccountProfile(AccountModel.AccountProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }

            DateTime dateOfBirth = new DateTime();
            if (model.Day > 0 && model.Month > 0 && model.Year > 0)
            {
                dateOfBirth = new DateTime(model.Year, model.Month, model.Day);
            }
            var mCustomer = new Customers
            {
                CustomerId = Extensions.GetCurrentCustomerId(),
                CustomerName = model.CustomerName,
                CustomerMail = model.Email,
                CustomerMobile = model.CustomerMobile == null ? string.Empty : model.CustomerMobile,
                CustomerFullName = model.FullName,
                CustomerNickName = string.Empty,
                Address = model.OrganAddress,
                DateOfBirth = dateOfBirth,
                OrganPhone = model.OrganPhone == null ? string.Empty : model.OrganPhone,
                OrganFax = model.OrganFax == null ? string.Empty : model.OrganFax,
                OrganAddress = model.OrganAddress,
                OrganName = model.OrganName,
                CustomerGroupId = 1,
                GenderId = model.GenderId,
                SiteId = Constants.SiteId,
                Website = string.Empty,
                Facebook = string.Empty,
                OccupationId = model.OccupationId,

                CustomerPass = model.Password,
                ApplicationId = 0,
                RegisterNewsletter = 0,
                MaxConcurrentLogin = 0,
                LockPassword = 0,
                StatusId = 1

            };
            //Dịch vụ quan tâm)
            if (model.ServiceOfInterest != null && model.ServiceOfInterest.Length > 0)
            {
                mCustomer.ApplicationId = model.ServiceOfInterest.Length == 1 ? model.ServiceOfInterest[0] : Constants.AllNewsletterGroupId;
            }

            var registerResult = CustomerHelpers.Update(mCustomer);

            //if (registerResult >= 0)
            //{
                //Lĩnh vực văn bản quan tâm
                if (model.FieldId != null && model.FieldId.Length > 0)
                {
                    var mCustomerFields = new CustomerFields
                    {
                        CustomerId = Extensions.GetCurrentCustomerId(),
                        DocGroupId = Constants.DocGroupIdVbpq,
                        DisplayOrder = 0
                    };
                    mCustomerFields.InsertByListId(String.Join(",", model.FieldId));
                }
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "Cập nhật thông tin khách hàng thành công",
                    Completed = true,
                    ReturnUrl = string.Concat(Constants.ROOT_PATH, "user/thong-tin-tai-khoan.html")
                };
            //}
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Cập nhật thông tin khách hàng thất bại",
                Completed = false
            };
        }
        #endregion
        #endregion
        
        /// <summary>
        /// View Văn bản của tôi
        /// </summary>
        /// <returns></returns>

        [SEOAction]
        [AllowAnonymous]
        public ActionResult MyDocuments(byte docGroupId = 0, int page = 1, short fieldId = 0, byte effectStatusId = 0, short organId = 0, byte docTypeId = 0)
        {
            var docFilterParams = new DocFilterParams
            {
                FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId",
                CustomerId = Extensions.GetCurrentCustomerId(),
                RegistTypeId = Constants.RegistTypeIdVbqt,
                DocGroupId = docGroupId,
                FieldId = fieldId,
                EffectStatusId = effectStatusId,
                OrganId = organId,
                DocTypeId = docTypeId,
                LanguageId = Constants.LanguageId,
                RowAmount = Constants.RowAmount20,
                PageIndex = page > 0 ? page - 1 : page,
                GetRowCount = 1,
                GetCountByField = 1,
                GetEffectStatusName = 1,
                GetCountByEffectStatus = 1,
                GetCountByDocType = 1,
                GetCountByOrgan = 1
            };
            var model = new DocByFieldViewModel
            {
                Page = page,
                FieldId = fieldId,
                EffectStatusId = effectStatusId,
                OrganId = organId,
                DocTypeId = docTypeId,
                PageSize = Constants.RowAmount20,
                DocList = DocHelpers.GetPage(docFilterParams)
            };
            return View(model);
        }

        [HttpPost]
        [VbLuatAuthorize]
        public AjaxResult DocSendMail(DocViewModel.DocSendMail model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }
            var messageTemplate = new MessageTemplates().Get(Constants.TemplateDocSendMail);
            if (messageTemplate.MessageTemplateId > 0)
            {
                var messageSends = new MessageSends
                {
                    SiteId = Constants.SiteId,
                    MessageTemplateId = messageTemplate.MessageTemplateId,
                    SendFrom = messageTemplate.SendFrom,
                    SendTo = model.SendMail,
                    Title = "Vanbanluat.vn gửi link văn bản",
                    MsgContent = messageTemplate.MsgContent.Replace("#Email#", model.SendMail).Replace("#DocUrl#", string.Concat(Constants.WEBSITE_DOMAIN, model.DocUrl)),
                    SendMethodId = 1, //email
                    SendStatusId = 1, //chờ gửi
                    SendTime = DateTime.Now
                };
                messageSends.Insert();
                if (messageSends.MessageSendId > 0)
                {
                    return new AjaxResult
                    {
                        StatusCode = 200,
                        AllowGet = true,
                        Message = "Gửi link văn bản thành công.",
                        Completed = true
                    };
                }
            }
            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Quý khách vui lòng thử lại sau.",
                Completed = false
            };
        }

        [HttpPost]
        [VbLuatAuthorize]
        public AjaxResult DocFeedBack(DocViewModel.DocFeedback model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }
            short sysMessageId = 0;
            int actUserId = 0;
            var feedBack = new FeedBacks
            {
                FullName = Extensions.GetCurrentCustomerFullName(),
                PhoneNumber = Extensions.GetCurrentCustomerMobile(),
                Email = Extensions.GetCurrentCustomerMail(),
                Comment = model.FeedbackContent,
                SiteId = Constants.SiteId,
                Title = model.DocName,
                FeedBackGroupId = Constants.FeedBackGroupIdGopY,
                FromIP = Request.UserHostAddress,
                Address = string.Empty,
                ReviewStatusId=1
            };
            feedBack.InsertOrUpdate(0, actUserId, ref sysMessageId);
            if (feedBack.FeedBackId > 0)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "Cảm ơn quý khách đã gửi góp ý.",
                    Completed = true
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Quý khách vui lòng thử lại sau.",
                Completed = false
            };
        }

        [HttpPost]
        [VbLuatAuthorize]
        public AjaxResult ArticleFeedBack(ArticlesModel.ArticleFeedback model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }
            short sysMessageId = 0;
            int actUserId = 0;
            var feedBack = new FeedBacks
            {
                FullName = Extensions.GetCurrentCustomerFullName(),
                PhoneNumber = Extensions.GetCurrentCustomerMobile(),
                Email = Extensions.GetCurrentCustomerMail(),
                Comment = model.FeedbackContent,
                SiteId = Constants.SiteId,
                Title = model.Title,
                FeedBackGroupId = Constants.FeedBackGroupIdGopY,
                FromIP = Request.UserHostAddress,
                Address = string.Empty,
                ReviewStatusId = 1
            };
            feedBack.InsertOrUpdate(0, actUserId, ref sysMessageId);
            if (feedBack.FeedBackId > 0)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "Cảm ơn quý khách đã gửi góp ý.",
                    Completed = true
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Quý khách vui lòng thử lại sau.",
                Completed = false
            };
        }

        [HttpPost]
        [VbLuatAuthorize]
        public AjaxResult GuiYeuCauVB(DocViewModel.GuiYeuCauVB model)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = false,
                    Message = "Quý khách vui lòng thử lại sau.",
                    Completed = false
                };
            }
            short sysMessageId = 0;
            int actUserId = 0;
            var feedBack = new FeedBacks
            {
                FullName = Extensions.GetCurrentCustomerFullName(),
                PhoneNumber = Extensions.GetCurrentCustomerMobile(),
                Email = Extensions.GetCurrentCustomerMail(),
                Comment = model.FeedbackContent,
                SiteId = Constants.SiteId,
                Title = "Gửi yêu cầu văn bản",
                FeedBackGroupId = Constants.FeedBackGroupIdGuiYeuCauVB,
                ReviewStatusId = 1,
                FromIP = Request.UserHostAddress,
                Address = string.Empty
            };
            feedBack.InsertOrUpdate(0, actUserId, ref sysMessageId);
            if (feedBack.FeedBackId > 0)
            {
                return new AjaxResult
                {
                    StatusCode = 200,
                    AllowGet = true,
                    Message = "Cảm ơn quý khách đã gửi góp ý.",
                    Completed = true
                };
            }

            return new AjaxResult
            {
                StatusCode = 200,
                AllowGet = true,
                Message = "Quý khách vui lòng thử lại sau.",
                Completed = false
            };
        }

    }
}
