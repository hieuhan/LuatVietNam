using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processor
{
    public class FileTypeHelpers
    {
        #region private properties
       
        #endregion

        #region public properties
        public static byte Doc
        {
            get { return 1; }
            set {}
        }


        public static byte Pdf
        {
            get { return 2; }
            set { }
        }
        public static byte Html
        {
            get { return 3; }
            set { }
        }
        public static byte Docx
        {
            get { return 4; }
            set { }
        }
        
        #endregion
        FileTypeHelpers()
        {
            
        }
        
       
    }

}
