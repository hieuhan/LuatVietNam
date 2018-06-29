using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSoft.LawDocsLib;

namespace ICSoft.CMSViewLib
{
    public class DocsViewDetail
    {
        private DocsView _mDocsView;
        private List<DocFiles> _lDocFiles;
        private List<DocIndexes> _lDocIndexes;
        private DocsView _mDocsEngView;
        private DocPropertiesView _mDocPropertiesView;
        private List<DocRelates> _lDocRelates;
        private List<DocsView> _lDocOthers;
        private List<RelateTypes> _lRelateTypes;
        private List<FieldDisplays> _lFieldDisplays;
        private List<DocsView> _lDocOthersNewest;

        public List<DocRelates> lDocRelates
        {
            get { return _lDocRelates; }
            set { _lDocRelates = value; }
        }

        public DocsView mDocsView
        {
            get { return _mDocsView; }
            set { _mDocsView = value; }
        }

        public DocsView mDocsEngView
        {
            get { return _mDocsEngView; }
            set { _mDocsEngView = value; }
        }

        public List<DocFiles> lDocFiles
        {
            get { return _lDocFiles; }
            set { _lDocFiles = value; }
        }

        public List<DocIndexes> lDocIndexes
        {
            get { return _lDocIndexes; }
            set { _lDocIndexes = value; }
        }

        public DocPropertiesView mDocPropertiesView
        {
            get { return _mDocPropertiesView; }
            set { _mDocPropertiesView = value; }
        }

        public List<DocsView> lDocOthers
        {
            get
            {
                return _lDocOthers;
            }

            set
            {
                _lDocOthers = value;
            }
        }

        public List<DocsView> lDocOthersNewest
        {
            get
            {
                return _lDocOthersNewest;
            }

            set
            {
                _lDocOthersNewest = value;
            }
        }

        public List<RelateTypes> lRelateTypes
        {
            get { return _lRelateTypes; }
            set { _lRelateTypes = value; }
        }

        public List<FieldDisplays> lFieldDisplays
        {
            get
            {
                return _lFieldDisplays;
            }

            set
            {
                _lFieldDisplays = value;
            }
        }

        //-----------------------------------------------------------------
        public DocsViewDetail()
        {
        }
        //-----------------------------------------------------------------
        ~DocsViewDetail()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //----------------------------------------------------------------
    }
}
