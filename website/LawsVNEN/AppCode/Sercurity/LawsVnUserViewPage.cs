using LawsVN.Library.Sercurity;
using System.Web.Mvc;

namespace LawsVN.Library.Sercurity
{
    /// <summary>
    /// Ghi đè User base về LawsUser trả về render view, 
    /// phía view sử dụng LawsUser để truy cập thuộc tính của User đang sử dụng
    /// </summary>
    public abstract class LawsVnUserViewPage : WebViewPage
    {
        public virtual LawsVnPrincipal LawsUser
        {
            get { return base.User as LawsVnPrincipal; }
        }
    }
    public abstract class LawsVnUserViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual LawsVnPrincipal LawsUser
        {
            get { return base.User as LawsVnPrincipal; }
        }
    }
}