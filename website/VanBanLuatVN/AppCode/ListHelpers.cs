using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using ICSoft.ViewLibV3;

namespace VanBanLuat.Librarys
{
    public class ListHelpers
    {
        #region MenuItems
        /// <summary>
        /// Danh sách MenuItem theo Site
        /// </summary>
        /// <returns>retVal</returns>
        public static List<MenuItems> GetListMenuItem()
        {
            var retVal = new CacheHelpers().GetOrSet<List<MenuItems>>("MenuItems", () => MenuHelpers.GetListMenuItem(0,Constants.SiteId));
            return retVal;
        }

        /// <summary>
        /// Danh sách MenuItem theo MenuId
        /// </summary>
        /// <param name="menuId">menuId</param>
        /// <returns>retVal</returns>
        public static List<MenuItems> GetListByMenuId(int menuId)
        {
            List<MenuItems> list = GetListMenuItem();
            return MenuHelpers.GetListByMenuId(menuId, list);
        }
        #endregion

        #region Fields

        /// <summary>
        /// Danh sách lĩnh vực theo DisplayTypeId
        /// </summary>
        /// <param name="displayTypeId">displayTypeId</param>
        /// <returns></returns>
        /// //lấy theo mức độ ưu tiên
        public static List<Fields> FieldsGetListByDisplayType(short displayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<Fields>>(string.Concat("FieldsGetListByDisplayType", displayTypeId), () => Fields.Static_GetListByDisplayType(displayTypeId,2,1,"FieldId,FieldName,FieldDesc","DisplayOrder Desc"));
        }
        // theo bang chu cai
        public static List<Fields> FieldsGetListByDisplayTypeV2(short displayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<Fields>>(string.Concat("FieldsGetListByDisplayType", displayTypeId), () => Fields.Static_GetListByDisplayType(displayTypeId, 2, 1, "FieldId,FieldName,FieldDesc", "FieldDesc Asc"));
        }

        public static List<Fields> FieldsGetList()
        {
            return new CacheHelpers().GetOrSet<List<Fields>>("FieldsGetList", ()=>
                Fields.Static_GetList(Constants.ReviewStatusIdApproved, Constants.LanguageId));
        }
        #endregion

        #region DocList
        /// <summary>
        /// Danh sách thống kê bộ lọc tìm văn bản
        /// </summary>
        /// <returns></returns>
        public static DocList GetDocListFilter()
        {
            return new CacheHelpers().GetOrSet<DocList>("DocListFilter", () => DocHelpers.GetPage(new DocFilterParams
            {
                GetRowCount = 1,
                GetCountByGroup = 1,
                GetCountByEffectStatus = 1
                //GetCountByDocType = 1
            }));
        }

        /// <summary>
        /// Top danh sách thống kê bộ lọc tìm kiếm vb order theo số lượng
        /// </summary>
        /// <returns></returns>
        public static DocList GetDocListFilterOrderByDocCount()
        {
            var list = GetDocListFilter();
            if (list.lCountByDocGroup.HasValue())
            {
                list.lCountByDocGroup = list.lCountByDocGroup.OrderByDescending(docGroup => docGroup.DocCount).ToList();
            }
            if (list.lCountByEffectStatus.HasValue())
            {
                list.lCountByEffectStatus =
                    list.lCountByEffectStatus.OrderByDescending(effectStatus => effectStatus.DocCount).ToList();
            }
            //if (list.lCountByDocType.HasValue())
            //{
            //    list.lCountByDocType =
            //        list.lCountByDocType.OrderByDescending(docType => docType.DocCount)
            //            .Take(Constants.RowAmount6).ToList();
            //}
            return list;
        }
        #endregion

        #region DocTypes
        public static List<DocTypes> DocTypesGetList()
        {
            return new CacheHelpers().GetOrSet<List<DocTypes>>("DocTypesGetList", () => DocTypes.Static_GetList(Constants.ReviewStatusIdApproved, Constants.LanguageId));
        }
        public static List<DocTypes> DocTypesGetListByDisplayType(short displayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<DocTypes>>(string.Concat("DocTypesGetListByDisplayType", displayTypeId), () => displayTypeId > 0 ? DocTypes.Static_GetListByDisplayType(displayTypeId,Constants.ReviewStatusIdApproved,Constants.LanguageId) : DocTypesGetList());
        }


        #endregion

        #region Organs

        public static List<Organs> OrgansGetList()
        {
            return new CacheHelpers().GetOrSet<List<Organs>>("OrgansGetList", () => Organs.Static_GetList(Constants.ReviewStatusIdApproved,Constants.LanguageId));
        }
        public static List<Organs> OrgansGetListByDisplayType(short displayTypeId)
        {
            return new CacheHelpers().GetOrSet<List<Organs>>(string.Concat("OrgansGetListByDisplayType", displayTypeId), () => Organs.Static_GetListByDisplayType(displayTypeId, Constants.ReviewStatusIdApproved, Constants.LanguageId));
        }


        #endregion

        #region Languages

        public static List<Languages> LanguagesGetList()
        {
            return new CacheHelpers().GetOrSet<List<Languages>>("LanguagesGetList", () => Languages.Static_GetList());
        }

        #endregion

        #region EffectStatus

        public static List<EffectStatus> EffectStatusGetList()
        {
            return new CacheHelpers().GetOrSet<List<EffectStatus>>("EffectStatusGetList", () => EffectStatus.Static_GetList());
        }

        #endregion

        #region DocGroups

        public static List<DocGroups> DocGroupsGetList()
        {
            return new CacheHelpers().GetOrSet<List<DocGroups>>("DocGroupsGetList", () => DocGroups.Static_GetList().Where(x=>x.DocGroupId != Constants.DocGroupIdKhongCoNoiDungDownload).ToList());
        }

        #endregion

        #region Occupations
        public static List<Occupations> GetAllOccupations()
        {
            Occupations mOccupations = new Occupations();
            return new CacheHelpers().GetOrSet<List<Occupations>>("Occupations", () => mOccupations.GetListOrderBy("OccupationDesc ASC"));
        }
        #endregion

        #region Categories
        public static List<Categories> GetCategoryById(short categoryId = 0)
        {
            if (categoryId > 0)
            {
                return new CacheHelpers().GetOrSet<List<Categories>>("GetCategoryById", () => CategoryHelpers.GetByParentItemId(categoryId));
            }
            return new List<Categories>();
        }
        #endregion
    }
}