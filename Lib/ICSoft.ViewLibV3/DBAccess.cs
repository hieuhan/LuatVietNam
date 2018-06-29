using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace ICSoft.ViewLibV3
{
    public class DBAccess
    {
        private string connectionString;

        public DBAccess()
        {
            connectionString = Constants.CONNECTION_STRING;
        }
        //---------------------------------------------------------------------------------------------------
        public DBAccess(string server,string userName, string userPass,string dbName)
        {
            connectionString = BuildConstr(server, userName, userPass, dbName);            
        }

        //---------------------------------------------------------------------------------------------------
        public DBAccess(string connStr)
        {
            if (connStr.Length > 0)
            {
                connectionString = connStr;
            }
            else
            {
                connectionString = Constants.CONNECTION_STRING;
            }
        }
        //------------------------------------------------------------------------
        public static string BuildConstr (string server, string userName, string userPass, string dbName)
        {
            string retVal = "";
            if ((server.Length > 0) && (userName.Length > 0) && (userPass.Length > 0) && (dbName.Length > 0))
            {
                retVal = "server=" + server + ";uid=" + userName + ";pwd=" + userPass + ";database=" + dbName + ";";
            }
            else
            {
                retVal = Constants.CONNECTION_STRING;
            }
            return retVal;
        }
        //-------------------------------------------------------------------------------------------
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
        
        public  bool checkConnection(string connStr)
        {
            bool retVal = false;
            try
            {
                SqlConnection mySqlConnection = new SqlConnection(connStr);
                mySqlConnection.Open();
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close();
                    retVal = true;
                }
            }
            catch 
            {
                retVal = false;
            }
            return retVal;
        }

        public SqlConnection getConnection()
        {
            try
            {
                return new SqlConnection(connectionString);
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Khong ket noi duoc toi CSDL: " + sqlEx.Message);
            }
        }

        public OleDbConnection getOLEConnection()
        {
            try
            {
                return new OleDbConnection(connectionString);
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Khong ket noi duoc toi CSDL: " + sqlEx.Message);
            }
        }
        public void closeConnection(SqlConnection con)
        {
            try
            {
                if (con != null || con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
        }


        #region Management Methods

        /// <summary>
        /// return an Open SqlConnection
        /// </summary>
        public SqlConnection OpenConnection(string connectionString)
        {
            try
            {
                SqlConnection mySqlConnection = new SqlConnection(connectionString);
                mySqlConnection.Open();
                return mySqlConnection;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        /// <summary>
        /// return an Open SqlConnection
        /// </summary>
        public SqlConnection OpenConnection()
        {
            try
            {
                //connectionString = this.connectionString;
                SqlConnection mySqlConnection = new SqlConnection(connectionString);
                mySqlConnection.Open();
                return mySqlConnection;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        /// <summary>
        /// close an SqlConnection
        /// </summary>
        public void CloseConnection(SqlConnection mySqlConnection)
        {
            try
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close();
                    mySqlConnection.Dispose();
                }
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message));
            }
        }

        /// <summary>
        /// return a DataSet from SQL Request
        /// </summary>
        public DataSet getDataSet(string strSQL)
        {
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                SqlDataAdapter myDataAdapter = new SqlDataAdapter(strSQL, myConn);
                DataSet myDataSet = new DataSet();
                myDataAdapter.Fill(myDataSet);

                myDataAdapter.Dispose();
                myBase.CloseConnection(myConn);
                
                return myDataSet;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + strSQL));
            }
        }

        public SqlDataReader getReader(SqlCommand cmd)
        {
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                cmd.Connection = myConn;
                SqlDataReader reader = cmd.ExecuteReader();
                //myBase.CloseConnection(myConn);
                return reader;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + cmd.CommandText));
            }
        }

        public SqlDataReader getReader(SqlCommand cmd, SqlConnection con)
        {
            try
            {
                cmd.Connection = con;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + cmd.CommandText));
            }
        }

        public DataTable getDataTable(string strSQL)
        {
            return getDataSet(strSQL).Tables[0];
        }

        public DataSet getDataSet(string strSQL, SqlParameter[] Parameters)
        {
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                SqlCommand myCommand = new SqlCommand(strSQL, myConn);
                myCommand.Parameters.Clear();
                foreach (SqlParameter par in Parameters)
                {
                    myCommand.Parameters.Add(par);
                }
                SqlDataAdapter myDataAdapter = new SqlDataAdapter(myCommand);
                DataSet myDataSet = new DataSet();
                myDataAdapter.Fill(myDataSet);
                myBase.CloseConnection(myConn);
                return myDataSet;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + strSQL));
            }
        }

        public DataTable getDataTable(string strSQL, params SqlParameter[] Parameters)
        {
            return getDataSet(strSQL, Parameters).Tables[0];
        }

        public DataSet getDataSet(SqlCommand mCommand)
        {
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                mCommand.Connection = myConn;
                SqlDataAdapter myDataAdapter = new SqlDataAdapter(mCommand);
                DataSet myDataSet = new DataSet();
                myDataAdapter.Fill(myDataSet);
                
                myBase.CloseConnection(myConn);
                return myDataSet;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + mCommand.CommandText));
            }
        }

        public DataTable getDataTable(SqlCommand mCommand)
        {
            return getDataSet(mCommand).Tables[0];
        }

        public void ExecuteSQL(string strSQL)
        {
            DBAccess myBase = new DBAccess();
            SqlConnection myConn = myBase.OpenConnection(connectionString);
            SqlTransaction tran = myConn.BeginTransaction();

            try
            {
                SqlCommand myCommand = new SqlCommand(strSQL, myConn);
                myCommand.Transaction = tran;
                myCommand.ExecuteNonQuery();
                tran.Commit();
                myCommand.Dispose();
            }
            catch (Exception myException)
            {
                tran.Rollback();
                throw (new Exception(myException.Message + " => " + strSQL));
            }
            finally
            {
                myBase.CloseConnection(myConn);
            }
        }

        public void ExecuteSQL(string strSQL, params SqlParameter[] Parameters)
        {
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                SqlCommand myCommand = new SqlCommand(strSQL, myConn);
                myCommand.Parameters.Clear();
                foreach (SqlParameter par in Parameters)
                {
                    myCommand.Parameters.Add(par);
                }
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                myBase.CloseConnection(myConn);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + strSQL));
            }

        }

        public void ExecuteSQL(string strSQL, IList paraList)
        {
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                SqlCommand myCommand = new SqlCommand(strSQL, myConn);
                myCommand.Parameters.Clear();
                foreach (SqlParameter par in paraList)
                {
                    myCommand.Parameters.Add(par);
                }
                myCommand.ExecuteNonQuery();

                myCommand.Dispose();
                myBase.CloseConnection(myConn);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + strSQL));
            }

        }

        public void ExecuteSQL(SqlCommand mCommand)
        {
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                mCommand.Connection = myConn;
                mCommand.ExecuteNonQuery();

                mCommand.Dispose();
                myBase.CloseConnection(myConn);
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + mCommand.CommandText));
            }
        }

        public void ExecuteSQL(string strSQL, SqlConnection mConn)
        {
            try
            {
                SqlCommand myCom = new SqlCommand(strSQL, mConn);
                myCom.ExecuteNonQuery();
                myCom.Dispose();
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + strSQL));
            }
        }


        /*
         * Execute Scalar
         */
        public int ExecuteScalar(string strSQL)
        {
            int temp = 0;
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                SqlCommand myCommand = new SqlCommand(strSQL, myConn);
                temp = Convert.ToInt32(myCommand.ExecuteScalar().ToString());

                myCommand.Dispose();
                myBase.CloseConnection(myConn);

                return temp;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + strSQL));
            }

        }

        public int ExecuteScalar(string strSQL, params SqlParameter[] Parameters)
        {
            int temp = 0;
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                SqlCommand myCommand = new SqlCommand(strSQL, myConn);
                myCommand.Parameters.Clear();
                foreach (SqlParameter par in Parameters)
                {
                    myCommand.Parameters.Add(par);
                }
                
                temp = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
                myCommand.Dispose();
                myBase.CloseConnection(myConn);
                return temp;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + strSQL));
            }

        }

        public int ExecuteScalar(string strSQL, IList paraList)
        {
            int temp = 0;
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                SqlCommand myCommand = new SqlCommand(strSQL, myConn);
                myCommand.Parameters.Clear();
                foreach (SqlParameter par in paraList)
                {
                    myCommand.Parameters.Add(par);
                }
                temp = Convert.ToInt32(myCommand.ExecuteScalar().ToString());
                myCommand.Dispose();
                myBase.CloseConnection(myConn);
                return temp;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + strSQL));
            }

        }

        public int ExecuteScalar(SqlCommand mCommand)
        {
            int temp = 0;
            DBAccess myBase = new DBAccess();
            try
            {
                SqlConnection myConn = myBase.OpenConnection(connectionString);
                mCommand.Connection = myConn;
                temp = Convert.ToInt32(mCommand.ExecuteScalar().ToString());
                mCommand.Dispose();
                myBase.CloseConnection(myConn);
                return temp;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + mCommand.CommandText));
            }

        }

        public int ExecuteScalar(string strSQL, SqlConnection mConn)
        {
            int temp = 0;
            try
            {
                SqlCommand myCom = new SqlCommand(strSQL, mConn);
                temp = Convert.ToInt32(myCom.ExecuteScalar().ToString());
                myCom.Dispose();
                return temp;
            }
            catch (Exception myException)
            {
                throw (new Exception(myException.Message + " => " + strSQL));
            }

        }

        public DataTable SelectDBRows(string query)
        {
            try
            {
                SqlConnection sqlcon = new SqlConnection(connectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                sqlcon.Open();
                da.SelectCommand = new SqlCommand(query, sqlcon);
                da.Fill(dt);
                sqlcon.Close();
                da.Dispose();
                sqlcon.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long GetNumber(string Query)
        {
            try
            {
                Object num;
                SqlConnection sqlcon = new SqlConnection(connectionString);
                SqlCommand Com = new SqlCommand(Query, sqlcon);
                sqlcon.Open();
                num = (int)Com.ExecuteScalar();
                sqlcon.Close();
                sqlcon.Dispose();
                Com.Dispose();
                return (long)num;
            }
            catch
            {
                return 0;
            }
        }
        public string GetString(string Query)
        {
            try
            {
                Object num;
                SqlConnection sqlcon = new SqlConnection(connectionString);
                SqlCommand Com = new SqlCommand(Query, sqlcon);
                sqlcon.Open();
                num = (string)Com.ExecuteScalar();
                sqlcon.Close();
                sqlcon.Dispose();
                Com.Dispose();
                return (string)num;
            }
            catch
            {
                return "";
            }
        }   
        #endregion
    }
}