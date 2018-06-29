using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processor
{
    public class DocItemLevels
    {
        #region private properties
       
        #endregion

        #region public properties
        public static byte Part
        {
            get { return 1; }
            set {}
        }


        public static byte Chapter
        {
            get { return 2; }
            set { }
        }
        public static byte Section
        {
            get { return 3; }
            set { }
        }
        public static byte SubSection
        {
            get { return 4; }
            set { }
        }
        public static byte Article
        {
            get { return 5; }
            set { }
        }
        public static byte Parent
        {
            get { return 6; }
            set { }
        }
        public static byte Children
        {
            get { return 7; }
            set { }
        }
        public static byte Header
        {
            get { return 8; }
            set { }
        }
        public static byte Footer
        {
            get { return 9; }
            set { }
        }
        public static byte Appendix
        {
            get { return 10; }
            set { }
        }
        #endregion
        DocItemLevels()
        {
            
        }
        
       
    }

}
