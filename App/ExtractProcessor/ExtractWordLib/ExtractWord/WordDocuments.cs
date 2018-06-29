using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ExtractWordLib
{
    public class WordDocuments
    {
        #region private properties
        private string _Publisher;
        
        private string _DocNo;
        
        private DateTime _PublicDate;
        
        private string _DocName;
        
        private string _DocType;
        
        private string _DocNote;
        
        private string _DocFooter;
        private byte _LanguageId;        
        private List<ContentItems> _l_Content;
        private List<Appendices> _l_Appendices;
        private List<string> _l_DocNoRelated;

       
        
        #endregion

        #region public properties
        public string Publisher
        {
            get { return _Publisher; }
            set { _Publisher = value; }
        }
        public string DocNo
        {
            get { return _DocNo; }
            set { _DocNo = value; }
        }
        public DateTime PublicDate
        {
            get { return _PublicDate; }
            set { _PublicDate = value; }
        }
        public string DocName
        {
            get { return _DocName; }
            set { _DocName = value; }
        }
        public string DocType
        {
            get { return _DocType; }
            set { _DocType = value; }
        }
        public string DocNote
        {
            get { return _DocNote; }
            set { _DocNote = value; }
        }
        public string DocFooter
        {
            get { return _DocFooter; }
            set { _DocFooter = value; }
        }
        public byte LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        public List<ContentItems> l_Content
        {
            get { return _l_Content; }
            set { _l_Content = value; }
        }
        public List<Appendices> l_Appendices
        {
            get { return _l_Appendices; }
            set { _l_Appendices = value; }
        }
        public List<string> l_DocNoRelated
        {
            get { return _l_DocNoRelated; }
            set { _l_DocNoRelated = value; }
        }
        #endregion
        #region init
        public WordDocuments()
        {
            l_Content = new List<ContentItems>();
            l_Appendices = new List<Appendices>();
        }
        #endregion
        #region public method
        public int ReadContent(string FileContent, out string ErrorList)
        {
            int RetVal = 0;
            ErrorList = "";
            Extractors m_Extractors = new Extractors();
            string Header, Footer, Content;
            List<Appendices> l_AppendicesTemp = new List<Appendices>();
            List<string> l_DocNoTemp = new List<string>();
            int ReadDocItem = 0;
            DateTime PublicDateRead;
            string NoRead;
            string NoteRead;
            string TypeRead;
            ReadDocItem = m_Extractors.SplitContent(FileContent, out Header, out Content, out Footer, ref l_AppendicesTemp);
            l_Appendices = l_AppendicesTemp;
            this.DocFooter = Footer;            
            this.Publisher = m_Extractors.GetPublisher(Header);
            this.Publisher = ExtractorHelpers.ValidatePublisher(this.Publisher);
            if (m_Extractors.IsVietnamese(Header))
                this.LanguageId = 1;
            else
                this.LanguageId = 2;
            m_Extractors.GetPublicDate(Header, out PublicDateRead);
            this.PublicDate = PublicDateRead;
            m_Extractors.GetLawdocNo(Header + System.Environment.NewLine + Content, out NoRead, ref l_DocNoTemp);
            this.DocNo = NoRead;
            this.l_DocNoRelated = l_DocNoTemp;
            this.DocName = m_Extractors.GetLawdocName(Header, out TypeRead, out NoteRead);
            this.DocNote = NoteRead;
            this.DocType = ExtractorHelpers.ValidateLawdocType( TypeRead);
            //this.l_Content = m_Extractors.ExtractContent(Content, out ErrorList);
            //ExtractorHelpers.ValidateContentTree(this.l_Content);
            return RetVal;
        }
        
        #endregion
    }

}
