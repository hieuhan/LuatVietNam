using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSoft.CMSLibEstate;
using System.Data.SqlClient;
using System.Data;
using sms.database;

namespace ICSoft.CMSViewLib
{
    public class ProfileAgenciesHelpers
    {
        private static DBAccess db;
        //-----------------------------------------------------------------
        public static List<ProfileAgencies> GetNewest(int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            List<ProfileAgencies> RetVal = new List<ProfileAgencies>();
            try
            {
                SqlCommand cmd = new SqlCommand("ProfileAgencies_View_GetNewest");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                db = new DBAccess(EstateConstants.CONN_ESTATE);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //lay ProfileAgencies
                RetVal = ProfileAgencies.Init(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);

                string m_ListUserId = ProfileAgencies.Static_GetListUserId(RetVal);
                //Lay ProfileAgencyRealEstateTypes
                List<ProfileAgencyRealEstateTypes> l_ProfileAgencyRealEstateTypes = ProfileAgencyRealEstateTypes.Static_GetListByListUserId(m_ListUserId);

                //List<ProfileAgencyScopes>
                List<ProfileAgencyScopes> l_ProfileAgencyScopes = ProfileAgencyScopes.Static_GetListByListUserId(m_ListUserId);

                for (int i = 0; i < RetVal.Count; i++)
                {
                    RetVal[i].lProfileAgencyRealEstateTypes = ProfileAgencyRealEstateTypes.Static_GetListByUserId(RetVal[i].UserId, l_ProfileAgencyRealEstateTypes);
                    RetVal[i].lProfileAgencyScopes = ProfileAgencyScopes.Static_GetListByUserId(RetVal[i].UserId, l_ProfileAgencyScopes);
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
        //-----------------------------------------------------------------
        public static List<ProfileAgencies> GetHot(int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            List<ProfileAgencies> RetVal = new List<ProfileAgencies>();
            try
            {
                SqlCommand cmd = new SqlCommand("ProfileAgencies_View_GetHot");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RowAmount", RowAmount));
                cmd.Parameters.Add(new SqlParameter("@PageIndex", PageIndex));
                cmd.Parameters.Add(new SqlParameter("@GetRowCount", GetRowCount));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                db = new DBAccess(EstateConstants.CONN_ESTATE);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);

                //lay ProfileAgencies
                RetVal = ProfileAgencies.Init(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);

                string m_ListUserId = ProfileAgencies.Static_GetListUserId(RetVal);
                //Lay ProfileAgencyRealEstateTypes
                List<ProfileAgencyRealEstateTypes> l_ProfileAgencyRealEstateTypes = ProfileAgencyRealEstateTypes.Static_GetListByListUserId(m_ListUserId);

                //List<ProfileAgencyScopes>
                List<ProfileAgencyScopes> l_ProfileAgencyScopes = ProfileAgencyScopes.Static_GetListByListUserId(m_ListUserId);

                for (int i = 0; i < RetVal.Count; i++)
                {
                    RetVal[i].lProfileAgencyRealEstateTypes = ProfileAgencyRealEstateTypes.Static_GetListByUserId(RetVal[i].UserId, l_ProfileAgencyRealEstateTypes);
                    RetVal[i].lProfileAgencyScopes = ProfileAgencyScopes.Static_GetListByUserId(RetVal[i].UserId, l_ProfileAgencyScopes);
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
    }
}
