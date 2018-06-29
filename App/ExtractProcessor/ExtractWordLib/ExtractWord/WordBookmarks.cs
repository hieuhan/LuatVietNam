using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
namespace ICSoft.ExtractWordLib
{
    public class WordBookmarks
    {
        #region private properties
        private string _BookmarkName;
        private string _BookmarkDesc;
        private short _BookmarkLevel;
        private string _RegexRulesGroup;

        
        #endregion


        #region public properties
        public string BookmarkName
        {
            get { return _BookmarkName; }
            set { _BookmarkName = value; }
        }
        public string BookmarkDesc
        {
            get { return _BookmarkDesc; }
            set { _BookmarkDesc = value; }
        }

        public short BookmarkLevel
        {
            get { return _BookmarkLevel; }
            set { _BookmarkLevel = value; }
        }
        
        #endregion
        public WordBookmarks()
        {
            //init properties
            BookmarkName = "";
            BookmarkDesc = "";
            BookmarkLevel = 0;
        }
        //=========================================================
        
    }

}
