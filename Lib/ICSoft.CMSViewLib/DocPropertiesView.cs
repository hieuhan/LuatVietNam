using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSoft.LawDocsLib;

namespace ICSoft.CMSViewLib
{
    /// <summary>
    /// Thuộc tính văn bản
    /// </summary>
    public class DocPropertiesView
    {
        private string _docName;
        private string _docIdentity;
        private string _organName;
        private string _gazetteNumber;
        /// <summary>
        /// Ngày công báo
        /// </summary>
        private DateTime _gazetteDate;
        private string _docTypeName;
        private string _signerName;
        /// <summary>
        /// Ngày ban hành
        /// </summary>
        private DateTime _issueDate;

        private DateTime _expireDate;
        private DateTime _effectDate;
        private string _effectStatus;
        private string _fieldName;
        private string _docSummary;
        private List<RelateTypes> _lRelateTypes;
        private List<DocIndexes> _lDocIndexes;

        public List<DocIndexes> lDocIndexes
        {
            get { return _lDocIndexes; }
            set { _lDocIndexes = value; }
        }

        public string DocName
        {
            get { return _docName; }
            set { _docName = value; }
        }

        public string DocIdentity
        {
            get { return _docIdentity; }
            set { _docIdentity = value; }
        }

        public string OrganName
        {
            get { return _organName; }
            set { _organName = value; }
        }

        public string GazetteNumber
        {
            get { return _gazetteNumber; }
            set { _gazetteNumber = value; }
        }

        public DateTime GazetteDate
        {
            get { return _gazetteDate; }
            set { _gazetteDate = value; }
        }

        public string DocTypeName
        {
            get { return _docTypeName; }
            set { _docTypeName = value; }
        }

        public string SignersName
        {
            get { return _signerName; }
            set { _signerName = value; }
        }

        public DateTime IssueDate
        {
            get { return _issueDate; }
            set { _issueDate = value; }
        }

        public DateTime EffectDate
        {
            get { return _effectDate; }
            set { _effectDate = value; }
        }

        public DateTime ExpireDate
        {
            get { return _expireDate; }
            set { _expireDate = value; }
        }

        public string EffectStatus
        {
            get { return _effectStatus; }
            set { _effectStatus = value; }
        }

        public string FieldsName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public string DocSummary
        {
            get { return _docSummary; }
            set { _docSummary = value; }
        }

        public List<RelateTypes> lRelateTypes
        {
            get { return _lRelateTypes; }
            set { _lRelateTypes = value; }
        }

        //-----------------------------------------------------------------
        public DocPropertiesView()
        {
        }
        //-----------------------------------------------------------------
        ~DocPropertiesView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
    }
}
