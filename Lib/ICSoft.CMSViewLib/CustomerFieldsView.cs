using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.CMSViewLib
{
    public class CustomerFieldsView
    {
        private short _fieldId;
        private string _fieldName;
        private string _fieldDesc;
        private int _customerId;
        private byte _docGroupId;
        private DateTime _crDateTime;
        private int _customerFieldId;

        public short FieldId
        {
            get { return _fieldId; }
            set { _fieldId = value; }
        }

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public string FieldDesc
        {
            get { return _fieldDesc; }
            set { _fieldDesc = value; }
        }

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public byte DocGroupId
        {
            get { return _docGroupId; }
            set { _docGroupId = value; }
        }

        public DateTime CrDateTime
        {
            get { return _crDateTime; }
            set { _crDateTime = value; }
        }

        public int CustomerFieldId
        {
            get { return _customerFieldId; }
            set { _customerFieldId = value; }
        }

        //-----------------------------------------------------------------
        public CustomerFieldsView()
        {
        }
        //-----------------------------------------------------------------
        ~CustomerFieldsView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //----------------------------------------------------------------
    }
}
