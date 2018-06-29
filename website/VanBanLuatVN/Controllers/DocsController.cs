using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using VanBanLuat.Models;
using VanBanLuat.AppCode.Attribute;
using ICSoft.ViewLibV3;
using VanBanLuat.Librarys;
using VanBanLuatVN.Models;
using Constants = VanBanLuat.Librarys.Constants;

namespace VanBanLuatVN.Controllers
{
    public class DocsController : Controller
    {
        //
        // GET: /Docs/
        [SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ListDocByField(short fieldId = 0, byte docGroupId = 1, int page = 1)
        {
            if (fieldId > 0)
            {
                var field = Fields.Static_GetById(fieldId);
                if (field.FieldId > 0)
                {
                    DocFilterParams docFilterParams = new DocFilterParams
                    {
                        DocGroupId = docGroupId,
                        FieldId = fieldId,
                        FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId",
                        PageIndex = page > 0 ? page - 1 : page,
                        RowAmount = Constants.RowAmount20,
                        LanguageId = Constants.LanguageId,
                        OrderBy = "IssueDate DESC",
                        GetEffectStatusName = 1,
                        GetCountByEffectStatus = 1,
                        GetRowCount = 1
                    };
                    DocByFieldViewModel model =
                        new DocByFieldViewModel
                        {
                            Page= page,
                            PageSize = Constants.RowAmount20,
                            IsHomePage = true,
                            Field = field,
                            DocGroupId = docGroupId,
                            SeoReplace = field.FieldName.TrimmedOrDefault(field.FieldDesc),
                            DocList = DocHelpers.GetPage(docFilterParams)
                        };
                    return View(model);
                }
                return RedirectToAction("Index", "Error");
            }
            return RedirectToAction("Index", "Error");
        }

        [SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ListDocByFieldV2(string fieldHexId = "0", byte docGroupId = 0, int page = 1)
        {
            short fieldId = short.Parse(fieldHexId, System.Globalization.NumberStyles.HexNumber); 
            if (fieldId > 0)
            {
                var field = Fields.Static_GetById(fieldId);
                if (field.FieldId > 0)
                {
                    DocFilterParams docFilterParams = new DocFilterParams
                    {
                        DocGroupId = docGroupId,
                        FieldId = fieldId,
                        FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId",
                        PageIndex = page > 0 ? page - 1 : page,
                        RowAmount = Constants.RowAmount20,
                        LanguageId = Constants.LanguageId,
                        OrderBy = "IssueDate DESC",
                        GetEffectStatusName = 1,
                        GetCountByEffectStatus = 1,
                        GetRowCount = 1
                    };
                    DocByFieldViewModel model =
                        new DocByFieldViewModel
                        {
                            Page = page,
                            PageSize = Constants.RowAmount20,
                            IsHomePage = true,
                            Field = field,
                            DocGroupId = docGroupId,
                            SeoReplace = field.FieldName.TrimmedOrDefault(field.FieldDesc),
                            DocList = DocHelpers.GetPage(docFilterParams)
                        };
                    return View("ListDocByField", model);
                }
                return RedirectToAction("Index", "Error");
            }
            return RedirectToAction("Index", "Error");
        }

        [SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ListDocByDocGroupId(byte docGroupId = 0, short menuItemId = 0, int page = 1)
        {
            if (docGroupId > 0)
            {
                string FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId";
                DocByGroupViewModel model = new DocByGroupViewModel
                {
                    Page = page,
                    PageSize = Constants.RowAmount20,
                    IsHomePage = true,
                    MenuItemId = menuItemId,
                    DocGroups = DocGroups.Static_Get(docGroupId),
                    DocList = DocHelpers.GetByDocGroupId(docGroupId, FieldSelect, "IssueDate Desc",
                        Constants.RowAmount20, page > 0 ? page - 1 : page, Constants.LanguageId, 1),
                    DocGroupId = docGroupId
                };
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ListDocByEffectStatus(string effectStatusType = "", int page = 1)
        {
            if (!string.IsNullOrEmpty(effectStatusType))
            {
                DocFilterParams docFilterParams = new DocFilterParams
                {
                    EffectStatusType = effectStatusType,
                    FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId",
                    PageIndex = page > 0 ? page - 1 : page,
                    RowAmount = Constants.RowAmount20,
                    LanguageId = Constants.LanguageId,
                    OrderBy = "IssueDate DESC",
                    GetRowCount = 1,
                    GetEffectStatusName = 1
                };
                DocByFieldViewModel model = new DocByFieldViewModel
                {
                    Page = page,
                    PageSize = Constants.RowAmount20,
                    IsHomePage = true,
                    EffectStatusType = effectStatusType,
                    DocList = DocHelpers.GetPage(docFilterParams)
                };
                switch (effectStatusType)
                {
                    case "HetHieuLuc":
                    {
                        model.EffectStatusTypeText = "Văn bản hết hiệu lực";
                        break;
                    }
                    case "SapCoHieuLuc":
                    {
                        model.EffectStatusTypeText = "Văn bản sắp có hiệu lực";
                        break;
                    }
                    case "SapHetHieuLuc":
                    {
                        model.EffectStatusTypeText = "Văn bản sắp hết hiệu lực";
                        break;
                    }
                    default:
                    {
                        model.EffectStatusTypeText = "Văn bản sắp sửa đổi";
                        break;
                    }
                }
                return View(model);
            }
            return RedirectToAction("Index", "Error");
        }

        [SEOAction]
        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult ListDocs(byte docGroupId = 0, short menuItemId = 0, int page = 1)
        {
                DocFilterParams docFilterParams = new DocFilterParams { DocGroupId = docGroupId, FieldId = 0, FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId", GetEffectStatusName = 1, PageIndex = page > 0 ? page - 1 : page, RowAmount = Constants.RowAmount20, LanguageId = Constants.LanguageId, OrderBy = "IssueDate DESC", GetCountByEffectStatus = 1, GetRowCount = 1};
                DocByFieldViewModel model = new DocByFieldViewModel
                {
                    Page = page,
                    PageSize = Constants.RowAmount20,
                    IsHomePage = true,
                    MenuItemId = menuItemId,
                    DocList = DocHelpers.GetPage(docFilterParams)
                };
            return View(model);
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult DetailFullV2(string docHexId = "0", byte docGroupId = 1, int page = 1, byte relateTypeId = 0, string displayPosition = "")
        {
            int docId = int.Parse(docHexId, System.Globalization.NumberStyles.HexNumber); 
            if (docId > 0)
            {
                var model = new DocViewModel.DocDetailModel
                {
                    Docs = DocHelpers.GetById(new DocFilterParams
                    {
                        DocId = docId,
                        DocGroupId = docGroupId,
                        FieldSelect = "DocId,DocName,DocGroupId,DocSummary,DocContent,DocUrl,DocIdentity,DocTypeId,IssueDate,EffectDate,ExpireDate,EffectStatusId,GazetteNumber,GazetteDate,H1Tag,MetaTitle,MetaDesc,MetaKeyword",
                        GetFieldName = 1,
                        GetDocTypeName = 1,
                        GetOrganName = 1,
                        GetSignerName = 1,
                        GetDocFile = 1,
                        GetDocIndex = 1,
                        GetEffectStatusName = 1,
                        LanguageId = Constants.LanguageId
                    }),
                    Pagination = new PaginationModel
                    {
                        Page = page,
                        RelateTypeId = relateTypeId,
                        DisplayPosition = displayPosition
                    }
                };
                if (model.Docs.DocId > 0)
                {
                    // TODO: SEO MetaTitle -> H1Tag -> Số hiệu+CQBH+ngày ban hành
                    if (!string.IsNullOrEmpty(model.Docs.MetaTitle))
                    {
                        model.WebsiteTitle = model.Docs.MetaTitle;
                        model.WebsiteDescription = model.Docs.MetaDesc.TrimmedOrDefault(model.Docs.MetaTitle);
                        model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(model.Docs.MetaTitle);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.Docs.H1Tag))
                        {
                            model.WebsiteTitle = model.Docs.H1Tag;
                            model.WebsiteDescription = model.MetaDesc.TrimmedOrDefault(model.Docs.H1Tag);
                            model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(model.Docs.H1Tag);
                        }
                        else
                        {
                            string titleFromProperties = model.Docs.DocTypeName.Trim();
                            titleFromProperties = model.Docs.DocName.Length <= 75 ? model.Docs.DocName.Replace("\"", "") : string.Format("{0} {1} của {2} ban hành ngày {3}", titleFromProperties, model.Docs.DocIdentity.Trim(), model.Docs.OrganName.Trim(), model.Docs.IssueDate.toString());
                            model.WebsiteTitle = titleFromProperties;
                            model.WebsiteDescription =
                                model.Docs.MetaDesc.TrimmedOrDefault(model.Docs.DocName.Replace("\"", ""));
                            model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(titleFromProperties);
                        }
                    }
                    // TODO: tab Nội dung: xóa thẻ style
                    if (!string.IsNullOrEmpty(model.Docs.DocContent))
                    {
                        string docContent = model.Docs.DocContent;
                        docContent = Regex.Replace(docContent, "(<style.+?</style>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        model.Docs.DocContent = docContent;
                    }
                    //ToDo file pdf
                    if (model.Docs.lDocFiles.HasValue())
                    {
                        model.FilePath =
                            model.Docs.lDocFiles
                                .Where(f => f.FilePath.ToLower().EndsWith(".pdf") &&
                                            f.LanguageId == Constants.LanguageId).Select(f => f.FilePath)
                                .FirstOrDefault() ?? string.Empty;
                    }
                    //ToDo ds files tiếng Việt
                    if (model.Docs.lDocFiles.HasValue())
                    {
                        model.ListDocFiles = model.Docs.lDocFiles.FindAll(f => f.LanguageId == Constants.LanguageId);
                    }
                    //Todo tim kiem van ban chua duyet va thay link (them #)
                    string RegexLinkRule = "<a([^>]+)href=([^>]+)/([^>]+)-([0-9]+)-(d[1-8]).html([^>]+)>";
                    RegexOptions m_RegexOptions = RegexOptions.None;// RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    Match m_Match = Regex.Match(model.Docs.DocContent, RegexLinkRule, m_RegexOptions);
                    Docs sDocs = new Docs();
                    string oldUrl, newUrl;
                    while (m_Match.Success)
                    {
                        int sDocId = int.Parse(m_Match.Groups[4].Value);
                        sDocs = DocHelpers.GetById(new DocFilterParams
                        {
                            DocId = sDocId,
                            DocGroupId = docGroupId,
                            FieldSelect = "DocId,ReviewStatusId",
                            GetFieldName = 1,
                            GetDocTypeName = 1,
                            GetOrganName = 1,
                            GetSignerName = 1,
                            GetDocFile = 1,
                            GetDocIndex = 1,
                            GetEffectStatusName = 1,
                            LanguageId = Constants.LanguageId
                        });
                        if (sDocs.ReviewStatusId != 2)//khac duyet
                        {
                            oldUrl = m_Match.Groups[4].Value + "-" + m_Match.Groups[5].Value + ".html";
                            newUrl = "#" + oldUrl;
                            model.Docs.DocContent = model.Docs.DocContent.Replace(newUrl, oldUrl);
                        }
                        m_Match = m_Match.NextMatch();
                    }

                    //ToDo DocIndex 
                    model.IsIndex = model.Docs.DocId >= Constants.DocIdMinIndex;
                    if (model.Docs.DocGroupId == Constants.DocGroupIdVbhn)
                        return View("DocsConsolidationDetailFull", model);
                    if (model.Docs.DocGroupId == Constants.DocGroupIdTcvn)
                        return View("VietNamStandardsDetailFull", model);
                    return View("DetailFull", model);
                }
                 return RedirectToAction("Index", "Error");
            }
            return RedirectToAction("Index", "Error");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult DetailFull(int docId = 0, byte docGroupId = 1, int page = 1, byte relateTypeId = 0, string displayPosition = "")
        {
            if (docId > 0)
            {
                var model = new DocViewModel.DocDetailModel
                {
                    Docs = DocHelpers.GetById(new DocFilterParams
                    {
                        DocId = docId,
                        DocGroupId = docGroupId,
                        FieldSelect = "DocId,DocName,DocGroupId,DocSummary,DocContent,DocUrl,DocIdentity,DocTypeId,IssueDate,EffectDate,ExpireDate,EffectStatusId,GazetteNumber,GazetteDate,H1Tag,MetaTitle,MetaDesc,MetaKeyword",
                        GetFieldName = 1,
                        GetDocTypeName = 1,
                        GetOrganName = 1,
                        GetSignerName = 1,
                        GetDocFile = 1,
                        GetDocIndex = 1,
                        GetEffectStatusName = 1,
                        LanguageId = Constants.LanguageId
                    }),
                    Pagination = new PaginationModel
                    {
                        Page = page,
                        RelateTypeId = relateTypeId,
                        DisplayPosition = displayPosition
                    }
                };
                if (model.Docs.DocId > 0)
                {
                    // TODO: SEO MetaTitle -> H1Tag -> Số hiệu+CQBH+ngày ban hành
                    if (!string.IsNullOrEmpty(model.Docs.MetaTitle))
                    {
                        model.WebsiteTitle = model.Docs.MetaTitle;
                        model.WebsiteDescription = model.Docs.MetaDesc.TrimmedOrDefault(model.Docs.MetaTitle);
                        model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(model.Docs.MetaTitle);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.Docs.H1Tag))
                        {
                            model.WebsiteTitle = model.Docs.H1Tag;
                            model.WebsiteDescription = model.MetaDesc.TrimmedOrDefault(model.Docs.H1Tag);
                            model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(model.Docs.H1Tag);
                        }
                        else
                        {
                            string titleFromProperties = model.Docs.DocTypeName.Trim();
                            titleFromProperties = model.Docs.DocName.Length <= 75 ? model.Docs.DocName.Replace("\"", "") : string.Format("{0} {1} của {2} ban hành ngày {3}", titleFromProperties, model.Docs.DocIdentity.Trim(), model.Docs.OrganName.Trim(), model.Docs.IssueDate.toString());
                            model.WebsiteTitle = titleFromProperties;
                            model.WebsiteDescription =
                                model.Docs.MetaDesc.TrimmedOrDefault(model.Docs.DocName.Replace("\"", ""));
                            model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(titleFromProperties);
                        }
                    }
                    // TODO: tab Nội dung: xóa thẻ style
                    if (!string.IsNullOrEmpty(model.Docs.DocContent))
                    {
                        string docContent = model.Docs.DocContent;
                        docContent = Regex.Replace(docContent, "(<style.+?</style>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        model.Docs.DocContent = docContent;
                    }
                    //ToDo file pdf
                    if (model.Docs.lDocFiles.HasValue())
                    {
                        model.FilePath =
                            model.Docs.lDocFiles
                                .Where(f => f.FilePath.ToLower().EndsWith(".pdf") &&
                                            f.LanguageId == Constants.LanguageId).Select(f => f.FilePath)
                                .FirstOrDefault() ?? string.Empty;
                    }
                    //ToDo ds files tiếng Việt
                    if (model.Docs.lDocFiles.HasValue())
                    {
                        model.ListDocFiles = model.Docs.lDocFiles.FindAll(f => f.LanguageId == Constants.LanguageId);
                    }
                    //Todo tim kiem van ban chua duyet va thay link (them #)
                    string RegexLinkRule = "<a([^>]+)href=([^>]+)/([^>]+)-([0-9]+)-(d[1-8]).html([^>]+)>";
                    RegexOptions m_RegexOptions = RegexOptions.None;// RegexOptions.Multiline | RegexOptions.IgnoreCase;
                    Match m_Match = Regex.Match(model.Docs.DocContent, RegexLinkRule, m_RegexOptions);
                    Docs sDocs = new Docs();
                    string oldUrl, newUrl;
                    while (m_Match.Success)
                    {
                        int sDocId = int.Parse(m_Match.Groups[4].Value);
                        sDocs = DocHelpers.GetById(new DocFilterParams
                        {
                            DocId = sDocId,
                            DocGroupId = docGroupId,
                            FieldSelect = "DocId,ReviewStatusId",
                            LanguageId = Constants.LanguageId
                        });
                        if (sDocs.ReviewStatusId != 2)//khac duyet
                        {
                            oldUrl = m_Match.Value;
                            newUrl = "<a href=\"#\">";
                            model.Docs.DocContent = model.Docs.DocContent.Replace(oldUrl, newUrl);
                        }
                        m_Match = m_Match.NextMatch();
                    }
                    return View(model);
                }
                return RedirectToAction("Index", "Error");
            }
            return RedirectToAction("Index", "Error");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult VietNamStandardsDetailFull(int docId = 0, byte docGroupId = 3, int page = 1, byte relateTypeId = 0, string displayPosition = "")
        {
            if (docId > 0)
            {
                var model = new DocViewModel.DocDetailModel
                {
                    Docs = DocHelpers.GetById(new DocFilterParams
                    {
                        DocId = docId,
                        DocGroupId = docGroupId,
                        FieldSelect = "DocId,DocName,DocGroupId,DocSummary,DocContent,DocUrl,DocIdentity,DocTypeId,IssueDate,IssueYear,EffectDate,ExpireDate,EffectStatusId,GazetteNumber,H1Tag,MetaTitle,MetaDesc,MetaKeyword",
                        GetFieldName = 1,
                        GetDocTypeName = 1,
                        GetOrganName = 1,
                        GetSignerName = 1,
                        GetDocFile = 1,
                        GetDocIndex = 1,
                        GetEffectStatusName = 1,
                        LanguageId = Constants.LanguageId
                    }),
                    Pagination = new PaginationModel
                    {
                        Page = page,
                        RelateTypeId = relateTypeId,
                        DisplayPosition = displayPosition
                    }
                };
                if (model.Docs.DocId > 0)
                {
                    // TODO: SEO MetaTitle -> H1Tag -> Số hiệu+CQBH+ngày ban hành
                    if (!string.IsNullOrEmpty(model.MetaTitle))
                    {
                        model.WebsiteTitle = model.Docs.MetaTitle;
                        model.WebsiteDescription = model.Docs.MetaDesc.TrimmedOrDefault(model.MetaTitle);
                        model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(model.MetaTitle);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.Docs.H1Tag))
                        {
                            model.WebsiteTitle = model.Docs.H1Tag;
                            model.WebsiteDescription = model.MetaDesc.TrimmedOrDefault(model.Docs.H1Tag);
                            model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(model.Docs.H1Tag);
                        }
                        else
                        {
                            string titleFromProperties = model.Docs.DocTypeName.Trim();
                            titleFromProperties = model.Docs.DocName.Length <= 75 ? model.Docs.DocName.Replace("\"", "") : string.Format("{0} {1} của {2} ban hành ngày {3}", titleFromProperties, model.Docs.DocIdentity.Trim(), model.Docs.OrganName.Trim(), model.Docs.IssueDate.toString());
                            model.WebsiteTitle = titleFromProperties;
                            model.WebsiteDescription =
                                model.Docs.MetaDesc.TrimmedOrDefault(model.Docs.DocName.Replace("\"", ""));
                            model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(titleFromProperties);
                        }
                    }
                    //ToDo file pdf
                    if (model.Docs.lDocFiles.HasValue())
                    {
                        model.FilePath =
                            model.Docs.lDocFiles
                                .Where(f => f.FilePath.ToLower().EndsWith(".pdf") &&
                                            f.LanguageId == Constants.LanguageId).Select(f => f.FilePath)
                                .FirstOrDefault() ?? string.Empty;
                    }
                    //ToDo ds files tiếng Việt
                    if (model.Docs.lDocFiles.HasValue())
                    {
                        model.ListDocFiles = model.Docs.lDocFiles.FindAll(f => f.LanguageId == Constants.LanguageId);
                    }
                    //ToDo DocIndex 
                    model.IsIndex = model.Docs.DocId >= Constants.DocIdMinIndex;
                    return View(model);
                }
                return RedirectToAction("Index", "Error");
            }
            return RedirectToAction("Index", "Error");
        }

        [OutputCache(CacheProfile = "Cache3MinutesAllParam")]
        public ActionResult DocsConsolidationDetailFull(int docId = 0, byte docGroupId = 1, int page = 1, byte relateTypeId = 0, string displayPosition = "")
        {
            if (docId > 0)
            {
                var model = new DocViewModel.DocDetailModel
                {
                    Docs = DocHelpers.GetById(new DocFilterParams
                    {
                        DocId = docId,
                        DocGroupId = docGroupId,
                        FieldSelect = "DocId,DocName,DocGroupId,DocSummary,DocContent,DocUrl,DocIdentity,DocTypeId,IssueDate,ExpireDate,GazetteDate,EffectStatusId,GazetteNumber,H1Tag,MetaTitle,MetaDesc,MetaKeyword",
                        GetFieldName = 1,
                        GetDocTypeName = 1,
                        GetOrganName = 1,
                        GetSignerName = 1,
                        GetDocFile = 1,
                        GetDocIndex = 1,
                        GetEffectStatusName = 1,
                        LanguageId = Constants.LanguageId
                    }),
                    Pagination = new PaginationModel
                    {
                        Page = page,
                        RelateTypeId = relateTypeId,
                        DisplayPosition = displayPosition
                    }
                };
                if (model.Docs.DocId > 0)
                {
                    // TODO: SEO MetaTitle -> H1Tag -> Số hiệu+CQBH+ngày ban hành
                    if (!string.IsNullOrEmpty(model.MetaTitle))
                    {
                        model.WebsiteTitle = model.Docs.MetaTitle;
                        model.WebsiteDescription = model.Docs.MetaDesc.TrimmedOrDefault(model.MetaTitle);
                        model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(model.MetaTitle);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.Docs.H1Tag))
                        {
                            model.WebsiteTitle = model.Docs.H1Tag;
                            model.WebsiteDescription = model.MetaDesc.TrimmedOrDefault(model.Docs.H1Tag);
                            model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(model.Docs.H1Tag);
                        }
                        else
                        {
                            string titleFromProperties = model.Docs.DocTypeName.Trim();
                            titleFromProperties = model.Docs.DocName.Length <= 75 ? model.Docs.DocName.Replace("\"", "") : string.Format("{0} {1} của {2} ban hành ngày {3}", titleFromProperties, model.Docs.DocIdentity.Trim(), model.Docs.OrganName.Trim(), model.Docs.IssueDate.toString());
                            model.WebsiteTitle = titleFromProperties;
                            model.WebsiteDescription =
                                model.Docs.MetaDesc.TrimmedOrDefault(model.Docs.DocName.Replace("\"", ""));
                            model.WebsiteKeywords = model.Docs.MetaKeyword.TrimmedOrDefault(titleFromProperties);
                        }
                    }
                    // TODO: tab Nội dung: xóa thẻ style
                    if (!string.IsNullOrEmpty(model.Docs.DocContent))
                    {
                        string docContent = model.Docs.DocContent;
                        docContent = Regex.Replace(docContent, "(<style.+?</style>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        model.Docs.DocContent = docContent;
                    }
                    //ToDo file pdf
                    if (model.Docs.lDocFiles.HasValue())
                    {
                        model.FilePath =
                            model.Docs.lDocFiles
                                .Where(f => f.FilePath.ToLower().EndsWith(".pdf") &&
                                            f.LanguageId == Constants.LanguageId).Select(f => f.FilePath)
                                .FirstOrDefault() ?? string.Empty;
                    }
                    //ToDo ds files tiếng Việt
                    if (model.Docs.lDocFiles.HasValue())
                    {
                        model.ListDocFiles = model.Docs.lDocFiles.FindAll(f => f.LanguageId == Constants.LanguageId);
                    }
                    //ToDo DocIndex 
                    model.IsIndex = model.Docs.DocId >= Constants.DocIdMinIndex;
                    return View(model);
                }
                return RedirectToAction("Index", "Error");
            }
            return RedirectToAction("Index", "Error");
        }

    }
}
