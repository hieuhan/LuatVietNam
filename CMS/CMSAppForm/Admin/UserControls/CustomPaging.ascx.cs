using System;
namespace ICSoft.AppForm.Admin
{
    public partial class CustomPaging : System.Web.UI.UserControl
    {
        private int _TotalPage;
        public int TotalPage
        {
            get { return _TotalPage; }
            set { _TotalPage = value; }
        }
        public int PageIndex
        {
            get { return Int32.Parse(hdfPageIndex.Value == "" ? "1" : hdfPageIndex.Value); }
            set
            {
                hdfOldPage.Value = hdfPageIndex.Value;
                hdfPageIndex.Value = value.ToString();
            }
        }
        public bool ChangePage
        {
            get { return (hdfOldPage.Value != hdfPageIndex.Value); }
        }
        protected string page = "";
        protected int numberofbutton = 3;
        protected int startpage;
        protected int endpage;
        protected void Page_Load(object sender, EventArgs e)
        {
            startpage = PageIndex - numberofbutton <= 0 ? 1 : PageIndex - numberofbutton;
            endpage = 2 * numberofbutton + startpage;
        }
        protected void btChange_Click(object sender, EventArgs e)
        {
            hdfOldPage.Value = hdfPageIndex.Value;
        }
    }
}