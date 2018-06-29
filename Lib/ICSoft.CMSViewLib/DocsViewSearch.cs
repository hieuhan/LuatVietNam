using ICSoft.CMSLib;
using ICSoft.LawDocsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.CMSViewLib
{
    public class DocsViewSearch
    {
        private List<DocsView> _lDocsView;
        private List<Fields> _lFields;
        private List<DocTypes> _lDocTypes;
        private List<Organs> _lOrgans;
        private List<Provinces> _lProvinces;
        private List<EffectStatus> _lEffectStatus;
        private List<YearView> _YearView;
        private List<DocGroupsView> _lDocGroupsView;

        //-----------------------------------------------------------------
        ~DocsViewSearch()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public List<DocsView> lDocsView
        {
            get { return _lDocsView; }
            set { _lDocsView = value; }
        }
        //-----------------------------------------------------------------    
        public List<Fields> lFields
        {
            get { return _lFields; }
            set { _lFields = value; }
        }
        //-----------------------------------------------------------------    
        public List<DocTypes> lDocTypes
        {
            get { return _lDocTypes; }
            set { _lDocTypes = value; }
        }
        //-----------------------------------------------------------------    
        public List<Organs> lOrgans
        {
            get { return _lOrgans; }
            set { _lOrgans = value; }
        }
        //-----------------------------------------------------------------    
        public List<Provinces> lProvinces
        {
            get { return _lProvinces; }
            set { _lProvinces = value; }
        }
        //-----------------------------------------------------------------    
        public List<EffectStatus> lEffectStatus
        {
            get { return _lEffectStatus; }
            set { _lEffectStatus = value; }
        }
        //-----------------------------------------------------------------    
        public List<YearView> YearView
        {
            get { return _YearView; }
            set { _YearView = value; }
        }

        public List<DocGroupsView> lDocGroupsView
        {
            get { return _lDocGroupsView; }
            set { _lDocGroupsView = value; }
        }
    }
}
