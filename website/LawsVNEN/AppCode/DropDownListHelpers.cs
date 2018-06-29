using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVNEN.AppCode;

namespace LawsVNEN.Library
{
    /// <summary>
    /// Danh sách dữ liệu từ điển
    /// </summary>
    public class DropDownListHelpers
    {
        public static List<MenuItemsView> GetAllMenuItems()
        {
            var retVal = new CacheHelpers().GetOrSet<List<MenuItemsView>>(string.Concat("MenuItemsView", LawsVnLanguages.GetCurrentLanguageId()), () => MenusViewHelpers.GetListMenuItemBySiteId(Extensions.GetSiteId()));
            return retVal;
        }

        public static List<MenuItemsView> GetMenuItemsByMenuId(int menuId)
        {
            List<MenuItemsView> retVal = GetAllMenuItems();
            return retVal.Where(x=>x.MenuId==menuId).ToList();
        }

        public static List<Occupations> GetAllOccupations()
        {
            return new CacheHelpers().GetOrSet<List<Occupations>>("Occupations", () => Occupations.Static_GetListAll("..."));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Fields> GetAllFieldsByLanguage()
        {
            int languageId = LawsVnLanguages.GetCurrentLanguageId();
            var retVal = new CacheHelpers().GetOrSet<List<Fields>>(string.Concat("Fields_", languageId), () => Fields.Static_GetListByLanguage(languageId));
            return retVal;
        }

        public static List<Services> GetAllServices()
        {
            List<Services> retVal;
            retVal = new CacheHelpers().GetOrSet<List<Services>>("Services", () => Services.Static_GetList(0, Constants.SiteId_TiengViet));
            return retVal;
        }

        /// <summary>
        /// Lĩnh vực theo vị trí hiển thị
        /// </summary>
        /// <returns></returns>
        public static List<FieldDisplays> GetAllFieldDisplays(short displayTypeId)
        {
            byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
            if (_currentLanguageId == 1)
            {
                return new CacheHelpers().GetOrSet<List<FieldDisplays>>(string.Concat("FieldDisplaysDisplayTypeId", displayTypeId), () => new FieldDisplays().FieldDisplays_GetList(0, "FieldName ASC", displayTypeId, _currentLanguageId));
            }
            else
            {
                return new CacheHelpers().GetOrSet<List<FieldDisplays>>(string.Concat("FieldDisplaysDisplayTypeIdEN", displayTypeId), () => new FieldDisplays().FieldDisplays_GetList(0, "FieldName ASC", displayTypeId, _currentLanguageId));
            }
        }

        /// <summary>
        /// Loại văn bản theo vị trí hiển thị
        /// </summary>
        /// <param name="DisplayTypeId"></param>
        /// <returns></returns>
        public static List<DocTypeDisplays> GetAllDocTypeDisplays(string CacheName, short DisplayTypeId)
        {
            byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
            return new CacheHelpers().GetOrSet<List<DocTypeDisplays>>(CacheName, () => new DocTypeDisplays().DocTypeDisplays_GetList(0, "DisplayOrder ASC", DisplayTypeId, _currentLanguageId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CacheName"></param>
        /// <param name="DisplayTypeId"></param>
        /// <returns></returns>
        public static List<OrganDisplays> GetAllOrganDisplays(string CacheName, short DisplayTypeId)
        {
            byte _currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
            return new CacheHelpers().GetOrSet<List<OrganDisplays>>(CacheName, () => new OrganDisplays().OrganDisplays_GetList(0, "DisplayOrder ASC", DisplayTypeId, _currentLanguageId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public static List<Provinces> GetAllProvinces(string text = "Tỉnh/Thành phố", short countryId = 1)
        {
            var mCacheHelpers = new CacheHelpers();
            List<Provinces> retVal = mCacheHelpers.GetOrSet<List<Provinces>>("Provinces", () => Provinces.Static_GetList(countryId));
            if (mCacheHelpers.IsSussces)
            {
                retVal.Insert(0, new Provinces() { ProvinceId = 0, ProvinceDesc = text });
            }
            return retVal;
        }
        public static List<DocTypes> GetAllDocTypes()
        {
            var mCacheHelpers = new CacheHelpers();
            List<DocTypes> retVal = mCacheHelpers.GetOrSet<List<DocTypes>>("DocTypes", () => DocTypes.Static_GetList());
            return retVal;
        }

        public static List<Organs> GetAllOrgans()
        {
            byte currentLanguageId = LawsVnLanguages.GetCurrentLanguageId();
            List<Organs> retVal = new CacheHelpers().GetOrSet<List<Organs>>(string.Concat("Organs", currentLanguageId), () => new Organs().GetListByOrganId(0, currentLanguageId));
            return retVal;
        }

        public static List<Organs> GetListOrgansByDisplayTypeId(short displayTypeId)
        {
            var mCacheHelpers = new CacheHelpers();
            List<Organs> retVal = mCacheHelpers.GetOrSet<List<Organs>>(string.Concat("OrgansByDisplayTypeId", displayTypeId), () => Organs.Static_GetListbyDisplayTypeId(displayTypeId));
            return retVal;
        }

        public static List<Signers> GetAllSigners()
        {
            var mCacheHelpers = new CacheHelpers();
            List<Signers> retVal = mCacheHelpers.GetOrSet<List<Signers>>("Signers", () => Signers.Static_GetList());
            return retVal;
        }

        public static List<ICSoft.CMSLib.Languages> GetAllLanguages()
        {
            var mCacheHelpers = new CacheHelpers();
            List<ICSoft.CMSLib.Languages> retVal = mCacheHelpers.GetOrSet<List<ICSoft.CMSLib.Languages>>("AllLanguages", () => ICSoft.CMSLib.Languages.Static_GetList());
            return retVal;
        }

        /// <summary>
        /// Tình trạng hiệu lực
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<EffectStatus> GetAllEffectStatus()
        {
            var mCacheHelpers = new CacheHelpers();
            List<EffectStatus> retVal = mCacheHelpers.GetOrSet<List<EffectStatus>>("AllEffectStatus", () => EffectStatus.Static_GetListOrderBy("EffectStatusName"));
            return retVal;
        }

        public static List<DocGroups> GetAllDocGroups()
        {
            var mCacheHelpers = new CacheHelpers();
            List<DocGroups> retVal = mCacheHelpers.GetOrSet<List<DocGroups>>("AllDocGroups", DocGroups.GetList);
            return retVal;
        }

        /// <summary>
        /// Danh sách loại văn bản theo displayTypeId
        /// </summary>
        /// <param name="displayTypeId"></param>
        /// <returns></returns>
        public static List<DocTypes> GetListDocTypesbyDisplayTypeId(short displayTypeId)
        {
            var mCacheHelpers = new CacheHelpers();
            List<DocTypes> retVal = mCacheHelpers.GetOrSet<List<DocTypes>>(string.Concat("GetListDocTypesbyDisplayTypeId_",displayTypeId), () => DocTypes.Static_GetListbyDisplayTypeId(displayTypeId));
            return retVal;
        }

        public static List<DocTypes> DocTypes_GetList(short displayTypeId,byte languageId)
        {
            List<DocTypes> retVal = new CacheHelpers().GetOrSet<List<DocTypes>>(string.Concat("DocTypes_GetList", displayTypeId, languageId), () => new DocTypes().GetListByDocTypeId(0, languageId));
            return retVal;
        }

        public static List<LawTerminGroups> GetAllLawTerminGroups()
        {
            var mCacheHelpers = new CacheHelpers();
            List<LawTerminGroups> retVal = mCacheHelpers.GetOrSet<List<LawTerminGroups>>("AllLawTerminGroups", () => LawTerminGroups.Static_GetList());
            return retVal;
        }
        // linh vuc hoat dong cua luat su
        public static List<Fields> GetAllFieldsLawer()
        {
            var mCacheHelpers = new CacheHelpers();
            List<Fields> retVal = mCacheHelpers.GetOrSet<List<Fields>>("AllFieldsLawer", () => Fields.Static_GetList());
            return retVal;
        }
        // Get quận huyện

        public static List<Districts> GetAllDistricts()
        {
            var mCacheHelpers = new CacheHelpers();
            List<Districts> retVal = mCacheHelpers.GetOrSet<List<Districts>>("GetAllDistricts", () => Districts.Static_GetList());
            return retVal;
        }
        //get Phuong xa
        //public static List<Wards> GetAllWards()
        //{
        //    var mCacheHelpers = new CacheHelpers();
        //    List<Wards> retVal = mCacheHelpers.GetOrSet<List<Wards>>("GetAllWards", () => Wards.Static_GetList());
        //    return retVal;
        //}

        public static IEnumerable<int> GetIssueYears(int fromYear = 2000)
        {
            for (int index = fromYear; index <= DateTime.Now.Year; index++)
            {
                yield return index;
            }
        }

        public static List<ServicePackages> GetAllServicePackages()
        {
            return new CacheHelpers().GetOrSet<List<ServicePackages>>("ServicePackages", () => ServicePackages.Static_GetList(0,Constants.SiteId_TiengViet,0));
        }

        public static List<PaymentTypes> GetAllPaymentTypes()
        {
            return new CacheHelpers().GetOrSet<List<PaymentTypes>>("PaymentTypes", () => PaymentTypes.Static_GetListAll());
        }

        public static ArticlesViewDetail GetArticlesViewDetail(int articleId=0)
        {
            return new CacheHelpers().GetOrSet<ArticlesViewDetail>(string.Concat("ArticlesViewDetail_", articleId), () => ArticlesViewHelpers.View_GetArticleDetail(articleId, 0, 0, 0));
        }

        public static List<TransactionTypes> GetAllPTransactionTypes()
        {
            return new CacheHelpers().GetOrSet<List<TransactionTypes>>("TransactionTypes", () => TransactionTypes.Static_GetList());
        }

    }
}