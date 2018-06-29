using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.CMSViewLib
{
    public class YearView
    {
        private int _Year;
        private int _SoLuong;
        //-----------------------------------------------------------------
        ~YearView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        //-----------------------------------------------------------------    
        public int SoLuong
        {
            get { return _SoLuong; }
            set { _SoLuong = value; }
        }
    }
}
