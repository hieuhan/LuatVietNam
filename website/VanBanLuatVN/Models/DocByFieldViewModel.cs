using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSoft.ViewLibV3;
using VanBanLuatVN.Models;

namespace VanBanLuat.Models
{
    public class DocByFieldViewModel : ViewModelBase
    {
        public DocList DocList { get; set; }
        public Fields Field { get; set; }
        public string EffectStatusType { get; set; }
        public string EffectStatusTypeText { get; set; }
        public short FieldId { get; set; }
        public byte EffectStatusId { get; set; }
        public short OrganId { get; set; }
        public byte DocTypeId { get; set; }
    }
}