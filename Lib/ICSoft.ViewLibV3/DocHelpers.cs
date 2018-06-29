using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICSoft.ViewLibV3
{
    public class DocHelpers
    {
        //-----------------------------------------------------------------
        public static List<Docs> Init(SmartDataReader smartReader, string FieldSelect = "*")
        {
            List<Docs> l_Docs = new List<Docs>();
            Docs m_Docs;
            while (smartReader.Read())
            {
                m_Docs = new Docs();
                if (FieldSelect == "*" || FieldSelect.Contains("LanguageId")) m_Docs.LanguageId = smartReader.GetByte("LanguageId");
                if (FieldSelect == "*" || FieldSelect.Contains("DocId")) m_Docs.DocId = smartReader.GetInt32("DocId");
                if (FieldSelect == "*" || FieldSelect.Contains("DocGUId")) m_Docs.DocGUId = smartReader.GetString("DocGUId");
                if (FieldSelect == "*" || FieldSelect.Contains("DocName")) m_Docs.DocName = smartReader.GetString("DocName");
                if (FieldSelect == "*" || FieldSelect.Contains("DocIdentity")) m_Docs.DocIdentity = smartReader.GetString("DocIdentity");
                if (FieldSelect == "*" || FieldSelect.Contains("DocIdentityClear")) m_Docs.DocIdentityClear = smartReader.GetString("DocIdentityClear");
                if (FieldSelect == "*" || FieldSelect.Contains("DocSummary")) m_Docs.DocSummary = smartReader.GetString("DocSummary");
                if (FieldSelect == "*" || FieldSelect.Contains("DocContent")) m_Docs.DocContent = smartReader.GetString("DocContent");
                if (FieldSelect == "*" || FieldSelect.Contains("DocTypeId")) m_Docs.DocTypeId = smartReader.GetByte("DocTypeId");
                if (FieldSelect == "*" || FieldSelect.Contains("IssueDate")) m_Docs.IssueDate = smartReader.GetDateTime("IssueDate");
                if (FieldSelect == "*" || FieldSelect.Contains("EffectDate")) m_Docs.EffectDate = smartReader.GetDateTime("EffectDate");
                if (FieldSelect == "*" || FieldSelect.Contains("ExpireDate")) m_Docs.ExpireDate = smartReader.GetDateTime("ExpireDate");
                if (FieldSelect == "*" || FieldSelect.Contains("GazetteNumber")) m_Docs.GazetteNumber = smartReader.GetString("GazetteNumber");
                if (FieldSelect == "*" || FieldSelect.Contains("GazetteDate")) m_Docs.GazetteDate = smartReader.GetDateTime("GazetteDate");
                if (FieldSelect == "*" || FieldSelect.Contains("EffectStatusId")) m_Docs.EffectStatusId = smartReader.GetByte("EffectStatusId");
                if (FieldSelect == "*" || FieldSelect.Contains("ReviewStatusId")) m_Docs.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                if (FieldSelect == "*" || FieldSelect.Contains("DocUrl")) m_Docs.DocUrl = smartReader.GetString("DocUrl");
                if (FieldSelect == "*" || FieldSelect.Contains("DocScopes")) m_Docs.DocScopes = smartReader.GetString("DocScopes");
                if (FieldSelect == "*" || FieldSelect.Contains("GrantLevelId")) m_Docs.GrantLevelId = smartReader.GetByte("GrantLevelId");
                if (FieldSelect == "*" || FieldSelect.Contains("PerformMethod")) m_Docs.PerformMethod = smartReader.GetString("PerformMethod");
                if (FieldSelect == "*" || FieldSelect.Contains("NumDossier")) m_Docs.NumDossier = smartReader.GetByte("NumDossier");
                if (FieldSelect == "*" || FieldSelect.Contains("Fee")) m_Docs.Fee = smartReader.GetString("Fee");
                if (FieldSelect == "*" || FieldSelect.Contains("ElementDossier")) m_Docs.ElementDossier = smartReader.GetString("ElementDossier");
                if (FieldSelect == "*" || FieldSelect.Contains("SettlementTime")) m_Docs.SettlementTime = smartReader.GetString("SettlementTime");
                if (FieldSelect == "*" || FieldSelect.Contains("Result")) m_Docs.Result = smartReader.GetString("Result");
                if (FieldSelect == "*" || FieldSelect.Contains("ConfirmSignerDate")) m_Docs.ConfirmSignerDate = smartReader.GetDateTime("ConfirmSignerDate");
                if (FieldSelect == "*" || FieldSelect.Contains("DocGroupId")) m_Docs.DocGroupId = smartReader.GetByte("DocGroupId");
                if (FieldSelect == "*" || FieldSelect.Contains("IssueYear")) m_Docs.IssueYear = smartReader.GetInt16("IssueYear");
                if (FieldSelect == "*" || FieldSelect.Contains("H1Tag")) m_Docs.H1Tag = smartReader.GetString("H1Tag");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaTitle")) m_Docs.MetaTitle = smartReader.GetString("MetaTitle");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaDesc")) m_Docs.MetaDesc = smartReader.GetString("MetaDesc");
                if (FieldSelect == "*" || FieldSelect.Contains("MetaKeyword")) m_Docs.MetaKeyword = smartReader.GetString("MetaKeyword");

                l_Docs.Add(m_Docs);
            }
            return l_Docs;
        }
        //-----------------------------------------------------------------
        public static Docs InitOne(SmartDataReader smartReader, string FieldSelect = "*")
        {
            List<Docs> l_Docs = Init(smartReader, FieldSelect);
            if (l_Docs.Count > 0) return l_Docs[0];
            return null;
        }
        //-----------------------------------------------------------------
        public static Docs GetById(int DocId, List<Docs> list)
        {
            Docs RetVal = list.Find(i => i.DocId == DocId);
            return RetVal == null ? new Docs() : RetVal;
        }
        //-----------------------------------------------------------------
        private static string GetListId(List<Docs> list)
        {
            string RetVal = "";
            for (int j = 0; j < list.Count; j++)
            {
                if (RetVal != "") RetVal = RetVal + ",";
                RetVal = RetVal + list[j].DocId;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        private static string GetListId(List<DocRelates> list)
        {
            string RetVal = "";
            for (int j = 0; j < list.Count; j++)
            {
                if (RetVal != "") RetVal = RetVal + ",";
                RetVal = RetVal + list[j].DocId;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        //Lay thuoc tinh van ban
        public static Docs GetProperty(int DocId, byte GetDocSummary = 0, byte LanguageId = 1)
        {
            DocFilterParams mDocFilterParams = new DocFilterParams();
            mDocFilterParams.DocId = DocId;
            mDocFilterParams.LanguageId = LanguageId;
            mDocFilterParams.GetEffectStatusName = 1;
            mDocFilterParams.GetDocTypeName = 1;
            mDocFilterParams.GetOrganName = 1;
            mDocFilterParams.GetSignerName = 1;
            mDocFilterParams.GetFieldName = 1;
            mDocFilterParams.FieldSelect = "DocId,DocName,DocIdentity,DocTypeId,IssueDate,EffectDate,ExpireDate,EffectStatusId,GazetteNumber,GazetteDate";
            if (GetDocSummary == 1) mDocFilterParams.FieldSelect = mDocFilterParams.FieldSelect + ",DocSummary";

            return GetById(mDocFilterParams);
        }
        //-----------------------------------------------------------------
        //Lay noi dung van ban
        public static Docs GetDocContent(int DocId, string FieldSelect = "DocId,DocContent", byte LanguageId = 1)
        {
            DocFilterParams mDocFilterParams = new DocFilterParams();
            mDocFilterParams.DocId = DocId;
            mDocFilterParams.LanguageId = LanguageId;
            mDocFilterParams.FieldSelect = FieldSelect;

            return GetById(mDocFilterParams);
        }
        //-----------------------------------------------------------
        //Lay chi tiet VB
        //mDocFilterParams.FieldSelect dung de lua chon cac truong du lieu muon tra ve
        //mDocFilterParams.GetDocFile=1 de lay danh sach file, sau do dung ham DocFiles.Static_GetByLanguageId de loc file theo ngon ngu
        //mDocFilterParams.GetDocIndex=1 de lay muc luc van ban
        public static Docs GetById(DocFilterParams mDocFilterParams)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<Docs> RetVal = new List<Docs>();
            try
            {
                if (mDocFilterParams.GetEffectStatusName == 1 && !mDocFilterParams.FieldSelect.Contains("EffectStatusId"))
                {
                    mDocFilterParams.FieldSelect = mDocFilterParams.FieldSelect + ",EffectStatusId";
                }
                if (mDocFilterParams.GetDocTypeName == 1 && !mDocFilterParams.FieldSelect.Contains("DocTypeId"))
                {
                    mDocFilterParams.FieldSelect = mDocFilterParams.FieldSelect + ",DocTypeId";
                }
                SqlCommand cmd = new SqlCommand("Docs_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FieldSelect", mDocFilterParams.FieldSelect));
                cmd.Parameters.Add(new SqlParameter("@DocId", mDocFilterParams.DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", mDocFilterParams.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@GetDocFile", mDocFilterParams.GetDocFile));
                cmd.Parameters.Add(new SqlParameter("@GetDocIndex", mDocFilterParams.GetDocIndex));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Init(smartReader, mDocFilterParams.FieldSelect);
                if (mDocFilterParams.GetDocFile == 1)
                {
                    reader.NextResult();
                    if (RetVal.Count > 0) RetVal[0].lDocFiles = DocFiles.Static_Init(smartReader);
                }
                if (mDocFilterParams.GetDocIndex == 1)
                {
                    reader.NextResult();
                    if (RetVal.Count > 0) RetVal[0].lDocIndexes = DocIndexes.Static_Init(smartReader);
                }

                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }

            if (mDocFilterParams.GetFieldName == 1 || mDocFilterParams.GetSignerName == 1 || mDocFilterParams.GetOrganName == 1 || mDocFilterParams.GetDocTypeName == 1 || mDocFilterParams.GetEffectStatusName == 1)
            {
                DocLinkData mDocLinkData = GetDataLinkAPI(mDocFilterParams.DocId.ToString(), mDocFilterParams);
                for (int i = 0; i < RetVal.Count; i++)
                {
                    if (mDocFilterParams.GetOrganName == 1)
                    {
                        List<DocOrgans> lDocOrgans = DocOrgans.Static_GetByDocId(RetVal[i].DocId, mDocLinkData.lDocOrgans);
                        string mOrganName = "";
                        for (int j = 0; j < lDocOrgans.Count; j++)
                        {
                            if (mOrganName != "") mOrganName = mOrganName + ", ";
                            mOrganName = mOrganName + Organs.Static_GetById(lDocOrgans[j].OrganId, mDocLinkData.lOrgans).OrganDesc;
                        }
                        RetVal[i].OrganName = mOrganName;
                    }
                    if (mDocFilterParams.GetSignerName == 1)
                    {
                        List<DocSigners> lDocSigners = DocSigners.Static_GetByDocId(RetVal[i].DocId, mDocLinkData.lDocSigners);
                        string mSignerName = "";
                        for (int j = 0; j < lDocSigners.Count; j++)
                        {
                            if (mSignerName != "") mSignerName = mSignerName + ", ";
                            if(mDocFilterParams.LanguageId == 1) mSignerName = mSignerName + Signers.Static_GetById(lDocSigners[j].SignerId, mDocLinkData.lSigners).SignerName;
                            else mSignerName = mSignerName + Signers.Static_GetById(lDocSigners[j].SignerId, mDocLinkData.lSigners).SignerNameClear;
                        }
                        RetVal[i].SignerName = mSignerName;
                    }
                    if (mDocFilterParams.GetFieldName == 1)
                    {
                        List<DocFields> lDocFields = DocFields.Static_GetByDocId(RetVal[i].DocId, mDocLinkData.lDocFields);
                        string mFieldName = "";
                        for (int j = 0; j < lDocFields.Count; j++)
                        {
                            string mField = Fields.Static_GetById(lDocFields[j].FieldId, mDocLinkData.lFields).FieldDesc;
                            if (mFieldName != "") mFieldName = mFieldName + ", ";
                            mFieldName = mFieldName + mField;
                            lDocFields[j].FieldDesc = mField;
                        }
                        RetVal[i].FieldName = mFieldName;
                        RetVal[i].lDocFields = lDocFields;
                    }
                    if (mDocFilterParams.GetEffectStatusName == 1)
                    {
                        if(mDocFilterParams.LanguageId == 1) RetVal[i].EffectStatusName = EffectStatus.Static_GetById(RetVal[i].EffectStatusId, mDocLinkData.lEffectStatus).EffectStatusDesc;
                        else RetVal[i].EffectStatusName = EffectStatus.Static_GetById(RetVal[i].EffectStatusId, mDocLinkData.lEffectStatus).EffectStatusName;
                    }
                    if (mDocFilterParams.GetDocTypeName == 1)
                    {
                        RetVal[i].DocTypeName = DocTypes.Static_GetById(RetVal[i].DocTypeId, mDocLinkData.lDocTypes).DocTypeDesc;
                    }
                }
            }

            return RetVal.Count > 0 ? RetVal[0] : new Docs();
        }
        //-----------------------------------------------------------
        public static DocList GetByDisplayTypeId(short DisplayTypeId, string FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId", int RowAmount = 5, int PageIndex = 0, byte LanguageId = 1, byte GetRowCount = 0)
        {
            DocFilterParams mDocFilterParams = new DocFilterParams();
            mDocFilterParams.FieldSelect = FieldSelect;
            if (FieldSelect.Contains("EffectStatusId")) mDocFilterParams.GetEffectStatusName = 1;
            mDocFilterParams.DisplayTypeId = DisplayTypeId;
            mDocFilterParams.RowAmount = RowAmount;
            mDocFilterParams.PageIndex = PageIndex;
            mDocFilterParams.LanguageId = LanguageId;
            mDocFilterParams.GetRowCount = GetRowCount;
            return GetPage(mDocFilterParams);
        }
        //-----------------------------------------------------------
        //Danh sach van ban theo nhom
        public static DocList GetByDocGroupId(byte DocGroupId, string FieldSelect = "DocId,DocName,DocUrl,IssueDate,EffectDate,EffectStatusId", string OrderBy = "IssueDate DESC", int RowAmount = 5, int PageIndex = 0, byte LanguageId = 1, byte GetRowCount = 0)
        {
            DocFilterParams mDocFilterParams = new DocFilterParams();
            mDocFilterParams.FieldSelect = FieldSelect;
            mDocFilterParams.OrderBy = OrderBy;
            if (FieldSelect.Contains("EffectStatusId")) mDocFilterParams.GetEffectStatusName = 1;
            mDocFilterParams.DocGroupId = DocGroupId;
            mDocFilterParams.RowAmount = RowAmount;
            mDocFilterParams.PageIndex = PageIndex;
            mDocFilterParams.LanguageId = LanguageId;
            mDocFilterParams.GetRowCount = GetRowCount;
            return GetPage(mDocFilterParams);
        }
        //-----------------------------------------------------------
        //Lay danh sach VB theo cac dieu kien tim kiem duoc set trong mDocFilterParams
        public static DocList GetPage(DocFilterParams mDocFilterParams)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            DocList RetVal = new DocList();
            try
            {
                if (mDocFilterParams.GetEffectStatusName == 1 && !mDocFilterParams.FieldSelect.Contains("EffectStatusId"))
                {
                    mDocFilterParams.FieldSelect = mDocFilterParams.FieldSelect + ",EffectStatusId";
                }
                if (mDocFilterParams.GetDocTypeName == 1 && !mDocFilterParams.FieldSelect.Contains("DocTypeId"))
                {
                    mDocFilterParams.FieldSelect = mDocFilterParams.FieldSelect + ",DocTypeId";
                }
                SqlCommand cmd = new SqlCommand("Docs_GetDataAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FieldSelect", mDocFilterParams.FieldSelect));
                cmd.Parameters.Add(new SqlParameter("@DocId", mDocFilterParams.DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", mDocFilterParams.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocName", mDocFilterParams.DocName));
                cmd.Parameters.Add(new SqlParameter("@DocIdentity", mDocFilterParams.DocIdentity));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusId", mDocFilterParams.EffectStatusId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", mDocFilterParams.DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", mDocFilterParams.DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", mDocFilterParams.RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", mDocFilterParams.PageIndex));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", mDocFilterParams.OrderBy));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", mDocFilterParams.SearchKeyword));
                cmd.Parameters.Add(new SqlParameter("@IsSearchExact", mDocFilterParams.IsSearchExact));
                cmd.Parameters.Add(new SqlParameter("@HighlightKeyword", mDocFilterParams.HighlightKeyword));
                cmd.Parameters.Add(new SqlParameter("@FieldId", mDocFilterParams.FieldId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", mDocFilterParams.OrganId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", mDocFilterParams.SignerId));
                cmd.Parameters.Add(new SqlParameter("@DisplayTypeId", mDocFilterParams.DisplayTypeId));
                cmd.Parameters.Add(new SqlParameter("@ProvinceId", mDocFilterParams.ProvinceId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", mDocFilterParams.CustomerId));
                cmd.Parameters.Add(new SqlParameter("@RegistTypeId", mDocFilterParams.RegistTypeId));
                cmd.Parameters.Add(new SqlParameter("@EffectStatusType", mDocFilterParams.EffectStatusType));
                cmd.Parameters.Add(new SqlParameter("@SearchByDate", mDocFilterParams.SearchByDate));
                if (mDocFilterParams.DateFrom != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@DateFrom", mDocFilterParams.DateFrom));
                if (mDocFilterParams.DateTo != DateTime.MinValue) cmd.Parameters.Add(new SqlParameter("@DateTo", mDocFilterParams.DateTo));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", mDocFilterParams.GetRowCount));
                cmd.Parameters.Add(new SqlParameter("@GetCountByField", mDocFilterParams.GetCountByField));
                cmd.Parameters.Add(new SqlParameter("@GetCountByDocType", mDocFilterParams.GetCountByDocType));
                cmd.Parameters.Add(new SqlParameter("@GetCountByOrgan", mDocFilterParams.GetCountByOrgan));
                cmd.Parameters.Add(new SqlParameter("@GetCountByProvince", mDocFilterParams.GetCountByProvince));
                cmd.Parameters.Add(new SqlParameter("@GetCountByEffectStatus", mDocFilterParams.GetCountByEffectStatus));
                cmd.Parameters.Add(new SqlParameter("@GetCountByYear", mDocFilterParams.GetCountByYear));
                cmd.Parameters.Add(new SqlParameter("@GetCountByGroup", mDocFilterParams.GetCountByGroup));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal.lDocs = Init(smartReader, mDocFilterParams.FieldSelect);

                if (mDocFilterParams.GetCountByField == 1)
                {
                    reader.NextResult();
                    RetVal.lCountByField = Fields.Static_Init(smartReader, 1);
                }
                if (mDocFilterParams.GetCountByDocType == 1)
                {
                    reader.NextResult();
                    RetVal.lCountByDocType = DocTypes.Static_Init(smartReader, 1);
                }
                if (mDocFilterParams.GetCountByOrgan == 1)
                {
                    reader.NextResult();
                    RetVal.lCountByOrgan = Organs.Static_Init(smartReader, 1);
                }
                if (mDocFilterParams.GetCountByProvince == 1)
                {
                    reader.NextResult();
                    RetVal.lCountByProvince = Provinces.Static_Init(smartReader, 1);
                }
                if (mDocFilterParams.GetCountByEffectStatus == 1)
                {
                    reader.NextResult();
                    RetVal.lCountByEffectStatus = EffectStatus.Static_Init(smartReader, 1);
                }
                if (mDocFilterParams.GetCountByYear == 1)
                {
                    reader.NextResult();
                    RetVal.lCountByYear = Years.Static_Init(smartReader);
                }
                if (mDocFilterParams.GetCountByGroup == 1)
                {
                    reader.NextResult();
                    RetVal.lCountByDocGroup = DocGroups.Static_Init(smartReader, 1);
                }

                reader.Close();
                var value = cmd.Parameters["@RowCount"].Value;
                RetVal.RowCount = Convert.ToInt32(value == DBNull.Value || value == null ? "0" : cmd.Parameters["@RowCount"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }

            if (mDocFilterParams.GetFieldName == 1 || mDocFilterParams.GetSignerName == 1 || mDocFilterParams.GetOrganName == 1 || mDocFilterParams.GetDocTypeName == 1 || mDocFilterParams.GetEffectStatusName == 1)
            {
                DocLinkData mDocLinkData = GetDataLinkAPI(GetListId(RetVal.lDocs), mDocFilterParams);
                for (int i = 0; i < RetVal.lDocs.Count; i++)
                {
                    if (mDocFilterParams.GetOrganName == 1)
                    {
                        List<DocOrgans> lDocOrgans = DocOrgans.Static_GetByDocId(RetVal.lDocs[i].DocId, mDocLinkData.lDocOrgans);
                        string mOrganName = "";
                        for (int j = 0; j < lDocOrgans.Count; j++)
                        {
                            if (mOrganName != "") mOrganName = mOrganName + ", ";
                            mOrganName = mOrganName + Organs.Static_GetById(lDocOrgans[j].OrganId, mDocLinkData.lOrgans).OrganDesc;
                        }
                        RetVal.lDocs[i].OrganName = mOrganName;
                    }
                    if (mDocFilterParams.GetSignerName == 1)
                    {
                        List<DocSigners> lDocSigners = DocSigners.Static_GetByDocId(RetVal.lDocs[i].DocId, mDocLinkData.lDocSigners);
                        string mSignerName = "";
                        for (int j = 0; j < lDocSigners.Count; j++)
                        {
                            if (mSignerName != "") mSignerName = mSignerName + ", ";
                            if (mDocFilterParams.LanguageId == 1) mSignerName = mSignerName + Signers.Static_GetById(lDocSigners[j].SignerId, mDocLinkData.lSigners).SignerName;
                            else mSignerName = mSignerName + Signers.Static_GetById(lDocSigners[j].SignerId, mDocLinkData.lSigners).SignerNameClear;
                        }
                        RetVal.lDocs[i].SignerName = mSignerName;
                    }
                    if (mDocFilterParams.GetFieldName == 1)
                    {
                        List<DocFields> lDocFields = DocFields.Static_GetByDocId(RetVal.lDocs[i].DocId, mDocLinkData.lDocFields);
                        string mFieldName = "";
                        for (int j = 0; j < lDocFields.Count; j++)
                        {
                            string mField = Fields.Static_GetById(lDocFields[j].FieldId, mDocLinkData.lFields).FieldDesc;
                            if (mFieldName != "") mFieldName = mFieldName + ", ";
                            mFieldName = mFieldName + mField;
                            lDocFields[j].FieldDesc = mField;
                        }
                        RetVal.lDocs[i].FieldName = mFieldName;
                        RetVal.lDocs[i].lDocFields = lDocFields;
                    }
                    if (mDocFilterParams.GetEffectStatusName == 1)
                    {
                        if (mDocFilterParams.LanguageId == 1) RetVal.lDocs[i].EffectStatusName = EffectStatus.Static_GetById(RetVal.lDocs[i].EffectStatusId, mDocLinkData.lEffectStatus).EffectStatusDesc;
                        else RetVal.lDocs[i].EffectStatusName = EffectStatus.Static_GetById(RetVal.lDocs[i].EffectStatusId, mDocLinkData.lEffectStatus).EffectStatusName;
                    }
                    if (mDocFilterParams.GetDocTypeName == 1)
                    {
                        RetVal.lDocs[i].DocTypeName = DocTypes.Static_GetById(RetVal.lDocs[i].DocTypeId, mDocLinkData.lDocTypes).DocTypeDesc;
                    }
                }
            }

            return RetVal;
        }
        //-----------------------------------------------------------
        //Danh sach van ban xem nhieu
        public static List<Docs> GetMostView(byte DocGroupId = 1, short FieldId = 0, byte LanguageId = 1, int RowAmount = 10)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            List<Docs> RetVal = new List<Docs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetViewMostView");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal = Init(smartReader, "DocId,DocName,DocUrl,DocGroupId,IssueDate,EffectDate,EffectStatusId,H1Tag");

                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        private static DocLinkData GetDataLinkAPI(string ListDocId, DocFilterParams mDocFilterParams)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            DocLinkData RetVal = new DocLinkData();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetDataLinkAPI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", mDocFilterParams.LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ListDocId", ListDocId));
                cmd.Parameters.Add(new SqlParameter("@GetOrganName", mDocFilterParams.GetOrganName));
                cmd.Parameters.Add(new SqlParameter("@GetSignerName", mDocFilterParams.GetSignerName));
                cmd.Parameters.Add(new SqlParameter("@GetFieldName", mDocFilterParams.GetFieldName));
                cmd.Parameters.Add(new SqlParameter("@GetDocTypeName", mDocFilterParams.GetDocTypeName));
                cmd.Parameters.Add(new SqlParameter("@GetEffectStatusName", mDocFilterParams.GetEffectStatusName));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                bool ReadNext = false;
                if (mDocFilterParams.GetOrganName == 1)
                {
                    if (ReadNext) reader.NextResult();
                    RetVal.lDocOrgans = DocOrgans.Static_Init(smartReader);
                    reader.NextResult();
                    RetVal.lOrgans = Organs.Static_Init(smartReader);
                    ReadNext = true;
                }
                if (mDocFilterParams.GetSignerName == 1)
                {
                    if (ReadNext) reader.NextResult();
                    RetVal.lDocSigners = DocSigners.Static_Init(smartReader);
                    reader.NextResult();
                    RetVal.lSigners = Signers.Static_Init(smartReader);
                    ReadNext = true;
                }
                if (mDocFilterParams.GetFieldName == 1)
                {
                    if (ReadNext) reader.NextResult();
                    RetVal.lDocFields = DocFields.Static_Init(smartReader);
                    reader.NextResult();
                    RetVal.lFields = Fields.Static_Init(smartReader);
                    ReadNext = true;
                }
                if (mDocFilterParams.GetDocTypeName == 1)
                {
                    if (ReadNext) reader.NextResult();
                    RetVal.lDocTypes = DocTypes.Static_Init(smartReader);
                    ReadNext = true;
                }
                if (mDocFilterParams.GetEffectStatusName == 1)
                {
                    if (ReadNext) reader.NextResult();
                    RetVal.lEffectStatus = EffectStatus.Static_Init(smartReader);
                    ReadNext = true;
                }

                reader.Close();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        //Tra tat ca danh sach VB lien quan tieng viet, su dung cho ca phan hien thi luoc do
        public static DocRelateList GetAllDocRelatesVN(int DocId,  byte GetEffectStatusName = 0)
        {
            short RelateTypeId = 0;
            string DisplayPosition = "";
            int RowAmount = 1000;
            int PageIndex = 0;
            byte GetCountByRelateType = 1;
            byte LanguageId = 1;
            return GetDocRelates(DocId, RelateTypeId, DisplayPosition, RowAmount, PageIndex, GetCountByRelateType, LanguageId, GetEffectStatusName);
        }
        //-----------------------------------------------------------
        //Tra danh sach VB lien quan, su dung cho ca phan hien thi luoc do
        public static DocRelateList GetDocRelates(int DocId, short RelateTypeId = 0, string DisplayPosition = "", int RowAmount = 20, int PageIndex = 0, byte GetCountByRelateType = 1, byte LanguageId = 1, byte GetEffectStatusName = 0)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            DocRelateList RetVal = new DocRelateList();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetViewRelates");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@RelateTypeId", RelateTypeId));
                cmd.Parameters.Add(new SqlParameter("@DisplayPosition", DisplayPosition));
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@GetCountByRelateType", GetCountByRelateType));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal.lDocRelates = DocRelates.Static_Init(smartReader);
                if (GetCountByRelateType == 1)
                {
                    reader.NextResult();
                    RetVal.lCountByRelateType = RelateTypes.Static_Init(smartReader, 1);
                }
                reader.Close();
                RetVal.RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);

                if (GetEffectStatusName == 1)
                {
                    DocFilterParams mDocFilterParams = new DocFilterParams();
                    mDocFilterParams.GetEffectStatusName = 1;
                    DocLinkData mDocLinkData = GetDataLinkAPI(GetListId(RetVal.lDocRelates), mDocFilterParams);
                    for (int i = 0; i < RetVal.lDocRelates.Count; i++)
                    {
                        if (LanguageId == 1) RetVal.lDocRelates[i].EffectStatusName = EffectStatus.Static_GetById(RetVal.lDocRelates[i].EffectStatusId, mDocLinkData.lEffectStatus).EffectStatusDesc;
                        else RetVal.lDocRelates[i].EffectStatusName = EffectStatus.Static_GetById(RetVal.lDocRelates[i].EffectStatusId, mDocLinkData.lEffectStatus).EffectStatusName;
                    }
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        //Tra danh sach VB lien quan hieu luc
        public static DocRelateList GetDocRelatesEffect(int DocId, byte LanguageId = 1, byte GetEffectStatusName = 0)
        {
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            SqlConnection con = db.getConnection();

            DocRelateList RetVal = new DocRelateList();
            try
            {
                SqlCommand cmd = new SqlCommand("Docs_GetViewRelatesEffect");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));

                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                RetVal.lDocRelates = DocRelates.Static_Init(smartReader);
                
                reader.NextResult();
                RetVal.lCountByRelateType = RelateTypes.Static_Init(smartReader, 1);
                reader.Close();

                if (GetEffectStatusName == 1)
                {
                    DocFilterParams mDocFilterParams = new DocFilterParams();
                    mDocFilterParams.GetEffectStatusName = 1;
                    DocLinkData mDocLinkData = GetDataLinkAPI(GetListId(RetVal.lDocRelates), mDocFilterParams);
                    for (int i = 0; i < RetVal.lDocRelates.Count; i++)
                    {
                        if (LanguageId == 1) RetVal.lDocRelates[i].EffectStatusName = EffectStatus.Static_GetById(RetVal.lDocRelates[i].EffectStatusId, mDocLinkData.lEffectStatus).EffectStatusDesc;
                        else RetVal.lDocRelates[i].EffectStatusName = EffectStatus.Static_GetById(RetVal.lDocRelates[i].EffectStatusId, mDocLinkData.lEffectStatus).EffectStatusName;
                    }
                }
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        //Luu van ban quan tam cua user
        public static byte SaveMyDoc(int CustomerId, int DocId, byte LanguageId = 1)
        {
            byte RegistTypeId = 1;
            byte Replicated = 1;
            byte ActUserId = 0;
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            byte RetVal = 1;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerDocs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                cmd.Parameters.Add("@CustomerDocId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        //Luu van ban nhan thong bao hieu luc
        public static byte SaveMyDocNotifyEffect(int CustomerId, int DocId, byte LanguageId = 1)
        {
            byte RegistTypeId = 2;
            byte Replicated = 1;
            byte ActUserId = 0;
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            byte RetVal = 1;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerDocs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@RegistTypeId", RegistTypeId));
                cmd.Parameters.Add("@CustomerDocId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        //Xoa van ban quan tam cua user
        public static byte DeleteMyDoc(int CustomerId, int DocId, byte LanguageId = 1)
        {
            byte Replicated = 1;
            byte ActUserId = 0;
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            byte RetVal = 1;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerDocs_DeleteBy");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        //Luu log xem VB
        public static void InsertViewLog(int DocId, int CustomerId, int DocFileId, byte DocGroupId, byte LanguageId, byte ActionTypeId, string RefererFrom, string UserAgent, string FromIP)
        {
            byte Replicated = 1;
            byte ActUserId = 0;
            byte ApplicationTypeId = 0;
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            try
            {
                SqlCommand cmd = new SqlCommand("DocViewLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@DocId", DocId));
                cmd.Parameters.Add(new SqlParameter("@DocFileId", DocFileId));
                cmd.Parameters.Add(new SqlParameter("@DocGroupId", DocGroupId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@ApplicationTypeId", ApplicationTypeId));
                cmd.Parameters.Add(new SqlParameter("@ActionTypeId", ActionTypeId));
                cmd.Parameters.Add(new SqlParameter("@RefererFrom", RefererFrom));
                cmd.Parameters.Add(new SqlParameter("@UserAgent", UserAgent));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@FromIP", FromIP));
                cmd.Parameters.Add("@DocViewLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------
        //Luu log search VB
        public static void InsertSearchLog(string SearchKeyword, DateTime DateFrom, DateTime DateTo, byte DocTypeId, short OrganId, int SignerId, short FieldId, byte LanguageId, int CustomerId)
        {
            byte Replicated = 1;
            byte ActUserId = 0;
            DBAccess db = new DBAccess(Constants.DOC_CONSTR);
            try
            {
                SqlCommand cmd = new SqlCommand("DocSearchLogs_Insert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SearchKeyword", SearchKeyword));
                if (DateFrom == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateFrom", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));

                if (DateTo == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@DateTo", DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@DocTypeId", DocTypeId));
                cmd.Parameters.Add(new SqlParameter("@OrganId", OrganId));
                cmd.Parameters.Add(new SqlParameter("@SignerId", SignerId));
                cmd.Parameters.Add(new SqlParameter("@FieldId", FieldId));
                cmd.Parameters.Add(new SqlParameter("@LanguageId", LanguageId));
                cmd.Parameters.Add(new SqlParameter("@CustomerId", CustomerId));
                cmd.Parameters.Add("@DocSearchLogId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
        }
    }
}
