using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSoft.CMSLibEstate;
using System.Data.SqlClient;
using sms.database;
using System.Data;
using ICSoft.LawDocsLib;

namespace ICSoft.CMSViewLib
{
    public class EstatePostsHelpers
    {
        private static DBAccess db;
        //-----------------------------------------------------------------
        public static EstatePostsViewDetail GetById(int EstatePostId, byte GetMedia = 0, byte GetTag = 0, byte GetComment = 0, int RowAmountComment = 10, byte GetOtherPost = 0, string GetOtherBy = "Category", int RowAmountOther = 10, byte GetAllForeignKeyDesc = 0)
        {
            EstatePostsViewDetail RetVal = new EstatePostsViewDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("EstatePosts_View_GetById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EstatePostId", EstatePostId));
                cmd.Parameters.Add(new SqlParameter("@GetMedia", GetMedia));
                cmd.Parameters.Add(new SqlParameter("@GetTag", GetTag));
                cmd.Parameters.Add(new SqlParameter("@GetComment", GetComment));
                cmd.Parameters.Add(new SqlParameter("@RowAmountComment", RowAmountComment));
                cmd.Parameters.Add(new SqlParameter("@GetOtherPost", GetOtherPost));
                cmd.Parameters.Add(new SqlParameter("@GetOtherBy", GetOtherBy));
                cmd.Parameters.Add(new SqlParameter("@RowAmountOther", RowAmountOther));
                cmd.Parameters.Add(new SqlParameter("@GetAllForeignKeyDesc", GetAllForeignKeyDesc));

                db = new DBAccess(EstateConstants.CONN_ESTATE);
                SqlConnection con = db.getConnection();
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                //lay post
                List<EstatePosts> lEstatePosts = EstatePosts.Init(smartReader);
                RetVal.mEstatePosts = lEstatePosts.Count == 0 ? new EstatePosts() : lEstatePosts[0];
                //Lay category
                reader.NextResult();
                RetVal.mCategory = InitViewHelpers.InitCateOne(smartReader, null);
                //Lay tag
                if (GetTag == 1)
                {
                    reader.NextResult();
                    RetVal.lTag = InitViewHelpers.InitTagsView(smartReader);
                }
                //Lay media
                if (GetMedia == 1)
                {
                    reader.NextResult();
                    RetVal.lEstatePostMedias = EstatePostMedias.Init(smartReader);
                }
                //Lay comment
                if (GetComment == 1)
                {
                    reader.NextResult();
                    RetVal.lEstatePostComments = EstatePostComments.Init(smartReader);
                }
                //Lay post khac
                if (GetOtherPost == 1)
                {
                    reader.NextResult();
                    RetVal.lOtherEstatePosts = EstatePosts.InitSomeField(smartReader);
                }
                //Lay ForeignKeyDesc
                if (GetAllForeignKeyDesc == 1)
                {
                    reader.NextResult();
                    RetVal.mEstatePostForeignKeyDesc = EstatePostForeignKeyDesc.Init(smartReader);
                }

                reader.Close();

                //Lay thong tin nguoi post
                RetVal.mProfileAgency = ProfileAgencies.Static_GetByUserId(RetVal.mEstatePosts.CrUserId);
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
        public static EstatePostsViewListByCate GetByCateId(int CategoryId, int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            EstatePostsViewListByCate RetVal = new EstatePostsViewListByCate();
            try
            {
                SqlCommand cmd = new SqlCommand("EstatePosts_View_GetByCateId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CategoryId));
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

                //Lay category
                RetVal.mCategory = InitViewHelpers.InitCateOne(smartReader, null);
                //lay post
                reader.NextResult();
                RetVal.lEstatePosts = EstatePosts.InitSomeField(smartReader);
                
                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
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
        public static EstatePostsViewListByTag GetByTagId(int TagId, int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            EstatePostsViewListByTag RetVal = new EstatePostsViewListByTag();
            try
            {
                SqlCommand cmd = new SqlCommand("EstatePosts_View_GetByTagId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TagId", TagId));
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

                //Lay Tag
                List<TagsView> l_TagsView = InitViewHelpers.InitTagsView(smartReader);
                RetVal.mTag = (l_TagsView.Count == 0) ? new TagsView() : l_TagsView[0];
                //lay post
                reader.NextResult();
                RetVal.lEstatePosts = EstatePosts.InitSomeField(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
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
        public static List<EstatePosts> GetNewest(int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            List<EstatePosts> RetVal = new List<EstatePosts>();
            try
            {
                SqlCommand cmd = new SqlCommand("EstatePosts_View_GetNewest");
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

                //lay post
                RetVal = EstatePosts.InitSomeField(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
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
        public static List<EstatePosts> GetHot(int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            List<EstatePosts> RetVal = new List<EstatePosts>();
            try
            {
                SqlCommand cmd = new SqlCommand("EstatePosts_View_GetHot");
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

                //lay post
                RetVal = EstatePosts.InitSomeField(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
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
        public static EstatePostsViewListByEstateType GetByRealEstateTypeId(byte EstateTypeId, int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            EstatePostsViewListByEstateType RetVal = new EstatePostsViewListByEstateType();
            try
            {
                SqlCommand cmd = new SqlCommand("EstatePosts_View_GetByEstateTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EstateTypeId", EstateTypeId));
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

                //Lay EstateType
                List<RealEstateTypes> lRealEstateTypes = RealEstateTypes.Init(smartReader);
                RetVal.mRealEstateTypes = lRealEstateTypes.Count == 0 ? new RealEstateTypes() : lRealEstateTypes[0];
                //lay post
                reader.NextResult();
                RetVal.lEstatePosts = EstatePosts.InitSomeField(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
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
        public static EstatePostsViewListByCrUser GetByCrUserId(int CrUserId, int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            EstatePostsViewListByCrUser RetVal = new EstatePostsViewListByCrUser();
            try
            {
                SqlCommand cmd = new SqlCommand("EstatePosts_View_GetByCrUserId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CrUserId", CrUserId));
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

                //Lay Customers
                List<Customers> lCustomers = InitViewHelpers.InitCustomers(smartReader);
                RetVal.mCustomers = lCustomers.Count == 0 ? new Customers() : lCustomers[0];
                //lay post
                reader.NextResult();
                RetVal.lEstatePosts = EstatePosts.InitSomeField(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
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
        public static List<EstatePosts> GetListOfLeader(int ActUserId, int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            List<EstatePosts> RetVal = new List<EstatePosts>();
            try
            {
                SqlCommand cmd = new SqlCommand("EstatePosts_View_GetListOfLeader");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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

                //lay post
                RetVal = EstatePosts.InitSomeField(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
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
        public static List<EstatePosts> GetListOfMember(int ActUserId, int RowAmount, int PageIndex, byte GetRowCount, ref int RowCount)
        {
            List<EstatePosts> RetVal = new List<EstatePosts>();
            try
            {
                SqlCommand cmd = new SqlCommand("EstatePosts_View_GetListOfMember");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
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

                //lay post
                RetVal = EstatePosts.InitSomeField(smartReader);

                reader.Close();
                RowCount = Convert.ToInt32(cmd.Parameters["@RowCount"].Value);
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
