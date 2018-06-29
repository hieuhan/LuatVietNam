using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.CMSViewLib
{
    public class DocGroupsView
    {
        private byte _docGroupId;
        private string _docGroupName;
        private string _docGroupDesc;
        private int _soLuong;

        public byte DocGroupId
        {
            get { return _docGroupId; }
            set { _docGroupId = value; }
        }

        public string DocGroupName
        {
            get { return _docGroupName; }
            set { _docGroupName = value; }
        }

        public string DocGroupDesc
        {
            get { return _docGroupDesc; }
            set { _docGroupDesc = value; }
        }

        public int SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; }
        }

        public DocGroupsView()
        {
        }
    }
}
