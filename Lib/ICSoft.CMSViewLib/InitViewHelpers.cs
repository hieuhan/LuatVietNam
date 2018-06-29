using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using ICSoft.CMSLib;
using sms.database;
using sms.utils;
using ICSoft.LawDocsLib;
namespace ICSoft.CMSViewLib
{
    public class InitViewHelpers
    {
        public static List<ArticlesView> InitArticle(SmartDataReader smartReader, bool hasDisplayType=false)
        {
            List<ArticlesView> l_ArticlesView = new List<ArticlesView>();
            ArticlesView m_Articles;
            while (smartReader.Read()) 
            {
                m_Articles = new ArticlesView();
                //m_Articles.LanguageId = smartReader.GetByte("LanguageId");
                //m_Articles.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                m_Articles.ArticleId = smartReader.GetInt32("ArticleId");
                m_Articles.Title = smartReader.GetString("Title");
                m_Articles.Summary = smartReader.GetString("Summary");
                m_Articles.ImagePath = smartReader.GetString("ImagePath");
                m_Articles.PublishTime = smartReader.GetDateTime("PublishTime");
                m_Articles.ArticleUrl = smartReader.GetString("ArticleUrl");
                m_Articles.SourceUrl = smartReader.GetString("SourceUrl");
                m_Articles.ArticleCode = smartReader.GetString("ArticleCode");
                m_Articles.ArticleContent = smartReader.GetString("ArticleContent");
                m_Articles.OriginalPrice = smartReader.GetDouble("OriginalPrice");
                m_Articles.SalePrice = smartReader.GetDouble("SalePrice");
                m_Articles.ContactPrice = smartReader.GetString("ContactPrice");
                m_Articles.CurrencyId = smartReader.GetByte("CurrencyId");
                m_Articles.InventoryStatusId = smartReader.GetByte("InventoryStatusId");
                m_Articles.IconStatusId = smartReader.GetByte("IconStatusId");
                m_Articles.ArticleTypeId = smartReader.GetByte("ArticleTypeId");
                m_Articles.SiteId = smartReader.GetInt16("SiteId");
                m_Articles.DataTypeId = smartReader.GetByte("DataTypeId");
                m_Articles.CategoryId = smartReader.GetInt16("CategoryId");
                m_Articles.ProvinceId = smartReader.GetInt16("ProvinceId");
                m_Articles.MetaTitle = smartReader.GetString("MetaTitle");
                m_Articles.MetaDesc = smartReader.GetString("MetaDesc");
                m_Articles.MetaKeyword = smartReader.GetString("MetaKeyword");
                m_Articles.ViewCount = smartReader.GetInt32("ViewCount");
                m_Articles.CommentCount = smartReader.GetInt32("CommentCount");

                if (hasDisplayType)
                {
                    m_Articles.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_Articles.DisplayTypeName = smartReader.GetString("DisplayTypeName");
                }

                l_ArticlesView.Add(m_Articles);
            }
            return l_ArticlesView;
        }
        //-----------------------------------------------------------------
        public static ArticlesView InitArticleOne(SmartDataReader smartReader)
        {
            List<ArticlesView> l_ArticlesView = InitArticle(smartReader);
            if (l_ArticlesView != null && l_ArticlesView.Count > 0)
            {
                return l_ArticlesView[0];
            }
            else
            {
                return new ArticlesView();
            }
        }
        //-----------------------------------------------------------------
        public static List<CategoriesView> InitCate(SmartDataReader smartReader, List<ArticlesView> l_ArticlesView, bool hasDisplayType=false)
        {
            List<CategoriesView> l_Categories = new List<CategoriesView>();
            CategoriesView m_Categories;
            while (smartReader.Read())
            {
                m_Categories = new CategoriesView();
                //m_Categories.LanguageId = smartReader.GetByte("LanguageId");
                //m_Categories.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                m_Categories.CategoryId = smartReader.GetInt16("CategoryId");
                m_Categories.CategoryName = smartReader.GetString("CategoryName");
                m_Categories.CategoryDesc = smartReader.GetString("CategoryDesc");
                m_Categories.CategoryUrl = smartReader.GetString("CategoryUrl");
                m_Categories.ParentCategoryId = smartReader.GetInt32("ParentCategoryId");
                m_Categories.CategoryLevel = smartReader.GetByte("CategoryLevel");
                m_Categories.ImagePath = smartReader.GetString("ImagePath");
                m_Categories.DataTypeId = smartReader.GetInt16("DataTypeId");
                m_Categories.SiteId = smartReader.GetInt16("SiteId");
                m_Categories.MetaTitle = smartReader.GetString("MetaTitle");
                if (String.IsNullOrEmpty(m_Categories.MetaTitle)) m_Categories.MetaTitle = m_Categories.CategoryName;
                m_Categories.MetaDesc = smartReader.GetString("MetaDesc");
                if (String.IsNullOrEmpty(m_Categories.MetaDesc)) m_Categories.MetaTitle = m_Categories.CategoryDesc;
                m_Categories.MetaKeyword = smartReader.GetString("MetaKeyword");
                m_Categories.CanonicalTag = smartReader.GetString("CanonicalTag");
                m_Categories.H1Tag = smartReader.GetString("H1Tag");
                m_Categories.SeoFooter = smartReader.GetString("SeoFooter");

                if (hasDisplayType)
                {
                    m_Categories.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_Categories.DisplayTypeName = smartReader.GetString("DisplayTypeName");
                    if (l_ArticlesView != null) m_Categories.lArticlesView = ArticlesView.Static_GetByCategoryIdAndDisplayTypeName(m_Categories.CategoryId, m_Categories.DisplayTypeName, l_ArticlesView);
                }
                else
                {
                    if (l_ArticlesView != null) m_Categories.lArticlesView = ArticlesView.Static_GetByCategoryId(m_Categories.CategoryId, l_ArticlesView);
                }

                l_Categories.Add(m_Categories);
            }
            return l_Categories;
        }
        //-----------------------------------------------------------------
        public static CategoriesView InitCateOne(SmartDataReader smartReader, List<ArticlesView> l_ArticlesView)
        {
            List<CategoriesView> l_Categories = InitCate(smartReader, l_ArticlesView);
            if (l_Categories != null && l_Categories.Count > 0) return l_Categories[0];
            else return new CategoriesView();
        }
        //-----------------------------------------------------------------
        public static List<ArticleFeatures> InitArticleFeatures(SmartDataReader smartReader)
        {
            List<ArticleFeatures> l_ArticleFeatures = new List<ArticleFeatures>();
            while (smartReader.Read())
            {
                ArticleFeatures m_ArticleFeatures = new ArticleFeatures();
                m_ArticleFeatures.ArticleFeatureId = smartReader.GetInt32("ArticleFeatureId");
                m_ArticleFeatures.ArticleId = smartReader.GetInt32("ArticleId");
                m_ArticleFeatures.FeatureId = smartReader.GetInt16("FeatureId");
                m_ArticleFeatures.FeatureValue = smartReader.GetString("FeatureValue");
                m_ArticleFeatures.CrUserId = smartReader.GetInt32("CrUserId");
                m_ArticleFeatures.CrDateTime = smartReader.GetDateTime("CrDateTime");

                l_ArticleFeatures.Add(m_ArticleFeatures);
            }
            return l_ArticleFeatures;
        }
        //-----------------------------------------------------------------
        public static List<TagsView> InitTagsView(SmartDataReader smartReader)
        {
            List<TagsView> l_TagsView = new List<TagsView>();
            while (smartReader.Read())
            {
                TagsView m_Tags = new TagsView();
                m_Tags.LanguageId = smartReader.GetByte("LanguageId");
                m_Tags.TagId = smartReader.GetInt32("TagId");
                m_Tags.TagName = smartReader.GetString("TagName");
                m_Tags.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                m_Tags.CrUserId = smartReader.GetInt32("CrUserId");
                m_Tags.CrDateTime = smartReader.GetDateTime("CrDateTime");

                l_TagsView.Add(m_Tags);
            }
            return l_TagsView;
        }
        //-----------------------------------------------------------------
        public static List<MediasView> InitMediasView(SmartDataReader smartReader)
        {
            List<MediasView> l_MediasView = new List<MediasView>();
            while (smartReader.Read())
            {
                MediasView m_Medias = new MediasView();
                m_Medias.MediaId = smartReader.GetInt32("MediaId");
                m_Medias.MediaTypeId = smartReader.GetByte("MediaTypeId");
                m_Medias.MediaGroupId = smartReader.GetInt16("MediaGroupId");
                m_Medias.MediaName = smartReader.GetString("MediaName");
                m_Medias.MediaDesc = smartReader.GetString("MediaDesc");
                m_Medias.FilePath = smartReader.GetString("FilePath");
                m_Medias.FileSize = smartReader.GetInt32("FileSize");
                m_Medias.CrUserId = smartReader.GetInt32("CrUserId");
                m_Medias.CrDateTime = smartReader.GetDateTime("CrDateTime");

                l_MediasView.Add(m_Medias);
            }
            return l_MediasView;
        }
        //-----------------------------------------------------------------
        public static List<ArticleCommentsView> InitArticleCommentsView(SmartDataReader smartReader)
        {
            List<ArticleCommentsView> l_ArticleCommentsView = new List<ArticleCommentsView>();
            while (smartReader.Read())
            {
                ArticleCommentsView m_ArticleComments = new ArticleCommentsView();
                m_ArticleComments.ArticleCommentId = smartReader.GetInt32("ArticleCommentId");
                m_ArticleComments.LanguageId = smartReader.GetByte("LanguageId");
                m_ArticleComments.ApplicationTypeId = smartReader.GetByte("ApplicationTypeId");
                m_ArticleComments.SiteId = smartReader.GetInt16("SiteId");
                m_ArticleComments.DataTypeId = smartReader.GetByte("DataTypeId");
                m_ArticleComments.ArticleId = smartReader.GetInt32("ArticleId");
                m_ArticleComments.ParentCommentId = smartReader.GetInt32("ParentCommentId");
                m_ArticleComments.CommentLevel = smartReader.GetByte("CommentLevel");
                m_ArticleComments.FullName = smartReader.GetString("FullName");
                m_ArticleComments.Email = smartReader.GetString("Email");
                m_ArticleComments.PhoneNumber = smartReader.GetString("PhoneNumber");
                m_ArticleComments.Comment = smartReader.GetString("Comment");
                m_ArticleComments.RatingScore = smartReader.GetByte("RatingScore");
                m_ArticleComments.FromIP = smartReader.GetString("FromIP");
                m_ArticleComments.UserAgent = smartReader.GetString("UserAgent");
                m_ArticleComments.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                m_ArticleComments.CrDateTime = smartReader.GetDateTime("CrDateTime");

                l_ArticleCommentsView.Add(m_ArticleComments);
            }
            return l_ArticleCommentsView;
        }
        //-----------------------------------------------------------------
        public static List<SongsView> InitSongsView(SmartDataReader smartReader)
        {
            List<SongsView> l_Songs = new List<SongsView>();
            while (smartReader.Read())
            {
                SongsView m_Songs = new SongsView();
                m_Songs.SongId = smartReader.GetInt32("SongId");
                m_Songs.SongSingerId = smartReader.GetInt32("SongSingerId");
                m_Songs.SongSingerName = smartReader.GetString("SongSingerName");
                m_Songs.SongName = smartReader.GetString("SongName");
                m_Songs.SongDesc = smartReader.GetString("SongDesc");
                m_Songs.CountryId = smartReader.GetInt16("CountryId");
                m_Songs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                m_Songs.CrUserId = smartReader.GetInt32("CrUserId");
                m_Songs.CrDateTime = smartReader.GetDateTime("CrDateTime");

                l_Songs.Add(m_Songs);
            }
            return l_Songs;
        }
        //-----------------------------------------------------------------
        public static List<Customers> InitCustomers(SmartDataReader smartReader)
        {
            List<Customers> l_Customers = new List<Customers>();
            try
            {
                while (smartReader.Read())
                {
                    Customers m_Customers = new Customers();
                    m_Customers.CustomerId = smartReader.GetInt32("CustomerId");
                    m_Customers.CustomerName = smartReader.GetString("CustomerName");
                    m_Customers.CustomerPass = smartReader.GetString("CustomerPass");
                    m_Customers.CustomerFullName = smartReader.GetString("CustomerFullName");
                    m_Customers.CustomerNickName = smartReader.GetString("CustomerNickName");
                    m_Customers.CustomerMail = smartReader.GetString("CustomerMail");
                    m_Customers.Website = smartReader.GetString("Website");
                    m_Customers.Facebook = smartReader.GetString("Facebook");
                    m_Customers.Avatar = smartReader.GetString("Avatar");
                    m_Customers.CustomerBalance = smartReader.GetInt32("CustomerBalance");
                    m_Customers.CustomerDayLeft = smartReader.GetInt16("CustomerDayLeft");
                    m_Customers.SiteId = smartReader.GetInt16("SiteId");
                    m_Customers.StatusId = smartReader.GetByte("StatusId");
                    m_Customers.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_Customers.CustomerMobile = smartReader.GetString("CustomerMobile");
                    m_Customers.ClientId = smartReader.GetString("ClientId");
                    m_Customers.GenderId = smartReader.GetByte("GenderId");
                    m_Customers.ProvinceId = smartReader.GetInt16("ProvinceId");
                    m_Customers.DateOfBirth = smartReader.GetDateTime("DateOfBirth");
                    m_Customers.Identifier = smartReader.GetString("Identifier");
                    m_Customers.IMEI = smartReader.GetString("IMEI");
                    m_Customers.GCMRegisterId = smartReader.GetString("GCMRegisterId");
                    m_Customers.MSISDN = smartReader.GetString("MSISDN");
                    m_Customers.EmailAuto = smartReader.GetString("EmailAuto");
                    m_Customers.BusinessApplicationPlatformId = smartReader.GetInt32("BusinessApplicationPlatformId");
                    m_Customers.ApplicationId = smartReader.GetInt16("ApplicationId");
                    m_Customers.PlatformId = smartReader.GetInt16("PlatformId");
                    m_Customers.BusinessPartnerId = smartReader.GetInt16("BusinessPartnerId");
                    m_Customers.AppPlatformId = smartReader.GetInt16("AppPlatformId");
                    m_Customers.MapCustomerId = smartReader.GetInt32("MapCustomerId");
                    m_Customers.CustomerGroupId = smartReader.GetInt16("CustomerGroupId");
                    m_Customers.OccupationId = smartReader.GetInt16("OccupationId");
                    m_Customers.Address = smartReader.GetString("Address");
                    m_Customers.AccountNumber = smartReader.GetString("AccountNumber");
                    m_Customers.OrganName = smartReader.GetString("OrganName");
                    m_Customers.OrganPhone = smartReader.GetString("OrganPhone");
                    m_Customers.OrganFax = smartReader.GetString("OrganFax");
                    m_Customers.OrganAddress = smartReader.GetString("OrganAddress");
                    m_Customers.OrganTaxCode = smartReader.GetString("OrganTaxCode");
                    m_Customers.Note = smartReader.GetString("Note");
                    m_Customers.InfoChannelId = smartReader.GetInt16("InfoChannelId");
                    m_Customers.RegisterNewsletter = smartReader.GetByte("RegisterNewsletter");
                    m_Customers.MaxConcurrentLogin = smartReader.GetByte("MaxConcurrentLogin");
                    m_Customers.LockPassword = smartReader.GetByte("LockPassword");

                    l_Customers.Add(m_Customers);
                }
                return l_Customers;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------------
        public static List<CustomerOnline> InitCustomersOnline(SmartDataReader smartReader)
        {
            List<CustomerOnline> l_CustomerOnline = new List<CustomerOnline>();
            try
            {
                while (smartReader.Read())
                {
                    CustomerOnline m_CustomerOnline = new CustomerOnline();
                    m_CustomerOnline.CustomerOnlineId = smartReader.GetInt32("CustomerOnlineId");
                    m_CustomerOnline.CustomerId = smartReader.GetInt32("CustomerId");
                    m_CustomerOnline.CustomerName = smartReader.GetString("CustomerName");
                    m_CustomerOnline.SessionId = smartReader.GetString("SessionId");
                    m_CustomerOnline.FromIP = smartReader.GetString("FromIP");
                    m_CustomerOnline.LastTime = smartReader.GetDateTime("LastTime");

                    l_CustomerOnline.Add(m_CustomerOnline);
                }
                return l_CustomerOnline;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        //-----------------------------------------------------------------
        public static Customers InitCustomersOne(SmartDataReader smartReader)
        {
            List<Customers> lCustomers = InitCustomers(smartReader);
            if (lCustomers != null && lCustomers.Count > 0)
            {
                return lCustomers[0];
            }
            else
            {
                return new Customers();
            }
        }

        public static List<Roles> InitRoles(SmartDataReader smartReader)
        {
            List<Roles> retVal = new List<Roles>();
            try
            {
                while (smartReader.Read())
                {
                    Roles mRoles = new Roles
                    {
                        RoleId = smartReader.GetByte("RoleId"),
                        RoleName = smartReader.GetString("RoleName"),
                        RoleDesc = smartReader.GetString("RoleDesc")
                    };
                    retVal.Add(mRoles);
                }
                return retVal;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        //-----------------------------------------------------------------
        public static List<DocsView> InitDocs(SmartDataReader smartReader)
        {
            List<DocsView> lDocsView = new List<DocsView>();
            DocsView mDocsView;
            while (smartReader.Read())
            {
                mDocsView = new DocsView
                {
                    DocId = smartReader.GetInt32("DocId"),
                    DocGroupId = smartReader.GetByte("DocGroupId"),
                    DocTypeId = smartReader.GetByte("DocTypeId"),
                    DocIdentity = smartReader.GetString("DocIdentity"),
                    DocIdentityClear = smartReader.GetString("DocIdentityClear"),
                    DocName = smartReader.GetString("DocName"),
                    DocSummary = smartReader.GetString("DocSummary"),
                    DocContent = smartReader.GetString("DocContent"),
                    ExpireDate = smartReader.GetDateTime("ExpireDate"),
                    IssueDate = smartReader.GetDateTime("IssueDate"),
                    EffectDate = smartReader.GetDateTime("EffectDate"),
                    GazetteNumber = smartReader.GetString("GazetteNumber"),
                    GazetteDate = smartReader.GetDateTime("GazetteDate"),
                    EffectStatusId = smartReader.GetByte("EffectStatusId"),
                    EffectStatusName = smartReader.GetString("EffectStatusName"),
                    CrDateTime = smartReader.GetDateTime("CrDateTime"),
                    FieldsText = smartReader.GetString("FieldName"),
                    SignersText = smartReader.GetString("SignerName"),
                    OrgansText = smartReader.GetString("OrganName"),
                    DocTypesText = smartReader.GetString("DocTypeName"),
                    DocUrl = smartReader.GetString("DocUrl"),
                    Result = smartReader.GetString("Result"),
                    IssueYear = smartReader.GetInt16("IssueYear"),
                    LanguageId = smartReader.GetByte("LanguageId"),
                    MetaTitle = smartReader.GetString("MetaTitle"),
                    MetaDesc = smartReader.GetString("MetaDesc"),
                    MetaKeyword = smartReader.GetString("MetaKeyword"),
                    H1Tag = smartReader.GetString("H1Tag"),
                    SeoHeader = smartReader.GetString("SeoHeader"),
                    SeoFooter = smartReader.GetString("SeoFooter"),
                    SocialTitle = smartReader.GetString("SocialTitle"),
                    SocialDesc = smartReader.GetString("SocialDesc"),
                    SocialImage = smartReader.GetString("SocialImage")
            };
                lDocsView.Add(mDocsView);
            }
            return lDocsView;
        }
        //-----------------------------------------------------------------
        public static DocsView InitDocsOne(SmartDataReader smartReader)
        {
            List<DocsView> lDocsView = InitDocs(smartReader);
            if (lDocsView != null && lDocsView.Count > 0)
            {
                return lDocsView[0];
            }
            else
            {
                return new DocsView();
            }
        }

        //-----------------------------------------------------------------
        public static List<DocIndexes> InitDocIndexes(SmartDataReader smartReader)
        {
            List<DocIndexes> lDocIndexes = new List<DocIndexes>();
            while (smartReader.Read())
            {
                DocIndexes mDocIndexes = new DocIndexes
                {
                    DocIndexId = smartReader.GetInt32("DocIndexId"),
                    DocId = smartReader.GetInt32("DocId"),
                    LanguageId = smartReader.GetByte("LanguageId"),
                    Title = smartReader.GetString("Title"),
                    Bookmark = smartReader.GetString("Bookmark"),
                    DisplayOrder = smartReader.GetInt16("DisplayOrder"),
                    LevelId = smartReader.GetByte("LevelId"),
                    CrUserId = smartReader.GetInt32("CrUserId"),
                    CrDateTime = smartReader.GetDateTime("CrDateTime")
                };
                lDocIndexes.Add(mDocIndexes);
            }
            return lDocIndexes;
        }

        //-----------------------------------------------------------------
        public static List<DocFiles> InitDocFiles(SmartDataReader smartReader)
        {
            List<DocFiles> lDocFiles = new List<DocFiles>();
            while (smartReader.Read())
            {
                DocFiles mDocFiles = new DocFiles
                {
                    DocId = smartReader.GetInt32("DocId"),
                    DocFileName = smartReader.GetString("DocFileName"),
                    FileTypeId = smartReader.GetByte("FileTypeId"),
                    FileSize = smartReader.GetInt32("FileSize")
                };
                lDocFiles.Add(mDocFiles);
            }
            return lDocFiles;
        }

        //-----------------------------------------------------------------
        public static List<DocPropertiesView> InitDocProperties(SmartDataReader smartReader)
        {
            List<DocPropertiesView> lDocPropertiesView = new List<DocPropertiesView>();
            while (smartReader.Read())
            {
                DocPropertiesView mDocPropertiesView = new DocPropertiesView
                {
                    DocName = smartReader.GetString("DocName"),
                    DocIdentity = smartReader.GetString("DocIdentity"),
                    DocSummary = smartReader.GetString("DocSummary"),
                    DocTypeName = smartReader.GetString("DocTypeName"),
                    OrganName = smartReader.GetString("OrganName"),
                    SignersName = smartReader.GetString("SignerName"),
                    FieldsName = smartReader.GetString("FieldsName"),
                    IssueDate = smartReader.GetDateTime("IssueDate"),
                    EffectDate = smartReader.GetDateTime("EffectDate"),
                    ExpireDate = smartReader.GetDateTime("ExpireDate"),
                    GazetteNumber = smartReader.GetString("GazetteNumber"),
                    GazetteDate = smartReader.GetDateTime("GazetteDate")
                };
                lDocPropertiesView.Add(mDocPropertiesView);
            }
            return lDocPropertiesView;
        }

        //-----------------------------------------------------------------
        public static DocPropertiesView InitDocPropertiesOne(SmartDataReader smartReader)
        {
            List<DocPropertiesView> lDocPropertiesView = InitDocProperties(smartReader);
            if (lDocPropertiesView != null && lDocPropertiesView.Count > 0)
            {
                return lDocPropertiesView[0];
            }
            else
            {
                return new DocPropertiesView();
            }
        }

        //-----------------------------------------------------------------
        public static List<DocRelates> InitDocRelateEffect(SmartDataReader smartReader)
        {
            List<DocRelates> lDocRelates = new List<DocRelates>();
            while (smartReader.Read())
            {
                DocRelates mDocRelates = new DocRelates
                {
                    DocId = smartReader.GetInt32("DocId"),
                    DocReferenceId = smartReader.GetInt32("DocReferenceId"),
                    RelateTypeId = smartReader.GetByte("RelateTypeId"),
                    DocNameRef = smartReader.GetString("DocNameRef")
                };
                lDocRelates.Add(mDocRelates);
            }
            return lDocRelates;
        }

        //-----------------------------------------------------------------
        public static List<DocRelates> InitDocRelate(SmartDataReader smartReader)
        {
            List<DocRelates> lDocRelates = new List<DocRelates>();
            while (smartReader.Read())
            {
                DocRelates mDocRelates = new DocRelates
                {
                    DocId = smartReader.GetInt32("DocId"),
                    DocRelateId = smartReader.GetInt32("DocRelateId"),
                    DocName = smartReader.GetString("DocName"),
                    IssueDate = smartReader.GetDateTime("IssueDate"),
                    EffectDate = smartReader.GetDateTime("EffectDate"),
                    EffectStatusId = smartReader.GetByte("EffectStatusId"),
                    EffectStatusName = smartReader.GetString("EffectStatusName"),
                    RelateTypeId = smartReader.GetByte("RelateTypeId"),
            };
                lDocRelates.Add(mDocRelates);
            }
            return lDocRelates;
        }

        public static List<DocsView> InitDocsViewNewest(SmartDataReader smartReader)
        {
            List<DocsView> lDocs = new List<DocsView>();
            DocsView mDocsView;
            try
            {
                while (smartReader.Read())
                {
                    mDocsView = new DocsView
                    {
                        DocId = smartReader.GetInt32("DocId"),
                        DocName = smartReader.GetString("DocName"),
                        IssueDate = smartReader.GetDateTime("IssueDate"),
                        IssueYear = smartReader.GetInt16("IssueYear"),
                        EffectDate = smartReader.GetDateTime("EffectDate"),
                        EffectStatusId = smartReader.GetByte("EffectStatusId"),
                        EffectStatusName = smartReader.GetString("EffectStatusName"),
                        DocUrl = smartReader.GetString("DocUrl"),
                        DocGroupId = smartReader.GetByte("DocGroupId"),
                        LanguageId = smartReader.GetByte("LanguageId")
                    };

                    lDocs.Add(mDocsView);
                }
                return lDocs;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        public static List<DocsView> InitDocsMostView(SmartDataReader smartReader)
        {
            List<DocsView> lDocs = new List<DocsView>();
            DocsView mDocsView;
            try
            {
                while (smartReader.Read())
                {
                    mDocsView = new DocsView
                    {
                        DocId = smartReader.GetInt32("DocId"),
                        DocName = smartReader.GetString("DocName"),
                        IssueDate = smartReader.GetDateTime("IssueDate"),
                        EffectDate = smartReader.GetDateTime("EffectDate"),
                        EffectStatusId = smartReader.GetByte("EffectStatusId"),
                        DocUrl = smartReader.GetString("DocUrl"),
                        DocGroupId = smartReader.GetByte("DocGroupId"),
                        H1Tag = smartReader.GetString("H1Tag")
                    };

                    lDocs.Add(mDocsView);
                }
                return lDocs;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        public static List<DocsView> InitCustomerDocsNewest(SmartDataReader smartReader)
        {
            List<DocsView> lDocs = new List<DocsView>();
            DocsView mCustomerDocsNewest;
            try
            {
                while (smartReader.Read())
                {
                    mCustomerDocsNewest = new DocsView
                    {
                        DocId = smartReader.GetInt32("DocId"),
                        DocName = smartReader.GetString("DocName"),
                        IssueDate = smartReader.GetDateTime("IssueDate"),
                        EffectDate = smartReader.GetDateTime("EffectDate"),
                        DocUrl = smartReader.GetString("DocUrl"),
                        DocGroupId = smartReader.GetByte("DocGroupId"),
                        LanguageId = smartReader.GetByte("LanguageId"),
                        H1Tag = smartReader.GetString("H1Tag")
                    };

                    lDocs.Add(mCustomerDocsNewest);
                }
                return lDocs;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        public static List<DocRelates> InitDocsViewRelates(SmartDataReader smartReader)
        {
            List<DocRelates> lDocs = new List<DocRelates>();
            DocRelates mDocRelates;
            try
            {
                while (smartReader.Read())
                {
                    mDocRelates = new DocRelates
                    {
                        DocId = smartReader.GetInt32("DocId"),
                        DocGroupId = smartReader.GetByte("DocGroupId"),
                        LanguageId = smartReader.GetByte("LanguageId"),
                        DisplayPosition = smartReader.GetString("DisplayPosition"),
                        RelateTypeId = smartReader.GetByte("RelateTypeId"),
                        RelateTypeName = smartReader.GetString("RelateTypeName"),
                        DocName = smartReader.GetString("DocName"),
                        DocUrl = smartReader.GetString("DocUrl"),
                        IssueDate = smartReader.GetDateTime("IssueDate"),
                        EffectDate = smartReader.GetDateTime("EffectDate"),
                        EffectStatusId = smartReader.GetByte("EffectStatusId"),
                        IssueYear = smartReader.GetInt16("IssueYear")
                    };
                    lDocs.Add(mDocRelates);
                }
                return lDocs;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        public static List<FieldDisplays> InitFieldDisplays(SmartDataReader smartReader)
        {
            
            List<FieldDisplays> l_FieldDisplays = new List<FieldDisplays>();
            try
            {
                while (smartReader.Read())
                {
                    FieldDisplays m_FieldDisplays = new FieldDisplays();
                    m_FieldDisplays.FieldDisplayId = smartReader.GetInt16("FieldDisplayId");
                    m_FieldDisplays.DisplayTypeId = smartReader.GetInt16("DisplayTypeId");
                    m_FieldDisplays.LanguageId = smartReader.GetByte("LanguageId");
                    m_FieldDisplays.FieldId = smartReader.GetInt16("FieldId");
                    m_FieldDisplays.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    m_FieldDisplays.CrUserId = smartReader.GetInt32("CrUserId");
                    m_FieldDisplays.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    m_FieldDisplays.FieldName = smartReader.GetString("FieldName");
                    m_FieldDisplays.FiedlDesc = smartReader.GetString("FiedlDesc");

                    l_FieldDisplays.Add(m_FieldDisplays);
                }
                return l_FieldDisplays;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                
            }
        }
        public static List<DocsView> InitDocsSearchView(SmartDataReader smartReader)
        {
            List<DocsView> lDocs = new List<DocsView>();
            DocsView mDocsView;
            try
            {
                while (smartReader.Read())
                {
                    mDocsView = new DocsView
                    {
                        DocId = smartReader.GetInt32("DocId"),
                        DocGUId = smartReader.GetString("DocGUId"),
                        DocName = smartReader.GetString("DocName"),
                        DocIdentity = smartReader.GetString("DocIdentity"),
                        DocIdentityClear = smartReader.GetString("DocIdentityClear"),
                        DocTypeId = smartReader.GetByte("DocTypeId"),
                        IssueDate = smartReader.GetDateTime("IssueDate"),
                        IssueYear = smartReader.GetInt16("IssueYear"),
                        EffectDate = smartReader.GetDateTime("EffectDate"),
                        ExpireDate = smartReader.GetDateTime("ExpireDate"),
                        DocGroupId = smartReader.GetByte("DocGroupId"),
                        EffectStatusId = smartReader.GetByte("EffectStatusId"),
                        DocUrl = smartReader.GetString("DocUrl"),
                        LanguageId = smartReader.GetByte("LanguageId"),
                        H1Tag = smartReader.GetString("H1Tag")
                };
                    lDocs.Add(mDocsView);
                }
                return lDocs;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        public static List<DocGroupsView> InitDocGroupsViews(SmartDataReader smartReader)
        {
            List<DocGroupsView> lDocGroupsView = new List<DocGroupsView>();
            try
            {
                while (smartReader.Read())
                {
                    var mDocGroupsView = new DocGroupsView
                    {
                        DocGroupId = smartReader.GetByte("DocGroupId"),
                        DocGroupName = smartReader.GetString("DocGroupName"),
                        DocGroupDesc = smartReader.GetString("DocGroupDesc"),
                        SoLuong = smartReader.GetInt32("SoLuong")
                    };
                    lDocGroupsView.Add(mDocGroupsView);
                }
                return lDocGroupsView;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        public static List<RelateTypes> InitRelateTypes(SmartDataReader smartReader)
        {
            List<RelateTypes> retVal = new List<RelateTypes>();
            RelateTypes mRelateTypes;
            try
            {
                while (smartReader.Read())
                {
                    mRelateTypes = new RelateTypes
                    {
                        RelateTypeId = smartReader.GetByte("RelateTypeId"),
                        RelateTypeName = smartReader.GetString("RelateTypeName"),
                        DisplayPosition = smartReader.GetString("DisplayPosition"),
                        RowCount = smartReader.GetInt32("Soluong")
                    };
                    retVal.Add(mRelateTypes);
                }
                return retVal;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        public static List<Fields> InitFieldsSearchView(SmartDataReader smartReader)
        {
            List<Fields> lFields = new List<Fields>();
            Fields mFields;
            try
            {
                while (smartReader.Read())
                {
                    mFields = new Fields
                    {
                        FieldId = smartReader.GetInt16("FieldId"),
                        FieldName = smartReader.GetString("FieldName"),
                        FieldDesc = smartReader.GetString("FieldDesc"),
                        SoLuong = smartReader.GetInt32("Soluong"),
                    };
                    lFields.Add(mFields);
                }
                return lFields;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        public static List<DocTypes> InitDocTypesSearchView(SmartDataReader smartReader)
        {
            List<DocTypes> lDocTypes = new List<DocTypes>();
            DocTypes mFields;
            try
            {
                while (smartReader.Read())
                {
                    mFields = new DocTypes
                    {
                        DocTypeId = smartReader.GetByte("DocTypeId"),
                        DocTypeName = smartReader.GetString("DocTypeName"),
                        DocTypeDesc = smartReader.GetString("DocTypeDesc"),
                        SoLuong = smartReader.GetInt32("Soluong"),
                    };
                    lDocTypes.Add(mFields);
                }
                return lDocTypes;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        public static List<Organs> InitOrgansSearchView(SmartDataReader smartReader)
        {
            List<Organs> lOrgans = new List<Organs>();
            Organs mOrgans;
            try
            {
                while (smartReader.Read())
                {
                    mOrgans = new Organs
                    {
                        OrganId = smartReader.GetInt16("OrganId"),
                        OrganName = smartReader.GetString("OrganName"),
                        OrganDesc = smartReader.GetString("OrganDesc"),
                        SoLuong = smartReader.GetInt32("Soluong"),
                    };
                    lOrgans.Add(mOrgans);
                }
                return lOrgans;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        public static List<Provinces> InitProvincesSearchView(SmartDataReader smartReader)
        {
            List<Provinces> lProvinces = new List<Provinces>();
            Provinces mProvinces;
            try
            {
                while (smartReader.Read())
                {
                    mProvinces = new Provinces
                    {
                        ProvinceId = smartReader.GetInt16("ProvinceId"),
                        SoLuong = smartReader.GetInt32("Soluong"),
                    };
                    lProvinces.Add(mProvinces);
                }
                return lProvinces;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        public static List<EffectStatus> InitEffectStatusSearchView(SmartDataReader smartReader)
        {
            List<EffectStatus> lEffectStatus = new List<EffectStatus>();
            EffectStatus mEffectStatus;
            try
            {
                while (smartReader.Read())
                {
                    mEffectStatus = new EffectStatus
                    {
                        EffectStatusId = smartReader.GetByte("EffectStatusId"),
                        EffectStatusName = smartReader.GetString("EffectStatusName"),
                        EffectStatusDesc = smartReader.GetString("EffectStatusDesc"),
                        SoLuong = smartReader.GetInt32("Soluong"),
                    };
                    lEffectStatus.Add(mEffectStatus);
                }
                return lEffectStatus;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        public static List<YearView> InitYearViewSearchView(SmartDataReader smartReader)
        {
            List<YearView> lYearView = new List<YearView>();
            YearView mYearView;
            try
            {
                while (smartReader.Read())
                {
                    mYearView = new YearView
                    {
                        Year = smartReader.GetInt32("Year"),
                        SoLuong = smartReader.GetInt32("Soluong"),
                    };
                    lYearView.Add(mYearView);
                }
                return lYearView;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        //-----------------------------------------------------------------
        public static List<CustomerFieldsView> InitCustomerFieldsView(SmartDataReader smartReader)
        {
            List<CustomerFieldsView> lCustomerFieldsView = new List<CustomerFieldsView>();
            try
            {
                while (smartReader.Read())
                {
                    CustomerFieldsView mCustomerFieldsView =
                        new CustomerFieldsView
                        {
                            CustomerFieldId = smartReader.GetInt32("CustomerFieldId"),
                            CustomerId = smartReader.GetInt32("CustomerId"),
                            FieldId = smartReader.GetInt16("FieldId"),
                            DocGroupId = smartReader.GetByte("DocGroupId"),
                            FieldName = smartReader.GetString("FieldName"),
                            FieldDesc = smartReader.GetString("FieldDesc")
                        };

                    lCustomerFieldsView.Add(mCustomerFieldsView);
                }
                return lCustomerFieldsView;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        //-----------------------------------------------------------------
        public static List<CustomerServicesView> InitCustomerServicesView(SmartDataReader smartReader)
        {
            List<CustomerServicesView> lCustomerServicesView = new List<CustomerServicesView>();
            try
            {
                while (smartReader.Read())
                {
                    CustomerServicesView mCustomerServicesView =
                        new CustomerServicesView
                        {
                            CustomerId = smartReader.GetInt32("CustomerId"),
                            ServiceId = smartReader.GetInt16("ServiceId"),
                            ServiceDesc = smartReader.GetString("ServiceDesc"),
                            ServicePackageId = smartReader.GetInt16("ServicePackageId"),
                            ServicePackageDesc = smartReader.GetString("ServicePackageDesc"),
                            ConcurrentLogin = smartReader.GetByte("ConcurrentLogin"),
                            BeginTime = smartReader.GetDateTime("BeginTime"),
                            EndTime = smartReader.GetDateTime("EndTime")
                        };

                    lCustomerServicesView.Add(mCustomerServicesView);
                }
                return lCustomerServicesView;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        //-----------------------------------------------------------------
        public static CustomerServicesView InitCustomerServicesViewOne(SmartDataReader smartReader)
        {
            List<CustomerServicesView> lCustomerServicesView = InitCustomerServicesView(smartReader);
            if (lCustomerServicesView != null && lCustomerServicesView.Count > 0)
            {
                return lCustomerServicesView[0];
            }
            else
            {
                return new CustomerServicesView();
            }
        }

        //-----------------------------------------------------------------
        private static List<CustomerAccessLogs> InitCustomerAccessLogs(SmartDataReader smartReader)
        {
            List<CustomerAccessLogs> lCustomerAccessLogs = new List<CustomerAccessLogs>();
            try
            {
                while (smartReader.Read())
                {
                    CustomerAccessLogs mCustomerAccessLogs = new CustomerAccessLogs
                    {
                        CustomerAccessLogId = smartReader.GetInt32("CustomerAccessLogId"),
                        CustomerId = smartReader.GetInt32("CustomerId"),
                        ActionTypeId = smartReader.GetByte("ActionTypeId"),
                        FromIP = smartReader.GetString("FromIP"),
                        UserAgent = smartReader.GetString("UserAgent"),
                        CrDateTime = smartReader.GetDateTime("CrDateTime"),
                        Channel = smartReader.GetString("Channel"),
                        CrUserId = smartReader.GetInt32("CrUserId"),
                        CustomerName = smartReader.GetString("CustomerName")
                    };
                    lCustomerAccessLogs.Add(mCustomerAccessLogs);
                }
                return lCustomerAccessLogs;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }

        public static CustomerAccessLogs InitCustomerAccessLogsOne(SmartDataReader smartReader)
        {
            List<CustomerAccessLogs> lCustomerAccessLogs = InitCustomerAccessLogs(smartReader);
            if (lCustomerAccessLogs != null && lCustomerAccessLogs.Count > 0)
            {
                return lCustomerAccessLogs[0];
            }
            else
            {
                return new CustomerAccessLogs();
            }
        }

    }

}//end namespace service
