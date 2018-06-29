using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ExtractWordLib
{
    public class Appendices
    {
        #region private properties
        private string _AppendixName;
        private string _AppendixValue;   
        #endregion

        #region public properties
        public string AppendixName
        {
            get { return _AppendixName; }
            set { _AppendixName = value; }
        }

        public string AppendixValue
        {
            get { return _AppendixValue; }
            set { _AppendixValue = value; }
        }

        
        #endregion
        public Appendices()
        {
            AppendixName = "";
            AppendixValue = "";
        }
        
    }

}
