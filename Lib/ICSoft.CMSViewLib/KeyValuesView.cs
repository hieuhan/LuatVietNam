
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;


namespace ICSoft.CMSViewLib
{
    public class KeyValuesView
    {
        private int _ItemId;
        private string _ItemIdString;
        private string _ItemName;
        private string _ItemDesc;

        //-----------------------------------------------------------------
        public KeyValuesView()
        {
        }
        //-----------------------------------------------------------------
        ~KeyValuesView()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public int ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }
        //-----------------------------------------------------------------    
        public string ItemIdString
        {
            get { return _ItemIdString; }
            set { _ItemIdString = value; }
        }
        //-----------------------------------------------------------------    
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
        //-----------------------------------------------------------------    
        public string ItemDesc
        {
            get { return _ItemDesc; }
            set { _ItemDesc = value; }
        }
        //-----------------------------------------------------------------
        public static List<KeyValuesView> Init(SmartDataReader smartReader)
        {
            List<KeyValuesView> l_Data = new List<KeyValuesView>();
            try
            {
                while (smartReader.Read())
                {
                    KeyValuesView m_Data = new KeyValuesView();
                    m_Data.ItemId = smartReader.GetInt32("ItemId");
                    m_Data.ItemIdString = m_Data.ItemId.ToString();
                    m_Data.ItemName = smartReader.GetString("ItemName");
                    m_Data.ItemDesc = smartReader.GetString("ItemDesc");
                    l_Data.Add(m_Data);
                }
                return l_Data;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data error: " + ex.Message);
            }
        }
        //-----------------------------------------------------------------    
        public static KeyValuesView Static_Get(int ItemId, List<KeyValuesView> list)
        {
            KeyValuesView RetVal = new KeyValuesView();
            try
            {
                RetVal = list.Find(i => i.ItemId == ItemId);
                if (RetVal == null) RetVal = new KeyValuesView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------    
        public static KeyValuesView Static_Get(string ItemId, List<KeyValuesView> list)
        {
            KeyValuesView RetVal = new KeyValuesView();
            try
            {
                RetVal = list.Find(i => i.ItemIdString == ItemId);
                if (RetVal == null) RetVal = new KeyValuesView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}