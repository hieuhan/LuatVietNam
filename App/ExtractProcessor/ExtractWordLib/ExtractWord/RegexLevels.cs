using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ExtractWordLib
{
    public class RegexLevels
    {
        #region private properties
       
        #endregion

        #region public properties
        public static short Part
        {
            get { return 1; }
            set {}
        }


        public static short Chapter
        {
            get { return 2; }
            set { }
        }
        public static short Section
        {
            get { return 3; }
            set { }
        }
        public static short SubSection
        {
            get { return 4; }
            set { }
        }
        public static short Article
        {
            get { return 5; }
            set { }
        }
        public static short Parent
        {
            get { return 6; }
            set { }
        }
        public static short Children
        {
            get { return 7; }
            set { }
        }
        public static short Header
        {
            get { return 8; }
            set { }
        }
        public static short Footer
        {
            get { return 9; }
            set { }
        }
        public static short Appendix
        {
            get { return 10; }
            set { }
        }
        #endregion
        RegexLevels()
        {
            
        }
        
       
    }

}
