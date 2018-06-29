using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.CMSViewLib
{
    public class CustomerServicesView
    {
        private short _serviceId;
        private int _customerId;
        private short _servicePackageId;
        private DateTime _beginTime;
        private DateTime _endTime;
        private byte _paymentTypeId;
        private byte _statusId;
        private string _servicePackageDesc;
        private short _numDayUse;
        private short _numMonthUse;
        private byte _concurrentLogin;
        private string _serviceDesc;

        public short ServiceId
        {
            get { return _serviceId; }
            set { _serviceId = value; }
        }

        public string ServiceDesc
        {
            get { return _serviceDesc; }
            set { _serviceDesc = value; }
        }

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public short ServicePackageId
        {
            get { return _servicePackageId; }
            set { _servicePackageId = value; }
        }

        public DateTime BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public byte PaymentTypeId
        {
            get { return _paymentTypeId; }
            set { _paymentTypeId = value; }
        }

        public byte StatusId
        {
            get { return _statusId; }
            set { _statusId = value; }
        }

        public string ServicePackageDesc
        {
            get { return _servicePackageDesc; }
            set { _servicePackageDesc = value; }
        }

        public short NumDayUse
        {
            get { return _numDayUse; }
            set { _numDayUse = value; }
        }

        public short NumMonthUse
        {
            get { return _numMonthUse; }
            set { _numMonthUse = value; }
        }

        public byte ConcurrentLogin
        {
            get { return _concurrentLogin; }
            set { _concurrentLogin = value; }
        }

        //-----------------------------------------------------------------
        ~CustomerServicesView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //----------------------------------------------------------------
    }
}
