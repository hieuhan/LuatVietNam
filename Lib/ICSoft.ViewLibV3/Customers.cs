using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class Customers
    {
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public DateTime CrDateTime { get; set; }
        public string CustomerFullName { get; set; }
        public short CustomerGroupId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNickName { get; set; }
        public string CustomerPass { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAuto { get; set; }
        public string Facebook { get; set; }
        public byte GenderId { get; set; }
        public string Identifier { get; set; }
        public short InfoChannelId { get; set; }
        public DateTime LastLoginTime { get; set; }
        public byte LockPassword { get; set; }
        public byte MaxConcurrentLogin { get; set; }
        public short NewsletterGroupId { get; set; }
        public short ApplicationId { get; set; }
        public string Note { get; set; }
        public short OccupationId { get; set; }
        public string OrganAddress { get; set; }
        public string OrganFax { get; set; }
        public string OrganName { get; set; }
        public short OrganOccupationId { get; set; }
        public string OrganPhone { get; set; }
        public string OrganTaxCode { get; set; }
        public short ProvinceId { get; set; }
        public byte RegisterNewsletter { get; set; }
        public short SiteId { get; set; }
        public byte StatusId { get; set; }
        public string Website { get; set; }
        //-----------------------------------------------------------------
        public static List<Customers> Static_Init(SmartDataReader smartReader)
        {
            List<Customers> list = new List<Customers>();
            Customers m_Object;
            while (smartReader.Read())
            {
                m_Object = new Customers();
                m_Object.CustomerId = smartReader.GetInt32("CustomerId");
                m_Object.CustomerName = smartReader.GetString("CustomerName");
                m_Object.CustomerPass = smartReader.GetString("CustomerPass");
                m_Object.CustomerFullName = smartReader.GetString("CustomerFullName");
                m_Object.CustomerNickName = smartReader.GetString("CustomerNickName");
                m_Object.CustomerMail = smartReader.GetString("CustomerMail");
                m_Object.Website = smartReader.GetString("Website");
                m_Object.Facebook = smartReader.GetString("Facebook");
                m_Object.Avatar = smartReader.GetString("Avatar");
                m_Object.SiteId = smartReader.GetInt16("SiteId");
                m_Object.StatusId = smartReader.GetByte("StatusId");
                m_Object.CrDateTime = smartReader.GetDateTime("CrDateTime");
                m_Object.CustomerMobile = smartReader.GetString("CustomerMobile");
                m_Object.GenderId = smartReader.GetByte("GenderId");
                m_Object.ProvinceId = smartReader.GetInt16("ProvinceId");
                m_Object.DateOfBirth = smartReader.GetDateTime("DateOfBirth");
                m_Object.Identifier = smartReader.GetString("Identifier");
                m_Object.EmailAuto = smartReader.GetString("EmailAuto");
                m_Object.CustomerGroupId = smartReader.GetInt16("CustomerGroupId");
                m_Object.OccupationId = smartReader.GetInt16("OccupationId");
                m_Object.OrganOccupationId = smartReader.GetInt16("OrganOccupationId");
                m_Object.ApplicationId = smartReader.GetInt16("ApplicationId");
                m_Object.Address = smartReader.GetString("Address");
                m_Object.AccountNumber = smartReader.GetString("AccountNumber");
                m_Object.OrganName = smartReader.GetString("OrganName");
                m_Object.OrganPhone = smartReader.GetString("OrganPhone");
                m_Object.OrganFax = smartReader.GetString("OrganFax");
                m_Object.OrganAddress = smartReader.GetString("OrganAddress");
                m_Object.OrganTaxCode = smartReader.GetString("OrganTaxCode");
                m_Object.Note = smartReader.GetString("Note");
                m_Object.InfoChannelId = smartReader.GetInt16("InfoChannelId");
                m_Object.RegisterNewsletter = smartReader.GetByte("RegisterNewsletter");
                m_Object.MaxConcurrentLogin = smartReader.GetByte("MaxConcurrentLogin");
                m_Object.LockPassword = smartReader.GetByte("LockPassword");

                list.Add(m_Object);
            }
            return list;
        }
        //-----------------------------------------------------------------
        public static Customers Static_InitOne(SmartDataReader smartReader)
        {
            List<Customers> list = Static_Init(smartReader);
            if (list.Count > 0) return list[0];
            return null;
        }
        //------------------------------------------------------------
        public string GetImageUrl()
        {
            string RetVal = Avatar;
            if (string.IsNullOrEmpty(Avatar))
            {
                RetVal = "";
            }
            else
            {
                if (!RetVal.StartsWith("http")) RetVal = Constants.WEBSITE_IMAGEDOMAIN + RetVal;
            }

            return RetVal;
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Icon()
        {
            return GetImageUrl().Replace("/Original/", "/Icon/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Thumb()
        {
            return GetImageUrl().Replace("/Original/", "/Thumb/");
        }
        //--------------------------------------------------------------  
        public string GetImageUrl_Standard()
        {
            return GetImageUrl().Replace("/Original/", "/Standard/");
        }
        //-------------------------------------------------------------- 
        public string GetImageUrl_Mobile()
        {
            return GetImageUrl().Replace("/Original/", "/Mobile/");
        }
    }
}
