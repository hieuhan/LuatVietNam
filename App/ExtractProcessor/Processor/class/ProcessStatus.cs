using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processor
{
    public class ProcessStatus
    {
        #region private properties
       
        #endregion

        #region public properties
        public static string Sleeping
        {
            get { return "Sleeping"; }
            set {}
        }


        public static string Processing
        {
            get { return "Processing"; }
            set { }
        }

        #endregion
        ProcessStatus()
        {
            
        }
        
       
    }

}
