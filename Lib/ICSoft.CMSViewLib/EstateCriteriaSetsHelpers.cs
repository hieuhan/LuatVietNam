using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sms.database;
using ICSoft.CMSLibEstate;
using System.Data.SqlClient;
using System.Data;

namespace ICSoft.CMSViewLib
{
    public class EstateCriteriaSetsHelpers
    {
        private static DBAccess db;
        //-----------------------------------------------------------------
        public static EstateCriteriaSets GetData(byte RealEstateTypeId, byte OwnerStatusId, byte PeopleInId)
        {
            EstateCriteriaSets RetVal = new EstateCriteriaSets();
            try
            {
                SqlCommand cmd = new SqlCommand("EstateCriteriaSets_View_GetData");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RealEstateTypeId", RealEstateTypeId));
                cmd.Parameters.Add(new SqlParameter("@OwnerStatusId", OwnerStatusId));
                cmd.Parameters.Add(new SqlParameter("@PeopleInId", PeopleInId));

                db = new DBAccess(EstateConstants.CONN_ESTATE);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //lay EstateCriteriaSets
                List<EstateCriteriaSets> l_EstateCriteriaSets = EstateCriteriaSets.Init(smartReader);
                if (l_EstateCriteriaSets.Count > 0) RetVal = l_EstateCriteriaSets[0];

                //lay EstateCriterias
                reader.NextResult();
                RetVal.lEstateCriterias = EstateCriterias.Init(smartReader);

                //lay EstateCriteriaAnswers
                reader.NextResult();
                List<EstateCriteriaAnswers> l_EstateCriteriaAnswers = EstateCriteriaAnswers.Init(smartReader);
                for (int i = 0; i < RetVal.lEstateCriterias.Count; i++)
                {
                    RetVal.lEstateCriterias[i].lEstateCriteriaAnswers = EstateCriteriaAnswers.Static_GetList(RetVal.lEstateCriterias[i].EstateCriteriaId, l_EstateCriteriaAnswers);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public static EstateCriteriaSets GetByEstateCriteriaSetId(short EstateCriteriaSetId)
        {
            EstateCriteriaSets RetVal = new EstateCriteriaSets();
            try
            {
                EstateCriteriaSets m_EstateCriteriaSets = new EstateCriteriaSets();
                RetVal = m_EstateCriteriaSets.Get(EstateCriteriaSetId);

                EstateCriterias m_EstateCriterias = new EstateCriterias();
                //lay EstateCriterias
                RetVal.lEstateCriterias = m_EstateCriterias.GetList(EstateCriteriaSetId);

                //lay EstateCriteriaAnswers
                EstateCriteriaAnswers m_EstateCriteriaAnswers = new EstateCriteriaAnswers();
                List<EstateCriteriaAnswers> l_EstateCriteriaAnswers = m_EstateCriteriaAnswers.GetListByEstateCriteriaSetId(EstateCriteriaSetId);
                for (int i = 0; i < RetVal.lEstateCriterias.Count; i++)
                {
                    RetVal.lEstateCriterias[i].lEstateCriteriaAnswers = EstateCriteriaAnswers.Static_GetList(RetVal.lEstateCriterias[i].EstateCriteriaId, l_EstateCriteriaAnswers);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------------
        public static List<EstateCriterias> GetReportDetail(int EstateCriteriaSetReviewId, short EstateCriteriaSetId, byte EstateCriteriaTypeId)
        {
            List<EstateCriterias> RetVal = new List<EstateCriterias>();
            try
            {
                SqlCommand cmd = new SqlCommand("EstateCriteriaReviews_GetReportByEstateCriteriaTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EstateCriteriaSetReviewId", EstateCriteriaSetReviewId));
                cmd.Parameters.Add(new SqlParameter("@EstateCriteriaTypeId", EstateCriteriaTypeId));

                db = new DBAccess(EstateConstants.CONN_ESTATE);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                RetVal = EstateCriterias.Init(smartReader);

                reader.Close();

                //lay EstateCriteriaAnswers
                EstateCriteriaAnswers m_EstateCriteriaAnswers = new EstateCriteriaAnswers();
                List<EstateCriteriaAnswers> l_EstateCriteriaAnswers = m_EstateCriteriaAnswers.GetListByEstateCriteriaSetId(EstateCriteriaSetId);
                for (int i = 0; i < RetVal.Count; i++)
                {
                    RetVal[i].lEstateCriteriaAnswers = EstateCriteriaAnswers.Static_GetList(RetVal[i].EstateCriteriaId, l_EstateCriteriaAnswers);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.closeConnection(db.getConnection());
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<EstateCriteriaSetReviews> GetListByCrUserId(int CrUserId, int RowAmount, int PageIndex,  ref int RowCount)
        {
            string OrderBy = "";
            string DateFrom = "";
            string DateTo = "";
            List<EstateCriteriaSetReviews> RetVal = new List<EstateCriteriaSetReviews>();
            try
            {
                EstateCriteriaSetReviews m_EstateCriteriaSetReviews = new EstateCriteriaSetReviews();
                m_EstateCriteriaSetReviews.CrUserId = CrUserId;
                RetVal = m_EstateCriteriaSetReviews.GetPage(CrUserId, RowAmount, PageIndex, OrderBy, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public static List<EstateCriteriaSetReviews> GetListByEstatePostId(int EstatePostId)
        {
            int RowAmount = 50;
            int PageIndex = 0;
            int RowCount = 0;
            string OrderBy = "";
            string DateFrom = "";
            string DateTo = "";
            int ActUserId = 0;
            List<EstateCriteriaSetReviews> RetVal = new List<EstateCriteriaSetReviews>();
            try
            {
                EstateCriteriaSetReviews m_EstateCriteriaSetReviews = new EstateCriteriaSetReviews();
                m_EstateCriteriaSetReviews.EstatePostId = EstatePostId;
                RetVal = m_EstateCriteriaSetReviews.GetPage(ActUserId, RowAmount, PageIndex, OrderBy, DateFrom, DateTo, ref RowCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
    }
}
