using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSLib;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;

namespace LawsVN.Library
{
    /// <summary>
    /// Danh sách dữ liệu từ điển
    /// </summary>
    public class DropDownListHelpers
    {
        public static List<MenuItemsView> GetAllMenuItems()
        {
            CacheHelpers mCacheHelpers = new CacheHelpers();
            var retVal = mCacheHelpers.GetOrSet<List<MenuItemsView>>("MenuItemsView", () => MenusViewHelpers.GetListMenuItemBySiteId(Constants.SiteId));
            return retVal;
        }

        public static List<MenuItemsView> GetMenuItemsByMenuId(int menuId)
        {
            List<MenuItemsView> retVal = GetAllMenuItems();
            return retVal.Where(x=>x.MenuId==menuId).ToList();
        }

        public static List<CategoriesView> ListCategoriesViewByParentId(short parentCategoryId = 0)
        {
            List<CategoriesView> list = new List<CategoriesView>();
            if (parentCategoryId > 0)
            {
                list = new CacheHelpers().GetOrSet<List<CategoriesView>>("ListCategoriesViewByParentId",
                    () => CategoriesViewHelpers.View_GetListByParentId(parentCategoryId));
            }
            return list;
        }

        public static List<Occupations> GetAllOccupations()
        {
            Occupations mOccupations = new Occupations();
            return new CacheHelpers().GetOrSet<List<Occupations>>("Occupations", () => mOccupations.GetListOrderBy("OccupationDesc ASC"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Fields> GetAllFields()
        {
            CacheHelpers mCacheHelpers = new CacheHelpers();
            var retVal = mCacheHelpers.GetOrSet<List<Fields>>("Fields", Fields.Static_GetListApproved);
            return retVal;
        }

        public static List<Services> GetAllServices()
        {
            List<Services> retVal;
            retVal = new CacheHelpers().GetOrSet<List<Services>>("Services", ()=>Services.Static_GetList(0, Constants.SiteId));
            return retVal;
        }

        /// <summary>
        /// Lĩnh vực theo vị trí hiển thị
        /// </summary>
        /// <returns></returns>
        /// 
        public static List<FieldDisplays> GetAllFieldDisplays_OrderBy(short displayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<FieldDisplays>>(string.Concat("FieldDisplaysDisplayTypeId", displayTypeId), () => new FieldDisplays().FieldDisplays_GetList(0, "FieldName ASC", displayTypeId, 1));
        }
        public static List<Fields> GetAllFieldByDisplayTypeId(short displayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<Fields>>(string.Concat("AllFieldsDisplayTypeId", displayTypeId), () => Fields.Static_GetListbyDisplayTypeId(displayTypeId));
        }
        
        public static List<FieldDisplays> GetAllFieldDisplays(short displayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<FieldDisplays>>(string.Concat("FieldDisplaysDisplayTypeId", displayTypeId), () => new FieldDisplays().FieldDisplays_GetList(0, "DisplayOrder ASC", displayTypeId, 1));
        }
        public static List<OrganOccupations> GetAllOrganOccupations()
        {
            return new CacheHelpers().GetOrSet<List<OrganOccupations>>("FieldDisplayCache", () => new OrganOccupations().GetListOrderBy("OrganOccupationDesc ASC"));
        }
        public static List<Fields> GetAllFieldDisplayCache(short displayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<Fields>>("FieldDisplayCache", () => GetAllFieldOrder(displayTypeId));
        }

        public static List<Fields> GetAllFieldOrder(short displayTypeId)
        {
            List<FieldDisplays> listFieldDisplays = GetAllFieldDisplays(displayTypeId);
            List<Fields> listFields = GetAllFields();

            List<Fields> list_FieldsDisplays = new List<Fields>();
            foreach(FieldDisplays FieldDisplays in listFieldDisplays)
            {
                foreach (Fields mFields in listFields)
                {
                    if(mFields.FieldId == FieldDisplays.FieldId)
                    {
                        list_FieldsDisplays.Add(mFields); break;
                    }
                }
            }
            List<Fields> list_FieldsOther = listFields.Where(f => listFieldDisplays.All(d => f.FieldId != d.FieldId)).ToList();
            list_FieldsDisplays.AddRange(list_FieldsOther);
            return list_FieldsDisplays;
        }

        /// <summary>
        /// Loại văn bản theo vị trí hiển thị
        /// </summary>
        /// <param name="DisplayTypeId"></param>
        /// <returns></returns>
        public static List<DocTypeDisplays> GetAllDocTypeDisplays(string CacheName, short DisplayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<DocTypeDisplays>>(CacheName, () => new DocTypeDisplays().DocTypeDisplays_GetList(0, "DisplayOrder ASC", DisplayTypeId, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CacheName"></param>
        /// <param name="DisplayTypeId"></param>
        /// <returns></returns>
        public static List<OrganDisplays> GetAllOrganDisplays(string CacheName, short DisplayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<OrganDisplays>>(CacheName, () => new OrganDisplays().OrganDisplays_GetList(0, "DisplayOrder ASC", DisplayTypeId, 0));
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
            var mCacheHelpers = new CacheHelpers();
            List<Organs> retVal = mCacheHelpers.GetOrSet<List<Organs>>("Organs", () => Organs.Static_GetList());
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
            List<EffectStatus> retVal = mCacheHelpers.GetOrSet<List<EffectStatus>>("AllEffectStatus", () => EffectStatus.Static_GetList());
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

        public static List<DocTypes> DocTypes_GetList(short displayTypeId)
        {
            var mCacheHelpers = new CacheHelpers();
            List<DocTypes> retVal = mCacheHelpers.GetOrSet<List<DocTypes>>(string.Concat("DocTypes_GetList", displayTypeId), () => new DocTypes{ReviewStatusId = 2}.GetListByDocTypeId(0,LawsVnLanguages.GetCurrentLanguageId()));
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
            return new CacheHelpers().GetOrSet<List<ServicePackages>>("ServicePackages", () => ServicePackages.Static_GetList(0,Constants.SiteId,0));
        }

        public static List<PaymentTypes> GetAllPaymentTypes()
        {
            return new CacheHelpers().GetOrSet<List<PaymentTypes>>("PaymentTypes", () => PaymentTypes.Static_GetListAll());
        }

        public static List<TransactionTypes> GetAllPTransactionTypes()
        {
            return new CacheHelpers().GetOrSet<List<TransactionTypes>>("TransactionTypes", () => TransactionTypes.Static_GetList());
        }

        public static ArticlesViewDetail GetArticlesViewDetail(int articleId = 0)
        {
            return new CacheHelpers().GetOrSet<ArticlesViewDetail>(string.Concat("ArticlesViewDetail_", articleId), () => ArticlesViewHelpers.View_GetArticleDetail(articleId, 0, 0, 0));
        }

    }
}