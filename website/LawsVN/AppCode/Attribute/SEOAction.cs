using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ICSoft.CMSLib;
using LawsVN.Library;
using LawsVN.Models;

namespace LawsVN.AppCode.Attribute
{
    /// <summary>
    /// ActionFilter lấy thông tin SEO theo Url
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SEOAction : ActionFilterAttribute
    {
        /// <summary>
        /// SeoId Fixed
        /// </summary>
        private int _seoId;

        /// <summary>
        /// SEO theo lĩnh vực
        /// </summary>
        private bool _seoByField;

        /// <summary>
        /// SEO vb UBND theo theo tỉnh thành
        /// </summary>
        private bool _seoByProvince;

        private byte _docGroupId;

        private string _controllerName = "Docs";

        private string _controllerNameUbnd = "DocsUBND";

        private string _actionName = "ListByField";

        private string _actionNameUbndListDocsByProvince = "ListDocsByProvince";

        public SEOAction()
        {
        }

        public SEOAction(int seoId)
        {
            _seoId = seoId;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.Ordinal))
            {
                var controller = filterContext.RouteData.Values["controller"];
                var action = filterContext.RouteData.Values["action"];
                _seoByField = controller.Equals(_controllerName) && action.Equals(_actionName);
                _seoByProvince = controller.Equals(_controllerNameUbnd) && action.Equals(_actionNameUbndListDocsByProvince);
                foreach (var p in filterContext.ActionDescriptor.GetParameters())
                {
                    if (filterContext.Controller.ValueProvider.GetValue(p.ParameterName) != null && p.ParameterName.Equals("docGroupId"))
                    {
                        _docGroupId = byte.Parse(filterContext.Controller.ValueProvider.GetValue(p.ParameterName).AttemptedValue);
                    }
                }
                filterContext.Result = GetSeo(filterContext);
            }

            base.OnActionExecuted(filterContext);
        }

        private ActionResult GetSeo(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is ViewResultBase)
            {
                return GetSeo(filterContext.Result as ViewResultBase);
            }

            return filterContext.Result;
        }

        private ActionResult GetSeo(ViewResultBase result)
        {
            var model = result.Model as ViewModelBase;
            if (model != null)
            {
                Seos seo = new Seos();
                //Todo SEO vb theo lĩnh vực
                if (_seoByField)
                {
                    switch (_docGroupId)
                    {
                        case 1:
                        {
                            _seoId = Constants.SeoByField1;
                            break;
                        }
                        case 2:
                        {
                            _seoId = Constants.SeoByField2;
                            break;
                        }
                        case 3:
                        {
                            _seoId = Constants.SeoByField3;
                            break;
                        }
                        case 4:
                        {
                            _seoId = Constants.SeoByField4;
                            break;
                        }
                        case 5:
                        {
                            _seoId = Constants.SeoByField5;
                            break;
                        }
                        case 6:
                        {
                            _seoId = Constants.SeoByField6;
                            break;
                        }
                        case 8:
                        {
                            _seoId = Constants.SeoByField8;
                            break;
                        }
                        default:
                        {
                            _seoId = Constants.SeoByField;
                            break;
                        }
                    }
                    seo = seo.Get(_seoId);
                    if (seo.SeoId > 0 && !string.IsNullOrEmpty(model.SeoReplace))
                    {
                        var pattern = @"\[(.*?)\]";
                        model.WebsiteTitle = Regex.Replace(seo.MetaTitle, pattern, model.SeoReplace);
                        model.WebsiteDescription = Regex.Replace(seo.MetaDesc, pattern, model.SeoReplace);
                        model.MetaDesc = Regex.Replace(seo.MetaDesc, pattern, model.SeoReplace);
                        model.WebsiteKeywords = Regex.Replace(seo.MetaKeyword, pattern, model.SeoReplace);
                        model.SeoHeader = Regex.Replace(seo.H1Tag, pattern, model.SeoReplace);
                        if (string.IsNullOrEmpty(model.WebsiteCanonical))
                        {
                            model.WebsiteCanonical = Regex.Replace(seo.CanonicalTag, pattern, model.SeoReplace);
                        }
                    }
                }
                //Todo SEO vb UBND theo tỉnh thành
                else if (_seoByProvince)
                {
                    seo = seo.Get(Constants.SeoByProvince);
                    if (seo.SeoId > 0 && !string.IsNullOrEmpty(model.SeoReplace))
                    {
                        var pattern = @"\[(.*?)\]";
                        model.WebsiteTitle = Regex.Replace(seo.MetaTitle, pattern, model.SeoReplace);
                        model.WebsiteDescription = Regex.Replace(seo.MetaDesc, pattern, model.SeoReplace);
                        model.MetaDesc = Regex.Replace(seo.MetaDesc, pattern, model.SeoReplace);
                        model.WebsiteKeywords = Regex.Replace(seo.MetaKeyword, pattern, model.SeoReplace);
                        model.SeoHeader = Regex.Replace(seo.H1Tag, pattern, model.SeoReplace);
                        if (string.IsNullOrEmpty(model.WebsiteCanonical))
                        {
                            model.WebsiteCanonical = Regex.Replace(seo.CanonicalTag, pattern, model.SeoReplace);
                        }
                    }
                }
                else
                {
                    seo = seo.GetByUrl(HttpContext.Current.Request.Url.AbsolutePath);
                    if (seo.SeoId > 0)
                    {
                        model.WebsiteTitle = seo.MetaTitle.TrimmedOrDefault(model.WebsiteTitle);
                        model.WebsiteDescription = seo.MetaDesc.TrimmedOrDefault(model.WebsiteDescription);
                        model.MetaDesc = seo.MetaDesc.TrimmedOrDefault(model.MetaDesc);
                        model.WebsiteKeywords = seo.MetaKeyword.TrimmedOrDefault(model.WebsiteKeywords);
                        model.SeoHeader = seo.H1Tag.TrimmedOrDefault(model.SeoHeader);
                        model.WebsiteCanonical = seo.CanonicalTag;
                    }
                    else
                    {
                        model.WebsiteTitle = Constants.WebsiteTitle;
                        model.WebsiteDescription = Constants.WebsiteDescription;
                        model.WebsiteKeywords = Constants.WebsiteKeywords;
                        model.SeoHeader = Constants.SeoHeader;
                    }
                }
            }
            
            return new ViewResult
            {
                TempData = result.TempData,
                ViewData = result.ViewData,
                ViewName = result.ViewName
            };
        }

    }
}