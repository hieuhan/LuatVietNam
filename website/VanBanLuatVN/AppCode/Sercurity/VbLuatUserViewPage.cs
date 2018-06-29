using System.Web.Mvc;

namespace VanBanLuatVN.Library.Sercurity
{
    /// <summary>
    /// Ghi đè User base về LawsUser trả về render view, 
    /// phía view sử dụng LawsUser để truy cập thuộc tính của User đang sử dụng
    /// </summary>
    public abstract class VbLuatUserViewPage : WebViewPage
    {
        public virtual VbLuatPrincipal VbLuatUser
        {
            get { return base.User as VbLuatPrincipal; }
        }
    }
    public abstract class VbLuatUserViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual VbLuatPrincipal VbLuatUser
        {
            get { return base.User as VbLuatPrincipal; }
        }
    }
}