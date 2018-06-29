using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processor
{
    public class ContentMediaTypes
    {
        #region private properties
       
        #endregion

        #region public properties
        public static byte Text
        {
            get { return 1; }
            set {}
        }


        public static byte Table
        {
            get { return 2; }
            set { }
        }
        public static byte Image
        {
            get { return 3; }
            set { }
        }
        
        #endregion
        ContentMediaTypes()
        {
            
        }
        
       
    }

}
