using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ExtractWordLib
{
    public class ContentItems
    {
        #region private properties
        private string _ItemName;        

        private string _ItemDesc;
        
        private string _ItemValue;        

        private int _ItemLevel;
        
        private int _DocLevel;
        
        private List<ContentItems> _l_Children;

        #endregion

        #region public properties
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }

        public string ItemDesc
        {
            get { return _ItemDesc; }
            set { _ItemDesc = value; }
        }

        public string ItemValue
        {
            get { return _ItemValue; }
            set { _ItemValue = value; }
        }

        public int ItemLevel
        {
            get { return _ItemLevel; }
            set { _ItemLevel = value; }
        }

        public int DocLevel
        {
            get { return _DocLevel; }
            set { _DocLevel = value; }
        }

        public List<ContentItems> l_Children
        {
            get { return _l_Children; }
            set { _l_Children = value; }
        }
        #endregion
        public ContentItems()
        {
            ItemName = "";
            ItemDesc = "";
            ItemValue = "";
            ItemLevel = 0;
            DocLevel = RegexLevels.Part;
            l_Children = new List<ContentItems>();
        }
        public ContentItems Clone(ContentItems m_ContentItems)
        {
            ContentItems RetVal = new ContentItems();
            RetVal.ItemName = m_ContentItems.ItemName;
            RetVal.ItemDesc = m_ContentItems.ItemDesc;
            RetVal.ItemValue = m_ContentItems.ItemValue;
            RetVal.ItemLevel = m_ContentItems.ItemLevel;
            RetVal.DocLevel = m_ContentItems.DocLevel;
            RetVal.l_Children = new List<ContentItems>();
            foreach (ContentItems m_ContentItemsChildren in m_ContentItems.l_Children)
            {
                RetVal.l_Children.Add(CopyProperties(m_ContentItemsChildren));
            }
            return RetVal;
        }
        public ContentItems CopyProperties(ContentItems m_ContentItems)
        {
            ContentItems RetVal = new ContentItems();
            RetVal.ItemName = m_ContentItems.ItemName;
            RetVal.ItemDesc = m_ContentItems.ItemDesc;
            RetVal.ItemValue = m_ContentItems.ItemValue;
            RetVal.ItemLevel = m_ContentItems.ItemLevel;
            RetVal.DocLevel = m_ContentItems.DocLevel;
            RetVal.l_Children = new List<ContentItems>();
            return RetVal;
        }
    }

}
