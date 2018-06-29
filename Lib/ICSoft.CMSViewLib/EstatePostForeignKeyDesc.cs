using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sms.database;

namespace ICSoft.CMSViewLib
{
    public class EstatePostForeignKeyDesc
    {
        public string CustomerName = "";
        public string RealEstateTypeDesc = "";
        public string RealEstateTransactionTypeDesc = "";
        public string CurrencyDesc = "";
        public string UnitTypeDesc = "";
        public string GeoDirectionDesc = "";
        public string ProvinceDesc = "";
        public string DistrictDesc = "";
        public string WardDesc = "";
        public string LegalDocumentTypeDesc = "";
        public string LegalSupportTypeDesc = "";
        public string ShapeGroundTypeDesc = "";
        public string CompleteOutsideTypeDesc = "";
        public string WaterSourceDesc = "";
        public string SuitableForDesc = "";
        public string HandoverTypeDesc = "";
        public string TienichCongcong = "";
        public string CongdongXungquanh = "";
        //-----------------------------------------------------------------
        public static EstatePostForeignKeyDesc Init(SmartDataReader smartReader)
        {
            EstatePostForeignKeyDesc m_EstatePostForeignKeyDesc = new EstatePostForeignKeyDesc();
            try
            {
                while (smartReader.Read())
                {
                    m_EstatePostForeignKeyDesc.CustomerName = smartReader.GetString("CustomerName");
                    m_EstatePostForeignKeyDesc.RealEstateTypeDesc = smartReader.GetString("RealEstateTypeDesc");
                    m_EstatePostForeignKeyDesc.RealEstateTransactionTypeDesc = smartReader.GetString("RealEstateTransactionTypeDesc");
                    m_EstatePostForeignKeyDesc.CurrencyDesc = smartReader.GetString("CurrencyDesc");
                    m_EstatePostForeignKeyDesc.UnitTypeDesc = smartReader.GetString("UnitTypeDesc");
                    m_EstatePostForeignKeyDesc.GeoDirectionDesc = smartReader.GetString("GeoDirectionDesc");
                    m_EstatePostForeignKeyDesc.ProvinceDesc = smartReader.GetString("ProvinceDesc");
                    m_EstatePostForeignKeyDesc.DistrictDesc = smartReader.GetString("DistrictDesc");
                    m_EstatePostForeignKeyDesc.WardDesc = smartReader.GetString("WardDesc");
                    m_EstatePostForeignKeyDesc.LegalDocumentTypeDesc = smartReader.GetString("LegalDocumentTypeDesc");
                    m_EstatePostForeignKeyDesc.LegalSupportTypeDesc = smartReader.GetString("LegalSupportTypeDesc");
                    m_EstatePostForeignKeyDesc.ShapeGroundTypeDesc = smartReader.GetString("ShapeGroundTypeDesc");
                    m_EstatePostForeignKeyDesc.CompleteOutsideTypeDesc = smartReader.GetString("CompleteOutsideTypeDesc");
                    m_EstatePostForeignKeyDesc.WaterSourceDesc = smartReader.GetString("WaterSourceDesc");
                    m_EstatePostForeignKeyDesc.SuitableForDesc = smartReader.GetString("SuitableForDesc");
                    m_EstatePostForeignKeyDesc.HandoverTypeDesc = smartReader.GetString("HandoverTypeDesc");
                    m_EstatePostForeignKeyDesc.TienichCongcong = smartReader.GetString("TienichCongcong");
                    m_EstatePostForeignKeyDesc.CongdongXungquanh = smartReader.GetString("CongdongXungquanh");
                }
                return m_EstatePostForeignKeyDesc;
            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
    }
}
