using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.CMSViewLib;
using ICSoft.LawDocsLib;
using LawsVN.App_GlobalResources;
using LawsVN.Library;

namespace LawsVN.Models
{
    public class HomeViewModel:ViewModelBase
    {
        
        //public ArticlesViewHome mArticlesViewHome { get; set; }
        /// <summary>
        /// Điểm tin VB mới -Top1, Tin tức pháp luật - Top2 ,Tin vắn chính sách mới - Right1, Bản tin luật việt nam : Right2, Bản tin cập nhật hàng tuần: Right3
        /// </summary>
        public ArticlesViewMaster mArticlesViewMaster { get; set; }

        /// <summary>
        /// Danh sách văn bản 3 box trang chủ
        /// </summary>
        public List<DocsView> ListDocsViewFirst { get; set; }
        public List<DocsView> ListDocsViewSecond { get; set; }
        public List<DocsView> ListDocsViewThird { get; set; }

        /// <summary>
        /// Partial phân trang ajax 3 box
        /// </summary>
        public PartialPaginationAjax PartialPaginationAjaxFirst { get; set; }
        public PartialPaginationAjax PartialPaginationAjaxSecond { get; set; }
        public PartialPaginationAjax PartialPaginationAjaxThird { get; set; }

        /// <summary>
        /// Menu right dưới box Lĩnh vực tra cứu
        /// </summary>
        public List<MenuItemsView> ListMenuItemsView { get; set; }

        #region ds tu dien

        public List<Fields> ListFields
        {
            get { return DropDownListHelpers.GetAllFields(); }
        }

        public List<DocTypes> ListDocTypes
        {
            get { return DropDownListHelpers.GetAllDocTypes(); }
        }

        public List<Organs> ListOrgans
        {
            get { return DropDownListHelpers.GetAllOrgans(); }
        }

        public List<Signers> ListSigners
        {
            get { return DropDownListHelpers.GetAllSigners(); }
        }

        public List<DocGroups> ListDocGroups
        {
            get { return DropDownListHelpers.GetAllDocGroups(); }
        }

        public CategoriesView mSharedCorner
        {
            get
            {
                if (_mSharedCorner != null)
                    return _mSharedCorner;
                else
                {
                    ArticlesViewHome m_ArticlesViewHome = ArticlesViewHelpers.View_GetArticleHome(Constants.SiteId, 1, 1, 0, Constants.RowAmount3, 0);
                    if (m_ArticlesViewHome.lCategoriesMain3.Count > 1)
                        return m_ArticlesViewHome.lCategoriesMain3[1];
                    else
                        return new CategoriesView();
                }
            }

            set
            {
                _mSharedCorner = value;
            }
        }

        private CategoriesView _mSharedCorner;
        #endregion
    }
}