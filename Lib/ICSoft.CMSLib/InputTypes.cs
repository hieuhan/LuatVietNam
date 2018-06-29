
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using sms.database;
using sms.utils;

namespace ICSoft.CMSLib
{
    public class InputTypes
    {
        private byte _InputTypeId;
        private string _InputTypeName;
        private string _InputTypeDesc;
        private byte _DisplayOrder;
        private DBAccess db;
        //-----------------------------------------------------------------
        public InputTypes()
        {
            db = new DBAccess(CmsConstants.CMS_CONSTR);
        }
        //-----------------------------------------------------------------        
        public InputTypes(string constr)
        {
            db = new DBAccess((constr == "") ? CmsConstants.CMS_CONSTR : constr);
        }
        //-----------------------------------------------------------------
        ~InputTypes()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public byte InputTypeId
        {
            get { return _InputTypeId; }
            set { _InputTypeId = value; }
        }
        //-----------------------------------------------------------------
        public string InputTypeName
        {
            get { return _InputTypeName; }
            set { _InputTypeName = value; }
        }
        //-----------------------------------------------------------------
        public string InputTypeDesc
        {
            get { return _InputTypeDesc; }
            set { _InputTypeDesc = value; }
        }
        //-----------------------------------------------------------------
        public byte DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
        //-----------------------------------------------------------------

        private List<InputTypes> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<InputTypes> l_InputTypes = new List<InputTypes>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    InputTypes m_InputTypes = new InputTypes(db.ConnectionString);
                    m_InputTypes.InputTypeId = smartReader.GetByte("InputTypeId");
                    m_InputTypes.InputTypeName = smartReader.GetString("InputTypeName");
                    m_InputTypes.InputTypeDesc = smartReader.GetString("InputTypeDesc");
                    m_InputTypes.DisplayOrder = smartReader.GetByte("DisplayOrder");

                    l_InputTypes.Add(m_InputTypes);
                }
                reader.Close();
                return l_InputTypes;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
        }
        //-----------------------------------------------------------
        public List<InputTypes> GetList()
        {
            List<InputTypes> RetVal = new List<InputTypes>();
            try
            {
                string sql = "SELECT * FROM InputTypes ORDER BY DisplayOrder, InputTypeName";
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public List<InputTypes> GetList(string TextOptionAll = "...")
        {
            List<InputTypes> RetVal = GetList();
            TextOptionAll = (TextOptionAll == null) ? "" : TextOptionAll.Trim();
            if (TextOptionAll.Length > 0)
            {
                InputTypes m_InputTypes = new InputTypes();
                m_InputTypes.InputTypeName = TextOptionAll;
                m_InputTypes.InputTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_InputTypes);
            }
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<InputTypes> GetListOrderBy(string OrderBy)
        {
            List<InputTypes> RetVal = new List<InputTypes>();
            try
            {
                string sql = "SELECT * FROM InputTypes ORDER BY " + StringUtil.InjectionString(OrderBy);
                SqlCommand cmd = new SqlCommand(sql);
                RetVal = Init(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------    
        public List<InputTypes> GetListOrderBy(string OrderBy, string TextOptionAll = "...")
        {
            List<InputTypes> RetVal = GetListOrderBy(OrderBy);
            if (TextOptionAll == null) TextOptionAll = "";
            if (TextOptionAll.Length > 0)
            {
                InputTypes m_InputTypes = new InputTypes();
                m_InputTypes.InputTypeName = TextOptionAll;
                m_InputTypes.InputTypeDesc = TextOptionAll;
                RetVal.Insert(0, m_InputTypes);
            }
            return RetVal;
        }
        //--------------------------------------------------------------  
        public List<InputTypes> GetListByInputTypeId(byte InputTypeId)
        {
            List<InputTypes> RetVal = new List<InputTypes>();
            try
            {
                if (InputTypeId > 0)
                {
                    string sql = "SELECT * FROM InputTypes WHERE (InputTypeId=" + InputTypeId.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(sql);
                    RetVal = Init(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

        //-------------------------------------------------------------- 
        public InputTypes Get(byte InputTypeId)
        {
            InputTypes RetVal = new InputTypes();
            try
            {
                List<InputTypes> list = GetListByInputTypeId(InputTypeId);
                if (list.Count > 0)
                {
                    RetVal = (InputTypes)list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public static InputTypes Static_Get(byte InputTypeId, List<InputTypes> list)
        {
            InputTypes RetVal = new InputTypes();
            try
            {
                RetVal = list.Find(i => i.InputTypeId == InputTypeId);
                if (RetVal == null) RetVal = new InputTypes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-------------------------------------------------------------- 
        public InputTypes Get()
        {
            return Get(this.InputTypeId);
        }
        //-------------------------------------------------------------- 
        public static InputTypes Static_Get(string Constr, byte InputTypeId)
        {
            InputTypes m_InputTypes = new InputTypes(Constr);
            return m_InputTypes.Get(InputTypeId);
        }
        //-------------------------------------------------------------- 
        public static InputTypes Static_Get(byte InputTypeId)
        {
            return Static_Get("", InputTypeId);
        }
        //-------------------------------------------------------------- 
        public static List<InputTypes> Static_GetList(string ConStr)
        {
            List<InputTypes> RetVal = new List<InputTypes>();
            try
            {
                InputTypes m_InputTypes = new InputTypes(ConStr);
                RetVal = m_InputTypes.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<InputTypes> Static_GetList()
        {
            return Static_GetList("");
        }
    }
}

