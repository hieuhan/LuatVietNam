using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSoft.CMSLibEstate;

namespace ICSoft.CMSViewLib
{
    public class EstatePostsViewDetail
    {
        public EstatePosts mEstatePosts;
        public EstatePostForeignKeyDesc mEstatePostForeignKeyDesc;
        public CategoriesView mCategory;
        public ProfileAgencies mProfileAgency;
        public List<TagsView> lTag;
        public List<EstatePostMedias> lEstatePostMedias;
        public List<EstatePostComments> lEstatePostComments;
        public List<EstatePosts> lOtherEstatePosts;
    }
}
